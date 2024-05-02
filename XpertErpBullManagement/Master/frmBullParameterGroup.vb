Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine


Public Class frmBullParameterGroup
    Dim isNewEntry As Boolean = True
    Const ColCode As String = "ColCode"
    Const ColName As String = "ColName"
    Const ColType As String = "ColType"
    Const ColTPCode As String = "ColTPCode"
    'Const ColReqforResult As String = "ColReqforResult"
    Const ColReqforResult As String = "ColReqforResult"
    Const colRangeTo As String = "colRangeTo"
    Const colRangeFrom As String = "colRangeFrom"
    Const colBoolean As String = "colBoolean"
    Const colAlphaNumeric As String = "colAlphaNumeric"
    Const colRangeSelection As String = "colRangeSelection"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = True
    Dim ErrorControl As New clsErrorControl()

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBookingProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag

    End Sub

    Private Sub frmBullParameterGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            loadBlankGrid()
            CreateTable()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION Primary Key")
        coll.Add("Code", "varchar(30) NOT NULL REFERENCES TSPL_BULL_PARAMETER_GROUP_MASTER (Code)")
        coll.Add("TPCode", "varchar(30) NOT NULL REFERENCES TSPL_BULL_TEST_PARAMETER (Code)")
        coll.Add("Required_For_Result", "Char(1) null")
        coll.Add("From_Range", "integer null")
        coll.Add("To_Range", "integer null")
        coll.Add("R_Boolean", "Char(1) null")
        coll.Add("Alpha_Numeric", "NVARCHAR(20) null")
        coll.Add("Range_Selection", "integer null")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_BULL_PARAMETER_GROUP_DETAIL", coll)


        Dim colll As Dictionary(Of String, String)
        colll = New Dictionary(Of String, String)()
        colll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        colll.Add("Against_Group_Code", "varchar(30) NOT NULL REFERENCES TSPL_BULL_PARAMETER_GROUP_MASTER (Code)")
        colll.Add("Against_Detail_PK_Id", "integer NOT NULL REFERENCES TSPL_BULL_PARAMETER_GROUP_DETAIL (PK_Id)")
        colll.Add("Range_Selection", "Varchar(30) null")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_BULL_PARAMETER_GROUP_DETAIL_RANGE", colll)
    End Sub



    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            MessageBox.Show(Me, err.Message, Me.Text)
        End Try
    End Sub
    Sub loadBlankGrid()
        'gv1.Rows.Clear()

        Try
            Dim qry As String = String.Empty

            gv1.Rows.Clear()
            gv1.Columns.Clear()

            Dim gridcolCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            gridcolCode.FormatString = ""
            gridcolCode.HeaderText = "Code"
            gridcolCode.Name = ColCode
            gridcolCode.Width = 110
            gridcolCode.ReadOnly = False

            gv1.MasterTemplate.Columns.Add(gridcolCode)

            Dim gridcolName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Name"
            gridcolName.Name = ColName
            gridcolName.Width = 110
            gridcolName.ReadOnly = True

            gv1.MasterTemplate.Columns.Add(gridcolName)
            gridcolName.ReadOnly = True

            Dim gridcoltype As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            gridcoltype.FormatString = ""
            gridcoltype.HeaderText = "Type"
            gridcoltype.Name = ColType
            gridcoltype.Width = 110
            gridcoltype.ReadOnly = True
            gv1.MasterTemplate.Columns.Add(gridcoltype)
            gridcoltype.ReadOnly = True

            Dim gridcolReqforResult As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            gridcolReqforResult.FormatString = ""
            gridcolReqforResult.HeaderText = "Req. For Result"
            gridcolReqforResult.Name = ColReqforResult
            gridcolReqforResult.Width = 110
            gv1.MasterTemplate.Columns.Add(gridcolReqforResult)

            Dim gridcolRangeFrom As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            gridcolRangeFrom.FormatString = ""
            gridcolRangeFrom.HeaderText = "From Range"
            gridcolRangeFrom.Name = colRangeFrom
            gridcolRangeFrom.Width = 110
            gv1.MasterTemplate.Columns.Add(gridcolRangeFrom)
            'gridcolRangeFrom.IsVisible = False

            Dim gridcolRangeTo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            gridcolRangeTo.FormatString = ""
            gridcolRangeTo.HeaderText = "To Range"
            gridcolRangeTo.Name = colRangeTo
            gridcolRangeTo.Width = 110
            gv1.MasterTemplate.Columns.Add(gridcolRangeTo)
            'gridcolRangeTo.IsVisible = False



            Dim gridcolBoolean As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
            gridcolBoolean.FormatString = ""
            gridcolBoolean.HeaderText = "Boolean"
            gridcolBoolean.Name = colBoolean
            gridcolBoolean.Width = 110
            gv1.MasterTemplate.Columns.Add(gridcolBoolean)
            'gridcolBoolean.IsVisible = False

            Dim gridcolAlphaNumeric As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            gridcolAlphaNumeric.FormatString = ""
            gridcolAlphaNumeric.HeaderText = "Alpha Numeric"
            gridcolAlphaNumeric.Name = colAlphaNumeric
            gridcolAlphaNumeric.Width = 110
            gv1.MasterTemplate.Columns.Add(gridcolAlphaNumeric)

            Dim gridcolRangeSelection As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            gridcolRangeSelection.FormatString = ""
            gridcolRangeSelection.HeaderText = "Range Selection"
            gridcolRangeSelection.Name = colRangeSelection
            gridcolRangeSelection.Width = 110
            gv1.MasterTemplate.Columns.Add(gridcolRangeSelection)
            'gridcolRangeSelection.IsVisible = False
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
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Dim qry As String = "select count(*) from TSPL_BULL_BREED_MASTER"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

        If check > 0 Then
            qry = "select Code,Name from TSPL_BULL_PARAMETER_GROUP_MASTER"
        Else
            qry = "select '' as Code,'' as Name"
        End If

        transportSql.ExporttoExcel(qry, Me)
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            txtCode.Focus()
            clsCommon.MyMessageBoxShow(Me, "Bull Code can't be blank", Me.Text)
            Exit Function
            Return False
        End If
    End Function
    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim obj As New clsBullParameterGroup()
                obj.Code = txtCode.Value
                obj.Name = txtname.Text

                obj.Arr = New List(Of clsBullParameterGroupDetail)
                For Each row As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsBullParameterGroupDetail()
                    'objTr.TPCode = clsCommon.myCstr(row.Cells(ColTPCode).Value)
                    objTr.Code = clsCommon.myCstr(row.Cells(ColCode).Value)
                    If clsCommon.CompairString(clsCommon.myCstr(row.Cells(ColReqforResult).Value), "True") = CompairStringResult.Equal Then
                        objTr.Required_For_Result = "Y"
                    Else
                        objTr.Required_For_Result = "N"
                    End If
                    objTr.From_Range = clsCommon.myCDecimal(row.Cells(colRangeFrom).Value)
                    objTr.To_Range = clsCommon.myCDecimal(row.Cells(colRangeTo).Value)

                    If clsCommon.CompairString(clsCommon.myCstr(row.Cells(colBoolean).Value), "True") = CompairStringResult.Equal Then
                        objTr.R_Boolean = "Y"
                    Else
                        objTr.R_Boolean = "N"
                    End If
                    objTr.Alpha_Numeric = clsCommon.myCstr(row.Cells(colAlphaNumeric).Value)
                    objTr.Range_Selection = clsCommon.myCDecimal(row.Cells(colRangeSelection).Value)
                    If (clsCommon.myLen(objTr.Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If
                Next

                Dim Sqlqry As String = "select count(1) from TSPL_BULL_PARAMETER_GROUP_MASTER where Code ='" + txtCode.Value + "'"
                Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
                If count = 0 Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsBullParameterGroup.SaveData(obj, isNewEntry)) Then
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                    'LoadData(obj.Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            gv1.DataSource = Nothing
            gv1.Refresh()
            isInsideLoadData = True
            txtCode.MyReadOnly = True

            Dim obj As clsBullParameterGroup = clsBullParameterGroup.GetData(strCode, NavTyep)
            If obj IsNot Nothing Then
                isNewEntry = False
                txtCode.Value = obj.Code
                txtname.Text = obj.Name

                txtCode.MyReadOnly = True
                btnsave.Text = "Update"
                btndelete.Enabled = True
                If obj.Arr IsNot Nothing Then
                    For Each objrow As clsBullParameterGroupDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColName).Value = objrow.Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColCode).Value = objrow.TPCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColType).Value = objrow.Type
                        If clsCommon.CompairString(objrow.Required_For_Result, "Y") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColReqforResult).Value = True
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColReqforResult).Value = False
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRangeFrom).Value = objrow.From_Range
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRangeTo).Value = objrow.To_Range
                        If clsCommon.CompairString(objrow.R_Boolean, "Y") = CompairStringResult.Equal Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBoolean).Value = True
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(colBoolean).Value = False
                        End If
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAlphaNumeric).Value = objrow.Alpha_Numeric
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRangeSelection).Value = objrow.Range_Selection
                        gv1.Rows.AddNew()
                    Next
                End If
                SetGridLayout()
            Else
            End If
            isInsideLoadData = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetGridLayout()
        Try
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                For Each rows In gv1.Rows
                    If clsCommon.myCstr(rows.Cells(ColCode).Value) IsNot Nothing AndAlso clsCommon.myLen(rows.Cells(ColCode).Value) > 0 Then
                        rows.Cells(ColCode).ReadOnly = True
                    Else
                        rows.Cells(ColCode).ReadOnly = False
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub AddNew()
        isInsideLoadData = False
        txtCode.Value = ""
        txtname.Text = ""
        txtCode.MyReadOnly = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        isNewEntry = True
        txtname.Focus()
        txtname.Select()
        loadBlankGrid()
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = ""
            Dim strCode As String = ""
            Dim count As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_BULL_PARAMETER_GROUP_MASTER where Code ='" + txtCode.Value + "'"))
            'If count <= 0 Then
            '    Exit Sub
            'End If
            qry = "select TSPL_BULL_PARAMETER_GROUP_MASTER.Code as Code,TSPL_BULL_PARAMETER_GROUP_MASTER.Name as [Name] from TSPL_BULL_PARAMETER_GROUP_MASTER"
            strCode = clsCommon.ShowSelectForm("RTY", qry, "Code", "", txtCode.Value, "TSPL_BULL_PARAMETER_GROUP_MASTER.Code asc", isButtonClicked, Nothing)

            If clsCommon.myLen(strCode) > 0 Then
                LoadData(strCode, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub gv1_CellValueChanged_1(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If isCellValueChangedOpen Then
                    isCellValueChangedOpen = False
                    If e.Column Is gv1.Columns(ColCode) Then
                        OpenICodeList(False)
                    End If
                    isCellValueChangedOpen = True
                End If
            End If


            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColType).Value), "Range") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColReqforResult).Value), "True") = CompairStringResult.Equal Then
                For Each row In gv1.Rows
                    gv1.CurrentRow.Cells(colBoolean).ReadOnly = True
                    gv1.CurrentRow.Cells(colAlphaNumeric).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeSelection).ReadOnly = True
                Next
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColType).Value), "Boolean") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColReqforResult).Value), "True") = CompairStringResult.Equal Then
                For Each row In gv1.Rows
                    gv1.CurrentRow.Cells(colRangeFrom).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeTo).ReadOnly = True
                    gv1.CurrentRow.Cells(colAlphaNumeric).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeSelection).ReadOnly = True
                Next
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColType).Value), "Alpha Numeric") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColReqforResult).Value), "True") = CompairStringResult.Equal Then
                For Each row In gv1.Rows
                    gv1.CurrentRow.Cells(colRangeFrom).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeTo).ReadOnly = True
                    gv1.CurrentRow.Cells(colBoolean).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeSelection).ReadOnly = True
                Next
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColType).Value), "Range Selection") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColReqforResult).Value), "True") = CompairStringResult.Equal Then
                For Each row In gv1.Rows
                    gv1.CurrentRow.Cells(colRangeFrom).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeTo).ReadOnly = True
                    gv1.CurrentRow.Cells(colBoolean).ReadOnly = True
                    gv1.CurrentRow.Cells(colAlphaNumeric).ReadOnly = True
                Next
            End If

            If clsCommon.myCDecimal(gv1.CurrentRow.Cells(colRangeSelection).Value) > 0 Then
                Dim frmP As New frmBullParameterRangeSelection()
                frmP.AddGridView(clsCommon.myCDecimal(gv1.CurrentRow.Cells(colRangeSelection).Value))
                frmP.Show()
                'formShow(frm, strProgramCode, strProgramName, isOpenInMDI, strDocNo, IFTrueShowFormElseShowDialog)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Try
            Dim whrls As String = Nothing
            Dim check As List(Of String) = New List(Of String)
            If gv1.Rows.Count > 1 Then
                For Each row In gv1.Rows
                    check.Add(clsCommon.myCstr(row.Cells("ColCode").Value))
                Next
            End If
            If check IsNot Nothing AndAlso check.Count > 0 Then
                whrls = "  Code not in (" + clsCommon.GetMulcallString(check) + ")"
            End If
            Dim qry As String = " Select Code,Name,Type from TSPL_BULL_TEST_PARAMETER "
            gv1.CurrentRow.Cells(ColCode).Value = clsCommon.ShowSelectForm("ItemFnder@PriceMstr", qry, "Code", whrls, clsCommon.myCstr(gv1.CurrentRow.Cells(ColCode).Value), "", isButtonClick)

            Dim whrcls As String = " where Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColCode).Value) + "'"
            qry = " Select Code,Name,Type from TSPL_BULL_TEST_PARAMETER" + whrcls
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(ColCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                gv1.CurrentRow.Cells(ColName).Value = clsCommon.myCstr(dt.Rows(0)("Name"))
                gv1.CurrentRow.Cells(ColType).Value = clsCommon.myCstr(dt.Rows(0)("Type"))
            End If

        Catch ex As Exception
            gv1.CurrentRow.Cells(ColCode).Value = ""
            gv1.CurrentRow.Cells(ColCode).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsBullParameterGroup.DeleteData(txtCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(sender As Object, e As EventArgs) Handles RadMenuItem4.Click
        Dim gv_Import As New RadGridView()
        Me.Controls.Add(gv_Import)
        Dim oldNewentry As Boolean = isNewEntry
        Dim counter As Integer = 0

        If transportSql.importExcel(gv_Import, "Code", "Name") Then
            Dim obj As New clsBullParameterGroup()

            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv_Import.Rows
                    obj.Code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.myLen(obj.Code) > 50 Then
                        Throw New Exception("Code has max. length 50 see at line no. " + clsCommon.myCstr(counter + 1) + "")
                    End If

                    obj.Name = clsCommon.myCstr(grow.Cells("Description").Value).Replace("'", "`")
                    If clsCommon.myLen(obj.Name) <= 0 Then
                        Throw New Exception("Fill description at line no. " + clsCommon.myCstr(counter + 1) + "")
                    End If
                    If clsCommon.myLen(obj.Name) > 200 Then
                        obj.Name = obj.Name.Substring(0, 200)
                    End If

                    Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(*) from TSPL_BULL_PARAMETER_GROUP_MASTER where code='" + obj.Code + "'")
                    isNewEntry = True
                    If qry > 0 Then
                        isNewEntry = False
                    End If

                    If (obj.SaveData(obj, isNewEntry)) Then

                    End If
                    counter += 1
                Next

                clsCommon.ProgressBarHide()

                If counter >= 1 Then
                    clsCommon.MyMessageBoxShow(Me, "Data transfer successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No data found to transfer", Me.Text)
                End If


            Catch ex As Exception
                clsCommon.ProgressBarHide()
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End If

        isNewEntry = oldNewentry
        Me.Controls.Remove(gv_Import)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BULL_TEST_PARAMETER where Code='" + txtCode.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class