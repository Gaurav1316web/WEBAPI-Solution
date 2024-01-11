''Created by pradeep on 09/01/2014 @ ticet no BM00000001362

Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports common
Imports System.Collections.Specialized


Public Class ucAttachment
    Public RunServiceForUploadFolder As Boolean = False
    Public isDeleteTheAttachment As Boolean = True
    Public MandatoryPDFFile As Boolean = False
    Public MandatoryPDFFileAny As Boolean = False
    Public settAutoAttachment As Boolean = False
    Public Form_ID As String = ""
    Public Transaction_ID As String = ""
    Dim obj As clsAttachDocument
    Dim isInsideLoadData As Boolean = False

    ''
    Const ColCODE As String = "CODE"
    Const ColFormId As String = "FormId"
    Const ColTransactionId As String = "TransactionId"
    Const ColSNo As String = "SNo"
    Const ColFileName As String = "FileName"
    Const ColCOMMENTS As String = "COMMENTS"
    Const ColPath As String = "ColPath"
    Const ColUser As String = "ColUser"
    Const ColView As String = "View"
    Const ColDelete As String = "Delete"
    Const ColSelect As String = "ColSelect"

    Public Sub LoadData(ByRef StrTransaction_ID As String)
        LoadData(StrTransaction_ID, Nothing)
    End Sub
    Public Sub LoadData(ByRef StrTransaction_ID As String, ByVal trans As SqlTransaction)
        BlankAllControls()
        Me.Transaction_ID = StrTransaction_ID
        Dim Counter As Integer = 0
        obj = New clsAttachDocument
        Dim dt As DataTable = clsAttachDocument.GetData(Form_ID, Transaction_ID, trans)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gv1.Rows.AddNew()
                gv1.Rows(Counter).Cells(ColCODE).Value = dr("CODE")
                gv1.Rows(Counter).Cells(ColFormId).Value = dr("FormId")
                gv1.Rows(Counter).Cells(ColTransactionId).Value = dr("TransactionId")
                gv1.Rows(Counter).Cells(ColSNo).Value = dr("SNo")
                gv1.Rows(Counter).Cells(ColFileName).Value = dr("FileName")
                gv1.Rows(Counter).Cells(ColCOMMENTS).Value = dr("COMMENTS")
                gv1.Rows(Counter).Cells(ColUser).Value = clsCommon.myCstr(dr("Attach_By"))
                Counter += 1
            Next
        End If
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub


    Public Function SaveData(ByRef StrTransaction_ID As String) As Boolean
        Return SaveData(StrTransaction_ID, False, Nothing)
    End Function

    Public Function SaveData(ByRef StrTransaction_ID As String, ByVal isDeleteFirst As Boolean, ByVal trans As SqlTransaction) As Boolean
        Me.Transaction_ID = StrTransaction_ID
        Try
            If isDeleteFirst Then
                isDeleteFirst = False
                For ii As Integer = 0 To gv1.Rows.Count - 1
                    If clsCommon.myLen(gv1.Rows(ii).Cells(ColPath).Value) > 0 Then
                        isDeleteFirst = True
                        Exit For
                    End If
                Next
                If isDeleteFirst Then
                    clsDBFuncationality.ExecuteNonQuery("delete from TSPL_ATTACHMENTS where FormId='" + Form_ID + "' and TransactionId='" + StrTransaction_ID + "' ", trans)
                End If
            End If

            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(ColFileName).Value) > 0 Then
                    obj = New clsAttachDocument
                    obj.CODE = clsCommon.myCstr(gv1.Rows(ii).Cells(ColCODE).Value)
                    obj.FormId = Form_ID
                    obj.TransactionId = Transaction_ID
                    obj.SNo = clsCommon.myCstr(gv1.Rows(ii).Cells(ColSNo).Value)
                    obj.FileName = clsCommon.myCstr(gv1.Rows(ii).Cells(ColFileName).Value)
                    obj.COMMENTS = clsCommon.myCstr(gv1.Rows(ii).Cells(ColCOMMENTS).Value)
                    obj.SaveData(obj, trans)
                    Dim FileNo As Integer = 0
                    If clsCommon.myLen(gv1.Rows(ii).Cells(ColPath).Value) > 0 Then
                        Dim str As String
                        If RunServiceForUploadFolder Then
                            str = clsAttachDocument.UploadWithHttpRequest(clsCommon.myCstr(gv1.Rows(ii).Cells(ColPath).Value), obj.FileName)
                            Dim jObj As JObject = JObject.Parse(str)
                            Dim ArrJ As JArray = Nothing
                            If clsCommon.CompairString(clsCommon.myCstr(jObj.SelectToken("result")), "true") = CompairStringResult.Equal Then
                                ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
                                If clsCommon.myCDecimal(ArrJ(0).SelectToken("Result")) > 0 Then
                                    FileNo = clsCommon.myCDecimal(ArrJ(0).SelectToken("Result"))
                                Else
                                    Throw New Exception(ArrJ(0).SelectToken("Message"))
                                End If
                            Else
                                ArrJ = JArray.Parse(clsCommon.myCstr(jObj.SelectToken("data")))
                                Throw New Exception(ArrJ(0).SelectToken("Message"))
                            End If
                        End If

                        Dim bData As Byte()
                        Dim br As BinaryReader = New BinaryReader(System.IO.File.OpenRead(clsCommon.myCstr(gv1.Rows(ii).Cells(ColPath).Value)))
                        bData = br.ReadBytes(br.BaseStream.Length)

                        str = " UPDATE TSPL_ATTACHMENTS set FileData = @BLOBData "
                        If FileNo > 0 Then
                            str += " ,FILE_INFO=" + clsCommon.myCstr(FileNo) + ""
                        End If
                        str += " where CODE='" + obj.CODE + "'"

                        Dim cmd As SqlCommand = New SqlCommand(str, clsDBFuncationality.GetConnnection)
                        Dim prm As New SqlParameter("@BLOBData", bData)
                        cmd.Transaction = trans
                        cmd.Parameters.Add(prm)
                        cmd.ExecuteNonQuery()
                        br.Close() ' done by stuti reagrding memory leakage
                    End If
                End If
            Next
            LoadData(Transaction_ID, trans)
        Catch ex As Exception
            If trans Is Nothing Then
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Else
                Throw New Exception(ex.Message)
            End If
            Return False
        End Try
        Return True
    End Function







    Public Function AllowToSave() As Boolean
        If MandatoryPDFFile Then
            Dim flag As Boolean = False
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(ColFileName).Value) > 0 Then
                    If clsCommon.myLen(gv1.Rows(ii).Cells(ColPath).Value) > 0 Then
                        If clsCommon.myCstr(gv1.Rows(ii).Cells(ColFileName).Value).ToUpper().EndsWith(".PDF") Then
                            flag = True
                            Exit For
                        End If
                    End If
                End If
            Next
            If Not flag Then
                Throw New Exception("Please attach PDF file")
            End If
        End If
        If MandatoryPDFFileAny Then
            Dim flag As Boolean = False
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(ColFileName).Value) > 0 Then
                    If clsCommon.myCstr(gv1.Rows(ii).Cells(ColFileName).Value).ToUpper().EndsWith(".PDF") Then
                        flag = True
                        Exit For
                    End If
                End If
            Next
            If Not flag Then
                Throw New Exception("Please attach PDF file")
            End If
        End If
        Return True
    End Function

    Public Sub BlankAllControls()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.ReadOnly = False

        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Code"
        repoCode.Width = 100
        repoCode.Name = ColCODE
        repoCode.ReadOnly = True
        repoCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoFormId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFormId.FormatString = ""
        repoFormId.HeaderText = "Form Id"
        repoFormId.Width = 100
        repoFormId.Name = ColFormId
        repoFormId.ReadOnly = True
        repoFormId.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoFormId)

        Dim repoTransactionId As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTransactionId.FormatString = ""
        repoTransactionId.HeaderText = "Transaction Id"
        repoTransactionId.Width = 100
        repoTransactionId.Name = ColTransactionId
        repoTransactionId.ReadOnly = False
        repoTransactionId.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTransactionId)

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = ColSNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsVisible = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoGroupCod As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGroupCod.FormatString = ""
        repoGroupCod.HeaderText = "File Name"
        repoGroupCod.Width = 200
        repoGroupCod.Name = ColFileName
        repoGroupCod.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGroupCod)

        Dim repoNote As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNote.FormatString = ""
        repoNote.HeaderText = "Comments"
        repoNote.Width = 200
        repoNote.Name = ColCOMMENTS
        repoNote.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoNote)

        Dim repoPath As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoPath.FormatString = ""
        repoPath.HeaderText = "Path"
        repoPath.Width = 100
        repoPath.Name = ColPath
        repoPath.ReadOnly = True
        repoPath.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPath)

        repoPath = New GridViewTextBoxColumn()
        repoPath.FormatString = ""
        repoPath.HeaderText = "Attach By"
        repoPath.Width = 100
        repoPath.Name = ColUser
        repoPath.ReadOnly = True
        repoPath.IsVisible = MandatoryPDFFile
        gv1.MasterTemplate.Columns.Add(repoPath)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show"
        ShowBtn.HeaderText = "Show"
        ShowBtn.Name = ColView
        ShowBtn.FieldName = ColCODE
        ShowBtn.Width = 70
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(ShowBtn)

        Dim repoSelect As GridViewCommandColumn = New GridViewCommandColumn()
        repoSelect.FormatString = ""
        repoSelect.UseDefaultText = True
        repoSelect.DefaultText = "ADD"
        repoSelect.HeaderText = "ADD"
        repoSelect.Width = 70
        repoSelect.Name = ColSelect
        repoSelect.IsVisible = False
        repoSelect.FieldName = ColCODE
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoSelect)

        Dim repoType As GridViewCommandColumn = New GridViewCommandColumn()
        repoType.FormatString = ""
        repoType.UseDefaultText = True
        repoType.DefaultText = "Delete"
        repoType.HeaderText = "Delete"
        repoType.Width = 100
        repoType.Name = ColDelete
        repoType.FieldName = ColCODE
        repoType.IsVisible = isDeleteTheAttachment
        repoType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoType)


        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AutoHide
        gv1.HorizontalScrollState = ScrollState.AutoHide
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True

    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        'If gv1.RowCount > 0 Then
        '    Dim intCurrRow As Integer = gv1.CurrentRow.Index
        '    gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        '    If intCurrRow = gv1.Rows.Count - 1 Then
        '        gv1.Rows.AddNew()
        '        gv1.CurrentRow = gv1.Rows(intCurrRow)  
        '    End If
        'End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(ColSNo).Value = i + 1
            End If
        Next
    End Sub

    Sub gv1_CommandCellClick(ByVal sender As Object, ByVal e As EventArgs) Handles gv1.CommandCellClick
        Try
            If (Not isInsideLoadData) Then
                isInsideLoadData = True
                If gv1.CurrentColumn Is gv1.Columns(ColDelete) AndAlso isDeleteTheAttachment Then
                    If clsCommon.MyMessageBoxShow(" Do you want to Delete This Document.", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                        funDelete(gv1.CurrentRow.Cells(ColCODE).Value)
                    End If
                ElseIf gv1.CurrentColumn Is gv1.Columns(ColView) Then
                    FunShow(gv1.CurrentRow.Cells(ColCODE).Value)
                ElseIf gv1.CurrentColumn Is gv1.Columns(ColSelect) Then
                    OpenFileDialog1.ShowDialog()
                    gv1.CurrentRow.Cells(ColPath).Value = OpenFileDialog1.FileName
                    gv1.CurrentRow.Cells(ColFileName).Value = OpenFileDialog1.SafeFileName
                End If
                For ii As Integer = 1 To gv1.Rows.Count
                    gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
                Next
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Sub funDelete(ByVal StrCode As String, Optional ByVal ShowMgs As Boolean = True)
        Try
            If isDeleteTheAttachment Then
                If (clsCommon.myLen(StrCode) <= 0) Then
                    clsCommon.MyMessageBoxShow(Me, "Code not found to Delete.", Me.Text)
                    Return
                End If
                Dim qry As String
                qry = " delete from TSPL_ATTACHMENTS where CODE='" + StrCode + "' "
                If clsDBFuncationality.ExecuteNonQuery(qry) Then
                    If ShowMgs Then
                        clsCommon.MyMessageBoxShow(Me, "Document Deleted.", Me.Text)
                    End If
                End If
                LoadData(Transaction_ID)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.text)
        End Try
    End Sub

    Public Sub FunShow(ByVal strCode As String)
        If (clsCommon.myLen(strCode) <= 0) Then
            clsCommon.MyMessageBoxShow(Me, "Document not found to View.", Me.Text)
            Return
        End If

        Dim ds_attachment As DataTable
        Dim filename As String = ""
        Dim file_path As String = ""
        Dim file_extn As String = ""
        Try

            ds_attachment = New DataTable
            ds_attachment = clsAttachDocument.GetDocumentByte(strCode)
            filename = clsCommon.myCstr(ds_attachment.Rows(0)("FileName"))
            Dim blob As Byte() = ds_attachment.Rows(0)("FileData")
            file_path = "C:\ERPTempFolder"
            Dim dir As DirectoryInfo = New DirectoryInfo(file_path)
            If dir.Exists = False Then
                dir.Create()
            End If
            Dim index As Int16 = filename.LastIndexOf(".")
            file_extn = filename.Substring(index)
            filename = filename.Remove(index)
            filename += (clsCommon.myCDate(clsCommon.GETSERVERDATE(), "dd/MM/yy hh:mm:ss")).ToString()
            filename = filename.Replace(" ", "")
            filename = filename.Replace("/", "_")
            filename = filename.Replace(":", "_")
            Dim fs As FileStream = File.Create(file_path + "\\" + filename + file_extn)
            fs.Write(blob, 0, blob.Length)
            fs.Close()
            System.Diagnostics.Process.Start(file_path + "\\" + filename + file_extn)

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, "Error in Downloading Documnet.", Me.Text)
        End Try
    End Sub

    Private Sub btnaddNewAttachment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnaddNewAttachment.Click
        If Not settAutoAttachment Then
            'OpenFileDialog1.ShowDialog()
            If OpenFileDialog1.ShowDialog = DialogResult.OK Then
                AddAttachment(OpenFileDialog1.FileName, OpenFileDialog1.SafeFileName)
            End If
        End If
    End Sub

    Public Function AddAttachment(ByVal FileName As String, ByVal SafeFileName As String) As Boolean
        Try
            gv1.Rows.AddNew()
            gv1.Rows(gv1.RowCount - 1).Cells(ColPath).Value = FileName
            gv1.Rows(gv1.RowCount - 1).Cells(ColFileName).Value = SafeFileName
            If gv1.RowCount > 1 Then
                gv1.Rows(gv1.RowCount - 1).Cells(ColSNo).Value = (gv1.Rows(gv1.RowCount - 2).Cells(ColSNo).Value) + 1
            Else
                gv1.Rows(gv1.RowCount - 1).Cells(ColSNo).Value = 1
            End If
            For ii As Integer = 1 To gv1.Rows.Count
                gv1.Rows(ii - 1).Cells(ColSNo).Value = ii
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, "Atttachment", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
        Return True
    End Function

    Private Sub gv1_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If Not isDeleteTheAttachment Then
            e.Cancel = True
        End If
    End Sub
End Class
