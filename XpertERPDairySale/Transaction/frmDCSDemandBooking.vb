Imports System.Data.SqlClient
Imports System.IO
Imports common
Public Class frmDCSDemandBooking
#Region "Variables"
    Dim AllowPlandDeptMCCLocation As Boolean = False
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colLineNo As String = "colLineNo"
    Const colDCSCode As String = "colDCSCode"
    Const colDCSName As String = "colDCSName"
    Const colCreditType As String = "colCreditType"
    Const colOutstanding As String = "colOutstanding"
    Const colLastMilkdate As String = "colLastMilkdate"
    Const colunbilledMilkAmt As String = "colunbilledMilkAmt"
    Const colbilledMilkAmt As String = "colbilledMilkAmt"
    Const colCalAmtforSale As String = "colCalAmtforSale"
    Const colItemCode As String = "colItemCode"
    Const colTotal As String = "colTotal"
    Public Const RowCash As String = "Cash"
    Public Const RowCredit As String = "Credit"
#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnreverse.Visible = False
        If MyBase.isExport = True Then
            btnExport.Enabled = True
            btnImport.Enabled = True
        Else
            btnExport.Enabled = False
            btnImport.Enabled = False
        End If
    End Sub
    Private Sub frmDCSDemandBooking_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AllowPlandDeptMCCLocation = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.Allow_Plant_Depot_MCC_typeLocation, clsFixedParameterCode.Allow_Plant_Depot_MCC_typeLocation, Nothing)) = "1", True, False))
        SetUserMgmtNew()
        'CreateTable()
        AddNew()
        'If clsCommon.myLen(PurchaseOrderNo) > 0 Then
        '    LoadData(PurchaseOrderNo, NavigatorType.Current)
        'End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub frmDCSDemandBooking_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 AndAlso gv1.CurrentCell IsNot Nothing Then
            'setGridFocus()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        ElseIf e.KeyCode = Keys.Enter Then
            setGridFocus()
        ElseIf e.KeyCode = Keys.PageDown Then
            setPagedown()
        ElseIf e.KeyCode = Keys.Home Then
            setGridFocusHome()
        ElseIf e.KeyCode = Keys.End Then
            setGridFocusEnd()
        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = "SIRC"
                frm.strCode = "SIReversAndCreate"
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnreverse.Visible = True
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
            End If
        End If
    End Sub
    Private Sub btnClose_Click_1(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Private Sub btnAddNew_Click_1(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Public Sub AddNew()
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtRouteNo.Value = ""
        lblRouteDesc.Text = ""
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        txtVehicleNo.Text = ""
        chkIndividualCustomer.Checked = False
        txtCustomerNo.Value = ""
        lblCustomerName.Text = ""
        txtCustomerNo.Visible = False
        lblCustomerName.Visible = False
        lblCustomerCode.Visible = False
        rbtnCatelFeed.IsChecked = True
        LoadBlankGrid()
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Enabled = True
        txtRouteNo.Enabled = True
        txtLocation.Enabled = True
        txtDocNo.MyReadOnly = False
        isNewEntry = True
        CategoriesFlag(True)
    End Sub
    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.AddNew()
        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.RowSpan = 50
        repoLineNo.WrapText = True
        repoLineNo.ReadOnly = True
        repoLineNo.IsPinned = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)
        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "DCS Code"
        repoCustCode.Name = colDCSCode
        repoCustCode.HeaderImage = My.Resources.search4
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCustCode.Width = 50
        repoCustCode.WrapText = True
        repoCustCode.IsVisible = True
        repoCustCode.IsPinned = True
        repoCustCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoCName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCName.FormatString = ""
        repoCName.HeaderText = "Name"
        repoCName.Name = colDCSName
        repoCName.Width = 100
        repoCName.WrapText = True
        repoCName.ReadOnly = True
        repoCName.IsVisible = True
        repoCName.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoCName)
        Dim repoCreditType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoCreditType.FormatString = ""
        repoCreditType.HeaderText = "Credit Type"
        repoCreditType.Name = colCreditType
        repoCreditType.Width = 80
        repoCreditType.WrapText = True
        repoCreditType.ReadOnly = False
        repoCreditType.DataSource = GetCreditType()
        repoCreditType.ValueMember = "Code"
        repoCreditType.DisplayMember = "Code"
        repoCreditType.IsVisible = True
        repoCreditType.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoCreditType)
        Dim repoOutstanding As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoOutstanding.FormatString = ""
        repoOutstanding.HeaderText = "OutStanding Amount"
        repoOutstanding.Name = colOutstanding
        repoOutstanding.Width = 50
        repoOutstanding.WrapText = True
        repoOutstanding.ReadOnly = True
        repoOutstanding.IsVisible = True
        repoOutstanding.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoOutstanding)
        Dim repoLastMilkdate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLastMilkdate.FormatString = ""
        repoLastMilkdate.HeaderText = "Last Milk Date"
        repoLastMilkdate.Name = colLastMilkdate
        repoLastMilkdate.Width = 80
        repoLastMilkdate.WrapText = True
        repoLastMilkdate.ReadOnly = True
        repoLastMilkdate.IsVisible = True
        repoLastMilkdate.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoLastMilkdate)
        Dim repoLastUnBilledMilkAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLastUnBilledMilkAmt.FormatString = ""
        repoLastUnBilledMilkAmt.HeaderText = "Unbilled Milk Amount"
        repoLastUnBilledMilkAmt.Name = colunbilledMilkAmt
        repoLastUnBilledMilkAmt.Width = 80
        repoLastUnBilledMilkAmt.WrapText = True
        repoLastUnBilledMilkAmt.ReadOnly = True
        repoLastUnBilledMilkAmt.IsVisible = True
        repoLastUnBilledMilkAmt.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoLastUnBilledMilkAmt)
        Dim repoLastBilledMilkAmt As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLastBilledMilkAmt.FormatString = ""
        repoLastBilledMilkAmt.HeaderText = "Last Billed Milk Amount"
        repoLastBilledMilkAmt.Name = colbilledMilkAmt
        repoLastBilledMilkAmt.Width = 80
        repoLastBilledMilkAmt.WrapText = True
        repoLastBilledMilkAmt.ReadOnly = True
        repoLastBilledMilkAmt.IsVisible = True
        repoLastBilledMilkAmt.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoLastBilledMilkAmt)
        Dim repoCalAmtforSale As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCalAmtforSale.FormatString = ""
        repoCalAmtforSale.HeaderText = "Calculated Amount For Sale"
        repoCalAmtforSale.Name = colCalAmtforSale
        repoCalAmtforSale.Width = 80
        repoCalAmtforSale.WrapText = True
        repoCalAmtforSale.ReadOnly = True
        repoCalAmtforSale.IsVisible = True
        repoCalAmtforSale.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoCalAmtforSale)
        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        qry = "select * from (
    select 'Ambient' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,tspl_item_master.TypeOfItm,tspl_item_master.DcsSeqNo   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_Ambient=1   and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and tspl_item_master.Active=1"
        If rbtnCatelFeed.IsChecked Then
            qry += " And tspl_item_master.TypeOfItm='CF' "
        ElseIf rbtnGhee.IsChecked Then
            qry += " And tspl_item_master.TypeOfItm='G' "
        ElseIf rbtnOther.IsChecked Then
            qry += " And tspl_item_master.TypeOfItm='O' "
        End If
        qry += " and tspl_item_master.DcsSeqNo is not null and tspl_item_master.DcsSeqNo>0
    and TSPL_ITEM_UOM_DETAIL.Default_UOM=1
    )z order by DcsSeqNo,Item_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim i As Integer = 1
            Dim obj As ItemValueClass = New ItemValueClass()
            For Each dr As DataRow In dt.Rows
                repoIName = New GridViewTextBoxColumn()
                repoIName.FormatString = ""
                repoIName.HeaderText = clsCommon.myCstr(dr("UOM_Code"))
                obj = New ItemValueClass()
                obj.itemCode = clsCommon.myCstr(dr("Item_Code"))
                obj.itemDesc = clsCommon.myCstr(dr("Item_Desc"))
                obj.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                obj.IsFreshAmbient = clsCommon.myCstr(dr("FreshAmbient"))
                obj.ShortDesc = clsCommon.myCstr(dr("Short_Description"))
                repoIName.Tag = obj
                repoIName.Name = colItemCode + clsCommon.myCstr(i)
                repoIName.Width = 150
                repoIName.IsVisible = True
                i = i + 1
                gv1.MasterTemplate.Columns.Add(repoIName)
            Next
        End If
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        View()
    End Sub
    Public Shared Function GetCreditType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = RowCash
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = RowCredit
        dt.Rows.Add(dr)
        Return dt
    End Function
    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup("DCS"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colLineNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colDCSCode).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colDCSName).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCreditType).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colOutstanding).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colLastMilkdate).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colunbilledMilkAmt).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colbilledMilkAmt).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCalAmtforSale).Name)
                view.ColumnGroups(0).IsPinned = True
                Dim TempColGroupCount As Integer = 1
                Dim obj As ItemValueClass = New ItemValueClass()
                Dim i As Integer = 1
                For dblcolumns As Integer = 6 To gv1.Columns.Count - 1
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                    If obj1 IsNot Nothing Then
                        view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(obj1.ShortDesc)))
                        view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                        view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                        TempColGroupCount += 1
                    End If
                Next
                gv1.ViewDefinition = view
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Function AllowToSave(ByVal trans As SqlTransaction) As Boolean
        Try
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                Throw New Exception("Please select Route")
            End If
            If clsCommon.myLen(txtLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If chkIndividualCustomer.Checked = True Then
                If clsCommon.myLen(txtCustomerNo) <= 0 Then
                    Throw New Exception("Please select Customer")

                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub SaveData()
        Try
            Dim qry As String = ""
            Dim strPriceCode As String = String.Empty
            Dim LineNo As Integer = 1
            Dim TotQty As Double = 0
            clsApply_Approval.CheckUpdate_Doc_Valid(MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value))
            If (AllowToSave(Nothing)) Then
                Dim obj As New clsDCSDemand()
                If Not isNewEntry Then
                    obj.Document_No = txtDocNo.Value
                End If
                obj.Document_Date = txtDate.Value
                obj.Route_No = txtRouteNo.Value
                obj.Location = txtLocation.Value
                If rbtnCatelFeed.IsChecked Then
                    obj.Categories = "CF"
                ElseIf rbtnGhee.IsChecked Then
                    obj.Categories = "G"
                ElseIf rbtnOther.IsChecked Then
                    obj.Categories = "O"
                End If
                obj.VehicleNo = txtVehicleNo.Text
                If chkIndividualCustomer.Checked Then
                    obj.IsIndividualCustomer = True
                Else
                    obj.IsIndividualCustomer = False
                End If
                obj.Cust_Code = txtCustomerNo.Value
                obj.Arr = New List(Of clsDCSDemandDetail)
                Dim intLine As Integer = 0
                For dblrows As Integer = 0 To gv1.Rows.Count - 1
                    Dim k As Integer = 1
                    For dblcolumns As Integer = 9 To gv1.Columns.Count - 1
                        Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                        k = k + 1
                        If obj1 IsNot Nothing Then
                            If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                                Dim objTr As New clsDCSDemandDetail()
                                objTr.Line_No = LineNo
                                objTr.VLC_Uploader = clsCommon.myCstr(gv1.Rows(dblrows).Cells(colDCSCode).Value)
                                objTr.CreditType = clsCommon.myCstr(gv1.Rows(dblrows).Cells(colCreditType).Value)
                                objTr.OutStandingAmt = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colOutstanding).Value)
                                objTr.LastMilkDate = clsCommon.GetPrintDate(gv1.Rows(dblrows).Cells(colLastMilkdate).Value, "dd/MMM/yyyy hh:mm tt")
                                objTr.UnbilledMilkAmt = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colunbilledMilkAmt).Value)
                                objTr.CalAmtforSale = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(colCalAmtforSale).Value)
                                If clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                                    objTr.Item_Code = clsCommon.myCstr(obj1.itemCode)
                                    objTr.Unit_code = clsCommon.myCstr(obj1.Unit_code)
                                    objTr.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                    TotQty += objTr.Qty
                                    If (clsCommon.myLen(objTr.VLC_Uploader) > 0) AndAlso (clsCommon.myLen(objTr.Item_Code) > 0) Then
                                        obj.Arr.Add(objTr)
                                    End If
                                    LineNo = LineNo + 1
                                End If
                            End If
                        End If
                    Next
                Next
                If (obj.SaveData(obj, isNewEntry)) = True Then
                    'clsApprovalScreen.CheckApprovalLevel(.FormId, "Tspl_Lost_defect_sealNo_Head", "doc_no", obj.Document_No, trans)
                    clsApply_Approval.CheckApprovalRequired(txtLocation.Value, "", MyBase.Form_ID, clsCommon.myCstr(txtDocNo.Value), txtDate.Text, "", "", 0, clsCommon.myCdbl(TotQty), "", Nothing, 0, False)
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            Dim obj As New clsDCSDemand
            obj = clsDCSDemand.GetData(strCode, NavTyep)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                AddNew()
                isNewEntry = False
                'Catel Feed=CF,Ghee=G,Other=O
                If clsCommon.CompairString(obj.Categories, "CF") = CompairStringResult.Equal Then
                    rbtnCatelFeed.IsChecked = True
                ElseIf clsCommon.CompairString(obj.Categories, "G") = CompairStringResult.Equal Then
                    rbtnGhee.IsChecked = True
                ElseIf clsCommon.CompairString(obj.Categories, "O") = CompairStringResult.Equal Then
                    rbtnOther.IsChecked = True
                End If
                CategoriesFlag(False)
                txtDate.Enabled = False
                txtRouteNo.Enabled = False
                txtLocation.Enabled = False
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtRouteNo.Value = obj.Route_No
                txtLocation.Value = obj.Location
                txtVehicleNo.Text = obj.VehicleNo
                chkIndividualCustomer.Checked = obj.IsIndividualCustomer
                txtCustomerNo.Value = obj.Cust_Code
                lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name  from TSPL_VLC_master_Head  where VLC_Code_VLC_Uploader='" + txtCustomerNo.Value + "'"))

                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                End If
                LoadDCS(obj.Route_No)
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsDCSDemandDetail In obj.Arr
                        For dblrows As Integer = 0 To gv1.Rows.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(dblrows).Cells(colDCSCode).Value), objTr.VLC_Uploader) = CompairStringResult.Equal Then
                                Dim k As Integer = 1
                                gv1.Rows(dblrows).Cells(colCreditType).Value = objTr.CreditType
                                gv1.Rows(dblrows).Cells(colOutstanding).Value = objTr.OutStandingAmt
                                gv1.Rows(dblrows).Cells(colLastMilkdate).Value = objTr.LastMilkDate
                                gv1.Rows(dblrows).Cells(colunbilledMilkAmt).Value = objTr.UnbilledMilkAmt
                                gv1.Rows(dblrows).Cells(colbilledMilkAmt).Value = objTr.LastBilledMilkAmt
                                gv1.Rows(dblrows).Cells(colCalAmtforSale).Value = objTr.CalAmtforSale
                                For columns = 9 To gv1.Columns.Count - 1
                                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                    k = k + 1
                                    If clsCommon.CompairString(objTr.Item_Code, clsCommon.myCstr(obj1.itemCode)) = CompairStringResult.Equal AndAlso clsCommon.CompairString(objTr.Unit_code, clsCommon.myCstr(obj1.Unit_code)) = CompairStringResult.Equal Then
                                        gv1.Rows(dblrows).Cells(columns).Value = objTr.Qty
                                    End If
                                Next
                            End If
                        Next
                    Next
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub CategoriesFlag(ByVal flag As Boolean)
        rbtnCatelFeed.Enabled = flag
        rbtnGhee.Enabled = flag
        rbtnOther.Enabled = flag
    End Sub
    Sub PostData()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If (myMessages.postConfirm()) Then
                    If (clsDCSDemand.PostData(MyBase.Form_ID, txtDocNo.Value)) Then
                        clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            Else
                Throw New Exception("Document not Found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub DeleteData()
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
                If (clsDCSDemand.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
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
    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        Dim intCurrCol As Integer = gv1.CurrentColumn.Index
        If intCurrRow = gv1.Rows.Count - 1 Then
            intCurrRow = 0
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        Else
            If gv1.CurrentColumn IsNot gv1.Columns(gv1.Columns.Count - 1) Then
                gv1.CurrentColumn = gv1.Columns(intCurrCol + 1)
            Else
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(6)
            End If
        End If
    End Sub
    Private Sub setPagedown()
        Dim scrollDelta As Integer = gv1.TableElement.ViewElement.ScrollableRows.Size.Height + CInt(gv1.TableElement.ViewElement.ScrollableRows.ScrollOffset.Height)
        Dim newVScrollValue As Integer = gv1.TableElement.VScrollBar.Value + scrollDelta
        If newVScrollValue < gv1.TableElement.VScrollBar.Maximum - gv1.TableElement.VScrollBar.LargeChange Then
            gv1.TableElement.VScrollBar.Value = newVScrollValue
        Else
            gv1.TableElement.VScrollBar.Value = gv1.TableElement.VScrollBar.Maximum - gv1.TableElement.VScrollBar.LargeChange
        End If
        Dim navigator As IGridNavigator = gv1.GridViewElement.Navigator
        navigator.BeginSelection(New GridNavigationContext(GridNavigationInputType.Keyboard, MouseButtons.None, Keys.None))
        navigator.SelectRow(Me.GetLastScrollableRow(gv1.TableElement))
        navigator.EndSelection()
    End Sub
    Private Function GetLastScrollableRow(ByVal tableElement As GridTableElement) As GridViewRowInfo
        Dim rows As ScrollableRowsContainerElement = tableElement.ViewElement.ScrollableRows
        Dim traverser As GridTraverser = CType((CType(tableElement.RowScroller, IEnumerable)).GetEnumerator(), GridTraverser)
        For i As Integer = 0 To rows.Children.Count - 1
            If rows.Children(i).BoundingRectangle.Bottom > rows.Size.Height Then
                Exit For
            End If
            If Not traverser.MoveNext() Then
                Exit For
            End If
        Next
        Return traverser.Current
    End Function
    Private Sub setGridFocusHome()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If gv1.Rows.Count > 0 Then
            gv1.Rows(intCurrRow).Cells(6).IsSelected = True
            gv1.Rows(intCurrRow).IsCurrent = True
            gv1.Columns(6).IsCurrent = True
        End If
    End Sub
    Private Sub setGridFocusEnd()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        If gv1.Rows.Count > 0 Then
            gv1.Rows(intCurrRow).Cells(gv1.Columns.Count - 1).IsSelected = True
            gv1.Rows(intCurrRow).IsCurrent = True
            gv1.Columns(gv1.Columns.Count - 1).IsCurrent = True
        End If
    End Sub
    Private Sub rbtnCatelFeed_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCatelFeed.ToggleStateChanged
        If rbtnCatelFeed.IsChecked Then
            rbtnGhee.IsChecked = False
            rbtnOther.IsChecked = False
            LoadBlankGrid()
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                LoadDCS(txtRouteNo.Value)
            End If
        End If
    End Sub
    Private Sub rbtnGhee_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnGhee.ToggleStateChanged
        If rbtnGhee.IsChecked Then
            rbtnCatelFeed.IsChecked = False
            rbtnOther.IsChecked = False
            LoadBlankGrid()
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                LoadDCS(txtRouteNo.Value)
            End If
        End If
    End Sub
    Private Sub rbtnOther_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnOther.ToggleStateChanged
        If rbtnOther.IsChecked Then
            rbtnGhee.IsChecked = False
            rbtnCatelFeed.IsChecked = False
            LoadBlankGrid()
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                LoadDCS(txtRouteNo.Value)
            End If
        End If
    End Sub
    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            Dim qry As String = "Select TSPL_BULK_ROUTE_MASTER.Route_No as Code from TSPL_BULK_ROUTE_MASTER"
            txtRouteNo.Value = clsCommon.ShowSelectForm("DCSDemandRouteFinder", qry, "Code", "", txtRouteNo.Value, "", isButtonClicked)
            lblRouteDesc.Text = clsCommon.myCstr(clsRouteMaster.GetName(txtRouteNo.Value, Nothing))
            LoadBlankGrid()
            LoadDCS(txtRouteNo.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function MCCLOCATIONFINDER()
        Dim arrloc As String = ""
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData(True)
            If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Default_LocCode) > 0 Then
                arrloc = obj.arrLocCodes
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return arrloc
    End Function
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Dim qry As String = Nothing
        Dim WhrCls As String = Nothing
        If AllowPlandDeptMCCLocation Then
            qry = "select Location_Code AS Code,Location_Desc as Name  from TSPL_LOCATION_MASTER"
            WhrCls = " Is_Sub_Location = 'N' AND Location_Category <> 'MCC' and GIT_Type  <> 'Y' "
        Else
            'qry = "select Location_Code as Code,Location_Desc as Name , tspl_mcc_master.Mcc_Code_VLC_Uploader as [MCC Code For VLC Uploder] from TSPL_LOCATION_MASTER left outer join tspl_mcc_master on tspl_mcc_master.MCC_Code = TSPL_LOCATION_MASTER.Location_Code  "
            qry = "select Location_Desc as Code , tspl_mcc_master.Mcc_Code_VLC_Uploader as [MCC Code For VLC Uploder] from TSPL_LOCATION_MASTER left outer join tspl_mcc_master on tspl_mcc_master.MCC_Code = TSPL_LOCATION_MASTER.Location_Code  "
            WhrCls = " Location_Type='Physical' and CSA_Type='N' and Is_Section='N' and Is_Sub_Location='N' "
            WhrCls += "  and location_category='MCC' and  Location_Code in (" + MCCLOCATIONFINDER() + ")"
        End If
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ") "
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("DCSDemandLocFnd", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
        lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
    End Sub
    Public Sub LoadDCS(ByVal RouteNo As String)
        Try
            If clsCommon.myLen(RouteNo) > 0 Then
                Dim dbrow As Double = 0
                Dim StrQry As String = "select VLC_Code_VLC_Uploader,VLC_Name,VSP_Code from TSPL_VLC_master_Head  where Route_Code='" + RouteNo + "'"
                If chkIndividualCustomer.Checked Then
                    StrQry += " and VLC_Code_VLC_Uploader='" + txtCustomerNo.Value + "'"
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    LoadBlankGrid()
                    For Each dr As DataRow In dt.Rows
                        gv1.Rows(dbrow).Cells(colLineNo).Value = dbrow + 1
                        gv1.Rows(dbrow).Cells(colDCSCode).Value = clsCommon.myCstr(dr("VLC_Code_VLC_Uploader"))
                        gv1.Rows(dbrow).Cells(colDCSName).Value = clsCommon.myCstr(dr("VLC_Name"))
                        gv1.Rows(dbrow).Cells(colCreditType).Value = RowCredit

                        gv1.Rows(dbrow).Cells(colOutstanding).Value = GetOutStandingBal(clsCommon.myCstr(dr("VSP_Code")), clsCommon.GetPrintDate(txtDate.Value))
                        gv1.Rows(dbrow).Cells(colLastMilkdate).Value = clsCommon.GetPrintDate(clsDBFuncationality.getSingleValue("select top 1 DOC_DATE from TSPL_MILK_SRN_HEAD Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VLC_Code = TSPL_MILK_SRN_HEAD.VLC_CODE where TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader='" + clsCommon.myCstr(dr("VLC_Code_VLC_Uploader")) + "' order by TSPL_MILK_SRN_HEAD.DOC_DATE desc"), "dd-MMM-yyyy")
                        gv1.Rows(dbrow).Cells(colunbilledMilkAmt).Value = GetUnbilledAmt(clsCommon.myCstr(dr("VSP_Code")))
                            gv1.Rows(dbrow).Cells(colbilledMilkAmt).Value = GetLastbilledAmt(clsCommon.myCstr(dr("VSP_Code")))


                        Dim chkCreditlimt As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Credit_Limit_On_Milk_Receipt_Per from TSPL_VENDOR_MASTER where vendor_code='" + clsCommon.myCstr(dr("VSP_Code")) + "' "))
                        If chkCreditlimt > 0 Then
                            gv1.Rows(dbrow).Cells(colCalAmtforSale).Value = (clsCommon.myCdbl(gv1.Rows(dbrow).Cells(colunbilledMilkAmt).Value) * chkCreditlimt) / 100
                        Else
                            gv1.Rows(dbrow).Cells(colCalAmtforSale).Value = 0
                        End If
                        dbrow += 1
                        gv1.Rows.AddNew()
                    Next
                End If
            Else
                Throw New Exception("Please Select Route No")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_No", "varchar(30) NOT NULL Primary key")
        coll.Add("Document_Date", "datetime not NULL")
        coll.Add("Route_No", "varchar(30) NULL REFERENCES TSPL_BULK_ROUTE_MASTER (ROUTE_NO)")
        coll.Add("Location", "Varchar(12) NULL  References TSPL_LOCATION_MASTER(Location_Code)")
        coll.Add("Categories", "Varchar(2) Not NULL")
        coll.Add("Posted", "int not NULL default 0")
        coll.Add("Posting_Date", "Datetime NULL")
        coll.Add("Created_By", "varchar(12) NOT NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DCS_DEMAND_BOOKING_MASTER", coll, Nothing, False, False, "", "Document_No", "Document_Date")
        coll = New Dictionary(Of String, String)()
        coll.Add("TR_Code", "varchar(30) NOT NULL primary Key")
        coll.Add("Document_No", "varchar(30) NOT NULL REFERENCES TSPL_DCS_DEMAND_BOOKING_MASTER(Document_No)")
        coll.Add("Line_No", "integer not null")
        coll.Add("VLC_Uploader", "Varchar(30) Not null")
        coll.Add("CreditType", "Varchar(30) Not null")
        coll.Add("OutStandingAmt", "decimal(18,2) null")
        coll.Add("UnbilledMilkAmt", "decimal(18,2) null")
        coll.Add("LastBilledMilkAmt", "decimal(18,2) null")
        coll.Add("CalAmtforSale", "decimal(18,2) null")
        coll.Add("LastMilkDate", "Datetime NULL")
        coll.Add("Item_Code", "Varchar(50) null references TSPL_Item_MASTER(Item_Code)")
        coll.Add("Qty", "decimal(18,2) null")
        coll.Add("Unit_code", "Varchar(12) null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_DCS_DEMAND_BOOKING_DETAIL", coll, "", False, False, "TSPL_DCS_DEMAND_BOOKING_MASTER", "Document_No", "")
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qry As String = "select TSPL_DCS_DEMAND_BOOKING_MASTER.Document_No as DocumentNo,convert(varchar(12),TSPL_DCS_DEMAND_BOOKING_MASTER.Document_date,103) as DocumentDate,TSPL_DCS_DEMAND_BOOKING_MASTER.Route_No as [Route No],TSPL_DCS_DEMAND_BOOKING_MASTER.Location as [Location Code],case when Posted=1 then 'posted' else 'Unposted' end as Posted from TSPL_DCS_DEMAND_BOOKING_MASTER "
            Reset()
            LoadData(clsCommon.ShowSelectForm("FSBook1DocNo", qry, "DocumentNo", "", txtDocNo.Value, "Document_date DESC", isButtonClicked, " TSPL_DCS_DEMAND_BOOKING_MASTER.Document_date "), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_DCS_DEMAND_BOOKING_MASTER where Document_No='" + txtDocNo.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtDocNo.MyReadOnly = True
            ElseIf check <= 0 Then
                txtDocNo.MyReadOnly = False
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function GetOutStandingBal(ByVal VendorNo As String, ByVal dtDoc As DateTime) As Decimal
        Dim OSBal As Decimal = 0
        Try
            Dim strcustomerfilter As String = String.Empty
            Dim fromDate As DateTime = Nothing
            Dim toDate As DateTime = Nothing
            If clsCommon.myCdbl(dtDoc.Day) < 16 Then
                fromDate = New DateTime(dtDoc.Year, dtDoc.Month, 1)
                toDate = New Date(dtDoc.Year, dtDoc.Month, 15)
            Else
                fromDate = New DateTime(dtDoc.Year, dtDoc.Month, 16)
                toDate = New DateTime(dtDoc.Year, dtDoc.Month, DateTime.DaysInMonth(dtDoc.Year, dtDoc.Month))
            End If
            strcustomerfilter = "'" + VendorNo + "'"
            Dim qry As String = "Select (OP + Sale) as [Balance Amount] from ( 
                select max( TSPL_DEDUCTION_MASTER.Description ) as DeductionName, TSPL_VENDOR_MASTER.Vendor_Code, max(VLC_Code_VLC_Uploader) as DCSCode, max(TSPL_VENDOR_MASTER.Vendor_Name) as [DCS Name],
            sum( (Amount - Reduce_Deduc_Amt) * ( case when convert(date,Document_Date,103) < '" + clsCommon.GetPrintDate(fromDate) + "' then 1 else 0 end ) * (case when RI = 1 then 1 else -1 end) ) as OP,
            sum( Amount * ( case when convert(date,Document_Date,103) >= '" + clsCommon.GetPrintDate(fromDate) + "' and convert(date,Document_Date,103) <= '" + clsCommon.GetPrintDate(toDate) + "' then 1 else 0 end ) * (case when RI = 1 then 1 else 0 end) ) as Sale,
            sum( (Amount - Reduce_Deduc_Amt) * ( case when convert(date,Document_Date,103) <= '" + clsCommon.GetPrintDate(fromDate) + "' and convert(date,Document_Date,103) <= '" + clsCommon.GetPrintDate(toDate) + "' then 1 else 0 end ) * (case when RI = 1 then 0 else 1 end) ) as AMTDeducted 
            from ( select TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No, TSPL_MULTIPLE_DEDUCTION_HEAD.Document_Date, TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo as AP_Invoice_No, TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode, TSPL_MULTIPLE_DEDUCTION_detail.Vendor_Code, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_MULTIPLE_DEDUCTION_DETAIL.Amount, 0 as Reduce_Deduc_Amt, 1 as RI from TSPL_MULTIPLE_DEDUCTION_DETAIL left join TSPL_MULTIPLE_DEDUCTION_HEAD on TSPL_MULTIPLE_DEDUCTION_HEAD.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Document_No left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code where 2 = 2 and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = '" + VendorNo + "' 
            Union all select TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date, TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No, TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode, TSPL_PAYMENT_PROCESS_DEDUCTION.Vendor_CODE, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_PAYMENT_PROCESS_DEDUCTION.Amount, TSPL_PAYMENT_PROCESS_DEDUCTION.Reduce_Deduc_Amt, 2 as RI from TSPL_PAYMENT_PROCESS_DEDUCTION left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_DEDUCTION.Doc_No left join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_DEDUCTION.AP_Invoice_No left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code where 2 = 2 and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = '" + VendorNo + "' 
            Union all select TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No as Document_No, TSPL_PAYMENT_PROCESS_HEAD.To_Date as Doc_Date, TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No, TSPL_VENDOR_INVOICE_HEAD.Posting_Date as AP_Invoice_Date, TSPL_VENDOR_INVOICE_HEAD.Document_Type, TSPL_MULTIPLE_DEDUCTION_detail.DeductionCode, TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Vendor_CODE, TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader, TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Amount, 0 as Reduce_Deduc_Amt, 3 as RI from TSPL_PAYMENT_PROCESS_CREDIT_NOTE left join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No left join TSPL_PAYMENT_PROCESS_HEAD on TSPL_PAYMENT_PROCESS_HEAD.Doc_No = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.Doc_No left join TSPL_MULTIPLE_DEDUCTION_DETAIL on TSPL_MULTIPLE_DEDUCTION_DETAIL.Against_Deduction_DocNo = TSPL_PAYMENT_PROCESS_CREDIT_NOTE.AP_Invoice_No left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_VENDOR_INVOICE_HEAD.Vendor_Code where 2 = 2 and TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader = '" + VendorNo + "' ) xx 
            left join TSPL_DEDUCTION_MASTER on TSPL_DEDUCTION_MASTER.Code = xx.DeductionCode left join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code = xx.Vendor_Code group by TSPL_VENDOR_MASTER.Vendor_Code ) xxx"
            Dim dblBal As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Nothing))
            OSBal = dblBal
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return OSBal
    End Function
    Public Function GetUnbilledAmt(ByVal VSP_Code As String) As Decimal
        Dim dblBal As Decimal
        Dim qry As String = "select sum(AMOUNT) as unbilledAmt
from TSPL_MILK_SRN_HEAD
left join TSPL_MILK_SRN_DETAIL on TSPL_MILK_SRN_HEAD.DOC_CODE=TSPL_MILK_SRN_DETAIL.DOC_CODE
where TSPL_MILK_SRN_HEAD.DOC_CODE not in( select SRN_CODE from TSPL_MILK_PURCHASE_INVOICE_DETAIL) and TSPL_MILK_SRN_HEAD.VSP_CODE='" + VSP_Code + "'"
        dblBal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Nothing))
        Return dblBal
    End Function
    Public Function GetLastbilledAmt(ByVal VSP_Code As String) As Decimal
        Dim dblBal As Decimal
        Dim qry As String = "select sum(TOTAL_AMOUNT) as Total_Amt from TSPL_MILK_PURCHASE_INVOICE_DETAIL where DOC_CODE in(select top 1 DOC_CODE from TSPL_MILK_PURCHASE_INVOICE_HEAD where VSP_CODE='" + VSP_Code + "' and  convert(date,FROM_DATE,103)<='" + clsCommon.GetPrintDate(txtDate.Value) + "' and convert(date,TO_DATE,103)<='" + clsCommon.GetPrintDate(txtDate.Value) + "'
