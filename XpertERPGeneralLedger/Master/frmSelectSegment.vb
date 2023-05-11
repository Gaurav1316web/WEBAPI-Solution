Imports common
Public Class FrmSelectSegment
    Public arr As ArrayList = Nothing
    Public strSegment As String = Nothing
    Public strSegmentCode As String = Nothing
    Public isCencelButtonClicked As Boolean = False

    Private Sub FrmSelectSegment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       

        Dim qry As String = "SELECT Segment_code as Code,Description from tspl_gl_segment_code where Segment_name='" + strSegment + "'"
        If clsCommon.myLen(strSegmentCode) > 0 Then
            qry += " and Segment_code not in (" & strSegmentCode & ")"
        End If
        cbg1.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbg1.ValueMember = "Code"
        cbg1.DisplayMember = "Description"

        cbg1.CheckedValue = arr
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        arr = New ArrayList()
        arr = cbg1.CheckedValue
        Me.Close()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        isCencelButtonClicked = True
        Me.Close()
    End Sub
End Class
