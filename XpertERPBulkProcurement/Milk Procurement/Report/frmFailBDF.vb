Imports common
Imports System.ComponentModel
Imports System.IO

Public Class frmFailBDF
    Inherits FrmMainTranScreen

    Dim LocalSourceDirectory As String = Nothing
    Dim LocalSucessDirectory As String = Nothing
    Dim LocalFailureDirectory As String = Nothing
    Dim isCollectMPFromBenny As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const ColPath As String = "ColPath"
    Const ColView As String = "View"
    Const ColDelete As String = "Delete"

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub

    Public Sub BlankAllControls()
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.ReadOnly = False

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = ColSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        Gv1.MasterTemplate.Columns.Add(repoLineNo)


        Dim repoPath As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPath.FormatString = ""
        repoPath.HeaderText = "Path"
        repoPath.Width = 800
        repoPath.Name = ColPath
        repoPath.ReadOnly = True
        repoPath.IsVisible = True
        Gv1.MasterTemplate.Columns.Add(repoPath)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show Error"
        ShowBtn.HeaderText = ""
        ShowBtn.Name = ColView
        ShowBtn.FieldName = ColView
        ShowBtn.Width = 100
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Gv1.Columns.Add(ShowBtn)

        Dim repoType As GridViewCommandColumn = New GridViewCommandColumn()
        repoType.FormatString = ""
        repoType.UseDefaultText = True
        repoType.DefaultText = "Try Again"
        repoType.HeaderText = ""
        repoType.Width = 100
        repoType.Name = ColDelete
        repoType.FieldName = ColDelete
        repoType.IsVisible = False
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        Gv1.MasterTemplate.Columns.Add(repoType)


        Gv1.AllowDeleteRow = True
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = True
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = True
        Gv1.EnableFiltering = True
        Gv1.EnableAlternatingRowColor = True
        Gv1.AutoSizeRows = False
        Gv1.AllowRowResize = True
        Gv1.VerticalScrollState = ScrollState.AutoHide
        Gv1.HorizontalScrollState = ScrollState.AutoHide
        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.ShowFilteringRow = True

    End Sub

    Private Sub rptMilkBillProcurementSummary_Load(sender As Object, e As EventArgs) Handles Me.Load
        If File.Exists("ConfigMPCollFromBenny.Txp") Then
            Dim objReader As New System.IO.StreamReader("ConfigMPCollFromBenny.Txp")

            While objReader.Peek() <> -1
                Dim line As String = objReader.ReadLine()

                If clsCommon.myLen(line) > 0 Then
                    Dim spearator As Char() = {"#"c}
                    Dim strTemp As String() = line.Split(spearator)

                    If strTemp.Length = 3 Then
                        LocalSourceDirectory = strTemp(0).Trim()
                        LocalSucessDirectory = strTemp(1).Trim()
                        LocalFailureDirectory = strTemp(2).Trim()

                        If clsCommon.myLen(LocalSourceDirectory) > 0 AndAlso clsCommon.myLen(LocalSucessDirectory) > 0 AndAlso clsCommon.myLen(LocalFailureDirectory) > 0 Then
                            isCollectMPFromBenny = True
                        End If
                    End If
                End If
            End While
        End If


        Reset()
    End Sub
    Sub Reset()
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Print(False)
    End Sub

    Sub GetDirectories(ByVal StartPath As String, ByRef DirectoryList As ArrayList, ByRef arrPat As List(Of String))
        Dim Dirs() As String = Directory.GetDirectories(StartPath)
        DirectoryList.AddRange(Dirs)
        For Each Dir As String In Dirs
            GetDirectories(Dir, DirectoryList, arrPat)
        Next
        For Each item As String In DirectoryList
            If Not arrPat.Contains(item) Then
                arrPat.Add(item)
            End If
        Next
    End Sub

    Sub Print(ByVal isPrint As Boolean)
        Try
            BlankAllControls()
            If Not isCollectMPFromBenny Then
                Throw New Exception("This report is not for you")
            End If
            isInsideLoadData = True
            Dim flag As Boolean = True
            Dim arrPat As New List(Of String)
            If clsCommon.myLen(LocalFailureDirectory) > 0 Then
                Dim DirList As New ArrayList
                GetDirectories(LocalFailureDirectory, DirList, arrPat)
            End If
            clsCommon.ProgressBarPercentShow()
            For cntFolder As Integer = 0 To arrPat.Count - 1
                Try
                    clsCommon.ProgressBarPercentUpdate(((cntFolder) * 100 / (arrPat.Count)), "Loading Failed BDF Files")
                    If clsCommon.myLen(arrPat(cntFolder)) > 0 Then
                        Dim strFileSize As String = ""
                        Dim di As New IO.DirectoryInfo(arrPat(cntFolder))
                        Dim aryFi As IO.FileInfo() = di.GetFiles("*.BDF")
                        Dim fi As IO.FileInfo

                        Dim Total As Integer = aryFi.Count
                        For Each fi In aryFi
                            Try

                                flag = False
                                Gv1.Rows.AddNew()
                                Gv1.Rows(Gv1.RowCount - 1).Cells(ColSNo).Value = Gv1.Rows.Count
                                Gv1.Rows(Gv1.RowCount - 1).Cells(ColPath).Value = fi.FullName
                            Catch ex As Exception
                                Throw New Exception(Environment.NewLine + "Error in File " + arrPat(cntFolder) + "\" + fi.Name + Environment.NewLine + ex.Message)
                            End Try
                        Next
                    End If
                Catch ex1 As Exception
                End Try
            Next
            clsCommon.ProgressBarPercentHide()
            If flag Then
                clsCommon.MyMessageBoxShow(Me, "No Failed BDF File Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub




    Private Sub btnReset_Click(sender As Object, e As EventArgs)
        EnableDisaableControls(True)
    End Sub
    Sub EnableDisaableControls(ByVal flag As Boolean)
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)
        Print(True)
    End Sub

    Dim isInsideLoadData As Boolean = False

    Sub gv1_CommandCellClick(ByVal sender As Object, ByVal e As EventArgs) Handles Gv1.CommandCellClick
        Try
            If (Not isInsideLoadData) Then
                isInsideLoadData = True
                If Gv1.CurrentColumn Is Gv1.Columns(ColDelete) Then
                    If clsCommon.MyMessageBoxShow(" Do you want to Delete This Document.", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        'funDelete(Gv1.CurrentRow.Cells(ColCODE).Value)
                    End If
                ElseIf Gv1.CurrentColumn Is Gv1.Columns(ColView) Then
                    Dim strPath As String = clsCommon.myCstr(Gv1.CurrentRow.Cells(ColPath).Value)
                    If clsCommon.myLen(strPath) > 0 Then
                        If File.Exists(strPath + "_Log.txt") Then
                            System.Diagnostics.Process.Start(strPath + "_Log.txt")
                        Else
                            Throw New Exception("Log File Not found")
                        End If
                    End If
                End If
                isInsideLoadData = False
                End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub
End Class
