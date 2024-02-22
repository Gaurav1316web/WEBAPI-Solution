Imports common
Imports System.Data.SqlClient
Public Class frmGazeReading
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim ErrorControl As clsErrorControl = New clsErrorControl()
    Dim isNewEntry As Boolean = True

    Const colCM As String = "colCM"
    Const colMM As String = "colMM"

    Const colMM0 As String = "colMM0"
    Const colMM1 As String = "colMM1"
    Const colMM2 As String = "colMM2"
    Const colMM3 As String = "colMM3"
    Const colMM4 As String = "colMM4"
    Const colMM5 As String = "colMM5"
    Const colMM6 As String = "colMM6"
    Const colMM7 As String = "colMM7"
    Const colMM8 As String = "colMM8"
    Const colMM9 As String = "colMM9"

#End Region

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub FrmPriceChartMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        UcAttachment1.Form_ID = MyBase.Form_ID
        UcAttachment1.isDeleteTheAttachment = False
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        If btnsave.Visible = True Then
            btnExport.Enabled = True
            btnImport.Enabled = True
        Else
            btnExport.Enabled = False
            btnImport.Enabled = False
        End If
        btnExport.Visible = MyBase.isExport
    End Sub
    Sub Reset()
        txtCode.Value = ""
        txtDesc.Text = ""
        txtCapacity.Value = 0
        LoadBlankGrid()

        isNewEntry = True
        txtCode.MyReadOnly = False
        btndelete.Enabled = False
        btnsave.Enabled = True
        UcAttachment1.BlankAllControls()
    End Sub
    Function AllowToSave() As Boolean
        Try
            'If txtVLC.arrValueMember Is Nothing OrElse txtVLC.arrValueMember.Count <= 0 Then
            '    txtVLC.Focus()
            '    txtVLC.Select()
            '    ErrorControl.SetError(txtVLC, "Please select VLC")
            '    Throw New Exception("Please select VLC")
            'Else
            '    ErrorControl.ResetError(txtVLC)
            'End If
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function
    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsGazeReading()
                obj.Code = txtCode.Value
                obj.Description = txtDesc.Text
                obj.Capacity = txtCapacity.Value

                obj.Arr = New List(Of clsGazeReadingDetail)
                For ii As Integer = 0 To gv.Rows.Count - 1
                    For jj As Integer = 0 To 9
                        Dim objTR As New clsGazeReadingDetail
                        objTR.MM = clsCommon.myCDecimal(clsCommon.myCstr(gv.Rows(ii).Cells(colCM).Value) + clsCommon.myCstr(gv.Columns(colMM + clsCommon.myCstr(jj)).HeaderText))
                        objTR.Value = clsCommon.myCDecimal(gv.Rows(ii).Cells(colMM + clsCommon.myCstr(jj)).Value)
                        If objTR.Value > 0 Then
                            obj.Arr.Add(objTR)
                        End If
                    Next
                Next
                If clsGazeReading.SaveData(obj, isNewEntry) Then
                    UcAttachment1.SaveData(obj.Code)
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Code, NavigatorType.Current)
                End If
            Else
                txtCode.MyReadOnly = False
                btndelete.Enabled = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Sub FrmPriceChartMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            btnnew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            clsGazeReading.DeleteData(txtCode.Value)
            clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Reset()
            Dim obj As clsGazeReading = clsGazeReading.GetData(strCode, NavType)
            isNewEntry = True
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0 Then
                isNewEntry = False
                txtCode.Value = obj.Code
                txtDesc.Text = obj.Description
                txtCapacity.Value = obj.Capacity
                Dim PreCM As Integer = -1
                Dim PreMM As Integer = -1
                For Each objTR As clsGazeReadingDetail In obj.Arr
                    Dim CurrCM As Integer = Math.Floor(objTR.MM / 10)
                    If CurrCM <> PreCM Then
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colCM).Value = CurrCM
                        PreCM = CurrCM
                    End If
                    Dim CurrMM As Integer = Microsoft.VisualBasic.Right(clsCommon.myCstr(objTR.MM), 1)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM + clsCommon.myCstr(CurrMM)).Value = objTR.Value
                Next
                UcAttachment1.LoadData(obj.Code)
                btndelete.Enabled = True
                txtCode.MyReadOnly = True
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        LoadData(txtCode.Value, NavType)
    End Sub
    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim qry As String = "select Code,Description,Capacity from TSPL_GAZE_READING"

        Dim whrcls As String = ""
        txtCode.Value = clsCommon.ShowSelectForm("F#GRE", qry, "Code", whrcls, txtCode.Value, "Code", isButtonClicked)

        If clsCommon.myLen(txtCode.Value) > 0 Then
            LoadData(txtCode.Value, NavigatorType.Current)
        Else
            Reset()
        End If
    End Sub
    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub
    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "CM\MM"
        repoOrderQty.Name = colCM
        repoOrderQty.Width = 100
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoOrderQty.ReadOnly = True
        repoOrderQty.Minimum = 0
        repoOrderQty.DecimalPlaces = 0
        repoOrderQty.ShowUpDownButtons = False
        gv.MasterTemplate.Columns.Add(repoOrderQty)

        For ii As Integer = 0 To 9
            repoOrderQty = New GridViewDecimalColumn()
            repoOrderQty.FormatString = ""
            repoOrderQty.HeaderText = clsCommon.myCstr(ii)
            repoOrderQty.Name = colMM + clsCommon.myCstr(ii)
            repoOrderQty.Width = 100
            repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            repoOrderQty.ReadOnly = True
            repoOrderQty.Minimum = 0
            repoOrderQty.DecimalPlaces = 0
            repoOrderQty.ShowUpDownButtons = False
            gv.MasterTemplate.Columns.Add(repoOrderQty)
        Next


        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 30
        gv.EnableSorting = False
    End Sub
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            Dim qry As String = "select '0' as [CM/MM],'14' as '0','15' as '1','16' as '2','17' as '3','18' as '4','19' as '5','20' as '6','21' as '7','22' as '8','23' as '9'
union all 
select '1' as [CM/MM],'24' as '0','24' as '1','26' as '2','27' as '3','28' as '4','29' as '5','30' as '6','31' as '7','32' as '8','33' as '9'"
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim gvTemp As New RadGridView()
        Me.Controls.Add(gvTemp)
        Try
            UcAttachment1.BlankAllControls()
            LoadBlankGrid()
            Dim FileName As String = ""
            Dim SafeFileName As String = ""
            If transportSql.importExcel(FileName, SafeFileName, gvTemp, "CM/MM", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9") Then
                For Each grow As GridViewRowInfo In gvTemp.Rows
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells(colCM).Value = clsCommon.myCDecimal(grow.Cells("CM/MM").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM0).Value = clsCommon.myCDecimal(grow.Cells("0").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM1).Value = clsCommon.myCDecimal(grow.Cells("1").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM2).Value = clsCommon.myCDecimal(grow.Cells("2").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM3).Value = clsCommon.myCDecimal(grow.Cells("3").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM4).Value = clsCommon.myCDecimal(grow.Cells("4").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM5).Value = clsCommon.myCDecimal(grow.Cells("5").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM6).Value = clsCommon.myCDecimal(grow.Cells("6").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM7).Value = clsCommon.myCDecimal(grow.Cells("7").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM8).Value = clsCommon.myCDecimal(grow.Cells("8").Value)
                    gv.Rows(gv.Rows.Count - 1).Cells(colMM9).Value = clsCommon.myCDecimal(grow.Cells("9").Value)
                Next
                UcAttachment1.AddAttachment(FileName, SafeFileName)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gvTemp)
        End Try
    End Sub
End Class


