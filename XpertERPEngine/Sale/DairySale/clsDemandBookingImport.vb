Imports System.IO
Imports Microsoft.Office.Interop
Public Module clsDemandBookingImport
    Public Function importExcel(ByVal gv As RadGridView, ByVal ParamArray fieldNames As String()) As Boolean
        Return importExcel("", "", gv, fieldNames)
    End Function
    Public Function importExcel(ByRef FileName As String, ByRef SafeFileName As String, ByVal gv As RadGridView, ByVal ParamArray fieldNames As String()) As Boolean
        Return importExcel(False, FileName, SafeFileName, gv, fieldNames)
    End Function
    Public Function importExcel(ByVal DBFOnly As Boolean, ByRef FileName As String, ByRef SafeFileName As String, ByVal gv As RadGridView, ByVal ParamArray fieldNames As String()) As Boolean
        Try
            If Not LoadDocument(DBFOnly, gv, "", FileName, SafeFileName, fieldNames) Then
                Return False
            End If
            Dim fieldCount As Integer = fieldNames.Length
            Dim strfields As String = ""
            For Each field As String In fieldNames
                strfields = strfields + field + ","
            Next
            If fieldCount <= gv.ColumnCount Then
                Dim i As Integer = 0
                Dim arr As ArrayList = New ArrayList()
                For Each GC As GridViewColumn In gv.Columns
                    arr.Add(GC.HeaderText.Trim().ToUpper())
                Next
                For Each field As String In fieldNames
                    If arr.Contains(field.Trim().ToUpper()) Then
                        For Each GC As GridViewColumn In gv.Columns
                            If GC.HeaderText = field Then
                                gv.Columns.Move(GC.Index, i)
                                Exit For
                            End If
                        Next
                    Else
                        common.clsCommon.MyMessageBoxShow("Excel Sheet is not in expected format.It should have the columns named - " + strfields)
                        Return False
                    End If
                    i = i + 1
                Next
            Else
                common.clsCommon.MyMessageBoxShow("Excel Sheet is not in expected format. It should have the columns named - " + strfields)
                Return False
            End If
        Catch ex As Exception
            'common.clsCommon.MyMessageBoxShow("No data transfered.", "Import Error", MessageBoxButtons.OK)
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
        Return True
    End Function
    Public Function LoadDocument(ByVal DBFOnly As Boolean, ByVal gv As RadGridView, sheetName As String, ByRef FileName As String, ByRef SafeFileName As String, ByVal ParamArray fieldNames As String()) As Boolean
        Dim workbook As Excel.Workbook = Nothing
        Dim rvalue As Boolean = False
        Dim ofd As OpenFileDialog = New OpenFileDialog()
        Dim filePath As String
        If clsCommon.myLen(sheetName) <= 0 Then
            sheetName = "Sheet1"
        End If
        FileName = ""
        SafeFileName = ""
        If DBFOnly Then
            ofd.Filter = "DBF Files (*.DBF) |*.DBF"
        Else
            ofd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
        End If
        If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            filePath = ofd.FileName
            FileName = ofd.FileName
            SafeFileName = ofd.SafeFileName
        Else
            Return False
        End If
        Dim Extension As String = Path.GetExtension(filePath)
        Dim conStr As String = ""
        Dim selectedFormat As String = Extension
        Dim dt As DataTable = New DataTable()
        Dim colCount As Integer = 0
        clsCommon.ProgressBarPercentShow()
        Try
            Dim oApp As Excel.Application
            oApp = New Excel.Application
            oApp.Visible = False
            oApp.DisplayAlerts = False
            workbook = oApp.Workbooks.Open(filePath)
            Dim worksheet As Microsoft.Office.Interop.Excel.Worksheet = workbook.Worksheets(1)
            Dim r As Microsoft.Office.Interop.Excel.Range = worksheet.UsedRange
            Dim array(,) As Object = r.Value(Microsoft.Office.Interop.Excel.XlRangeValueDataType.xlRangeValueDefault)
            Dim bound0 As Integer = array.GetUpperBound(0)
            Dim bound1 As Integer = array.GetUpperBound(1)
            For i As Integer = 1 To bound1
                Dim str As String = ""
                clsCommon.ProgressBarPercentUpdate(((i) * 100 / bound1), "Getting Field List " & (i) & " Of Total " & bound1 & " From Excel Sheet")
                If clsCommon.myCstr(array(1, i)).Trim() = "" Then
                    str = clsCommon.myCstr(array(1, i - 1))
                    If str <> "Total" Then
                        array(1, i) = str + clsCommon.myCstr(i)
                        dt.Columns.Add(clsCommon.myCstr(array(1, i)).Trim(), "".GetType())
                        dt.Columns(clsCommon.myCstr(array(1, i)).Trim()).Caption = array(1, i)
                        str = ""
                    End If
                Else
                    dt.Columns.Add(clsCommon.myCstr(array(1, i)).Trim(), "".GetType())
                    dt.Columns(clsCommon.myCstr(array(1, i)).Trim()).Caption = array(1, i)
                End If
            Next
            For j As Integer = 2 To bound0
                clsCommon.ProgressBarPercentUpdate(((j) * 100 / bound0), "Getting Record List " & (j) & " Of Total " & bound0 & " From Excel Sheet")
                Dim dr As DataRow = dt.NewRow()
                Dim strs As String = ""
                For x As Integer = 1 To bound1
                    If clsCommon.myCstr(array(1, x)).Trim() = "" Then
                        strs = clsCommon.myCstr(array(1, x - 1)).Trim()
                        If strs <> "Total" Then
                            array(1, x) = strs + clsCommon.myCstr(x)
                            dr(clsCommon.myCstr(array(1, x)).Trim()) = array(j, x)
                            strs = ""
                        End If
                    Else
                        dr(clsCommon.myCstr(array(1, x)).Trim()) = array(j, x)
                    End If
                Next
                dt.Rows.Add(dr)
            Next
            oApp.Workbooks.Close()
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                rvalue = False
            Else
                gv.DataSource = dt
                rvalue = True
            End If
            clsCommon.ProgressBarPercentHide()
        Catch ex As IOException
            clsCommon.ProgressBarPercentHide()
            Throw New Exception(ex.Message)
        End Try
        Return rvalue
    End Function
End Module
