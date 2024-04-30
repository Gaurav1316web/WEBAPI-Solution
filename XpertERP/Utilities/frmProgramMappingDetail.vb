'-------------shivani tyagi
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class FrmProgramMappingDetail
    Inherits FrmMainTranScreen
    Const colColumnName As String = "colColumnName"
    Const colCaption As String = "colCaption"
    Dim isInsideLoadData As Boolean = False
    Public strFormID As String = ""

    Public Sub LoadBlankGrid1()

        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim ColumnName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ColumnName.FormatString = ""
        ColumnName.HeaderText = "Columns"
        ColumnName.Name = colColumnName
        ColumnName.Width = 200
        ColumnName.IsVisible = True
        ColumnName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(ColumnName)

        Dim Caption As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Caption.FormatString = ""
        Caption.HeaderText = "Caption"
        Caption.Name = colCaption
        Caption.Width = 250
        Caption.IsVisible = True
        Caption.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(Caption)


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

    Public Sub LoadBlankGrid2()

        gv2.Rows.Clear()
        gv2.Columns.Clear()

        Dim ColumnName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ColumnName.FormatString = ""
        ColumnName.HeaderText = "Columns"
        ColumnName.Name = colColumnName
        ColumnName.Width = 200
        ColumnName.IsVisible = True
        ColumnName.ReadOnly = True
        gv2.MasterTemplate.Columns.Add(ColumnName)

        Dim Caption As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Caption.FormatString = ""
        Caption.HeaderText = "Caption"
        Caption.Name = colCaption
        Caption.Width = 250
        Caption.IsVisible = True
        Caption.ReadOnly = False
        gv2.MasterTemplate.Columns.Add(Caption)


        gv2.AllowDeleteRow = True
        gv2.AllowAddNewRow = False
        gv2.ShowGroupPanel = False
        gv2.AllowColumnReorder = True
        gv2.AllowRowReorder = False
        gv2.EnableSorting = True
        gv2.EnableFiltering = True
        gv2.EnableAlternatingRowColor = True
        gv2.AutoSizeRows = False
        gv2.AllowRowResize = True
        gv2.VerticalScrollState = ScrollState.AlwaysShow
        gv2.HorizontalScrollState = ScrollState.AlwaysShow
        gv2.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        gv2.TableElement.TableHeaderHeight = 40
        gv2.ShowFilteringRow = True

    End Sub
    Public Sub LoadBlankGrid3()

        gv3.Rows.Clear()
        gv3.Columns.Clear()

        Dim ColumnName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ColumnName.FormatString = ""
        ColumnName.HeaderText = "Columns"
        ColumnName.Name = colColumnName
        ColumnName.Width = 200
        ColumnName.IsVisible = True
        ColumnName.ReadOnly = True
        gv3.MasterTemplate.Columns.Add(ColumnName)

        Dim Caption As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Caption.FormatString = ""
        Caption.HeaderText = "Caption"
        Caption.Name = colCaption
        Caption.Width = 250
        Caption.IsVisible = True
        Caption.ReadOnly = False
        gv3.MasterTemplate.Columns.Add(Caption)


        gv3.AllowDeleteRow = True
        gv3.AllowAddNewRow = False
        gv3.ShowGroupPanel = False
        gv3.AllowColumnReorder = True
        gv3.AllowRowReorder = False
        gv3.EnableSorting = True
        gv3.EnableFiltering = True
        gv3.EnableAlternatingRowColor = True
        gv3.AutoSizeRows = False
        gv3.AllowRowResize = True
        gv3.VerticalScrollState = ScrollState.AlwaysShow
        gv3.HorizontalScrollState = ScrollState.AlwaysShow
        gv3.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv3.MasterTemplate.ShowRowHeaderColumn = False
        gv3.TableElement.TableHeaderHeight = 40
        gv3.ShowFilteringRow = True

    End Sub
    Public Sub LoadBlankGrid4()

        gv4.Rows.Clear()
        gv4.Columns.Clear()

        Dim ColumnName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ColumnName.FormatString = ""
        ColumnName.HeaderText = "Columns"
        ColumnName.Name = colColumnName
        ColumnName.Width = 200
        ColumnName.IsVisible = True
        ColumnName.ReadOnly = False
        gv4.MasterTemplate.Columns.Add(ColumnName)

        Dim Caption As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Caption.FormatString = ""
        Caption.HeaderText = "Caption"
        Caption.Name = colCaption
        Caption.Width = 250
        Caption.IsVisible = True
        Caption.ReadOnly = False
        gv4.MasterTemplate.Columns.Add(Caption)


        gv4.AllowDeleteRow = True
        gv4.AllowAddNewRow = False
        gv4.ShowGroupPanel = False
        gv4.AllowColumnReorder = True
        gv4.AllowRowReorder = False
        gv4.EnableSorting = True
        gv4.EnableFiltering = True
        gv4.EnableAlternatingRowColor = True
        gv4.AutoSizeRows = False
        gv4.AllowRowResize = True
        gv4.VerticalScrollState = ScrollState.AlwaysShow
        gv4.HorizontalScrollState = ScrollState.AlwaysShow
        gv4.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv4.MasterTemplate.ShowRowHeaderColumn = False
        gv4.TableElement.TableHeaderHeight = 40
        gv4.ShowFilteringRow = True

    End Sub
    Public Sub LoadBlankGrid5()

        gv5.Rows.Clear()
        gv5.Columns.Clear()

        Dim ColumnName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ColumnName.FormatString = ""
        ColumnName.HeaderText = "Columns"
        ColumnName.Name = colColumnName
        ColumnName.Width = 200
        ColumnName.IsVisible = True
        ColumnName.ReadOnly = True
        gv5.MasterTemplate.Columns.Add(ColumnName)

        Dim Caption As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Caption.FormatString = ""
        Caption.HeaderText = "Caption"
        Caption.Name = colCaption
        Caption.Width = 250
        Caption.IsVisible = True
        Caption.ReadOnly = False
        gv5.MasterTemplate.Columns.Add(Caption)


        gv5.AllowDeleteRow = True
        gv5.AllowAddNewRow = False
        gv5.ShowGroupPanel = False
        gv5.AllowColumnReorder = True
        gv5.AllowRowReorder = False
        gv5.EnableSorting = True
        gv5.EnableFiltering = True
        gv5.EnableAlternatingRowColor = True
        gv5.AutoSizeRows = False
        gv5.AllowRowResize = True
        gv5.VerticalScrollState = ScrollState.AlwaysShow
        gv5.HorizontalScrollState = ScrollState.AlwaysShow
        gv5.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv5.MasterTemplate.ShowRowHeaderColumn = False
        gv5.TableElement.TableHeaderHeight = 40
        gv5.ShowFilteringRow = True

    End Sub
    Sub loadFun()
        
        LoadBlankGrid1()
    End Sub
    Private Sub FrmProgramMappingDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadColumn()
    End Sub
  
   
    Private Sub gv2_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv2.Columns(colColumnName) Then
                    OpenTable2List(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenTable2List(ByVal isButtonClick As Boolean)
       
        
    End Sub
    Sub SaveData()
        Try
            Dim Array As New List(Of ClsProgramMappingDetail)
            Dim obj As New ClsProgramMappingDetail
            For Each grow As GridViewRowInfo In gv1.Rows
                obj = New ClsProgramMappingDetail
                obj.Program_Code = strFormID
                obj.Table_Name = RadPageViewPage1.Text
                obj.Column_Caption = clsCommon.myCstr(grow.Cells(colCaption).Value)
                obj.Column_Name = clsCommon.myCstr(grow.Cells(colColumnName).Value)
               
                Array.Add(obj)
            Next
            For Each grow As GridViewRowInfo In gv2.Rows
                obj = New ClsProgramMappingDetail
                obj.Program_Code = strFormID
                obj.Table_Name = RadPageViewPage2.Text
                obj.Column_Caption = clsCommon.myCstr(grow.Cells(colCaption).Value)
                obj.Column_Name = clsCommon.myCstr(grow.Cells(colColumnName).Value)

                Array.Add(obj)
            Next
            For Each grow As GridViewRowInfo In gv3.Rows
                obj = New ClsProgramMappingDetail
                obj.Program_Code = strFormID
                obj.Table_Name = RadPageViewPage3.Text
                obj.Column_Caption = clsCommon.myCstr(grow.Cells(colCaption).Value)
                obj.Column_Name = clsCommon.myCstr(grow.Cells(colColumnName).Value)

                Array.Add(obj)
            Next
            For Each grow As GridViewRowInfo In gv4.Rows
                obj = New ClsProgramMappingDetail
                obj.Program_Code = strFormID
                obj.Table_Name = RadPageViewPage4.Text
                obj.Column_Caption = clsCommon.myCstr(grow.Cells(colCaption).Value)
                obj.Column_Name = clsCommon.myCstr(grow.Cells(colColumnName).Value)

                Array.Add(obj)
            Next
            For Each grow As GridViewRowInfo In gv5.Rows
                obj = New ClsProgramMappingDetail
                obj.Program_Code = strFormID
                obj.Table_Name = RadPageViewPage5.Text
                obj.Column_Caption = clsCommon.myCstr(grow.Cells(colCaption).Value)
                obj.Column_Name = clsCommon.myCstr(grow.Cells(colColumnName).Value)

                Array.Add(obj)
            Next
            
            If (ClsProgramMappingDetail.SaveData(Array)) Then
                clsCommon.MyMessageBoxShow("Data Saved Successfully")

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
   

    Private Sub gv3_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv3.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv3.Columns(colColumnName) Then
                    OpenTable3List(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenTable3List(ByVal isButtonClick As Boolean)

    End Sub
   
    Private Sub gv4_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv4.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv4.Columns(colColumnName) Then
                    OpenTable4List(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenTable4List(ByVal isButtonClick As Boolean)
        
    End Sub
  

    Private Sub gv5_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv5.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv5.Columns(colColumnName) Then
                    OpenTable5List(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenTable5List(ByVal isButtonClick As Boolean)

    End Sub
    
    Sub LoadColumn()
        isInsideLoadData = True
        Dim page1 As String = clsDBFuncationality.getSingleValue("select Table_1 from TSPL_PROGRAM_TABLE_MAPPING where Program_Code ='" + strFormID + "'")
        Dim page2 As String = clsDBFuncationality.getSingleValue("select Table_2 from TSPL_PROGRAM_TABLE_MAPPING  where Program_Code ='" + strFormID + "'")
        Dim page3 As String = clsDBFuncationality.getSingleValue("select Table_3 from TSPL_PROGRAM_TABLE_MAPPING  where Program_Code ='" + strFormID + "'")
        Dim page4 As String = clsDBFuncationality.getSingleValue("select Table_4 from TSPL_PROGRAM_TABLE_MAPPING  where Program_Code ='" + strFormID + "'")
        Dim page5 As String = clsDBFuncationality.getSingleValue("select Table_5 from TSPL_PROGRAM_TABLE_MAPPING  where Program_Code ='" + strFormID + "'")

        RadPageViewPage1.Text = page1
        RadPageViewPage2.Text = page2
        RadPageViewPage3.Text = page3
        RadPageViewPage4.Text = page4
        RadPageViewPage5.Text = page5
        loadFun()
        LoadBlankGrid2()
        LoadBlankGrid3()
        LoadBlankGrid4()
        LoadBlankGrid5()
        Dim qry1 As String = "select INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME , TSPL_PROGRAM_MAPPING_DETAIL.Column_Caption,INFORMATION_SCHEMA.COLUMNS.TABLE_NAME"
        qry1 += " from INFORMATION_SCHEMA.COLUMNS LEFT OUTER JOIN TSPL_PROGRAM_MAPPING_DETAIL ON TSPL_PROGRAM_MAPPING_DETAIL.Column_Name=INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME and TSPL_PROGRAM_MAPPING_DETAIL.Table_Name=INFORMATION_SCHEMA.COLUMNS.TABLE_NAME "
        qry1 += " where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME='" + page1 + "'"
        'clsDBFuncationality.ExecuteNonQuery(qry1)
        Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
        For Each dr As DataRow In dt1.Rows

            gv1.Rows.AddNew()
            gv1.Rows(gv1.Rows.Count - 1).Cells(colColumnName).Value = clsCommon.myCstr(dr("COLUMN_NAME"))
            gv1.Rows(gv1.Rows.Count - 1).Cells(colCaption).Value = clsCommon.myCstr(dr("Column_Caption"))

          
        Next

        Dim qry2 As String = "select INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME , TSPL_PROGRAM_MAPPING_DETAIL.Column_Caption,INFORMATION_SCHEMA.COLUMNS.TABLE_NAME"
        qry2 += " from INFORMATION_SCHEMA.COLUMNS LEFT OUTER JOIN TSPL_PROGRAM_MAPPING_DETAIL ON TSPL_PROGRAM_MAPPING_DETAIL.Column_Name=INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME and TSPL_PROGRAM_MAPPING_DETAIL.Table_Name=INFORMATION_SCHEMA.COLUMNS.TABLE_NAME "
        qry2 += " where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME='" + page2 + "'"
        'clsDBFuncationality.ExecuteNonQuery(qry2)
        Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
        For Each dr2 As DataRow In dt2.Rows

            gv2.Rows.AddNew()
            gv2.Rows(gv2.Rows.Count - 1).Cells(colColumnName).Value = clsCommon.myCstr(dr2("COLUMN_NAME"))
            gv2.Rows(gv2.Rows.Count - 1).Cells(colCaption).Value = clsCommon.myCstr(dr2("Column_Caption"))

        Next

        Dim qry3 As String = "select INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME , TSPL_PROGRAM_MAPPING_DETAIL.Column_Caption,INFORMATION_SCHEMA.COLUMNS.TABLE_NAME"
        qry3 += " from INFORMATION_SCHEMA.COLUMNS LEFT OUTER JOIN TSPL_PROGRAM_MAPPING_DETAIL ON TSPL_PROGRAM_MAPPING_DETAIL.Column_Name=INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME and TSPL_PROGRAM_MAPPING_DETAIL.Table_Name=INFORMATION_SCHEMA.COLUMNS.TABLE_NAME "
        qry3 += " where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME='" + page3 + "'"
        'clsDBFuncationality.ExecuteNonQuery(qry3)
        Dim dt3 As DataTable = clsDBFuncationality.GetDataTable(qry3)
        For Each dr3 As DataRow In dt3.Rows

            gv3.Rows.AddNew()
            gv3.Rows(gv3.Rows.Count - 1).Cells(colColumnName).Value = clsCommon.myCstr(dr3("COLUMN_NAME"))
            gv3.Rows(gv3.Rows.Count - 1).Cells(colCaption).Value = clsCommon.myCstr(dr3("Column_Caption"))

        Next

        Dim qry4 As String = "select INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME , TSPL_PROGRAM_MAPPING_DETAIL.Column_Caption,INFORMATION_SCHEMA.COLUMNS.TABLE_NAME"
        qry4 += " from INFORMATION_SCHEMA.COLUMNS LEFT OUTER JOIN TSPL_PROGRAM_MAPPING_DETAIL ON TSPL_PROGRAM_MAPPING_DETAIL.Column_Name=INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME and TSPL_PROGRAM_MAPPING_DETAIL.Table_Name=INFORMATION_SCHEMA.COLUMNS.TABLE_NAME "
        qry4 += " where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME='" + page4 + "'"
        'clsDBFuncationality.ExecuteNonQuery(qry4)
        Dim dt4 As DataTable = clsDBFuncationality.GetDataTable(qry4)
        For Each dr4 As DataRow In dt4.Rows

            gv4.Rows.AddNew()
            gv4.Rows(gv4.Rows.Count - 1).Cells(colColumnName).Value = clsCommon.myCstr(dr4("COLUMN_NAME"))

            gv4.Rows(gv4.Rows.Count - 1).Cells(colCaption).Value = clsCommon.myCstr(dr4("Column_Caption"))
        Next

        Dim qry5 As String = "select INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME , TSPL_PROGRAM_MAPPING_DETAIL.Column_Caption,INFORMATION_SCHEMA.COLUMNS.TABLE_NAME"
        qry5 += " from INFORMATION_SCHEMA.COLUMNS LEFT OUTER JOIN TSPL_PROGRAM_MAPPING_DETAIL ON TSPL_PROGRAM_MAPPING_DETAIL.Column_Name=INFORMATION_SCHEMA.COLUMNS.COLUMN_NAME and TSPL_PROGRAM_MAPPING_DETAIL.Table_Name=INFORMATION_SCHEMA.COLUMNS.TABLE_NAME "
        qry5 += " where INFORMATION_SCHEMA.COLUMNS.TABLE_NAME='" + page5 + "'"
        'clsDBFuncationality.ExecuteNonQuery(qry5)
        Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(qry5)
        For Each dr5 As DataRow In dt5.Rows

            gv5.Rows.AddNew()
            gv5.Rows(gv5.Rows.Count - 1).Cells(colColumnName).Value = clsCommon.myCstr(dr5("COLUMN_NAME"))
            gv5.Rows(gv5.Rows.Count - 1).Cells(colCaption).Value = clsCommon.myCstr(dr5("Column_Caption"))

        Next

        isInsideLoadData = False
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub DeleteData()
        'For Each grow As GridViewRowInfo In gv1.Rows
        Dim qry As String = "delete from TSPL_PROGRAM_MAPPING_DETAIL where Program_Code='" + strFormID + "'"
        clsDBFuncationality.ExecuteNonQuery(qry)
        'Next
        clsCommon.MyMessageBoxShow("delete successfully")

    End Sub
    Sub reset()
        For Each grow As GridViewRowInfo In gv1.Rows
            grow.Cells(colCaption).Value = ""
        Next
        For Each grow As GridViewRowInfo In gv2.Rows
            grow.Cells(colCaption).Value = ""
        Next
        For Each grow As GridViewRowInfo In gv3.Rows
            grow.Cells(colCaption).Value = ""
        Next
        For Each grow As GridViewRowInfo In gv4.Rows
            grow.Cells(colCaption).Value = ""
        Next
        For Each grow As GridViewRowInfo In gv5.Rows
            grow.Cells(colCaption).Value = ""
        Next
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
        reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
