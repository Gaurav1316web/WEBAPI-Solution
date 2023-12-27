Imports common
Public Class frmCustomFieldMapping
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Const colCustomFieldCode As String = "colCustomFieldCode"
    Const colCustomFieldName As String = "colCustomFieldName"
    Const colIsMandatory As String = "colIsMandatory"
    Const colDefaultValue As String = "colDefaultValue"
    Const colIsValidate As String = "colIsValidate"
    Const colType As String = "colType"
    Const colisForDetailLevel As String = "colisForDetailLevel"
    Const colisForPrint As String = "colisForPrint"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim repoIsForDetailLevel As GridViewCheckBoxColumn = Nothing
#End Region

    Private Sub FrmCustomFieldMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        AddNew()
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomFieldMapping)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub AddNew()
        txtModule.Value = ""
        txtScreen.Value = ""
        LoadBlankGrid()
        'gv1.Rows.AddNew()
        btnSave.Text = "Save"
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Custom Field Code"
        repoICode.Name = colCustomFieldCode
        repoICode.HeaderImage = Global.XpertERPCommanServices.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 170
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Description"
        repoIName.Name = colCustomFieldName
        repoIName.Width = 250
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoMandatory As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoMandatory.HeaderText = "Mandatory"
        repoMandatory.Name = colIsMandatory
        repoMandatory.ReadOnly = False
        repoMandatory.IsVisible = True
        repoMandatory.Width = 80
        repoMandatory.ReadOnly = True
        repoMandatory.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoMandatory)

        Dim repoVendroItemNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoVendroItemNo = New GridViewTextBoxColumn()
        repoVendroItemNo.FormatString = ""
        repoVendroItemNo.HeaderText = "Default Value"
        repoVendroItemNo.Name = colDefaultValue
        repoVendroItemNo.Width = 250
        repoVendroItemNo.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoVendroItemNo)

        Dim repoIsValidate As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsValidate.HeaderText = "Is Validate"
        repoIsValidate.Name = colIsValidate
        repoIsValidate.ReadOnly = True
        repoIsValidate.IsVisible = False
        repoIsValidate.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsValidate.Width = 100
        repoIsValidate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIsValidate)

        repoIsForDetailLevel = New GridViewCheckBoxColumn()
        repoIsForDetailLevel.HeaderText = "Is For Detail Level"
        repoIsForDetailLevel.Name = colisForDetailLevel
        repoIsForDetailLevel.ReadOnly = False
        repoIsForDetailLevel.IsVisible = True
        repoIsForDetailLevel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsForDetailLevel.ReadOnly = True
        repoIsForDetailLevel.Width = 150

        Dim repoIsForPrint As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoIsForPrint.HeaderText = "Is For Print"
        repoIsForPrint.Name = colisForPrint
        repoIsForPrint.ReadOnly = True
        repoIsForPrint.IsVisible = True
        repoIsForPrint.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        repoIsForPrint.Width = 150
        gv1.MasterTemplate.Columns.Add(repoIsForPrint)

        If IsSetup(Me.txtScreen.Value) = True Then
            repoIsForDetailLevel.IsVisible = False
        End If
        gv1.MasterTemplate.Columns.Add(repoIsForDetailLevel)
        
        

        Dim repoType As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoType.FormatString = ""
        repoType.HeaderText = "Type"
        repoType.Name = colType
        repoType.IsVisible = False
        repoType.ReadOnly = True
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoType)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub chkValidate_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
        LoadBlankGrid()
        gv1.Rows.AddNew()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub SaveData()
        Try
            
            If (AllowToSave()) Then
                If MyBase.isModifyonPasswordFlag Then
                    If clsPasswordCheckForMasters.CheckMasterPwd("", clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                    Else
                        Return
                    End If
                End If
                Dim Arr As New List(Of clsCustomFieldMapping)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsCustomFieldMapping()
                    objTr.Custom_Field_Code = clsCommon.myCstr(grow.Cells(colCustomFieldCode).Value)
                    objTr.Is_Mandatory = clsCommon.myCBool(grow.Cells(colIsMandatory).Value)
                    objTr.Default_Value = clsCommon.myCstr(grow.Cells(colDefaultValue).Value)
                    objTr.Is_For_Detail_Level = clsCommon.myCBool(grow.Cells(colisForDetailLevel).Value)
                    objTr.Is_For_Print = clsCommon.myCBool(grow.Cells(colisForPrint).Value)
                    If clsCommon.myLen(objTr.Custom_Field_Code) > 0 Then
                        Arr.Add(objTr)
                    End If
                Next
                If (clsCustomFieldMapping.SaveData(txtScreen.Value, Arr)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                    LoadData()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData()
        Try
            If clsCommon.myLen(txtScreen.Value) <= 0 Then
                txtScreen.Focus()
                clsCommon.MyMessageBoxShow(Me, "Please first select screen", Me.Text)
                Exit Sub
            End If

            isInsideLoadData = True
            LoadBlankGrid()
            Dim Arr As List(Of clsCustomFieldMapping) = clsCustomFieldMapping.GetData(txtScreen.Value, "")
            If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
                    For Each objTr As clsCustomFieldMapping In Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomFieldCode).Value = objTr.Custom_Field_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCustomFieldName).Value = objTr.Custom_Field_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsMandatory).Value = objTr.Is_Mandatory
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDefaultValue).Value = objTr.Default_Value
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIsValidate).Value = objTr.Is_Validate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colisForDetailLevel).Value = objTr.Is_For_Detail_Level
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colisForPrint).Value = objTr.Is_For_Print
                    Next
                End If
            End If
            gv1.Rows.AddNew()
            EnableDisableControl(False)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Function IsSetup(ByVal formId As String) As Boolean
        Dim strq As String = "select program_code from TSPL_PROGRAM_MASTER where Parent_Code in (select Program_Code from TSPL_PROGRAM_MASTER where Program_Name='Setup') and program_code='" & formId & "'"
        Dim dt As DataTable
        dt = clsDBFuncationality.GetDataTable(strq)
        If dt.Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtScreen.Value) <= 0 Then
                txtScreen.Focus()
                Throw New Exception("Please select Name of the Screen.")
            End If

            If gv1.Rows.Count <= 0 Then
                gv1.Focus()
                Throw New Exception("Please select at least one item in grid.")
            End If

            Dim RowCount As Integer = 0
            For ii As Integer = 0 To gv1.Rows.Count - 1
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If jj = ii Then
                        Continue For
                    End If
                    If clsCommon.myLen(gv1.Rows(ii).Cells(colCustomFieldCode).Value) > 0 AndAlso clsCommon.myLen(gv1.Rows(jj).Cells(colCustomFieldCode).Value) > 0 Then
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(ii).Cells(colCustomFieldCode).Value), clsCommon.myCstr(gv1.Rows(jj).Cells(colCustomFieldCode).Value)) = CompairStringResult.Equal AndAlso (clsCommon.myCBool(gv1.Rows(ii).Cells(colisForDetailLevel).Value) = clsCommon.myCBool(gv1.Rows(jj).Cells(colisForDetailLevel).Value)) Then
                            Dim Msg As String = " Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
                            Msg = Msg + Environment.NewLine + "Item: " + clsCommon.myCstr(gv1.Rows(ii).Cells(colCustomFieldCode).Value)
                            common.clsCommon.MyMessageBoxShow(Msg)
                            Return False
                        End If
                    End If
                Next
                If clsCommon.myLen(gv1.Rows(ii).Cells(colCustomFieldCode).Value) > 0 Then
                    RowCount += 1
                End If
            Next
            If RowCount <= 0 Then
                gv1.Focus()
                Throw New Exception("Please select at least one item in grid.")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function

    Private Sub FrmCustomFieldMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    'Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
    '    If Not isInsideLoadData Then
    '        If Not isCellValueChangedOpen Then
    '            isCellValueChangedOpen = True
    '            If e.Column Is gv1.Columns(colCustomFieldCode) Then
    '                OpenCustomFieldList(False)
    '            ElseIf e.Column Is gv1.Columns(colDefaultValue) Then
    '                If clsCommon.myCBool(gv1.CurrentRow.Cells(colIsValidate).Value) Then
    '                    OpenFinderForValue(False)
    '                ElseIf gv1.CurrentRow.Cells(colType).Value = EnumCustomFieldType.NumberType Then
    '                    gv1.CurrentRow.Cells(colDefaultValue).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colDefaultValue).Value)
    '                ElseIf gv1.CurrentRow.Cells(colType).Value = EnumCustomFieldType.DateType Then
    '                    gv1.CurrentRow.Cells(colDefaultValue).Value = clsCommon.myCDate(gv1.CurrentRow.Cells(colDefaultValue).Value)
    '                ElseIf gv1.CurrentRow.Cells(colType).Value = EnumCustomFieldType.CheckType Then
    '                    gv1.CurrentRow.Cells(colDefaultValue).Value = clsCommon.myCBool(gv1.CurrentRow.Cells(colDefaultValue).Value)
    '                Else
    '                    ''By default it is of string 
    '                End If
    '            End If
    '            isCellValueChangedOpen = False
    '        End If
    '    End If
    'End Sub


    Sub OpenFinderForValue(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Value from TSPL_CUSTOM_FIELD_DETAIL"
        Dim whrCls As String = "Custom_Field_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colCustomFieldCode).Value) + "'"
        gv1.CurrentRow.Cells(colDefaultValue).Value = clsCommon.ShowSelectForm("cumFMDetaolValueList", qry, "Value", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colDefaultValue).Value), "", isButtonClick)
    End Sub

    Sub OpenCustomFieldList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Code,Name from TSPL_CUSTOM_FIELD_HEAD"
        Dim whrCls As String = ""
        gv1.CurrentRow.Cells(colCustomFieldCode).Value = clsCommon.ShowSelectForm("cumFMList", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colCustomFieldCode).Value), "Code", isButtonClick)
        Dim obj As clsCustomFieldHead = clsCustomFieldHead.GetData(clsCommon.myCstr(gv1.CurrentRow.Cells(colCustomFieldCode).Value), NavigatorType.Current, Nothing)
        If obj IsNot Nothing Then
            gv1.CurrentRow.Cells(colCustomFieldName).Value = obj.Name
            gv1.CurrentRow.Cells(colIsValidate).Value = obj.Is_Validate
            gv1.CurrentRow.Cells(colType).Value = obj.Type
        Else
            gv1.CurrentRow.Cells(colCustomFieldName).Value = ""
            gv1.CurrentRow.Cells(colIsValidate).Value = False
            gv1.CurrentRow.Cells(colType).Value = 0
        End If
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        LoadData()
    End Sub

    Sub EnableDisableControl(ByVal val As Boolean)
        txtModule.Enabled = val
        txtScreen.Enabled = val
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        AddNew()
        EnableDisableControl(True)
    End Sub

    Private Sub txtModule__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtModule._MYValidating
        Dim qry As String = "Select Program_Code as Code, [Program_Name] as Description from TSPL_PROGRAM_MASTER"
        Dim WhrCls As String = " Type='M'"
        If objCommonVar.IsDemoERP Then
            WhrCls += " and Program_Code not in ('MSales')"
        Else
            WhrCls += " and Program_Code not in ('MSalesNew')"
        End If
        WhrCls += " AND Program_Code NOT IN ('MFavourite','MFixedAsset','MHR','ModuleBI','MProduction','MSysAdmin','MTDS','MUtility')"
        txtModule.Value = clsCommon.ShowSelectForm("CusFielMapModule", qry, "Code", WhrCls, txtModule.Value, "SNo", isButtonClicked)
        lblModule.Text = txtModule.Value
        txtScreen.Value = ""
        lblScreen.Text = ""
    End Sub

    Private Sub txtScreen__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtScreen._MYValidating
        If clsCommon.myLen(txtModule.Value) <= 0 Then
            txtModule.Focus()
            clsCommon.MyMessageBoxShow(Me, "Please first select module", Me.Text)
            Exit Sub
        End If
        Dim arrDoneScreen As New List(Of String)()
        ''Purchare Transactions
        arrDoneScreen.Add(clsUserMgtCode.mbtnPurchaseRequistion)
        arrDoneScreen.Add(clsUserMgtCode.VendorQuotation)
        arrDoneScreen.Add(clsUserMgtCode.mbtnSRN)
        arrDoneScreen.Add(clsUserMgtCode.SRNReturn)
        arrDoneScreen.Add(clsUserMgtCode.mbtnPurchaseOrder)
        arrDoneScreen.Add(clsUserMgtCode.mbtnPurchaseReturn)
        arrDoneScreen.Add(clsUserMgtCode.mbtnPurchaseInvoice)
        arrDoneScreen.Add(clsUserMgtCode.mbtnGatePass)
        arrDoneScreen.Add(clsUserMgtCode.mbtnIssueReturn)
        arrDoneScreen.Add(clsUserMgtCode.ScrapSale)

        '--Inventory---
        arrDoneScreen.Add(clsUserMgtCode.locationMaster)
        arrDoneScreen.Add(clsUserMgtCode.Transfer)

        ''sales Transactions
        arrDoneScreen.Add(clsUserMgtCode.frmSNSalesOrder)
        arrDoneScreen.Add(clsUserMgtCode.frmSaleQuotation)
        arrDoneScreen.Add(clsUserMgtCode.frmSNShipment)
        arrDoneScreen.Add(clsUserMgtCode.frmSNSaleInvoice)
        arrDoneScreen.Add(clsUserMgtCode.frmSNSaleReturn)

        '' masters
        arrDoneScreen.Add(clsUserMgtCode.CustomerMaster)
        arrDoneScreen.Add(clsUserMgtCode.FrmItemMasterRMOther)
        arrDoneScreen.Add("VENDOR-M")

        ''AP Invoice 
        arrDoneScreen.Add(clsUserMgtCode.mbtnAPInvoiceEntry)
        arrDoneScreen.Add(clsUserMgtCode.PaymentEntryNew)

        ''Ar Invoice
        arrDoneScreen.Add(clsUserMgtCode.mbtnARInvoiceEntry)
        arrDoneScreen.Add(clsUserMgtCode.ReceiptEntry)
        arrDoneScreen.Add(clsUserMgtCode.frmTaskMaster)
        arrDoneScreen.Add(clsUserMgtCode.ShiptoLocation)


        'arrDoneScreen.Add(clsUserMgtCode.frmShipToLocationDetails)
        '---Project Management--------------
        arrDoneScreen.Add(clsUserMgtCode.frmPJCSettings)
        arrDoneScreen.Add(clsUserMgtCode.frmCostTypes)
        arrDoneScreen.Add(clsUserMgtCode.frmPJCAccountSets)
        arrDoneScreen.Add(clsUserMgtCode.frmJobMaster)
        arrDoneScreen.Add(clsUserMgtCode.frmTaskMaster)
        arrDoneScreen.Add(clsUserMgtCode.frmPJCEmployeeMaster)
        arrDoneScreen.Add(clsUserMgtCode.FrmUserApproval)
        arrDoneScreen.Add(clsUserMgtCode.FrmBudgetMaintenance)
        arrDoneScreen.Add(clsUserMgtCode.ProjectMaster)
        arrDoneScreen.Add(clsUserMgtCode.FrmExpenseType)
        arrDoneScreen.Add(clsUserMgtCode.FrmProjectStatus)
        arrDoneScreen.Add(clsUserMgtCode.frmTimeSheet)
        arrDoneScreen.Add(clsUserMgtCode.frmUserLog)
        arrDoneScreen.Add(clsUserMgtCode.frmAssemblies)
        arrDoneScreen.Add(clsUserMgtCode.FrmPJCExpense)
        '--------------------------- Service Module --------------
        arrDoneScreen.Add(clsUserMgtCode.frmAssetDistatch)
        '============Added By Rohit on 02,March For Mcc Procurement (VLC,MCC,VSP,ROUTE,RECEIPT,Sample,Shift_END,Open_Shift)===============
        arrDoneScreen.Add(clsUserMgtCode.frmVLCMaster)
        arrDoneScreen.Add(clsUserMgtCode.frmMCCMaster)
        arrDoneScreen.Add(clsUserMgtCode.frmMilkRouteMaster)
        arrDoneScreen.Add(clsUserMgtCode.frmVSPMaster)
        arrDoneScreen.Add(clsUserMgtCode.frmOpenMCCShift)
        arrDoneScreen.Add(clsUserMgtCode.frmMilkReceipt)
        arrDoneScreen.Add(clsUserMgtCode.frmMilkSample)
        arrDoneScreen.Add(clsUserMgtCode.frmMilkShiftEndMCC)
        arrDoneScreen.Add(clsUserMgtCode.frmMPMaster)
        '======================================================================================================================
        'Added By Pankaj Jha For Custom Fields on Bulk Procurement Module
        arrDoneScreen.Add(clsUserMgtCode.frmMCCDispatch)
        '========================================================================
        'Added By Panch Raj For Custom Fields on Item Structure
        arrDoneScreen.Add(clsUserMgtCode.itemStructure)
        'Added By Panch Raj For Custom Fields on Customer group
        arrDoneScreen.Add(clsUserMgtCode.CustomerGroup)
        '========================================================================
        '=================export=============
        arrDoneScreen.Add(clsUserMgtCode.frmEXSalesQuotation)
        arrDoneScreen.Add(clsUserMgtCode.frmEXSalesOrder)
        arrDoneScreen.Add(clsUserMgtCode.frmEXPorformaInvoice)
        arrDoneScreen.Add(clsUserMgtCode.frmEXCommercialInvoice)
        arrDoneScreen.Add(clsUserMgtCode.frmEXSalesInvoice)
        arrDoneScreen.Add(clsUserMgtCode.frmEXSalesReturn)
        arrDoneScreen.Add(clsUserMgtCode.frmSalesOrderMT)
        arrDoneScreen.Add(clsUserMgtCode.frmProformaInvoiceMT)
        arrDoneScreen.Add(clsUserMgtCode.frmCommercialInvoiceMT)
        arrDoneScreen.Add(clsUserMgtCode.frmSalesInvoiceMT)




        Dim qry As String = "Select Program_Code as Code, [Program_Name] as Description from TSPL_PROGRAM_MASTER"
        Dim WhrCls As String = "Parent_Code in (Select Program_Code from TSPL_PROGRAM_MASTER Where Parent_Code='" + txtModule.Value + "') AND Program_Code in (" + clsCommon.GetMulcallString(arrDoneScreen) + ") "

        txtScreen.Value = clsCommon.ShowSelectForm("VendorMasteidfndr", qry, "Code", WhrCls, txtScreen.Value, "SNo", isButtonClicked)
        lblScreen.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Program_Name from TSPL_PROGRAM_MASTER where Program_Code ='" + txtScreen.Value + "'"))
        DeactiveDetailLevelChkBox()
    End Sub

    '----------------------17/04/2014-----------------for CEECO(BM00000001934)
    Sub DeactiveDetailLevelChkBox()
        Try
            Dim qry As String = "select count(*) from TSPL_PROGRAM_MASTER where Program_Code='" + txtScreen.Value + "' and Parent_Code like '%setup%'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                repoIsForDetailLevel.IsVisible = False
            Else
                repoIsForDetailLevel.IsVisible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        If gv1.RowCount > 0 Then
            If e.RowIndex >= 0 Then
                If e.ColumnIndex >= 0 Then
                    If clsCommon.myLen(gv1.Rows(e.RowIndex).Cells(colCustomFieldCode).Value) > 0 Then
                        Try
                            Dim frm As New frmCustomFieldEditor
                            frm.formId = txtScreen.Value
                            frm.isNewEntry = False
                            frm.CustomFieldCodeToLoad = gv1.Rows(e.RowIndex).Cells(colCustomFieldCode).Value
                            frm.ShowDialog()
                            frm = Nothing
                            GC.Collect()
                            GC.WaitForPendingFinalizers()
                        Catch ex As Exception
                            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                        End Try
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged

    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        'If gv1.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '    If intCurrRow = gv1.Rows.Count - 1 Then
        '        gv1.Rows.AddNew()
        '        gv1.CurrentRow = gv1.Rows(intCurrRow)
        '    End If
        'End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        'If clsCommon.myLen(txtScreen.Value) > 0 Then
        '    If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
        '        e.Cancel = True
        '        Exit Sub
        '    Else
        '        Dim qry As String = " Select count(*) from TSPL_CUSTOM_FIELD_VALUES where Custom_Field_Code= '" + gv1.CurrentRow.Cells(colCustomFieldCode).Value + "' and Program_Code='" + txtScreen.Value + "' "
        '        If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Nothing)) > 0 Then
        '            clsCommon.MyMessageBoxShow("Current File is in Use.")
        '            e.Cancel = True
        '            Exit Sub
        '        Else
        '            qry = " delete from TSPL_CUSTOM_FIELD_MAPPING where Program_Code='" + txtScreen.Value + "' and Custom_Field_Code= '" + gv1.CurrentRow.Cells(colCustomFieldCode).Value + "'"
        '            clsDBFuncationality.ExecuteNonQuery(qry, Nothing)
        '        End If
        '    End If
        'End If
    End Sub

    

    Private Sub btnAddNewField_Click(sender As Object, e As EventArgs) Handles btnAddNewField.Click
        Try
            If clsCommon.myLen(txtScreen.Value) <= 0 Then
                Throw New Exception("Please select Screen First")
            End If
            Dim frm As New frmCustomFieldEditor
            frm.formId = txtScreen.Value
            frm.isNewEntry = True

            frm.ShowDialog()
            frm = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
