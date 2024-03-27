'--17/09/2013--form Add By- Pradeep Sharma ---------Ticket no BM00000000460
Imports common
Imports System.Data
Imports System.Data.SqlClient

Public Class frmWorkCenterMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim dtcbo As DataTable
    Dim dtcboBasis As DataTable

    Dim isCellValueChangedOpen As Boolean = False
#End Region

#Region "gv Shift"
    Const colShiftLineNo As String = "LineNo"
    Const colShift As String = "Shift"
    Const colShiftRegularHours As String = "RegularHours"
    Const colShiftOvertimeHours As String = "OvertimeHours"
    Const colShiftStationsInUse As String = "StationsInUse"
#End Region

#Region "Resourc "
    Const colResLineNo As String = "LineNo"
    Const colResResourceCode As String = "ResourceCode"
    Const colResDescription As String = "ResDescription"
    Const colResType As String = "ResType"
    Const colResBasis As String = "ResBasis"
    Const colResUom As String = "ResUom"
    Const colResQuantity As String = "ResQuantity"
    Const colResUnitCost As String = "ResUnitCost"
    Const colResTotalCost As String = "ResTotalCost"
#End Region

#Region "Tool "
    Const colToolLineNo As String = "LineNo"
    Const colToolTypeCode As String = "ToolTypeCode"
    Const colToolDescription As String = "ResDescription"
    Const colToolBasis As String = "ResBasis"
    Const colToolUom As String = "ResUom"
    Const colToolQuantity As String = "ResQuantity"
    Const colToolUnitCost As String = "ResUnitCost"
    Const colToolTotalCost As String = "ResTotalCost"