order by FROM_DATE desc)"
        dblBal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry, Nothing))
        Return dblBal
    End Function

    Private Sub txtCustomerNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCustomerNo._MYValidating
        Try
            Dim WhrCls As String = Nothing
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then

                Dim strQry As String = "select VLC_Code_VLC_Uploader as Code,VLC_Name as [VLC Name],VSP_Code as [VSP Code] from TSPL_VLC_master_Head "
                WhrCls = " Route_Code='" + txtRouteNo.Value + "'"
                txtCustomerNo.Value = clsCommon.ShowSelectForm("DCSDemandCustomerFinder", strQry, "Code", WhrCls, txtCustomerNo.Value, "", isButtonClicked)
                lblCustomerName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select VLC_Name  from TSPL_VLC_master_Head  where VLC_Code_VLC_Uploader='" + txtCustomerNo.Value + "'"))
                LoadBlankGrid()
                LoadDCS(txtRouteNo.Value)
            Else
                Throw New Exception("Please Select Route No.")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkIndividualCustomer_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkIndividualCustomer.ToggleStateChanged
        If chkIndividualCustomer.Checked = True Then
            txtCustomerNo.Enabled = True
            txtCustomerNo.Visible = True
            lblCustomerName.Visible = True
            lblCustomerCode.Visible = True

        Else
            txtCustomerNo.Enabled = False
            txtCustomerNo.Visible = False
            lblCustomerName.Visible = False
            lblCustomerCode.Visible = False

        End If
    End Sub
End Class