Imports common
Imports System.Data.SqlClient
Public Class frmDocumentSequence
    Dim objDocSeq As New clsDocumentSequence
    Private Sub frmDocumentSequence_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''Against ticket BM00000007766
        If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal Then
            SplitContainer1.Panel1Collapsed = True
        End If
    End Sub

    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Me.Close()
    End Sub

    ''Bulk Procurement Gate Entery
    Private Sub txtBulkProcSeq__My_Click(sender As Object, e As EventArgs) Handles txtBulkProcSeq._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Gate_Entry_No,0,len(Gate_Entry_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from Tspl_Gate_Entry_Details   )xx group by DocNo order by DocNo"
            txtBulkProcSeq.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcSequti", qry, "DocNo", "", txtBulkProcSeq.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton47_Click(sender As Object, e As EventArgs) Handles RadButton47.Click
        If txtBulkProcSeq.arrValueMember Is Nothing OrElse txtBulkProcSeq.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkProcSeq.arrValueMember IsNot Nothing AndAlso txtBulkProcSeq.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkProcSeq.arrValueMember
                    Dim qry As String = "select Gate_Entry_No from Tspl_Gate_Entry_Details where gate_entry_no like '" + StrSeq + "%' order by Date_And_Time"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Gate_Entry_No from Tspl_Gate_Entry_Details where gate_entry_no like '" + StrSeq + "%' order by Gate_Entry_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Gate_Entry_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Gate_Entry_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkGateEntry(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkGateEntry(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkGateEntry','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''Bulk Procurement Gate Entery End here


    ''Bulk Procurement Weighment
    Private Sub txtWeighmentSeqGrp__My_Click(sender As Object, e As EventArgs) Handles txtWeighmentSeqGrp._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Weighment_No,0,len(Weighment_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_Weighment_Detail   )xx group by DocNo order by DocNo"
            txtWeighmentSeqGrp.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcWeSe", qry, "DocNo", "", txtWeighmentSeqGrp.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton44_Click(sender As Object, e As EventArgs) Handles RadButton44.Click
        If txtWeighmentSeqGrp.arrValueMember Is Nothing OrElse txtWeighmentSeqGrp.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtWeighmentSeqGrp.arrValueMember IsNot Nothing AndAlso txtWeighmentSeqGrp.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtWeighmentSeqGrp.arrValueMember
                    Dim qry As String = "select Weighment_No from TSPL_Weighment_Detail where Weighment_No like '" + StrSeq + "%' order by Weighment_date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Weighment_No from TSPL_Weighment_Detail where Weighment_No like '" + StrSeq + "%' order by Weighment_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Weighment_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Weighment_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkWeighment(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkWeighment(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryWeighment','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''Bulk Procurement Weighment End here

    ''Bulk Procurement QC 
    Private Sub txtBulkProcQC__My_Click(sender As Object, e As EventArgs) Handles txtBulkProcQC._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( QC_No,0,len(QC_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_QUALITY_CHECK   )xx group by DocNo order by DocNo"
            txtBulkProcQC.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcQCSequti", qry, "DocNo", "", txtBulkProcQC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton46_Click(sender As Object, e As EventArgs) Handles RadButton46.Click
        If txtBulkProcQC.arrValueMember Is Nothing OrElse txtBulkProcQC.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkProcQC.arrValueMember IsNot Nothing AndAlso txtBulkProcQC.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkProcQC.arrValueMember
                    Dim qry As String = "select QC_No from TSPL_QUALITY_CHECK where QC_No like '" + StrSeq + "%' order by QC_In_Date_Time"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select QC_No from TSPL_QUALITY_CHECK where QC_No like '" + StrSeq + "%' order by QC_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("QC_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("QC_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkQC(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkQC(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryQC','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''Bulk Procurement QC End here

    ''Bulk SRN
    Private Sub txtBulkSRNSequence__My_Click(sender As Object, e As EventArgs) Handles txtBulkSRNSequence._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( SRN_NO,0,len(SRN_NO)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_Bulk_MILK_SRN)xx group by DocNo order by DocNo"
            txtBulkSRNSequence.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcSRNSequti", qry, "DocNo", "", txtBulkSRNSequence.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton48_Click(sender As Object, e As EventArgs) Handles RadButton48.Click
        If txtBulkSRNSequence.arrValueMember Is Nothing OrElse txtBulkSRNSequence.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkSRNSequence.arrValueMember IsNot Nothing AndAlso txtBulkSRNSequence.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkSRNSequence.arrValueMember
                    Dim qry As String = "select SRN_NO from TSPL_Bulk_MILK_SRN where SRN_NO like '" + StrSeq + "%' order by SRN_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select SRN_NO from TSPL_Bulk_MILK_SRN where SRN_NO like '" + StrSeq + "%' order by SRN_NO"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("SRN_NO"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("SRN_NO"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSRN(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSRN(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntrySRN','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''Bulk SRN End here

    ''Bulk PI  
    Private Sub txtBulkPI__My_Click(sender As Object, e As EventArgs) Handles txtBulkPI._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( DOC_NO,0,len(DOC_NO)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from tspl_Bulk_milk_purchase_Invoice_head)xx group by DocNo order by DocNo"
            txtBulkPI.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcPISequti", qry, "DocNo", "", txtBulkPI.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton49_Click(sender As Object, e As EventArgs) Handles RadButton49.Click
        If txtBulkPI.arrValueMember Is Nothing OrElse txtBulkPI.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkPI.arrValueMember IsNot Nothing AndAlso txtBulkPI.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkPI.arrValueMember
                    Dim qry As String = "select DOC_NO from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO like '" + StrSeq + "%' order by DOC_DATE"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select DOC_NO from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO like '" + StrSeq + "%' order by DOC_NO"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("DOC_NO"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("DOC_NO"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkPI(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkPI(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryPI','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''Bulk PI End here

    ''Bulk SAale Entry 
    Private Sub txtBulkSaleGateEntry__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleGateEntry._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_GATEENTRY_SALE)xx group by DocNo order by DocNo"
            txtBulkSaleGateEntry.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkSaleGateEntry", qry, "DocNo", "", txtBulkSaleGateEntry.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton54_Click(sender As Object, e As EventArgs) Handles RadButton54.Click
        If txtBulkSaleGateEntry.arrValueMember Is Nothing OrElse txtBulkSaleGateEntry.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkSaleGateEntry.arrValueMember IsNot Nothing AndAlso txtBulkSaleGateEntry.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkSaleGateEntry.arrValueMember
                    Dim qry As String = "select Document_No from TSPL_GATEENTRY_SALE where Document_No like '" + StrSeq + "%' order by Document_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Document_No from TSPL_GATEENTRY_SALE where Document_No like '" + StrSeq + "%' order by Document_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Document_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Document_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleEntry(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleEntry(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleEntry','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''End of Bulk SAale Entry


    ''Bulk Sale Weighment
    Private Sub TxtMultiSelectFinder4__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleWeighment._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( weighment_No,0,len(weighment_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_WEIGHMENT_DETAIL_BULKSALE)xx group by DocNo order by DocNo"
            txtBulkSaleWeighment.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiSelectFinder4", qry, "DocNo", "", txtBulkSaleWeighment.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton53_Click(sender As Object, e As EventArgs) Handles RadButton53.Click
        If txtBulkSaleWeighment.arrValueMember Is Nothing OrElse txtBulkSaleWeighment.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkSaleWeighment.arrValueMember IsNot Nothing AndAlso txtBulkSaleWeighment.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkSaleWeighment.arrValueMember
                    Dim qry As String = "select weighment_No from TSPL_WEIGHMENT_DETAIL_BULKSALE where weighment_No like '" + StrSeq + "%' order by Weighment_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select weighment_No from TSPL_WEIGHMENT_DETAIL_BULKSALE where weighment_No like '" + StrSeq + "%' order by weighment_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("weighment_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("weighment_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleWeighment(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleWeighment(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleWeighment','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''End of Bulk Sale Weighment

    ''Bulk Sale Loading
    Private Sub TxtMultiSelectFinder3__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleLoading._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( LoadingTanker_No,0,len(LoadingTanker_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_LOADING_TANKER_DETAIL_BULKSALE)xx group by DocNo order by DocNo"
            txtBulkSaleLoading.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkSaleLoading", qry, "DocNo", "", txtBulkSaleLoading.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBulkSaleLoading__My_Click(sender As Object, e As EventArgs) Handles RadButton55.Click
        If txtBulkSaleLoading.arrValueMember Is Nothing OrElse txtBulkSaleLoading.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkSaleLoading.arrValueMember IsNot Nothing AndAlso txtBulkSaleLoading.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkSaleLoading.arrValueMember
                    Dim qry As String = "select LoadingTanker_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No like '" + StrSeq + "%' order by LoadingTanker_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select LoadingTanker_No from TSPL_LOADING_TANKER_DETAIL_BULKSALE where LoadingTanker_No like '" + StrSeq + "%' order by LoadingTanker_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("LoadingTanker_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("LoadingTanker_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleLoading(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleLoading(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleLoading','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''End of Bulk Sale Loading




    ''Bulk Sale QC
    Private Sub txtBulkSaleQC__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleQC._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( QC_No,0,len(QC_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_Quality_Check_BulkSale)xx group by DocNo order by DocNo"
            txtBulkSaleQC.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiSelectFinder4", qry, "DocNo", "", txtBulkSaleQC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton52_Click(sender As Object, e As EventArgs) Handles RadButton52.Click
        If txtBulkSaleQC.arrValueMember Is Nothing OrElse txtBulkSaleQC.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkSaleQC.arrValueMember IsNot Nothing AndAlso txtBulkSaleQC.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkSaleQC.arrValueMember
                    Dim qry As String = "select QC_No from TSPL_Quality_Check_BulkSale where QC_No like '" + StrSeq + "%' order by QC_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select QC_No from TSPL_Quality_Check_BulkSale where QC_No like '" + StrSeq + "%' order by QC_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("QC_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("QC_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleQC(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleQC(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleQC','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''End of Bulk Sale QC

    ''Bulk Sale Dispatch
    Private Sub txtBSDispatch__My_Click(sender As Object, e As EventArgs) Handles txtBSDispatch._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_Dispatch_BulkSale)xx group by DocNo order by DocNo"
            txtBSDispatch.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBSDispatch", qry, "DocNo", "", txtBSDispatch.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton51_Click(sender As Object, e As EventArgs) Handles RadButton51.Click
        If txtBSDispatch.arrValueMember Is Nothing OrElse txtBSDispatch.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBSDispatch.arrValueMember IsNot Nothing AndAlso txtBSDispatch.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBSDispatch.arrValueMember
                    Dim qry As String = "select Document_No from TSPL_Dispatch_BulkSale where Document_No like '" + StrSeq + "%' order by Document_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Document_No from TSPL_Dispatch_BulkSale where Document_No like '" + StrSeq + "%' order by Document_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Document_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Document_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleDispatch(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleDispatch(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleDispatch','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''End of Bulk Sale Dispatch

    ''  Bulk Sale Invoice 
    Private Sub txtBulkSaleInvoice__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleInvoice._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_INVOICE_MASTER_BULKSALE)xx group by DocNo order by DocNo"
            txtBulkSaleInvoice.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkSaleInvoice", qry, "DocNo", "", txtBulkSaleInvoice.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton50_Click(sender As Object, e As EventArgs) Handles RadButton50.Click
        If txtBulkSaleInvoice.arrValueMember Is Nothing OrElse txtBulkSaleInvoice.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkSaleInvoice.arrValueMember IsNot Nothing AndAlso txtBulkSaleInvoice.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkSaleInvoice.arrValueMember
                    Dim qry As String = "select Document_No from TSPL_INVOICE_MASTER_BULKSALE where Document_No like '" + StrSeq + "%' order by Document_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Document_No from TSPL_INVOICE_MASTER_BULKSALE where Document_No like '" + StrSeq + "%' order by Document_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Document_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Document_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleInvoice(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkSaleInvoice(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleInvoice','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''End of Bulk Sale Invoice 

    ''Bulk Sale Gateout 
    Private Sub txtBulkSaleGateOut__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleGateOut._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_GATEOUT_SALE)xx group by DocNo order by DocNo"
            txtBulkSaleGateOut.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkSaleGateOut", qry, "DocNo", "", txtBulkSaleGateOut.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton56_Click(sender As Object, e As EventArgs) Handles RadButton56.Click
        If txtBulkSaleGateOut.arrValueMember Is Nothing OrElse txtBulkSaleGateOut.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtBulkSaleGateOut.arrValueMember IsNot Nothing AndAlso txtBulkSaleGateOut.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtBulkSaleGateOut.arrValueMember
                    Dim qry As String = "select Document_No from TSPL_GATEOUT_SALE where Document_No like '" + StrSeq + "%' order by Document_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Document_No from TSPL_GATEOUT_SALE where Document_No like '" + StrSeq + "%' order by Document_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Document_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Document_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkGateOut(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkGateOut(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkGateOut','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub

    ''End of Bulk Sale Gateout 





    ''AR 
    Private Sub txtAR__My_Click(sender As Object, e As EventArgs) Handles txtAR._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_Customer_Invoice_Head)xx group by DocNo order by DocNo"
            txtAR.arrValueMember = clsCommon.ShowMultipleSelectForm("txtAR", qry, "DocNo", "", txtAR.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        If txtAR.arrValueMember Is Nothing OrElse txtAR.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtAR.arrValueMember IsNot Nothing AndAlso txtAR.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtAR.arrValueMember
                    Dim qry As String = "select Document_No from TSPL_Customer_Invoice_Head where Document_No like '" + StrSeq + "%' order by Document_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Document_No from TSPL_Customer_Invoice_Head where Document_No like '" + StrSeq + "%' order by Document_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Document_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Document_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateARInvoice(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateARInvoice(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'ARInvoice','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''End of AR


    ''AP
    Private Sub txtAP__My_Click(sender As Object, e As EventArgs) Handles txtAP._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_VENDOR_INVOICE_HEAD)xx group by DocNo order by DocNo"
            txtAP.arrValueMember = clsCommon.ShowMultipleSelectForm("txtAP", qry, "DocNo", "", txtAP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        If txtAP.arrValueMember Is Nothing OrElse txtAP.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtAP.arrValueMember IsNot Nothing AndAlso txtAP.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtAP.arrValueMember
                    Dim qry As String = "select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_No like '" + StrSeq + "%' order by Posting_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Document_No from TSPL_VENDOR_INVOICE_HEAD where Document_No like '" + StrSeq + "%' order by Document_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Document_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Document_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateAPInvoice(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateAPInvoice(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'APInvoice','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''End of AP

    ''Journal Entry
    Private Sub txtJournalEntry__My_Click(sender As Object, e As EventArgs) Handles txtJournalEntry._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Voucher_No,0,len(Voucher_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_JOURNAL_MASTER)xx group by DocNo order by DocNo"
            txtJournalEntry.arrValueMember = clsCommon.ShowMultipleSelectForm("txtJournalEntry", qry, "DocNo", "", txtJournalEntry.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        If txtJournalEntry.arrValueMember Is Nothing OrElse txtJournalEntry.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtJournalEntry.arrValueMember IsNot Nothing AndAlso txtJournalEntry.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtJournalEntry.arrValueMember
                    Dim qry As String = "select Voucher_No from TSPL_JOURNAL_MASTER where Voucher_No like '" + StrSeq + "%' order by Voucher_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Voucher_No from TSPL_JOURNAL_MASTER where Voucher_No like '" + StrSeq + "%' order by Voucher_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Voucher_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Voucher_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateJournalEntry(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateJournalEntry(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'JournalEntry','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    ''End of Journal Entry
    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        XpertERPEngine.clsCreateAllTables.CreateAllTable()
    End Sub

    Private Sub txtGateout__My_Click(sender As Object, e As EventArgs) Handles txtGateout._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Doc_No,0,len(Doc_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSpl_Gate_Out)xx group by DocNo order by DocNo"
            txtGateout.arrValueMember = clsCommon.ShowMultipleSelectForm("txtGateout", qry, "DocNo", "", txtGateout.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        If txtGateout.arrValueMember Is Nothing OrElse txtGateout.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtGateout.arrValueMember IsNot Nothing AndAlso txtGateout.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtGateout.arrValueMember
                    Dim qry As String = "select Doc_No from TSpl_Gate_Out where Doc_No like '" + StrSeq + "%' order by Doc_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Doc_No from TSpl_Gate_Out where Doc_No like '" + StrSeq + "%' order by Doc_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Doc_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Doc_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkPurchaseGateout(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkPurchaseGateout(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryGateOut','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtUnloading__My_Click(sender As Object, e As EventArgs) Handles txtUnloading._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Unloading_No,0,len(Unloading_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_MILK_UNLOADING)xx group by DocNo order by DocNo"
            txtUnloading.arrValueMember = clsCommon.ShowMultipleSelectForm("txtUnloading", qry, "DocNo", "", txtUnloading.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        If txtUnloading.arrValueMember Is Nothing OrElse txtUnloading.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            'If True Then
            '    Throw New Exception("Balwinder")
            'End If

            If txtUnloading.arrValueMember IsNot Nothing AndAlso txtUnloading.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtUnloading.arrValueMember
                    Dim qry As String = "select Unloading_No from TSPL_MILK_UNLOADING where Unloading_No like '" + StrSeq + "%' order by Unloading_Date_Time"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Unloading_No from TSPL_MILK_UNLOADING where Unloading_No like '" + StrSeq + "%' order by Unloading_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Unloading_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Unloading_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkPurchaseUnloading(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateBulkPurchaseUnloading(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryUnloading','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtReceipt__My_Click(sender As Object, e As EventArgs) Handles txtReceipt._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Receipt_No,0,len(Receipt_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_RECEIPT_HEADER)xx group by DocNo order by DocNo"
            txtReceipt.arrValueMember = clsCommon.ShowMultipleSelectForm("txtReceipt", qry, "DocNo", "", txtReceipt.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton8_Click(sender As Object, e As EventArgs) Handles RadButton8.Click
        If txtReceipt.arrValueMember Is Nothing OrElse txtReceipt.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtReceipt.arrValueMember IsNot Nothing AndAlso txtReceipt.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtReceipt.arrValueMember
                    Dim qry As String = "select Receipt_No from TSPL_RECEIPT_HEADER where Receipt_No like '" + StrSeq + "%' order by Receipt_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Receipt_No from TSPL_RECEIPT_HEADER where Receipt_No like '" + StrSeq + "%' order by Receipt_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Receipt_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Receipt_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateReceipt(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateReceipt(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'ReceiptEntry','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtPayment__My_Click(sender As Object, e As EventArgs) Handles txtPayment._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Payment_No,0,len(Payment_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_PAYMENT_HEADER)xx group by DocNo order by DocNo"
            txtPayment.arrValueMember = clsCommon.ShowMultipleSelectForm("txtPayment", qry, "DocNo", "", txtPayment.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton9_Click(sender As Object, e As EventArgs) Handles RadButton9.Click
        If txtPayment.arrValueMember Is Nothing OrElse txtPayment.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtPayment.arrValueMember IsNot Nothing AndAlso txtPayment.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtPayment.arrValueMember
                    Dim qry As String = "select Payment_No from TSPL_PAYMENT_HEADER where Payment_No like '" + StrSeq + "%' order by Payment_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Payment_No from TSPL_PAYMENT_HEADER where Payment_No like '" + StrSeq + "%' order by Payment_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Payment_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Payment_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdatePayment(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdatePayment(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'PaymentEntry','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtStockAdjustment__My_Click(sender As Object, e As EventArgs) Handles txtStockAdjustment._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Adjustment_No,0,len(Adjustment_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_ADJUSTMENT_HEADER)xx group by DocNo order by DocNo"
            txtStockAdjustment.arrValueMember = clsCommon.ShowMultipleSelectForm("txtSAdjustment", qry, "DocNo", "", txtStockAdjustment.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton10_Click(sender As Object, e As EventArgs) Handles RadButton10.Click
        If txtStockAdjustment.arrValueMember Is Nothing OrElse txtStockAdjustment.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtStockAdjustment.arrValueMember IsNot Nothing AndAlso txtStockAdjustment.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtStockAdjustment.arrValueMember
                    Dim qry As String = "select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Adjustment_No like '" + StrSeq + "%' order by Adjustment_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Adjustment_No from TSPL_ADJUSTMENT_HEADER where Adjustment_No like '" + StrSeq + "%' order by Adjustment_No"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Adjustment_No"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Adjustment_No"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateStoreAdjustment(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateStoreAdjustment(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'StoreAdjustment','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmDocumentSequence_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.Alt AndAlso e.Shift AndAlso e.KeyCode = Keys.F12 Then
            Dim pwd As New FrmPWD(Nothing)
            pwd.strCode = clsFixedParameterCode.DocumentSequence
            pwd.strType = clsFixedParameterType.DocumentSequence
            pwd.ShowDialog()
            If pwd.isPasswordCorrect Then
                Dim frm As New frmDocumentCorrection()
                frm.MdiParent = MDI
                frm.Show()
            End If
        End If
    End Sub

    Private Sub txtMilkGateEntryIn__My_Click(sender As Object, e As EventArgs) Handles txtMilkGateEntryIn._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( TSPL_MILK_GATE_ENTRY_IN.Entry_Code,0,len(TSPL_MILK_GATE_ENTRY_IN.Entry_Code)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_MILK_GATE_ENTRY_IN)xx group by DocNo order by DocNo"
            txtMilkGateEntryIn.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMGEI", qry, "DocNo", "", txtMilkGateEntryIn.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton11_Click(sender As Object, e As EventArgs) Handles RadButton11.Click
        If txtMilkGateEntryIn.arrValueMember Is Nothing OrElse txtMilkGateEntryIn.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow(Me, "You are Running this utility on MCC Database." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtMilkGateEntryIn.arrValueMember IsNot Nothing AndAlso txtMilkGateEntryIn.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtMilkGateEntryIn.arrValueMember
                    Dim qry As String = "select Entry_Code from TSPL_MILK_GATE_ENTRY_IN where Entry_Code like '" + StrSeq + "%' order by Entry_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Entry_Code from TSPL_MILK_GATE_ENTRY_IN where Entry_Code like '" + StrSeq + "%' order by Entry_Code"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Entry_Code"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Entry_Code"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateMilkGateEntryIn(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateMilkGateEntryIn(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'MilkGateEntryIN','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    
    Private Sub txtMilkGateEntryWeightment__My_Click(sender As Object, e As EventArgs) Handles txtMilkGateEntryWeightment._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code,0,len(TSPL_MILK_GATE_ENTRY_WEIGHTMENT.Weighment_Code)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_MILK_GATE_ENTRY_WEIGHTMENT)xx group by DocNo order by DocNo"
            txtMilkGateEntryWeightment.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMGEW", qry, "DocNo", "", txtMilkGateEntryWeightment.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton13_Click(sender As Object, e As EventArgs) Handles RadButton13.Click
        If txtMilkGateEntryWeightment.arrValueMember Is Nothing OrElse txtMilkGateEntryWeightment.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow(Me, "You are Running this utility on MCC Database." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtMilkGateEntryWeightment.arrValueMember IsNot Nothing AndAlso txtMilkGateEntryWeightment.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtMilkGateEntryWeightment.arrValueMember
                    Dim qry As String = "select Weighment_Code from TSPL_MILK_GATE_ENTRY_WEIGHTMENT where Weighment_Code like '" + StrSeq + "%' order by GW_Weighment_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Weighment_Code from TSPL_MILK_GATE_ENTRY_WEIGHTMENT where Weighment_Code like '" + StrSeq + "%' order by Weighment_Code"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Weighment_Code"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Weighment_Code"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateMilkGateEntryWeighment(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateMilkGateEntryWeighment(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'MilkGateEntryWGH','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtMilkGateEntryOut__My_Click(sender As Object, e As EventArgs) Handles txtMilkGateEntryOut._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code,0,len(TSPL_MILK_GATE_ENTRY_OUT.Gate_Out_Code)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_MILK_GATE_ENTRY_OUT)xx group by DocNo order by DocNo"
            txtMilkGateEntryOut.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMGEO", qry, "DocNo", "", txtMilkGateEntryOut.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton12_Click(sender As Object, e As EventArgs) Handles RadButton12.Click
        If txtMilkGateEntryOut.arrValueMember Is Nothing OrElse txtMilkGateEntryOut.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow(Me, "You are Running this utility on MCC Database." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtMilkGateEntryOut.arrValueMember IsNot Nothing AndAlso txtMilkGateEntryOut.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtMilkGateEntryOut.arrValueMember
                    Dim qry As String = "select Gate_Out_Code from TSPL_MILK_GATE_ENTRY_OUT where Gate_Out_Code like '" + StrSeq + "%' order by Gate_Out_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        qry = "select Gate_Out_Code from TSPL_MILK_GATE_ENTRY_OUT where Gate_Out_Code like '" + StrSeq + "%' order by Gate_Out_Code"
                        Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Gate_Out_Code"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("Gate_Out_Code"))
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateMilkGateEntryOut(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateMilkGateEntryOut(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'MilkGateEntryOUT','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    

    Private Sub txtMilkSRN__My_Click(sender As Object, e As EventArgs) Handles txtMilkSRN._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( DOC_CODE,0,len(DOC_CODE)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_MILK_SRN_HEAD)xx group by DocNo order by DocNo"
            txtMilkSRN.arrValueMember = clsCommon.ShowMultipleSelectForm("txtMSRN", qry, "DocNo", "", txtMilkSRN.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton14_Click(sender As Object, e As EventArgs) Handles RadButton14.Click
        If txtMilkSRN.arrValueMember Is Nothing OrElse txtMilkSRN.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow(Me, "You are Running this utility on MCC Database." + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Try
            Try
                clsDBFuncationality.ExecuteNonQuery("DISABLE TRIGGER  [trg_dontdeletecreatedsrnsampleno] on  [TSPL_MILK_SAMPLE_DETAIL]")
            Catch ex As Exception
            End Try


            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
            Try
                Dim strCounter As String = "X0000000000000001"
                clsCommon.ProgressBarPercentShow()
                clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
                If txtMilkSRN.arrValueMember IsNot Nothing AndAlso txtMilkSRN.arrValueMember.Count > 0 Then
                    For Each StrSeq As String In txtMilkSRN.arrValueMember
                        Dim qry As String = "select DOC_CODE from TSPL_MILK_SRN_HEAD where DOC_CODE like '" + StrSeq + "%' order by DOC_DATE"
                        Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                            qry = "select DOC_CODE from TSPL_MILK_SRN_HEAD where DOC_CODE like '" + StrSeq + "%' order by DOC_CODE"
                            Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            For ii As Integer = 0 To dtOld.Rows.Count - 1
                                clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtNew.Rows.Count))
                                Dim coll As New Hashtable()
                                clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                                clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("DOC_CODE"))
                                clsCommon.AddColumnsForChange(coll, "NEW_NO", dtNew.Rows(ii)("DOC_CODE"))
                                clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                                strCounter = clsCommon.incval(strCounter)
                            Next
                        End If
                    Next
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For ii As Integer = 0 To dt.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                            If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                                objDocSeq.UpdateMilkSRN(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                            End If
                        Next
                        For ii As Integer = 0 To dt.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                            If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                                objDocSeq.UpdateMilkSRN(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                            End If
                        Next
                    End If
                End If
                clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'MilkGateEntrySRN','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
                trans.Commit()
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarPercentHide()
                Throw New Exception(ex.Message)
            End Try
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Try
                clsDBFuncationality.ExecuteNonQuery("Enable TRIGGER  [trg_dontdeletecreatedsrnsampleno] on  [TSPL_MILK_SAMPLE_DETAIL]")
            Catch ex As Exception
            End Try
        End Try
       
    End Sub

    Private Sub txtPurchaseTaxInvoice__My_Click(sender As Object, e As EventArgs) Handles txtPurchaseTaxInvoice._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING(Purchase_Tax_Invoice,0,len(Purchase_Tax_Invoice)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from tspl_vendor_invoice_head  where Purchase_Tax_Invoice is not null and Purchase_Tax_Invoice_Type='P')xx group by DocNo order by DocNo"
            txtPurchaseTaxInvoice.arrValueMember = clsCommon.ShowMultipleSelectForm("txtPurchaseTaxInvoice", qry, "DocNo", "", txtPurchaseTaxInvoice.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton15_Click(sender As Object, e As EventArgs) Handles RadButton15.Click
        If txtPurchaseTaxInvoice.arrValueMember Is Nothing OrElse txtPurchaseTaxInvoice.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow(Me, "Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If txtPurchaseTaxInvoice.arrValueMember IsNot Nothing AndAlso txtPurchaseTaxInvoice.arrValueMember.Count > 0 Then
                For Each StrSeq As String In txtPurchaseTaxInvoice.arrValueMember
                    Dim qry As String = "select Purchase_Tax_Invoice from TSPL_VENDOR_INVOICE_HEAD where Purchase_Tax_Invoice like '" + StrSeq + "%' order by Posting_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        'qry = "select Purchase_Tax_Invoice from TSPL_VENDOR_INVOICE_HEAD where Purchase_Tax_Invoice like '" + StrSeq + "%' order by Purchase_Tax_Invoice"
                        'Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        Dim NewNum As String = StrSeq
                        For ii As Integer = 1 To txtSplit.Value - 1
                            NewNum += "0"
                        Next
                        NewNum += "1"


                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtOld.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Purchase_Tax_Invoice"))
                            clsCommon.AddColumnsForChange(coll, "NEW_NO", NewNum)
                            clsCommonFunctionality.UpdateDataTable(coll, "TMP_DOC_SEQ", OMInsertOrUpdate.Insert, "", trans)
                            strCounter = clsCommon.incval(strCounter)
                            NewNum = clsCommon.incval(NewNum)
                        Next
                    End If
                Next
                Dim dt As DataTable = clsDBFuncationality.GetDataTable("select * from TMP_DOC_SEQ order by S_NO", trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Temp Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateDocumentSequenceOfGSTPurchaseTaxInvoice(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            objDocSeq.UpdateDocumentSequenceOfGSTPurchaseTaxInvoice(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'GST PurchaseTaxInvoice','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, "Task Done successfully", Me.Text)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class