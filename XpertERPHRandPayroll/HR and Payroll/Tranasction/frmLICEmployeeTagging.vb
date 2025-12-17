Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmLICEmployeeTagging
    Inherits FrmMainTranScreen

#Region "Variable"
    Const colLineNo As String = "LineNo"
    Const colpayHeadCode As String = "PayHeadCode"
    Const colpayHeadName As String = "PayHeadName"
    Const colPayHeadFormula As String = "Formula"
    Const colRateAmount As String = "RateAmount"
    Const colHiddenComponent As String = "HiddenComponent"
    Const colMax_Amount As String = "colMax_Amount"
    Const colPAYPERIOD_Amount As String = "colPAYPERIOD_Amount"
    Const ColPayheadtype As String = "ColPayheadtype"
    Const ColPayhead As String = "ColPayhead"
    Const colRowtype As String = "ColRowType"
    Public sal_structure_code As String = String.Empty
    Dim isNewEntry As Boolean = True
    Dim ButtonToolTip As ToolTip = New ToolTip()
    'Private Obj As clsmo
    Dim obj As New ClsEmployeeLICTagging
    Private ObjList As New List(Of ClsEmployeeLICTaggingDetails)
    Private isCellValueChangedOpen As Boolean = False
    Public isInsideLoadData As Boolean = False
    Public isCellValueChanged As Boolean = False