#End Region


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsWorkCenterMaster()
            obj.WORK_CENTER_CODE = txtCode.Value
            obj.Description = txtDescription.Text
            obj.SETUP_TIME = clsCommon.myCdbl(txtSetupTime.Value)
            obj.SETUP_TIME_TYPE = clsCommon.myCstr(CboSetupTime.SelectedValue)
            obj.RUN_TIME = clsCommon.myCdbl(txtRunTime.Value)
            obj.RUN_TIME_TYPE = clsCommon.myCstr(cboRunTime.SelectedValue)
            obj.CLEANUP_TIME = clsCommon.myCdbl(txtCleanupTime.Value)
            obj.CLEANUP_TIME_TYPE = clsCommon.myCstr(cboCleanupTime.SelectedValue)
            obj.WAIT_TIME = clsCommon.myCdbl(txtWaitTime.Value)
            obj.WAIT_TIME_TYPE = clsCommon.myCstr(CboWaitTime.SelectedValue)
            obj.WORK_AREA = clsCommon.myCstr(txtworkArea.Text)
            obj.NO_OF_STATIONS = Convert.ToInt16(txtNoOfStations.Value)
            obj.STD_SETUP_LABOR = clsCommon.myCdbl(txtStdSetupLabor.Value)
            obj.STD_RUN_LABOR = clsCommon.myCdbl(txtStdRunLabor.Value)
            obj.STD_EFFICIENCY = clsCommon.myCdbl(txtStdEfficiency.Value)
            obj.STD_UTILIZATION = clsCommon.myCstr(txtstdUtilization.Value)
            obj.COMMENTS = clsCommon.myCstr(txtComment.Text)
            obj.TotalResourceCost = clsCommon.myCdbl(txtTotalResourceCost.Value)
            obj.TotalToolCost = clsCommon.myCdbl(txtTotalToolCost.Value)

            ''gv Shift
            obj.ObjList_ShiftDetails = New List(Of clsWorkCenterShiftDetail)
            For Each grow As GridViewRowInfo In gvMfgInfo.Rows
                If clsCommon.myLen(grow.Cells(colShift).Value) > 0 Then
                    Dim objShift As New clsWorkCenterShiftDetail()
                    objShift.WORK_CENTER_CODE = clsCommon.myCstr(txtCode.Value)
                    objShift.SHIFT = clsCommon.myCstr(grow.Cells(colShift).Value)
                    objShift.OVERTIME_HOURS = clsCommon.myCdbl(grow.Cells(colShiftOvertimeHours).Value)
                    objShift.REGULAR_HOURS = clsCommon.myCdbl(grow.Cells(colShiftRegularHours).Value)
                    objShift.STATIONS_IN_USE = clsCommon.myCdbl(grow.Cells(colShiftStationsInUse).Value)
                    obj.ObjList_ShiftDetails.Add(objShift)
                End If
            Next

            ''gv resource
            obj.ObjList_resourceDetails = New List(Of clsWorkCenterResourceDetail)
            For Each grow As GridViewRowInfo In gvResource.Rows
                If clsCommon.myLen(grow.Cells(colResResourceCode).Value) > 0 Then
                    Dim objres As New clsWorkCenterResourceDetail()
                    objres.WORK_CENTER_CODE = clsCommon.myCstr(txtCode.Value)
                    objres.RESOURCE_CODE = clsCommon.myCstr(grow.Cells(colResResourceCode).Value)
                    objres.Basis = clsCommon.myCstr(grow.Cells(colResBasis).Value)
                    objres.QUANTITY = clsCommon.myCdbl(grow.Cells(colResQuantity).Value)
                    objres.UNIT_COST = clsCommon.myCdbl(grow.Cells(colResUnitCost).Value)
                    objres.TOTAL_COST = clsCommon.myCdbl(grow.Cells(colResTotalCost).Value)
                    obj.ObjList_resourceDetails.Add(objres)
                End If
            Next

            ''gv Tool
            obj.ObjList_ToolDetails = New List(Of clsWorkCenterToolDetail)
            For Each grow As GridViewRowInfo In gvTool.Rows
                If clsCommon.myLen(grow.Cells(colToolTypeCode).Value) > 0 Then
                    Dim objTool As New clsWorkCenterToolDetail()
                    objTool.WORK_CENTER_CODE = clsCommon.myCstr(txtCode.Value)
                    objTool.TOOL_TYPE_CODE = clsCommon.myCstr(grow.Cells(colToolTypeCode).Value)
                    objTool.Basis = clsCommon.myCstr(grow.Cells(colToolBasis).Value)
                    objTool.QUANTITY = clsCommon.myCdbl(grow.Cells(colToolQuantity).Value)
                    objTool.UNIT_COST = clsCommon.myCdbl(grow.Cells(colToolUnitCost).Value)
                    objTool.TOTAL_COST = clsCommon.myCdbl(grow.Cells(colToolTotalCost).Value)
                    obj.ObjList_ToolDetails.Add(objTool)
                End If
            Next

            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                LoadData(obj.WORK_CENTER_CODE, NavigatorType.Current)
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If
        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsWorkCenterMaster()
        obj = clsWorkCenterMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.WORK_CENTER_CODE) > 0) Then
            funReset()
            isNewEntry = False
            btnSave.Text = "Update"
            txtCode.Value = clsCommon.myCstr(obj.WORK_CENTER_CODE)
            txtDescription.Text = clsCommon.myCstr(obj.Description)
            txtSetupTime.Value = clsCommon.myCdbl(obj.SETUP_TIME)
            CboSetupTime.SelectedValue = clsCommon.myCstr(obj.SETUP_TIME_TYPE)
            txtRunTime.Value = clsCommon.myCdbl(obj.RUN_TIME)
            cboRunTime.SelectedValue = clsCommon.myCstr(obj.RUN_TIME_TYPE)
            txtCleanupTime.Value = clsCommon.myCdbl(obj.CLEANUP_TIME)
            cboCleanupTime.SelectedValue = clsCommon.myCstr(obj.CLEANUP_TIME_TYPE)
            txtWaitTime.Value = clsCommon.myCdbl(obj.WAIT_TIME)
            CboWaitTime.SelectedValue = clsCommon.myCstr(obj.WAIT_TIME_TYPE)
            txtworkArea.Text = clsCommon.myCstr(obj.WORK_AREA)
            txtNoOfStations.Value = Convert.ToInt16(obj.NO_OF_STATIONS)
            txtStdSetupLabor.Value = clsCommon.myCdbl(obj.STD_SETUP_LABOR)
            txtStdRunLabor.Value = clsCommon.myCdbl(obj.STD_RUN_LABOR)
            txtStdEfficiency.Value = clsCommon.myCdbl(obj.STD_EFFICIENCY)
            txtstdUtilization.Value = clsCommon.myCdbl(obj.STD_UTILIZATION)
            txtComment.Text = clsCommon.myCstr(obj.COMMENTS)
            dtpLastChanged.Value = clsCommon.myCDate(obj.Modified_Date)
            txtChangedBy.Text = clsCommon.myCstr(obj.Modified_By)
            txtTotalResourceCost.Value = clsCommon.myCdbl(obj.TotalResourceCost)
            txtTotalToolCost.Value = clsCommon.myCdbl(obj.TotalToolCost)
            Fill_ShiftGV(obj.ObjList_ShiftDetails)
            Fill_ResourceGrid(obj.ObjList_resourceDetails)
            Fill_ToolGrid(obj.ObjList_ToolDetails)
        End If

    End Sub

    Function AllowToSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) AndAlso clsCommon.myLen(txtCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Code", Me.Text)
            txtCode.Focus()
            Return False
        ElseIf clsCommon.myLen(txtworkArea.Text) <= 0 Then
            myMessages.blankValue(Me, "Skill Name", Me.Text)
            txtworkArea.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsWorkCenterMaster.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub frmWorkCenterMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")

        dtcbo = New DataTable
        dtcbo.Columns.Add("Code", GetType(String))
        dtcbo.Columns.Add("Value", GetType(String))

        Dim dr As DataRow
        dr = dtcbo.NewRow()
        dr("Code") = "Hour"
        dr("Value") = "Hour"
        dtcbo.Rows.Add(dr)

        dr = dtcbo.NewRow()
        dr("Code") = "Minute"
        dr("Value") = "Minute"
        dtcbo.Rows.Add(dr)
        dtcbo.AcceptChanges()

        dtcboBasis = New DataTable
        dtcboBasis.Columns.Add("Code", GetType(String))
        dtcboBasis.Columns.Add("Value", GetType(String))

        dr = dtcboBasis.NewRow()
        dr("Code") = "Variable"
        dr("Value") = "Variable"
        dtcboBasis.Rows.Add(dr)

        dr = dtcboBasis.NewRow()
        dr("Code") = "Fixed"
        dr("Value") = "Fixed"
        dtcboBasis.Rows.Add(dr)
        dtcboBasis.AcceptChanges()
        funReset()
        gvMfgInfo.Rows.AddNew()
        gvResource.Rows.AddNew()
        gvTool.Rows.AddNew()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmWorkCenterMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtworkArea.Text = ""
        txtDescription.Text = ""

        ''mfg tab
        txtSetupTime.Value = 0
        CboSetupTime.DataSource = dtcbo
        CboSetupTime.ValueMember = "Code"
        CboSetupTime.DisplayMember = "Value"
        txtRunTime.Value = 0
        cboRunTime.DataSource = dtcbo.Copy()
        cboRunTime.ValueMember = "Code"
        cboRunTime.DisplayMember = "Value"
        txtCleanupTime.Value = 0
        cboCleanupTime.DataSource = dtcbo.Copy()
        cboCleanupTime.ValueMember = "Code"
        cboCleanupTime.DisplayMember = "Value"
        txtWaitTime.Value = 0
        CboWaitTime.DataSource = dtcbo.Copy()
        CboWaitTime.ValueMember = "Code"
        CboWaitTime.DisplayMember = "Value"
        txtworkArea.Text = ""
        txtNoOfStations.Value = 0
        txtStdSetupLabor.Value = 0
        txtStdRunLabor.Value = 0
        txtStdRunLabor.Value = 0
        dtpLastChanged.Value = Nothing
        txtChangedBy.Text = ""
        txtStdEfficiency.Value = 0
        txtstdUtilization.Value = 0
        txtComment.Text = ""
        gvMfgInfo.DataSource = Nothing
        gvMfgInfo.Rows.Clear()
        gvMfgInfo.Columns.Clear()
        LoadGridColumns_Shift()

        ''resource tab
        gvResource.DataSource = Nothing
        gvResource.Rows.Clear()
        gvResource.Columns.Clear()
        txtTotalResourceCost.Value = 0
        LoadGridColumns_Resource()

        ''tool tab
        gvTool.DataSource = Nothing
        gvTool.Rows.Clear()
        gvTool.Columns.Clear()
        txtTotalToolCost.Value = 0
        LoadGridColumns_Tool()

        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating

        Dim str As String = "select count(*) from TSPL_MF_WORK_CENTER where WORK_CENTER_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select WORK_CENTER_CODE AS [Code], DESCRIPTION as [Description] from TSPL_MF_WORK_CENTER"
            'txtCode.Value = clsCommon.ShowSelectForm("TSPL_MF_WORK_CENTER", qry, "Code", "", txtCode.Value, "Code", isButtonClicked)
            txtCode.Value = clsWorkCenterMaster.getFinder("", txtCode.Value, isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCode.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub frmWorkCenterMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub


#Region "GV Shift Exp"

    Sub LoadGridColumns_Shift()
        gvMfgInfo.DataSource = Nothing
        gvMfgInfo.Rows.Clear()
        gvMfgInfo.Columns.Clear()
        gvMfgInfo.ReadOnly = False

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colShiftLineNo
        lineNo.Width = 40
        lineNo.ReadOnly = True
        'lineNo.IsVisible = False
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMfgInfo.Columns.Add(lineNo)

        Dim Shift As New GridViewDecimalColumn()
        Shift.FormatString = ""
        Shift.HeaderText = "Shift"
        Shift.Name = colShift
        Shift.Width = 70
        Shift.ReadOnly = False
        Shift.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMfgInfo.Columns.Add(Shift)

        Dim RegularHours As New GridViewDecimalColumn()
        RegularHours.FormatString = ""
        RegularHours.HeaderText = "Regular Hours"
        RegularHours.Name = colShiftRegularHours
        RegularHours.Width = 80
        RegularHours.ReadOnly = False
        RegularHours.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMfgInfo.Columns.Add(RegularHours)

        Dim OvertimeHours As New GridViewDecimalColumn()
        OvertimeHours.FormatString = ""
        OvertimeHours.HeaderText = "Overtime Hours"
        OvertimeHours.Name = colShiftOvertimeHours
        OvertimeHours.Width = 80
        OvertimeHours.ReadOnly = False
        OvertimeHours.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMfgInfo.Columns.Add(OvertimeHours)

        Dim StationsInUse As New GridViewDecimalColumn()
        StationsInUse.FormatString = ""
        StationsInUse.HeaderText = "Stations In Use"
        StationsInUse.Name = colShiftStationsInUse
        StationsInUse.Width = 80
        StationsInUse.ReadOnly = False
        StationsInUse.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvMfgInfo.Columns.Add(StationsInUse)

    End Sub

    Private Sub Fill_ShiftGV(ByVal ObjList As List(Of clsWorkCenterShiftDetail))
        If ObjList IsNot Nothing AndAlso ObjList.Count > 0 Then
            For Each objtr As clsWorkCenterShiftDetail In ObjList
                gvMfgInfo.Rows.AddNew()
                gvMfgInfo.Rows(gvMfgInfo.Rows.Count - 1).Cells(colShiftLineNo).Value = gvMfgInfo.Rows.Count
                gvMfgInfo.Rows(gvMfgInfo.Rows.Count - 1).Cells(colShift).Value = objtr.SHIFT
                gvMfgInfo.Rows(gvMfgInfo.Rows.Count - 1).Cells(colShiftRegularHours).Value = objtr.REGULAR_HOURS
                gvMfgInfo.Rows(gvMfgInfo.Rows.Count - 1).Cells(colShiftOvertimeHours).Value = objtr.OVERTIME_HOURS
                gvMfgInfo.Rows(gvMfgInfo.Rows.Count - 1).Cells(colShiftStationsInUse).Value = objtr.STATIONS_IN_USE
            Next
        End If
        gvMfgInfo.Rows.AddNew()
    End Sub

    Private Sub gvShift_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvMfgInfo.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvMfgInfo_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvMfgInfo.CurrentColumnChanged
        If gvMfgInfo.RowCount > 0 Then
            Dim intCurrRow As Integer = gvMfgInfo.CurrentRow.Index
            gvMfgInfo.CurrentRow.Cells(colShiftLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvMfgInfo.Rows.Count - 1 Then
                gvMfgInfo.Rows.AddNew()
                gvMfgInfo.CurrentRow = gvMfgInfo.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvMfgInfo_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvMfgInfo.UserAddedRow
        For i As Integer = 0 To gvMfgInfo.Rows.Count - 1
            gvMfgInfo.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gvMfgInfo.Rows(i).Cells(colShiftLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gvMfgInfo_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvMfgInfo.UserDeletedRow
        For ii As Integer = 1 To gvMfgInfo.Rows.Count
            gvMfgInfo.Rows(ii - 1).Cells(colShiftLineNo).Value = ii
        Next
    End Sub

#End Region

#Region "GV Resource"

    Sub LoadGridColumns_Resource()
        gvResource.DataSource = Nothing
        gvResource.Rows.Clear()
        gvResource.Columns.Clear()
        'gvResource.ReadOnly = True

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colResLineNo
        lineNo.Width = 70
        lineNo.ReadOnly = True
        'lineNo.IsVisible = False
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResource.Columns.Add(lineNo)

        Dim ResourceCode As New GridViewTextBoxColumn()
        ResourceCode.FormatString = ""
        ResourceCode.HeaderText = "Resource Code"
        ResourceCode.Name = colResResourceCode
        ResourceCode.Width = 100
        ResourceCode.ReadOnly = False
        ResourceCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvResource.Columns.Add(ResourceCode)

        Dim ResDescription As New GridViewTextBoxColumn()
        ResDescription.FormatString = ""
        ResDescription.HeaderText = "Description"
        ResDescription.Name = colResDescription
        ResDescription.Width = 150
        ResDescription.ReadOnly = True
        ResDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvResource.Columns.Add(ResDescription)

        Dim ResType As New GridViewTextBoxColumn()
        ResType.FormatString = ""
        ResType.HeaderText = "Type"
        ResType.Name = colResType
        ResType.Width = 100
        ResType.ReadOnly = True
        ResType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvResource.Columns.Add(ResType)

        Dim ResBasis As New GridViewComboBoxColumn()
        ResBasis.FormatString = ""
        ResBasis.HeaderText = "Basis"
        ResBasis.Name = colResBasis
        ResBasis.DataSource = dtcboBasis.Copy()
        ResBasis.ValueMember = "Code"
        ResBasis.DisplayMember = "Value"
        ResBasis.Width = 100
        ResBasis.ReadOnly = False
        ResBasis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResource.Columns.Add(ResBasis)

        Dim ResUom As New GridViewTextBoxColumn()
        ResUom.FormatString = ""
        ResUom.HeaderText = "UOM"
        ResUom.Name = colResUom
        ResUom.Width = 100
        ResUom.ReadOnly = True
        ResUom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResource.Columns.Add(ResUom)

        Dim ResQuantity As New GridViewDecimalColumn()
        ResQuantity.FormatString = ""
        ResQuantity.HeaderText = "Quantity"
        ResQuantity.Name = colResQuantity
        ResQuantity.Width = 100
        ResQuantity.ReadOnly = False
        ResQuantity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResource.Columns.Add(ResQuantity)

        Dim ResUnitCost As New GridViewDecimalColumn()
        ResUnitCost.FormatString = ""
        ResUnitCost.HeaderText = "Unit Cost"
        ResUnitCost.Name = colResUnitCost
        ResUnitCost.Width = 100
        ResUnitCost.ReadOnly = True
        ResUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResource.Columns.Add(ResUnitCost)

        Dim ResTotalCost As New GridViewDecimalColumn()
        ResTotalCost.FormatString = ""
        ResTotalCost.HeaderText = "Total Cost"
        ResTotalCost.Name = colResTotalCost
        ResTotalCost.Width = 100
        ResTotalCost.ReadOnly = True
        ResTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvResource.Columns.Add(ResTotalCost)

    End Sub

    Private Sub Fill_ResourceGrid(ByVal ObjList As List(Of clsWorkCenterResourceDetail))
        If ObjList IsNot Nothing AndAlso ObjList.Count > 0 Then
            For Each objtr As clsWorkCenterResourceDetail In ObjList
                gvResource.Rows.AddNew()
                isCellValueChangedOpen = True
                gvResource.Rows(gvResource.Rows.Count - 1).Cells(colResLineNo).Value = gvResource.Rows.Count
                gvResource.Rows(gvResource.Rows.Count - 1).Cells(colResResourceCode).Value = objtr.RESOURCE_CODE
                gvResource.Rows(gvResource.Rows.Count - 1).Cells(colResDescription).Value = objtr.DESCRIPTION
                gvResource.Rows(gvResource.Rows.Count - 1).Cells(colResType).Value = objtr.RESOURCE_TYPE
                gvResource.Rows(gvResource.Rows.Count - 1).Cells(colResBasis).Value = objtr.Basis
                gvResource.Rows(gvResource.Rows.Count - 1).Cells(colResUom).Value = objtr.UNIT_CODE_OTHER
                gvResource.Rows(gvResource.Rows.Count - 1).Cells(colResQuantity).Value = objtr.QUANTITY
                gvResource.Rows(gvResource.Rows.Count - 1).Cells(colResUnitCost).Value = objtr.UNIT_COST
                gvResource.Rows(gvResource.Rows.Count - 1).Cells(colResTotalCost).Value = objtr.TOTAL_COST
                isCellValueChangedOpen = False
            Next
        End If
        gvResource.Rows.AddNew()
    End Sub

    Private Sub gvResource_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvResource.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvResource_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvResource.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gvResource.Columns(colResResourceCode) Then
                    OpenResCodeList(True)
                End If
                If e.Column Is gvResource.Columns(colResQuantity) Then
                    If clsCommon.myLen(gvResource.CurrentRow.Cells(colResQuantity).Value) > 0 Then
                        gvResource.CurrentRow.Cells(colResTotalCost).Value = clsCommon.myCdbl(gvResource.CurrentRow.Cells(colResQuantity).Value) * clsCommon.myCdbl(gvResource.CurrentRow.Cells(colResUnitCost).Value)
                        Cal_Res_Total()
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub

    Sub OpenResCodeList(ByVal isButtonClick As Boolean)
        Dim str_Item_Price_ID = ""
        Dim qry As String = "select RESOURCE_CODE as [Code], STATUS AS [Status], DESCRIPTION as [Description], RESOURCE_TYPE, UNIT_CODE, COST, COMMENTS from TSPL_MF_RESOURCE_MASTER "
        str_Item_Price_ID = clsCommon.ShowSelectForm("RESOURCE_MASTER", qry, "Code", "", gvResource.CurrentRow.Cells(colResResourceCode).Value, "Code", isButtonClick)
        If clsCommon.myLen(str_Item_Price_ID) > 0 Then
            qry = " "
            qry += " Select RESOURCE_CODE as [Code], STATUS AS [Code], DESCRIPTION as [Description], RESOURCE_TYPE as [Type], UNIT_CODE ,UNIT_CODE_OTHER, COST as [Cost], COMMENTS as [COMMENTS]  from TSPL_MF_RESOURCE_MASTER "
            qry += " where RESOURCE_CODE ='" + str_Item_Price_ID + "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvResource.CurrentRow.Cells(colResResourceCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                gvResource.CurrentRow.Cells(colResDescription).Value = clsCommon.myCstr(dt.Rows(0)("Description"))
                gvResource.CurrentRow.Cells(colResType).Value = clsCommon.myCstr(dt.Rows(0)("Type"))
                gvResource.CurrentRow.Cells(colResUom).Value = clsCommon.myCstr(dt.Rows(0)("UNIT_CODE_OTHER"))
                gvResource.CurrentRow.Cells(colResUnitCost).Value = clsCommon.myCdbl(dt.Rows(0)("COST"))
            Else
                gvResource.CurrentRow.Cells(colResResourceCode).Value = ""
                gvResource.CurrentRow.Cells(colResDescription).Value = ""
                gvResource.CurrentRow.Cells(colResType).Value = ""
                gvResource.CurrentRow.Cells(colResUom).Value = ""
                gvResource.CurrentRow.Cells(colResUnitCost).Value = ""
            End If
        End If
    End Sub

    Private Sub gvResource_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvResource.CurrentColumnChanged
        If gvResource.RowCount > 0 Then
            Dim intCurrRow As Integer = gvResource.CurrentRow.Index
            gvResource.CurrentRow.Cells(colResLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvResource.Rows.Count - 1 Then
                gvResource.Rows.AddNew()
                gvResource.CurrentRow = gvResource.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvResource_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvResource.UserAddedRow
        For i As Integer = 0 To gvResource.Rows.Count - 1
            gvResource.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gvResource.Rows(i).Cells(colResLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gvResource_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvResource.UserDeletedRow
        For ii As Integer = 1 To gvResource.Rows.Count
            gvResource.Rows(ii - 1).Cells(colResLineNo).Value = ii
        Next
    End Sub

    Private Sub Cal_Res_Total()
        Dim tot As Double
        For Each dr As GridViewRowInfo In gvResource.Rows
            tot = tot + clsCommon.myCdbl(dr.Cells(colResTotalCost).Value)
        Next
        txtTotalResourceCost.Value = tot
    End Sub

#End Region

#Region "GV Tool"

    Sub LoadGridColumns_Tool()
        gvTool.DataSource = Nothing
        gvTool.Rows.Clear()
        gvTool.Columns.Clear()
        'gvTool.ReadOnly = True

        Dim lineNo As New GridViewTextBoxColumn()
        lineNo.FormatString = ""
        lineNo.HeaderText = "Line No"
        lineNo.Name = colToolLineNo
        lineNo.Width = 70
        lineNo.ReadOnly = True
        'lineNo.IsVisible = False
        lineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTool.Columns.Add(lineNo)

        Dim ToolTypeCode As New GridViewTextBoxColumn()
        ToolTypeCode.FormatString = ""
        ToolTypeCode.HeaderText = "Tool Type Code"
        ToolTypeCode.Name = colToolTypeCode
        ToolTypeCode.Width = 100
        ToolTypeCode.ReadOnly = False
        ToolTypeCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTool.Columns.Add(ToolTypeCode)

        Dim ToolDescription As New GridViewTextBoxColumn()
        ToolDescription.FormatString = ""
        ToolDescription.HeaderText = "Description"
        ToolDescription.Name = colToolDescription
        ToolDescription.Width = 100
        ToolDescription.ReadOnly = True
        ToolDescription.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvTool.Columns.Add(ToolDescription)

        Dim ToolBasis As New GridViewComboBoxColumn()
        ToolBasis.FormatString = ""
        ToolBasis.HeaderText = "Basis"
        ToolBasis.Name = colToolBasis
        ToolBasis.DataSource = dtcboBasis.Copy()
        ToolBasis.ValueMember = "Code"
        ToolBasis.DisplayMember = "Value"
        ToolBasis.Width = 100
        ToolTypeCode.ReadOnly = False
        ToolBasis.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTool.Columns.Add(ToolBasis)

        Dim ToolUom As New GridViewTextBoxColumn()
        ToolUom.FormatString = ""
        ToolUom.HeaderText = "UOM"
        ToolUom.Name = colToolUom
        ToolUom.Width = 100
        ToolUom.ReadOnly = True
        ToolUom.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTool.Columns.Add(ToolUom)

        Dim ToolQuantity As New GridViewDecimalColumn()
        ToolQuantity.FormatString = ""
        ToolQuantity.HeaderText = "Quantity"
        ToolQuantity.Name = colToolQuantity
        ToolQuantity.Width = 100
        ToolQuantity.ReadOnly = False
        ToolQuantity.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTool.Columns.Add(ToolQuantity)

        Dim ToolUnitCost As New GridViewDecimalColumn()
        ToolUnitCost.FormatString = ""
        ToolUnitCost.HeaderText = "Unit Cost"
        ToolUnitCost.Name = colToolUnitCost
        ToolUnitCost.Width = 100
        ToolUnitCost.ReadOnly = True
        ToolUnitCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTool.Columns.Add(ToolUnitCost)

        Dim ToolTotalCost As New GridViewDecimalColumn()
        ToolTotalCost.FormatString = ""
        ToolTotalCost.HeaderText = "Total Cost"
        ToolTotalCost.Name = colToolTotalCost
        ToolTotalCost.Width = 100
        ToolTotalCost.ReadOnly = True
        ToolTotalCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvTool.Columns.Add(ToolTotalCost)

    End Sub

    Private Sub Fill_ToolGrid(ByVal ObjList As List(Of clsWorkCenterToolDetail))
        If ObjList IsNot Nothing AndAlso ObjList.Count > 0 Then
            For Each objtr As clsWorkCenterToolDetail In ObjList
                isCellValueChangedOpen = True
                gvTool.Rows.AddNew()
                gvTool.Rows(gvTool.Rows.Count - 1).Cells(colToolLineNo).Value = gvTool.Rows.Count
                gvTool.Rows(gvTool.Rows.Count - 1).Cells(colToolTypeCode).Value = objtr.TOOL_TYPE_CODE
                gvTool.Rows(gvTool.Rows.Count - 1).Cells(colToolDescription).Value = objtr.DESCRIPTION
                gvTool.Rows(gvTool.Rows.Count - 1).Cells(colToolBasis).Value = objtr.Basis
                gvTool.Rows(gvTool.Rows.Count - 1).Cells(colToolUom).Value = objtr.UNIT_CODE
                gvTool.Rows(gvTool.Rows.Count - 1).Cells(colToolQuantity).Value = objtr.QUANTITY
                gvTool.Rows(gvTool.Rows.Count - 1).Cells(colToolUnitCost).Value = objtr.UNIT_COST
                gvTool.Rows(gvTool.Rows.Count - 1).Cells(colToolTotalCost).Value = objtr.TOTAL_COST
                isCellValueChangedOpen = False
            Next
        End If
        gvTool.Rows.AddNew()
    End Sub

    Private Sub gvTool_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvTool.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub gvTool_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvTool.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gvTool.Columns(colToolTypeCode) Then
                    OpenToolCodeList(True)
                End If
                If e.Column Is gvTool.Columns(colToolQuantity) Then
                    If clsCommon.myLen(gvTool.CurrentRow.Cells(colToolQuantity).Value) > 0 Then
                        gvTool.CurrentRow.Cells(colToolTotalCost).Value = clsCommon.myCdbl(gvTool.CurrentRow.Cells(colToolQuantity).Value) * clsCommon.myCdbl(gvTool.CurrentRow.Cells(colToolUnitCost).Value)
                        Cal_Toot_Total()
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub

    Sub OpenToolCodeList(ByVal isButtonClick As Boolean)
        Dim str_Item_Price_ID = ""
        Dim qry As String = "select TOOL_TYPE_CODE, STATUS, DESCRIPTION, INACTIVE_DATE, UNIT_CODE, COST_PER_UNIT,COMMENTS from TSPL_MF_TOOL_TYPE "
        str_Item_Price_ID = clsCommon.ShowSelectForm("TOOL_TYPE_CODE", qry, "TOOL_TYPE_CODE", "", gvTool.CurrentRow.Cells(colToolTypeCode).Value, "TOOL_TYPE_CODE", isButtonClick)
        gvTool.CurrentRow.Cells(colToolTypeCode).Value = str_Item_Price_ID
        If clsCommon.myLen(str_Item_Price_ID) > 0 Then
            qry = " "
            qry += " Select TOOL_TYPE_CODE, STATUS, DESCRIPTION, INACTIVE_DATE, UNIT_CODE, COST_PER_UNIT,COMMENTS from TSPL_MF_TOOL_TYPE "
            qry += " where TOOL_TYPE_CODE ='" + str_Item_Price_ID + "' "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gvTool.CurrentRow.Cells(colToolTypeCode).Value = clsCommon.myCstr(dt.Rows(0)("TOOL_TYPE_CODE"))
                gvTool.CurrentRow.Cells(colToolDescription).Value = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
                gvTool.CurrentRow.Cells(colToolUom).Value = clsCommon.myCstr(dt.Rows(0)("UNIT_CODE"))
                gvTool.CurrentRow.Cells(colToolUnitCost).Value = clsCommon.myCstr(dt.Rows(0)("COST_PER_UNIT"))
            Else
                gvTool.CurrentRow.Cells(colToolTypeCode).Value = ""
                gvTool.CurrentRow.Cells(colToolDescription).Value = ""
                gvTool.CurrentRow.Cells(colToolUom).Value = ""
                gvTool.CurrentRow.Cells(colToolUnitCost).Value = ""
            End If
        End If
    End Sub

    Private Sub gvTool_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gvTool.CurrentColumnChanged
        If gvTool.RowCount > 0 Then
            Dim intCurrRow As Integer = gvTool.CurrentRow.Index
            gvTool.CurrentRow.Cells(colToolLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvTool.Rows.Count - 1 Then
                gvTool.Rows.AddNew()
                gvTool.CurrentRow = gvTool.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gvTool_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvTool.UserAddedRow
        For i As Integer = 0 To gvTool.Rows.Count - 1
            gvTool.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gvTool.Rows(i).Cells(colToolLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gvTool_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gvTool.UserDeletedRow
        For ii As Integer = 1 To gvTool.Rows.Count
            gvTool.Rows(ii - 1).Cells(colToolLineNo).Value = ii
        Next
    End Sub

    Private Sub Cal_Toot_Total()
        Dim tot As Double
        For Each dr As GridViewRowInfo In gvTool.Rows
            tot = tot + clsCommon.myCdbl(dr.Cells(colToolTotalCost).Value)
        Next
        txtTotalToolCost.Value = tot
    End Sub

#End Region


End Class
