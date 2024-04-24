Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports Telerik
Imports XpertERPEngine
Imports XpertERPEngineFine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

Public Class frmBullVaccinationEntry

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

    Private Sub frmBullVaccinationEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBullCode._MYValidating
        Try
            Dim qry As String = "select Bull_Code as Code,Bull_Alia_Name as Name,Prev_Bull_Id as [Previous Bull Id],Registration_Date as [Registration Date] ,SS_Bull_Id as [SS Bull Id],Breed_Code as Breed ,SS_Centre_Code as [SS Centre],Date_Of_Birth as [Date of Birth] from  TSPL_BULL_MASTER "
            txtBullCode.Value = clsCommon.ShowSelectForm("BullVaccEntry", qry, "Code", "", txtBullCode.Value, "Code", isButtonClicked)
            qry += "where bull_code = '" & txtBullCode.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt.Rows.Count > 0 Then
                lblBullAliasName.Text = clsCommon.myCstr(dt.Rows(0)("Name"))
                lblPreBullId.Text = clsCommon.myCstr(dt.Rows(0)("Previous Bull Id"))
                lblSSBullId.Text = clsCommon.myCstr(dt.Rows(0)("SS Bull Id"))
                lblDOB.Text = clsCommon.myCDate(dt.Rows(0)("Date of Birth"))
                lblBreed.Text = clsCommon.myCstr(dt.Rows(0)("Breed"))
                lblSSCentre.Text = clsCommon.myCstr(dt.Rows(0)("SS Centre"))
                lblRegDate.Text = clsCommon.myCDate(dt.Rows(0)("Registration Date"))
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BULL_VACCINE_ENTRY where Document_Code='" + txtDocumentNo.Value + "' "
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
        txtDocumentNo.Value = clsBullVaccinationEntry.getFinder(txtDocumentNo.Value, isButtonClicked)
        LoadData(txtDocumentNo.Value, NavigatorType.Current)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        Addnew()
    End Sub
    Private Sub frmBullVaccinationEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
                If clsBullVaccinationEntry.ReverseAndUnpost(txtDocumentNo.Value) Then
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
                If CheckBalanceQty() Then
                    clsBullVaccinationEntry.PostData(MyBase.Form_ID, txtDocumentNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Function AllowToSave() As Boolean

        If clsCommon.myLen(txtBullCode.Value) <= 0 Then
            txtBullCode.Focus()
            clsCommon.MyMessageBoxShow("Bull Code can't be blank.")
            Exit Function
            Return False
        End If
        If CheckBalanceQty() Then
            Return True
        Else
            Return False
            Exit Function
        End If

        Return True
    End Function

    Function CheckBalanceQty() As Boolean
        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
            Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemDesc).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnitCode).Value)
            If clsCommon.myLen(strICode) > 0 Then
                If clsCommon.myLen(strUOM) <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please enter UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1))
                    Return False
                End If

                Dim arrItem As List(Of String) = New List(Of String)
                Dim isItemExist As Integer = clsDBFuncationality.getSingleValue("select count(Item_Code) from TSPL_BULL_VACCINE_ENTRY_DETAIL where Document_Code = '" & txtDocumentNo.Value & "' and Item_Code = '" & strICode & "' ")
                If isItemExist > 1 Then
                    common.clsCommon.MyMessageBoxShow("Item Name " + strIName + " already exist. At Line No" + clsCommon.myCstr(ii + 1))
                    Return False
                End If

                Dim Location As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from tspl_user_master where User_Code = '" & objCommonVar.CurrentUserCode & "'"))

                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
                Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strICode, Location, txtDocumentNo.Value, txtdate.Value, Nothing, strUOM, Nothing)
                Dim dblEnteredQty As Double = Math.Round(dblQty, 2, MidpointRounding.ToEven)
                If dblEnteredQty > dblBalQty Then
                    common.clsCommon.MyMessageBoxShow("Item - " + strICode + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblEnteredQty) + " and Balance Quantity - " + clsCommon.myCstr(dblBalQty), Me.Text)
                    Return False
                End If
            End If

        Next
        Return True
    End Function
    Sub SaveData()
        Try
            If (AllowToSave()) Then
                Dim obj As New clsBullVaccinationEntry()
                obj.Document_Code = clsCommon.myCstr(txtDocumentNo.Value)
                obj.Document_date = clsCommon.myCDate(txtdate.Value)
                obj.Bull_Code = clsCommon.myCstr(txtBullCode.Value)
                obj.Remarks = clsCommon.myCstr(txtRemarks.Text)
                obj.Arr = New List(Of clsBullVaccinationEntryDetail)

                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsBullVaccinationEntryDetail()
                    If clsCommon.myLen((grow.Cells(colItemCode).Value)) > 0 Then
                        objTr.Qty = clsCommon.myCdbl((grow.Cells(colQty).Value))
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.Unit_code = clsCommon.myCstr(grow.Cells(colUnitCode).Value)
                        obj.Arr.Add(objTr)

                    ElseIf clsCommon.myLen((grow.Cells(colUnitCode).Value)) > 0 AndAlso clsCommon.myLen((grow.Cells(colQty).Value)) > 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Please select Item", Me.Text)
                        Exit Sub

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
        lblBullAliasName.Text = ""
        lblBreed.Text = ""
        lblPreBullId.Text = ""
        lblRegDate.Text = ""
        lblDOB.Text = ""
        lblSSBullId.Text = ""
        lblSSCentre.Text = ""
        btnPost.Enabled = True
        txtdate.Value = clsCommon.GETSERVERDATE()
        btnsave.Text = "Save"
        isInsideLoadData = False
        btndelete.Enabled = True
        txtBullCode.Value = ""
        lblBullAliasName.Text = ""
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
                txtDocumentNo.Value = clsCommon.myCstr(obj.Document_Code)
                txtdate.Value = obj.Document_date
                txtBullCode.Value = clsCommon.myCstr(obj.Bull_Code)
                lblBullAliasName.Text = clsCommon.myCstr(obj.BullAliasName)
                lblPreBullId.Text = clsCommon.myCstr(obj.PreBullId)
                lblSSBullId.Text = clsCommon.myCstr(obj.SSBullId)
                lblDOB.Text = clsCommon.myCDate(obj.DOB)
                lblBreed.Text = clsCommon.myCstr(obj.Breed)
                lblSSCentre.Text = clsCommon.myCstr(obj.SSCentre)
                lblRegDate.Text = clsCommon.myCDate(obj.RegDate)
                txtRemarks.Text = clsCommon.myCstr(obj.Remarks)
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
            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
                Throw New Exception("Document No not found to delete")
            End If
            If (myMessages.deleteConfirm()) Then
                clsBullVaccinationEntry.DeleteData(txtDocumentNo.Value)
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
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmBullVaccinationEntry & "'"))
                arrHeader.Add("Date : " & clsCommon.myCDate(txtdate.Value))
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

End Class