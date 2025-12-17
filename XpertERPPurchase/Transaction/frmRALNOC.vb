Imports System.Data.SqlClient
Imports common

Public Class frmRALNOC
    Inherits FrmMainTranScreen

#Region "Variables"
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Const colScheduleSNo As String = "colScheduleSNo"
    Const colScheduleParentSNo As String = "colScheduleParentSNo"
    Const colScheduleNo As String = "colScheduleNo"
    Const colScheduleFromDate As String = "colScheduleFromDate"
    Const colScheduleToDate As String = "colScheduleToDate"
    Const colScheduleQtyPer As String = "colScheduleQtyPer"
    Const colScheduleQty As String = "colScheduleQty"
    Const colScheduleShortPer As String = "colScheduleShortPer"
    Const colScheduleShort As String = "colScheduleShort"
    Const colScheduleLateDays As String = "colScheduleLateDays"
#End Region
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag

        btnReverse.Visible = False

    End Sub
    Private Sub FrmAPInvoiceEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim coll As New Dictionary(Of String, String)

        coll = New Dictionary(Of String, String)
        coll.Add("Document_No", "varchar(30) NOT NULL Primary Key")
        coll.Add("Document_Date", "DateTime not NULL")
        coll.Add("Location_Code", "varchar(12) not NULL References TSPL_LOCATION_MASTER(Location_Code)")
        coll.Add("Tender_No", "varchar(50) not NULL References TSPL_TENDER_HEADER(DocumentCode)")
        coll.Add("Vendor_Code", "varchar(12) not NULL References TSPL_VENDOR_MASTER(Vendor_Code)")
        coll.Add("Item_Code", "varchar(50) not NULL References TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Remarks", "varchar(200) NULL")
        coll.Add("Status", "integer not null default 0")
        coll.Add("Created_By", "VARCHAR(12) not NULL REFERENCES TSPL_USER_MASTER(User_Code) ")
        coll.Add("Created_Date", "DateTime not NULL")
        coll.Add("Modify_By", "VARCHAR(12) not NULL REFERENCES TSPL_USER_MASTER(User_Code) ")
        coll.Add("Modify_Date", "DateTime not NULL")
        coll.Add("Post_By", "VARCHAR(12) NULL REFERENCES TSPL_USER_MASTER(User_Code) ")
        coll.Add("Post_Date", "DateTime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_RAL_NOC", coll, "", True, True, "", "Document_No", "Document_Date", True)


        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_No", "varchar(30) not null References TSPL_RAL_NOC(Document_No)")
        coll.Add("PSNo", "integer null")
        coll.Add("Schedule_No", "integer null ")
        coll.Add("From_Date", "date NULL")
        coll.Add("To_Date", "date NULL")
        coll.Add("Schedule_Qty_Per", "decimal(18, 2) NULL")
        coll.Add("Schedule_Qty", "decimal(18, 2) NULL")
        coll.Add("Schedule_Short_Per", "decimal(18, 2) NULL")
        coll.Add("Schedule_Short", "decimal(18, 2) NULL")
        coll.Add("Late_Days", "integer NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_RAL_NOC_SCHEDULE", coll, Nothing, True, False, "TSPL_TENDER_HEADER", "DocumentCode", "", True)


        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_No", "varchar(30) not null References TSPL_RAL_NOC(Document_No)")
        coll.Add("Against_RAL_NOC_Schedule_PK_Id", "integer NOT NULL References TSPL_RAL_NOC_SCHEDULE(PK_Id)")
        coll.Add("Penalty_Date", "date NULL")
        coll.Add("Penalty", "Decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_RAL_NOC_SCHEDULE_PENALTY", coll, Nothing, True, False, "TSPL_TENDER_HEADER", "DocumentCode", "", True)


        coll = New Dictionary(Of String, String)()
        coll.Add("Document_No", "varchar(30) not null References TSPL_RAL_NOC(Document_No)")
        coll.Add("PK_Id", "integer NOT NULL primary key")
        coll.Add("PSNo", "integer null")
        coll.Add("Schedule_No", "integer null ")
        coll.Add("From_Date", "date NULL")
        coll.Add("To_Date", "date NULL")
        coll.Add("Schedule_Qty_Per", "decimal(18, 2) NULL")
        coll.Add("Schedule_Qty", "decimal(18, 2) NULL")
        coll.Add("Schedule_Short_Per", "decimal(18, 2) NULL")
        coll.Add("Schedule_Short", "decimal(18, 2) NULL")
        coll.Add("Late_Days", "integer NULL")
        coll.Add("Extension_Days", "integer NULL")
        coll.Add("Item_Type", "varchar(5) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_RAL_NOC_ORG_SCHEDULE", coll, Nothing, True, False, "TSPL_TENDER_HEADER", "DocumentCode", "", True)


        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL primary key")
        coll.Add("Against_Tender_Schedule_PK_Id", "integer NOT NULL References TSPL_RAL_NOC_ORG_SCHEDULE(PK_Id)")
        coll.Add("Penalty_Date", "date NULL")
        coll.Add("Penalty", "Decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_RAL_NOC_ORG_SCHEDULE_PENALTY", coll, Nothing, True, False, "TSPL_TENDER_HEADER", "DocumentCode", "", True)


        SetUserMgmtNew()
        txtVendorNo.MendatroryField = True
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        UcAttachment1.Form_ID = Me.Form_ID

        AddNew()
        SetLength()
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtRemarks.MaxLength = 200
    End Sub
    Sub BlankAllControls()
        'BtnCancel.Enabled = False
        txtDocNo.Value = ""
        txtVendorNo.Value = ""
        lblVendorName.Text = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtBillToLocation.Value = ""
        lblBillToLocation.Text = ""
        txtItem.Value = ""
        lblItem.Text = ""
        txtTenderNo.Value = ""
        txtBillToLocation.Enabled = True
        txtItem.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = False
        btnDelete.Enabled = False

        EnableDisableControls(True)
        UcAttachment1.BlankAllControls()
        LoadBlankGrid()

    End Sub

    Sub EnableDisableControls(ByVal val As Boolean)
        txtTenderNo.Enabled = val
        txtVendorNo.Enabled = val
        txtItem.Enabled = val
        txtBillToLocation.Enabled = val
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Sub AddNew()
        BlankAllControls()
        txtDate.Focus()
        'RadButton1.Visible = False
        RadButton2.Enabled = True
        'RadButton3.Enabled = True
        EnabledDisable(True)
    End Sub

    Sub LoadBlankGrid()
        gvSchedule.DataSource = Nothing
        gvSchedule.Columns.Clear()
        gvSchedule.Rows.Clear()

        Dim repoNum As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "SNo"
        repoNum.Name = colScheduleSNo
        repoNum.Width = 50
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Schedule No"
        repoNum.Name = colScheduleNo
        repoNum.Width = 100
        repoNum.ReadOnly = True
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "From Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colScheduleFromDate
        repoDate.WrapText = True
        repoDate.ReadOnly = False
        repoDate.Width = 80
        gvSchedule.MasterTemplate.Columns.Add(repoDate)

        repoDate = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "To Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colScheduleToDate
        repoDate.WrapText = True
        repoDate.ReadOnly = False
        repoDate.Width = 80
        gvSchedule.MasterTemplate.Columns.Add(repoDate)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Parent SNo"
        repoNum.Name = colScheduleParentSNo
        repoNum.Width = 50
        repoNum.ReadOnly = True
        repoNum.IsVisible = False
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)



        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Quantity %"
        repoNum.Name = colScheduleQtyPer
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Quantity"
        repoNum.Name = colScheduleQty
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Short %"
        repoNum.Name = colScheduleShortPer
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Short Quantity"
        repoNum.Name = colScheduleShort
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        repoNum = New GridViewDecimalColumn()
        repoNum.FormatString = ""
        repoNum.HeaderText = "Late Days"
        repoNum.Name = colScheduleLateDays
        repoNum.ReadOnly = True
        repoNum.Width = 80
        repoNum.Minimum = 0
        repoNum.ShowUpDownButtons = False
        repoNum.Step = 0
        repoNum.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSchedule.MasterTemplate.Columns.Add(repoNum)

        gvSchedule.AllowDeleteRow = True
        gvSchedule.AllowAddNewRow = False
        gvSchedule.ShowGroupPanel = False
        gvSchedule.AllowColumnReorder = False
        gvSchedule.AllowRowReorder = False
        gvSchedule.EnableSorting = False
        gvSchedule.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSchedule.MasterTemplate.ShowRowHeaderColumn = False
        gvSchedule.TableElement.TableHeaderHeight = 40
    End Sub
    Function AllowToSave() As Boolean
        Try
            Xtra.TransactionValidity(txtDate.Value)
            'For ii As Integer = 0 To gvSchedule.Rows.Count - 1
            '    If clsCommon.myCBool(gvSchedule.Rows(ii).Cells("UserStatus").Value) Then
            '        If clsCommon.myCDecimal(gvSchedule.Rows(ii).Cells("FinalStatus").Value) = 0 Then
            '            If clsCommon.myCDecimal(gvSchedule.Rows(ii).Cells("NIRQCStatus").Value) = 0 Then
            '                Throw New Exception("QC of GRN [" + clsCommon.myCstr(gvSchedule.Rows(ii).Cells("GRN_No").Value) + "] is Pending/Not Generated But NIR QC Genrated")
            '            Else
            '                Throw New Exception("Invalid GRN [" + clsCommon.myCstr(gvSchedule.Rows(ii).Cells("GRN_No").Value) + "] Because SRN should be Posted")
            '            End If
            '        End If
            '        If ii > 0 Then
            '            If Not clsCommon.myCBool(gvSchedule.Rows(ii - 1).Cells("UserStatus").Value) Then
            '                Throw New Exception("Please First Check GRN [" + clsCommon.myCstr(gvSchedule.Rows(ii - 1).Cells("GRN_No").Value) + "]")
            '            End If
            '        End If
            '    End If
            'Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData(False)
    End Sub
    Sub SaveData(ByVal ChekBtnPost As Boolean, Optional ByVal isamendment As Boolean = False)
        Dim obj As New clsRALNOC()
        Try
            btnSave.Focus()
            If (AllowToSave()) Then
                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Tender_No = txtTenderNo.Value
                obj.Vendor_Code = txtVendorNo.Value
                obj.Item_Code = txtItem.Value
                obj.Location_Code = txtBillToLocation.Value
                obj.Remarks = txtRemarks.Text
                obj.ArrSchedule = New List(Of clsRALNOCSchedule)
                For Each grow As GridViewRowInfo In gvSchedule.Rows
                    Dim objTr As New clsRALNOCSchedule()
                    objTr.SNo = clsCommon.myCDecimal(grow.Cells(colScheduleSNo).Value)
                    objTr.PSNo = clsCommon.myCDecimal(grow.Cells(colScheduleParentSNo).Value)
                    objTr.Schedule_No = clsCommon.myCDecimal(grow.Cells(colScheduleNo).Value)
                    objTr.From_Date = clsCommon.myCDate(grow.Cells(colScheduleFromDate).Value)
                    objTr.To_Date = clsCommon.myCDate(grow.Cells(colScheduleToDate).Value)
                    objTr.Schedule_Qty_Per = clsCommon.myCDecimal(grow.Cells(colScheduleQtyPer).Value)
                    objTr.Schedule_Qty = clsCommon.myCDecimal(grow.Cells(colScheduleQty).Value)
                    objTr.Schedule_Short_Per = clsCommon.myCDecimal(grow.Cells(colScheduleShortPer).Value)
                    objTr.Schedule_Short = clsCommon.myCDecimal(grow.Cells(colScheduleShort).Value)
                    objTr.Late_Days = clsCommon.myCDecimal(grow.Cells(colScheduleLateDays).Value)
                    objTr.Arr = TryCast(grow.Cells(colScheduleLateDays).Tag, List(Of clsRALNOCSchedulePenelty))
                    obj.ArrSchedule.Add(objTr)
                Next
                If (obj.ArrSchedule Is Nothing OrElse obj.ArrSchedule.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If
                If (obj.SaveData(obj, isNewEntry)) Then
                    UcAttachment1.SaveData(txtDocNo.Value)
                    If ChekBtnPost = False Then
                        clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    End If
                    LoadData(obj.Document_No, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
        End Try
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Dim obj As New clsRALNOC()
        Try
            btnSave.Enabled = True
            btnPost.Enabled = False
            btnDelete.Enabled = False
            isInsideLoadData = False
            isNewEntry = True
            btnSave.Text = "Save"
            BlankAllControls()
            obj = clsRALNOC.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                isInsideLoadData = True
                isNewEntry = False
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                RadButton2.Enabled = True
                btnReverse.Enabled = False


                btnSave.Text = "Update"
                If obj.Status = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                    RadButton2.Enabled = False
                    btnReverse.Enabled = True

                End If

                UsLock1.Status = obj.Status
                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtTenderNo.Value = obj.Tender_No
                txtVendorNo.Value = obj.Vendor_Code
                lblVendorName.Text = obj.VendorName
                txtItem.Value = obj.Item_Code
                lblItem.Text = obj.ItemName
                txtBillToLocation.Value = obj.Location_Code
                lblBillToLocation.Text = obj.LocationName
                txtRemarks.Text = obj.Remarks


                EnableDisableControls(False)

                If obj.ArrSchedule IsNot Nothing AndAlso obj.ArrSchedule.Count > 0 Then
                    For Each objTr As clsRALNOCSchedule In obj.ArrSchedule
                        gvSchedule.Rows.AddNew()
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleSNo).Value = objTr.SNo
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleParentSNo).Value = objTr.PSNo
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleNo).Value = objTr.Schedule_No
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleFromDate).Value = objTr.From_Date
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleToDate).Value = objTr.To_Date
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQtyPer).Value = objTr.Schedule_Qty_Per
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQty).Value = objTr.Schedule_Qty
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShortPer).Value = objTr.Schedule_Short_Per
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShort).Value = objTr.Schedule_Short
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Value = objTr.Late_Days
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Tag = objTr.Arr
                    Next
                End If
                UcAttachment1.LoadData(txtDocNo.Value)
                EnabledDisable(False)
            End If
        Catch ex As Exception
            isNewEntry = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
            obj = Nothing
        End Try
    End Sub



    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
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
                'SaveData(True)
                If (clsRALNOC.PostData(txtDocNo.Value)) Then
                    clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
                If (clsRALNOC.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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
    Private Sub gv1_CellEditorInitialized(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvSchedule.CellEditorInitialized
        If TypeOf Me.gvSchedule.CurrentColumn Is GridViewMultiComboBoxColumn Then
            Dim editor As RadMultiColumnComboBoxElement = DirectCast(Me.gvSchedule.ActiveEditor, RadMultiColumnComboBoxElement)
            editor.AutoSizeDropDownToBestFit = True
            editor.EditorControl.MasterTemplate.BestFitColumns()
            editor.DropDownStyle = RadDropDownStyle.DropDown
            editor.AutoFilter = True
            If editor.EditorControl.MasterTemplate.FilterDescriptors.Count = 0 Then
                Dim autoFilter As FilterDescriptor = New FilterDescriptor("Name", FilterOperator.StartsWith, "")
                autoFilter.IsFilterEditor = True
                editor.EditorControl.FilterDescriptors.Add(autoFilter)
            End If
        End If
    End Sub
    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_RAL_NOC where Document_No='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "SELECT TSPL_RAL_NOC.Document_No,TSPL_RAL_NOC.Document_Date,TSPL_RAL_NOC.Tender_No, TSPL_RAL_NOC.Location_Code as Location, TSPL_LOCATION_MASTER.Location_Desc as LocationName,TSPL_RAL_NOC.Vendor_Code as Vendor,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_RAL_NOC.Item_Code as Item, TSPL_ITEM_MASTER.Item_Desc as ItemName,TSPL_RAL_NOC.Remarks,case when TSPL_RAL_NOC.Status='0' then 'Pending' else 'Approved' end as [Status]
FROM TSPL_RAL_NOC 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_RAL_NOC.Location_Code 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_RAL_NOC.Vendor_Code 
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_RAL_NOC.Item_Code"
        Dim whrClas As String = " 2=2   "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " and TSPL_RAL_NOC.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        LoadData(clsCommon.ShowSelectForm("RALNOC@Fnd", qry, "Document_No", whrClas, txtDocNo.Value, "TSPL_RAL_NOC.Document_Date desc", isButtonClicked, "TSPL_RAL_NOC.Document_Date"), NavigatorType.Current)
    End Sub
    Private Sub FrmAPInvoiceEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()

        ElseIf e.Alt AndAlso e.Shift AndAlso e.Control And e.KeyCode = Keys.F12 Then
            If MyBase.isReverse Then
                Dim frm As New FrmPWD(Nothing)
                frm.strType = clsFixedParameterType.SIR
                frm.strCode = clsFixedParameterCode.SIReversAndCreate
                frm.ShowDialog()
                If frm.isPasswordCorrect Then
                    btnReverse.Visible = True

                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "You are not authorized to perform this action.", Me.Text, MessageBoxButtons.OK, Telerik.WinControls.RadMessageIcon.Error)
                'MessageBox.Show("You are not authorized to perform this action.", "Unauthorized Access", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvSchedule.UserDeletingRow
        e.Cancel = True
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    clsRALNOC.ReverseAndUnpost(txtDocNo.Value)
                    clsCommon.MyMessageBoxShow(Me, "Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtBillToLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtBillToLocation._MYValidating
        Try
            Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER "
            Dim WhrCls As String = " Location_Type='Physical' and Rejected_Type='N' "
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtBillToLocation.Value = clsCommon.ShowSelectForm("RAL@VMaFND", qry, "Code", WhrCls, txtBillToLocation.Value, "Code", isButtonClicked)
            lblBillToLocation.Text = clsLocation.GetName(txtBillToLocation.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtTenderNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtTenderNo._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then

                Throw New Exception("Please select Location")
            End If
            Dim qry As String = "select xx.DocumentCode,max(xx.DocumentDate) as DocumentDate,xx.Location,max(TSPL_LOCATION_MASTER.Location_Desc) as LocationName,xx.Vendor_Code,max(TSPL_VENDOR_MASTER.Vendor_Name) as VendorName,xx.Item_Code,max(TSPL_ITEM_MASTER.Item_Desc) as ItemDesc from (
select DocumentCode,max(DocumentDate) as DocumentDate,Location,Vendor_Code,Item_Code,1 as RI,1 as Chk from (
select TSPL_TENDER_HEADER.DocumentCode,TSPL_TENDER_HEADER.DocumentDate,TSPL_TENDER_DETAIL.Location,TSPL_TENDER_DETAIL.Vendor_Code,TSPL_TENDER_DETAIL.Item_Code from TSPL_TENDER_DETAIL
left outer join TSPL_TENDER_HEADER on TSPL_TENDER_HEADER.DocumentCode=TSPL_TENDER_DETAIL.DocumentCode
where TSPL_TENDER_HEADER.Posted=1  and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "'
)x Group by DocumentCode,Location,Vendor_Code,Item_Code
union all
select TSPL_RAL_NOC.Tender_No as DocumentCode,null as  DocumentDate, Location_Code as Location,Vendor_Code,Item_Code,-1 as RI,0 as Chk from TSPL_RAL_NOC where TSPL_RAL_NOC.Document_No not in ('" + txtDocNo.Value + "')
)xx 
left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=xx.Location
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=xx.Vendor_Code
left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=xx.Item_Code
Group by xx.DocumentCode,xx.Location,xx.Vendor_Code,xx.Item_Code having sum(xx.RI)>0 and sum(xx.Chk)>0 order by DocumentDate"

            Dim whr As String = "TSPL_TENDER_HEADER.Posted=1 and exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode=TSPL_TENDER_HEADER.DocumentCode and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "')"
            txtTenderNo.Value = clsTenderHead.getFinder(whr, txtTenderNo.Value, isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtTenderNo.Value = ""
        End Try
    End Sub
    Private Sub txtVendorNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVendorNo._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                Throw New Exception("Please select Tender")
            End If
            Dim qry As String = "select Vendor_Code as Code,Vendor_Name as Name,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name],Terms_Code as [Term Code] ,Terms_Code_Desc as [Term Description] ,Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description] from TSPL_VENDOR_MASTER"
            Dim whr As String = " TSPL_VENDOR_MASTER.Status='N' and exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "' and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "' and TSPL_TENDER_DETAIL.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code) "
            txtVendorNo.Value = clsVendorMaster.getFinder(whr, txtVendorNo.Value, isButtonClicked)
            lblVendorName.Text = clsVendorMaster.GetName(txtVendorNo.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtVendorNo.Value = ""
        End Try
    End Sub
    Private Sub txtItem__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtItem._MYValidating
        Try
            If clsCommon.myLen(txtBillToLocation.Value) <= 0 Then
                Throw New Exception("Please select Location")
            End If
            If clsCommon.myLen(txtTenderNo.Value) <= 0 Then
                Throw New Exception("Please select Tender")
            End If
            If clsCommon.myLen(txtVendorNo.Value) <= 0 Then
                Throw New Exception("Please select Vendor")
            End If
            Dim whr As String = "  exists (select 1 from TSPL_TENDER_DETAIL where TSPL_TENDER_DETAIL.DocumentCode='" + txtTenderNo.Value + "' and TSPL_TENDER_DETAIL.Location='" + txtBillToLocation.Value + "' and TSPL_TENDER_DETAIL.Vendor_Code='" + txtVendorNo.Value + "' and TSPL_TENDER_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code) "
            txtItem.Value = clsItemMaster.getFinder(whr, txtItem.Value, isButtonClicked)
            lblItem.Text = clsItemMaster.GetItemName(txtItem.Value, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            txtItem.Value = ""
        End Try
    End Sub

    Private Sub RadButton2_Click_1(sender As Object, e As EventArgs) Handles RadButton2.Click
        FillData()

    End Sub

    Private Sub FillData()
        Try
            LoadBlankGrid()
            Dim qry As String = " select min(From_Date) as From_Date,max(To_Date) as To_Date,min(PSNo) as PSNo,SUM(Schedule_Qty) as Schedule_Qty 
from TSPL_TENDER_SCHEDULE 
where DocumentCode='" + txtTenderNo.Value + "' and Vendor_Code='" + txtVendorNo.Value + "' and Location_Code='" + txtBillToLocation.Value + "' and Item_Code='" + txtItem.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("No RAL Details found")
            End If

            Dim dtRunningDate As DateTime = clsCommon.myCDate(clsCommon.GetDateWithStartTime(dt.Rows(0)("From_Date")))
            Dim dtToDate As DateTime = clsCommon.myCDate(clsCommon.GetDateWithEndTime(dt.Rows(0)("To_Date")))
            If Not (txtDate.Value >= dtRunningDate AndAlso txtDate.Value <= dtToDate) Then
                Throw New Exception("NOC Date Should be between [" + clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("From_Date")), "dd/MM/yyyy") + "] and [" + clsCommon.GetPrintDate(clsCommon.myCDate(dt.Rows(0)("To_Date")), "dd/MM/yyyy") + "] ")
            End If
            Dim ArrSch As List(Of clsItemNOCSchedule) = clsItemNOCSchedule.GetData(txtItem.Value, Nothing)
            If ArrSch IsNot Nothing AndAlso ArrSch.Count > 0 Then
                For ii As Integer = 0 To ArrSch.Count - 1
                    If clsCommon.GetDateWithStartTime(dtRunningDate) < clsCommon.GetDateWithStartTime(dtToDate) Then
                        gvSchedule.Rows.AddNew()
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleSNo).Value = gvSchedule.Rows.Count
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleNo).Value = ArrSch(ii).SNo
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleFromDate).Value = dtRunningDate
                        If ii = 0 Then
                            dtRunningDate = txtDate.Value.AddDays(ArrSch(ii).Days - 1)
                            If dtRunningDate > dtToDate Then
                                dtRunningDate = dtToDate
                            End If
                        Else
                            dtRunningDate = dtToDate
                        End If
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleToDate).Value = dtRunningDate
                        dtRunningDate = dtRunningDate.AddDays(1)

                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleParentSNo).Value = clsCommon.myCDecimal(dt.Rows(0)("PSNo"))
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQtyPer).Value = ArrSch(ii).Qty_Per
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleQty).Value = ((clsCommon.myCDecimal(dt.Rows(0)("Schedule_Qty")) * ArrSch(ii).Qty_Per) / 100)
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShortPer).Value = ArrSch(ii).Short_Per
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleShort).Value = ((clsCommon.myCDecimal(dt.Rows(0)("Schedule_Qty")) * ArrSch(ii).Short_Per) / 100)
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Value = ArrSch(ii).Late_Days
                        gvSchedule.Rows(gvSchedule.Rows.Count - 1).Cells(colScheduleLateDays).Tag = SetNOCSchedulePenalty(ArrSch(ii).Arr, dtRunningDate)
                    End If
                Next
                EnabledDisable(False)
            Else
                Throw New Exception("Please define NOC schedule penalty")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub EnabledDisable(ByVal v As Boolean)
        txtBillToLocation.Enabled = v
        txtTenderNo.Enabled = v
        txtVendorNo.Enabled = v
        txtItem.Enabled = v
        txtDate.Enabled = v
    End Sub

    Private Function SetNOCSchedulePenalty(ByVal Arr As List(Of clsItemNOCSchedulePenalty), ByVal dtRunningDate As DateTime) As List(Of clsRALNOCSchedulePenelty)
        Dim ArrTemp As List(Of clsRALNOCSchedulePenelty) = Nothing
        If Arr IsNot Nothing AndAlso Arr.Count > 0 Then
            ArrTemp = New List(Of clsRALNOCSchedulePenelty)
            For Each objtr As clsItemNOCSchedulePenalty In Arr
                Dim objTemp As New clsRALNOCSchedulePenelty
                objTemp.Penalty_Date = dtRunningDate.AddDays(objtr.Penalty_Days - 1)
                objTemp.Penalty = objtr.Penalty
                ArrTemp.Add(objTemp)
            Next
        End If
        Return ArrTemp
    End Function

    Sub SetGridFormation(ByVal dt As DataTable)
        gvSchedule.DataSource = Nothing
        gvSchedule.Columns.Clear()
        gvSchedule.Rows.Clear()
        gvSchedule.DataSource = dt
        For ii As Integer = 0 To gvSchedule.Columns.Count - 1
            gvSchedule.Columns(ii).ReadOnly = True
            gvSchedule.Columns(ii).IsVisible = False
        Next
        gvSchedule.Columns("UserStatus").IsVisible = True
        gvSchedule.Columns("UserStatus").Width = 30
        gvSchedule.Columns("UserStatus").HeaderText = " "
        gvSchedule.Columns("UserStatus").ReadOnly = False

        gvSchedule.Columns("GRN_No").IsVisible = True
        gvSchedule.Columns("GRN_No").Width = 120
        gvSchedule.Columns("GRN_No").HeaderText = "GRN"

        gvSchedule.Columns("GRN_Date").IsVisible = True
        gvSchedule.Columns("GRN_Date").Width = 100
        gvSchedule.Columns("GRN_Date").HeaderText = "GRN Date"

        gvSchedule.Columns("VehicleNo").IsVisible = True
        gvSchedule.Columns("VehicleNo").Width = 100
        gvSchedule.Columns("VehicleNo").HeaderText = "Vehicle No"

        gvSchedule.Columns("GRNStatus").IsVisible = False

        gvSchedule.Columns("SRN_No").IsVisible = True
        gvSchedule.Columns("SRN_No").Width = 120
        gvSchedule.Columns("SRN_No").HeaderText = "SRN"

        gvSchedule.Columns("SRN_Date").IsVisible = True
        gvSchedule.Columns("SRN_Date").Width = 100
        gvSchedule.Columns("SRN_Date").HeaderText = "SRN Date"

        gvSchedule.Columns("SRNStatus").IsVisible = False

        gvSchedule.Columns("Weighment_Code").IsVisible = True
        gvSchedule.Columns("Weighment_Code").Width = 120
        gvSchedule.Columns("Weighment_Code").HeaderText = "Weighemnt No"

        gvSchedule.Columns("Weighment_Date").IsVisible = True
        gvSchedule.Columns("Weighment_Date").Width = 100
        gvSchedule.Columns("Weighment_Date").HeaderText = "Weighemnt Date"

        gvSchedule.Columns("Gross_Weight").IsVisible = True
        gvSchedule.Columns("Gross_Weight").Width = 100
        gvSchedule.Columns("Gross_Weight").HeaderText = "Gross Weight"
        gvSchedule.Columns("Gross_Weight").FormatString = "{0:n3}"

        gvSchedule.Columns("Tare_Weight").IsVisible = True
        gvSchedule.Columns("Tare_Weight").Width = 100
        gvSchedule.Columns("Tare_Weight").HeaderText = "Tare Weight"
        gvSchedule.Columns("Tare_Weight").FormatString = "{0:n3}"

        gvSchedule.Columns("Extra_Weight").IsVisible = True
        gvSchedule.Columns("Extra_Weight").Width = 100
        gvSchedule.Columns("Extra_Weight").HeaderText = "Extra Weight"
        gvSchedule.Columns("Extra_Weight").FormatString = "{0:n3}"

        gvSchedule.Columns("UOM").IsVisible = True
        gvSchedule.Columns("UOM").Width = 100
        gvSchedule.Columns("UOM").HeaderText = "UOM"

        gvSchedule.Columns("Net_Weight").IsVisible = True
        gvSchedule.Columns("Net_Weight").Width = 100
        gvSchedule.Columns("Net_Weight").HeaderText = "Net Weight"
        gvSchedule.Columns("Net_Weight").FormatString = "{0:n3}"

        gvSchedule.Columns("WeightmentStatus").IsVisible = False


        gvSchedule.Columns("SRN_Qty").IsVisible = True
        gvSchedule.Columns("SRN_Qty").Width = 100
        gvSchedule.Columns("SRN_Qty").HeaderText = "SRN Accepted Qty"
        gvSchedule.Columns("SRN_Qty").FormatString = "{0:n2}"

        gvSchedule.Columns("SecurityDeductionAmt").IsVisible = True
        gvSchedule.Columns("SecurityDeductionAmt").Width = 100
        gvSchedule.Columns("SecurityDeductionAmt").HeaderText = "Security Deduction"
        gvSchedule.Columns("SecurityDeductionAmt").FormatString = "{0:n2}"

        gvSchedule.Columns("QualityDeductionPer").IsVisible = True
        gvSchedule.Columns("QualityDeductionPer").Width = 100
        gvSchedule.Columns("QualityDeductionPer").HeaderText = "Quality Deduction %"
        gvSchedule.Columns("QualityDeductionPer").FormatString = "{0:n2}"

        gvSchedule.Columns("QualityDeductionAmt").IsVisible = True
        gvSchedule.Columns("QualityDeductionAmt").Width = 100
        gvSchedule.Columns("QualityDeductionAmt").HeaderText = "Quality Deduction Amount"
        gvSchedule.Columns("QualityDeductionAmt").FormatString = "{0:n2}"

        gvSchedule.Columns("LatePenaltyQty").IsVisible = True
        gvSchedule.Columns("LatePenaltyQty").Width = 100
        gvSchedule.Columns("LatePenaltyQty").HeaderText = "Late Penalty Qty"
        gvSchedule.Columns("LatePenaltyQty").FormatString = "{0:n2}"

        gvSchedule.Columns("LatePenaltyPer").IsVisible = True
        gvSchedule.Columns("LatePenaltyPer").Width = 100
        gvSchedule.Columns("LatePenaltyPer").HeaderText = "Late Penalty %"
        gvSchedule.Columns("LatePenaltyPer").FormatString = "{0:n2}"

        gvSchedule.Columns("LatePenaltyAmt").IsVisible = True
        gvSchedule.Columns("LatePenaltyAmt").Width = 100
        gvSchedule.Columns("LatePenaltyAmt").HeaderText = "Late Penalty Amount"
        gvSchedule.Columns("LatePenaltyAmt").FormatString = "{0:n2}"

        gvSchedule.Columns("FinalStatus").IsVisible = False

        gvSchedule.AllowAddNewRow = False
        gvSchedule.ShowGroupPanel = False
        gvSchedule.AllowColumnReorder = False
        gvSchedule.AllowRowReorder = False
        gvSchedule.EnableSorting = False
        gvSchedule.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSchedule.MasterTemplate.ShowRowHeaderColumn = False
        gvSchedule.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub gvSchedule_KeyDown(sender As Object, e As KeyEventArgs) Handles gvSchedule.KeyDown
        If e.KeyCode = Keys.F5 Then
            ShowPenalty()
        End If
    End Sub
    Private Sub ShowPenalty()
        Try
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add("Penalty Date", GetType(String))
            dt.Columns.Add("Penalty", GetType(Decimal))

            Dim arr As List(Of clsRALNOCSchedulePenelty) = TryCast(gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag, List(Of clsRALNOCSchedulePenelty))
            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                For ii As Integer = 0 To arr.Count - 1
                    Dim dr As DataRow = dt.NewRow
                    dr("Penalty Date") = clsCommon.GetPrintDate(arr(ii).Penalty_Date, "dd/MM/yyyy")
                    dr("Penalty") = arr(ii).Penalty
                    dt.Rows.Add(dr)
                Next
            End If
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As New FrmFreeGrid
                frm.dt = dt
                'frm.arrEditableColumn = New List(Of String)
                'frm.arrEditableColumn.Add("Penalty")
                frm.strFormName = "Show Penalty"
                frm.ReportID = "SchPenaltyD"
                frm.WindowState = FormWindowState.Maximized
                frm.ShowDialog()
                'If frm.dt IsNot Nothing AndAlso frm.dt.Rows.Count > 0 Then
                '    Dim ArrTemp As New List(Of clsItemSchedulePenalty)
                '    Dim obj As clsItemSchedulePenalty = Nothing
                '    For Each dr As DataRow In frm.dt.Rows
                '        obj = New clsItemSchedulePenalty()
                '        obj.Penalty_Days = clsCommon.myCDecimal(dr("Days"))
                '        obj.Penalty = clsCommon.myCDecimal(dr("Penalty"))
                '        ArrTemp.Add(obj)
                '    Next
                '    gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag = ArrTemp
                'Else
                '    gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag = Nothing
                'End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvSchedule_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSchedule.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvSchedule.Columns(colScheduleToDate) Then
                        Dim PKID As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select PK_Id from (select ROW_NUMBER() over (Partition by Item_Code order by PK_Id) as SNO, * from TSPL_ITEM_NOC_SCHEDULE where Item_Code= '" + txtItem.Value + "' )xx where SNO=" + clsCommon.myCstr(gvSchedule.CurrentRow.Cells(colScheduleNo).Value) + ""))
                        If clsCommon.myLen(PKID) > 0 Then
                            Dim Arr As List(Of clsItemNOCSchedulePenalty) = clsItemNOCSchedulePenalty.GetData(PKID, Nothing)
                            gvSchedule.CurrentRow.Cells(colScheduleLateDays).Tag = SetNOCSchedulePenalty(Arr, clsCommon.myCDate(gvSchedule.CurrentRow.Cells(colScheduleToDate).Value).AddDays(1))
                        End If
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtDocNo.Value, "Document_No", "TSPL_RAL_NOC", "TSPL_RAL_NOC_SCHEDULE")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
