Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports Telerik
Imports XpertERPEngine
Imports XpertERPEngineFine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmBullInsurance


#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colSNo As String = "colSNo"
    Const colTagNo As String = "colTagNo"

    Dim isLoadData As Boolean = False
#End Region

    Private Sub frmBullInsurance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()

        coll.Add("Document_Code", "varchar(30) NOT NULL Primary Key")
        coll.Add("Policy_Code", "varchar(30) NOT NULL ")
        coll.Add("Policy_Date", "DateTime not NULL")
        coll.Add("Policy_Start_Date", "Date  NULL")
        coll.Add("Policy_End__Date", "Date NULL")
        coll.Add("Ins_Comp_Code", "VARCHAR(30) not NULL  REFERENCES TSPL_BULL_INSURANCE_MASTER (Code) ")
        coll.Add("Ins_Type_Code", "VARCHAR(30) not NULL REFERENCES TSPL_BULL_INSURANCE_TYPE (Code)")
        coll.Add("Insured_Amount", "Decimal (18,2) Null")
        coll.Add("Premium_Per", "Decimal (18,2) Null")
        coll.Add("Premium_Amount", "Decimal (18,2) Null")
        coll.Add("Ser_Charge_Per", "Decimal (18,2) Null")
        coll.Add("Ser_Charge_Amount", "Decimal (18,2) Null")
        coll.Add("Total_Amount", "Decimal (18,2) Null")
        coll.Add("Status", "integer not null default 0")
        coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE) ")
        coll.Add("Created_Date", "datetime NOT NULL  ")
        coll.Add("Modified_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE) ")
        coll.Add("Modified_Date", "datetime NOT NULL ")
        coll.Add("Posted_By", "varchar(12) NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Posted_Date", "datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_BULL_INSURANCE", coll, "unique(Ins_Comp_Code, Policy_Code)", True, False, Nothing, Nothing, Nothing, False)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION")
        coll.Add("Document_Code", "Varchar(30) not null REFERENCES TSPL_BULL_INSURANCE(Document_Code)")
        coll.Add("Tag_No", "varchar(30) NOT NULL UNIQUE ")
        clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_BULL_INSURANCE_TAG", coll, Nothing, True, False, "TSPL_BULL_INSURANCE", "Document_Code", "Policy_Date", False)

        SetUserMgmtNew()
        LoadBlankGrid()
        Addnew()
        btnPost.Visible = True
        btnPost.Enabled = False
        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
        End If
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        RadSplitButton1.Visible = MyBase.isExport
        btnPost.Visible = MyBase.isPostFlag
        If MyBase.isExport = True Then
            rmExport.Enabled = True
            rmimport.Enabled = True
        Else
            rmExport.Enabled = False
            rmimport.Enabled = False
        End If

    End Sub

    Private Sub txtInsCompany__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtInsCompany._MYValidating
        Try
            Dim qry As String = "select Code ,Name from TSPL_BULL_INSURANCE_MASTER "

            txtInsCompany.Value = clsCommon.ShowSelectForm("BullInsurance", qry, "Code", "", txtInsCompany.Value, "Code", isButtonClicked)
            qry += "where Code = '" & txtInsCompany.Value & "'"
            lblInsCompName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_BULL_INSURANCE_MASTER where Code ='" + txtInsCompany.Value + "'"))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BULL_INSURANCE where Document_Code='" + txtDocumentNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocumentNo.MyReadOnly = False
            Else
                txtDocumentNo.MyReadOnly = True
            End If
            LoadData(txtDocumentNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocumentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
        If clsCommon.myLen(txtDocumentNo) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
        End If
        txtDocumentNo.Value = clsBullInsurance.getFinder(txtDocumentNo.Value, isButtonClicked)
        LoadData(txtDocumentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub frmBullInsurance_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnsave.Enabled AndAlso MyBase.isDeleteFlag Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
            Me.Close()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then

                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverseUnpost.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        CancelPressed()
    End Sub
    Private Sub btnReverseUnpost_Click_(sender As Object, e As EventArgs) Handles btnReverseUnpost.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsBullInsurance.ReverseAndUnpost(txtDocumentNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click

        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocumentNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                clsBullInsurance.PostData(MyBase.Form_ID, txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtDocumentNo.Value, NavigatorType.Current)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtPolicyNo.Text) <= 0 Then
            txtPolicyNo.Focus()
            clsCommon.MyMessageBoxShow("Policy No can't be blank.")
            Exit Function
            Return False
        End If

        If clsCommon.myLen(txtInsCompany.Value) <= 0 Then
            txtInsCompany.Focus()
            clsCommon.MyMessageBoxShow("Insurance Company can't be blank.")
            Exit Function
            Return False
        End If
        If clsCommon.myLen(txtInsType.Value) <= 0 Then
            txtInsType.Focus()
            clsCommon.MyMessageBoxShow("Insurance Type can't be blank.")
            Exit Function
            Return False
        End If
        Return True
    End Function


    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsBullInsurance()
                obj.Document_Code = clsCommon.myCstr(txtDocumentNo.Value)
                obj.Policy_Code = clsCommon.myCstr(txtPolicyNo.Text)
                obj.Policy_Date = clsCommon.myCDate(txtPolicydate.Value)
                obj.Policy_Start_Date = clsCommon.myCDate(txtInsStartDate.Value)
                obj.Policy_End__Date = clsCommon.myCDate(txtInsEndDate.Value)
                obj.Ins_Comp_Code = clsCommon.myCstr(txtInsCompany.Value)
                obj.Ins_Type_Code = clsCommon.myCstr(txtInsType.Value)
                obj.Insured_Amount = clsCommon.myCdbl(txtInsAmt.Value)
                obj.Premium_Amount = clsCommon.myCdbl(lblPremAmt.Text)
                obj.Ser_Charge_Amount = clsCommon.myCdbl(lblSerChargeAmt.Text)
                obj.Total_Amount = clsCommon.myCdbl(lblTotalAmt.Text)
                obj.Premium_Per = clsCommon.myCDecimal(txtPremiumPer.Value)
                obj.Ser_Charge_Per = clsCommon.myCDecimal(txtSerChargePer.Value)

                obj.Arr = New List(Of clsBullInsuranceDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsBullInsuranceDetail()
                    If clsCommon.myLen((grow.Cells(colTagNo).Value)) > 0 Then
                        objTr.Tag_No = clsCommon.myCstr((grow.Cells(colTagNo).Value))
                        obj.Arr.Add(objTr)

                    End If

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    clsCommon.MyMessageBoxShow("Please Fill at list one Item")
                    Exit Sub
                End If
                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        isNewEntry = True
        txtDocumentNo.Value = ""
        btnsave.Enabled = True
        txtInsCompany.Value = ""
        lblInsCompName.Text = ""
        txtInsType.Value = ""
        lblInsType.Text = ""
        txtPolicyNo.Text = ""
        txtInsAmt.Value = Nothing
        txtInsAmt.Tag = Nothing
        txtPremiumPer.Value = Nothing
        txtPremiumPer.Tag = Nothing
        lblPremAmt.Text = Nothing
        txtSerChargePer.Value = Nothing
        txtSerChargePer.Tag = Nothing
        lblSerChargeAmt.Text = Nothing
        lblSerChargeAmt.Tag = Nothing
        lblTotalAmt.Text = Nothing
        btnPost.Enabled = True
        txtPolicydate.Value = clsCommon.GETSERVERDATE()
        txtInsStartDate.Value = clsCommon.GETSERVERDATE()
        txtInsEndDate.Value = clsCommon.GETSERVERDATE()
        btnsave.Text = "Save"
        isInsideLoadData = False
        btndelete.Enabled = True
        txtInsCompany.Value = ""
        lblStatus.Status = ERPTransactionStatus.Pending
        LoadBlankGrid()
        gv1.Rows.AddNew()

    End Sub
    Private Sub LoadBlankGrid()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.AddNewRowPosition = SystemRowPosition.Bottom
        Dim repoSNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSNo.Name = colSNo
        repoSNo.Width = 60
        repoSNo.ReadOnly = True
        repoSNo.HeaderText = "SNO"
        gv1.MasterTemplate.Columns.Add(repoSNo)

        Dim repoTagNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTagNo.FormatString = ""
        repoTagNo.Width = 100
        repoTagNo.HeaderText = "Tag No"
        repoTagNo.Name = colTagNo
        repoTagNo.IsVisible = True
        repoTagNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTagNo)

        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False

        ReStoreGridLayout()
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnsave.Enabled = True
            btnPost.Enabled = True

            Addnew()
            Dim obj As New clsBullInsurance()
            obj = clsBullInsurance.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_Code)) > 0) Then
                LoadBlankGrid()
                txtDocumentNo.Value = clsCommon.myCstr(obj.Document_Code)
                txtPolicyNo.Text = clsCommon.myCstr(obj.Policy_Code)
                txtPolicydate.Value = clsCommon.myCDate(obj.Policy_Date)
                txtInsStartDate.Value = clsCommon.myCDate(obj.Policy_Start_Date)
                txtInsEndDate.Value = clsCommon.myCDate(obj.Policy_End__Date)
                txtInsCompany.Value = clsCommon.myCstr(obj.Ins_Comp_Code)
                lblInsCompName.Text = clsCommon.myCstr(obj.Ins_Comp_Name)
                txtInsType.Value = clsCommon.myCstr(obj.Ins_Type_Code)
                lblInsType.Text = clsCommon.myCstr(obj.Ins_Type_Name)
                txtPremiumPer.Value = clsCommon.myCDecimal(obj.Premium_Per)
                lblPremAmt.Text = clsCommon.myCdbl(obj.Premium_Amount)
                txtInsAmt.Value = clsCommon.myCdbl(obj.Insured_Amount)
                lblTotalAmt.Text = clsCommon.myCdbl(obj.Total_Amount)
                txtSerChargePer.Value = clsCommon.myCDecimal(obj.Ser_Charge_Per)
                lblSerChargeAmt.Text = clsCommon.myCdbl(obj.Ser_Charge_Amount)
                isNewEntry = False
                isInsideLoadData = True
                btnsave.Text = "Update"
                If obj.Status = 1 Then
                    lblStatus.Status = ERPTransactionStatus.Approved
                    btndelete.Enabled = False
                    btnsave.Enabled = False
                    btnPost.Enabled = False
                Else
                    lblStatus.Status = ERPTransactionStatus.Pending
                    btndelete.Enabled = True
                    btnsave.Enabled = True
                    btnPost.Enabled = True
                End If

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    Dim ii As Integer = 1
                    For Each objTr As clsBullInsuranceDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = ii
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTagNo).Value = objTr.Tag_No

                        ii = ii + 1
                    Next
                End If

            End If

            isLoadData = True
            isInsideLoadData = False

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub
    Private Sub ReStoreGridLayout()
        Try

            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                    gv1.Columns(ii).IsVisible = False
                    gv1.Columns(ii).VisibleInColumnChooser = True
                Next

                gv1.LoadLayout(obj.GridLayout)
                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSNo).Value = ii
        Next
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        Else
            e.Cancel = False
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colSNo).Value = i + 1
            End If
        Next
    End Sub
    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("Document No not found to delete")
            End If
            If (myMessages.deleteConfirm()) Then
                clsBullInsurance.DeleteData(txtDocumentNo.Value)
                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
                Addnew()
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    Private Sub rmimport_Click(sender As Object, e As EventArgs) Handles rmimport.Click
        Try

            Dim gvImport As New RadGridView()
            Me.Controls.Add(gvImport)
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gvImport, "SNO", "Tag No") Then
                isInsideLoadData = True
                Dim Arr As New List(Of clsBullInsuranceDetail)
                Try
                    clsCommon.ProgressBarPercentShow()
                    For ii As Integer = 0 To gvImport.Rows.Count - 1
                        If clsCommon.myLen(gvImport.Rows(ii).Cells("Tag No").Value) > 0 Then
                            clsCommon.ProgressBarPercentUpdate((gvImport.Rows(ii).Index + 1) * 100 / (gvImport.Rows.Count + 1), "Importing  : " & (gvImport.Rows(ii).Index + 1) & "/" & gvImport.Rows.Count & "")
                            Try
                                gv1.Rows(ii).Cells(colSNo).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("SNO").Value)
                                gv1.Rows(ii).Cells(colTagNo).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Tag No").Value)

                                If clsCommon.myLen(txtDocumentNo.Value) = 0 Then
                                    If gv1.Rows.Count = gvImport.Rows.Count Then
                                    Else
                                        gv1.Rows.AddNew()

                                    End If
                                End If

                            Catch ex As Exception
                                gv1.Rows.RemoveAt(ii)
                                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                            End Try
                        Else
                            clsCommon.MyMessageBoxShow(Me, "Please fill Tag No", Me.Text)
                        End If
                    Next

                    clsCommon.ProgressBarPercentHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data imported successfully", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    clsCommon.ProgressBarPercentHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gvImport)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDeleteLayout_Click(sender As Object, e As EventArgs) Handles btnDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBullInsurance & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtPolicydate.Value))
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            Else
                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
            Try
                If gv1.Rows.Count >= 0 Then

                    clsCommon.MyExportToPDF(Me.Text, gv1, Nothing, Me.Text, MyBase.Form_ID, objCommonVar.CurrentUserCode)
                Else
                    Throw New Exception("no record found.")
                End If
            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End Sub

        Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim qry As String = "select '' as SNO ,'' as [Tag No] "
        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
        End Sub

        Private Sub txtInsType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtInsType._MYValidating
            Try
                Dim qry As String = "select Code ,Name from TSPL_BULL_INSURANCE_TYPE "

                txtInsType.Value = clsCommon.ShowSelectForm("BullInsurance", qry, "Code", "", txtInsType.Value, "Code", isButtonClicked)
                qry += "where Code = '" & txtInsType.Value & "'"
                lblInsType.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Name from TSPL_BULL_INSURANCE_TYPE where Code ='" + txtInsType.Value + "'"))

            Catch ex As Exception
                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            End Try
        End Sub

        Private Sub txtInsAmt_TextChanged(sender As Object, e As EventArgs) Handles txtInsAmt.TextChanged
            lblPremAmt.Text = Math.Round((txtInsAmt.Value * txtPremiumPer.Value), 2, MidpointRounding.ToEven)
        lblSerChargeAmt.Text = Math.Round(((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100, 2, MidpointRounding.ToEven)
    End Sub

    Private Sub txtPremiumPer_TextChanged(sender As Object, e As EventArgs) Handles txtPremiumPer.TextChanged
            lblPremAmt.Text = Math.Round((txtInsAmt.Value * txtPremiumPer.Value), 2, MidpointRounding.ToEven)
        lblSerChargeAmt.Text = Math.Round(((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100, 2, MidpointRounding.ToEven)
        lblTotalAmt.Text = Math.Round((txtInsAmt.Value * txtPremiumPer.Value) + (((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100), 2, MidpointRounding.ToEven)
    End Sub

    Private Sub txtInsAmt_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles txtInsAmt.Validating
            lblPremAmt.Text = Math.Round((txtInsAmt.Value * txtPremiumPer.Value), 2, MidpointRounding.ToEven)
        lblSerChargeAmt.Text = Math.Round(((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100, 2, MidpointRounding.ToEven)
        lblTotalAmt.Text = Math.Round((txtInsAmt.Value * txtPremiumPer.Value) + (((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100), 2, MidpointRounding.ToEven)
    End Sub

    Private Sub txtPremiumPer_Validating(sender As Object, e As EventArgs) Handles txtPremiumPer.Validating
            lblPremAmt.Text = Math.Round((txtInsAmt.Value * txtPremiumPer.Value), 2, MidpointRounding.ToEven)
        lblSerChargeAmt.Text = Math.Round(((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100, 2, MidpointRounding.ToEven)
        lblTotalAmt.Text = Math.Round((txtInsAmt.Value * txtPremiumPer.Value) + (((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100), 2, MidpointRounding.ToEven)
    End Sub

    Private Sub txtSerChargePer_TextChanged(sender As Object, e As EventArgs) Handles txtSerChargePer.TextChanged
        lblSerChargeAmt.Text = Math.Round(((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100, 2, MidpointRounding.ToEven)
        lblTotalAmt.Text = Math.Round((txtInsAmt.Value * txtPremiumPer.Value) + (((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100), 2, MidpointRounding.ToEven)
    End Sub
    Private Sub txtSerChargePer_Validating(sender As Object, e As EventArgs) Handles txtSerChargePer.Validating
        lblSerChargeAmt.Text = Math.Round(((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100, 2, MidpointRounding.ToEven)
        lblTotalAmt.Text = Math.Round((txtInsAmt.Value * txtPremiumPer.Value) + (((txtInsAmt.Value * txtPremiumPer.Value) * txtSerChargePer.Value) / 100), 2, MidpointRounding.ToEven)
    End Sub

    Private Sub gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gv1.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFocus()
        Try
            If gv1.CurrentCell IsNot Nothing Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                gv1.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
                Dim setNxtRow As Boolean = False
                If gv1.CurrentCell.ColumnInfo.Name = colTagNo Then
                    setNxtRow = True
                    gv1.CurrentColumn = gv1.Columns(colTagNo)
                End If
                If setNxtRow Then
                    If gv1.Rows.Count > gv1.CurrentRow.Index + 1 Then
                        If clsCommon.myLen(gv1.CurrentRow.Cells(colTagNo).Value) > 0 Then
                            gv1.Rows.AddNew()
                            gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index - 1)
                            gv1.CurrentColumn = gv1.Columns(colTagNo)
                        End If
                    End If

                    End If
            End If
        Catch ex As Exception

        End Try


    End Sub


End Class