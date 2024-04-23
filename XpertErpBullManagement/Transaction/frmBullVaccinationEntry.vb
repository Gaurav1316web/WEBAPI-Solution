Imports System.Data.SqlClient
Imports common
Imports System.IO
Imports Telerik
Imports XpertERPEngine

Public Class frmBullVaccinationEntry

    '#Region "Variables"
    '    Private isNewEntry As Boolean = False
    '    Dim isInsideLoadData As Boolean = False
    '    Dim isCellValueChangedOpen As Boolean = False
    '    Const colSNo As String = "colSNo"
    '    Const colTenderQty As String = "colTenderQty"
    '    Const colRate As String = "colRate"
    '    Const colProRate As String = "colProRate"
    '    Const colDieselPetrol As String = "colDieselPetrol"
    '    Const colApplicableRate As String = "colApplicableRate"
    '    Const colGPSKM As String = "colGPSKM"
    '    Const colPayableAmount As String = "colPayableAmount"
    '    Dim isLoadData As Boolean = False
    '    Dim isCopyData As Boolean = False
    '#End Region

    '    Private Sub frmBullVaccinationEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '        Dim coll As Dictionary(Of String, String)
    '        coll = New Dictionary(Of String, String)()
    '        coll.Add("Document_Code", "varchar(30) NOT NULL Primary Key")
    '        coll.Add("Document_Date", "DateTime not NULL")
    '        coll.Add("Bull_Code", "varchar(12) NOT NULL")
    '        coll.Add("Status", "integer not null default 0")
    '        coll.Add("Start_Date", "Date Not null")
    '        coll.Add("Created_By", "varchar(12) NOT NULL")
    '        coll.Add("Created_Date", "datetime NOT NULL")
    '        coll.Add("Modified_By", "varchar(12) NOT NULL")
    '        coll.Add("Modified_Date", "datetime NOT NULL")
    '        coll.Add("Posted_By", "varchar(12) NULL")
    '        coll.Add("Posted_Date", "datetime NULL")
    '        clsCommonFunctionality.CreateOrAlterTable(False, False, "TSPL_BLK_FREIGHT_MASTER", coll, Nothing, True, False, Nothing, Nothing, Nothing, True)


    '        SetUserMgmtNew()
    '        LoadBlankGrid()
    '        Addnew()
    '        btnPost.Visible = True
    '        btnPost.Enabled = False
    '        If clsCommon.myLen(txtDocumentNo.Value) > 0 Then
    '            LoadData(clsCommon.myCstr(txtDocumentNo.Value), NavigatorType.Current)
    '        End If
    '    End Sub
    '    Private Sub SetUserMgmtNew()
    '        If Not (MyBase.isReadFlag) Then
    '            Throw New Exception("Permission Denied")
    '        End If
    '        btnsave.Visible = MyBase.isModifyFlag
    '        btndelete.Visible = MyBase.isDeleteFlag
    '        RadSplitButton1.Visible = MyBase.isExport
    '        btnPost.Visible = MyBase.isPostFlag
    '        If MyBase.isExport = True Then
    '            rmExport.Enabled = True
    '            rmimport.Enabled = True
    '        Else
    '            rmExport.Enabled = False
    '            rmimport.Enabled = False
    '        End If

    '    End Sub

    '    Private Sub txtCustomer__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtBullCode._MYValidating
    '        Dim qry As String = "select TSPL_CUSTOMER_MASTER.Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_MASTER"
    '        txtBullCode.Value = clsCommon.ShowSelectForm("BlkFreightMstr", qry, "Code", "", txtBullCode.Value, "Code", isButtonClicked)
    '        lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + txtBullCode.Value + "' ")
    '    End Sub

    '    Private Sub txtDocumentNo__MYNavigator(sender As Object, e As EventArgs, NavType As common.NavigatorType) Handles txtDocumentNo._MYNavigator
    '        Try
    '            Dim qry As String = "select count(*) from TSPL_BLK_FREIGHT_MASTER where Document_Code='" + txtDocumentNo.Value + "' "
    '            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
    '            If count = 0 Then
    '                txtDocumentNo.MyReadOnly = False
    '            Else
    '                txtDocumentNo.MyReadOnly = True
    '            End If
    '            LoadData(txtDocumentNo.Value, NavType)
    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub

    '    Private Sub txtDocumentNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocumentNo._MYValidating
    '        If clsCommon.myLen(txtDocumentNo) <= 0 Then
    '            clsCommon.MyMessageBoxShow(Me, "Document No can't be blank", Me.Text)
    '        End If
    '        txtDocumentNo.Value = clsBullVaccinationEntry.getFinder(txtDocumentNo.Value, isButtonClicked)
    '        LoadData(txtDocumentNo.Value, NavigatorType.Current)
    '    End Sub

    '    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
    '        Addnew()
    '    End Sub
    '    Private Sub frmBullVaccinationEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    '        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
    '            btnAddNew.PerformClick()
    '        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled AndAlso MyBase.isModifyFlag Then
    '            SaveData()
    '        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnsave.Enabled AndAlso MyBase.isDeleteFlag Then
    '            btndelete.PerformClick()
    '        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
    '            btnPost.PerformClick()
    '        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnclose.Enabled Then
    '            Me.Close()
    '        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
    '            If MyBase.isReverse Then

    '                Dim frm As New FrmPWD(Nothing)
    '                frm.strType = "SIRC"
    '                frm.strCode = "SIReversAndCreate"
    '                frm.ShowDialog()
    '                If frm.isPasswordCorrect Then
    '                    btnReverseUnpost.Visible = True
    '                End If
    '            Else
    '                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
    '                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '            End If
    '        End If
    '    End Sub
    '    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
    '        CancelPressed()
    '    End Sub
    '    Private Sub btnReverseUnpost_Click_(sender As Object, e As EventArgs) Handles btnReverseUnpost.Click
    '        Try
    '            If clsCommon.MyMessageBoxShow(Me, "Do you want to Reverse and unpost the current Document" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
    '                Dim Reason As String = ""
    '                Dim frm As New FrmFreeTxtBox1
    '                frm.Text = "Remarks for Reverse"
    '                frm.ShowDialog()
    '                If clsCommon.myLen(frm.strRmks) <= 0 Then
    '                    Exit Sub
    '                Else
    '                    Reason = frm.strRmks
    '                End If
    '                If clsBullVaccinationEntry.ReverseAndUnpost(txtDocumentNo.Value) Then
    '                    clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
    '                    LoadData(txtDocumentNo.Value, NavigatorType.Current)
    '                End If
    '            End If
    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub
    '    Sub CancelPressed()
    '        Me.Close()
    '    End Sub

    '    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click

    '        Try
    '            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
    '                Throw New Exception("No document found to post")
    '            End If
    '            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + txtDocumentNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, WinControls.RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
    '                clsBullVaccinationEntry.PostData(MyBase.Form_ID, txtDocumentNo.Value)
    '                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
    '                LoadData(txtDocumentNo.Value, NavigatorType.Current)
    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub

    '    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
    '        SaveData()
    '    End Sub

    '    Function AllowToSave() As Boolean

    '        If clsCommon.myLen(txtBullCode.Value) <= 0 Then
    '            txtBullCode.Focus()
    '            clsCommon.MyMessageBoxShow("Bull Code can't be blank.")
    '        End If
    '        Return True
    '    End Function

    '    Sub SaveData()
    '        Try
    '            If (AllowToSave()) Then
    '                Dim obj As New clsBullVaccinationEntry()
    '                obj.Document_Code = clsCommon.myCstr(txtDocumentNo.Value)
    '                obj.Document_date = clsCommon.myCDate(txtdate.Value)
    '                obj.Customer_Code = clsCommon.myCstr(txtCustomer.Value)
    '                obj.Start_Date = clsCommon.myCDate(txtStartDate.Value)
    '                obj.Arr = New List(Of clsBullVaccinationEntryDetail)

    '                For Each grow As GridViewRowInfo In gv1.Rows
    '                    Dim objTr As New clsBullVaccinationEntryDetail()
    '                    If clsCommon.myCdbl((grow.Cells(colTenderQty).Value)) > 0 Then
    '                        objTr.SNO = grow.Cells(colSNo).Value
    '                        objTr.Tender_Qty = clsCommon.myCdbl((grow.Cells(colTenderQty).Value))
    '                        objTr.Rate = clsCommon.myCDecimal(grow.Cells(colRate).Value)
    '                        objTr.Pro_Rate = clsCommon.myCDecimal(grow.Cells(colProRate).Value)
    '                        objTr.Applicable_Rate = clsCommon.myCDecimal(grow.Cells(colApplicableRate).Value)
    '                        objTr.Payable_Amount = clsCommon.myCDecimal(grow.Cells(colPayableAmount).Value)
    '                        objTr.DieselPetrol = clsCommon.myCDecimal(grow.Cells(colDieselPetrol).Value)
    '                        objTr.GPS_KM = clsCommon.myCDecimal(grow.Cells(colGPSKM).Value)
    '                        obj.Arr.Add(objTr)
    '                    End If

    '                Next
    '                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
    '                    clsCommon.MyMessageBoxShow("Please Fill at list one Item")
    '                End If
    '                If (obj.SaveData(obj, isNewEntry, Nothing, False)) Then
    '                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
    '                    LoadData(obj.Document_Code, NavigatorType.Current)
    '                End If
    '            End If
    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub

    '    Private Sub Addnew()
    '        isNewEntry = True
    '        txtDocumentNo.Value = ""
    '        btnsave.Enabled = True
    '        btnPost.Enabled = True
    '        txtdate.Value = clsCommon.GETSERVERDATE()
    '        txtStartDate.Value = clsCommon.GETSERVERDATE()
    '        btnsave.Text = "Save"
    '        isInsideLoadData = False
    '        btndelete.Enabled = True
    '        txtCustomer.Value = ""
    '        lblCustomerName.Text = ""
    '        lblStatus.Status = ERPTransactionStatus.Pending
    '        LoadBlankGrid()
    '        gv1.Rows.AddNew()

    '    End Sub
    '    Private Sub LoadBlankGrid()
    '        gv1.DataSource = Nothing
    '        gv1.Rows.Clear()
    '        gv1.Columns.Clear()
    '        gv1.AllowDeleteRow = True
    '        gv1.AllowAddNewRow = False
    '        gv1.AddNewRowPosition = SystemRowPosition.Bottom
    '        Dim repoNumBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '        repoNumBox.Name = colSNo
    '        repoNumBox.Width = 60
    '        repoNumBox.ReadOnly = True
    '        repoNumBox.HeaderText = "SNO"
    '        gv1.MasterTemplate.Columns.Add(repoNumBox)

    '        Dim repoTenderQty As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    '        repoTenderQty.FormatString = ""
    '        repoTenderQty.Width = 100
    '        repoTenderQty.HeaderText = "Tender Qty"
    '        repoTenderQty.Name = colTenderQty
    '        repoTenderQty.IsVisible = True
    '        repoTenderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoTenderQty)

    '        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
    '        repoRate.FormatString = ""
    '        repoRate.Width = 100
    '        repoRate.HeaderText = "Rate Per 9KL"
    '        repoRate.Name = colRate
    '        repoRate.IsVisible = True
    '        repoRate.Minimum = 0
    '        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoRate)

    '        Dim repoProRate As GridViewDecimalColumn = New GridViewDecimalColumn()
    '        repoProRate.FormatString = ""
    '        repoProRate.Width = 120
    '        repoProRate.HeaderText = "Pro-Rate Payable Rate"
    '        repoProRate.Name = colProRate
    '        repoProRate.IsVisible = True
    '        repoProRate.Minimum = 0
    '        repoProRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoProRate)

    '        Dim repoDieselPetrol As GridViewDecimalColumn = New GridViewDecimalColumn()
    '        repoDieselPetrol = New GridViewDecimalColumn()
    '        repoDieselPetrol.FormatString = ""
    '        repoDieselPetrol.Width = 120
    '        repoDieselPetrol.HeaderText = "Diesel Hike/Red."
    '        repoDieselPetrol.Name = colDieselPetrol
    '        repoDieselPetrol.IsVisible = True
    '        repoDieselPetrol.Minimum = 0
    '        repoDieselPetrol.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoDieselPetrol)

    '        Dim repoApplicableRate As GridViewDecimalColumn = New GridViewDecimalColumn()
    '        repoApplicableRate.FormatString = ""
    '        repoApplicableRate.Width = 100
    '        repoApplicableRate.HeaderText = "Applicable Rate"
    '        repoApplicableRate.Name = colApplicableRate
    '        repoApplicableRate.IsVisible = True
    '        repoApplicableRate.Minimum = 0
    '        repoApplicableRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoApplicableRate)

    '        Dim repoGPSKM As GridViewDecimalColumn = New GridViewDecimalColumn()
    '        repoGPSKM.FormatString = ""
    '        repoGPSKM.Width = 90
    '        repoGPSKM.HeaderText = "GPS KM"
    '        repoGPSKM.Name = colGPSKM
    '        repoGPSKM.IsVisible = True
    '        repoGPSKM.Minimum = 0
    '        repoGPSKM.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoGPSKM)

    '        Dim repoPayableAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
    '        repoPayableAmount.FormatString = ""
    '        repoPayableAmount.Width = 120
    '        repoPayableAmount.HeaderText = "Payable Amount"
    '        repoPayableAmount.Name = colPayableAmount
    '        repoPayableAmount.IsVisible = True
    '        repoPayableAmount.Minimum = 0
    '        repoPayableAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
    '        gv1.MasterTemplate.Columns.Add(repoPayableAmount)

    '        gv1.AllowAddNewRow = False
    '        gv1.ShowGroupPanel = False
    '        gv1.AllowColumnReorder = True
    '        gv1.AllowRowReorder = False
    '        gv1.EnableSorting = False
    '        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
    '        gv1.MasterTemplate.ShowRowHeaderColumn = False

    '        ReStoreGridLayout()
    '    End Sub

    '    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
    '        Try
    '            btnsave.Enabled = True
    '            btnPost.Enabled = True

    '            Addnew()
    '            Dim obj As New clsBullVaccinationEntry()
    '            obj = clsBullVaccinationEntry.GetData(strCode, NavTyep, Nothing)
    '            If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.Document_Code)) > 0) Then
    '                txtDocumentNo.Value = obj.Document_Code
    '                txtdate.Value = obj.Document_date
    '                txtStartDate.Value = obj.Start_Date
    '                txtCustomer.Value = obj.Customer_Code
    '                lblCustomerName.Text = clsDBFuncationality.getSingleValue("Select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code  ='" + txtCustomer.Value + "' ")

    '                isNewEntry = False
    '                btnsave.Text = "Update"
    '                If obj.Status = 1 Then
    '                    lblStatus.Status = ERPTransactionStatus.Approved
    '                    btndelete.Enabled = False
    '                    btnsave.Enabled = False
    '                    btnPost.Enabled = False
    '                Else
    '                    lblStatus.Status = ERPTransactionStatus.Pending
    '                    btndelete.Enabled = True
    '                End If

    '                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
    '                    For Each objTr As clsBullVaccinationEntryDetail In obj.Arr
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = objTr.SNO
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTenderQty).Value = objTr.Tender_Qty
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = objTr.Rate
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colProRate).Value = objTr.Pro_Rate
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colApplicableRate).Value = objTr.Applicable_Rate
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colDieselPetrol).Value = objTr.DieselPetrol
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colGPSKM).Value = objTr.GPS_KM
    '                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPayableAmount).Value = objTr.Payable_Amount

    '                    Next
    '                End If

    '            End If

    '            isLoadData = True
    '            isInsideLoadData = True
    '            isInsideLoadData = False

    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        Finally

    '        End Try
    '    End Sub
    '    Private Sub ReStoreGridLayout()
    '        Try

    '            Dim obj As clsGridLayout = New clsGridLayout()
    '            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
    '            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
    '                Dim ii As Integer
    '                For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
    '                    gv1.Columns(ii).IsVisible = False
    '                    gv1.Columns(ii).VisibleInColumnChooser = True
    '                Next

    '                gv1.LoadLayout(obj.GridLayout)
    '                obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '            End If
    '        Catch err As Exception
    '            MessageBox.Show(err.Message)
    '        End Try
    '    End Sub

    '    Private Sub gv1_UserDeletedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserDeletedRow
    '        For ii As Integer = 1 To gv1.Rows.Count
    '            gv1.Rows(ii - 1).Cells(colSNo).Value = ii
    '        Next
    '    End Sub

    '    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
    '        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
    '            e.Cancel = True
    '            Exit Sub
    '        Else
    '            e.Cancel = False
    '        End If
    '    End Sub

    '    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
    '        'For i As Integer = 0 To gv1.Rows.Count - 1
    '        '    gv1.Rows(0).Cells(0).Value = 1
    '        '    If i <> 0 Then
    '        '        gv1.Rows(i).Cells(colSNo).Value = i + 1
    '        '    End If
    '        'Next
    '    End Sub
    '    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
    '        If gv1.RowCount > 0 Then
    '            Dim intCurrRow As Integer = gv1.CurrentRow.Index
    '            gv1.CurrentRow.Cells(colSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
    '            If intCurrRow = gv1.Rows.Count - 1 Then
    '                ' gv1.Rows.AddNew()
    '                gv1.CurrentRow = gv1.Rows(intCurrRow)
    '            End If
    '        End If
    '    End Sub

    '    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
    '        Try
    '            If clsCommon.myLen(txtDocumentNo.Value) <= 0 Then
    '                Throw New Exception("Document No not found to delete")
    '            End If
    '            If (myMessages.deleteConfirm()) Then
    '                clsBullVaccinationEntry.DeleteData(txtDocumentNo.Value)
    '                clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully", Me.Text)
    '                Addnew()
    '            End If
    '        Catch ex As Exception
    '            myMessages.myExceptions(ex)
    '        End Try
    '    End Sub


    '    Private Sub rmimport_Click(sender As Object, e As EventArgs) Handles rmimport.Click
    '        Try

    '            Dim gvImport As New RadGridView()
    '            Me.Controls.Add(gvImport)
    '            Dim currentdate As Date = Date.Today
    '            If transportSql.importExcel(gvImport, "SNO", "Tender Qty", "Rate", "Pro Rate", "Diesel Hike/Red.", "Applicable Rate", "GPS KM", "Payable Amount") Then
    '                Dim Arr As New List(Of clsBullVaccinationEntryDetail)

    '                Try
    '                    clsCommon.ProgressBarPercentShow()
    '                    For ii As Integer = 0 To gvImport.Rows.Count - 1
    '                        If clsCommon.myLen(gvImport.Rows(ii).Cells("Tender Qty").Value) > 0 Then

    '                            clsCommon.ProgressBarPercentUpdate((gvImport.Rows(ii).Index + 1) * 100 / (gvImport.Rows.Count + 1), "Importing  : " & (gvImport.Rows(ii).Index + 1) & "/" & gvImport.Rows.Count & "")
    '                            Try

    '                                gv1.Rows(ii).Cells(colSNo).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("SNO").Value)
    '                                gv1.Rows(ii).Cells(colTenderQty).Value = clsCommon.myCDecimal(gvImport.Rows(ii).Cells("Tender Qty").Value)
    '                                gv1.Rows(ii).Cells(colRate).Value = clsCommon.myCDecimal(gvImport.Rows(ii).Cells("Rate").Value)
    '                                gv1.Rows(ii).Cells(colProRate).Value = clsCommon.myCDecimal(gvImport.Rows(ii).Cells("Pro Rate").Value)
    '                                gv1.Rows(ii).Cells(colApplicableRate).Value = clsCommon.myCDecimal(gvImport.Rows(ii).Cells("Applicable Rate").Value)
    '                                gv1.Rows(ii).Cells(colGPSKM).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("GPS KM").Value)
    '                                gv1.Rows(ii).Cells(colPayableAmount).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells("Payable Amount").Value)

    '                                If clsCommon.myLen(txtDocumentNo.Value) = 0 Then
    '                                    If gv1.Rows.Count = gvImport.Rows.Count Then
    '                                    Else
    '                                        gv1.Rows.AddNew()

    '                                    End If
    '                                End If

    '                            Catch ex As Exception
    '                                gv1.Rows.RemoveAt(ii)
    '                                clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '                            End Try
    '                        End If
    '                    Next

    '                    clsCommon.ProgressBarPercentHide()
    '                    common.clsCommon.MyMessageBoxShow(Me, "Data imported successfully", Me.Text, MessageBoxButtons.OK)
    '                Catch ex As Exception
    '                    clsCommon.ProgressBarPercentHide()
    '                    Throw New Exception(ex.Message)
    '                End Try
    '            End If
    '            Me.Controls.Remove(gvImport)
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub

    '    Private Sub btnDeleteLayout_Click(sender As Object, e As EventArgs) Handles btnDeleteLayout.Click
    '        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
    '    End Sub

    '    Private Sub btnSaveLayout_Click(sender As Object, e As EventArgs) Handles btnSaveLayout.Click
    '        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
    '            gv1.MasterTemplate.FilterDescriptors.Clear()
    '            Dim obj As New clsGridLayout()
    '            obj.ReportID = MyBase.Form_ID
    '            obj.UserID = objCommonVar.CurrentUserCode
    '            obj.GridLayout = New MemoryStream()
    '            gv1.SaveLayout(obj.GridLayout)
    '            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
    '            obj.GridColumns = gv1.ColumnCount
    '            If obj.SaveData() Then
    '                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
    '            End If
    '            ''stuti regarding memory leakage
    '            obj.GridLayout.Close()
    '            obj.GridLayout.Dispose()
    '        End If
    '    End Sub

    '    Private Sub btnExcel_Click(sender As Object, e As EventArgs) Handles btnExcel.Click
    '        Try
    '            If gv1.Rows.Count > 0 Then
    '                Dim arrHeader As List(Of String) = New List(Of String)()
    '                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
    '                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.BulkSaleFreightMaster & "'"))
    '                arrHeader.Add("Date : " & clsCommon.myCDate(txtdate.Value))
    '                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
    '            Else
    '                clsCommon.MyMessageBoxShow(Me, "No data found to export", Me.Text)
    '            End If
    '        Catch ex As Exception
    '            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub

    '    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
    '        Try
    '            If gv1.Rows.Count >= 0 Then

    '                clsCommon.MyExportToPDF("Bulk Sale Freight Master", gv1, Nothing, "Bulk Sale Freight Master", MyBase.Form_ID, objCommonVar.CurrentUserCode)
    '            Else
    '                Throw New Exception("no record found.")
    '            End If
    '        Catch ex As Exception
    '            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '        End Try
    '    End Sub

    '    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
    '        Dim qry As String = "select '' as SNO ,'' AS [Tender Qty] , '' AS Rate , '' as [Pro Rate] ,'' as [Diesel Hike/Red.], '' as [Applicable Rate] , '' as [GPS KM] , '' [Payable Amount]"
    '        transportSql.ExporttoExcelWithoutFilter(qry, "", "", Me)
    '    End Sub
End Class