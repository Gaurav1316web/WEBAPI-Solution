Imports System.Data
Imports System.Data.SqlClient
Imports common
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine


Public Class frmBullCurlingEntry
    Inherits FrmMainTranScreen

    Dim isNewEntry As Boolean = True

    Const ColBullCode As String = "ColCode"
    Const ColSpeciesCode As String = "ColSpeciesCode"
    Const ColCategoryCode As String = "ColCategoryCode"
    Const ColSubCategoryCode As String = "ColSubCategoryCode"
    Const ColSSCenterCode As String = "ColSSCenterCode"
    Const ColBreed As String = "ColBreed"
    Const ColShedCode As String = "ColShedCode"
    Const ColPenCode As String = "ColPenCode"
    Const ColStatusCode As String = "ColStatusCode"
    Const ColSubStatusCode As String = "ColSubStatusCode"
    Const ColBullImported As String = "ColBullImported"
    Const ColExoticBlood As String = "ColExoticBlood"
    Const ColBullBookValue As String = "ColBullBookValue"
    Const ColRegistrationDate As String = "ColRegistrationDate"
    Const ColBullID As String = "ColBullID"
    Const ColPrevBullID As String = "ColPrevBullID"
    Const ColDateOfBirth As String = "ColDateOfBirth"
    Const ColSSBullID As String = "ColSSBullID"
    Const ColBullaliasName As String = "ColBullName"
    Const ColSSCenter As String = "ColSSCenter"
    Const ColAmount As String = "ColAmount"

    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = True
    Dim ErrorControl As New clsErrorControl()

    Private Sub frmBullCurlingEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadBlankGrid()
    End Sub

    Sub loadBlankGrid()
        Try
            Dim qry As String = String.Empty

            gv1.Rows.Clear()
            gv1.Columns.Clear()

            Dim gridcolCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            Dim gridcolName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            Dim gridcoltype As GridViewTextBoxColumn = New GridViewTextBoxColumn()

            gridcolCode.FormatString = ""
            gridcolCode.HeaderText = "Species Code"
            gridcolCode.Name = ColSpeciesCode
            gridcolCode.Width = 110
            gridcolName.IsVisible = False
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolCode)

            gridcolCode.FormatString = ""
            gridcolCode.HeaderText = "Category Code"
            gridcolCode.Name = ColCategoryCode
            gridcolCode.Width = 110
            gridcolName.IsVisible = False
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolCode)

            gridcolCode.FormatString = ""
            gridcolCode.HeaderText = "Sub Category Code"
            gridcolCode.Name = ColSubCategoryCode
            gridcolCode.Width = 110
            gridcolName.IsVisible = False
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolCode)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "SS Center"
            gridcolName.Name = ColSSCenterCode
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Breed"
            gridcolName.Name = ColBreed
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Shed Code"
            gridcolName.Name = ColShedCode
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Pen Code"
            gridcolName.Name = ColPenCode
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Status Code"
            gridcolName.Name = ColStatusCode
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Sub Status Code"
            gridcolName.Name = ColSubStatusCode
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Bull Imported"
            gridcolName.Name = ColBullImported
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Exotic Blood"
            gridcolName.Name = ColExoticBlood
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Bull Book Value"
            gridcolName.Name = ColBullBookValue
            gridcolName.Width = 110
            gridcolName.IsVisible = False
            gridcolName.VisibleInColumnChooser = True
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Registration Date"
            gridcolName.Name = ColRegistrationDate
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Bull ID"
            gridcolName.Name = ColBullID
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Prev Bull ID"
            gridcolName.Name = ColPrevBullID
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Date Of Birth"
            gridcolName.Name = ColDateOfBirth
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "SS Bull ID"
            gridcolName.Name = ColSSBullID
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Bull Name"
            gridcolName.Name = ColBullaliasName
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "SS Center"
            gridcolName.Name = ColSSCenter
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

            gridcolName.FormatString = ""
            gridcolName.HeaderText = "Amount"
            gridcolName.Name = ColAmount
            gridcolName.Width = 110
            gridcolName.IsVisible = True
            gridcolName.VisibleInColumnChooser = False
            gv1.MasterTemplate.Columns.Add(gridcolName)

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
        txtremarks.Text = ""
        dtpDate.Value = clsCommon.GETSERVERDATE()
        gv1.DataSource = Nothing
    End Sub

    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BULL_CURLING where Code='" + fndCode.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                fndCode.MyReadOnly = False
            Else
                fndCode.MyReadOnly = True
            End If
            'LoadData(clsCommon.myCstr(fndCode.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Try
            'Dim Sqlqry As String = "select count(*) from TSPL_BULL_SHED_PARAMETER_MASTER where Code ='" + fndCode.Value + "'"
            'Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
            'If count = 0 Then
            '    fndCode.MyReadOnly = False
            'Else
            '    fndCode.MyReadOnly = True
            'End If
            'If fndCode.MyReadOnly OrElse isButtonClicked Then
            '    Dim whrClas As String = ""
            Dim qry As String
            qry = " select count(*) from TSPL_BULL_CURLING where Code ='" + fndCode.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))

            If count > 0 Then
                qry = "select Code as Code,Remarks as [Remarks],Doc_Date as Date from TSPL_BULL_CURLING"
                fndCode.Value = clsCommon.ShowSelectForm("RTY", qry, "Code", "", fndCode.Value, " Code asc", isButtonClicked, Nothing)
                'LoadData(fndCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Function SaveData() As Boolean

        Dim obj As New ClsBullCurlingEntry()
        obj.Code = fndCode.Value
        obj.Remarks = txtremarks.Text
        obj.Doc_Date = dtpDate.Value

        obj.Arr = New List(Of ClsBullCurlingEntryDeatil)

        For Each row As GridViewRowInfo In gv1.Rows
            Dim objTr As New ClsBullCurlingEntryDeatil()

            objTr.BullID = clsCommon.myCstr(row.Cells(ColBullID).Value)
            objTr.Amount = clsCommon.myCstr(row.Cells(ColBullID).Value)

            If (clsCommon.myLen(objTr.BullID) > 0) Then
                obj.Arr.Add(objTr)
            End If
        Next

        Dim Sqlqry As String = "select count(1) from TSPL_BULL_CURLING where Code ='" + fndCode.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(Sqlqry))
        If count = 0 Then
            isNewEntry = True
        Else
            isNewEntry = False
        End If

        If (ClsBullCurlingEntry.SaveData(obj, isNewEntry)) Then
            clsCommon.MyMessageBoxShow(Me, "Data save successfully.")
            'LoadData(obj.Code, NavigatorType.Current)

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
                If (ClsBullCurlingEntry.DeleteData(fndCode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    'AddNew()
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
                ClsBullCurlingEntry.PostData(clsCommon.myCstr(fndCode.Value))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                'LoadData(clsCommon.myCstr(txtCode.Value), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
    '    Try
    '        gv1.DataSource = Nothing
    '        gv1.Refresh()
    '        isInsideLoadData = True
    '        fndCode.MyReadOnly = True


    '        'Dim obj As ClsBullCurlingEntry = ClsBullCurlingEntry.GetData(strCode, NavTyep)
    '        'If obj IsNot Nothing Then
    '        '    isNewEntry = False
    '        '    fndCode.Value = obj.Code
    '        '    txtremarks.Text = obj.Remarks
    '        '    dtpDate.Value = obj.Doc_Date

    '        fndCode.MyReadOnly = True
    '            btnsave.Text = "Update"
    '            btndelete.Enabled = True
    '            'If obj.Arr IsNot Nothing Then
    '            '    For Each objrow As ClsBullCurlingEntryDeatil In obj.Arr
    '            '        'gv1.Rows(gv1.Rows.Count - 1).Cells(cold).Value = objrow.Document_No
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(ColBullID).Value = objrow.BullID
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(ColAmount).Value = objrow.Amount
    '            '        'gv1.Rows(gv1.Rows.Count - 1).Cells(ColCode).Value = objrow.Code

    '            '        'sno += 1
    '            '        gv1.Rows.AddNew()
    '            '    Next
    '            'End If
    '            ' OpenICodeList(False)

    '            'isInsideLoadData = False

    '        Else
    '            'AddNew()
    '        End If
    '    Catch ex As Exception
    '        'isInsideLoadData = False

    '        clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

End Class