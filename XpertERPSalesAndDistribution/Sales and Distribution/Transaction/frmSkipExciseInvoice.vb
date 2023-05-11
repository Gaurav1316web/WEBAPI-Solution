Imports common
Imports System.Data.SqlClient

Public Class FrmSkipExciseInvoice
#Region "Varibales"
    Public strDocType As String = ""
    Public strDocTransType As String = ""
#End Region

    Private Sub FrmSkipExciseInvoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = "Skip " + strDocType + " " + strDocTransType
        If clsCommon.myLen(strDocType) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Docutment Type not found", Me.Text)
            Me.Close()
        End If

        lblNextCode.Text = ""
        lblNextCodeAfterSkip.Text = ""
        txtNoOfInvoiceToSkip.Value = 0
        lblLocationName.Text = ""
    End Sub

    Private Sub txtLocationSegment__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as [Description] from tspl_location_Master "
            Dim whrClas As String = "Excisable='T'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrClas += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("SkipLocSeg", qry, "Code", whrClas, txtLocation.Value, "Code", isButtonClicked))
            lblLocationName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from tspl_location_Master where Location_Code ='" + txtLocation.Value + "'"))
            If clsCommon.myLen(txtLocation.Value) > 0 Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                lblNextCode.Text = clsERPFuncationality.GetNextCode(trans, clsCommon.GETSERVERDATE(trans), strDocType, strDocTransType, txtLocation.Value)
                trans.Rollback()
                If txtNoOfInvoiceToSkip.Value > 0 Then
                    GetNewCode()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtNoOfInvoiceToSkip_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNoOfInvoiceToSkip.Validating
        GetNewCode()
    End Sub

    Private Sub GetNewCode()
        If clsCommon.myLen(lblNextCode) > 0 Then
            lblNextCodeAfterSkip.Text = clsCommon.incval(lblNextCode.Text, 0, txtNoOfInvoiceToSkip.Value)
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If AllowToSave() Then
            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            Try
                Dim qry As String = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + txtLocation.Value + "'"
                Dim strLocatinSegmentCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                qry = "Update TSPL_DOCPREFIX_MASTER set Next_Number=Next_Number+" + clsCommon.myCstr(txtNoOfInvoiceToSkip.Value) + ""
                qry += " where Doc_Type='" + strDocType + "' and isnull(Doc_Trans_Type,'')='" + strDocTransType + "' and isnull(Location_Code,'')='" + strLocatinSegmentCode + "' "
                clsDBFuncationality.ExecuteNonQuery(qry, trans)
                trans.Commit()
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                Me.Close()
            Catch ex As Exception
                trans.Rollback()
                common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
            End Try
        End If
    End Sub

    Private Function AllowToSave() As Boolean
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please select Location", Me.Text)
            txtLocation.Focus()
            Return False
        ElseIf txtNoOfInvoiceToSkip.Value <= 0 Then
            common.clsCommon.MyMessageBoxShow("Please Enter Number Of Invoices to skip", Me.Text)
            txtNoOfInvoiceToSkip.Focus()
            Return False
        ElseIf common.clsCommon.MyMessageBoxShow("Skip " + clsCommon.myCstr(txtNoOfInvoiceToSkip.Value) + " Documents" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Return False
        End If
        Return True
    End Function
End Class