#End Region

    Private Sub frmLICEmployeeTagging_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim coll As Dictionary(Of String, String)

        coll = New Dictionary(Of String, String)()
        coll.Add("DOCUMENT_CODE", "Varchar(50) not null PRIMARY KEY")
        coll.Add("DOCUMENT_Date", "Datetime NOT NULL")
        coll.Add("EMP_CODE", "VARCHAR(12)  NOT NULL REFERENCES TSPL_EMPLOYEE_MASTER(EMP_CODE)")
        coll.Add("PAY_HEAD_CODE", "VARCHAR(30) NOT NULL REFERENCES TSPL_PAYHEAD_MASTER(PAY_HEAD_CODE)")
        coll.Add("APPLICABLE_FROM", "Datetime NOT NULL")
        coll.Add("APPLICABLE_TO", "Datetime NOT NULL")
        coll.Add("Total_AMT", "Decimal(18,2) NULL")
        coll.Add("Posting_Date", "Datetime NULL")
        coll.Add("POSTED", "integer null")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_EMPLOYEE_LIC_TAGGING", coll, "", True, False, "", "", "", True)

        coll = New Dictionary(Of String, String)()
        coll.Add("DOCUMENT_CODE", "VARCHAR(50) NOT NULL REFERENCES TSPL_EMPLOYEE_LIC_TAGGING(DOCUMENT_CODE)")
        coll.Add("Policy_Account_No", "Varchar(30) NOT null")
        coll.Add("LIC_PREMIUM_AMT", "Decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_EMPLOYEE_LIC_TAGGING_DETAIL", coll, "", True, False, "", "", "", True)



        SetUserMgmtNew()
        'LoadGridColumns()
        AddNew()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
    End Sub

    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmEmpSalary)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        'RadMenu2.Visible = MyBase.isExport
        'btnReverse.Visible = False
        'If MyBase.isReverse Then
        '    btnReverse.Enabled = True
        'Else
        '    btnReverse.Enabled = False
        'End If
        If MyBase.isExport = True Then
            MenuItemImport.Enabled = True
            MenuItemExport.Enabled = True
        Else
            MenuItemImport.Enabled = False
            MenuItemExport.Enabled = False
        End If
    End Sub


    Sub LoadGridColumns()
        Dim qry As String = String.Empty
        'gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim LineNo As New GridViewTextBoxColumn
        Dim PolicyNo As New GridViewTextBoxColumn
        Dim payHeadtype As New GridViewTextBoxColumn
        Dim Payhead As New GridViewTextBoxColumn
        Dim PayHeadName As New GridViewTextBoxColumn
        Dim Formula As New GridViewTextBoxColumn
        Dim RateAmount As New GridViewDecimalColumn
        Dim IsHiddenComponent As New GridViewCheckBoxColumn
        Dim Max_Amount As New GridViewDecimalColumn
        Dim PAYPERIOD_Amount As New GridViewDecimalColumn
        Dim LocationCode As New GridViewDecimalColumn

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 100
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(LineNo)

        PolicyNo.FormatString = ""
        PolicyNo.HeaderText = "Policy Account No."
        PolicyNo.Name = colpayHeadCode
        PolicyNo.Width = 100
        PolicyNo.ReadOnly = False
        PolicyNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.Columns.Add(PolicyNo)

        RateAmount.FormatString = ""
        RateAmount.HeaderText = "Amount"
        RateAmount.Name = colRateAmount
        RateAmount.Width = 100
        RateAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.Columns.Add(RateAmount)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.Rows.AddNew()

    End Sub

    Private Sub txtEMPCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtEMPCode._MYValidating
        Try
            Dim whrcls As String = Nothing
            Dim LocCode As String = Nothing
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                LocCode = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(TSPL_USER_MASTER.Default_Location,'') from TSPL_USER_MASTER Left Outer Join TSPL_LOCATION_MASTER on TSPL_USER_MASTER.Default_Location =TSPL_LOCATION_MASTER.Location_Code where 1=1 and TSPL_USER_MASTER.User_Code='" + objCommonVar.CurrentUserCode + "' "))
                If clsCommon.myLen(LocCode) > 0 Then
                    whrcls = " LOCATION_CODE='" + LocCode + "' and Emp_Status<>'Inactive'"
                End If
            Else
                whrcls += " Emp_Status<>'Inactive'"
            End If
            Dim qry As String = "SELECT EMP_CODE as Code,EMP_Name as Name,TSPL_EMPLOYEE_MASTER.PF_NO as [PF No] FROM TSPL_EMPLOYEE_MASTER "
            txtEMPCode.Value = clsCommon.ShowSelectForm("TSPL_EMPLOYEE_MASTER1", qry, "Code", whrcls, txtEMPCode.Value, "", isButtonClicked)
            Dim clsemp As clsEmployeeMaster
            clsemp = clsEmployeeMaster.FinderForEmployee(txtEMPCode.Value, Nothing)
            If Not clsemp Is Nothing Then
                lblEMPName.Text = clsemp.Emp_Name
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub txtPayHead__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPayHead._MYValidating
        Try
            Dim qry As String = "Select PAY_HEAD_CODE as Code,PAY_HEAD_NAME as Name from TSPL_PAYHEAD_MASTER "
            txtPayHead.Value = clsCommon.ShowSelectForm("TSPL_PAYHEAD_MASTER", qry, "Code", Nothing, txtPayHead.Value, "", isButtonClicked)
            lblPayHead.Text = clsDBFuncationality.getSingleValue("select PAY_HEAD_NAME from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE='" & txtPayHead.Value & "'")

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click_1(sender As Object, e As EventArgs) Handles btnSave.Click
        SavingData(False)
    End Sub

    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (Save()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
            End If
        End If
    End Sub

    Public Function Save() As Boolean
        Try
            If AllowToSave() Then
                Dim obj As New ClsEmployeeLICTagging
                obj.Document_code = txtCode.Value
                obj.Document_Date = txtDate.Value
                obj.EMP_CODE = txtEMPCode.Value
                obj.PAY_HEAD = txtPayHead.Value
                obj.Applicable_From = txtfromDate.Value
                obj.Applicable_To = txtToDate.Value
                Dim obj1 As ClsEmployeeLICTaggingDetails
                ObjList = New List(Of ClsEmployeeLICTaggingDetails)
                For Each grow As GridViewRowInfo In gv1.Rows
                    obj1 = New ClsEmployeeLICTaggingDetails()
                    obj1.PolicyAccountNo = clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)
                    'obj1.Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    obj1.Amount = clsCommon.myCdbl(grow.Cells(colRateAmount).Value)
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colpayHeadCode).Value)) > 0 Then
                        ObjList.Add(obj1)
                    End If

                Next
                If (obj.SaveData(obj, ObjList, isNewEntry, clsCommon.myCstr(txtCode.Value))) Then
                    LoadData(obj.Document_code, NavigatorType.Current)
                    Return True
                    common.clsCommon.MyMessageBoxShow("Data Saved Successfully")
                End If
                Return False
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow("Error")
        End Try
        Return False
    End Function

    Dim isPosted As Boolean = False

    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        isInsideLoadData = True
        funReset()
        txtCode.MyReadOnly = True
        btnSave.Enabled = True
        btnDelete.Enabled = True
        obj = ClsEmployeeLICTagging.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_code) > 0) Then
            isNewEntry = False
            btnSave.Text = "Update"
            If obj.POSTED Then
                btnSave.Enabled = False
                btnPost.Enabled = False
                btnDelete.Enabled = False
                UsLock1.Status = ERPTransactionStatus.Approved
                isPosted = obj.POSTED
            Else
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending
                isPosted = obj.POSTED
            End If
            Dim ii As Int16 = 0
            LoadGridColumns()
            txtCode.Value = obj.Document_code
            txtDate.Value = obj.Document_Date
            txtfromDate.Value = obj.Applicable_From
            txtToDate.Value = obj.Applicable_To
            TxtLICAmt.Text = obj.Total_AMT
            txtEMPCode.Value = obj.EMP_CODE
            txtPayHead.Value = obj.PAY_HEAD

            If (ClsEmployeeLICTagging.ObjList IsNot Nothing AndAlso ClsEmployeeLICTagging.ObjList.Count > 0) Then
                For Each obj1 As ClsEmployeeLICTaggingDetails In ClsEmployeeLICTagging.ObjList
                    gv1.Rows.AddNew()
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = obj1.Line_No
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpayHeadCode).Value = obj1.PolicyAccountNo
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colpayHeadName).Value = obj1.Amount
                Next
            Else
                gv1.Rows.AddNew()
            End If
        End If
        gv1.Rows.AddNew()
        isInsideLoadData = False

    End Sub
    Function AllowToSave() As Boolean
        Xtra.TransactionValidity(txtDate.Value)
        Return True
    End Function

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        funDelete()
    End Sub

    Sub funDelete()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                If (ClsEmployeeLICTagging.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtCode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        UsLock1.Status = ERPTransactionStatus.Pending
        lblEMPName.Text = ""
        Me.txtEMPCode.Value = Nothing
        Me.txtEMPCode.Text = ""
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnDelete.Enabled = True
        btnPost.Enabled = True
        Me.gv1.Rows.Clear()
        Me.gv1.Rows.AddNew()
        txtfromDate.Value = clsCommon.GETSERVERDATE
        txtToDate.Value = clsCommon.GETSERVERDATE
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowTransHistoryData(txtCode.Value, "DOCUMENT_CODE", "TSPL_EMPLOYEE_LIC_TAGGING", "TSPL_EMPLOYEE_LIC_TAGGING_DETAILS")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            txtCode.Value = ClsEmployeeLICTagging.getFinder(Nothing, txtCode.Value, isButtonClicked)
            'Dim qry As String = "select TSPL_BMC_TRANSPORTER_BILL_HEAD.Document_Code ,convert(varchar,TSPL_BMC_TRANSPORTER_BILL_HEAD.Document_date,103) as Document_date,TSPL_BMC_TRANSPORTER_BILL_HEAD.Tanker_No,case when TSPL_BMC_TRANSPORTER_BILL_HEAD.status =1  then 'Approved' else 'Pending' end as Status   
            '                 from TSPL_BMC_TRANSPORTER_BILL_HEAD "
            'LoadData(clsCommon.ShowSelectForm("TrsToSav@F", qry, "Document_Code", "", txtDocNo.Value, "Document_Code", isButtonClicked, "Document_date"), NavigatorType.Current)
            If clsCommon.myLen(txtCode.Value) > 0 Then
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_KeyUp(sender As Object, e As KeyEventArgs) Handles gv1.KeyUp
        If e.KeyCode = Keys.Home Then
            If gv1.Rows.Count = gv1.CurrentRow.Index + 1 Then
                gv1.Rows.AddNew()
            End If
            gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)

        End If
    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As KeyEventArgs) Handles gv1.KeyDown
        If e.KeyCode = Keys.Enter Then
            gv1.BeginEdit()
        End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try

            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column.FieldName.StartsWith("_CFLD_") Then
                        clsCustomFieldGrid.getFinderForCustomFieldGrid(gv1, e.Column.Name.ToString, MyBase.Form_ID)
                    End If
                    'If (clsCommon.CompairString(e.Column.Name, colVlcUploderCode) = CompairStringResult.Equal) Then
                    '    If (clsCommon.CompairString(e.Column.Name, colVlcUploderCode) = CompairStringResult.Equal) Then
                    '        OpenVlcUploder(False)
                    '    End If
                    'End If
                    UpdateAllTotals()
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isInsideLoadData = False
            isCellValueChangedOpen = False
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
        End Try
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub

    Private Sub gv1_CellBeginEdit(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellCancelEventArgs) Handles gv1.CellBeginEdit
        If TypeOf Me.gv1.CurrentColumn Is GridViewTextBoxColumn Then
            Dim editor As RadTextBoxEditor = DirectCast(Me.gv1.ActiveEditor, RadTextBoxEditor)
            Dim editorElement As RadTextBoxElement = DirectCast(editor.EditorElement, RadTextBoxElement)

        End If
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        UpdateAllTotals()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub UpdateAllTotals()
        Dim dblTotalDocAmt As Decimal = 0
        For i As Int16 = 0 To gv1.Rows.Count - 1
            dblTotalDocAmt = dblTotalDocAmt + clsCommon.myCdbl(gv1.Rows(i).Cells(colRateAmount).Value)
        Next
        TxtLICAmt.Text = Math.Round(clsCommon.myCdbl(dblTotalDocAmt), 2)
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

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        TxtLICAmt.Text = ""
        txtCode.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtfromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        'BlankAllControls()
        LoadGridColumns()
        btnSave.Text = "Save"
        txtCode.MyReadOnly = False
        txtDate.Enabled = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        'UcAttachment1.BlankAllControls()
        'gv1.Rows.AddNew()
        isNewEntry = True
        'ReStoreGridLayout()
    End Sub

End Class