Imports common
Imports System.IO
Imports System.ComponentModel
Imports Telerik.WinControls.UI.Export

Public Class frmMilkSampleMCCExtraQCParameter
    Public strSampleNo As String = ""
    Private dt As DataTable = Nothing
    Const colSampleNo As String = "colSampleNo"

    Private Sub FrmFreeGrid_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            btnopen.Visible = True
            Dim dtTotalSample As DataTable = clsDBFuncationality.GetDataTable("select SAMPLE_NO from TSPL_MILK_SAMPLE_DETAIL where doc_code='" + strSampleNo + "' order by SAMPLE_NO")
            If dtTotalSample Is Nothing OrElse dtTotalSample.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Fist take Sample then try to fill Extra QC Parameters", Me.Text)
                Me.Close()
            Else
                loadBlankParameterGrid()
                For Each dr As DataRow In dtTotalSample.Rows
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colSampleNo).Value = clsCommon.myCdbl(dr("SAMPLE_NO"))
                Next

                Dim ArrParamDetail As List(Of clsMilkSampleQCParameterDetail) = clsMilkSampleQCParameterDetail.getData(strSampleNo)
                If ArrParamDetail IsNot Nothing AndAlso ArrParamDetail.Count > 0 Then
                    For Each objTr As clsMilkSampleQCParameterDetail In ArrParamDetail
                        Try
                            gv1.Rows(objTr.Sample_No - 1).Cells(objTr.Param_Field_Code).Value = objTr.Param_Field_Value
                        Catch ex As Exception
                        End Try
                    Next
                End If
                gv1.CurrentRow = gv1.Rows(0)
                gv1.CurrentColumn = gv1.Columns(0)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Sub loadBlankParameterGrid()


        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60

        ''---------------------
        Dim dt As DataTable = clsMilkSampleQCParameterDetail.GetExtraQCParameters()
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        Dim repoComboColumn As GridViewComboBoxColumn
        Dim repoTextColumn As GridViewTextBoxColumn
        Dim repoDecimalColumn As GridViewDecimalColumn = Nothing

        repoDecimalColumn = New GridViewDecimalColumn()
        repoDecimalColumn.Name = colSampleNo
        repoDecimalColumn.Width = 50
        repoDecimalColumn.FormatString = "{0:n0}"
        repoDecimalColumn.DecimalPlaces = 0
        repoDecimalColumn.HeaderText = "Sample No"
        repoDecimalColumn.Tag = colSampleNo
        repoDecimalColumn.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDecimalColumn)

        If pFields Then
            For i As Integer = 0 To dt.Rows.Count() - 1
                If clsCommon.CompairString(dt.Rows(i)("nature"), "R") = CompairStringResult.Equal Then
                    repoDecimalColumn = New GridViewDecimalColumn()
                    repoDecimalColumn.Name = dt.Rows(i)("Code")
                    repoDecimalColumn.Width = 120
                    repoDecimalColumn.FormatString = "{0:n3}"
                    If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal OrElse clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                        repoDecimalColumn.FormatString = "{0:n2}"
                    End If
                    repoDecimalColumn.DecimalPlaces = 3
                    repoDecimalColumn.HeaderText = dt.Rows(i)("Description")
                    repoDecimalColumn.Tag = dt.Rows(i)("Type")
                    repoDecimalColumn.ReadOnly = False
                    gv1.MasterTemplate.Columns.Add(repoDecimalColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "A") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = OpenParameterValueList(dt.Rows(i)("Code"))
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    repoComboColumn.ReadOnly = False
                    gv1.MasterTemplate.Columns.Add(repoComboColumn)
                ElseIf clsCommon.CompairString(dt.Rows(i)("nature"), "B") = CompairStringResult.Equal Then
                    repoComboColumn = New GridViewComboBoxColumn()
                    repoComboColumn.Name = dt.Rows(i)("Code")
                    repoComboColumn.Width = 120
                    repoComboColumn.HeaderText = dt.Rows(i)("Description")
                    repoComboColumn.Tag = dt.Rows(i)("Type")
                    repoComboColumn.DataSource = FillYesNoValue()
                    repoComboColumn.DisplayMember = "Value"
                    repoComboColumn.ValueMember = "Value"
                    repoComboColumn.ReadOnly = False
                    gv1.MasterTemplate.Columns.Add(repoComboColumn)
                Else
                    repoTextColumn = New GridViewTextBoxColumn()
                    repoTextColumn.Name = dt.Rows(i)("Code")
                    repoTextColumn.Width = 120
                    repoTextColumn.HeaderText = dt.Rows(i)("Description")
                    repoTextColumn.Tag = dt.Rows(i)("Type")
                    repoTextColumn.ReadOnly = False
                    gv1.MasterTemplate.Columns.Add(repoDecimalColumn)
                End If
            Next
        End If

        Dim blnExit As Boolean = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnChooser = True
        gv1.AllowColumnReorder = True
    End Sub

    Function OpenParameterValueList(ByVal code As String) As DataTable
        Dim qry As String = " select '' as value union all  select  Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function

    Function FillYesNoValue() As DataTable
        Dim qry As String = " select '' as value union all select 'Yes' as value union all select 'No' as value "
        Return clsDBFuncationality.GetDataTable(qry)
    End Function


     

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Me.Close()
    End Sub

    Private Sub Export(ByVal Type As Integer)
        If SaveFileDialog.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
            Return
        End If
        If SaveFileDialog.FileName.Equals(String.Empty) Then
            RadMessageBox.SetThemeName(gv1.ThemeName)
            ''sfd.Filter = "Excel (*.xls;*.xlsx)|*.xls;*.xlsx"
            common.clsCommon.MyMessageBoxShow("Please enter a file name.")
            Return
        End If
        Dim fileName As String = Me.SaveFileDialog.FileName

        Try
            Select Case Type

                Case 1 'export to excelML       
                    fileName += ".xls"
                    RunExportToExcelML(fileName)
                Case 2 'export to CSV      
                    fileName += ".csv"
                    RunExportToCSV(fileName)
                    'Case 3 'export to HTML                    
                    '    RunExportToHTML(fileName, openExportFile)
                Case 4 'export to PDF   
                    fileName += ".pdf"
                    RunExportToPDF(fileName)
            End Select

            If common.clsCommon.MyMessageBoxShow("Open the Exported file", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                Try

                    System.Diagnostics.Process.Start(fileName)
                Catch ex As Exception
                    Dim message As String = String.Format("The file cannot be opened on your system." & Constants.vbLf & "Error message: {0}", ex.Message)
                    common.clsCommon.MyMessageBoxShow(message, "Open File", MessageBoxButtons.OK, RadMessageIcon.Error)
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RunExportToExcelML(ByVal fileName As String)
        Dim excelExporter As New ExportToExcelML(gv1)
        excelExporter.SheetName = Me.Text
        excelExporter.SheetMaxRows = ExcelMaxRows._1048576
        excelExporter.SummariesExportOption = SummariesOption.ExportAll
        'excelExporter.ExportVisualSettings = Me.exportVisualSettings
        Try
            excelExporter.RunExport(fileName)


        Catch ex As IOException

            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RunExportToCSV(ByVal fileName As String)
        Dim csvExporter As New ExportToCSV(gv1)
        csvExporter.SummariesExportOption = SummariesOption.ExportAll
        Try
            csvExporter.RunExport(fileName)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RunExportToPDF(ByVal fileName As String)
        Dim pdfExporter As New ExportToPDF(gv1)
        pdfExporter.PdfExportSettings.Title = "My PDF Title"
        pdfExporter.PdfExportSettings.PageWidth = 297
        pdfExporter.PdfExportSettings.PageHeight = 210
        pdfExporter.PageTitle = Me.Text
        pdfExporter.FitToPageWidth = True
        Try
            pdfExporter.RunExport(fileName)
            RadMessageBox.SetThemeName(gv1.ThemeName)
        Catch ex As IOException
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RadMenuItem4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem4.Click
        Export(1)
    End Sub

    Private Sub RadMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem5.Click
        Export(4)
    End Sub

     

    Private Function GetParamCollection() As List(Of clsMilkSampleQCParameterDetail)
        Dim ArrParamDetail = New List(Of clsMilkSampleQCParameterDetail)
        Dim objParam As clsMilkSampleQCParameterDetail = Nothing
        For i As Integer = 0 To gv1.Columns.Count - 1
            If Not clsCommon.CompairString(gv1.Columns(i).Name, colSampleNo) = CompairStringResult.Equal Then
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    objParam = New clsMilkSampleQCParameterDetail
                    objParam.Param_Field_Code = clsCommon.myCstr(gv1.Columns(i).Name)
                    objParam.Param_Field_Desc = clsCommon.myCstr(gv1.Columns(i).HeaderText)
                    objParam.Param_Field_Value = clsCommon.myCstr(gv1.Rows(jj).Cells(i).Value)
                    objParam.Param_Type = clsCommon.myCstr(gv1.Columns(i).Tag)
                    objParam.Sample_No = clsCommon.myCdbl(gv1.Rows(jj).Cells(colSampleNo).Value)
                    ArrParamDetail.Add(objParam)
                Next
            End If
        Next
        Return ArrParamDetail
    End Function

    Private Sub btnopen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnopen.Click
        Try
            If AllowToSave() Then
                If clsMilkSampleQCParameterDetail.saveData(strSampleNo, GetParamCollection()) Then
                    clsCommon.MyMessageBoxShow("Data saved successfully", Me.Text)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Function AllowToSave() As Boolean
        Dim qry As String = "select Doc_code from tspl_milk_shift_end_head " + Environment.NewLine + _
        "inner join (select Mcc_code,Doc_date,Shift from TSPL_MILK_SAMPLE_HEAD where Doc_Code='" + strSampleNo + "')x" + Environment.NewLine + _
        "on x.mcc_code=tspl_milk_shift_end_head.mcc_code and  x.shift=tspl_milk_shift_end_head.shift and  convert(date, x.Doc_date,103)=convert(date, tspl_milk_shift_end_head.Doc_date,103)"
        If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry)) > 0 Then
            Throw New Exception("Shift End is Completed cannot Enter/Change any QC Parameter")
        End If

        Dim isManadatory As Integer = 0
        Dim NatureType As String = ""
        For jj As Integer = 0 To gv1.Rows.Count - 1
            For i As Integer = 0 To gv1.Columns.Count - 1
                isManadatory = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IsMandatory from TSPL_PARAMETER_MASTER where Code='" & gv1.Columns(i).Name & "'"))
                NatureType = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Nature  from TSPL_PARAMETER_MASTER where Code='" & gv1.Columns(i).Name & "'"))

                If clsCommon.CompairString(NatureType, "R") = CompairStringResult.Equal Then
                    If isManadatory = 1 And clsCommon.myCdbl(gv1.Rows(jj).Cells(i).Value) = 0 Then
                        Throw New Exception("Please Fill : " & gv1.Columns(i).HeaderText & " , It is Mandatory ")
                    End If
                ElseIf clsCommon.CompairString(NatureType, "A") = CompairStringResult.Equal Then
                    If isManadatory = 1 And clsCommon.myLen(gv1.Rows(jj).Cells(i).Value) <= 0 Then
                        Throw New Exception("Please Fill : " & gv1.Columns(i).HeaderText & " , It is Mandatory ")
                    End If
                ElseIf clsCommon.CompairString(NatureType, "B") = CompairStringResult.Equal Then
                    If isManadatory = 1 And (clsCommon.myLen(gv1.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gv1.Rows(jj).Cells(i).Value), "Yes") <> CompairStringResult.Equal) And (clsCommon.myLen(gv1.Rows(jj).Cells(i).Value) <= 0 OrElse clsCommon.CompairString((gv1.Rows(jj).Cells(i).Value), "No") <> CompairStringResult.Equal) Then
                        Throw New Exception("Please Fill : " & gv1.Columns(i).HeaderText & " , It is Mandatory ")
                    End If
                End If
            Next
        Next
        Return True
    End Function
End Class
