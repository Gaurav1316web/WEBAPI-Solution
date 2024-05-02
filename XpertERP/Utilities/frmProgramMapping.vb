'--------------shivani tyagi
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
Public Class FrmProgramMapping
    Dim isInsideLoadData As Boolean = False
    Const colProgramCode As String = "colProgramCode"
    Const colProgramName As String = "colProgramName"
    Const colTable1 As String = "colTable1"
    Const colTable2 As String = "colTable2"
    Const colTable3 As String = "colTable3"
    Const colTable4 As String = "colTable4"
    Const colTable5 As String = "colTable5"
    Private isNewEntry As Boolean = False

    Public Sub LoadBlankGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim programcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        programcode.FormatString = ""
        programcode.HeaderText = "Program Code"
        programcode.Name = colProgramCode
        programcode.Width = 150
        programcode.IsVisible = True
        programcode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(programcode)

        Dim programname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        programname.FormatString = ""
        programname.HeaderText = "Program Name"
        programname.Name = colProgramName
        programname.Width = 150
        programname.IsVisible = True
        programname.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(programname)

        Dim Table1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Table1.FormatString = ""
        Table1.HeaderText = "Table 1"
        Table1.Name = colTable1
        Table1.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Table1.TextImageRelation = TextImageRelation.TextBeforeImage
        Table1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Table1.Width = 150
        Table1.IsVisible = True
        Table1.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Table1)

        Dim Table2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Table2.FormatString = ""
        Table2.HeaderText = "Table 2"
        Table2.Name = colTable2
        Table2.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Table2.TextImageRelation = TextImageRelation.TextBeforeImage
        Table2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Table2.Width = 150
        Table2.IsVisible = True
        Table2.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Table2)

        Dim Table3 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Table3.FormatString = ""
        Table3.HeaderText = "Table 3"
        Table3.Name = colTable3
        Table3.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Table3.TextImageRelation = TextImageRelation.TextBeforeImage
        Table3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Table3.Width = 150
        Table3.IsVisible = True
        Table3.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Table3)

        Dim Table4 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Table4.FormatString = ""
        Table4.HeaderText = "Table 4"
        Table4.Name = colTable4
        Table4.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Table4.TextImageRelation = TextImageRelation.TextBeforeImage
        Table4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Table4.Width = 150
        Table4.IsVisible = True
        Table4.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Table4)

        Dim Table5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Table5.FormatString = ""
        Table5.HeaderText = "Table 5"
        Table5.Name = colTable5
        Table5.HeaderImage = Global.ERP.My.Resources.Resources.search4
        Table5.TextImageRelation = TextImageRelation.TextBeforeImage
        Table5.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Table5.Width = 150
        Table5.IsVisible = True
        Table5.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Table5)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True

    End Sub
    Sub LoadModuleName()
        isInsideLoadData = True
        Dim qry As String = "select xxx.Parent_Code as Code,ParentModuleTable.Program_Name as Name from (Select Parent_Code from TSPL_PROGRAM_MASTER where Parent_Code in"
        qry += " (select Program_Code from TSPL_PROGRAM_MASTER inner join TSPL_MODULE_PERMISSION on TSPL_MODULE_PERMISSION.Module_Name=TSPL_PROGRAM_MASTER.Parent_Code where"
        qry += " TSPL_PROGRAM_MASTER.Type='SM' and TSPL_PROGRAM_MASTER.Program_Name like '%Tran%' and TSPL_MODULE_PERMISSION.IsAvailable=1)group by Parent_Code) xxx "
        qry += "Left outer join TSPL_PROGRAM_MASTER as ParentTable on ParentTable.Program_Code=xxx.Parent_Code Left outer join TSPL_PROGRAM_MASTER as ParentModuleTable on ParentModuleTable.Program_Code=ParentTable.Parent_Code"
        cboModuleName.DataSource = clsDBFuncationality.GetDataTable(qry)
        cboModuleName.ValueMember = "Code"
        cboModuleName.DisplayMember = "Name"
        isInsideLoadData = False
    End Sub

    Sub LoadProgramCode()
        LoadBlankGrid()
        Dim moduleName As String = cboModuleName.SelectedValue
        Dim qry As String = "select TSPL_PROGRAM_MASTER.Program_Code,Program_Name,Table_1 ,Table_2 ,Table_3,Table_4 ,Table_5 from TSPL_PROGRAM_MASTER left outer join TSPL_PROGRAM_TABLE_MAPPING on TSPL_PROGRAM_TABLE_MAPPING.Program_Code =TSPL_PROGRAM_MASTER.Program_Code  where Parent_Code in (select Program_Code from TSPL_PROGRAM_MASTER "
        qry += " inner join TSPL_MODULE_PERMISSION on TSPL_MODULE_PERMISSION.Module_Name=TSPL_PROGRAM_MASTER.Parent_Code "
        qry += " where TSPL_PROGRAM_MASTER.Type='SM' and TSPL_PROGRAM_MASTER.Program_Name like '%Tran%' and TSPL_MODULE_PERMISSION.IsAvailable=1)"
        qry += " and Parent_Code = ('" + moduleName + "')"
        clsDBFuncationality.ExecuteNonQuery(qry)
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
        'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
        '    gv1.DataSource = Nothing
        '    gv1.Columns.Clear()
        '    gv1.Rows.Clear()
        '    gv1.DataSource = dt1
        'Else
        '    clsCommon.MyMessageBoxShow("No Data found to Display")
        'End If
        For Each dr As DataRow In dt1.Rows

            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colProgramCode).Value = clsCommon.myCstr(dr("Program_Code"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colProgramName).Value = clsCommon.myCstr(dr("Program_Name"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTable1).Value = clsCommon.myCstr(dr("Table_1"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTable2).Value = clsCommon.myCstr(dr("Table_2"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTable3).Value = clsCommon.myCstr(dr("Table_3"))

            gv1.Rows(gv1.Rows.Count - 1).Cells(colTable4).Value = clsCommon.myCstr(dr("Table_4"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colTable5).Value = clsCommon.myCstr(dr("Table_5"))

        Next


    End Sub
    Sub AddNew()
        LoadBlankGrid()
        Me.gv1.Rows.AddNew()
        cboModuleName.Text = ""
        'cboModuleName.ReadOnly = True
    End Sub
    Private Sub FrmProgramMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadModuleName()
        'LoadBlankGrid()

        'Me.gv1.Rows.AddNew()
        cboModuleName.Text = ""
        'cboModuleName.ReadOnly = True
    End Sub
    Sub SaveData()
        Try
            Dim Arr As New List(Of ClsProgramMapping)
            Dim obj As New ClsProgramMapping
            For Each grow As GridViewRowInfo In gv1.Rows
                obj = New ClsProgramMapping
                obj.Program_Code = clsCommon.myCstr(grow.Cells(colProgramCode).Value)
                obj.Table_1 = clsCommon.myCstr(grow.Cells(colTable1).Value)
                obj.Table_2 = clsCommon.myCstr(grow.Cells(colTable2).Value)
                obj.Table_3 = clsCommon.myCstr(grow.Cells(colTable3).Value)
                obj.Table_4 = clsCommon.myCstr(grow.Cells(colTable4).Value)
                obj.Table_5 = clsCommon.myCstr(grow.Cells(colTable5).Value)
                Arr.Add(obj)
            Next

            'Dim qry As String = clsDBFuncationality.getSingleValue("select count(Program_Code)from TSPL_PROGRAM_TABLE_MAPPING where Program_Code='" + obj.Program_Code + "'")
            'If qry = 0 Then
            '    isNewEntry = True
            'Else
            '    isNewEntry = False
            'End If

            If (ClsProgramMapping.SaveData(Arr)) Then
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
        For Each grow As GridViewRowInfo In gv1.Rows
            Dim qry As String = "delete from TSPL_PROGRAM_TABLE_MAPPING where program_code='" + clsCommon.myCstr(grow.Cells(colProgramCode).Value) + "'"
            clsDBFuncationality.ExecuteNonQuery(qry)
        Next
        clsCommon.MyMessageBoxShow("delete successfully")

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub OpenTableList(ByVal isButtonClick As Boolean)
        'gv1.CurrentRow.Cells(colTable1).Value = ""
        Dim qry As String = "select TABLE_NAME as Name from INFORMATION_SCHEMA .TABLES"
        gv1.CurrentRow.Cells(colTable1).Value = clsCommon.ShowSelectForm("tab", qry, "Name", "TABLE_NAME like 'Tspl%'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTable1).Value), "Name", isButtonClick)
        gv1.CurrentRow.Cells(colTable2).Value = clsCommon.ShowSelectForm("tab", qry, "Name", "TABLE_NAME like 'Tspl%'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTable2).Value), "Name", isButtonClick)
        gv1.CurrentRow.Cells(colTable3).Value = clsCommon.ShowSelectForm("tab", qry, "Name", "TABLE_NAME like 'Tspl%'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTable3).Value), "Name", isButtonClick)
        gv1.CurrentRow.Cells(colTable4).Value = clsCommon.ShowSelectForm("tab", qry, "Name", "TABLE_NAME like 'Tspl%'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTable4).Value), "Name", isButtonClick)
        gv1.CurrentRow.Cells(colTable5).Value = clsCommon.ShowSelectForm("tab", qry, "Name", "TABLE_NAME like 'Tspl%'", clsCommon.myCstr(gv1.CurrentRow.Cells(colTable5).Value), "Name", isButtonClick)
    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv1.Columns(colTable1) Then
                    OpenTableList(False)
                End If
                If e.Column Is gv1.Columns(colTable2) Then
                    OpenTableList(False)
                End If
                If e.Column Is gv1.Columns(colTable3) Then
                    OpenTableList(False)
                End If
                If e.Column Is gv1.Columns(colTable4) Then
                    OpenTableList(False)
                End If
                If e.Column Is gv1.Columns(colTable5) Then
                    OpenTableList(False)
                End If

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        AddNew()
    End Sub

   

  
    Private Sub cboModuleName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboModuleName.SelectedIndexChanged
        If isInsideLoadData = False Then
            LoadProgramCode()
        End If
    End Sub
End Class
