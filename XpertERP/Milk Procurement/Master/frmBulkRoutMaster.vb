Imports common
Imports System.Data
Imports System.Data.SqlClient

' Ticket No : BHA/21/06/18-000072
Public Class FrmBulkRoutMaster
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Const colLocationCode As String = "colLocationCode"
    Const colLocationName As String = "colLocationName"
    Const colDistance As String = "colDistance"
    Const colSNO As String = "colSNO"
    Const colBULKROUTEno As String = "colBULKROUTEno"


    Dim isCellValueChangedOpen As Boolean = False

#End Region
    Private Sub FrmBulkRoutMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D  for Delete")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New")
        funReset()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        RadMenuItem1.Enabled = MyBase.isModifyFlag ' For Import
        RadMenuItem2.Enabled = MyBase.isModifyFlag ' For Export
        rdbtndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Sub funReset()
        isNewEntry = True
        txtRouteNo.MyReadOnly = False
        txtRouteNo.Value = Nothing
        txtRouteNo.Focus()
        txtRouteName.Text = ""
        txtRouteNameHindi.Text = ""
        rdbtnsave.Text = "Save"
        rdbtnsave.Enabled = True
        rdbtndelete.Enabled = False
        txtDistance.Text = 0
        txtRate.Text = 0
        txtRate.Text = 0
        lblAmout.Text = ""
        txtWeight.Text = 0
        txtMCC.arrValueMember = Nothing
        txtToLocationCode.Enabled = False
        txtToLocationCode.Value = ""
        lblToLocationName.Text = ""
        txtTollAmount.Value = 0
        chkForContractor.Checked = False
        chkDefault.Checked = False
        txtTankerNo.Value = ""
        txtVehicleNo.Text = ""
        txtScheduleTime.Value = clsCommon.GETSERVERDATE()
        txtScheduleTimeM.Value = clsCommon.GETSERVERDATE()
        txtScheduleTimeE.Value = txtScheduleTimeM.Value
        LoadBlankGrid()
    End Sub



    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Dim str As String
        str = "select  ROUTE_NO as [Route No],ROUTE_NAME as [Route Name],Distance,Rate,isnull(Weight,0) as Weight ,Amount,Tanker_No as [TankerNo] , Schedule_Time_Morning as [Schedule Time Morning] , Schedule_Time_Evening as [Schedule Time Evening] from TSPL_BULK_ROUTE_MASTER"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim gv As New UserControls.MyRadGridView
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Route No", "Route Name", "Distance", "Rate", "Weight", "Amount", "TankerNo", "Schedule Time Morning", "Schedule Time Evening") Then
            Try
                clsCommon.ProgressBarShow()
                Dim ii As Integer = 0
                Try
                    For Each grow As GridViewRowInfo In gv.Rows
                        ii = ii + 1
                        Dim obj As New clsBulkRoutMaster()
                        obj.ROUTE_NO = clsCommon.myCstr(grow.Cells("Route No").Value)
                        If clsCommon.myLen(obj.ROUTE_NO) <= 0 Then
                            Continue For
                        End If
                        If clsCommon.myLen(obj.ROUTE_NO) > 30 Then
                            Throw New Exception("Route Code length greater then 30.")
                        End If
                        obj.ROUTE_NAME = clsCommon.myCstr(grow.Cells("Route Name").Value)
                        obj.Distance = clsCommon.myCdbl(grow.Cells("Distance").Value)
                        obj.Rate = clsCommon.myCdbl(grow.Cells("Rate").Value)
                        If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Weight").Value)) = True Then
                            Throw New Exception("Invalid Weight " + clsCommon.myCstr(grow.Cells("Weight").Value))
                        End If
                        If IsNumeric(grow.Cells("Weight").Value) = False Then
                            Throw New Exception("Invalid Weight " + clsCommon.myCstr(grow.Cells("Weight").Value))
                        End If
                        obj.Weight = clsCommon.myCdbl(grow.Cells("Weight").Value)

                        If obj.Distance <= 0 Then
                            Throw New Exception("Invalid Distance " + clsCommon.myCstr(obj.Distance))
                        End If
                        If obj.Rate <= 0 Then
                            Throw New Exception("Invalid Rate " + clsCommon.myCstr(obj.Rate))
                        End If
                        If obj.Weight < 0 Then
                            Throw New Exception("Invalid Weight " + clsCommon.myCstr(grow.Cells("Weight").Value))
                        End If

                        obj.Amount = obj.Distance * obj.Rate 'clsCommon.myCdbl(grow.Cells("Amount").Value)
                        If obj.Amount <= 0 Then
                            Throw New Exception("Invalid Amount " + clsCommon.myCstr(obj.Amount))
                        End If
                        If clsCommon.myLen(obj.ROUTE_NAME) <= 0 Then
                            Throw New Exception("Route Name can not be blank.")
                        End If

                        obj.Tanker_No = clsCommon.myCstr(grow.Cells("TankerNo").Value)
                        If clsCommon.myLen(obj.Tanker_No) > 0 Then
                            Qry = clsDBFuncationality.getSingleValue("select Tanker_No from TSPL_TANKER_MASTER where Tanker_No='" + obj.Tanker_No + "'")
                            If clsCommon.myLen(Qry) <= 0 Then
                                Throw New Exception("Invalid Tanker No " + clsCommon.myCstr(obj.Tanker_No))
                            End If
                        End If
                        obj.Schedule_Time_Morning = clsCommon.myCDate(grow.Cells("Schedule Time Morning").Value)
                        obj.Schedule_Time_Evening = clsCommon.myCDate(grow.Cells("Schedule Time Evening").Value)
                        clsBulkRoutMaster.SaveData(obj)
                    Next
                Catch ex As Exception
                    Throw New Exception("At Row No" + clsCommon.myCstr(ii) + " " + ex.Message)
                End Try
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rdbtnclose_Click(sender As Object, e As EventArgs) Handles rdbtnclose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Private Sub txtRouteNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtRouteNo._MYNavigator
        Try
            LoadData(txtRouteNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        funReset()
        txtRouteNo.MyReadOnly = True
        rdbtnsave.Enabled = True
        rdbtndelete.Enabled = True
        isNewEntry = False
        Dim obj As New clsBulkRoutMaster()
        obj = clsBulkRoutMaster.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.ROUTE_NO) > 0) Then
            funReset()
            isNewEntry = False
            rdbtnsave.Text = "Update"
            rdbtndelete.Enabled = True
            txtRouteNo.Value = obj.ROUTE_NO
            txtRouteName.Text = obj.ROUTE_NAME
            txtRouteNameHindi.Text = obj.ROUTE_NAME_HINDI

            txtTankerNo.Value = obj.Tanker_No
            txtVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TANKER_NAME from TSPL_TANKER_MASTER where Tanker_No='" & txtTankerNo.Value & "'"))
            txtDistance.Text = obj.Distance
            txtRate.Text = obj.Rate
            txtWeight.Text = obj.Weight
            lblAmout.Text = obj.Amount
            txtToLocationCode.Value = obj.Location_Code
            lblToLocationName.Text = clsCommon.myCstr(clsLocation.GetName(txtToLocationCode.Value, Nothing))
            txtTollAmount.Value = obj.TollAmount
            chkForContractor.Checked = IIf(obj.IsContractor = 1, True, False)
            chkDefault.Checked = IIf(obj.IsDefault = 1, True, False)
            txtMCC.arrValueMember = obj.arrMCC
            txtcuttofftime.Value = obj.CuttOff_Time
            If obj.Schedule_Time_Morning IsNot Nothing Then
                txtScheduleTimeM.Value = obj.Schedule_Time_Morning
            End If
            If obj.Schedule_Time_Evening IsNot Nothing Then
                txtScheduleTimeE.Value = obj.Schedule_Time_Evening
            End If
            If obj.Schedule_Time_Evening IsNot Nothing Then
                txtScheduleTimeE.Value = obj.Schedule_Time_Evening
            End If
            If obj.Schedule_Time IsNot Nothing Then
                txtScheduleTime.Value = obj.Schedule_Time
            End If

            Dim sno As Integer = 1
            If obj.Arr IsNot Nothing Then
                For Each objrow As clsBulkRoutdetail In obj.Arr
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSNO).Value = sno
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationCode).Value = objrow.Location_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLocationName).Value = objrow.lOCATION_desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDistance).Value = objrow.Distance
                    gv1.Rows.AddNew()
                    sno += 1
                Next
            End If
        End If
    End Sub
    'ROUTE_NAME as [Route Name],Distance,Rate,Amount 

    Private Sub rdbtnsave_Click(sender As Object, e As EventArgs) Handles rdbtnsave.Click
        Save()
    End Sub
    Public Sub Save()
        Try
            If AllowToSave() Then
                Dim obj As New clsBulkRoutMaster()
                obj.ROUTE_NO = txtRouteNo.Value
                obj.ROUTE_NAME = txtRouteName.Text
                obj.ROUTE_NAME_HINDI = txtRouteNameHindi.Text
                obj.Distance = txtDistance.Text
                obj.Rate = txtRate.Text

                ' Ticket No : BHA/31/07/18-000204 - Weight Feild add 
                obj.Weight = txtWeight.Text
                Dim strDistance As Double = 0
                Dim strRate As Double = 0
                If txtDistance.Text = "" Then
                    strDistance = 0
                Else
                    strDistance = txtDistance.Text
                End If
                If txtRate.Text = "" Then
                    strRate = 0
                Else
                    strRate = txtRate.Text
                End If
                obj.Amount = clsCommon.myCdbl(strDistance) * clsCommon.myCdbl(strRate)
                obj.TollAmount = txtTollAmount.Value
                obj.Location_Code = txtToLocationCode.Value
                obj.IsContractor = IIf(chkForContractor.Checked = True, 1, 0)
                obj.IsDefault = IIf(chkDefault.Checked = True, 1, 0)
                obj.Tanker_No = txtTankerNo.Value
                obj.arrMCC = txtMCC.arrValueMember
                obj.CuttOff_Time = txtcuttofftime.Value
                obj.Schedule_Time_Morning = txtScheduleTimeM.Value
                obj.Schedule_Time_Evening = txtScheduleTimeE.Value
                obj.Schedule_Time = txtScheduleTime.Value

                obj.Arr = New List(Of clsBulkRoutdetail)
                For Each row As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsBulkRoutdetail()
                    'objTr.BULK_ROUTE_no = txtRouteNo.Value

                    objTr.Location_Code = clsCommon.myCstr(row.Cells(colLocationCode).Value)
                    objTr.Distance = clsCommon.myCDecimal(row.Cells(colDistance).Value)
                    objTr.lOCATION_desc = clsCommon.myCstr(row.Cells(colLocationName).Value)
                    If (clsCommon.myLen(objTr.Location_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (clsBulkRoutMaster.SaveData(obj)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully")
                    LoadData(obj.ROUTE_NO, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
            myMessages.blankValue(Me, "Route No", Me.Text)
            txtRouteNo.Focus()
            Return False
        End If
        If clsCommon.myLen(txtRouteName.Text) <= 0 Then
            myMessages.blankValue(Me, "Route Name", Me.Text)
            txtRouteName.Focus()
            Return False
        End If
        If clsCommon.myLen(txtRouteNo.Value) > 30 Then
            clsCommon.MyMessageBoxShow(Me, "Route code Length is greater then 30.")
            txtRouteNo.Focus()
            Return False
        End If
        If String.IsNullOrEmpty(txtWeight.Text) = True Then
            myMessages.blankValue(Me, "Weight", Me.Text)
            txtWeight.Focus()
            Return False
        End If

        If clsCommon.myLen(txtToLocationCode.Value) <= 0 AndAlso chkForContractor.Checked = True Then
            clsCommon.MyMessageBoxShow(Me, "Please Select Location")
            txtToLocationCode.Focus()
            Return False
        End If
        If clsCommon.myLen(txtWeight.Value) <= 0 Then
            myMessages.blankValue(Me, "Weight", Me.Text)
            txtWeight.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub rdbtndelete_Click(sender As Object, e As EventArgs) Handles rdbtndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record")
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsBulkRoutMaster.DeleteData(txtRouteNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub txtRouteNo_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRouteNo.KeyPress
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Dim str As String = "select count(*) from TSPL_BULK_ROUTE_MASTER where ROUTE_NO ='" + txtRouteNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtRouteNo.MyReadOnly = False
        Else
            txtRouteNo.MyReadOnly = True
        End If
        If txtRouteNo.MyReadOnly OrElse isButtonClicked Then
            txtRouteNo.Value = clsBulkRoutMaster.getFinder("", txtRouteNo.Value, isButtonClicked)
            If txtRouteNo.Value <> "" Then
                LoadData(txtRouteNo.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub FrmBulkRoutMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Try
            Dim qry As String = "select MCC_Code,MCC_NAME from TSPL_MCC_MASTER  
            where not exists(select 1 from TSPL_BULK_ROUTE_MASTER_MCC where TSPL_BULK_ROUTE_MASTER_MCC.MCC_Code=TSPL_MCC_MASTER.MCC_Code and TSPL_BULK_ROUTE_MASTER_MCC.ROUTE_NO not in ('" + txtRouteNo.Value + "'))"
            txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("PCUMCC", qry, "MCC_Code", "MCC_NAME", txtMCC.arrValueMember, txtMCC.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtToLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtToLocationCode._MYValidating
        Try
            Dim whrclas As String = ""
            Dim qry As String = "Select  TSPL_LOCATION_MASTER.Location_Code as Code,TSPL_LOCATION_MASTER.Location_Desc as Description  from TSPL_LOCATION_MASTER  "
            If clsCommon.myLen(clsCommon.myCstr(objCommonVar.strCurrUserLocations)) > 0 Then
                whrclas = "TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtToLocationCode.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("USERLOC1", qry, "Code", whrclas, txtToLocationCode.Value, "Code", isButtonClicked))

            If clsCommon.myLen(txtToLocationCode.Value) > 0 Then
                lblToLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtToLocationCode.Value) + "'"))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub chkForContainer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkForContractor.ToggleStateChanged
        If chkForContractor.Checked = True Then
            txtToLocationCode.Enabled = True
            txtMCC.Enabled = False
            txtMCC.arrValueMember = Nothing
        Else
            txtToLocationCode.Enabled = False
            txtToLocationCode.Value = ""
            lblToLocationName.Text = ""
            txtMCC.Enabled = True
        End If
    End Sub

    Private Sub txtRouteNameHindi_Enter(sender As Object, e As EventArgs) Handles txtRouteNameHindi.Enter
        clsMccMaster.ToHindiInput()
    End Sub

    Private Sub txtRouteNameHindi_Leave(sender As Object, e As EventArgs) Handles txtRouteNameHindi.Leave
        clsMccMaster.ToEnglishInput()
    End Sub

    Private Sub txtTankerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTankerNo._MYValidating
        Try
            txtTankerNo.Value = clsfrmTankerMaster.GetFinder(" isnull( TSPL_TANKER_MASTER.Inactive,0)=0 ", txtTankerNo.Value, isButtonClicked)
            txtVehicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select TANKER_NAME from TSPL_TANKER_MASTER where Tanker_No='" & txtTankerNo.Value & "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtScheduleTimeE_ValueChanged(sender As Object, e As EventArgs) Handles txtScheduleTimeE.ValueChanged

    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtRouteNo.Value, "ROUTE_NO", "TSPL_BULK_ROUTE_MASTER")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colSNO
        repoLineNo.Width = 50
        repoLineNo.IsVisible = True
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)
        Dim repoRouteNumber As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteNumber.FormatString = ""
        repoRouteNumber.HeaderText = "Location Code"
        repoRouteNumber.Name = colLocationCode
        repoRouteNumber.HeaderImage = My.Resources.search4
        repoRouteNumber.TextImageRelation = TextImageRelation.TextBeforeImage
        repoRouteNumber.Width = 100
        repoRouteNumber.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoRouteNumber)
        Dim repoRouteName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRouteName.FormatString = ""
        repoRouteName.HeaderText = "Location Name"
        repoRouteName.Name = colLocationName
        repoRouteName.Width = 150
        repoRouteName.IsVisible = True
        'repoRouteName.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRouteName)

        Dim repoDistance As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDistance.FormatString = ""
        repoDistance.HeaderText = "Distance"
        repoDistance.Name = colDistance
        repoDistance.Width = 150
        repoDistance.IsVisible = True
        'repoRouteName.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoDistance)
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnChooser = True
        gv1.AllowColumnReorder = True
        gv1.Rows.AddNew()
    End Sub
    '    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs)
    '        Try
    '            Dim strLocCode As String = Nothing

    '            If (Not isInsideLoadData) Then
    '                If Not isCellValueChangedOpen Then
    '                    isCellValueChangedOpen = True
    '                    If e.Column Is gv1.Columns(colLocationCode) Then
    '                        'If rbtnDistributor.IsChecked Then

    '                        strLocCode = clsBulkRoutMaster.getFinder1("", clsCommon.myCstr(gv1.CurrentRow.Cells(colLocationCode).Value), False)
    '                    End If
    '                    gv1.CurrentRow.Cells(colLocationCode).Value = strLocCode
    '                    gv1.CurrentRow.Cells(colLocationName).Value = clsDBFuncationality.getSingleValue("SELECT TSPL_BULK_ROUTE_MASTER.Location_Code as Code,TSPL_LOCATION_MASTER.Location_Desc as Name  FROM TSPL_BULK_ROUTE_MASTER
    'left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_BULK_ROUTE_MASTER.Location_Code where TSPL_BULK_ROUTE_MASTER.Location_code='" & strLocCode & "' ")
    '                    gv1.CurrentRow.Cells(colDistance).Value = colDistance
    '                    isCellValueChangedOpen = False

    '                End If
    '            End If

    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '            isCellValueChangedOpen = False
    '        End Try
    '    End Sub

    'Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs)
    '    If gv1.Rows.Count > 0 Then
    '        If gv1.CurrentRow.Index = gv1.Rows.Count - 1 Then
    '            gv1.Rows(gv1.Rows.Count - 1).Cells(colSNO).Value = gv1.Rows.Count
    '            gv1.Rows.AddNew()
    '            gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 2)
    '        End If
    '    End If
    'End Sub
    'Private Sub gv1_UserAddedRow(sender As Object, e As GridViewRowEventArgs)
    '    For i As Integer = 0 To gv1.Rows.Count - 1
    '        gv1.Rows(0).Cells(0).Value = 1
    '        If i <> 0 Then
    '            gv1.Rows(i).Cells(colSNO).Value = i + 1
    '        End If
    '    Next
    'End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSNO).Value = ii
        Next
    End Sub
    'Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs)
    '    Try
    '        RefeshSNO()
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub gv1_CellValueChanged_1(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            Dim strLocCode As String = Nothing

            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colLocationCode) Then
                        'If rbtnDistributor.IsChecked Then

                        strLocCode = clsBulkRoutMaster.getFinder1("", clsCommon.myCstr(gv1.CurrentRow.Cells(colLocationCode).Value), False)
                        gv1.CurrentRow.Cells(colLocationCode).Value = strLocCode

                        gv1.CurrentRow.Cells(colLocationName).Value = clsDBFuncationality.getSingleValue("SELECT TSPL_LOCATION_MASTER.Location_Desc as Name FROM tspl_location_master where tspl_location_master.Location_code='" & strLocCode & "' ")
                        gv1.CurrentRow.Cells(colDistance).Value = Xtra.MyNoDecimalToDecimal(gv1.CurrentRow.Cells(colDistance).Value)
                    End If
                    'gv1.CurrentRow.Cells(colLocationCode).Value = strLocCode

                    isCellValueChangedOpen = False
                End If
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Private Sub gv1_UserAddedRow_1(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colSNO).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gv1_UserDeletedRow_1(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            RefeshSNO()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged_1(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.Rows.Count > 0 Then
            If gv1.CurrentRow.Index = gv1.Rows.Count - 1 Then
                gv1.Rows(gv1.Rows.Count - 1).Cells(colSNO).Value = gv1.Rows.Count
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 2)
            End If
        End If
    End Sub
End Class
