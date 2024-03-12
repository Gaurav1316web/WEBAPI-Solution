Imports common
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine


' Ticket No : ERO/24/10/18-000411 By Prabhakar

Public Class frmDailyElectricalEntry
    Inherits FrmMainTranScreen

#Region "Varibales"
    Dim isCellValueChangedOpen As Boolean = False
    Dim isCellValueChangedOpen2 As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isNewEntery As Boolean = True
    Public Const colSlotSNo As String = "Slot SNO"
    Public Const colSlotcode As String = "Slot Code"
    Public Const colSlotDesc As String = "Slot Desc"
    Public Const colslotUnit As String = "Slot Unit"
    ' colSlotSNo , colSlotcode , colSlotDesc , colslotUnit
    ' colDGCode , colDGName , colDGUnit , colDGConsumption , colDGRuningHours
    Public Const colDGSNo As String = "DG SNO"
    Public Const colDGCode As String = "DG Code"
    Public Const colDGName As String = "DG Name"
    Public Const colDGUnit As String = "DG Unit"
    Public Const colDGConsumption As String = "DG Consumption"
    Public Const colDGRuningHours As String = "DG Ruing Hours"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsDailyElectricalEntryHead = Nothing
#End Region

    Private Sub FrmJobMilkSRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If

    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnReverse.Visible = MyBase.isPostFlag
        'btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Sub Reset()
        BlankAllControl()
        isNewEntery = True
        btnSave.Enabled = True
        'btnPrint.Enabled = False
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSave.Text = "Save"
        txtDocNo.MyReadOnly = False
        lblPending.Status = ERPTransactionStatus.Pending
        isCellValueChangedOpen = False
        isCellValueChangedOpen2 = False
        txtRemarks.Text = ""
        lblTotalEBUnit.Text = "0"
        txtConsumptionDate.Value = Nothing
    End Sub

    Sub BlankAllControl()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy") 'clsCommon.GETSERVERDATE()
        loadSlotBlankItemGrid()
        loadDGBlankItemGrid()
    End Sub

    Sub loadSlotBlankItemGrid()
        gvSlot.Rows.Clear()
        gvSlot.Columns.Clear()

        ' colSlotSNo , colSlotcode , colSlotDesc , colslotUnit
        ' colDGCode , colDGName , colDGUnit , colDGConsumption , colDGRuningHours

        gvSlot.Columns.Add(colSlotSNo, "SNo")
        gvSlot.Columns(colSlotSNo).Width = 50
        gvSlot.Columns(colSlotSNo).ReadOnly = True

        'gvSlot.Columns.Add(colSlotcode, "Slot code")
        'gvSlot.Columns(colSlotcode).Width = 100
        'gvSlot.Columns(colSlotcode).ReadOnly = False

        Dim SlotCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SlotCode.FormatString = ""
        SlotCode.HeaderText = "Slot code"
        SlotCode.Name = colSlotcode
        SlotCode.Width = 130
        SlotCode.ReadOnly = False
        SlotCode.HeaderImage = My.Resources.search4
        SlotCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gvSlot.MasterTemplate.Columns.Add(SlotCode)




        gvSlot.Columns.Add(colSlotDesc, "Slot Desc")
        gvSlot.Columns(colSlotDesc).Width = 120
        gvSlot.Columns(colSlotDesc).ReadOnly = True

        'gvSlot.Columns.Add(colslotUnit, "Unit")
        'gvSlot.Columns(colslotUnit).Width = 100
        'gvSlot.Columns(colslotUnit).ReadOnly = False
        'gvSlot.Columns(colslotUnit).TextAlignment = ContentAlignment.MiddleRight

        Dim SlotUnit As GridViewDecimalColumn = New GridViewDecimalColumn()
        SlotUnit.FormatString = ""
        SlotUnit.HeaderText = "Unit"
        SlotUnit.Name = colslotUnit
        SlotUnit.Width = 100
        SlotUnit.ReadOnly = False
        gvSlot.MasterTemplate.Columns.Add(SlotUnit)



        gvSlot.AllowAddNewRow = False
        gvSlot.AllowDeleteRow = True
        gvSlot.AllowRowReorder = False
        gvSlot.ShowGroupPanel = False
        gvSlot.EnableFiltering = False
        gvSlot.EnableSorting = False
        gvSlot.EnableGrouping = False
        gvSlot.AllowColumnChooser = True
        gvSlot.AllowColumnReorder = True
        'ReStoreGridLayout()
        gvSlot.Rows.AddNew()
    End Sub

    Sub loadDGBlankItemGrid()
        gvDG.Rows.Clear()
        gvDG.Columns.Clear()

        ' colSlotSNo , colSlotcode , colSlotDesc , colslotUnit
        ' colDGSNo , colDGCode , colDGName , colDGUnit , colDGConsumption , colDGRuningHours

        gvDG.Columns.Add(colDGSNo, "SNo")
        gvDG.Columns(colDGSNo).Width = 50
        gvDG.Columns(colDGSNo).ReadOnly = True

        'gvDG.Columns.Add(colDGCode, "DG code")
        'gvDG.Columns(colDGCode).Width = 100
        'gvDG.Columns(colDGCode).ReadOnly = False

        Dim DGCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DGCode.FormatString = ""
        DGCode.HeaderText = "DG code"
        DGCode.Name = colDGCode
        DGCode.Width = 100
        DGCode.ReadOnly = False
        DGCode.HeaderImage = My.Resources.search4
        DGCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gvDG.MasterTemplate.Columns.Add(DGCode)

        gvDG.Columns.Add(colDGName, "DG Name")
        gvDG.Columns(colDGName).Width = 120
        gvDG.Columns(colDGName).ReadOnly = True

        'gvDG.Columns.Add(colDGUnit, "DG Unit")
        'gvDG.Columns(colDGUnit).Width = 100
        'gvDG.Columns(colDGUnit).ReadOnly = False
        'gvDG.Columns(colDGUnit).TextAlignment = ContentAlignment.MiddleRight

        Dim DGUnit As GridViewDecimalColumn = New GridViewDecimalColumn()
        DGUnit.FormatString = ""
        DGUnit.HeaderText = "DG Unit"
        DGUnit.Name = colDGUnit
        DGUnit.Width = 100
        DGUnit.ReadOnly = False
        gvDG.MasterTemplate.Columns.Add(DGUnit)



        'gvDG.Columns.Add(colDGConsumption, "Diesel Consumption")
        'gvDG.Columns(colDGConsumption).Width = 100
        'gvDG.Columns(colDGConsumption).ReadOnly = False
        'gvDG.Columns(colDGConsumption).TextAlignment = ContentAlignment.MiddleRight


        Dim DGConsumption As GridViewDecimalColumn = New GridViewDecimalColumn()
        DGConsumption.FormatString = ""
        DGConsumption.HeaderText = "Diesel Consumption"
        DGConsumption.Name = colDGConsumption
        DGUnit.Width = 100
        DGConsumption.ReadOnly = False
        gvDG.MasterTemplate.Columns.Add(DGConsumption)

        'gvDG.Columns.Add(colDGRuningHours, "Runing Hours")
        'gvDG.Columns(colDGRuningHours).Width = 100
        'gvDG.Columns(colDGRuningHours).ReadOnly = False
        'gvDG.Columns(colDGRuningHours).TextAlignment = ContentAlignment.MiddleRight

        Dim DGRuningHours As GridViewDecimalColumn = New GridViewDecimalColumn()
        DGRuningHours.FormatString = ""
        DGRuningHours.HeaderText = "Runing Hours"
        DGRuningHours.Name = colDGRuningHours
        DGRuningHours.Width = 100
        DGRuningHours.ReadOnly = False
        gvDG.MasterTemplate.Columns.Add(DGRuningHours)

        gvDG.AllowAddNewRow = False
        gvDG.AllowDeleteRow = True
        gvDG.AllowRowReorder = False
        gvDG.ShowGroupPanel = False
        gvDG.EnableFiltering = False
        gvDG.EnableSorting = False
        gvDG.EnableGrouping = False
        gvDG.AllowColumnChooser = True
        gvDG.AllowColumnReorder = True
        gvDG.Rows.AddNew()
        'ReStoreGridLayout()
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Try
            AllowToSave()
            obj = New clsDailyElectricalEntryHead()
            obj.Document_No = txtDocNo.Value
            obj.Document_Date = txtDate.Value
            obj.Consumption_Date = txtConsumptionDate.Value
            obj.Remarks = txtRemarks.Text
            
            obj.Arr = New List(Of clsDailyElectricalEntrySlotDetails)
            For ii As Integer = 0 To gvSlot.RowCount - 1
                Dim objtr As New clsDailyElectricalEntrySlotDetails
                objtr.SNo = clsCommon.myCdbl(gvSlot.Rows(ii).Cells(colSlotSNo).Value)
                objtr.Slot_Code = clsCommon.myCstr(gvSlot.Rows(ii).Cells(colSlotcode).Value)
                objtr.Slot_Unit = clsCommon.myCdbl(gvSlot.Rows(ii).Cells(colslotUnit).Value)
                If clsCommon.myLen(objtr.Slot_Code) > 0 Then
                    obj.Arr.Add(objtr)
                End If
            Next

            obj.ArrDG = New List(Of clsDailyElectricalEntryDGDetails)
            For ii As Integer = 0 To gvDG.RowCount - 1
                Dim objtrDG As New clsDailyElectricalEntryDGDetails
                objtrDG.SNo = clsCommon.myCdbl(gvDG.Rows(ii).Cells(colDGSNo).Value)
                objtrDG.DG_Code = clsCommon.myCstr(gvDG.Rows(ii).Cells(colDGCode).Value)
                objtrDG.DG_Unit = clsCommon.myCdbl(gvDG.Rows(ii).Cells(colDGUnit).Value)
                objtrDG.DG_Consumption = clsCommon.myCdbl(gvDG.Rows(ii).Cells(colDGConsumption).Value)
                objtrDG.DG_Runing_Hours = clsCommon.myCdbl(gvDG.Rows(ii).Cells(colDGRuningHours).Value)
                If clsCommon.myLen(objtrDG.DG_Code) > 0 Then
                    obj.ArrDG.Add(objtrDG)
                End If
            Next

            If clsDailyElectricalEntryHead.SaveData(obj, isNewEntery) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.Document_No, NavigatorType.Current)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(txtLocation.Value) <= 0 Then
            '    txtLocation.Focus()
            '    Throw New Exception("Please Select location")
            'End If
            'If clsCommon.myLen(txtJobLocation.Value) <= 0 Then
            '    txtJobLocation.Focus()
            '    Throw New Exception("Please Select Job Work location")
            'End If
            'If clsCommon.myLen(txtVendor.Value) <= 0 Then
            '    txtVendor.Focus()
            '    Throw New Exception("Please Select Vendor")
            'End If
            'If clsCommon.myLen(txtVehicleNo.Value) <= 0 Then
            '    txtVehicleNo.Focus()
            '    Throw New Exception("Please Select vehicel No /Tanker No")
            'End If
            'If clsCommon.myLen(txtGateEntryNo.Text) <= 0 Then
            '    txtGateEntryNo.Focus()
            '    Throw New Exception("Please Enter Gate entry No")
            'End If
            If clsCommon.myLen(txtConsumptionDate.Text) <= 0 Then
                txtConsumptionDate.Focus()
                Throw New Exception("Please select Consumption Date")
            End If
            For ii As Integer = 0 To gvSlot.RowCount - 1
                'UpdateCurrentRow(ii)
                If clsCommon.myLen(gvSlot.Rows(ii).Cells(colSlotcode).Value) > 0 Then
                    
                    If clsCommon.myLen(gvSlot.Rows(ii).Cells(colslotUnit).Value) <= 0 Then
                        Throw New Exception("Slot Unit is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    End If
                End If
                '    If clsCommon.myLen(gvISlot.Rows(ii).Cells(colPriceCode).Value) <= 0 Then
                '        Throw New Exception("Error at row no-" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " job work charges not found")
                '    End If
                '    If clsCommon.myCdbl(gvISlot.Rows(ii).Cells(colQty).Value) <= 0 Then
                '        Throw New Exception("Error at row no-" + clsCommon.myCstr(ii + 1) + Environment.NewLine + " Quantity should be greater than zero")
                '    End If
                '    If clsCommon.myCdbl(gvISlot.Rows(ii).Cells(colRate).Value) <= 0 Then
                '        Throw New Exception(" Please enter Rate at row no-" + clsCommon.myCstr(ii + 1) + Environment.NewLine + "")
                '    End If
                'End If

            Next
            For ii As Integer = 0 To gvDG.RowCount - 1
                If clsCommon.myLen(gvDG.Rows(ii).Cells(colDGCode).Value) > 0 Then
                    If clsCommon.myLen(gvDG.Rows(ii).Cells(colDGUnit).Value) <= 0 Then
                        Throw New Exception("DG Unit is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    End If
                    If clsCommon.myLen(gvDG.Rows(ii).Cells(colDGConsumption).Value) <= 0 Then
                        Throw New Exception("DG Consumption is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    End If
                    If clsCommon.myLen(gvDG.Rows(ii).Cells(colDGRuningHours).Value) <= 0 Then
                        Throw New Exception("DG Runing Hours is Mandatory. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " ")
                    End If
                End If
            Next
            For ii As Integer = 0 To gvDG.RowCount - 1
                If clsCommon.myLen(gvDG.Rows(ii).Cells(colDGCode).Value) > 0 Then
                    For iii As Integer = 0 To gvDG.RowCount - 1
                        If clsCommon.myLen(gvDG.Rows(iii).Cells(colDGCode).Value) > 0 Then
                            If clsCommon.CompairString(gvDG.Rows(ii).Cells(colDGCode).Value, gvDG.Rows(iii).Cells(colDGCode).Value) = CompairStringResult.Equal AndAlso ii <> iii Then
                                Throw New Exception("Same DG Code not allow. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and " + clsCommon.myCstr(clsCommon.myCdbl(iii + 1)) + " ")
                            End If
                        End If
                    Next
                End If
            Next

            For ii As Integer = 0 To gvSlot.RowCount - 1
                If clsCommon.myLen(gvSlot.Rows(ii).Cells(colSlotcode).Value) > 0 Then
                    For iii As Integer = 0 To gvSlot.RowCount - 1
                        If clsCommon.myLen(gvSlot.Rows(iii).Cells(colSlotcode).Value) > 0 Then
                            If clsCommon.CompairString(gvSlot.Rows(ii).Cells(colSlotcode).Value, gvSlot.Rows(iii).Cells(colSlotcode).Value) = CompairStringResult.Equal AndAlso ii <> iii Then
                                Throw New Exception("Same slot Code not allow. At Line No: " + clsCommon.myCstr(clsCommon.myCdbl(ii + 1)) + " and " + clsCommon.myCstr(clsCommon.myCdbl(iii + 1)) + " ")
                            End If
                        End If
                    Next
                End If
            Next

            Dim isSlotcode As Integer = 0
            Dim isDGCode As Integer = 0
            For ii As Integer = 0 To gvSlot.RowCount - 1
                If clsCommon.myLen(gvSlot.Rows(ii).Cells(colSlotcode).Value) > 0 Then
                    isSlotcode = 1
                End If
            Next
            For ii As Integer = 0 To gvDG.RowCount - 1
                If clsCommon.myLen(gvDG.Rows(ii).Cells(colDGCode).Value) > 0 Then
                    isDGCode = 1
                End If
            Next
            If isSlotcode = 0 AndAlso isDGCode = 0 Then
                Throw New Exception("First Enter EB Unit Details OR Diesel Genset Details OR Both")
            End If

            'UpdateAllTotals()
            UpdateEBUnitTotals()
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub mnuSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveLayout.Click
        'If clsCommon.myLen(MyBase.Form_ID) > 0 Then
        '    gvISlot.MasterTemplate.FilterDescriptors.Clear()
        '    Dim obj As New clsGridLayout()
        '    obj.ReportID = MyBase.Form_ID & "gvItem"
        '    obj.UserID = objCommonVar.CurrentUserCode
        '    obj.GridLayout = New MemoryStream()
        '    gvISlot.SaveLayout(obj.GridLayout)
        '    obj.GridColumns = gvISlot.ColumnCount
        '    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        '    If obj.SaveData() Then
        '        gvISlot.MasterTemplate.FilterDescriptors.Clear()
        '    End If
        '    obj.GridLayout.Close()
        '    obj.GridLayout.Dispose()
        'End If
    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        'clsGridLayout.DeleteData(MyBase.Form_ID & "gvItem", objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(MyBase.Form_ID & "gvParam", objCommonVar.CurrentUserCode)
        'clsGridLayout.DeleteData(MyBase.Form_ID & "gvRange", objCommonVar.CurrentUserCode)
        'ReStoreGridLayout()
        'common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub ReStoreGridLayout()
        'Try
        '    If clsCommon.myLen(MyBase.Form_ID) > 0 Then
        '        Dim obj As clsGridLayout = New clsGridLayout()
        '        obj = CType(obj.GetData(Form_ID & "gvItem", "", objCommonVar.CurrentUserCode), clsGridLayout)
        '        If Not obj Is Nothing AndAlso obj.GridColumns >= gvISlot.ColumnCount Then
        '            Dim ii As Integer
        '            For ii = 0 To gvISlot.Columns.Count - 1 Step ii + 1
        '                gvISlot.Columns(ii).IsVisible = False
        '                gvISlot.Columns(ii).VisibleInColumnChooser = True
        '            Next
        '            gvISlot.LoadLayout(obj.GridLayout)
        '            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        '        End If
        '    End If
        'Catch err As Exception
        '    MessageBox.Show(err.Message)
        'End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub FrmJobMilkSRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
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

    Private Sub RdbManual_CheckStateChanged(sender As Object, e As EventArgs)
        'Try
        '    If RdbManual.IsChecked = True Then
        '        TxtTankerNo.Visible = True
        '        TxtTankerNo.Enabled = True
        '        FndVendor.Visible = True
        '        txtLocation.Visible = True
        '        txtChallanNo.ReadOnly = False
        '        dtpChallanDate.Enabled = True
        '        dtpChallanDate.ReadOnly = False
        '        fndTankerNo.Visible = False
        '        txtVendor.Visible = False
        '        txtLocation.Visible = False
        '        RadPageView1.Pages("QcDetails").Item.Visibility = ElementVisibility.Hidden
        '        If gvItem.Columns.Count > 0 Then
        '            gvItem.Columns(colQty).IsVisible = True
        '        End If
        '    Else
        '        TxtTankerNo.Visible = False
        '        TxtTankerNo.Text = Nothing
        '        FndVendor.Value = Nothing
        '        txtLocation.Value = Nothing
        '        FndVendor.Visible = False
        '        txtLocation.Visible = False
        '        txtChallanNo.ReadOnly = True
        '        dtpChallanDate.Enabled = False
        '        dtpChallanDate.ReadOnly = True
        '        fndTankerNo.Visible = True
        '        txtVendor.Visible = True
        '        txtLocation.Visible = True
        '        RadPageView1.Pages("QcDetails").Item.Visibility = ElementVisibility.Visible
        '        If gvItem.Columns.Count > 0 Then
        '            gvItem.Columns(colQty).IsVisible = False
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Enter Document Code To delete ")
        Else
            If myMessages.deleteConfirm() Then
                If clsDailyElectricalEntryHead.deleteData(txtDocNo.Value) Then
                    Reset()
                    myMessages.delete()
                End If
            End If
        End If
    End Sub

    Sub printData(ByVal SRNNo As String)
        'Dim frmCrystalReportViewer As New frmCrystalReportViewer()
        'Dim strCompany_Name As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select Comp_Name from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "))
        'Dim strCompanyLocation As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  'Registered Office : '+ tspl_company_master.Comp_Name + Case when len(tspl_company_master.Add1) >0 then ','+tspl_company_master.Add1 else '' end + case when len( tspl_company_master.Add2) >0 then ',' + tspl_company_master.Add2 else '' end + case when len(tspl_company_master.Add3) >0  then ','+tspl_company_master.Add3 else '' end + case when len(tspl_company_master.Fax) >0 then ', FAX :'+ tspl_company_master.Fax else '' end + case when len(tspl_company_master.Email) >0 then ', Email : '+tspl_company_master.Email else '' end + case when len( tspl_company_master.CINNo) >0 then  'CIN NO : '+tspl_company_master.CINNo else '' end + case when len(tspl_company_master.Pan_No) > 0 then ', PAN No : '+ tspl_company_master.Pan_No else '' end as Company_Address from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "))
        'If clsCommon.myLen(SRNNo) > 0 Then
        '    Dim strQuery As String = " select    case when isnull(JOB_WORK_VENDOR_MASTER.Add1,'')<>'' then JOB_WORK_VENDOR_MASTER.Add1  else '' end + case when  isnull(JOB_WORK_VENDOR_MASTER.Add2,'')<>'' then  ','+JOB_WORK_VENDOR_MASTER.Add2 else '' end + case when isnull(JOB_WORK_VENDOR_MASTER.Add3,'')<>'' then ',' + JOB_WORK_VENDOR_MASTER.Add3   else '' end as JobWork_Vendor_Add" & _
        '                             ",JOB_WORK_VENDOR_MASTER.GSTFinalNo as JobWork_Vendor_GSTIN,Job_Work_VendorState_Master.GST_STATE_Code as JobWork_Vendor_Gst_State," & _
        '                            " TSPL_LOCATION_MASTER_From_loc.GSTNO as From_location_GSTIN,TSPL_STATE_MASTER_From_loc.GST_STATE_Code as From_Loc_GST_STATE_Code,    TSPL_LOCATION_MASTER_To_loc.GSTNO as To_Location_GSTIN,TSPL_STATE_MASTER_To_loc.GST_STATE_Code as     To_Loc_GST_State_Code,'" + strCompany_Name + "' as Comp_Name, '" + strCompanyLocation + "' as Company_Address ,TSPL_JWO_SRN_DETAIL.Document_No ,convert(varchar,TSPL_JWO_SRN_HEAD.Document_Date,103) as  Document_Date,TSPL_JWO_SRN_DETAIL.SNO,TSPL_JWO_SRN_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.HSN_Code,TSPL_JWO_SRN_DETAIL.UOM,TSPL_JWO_SRN_DETAIL.Gross_Weight,TSPL_JWO_SRN_DETAIL.Tare_Weight,TSPL_JWO_SRN_DETAIL.Net_Weight,TSPL_JWO_SRN_DETAIL.Qty,TSPL_JWO_SRN_DETAIL.Rate,TSPL_JWO_SRN_DETAIL.Amount,TSPL_JWO_SRN_HEAD.Document_Type,TSPL_JWO_SRN_HEAD.Loc_Code,TSPL_JWO_SRN_HEAD.Job_Loc_Code,TSPL_JWO_SRN_HEAD.Vendor_Code,TSPL_JWO_SRN_HEAD.Challan_No,convert(varchar,TSPL_JWO_SRN_HEAD.Challan_Date,103) asChallan_Date, TSPL_JWO_SRN_HEAD.Tanker_No,TSPL_JWO_SRN_HEAD.Gate_Entry_No, convert (varchar,TSPL_JWO_SRN_HEAD.Gate_Entry_Date ,103) as Gate_Entry_Date,TSPL_JWO_SRN_HEAD.Posted , TSPL_JWO_SRN_HEAD.Posted_Date,TSPL_JWO_SRN_HEAD.Created_By,TSPL_JWO_SRN_HEAD.Modified_By, tspl_user_master_Modified_By.User_Name as Modified_Name,tspl_user_master_Created_By.User_Name as Created_Name ,TSPL_JWO_SRN_HEAD.Document_Amt,TSPL_JWO_SRN_HEAD.Unloading_No, TSPL_VENDOR_MASTER.Vendor_Name,TSPL_VENDOR_MASTER.Add1 + case when len(TSPL_VENDOR_MASTER.Add2) >0 then ','+ TSPL_VENDOR_MASTER.Add2 else '' end + case when len(TSPL_VENDOR_MASTER.Add3)>0 then ','+ TSPL_VENDOR_MASTER.Add3 else '' end as Vendor_Address,TSPL_CITY_MASTER_Vendor.City_Code as Vendor_City_Code,TSPL_CITY_MASTER_Vendor.City_Name as Vendor_City_Name,TSPL_VENDOR_MASTER.GSTFinalNo , TSPL_STATE_MASTER_Vendor.STATE_NAME as Vendor_State_Name, TSPL_STATE_MASTER_Vendor.STATE_CODE as Vendor_State_Code, TSPL_STATE_MASTER_Vendor.GST_STATE_Code " & _
        '                             "  ,    TSPL_LOCATION_MASTER_From_loc.Add1 + case when len(TSPL_LOCATION_MASTER_From_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_From_loc. Add2 else ' '     end + Case when len( TSPL_LOCATION_MASTER_From_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_From_loc. Add3 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_From_loc.Add4 else '' end + case when len     (TSPL_LOCATION_MASTER_From_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_From_loc.City_Code else '' end + case when len    (TSPL_LOCATION_MASTER_From_loc.state) > 0 then ','+ TSPL_STATE_MASTER_From_loc.STATE_NAME else ''  end + Case when len     (TSPL_LOCATION_MASTER_From_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_From_loc.Pin_Code,103) else '' end as From_Location_Address , TSPL_LOCATION_MASTER_To_loc.Add1 + case when len(TSPL_LOCATION_MASTER_To_loc. Add2) > 0 then +','+ TSPL_LOCATION_MASTER_To_loc. Add2 else ' ' end + Case when len( TSPL_LOCATION_MASTER_To_loc. Add3) >0 then ','+TSPL_LOCATION_MASTER_To_loc. Add3 else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.Add4) >0 then ','+TSPL_LOCATION_MASTER_To_loc.Add4 else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.City_Code)>0 then ','+ TSPL_LOCATION_MASTER_To_loc.City_Code else '' end + case when len(TSPL_LOCATION_MASTER_To_loc.state) > 0 then ','+ TSPL_STATE_MASTER_To_loc.STATE_NAME else ''  end + Case when len(TSPL_LOCATION_MASTER_To_loc.Pin_Code ) >0 then ' - '+ convert(varchar, TSPL_LOCATION_MASTER_To_loc.Pin_Code,103) else '' end  as To_Location_Address from TSPL_JWO_SRN_DETAIL left outer join TSPL_JWO_SRN_HEAD on TSPL_JWO_SRN_DETAIL.Document_No =TSPL_JWO_SRN_HEAD.Document_No left outer join TSPL_ITEM_MASTER on TSPL_JWO_SRN_DETAIL.item_code= TSPL_ITEM_MASTER.Item_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.vendor_code=TSPL_JWO_SRN_HEAD.Vendor_Code left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_Vendor on TSPL_CITY_MASTER_Vendor.city_code =TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_Vendor on TSPL_STATE_MASTER_Vendor.STATE_CODE = TSPL_VENDOR_MASTER.State_Code " & _
        '                             " left outer join tspl_user_master as tspl_user_master_Modified_By on tspl_user_master_Modified_By.User_Code =TSPL_JWO_SRN_HEAD.Modified_By " & _
        '                             " left outer join tspl_user_master as tspl_user_master_Created_By on tspl_user_master_Created_By.User_Code =TSPL_JWO_SRN_HEAD.Created_By " & _
        '                             " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_From_loc on TSPL_LOCATION_MASTER_From_loc.Location_Code =TSPL_JWO_SRN_HEAD.loc_Code   left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_To_loc on TSPL_LOCATION_MASTER_To_loc.Location_Code =TSPL_JWO_SRN_HEAD.Job_Loc_Code    left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_From_loc on  TSPL_STATE_MASTER_From_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State    left outer join TSPL_STATE_MASTER as TSPL_STATE_MASTER_To_loc on  TSPL_STATE_MASTER_To_loc.STATE_CODE = TSPL_LOCATION_MASTER_From_loc.State   left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_From_location on TSPL_CITY_MASTER_From_location.City_Code = TSPL_LOCATION_MASTER_From_loc.City_Code  left outer join TSPL_CITY_MASTER as TSPL_CITY_MASTER_To_Location on TSPL_CITY_MASTER_To_Location.City_Code =TSPL_LOCATION_MASTER_To_loc.City_Code " & _
        '                             " left outer join TSPL_LOCATION_MASTER AS JOB_WORK_LOCATION_MASTER ON TSPL_JWO_SRN_HEAD.Job_Loc_Code =JOB_WORK_LOCATION_MASTER.Location_Code " & _
        '                            " LEFT OUTER JOIN TSPL_VENDOR_MASTER AS JOB_WORK_VENDOR_MASTER ON JOB_WORK_LOCATION_MASTER.Jobwork_Vendor=JOB_WORK_VENDOR_MASTER.Vendor_Code" & _
        '                                " LEFT OUTER JOIN TSPL_STATE_MASTER AS Job_Work_VendorState_Master on JOB_WORK_VENDOR_MASTER.State_Code=Job_Work_VendorState_Master.STATE_CODE " & _
        '                             "  where  TSPL_JWO_SRN_HEAD.Document_No = '" & SRNNo & "' "
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
        '    Dim dtDocdate As Date?
        '    dtDocdate = Nothing
        '    dtDocdate = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
        '    frmCrystalReportViewer.funreport(CrystalReportFolder.ServiceReport, dt, "rptJobWorkSRN", "Job Work SRN", dtDocdate)
        'Else
        '    clsCommon.MyMessageBoxShow("Please select an SRN to print")
        'End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'printData(txtDocNo.Value)
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
    End Sub

    Sub postData()
        Try
            If (myMessages.postConfirm()) Then
                clsDailyElectricalEntryHead.postData(txtDocNo.Value)
                clsCommon.MyMessageBoxShow("Successfully Posted")
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Sub LoadData(ByVal strSrnNo As String, ByVal nav As NavigatorType)
        Try
            Reset()
            loadSlotBlankItemGrid()
            loadDGBlankItemGrid()
            isInsideLoadData = True
            obj = clsDailyElectricalEntryHead.GetData(strSrnNo, nav)
            If obj IsNot Nothing Then
                isNewEntery = False
                btnSave.Text = "Update"
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtRemarks.Text = obj.Remarks
                txtConsumptionDate.Value = obj.Consumption_Date
                txtDocNo.MyReadOnly = True
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objtr As clsDailyElectricalEntrySlotDetails In obj.Arr
                        gvSlot.Rows(gvSlot.RowCount - 1).Cells(colSlotSNo).Value = objtr.SNo
                        gvSlot.Rows(gvSlot.RowCount - 1).Cells(colSlotcode).Value = objtr.Slot_Code
                        gvSlot.Rows(gvSlot.RowCount - 1).Cells(colSlotDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_SLOT_MASTER.Description  from TSPL_SLOT_MASTER where TSPL_SLOT_MASTER.Code = '" + objtr.Slot_Code + "'"))
                        gvSlot.Rows(gvSlot.RowCount - 1).Cells(colslotUnit).Value = objtr.Slot_Unit
                        gvSlot.Rows.AddNew()
                    Next
                End If
                If obj.ArrDG IsNot Nothing AndAlso obj.ArrDG.Count > 0 Then
                    For Each objtrDG As clsDailyElectricalEntryDGDetails In obj.ArrDG
                        gvDG.Rows(gvDG.RowCount - 1).Cells(colDGSNo).Value = objtrDG.SNo
                        gvDG.Rows(gvDG.RowCount - 1).Cells(colDGCode).Value = objtrDG.DG_Code
                        gvDG.Rows(gvDG.RowCount - 1).Cells(colDGName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_DG_MASTER.Description  from TSPL_DG_MASTER where TSPL_DG_MASTER.Code = '" + objtrDG.DG_Code + "'"))
                        gvDG.Rows(gvDG.RowCount - 1).Cells(colDGUnit).Value = objtrDG.DG_Unit
                        gvDG.Rows(gvDG.RowCount - 1).Cells(colDGConsumption).Value = objtrDG.DG_Consumption
                        gvDG.Rows(gvDG.RowCount - 1).Cells(colDGRuningHours).Value = objtrDG.DG_Runing_Hours
                        gvDG.Rows.AddNew()
                    Next
                End If
                UpdateEBUnitTotals()

                lblPending.Status = obj.Posted
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnDelete.Enabled = True
                    btnPost.Enabled = True
                End If
                'btnPrint.Enabled = True
            Else
                Reset()
            End If
            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub fndSRNNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        LoadData(txtDocNo.Value, NavType)
    End Sub

    'Private Sub fndSRNNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
    '    Try
    '        Dim whrCls As String = String.Empty
    '        If Not clsMccMaster.isCurrentUserHO() Then
    '            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '                whrCls = "  loc_code in  (" & objCommonVar.strCurrUserLocations & ") "
    '            End If
    '        End If
    '        txtDocNo.Value = clsJWOSRNHead.getFinder(whrCls, txtDocNo.Value, isButtonClicked)
    '        If clsCommon.myLen(txtDocNo.Value) > 0 Then
    '            LoadData(txtDocNo.Value, NavigatorType.Current)
    '        Else
    '            Reset()
    '        End If

    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)
    '    Try
    '        Dim strDocNo As String = txtDocNo.Value
    '        If rbtnTanker.IsChecked Then
    '            Dim qry As String = "select TSPL_JWO_GATE_ENTRY.Tanker_No as TankerNo,TSPL_JWO_GATE_ENTRY.Gate_Entry_No as [GateEntryNo] ,TSPL_JWO_GATE_ENTRY.Doc_Type as [Doc Type] ,TSPL_JWO_GATE_ENTRY.Date_And_Time as [Gate Entry Date And Time],  TSPL_JWO_Weighment.Weighment_No as [Weighment No],TSPL_JWO_Weighment.Weighment_Date as [Weighment Date],TSPL_JWO_QUALITY_CHECK.QC_No as [QC No],TSPL_JWO_QUALITY_CHECK.Qc_In_Date_Time as [QC Date Time]  ,TSPL_JWO_GATE_ENTRY.Challan_No as [Challan No] ,TSPL_JWO_GATE_ENTRY.Challan_Date as [Challan Date]  , case when isnull (TSPL_JWO_GATE_ENTRY.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_JWO_GATE_ENTRY.Posting_Date as [Posting Date] ,TSPL_JWO_GATE_ENTRY.Dispatched_From_Mcc as [Dispatched From Mcc] ,TSPL_JWO_GATE_ENTRY.location_Code as [Location Code] ,TSPL_JWO_GATE_ENTRY.Location_Desc as [Location Desc] ,TSPL_JWO_GATE_ENTRY.Vendor_Code as [Vendor Code] ,TSPL_JWO_GATE_ENTRY.Vendor_Desc as [Vendor Desc] ,TSPL_JWO_GATE_ENTRY.Item_Code as [Item Code],TSPL_JWO_GATE_ENTRY.UOM ,TSPL_JWO_GATE_ENTRY.Item_Desc as [Item Desc] ,TSPL_JWO_GATE_ENTRY.Qty_In_Kg as [Qty In Kg] ,TSPL_JWO_GATE_ENTRY.snf_Per as [SNF %] ,TSPL_JWO_GATE_ENTRY.fat_per as [FAT %] ,TSPL_JWO_GATE_ENTRY.Created_By as [Created By] ,TSPL_JWO_GATE_ENTRY.Created_Date as [Created Date] ,TSPL_JWO_GATE_ENTRY.Modify_By as [Modify By] ,TSPL_JWO_GATE_ENTRY.Modify_Date as [Modify Date] ,TSPL_JWO_GATE_ENTRY.comp_code as [Company Code] , case when ISNULL(TSPL_JWO_GATE_ENTRY.Doc_Type,'')='Sku_Receipt' then 1 when ISNULL(TSPL_JWO_QUALITY_CHECK.is_param_accepted,0)>0 then 1 else 0 end as Accepted " + Environment.NewLine + _
    '        " ,TSPL_JWO_UNLOADING.Unloading_No,TSPL_JWO_UNLOADING.Unloading_Date_Time,TSPL_JWO_GATE_ENTRY.JobWorkLocation,TabJOBLocation.Location_Desc as JobWorkLocationDescription ,TSPL_JWO_WEIGHMENT.Gross_Weight,TSPL_JWO_WEIGHMENT.Tare_Weight,TSPL_JWO_WEIGHMENT.Net_Weight" + Environment.NewLine + _
    '        " From TSPL_JWO_GATE_ENTRY" + Environment.NewLine + _
    '        " left outer join TSPL_JWO_Weighment on TSPL_JWO_Weighment.Gate_Entry_No=TSPL_JWO_GATE_ENTRY.Gate_Entry_No  " + Environment.NewLine + _
    '        " left outer join TSPL_JWO_QUALITY_CHECK on TSPL_JWO_QUALITY_CHECK.gate_entry_no=TSPL_JWO_GATE_ENTRY.Gate_Entry_No " + Environment.NewLine + _
    '        " left outer join TSPL_JWO_UNLOADING on TSPL_JWO_UNLOADING.gate_entry_no=TSPL_JWO_GATE_ENTRY.Gate_Entry_No " + Environment.NewLine + _
    '        " left outer join TSPL_LOCATION_MASTER as TabJOBLocation on TabJOBLocation.Location_Code=TSPL_JWO_GATE_ENTRY.JobWorkLocation " + Environment.NewLine + _
    '        " where TSPL_JWO_UNLOADING.isPosted=1 and TSPL_JWO_Weighment.isPosted=1  and not exists (select 1 from TSPL_JWO_SRN_HEAD where TSPL_JWO_SRN_HEAD.Unloading_No=TSPL_JWO_UNLOADING.Unloading_No and TSPL_JWO_SRN_HEAD.Document_No not in ('" + strDocNo + "') and not exists(select 1 from TSPL_JWO_SRN_RETURN where TSPL_JWO_SRN_RETURN.JWO_SRN_No=TSPL_JWO_SRN_HEAD.Document_No) )"
    '            Dim dr As DataRow = clsCommon.ShowSelectFormForRow("jowsankerfi", qry, "TankerNo", txtVehicleNo.Value)
    '            If dr Is Nothing Then
    '                Exit Sub
    '            End If
    '            BlankAllControl()
    '            txtVehicleNo.Value = clsCommon.myCstr(dr("TankerNo"))
    '            txtGateEntryNo.Text = clsCommon.myCstr(dr("GateEntryNo"))
    '            txtGateEntryDate.Value = clsCommon.myCDate(dr("Gate Entry Date And Time"))
    '            txtChallanNo.Text = clsCommon.myCstr(dr("Challan No"))
    '            txtChallanDate.Value = clsCommon.myCDate(dr("Challan Date"))
    '            txtVendor.Value = clsCommon.myCstr(dr("Vendor Code"))
    '            lblVendorName.Text = clsCommon.myCstr(dr("Vendor Desc"))
    '            txtLocation.Value = clsCommon.myCstr(dr("Location Code"))
    '            lblLocationDesc.Text = clsCommon.myCstr(dr("Location Desc"))
    '            txtJobLocation.Value = clsCommon.myCstr(dr("JobWorkLocation"))
    '            lblJobLocation.Text = clsCommon.myCstr(dr("JobWorkLocationDescription"))
    '            lblUnloadingNo.Text = clsCommon.myCstr(dr("Unloading_No"))
    '            isInsideLoadData = True
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colSNo).Value = 1
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colICode).Value = clsCommon.myCstr(dr("Item Code"))
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colIName).Value = clsCommon.myCstr(dr("Item Desc"))
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(clsCommon.myCstr(dr("Item Code")), Nothing)
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colUOM).Value = clsCommon.myCstr(dr("UOM"))
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colGrossWeight).Value = clsCommon.myCdbl(dr("Gross_Weight"))
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colTareWeight).Value = clsCommon.myCdbl(dr("Tare_Weight"))
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colNetWeight).Value = clsCommon.myCdbl(dr("Net_Weight"))
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("Net_Weight"))
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colFat).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Param_Field_Value from TSPL_JWO_QC_PARAMETER_DETAIL where QC_No='" + clsCommon.myCstr(dr("QC No")) + "' and Param_Type in ('FAT')"))
    '            gvISlot.Rows(gvISlot.RowCount - 1).Cells(colSNF).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Param_Field_Value from TSPL_JWO_QC_PARAMETER_DETAIL where QC_No='" + clsCommon.myCstr(dr("QC No")) + "' and Param_Type in ('SNF')"))
    '            isInsideLoadData = False
    '            UpdateCurrentRow(gvISlot.RowCount - 1)
    '            UpdateAllTotals()
    '            txtDocNo.Value = strDocNo
    '        ElseIf chkSKU.IsChecked Then
    '            Dim qry As String = "select Tanker_No, Gate_Entry_No as GateEntryNo,convert(varchar, Date_And_Time,103) as GateEntryDate ,Challan_No,Challan_Date ,location_Code,Location_Desc,JobWorkLocation,Vendor_Code,Vendor_Desc" + Environment.NewLine + _
    '            " from TSPL_JWO_GATE_ENTRY" + Environment.NewLine

    '            Dim Whrclas As String = " Doc_Type='Sku_Receipt' " + Environment.NewLine + _
    '            " and isPosted=1 " + Environment.NewLine + _
    '            " and not exists (select 1 from TSPL_JWO_SRN_HEAD where TSPL_JWO_SRN_HEAD.Against_Gate_Entry_No=TSPL_JWO_GATE_ENTRY.Gate_Entry_No and TSPL_JWO_SRN_HEAD.Document_No not in ('" + strDocNo + "') and not exists(select 1 from TSPL_JWO_SRN_RETURN where TSPL_JWO_SRN_RETURN.JWO_SRN_No=TSPL_JWO_SRN_HEAD.Document_No) )"
    '            Dim strGateEntryNo As String = clsCommon.ShowSelectForm("jowdgenkerfi", qry, "GateEntryNo", Whrclas, txtVehicleNo.Value, "", isButtonClicked)
    '            BlankAllControl()
    '            If clsCommon.myLen(strGateEntryNo) > 0 Then
    '                Dim obj As clsMilkGateEntry_JOW = clsMilkGateEntry_JOW.getData(strGateEntryNo, "Sku_Receipt", NavigatorType.Current)
    '                txtVehicleNo.Value = obj.Tanker_No
    '                txtGateEntryNo.Text = obj.Gate_Entry_No
    '                txtGateEntryDate.Value = obj.Date_And_Time
    '                txtChallanNo.Text = obj.Challan_No
    '                txtChallanDate.Value = obj.Challan_Date
    '                txtVendor.Value = obj.Vendor_Code
    '                lblVendorName.Text = obj.Vendor_Desc
    '                txtLocation.Value = obj.location_Code
    '                lblLocationDesc.Text = obj.Location_Desc
    '                txtJobLocation.Value = obj.JobWorkLocation
    '                lblJobLocation.Text = clsLocation.GetName(obj.JobWorkLocation, Nothing)
    '                lblUnloadingNo.Text = ""
    '                isInsideLoadData = True
    '                If obj.arrJOWGateEntryDetail IsNot Nothing AndAlso obj.arrJOWGateEntryDetail.Count > 0 Then
    '                    For Each objTr As clsMilkGateEntryDetail_JOW In obj.arrJOWGateEntryDetail
    '                        gvISlot.Rows(gvISlot.Rows.Count - 1).Cells(colSNo).Value = gvISlot.Rows.Count
    '                        gvISlot.Rows(gvISlot.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
    '                        gvISlot.Rows(gvISlot.Rows.Count - 1).Cells(colIName).Value = clsItemMaster.GetItemName(objTr.Item_Code, Nothing)
    '                        gvISlot.Rows(gvISlot.RowCount - 1).Cells(colHSNCode).Value = clsItemMaster.GetItemHSNCode(objTr.Item_Code, Nothing)
    '                        gvISlot.Rows(gvISlot.Rows.Count - 1).Cells(colUOM).Value = objTr.UOM
    '                        gvISlot.Rows(gvISlot.Rows.Count - 1).Cells(colQty).Value = objTr.Qty_In_Kg
    '                        gvISlot.Rows(gvISlot.Rows.Count - 1).Cells(colFat).Value = objTr.fat_per
    '                        gvISlot.Rows(gvISlot.Rows.Count - 1).Cells(colSNF).Value = objTr.snf_Per
    '                        UpdateCurrentRow(gvISlot.RowCount - 1)
    '                        gvISlot.Rows.AddNew()
    '                    Next
    '                End If
    '                isInsideLoadData = False
    '                UpdateAllTotals()
    '            End If
    '            txtDocNo.Value = strDocNo
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub FndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Dim WhrCls As String = " Location_Type='Physical' and  isnull(Is_Jobwork,0)=0"
    '    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
    '        WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
    '    End If
    '    txtLocation.Value = clsLocation.getFinder(WhrCls, txtLocation.Value, isButtonClicked)
    '    If clsCommon.myLen(txtLocation.Value) > 0 Then
    '        lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    '        txtLocation.Text = txtLocation.Value
    '    Else
    '        lblLocationDesc.Text = ""
    '        txtLocation.Text = ""
    '    End If
    '    loadBlankItemGrid()
    'End Sub

    'Private Sub txtJobLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
    '    Dim WhrCls As String = " isnull(Is_Jobwork,0)=1"
    '    txtJobLocation.Value = clsLocation.getFinder(WhrCls, txtJobLocation.Value, isButtonClicked)
    '    setVendorLocation(True)
    '    loadBlankItemGrid()
    'End Sub

    'Sub setVendorLocation(ByVal isLocation)
    '    Dim qry As String = "select TSPL_LOCATION_MASTER.Location_Code,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Jobwork_Vendor,TSPL_VENDOR_MASTER.Vendor_Name from " + Environment.NewLine + _
    '    "TSPL_LOCATION_MASTER" + Environment.NewLine + _
    '    "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor" + Environment.NewLine + _
    '    "where 2=2"
    '    If isLocation Then
    '        qry += " and TSPL_LOCATION_MASTER.Location_Code='" + txtJobLocation.Value + "'"
    '    Else
    '        qry += " and TSPL_LOCATION_MASTER.Jobwork_Vendor='" + txtVendor.Value + "'"
    '    End If
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        txtJobLocation.Value = clsCommon.myCstr(dt.Rows(0)("Location_Code"))
    '        lblJobLocation.Text = clsCommon.myCstr(dt.Rows(0)("Location_Desc"))
    '        txtVendor.Value = clsCommon.myCstr(dt.Rows(0)("Jobwork_Vendor"))
    '        lblVendorName.Text = clsCommon.myCstr(dt.Rows(0)("Vendor_Name"))
    '    Else
    '        txtJobLocation.Value = ""
    '        lblJobLocation.Text = ""
    '        txtVendor.Value = ""
    '        lblVendorName.Text = ""
    '    End If
    'End Sub

    'Private Sub fndVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
    '    Dim qry As String = "select TSPL_LOCATION_MASTER.Jobwork_Vendor as VendorCode,TSPL_VENDOR_MASTER.Vendor_Name as VendorName from " + Environment.NewLine + _
    '    " TSPL_LOCATION_MASTER" + Environment.NewLine + _
    '    " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_LOCATION_MASTER.Jobwork_Vendor"
    '    Dim wrhclas As String = "len(isnull( TSPL_LOCATION_MASTER.Jobwork_Vendor,''))>0"
    '    txtVendor.Value = clsCommon.ShowSelectForm("vendorFromloc", qry, "VendorCode", wrhclas, txtVendor.Value, "", isButtonClicked)
    '    setVendorLocation(False)
    'End Sub




    'Private Sub gvItem_CellValueChanged(sender As Object, e As GridViewCellEventArgs)
    '    Try
    '        If (Not isInsideLoadData) Then
    '            If Not isCellValueChangedOpen Then
    '                isCellValueChangedOpen = True
    '                If e.Column Is gvISlot.Columns(colICode) OrElse e.Column Is gvISlot.Columns(colUOM) OrElse e.Column Is gvISlot.Columns(colQty) OrElse e.Column Is gvISlot.Columns(colRate) Then
    '                    If e.Column Is gvISlot.Columns(colICode) Then
    '                        OpenICodeList(False)
    '                    ElseIf e.Column Is gvISlot.Columns(colUOM) Then
    '                        OpenUOMList(False)
    '                    End If
    '                    UpdateCurrentRow(gvISlot.CurrentRow.Index)
    '                    UpdateAllTotals()
    '                End If
    '                isCellValueChangedOpen = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        isCellValueChangedOpen = False
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Sub OpenICodeList(ByVal isButtonClick As Boolean)
    '    Dim whrcls As String = ""
    '    Dim obj As clsItemMaster
    '    obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gvISlot.CurrentRow.Cells(colICode).Value), "", isButtonClick, Nothing, whrcls)
    '    If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
    '        gvISlot.CurrentRow.Cells(colICode).Value = obj.Item_Code
    '        gvISlot.CurrentRow.Cells(colIName).Value = obj.Item_Desc
    '        gvISlot.CurrentRow.Cells(colUOM).Value = obj.Unit_Code
    '        gvISlot.CurrentRow.Cells(colFat).Value = clsBOM.GetFAT_PERS(obj.Item_Code)
    '        gvISlot.CurrentRow.Cells(colSNF).Value = clsBOM.GetSNF_PERS(obj.Item_Code)
    '        gvISlot.CurrentRow.Cells(colHSNCode).Value = obj.HSNCode
    '    Else
    '        gvISlot.CurrentRow.Cells(colICode).Value = ""
    '        gvISlot.CurrentRow.Cells(colIName).Value = ""
    '        gvISlot.CurrentRow.Cells(colUOM).Value = ""
    '        gvISlot.CurrentRow.Cells(colFat).Value = 0.0
    '        gvISlot.CurrentRow.Cells(colSNF).Value = 0.0
    '        gvISlot.CurrentRow.Cells(colSNF).Value = 0.0
    '        gvISlot.CurrentRow.Cells(colHSNCode).Value = ""
    '    End If
    'End Sub

    'Sub OpenUOMList(ByVal isButtonClick As Boolean)
    '    Dim strICode As String = clsCommon.myCstr(gvISlot.CurrentRow.Cells(colICode).Value)
    '    If clsCommon.myLen(strICode) > 0 Then
    '        Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
    '        Dim whrCls As String = "Item_Code='" + strICode + "'"
    '        gvISlot.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("SRdsefndnder", qry, "Code", whrCls, clsCommon.myCstr(gvISlot.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
    '    End If
    'End Sub

    'Sub UpdateCurrentRow(ByVal ii As Integer)
    '    Dim obj As clsVendorItemChargeDetail = clsVendorItemChargeDetail.GetJobPrice(txtVendor.Value, clsCommon.myCstr(gvItem.Rows(ii).Cells(colICode).Value), clsCommon.myCstr(gvItem.Rows(ii).Cells(colUOM).Value), txtDate.Value, Nothing)
    '    Dim dblQty As Double = clsCommon.myCdbl(gvISlot.Rows(ii).Cells(colQty).Value)
    '    Dim dblRate As Double = clsCommon.myCdbl(gvISlot.Rows(ii).Cells(colRate).Value)
    '    Dim dblJobRate As Double = obj.ItemCharge
    '    Dim dblJobAmt As Double = dblQty * dblJobRate
    '    Dim dblAmt As Double = dblQty * dblRate
    '    gvISlot.Rows(ii).Cells(colPriceCode).Value = obj.Price_Code
    '    gvISlot.Rows(ii).Cells(colJobRate).Value = obj.ItemCharge
    '    gvISlot.Rows(ii).Cells(colJobAmt).Value = dblJobAmt
    '    gvISlot.Rows(ii).Cells(colAmt).Value = dblAmt
    'End Sub

    Sub UpdateEBUnitTotals()
        Dim dblSlotUnit As Decimal = 0
        For ii As Integer = 0 To gvSlot.RowCount - 1
            If clsCommon.myLen(gvSlot.Rows(ii).Cells(colSlotcode).Value) > 0 Then
                dblSlotUnit += clsCommon.myCdbl(gvSlot.Rows(ii).Cells(colslotUnit).Value)
            End If
        Next
        lblTotalEBUnit.Text = clsCommon.myCstr(dblSlotUnit)
    End Sub

    'Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs)
    '    If gvISlot.RowCount > 0 Then
    '        Dim intCurrRow As Integer = gvISlot.CurrentRow.Index
    '        gvISlot.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '        If intCurrRow = gvISlot.Rows.Count - 1 Then
    '            gvISlot.Rows.AddNew()
    '            gvISlot.CurrentRow = gvISlot.Rows(intCurrRow)
    '        End If
    '    End If
    'End Sub

    'Private Sub rbtnManual_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
    '    If rbtnManual.IsChecked Then
    '        txtLocation.Enabled = True
    '        txtJobLocation.Enabled = True
    '        txtVendor.Enabled = True
    '        txtGateEntryNo.Enabled = True
    '        txtGateEntryDate.Enabled = True
    '        txtChallanNo.Enabled = True
    '        txtChallanDate.Enabled = True
    '    ElseIf rbtnTanker.IsChecked Then
    '        txtLocation.Enabled = False
    '        txtJobLocation.Enabled = False
    '        txtVendor.Enabled = False
    '        txtGateEntryNo.Enabled = False
    '        txtGateEntryDate.Enabled = False
    '        txtChallanNo.Enabled = False
    '        txtChallanDate.Enabled = False
    '    ElseIf chkSKU.IsChecked Then
    '        txtLocation.Enabled = False
    '        txtJobLocation.Enabled = False
    '        txtVendor.Enabled = False
    '        txtGateEntryNo.Enabled = False
    '        txtGateEntryDate.Enabled = False
    '        txtChallanNo.Enabled = False
    '        txtChallanDate.Enabled = False
    '    End If
    'End Sub


    'Private Sub gvItem_CellFormatting(sender As Object, e As CellFormattingEventArgs)
    '    Try
    '        If e.Column.Index >= 0 Then
    '            If e.Column Is gvISlot.Columns(colICode) Then
    '                gvISlot.CurrentRow.Cells(colICode).ReadOnly = Not rbtnManual.IsChecked
    '            ElseIf e.Column Is gvISlot.Columns(colUOM) Then
    '                gvISlot.CurrentRow.Cells(colUOM).ReadOnly = Not rbtnManual.IsChecked
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs)
    '    If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
    '        e.Cancel = True
    '    End If
    'End Sub

    'Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs)
    '    UpdateAllTotals()
    '    For ii As Integer = 1 To gvISlot.Rows.Count
    '        gvISlot.Rows(ii - 1).Cells(colSNo).Value = ii
    '    Next

    'End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow("Reverese and unpost current document " + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                    clsDailyElectricalEntryHead.ReverseAndUnpostData(txtDocNo.Value)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvSlot_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSlot.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvSlot.Columns(colSlotcode) Then
                        If e.Column Is gvSlot.Columns(colSlotcode) Then
                            OpenSlotCodeList(False)
                        End If
                        'UpdateCurrentRow(gvISlot.CurrentRow.Index)
                        'UpdateAllTotals()

                    End If
                    UpdateEBUnitTotals()
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenSlotCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Code as Code,Description as [Description] from TSPL_SLOT_MASTER"
        Dim whrCls As String = ""
        gvSlot.CurrentRow.Cells(colSlotcode).Value = clsCommon.ShowSelectForm("SlotCode@fndnder", qry, "Code", whrCls, clsCommon.myCstr(gvSlot.CurrentRow.Cells(colSlotcode).Value), "Code", isButtonClick)
        gvSlot.CurrentRow.Cells(colSlotDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_SLOT_MASTER.Description  from TSPL_SLOT_MASTER where TSPL_SLOT_MASTER.Code = '" + gvSlot.CurrentRow.Cells(colSlotcode).Value + "'"))
    End Sub

    Private Sub gvDG_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvDG.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvDG.Columns(colDGCode) Then
                        If e.Column Is gvDG.Columns(colDGCode) Then
                            OpenDGCodeList(False)
                        End If
                        'UpdateCurrentRow(gvISlot.CurrentRow.Index)
                        'UpdateAllTotals()

                    End If
                    isCellValueChangedOpen = False
                End If

            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub OpenDGCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Code as Code,Description as [Description] from TSPL_DG_MASTER"
        Dim whrCls As String = ""
        gvDG.CurrentRow.Cells(colDGCode).Value = clsCommon.ShowSelectForm("DGCode@fndnder", qry, "Code", whrCls, clsCommon.myCstr(gvDG.CurrentRow.Cells(colDGCode).Value), "Code", isButtonClick)
        gvDG.CurrentRow.Cells(colDGName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TSPL_DG_MASTER.Description  from TSPL_DG_MASTER where TSPL_DG_MASTER.Code = '" + gvDG.CurrentRow.Cells(colDGCode).Value + "'"))
    End Sub

    Private Sub gvSlot_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvSlot.UserDeletedRow
        UpdateEBUnitTotals()
        For ii As Integer = 1 To gvSlot.Rows.Count
            gvSlot.Rows(ii - 1).Cells(colSlotSNo).Value = ii
        Next
    End Sub

    Private Sub gvDG_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gvDG.UserDeletedRow
        For ii As Integer = 1 To gvDG.Rows.Count
            gvDG.Rows(ii - 1).Cells(colDGSNo).Value = ii
        Next
    End Sub

    Private Sub gvSlot_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvSlot.CurrentColumnChanged
        If gvSlot.RowCount > 0 Then
            Dim intCurrRow As Integer = gvSlot.CurrentRow.Index
            gvSlot.CurrentRow.Cells(colSlotSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvSlot.Rows.Count - 1 Then
                gvSlot.Rows.AddNew()
                gvSlot.CurrentRow = gvSlot.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvDG_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvDG.CurrentColumnChanged
        If gvDG.RowCount > 0 Then
            Dim intCurrRow As Integer = gvDG.CurrentRow.Index
            gvDG.CurrentRow.Cells(colDGSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvDG.Rows.Count - 1 Then
                gvDG.Rows.AddNew()
                gvDG.CurrentRow = gvDG.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvSlot_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gvSlot.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qst As String = "Select Count (*) from TSPL_DAILY_ELECTRICAL_ENTRY_HEAD where TSPL_DAILY_ELECTRICAL_ENTRY_HEAD.Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                LoadData(clsDailyElectricalEntryHead.getFinder("", txtDocNo.Value, isButtonClicked), NavigatorType.Current)
                LoadData(txtDocNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
End Class
