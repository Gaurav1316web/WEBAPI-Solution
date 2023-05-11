Imports System.IO
Imports System.Data.SqlClient
Imports common
Public Class FrmLocationDistanceMaster
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colFromLocCode As String = "colFromLocCode"
    Const colFromLocDesc As String = "colFromLocDesc"
    Const colToLocCode As String = "colToLocCode"
    Const colToLocDesc As String = "colToLocDesc"
    Const colDistance As String = "colDistance"
    Const colTollAmt As String = "colTollAmt"
    Dim isNewEntry As Boolean = True
    Dim isLoadData As Boolean = False
    Dim isValueChanged As Boolean = True
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.LocationDistanceMapping)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        isLoadData = False
        isValueChanged = True
        gv.Rows.Clear()
        gv.Rows.AddNew()
        isNewEntry = True
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub

    Sub LoadData()
        Try
            gv.Rows.Clear()
            gv.Rows.AddNew()

            isNewEntry = True
            Dim qry As String = "select * from tspl_location_distance_master"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            isLoadData = True
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                isNewEntry = False
                For Each dr As DataRow In dt.Rows
                    gv.Rows(gv.Rows.Count - 1).Cells(colFromLocCode).Value = clsCommon.myCstr(dr("from_location_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colFromLocDesc).Value = clsLocation.GetName(clsCommon.myCstr(dr("from_location_code")), Nothing)
                    gv.Rows(gv.Rows.Count - 1).Cells(colToLocCode).Value = clsCommon.myCstr(dr("to_location_code"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colToLocDesc).Value = clsLocation.GetName(clsCommon.myCstr(dr("to_location_code")), Nothing)
                    gv.Rows(gv.Rows.Count - 1).Cells(colDistance).Value = clsCommon.myCdbl(dr("distance"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colTollAmt).Value = clsCommon.myCdbl(dr("Toll_Amt"))
                    gv.Rows.AddNew()
                Next

                btnsave.Text = "Update"
                btndelete.Enabled = True
            End If

            isLoadData = False
        Catch ex As Exception
            isNewEntry = True
            isLoadData = False
        End Try
    End Sub

    Private Sub frmLocationDistanceMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub frmLocationDistanceMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        Reset()
        LoadData()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")


    End Sub

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Name = colFromLocCode
        repocode.Width = 100
        repocode.HeaderText = "From Location"
        repocode.ReadOnly = False
        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)


        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.Name = colFromLocDesc
        reponame.Width = 205
        reponame.HeaderText = "From Location Description"
        reponame.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reponame)


        Dim repocode1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode1.Name = colToLocCode
        repocode1.Width = 100
        repocode1.HeaderText = "To Location"
        repocode1.ReadOnly = False
        repocode1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode1.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode1)


        Dim reponame1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame1.FormatString = ""
        reponame1.Name = colToLocDesc
        reponame1.Width = 205
        reponame1.HeaderText = "To Location Description"
        reponame1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reponame1)

        Dim repoDistance As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDistance.Name = colDistance
        repoDistance.Width = 80
        repoDistance.HeaderText = "Distance"
        gv.MasterTemplate.Columns.Add(repoDistance)

        ''richa agarwal add Toll amount column 27 May
        Dim repoTollAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTollAmt.Name = colTollAmt
        repoTollAmt.Width = 80
        repoTollAmt.HeaderText = "Toll Amount"
        gv.MasterTemplate.Columns.Add(repoTollAmt)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.Rows.AddNew()
    End Sub

    Function AllowToSave() As Boolean
        Try
            For i As Integer = 0 To gv.Rows.Count - 2
                For j As Integer = i + 1 To gv.Rows.Count - 1
                    If (clsCommon.CompairString(gv.Rows(i).Cells(colFromLocCode).Value, gv.Rows(j).Cells(colFromLocCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv.Rows(i).Cells(colToLocCode).Value, gv.Rows(j).Cells(colToLocCode).Value) = CompairStringResult.Equal) OrElse (clsCommon.CompairString(gv.Rows(i).Cells(colToLocCode).Value, gv.Rows(j).Cells(colFromLocCode).Value) = CompairStringResult.Equal AndAlso clsCommon.CompairString(gv.Rows(i).Cells(colFromLocCode).Value, gv.Rows(j).Cells(colToLocCode).Value) = CompairStringResult.Equal) Then
                        clsCommon.MyMessageBoxShow("Duplicate Row Found At Row no: " & (j + 1))
                        Return False
                    End If
                Next
            Next


            For i As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myCdbl(gv.Rows(i).Cells(colDistance).Value) < 0 AndAlso clsCommon.myLen(gv.Rows(i).Cells(colFromLocCode).Value) > 0 AndAlso clsCommon.myLen(gv.Rows(i).Cells(colToLocCode).Value) > 0 Then
                    clsCommon.MyMessageBoxShow(" Distance can't be negative At Row No: " & (i + 1))
                    Return False
                End If

                If clsCommon.myCdbl(gv.Rows(i).Cells(colTollAmt).Value) < 0 AndAlso clsCommon.myLen(gv.Rows(i).Cells(colFromLocCode).Value) > 0 AndAlso clsCommon.myLen(gv.Rows(i).Cells(colToLocCode).Value) > 0 Then
                    clsCommon.MyMessageBoxShow(" Toll Amount can't be negative At Row No: " & (i + 1))
                    Return False
                End If
            Next

            Dim rowCount As Integer = 0
            For i As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(gv.Rows(i).Cells(colFromLocCode).Value) > 0 AndAlso clsCommon.myLen(gv.Rows(i).Cells(colToLocCode).Value) > 0 Then
                    rowCount = rowCount + 1
                End If
            Next

            If rowCount = 0 Then
                clsCommon.MyMessageBoxShow(" Please Fill data in atleast one Row")
                Return False
            End If

            For i As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myCdbl(gv.Rows(i).Cells(colDistance).Value) = 0 AndAlso clsCommon.myLen(gv.Rows(i).Cells(colFromLocCode).Value) > 0 AndAlso clsCommon.myLen(gv.Rows(i).Cells(colToLocCode).Value) > 0 Then
                    clsCommon.MyMessageBoxShow("Please Fill Distance At Row No: " & (i + 1))
                    Return False
                End If
            Next

            For i As Integer = 0 To gv.Rows.Count - 1
                If clsCommon.myLen(gv.Rows(i).Cells(colFromLocCode).Value) > 0 AndAlso clsCommon.myLen(gv.Rows(i).Cells(colToLocCode).Value) > 0 AndAlso clsCommon.CompairString(gv.Rows(i).Cells(colToLocCode).Value, gv.Rows(i).Cells(colFromLocCode).Value) = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow(" From and To Location Can't Be Same At Row No: " & (i + 1))
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.LocationDistanceMapping, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim arr As New List(Of clsLocationDistanceMaster)


            For Each grow As GridViewRowInfo In gv.Rows
                Dim obj As New clsLocationDistanceMaster()
                obj.From_Location_code = clsCommon.myCstr(grow.Cells(colFromLocCode).Value)
                obj.to_Location_Code = clsCommon.myCstr(grow.Cells(colToLocCode).Value)
                obj.Distance = clsCommon.myCdbl(grow.Cells(colDistance).Value)
                obj.Toll_Amt = clsCommon.myCdbl(grow.Cells(colTollAmt).Value)
                If clsCommon.myLen(obj.From_Location_code) > 0 AndAlso clsCommon.myLen(obj.to_Location_Code) > 0 AndAlso clsCommon.myCdbl(obj.Distance) > 0 Then
                    arr.Add(obj)
                End If
            Next

            If clsLocationDistanceMaster.SaveData(arr, trans) Then
                trans.Commit()
                If clsCommon.CompairString(btnsave.Text, "Save") = CompairStringResult.Equal Then
                    clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                End If
                btnsave.Text = "Update"
                btndelete.Enabled = True
                LoadData()
            Else
                btnsave.Text = "Save"
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(gv.Rows(0).Cells(colFromLocCode).Value) <= 0 AndAlso clsCommon.myLen(gv.Rows(0).Cells(colToLocCode).Value) <= 0 AndAlso clsCommon.myCdbl(gv.Rows(0).Cells(colDistance).Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Select Data For Deletion First", Me.Text)
            Return
        End If

        If Not myMessages.deleteConfirm() Then
            Return
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim qry As String = "delete from tspl_location_distance_master "
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
            trans.Commit()
            clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)

            Reset()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        'Dim qry As String = "select count(*) from tspl_location_distance_master "
        'Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        'If check > 0 Then
        '    '            qry = "select tspl_location_distance_master.from_location_code,tspl_location_distance_master.to_location_code,tspl_location_distance_master.distance from tspl_location_distance_master left outer join tspl_location_master on tspl_parameter_range_master.code=tspl_parameter_master.code and tspl_parameter_master.comp_code=tspl_parameter_range_master.comp_code"
        'Else
        '    '           qry = "select '' as Code,'' as Description,'' as Effective_Date,'' as Lower_Range,'' as Upper_Range,'' as Value"
        'End If
        'transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        'Dim gv1 As New RadGridView()
        'Me.Controls.Add(gv1)
        'Dim currentdate As Date = Date.Today
        'If transportSql.importExcel(gv1, "Code", "Description", "Effective_Date", "Lower_Range", "Upper_Range", "Value") Then
        '    Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        '    clsCommon.ProgressBarShow()
        '    Try
        '        Dim arr As New List(Of clsfrmParameterRangeMaster)
        '        Dim counter As Integer = 1

        '        For Each grow As GridViewRowInfo In gv1.Rows
        '            Dim obj As New clsfrmParameterRangeMaster()

        '            obj.code = clsCommon.myCstr(grow.Cells("Code").Value)
        '            obj.desc = clsCommon.myCstr(grow.Cells("Description").Value)
        '            If clsDBFuncationality.getSingleValue("select count(*) from tspl_parameter_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and code='" & obj.code & "' and nature='r'", trans) = 0 Then
        '                Throw New Exception("Please Fill Valid Code Of Parameter, Specified Code Not found in Master At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            If clsCommon.myLen(obj.code) <= 0 AndAlso clsCommon.myLen(obj.desc) <= 0 Then
        '                Throw New Exception("Please Fill Code/Description Of Parameter At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If
        '            If clsCommon.myLen(obj.code) <= 0 AndAlso clsCommon.myLen(obj.desc) > 0 Then
        '                Dim qry As String = "select code from tspl_parameter_master where description='" + obj.desc + "'"
        '                obj.code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
        '            End If
        '            If clsCommon.myLen(obj.code) <= 0 Then
        '                Throw New Exception("Please Fill Code Of Parameter At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If


        '            Try
        '                obj.Eff_date = Convert.ToDateTime(grow.Cells("effective_date").Value).ToString("dd/MMM/yyyy")
        '            Catch exx As Exception
        '                obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MMM/yyyy")
        '            End Try

        '            Try
        '                If obj.Eff_date.ToString().Substring(6, 4) = "0001" Then
        '                    obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        '                End If
        '            Catch exx As Exception
        '                obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        '            End Try

        '            obj.Lrange = clsCommon.myCdbl(grow.Cells("lower_range").Value)
        '            If clsCommon.myLen(obj.code) > 0 AndAlso clsCommon.myLen(obj.Lrange) <= 0 Then
        '                Throw New Exception("Please Fill Lower Range At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If

        '            obj.Urange = clsCommon.myCdbl(grow.Cells("upper_range").Value)
        '            If clsCommon.myLen(obj.code) > 0 AndAlso clsCommon.myLen(obj.Urange) <= 0 Then
        '                Throw New Exception("Please Fill Upper Range At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If

        '            obj.Value = clsCommon.myCstr(grow.Cells("value").Value)
        '            If clsCommon.myLen(obj.code) > 0 AndAlso clsCommon.myLen(obj.Value) <= 0 Then
        '                Throw New Exception("Please Fill Value At Line No. " + clsCommon.myCstr(counter) + "")
        '            End If

        '            If clsCommon.myLen(obj.code) > 0 Then
        '                arr.Add(obj)
        '            End If

        '            counter += 1
        '        Next

        '        clsCommon.ProgressBarHide()
        '        If clsfrmParameterRangeMaster.SaveData(arr, trans) Then
        '            clsCommon.MyMessageBoxShow("Data Transfer Successfully", Me.Text)
        '        Else
        '            clsCommon.MyMessageBoxShow("No Data Transfer", Me.Text)
        '        End If
        '        Reset()
        '        LoadData()

        '    Catch ex As Exception
        '        clsCommon.ProgressBarHide()
        '        clsCommon.MyMessageBoxShow(ex.Message)
        '    End Try
        'End If
        'Me.Controls.Remove(gv1)
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isLoadData Then '------when on loaddata then it should not run
            If isValueChanged Then
                If gv.CurrentColumn Is gv.Columns(colFromLocCode) Then
                    isValueChanged = False
                    OpenLocationFinder(1) ''''' 1 for from_location_code
                    isValueChanged = True
                ElseIf gv.CurrentColumn Is gv.Columns(colToLocCode) Then
                    isValueChanged = False
                    OpenLocationFinder(2) '' 2 for To_location_code
                    isValueChanged = True
                End If
            End If
        End If
        ' isValueChanged = True
    End Sub

    Sub OpenLocationFinder(ByVal colType As Integer)


        If colType = 1 Then
            gv.CurrentRow.Cells(colFromLocCode).Value = clsLocation.getFinder("", gv.CurrentRow.Cells(colFromLocCode).Value, True)
            gv.CurrentRow.Cells(colFromLocDesc).Value = clsCommon.myCstr(clsLocation.GetName(gv.CurrentRow.Cells(colFromLocCode).Value, Nothing))
        Else
            gv.CurrentRow.Cells(colToLocCode).Value = clsLocation.getFinder("", gv.CurrentRow.Cells(colToLocCode).Value, True)
            gv.CurrentRow.Cells(colToLocDesc).Value = clsCommon.myCstr(clsLocation.GetName(gv.CurrentRow.Cells(colToLocCode).Value, Nothing))
        End If

    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intcurrrow As Integer = gv.CurrentRow.Index
            If intcurrrow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intcurrrow)
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow

        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        End If

    End Sub


End Class
