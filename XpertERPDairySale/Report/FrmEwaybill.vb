Imports common

Public Class FrmEwaybill
    Inherits FrmMainTranScreen
#Region "Variables"

#End Region
    Private Sub FrmEwaybill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadEwaybillType()
    End Sub
    Sub LoadEwaybillType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "--- Select Eway Bill Types ----"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Eway Bill Details"
        dr("Name") = "Eway Bill Details"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Update Transpoter"
        dr("Name") = "Update Transpoter"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Update PART-B/Vehicle Number"
        dr("Name") = "Update PART-B/Vehicle Number"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Cancel Eway Bill"
        dr("Name") = "Cancel Eway Bill"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Eway Bills By Date"
        dr("Name") = "Eway Bills By Date"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Extend Validity of Eway Bill"
        dr("Name") = "Extend Validity of Eway Bill"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Error List"
        dr("Name") = "Error List"
        dt.Rows.Add(dr)



        cmbEwaybillType.DataSource = dt
        cmbEwaybillType.ValueMember = "Code"
        cmbEwaybillType.DisplayMember = "Name"
    End Sub

    Private Sub btnreset_Click(sender As Object, e As EventArgs) Handles btnreset.Click

    End Sub
    Private Sub Reset()
        cmbEwaybillType.SelectedValue = ""
        lblewbno.Visible = False
        txtewbno.Visible = False
        txtewbno.Text = ""
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click

    End Sub

    Private Sub cmbEwaybillType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbEwaybillType.SelectedIndexChanged
        Try
            If clsCommon.CompairString(cmbEwaybillType.SelectedValue, "") = CompairStringResult.Equal Then
                Throw New Exception("Please Select Eway Bill Type")
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Eway Bill Details") = CompairStringResult.Equal Then
                lblewbno.Visible = True
                txtewbno.Visible = True
                If clsCommon.myLen(txtewbno.Text) > 0 Then

                End If
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Update Transpoter") = CompairStringResult.Equal Then
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Update PART-B/Vehicle Number") = CompairStringResult.Equal Then
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Cancel Eway Bill") = CompairStringResult.Equal Then
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Eway Bills By Date") = CompairStringResult.Equal Then
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Extend Validity of Eway Bill") = CompairStringResult.Equal Then
            ElseIf clsCommon.CompairString(cmbEwaybillType.SelectedValue, "Error List") = CompairStringResult.Equal Then

            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class