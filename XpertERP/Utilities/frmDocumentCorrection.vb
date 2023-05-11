Imports common
Imports System.Data.SqlClient
Public Class frmDocumentCorrection
    Private Sub frmDocumentSequence_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''Against ticket BM00000007766

    End Sub
  
    Private Sub RadButton3_Click(sender As Object, e As EventArgs) Handles RadButton3.Click
        Me.Close()
    End Sub


    Sub ExecuteNonQueryWithDropContraint(ByVal strQuery As String, ByVal tran As SqlTransaction)
line1:
        Try
            clsDBFuncationality.ExecuteNonQuery(strQuery, tran)
        Catch ex As Exception
            If ex.Message.Contains("statement conflicted with the REFERENCE constraint") Then
                Dim TestArray() As String = ex.Message.Split("""")
                For i As Integer = 0 To TestArray.Length - 1
                    clsDBFuncationality.ExecuteNonQuery("Alter table " + TestArray(5) + " drop constraint " + TestArray(1), tran)
                    GoTo line1
                Next
            Else
                Throw New Exception(ex.Message)
            End If
        End Try
    End Sub

    ''Bulk Procurement Gate Entery
    Private Sub txtBulkProcSeq__My_Click(sender As Object, e As EventArgs) Handles txtBulkProcSeq._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Gate_Entry_No,1,len(Gate_Entry_No)-" + clsCommon.myCstr(txtSplit.Value) + ") as DocNo  from Tspl_Gate_Entry_Details   )xx group by DocNo order by DocNo"
            txtBulkProcSeq.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcSequti", qry, "DocNo", "", txtBulkProcSeq.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton47_Click(sender As Object, e As EventArgs) Handles RadButton47.Click
        
        Dim docList As New List(Of clsTEMP_CORRECT_DOC)
        Dim obj As New clsTEMP_CORRECT_DOC
        If txtBulkProcSeq.arrValueMember Is Nothing OrElse txtBulkProcSeq.arrValueMember.Count <= 0 Then
            Dim qry As String = "select * from TEMP_CORRECT_DOC"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim count As Integer = dt.Rows.Count
            If count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Pick documents", Me.Text)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    obj = New clsTEMP_CORRECT_DOC
                    obj.Doc_No = clsCommon.myCstr(dr.Item("Doc_No"))
                    obj.DOC_DATE = clsCommon.myCstr(dr.Item("DOC_DATE"))
                    docList.Add(obj)
                Next
            End If
        Else
            'docList = txtBulkProcSeq.arrValueMember.Cast(Of clsTEMP_CORRECT_DOC).ToList
            For Each StrDoc As String In txtBulkProcSeq.arrValueMember
                obj = New clsTEMP_CORRECT_DOC
                obj.Doc_No = StrDoc
                docList.Add(obj)
            Next
        End If
        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If docList.Count > 0 Then
                For Each Doc As clsTEMP_CORRECT_DOC In docList
                    Dim StrSeq As String = Doc.Doc_No
                    Dim qry As String = "select Gate_Entry_No,Doc_Type,Date_And_Time,location_Code from Tspl_Gate_Entry_Details where gate_entry_no like '" + StrSeq + "%' order by Date_And_Time"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        'qry = "select Gate_Entry_No from Tspl_Gate_Entry_Details where gate_entry_no like '" + StrSeq + "%' order by Gate_Entry_No"
                        'Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtOld.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Gate_Entry_No"))
                            Dim strNewCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.myCDate(dtOld.Rows(ii)("Date_And_Time")), "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.GateEntry, IIf(clsCommon.CompairString("MccProc", clsCommon.myCstr(dtOld.Rows(ii)("Doc_Type"))) = CompairStringResult.Equal, clsDocTransactionType.MccProc, clsDocTransactionType.BulkProc), clsCommon.myCstr(dtOld.Rows(ii)("location_Code")))
                            If clsCommon.myLen(strNewCode) <= 0 Then
                                Throw New Exception("Error in code generation")
                            End If
                            qry = "select 1 from Tspl_Gate_Entry_Details where gate_entry_no='" + strNewCode + "' "
                            Dim dtDocExists As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtDocExists IsNot Nothing AndAlso dtDocExists.Rows.Count > 0 Then
                                Throw New Exception("Doc No - " + clsCommon.myCstr(dtOld.Rows(ii)("Gate_Entry_No")) + " it's  New Doc No " + strNewCode + " is already exists in the system")
                            End If

                            clsCommon.AddColumnsForChange(coll, "NEW_NO", strNewCode)
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
                            UpdateBulkGateEntry(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkGateEntry(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkGateEntryCorr','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub

    Public Sub UpdateBulkGateEntry(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update Tspl_Gate_Entry_Details set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Weighment_Detail set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_QUALITY_CHECK set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_UNLOADING set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Cleaning set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Gate_Out set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Bulk_MILK_SRN set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_milk_transfer_in set  Gate_Entry_No='" + strToBeReplaceNo + "' where Gate_Entry_No='" + strFindNo + "'", trans)
    End Sub
    ''Bulk Procurement Gate Entery End here


    ''Bulk Procurement Weighment
    Private Sub txtWeighmentSeqGrp__My_Click(sender As Object, e As EventArgs) Handles txtWeighmentSeqGrp._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Weighment_No,1,len(Weighment_No)-" + clsCommon.myCstr(txtSplit.Value) + ") as DocNo  from TSPL_Weighment_Detail   )xx group by DocNo order by DocNo"
            txtWeighmentSeqGrp.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcWeSe", qry, "DocNo", "", txtWeighmentSeqGrp.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton44_Click(sender As Object, e As EventArgs) Handles RadButton44.Click
        'If txtWeighmentSeqGrp.arrValueMember Is Nothing OrElse txtWeighmentSeqGrp.arrValueMember.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
        '    Exit Sub
        'End If

        Dim docList As New List(Of clsTEMP_CORRECT_DOC)
        Dim obj As New clsTEMP_CORRECT_DOC
        If txtWeighmentSeqGrp.arrValueMember Is Nothing OrElse txtWeighmentSeqGrp.arrValueMember.Count <= 0 Then
            Dim qry As String = "select * from TEMP_CORRECT_DOC"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim count As Integer = dt.Rows.Count
            If count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Pick documents", Me.Text)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    obj = New clsTEMP_CORRECT_DOC
                    obj.Doc_No = clsCommon.myCstr(dr.Item("Doc_No"))
                    obj.DOC_DATE = clsCommon.myCstr(dr.Item("DOC_DATE"))
                    docList.Add(obj)
                Next
            End If
        Else
            'docList = txtWeighmentSeqGrp.arrValueMember.Cast(Of clsTEMP_CORRECT_DOC).ToList
            For Each StrDoc As String In txtWeighmentSeqGrp.arrValueMember
                obj = New clsTEMP_CORRECT_DOC
                obj.Doc_No = StrDoc
                docList.Add(obj)
            Next
        End If
        

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If docList.Count > 0 Then
                For Each Doc As clsTEMP_CORRECT_DOC In docList
                    Dim StrSeq As String = Doc.Doc_No
                    Dim qry As String = "select Weighment_No,Weighment_date,Doc_Type,location_Code from TSPL_Weighment_Detail where Weighment_No like '" + StrSeq + "%' order by Weighment_date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        'qry = "select Weighment_No from TSPL_Weighment_Detail where Weighment_No like '" + StrSeq + "%' order by Weighment_No"
                        'Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtOld.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Weighment_No"))

                            Dim strNewCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.myCDate(dtOld.Rows(ii)("Weighment_date")), "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.Weighment, IIf(clsCommon.CompairString("MccProc", clsCommon.myCstr(dtOld.Rows(ii)("Doc_Type"))) = CompairStringResult.Equal, clsDocTransactionType.MccProc, clsDocTransactionType.BulkProc), clsCommon.myCstr(dtOld.Rows(ii)("location_Code")))
                            If clsCommon.myLen(strNewCode) <= 0 Then
                                Throw New Exception("Error in code generation")
                            End If
                            qry = "select 1 from TSPL_Weighment_Detail where Weighment_No='" + strNewCode + "' "
                            Dim dtDocExists As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtDocExists IsNot Nothing AndAlso dtDocExists.Rows.Count > 0 Then
                                Throw New Exception("Doc No - " + clsCommon.myCstr(dtOld.Rows(ii)("Weighment_No")) + " it's  New Doc No " + strNewCode + " is already exists in the system")
                            End If

                            clsCommon.AddColumnsForChange(coll, "NEW_NO", strNewCode)
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
                            UpdateBulkWeighment(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkWeighment(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryWeighment_corr','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkWeighment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Weighment_Detail set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_QUALITY_CHECK set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_UNLOADING set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Cleaning set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Gate_Out set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Bulk_MILK_SRN set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_milk_transfer_in set  Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
    End Sub
    ''Bulk Procurement Weighment End here

    ''Bulk Procurement QC 
    Private Sub txtBulkProcQC__My_Click(sender As Object, e As EventArgs) Handles txtBulkProcQC._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( QC_No,1,len(QC_No)-" + clsCommon.myCstr(txtSplit.Value) + ") as DocNo  from TSPL_QUALITY_CHECK   )xx group by DocNo order by DocNo"
            txtBulkProcQC.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcQCSequti", qry, "DocNo", "", txtBulkProcQC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton46_Click(sender As Object, e As EventArgs) Handles RadButton46.Click
        'If txtBulkProcQC.arrValueMember Is Nothing OrElse txtBulkProcQC.arrValueMember.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
        '    Exit Sub
        'End If
        Dim docList As New List(Of clsTEMP_CORRECT_DOC)
        Dim obj As New clsTEMP_CORRECT_DOC
        If txtBulkProcQC.arrValueMember Is Nothing OrElse txtBulkProcQC.arrValueMember.Count <= 0 Then
            Dim qry As String = "select * from TEMP_CORRECT_DOC"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim count As Integer = dt.Rows.Count
            If count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Pick documents", Me.Text)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    obj = New clsTEMP_CORRECT_DOC
                    obj.Doc_No = clsCommon.myCstr(dr.Item("Doc_No"))
                    obj.DOC_DATE = clsCommon.myCstr(dr.Item("DOC_DATE"))
                    docList.Add(obj)
                Next
            End If
        Else
            'docList = txtBulkProcQC.arrValueMember.Cast(Of clsTEMP_CORRECT_DOC).ToList
            For Each StrDoc As String In txtBulkProcQC.arrValueMember
                obj = New clsTEMP_CORRECT_DOC
                obj.Doc_No = StrDoc
                docList.Add(obj)
            Next
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If docList.Count > 0 Then
                For Each Doc As clsTEMP_CORRECT_DOC In docList
                    Dim StrSeq As String = Doc.Doc_No
                    Dim qry As String = "select QC_No,QC_In_Date_Time,Doc_Type,location_Code from TSPL_QUALITY_CHECK where QC_No like '" + StrSeq + "%' order by QC_In_Date_Time"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        'qry = "select QC_No from TSPL_QUALITY_CHECK where QC_No like '" + StrSeq + "%' order by QC_No"
                        'Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtOld.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("QC_No"))

                            Dim strNewCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.myCDate(dtOld.Rows(ii)("QC_In_Date_Time")), "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.QualityCheck, IIf(clsCommon.CompairString("MccProc", clsCommon.myCstr(dtOld.Rows(ii)("Doc_Type"))) = CompairStringResult.Equal, clsDocTransactionType.MccProc, clsDocTransactionType.BulkProc), clsCommon.myCstr(dtOld.Rows(ii)("location_Code")))

                            If clsCommon.myLen(strNewCode) <= 0 Then
                                Throw New Exception("Error in code generation")
                            End If
                            qry = "select 1 from TSPL_QUALITY_CHECK where QC_No='" + strNewCode + "' "
                            Dim dtDocExists As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtDocExists IsNot Nothing AndAlso dtDocExists.Rows.Count > 0 Then
                                Throw New Exception("Doc No - " + clsCommon.myCstr(dtOld.Rows(ii)("QC_No")) + " it's  New Doc No " + strNewCode + " is already exists in the system")
                            End If


                            clsCommon.AddColumnsForChange(coll, "NEW_NO", strNewCode)
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
                            UpdateBulkQC(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkQC(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryQC_Corr','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkQC(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_QUALITY_CHECK set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_transaction_approval set  Document_No ='" + strToBeReplaceNo + "' where Document_No ='" + strFindNo + "' and Program_Code='M-QC'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_qc_parameter_detail set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_UNLOADING set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Cleaning set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Gate_Out set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Bulk_MILK_SRN set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_milk_transfer_in set  QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
    End Sub
    ''Bulk Procurement QC End here

    ''Bulk SRN
    Private Sub txtBulkSRNSequence__My_Click(sender As Object, e As EventArgs) Handles txtBulkSRNSequence._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( SRN_NO,1,len(SRN_NO)-" + clsCommon.myCstr(txtSplit.Value) + ") as DocNo  from TSPL_Bulk_MILK_SRN)xx group by DocNo order by DocNo"
            txtBulkSRNSequence.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcSRNSequti", qry, "DocNo", "", txtBulkSRNSequence.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton48_Click(sender As Object, e As EventArgs) Handles RadButton48.Click
        'If txtBulkSRNSequence.arrValueMember Is Nothing OrElse txtBulkSRNSequence.arrValueMember.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
        '    Exit Sub
        'End If
        Dim docList As New List(Of clsTEMP_CORRECT_DOC)
        Dim obj As New clsTEMP_CORRECT_DOC
        If txtBulkSRNSequence.arrValueMember Is Nothing OrElse txtBulkSRNSequence.arrValueMember.Count <= 0 Then
            Dim qry As String = "select * from TEMP_CORRECT_DOC"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim count As Integer = dt.Rows.Count
            If count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Pick documents", Me.Text)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    obj = New clsTEMP_CORRECT_DOC
                    obj.Doc_No = clsCommon.myCstr(dr.Item("Doc_No"))
                    obj.DOC_DATE = clsCommon.myCstr(dr.Item("DOC_DATE"))
                    docList.Add(obj)
                Next
            End If
        Else
            'docList = txtBulkSRNSequence.arrValueMember.Cast(Of clsTEMP_CORRECT_DOC).ToList
            For Each StrDoc As String In txtBulkSRNSequence.arrValueMember
                obj = New clsTEMP_CORRECT_DOC
                obj.Doc_No = StrDoc
                docList.Add(obj)
            Next
        End If
        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If docList.Count > 0 Then
                For Each Doc As clsTEMP_CORRECT_DOC In docList
                    Dim StrSeq As String = Doc.Doc_No
                    Dim qry As String = "select SRN_NO,SRN_Date,Loc_Code from TSPL_Bulk_MILK_SRN where SRN_NO like '" + StrSeq + "%' order by SRN_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        'qry = "select SRN_NO from TSPL_Bulk_MILK_SRN where SRN_NO like '" + StrSeq + "%' order by SRN_NO"
                        'Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtOld.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("SRN_NO"))

                            Dim strNewCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.myCDate(dtOld.Rows(ii)("SRN_Date")), "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.BulkMilkSRN, "", clsCommon.myCstr(dtOld.Rows(ii)("Loc_Code")))

                            If clsCommon.myLen(strNewCode) <= 0 Then
                                Throw New Exception("Error in code generation")
                            End If
                            qry = "select 1 from TSPL_Bulk_MILK_SRN where SRN_NO='" + strNewCode + "' "
                            Dim dtDocExists As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtDocExists IsNot Nothing AndAlso dtDocExists.Rows.Count > 0 Then
                                Throw New Exception("Doc No - " + clsCommon.myCstr(dtOld.Rows(ii)("SRN_NO")) + " it's  New Doc No " + strNewCode + " is already exists in the system")
                            End If

                            clsCommon.AddColumnsForChange(coll, "NEW_NO", strNewCode)
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
                            UpdateBulkSRN(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkSRN(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntrySRN_Corr','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Public Sub UpdateBulkSRN(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Bulk_MILK_SRN  set SRN_No='" + strToBeReplaceNo + "' where SRN_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_Bulk_milk_purchase_Invoice_Detail set  SRN_No='" + strToBeReplaceNo + "' where SRN_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_transaction_approval set Document_No ='" + strToBeReplaceNo + "' where Program_Code = 'M-SRN-B' and Document_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set  source_doc_no ='" + strToBeReplaceNo + "' where source_doc_no ='" + strFindNo + "' and trans_type = 'BulkSRN'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "' and Source_Code = 'BM-SR'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_RGP_Detail set  Bulk_Milk_SRN_Code='" + strToBeReplaceNo + "' where Bulk_Milk_SRN_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_ADJUSTMENT_HEADER set  Against_Bulk_Srn_PI_adjustment='" + strToBeReplaceNo + "' where Against_Bulk_Srn_PI_adjustment='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_vendor_invoice_head set  Description=REPLACE( Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where Description like '%" + strFindNo + "%'", trans)
    End Sub
    ''Bulk SRN End here

    ''Bulk PI  
    Private Sub txtBulkPI__My_Click(sender As Object, e As EventArgs) Handles txtBulkPI._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( DOC_NO,1,len(DOC_NO)-" + clsCommon.myCstr(txtSplit.Value) + ") as DocNo  from tspl_Bulk_milk_purchase_Invoice_head)xx group by DocNo order by DocNo"
            txtBulkPI.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkProcPISequti", qry, "DocNo", "", txtBulkPI.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton49_Click(sender As Object, e As EventArgs) Handles RadButton49.Click
        'If txtBulkPI.arrValueMember Is Nothing OrElse txtBulkPI.arrValueMember.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
        '    Exit Sub
        'End If

        Dim docList As New List(Of clsTEMP_CORRECT_DOC)
        Dim obj As New clsTEMP_CORRECT_DOC
        If txtBulkPI.arrValueMember Is Nothing OrElse txtBulkPI.arrValueMember.Count <= 0 Then
            Dim qry As String = "select * from TEMP_CORRECT_DOC"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim count As Integer = dt.Rows.Count
            If count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Pick documents", Me.Text)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    obj = New clsTEMP_CORRECT_DOC
                    obj.Doc_No = clsCommon.myCstr(dr.Item("Doc_No"))
                    obj.DOC_DATE = clsCommon.myCstr(dr.Item("DOC_DATE"))
                    docList.Add(obj)
                Next
            End If
        Else
            'docList = txtBulkPI.arrValueMember.Cast(Of clsTEMP_CORRECT_DOC).ToList
            For Each StrDoc As String In txtBulkPI.arrValueMember
                obj = New clsTEMP_CORRECT_DOC
                obj.Doc_No = StrDoc
                docList.Add(obj)
            Next
        End If
        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If docList.Count > 0 Then
                For Each Doc As clsTEMP_CORRECT_DOC In docList
                    Dim StrSeq As String = Doc.Doc_No
                    Dim qry As String = "select DOC_NO,isSRNTradeInvoice,vendor_code,DOC_DATE,loc_code from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO like '" + StrSeq + "%' order by DOC_DATE"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        'qry = "select DOC_NO from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO like '" + StrSeq + "%' order by DOC_NO"
                        'Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtOld.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("DOC_NO"))

                            Dim strNewCode As String = ""
                            If clsCommon.myCdbl(dtOld.Rows(ii)("isSRNTradeInvoice")) = 1 Then
                                If FrmMilkPurchaseInvoice.isVendorInvoiceNo(clsCommon.myCstr(dtOld.Rows(ii)("vendor_code")), trans) Then
                                    strNewCode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dtOld.Rows(ii)("DOC_DATE")), clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithVendorInvoiceNo, clsCommon.myCstr(dtOld.Rows(ii)("loc_code")))
                                Else
                                    strNewCode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dtOld.Rows(ii)("DOC_DATE")), clsDocType.BulkMilkPurchaseInvoiceTrade, clsDocTransactionType.WithoutVendorInvoiceNo, clsCommon.myCstr(dtOld.Rows(ii)("loc_code")))
                                End If
                            Else
                                If FrmMilkPurchaseInvoice.isVendorInvoiceNo(clsCommon.myCstr(dtOld.Rows(ii)("vendor_code")), trans) Then
                                    strNewCode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dtOld.Rows(ii)("DOC_DATE")), clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithVendorInvoiceNo, clsCommon.myCstr(dtOld.Rows(ii)("loc_code")))
                                Else
                                    strNewCode = clsERPFuncationality.GetNextCode(trans, clsCommon.myCDate(dtOld.Rows(ii)("DOC_DATE")), clsDocType.BulkMilkPurchaseInvoice, clsDocTransactionType.WithoutVendorInvoiceNo, clsCommon.myCstr(dtOld.Rows(ii)("loc_code")))
                                End If
                            End If


                            If clsCommon.myLen(strNewCode) <= 0 Then
                                Throw New Exception("Error in code generation")
                            End If
                            qry = "select 1 from tspl_Bulk_milk_purchase_Invoice_head where DOC_NO='" + strNewCode + "' "
                            Dim dtDocExists As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtDocExists IsNot Nothing AndAlso dtDocExists.Rows.Count > 0 Then
                                Throw New Exception("Doc No - " + clsCommon.myCstr(dtOld.Rows(ii)("DOC_NO")) + " it's  New Doc No " + strNewCode + " is already exists in the system")
                            End If


                            clsCommon.AddColumnsForChange(coll, "NEW_NO", strNewCode)
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
                            UpdateBulkPI(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkPI(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryPI_corr','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkPI(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update tspl_Bulk_milk_purchase_Invoice_head  set Doc_no='" + strToBeReplaceNo + "' where Doc_no='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_Bulk_milk_purchase_Invoice_detail  set Doc_no='" + strToBeReplaceNo + "' where Doc_no='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_vendor_invoice_head set Against_BulkMillkPurchaseInvoice_No='" + strToBeReplaceNo + "', Description=REPLACE( Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where Against_BulkMillkPurchaseInvoice_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_ADJUSTMENT_HEADER set Against_Bulk_Srn_PI_adjustment='" + strToBeReplaceNo + "', Description=REPLACE( Description,'" + strFindNo + "','" + strToBeReplaceNo + "') where Against_Bulk_Srn_PI_adjustment ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "' and Source_Code = 'BM-PI'", trans)
    End Sub

    ''Bulk PI End here

    ''Bulk SAale Entry 
    Private Sub txtBulkSaleGateEntry__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleGateEntry._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_GATEENTRY_SALE)xx group by DocNo order by DocNo"
            txtBulkSaleGateEntry.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkSaleGateEntry", qry, "DocNo", "", txtBulkSaleGateEntry.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton54_Click(sender As Object, e As EventArgs) Handles RadButton54.Click
        If txtBulkSaleGateEntry.arrValueMember Is Nothing OrElse txtBulkSaleGateEntry.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateBulkSaleEntry(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkSaleEntry(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleEntry','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkSaleEntry(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEENTRY_SALE  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_WEIGHMENT_DETAIL_BULKSALE  set GateEntry_Document_No='" + strToBeReplaceNo + "' where GateEntry_Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Quality_Check_BulkSale set GateEntry_Document_No='" + strToBeReplaceNo + "' where GateEntry_Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEOUT_SALE set  GateEntryNo='" + strToBeReplaceNo + "' where GateEntryNo='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SALE_RETURN_MASTER_BULKSALE set  GateEntryNo='" + strToBeReplaceNo + "' where GateEntryNo='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEENTRY_SALE set  SaleReturnAgaintGEN='" + strToBeReplaceNo + "' where SaleReturnAgaintGEN='" + strFindNo + "'", trans)
    End Sub
    ''End of Bulk SAale Entry


    ''Bulk Sale Weighment
    Private Sub TxtMultiSelectFinder4__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleWeighment._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( weighment_No,0,len(weighment_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_WEIGHMENT_DETAIL_BULKSALE)xx group by DocNo order by DocNo"
            txtBulkSaleWeighment.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiSelectFinder4", qry, "DocNo", "", txtBulkSaleWeighment.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton53_Click(sender As Object, e As EventArgs) Handles RadButton53.Click
        If txtBulkSaleWeighment.arrValueMember Is Nothing OrElse txtBulkSaleWeighment.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateBulkSaleWeighment(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkSaleWeighment(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleWeighment','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkSaleWeighment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_WEIGHMENT_DETAIL_BULKSALE  set Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_LOADING_TANKER_DETAIL_BULKSALE  set Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Quality_Check_BulkSale set Weighment_No='" + strToBeReplaceNo + "' where Weighment_No='" + strFindNo + "'", trans)
    End Sub
    ''End of Bulk Sale Weighment

    ''Bulk Sale Loading
    Private Sub TxtMultiSelectFinder3__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleLoading._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( LoadingTanker_No,0,len(LoadingTanker_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_LOADING_TANKER_DETAIL_BULKSALE)xx group by DocNo order by DocNo"
            txtBulkSaleLoading.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkSaleLoading", qry, "DocNo", "", txtBulkSaleLoading.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBulkSaleLoading__My_Click(sender As Object, e As EventArgs) Handles RadButton55.Click
        If txtBulkSaleLoading.arrValueMember Is Nothing OrElse txtBulkSaleLoading.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateBulkSaleLoading(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkSaleLoading(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleLoading','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkSaleLoading(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_LOADING_TANKER_DETAIL_BULKSALE  set LoadingTanker_No='" + strToBeReplaceNo + "' where LoadingTanker_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Quality_Check_BulkSale  set LoadingTanker_No='" + strToBeReplaceNo + "' where LoadingTanker_No='" + strFindNo + "'", trans)
    End Sub
    ''End of Bulk Sale Loading




    ''Bulk Sale QC
    Private Sub txtBulkSaleQC__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleQC._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( QC_No,0,len(QC_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_Quality_Check_BulkSale)xx group by DocNo order by DocNo"
            txtBulkSaleQC.arrValueMember = clsCommon.ShowMultipleSelectForm("TxtMultiSelectFinder4", qry, "DocNo", "", txtBulkSaleQC.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton52_Click(sender As Object, e As EventArgs) Handles RadButton52.Click
        If txtBulkSaleQC.arrValueMember Is Nothing OrElse txtBulkSaleQC.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateBulkSaleQC(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkSaleQC(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleQC','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkSaleQC(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Quality_Check_BulkSale  set QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Dispatch_BulkSale  set QC_Code='" + strToBeReplaceNo + "' where QC_Code='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_QC_Parameter_Detail_BulKSALE  set QC_No='" + strToBeReplaceNo + "' where QC_No='" + strFindNo + "'", trans)
    End Sub

    ''End of Bulk Sale QC

    ''Bulk Sale Dispatch
    Private Sub txtBSDispatch__My_Click(sender As Object, e As EventArgs) Handles txtBSDispatch._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_Dispatch_BulkSale)xx group by DocNo order by DocNo"
            txtBSDispatch.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBSDispatch", qry, "DocNo", "", txtBSDispatch.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton51_Click(sender As Object, e As EventArgs) Handles RadButton51.Click
        If txtBSDispatch.arrValueMember Is Nothing OrElse txtBSDispatch.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateBulkSaleDispatch(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkSaleDispatch(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleDispatch','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkSaleDispatch(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Dispatch_BulkSale  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Dispatch_Detail_BulkSale  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Source_Doc_No='" + strToBeReplaceNo + "',Remarks=REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "'),Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "' and Source_Code = 'DS-BS'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SALE_RETURN_MASTER_BULKSALE set DispatchNo='" + strToBeReplaceNo + "' where DispatchNo ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SALE_RETURN_DETAIL_BULKSALE set Dispatch_Code='" + strToBeReplaceNo + "' where Dispatch_Code ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEENTRY_SALE set Dispatch_No='" + strToBeReplaceNo + "' where Dispatch_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVOICE_DETAIL_BULKSALE set Dispatch_Code='" + strToBeReplaceNo + "' where Dispatch_Code ='" + strFindNo + "'", trans)
    End Sub
    ''End of Bulk Sale Dispatch

    ''  Bulk Sale Invoice 
    Private Sub txtBulkSaleInvoice__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleInvoice._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_INVOICE_MASTER_BULKSALE)xx group by DocNo order by DocNo"
            txtBulkSaleInvoice.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkSaleInvoice", qry, "DocNo", "", txtBulkSaleInvoice.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton50_Click(sender As Object, e As EventArgs) Handles RadButton50.Click
        If txtBulkSaleInvoice.arrValueMember Is Nothing OrElse txtBulkSaleInvoice.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateBulkSaleInvoice(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkSaleInvoice(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkSaleInvoice','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkSaleInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_INVOICE_MASTER_BULKSALE  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVOICE_DETAIL_BULKSALE  set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set Against_Sale_No='" + strToBeReplaceNo + "' where Against_Sale_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_SALE_RETURN_MASTER_BULKSALE set InvoiceNo='" + strToBeReplaceNo + "' where InvoiceNo ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Remarks =REPLACE(Remarks,'" + strFindNo + "','" + strToBeReplaceNo + "') where Remarks like'%" + strFindNo + "%' and Source_Code = 'AR-IN'", trans)
    End Sub
    ''End of Bulk Sale Invoice 

    ''Bulk Sale Gateout 
    Private Sub txtBulkSaleGateOut__My_Click(sender As Object, e As EventArgs) Handles txtBulkSaleGateOut._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_GATEOUT_SALE)xx group by DocNo order by DocNo"
            txtBulkSaleGateOut.arrValueMember = clsCommon.ShowMultipleSelectForm("txtBulkSaleGateOut", qry, "DocNo", "", txtBulkSaleGateOut.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton56_Click(sender As Object, e As EventArgs) Handles RadButton56.Click
        If txtBulkSaleGateOut.arrValueMember Is Nothing OrElse txtBulkSaleGateOut.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateBulkGateOut(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkGateOut(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkGateOut','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try


    End Sub

    Public Sub UpdateBulkGateOut(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_GATEOUT_SALE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
    End Sub
    ''End of Bulk Sale Gateout 





    ''AR 
    Private Sub txtAR__My_Click(sender As Object, e As EventArgs) Handles txtAR._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_Customer_Invoice_Head)xx group by DocNo order by DocNo"
            txtAR.arrValueMember = clsCommon.ShowMultipleSelectForm("txtAR", qry, "DocNo", "", txtAR.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        If txtAR.arrValueMember Is Nothing OrElse txtAR.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateARInvoice(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateARInvoice(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'ARInvoice','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateARInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Head set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Customer_Invoice_Detail set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_RECEIPT_DETAIL set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "'", trans)
    End Sub

    ''End of AR


    ''AP
    Private Sub txtAP__My_Click(sender As Object, e As EventArgs) Handles txtAP._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Document_No,0,len(Document_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_VENDOR_INVOICE_HEAD)xx group by DocNo order by DocNo"
            txtAP.arrValueMember = clsCommon.ShowMultipleSelectForm("txtAP", qry, "DocNo", "", txtAP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        If txtAP.arrValueMember Is Nothing OrElse txtAP.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateAPInvoice(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateAPInvoice(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'APInvoice','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateAPInvoice(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_VENDOR_INVOICE_HEAD set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_VENDOR_INVOICE_DETAIL set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_REMITTANCE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PAYMENT_DETAIL set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "'", trans)
    End Sub
    ''End of AP

    ''Journal Entry
    Private Sub txtJournalEntry__My_Click(sender As Object, e As EventArgs) Handles txtJournalEntry._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Voucher_No,0,len(Voucher_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_JOURNAL_MASTER)xx group by DocNo order by DocNo"
            txtJournalEntry.arrValueMember = clsCommon.ShowMultipleSelectForm("txtJournalEntry", qry, "DocNo", "", txtJournalEntry.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        If txtJournalEntry.arrValueMember Is Nothing OrElse txtJournalEntry.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateJournalEntry(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateJournalEntry(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'JournalEntry','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateJournalEntry(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Voucher_No='" + strToBeReplaceNo + "' where Voucher_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_DETAILS set Voucher_No='" + strToBeReplaceNo + "' where Voucher_No='" + strFindNo + "'", trans)
    End Sub
    ''End of Journal Entry


    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        XpertERPEngine.clsCreateAllTables.CreateAllTable()
    End Sub

    Private Sub txtGateout__My_Click(sender As Object, e As EventArgs) Handles txtGateout._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Doc_No,1,len(Doc_No)-" + clsCommon.myCstr(txtSplit.Value) + ") as DocNo  from TSpl_Gate_Out)xx group by DocNo order by DocNo"
            txtGateout.arrValueMember = clsCommon.ShowMultipleSelectForm("txtGateout", qry, "DocNo", "", txtGateout.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        'If txtGateout.arrValueMember Is Nothing OrElse txtGateout.arrValueMember.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
        '    Exit Sub
        'End If

        Dim docList As New List(Of clsTEMP_CORRECT_DOC)
        Dim obj As New clsTEMP_CORRECT_DOC
        If txtGateout.arrValueMember Is Nothing OrElse txtGateout.arrValueMember.Count <= 0 Then
            Dim qry As String = "select * from TEMP_CORRECT_DOC"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim count As Integer = dt.Rows.Count
            If count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Pick documents", Me.Text)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    obj = New clsTEMP_CORRECT_DOC
                    obj.Doc_No = clsCommon.myCstr(dr.Item("Doc_No"))
                    obj.DOC_DATE = clsCommon.myCstr(dr.Item("DOC_DATE"))
                    docList.Add(obj)
                Next
            End If
        Else
            'docList = txtGateout.arrValueMember.Cast(Of clsTEMP_CORRECT_DOC).ToList
            For Each StrDoc As String In txtGateout.arrValueMember
                obj = New clsTEMP_CORRECT_DOC
                obj.Doc_No = StrDoc
                docList.Add(obj)
            Next
        End If
        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin
        Try
            Dim strCounter As String = "X0000000000000001"
            clsCommon.ProgressBarPercentShow()
            clsDBFuncationality.ExecuteNonQuery("delete from TMP_DOC_SEQ", trans)
            If docList.Count > 0 Then
                For Each Doc As clsTEMP_CORRECT_DOC In docList
                    Dim StrSeq As String = Doc.Doc_No
                    Dim qry As String = "select Doc_No,Doc_Date,Gate_Entry_No from TSpl_Gate_Out where Doc_No like '" + StrSeq + "%' order by Doc_Date"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        'qry = "select Doc_No from TSpl_Gate_Out where Doc_No like '" + StrSeq + "%' order by Doc_No"
                        'Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtOld.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Doc_No"))

                            Dim strLoc As String = clsDBFuncationality.getSingleValue("select location_code from Tspl_Gate_Entry_Details  where gate_entry_no='" & clsCommon.myCstr(dtOld.Rows(ii)("Gate_Entry_No")) & "'", trans)
                            Dim strNewCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.myCDate(dtOld.Rows(ii)("Doc_Date")), "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.GateOut, clsDocTransactionType.NA, strLoc)

                            If clsCommon.myLen(strNewCode) <= 0 Then
                                Throw New Exception("Error in code generation")
                            End If
                            qry = "select 1 from TSpl_Gate_Out where Doc_No='" + strNewCode + "' "
                            Dim dtDocExists As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtDocExists IsNot Nothing AndAlso dtDocExists.Rows.Count > 0 Then
                                Throw New Exception("Doc No - " + clsCommon.myCstr(dtOld.Rows(ii)("Doc_No")) + " it's  New Doc No " + strNewCode + " is already exists in the system")
                            End If



                            clsCommon.AddColumnsForChange(coll, "NEW_NO", strNewCode)
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
                            UpdateBulkPurchaseGateout(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkPurchaseGateout(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryGateOut_Corr','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkPurchaseGateout(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSpl_Gate_Out set Doc_No='" + strToBeReplaceNo + "' where Doc_No='" + strFindNo + "'", trans)
    End Sub

    Private Sub txtUnloading__My_Click(sender As Object, e As EventArgs) Handles txtUnloading._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Unloading_No,1,len(Unloading_No)-" + clsCommon.myCstr(txtSplit.Value) + ") as DocNo  from TSPL_MILK_UNLOADING)xx group by DocNo order by DocNo"
            txtUnloading.arrValueMember = clsCommon.ShowMultipleSelectForm("txtUnloading", qry, "DocNo", "", txtUnloading.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        'If txtUnloading.arrValueMember Is Nothing OrElse txtUnloading.arrValueMember.Count <= 0 Then
        '    clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
        '    Exit Sub
        'End If

        Dim docList As New List(Of clsTEMP_CORRECT_DOC)
        Dim obj As New clsTEMP_CORRECT_DOC
        If txtUnloading.arrValueMember Is Nothing OrElse txtUnloading.arrValueMember.Count <= 0 Then
            Dim qry As String = "select * from TEMP_CORRECT_DOC"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim count As Integer = dt.Rows.Count
            If count <= 0 Then
                clsCommon.MyMessageBoxShow("Please Pick documents", Me.Text)
                Exit Sub
            Else
                For Each dr As DataRow In dt.Rows
                    obj = New clsTEMP_CORRECT_DOC
                    obj.Doc_No = clsCommon.myCstr(dr.Item("Doc_No"))
                    obj.DOC_DATE = clsCommon.myCstr(dr.Item("DOC_DATE"))
                    docList.Add(obj)
                Next
            End If
        Else
            'docList = txtUnloading.arrValueMember.Cast(Of clsTEMP_CORRECT_DOC).ToList
            For Each StrDoc As String In txtUnloading.arrValueMember
                obj = New clsTEMP_CORRECT_DOC
                obj.Doc_No = StrDoc
                docList.Add(obj)
            Next
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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

            If docList.Count > 0 Then
                For Each Doc As clsTEMP_CORRECT_DOC In docList
                    Dim StrSeq As String = Doc.Doc_No
                    Dim qry As String = "select Unloading_No,Unloading_Date_Time,location_Code from TSPL_MILK_UNLOADING where Unloading_No like '" + StrSeq + "%' order by Unloading_Date_Time"
                    Dim dtOld As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                    If dtOld IsNot Nothing AndAlso dtOld.Rows.Count > 0 Then
                        'qry = "select Unloading_No from TSPL_MILK_UNLOADING where Unloading_No like '" + StrSeq + "%' order by Unloading_No"
                        'Dim dtNew As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                        For ii As Integer = 0 To dtOld.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dtOld.Rows.Count, "Collecting Information of [" + StrSeq + "] " + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dtOld.Rows.Count))
                            Dim coll As New Hashtable()
                            clsCommon.AddColumnsForChange(coll, "S_NO", strCounter)
                            clsCommon.AddColumnsForChange(coll, "OLD_NO", dtOld.Rows(ii)("Unloading_No"))

                            Dim strNewCode As String = clsERPFuncationality.GetNextCode(trans, clsCommon.GetPrintDate(clsCommon.myCDate(dtOld.Rows(ii)("Unloading_Date_Time")), "dd/MMM/yyyy hh:mm:ss tt"), clsDocType.Unloading, "", clsCommon.myCstr(dtOld.Rows(ii)("location_Code")))
                            If clsCommon.myLen(strNewCode) <= 0 Then
                                Throw New Exception("Error in code generation")
                            End If
                            qry = "select 1 from TSPL_MILK_UNLOADING where Unloading_No='" + strNewCode + "' "
                            Dim dtDocExists As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
                            If dtDocExists IsNot Nothing AndAlso dtDocExists.Rows.Count > 0 Then
                                Throw New Exception("Doc No - " + clsCommon.myCstr(dtOld.Rows(ii)("Unloading_No")) + " it's  New Doc No " + strNewCode + " is already exists in the system")
                            End If


                            clsCommon.AddColumnsForChange(coll, "NEW_NO", strNewCode)
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
                            UpdateBulkPurchaseUnloading(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateBulkPurchaseUnloading(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'BulkEntryUnloading','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateBulkPurchaseUnloading(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_MILK_UNLOADING set Unloading_No='" + strToBeReplaceNo + "' where Unloading_No='" + strFindNo + "'", trans)
    End Sub

    Private Sub txtReceipt__My_Click(sender As Object, e As EventArgs) Handles txtReceipt._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Receipt_No,0,len(Receipt_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_RECEIPT_HEADER)xx group by DocNo order by DocNo"
            txtReceipt.arrValueMember = clsCommon.ShowMultipleSelectForm("txtReceipt", qry, "DocNo", "", txtReceipt.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton8_Click(sender As Object, e As EventArgs) Handles RadButton8.Click
        If txtReceipt.arrValueMember Is Nothing OrElse txtReceipt.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateReceipt(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateReceipt(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'ReceiptEntry','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateReceipt(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_RECEIPT_HEADER set Receipt_No='" + strToBeReplaceNo + "' where Receipt_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_RECEIPT_DETAIL set Receipt_No='" + strToBeReplaceNo + "' where Receipt_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("delete from TSPL_BANK_BOOK where SOURCEDOC_NO='" + strFindNo + "' and DocType='Receipt'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_BANK_REVERSE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "' and Source_Type='AR'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_BankReco_Detail set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "' and Document_Type='Receipt'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "' where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_RECEIPT_HEADER set Applied_Receipt='" + strToBeReplaceNo + "' where Applied_Receipt='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Uncleared_Doc_Detail set Cleared_Doc_No='" + strToBeReplaceNo + "' where Cleared_Doc_No='" + strFindNo + "' and exists(select 1 from TSPL_UNCLEARED_DOC_HEAD where TSPL_UNCLEARED_DOC_HEAD.Doc_Type='R' and TSPL_UNCLEARED_DOC_HEAD.DOC_No=TSPL_Uncleared_Doc_Detail.DOC_No )", trans)
    End Sub

    Private Sub txtPayment__My_Click(sender As Object, e As EventArgs) Handles txtPayment._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Payment_No,0,len(Payment_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_PAYMENT_HEADER)xx group by DocNo order by DocNo"
            txtPayment.arrValueMember = clsCommon.ShowMultipleSelectForm("txtPayment", qry, "DocNo", "", txtPayment.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton9_Click(sender As Object, e As EventArgs) Handles RadButton9.Click
        If txtPayment.arrValueMember Is Nothing OrElse txtPayment.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdatePayment(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdatePayment(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'PaymentEntry','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdatePayment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_PAYMENT_HEADER set Payment_No='" + strToBeReplaceNo + "' where Payment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PAYMENT_DETAIL set Payment_No='" + strToBeReplaceNo + "' where Payment_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("delete from TSPL_BANK_BOOK where SOURCEDOC_NO='" + strFindNo + "' and DocType='Payment'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_BANK_REVERSE set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "' and Source_Type='AP'", trans)
        ExecuteNonQueryWithDropContraint("update tspl_BankReco_Detail set Document_No='" + strToBeReplaceNo + "' where Document_No='" + strFindNo + "' and Document_Type='Payment'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set Source_Doc_No='" + strToBeReplaceNo + "',Source_Narration=REPLACE( Source_Narration,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_PAYMENT_HEADER set Applied_Payment='" + strToBeReplaceNo + "' where Applied_Payment='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_Uncleared_Doc_Detail set Cleared_Doc_No='" + strToBeReplaceNo + "' where Cleared_Doc_No='" + strFindNo + "' and exists(select 1 from TSPL_UNCLEARED_DOC_HEAD where TSPL_UNCLEARED_DOC_HEAD.Doc_Type='P' and TSPL_UNCLEARED_DOC_HEAD.DOC_No=TSPL_Uncleared_Doc_Detail.DOC_No )", trans)
    End Sub

    Private Sub txtStockAdjustment__My_Click(sender As Object, e As EventArgs) Handles txtStockAdjustment._My_Click
        Try
            Dim qry As String = "select DocNo,sum(1) as Transactions from (select SUBSTRING( Adjustment_No,0,len(Adjustment_No)-" + clsCommon.myCstr(txtSplit.Value - 1) + ") as DocNo  from TSPL_ADJUSTMENT_HEADER)xx group by DocNo order by DocNo"
            txtStockAdjustment.arrValueMember = clsCommon.ShowMultipleSelectForm("txtSAdjustment", qry, "DocNo", "", txtStockAdjustment.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton10_Click(sender As Object, e As EventArgs) Handles RadButton10.Click
        If txtStockAdjustment.arrValueMember Is Nothing OrElse txtStockAdjustment.arrValueMember.Count <= 0 Then
            clsCommon.MyMessageBoxShow("Please select Sequence", Me.Text)
            Exit Sub
        End If

        If clsCommon.MyMessageBoxShow("Apply Documents Sequence." + Environment.NewLine + "Are you sure ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.No Then
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
                            UpdateStoreAdjustment(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("S_NO")), trans)
                        End If
                    Next
                    For ii As Integer = 0 To dt.Rows.Count - 1
                        clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / dt.Rows.Count, "Sequence document For Original Value" + clsCommon.myCstr(ii + 1) + "/" + clsCommon.myCstr(dt.Rows.Count))
                        If Not clsCommon.CompairString(clsCommon.myCstr(dt.Rows(ii)("OLD_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO"))) = CompairStringResult.Equal Then
                            UpdateStoreAdjustment(clsCommon.myCstr(dt.Rows(ii)("S_NO")), clsCommon.myCstr(dt.Rows(ii)("NEW_NO")), trans)
                        End If
                    Next
                End If
            End If
            clsDBFuncationality.ExecuteNonQuery("insert into TMP_DOC_SEQ_HIST select *,'StoreAdjustment','" + objCommonVar.CurrentUserCode + "',GETDATE() from TMP_DOC_SEQ", trans)
            trans.Commit()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow("Task Done successfully")
        Catch ex As Exception
            trans.Rollback()
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub UpdateStoreAdjustment(ByVal strFindNo As String, ByVal strToBeReplaceNo As String, ByVal trans As SqlTransaction)
        ExecuteNonQueryWithDropContraint("update TSPL_ADJUSTMENT_DETAIL set  Adjustment_No ='" + strToBeReplaceNo + "' where Adjustment_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_ADJUSTMENT_HEADER set  Adjustment_No ='" + strToBeReplaceNo + "' where Adjustment_No ='" + strFindNo + "'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT set  source_doc_no ='" + strToBeReplaceNo + "' where source_doc_no ='" + strFindNo + "' and trans_type = 'IC-AD'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_INVENTORY_MOVEMENT_NEW set  source_doc_no ='" + strToBeReplaceNo + "' where source_doc_no ='" + strFindNo + "' and trans_type = 'IC-AD'", trans)
        ExecuteNonQueryWithDropContraint("update TSPL_JOURNAL_MASTER set  Source_Doc_No='" + strToBeReplaceNo + "',Voucher_Desc=REPLACE(Voucher_Desc,'" + strFindNo + "','" + strToBeReplaceNo + "') where Source_Doc_No='" + strFindNo + "' and Source_Code = 'IC-AD'", trans)
    End Sub
    Private Sub btnResetGE_Click(sender As Object, e As EventArgs) Handles btnResetGE.Click
        txtBulkProcSeq.arrValueMember = Nothing
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC")        
    End Sub

    Private Sub btnResetWeigh_Click(sender As Object, e As EventArgs) Handles btnResetWeigh.Click
        txtWeighmentSeqGrp.arrValueMember = Nothing
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC")
    End Sub

    Private Sub btnResetQC_Click(sender As Object, e As EventArgs) Handles btnResetQC.Click
        txtBulkProcQC.arrValueMember = Nothing
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC")
    End Sub

    Private Sub btnResetSRN_Click(sender As Object, e As EventArgs) Handles btnResetSRN.Click
        txtBulkSRNSequence.arrValueMember = Nothing
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC")    
    End Sub

    Private Sub btnResetPI_Click(sender As Object, e As EventArgs) Handles btnResetPI.Click
        txtBulkPI.arrValueMember = Nothing
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC")
    End Sub

    Private Sub btnResetGateOut_Click(sender As Object, e As EventArgs) Handles btnResetGateOut.Click
        txtGateout.arrValueMember = Nothing
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC")        
    End Sub

    Private Sub btnResetUnl_Click(sender As Object, e As EventArgs) Handles btnResetUnl.Click
        txtUnloading.arrValueMember = Nothing
        clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC")    
    End Sub

    Private Sub btnPickGENo_Click(sender As Object, e As EventArgs) Handles btnPickGENo.Click
        txtBulkProcSeq.arrValueMember = Nothing
        ImportDoc()
    End Sub
    Sub ImportDoc()
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "DocNo", "DocDate") Then
            Try
                clsDBFuncationality.ExecuteNonQuery("delete from TEMP_CORRECT_DOC")
            Catch ex As Exception
            End Try

            Dim trans As SqlTransaction = Nothing
            Try
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim strDocNo As String = clsCommon.myCstr(grow.Cells(0).Value)
                    Dim strDATE As String = clsCommon.myCstr(grow.Cells(1).Value)
                    'Dim strJVNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where TSPL_JOURNAL_MASTER.Source_Doc_No='" + strDocNo + "' ", trans))
                    'If clsCommon.myLen(strDocNo) > 0 AndAlso clsCommon.myLen(strJVNo) > 0 Then
                    If clsCommon.myLen(strDocNo) > 0 Then
                        Dim Qry As String = "INSERT Into TEMP_CORRECT_DOC (Doc_No,Doc_Date) values('" + strDocNo + "', '" + strDATE + "')"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                Me.Controls.Remove(gv)
            End Try
        End If
    End Sub

    Private Sub btnPickWE_Click(sender As Object, e As EventArgs) Handles btnPickWE.Click
        txtWeighmentSeqGrp.arrValueMember = Nothing
        ImportDoc()
    End Sub

    Private Sub btnPickQC_Click(sender As Object, e As EventArgs) Handles btnPickQC.Click
        txtBulkProcQC.arrValueMember = Nothing
        ImportDoc()
    End Sub

    Private Sub btnPickSRN_Click(sender As Object, e As EventArgs) Handles btnPickSRN.Click
        txtBulkSRNSequence.arrValueMember = Nothing
        ImportDoc()
    End Sub

    Private Sub btnPickPI_Click(sender As Object, e As EventArgs) Handles btnPickPI.Click
        txtBulkPI.arrValueMember = Nothing
        ImportDoc()
    End Sub

    Private Sub btnPickGOut_Click(sender As Object, e As EventArgs) Handles btnPickGOut.Click
        txtGateout.arrValueMember = Nothing
        ImportDoc()
    End Sub

    Private Sub btnPickUnl_Click(sender As Object, e As EventArgs) Handles btnPickUnl.Click
        txtUnloading.arrValueMember = Nothing
        ImportDoc()
    End Sub
End Class
Public Class clsTEMP_CORRECT_DOC
    Public Doc_No As String
    Public DOC_DATE As String
End Class