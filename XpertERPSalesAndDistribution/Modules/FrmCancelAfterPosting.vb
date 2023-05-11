Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine
Public Class FrmCancelAfterPosting
    Dim isInsideLoadData As Boolean = False
    Const COl_Screen_Id As String = "COl_Screen_Id"
    Const COl_Screen_Name As String = "COl_Screen_Name"
    Const ColIsSelect As String = "ColIsSelect"
    Const COl_Starting_Date As String = "COl_Starting_Date"
    Const COl_Inactive_Date As String = "COl_Inactive_Date"
    Private isNewEntry As Boolean = False
    Dim is_Send_SMS As Boolean
    Dim Send_SMS_Time As String
    Dim qry As String


    Public Sub LoadBlankGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim SelectCoL As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        SelectCoL.FormatString = ""
        SelectCoL.HeaderText = "Select"
        SelectCoL.Name = ColIsSelect
        SelectCoL.Width = 80
        SelectCoL.IsVisible = True
        SelectCoL.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(SelectCoL)


        Dim Mail_Id_COL As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Mail_Id_COL.FormatString = ""
        Mail_Id_COL.HeaderText = "Screen Code"
        Mail_Id_COL.Name = COl_Screen_Id
        Mail_Id_COL.Width = 300
        Mail_Id_COL.IsVisible = True
        Mail_Id_COL.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Mail_Id_COL)

        Dim Mobile_No_COL As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Mobile_No_COL.FormatString = ""
        Mobile_No_COL.HeaderText = "Screen Name"
        Mobile_No_COL.Name = COl_Screen_Name
        Mobile_No_COL.Width = 300
        Mobile_No_COL.IsVisible = True
        Mobile_No_COL.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Mobile_No_COL)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MMM-yyyy"
        repoDate.HeaderText = "Starting Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = COl_Starting_Date
        repoDate.WrapText = True
        repoDate.ReadOnly = False
        repoDate.Width = 150
        gv1.MasterTemplate.Columns.Add(repoDate)

        'Dim repoInactiveDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        'repoInactiveDate.Format = DateTimePickerFormat.Custom
        'repoInactiveDate.CustomFormat = "dd-MMM-yyyy"
        'repoInactiveDate.HeaderText = "Inactive Date"
        'repoInactiveDate.FormatString = "{0:d}"
        'repoInactiveDate.Name = COl_Inactive_Date
        'repoInactiveDate.WrapText = True
        'repoInactiveDate.ReadOnly = False
        'repoInactiveDate.Width = 150
        'gv1.MasterTemplate.Columns.Add(repoInactiveDate)



        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        ' gv1.VerticalScrollState = ScrollState.AlwaysShow
        'gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True

    End Sub

    Sub LoadProgramCode()
        LoadBlankGrid()
        ' Dim moduleName As String = cboModuleName.SelectedValue
        Dim qry As String = "select distinct TSPL_PROGRAM_MASTER.Program_Code,Program_Name,Starting_Date from TSPL_PROGRAM_MASTER inner join TSPL_CANCEL_TABLE_DETAILS " _
                            & " on TSPL_CANCEL_TABLE_DETAILS.form_Id=Program_Code left join TSPL_Cancel_After_Posting_Tables_Details on " _
                            & " TSPL_Cancel_After_Posting_Tables_Details.form_Id=TSPL_CANCEL_TABLE_DETAILS.form_id "
        clsDBFuncationality.ExecuteNonQuery(qry)
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        isInsideLoadData = True
        For Each row As DataRow In dt1.Rows
            gv1.Rows.AddNew()
            gv1.CurrentRow.Cells(COl_Screen_Id).Value = clsCommon.myCstr(row("Program_Code"))
            gv1.CurrentRow.Cells(COl_Screen_Name).Value = clsCommon.myCstr(row("Program_Name"))
            If clsCommon.myLen(clsCommon.myCstr(row("Starting_Date"))) <= 0 Then
                gv1.CurrentRow.Cells(COl_Starting_Date).Value = Nothing
                gv1.CurrentRow.Cells(ColIsSelect).ReadOnly = True
            Else
                gv1.CurrentRow.Cells(COl_Starting_Date).Value = clsCommon.myCDate(row("Starting_Date"))
                gv1.CurrentRow.Cells(ColIsSelect).Value = True
            End If

            'If clsCommon.myLen(clsCommon.myCstr(row("Starting_Date"))) > 0 Then 'clsCommon.myLen(clsCommon.myCstr(row("Inactive_Date"))) <= 0 And 
            '    gv1.CurrentRow.Cells(ColIsSelect).Value = True
            'Else
            '    gv1.CurrentRow.Cells(ColIsSelect).Value = False
            'End If
            'If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(COl_Starting_Date).Value)) <= 0 Then
            '    gv1.CurrentRow.Cells(ColIsSelect).ReadOnly = True
            'ElseIf clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(COl_Starting_Date).Value)) > 0 Then
            '    gv1.CurrentRow.Cells(ColIsSelect).Value = True
            'End If
        Next
        ' gv1.DataSource = dt1
        isInsideLoadData = False
        'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
        '    gv1.DataSource = Nothing
        '    gv1.Columns.Clear()
        '    gv1.Rows.Clear()
        '    gv1.DataSource = dt1
        'Else
        '    clsCommon.MyMessageBoxShow("No Data found to Display")
        'End If
    End Sub

    Sub AddNew()
        LoadBlankGrid()
        Me.gv1.Rows.AddNew()
    End Sub

    Private Sub FrmCancelAfterPosting_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadProgramCode()

    End Sub

    Sub SaveData()
        Try
            Dim Arr As New List(Of ClsCancelAfterPosting)
            Dim obj As New ClsCancelAfterPosting
            For Each grow As GridViewRowInfo In gv1.Rows
                obj = New ClsCancelAfterPosting
                obj.Program_Code = clsCommon.myCstr(grow.Cells(COl_Screen_Id).Value)
                obj.Starting_Date = clsCommon.myCstr(grow.Cells(COl_Starting_Date).Value)
                If clsCommon.myCBool(grow.Cells(ColIsSelect).Value) = False Then
                    obj.Inactive_Date = clsCommon.GETSERVERDATE()
                Else
                    obj.Inactive_Date = Nothing
                End If
                Arr.Add(obj)
            Next

            'Dim qry As String = clsDBFuncationality.getSingleValue("select count(Program_Code)from TSPL_MCC_MAIL_SMS_Setting where Program_Code='" + obj.Program_Code + "'")
            'If qry = 0 Then
            '    isNewEntry = True
            'Else
            '    isNewEntry = False
            'End If

            If (ClsCancelAfterPosting.SaveData(Arr)) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully")

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Sub DeleteData()
        'For Each grow As GridViewRowInfo In gv1.Rows
        '    Dim qry As String = "delete from TSPL_MCC_MAIL_SMS_Setting where program_code='" + clsCommon.myCstr(cboModuleName.SelectedValue) + "'"
        '    clsDBFuncationality.ExecuteNonQuery(qry)
        'Next
        'clsCommon.MyMessageBoxShow("delete successfully")

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        AddNew()
    End Sub

    Private Sub cboModuleName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)
        If isInsideLoadData = False Then
            Loaddata()
        End If
    End Sub

    Sub Loaddata()
        Try
            LoadBlankGrid()
            Dim obj As List(Of ClsCancelAfterPosting) = ClsCancelAfterPosting.GetData("")
            If obj.Count > 0 Then
                ' LoadBlankGrid()
                For Each objl As ClsCancelAfterPosting In obj
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COl_Screen_Id).Value = objl.Program_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COl_Inactive_Date).Value = objl.Inactive_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(COl_Starting_Date).Value = objl.Starting_Date
                    If clsCommon.myLen(clsCommon.myCstr(objl.Inactive_Date)) <= 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColIsSelect).Value = False
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColIsSelect).Value = True
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        If e.Column Is gv1.Columns(ColIsSelect) Then
            If clsCommon.myCBool(gv1.CurrentRow.Cells(ColIsSelect).Value) = True Then
                gv1.CurrentRow.Cells(COl_Starting_Date).ReadOnly = True
            End If
        ElseIf e.Column Is gv1.Columns(COl_Starting_Date) Then
            If clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(COl_Starting_Date).Value)) <= 0 Then
                gv1.CurrentRow.Cells(ColIsSelect).ReadOnly = True
            ElseIf clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(COl_Starting_Date).Value)) > 0 Then
                gv1.CurrentRow.Cells(ColIsSelect).Value = True
            End If
        End If
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        LoadProgramCode()
    End Sub
End Class
