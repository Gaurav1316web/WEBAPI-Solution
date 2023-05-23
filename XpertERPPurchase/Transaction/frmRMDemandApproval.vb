Imports System.Data.SqlClient
Imports System.IO
Imports common

Public Class frmRMDemandApproval
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim blnPageLoad As Boolean = False
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isPageLoadData As Boolean = False
    Const colSNO As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colUnit As String = "COLUNIT"
    Const colGLCode As String = "colGLCode"
    Const ColGQtyIndent As String = "ColGQtyIndent"
    Const ColGQtyStock As String = "ColGQtyStock"
    Const ColGQtyApprove As String = "ColGQtyApprove"
    Const colTotal As String = "colTotal"
    Const colTotalstock As String = "colTotalstock"

    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim DTLocation As DataTable
    Public isDeleteTheAttachment As Boolean = True
    Public MandatoryPDFFile As Boolean = False
    Public settAutoAttachment As Boolean = False
    Public Form_ID As String = ""
    Public Transaction_ID As String = ""
#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        If MyBase.isReverse Then
            btnreverse.Enabled = True
        Else
            btnreverse.Enabled = False
        End If
    End Sub
    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isPageLoadData = True
        SetUserMgmtNew()
        UcAttachment1.Form_ID = MyBase.Form_ID
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        DTLocation = clsDBFuncationality.GetDataTable("select Location_Code,Loc_Short_Name from TSPL_LOCATION_MASTER where IsMainPlant=0 order by Location_Code")
        LoadBlankGrid()
        AddNew()
        isPageLoadData = False
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub BlankAllControls()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtDate.Value
        txtToDate.Value = txtDate.Value
        UsLock1.Status = ERPTransactionStatus.Pending
        UcAttachment1.BlankAllControls()
        LoadBlankGrid()
    End Sub
    Sub LoadBlankGrid()

        Dim viewBlank As New TableViewDefinition()
        gv1.ViewDefinition = viewBlank

        gv1.Rows.Clear()
        gv1.Columns.Clear()


        Dim view As New ColumnGroupsViewDefinition()
        view.ColumnGroups.Add(New GridViewColumnGroup(""))
        view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "SNo"
        repoLineNo.Name = colSNO
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)
        view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(colSNO)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.Width = 100
        repoICode.IsVisible = True
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)
        view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(colICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoIName)
        view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(colIName)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 70
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)
        view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(colUnit)
        view.ColumnGroups(view.ColumnGroups.Count - 1).IsPinned = True

        If DTLocation IsNot Nothing AndAlso DTLocation.Rows.Count > 0 Then
            For ii As Integer = 0 To DTLocation.Rows.Count - 1
                view.ColumnGroups.Add(New GridViewColumnGroup(DTLocation.Rows(ii)("Loc_Short_Name")))
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())

                Dim repoLCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
                repoLCode.FormatString = ""
                repoLCode.HeaderText = "Location"
                repoLCode.Name = colGLCode + DTLocation.Rows(ii)("Location_Code")
                repoLCode.Tag = DTLocation.Rows(ii)("Location_Code")
                repoLCode.ReadOnly = True
                repoLCode.IsVisible = False
                gv1.MasterTemplate.Columns.Add(repoLCode)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(repoLCode.Name)

                Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
                repoQty = New GridViewDecimalColumn()
                repoQty.FormatString = ""
                repoQty.HeaderText = "Demand"
                repoQty.Name = ColGQtyIndent + DTLocation.Rows(ii)("Location_Code")
                repoQty.Width = 80
                repoQty.Minimum = 0
                repoQty.ShowUpDownButtons = False
                repoQty.Step = 0
                repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                repoQty.ReadOnly = True
                gv1.MasterTemplate.Columns.Add(repoQty)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(repoQty.Name)

                repoQty = New GridViewDecimalColumn()
                repoQty.FormatString = ""
                repoQty.HeaderText = "Stock"
                repoQty.Name = ColGQtyStock + DTLocation.Rows(ii)("Location_Code")
                repoQty.Width = 80
                repoQty.Minimum = 0
                repoQty.ShowUpDownButtons = False
                repoQty.Step = 0
                repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                repoQty.ReadOnly = True
                gv1.MasterTemplate.Columns.Add(repoQty)

                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(repoQty.Name)

                repoQty = New GridViewDecimalColumn()
                repoQty.FormatString = ""
                repoQty.HeaderText = "Approved"
                repoQty.Name = ColGQtyApprove + DTLocation.Rows(ii)("Location_Code")
                repoQty.Width = 80
                repoQty.Minimum = 0
                repoQty.ShowUpDownButtons = False
                repoQty.Step = 0
                repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                repoQty.ReadOnly = False
                gv1.MasterTemplate.Columns.Add(repoQty)
                view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(repoQty.Name)
            Next

            ''''shaurya kumar
            'view.ColumnGroups.Add(New GridViewColumnGroup("Total Stock"))
            'view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())

            view.ColumnGroups.Add(New GridViewColumnGroup("Total"))
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows.Add(New GridViewColumnGroupRow())
            Dim repoTotalstock As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoTotalstock = New GridViewDecimalColumn()
            repoTotalstock.FormatString = ""
            repoTotalstock.HeaderText = "Stock"
            repoTotalstock.Name = colTotalstock
            repoTotalstock.Width = 80
            repoTotalstock.ReadOnly = True
            repoTotalstock.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gv1.MasterTemplate.Columns.Add(repoTotalstock)
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(colTotalstock)


            Dim repoRMTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoRMTotal = New GridViewDecimalColumn()
            repoRMTotal.FormatString = ""
            repoRMTotal.HeaderText = "Approved"
            repoRMTotal.Name = colTotal
            repoRMTotal.Width = 80
            repoRMTotal.ReadOnly = True
            repoRMTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
            gv1.MasterTemplate.Columns.Add(repoRMTotal)
            view.ColumnGroups(view.ColumnGroups.Count - 1).Rows(0).ColumnNames.Add(colTotal)

        End If


        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.ViewDefinition = view
        gv1.TableElement.TableHeaderHeight = 80
    End Sub
    Sub AddNew()
        BlankAllControls()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        txtDate.Enabled = True
        txtDocNo.MyReadOnly = False
        SetFromToDate(True)
    End Sub
    Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        Try
            UcAttachment1.AllowToSave()
            'If clsCommon.myLen(txtDocNo.Value) <= 0 Then
            '    common.clsCommon.MyMessageBoxShow(Me, "Please Enter Tender No", Me.Text)
            '    txtDocNo.Focus()
            '    Return False
            'End If

            'If AllowToSaveRAL_Type(trans) = False Then
            '    Return False
            'End If

            'Dim dblTotalAmount As Double = 0
            'For ii As Integer = 0 To gv1.Rows.Count - 1
            '    Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            '    Dim strVCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colVCode).Value)
            '    Dim strLCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colGLCode).Value)

            '    Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
            '    Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(ColGQtyIndent).Value)
            '    Dim dblrate As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colRate).Value)
            '    Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            '    Dim dblAmount As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colAmt).Value)


            '    dblTotalAmount = dblTotalAmount + dblAmount

            '    If clsCommon.myLen(strICode) <= 0 Then
            '        common.clsCommon.MyMessageBoxShow(Me, "Please enter Item At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
            '        Return False
            '    End If

            '    If clsCommon.myLen(strLCode) <= 0 Then
            '        common.clsCommon.MyMessageBoxShow(Me, "Please enter Location for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
            '        Return False
            '    End If

            '    If clsCommon.myLen(strVCode) <= 0 Then
            '        common.clsCommon.MyMessageBoxShow(Me, "Please enter Vendor for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
            '        Return False
            '    End If

            '    If clsCommon.myLen(strUOM) <= 0 Then
            '        common.clsCommon.MyMessageBoxShow(Me, "Please enter Quantity UOM for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
            '        Return False
            '    End If

            '    If dblQty <= 0 AndAlso clsCommon.myCDecimal(cboTenderType.SelectedValue) <> 2 Then
            '        common.clsCommon.MyMessageBoxShow(Me, "Please enter Booked Quantity for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
            '        Return False
            '    End If

            '    If dblrate <= 0 Then
            '        clsCommon.MyMessageBoxShow(Me, "Please enter Booked Rate for " + strIName + ". At Line No" + clsCommon.myCstr(ii + 1), Me.Text)
            '        Return False
            '    End If

            '    If (clsCommon.myLen(strICode) > 0) Then

            '        For jj As Integer = 0 To gv1.Rows.Count - 1
            '            If jj = ii Then
            '                Continue For
            '            End If
            '            Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
            '            Dim strInnerLCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colGLCode).Value)
            '            Dim strInnerVCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colVCode).Value)
            '            Dim strInnerUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)

            '            If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strLCode, strInnerLCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strVCode, strInnerVCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInnerUOM) = CompairStringResult.Equal Then
            '                Dim Msg As String = "Same Detail Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)
            '                common.clsCommon.MyMessageBoxShow(Me, Msg, Me.Text)
            '                Return False
            '            End If

            '        Next
            '    End If
            'Next
            'RefreshSerialNo()
            'lblTotalDocAmt.Text = Math.Round(clsCommon.myCdbl(dblTotalAmount), 2)

            ''UpdateAllTotals()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If UcAttachment1.Visible = True Then
            UcAttachment1.SaveData("SRN_BOQ_0001")
        Else
            SavingData(False)
        End If
    End Sub
    Sub SavingData(ByVal ChekBtnPost As Boolean)
        If (SaveData()) Then
            If ChekBtnPost = False Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                btnAddNew.Focus()
            End If
        End If
    End Sub
    Private Function SaveData() As Boolean
        Try
            Dim qry As String = ""

            If (AllowToSave(Nothing)) Then
                Dim obj As New clsRMDemandApproval()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.From_Date = txtFromDate.Value
                obj.To_Date = txtToDate.Text
                obj.Comment = txtComment.Text
                obj.Remarks = txtRemarks.Text
                obj.Arr = New List(Of clsRMDemandApprovalItemLocation)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If DTLocation IsNot Nothing AndAlso DTLocation.Rows.Count > 0 Then
                        For ii As Integer = 0 To DTLocation.Rows.Count - 1
                            If (clsCommon.myCDecimal(grow.Cells(ColGQtyIndent + clsCommon.myCstr(DTLocation.Rows(ii)("Location_Code"))).Value) > 0) Then
                                Dim objTr As New clsRMDemandApprovalItemLocation()
                                objTr.Location = clsCommon.myCstr(grow.Cells(colGLCode + clsCommon.myCstr(DTLocation.Rows(ii)("Location_Code"))).Value)
                                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                                objTr.UOM = clsCommon.myCstr(grow.Cells(colUnit).Value)
                                objTr.Qty_Indent = clsCommon.myCstr(grow.Cells(ColGQtyIndent + clsCommon.myCstr(DTLocation.Rows(ii)("Location_Code"))).Value)
                                objTr.Qty_Stock = clsCommon.myCstr(grow.Cells(ColGQtyStock + clsCommon.myCstr(DTLocation.Rows(ii)("Location_Code"))).Value)
                                objTr.Qty_Approve = clsCommon.myCstr(grow.Cells(ColGQtyApprove + clsCommon.myCstr(DTLocation.Rows(ii)("Location_Code"))).Value)
                                objTr.ArrIndent = TryCast(grow.Cells(colGLCode + clsCommon.myCstr(DTLocation.Rows(ii)("Location_Code"))).Tag, ArrayList)
                                obj.Arr.Add(objTr)
                            End If
                        Next
                    End If
                Next
                obj.SaveData(obj, isNewEntry)
                LoadData(obj.Document_Code, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return False
    End Function
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            AddNew()
            Dim obj As clsRMDemandApproval = clsRMDemandApproval.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                isNewEntry = False
                btnSave.Text = "Update"

                txtDocNo.MyReadOnly = True
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtFromDate.Value = obj.From_Date
                txtToDate.Value = obj.To_Date
                SetFromToDate(False)
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                End If
                UsLock1.Status = obj.Status
                txtFromDate.Text = obj.From_Date
                txtToDate.Text = obj.To_Date

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    Dim arrUnique As New Dictionary(Of String, Integer)
                    For Each objTr As clsRMDemandApprovalItemLocation In obj.Arr
                        Dim indx As Integer = -1
                        If Not arrUnique.ContainsKey(objTr.Item_Code) Then
                            gv1.Rows.AddNew()
                            arrUnique.Add(objTr.Item_Code, arrUnique.Count)
                            indx = gv1.Rows.Count - 1
                            gv1.Rows(indx).Cells(colSNO).Value = gv1.Rows.Count
                            gv1.Rows(indx).Cells(colICode).Value = objTr.Item_Code
                            gv1.Rows(indx).Cells(colIName).Value = objTr.Item_Name
                            gv1.Rows(indx).Cells(colUnit).Value = objTr.UOM
                            gv1.Rows(indx).Cells(colTotal).Value = clsCommon.myCDecimal(gv1.Rows(indx).Cells(colTotal).Value) + objTr.Qty_Approve
                            gv1.Rows(indx).Cells(colTotalstock).Value = clsCommon.myCDecimal(gv1.Rows(indx).Cells(colTotalstock).Value) + objTr.Qty_Stock

                        Else
                            indx = arrUnique.Item(objTr.Item_Code)
                            gv1.Rows(indx).Cells(colTotal).Value = clsCommon.myCDecimal(gv1.Rows(indx).Cells(colTotal).Value) + objTr.Qty_Approve
                            gv1.Rows(indx).Cells(colTotalstock).Value = clsCommon.myCDecimal(gv1.Rows(indx).Cells(colTotalstock).Value) + objTr.Qty_Stock

                        End If
                        gv1.Rows(indx).Cells(colGLCode + objTr.Location).Value = objTr.Location
                        gv1.Rows(indx).Cells(ColGQtyIndent + objTr.Location).Value = objTr.Qty_Indent
                        gv1.Rows(indx).Cells(ColGQtyStock + objTr.Location).Value = objTr.Qty_Stock
                        gv1.Rows(indx).Cells(ColGQtyApprove + objTr.Location).Value = objTr.Qty_Approve
                        gv1.Rows(indx).Cells(colGLCode + objTr.Location).Tag = objTr.ArrIndent
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If UcAttachment1.Visible = True Then
            btnAttachments.Visible = False
            UcAttachment1.Visible = False
        Else
            CloseForm()
        End If
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If clsCommon.myLen(txtDocNo.Value) > 0 Then
                    clsRMDemandApproval.PostData(txtDocNo.Value)
                    Dim msg = "Successfully Posted"
                    common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                Else
                    Throw New Exception("No Data found to Post")
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
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
                If (clsRMDemandApproval.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qst As String = "select count(*) from TSPL_RM_DEMAND_APPROVAL where Document_Code='" + txtDocNo.Value + "'"
        Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
        If count = 0 Then
            txtDocNo.MyReadOnly = False
        Else
            txtDocNo.MyReadOnly = True
        End If
        If txtDocNo.MyReadOnly OrElse isButtonClicked Then
            txtDocNo.Value = clsRMDemandApproval.GetFinder("", txtDocNo.Value, isButtonClicked)
            LoadData(txtDocNo.Value, NavigatorType.Current)
        End If
    End Sub
    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SavingData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.Control AndAlso e.KeyCode = Keys.F7 Then
            'SelectRequistionItems()

        ElseIf e.Control AndAlso e.Alt AndAlso e.Shift And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnreverse.Visible = True
            End If
            ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction" + Environment.NewLine +
                              "TSPL_RM_DEMAND_APPROVAL " + Environment.NewLine +
                              "TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION " + Environment.NewLine +
                              "TSPL_RM_DEMAND_APPROVAL_INDENT")
        ElseIf e.Control AndAlso e.Alt AndAlso e.Shift And e.KeyCode = Keys.F11 Then
            btnAttachments.Visible = Not btnAttachments.Visible
        End If
    End Sub
    Private Sub btnreverse_Click(sender As Object, e As EventArgs) Handles btnreverse.Click
        Try
            If common.clsCommon.MyMessageBoxShow(Me, "Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                '' REASON FOR DELETE 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                If clsRMDemandApproval.ReverseAndUnpost(txtDocNo.Value) Then
                    saveCancelLog(Reason, "Reverse And Recreate", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            LoadBlankGrid()
            Dim BaseQry As String = "select Location,Requisition_Id,Item_Code,max(Item_Desc) as Item_Desc,max(Requisition_Qty) as Requisition_Qty,max(Unit_Code) as Unit_Code from (
select TSPL_REQUISITION_HEAD.Location,TSPL_REQUISITION_HEAD.Requisition_Id, TSPL_REQUISITION_DETAIL.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_REQUISITION_DETAIL.Requisition_Qty,TSPL_REQUISITION_DETAIL.Unit_Code,1 as RI,1 as Chk  from TSPL_REQUISITION_DETAIL
left outer join TSPL_REQUISITION_HEAD on TSPL_REQUISITION_HEAD.Requisition_Id=TSPL_REQUISITION_DETAIL.Requisition_Id
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_REQUISITION_DETAIL.Item_Code
where TSPL_REQUISITION_HEAD.Requisition_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_REQUISITION_HEAD.Requisition_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and TSPL_REQUISITION_HEAD.Is_Tender='Y' and isnull(TSPL_ITEM_MASTER.RAL,0)=1
union all
select TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Location,TSPL_RM_DEMAND_APPROVAL_INDENT.Against_Requisition,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Item_Code,null as Item_Desc
,0 as Requisition_Qty,null as   Unit_Code,-1 as RI,0 as Chk from TSPL_RM_DEMAND_APPROVAL_INDENT left outer join TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION on 
TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.TRNo=TSPL_RM_DEMAND_APPROVAL_INDENT.Against_TRNo
where   TSPL_RM_DEMAND_APPROVAL_INDENT.Document_Code not in ('" + txtDocNo.Value + "') 
union 
select loc.Location,'' as Against_Requisition,item.Item_Code,TSPL_ITEM_MASTER.Item_Desc as Item_Desc
,0 as Requisition_Qty,TSPL_ITEM_MASTER.Unit_Code as   Unit_Code,-1 as RI,0 as Chk 
from
(select distinct TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Location from TSPL_RM_DEMAND_APPROVAL_INDENT 
left outer join TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION on 
TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.TRNo=TSPL_RM_DEMAND_APPROVAL_INDENT.Against_TRNo 
)loc
cross join 
(select distinct TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Item_Code from TSPL_RM_DEMAND_APPROVAL_INDENT 
left outer join TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION on 
TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.TRNo=TSPL_RM_DEMAND_APPROVAL_INDENT.Against_TRNo 
)item
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=item.Item_Code
)x
group by Requisition_Id,Item_Code,Location "  'having sum(RI)>0 and sum(chk)>0


            Dim qry As String = "select Location,xx.Item_Code,max(Item_Desc) as Item_Desc,sum(Requisition_Qty) as Requisition_Qty,coalesce(max(Unit_Code),MAX(TSPL_ITEM_UOM_DETAIL.UOM_CODE)) as Unit_Code  from (
" + BaseQry + ")xx left join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.Item_Code and TSPL_ITEM_UOM_DETAIL.Default_UOM=1 group by Location,xx.Item_Code"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim dtBase As DataTable = clsDBFuncationality.GetDataTable(BaseQry)
                Dim arrUnique As New Dictionary(Of String, Integer)
                For Each dr As DataRow In dt.Rows
                    Dim indx As Integer = -1
                    If Not arrUnique.ContainsKey(clsCommon.myCstr(dr("Item_Code"))) Then
                        gv1.Rows.AddNew()
                        arrUnique.Add(clsCommon.myCstr(dr("Item_Code")), arrUnique.Count)
                        indx = gv1.Rows.Count - 1
                        gv1.Rows(indx).Cells(colSNO).Value = gv1.Rows.Count
                        gv1.Rows(indx).Cells(colICode).Value = clsCommon.myCstr(dr("Item_Code"))
                        gv1.Rows(indx).Cells(colIName).Value = clsCommon.myCstr(dr("Item_Desc"))
                        gv1.Rows(indx).Cells(colUnit).Value = clsCommon.myCstr(dr("Unit_Code"))
                    Else
                        indx = arrUnique.Item(clsCommon.myCstr(dr("Item_Code")))
                    End If
                    gv1.Rows(indx).Cells(colGLCode + clsCommon.myCstr(dr("Location"))).Value = clsCommon.myCstr(dr("Location"))
                    gv1.Rows(indx).Cells(ColGQtyIndent + clsCommon.myCstr(dr("Location"))).Value = clsCommon.myCDecimal(dr("Requisition_Qty"))
                    gv1.Rows(indx).Cells(ColGQtyStock + clsCommon.myCstr(dr("Location"))).Value = clsItemLocationDetails.getBalance(clsCommon.myCstr(dr("Item_Code")), clsCommon.myCstr(dr("Location")), "", txtToDate.Value, Nothing, clsCommon.myCstr(dr("Unit_Code")), 0)
                    gv1.Rows(indx).Cells(ColGQtyApprove + clsCommon.myCstr(dr("Location"))).Value = clsCommon.myCDecimal(dr("Requisition_Qty"))
                    gv1.Rows(indx).Cells(colTotalstock).Value = clsCommon.myCDecimal(gv1.Rows(indx).Cells(colTotalstock).Value) + clsCommon.myCDecimal(gv1.Rows(indx).Cells(ColGQtyStock + clsCommon.myCstr(dr("Location"))).Value)

                    Dim arrIndent As New ArrayList
                    For ii As Integer = 0 To dtBase.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(dr("Location")), clsCommon.myCstr(dtBase.Rows(ii)("Location"))) = CompairStringResult.Equal Then
                            If clsCommon.CompairString(clsCommon.myCstr(dr("Item_Code")), clsCommon.myCstr(dtBase.Rows(ii)("Item_Code"))) = CompairStringResult.Equal Then
                                If clsCommon.myLen(dtBase.Rows(ii)("Requisition_Id")) > 0 Then
                                    arrIndent.Add(clsCommon.myCstr(dtBase.Rows(ii)("Requisition_Id")))
                                End If
                            End If
                        End If
                    Next
                    gv1.Rows(indx).Cells(colGLCode + clsCommon.myCstr(dr("Location"))).Tag = arrIndent
                Next
                SetFromToDate(False)
            Else
                Throw New Exception("No Pending Demand Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub btnRest_Click(sender As Object, e As EventArgs) Handles btnRest.Click
        SetFromToDate(True)
    End Sub
    Sub SetFromToDate(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        btnSubmit.Enabled = val
        btnRest.Enabled = Not val
    End Sub
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        printData()
    End Sub
    Sub printData()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then

                Dim strQuery As String = "select TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1 as comp_add1 , TSPL_COMPANY_MASTER.Add2 as  comp_add2 ,TSPL_COMPANY_MASTER.Add3 as comp_add3 ,TSPL_COMPANY_MASTER.Fax as comp_Fax ,TSPL_COMPANY_MASTER.Email as comp_Email, case when ISNULL(TSPL_COMPANY_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_COMPANY_MASTER.Phone1 end +  Case When ISNULL (TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_COMPANY_MASTER.Phone2 Else'' End as CompPhone , cast(TSPL_COMPANY_MASTER.logo_img2 as image) as logo_img,tspl_company_master.Pincode,tspl_company_master.Tcan_No
, xxxx.*
from (
select max(Document_Code) as Document_Code,max(Document_Date) as Document_Date,max(From_Date) as From_Date	,max(To_Date) as To_Date,max(Remarks) as Remarks,max(Comment) as Comment,max(Status) as Status,max(Posted_By) as Posted_By,max(Posting_Date) as Posting_Date,Item_Code ,max(Item_Desc) as Item_Desc,max(UOM ) as UOM "
                For Each dr As DataRow In DTLocation.Rows
                    Dim strLoc As String = clsCommon.myCstr(dr("Location_Code"))
                    strQuery += " ,max(case when Location='" + strLoc + "' then  Location else null end ) as [" + strLoc + "_Location]
,'" + clsCommon.myCstr(dr("Loc_Short_Name")) + "' as [" + strLoc + "_Loc_Short_Name]
,max(case when Location='" + strLoc + "' then  Location_Desc else null end ) as [" + strLoc + "_Location_Desc]
,max(case when Location='" + strLoc + "' then  Qty_Indent else null end ) as [" + strLoc + "_Qty_Indent]
,max(case when Location='" + strLoc + "' then  Qty_Stock else null end ) as [" + strLoc + "_Qty_Stock]
,max(case when Location='" + strLoc + "' then  Qty_Approve else null end ) as [" + strLoc + "_Qty_Approve]"

                Next

                strQuery += " from (
select 
TSPL_RM_DEMAND_APPROVAL.Document_Code,TSPL_RM_DEMAND_APPROVAL.Document_Date,TSPL_RM_DEMAND_APPROVAL.From_Date,TSPL_RM_DEMAND_APPROVAL.To_Date,TSPL_RM_DEMAND_APPROVAL.Remarks,TSPL_RM_DEMAND_APPROVAL.Comment,TSPL_RM_DEMAND_APPROVAL.Status,TSPL_RM_DEMAND_APPROVAL.Posted_By,TSPL_RM_DEMAND_APPROVAL.Posting_Date,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Location,TSPL_LOCATION_MASTER.Loc_Short_Name,TSPL_LOCATION_MASTER.Location_Desc,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.UOM,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Qty_Indent,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Qty_Stock,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Qty_Approve
from TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Location
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Item_Code
left outer join TSPL_RM_DEMAND_APPROVAL on TSPL_RM_DEMAND_APPROVAL.Document_Code=TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Document_Code
where TSPL_RM_DEMAND_APPROVAL.Document_Code='" + txtDocNo.Value + "' 
)xxx
group by Item_Code
)xxxx left outer join TSPL_COMPANY_MASTER  on TSPL_COMPANY_MASTER.Comp_Code ='" + objCommonVar.CurrentCompanyCode + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.PurchaseOrder, dt, "rptRMDemandApproval", "RM Demand Approval for RAL", clsCommon.myCDate(dt.Rows(0)("Document_Date")))
                frmCRV = Nothing
            Else
                Throw New Exception("Please select document to print")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    '---Shaurya Kumar
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If DTLocation IsNot Nothing AndAlso DTLocation.Rows.Count > 0 Then
                        For Each dr As DataRow In DTLocation.Rows
                            If e.Column Is gv1.Columns(ColGQtyApprove + clsCommon.myCstr(dr("Location_Code"))) Then
                                gv1.Rows(gv1.CurrentRow.Index).Cells(colTotal).Value = clsCommon.myCDecimal(gv1.Rows(gv1.CurrentRow.Index).Cells(colTotal).Value) + clsCommon.myCDecimal(gv1.Rows(gv1.CurrentRow.Index).Cells(ColGQtyApprove + clsCommon.myCstr(dr("Location_Code"))).Value)
                            End If
                        Next
                    End If

                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try


        If (Not isInsideLoadData) Then
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If DTLocation IsNot Nothing AndAlso DTLocation.Rows.Count > 0 Then
                    For Each dr As DataRow In DTLocation.Rows
                        If e.Column Is gv1.Columns(ColGQtyApprove + clsCommon.myCstr(dr("Location_Code"))) Then
                            gv1.Rows(gv1.CurrentRow.Index).Cells(colTotal).Value = 0
                            For Each drTotal As DataRow In DTLocation.Rows
                                gv1.Rows(gv1.CurrentRow.Index).Cells(colTotal).Value = clsCommon.myCDecimal(gv1.Rows(gv1.CurrentRow.Index).Cells(colTotal).Value) + clsCommon.myCDecimal(gv1.Rows(gv1.CurrentRow.Index).Cells(ColGQtyApprove + clsCommon.myCstr(drTotal("Location_Code"))).Value)
                            Next
                            Exit For
                        End If
                    Next
                End If

                isCellValueChangedOpen = False
            End If
        End If

    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Please select Document No")
            End If
            Dim qry As String = "select '' as SNo, (Short_Description +' '+ case when Loc_Short_Name is null then '' else ' - '+ Loc_Short_Name end ) as [Item Description],case when Loc_Short_Name is null then  '' else 'item' end as [Item Code / Make],Qty_Approve as [Quantity (In Quintals)] ,UOM as [Quantity offered (In Quintals)]  from (
select TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Short_Description,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Loc_Short_Name,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Qty_Approve,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.UOM,TSPL_ITEM_MASTER.Sku_Seq
from TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Location 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Item_Code
where Document_Code='" + txtDocNo.Value + "' 
union all
select Item_Code,max(Item_Desc) as Item_Desc,max(Short_Description) as Short_Description,null as Location,null as Location_Desc,null as Loc_Short_Name,null as Qty_Approve,null as UOM,max(Sku_Seq) as Sku_Seq from ( select TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER.Short_Description,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Location,TSPL_LOCATION_MASTER.Location_Desc,TSPL_LOCATION_MASTER.Loc_Short_Name,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Qty_Approve,TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.UOM,TSPL_ITEM_MASTER.Sku_Seq
from TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Location 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_RM_DEMAND_APPROVAL_ITEM_LOCATION.Item_Code
where Document_Code='" + txtDocNo.Value + "')x  Group by Item_Code
)xx order by Sku_Seq,Location"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No Item Found of Document No [" + txtDocNo.Value + "]")
            End If
            Dim ItemCount As Integer = 0
            Dim SrCounter As Integer = 0
            Dim StrSrCounter As String = ""
            Dim strItem As String = clsCommon.myCstr(dt.Rows(0)("Item Description"))
            For ii As Integer = 0 To dt.Rows.Count - 1
                If clsCommon.myLen(dt.Rows(ii)("Item Code / Make")) <= 0 Then
                    SrCounter += 1
                    dt.Rows(ii)("SNo") = SrCounter
                    StrSrCounter = clsCommon.myCstr(SrCounter) + ".00"
                Else
                    StrSrCounter = clsCommon.incval(StrSrCounter)
                    dt.Rows(ii)("SNo") = StrSrCounter

                    ItemCount += 1
                    dt.Rows(ii)("Item Code / Make") = clsCommon.myCstr(dt.Rows(ii)("Item Code / Make")) + clsCommon.myCstr(ItemCount)
                End If
            Next
            dt.AcceptChanges()

            Dim FilePath As String = GetFileNameAndFile()
            ExporttoExcel(dt, Me, FilePath)
            'transportSql.ExporttoExcel(dt, Me)


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function GetFileNameAndFile()
        Dim FilePath As String = Nothing
        Try
            Dim qry = "Select top(1) FileName,FileData from TSPL_ATTACHMENTS where FormId='" + MyBase.Form_ID + "' And TransactionId='SRN_BOQ_0001'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim FileName As String = dt.Rows(0)("FileName")
            Dim FileData As Byte() = dt.Rows(0)("FileData")

            Dim sTempFileName As String = "C:\ERPTempFolder" & "\" & FileName
            FilePath = sTempFileName
            Using fs As New FileStream(sTempFileName, FileMode.Create, FileAccess.Write)
                fs.Write(FileData, 0, FileData.Length)
                fs.Flush()
                fs.Close()
            End Using
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return FilePath
    End Function


    Public Function ExporttoExcel(ByVal dt As DataTable, ByVal frm As RadForm, filePath As String) As Boolean
        Try
            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = frm.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Return False
            'End If
            If Not filePath.Equals(String.Empty) Then
                Dim gv As New RadGridView()
                Try
                    gv.Name = "gTax1"
                    frm.Controls.Add(gv)
                    FillGridViewWithDt(dt, gv)
                    gv.Visible = False
                    If gv.Rows.Count = 0 Then
                        common.clsCommon.MyMessageBoxShow("There is no data to transfer.")
                        Return False
                    End If
                    Dim i As Integer = 0
                    If gv.Rows.Count > 0 Then
                        For i = 0 To gv.ColumnCount - 1
                            Dim grow As GridViewRowInfo = TryCast(gv.Rows(0), GridViewRowInfo)
                            If TypeOf grow.Cells(i).Value Is DateTime Then
                                Dim datecol As GridViewDateTimeColumn = TryCast(gv.Columns(i), GridViewDateTimeColumn)
                                datecol.ExcelExportType = UI.Export.DisplayFormatType.ShortDate
                            End If
                        Next i
                    End If

                    Dim ext As String = Path.GetExtension(filePath)
                    If clsCommon.CompairString(ext, ".csv") = CompairStringResult.Equal Then
                        exportdataInCSV(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), False)
                    Else
                        exportdataBOQ(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), False, Nothing, False, False, False, True)
                    End If
                    If clsCommon.CompairString(ext, ".csv") = CompairStringResult.Equal Then
                        common.clsCommon.MyMessageBoxShow("Data transfer Completed!", "Export", MessageBoxButtons.OK)
                        System.Diagnostics.Process.Start(filePath)
                    End If
                Catch ex As Exception
                    frm.Controls.Remove(gv)
                    Throw New Exception(ex.Message)
                    Return False
                End Try
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow("No data transfered." + Environment.NewLine + ex.Message, "Export Error", MessageBoxButtons.OK)
        End Try
        Return True
    End Function

    Private Sub btnAttachments_Click(sender As Object, e As EventArgs) Handles btnAttachments.Click
        Dim obj As New ucAttachment
        UcAttachment1.Visible = True
        UcAttachment1.LoadData("SRN_BOQ_0001", Nothing)
    End Sub
End Class
