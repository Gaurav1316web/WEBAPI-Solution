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

    Const colLevelNo As String = "LevelNo"
    Const colLevel_UserCode As String = "LevelUserCode"
    Const colLevel_UserName As String = "LevelUserName"
    Const colLevel_Department As String = "LevelDepartment"
    Const colLevel_DepartmentName As String = "LevelDepartmentName"
    Const colAmount_Qty As String = "Amount_Qty"
    Const colAmount_Qty_Type As String = "Amt_Qty_Type" 'min,max
#End Region
    

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
        repoTextbox.HeaderImage = Global.ERP.My.Resources.Resources.search4
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
        repoTextbox.HeaderImage = Global.ERP.My.Resources.Resources.search4
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

        dr = dt.NewRow()
        dr("Code") = "Amount"
        dr("Name") = "Amount Based"
        dt.Rows.Add(dr)

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

    Private Sub frmApprovalScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim OpenWorkFlowInERP As Boolean = clsCommon.myCBool(IIf(clsFixedParameter.GetData(clsFixedParameterType.WorkApprovalFlowInERP, clsFixedParameterCode.WorkApprovalFlowInERP, Nothing) = "1", True, False))
        If Not OpenWorkFlowInERP Then
            Throw New Exception("Unauthorized access of Work flow of Approval.")
            Me.Close()
        End If


        SetUserMgmtNew()
        LoadBlankGrid()
        LoadApprovalType()

        LoadModuleType()
        LoadTrnsListOfSelectedModeule()
        Reset()


        If clsCommon.myLen(ModuleName) > 0 Then
            cboModule.SelectedValue = ModuleName
            cboTransaction.SelectedValue = Transaction
        End If

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for save record.")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D for delete record.")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N for reset window.")
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
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow
        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleCommonServices
        'dr("Name") = "Common Services"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleReceivable
        'dr("Name") = "Receivables"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModulePayable
        'dr("Name") = "Payables"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleGL
        'dr("Name") = "General Ledger"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleSalesNew
        dr("Name") = "Standard Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleProductSale
        dr("Name") = "Product Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleSaleDairy
        dr("Name") = "Dairy Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleCSASale
        dr("Name") = "CSA Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleFreshSale
        dr("Name") = "Fresh Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleBulkSale
        dr("Name") = "Bulk Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleMerchantTradeSale
        dr("Name") = "Merchant Trade Sales"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleMCCMilkProcurement
        dr("Name") = "MCC Milk Procurement"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModuleBulkMilkProcurement
        dr("Name") = "Bulk Milk Procurement"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleMaterial
        'dr("Name") = "Material Management"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = clsUserMgtCode.ModulePurchase
        dr("Name") = "Purchase"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleTDS
        'dr("Name") = "Tax Deducted At Source"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleFixedAsset
        'dr("Name") = "Fixed Assets"
        'dt.Rows.Add(dr)

        ' '' NEW ADDED MODULES 
        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleHR
        'dr("Name") = "HR and Payroll"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleProductionSTD
        'dr("Name") = "Standard Production"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleProductionDairy
        'dr("Name") = "Dairy Production"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = clsUserMgtCode.ModuleService
        'dr("Name") = "Service Module"
        'dt.Rows.Add(dr)


        cboModule.DataSource = dt
        cboModule.DisplayMember = "Name"
        cboModule.ValueMember = "Code"
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
            Dim whrCls As String = " and Program_Code in ('PO-ODR','PO-REQ','PO-INV','SALN-SO','SALE-ORDER','SALN-SP','DEL-NOTE-FS','DEL-ORD-PS','DISPATCH-BS','M-Material','VSP-Item','SHIPMENT-PS','CSA-INV-TRN','M-SRN-B','M-RECEIPT','M-QC','LC-CREATION','BOOK-DS','CSA-DO-TRN','SALE-RET-PS','RETRUN-DS')"

            

            If Not clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleSaleDairy) = CompairStringResult.Equal Then ''std. sales
                If clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleBulkMilkProcurement) = CompairStringResult.Equal Then
                    qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Bulk Transaction')) " + whrCls + " "

                ElseIf clsCommon.CompairString(clsCommon.myCstr(cboModule.SelectedValue), clsUserMgtCode.ModuleMCCMilkProcurement) = CompairStringResult.Equal Then
                    qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('MCC Transaction')) " + whrCls + " "
                Else
                    qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                            "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Transaction')) " + whrCls + " "
                End If
            Else
                qry = "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                    "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Fresh Sale')) " + whrCls + "  " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Bulk Sale')) " + whrCls + " " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Product Sale','Product postdaturn')) " + whrCls + " " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Export Sale')) " + whrCls + " " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('CSA Sale')) " + whrCls + " " & _
                "union all " & _
                "select Program_Code AS Code, Program_Name as Name from tspl_program_master " & _
                "where Parent_Code=(select Program_Code from TSPL_PROGRAM_MASTER where Parent_Code='" + cboModule.SelectedValue.ToString() + "' and Program_Name in ('Transaction')) " + whrCls + " "
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

        ChkAutoPost.Checked = False
        ChkAutoPost.IsThreeState = False

        chk_isdepartmentwise.Checked = False
        chk_isdepartmentwise.IsThreeState = False

        gv1.Rows.Clear()
        'gv1.Rows.AddNew()

        btnSave.Text = "Save"
        btnDelete.Enabled = False
        btnSave.Enabled = True
        cboModule.Enabled = True
        cboTransaction.Enabled = True

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

            obj = clsApprovalScreen.GetData(ModuleName, ScreenName)
            gv1.Rows.Clear()
            'gv1.Rows.AddNew()
            If obj IsNot Nothing AndAlso obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                cboModule.SelectedValue = obj.Arr(0).Module_Code
                cboTransaction.SelectedValue = obj.Arr(0).Trans_Code
                txtLevelNo.Text = obj.Arr(0).Level
                cboApprovalType.SelectedValue = obj.Arr(0).Approval_Type
                ChkAutoPost.Checked = IIf(obj.Arr(0).Auto_Post = True, True, False)
                chk_isdepartmentwise.Checked = IIf(obj.Arr(0).Is_DepartmentWise = True, True, False)

                For Each obj1 As clsApprovalScreen In obj.Arr
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevelNo).Value = obj1.No_Of_Level
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserCode).Value = obj1.User_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_UserName).Value = obj1.User_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_Department).Value = obj1.Department_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLevel_DepartmentName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description as Name from TSPL_GL_SEGMENT_CODE where Seg_No=3 and Segment_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLevel_Department).Value) + "'", Nothing))
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
                    If Not arr.Contains(user_Code) Then
                        arr.Add(user_Code)
                    Else
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colLevel_UserCode)
                        Throw New Exception("Duplicate user mapped at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If chk_isdepartmentwise.Checked Then
                        If clsCommon.myLen(Department) <= 0 Then
                            gv1.CurrentRow = gv1.Rows(grow.Index)
                            gv1.CurrentColumn = gv1.Columns(colLevel_Department)
                            Throw New Exception("Fill Department at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                        End If
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(cboApprovalType.SelectedValue), "Amount") = CompairStringResult.Equal AndAlso clsCommon.myLen(app_type) <= 0 AndAlso amount <= 0 Then
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colAmount_Qty)
                        Throw New Exception("Fill " + clsCommon.myCstr(cboApprovalType.SelectedValue) + " limit at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(cboApprovalType.SelectedValue), "Amount") = CompairStringResult.Equal AndAlso clsCommon.myLen(app_type) > 0 AndAlso amount <= 0 Then
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colAmount_Qty)
                        Throw New Exception("Fill " + clsCommon.myCstr(cboApprovalType.SelectedValue) + " limit at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If

                    If clsCommon.CompairString(clsCommon.myCstr(cboApprovalType.SelectedValue), "Amount") = CompairStringResult.Equal AndAlso clsCommon.myLen(app_type) <= 0 AndAlso amount > 0 Then
                        gv1.CurrentRow = gv1.Rows(grow.Index)
                        gv1.CurrentColumn = gv1.Columns(colAmount_Qty_Type)
                        Throw New Exception("Select " + clsCommon.myCstr(cboApprovalType.SelectedValue) + " type at row no. " + clsCommon.myCstr(grow.Index + 1) + ".")
                    End If
                End If ''end no of level
            Next

            If arr Is Nothing OrElse arr.Count <= 0 Then
                Throw New Exception("No record found.")
            End If

            If clsCommon.myCdbl(txtLevelNo.Text) <> max_no_of_level Then
                Throw New Exception("Set approval level are not match with grid level.")
            End If

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

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

                obj1.User_Code = clsCommon.myCstr(grow.Cells(colLevel_UserCode).Value)
                obj1.No_Of_Level = clsCommon.myCdbl(grow.Cells(colLevelNo).Value)
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

            If Not clsCommon.MyMessageBoxShow("Are you sure,want to delete setting.", "Attention", MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            If clsApprovalScreen.DeleteData(clsCommon.myCstr(cboModule.SelectedValue), clsCommon.myCstr(cboTransaction.SelectedValue)) Then
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
                        isCellValueChanged = True
                        OpenUserCode(False)
                        isCellValueChanged = False
                    ElseIf e.Column Is gv1.Columns(colLevel_Department) Then
                        isCellValueChanged = True
                        OpenDepartment(False)
                        isCellValueChanged = False
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
            If clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "SALN-SP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "M-Material") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "PO-ODR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "PO-REQ") OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "PO-INV") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboTransaction.SelectedValue), "DEL-ORD-PS") = CompairStringResult.Equal Then
                chk_isdepartmentwise.Visible = True
                chk_isdepartmentwise.Checked = False
            Else
                chk_isdepartmentwise.Visible = False
                chk_isdepartmentwise.Checked = False
            End If
            gridcolvisible()
            LoadApprovalType()
        End If
    End Sub

    Sub showapprovaltype()
        
    End Sub
End Class




