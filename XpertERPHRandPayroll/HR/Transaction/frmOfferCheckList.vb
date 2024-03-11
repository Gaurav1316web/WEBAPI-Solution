'------------------------Created By Priti Gupta Ticket No.[BM00000003525]
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports System.IO
Imports XpertERPEngine

Public Class FrmOfferCheckList
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Const colApplicantcode As String = "colApplicantcode"
    Const colOfferCode As String = "colOfferCode"
    Const colRemarks As String = "colRemarks"
    Const colDescription As String = "colDescription"
    Const colReceived As String = "colReceived"
    Const colAttachment As String = "colAttachment"
    Const colShowAttachment As String = "colShowAttachment"
    Const colAttachmentId As String = "colAttachmentId"
    Const colAttachmentpath As String = "colAttachmentpath"
    Const colOfferMandatory As String = "colOfferMandatory"
    Const ColDelete As String = "ColDelete"
    Private isInsideLoadData As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Dim is_Entered_Manually As Boolean = False
    Dim openFileDialog1 As New OpenFileDialog
    Dim isFlag As Boolean = False
    Private Const ReportID As String = "Off_Chk"
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.OfferChkList)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub LoadBlankoffer()

        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim received As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        received.FormatString = ""
        received.HeaderText = " Received"
        received.Name = colReceived
        received.Width = 70
        received.IsVisible = True
        received.ReadOnly = False
        gv.MasterTemplate.Columns.Add(received)

        Dim applicantcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        applicantcode.FormatString = ""
        applicantcode.HeaderText = "Applicant Code"
        applicantcode.Name = colApplicantcode
        applicantcode.Width = 100
        applicantcode.IsVisible = False
        applicantcode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(applicantcode)

        Dim repochkcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repochkcode.FormatString = ""
        repochkcode.HeaderText = "Joining Code"
        repochkcode.Name = colOfferCode
        repochkcode.Width = 100
        repochkcode.IsVisible = True
        repochkcode.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repochkcode)



        Dim repoDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDescription.FormatString = ""
        repoDescription.HeaderText = "Description"
        repoDescription.Name = colDescription
        repoDescription.Width = 200
        repoDescription.IsVisible = True
        repoDescription.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoDescription)

        Dim reporemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reporemarks.FormatString = ""
        reporemarks.HeaderText = "Remarks"
        reporemarks.Name = colRemarks
        reporemarks.Width = 160
        reporemarks.IsVisible = True
        reporemarks.ReadOnly = False
        gv.MasterTemplate.Columns.Add(reporemarks)

        Dim repoAttachment As GridViewCommandColumn = New GridViewCommandColumn()
        repoAttachment.FormatString = ""
        repoAttachment.UseDefaultText = True
        repoAttachment.DefaultText = "Add"
        repoAttachment.HeaderText = "Add"
        repoAttachment.Width = 70
        repoAttachment.Name = colAttachment
        'repoSelect.IsVisible = False
        repoAttachment.FieldName = colAttachment
        repoAttachment.ImageAlignment = ContentAlignment.MiddleCenter
        repoAttachment.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoAttachment)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "Show"
        ShowBtn.HeaderText = "Show"
        ShowBtn.Name = colShowAttachment
        ShowBtn.FieldName = colShowAttachment
        ShowBtn.Width = 70
        ShowBtn.ImageAlignment = ContentAlignment.MiddleCenter
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.Columns.Add(ShowBtn)

        Dim repoDelete As GridViewCommandColumn = New GridViewCommandColumn()
        repoDelete.FormatString = ""
        repoDelete.UseDefaultText = True

        repoDelete.DefaultText = "Delete"
        repoDelete.HeaderText = "Delete"
        repoDelete.Width = 100
        repoDelete.Name = ColDelete
        repoDelete.FieldName = ColDelete
        repoDelete.Width = 70
        repoDelete.ImageAlignment = ContentAlignment.MiddleCenter
        repoDelete.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv.MasterTemplate.Columns.Add(repoDelete)


        Dim repoAttachment_Id As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAttachment_Id.FormatString = ""
        repoAttachment_Id.HeaderText = "Attachment_id"
        repoAttachment_Id.Name = colAttachmentId
        repoAttachment_Id.Width = 0
        repoAttachment_Id.ReadOnly = True
        repoAttachment_Id.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoAttachment_Id)

        Dim repoAttachmentpath As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAttachmentpath.FormatString = ""
        repoAttachmentpath.HeaderText = "Attachment Path"
        repoAttachmentpath.Name = colAttachmentpath
        repoAttachmentpath.Width = 330
        repoAttachmentpath.ReadOnly = True
        repoAttachmentpath.IsVisible = True
        repoAttachmentpath.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repoAttachmentpath)

        Dim Offer As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Offer.FormatString = ""
        Offer.HeaderText = "Offer Mandatory"
        Offer.Name = colOfferMandatory
        Offer.Width = 100
        Offer.IsVisible = False
        Offer.ReadOnly = False
        gv.MasterTemplate.Columns.Add(Offer)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = True
        gv.EnableFiltering = True
        gv.EnableAlternatingRowColor = True
        gv.AutoSizeRows = False
        gv.AllowRowResize = True
        'gv.VerticalScrollState = ScrollState.AlwaysShow
        'gv.HorizontalScrollState = ScrollState.AlwaysShow
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
        gv.ShowFilteringRow = True
        ReStoreGridLayout()
    End Sub
    'Sub LoadData()
    '    LoadBlankoffer()
    '    Dim qry As String = ""

    '    Try
    '        qry = "select TSPL_HR_Offer_ChkList_JOB_TITLE.Chk_Code,TSPL_HR_Check_List.Chk_Description	"
    '        qry += " from TSPL_HR_APPLICANT_ENTRY "
    '        qry += " left outer join TSPL_HR_REQUISITION on TSPL_HR_REQUISITION.Requisition_Code=TSPL_HR_APPLICANT_ENTRY.Requisition_Code"
    '        qry += " left outer join TSPL_HR_Offer_ChkList_JOB_TITLE on TSPL_HR_Offer_ChkList_JOB_TITLE.Job_Title_Code=TSPL_HR_REQUISITION.Job_Title_Code"
    '        qry += " left outer join TSPL_HR_Check_List on TSPL_HR_Check_List.Chk_Code=TSPL_HR_Offer_ChkList_JOB_TITLE.Chk_Code "

    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '        'For Each dr As DataRow In dt.Rows
    '        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '            For i As Integer = 0 To dt.Rows.Count - 1
    '                gv.Rows.AddNew()
    '                'gv.CurrentRow.Cells(colReceived).Value = clsCommon.myCstr(dt.Rows(0)(colReceived))
    '                gv.CurrentRow.Cells(colOfferCode).Value = clsCommon.myCstr(dt.Rows(i)("Chk_Code"))
    '                gv.CurrentRow.Cells(colDescription).Value = clsCommon.myCstr(dt.Rows(i)("Chk_Description"))
    '                'gv.CurrentRow.Cells(colRemarks).Value = clsCommon.myCstr(dt.Rows(0)(colRemarks))
    '                'gv.CurrentRow.Cells(colAttachment).Value = clsCommon.myCstr(dt.Rows(0)(colAttachment))
    '            Next
    '        End If
    '        'Next

    '        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
    '            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
    '            Exit Sub
    '        End If

    '    Catch ex As Exception
    '        Throw New Exception(ex.Message)

    '    End Try
    'End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        btnSave.Enabled = True
        fndaccountsetcode.Value = Nothing
        LoadBlankoffer()
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim obj As clsOfferChkListHead = clsOfferChkListHead.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then

            isNewEntry = False
            Reset()
            fndaccountsetcode.Value = obj.ApplicantCode
            Txtdate.Value = obj.Offerdate
            fndaccountsetcode.MyReadOnly = True

            btnPost.Enabled = True
            'btndelete.Enabled = True
            If obj.Posted = ERPTransactionStatus.Approved Then
                btnSave.Enabled = False
                btnPost.Enabled = False

            End If
            UsLock1.Status = obj.Posted

            UcRequisitionDetail1.AppCode = fndaccountsetcode.Value
            UcRequisitionDetail1.RefreshData()

            If Not IsNothing(obj.ObjList) Then
                Dim counter As Integer = 0
                For Each objtr As clsofferChkListDetails In obj.ObjList


                    gv.Rows.AddNew()
                    gv.Rows(counter).Cells(colReceived).Value = objtr.Received
                    gv.Rows(counter).Cells(colApplicantcode).Value = objtr.Applicant_Code
                    gv.Rows(counter).Cells(colAttachmentId).Value = objtr.Attachment
                    gv.Rows(counter).Cells(colDescription).Value = objtr.JoiningDescription
                    gv.Rows(counter).Cells(colOfferCode).Value = objtr.Chk_code
                    gv.Rows(counter).Cells(colOfferMandatory).Value = objtr.OfferMandatory
                    gv.Rows(counter).Cells(colShowAttachment).Value = "show Attachment"
                    gv.Rows(counter).Cells(colAttachment).Value = "Add Attachment"
                    gv.Rows(counter).Cells(colRemarks).Value = objtr.Remarks
                    gv.Rows(counter).Cells(colAttachmentpath).Value = objtr.FileName
                    counter += 1
                Next
            Else
                gv.Rows.AddNew()
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
            fndaccountsetcode.Value = obj.ApplicantCode

            ' trans.Commit()
        Else
            Reset()
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        gv.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = ReportID
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv.SaveLayout(obj.GridLayout)
        obj.GridColumns = gv.ColumnCount
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
        End If
        ''richa agarwal regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
        ''---------------
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout Deleted successfully", "Information", Me.Text)
        End If
    End Sub

    Private Sub FrmOfferCheckList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankoffer()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save ")

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        Reset()

        TxtDate.Value = clsCommon.GETSERVERDATE()
        lbldate.Visible = False
        TxtDate.Visible = False
        'LoadData()
        btnPost.Enabled = False
        btnRejected.Visible = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        If clsCommon.myLen(fndaccountsetcode.Value) > 0 Then
            PostData()
        Else
            clsCommon.MyMessageBoxShow(Me, "code not found to post", Me.Text)
        End If
    End Sub
    'Sub reject()
    '    Try
    '        Dim qry As String = "  update TSPL_HR_OFFER_CHECK_LIST_HEAD set Rejected='1' where APPLICANT_CODE ='" + fndaccountsetcode.Value + "'"
    '        clsDBFuncationality.ExecuteNonQuery(qry)



    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.ToString)
    '    End Try
    'End Sub
    'Private Sub btnRejected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRejected.Click
    '    reject()
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub fndaccountsetcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndaccountsetcode._MYValidating
        Try
            'Dim qry As String = "select  APPLICANT_CODE As Code from TSPL_HR_INTERVIEW_FEEDBACK"
            'fndaccountsetcode.Value = clsCommon.ShowSelectForm("Appliicant_Code", qry, "Code", "Posted = 1 and Final_Action = 'H'", fndaccountsetcode.Value, "CODE", isButtonClicked)
            fndaccountsetcode.Value = ClsInterviewFeedback.GetFinder(" ", fndaccountsetcode.Value, isButtonClicked)
            If clsCommon.myLen(fndaccountsetcode.Value) > 0 Then
                LoadData(fndaccountsetcode.Value, NavigatorType.Current)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub fndaccountsetcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndaccountsetcode._MYNavigator
        LoadData(fndaccountsetcode.Value, NavType)
       
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If fndaccountsetcode.Value = "" Then
                myMessages.blankValue("Applicant Code")

                fndaccountsetcode.Focus()
                fndaccountsetcode.Select()
                Errorcontrol.SetError(fndaccountsetcode, "Applicant Code")
                Return False
            Else
                Errorcontrol.ResetError(fndaccountsetcode)
            End If
            gv.EndEdit()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub SaveData()

        Try
            If (AllowToSave()) Then

                Dim objHead As clsOfferChkListHead
                objHead = New clsOfferChkListHead
                objHead.ApplicantCode = clsCommon.myCstr(fndaccountsetcode.Value)
                objHead.Offerdate = TxtDate.Value

                Dim objList As New List(Of clsofferChkListDetails)
                Dim objListAttachment As New List(Of clsOfferAttachment)



                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As clsofferChkListDetails
                    obj = New clsofferChkListDetails

                    If clsCommon.myLen(grow.Cells(colAttachmentpath).Value) > 0 And clsCommon.myCstr(grow.Cells(colAttachmentpath).Value).ToString.Contains("\") Then
                        Dim objAttachment As clsOfferAttachment

                        objAttachment = New clsOfferAttachment
                        objAttachment.Transaction_ID = clsCommon.myCstr(grow.Cells(colOfferCode).Value)

                        objAttachment.Form_ID = Form_ID
                        objAttachment.ColCOMMENTS = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        objAttachment.ColFileName = clsCommon.myCstr(grow.Cells(colAttachmentpath).Value.ToString.Substring(grow.Cells(colAttachmentpath).Value.ToString.LastIndexOf("\") + 1, grow.Cells(colAttachmentpath).Value.ToString.Length - grow.Cells(colAttachmentpath).Value.ToString.LastIndexOf("\") - 1))
                        objAttachment.ColFormId = Form_ID
                        objAttachment.ColPath = clsCommon.myCstr(grow.Cells(colAttachmentpath).Value) 'IIf(clsCommon.myLen(grow.Cells(col_Attachment_ID).Value) <= 0, clsCommon.myCstr(grow.Cells(colVLC_Procurement_Data_MP).Value), "")
                        objAttachment.ColTransactionId = clsCommon.myCstr(grow.Cells(colOfferCode).Value)
                        objListAttachment.Add(objAttachment)

                    End If
                 
                    Dim objJoin As New clsofferChkListDetails
                    If clsCommon.myCBool(grow.Cells(colOfferMandatory).Value) = True And clsCommon.myCBool(grow.Cells(colReceived).Value) = False Then
                        clsCommon.MyMessageBoxShow(Me, "Please check ! mandatory document (" + clsCommon.myCstr(grow.Cells(colOfferCode).Value) + ") must be received.")
                        Exit Sub
                    Else
                        objJoin.Received = clsCommon.myCdbl(grow.Cells(colReceived).Value)
                        objJoin.OfferMandatory = clsCommon.myCdbl(grow.Cells(colOfferMandatory).Value)
                        objJoin.Chk_code = clsCommon.myCstr(grow.Cells(colOfferCode).Value)
                        objJoin.Applicant_Code = clsCommon.myCstr(grow.Cells(colApplicantcode).Value)
                        objJoin.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                        'Dim qry As String = clsDBFuncationality.getSingleValue("select * from TSPL_HR_Offer_ChkList_JOB_TITLE where Chk_Code ='" + objJoin.Chk_code + "' and mandatory ='1'")
                        'If qry = 1 Then

                    End If
                    objList.Add(objJoin)
                Next

                'Next


                If clsOfferChkListHead.SaveData(objHead, objList, objListAttachment) Then
                    If Not isFlag Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                    End If
                    Me.gv.Rows.Clear()
                    LoadData(fndaccountsetcode.Value, NavigatorType.Current)
                End If
            End If


        Catch ex As Exception
            'trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub Reset()
        Txtdate.Value = clsCommon.GETSERVERDATE(Nothing)
        gv.Rows.Clear()
        fndaccountsetcode.Value = ""
        fndaccountsetcode.MyReadOnly = False
        UcRequisitionDetail1.AppCode = ""
        UcRequisitionDetail1.RefreshData()
        UsLock1.Status = ERPTransactionStatus.Pending
        btnSave.Enabled = True
        btnPost.Enabled = False

    End Sub

    Private Sub rdbtnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnreset.Click
        Reset()
    End Sub
    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If (gv.CurrentRow.Cells(colReceived).Value) = False Then
            gv.CurrentRow.Cells(colRemarks).Value = ""
            gv.CurrentRow.Cells(colAttachmentId).Value = ""
            gv.CurrentRow.Cells(colAttachmentpath).Value = ""
        Else
        End If
    End Sub
    Private Sub gv_CommandCellClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv.CommandCellClick
        Try
            If (Not isInsideLoadData) Then
                isInsideLoadData = True
                If gv.CurrentColumn Is gv.Columns(colShowAttachment) Then
                    Dim objAttachment As New ucAttachment
                    objAttachment.FunShow(gv.CurrentRow.Cells(colAttachmentId).Value)
                End If
                isInsideLoadData = False
            End If
            If (Not isInsideLoadData) Then
                isInsideLoadData = True
                If gv.CurrentColumn Is gv.Columns(ColDelete) Then
                    Dim objAttachment As New ucAttachment
                    Dim qry As String = "update TSPL_HR_JOINING_DETAIL set Attachment ='' where APPLICANT_CODE ='" + colApplicantcode + "' and Chk_Code='" + colOfferCode + "' "
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    If clsCommon.myCstr(gv.CurrentRow.Cells(colAttachmentpath).Value).ToString.Contains("\") Then
                        clsCommon.MyMessageBoxShow(Me, "Document Deleted", Me.Text)
                    Else
                        objAttachment.funDelete(gv.CurrentRow.Cells(colAttachmentId).Value)
                    End If
                    gv.CurrentRow.Cells(colAttachmentId).Value = ""
                    gv.CurrentRow.Cells(colAttachmentpath).Value = ""


                End If
                isInsideLoadData = False
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub gv_CellClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellClick
        Try
            If e.Column Is gv.Columns(colAttachment) Then
                If gv.CurrentRow.Index >= 0 AndAlso e.Row.Index >= 0 Then
                    openFileDialog1.FileName = ""

                    openFileDialog1.ShowDialog()

                    If openFileDialog1.FileName <> "" Then
                        gv.CurrentRow.Cells(colAttachmentpath).Value = openFileDialog1.FileName
                    End If
                    If clsCommon.myCBool(gv.CurrentRow.Cells(colReceived).Value) = True And clsCommon.myCBool(gv.CurrentRow.Cells(colOfferMandatory).Value) = True Then
                        gv.CurrentRow.Cells(colAttachmentpath).Value = openFileDialog1.FileName
                    ElseIf clsCommon.myCBool(gv.CurrentRow.Cells(colReceived).Value) = False Then
                        clsCommon.MyMessageBoxShow(Me, "Please check the received checkBox for'" + clsCommon.myCstr(gv.CurrentRow.Cells(colOfferCode).Value) + "'")
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Applicant_Code As String = ""
            isFlag = True
            If clsCommon.myLen(fndaccountsetcode.Value) > 0 Then
                Applicant_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) AS Applicant_Code from TSPL_HR_INTERVIEW_FEEDBACK  where Applicant_Code='" + fndaccountsetcode.Value + "'"))
                If Applicant_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        SaveData()
                        If (clsOfferChkListHead.PostData(MyBase.Form_ID, fndaccountsetcode.Value)) Then
                            'msg = "Successfully Posted"
                            'common.clsCommon.MyMessageBoxShow(msg)
                            LoadData(fndaccountsetcode.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering applicant code")
                End If

            Else
                Throw New Exception("Applicant code not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub


    Private Sub FrmOfferCheckList_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

   

    Private Sub gv_RowFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.RowFormattingEventArgs) Handles gv.RowFormatting
        If e.RowElement.RowInfo.Cells(colOfferMandatory).Value = True Then
            e.RowElement.DrawFill = True
            e.RowElement.GradientStyle = GradientStyles.Solid
            e.RowElement.BackColor = Color.LemonChiffon
        End If
    End Sub
End Class

