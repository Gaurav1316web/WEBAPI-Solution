Imports common
Imports System.Data.SqlClient

Public Class frmPostAllGLToTally
    Inherits FrmMainTranScreen
    Private Sub FrmChangePrntOrdr_ACGroup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        dtfrom.Value = clsCommon.GETSERVERDATE()
        dtTo.Value = clsCommon.GETSERVERDATE()
    End Sub


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.mbtnGRN)
        If Not (MyBase.isReadFlag) AndAlso (Not objCommonVar.IsSendToTally) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        PostToTally()
    End Sub

    Sub PostToTally()
        Try
            Dim objSendToTally As New clsSendToTally()
            Dim DT As DataTable = dgvAccountMap.DataSource
            Dim CountSend As Int16 = 0
            Dim CountSelected As Int16 = 0

            If DT Is Nothing Or DT.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found.", Me.Text)
                'lblRecordNo.Text = "Total Record Found : 0 "
            End If
            clsCommon.ProgressBarShow()
            For Each dr As DataRow In DT.Rows
                If clsCommon.myLen(dr("Voucher No")) > 0 AndAlso clsCommon.myCBool(dr("Select")) Then
                    CountSelected = CountSelected + 1
                    If objSendToTally.SendToTally_JournalEntry_BulkPost(clsCommon.myCstr(dr("Voucher No"))) Then
                        CountSend = CountSend + 1
                    End If
                End If
            Next
            clsCommon.ProgressBarHide()
            If CountSend >= 0 AndAlso CountSelected > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Total " + CountSend.ToString + " record send to Tally out of " + CountSelected.ToString + ".")
            ElseIf CountSelected <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Record Selected for Send To Tally.", Me.Text)
            End If
            Show_Records()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub BtnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnShow.Click
        Show_Records()
    End Sub
    Sub Show_Records()
        Dim strQry As String = " "

        strQry = " SELECT cast(0 as bit) AS [Select], journal_no as [Journal No], voucher_no as [Voucher No], CONVERT(VARCHAR,voucher_date,103) as [Voucher Date],(CASE WHEN  ISNULL (TSPL_GL_SOURCECODE.TallyName,'') <>'' THEN TSPL_GL_SOURCECODE.TallyName ELSE 'Journal' END) AS [Source], source_doc_no as [Source Doc No], " & _
                 " CONVERT(VARCHAR,Source_Doc_date,103) as [Source Doc Date],CONVERT(VARCHAR,Posting_Date,103) as [Posting Date],Voucher_Desc as [Voucher Desc.],SendToTally AS [Send To Tally] " & _
                 " FROM TSPL_JOURNAL_MASTER " & _
                 " INNER JOIN TSPL_GL_SOURCECODE ON TSPL_GL_SOURCECODE.SourceCode = TSPL_JOURNAL_MASTER.Source_Code  " & _
                 " WHERE (Voucher_Date >=  convert(datetime,'" + dtfrom.Value + "',103)) AND  (Voucher_Date <=  convert(datetime,'" + dtTo.Value + "',103)) "
        'If chkShowAll.Checked = False Then
        strQry += " AND SendTOTally = 0"
        'End If

        Dim DT As DataTable = clsDBFuncationality.GetDataTable(strQry)
        If DT Is Nothing Or DT.Rows.Count <= 0 Then
            dgvAccountMap.DataSource = Nothing
            clsCommon.MyMessageBoxShow(Me, "No Data Found.", Me.Text)
            lblRecordNo.Text = "Total Record Found : 0 "
        Else
            dgvAccountMap.DataSource = Nothing
            dgvAccountMap.DataSource = DT
            dgvAccountMap.BestFitColumns()
            lblRecordNo.Text = "Total Record Found : " + DT.Rows.Count.ToString()
        End If

    End Sub

    Private Sub btnUNSELECT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUNSELECT.Click
        Dim dt As DataTable = dgvAccountMap.DataSource()
        If dt.Rows.Count > 0 Then
            For Each dr As GridViewRowInfo In dgvAccountMap.Rows
                dr.Cells("select").Value = False
            Next
        End If
    End Sub

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Dim dt As DataTable = dgvAccountMap.DataSource
        If dt.Rows.Count > 0 Then
            For Each dr As GridViewRowInfo In dgvAccountMap.Rows
                dr.Cells("select").Value = True
            Next
        End If
    End Sub
End Class
