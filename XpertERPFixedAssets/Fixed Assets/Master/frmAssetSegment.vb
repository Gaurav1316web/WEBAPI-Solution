'--23/01/2013-09:00AM--Created By-Pankaj Kumar
'---------------------------------------------Table Used--[TSPL_ASSET_SEGMENT]
'---------------------------------------------Class Used--[clsAssetSegment]
Imports common
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class FrmAssetSegment
    Inherits FrmMainTranScreen
    Dim Qry As String = ""
    Dim dt As DataTable
    Dim IsLoaddata As Boolean = False
    Const colSegNo As String = "SegmentNo"
    Const colLength As String = "Length"
    Const colLinkTo As String = "LinkTo"

    Private Sub FrmAssetSegment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        SetMaxLength()
        LoadData()
        SetUserMgmtNew()
    End Sub

    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        ''MyBase.SetUserMgmt(clsUserMgtCode.AssetSegment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub Reset()
        txtNoOfSegment.Text = ""
        txtIdSegment.Text = ""
        LoadSegmentSeperator()
        LoadBlankGrid()
    End Sub
    Private Sub SetMaxLength()
        txtNoOfSegment.MaxLength = 1
        txtIdSegment.MaxLength = 50
    End Sub

    Sub LoadSegmentSeperator()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "/"
        dr("Name") = "/"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "-"
        dr("Name") = "-"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "."
        dr("Name") = "."
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = " "
        dr("Name") = "Blank Space"
        dt.Rows.Add(dr)

        cmbSegmentSeperator.DataSource = dt
        cmbSegmentSeperator.ValueMember = "Code"
        cmbSegmentSeperator.DisplayMember = "Name"
    End Sub

    Sub LoadBlankGrid()
        dgvAssetSegment.Rows.Clear()
        dgvAssetSegment.Columns.Clear()

        Dim segNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        segNo.FormatString = ""
        segNo.HeaderText = "Segment No"
        segNo.Name = colSegNo
        segNo.Width = 151
        segNo.ReadOnly = True
        segNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvAssetSegment.MasterTemplate.Columns.Add(segNo)

        Dim Length As GridViewDecimalColumn = New GridViewDecimalColumn()
        Length.FormatString = ""
        Length.HeaderText = "Length"
        Length.Name = colLength
        Length.Width = 151
        Length.ReadOnly = False
        Length.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvAssetSegment.MasterTemplate.Columns.Add(Length)

        Dim linkTo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        linkTo.FormatString = ""
        linkTo.HeaderText = "Link To"
        linkTo.Name = colLinkTo
        linkTo.Width = 501
        linkTo.MaxLength = 50
        linkTo.ReadOnly = False
        linkTo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        dgvAssetSegment.MasterTemplate.Columns.Add(linkTo)

        dgvAssetSegment.AllowDeleteRow = True
        dgvAssetSegment.AllowAddNewRow = False
        dgvAssetSegment.ShowGroupPanel = False
        dgvAssetSegment.AllowColumnReorder = False
        dgvAssetSegment.AllowRowReorder = False
        dgvAssetSegment.EnableSorting = False
        dgvAssetSegment.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvAssetSegment.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If AllowToSave() Then
                Dim Arr As New List(Of clsAssetSegment)
                For Each grow As GridViewRowInfo In dgvAssetSegment.Rows
                    Dim objTr As New clsAssetSegment()
                    objTr.No_Of_Segment = clsCommon.myCdbl(txtNoOfSegment.Text)
                    objTr.Id_Segment = clsCommon.myCstr(txtIdSegment.Text)
                    objTr.Segment_Seperator = clsCommon.myCstr(cmbSegmentSeperator.SelectedValue)
                    objTr.Segment_No = clsCommon.myCstr(grow.Cells(colSegNo).Value)
                    objTr.Length = clsCommon.myCstr(grow.Cells(colLength).Value)
                    objTr.Link_To = clsCommon.myCstr(grow.Cells(colLinkTo).Value)
                    Arr.Add(objTr)
                Next

                If (clsAssetSegment.SaveData(Arr)) Then
                    RadMessageBox.Show("Data Saved Successfully")
                    LoadData()
                End If
            End If
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        If clsCommon.myCdbl(txtNoOfSegment.Text) <= 0 Then
            RadMessageBox.Show("Please Insert No Of Segment Between 1 T0 9")
            txtNoOfSegment.Focus()
            Return False
        ElseIf clsCommon.myLen(txtIdSegment.Text) <= 0 Then
            RadMessageBox.Show("Please Insert Id Segment")
            txtIdSegment.Focus()
            Return False
        ElseIf dgvAssetSegment.Rows.Count > 0 Then
            For i As Integer = 0 To dgvAssetSegment.Rows.Count - 1
                If clsCommon.myCstr(dgvAssetSegment.Rows(i).Cells(colLength).Value) = "" Or dgvAssetSegment.Rows(i).Cells(colLength).Value <= 0 Then
                    RadMessageBox.Show("Please Insert Length (More Than 0) Against Segment No " + clsCommon.myCstr(i + 1) + "")
                    Return False
                ElseIf clsCommon.myCstr(dgvAssetSegment.Rows(i).Cells(colLinkTo).Value) = "" Then
                    RadMessageBox.Show("Please Insert 'Link To' Against Segment No " + clsCommon.myCstr(i + 1) + "")
                    Return False
                End If
            Next
        End If
        Return True
    End Function

    Private Sub LoadData()
        Try
            LoadBlankGrid()
            Dim ArrSeg As New List(Of clsAssetSegment)
            ArrSeg = clsAssetSegment.GetData()
            If ArrSeg.Count > 0 Then
                IsLoaddata = True
                Dim I As Integer = 0
                For Each objTr As clsAssetSegment In ArrSeg
                    dgvAssetSegment.Rows.AddNew()
                    I = I + 1
                    If I = 1 Then
                        txtNoOfSegment.Text = clsCommon.myCstr(objTr.No_Of_Segment)
                        txtIdSegment.Text = clsCommon.myCstr(objTr.Id_Segment)
                        cmbSegmentSeperator.SelectedValue = clsCommon.myCstr(objTr.Segment_Seperator)
                    End If
                    dgvAssetSegment.Rows(dgvAssetSegment.Rows.Count - 1).Cells(colSegNo).Value = objTr.Segment_No
                    dgvAssetSegment.Rows(dgvAssetSegment.Rows.Count - 1).Cells(colLength).Value = objTr.Length
                    dgvAssetSegment.Rows(dgvAssetSegment.Rows.Count - 1).Cells(colLinkTo).Value = objTr.Link_To
                Next
                btnSave.Enabled = False
                txtNoOfSegment.Enabled = False
                txtIdSegment.Enabled = False
                cmbSegmentSeperator.Enabled = False
                dgvAssetSegment.ReadOnly = True
            Else
                btnSave.Enabled = True
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub txtNoOfSegment_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNoOfSegment.Leave
        If IsLoaddata = False Then
            LoadBlankGrid()
            For i As Integer = 0 To clsCommon.myCdbl(txtNoOfSegment.Text) - 1
                dgvAssetSegment.Rows.AddNew()
                dgvAssetSegment.Rows(i).Cells(colSegNo).Value = i + 1
                dgvAssetSegment.Rows(i).Cells(colLength).Value = Nothing
                dgvAssetSegment.Rows(i).Cells(colLinkTo).Value = ""
            Next
        End If
        If dgvAssetSegment.Rows.Count > 0 Then
            dgvAssetSegment.CurrentRow = dgvAssetSegment.Rows(0)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
