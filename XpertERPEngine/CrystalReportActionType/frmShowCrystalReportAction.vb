
Public Class frmShowCrystalReportAction
#Region "Variables"
    Public Const colActionType As String = "colActionType"
    Public Report_ID As String = Nothing
    Public colCrystalReportName As String = "colCrystalReportName"
#End Region
    Private Sub frmShowCrystalReportAction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = clsDBFuncationality.getSingleValue("select case When LEN(isnull(Re_Name,''))>0 then Re_Name else Program_Name end as Name from tspl_Program_Master where Program_Code= '" + Report_ID + "'")
        LoadBlankGrid()
        LoadData()
    End Sub
    Private Sub frmShowCrystalReportAction_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F5 Then
            SaveData()
        ElseIf e.KeyCode = Keys.Escape Then
            CloseForm()
        End If
    End Sub

    Sub LoadBlankGrid()
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Dim repoCrystalReportName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCrystalReportName.FormatString = ""
        repoCrystalReportName.HeaderText = "Crystal Report Name"
        repoCrystalReportName.Width = 175
        repoCrystalReportName.Name = colCrystalReportName
        repoCrystalReportName.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(repoCrystalReportName)

        Dim repoActionType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoActionType.FormatString = ""
        repoActionType.HeaderText = "Action Type"
        repoActionType.Width = 100
        repoActionType.Name = colActionType
        repoActionType.ReadOnly = False
        repoActionType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        repoActionType.DataSource = GetActionType()
        repoActionType.ValueMember = "Code"
        repoActionType.DisplayMember = "Name"
        Gv1.MasterTemplate.Columns.Add(repoActionType)

    End Sub
    Private Function GetActionType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = 0
        dr("Name") = "View"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = 1
        dr("Name") = "PDF"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Sub LoadData()
        Try
            Dim qry As String = " select Form_ID, Report_Name, Action_Type from TSPL_CRYSTAL_REPORT_ACTION where FORM_ID = '" + Report_ID + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Gv1.Rows.AddNew()
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCrystalReportName).Value = clsCommon.myCstr(dr("Report_Name"))
                    Gv1.Rows(Gv1.Rows.Count - 1).Cells(colActionType).Value = clsCommon.myCdbl(dr("Action_Type"))
                Next
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Try
            If SaveData() Then
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                CloseForm()
            End If
        Catch err As Exception
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Public Function SaveData() As Boolean
        Dim isSaved As Boolean
        Dim obj As New clsCrystalReportActionType()
        obj.Arr = New List(Of clsCrystalReportActionType)()
            For Each grow As GridViewRowInfo In Gv1.Rows
                Dim objTr As New clsCrystalReportActionType()
                objTr.Form_ID = Report_ID
            objTr.Report_Name = clsCommon.myCstr(grow.Cells(colCrystalReportName).Value)
            objTr.Action_Type = clsCommon.myCstr(grow.Cells(colActionType).Value)
            obj.Arr.Add(objTr)
            Next
        isSaved = obj.SaveData(obj, False)
        Return isSaved
    End Function
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
End Class