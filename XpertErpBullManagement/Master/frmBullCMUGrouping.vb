Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine

Public Class frmBullCMUGrouping
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim isNewEntry As Boolean = True
    Const ColGroupCode As String = "ColGroupCode"
    Const ColParameterCode As String = "ColParameterCode"
    Const ColParameterName As String = "ColParameterName"
    Const ColParameterType As String = "ColParameterType"
    Const ColPkId As String = "ColPkId"
    Const ColGroupName As String = "ColGroupName"
    Const ColReqforResult As String = "ColReqforResult"
    Const colRangeTo As String = "colRangeTo"
    Const colRangeFrom As String = "colRangeFrom"
    Const colBoolean As String = "colBoolean"
    Const colAlphaNumeric As String = "colAlphaNumeric"
    Const colRS As String = "colRS"
    Const colRangeSelection As String = "colRangeSelection"
    Const colRangeBtn As String = "colRangeBtn"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = True
    Dim ErrorControl As New clsErrorControl()
#End Region
    Private Sub SetUserMgmtNew()
        Me.Form_ID = clsUserMgtCode.frmDairyGatePass
        MyBase.SetUserMgmt(clsUserMgtCode.frmDairyGatePass)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag

        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
    End Sub

    Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_No", "VARCHAR(30) NOT NULL PRIMARY KEY")
        coll.Add("Remarks", "VARCHAR(200) NULL")
        coll.Add("Name", "VARCHAR(200) NULL")
        coll.Add("Status", "integer NULL")
        coll.Add("Created_By", "varchar(20) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(20) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        coll.Add("Posted_By", "varchar(20)  NULL ")
        coll.Add("Posted_Date", "Datetime  NULL")
        'clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BULL_CURLING", coll)
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BULL_CMU_GROUPING", coll, Nothing, False, False, "", "Document_No", "")

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION Primary Key ")
        coll.Add("Document_No", "VARCHAR(30) NULL REFERENCES TSPL_BULL_CMU_GROUPING(Document_No) ")
        coll.Add("Against_Parameter_Group_Code", "Integer NULL REFERENCES TSPL_BULL_SHED_PARAMETER_DETAIL(PK_Id) ")
        coll.Add("Required_For_Result", "Char(1) null")
        coll.Add("From_Range", "integer null")
        coll.Add("To_Range", "integer null")
        coll.Add("R_Boolean", "Char(1) null")
        coll.Add("Alpha_Numeric", "NVARCHAR(20) null")
        coll.Add("Range_Selection", "integer null")
        'coll.Add("Parameter_Code", "VARCHAR(30) NULL REFERENCES TSPL_BULL_SHED_PARAMETER_MASTER(Code) ")
        ' clsCommonFunctionality.CreateOrAlterTable("TSPL_BULL_CURLING_Detail", coll)
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BULL_CMU_GROUPING_Detail", coll, Nothing, False, False, "TSPL_BULL_CMU_GROUPING", "Document_No", "")

        Dim colll As Dictionary(Of String, String)
        colll = New Dictionary(Of String, String)()
        colll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        colll.Add("Against_Group_Code", "varchar(30) NOT NULL REFERENCES TSPL_BULL_CMU_GROUPING (Document_No)")
        colll.Add("Against_Detail_PK_Id", "integer NOT NULL REFERENCES TSPL_BULL_CMU_GROUPING_Detail (PK_Id)")
        colll.Add("Range_Selection", "Varchar(30) null")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_BULL_CMU_GROUPING_DETAIL_RANGE", colll)

    End Sub
    Private Sub frmBullCMUGrouping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            CreateTable()
            AddNew()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub loadBlankGrid()
        Try
            gv1.Rows.Clear()
            gv1.Columns.Clear()

            Dim gridcolName As GridViewTextBoxColumn = New GridViewTextBoxColumn()

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Group Code"
            gridcolName.Name = ColGroupCode
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Group Name"
            gridcolName.Name = ColGroupName
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Parameter Code"
            gridcolName.Name = ColParameterCode
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Parameter Name"
            gridcolName.Name = ColParameterName
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Parameter Type"
            gridcolName.Name = ColParameterType
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName = New GridViewTextBoxColumn()
            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Pk_Id"
            gridcolName.Name = ColPkId
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.ReadOnly = True
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

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

            'Dim gridcolRS As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            'gridcolRS.FormatString = ""
            'gridcolRS.HeaderText = "Range"
            'gridcolRS.Name = colRS
            'gridcolRS.Width = 110
            'gv1.MasterTemplate.Columns.Add(gridcolRS)

            'Dim gridcolRangeSelection As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            'gridcolRangeSelection.FormatString = ""
            'gridcolRangeSelection.HeaderText = "Range Selection"
            'gridcolRangeSelection.Name = colRangeSelection
            'gridcolRangeSelection.Width = 110
            'gv1.MasterTemplate.Columns.Add(gridcolRangeSelection)
            'gridcolRangeSelection.IsVisible = True

            Dim ShowBtn As New GridViewCommandColumn()
            ShowBtn.FormatString = ""
            ShowBtn.UseDefaultText = True
            ShowBtn.DefaultText = "Range"
            ShowBtn.HeaderText = ""
            ShowBtn.Name = colRangeBtn
            ShowBtn.FieldName = colRangeBtn
            ShowBtn.Width = 100
            ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
            gv1.MasterTemplate.Columns.Add(ShowBtn)


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
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click
        Reset()
    End Sub

    Sub Reset()
        fndCode.Value = Nothing
        txtMultiGroup.arrValueMember = Nothing
        txtMultiGroup.arrDispalyMember = Nothing
        txtremarks.Text = ""
        dtpDate.Value = clsCommon.GETSERVERDATE()
        gv1.DataSource = Nothing
        loadBlankGrid()
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Enabled = True
        btnsave.Text = "Save"
        btndelete.Enabled = True
        btnPost.Enabled = True
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Public Sub AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
        fndCode.MyReadOnly = False
        fndCode.Value = Nothing
        txtMultiGroup.arrValueMember = Nothing
        txtMultiGroup.arrDispalyMember = Nothing
        dtpDate.Value = clsCommon.GETSERVERDATE()
        txtremarks.Text = ""
        TxtName.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        'isNewEntry = True
        loadBlankGrid()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            'loadBlankGrid()
            gv1.DataSource = Nothing
            gv1.Refresh()
            isInsideLoadData = True
            fndCode.MyReadOnly = True
            gv1.MasterTemplate.SummaryRowsBottom.Clear()

            Dim obj As ClsCMUGrouping = ClsCMUGrouping.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
                isNewEntry = False
                fndCode.Value = obj.Code
                txtremarks.Text = obj.Remarks
                TxtName.Text = obj.Name
                fndCode.MyReadOnly = True

                'Dim arrUserType As New ArrayList
                Dim qry As String = ""
                Dim dt As New DataTable
                qry = " select TSPL_BULL_SHED_PARAMETER_DETAIL.PK_Id,TSPL_BULL_SHED_PARAMETER_DETAIL.Code as GroupCode from TSPL_BULL_CMU_GROUPING_Detail
                                     left outer join TSPL_BULL_SHED_PARAMETER_DETAIL on TSPL_BULL_SHED_PARAMETER_DETAIL.PK_Id = TSPL_BULL_CMU_GROUPING_Detail.Against_Parameter_Group_Code
                                     where TSPL_BULL_CMU_GROUPING_Detail.Document_No= '" & obj.Code & "'  "

                dt = clsDBFuncationality.GetDataTable(qry)
                Dim arrGroupCode As New ArrayList

                For Each row As DataRow In dt.Rows
                    arrGroupCode.Add(row("GroupCode"))
                Next
                txtMultiGroup.arrValueMember = arrGroupCode

                If obj.Arr IsNot Nothing Then
                    For Each objrow As ClsCMUGroupingDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColGroupCode).Value = objrow.GroupCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColGroupName).Value = objrow.GroupName
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColParameterCode).Value = objrow.ParameterCode
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColParameterName).Value = objrow.Parameter_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColParameterType).Value = objrow.Parameter_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColPkId).Value = objrow.Pk_Id
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
                If clsCommon.myCdbl(ERPTransactionStatus.Approved) = clsCommon.myCdbl(obj.Status) Then
                    UsLock1.Status = obj.Status
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                ElseIf ERPTransactionStatus.Pending = obj.Status Then
                    UsLock1.Status = obj.Status
                    btnsave.Enabled = True
                    btnsave.Text = "Update"
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                End If
                isInsideLoadData = False

            Else
                AddNew()
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub txtMultiGroup__My_Click(sender As Object, e As EventArgs) Handles txtMultiGroup._My_Click
        Dim qry As String = "select Code,Name from TSPL_BULL_SHED_PARAMETER_MASTER"
        txtMultiGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransDetailedCardReport", qry, "Code", "Code", txtMultiGroup.arrValueMember, txtMultiGroup.arrDispalyMember)

        If txtMultiGroup.arrValueMember IsNot Nothing AndAlso clsCommon.myLen(txtMultiGroup.arrValueMember) > 0 Then
            GridData()
        End If

    End Sub

    Sub GridData()
        Try
            Dim Qry As String = ""
            Dim dt As New DataTable
            Dim whr As String = ""
            If txtMultiGroup.arrValueMember IsNot Nothing AndAlso txtMultiGroup.arrValueMember.Count > 0 Then
                whr += "and TSPL_BULL_SHED_PARAMETER_DETAIL.Code  IN (" + clsCommon.GetMulcallString(txtMultiGroup.arrValueMember) + ") "
            End If
            Qry = " SELECT TSPL_BULL_SHED_PARAMETER_DETAIL.PK_Id, TSPL_BULL_SHED_PARAMETER_DETAIL.Code as GroupCode,TSPL_BULL_SHED_PARAMETER_MASTER.Name as GroupName,
                    TSPL_BULL_SHED_PARAMETER_DETAIL.PCode as ParameterCode,TSPL_BULL_SHED_PARAMETER.Name as ParameterName,TSPL_BULL_SHED_PARAMETER.Type as ParameterType FROM TSPL_BULL_SHED_PARAMETER_DETAIL
                    LEFT OUTER JOIN TSPL_BULL_SHED_PARAMETER ON TSPL_BULL_SHED_PARAMETER.Code=TSPL_BULL_SHED_PARAMETER_DETAIL.PCode
                    left outer join TSPL_BULL_SHED_PARAMETER_MASTER on TSPL_BULL_SHED_PARAMETER_MASTER.Code = TSPL_BULL_SHED_PARAMETER_DETAIL.Code
                    where 2=2  " + whr + ""

            If clsCommon.myLen(Qry) > 0 Then
                dt = clsDBFuncationality.GetDataTable(Qry)
            End If

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each row In dt.Rows
                    gv1.CurrentRow.Cells(ColGroupCode).Value = clsCommon.myCstr(row("GroupCode"))
                    gv1.CurrentRow.Cells(ColGroupName).Value = clsCommon.myCstr(row("GroupName"))
                    gv1.CurrentRow.Cells(ColParameterCode).Value = clsCommon.myCstr(row("ParameterCode"))
                    gv1.CurrentRow.Cells(ColParameterName).Value = clsCommon.myCstr(row("ParameterName"))
                    gv1.CurrentRow.Cells(ColParameterType).Value = clsCommon.myCstr(row("ParameterType"))
                    gv1.CurrentRow.Cells(ColPkId).Value = clsCommon.myCstr(row("PK_Id"))

                    gv1.Rows.AddNew()
                Next
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    'Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
    '    Try
    '        Dim dt As New DataTable
    '        Dim qry As String = ""
    '        Dim whr As String = ""
    '        If txtMultiGroup.arrValueMember IsNot Nothing AndAlso txtMultiGroup.arrValueMember.Count > 0 Then
    '            whr += "and TSPL_BULL_SHED_PARAMETER_DETAIL.Code  IN (" + clsCommon.GetMulcallString(txtMultiGroup.arrValueMember) + ") "
    '        End If

    '        qry = " SELECT TSPL_BULL_SHED_PARAMETER_DETAIL.Code,TSPL_BULL_SHED_PARAMETER_DETAIL.PCode,TSPL_BULL_SHED_PARAMETER.Name,TSPL_BULL_SHED_PARAMETER.Type FROM TSPL_BULL_SHED_PARAMETER_DETAIL
    '                LEFT OUTER JOIN TSPL_BULL_SHED_PARAMETER ON TSPL_BULL_SHED_PARAMETER.Code=TSPL_BULL_SHED_PARAMETER_DETAIL.PCode
    '                where 2=2  " + whr + ""

    '        If clsCommon.myLen(qry) > 0 Then
    '            dt = clsDBFuncationality.GetDataTable(qry)
    '        End If


    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            gv1.DataSource = Nothing
    '            gv1.GroupDescriptors.Clear()
    '            gv1.SummaryRowsBottom.Clear()
    '            gv1.DataSource = dt
    '            gv1.BestFitColumns()
    '        Else
    '            clsCommon.MyMessageBoxShow(Me, "No data found to display.", Me.Text)
    '        End If
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Function SaveData() As Boolean

        Dim obj As New ClsCMUGrouping()
        obj.Code = fndCode.Value
        obj.Remarks = txtremarks.Text
        obj.Name = TxtName.Text
        Dim arrGroupType As New List(Of String)
        If txtMultiGroup.arrValueMember IsNot Nothing Then
            For i As Integer = 0 To txtMultiGroup.arrValueMember.Count - 1
                arrGroupType.Add(txtMultiGroup.arrValueMember(i))
            Next
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select atleast one Group type", Me.Text)
            Exit Function
        End If

        obj.Arr = New List(Of ClsCMUGroupingDetail)

        For Each row As GridViewRowInfo In gv1.Rows
            Dim objTr As New ClsCMUGroupingDetail()

            objTr.GroupCode = clsCommon.myCstr(row.Cells(ColGroupCode).Value)
            objTr.GroupName = clsCommon.myCstr(row.Cells(ColGroupName).Value)
            objTr.ParameterCode = clsCommon.myCdbl(row.Cells(ColParameterCode).Value)
            objTr.Pk_Id = clsCommon.myCdbl(row.Cells(ColPkId).Value)
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
            'objTr.Range_Selection = clsCommon.myCDecimal(row.Cells(colRangeSelection).Value)
            objTr.RangeArr = TryCast(row.Cells(colRangeBtn).Tag, Dictionary(Of String, String))

            If (clsCommon.myLen(objTr.GroupCode) > 0) Then
                obj.Arr.Add(objTr)
            End If
        Next

        Dim Sqlqry As String = "select count(1) from TSPL_BULL_CMU_GROUPING where Document_No ='" + fndCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            isNewEntry = True
        Else
            isNewEntry = False
        End If

        If (ClsCMUGrouping.SaveData(obj, isNewEntry)) Then
            clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
            LoadData(obj.Code, NavigatorType.Current)
        End If

        Return True
    End Function

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (ClsCMUGrouping.DeleteData(fndCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        If clsCommon.myLen(fndCode.Value) > 0 Then
            PostData(fndCode.Value)
        Else
        End If
    End Sub

    Sub PostData(ByVal strCode As String)
        Try
            If clsCommon.myLen(fndCode.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + fndCode.Value + "]" + Environment.NewLine + "Are You Sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                ClsCMUGrouping.PostData(clsCommon.myCstr(fndCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(fndCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Try
            Dim Sqlqry As String = "select count(*) from TSPL_BULL_CMU_GROUPING where Document_No ='" + fndCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
            If count = 0 Then
                fndCode.MyReadOnly = False
            Else
                fndCode.MyReadOnly = True
            End If
            If fndCode.MyReadOnly OrElse isButtonClicked Then
                Dim whrClas As String = ""
                Dim qry As String = ""
                qry = "select Document_No as Code,Remarks as [Remarks],Name from TSPL_BULL_CMU_GROUPING"
                fndCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Code", "", fndCode.Value, " Code asc", isButtonClicked, Nothing)
                LoadData(fndCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BULL_CMU_GROUPING where Document_No='" + fndCode.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                fndCode.MyReadOnly = False
            Else
                fndCode.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(fndCode.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColParameterType).Value), "Range") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColReqforResult).Value), "True") = CompairStringResult.Equal Then
                For Each row In gv1.Rows
                    gv1.CurrentRow.Cells(colBoolean).ReadOnly = True
                    gv1.CurrentRow.Cells(colAlphaNumeric).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeSelection).ReadOnly = True
                Next
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColParameterType).Value), "Boolean") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColReqforResult).Value), "True") = CompairStringResult.Equal Then
                For Each row In gv1.Rows
                    gv1.CurrentRow.Cells(colRangeFrom).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeTo).ReadOnly = True
                    gv1.CurrentRow.Cells(colAlphaNumeric).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeSelection).ReadOnly = True
                Next
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColParameterType).Value), "Alpha Numeric") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColReqforResult).Value), "True") = CompairStringResult.Equal Then
                For Each row In gv1.Rows
                    gv1.CurrentRow.Cells(colRangeFrom).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeTo).ReadOnly = True
                    gv1.CurrentRow.Cells(colBoolean).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeSelection).ReadOnly = True
                Next
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColParameterType).Value), "Range Selection") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(ColReqforResult).Value), "True") = CompairStringResult.Equal Then
                For Each row In gv1.Rows
                    gv1.CurrentRow.Cells(colRangeFrom).ReadOnly = True
                    gv1.CurrentRow.Cells(colRangeTo).ReadOnly = True
                    gv1.CurrentRow.Cells(colBoolean).ReadOnly = True
                    gv1.CurrentRow.Cells(colAlphaNumeric).ReadOnly = True
                Next
            End If

            'If clsCommon.myCDecimal(gv1.CurrentRow.Cells(colRangeSelection).Value) > 0 Then
            '    Dim frmP As New frmBullParameterRangeSelection()
            '    frmP.ArrRangeSelection = gv1.CurrentRow.Cells(colRangeSelection).Tag
            '    frmP.Range = gv1.CurrentRow.Cells(colRangeSelection).Value
            '    frmP.AddGridView(clsCommon.myCDecimal(gv1.CurrentRow.Cells(colRangeSelection).Value))
            '    frmP.Form_ID = clsUserMgtCode.frmBullCMUGrouping
            '    frmP.Show()
            '    If frmP.isOK Then
            '        gv1.CurrentRow.Cells(colRangeSelection).Tag = frmP.ArrRangeSelection
            '    End If
            '    'formShow(frm, strProgramCode, strProgramName, isOpenInMDI, strDocNo, IFTrueShowFormElseShowDialog)
            'End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub gv1_CellClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellClick
    '    If clsCommon.myCDecimal(gv1.CurrentRow.Cells(colRangeSelection).Value) > 0 Then
    '        Dim frmP As New frmBullParameterRangeSelection()
    '        frmP.ArrRangeSelection = gv1.CurrentRow.Cells(colRangeSelection).Tag
    '        frmP.Range = gv1.CurrentRow.Cells(colRangeSelection).Value
    '        frmP.AddGridView(clsCommon.myCDecimal(gv1.CurrentRow.Cells(colRangeSelection).Value))
    '        frmP.Form_ID = clsUserMgtCode.frmBullCMUGrouping
    '        frmP.Show()
    '        If frmP.isOK Then
    '            gv1.CurrentRow.Cells(colRangeSelection).Tag = frmP.ArrRangeSelection
    '        End If
    '        'formShow(frm, strProgramCode, strProgramName, isOpenInMDI, strDocNo, IFTrueShowFormElseShowDialog)
    '    End If
    'End Sub

    Private Sub gv1_CommandCellClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CommandCellClick
        Try
            If gv1.CurrentColumn Is gv1.Columns(colRangeBtn) Then
                Dim frmP As New frmBullParameterRangeSelection()
                'frmP.AddGridView(clsCommon.myCDecimal(gv1.CurrentRow.Cells(colRangeSelection).Value))
                frmP.Form_ID = clsUserMgtCode.frmBullCMUGrouping
                frmP.ArrRangeSelection = gv1.CurrentRow.Cells(colRangeBtn).Tag
                'frmP.Range = gv1.CurrentRow.Cells(colRangeSelection).Value
                frmP.ShowDialog()
                If frmP.isOK Then
                    gv1.CurrentRow.Cells(colRangeBtn).Tag = frmP.ArrRangeSelection
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class