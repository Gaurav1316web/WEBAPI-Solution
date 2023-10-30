' Developed  by pradeep on 18/09/2013  --- ticket no BM00000000472
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Data.SqlClient
Imports common
Public Class frmOperationMaster
    Inherits FrmMainTranScreen
    Dim PageMode As String
    Dim change As Boolean = True
    Const colLineNo As String = "LineNo"
    Const colWorkCode As String = "ItemCode"
    Const colDescription As String = "Description"
    Private isCellValueChangedOpen As Boolean = False
    Dim userCode, companyCode As String
    Dim isNewEntry As Boolean
    Dim dtcbo As DataTable

   
    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmOperationMaster)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            importmenu.Enabled = True
            exportmenu.Enabled = True
        Else
            importmenu.Enabled = False
            exportmenu.Enabled = False
        End If
        '--------------------------------------------------
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Public Sub SetLength()
        fndCode.MyMaxLength = 30
    End Sub
    Private Sub frmOperationMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        
        dtcbo = New DataTable
        dtcbo.Columns.Add("Code", GetType(String))
        dtcbo.Columns.Add("Value", GetType(String))

        Dim dr As DataRow
        dr = dtcbo.NewRow()
        dr("Code") = "Internal"
        dr("Value") = "Internal"
        dtcbo.Rows.Add(dr)

        dr = dtcbo.NewRow()
        dr("Code") = "Subcontract"
        dr("Value") = "Subcontract"
        dtcbo.Rows.Add(dr)
        dtcbo.AcceptChanges()
        SetLength()
        SetUserMgmtNew()
        PageMode = "New"
        ResetScreen()
        LoadGrid()
        gv1.Rows.AddNew()
        fndCode.MyCharacterCasing = CharacterCasing.Upper

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        savedata()
    End Sub

    Public Sub savedata()
        Dim isSaved As Boolean = False
        'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If ValidateSave() Then
                Dim obj As New clsOperationMaster
                Dim ObjList As New List(Of clsOperationMasterDetail)
                obj.OPERATION_CODE = clsCommon.myCstr(fndCode.Value)
                obj.OPERATION_TYPE = cboOperationType.SelectedValue
                obj.Descraption = txtDesc.Text
                obj.COMMENTS = txtComment.Text

                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colWorkCode).Value)) > 0 Then
                        Dim objtr As New clsOperationMasterDetail()
                        objtr.OPERATION_CODE = clsCommon.myCstr(fndCode.Value)
                        objtr.WORK_CENTER_CODE = clsCommon.myCstr(grow.Cells(colWorkCode).Value)
                        objtr.Descraption = clsCommon.myCstr(grow.Cells(colDescription).Value)
                        ObjList.Add(objtr)
                    End If
                Next
                obj.ObjList = ObjList

                If obj.SaveData(obj, isNewEntry, Nothing) Then
                    LoadData(obj.OPERATION_CODE, NavigatorType.Current)
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
            End If
        Catch ex As Exception
            'trans.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        fndCode.MyReadOnly = True
        Dim obj As New clsOperationMaster
        obj = obj.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.OPERATION_CODE) > 0) Then
            ResetScreen()
            isNewEntry = False
            btnSave.Text = "Update"
            Dim ii As Int16 = 0
            LoadGrid()
            fndCode.Value = obj.OPERATION_CODE
            cboOperationType.SelectedValue = clsCommon.myCstr(obj.OPERATION_TYPE)
            txtDesc.Text = clsCommon.myCstr(obj.Descraption)
            txtComment.Text = clsCommon.myCstr(obj.COMMENTS)
            txtCreatedBy.Text = clsCommon.myCstr(obj.Created_By)
            txtCreationDate.Text = clsCommon.myCstr(obj.Created_Date)
            If (obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0) Then
                isCellValueChangedOpen = True
                For Each objtr As clsOperationMasterDetail In obj.ObjList
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = gv1.Rows.Count
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colWorkCode).Value = objtr.WORK_CENTER_CODE
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = objtr.Descraption
                Next
                isCellValueChangedOpen = False
            End If
            gv1.Rows.AddNew()
        End If
    End Sub

    Private Sub ResetScreen()
        fndCode.Value = Nothing
        txtDesc.Text = ""
        isNewEntry = True
        cboOperationType.DataSource = dtcbo.Copy()
        cboOperationType.DisplayMember = "Code"
        cboOperationType.ValueMember = "Value"
        txtCreatedBy.Text = ""
        txtCreationDate.Text = ""
        txtComment.Text = ""
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        isCellValueChangedOpen = False
        btnSave.Text = "Save"
        LoadGrid()
        gv1.Rows.AddNew()
        fndCode.MyReadOnly = False
    End Sub

    Private Function ValidateSave() As Boolean
        If (IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowAutoGenerateDocNoInMaster, clsFixedParameterCode.AllowAutoGenerateDocNoInMaster, Nothing)) = "1", True, False) = False) Then
            If clsCommon.myLen(fndCode.Value) < 1 Then
                clsCommon.MyMessageBoxShow("Please enter a Operation No. ")
                Return False
            End If
        End If

        Dim IsWork As Boolean = False
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(grow.Cells(colWorkCode).Value) > 0 Then
                IsWork = True
            End If
        Next
        If Not IsWork Then
            clsCommon.MyMessageBoxShow("Insert at least one Work Center.")
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        funDelete()
    End Sub

    Public Sub funDelete()
        If clsCommon.myLen(fndCode.Value) < 1 Then
            clsCommon.MyMessageBoxShow("Please select Code to Delete.")
            Return
        ElseIf myMessages.deleteConfirm Then
            Try
                If clsOperationMaster.DeleteData(fndCode.Value) Then
                    myMessages.delete()
                    ResetScreen()
                End If
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        ResetScreen()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gv1.Columns(colWorkCode) Then
                    OpenWorkCenterList(True)
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
        End Try
    End Sub

    Sub OpenWorkCenterList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = Nothing
        Dim str_Item_Price_ID = ""
        Dim qry As String = ""
        qry += " select WORK_CENTER_CODE as [Code],DESCRIPTION, SETUP_TIME,SETUP_TIME_TYPE,RUN_TIME,RUN_TIME_TYPE,CLEANUP_TIME,CLEANUP_TIME_TYPE,WAIT_TIME,WAIT_TIME_TYPE,WORK_AREA,NO_OF_STATIONS,STD_SETUP_LABOR,STD_RUN_LABOR,STD_EFFICIENCY,STD_UTILIZATION,COMMENTS from TSPL_MF_WORK_CENTER "
        str_Item_Price_ID = clsCommon.ShowSelectForm("work_Finder", qry, "Code", "", gv1.CurrentRow.Cells(colWorkCode).Value, "Code", isButtonClick)
        gv1.CurrentRow.Cells(colWorkCode).Value = str_Item_Price_ID
        If clsCommon.myLen(str_Item_Price_ID) > 0 Then
            qry = " "
            qry += " select WORK_CENTER_CODE ,DESCRIPTION as [DESCRIPTION] from TSPL_MF_WORK_CENTER "
            qry += " where WORK_CENTER_CODE ='" + str_Item_Price_ID + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(colWorkCode).Value = clsCommon.myCstr(dt.Rows(0)("WORK_CENTER_CODE"))
                gv1.CurrentRow.Cells(colDescription).Value = clsCommon.myCstr(dt.Rows(0)("DESCRIPTION"))
            Else
                gv1.CurrentRow.Cells(colWorkCode).Value = ""
                gv1.CurrentRow.Cells(colDescription).Value = ""
            End If
        End If
    End Sub
   

    Private Sub importmenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles importmenu.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        If transportSql.importExcel(dgv, "Operation Code", "Description", "Operation Type", "Comments") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                ''connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()
                Dim obj As New clsOperationMaster()
                For Each dgrv As GridViewRowInfo In dgv.Rows
                    linno += 1
                    Dim stroperationCode As String = clsCommon.myCstr(dgrv.Cells("Operation Code").Value)
                    If clsCommon.myLen(stroperationCode) > 30 Or String.IsNullOrEmpty(stroperationCode) Then
                        Throw New Exception("Length of  Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")

                    End If

                    Dim strOperationType As String = clsCommon.myCstr(dgrv.Cells("Operation Type").Value)

                    Dim strComments As String = clsCommon.myCstr(dgrv.Cells("Comments").Value)
                    If clsCommon.myLen(stroperationCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_MF_OPERATION where OPERATION_CODE='" + stroperationCode + "' ", trans) > 0 Then
                        isNewEntry = False
                    Else
                        isNewEntry = True

                    End If


                    obj.SaveData(obj, isNewEntry, trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)


            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Sub LoadGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As New GridViewTextBoxColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Work Center No"
        repoItemCode.Name = colWorkCode
        repoItemCode.Width = 100
        repoItemCode.ReadOnly = False
        repoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDesc.FormatString = ""
        repoDesc.HeaderText = "Description"
        repoDesc.Name = colDescription
        repoDesc.Width = 200
        repoDesc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDesc)

    End Sub

    Private Sub frmOperationMaster_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            savedata()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            '    PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            funDelete()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub fndCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndCode._MYValidating
        Dim str As String = "select count(*) from TSPL_MF_OPERATION where OPERATION_CODE ='" + fndCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndCode.MyReadOnly = False
        Else
            fndCode.MyReadOnly = True
        End If
        If fndCode.MyReadOnly OrElse isButtonClicked Then

            'Dim qry As String = "select OPERATION_CODE as [Code],OPERATION_TYPE as [Type],COMMENTS as [Comments],DESCRIPTION as [Descraption] from TSPL_MF_OPERATION"
            'fndCode.Value = clsCommon.ShowSelectForm("ATTENDANCE_MASTER", qry, "Code", "", fndCode.Value, "Code", isButtonClicked)
            fndCode.Value = clsOperationMaster.getFinder("", fndCode.Value, isButtonClicked)
            If fndCode.Value <> "" Then
                LoadData(fndCode.Value, NavigatorType.Current)
            Else
                ResetScreen()
            End If
        End If
    End Sub

    Private Sub fndScheme__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndCode._MYNavigator
        Try
            LoadData(fndCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    '----preeti Gupta---Ticket No.BM00000002845--
    Private Sub rmHead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmHead.Click
        Dim gv As New RadGridView()
       
        Dim isSaved As Boolean = True

        Dim obj As clsOperationMaster
        Dim CreatedBy As String = objCommonVar.CurrentUserCode

        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Operation Code", "Description", "Operation Type", "Comments") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try


                clsCommon.ProgressBarShow()

                For Each grow As GridViewRowInfo In gv.Rows
                    trans = clsDBFuncationality.GetTransactin()
                    obj = New clsOperationMaster
                    linno += 1

                    Dim strOpercode As String = clsCommon.myCstr(grow.Cells("Operation Code").Value)
                    If (String.IsNullOrEmpty(strOpercode)) Or clsCommon.myLen(strOpercode) > 30 Then
                        Throw New Exception("Length of OT Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.OPERATION_CODE = strOpercode


                    Dim StrDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)

                    obj.Descraption = StrDescription

                    Dim strOperType As String = clsCommon.myCstr(grow.Cells("Operation Type").Value)
                    If (String.IsNullOrEmpty(strOperType)) Or clsCommon.myLen(strOperType) > 20 Then
                        Throw New Exception("Length of Slab Description should be max. 20 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    ElseIf clsCommon.CompairString(strOperType, "Internal") = CompairStringResult.Equal Or clsCommon.CompairString(strOperType, "Subcontract") = CompairStringResult.Equal Then
                    Else
                        Throw New Exception(" Operation Type should be amoung 'Internal','Subcontract' At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.OPERATION_TYPE = strOperType

                    Dim StrComments As String = clsCommon.myCstr(grow.Cells("Comments").Value)
                    Dim check As Integer = clsDBFuncationality.getSingleValue("Select count(*) from TSPL_MF_OPERATION where OPERATION_CODE='" + strOpercode + "' ", trans)

                    Dim coll As New Hashtable()
                    Try
                        clsCommon.AddColumnsForChange(coll, "comp_code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "OPERATION_TYPE", strOperType)
                        clsCommon.AddColumnsForChange(coll, "COMMENTS", StrComments)
                        clsCommon.AddColumnsForChange(coll, "DESCRIPTION", StrDescription)
                        clsCommon.AddColumnsForChange(coll, "Modified_By", CreatedBy)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(currentdate, "dd/MMM/yyyy"))
                        If check <= 0 Then
                            clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", strOpercode)
                            clsCommon.AddColumnsForChange(coll, "Created_By", CreatedBy)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(currentdate, "dd/MMM/yyyy"))
                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_OPERATION", OMInsertOrUpdate.Insert, "", trans)
                        Else

                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_OPERATION", OMInsertOrUpdate.Update, "TSPL_MF_OPERATION.OPERATION_CODE='" + obj.OPERATION_CODE + "'", trans)
                        End If
                        trans.Commit()
                    Catch ex As Exception

                        Throw New Exception(ex.Message)
                    End Try
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDetails.Click
        Dim gv As New RadGridView()
        Dim isSaved As Boolean = True
        Dim obj As clsOperationMasterDetail
        Dim OperationCode As String
        Dim WorkCenterCode As String
        Dim Description As String
        Me.Controls.Add(gv)
       
        If transportSql.importExcel(gv, "Operation Code", "Work Center Code", "Description") Then
            Dim linno As Integer = 0
           
            Try
                Dim trans As SqlTransaction = Nothing
                'clsCommon.ProgressBarShow()
                For i As Integer = 0 To gv.Rows.Count - 1
                    clsDBFuncationality.ExecuteNonQuery("delete from TSPL_MF_OPERATION_WORK_CENTER where OPERATION_CODE = '" & clsCommon.myCstr(gv.Rows(i).Cells("Operation Code").Value) & "'", trans)
                Next
                For Each grow As GridViewRowInfo In gv.Rows

                    trans = clsDBFuncationality.GetTransactin()
                    obj = New clsOperationMasterDetail
                    linno += 1
                    OperationCode = clsCommon.myCstr(grow.Cells("Operation Code").Value)
                    If clsCommon.myLen(OperationCode) <= 0 Then
                        Throw New Exception("Please Fill Scheme Code/Scheme Description")
                    End If
                    If clsCommon.myLen(OperationCode) > 0 Then
                        Dim qry As String = "select OPERATION_CODE from TSPL_MF_OPERATION where OPERATION_CODE='" + OperationCode + "'"
                        Dim OCode As String = (clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(OCode) <= 0 Then
                            Throw New Exception("Please Fill Operation Code For Operation master [" + OperationCode + "] Or Make Operation code Head Entry First")
                        End If

                    End If
                    Dim strOperationCode As String = clsCommon.myCstr(grow.Cells("Operation Code").Value)
                    If (String.IsNullOrEmpty(strOperationCode)) Or clsCommon.myLen(strOperationCode) > 30 Then
                        Throw New Exception("Length of Operation Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.OPERATION_CODE = strOperationCode

                    WorkCenterCode = clsCommon.myCstr(grow.Cells("Work Center Code").Value)
                    If clsCommon.myLen(WorkCenterCode) > 0 Then
                        Dim qry As String = "select WORK_CENTER_CODE from TSPL_MF_WORK_CENTER where WORK_CENTER_CODE='" + WorkCenterCode + "'"
                        Dim WrkCode As String = (clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(WrkCode) <= 0 Then
                            Throw New Exception("Please Fill Work center Code For Work Center master [" + WorkCenterCode + "]")
                        End If

                    End If

                    Dim strWorkCenterCode As String = clsCommon.myCstr(grow.Cells("Work Center Code").Value)
                    If (String.IsNullOrEmpty(strWorkCenterCode)) Or clsCommon.myLen(strWorkCenterCode) > 30 Then
                        Throw New Exception("Length of Work Center Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                    End If
                    obj.WORK_CENTER_CODE = strOperationCode

                    Description = clsCommon.myCstr(grow.Cells("Description").Value)
                    If clsCommon.myLen(Description) > 0 Then
                        Dim qry As String = "select DESCRIPTION from TSPL_MF_WORK_CENTER where WORK_CENTER_CODE='" + WorkCenterCode + "' and DESCRIPTION='" + Description + "'"
                        Dim Desc As String = (clsDBFuncationality.getSingleValue(qry, trans))
                        If clsCommon.myLen(Desc) <= 0 Then
                            Throw New Exception("Invalid value,Please check description in Work Center master [" + WorkCenterCode + "] for   [" + Description + "]")
                        End If

                    End If

                    Dim strDescription As String = clsCommon.myCstr(grow.Cells("Description").Value)

                    obj.Descraption = strDescription
                    'Dim Arr As List(Of clsOperationMasterDetail)
                    'If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
                    '    For Each obj As clsOperationMasterDetail In Arr
                    Dim coll As New Hashtable()
                    clsCommon.AddColumnsForChange(coll, "OPERATION_CODE", strOperationCode)
                    clsCommon.AddColumnsForChange(coll, "WORK_CENTER_CODE", strWorkCenterCode)
                    clsCommon.AddColumnsForChange(coll, "DESCRIPTION", strDescription)
                    clsCommonFunctionality.UpdateDataTable(coll, "TSPL_MF_OPERATION_WORK_CENTER", OMInsertOrUpdate.Insert, "", trans)
                    trans.Commit()

                Next


                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim query As String = "select OPERATION_CODE as [Operation Code] ,DESCRIPTION as [Description],OPERATION_TYPE as [Operation Type],COMMENTS as [Comments] from TSPL_MF_OPERATION"
        transportSql.ExporttoExcel(query, Me)
    End Sub

    Private Sub rmExdetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExdetails.Click
        Dim qry As String = "select OPERATION_CODE as [Operation Code] ,WORK_CENTER_CODE AS [Work Center Code] ,DESCRIPTION as [Description]  from TSPL_MF_OPERATION_WORK_CENTER "
        transportSql.ExporttoExcel(qry, Me)
    End Sub
    '-----------End--------------
End Class