
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmItemConversion
    Inherits FrmMainTranScreen

#Region "Variables"

    Public Const RowTypeAdjustmentQty As String = "Quantity"
    Public Const RowTypeAdjustmentCost As String = "Cost"
    Public Const RowTypeAdjustmentBoth As String = "Both"

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isFormLoad As Boolean = False

    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colUnit As String = "COLUNIT"
    Const colRemarks As String = "REMARKS"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public strDocumentNo As String
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmItemConversion)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        ' btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If btnSave.Visible = True Then
            RmiExport.Enabled = True
        Else
            RmiExport.Enabled = False
        End If
    End Sub

    Sub Load_Conversion_Type()
        Try
            isFormLoad = True
            Dim dt As New DataTable()
            dt.Columns.Add("Code", GetType(String))
            dt.Columns.Add("Name", GetType(String))
            dt.Rows.Add("O", "One To Many")
            dt.Rows.Add("M", "Many To One")
            CmbConversion.DataSource = dt
            CmbConversion.ValueMember = "Code"
            CmbConversion.DisplayMember = "Name"
        Catch ex As Exception
        Finally
            isFormLoad = False
        End Try
    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
    End Sub
    Private Sub fndItemCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles frnItemCode._MYValidating
        Try
            Dim obj As New clsItemMaster
            obj = clsItemMaster.FinderForItem(frnItemCode.Value, "", True)
            If obj IsNot Nothing Then
                LoadData_Item(obj.Item_Code, NavigatorType.Current)
                frnItemCode.Value = obj.Item_Code
                lblItemDesc.Text = obj.Item_Desc
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub

    Private Sub FrmItemConversion_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            'Add Tool tip Task No- TEC/22/05/18-000245
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine + _
                                   "TSPL_Item_Conversion_Head " + Environment.NewLine + _
                                   "TSPL_Item_Conversion_DETAIL ")
            'Add Tool tip Task No- TEC/22/05/18-000245
        End If

    End Sub

    Private Sub FrmItemConversion_Layout(ByVal sender As Object, ByVal e As System.Windows.Forms.LayoutEventArgs) Handles Me.Layout

    End Sub
    Private Sub FrmItemConversion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        SetLength()


        LoadBlankGrid()
        Load_Conversion_Type()
        AddNew()

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit)

       
        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remark"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 300
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        ' btnPost.Enabled = True
        btnDelete.Enabled = False
        'Load_Conversion_Type()
        CmbConversion.SelectedValue = 0
        lblItemCode.Text = "Main Item Code"
        RadGroupBox2.Text = "Converted Item Details"
        CmbConversion.Enabled = True
        gv1.Rows.AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Sub BlankAllControls()
        txtDesc.Text = ""
        txtDocNo.Value = ""
        frnItemCode.Value = ""
        lblItemDesc.Text = ""
        isCellValueChangedOpen = True
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If isCellValueChangedOpen Then
                    isCellValueChangedOpen = False
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colRemarks) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        End If
                    End If
                    isCellValueChangedOpen = True
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "", isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = obj.Unit_Code
        Else
            SetBlankOfItemColumns()
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Item Code", Me.Text)
            Exit Sub
        End If
        Dim qry As String = "select  UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL "
        Dim WhrCls As String = "Item_Code ='" + strICode + "'"
        gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("ExpiryDateUOM", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
    End Sub

    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
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
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean

        If clsCommon.myLen(frnItemCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please Fill Main Item...", Me.Text)
            Return False
        End If

        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)

            If clsCommon.myLen(strICode) > 0 Then
                If clsCommon.myLen(strUOM) <= 0 Then
                    clsCommon.MyMessageBoxShow("Please enter UOM of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then

                Dim obj As New clsItemConversion()

                
                obj.Doc_Code = txtDocNo.Value
                obj.Description = txtDesc.Text
                'obj.Posted()
                obj.Item_Code = frnItemCode.Value
                obj.Conv_Type = CmbConversion.SelectedValue

                obj.Arr = New List(Of clsItemConversionDetails)()
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsItemConversionDetails()
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Description = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objTr.Line_No = clsCommon.myCstr(grow.Cells(colLineNo).Value)
                    objTr.Doc_Code = txtDocNo.Value
                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry, clsCommon.myCstr(txtDocNo.Value), Nothing)
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Doc_Code, NavigatorType.Current)
                Return isSaved
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            'btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()


            Dim obj As New clsItemConversion()
            obj = clsItemConversion.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
                'If obj.POSTED = ERPTransactionStatus.Approved Then
                '    btnSave.Enabled = False
                '    ' btnPost.Enabled = False
                '    btnDelete.Enabled = False
                'End If

               
                txtDocNo.Value = obj.Doc_Code
                txtDesc.Text = obj.Description
                frnItemCode.Value = obj.Item_Code
                lblItemDesc.Text = obj.Item_Desc
                UsLock1.Status = obj.POSTED
                '' Anubhooti 10-Dec-2014
                CmbConversion.Enabled = False
                ''
                If clsCommon.myLen(obj.Conv_Type) > 0 Then
                    CmbConversion.SelectedValue = clsCommon.myCstr(obj.Conv_Type)
                End If
                If clsCommon.CompairString(CmbConversion.SelectedValue, "O") = CompairStringResult.Equal Then
                    lblItemCode.Text = "Main Item Code"
                    RadGroupBox2.Text = "Converted Item Details"
                ElseIf clsCommon.CompairString(CmbConversion.SelectedValue, "M") = CompairStringResult.Equal Then
                    lblItemCode.Text = "Converted Item Code"
                    RadGroupBox2.Text = "Main Item Code"
                End If
                ''
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsItemConversionDetails In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                    Next

                    If obj.POSTED = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                        'UsLock1.Status = ERPTransactionStatus.Pending
                    Else
                        'UsLock1.Status = ERPTransactionStatus.Approved
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub LoadData_Item(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            'btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Save"
            BlankAllControls()
            LoadBlankGrid()


            Dim obj As New clsItemConversion()
            obj = clsItemConversion.GetData_via_Item(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Doc_Code) > 0) Then
                'If obj.POSTED = ERPTransactionStatus.Approved Then
                '    btnSave.Enabled = False
                '    '   btnPost.Enabled = False
                '    btnDelete.Enabled = False
                'End If


                txtDocNo.Value = obj.Doc_Code
                txtDesc.Text = obj.Description
                frnItemCode.Value = obj.Item_Code
                lblItemDesc.Text = obj.Item_Desc
                UsLock1.Status = obj.POSTED
                '' Anubhooti 10-Dec-2014
                If clsCommon.myLen(obj.Conv_Type) > 0 Then
                    CmbConversion.SelectedValue = clsCommon.myCstr(obj.Conv_Type)
                End If
                If clsCommon.CompairString(CmbConversion.SelectedValue, "O") = CompairStringResult.Equal Then
                    lblItemCode.Text = "Main Item Code"
                    RadGroupBox2.Text = "Converted Item Details"
                ElseIf clsCommon.CompairString(CmbConversion.SelectedValue, "M") = CompairStringResult.Equal Then
                    lblItemCode.Text = "Converted Item Code"
                    RadGroupBox2.Text = "Main Item Code"
                End If
                ''
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsItemConversionDetails In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Line_No
                    Next

                    If obj.POSTED = ERPTransactionStatus.Pending Then
                        gv1.Rows.AddNew()
                        'UsLock1.Status = ERPTransactionStatus.Pending
                    Else
                        'UsLock1.Status = ERPTransactionStatus.Approved
                    End If
                End If
                btnSave.Text = "Update"
            Else
                gv1.Rows.AddNew()
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (AllowToSave()) Then
                If (myMessages.postConfirm()) Then
                    If (clsItemConversion.PostData(txtDocNo.Value, True)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
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
                If (clsItemConversion.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from tspl_item_Conversion_Head where Doc_code='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "select Doc_Code as [Doc_Code],Item_COde as [Item Code],Description from TSPL_ITEM_CONVERSION_Head "
        txtDocNo.Value = clsCommon.ShowSelectForm("Item_Conv", qry, "Doc_Code", "", txtDocNo.Value, "Doc_Code", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
    End Sub


    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'System.Diagnostics.Process.Start(Application.StartupPath & "\help file riva moda\Riva Help File.chm")
        PrintData()
    End Sub
    Sub PrintData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Transaction No not found to print")
            End If
            PrintData(txtDocNo.Value, False)
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub
    Public Shared Sub PrintData(ByVal strDocNo As String, ByVal IsPreprinted As Boolean)
        Try

            Dim qry As String
            Dim dt As DataTable
            qry = "select * from TSPL_Item_Conversion_HEAD left outer  join TSPL_Item_Conversion_DETAIL on TSPL_Item_Conversion_HEAD.Doc_Code=TSPL_Item_Conversion_DETAIL.Doc_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Item_Conversion_HEAD.loc_code left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_Item_Conversion_HEAD.comp_code " & _
                 " where TSPL_Item_Conversion_HEAD.Doc_Code='" + strDocNo + "' ORDER by document_line_no"
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            If IsPreprinted Then
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptExpiryDetails", "Expired Item Entry")
            Else
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "crptExpiryDetails", "Expired Item Entry")
            End If
            frmCRV = Nothing
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub
    '' Anubhooti 10-Dec-2014
    Private Sub CmbConversion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles CmbConversion.SelectedIndexChanged
        If Not isFormLoad Then
            If clsCommon.CompairString(clsCommon.myCstr(CmbConversion.SelectedValue), "O") = CompairStringResult.Equal Then
                lblItemCode.Text = "Main Item Code"
                RadGroupBox2.Text = "Converted Item Details"
            ElseIf clsCommon.CompairString(CmbConversion.SelectedValue, "M") = CompairStringResult.Equal Then
                lblItemCode.Text = "Converted Item Code"
                RadGroupBox2.Text = "Main Item Code"
            End If
        End If
    End Sub
End Class
