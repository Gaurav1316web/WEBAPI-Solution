Imports System.Data.SqlClient
Imports common
Imports Telerik
Imports Telerik.WinControls.UI
Imports XpertERPEngine

Public Class frmMobileDCSDemand
    Inherits FrmMainTranScreen
#Region "Variables"
    Private isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Dim isLoadData As Boolean = False
    Dim isCopyData As Boolean = False
    Dim j As Integer = 0
    Const colDSelect As String = "SELECT"
    Const colPKID As String = "colPKID"
    Const colDate As String = "colDate"
    Const colVendorCode As String = "colVendorCode"
    Const colDCSUploaderNo As String = "colDCSUploaderNo"
    Const colDCSCode As String = "colDCSCode"
    Const colDCSName As String = "colDCSName"
    Const colZoneCode As String = "colZoneCode"
    Const colZoneName As String = "colZoneName"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colApproveQty As String = "colApproveQty"
    Const colQty As String = "colQty"
    Const colUOM As String = "colUOM"
    Const colPriceCode As String = "colPriceCode"
    Const colRate As String = "colRate"
    Const colAmount As String = "colAmount"
    Const ColShowOSAmt As String = "ColShowOSAmt"

#End Region

    Private Sub frmMobileDCSDemand_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        SetUserMgmtNew()
        Addnew()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "Integer Not NULL identity primary key")
        coll.Add("Document_Date", "DateTime NOT NULL")
        coll.Add("Vendor_Code", "Varchar(12) Not null REFERENCES TSPL_VENDOR_MASTER (Vendor_Code)")
        coll.Add("Item_Code", "varchar(50) Not NULL REFERENCES TSPL_ITEM_MASTER(ITEM_CODE)")
        coll.Add("Approve_Qty", "decimal(18,2) NOT NULL")
        coll.Add("Qty", "decimal(18,2) NOT NULL")
        coll.Add("UOM", "VARCHAR(12) NOT NULL")
        coll.Add("Price_code", "varchar(12) NULL")
        coll.Add("Rate", "decimal(18,2) NOT NULL")
        coll.Add("Amount", "Decimal(18,2) NULL")
        coll.Add("Status", "integer not null default 0")
        coll.Add("Approved_By", "varchar(12) NULL")
        coll.Add("Approved_Date", "Datetime NULL")
        coll.Add("Created_By", "varchar(12) not NULL")
        coll.Add("Created_Date", "Datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL")
        coll.Add("Modified_Date", "Datetime NOT NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_MOBILE_DCS_DEMAND", coll, Nothing, True, False)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs)
        Addnew()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnApprove.Visible = MyBase.isPostFlag
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colDSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)

        Dim repoTextBox As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "PKID"
        repoTextBox.Name = colPKID
        repoTextBox.Width = 180
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoDate.Format = DateTimePickerFormat.Custom
        repoDate.CustomFormat = "dd-MM-yyyy"
        repoDate.HeaderText = "Date"
        repoDate.FormatString = "{0:d}"
        repoDate.Name = colDate
        repoDate.Width = 100
        repoDate.ReadOnly = True
        repoDate.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoDate)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Vendor code"
        repoTextBox.Name = colVendorCode
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoDCSUploader As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSUploader.FormatString = ""
        repoDCSUploader.HeaderText = "DCS Uploader No"
        repoDCSUploader.Name = colDCSUploaderNo
        repoDCSUploader.Width = 120
        repoDCSUploader.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDCSUploader)

        Dim repoDCSCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSCode.FormatString = ""
        repoDCSCode.HeaderText = "DCS Code"
        repoDCSCode.Name = colDCSCode
        repoDCSCode.Width = 120
        repoDCSCode.ReadOnly = True
        repoDCSCode.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoDCSCode)

        Dim repoDCSName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDCSName.FormatString = ""
        repoDCSName.HeaderText = "DCS Name"
        repoDCSName.Name = colDCSName
        repoDCSName.Width = 160
        repoDCSName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDCSName)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Zone Code"
        repoTextBox.Name = colZoneCode
        repoTextBox.Width = 80
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Zone Name"
        repoTextBox.Name = colZoneName
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item"
        repoTextBox.Name = colItemCode
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        repoTextBox.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Item Desc"
        repoTextBox.Name = colItemName
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        Dim repoDecimal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Qty"
        repoDecimal.Name = colQty
        repoDecimal.ReadOnly = True
        repoDecimal.Width = 100
        repoDecimal.WrapText = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Approve Qty"
        repoDecimal.Name = colApproveQty
        repoDecimal.ReadOnly = False
        repoDecimal.Width = 100
        repoDecimal.WrapText = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "UOM"
        repoTextBox.Name = colUOM
        repoTextBox.Width = 120
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoTextBox = New GridViewTextBoxColumn()
        repoTextBox.FormatString = ""
        repoTextBox.HeaderText = "Price"
        repoTextBox.Name = colPriceCode
        repoTextBox.Width = 100
        repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTextBox)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Rate"
        repoDecimal.Name = colRate
        repoDecimal.ReadOnly = True
        repoDecimal.Width = 90
        repoDecimal.WrapText = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        repoDecimal = New GridViewDecimalColumn()
        repoDecimal.FormatString = ""
        repoDecimal.HeaderText = "Amount"
        repoDecimal.Name = colAmount
        repoDecimal.ReadOnly = True
        repoDecimal.Width = 100
        repoDecimal.WrapText = True
        repoDecimal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoDecimal)

        Dim repoShowOSAmt As GridViewCommandColumn = New GridViewCommandColumn()
        repoShowOSAmt.FormatString = ""
        repoShowOSAmt.UseDefaultText = True
        repoShowOSAmt.DefaultText = "Show OutStanding Amt"
        repoShowOSAmt.HeaderText = "Show OutStanding Amt"
        repoShowOSAmt.Name = ColShowOSAmt
        repoShowOSAmt.FieldName = ColShowOSAmt
        repoShowOSAmt.Width = 100
        repoShowOSAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.Columns.Add(repoShowOSAmt)

        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = True
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        'gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub
    Private Sub ReStoreGridLayoutgv1()
        Try
            Dim obj As clsGridLayout = New clsGridLayout()
            obj = CType(obj.GetData(MyBase.Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
            If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                Dim ii As Integer
                For ii = 0 To gv1.Columns.Count - 1 Step ii & 1
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
    Private Sub EnableDisableControls(ByVal val As Boolean)
        RadGroupBox1.Enabled = val
        btnGo.Enabled = val
    End Sub
    Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
        CheckAll()
    End Sub

    Sub UnCheckAll()
        If gv1 IsNot Nothing AndAlso gv1.ChildRows.Count > 0 Then
            For i As Integer = 0 To gv1.ChildRows.Count - 1
                gv1.ChildRows(i).Cells(colDSelect).Value = False
            Next
        End If
    End Sub

    Sub CheckAll()
        If gv1 IsNot Nothing AndAlso gv1.ChildRows.Count > 0 Then
            For i As Integer = 0 To gv1.ChildRows.Count - 1
                gv1.ChildRows(i).Cells(colDSelect).Value = True
            Next
        End If
    End Sub

    Private Sub btnUnselectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnselectAll.Click
        UnCheckAll()
    End Sub
    Function AllowToSave() As Boolean
        If clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") > clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") Then
            txtFromDate.Focus()
            Throw New Exception("From Date cannot be greater than To Date")
        End If
        Return True
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then

            End If
            Dim ArrCheck As New ArrayList()
            For ii As Integer = 0 To gv1.RowCount - 1
                If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                    ArrCheck.Add(gv1.Rows(ii).Cells(colDCSCode).Value)
                End If
            Next
            If ArrCheck.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please select at least one Document", Me.Text)
                Exit Sub
            End If
            If clsCommon.MyMessageBoxShow(Me, "Are you sure to Approve [" & clsCommon.myCstr(ArrCheck.Count) & "] Records?", "", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            Dim obj As New clsMobileDCSDemand()
            obj.Arr = New List(Of clsMobileDCSDemand)

            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myLen(clsCommon.myCdbl(grow.Cells(colPKID).Value)) > 0 AndAlso (clsCommon.myCBool(grow.Cells(colDSelect).Value)) Then
                    Dim rowObj As New clsMobileDCSDemand()
                    rowObj.PK_ID = clsCommon.myCdbl(grow.Cells(colPKID).Value)
                    rowObj.Approve_Qty = clsCommon.myCDecimal(grow.Cells(colApproveQty).Value)
                    rowObj.Price_Code = clsCommon.myCDecimal(grow.Cells(colPriceCode).Value)
                    rowObj.Rate = clsCommon.myCDecimal(grow.Cells(colRate).Value)
                    rowObj.Amount = clsCommon.myCDecimal(grow.Cells(colAmount).Value)
                    obj.Arr.Add(rowObj)
                End If
            Next

            If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                If obj.SaveData(obj.Arr) Then
                    If obj.dtError.Rows.Count > 0 Then
                        Dim ff As New FrmFreeGrid
                        ff.ReportID = "MobileDCSDemand"
                        ff.Text = "Mobile DCS Demand Errors"
                        ff.dt = obj.dtError
                        ff.ShowDialog()
                        clsCommon.MyMessageBoxShow(Me, "[" & clsCommon.myCstr(obj.Arr.Count - obj.dtError.Rows.Count) & "] No of Documents Successfully Approved", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow(Me, "Successfully Approved", Me.Text)
                    End If
                    LoadGridData(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub Addnew()
        btnApprove.Enabled = True
        LoadBlankGrid()
        isInsideLoadData = False
        ReStoreGridLayoutgv1()
        EnableDisableControls(True)
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CancelPressed()
    End Sub

    Private Sub btnApprove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprove.Click
        SaveData()
    End Sub

    'Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
    '    Try
    '        If (myMessages.deleteConfirm()) Then
    '            If (obj.DeleteData(txtDocumentNo.Value)) Then
    '                common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
    '                btnAddNew.PerformClick()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        myMessages.myExceptions(ex)
    '    End Try
    'End Sub
    'Sub LoadData(ByVal PK_ID As Integer, ByVal NavTyep As NavigatorType)
    '    Try
    '        btnApprove.Enabled = True
    '        Addnew()
    '        Dim obj = New clsMobileDCSDemand()
    '        obj = obj.GetData(PK_ID, NavTyep, Nothing)
    '        If (obj IsNot Nothing AndAlso clsCommon.myLen(clsCommon.myCstr(obj.PK_ID)) > 0) Then
    '            Addnew()
    '            isLoadData = True
    '            isNewEntry = False
    '            EnableDisableControls(False)
    '            If obj.Status = 1 Then
    '                btnDelete.Enabled = False
    '                btnApprove.Enabled = False
    '            Else
    '                btnDelete.Enabled = True
    '            End If
    '            'If obj IsNot Nothing Then
    '            '    For Each objtr As clsMobileDCSDemand In obj
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colPKID).Value = objtr.PK_ID
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = objtr.Document_Date
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colSaleAmount).Tag = objtr.ArrInvoiceAllDetails
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepositAmount).Tag = objtr.ArrReceiptAllDetails
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colSaleAmount).Value = objtr.Sale_Amt
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colDepositAmount).Value = objtr.Deposit_Amt
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colCurrBalanceAmount).Value = objtr.Curr_Balance_Amt
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colBalanceAmt).Value = objtr.Balance_Amt
    '            '        gv1.Rows(gv1.Rows.Count - 1).Cells(colPenalty).Value = objtr.Penalty
    '            '        gv1.Rows.AddNew()
    '            '    Next
    '            'End If

    '        End If
    '        isInsideLoadData = True
    '        isInsideLoadData = False
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
    '    Finally

    '    End Try
    'End Sub

    Private Sub frmMobileDCSDemand_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset.PerformClick()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnApprove.Enabled AndAlso MyBase.isModifyFlag Then
            '    SaveData()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnApprove.Enabled AndAlso MyBase.isDeleteFlag Then
            '    btnDelete.PerformClick()
            'ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            '    btnPost.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            Me.Close()
        End If
    End Sub
    Sub CancelPressed()
        Me.Close()
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadGridData(True)
    End Sub

    Private Sub LoadGridData(ByVal ShowMessage As Boolean)
        Try
            Dim qry As String = ""
            LoadBlankGrid()
            qry = " select TSPL_MOBILE_DCS_DEMAND.PK_Id,TSPL_MOBILE_DCS_DEMAND.Document_Date,TSPL_MOBILE_DCS_DEMAND.Vendor_Code, VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VLC_Code,VLC_Name,TSPL_USER_CUSTOMER_ZONE.Zone_Code,TSPL_ZONE_MASTER.Description as Zone_Name,TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_MOBILE_DCS_DEMAND.Approve_Qty,TSPL_MOBILE_DCS_DEMAND.Qty,TSPL_MOBILE_DCS_DEMAND.UOM,
            TSPL_MOBILE_DCS_DEMAND.Price_code,TSPL_MOBILE_DCS_DEMAND.Rate,TSPL_MOBILE_DCS_DEMAND.Amount,case when isnull(TSPL_MOBILE_DCS_DEMAND.Status,0)= 1 then 'Approved' else  'Pending' end as Status  from TSPL_MOBILE_DCS_DEMAND  left join TSPL_USER_CUSTOMER_ZONE on TSPL_USER_CUSTOMER_ZONE.User_Code = '" & objCommonVar.CurrentUserCode & "' inner join TSPL_VENDOR_MASTER ON TSPL_VENDOR_MASTER.Vendor_Code = TSPL_MOBILE_DCS_DEMAND.VENDOR_CODE and TSPL_VENDOR_MASTER.ZONE_CODE= TSPL_USER_CUSTOMER_ZONE.ZONE_CODE left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code = TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_ZONE_MASTER ON TSPL_ZONE_MASTER.Zone_Code = TSPL_VENDOR_MASTER.Zone_Code
            left outer join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_MOBILE_DCS_DEMAND.Item_Code where isnull(TSPL_MOBILE_DCS_DEMAND.Status,0)= 0 and convert(date,TSPL_MOBILE_DCS_DEMAND.Document_Date,103) >= '" & clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") & "' and convert(date,TSPL_MOBILE_DCS_DEMAND.Document_Date,103) <= '" & clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") & "' "
            If txtDCS.arrValueMember IsNot Nothing AndAlso txtDCS.arrValueMember.Count > 0 Then
                qry += " and TSPL_VLC_MASTER_HEAD.VLC_CODE IN (" & clsCommon.GetMulcallString(txtDCS.arrValueMember) & ")"
            End If
            qry += " order by TSPL_MOBILE_DCS_DEMAND.PK_Id "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt.Rows.Count > 0 Then
                Dim Rate As Double = 0
                For ii As Integer = 0 To dt.Rows.Count - 1
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colPKID).Value = dt.Rows(ii)("PK_Id")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value = dt.Rows(ii)("Document_Date")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colVendorCode).Value = dt.Rows(ii)("Vendor_Code")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSUploaderNo).Value = dt.Rows(ii)("VLC_Code_VLC_Uploader")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSCode).Value = dt.Rows(ii)("VLC_Code")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCSName).Value = dt.Rows(ii)("VLC_Name")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colZoneCode).Value = dt.Rows(ii)("Zone_Code")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colZoneName).Value = dt.Rows(ii)("Zone_Name")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = dt.Rows(ii)("Item_Code")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = dt.Rows(ii)("Item_Desc")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colApproveQty).Value = dt.Rows(ii)("Approve_Qty")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = dt.Rows(ii)("Qty")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = dt.Rows(ii)("UOM")
                    GetRateMccSale(gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value, gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value, gv1.Rows(gv1.Rows.Count - 1).Cells(colDate).Value, gv1.CurrentRow.Index)
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = (clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colApproveQty).Value) * clsCommon.myCdbl(gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value))
                    UpdateCurrentRow(ii)
                Next
                CheckAll()
                EnableDisableControls(False)
            ElseIf ShowMessage Then
                Throw New Exception("No Data Found")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function GetRateMccSale(ByVal Itemcode As String, ByVal Unitcode As String, ByVal Effctv_date As Date, ByVal IntRowNo As Integer)
        Dim tranDate As String = clsCommon.GetPrintDate(Effctv_date, "dd/MMM/yyyy")
        Dim dt As DataTable = New DataTable()
        Dim Rate As Double = 0
        Dim qry As String = "select top 1 TSPL_MCC_RATE_UPLOADER_Detail.PK_ID, TSPL_MCC_RATE_UPLOADER_Detail.Price from TSPL_MCC_RATE_UPLOADER_master " _
              & " left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.Code=TSPL_MCC_RATE_UPLOADER_MASTER.code where Item_Code='" & Itemcode & "' " _
              & " and TSPL_MCC_RATE_UPLOADER_Detail.RATE_UOM='" & Unitcode & "' and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Date,103) <=convert(date,'" & tranDate & "',103) and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Effective_date,103) >=convert(date,'" & tranDate & "',103) order by date desc ,TSPL_MCC_RATE_UPLOADER_master.code desc "
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            gv1.Rows(IntRowNo).Cells(colPriceCode).Value = clsCommon.myCdbl(dt.Rows(0)("PK_ID"))
            gv1.Rows(IntRowNo).Cells(colRate).Value = clsCommon.myCdbl(dt.Rows(0)("Price"))
            Rate = clsCommon.myCdbl(dt.Rows(0)("Price"))
        End If

        If Rate <= 0 Then
            qry = "select top 1 TSPL_MCC_RATE_UPLOADER_Detail.PK_ID, TSPL_ITEM_UOM_DETAIL.Item_Code,Price,TSPL_ITEM_UOM_DETAIL.Conversion_Factor from TSPL_MCC_RATE_UPLOADER_master  " _
             & " left join TSPL_MCC_RATE_UPLOADER_Detail on TSPL_MCC_RATE_UPLOADER_Detail.Code=TSPL_MCC_RATE_UPLOADER_MASTER.code inner join TSPL_ITEM_UOM_DETAIL on " _
             & " TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_MCC_RATE_UPLOADER_Detail.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_MCC_RATE_UPLOADER_Detail.RATE_UOM where TSPL_MCC_RATE_UPLOADER_Detail.Item_Code='" & Itemcode & "' " _
             & " and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Date,103) <=convert(date,'" & tranDate & "',103) and convert(date,TSPL_MCC_RATE_UPLOADER_Master.Effective_date,103) >=convert(date,'" & tranDate & "',103) order by date desc ,TSPL_MCC_RATE_UPLOADER_master.code desc "
            dt = clsDBFuncationality.GetDataTable(qry)
            If Dt.Rows.Count > 0 Then
                Dim Conv_Fac As Double = clsDBFuncationality.getSingleValue("select conversion_factor from TSPL_ITEM_UOM_DETAIL where Item_Code='" & Itemcode & "'  and Uom_Code='" & Unitcode & "' ")
                Rate = Conv_Fac * clsCommon.myCdbl(dt.Rows(0)("Price")) / IIf(clsCommon.myCdbl(dt.Rows(0)("Conversion_Factor")) > 0, clsCommon.myCdbl(dt.Rows(0)("Conversion_Factor")), 1)
                gv1.Rows(IntRowNo).Cells(colPriceCode).Value = Rate
                gv1.Rows(IntRowNo).Cells(colRate).Value = clsCommon.myCdbl(dt.Rows(0)("Price"))
                Rate = clsCommon.myCdbl(dt.Rows(0)("Price"))
            End If
        End If
        Return Rate
    End Function
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim dblRate As Double = 0
        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colDCSCode).Value)) > 0 Then
            dblRate = gv1.Rows(IntRowNo).Cells(colRate).Value
            gv1.Rows(IntRowNo).Cells(colAmount).Value = (dblRate * gv1.Rows(IntRowNo).Cells(colApproveQty).Value)
        End If
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        EnableDisableControls(True)
        LoadBlankGrid()
    End Sub

    'Private Sub btnPrint_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select  '" & objCommonVar.CurrentUser & "' as User_Code, ROW_NUMBER( ) over( order by TSPL_CUSTOMER_PENALTY.Document_date) as SNo,TSPL_COMPANY_MASTER.Comp_Name,convert(varchar,TSPL_CUSTOMER_PENALTY_detail.Invoice_Date,103) as Invoice_Date,TSPL_CUSTOMER_PENALTY_detail.Sale_Amt,TSPL_CUSTOMER_PENALTY_detail.Deposit_Amt,TSPL_CUSTOMER_PENALTY_detail.Curr_Balance_Amt,TSPL_CUSTOMER_PENALTY_detail.Balance_Amt,TSPL_CUSTOMER_PENALTY_detail.Penalty,convert(varchar,TSPL_CUSTOMER_PENALTY.Document_date,103) as Document_date,TSPL_CUSTOMER_PENALTY.Cust_Code,TSPL_CUSTOMER_MASTER.Customer_Name,
    '    convert(varchar,TSPL_CUSTOMER_PENALTY.From_Date,103) as From_Date,convert(varchar,TSPL_CUSTOMER_PENALTY.To_Date,103) as To_Date,TSPL_CUSTOMER_PENALTY.Penalty_Per from  TSPL_CUSTOMER_PENALTY_detail
    '    left join TSPL_CUSTOMER_PENALTY on TSPL_CUSTOMER_PENALTY.Document_No = TSPL_CUSTOMER_PENALTY_detail.Document_No left join TSPL_COMPANY_MASTER on 1=1 left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.cust_code = TSPL_CUSTOMER_PENALTY.Cust_Code
    '    where TSPL_CUSTOMER_PENALTY.Document_No='" & txtDocumentNo.Value & "'"
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
    '    If dt.Rows.Count > 0 Then
    '        Dim frmCRV As New frmCrystalReportViewer()
    '        frmCRV.funreport(MyBase.Form_ID, False, CrystalReportFolder.KwalitySalesReport, dt, "rptCustomerPenalty", "Customer Penalty", clsCommon.myCDate(dt.Rows(0)("Document_date")))
    '        frmCRV = Nothing
    '    End If
    'End Sub

    Private Sub txtDCS__My_Click(sender As Object, e As EventArgs) Handles txtDCS._My_Click
        Try
            Dim qry As String = " select VLC_Code_VLC_Uploader as [DCS Uploader Code],VLC_Code as [DCS Code],VLC_Name as [DCS Name],tspl_vendor_master.Vendor_Code as [Vendor Code] from TSPL_VLC_MASTER_HEAD left join tspl_vendor_master on tspl_vendor_master.Vendor_Code = TSPL_VLC_MASTER_HEAD.VSP_CODE "
            txtDCS.arrValueMember = clsCommon.ShowMultipleSelectForm("DCSMulS", qry, "DCS Code", "DCS Name", txtDCS.arrValueMember, txtDCS.arrDispalyMember)
        Catch ex As Exception

        End Try
    End Sub
    Sub gv1_CommandCellClick(ByVal sender As Object, ByVal e As EventArgs) Handles gv1.CommandCellClick
        Try
            If (Not isInsideLoadData) Then
                isInsideLoadData = True
                If gv1.CurrentColumn Is gv1.Columns(ColShowOSAmt) Then
                    Try
                        Dim frm As New frmDCSOutstanding()
                        frm.VLC_Code_VLC_Uploader = gv1.CurrentRow.Cells(colDCSUploaderNo).Value
                        frm.VLC_Code = gv1.CurrentRow.Cells(colDCSCode).Value
                        frm.VLC_name = gv1.CurrentRow.Cells(colDCSName).Value
                        frm.Vendor_Code = gv1.CurrentRow.Cells(colVendorCode).Value
                        frm.TransDate = gv1.CurrentRow.Cells(colDate).Value
                        frm.Show()
                    Catch ex As Exception
                        clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                    End Try
                End If
                isInsideLoadData = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If (e.Column Is gv1.Columns(colApproveQty)) Then
                        UpdateCurrentRow(gv1.CurrentRow.Index) '
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class


