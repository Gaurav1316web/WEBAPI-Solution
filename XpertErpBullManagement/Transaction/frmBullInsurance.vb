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
    Const colQty As String = "colQty"
    Const colItemCode As String = "colItemCode"
    Const colItemDesc As String = "colItemDesc"
    Const colUnitCode As String = "colUnitCode"
    Dim isLoadData As Boolean = False
    Dim isCopyData As Boolean = False
#End Region

    Private Sub frmBullInsurance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()

        'coll.Add("Policy_Code", "varchar(30) NOT NULL Primary Key")
        'coll.Add("Policy_Date", "DateTime not NULL")
        'coll.Add("Policy_Start_Date", "Date  NULL")
        'coll.Add("Policy_End__Date", "Date NULL")
        'coll.Add("Bull_Code", "VARCHAR(30) not NULL REFERENCES TSPL_BULL_MASTER (Bull_Code)")
        'coll.Add("Status", "integer not null default 0")
        'coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE) ")
        'coll.Add("Created_Date", "datetime NOT NULL  ")
        'coll.Add("Modified_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE) ")
        'coll.Add("Modified_Date", "datetime NOT NULL ")
        'coll.Add("Posted_By", "varchar(12) NULL")
        'coll.Add("Posted_Date", "datetime NULL")
        'clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_BULL_VACCINE_ENTRY", coll, Nothing, True, False, Nothing, Nothing, Nothing, False)

        'coll = New Dictionary(Of String, String)()
        'coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION")
        'coll.Add("Document_Code", "Varchar(30) not null REFERENCES TSPL_BULL_VACCINE_ENTRY(Document_Code)")
        'coll.Add("Item_Code", "varchar(50) NOT NULL REFERENCES tspl_item_master(Item_Code) ")
        'coll.Add("Unit_code", "varchar(12) Not NULL ")
        'coll.Add("Qty", "decimal (18,2) NULL")
        'clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_BULL_VACCINE_ENTRY_DETAIL", coll, Nothing, True, False, "TSPL_BULL_VACCINE_ENTRY", "Document_Code", "Document_Date", False)

        SetUserMgmtNew()
        LoadBlankGrid()
        Addnew()
        btnPost.Visible = True
        btnPost.Enabled = False
        If clsCommon.myLen(txtPolicyNo.Value) > 0 Then
            LoadData(clsCommon.myCstr(txtPolicyNo.Value), NavigatorType.Current)
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
            Dim qry As String = "select Bull_Code as Code,Bull_Alia_Name as Name,Prev_Bull_Id as [Previous Bull Id],Registration_Date as [Registration Date] ,SS_Bull_Id as [SS Bull Id],Breed_Code as Breed ,SS_Centre_Code as [SS Centre],Date_Of_Birth as [Date of Birth] from  TSPL_BULL_MASTER "
            txtInsCompany.Value = clsCommon.ShowSelectForm("BullVaccEntry", qry, "Code", "", txtInsCompany.Value, "Code", isButtonClicked)
            qry += "where bull_code = '" & txtInsCompany.Value & "'"
            lblInsCompName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code ='" + txtInsCompany.Value + "'"))

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtPolicyNo__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtPolicyNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BULL_VACCINE_ENTRY where Document_Code='" + txtPolicyNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtPolicyNo.MyReadOnly = False
            Else
                txtPolicyNo.MyReadOnly = True
            End If
            LoadData(txtPolicyNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtPolicyNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtPolicyNo._MYValidating
        If clsCommon.myLen(txtPolicyNo) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
        End If
        txtPolicyNo.Value = clsBullVaccinationEntry.getFinder(txtPolicyNo.Value, isButtonClicked)
        LoadData(txtPolicyNo.Value, NavigatorType.Current)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub frmBullInsurance_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            btnAddNew.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
            'SaveData()
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
                If clsBullVaccinationEntry.ReverseAndUnpost(txtPolicyNo.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtPolicyNo.Value, NavigatorType.Current)
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
            If clsCommon.myLen(txtPolicyNo.Value) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtPolicyNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                clsBullVaccinationEntry.PostData(MyBase.Form_ID, txtPolicyNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(txtPolicyNo.Value, NavigatorType.Current)

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        ' SaveData()
    End Sub

    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtInsCompany.Value) <= 0 Then
            txtInsCompany.Focus()
            clsCommon.MyMessageBoxShow("Bull Code can't be blank.")
            Exit Function
            Return False
        End If

        Return True
    End Function


    'Sub SaveData()
    '    Try
    '        If (AllowToSave()) Then
    '            Dim obj As New clsBullVaccinationEntry()
    '            obj.Document_Code = clsCommon.myCstr(txtPolicyNo.Value)
    '            obj.Document_date = clsCommon.myCDate(txtdate.Value)
    '            obj.Bull_Code = clsCommon.myCstr(txtInsCompany.Value)
    '            'obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
    '            obj.Arr = New List(Of clsBullVaccinationEntryDetail)

    '            For Each grow As GridViewRowInfo In gv1.Rows
    '                Dim objTr As New clsBullVaccinationEntryDetail()
    '                If clsCommon.myLen((grow.Cells(colItemCode).Value)) > 0 Then
    '                    objTr.Qty = clsCommon.myCdbl((grow.Cells(colQty).Value))
    '                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
    '                    objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
    '                    obj.Arr.Add(objTr)

    '                ElseIf clsCommon.myLen((grow.Cells(colUnitCode).Value)) > 0 AndAlso clsCommon.myLen((grow.Cells(colQty).Value)) > 0 Then
    '                    clsCommon.MyMessageBoxShow(Me, "Please select Item", Me.Text)
    '                    Exit Sub

    '                End If

    '            Next
    '            If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
    '                clsCommon.MyMessageBoxShow("Please Fill at list one Item")
    '                Exit Sub
    '            End If
    '            If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
    '                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
    '                LoadData(obj.Document_Code, NavigatorType.Current)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    End Try
    'End Sub

    Private Sub Addnew()
        isNewEntry = True
        txtPolicyNo.Value = ""
        btnsave.Enabled = True
        txtInsCompany.Value = ""
        lblInsCompName.Text = ""
        txtInsType.Value = ""
        lblInsType.Text = ""
        txtInsAmt.Value = Nothing
        txtInsAmt.Tag = Nothing
        txtPremiumPer.Value = Nothing
        txtPremiumPer.Tag = Nothing
        txtPremAmt.Value = Nothing
        txtPremAmt.Tag = Nothing
        txtSerChargePer.Value = Nothing
        txtSerChargePer.Tag = Nothing
        txtSerChargeAmt.Value = Nothing
        txtSerChargeAmt.Tag = Nothing
        txtTotalAmt.Value = Nothing
        txtTotalAmt.Tag = Nothing
        btnPost.Enabled = True
        txtPolicydate.Value = clsCommon.GETSERVERDATE()
        txInsStartDate.Value = clsCommon.GETSERVERDATE()
        txInsEndDate.Value = clsCommon.GETSERVERDATE()
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
        Dim repoNumBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNumBox.Name = colSNo
        repoNumBox.Width = 60
        repoNumBox.ReadOnly = True
        repoNumBox.HeaderText = "SNO"
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.Width = 120
        repoItemCode.HeaderText = "Vaccine Code"
        repoItemCode.Name = colItemCode
        repoItemCode.HeaderImage = Global.XpertErpBullManagement.My.Resources.Resources.search4
        repoItemCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoItemCode.IsVisible = True
        repoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemDesc.FormatString = ""
        repoItemDesc.Width = 120
        repoItemDesc.HeaderText = "Vaccine Name"
        repoItemDesc.ReadOnly = True
        repoItemDesc.Name = colItemDesc
        repoItemDesc.IsVisible = True
        repoItemDesc.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemDesc)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.Width = 100
        repoQty.HeaderText = "Qty"
        repoQty.Minimum = 0
        repoQty.Name = colQty
        repoQty.ShowUpDownButtons = False
        repoQty.IsVisible = True
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoUnitCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnitCode.FormatString = ""
        repoUnitCode.Width = 70
        repoUnitCode.HeaderText = "UOM"
        repoUnitCode.Name = colUnitCode
        repoUnitCode.HeaderImage = Global.XpertErpBullManagement.My.Resources.Resources.search4
        repoUnitCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoUnitCode.IsVisible = True
        repoUnitCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUnitCode)

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
            Dim obj As New clsBullVaccinationEntry()
            obj = clsBullVaccinationEntry.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_Code)) > 0) Then
                LoadBlankGrid()
                txtPolicyNo.Value = clsCommon.myCstr(obj.Document_Code)
                txtPolicydate.Value = obj.Document_date
                txtInsCompany.Value = clsCommon.myCstr(obj.Bull_Code)
                lblInsCompName.Text = clsCommon.myCstr(obj.BullAliasName)
                txtInsType.Value = clsCommon.myCstr(obj.PreBullId)
                lblInsType.Text = clsCommon.myCstr(obj.SSBullId)
                'lblDOB.Text = clsCommon.myCDate(obj.DOB)
                'lblBreed.Text = clsCommon.myCstr(obj.Breed)
                'lblSSCentre.Text = clsCommon.myCstr(obj.SSCentre)
                'lblRegDate.Text = clsCommon.myCDate(obj.RegDate)
                'txtRemarks.Text = clsCommon.myCstr(obj.Remarks)
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
                    For Each objTr As clsBullVaccinationEntryDetail In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = ii
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemDesc).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnitCode).Value = objTr.Unit_code

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
        'For i As Integer = 0 To gv1.Rows.Count - 1
        '    gv1.Rows(0).Cells(0).Value = 1
        '    If i <> 0 Then
        '        gv1.Rows(i).Cells(colSNo).Value = i + 1
        '    End If
        'Next
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
            If clsCommon.myLen(txtPolicyNo.Value) <= 0 Then
                Throw New Exception("Document No not found to delete")
            End If
            If (myMessages.deleteConfirm()) Then
                clsBullVaccinationEntry.DeleteData(txtPolicyNo.Value)
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
            If transportSql.importExcel(gvImport, "SNO", "Item Code", "Item Desc", "Qty", "UOM") Then
                isInsideLoadData = True
                Dim Arr As New List(Of clsBullVaccinationEntryDetail)
                Dim strItemCode As String = ""
                Dim strItemName As String = ""
                Try
                    clsCommon.ProgressBarPercentShow()
                    For ii As Integer = 0 To gvImport.Rows.Count - 1
                        If clsCommon.myLen(gvImport.Rows(ii).Cells("Item Code").Value) > 0 Then
                            strItemCode = gvImport.Rows(ii).Cells("Item Code").Value
                            Dim qry As String = "select count(TSPL_ITEM_MASTER.Item_Code) FROM tspl_item_master LEFT OUTER JOIN TSPL_ITEM_TYPE_MASTER ON TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = tspl_item_master.Item_Type where TSPL_ITEM_TYPE_MASTER.IsVaccine = 'Y' and TSPL_ITEM_MASTER.Item_Code = '" & strItemCode & "'"
                            If clsCommon.myLen(clsDBFuncationality.getSingleValue(qry)) > 0 Then
                                clsCommon.MyMessageBoxShow(Me, "Item type should be Vaccine type", Me.Text)
                                clsCommon.ProgressBarPercentHide()
                                Exit Sub
                            End If
                            clsCommon.ProgressBarPercentUpdate((gvImport.Rows(ii).Index + 1) * 100 / (gvImport.Rows.Count + 1), "Importing  : " & (gvImport.Rows(ii).Index + 1) & "/" & gvImport.Rows.Count & "")
                            Try
                                gv1.Rows(ii).Cells(colSNo).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("SNO").Value)
                                gv1.Rows(ii).Cells(colItemCode).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("Item Code").Value)
                                gv1.Rows(ii).Cells(colItemDesc).Value = clsDBFuncationality.getSingleValue("select TSPL_ITEM_MASTER.Item_Desc FROM tspl_item_master where Item_Code = '" & strItemCode & "'")
                                gv1.Rows(ii).Cells(colQty).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Qty").Value)
                                gv1.Rows(ii).Cells(colUnitCode).Value = clsCommon.myCstr(gvImport.Rows(ii).Cells("UOM").Value)

                                If clsCommon.myLen(txtPolicyNo.Value) = 0 Then
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
                            clsCommon.MyMessageBoxShow(Me, "Please fill Item Code", Me.Text)
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
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True

                    If e.Column Is gv1.Columns(colItemCode) Then
                        OpenICodeList(False)
                    End If

                    If e.Column Is gv1.Columns(colUnitCode) Then
                        OpenUOMList(False)
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Try
            Dim qry As String = "select distinct TSPL_ITEM_MASTER.Item_Code as Code,TSPL_ITEM_MASTER.Item_Desc as Name,TSPL_ITEM_MASTER.Unit_Code,TSPL_ITEM_UOM_DETAIL.Net_Weight as [Net Weight] FROM TSPL_ITEM_MASTER left join TSPL_ITEM_UOM_DETAIL on  TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code
       and TSPL_ITEM_UOM_DETAIL.UOM_Code = TSPL_ITEM_MASTER.Unit_Code  LEFT OUTER JOIN TSPL_ITEM_TYPE_MASTER ON TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = tspl_item_master.Item_Type"
            Dim WhrCls As String = " TSPL_ITEM_TYPE_MASTER.IsVaccine = 'Y' "
            gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.ShowSelectForm("ItemFinder", qry, "Code", WhrCls, gv1.CurrentRow.Cells(colItemCode).Value, "Code", isButtonClick)
            If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 Then
                gv1.CurrentRow.Cells(colItemDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Desc from tspl_item_master where Item_Code='" + gv1.CurrentRow.Cells(colItemCode).Value + "'"))
                gv1.CurrentRow.Cells(colUnitCode).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UOM_Code from TSPL_ITEM_UOM_DETAIL where Default_UOM = 1 and Item_Code='" + gv1.CurrentRow.Cells(colItemCode).Value + "'"))

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try

    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Try
            Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)
            If clsCommon.myLen(strICode) > 0 Then
                Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
                Dim whrCls As String = "Item_Code='" + strICode + "' "
                gv1.CurrentRow.Cells(colUnitCode).Value = clsCommon.ShowSelectForm("BullVaccEntryfndnder", qry, "Code", whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnitCode).Value), "Code", isButtonClick)
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please select Item first", Me.Text)
                Exit Sub
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValidated(sender As Object, e As CellValidatedEventArgs) Handles gv1.CellValidated
        Try
            SetGridFocus()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFocus()
        If gv1.CurrentCell IsNot Nothing Then
            Dim setNxtRow As Boolean = False
            If gv1.CurrentCell.ColumnInfo.Name = colItemCode Then
                gv1.CurrentColumn = gv1.Columns(colQty)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colUnitCode Then
                setNxtRow = True
                gv1.CurrentColumn = gv1.Columns(colItemCode)
            End If
            If setNxtRow Then
                gv1.CurrentRow = gv1.Rows(gv1.CurrentRow.Index + 1)
                gv1.CurrentColumn = gv1.Columns(colItemCode)
            End If
        End If
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
        Dim qry As String = "select '' as SNO ,'' as [Item Code], '' as [Item Desc],'' AS Qty , '' AS UOM "
        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    End Sub

    Private Sub txtInsType__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtInsType._MYValidating

    End Sub

    Private Sub txtInsAmt_TextChanged(sender As Object, e As EventArgs) Handles txtInsAmt.TextChanged

    End Sub
End Class