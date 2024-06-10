Imports common
Imports common.UserControls
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine

Public Class frmRCDFRateControl
#Region "Variables"
    Dim isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = True
    Dim isNewEntry As Boolean = False
    Const colSN As String = "colSN"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colUOM As String = "colUOM"
    Const colItemType As String = "colItemType"
    Const colRate As String = "colRate"
    Const colTolerance As String = "colTolerance"
#End Region

    Private Sub frmRCDFRateControl_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub createTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Code", "varchar(30) NOT NULL Primary Key")
        coll.Add("Doc_Date", "DateTime NOT NULL ")
        coll.Add("From_Date", "Date NOT NULL")
        coll.Add("To_Date", "Date NULL")
        coll.Add("Remarks", "VARCHAR(200) null")
        coll.Add("Comments", "VARCHAR(200) null")
        coll.Add("Created_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Created_Date", "datetime NOT NULL")
        coll.Add("Modified_By", "varchar(12) NOT NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Modified_Date", "datetime NOT NULL")
        coll.Add("Posted_By", "varchar(12) NULL REFERENCES TSPL_USER_MASTER (USER_CODE)")
        coll.Add("Posted_Date", "datetime NULL")
        coll.Add("STATUS", "integer null")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_RCDF_RATE_CONTROL", coll)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Code", "varchar(30) NOT NULL REFERENCES TSPL_RCDF_RATE_CONTROL (Code)")
        coll.Add("Item_Code", "varchar(50) NOT NULL REFERENCES TSPL_ITEM_MASTER (Item_Code)")
        coll.Add("UOM", "varchar(12) NOT NULL REFERENCES TSPL_UNIT_MASTER (Unit_Code)")
        coll.Add("Rate", "decimal(18,2) NOT NULL")
        coll.Add("Tolerance", "decimal(18,2) NULL")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_RCDF_RATE_CONTROL_DETAIL", coll)

        coll = New Dictionary(Of String, String)()
        coll.Add("PK_Id", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Code", "varchar(30) NOT NULL REFERENCES TSPL_RCDF_RATE_CONTROL (CODE)")
        coll.Add("Against_PK_Id", "integer NOT NULL REFERENCES TSPL_RCDF_RATE_CONTROL_DETAIL (PK_Id)")
        coll.Add("UOM", "varchar(12) NOT NULL REFERENCES TSPL_UNIT_MASTER (Unit_Code)")
        coll.Add("Min_Rate", "decimal(18,2) NOT NULL")
        coll.Add("Max_Rate", "decimal(18,2) NOT NULL")
        clsCommonFunctionality.CreateOrAlterTable("TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM", coll)
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim gridcolSN As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolSN.FormatString = ""
        gridcolSN.HeaderText = "S.No."
        gridcolSN.Name = colSN
        gridcolSN.Width = 50
        gridcolSN.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(gridcolSN)
        gridcolSN.ReadOnly = True

        Dim gridcolItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolItemCode.FormatString = ""
        gridcolItemCode.HeaderText = "Item Code"
        gridcolItemCode.Name = colItemCode
        gridcolItemCode.Width = 110
        gv1.MasterTemplate.Columns.Add(gridcolItemCode)
        'gridcolItemCode.ReadOnly = True

        Dim gridcolItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolItemName.FormatString = ""
        gridcolItemName.HeaderText = "Item Name"
        gridcolItemName.Name = colItemName
        gridcolItemName.Width = 110
        gridcolItemName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(gridcolItemName)
        gridcolItemName.ReadOnly = True

        Dim gridcolUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolUOM.FormatString = ""
        gridcolUOM.HeaderText = "UOM"
        gridcolUOM.Name = colUOM
        gridcolUOM.Width = 110
        gv1.MasterTemplate.Columns.Add(gridcolUOM)
        'gridcolUOM.ReadOnly = True

        Dim gridcolItemType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        gridcolItemType.FormatString = ""
        gridcolItemType.HeaderText = "Item Type"
        gridcolItemType.Name = colItemType
        gridcolItemType.Width = 110
        gv1.MasterTemplate.Columns.Add(gridcolItemType)
        gridcolItemType.ReadOnly = True

        Dim gridcolRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        gridcolRate.FormatString = ""
        gridcolRate.HeaderText = "Rate"
        gridcolRate.Name = colRate
        gridcolRate.Width = 110
        gv1.MasterTemplate.Columns.Add(gridcolRate)
        'gridcolRate.IsVisible = False

        Dim gridcolTolerance As GridViewDecimalColumn = New GridViewDecimalColumn()
        gridcolTolerance.FormatString = ""
        gridcolTolerance.HeaderText = "Tolerance"
        gridcolTolerance.Name = colTolerance
        gridcolTolerance.Width = 110
        gv1.MasterTemplate.Columns.Add(gridcolTolerance)
        'gridcolRangeTo.IsVisible = False

        'gridcolRangeSelection.IsVisible = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.AllowRowReorder = False
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnChooser = True
        gv1.AllowColumnReorder = True
        gv1.Rows.AddNew()
        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
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
            End If
        Catch err As Exception
            clsCommon.MyMessageBoxShow(Me, err.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtCode._MYValidating
        Try
            Dim qry As String = ""
            Dim count As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_RCDF_RATE_CONTROL where Code ='" + txtCode.Value + "'"))
            If count = 0 Then
                txtCode.MyReadOnly = False
            Else
                txtCode.MyReadOnly = True
            End If
            If txtCode.MyReadOnly OrElse isButtonClicked Then
                qry = "select Code,Doc_Date As [Date],Case When IsNull(STATUS,0)=0 Then 'Pending' Else 'Approved' End As [Status] from TSPL_RCDF_RATE_CONTROL"
                txtCode.Value = clsCommon.ShowSelectForm("RRC", qry, "Code", "", txtCode.Value, "Code", isButtonClicked, Nothing)
                LoadData(txtCode.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            Reset()
            gv1.DataSource = Nothing
            gv1.Refresh()
            isInsideLoadData = True
            txtCode.MyReadOnly = True

            Dim obj As clsRCDFRateControl = clsRCDFRateControl.GetData(strCode, NavType)
            If obj IsNot Nothing Then
                txtCode.Value = clsCommon.myCstr(obj.Code)
                txtDocDate.Value = clsCommon.GetPrintDate(obj.Doc_Date, "dd/MM/yyyy")
                txtFromDate.Value = clsCommon.GetPrintDate(obj.From_Date, "dd/MM/yyyy")
                If clsCommon.myLen(obj.To_Date) > 0 Then
                    txtToDate.Checked = True
                    txtToDate.Value = clsCommon.GetPrintDate(obj.To_Date, "dd/MM/yyyy")
                Else
                    txtToDate.Checked = False
                    txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                End If
                txtRemarks.Text = clsCommon.myCstr(obj.Remarks)
                txtComments.Text = clsCommon.myCstr(obj.Comments)
                If clsCommon.myCDecimal(obj.Status) > 0 Then
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnsave.Enabled = False
                    btndelete.Enabled = False
                    btnPost.Enabled = False
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                    btnsave.Enabled = True
                    btndelete.Enabled = True
                    btnPost.Enabled = True
                End If
                If obj.arrDetail IsNot Nothing AndAlso obj.arrDetail.Count > 0 Then
                    Dim i As Integer = 1
                    For Each grow In obj.arrDetail
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSN).Value = i
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = grow.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = grow.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUOM).Value = grow.UOM
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemType).Value = grow.Item_Type
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRate).Value = grow.Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colTolerance).Value = grow.Tolerance
                        gv1.Rows.AddNew()
                        i += 1
                    Next
                End If
            End If
            isInsideLoadData = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isInsideLoadData Then
                If isCellValueChangedOpen Then
                    isCellValueChangedOpen = False
                    If e.Column Is gv1.Columns(colItemCode) Then
                        OpenICodeList(False, True, False)
                    ElseIf e.Column Is gv1.Columns(colUOM) Then
                        OpenICodeList(False, False, True)
                    End If
                    isCellValueChangedOpen = True
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub OpenICodeList(ByVal isButtonClick As Boolean, ByVal isItem As Boolean, ByVal isUOM As Boolean)
        Try
            Dim qry As String = Nothing
            Dim whrls As String = Nothing
            Dim whrcls As String = Nothing
            Dim dt As DataTable = Nothing
            Dim check As List(Of String) = New List(Of String)
            If clsCommon.myLen(gv1.CurrentRow.Cells(colItemCode).Value) > 0 AndAlso isItem AndAlso Not isUOM Then
                If gv1.Rows.Count > 2 Then
                    For Each row In gv1.Rows
                        check.Add(clsCommon.myCstr(row.Cells("colItemCode").Value))
                    Next
                End If
                If check IsNot Nothing AndAlso check.Count > 2 Then
                    whrls = "  Item_Code not in (" + clsCommon.GetMulcallString(check) + ")"
                End If
                qry = " select Item_Code As Code,Item_Desc As Name,Short_Description As Description,Unit_Code As UOM from TSPL_ITEM_MASTER "
                gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.ShowSelectForm("ItemFnder", qry, "Code", whrls, clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value), "", isButtonClick)

                whrcls = " where TSPL_ITEM_MASTER.Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "'"
                qry = " select TSPL_ITEM_MASTER.Item_Code As Code,TSPL_ITEM_MASTER.Item_Desc As Name,TSPL_ITEM_MASTER.Short_Description As Description,TSPL_ITEM_MASTER.Unit_Code As UOM,TSPL_ITEM_MASTER.Item_Type,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME from TSPL_ITEM_MASTER
                      Inner Join TSPL_ITEM_TYPE_MASTER ON TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE=TSPL_ITEM_MASTER.Item_Type " + whrcls
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.CurrentRow.Cells(colItemCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                    gv1.CurrentRow.Cells(colItemName).Value = clsCommon.myCstr(dt.Rows(0)("Name"))
                    gv1.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("UOM"))
                    gv1.CurrentRow.Cells(colItemType).Value = clsCommon.myCstr(dt.Rows(0)("ITEM_TYPE_NAME"))
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Item_Type")), "F") = CompairStringResult.Equal Then
                        gv1.CurrentRow.Cells(colTolerance).Value = 0
                        gv1.CurrentRow.Cells(colTolerance).ReadOnly = True
                    Else
                        gv1.CurrentRow.Cells(colTolerance).ReadOnly = False
                    End If
                End If
            ElseIf clsCommon.myLen(gv1.CurrentRow.Cells(colUOM).Value) > 0 AndAlso Not isItem AndAlso isUOM Then
                qry = " Select Unit_Code As Code from TSPL_UNIT_MASTER "
                gv1.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("ItemUOM", qry, "Code", whrls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUOM).Value), "", isButtonClick)

                whrcls = " where Unit_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colUOM).Value) + "'"
                qry = " Select Unit_Code As Code from TSPL_UNIT_MASTER " + whrcls
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    gv1.CurrentRow.Cells(colUOM).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                End If
            End If
        Catch ex As Exception
            gv1.CurrentRow.Cells(colItemCode).Value = ""
            gv1.CurrentRow.Cells(colItemName).Value = ""
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        Try
            If gv1.RowCount > 0 Then
                Dim intCurrRow As Integer = gv1.CurrentRow.Index
                If intCurrRow = gv1.Rows.Count - 1 AndAlso clsCommon.myLen(clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value)) > 0 Then
                    gv1.Rows.AddNew()
                    gv1.CurrentRow = gv1.Rows(intCurrRow)
                End If
                For i As Integer = 0 To gv1.Rows.Count - 1
                    gv1.Rows(0).Cells(0).Value = 1
                    If i <> 0 Then
                        gv1.Rows(i).Cells(colSN).Value = i + 1
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        Try
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        txtCode.Value = ""
        txtDocDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtFromDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtToDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
        txtRemarks.Text = ""
        txtComments.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        LoadBlankGrid()
    End Sub

    Function AllowToSave() As Boolean
        Dim isAllow As Boolean = True
        If txtToDate.Checked AndAlso clsCommon.myCDate(txtFromDate.Value) >= clsCommon.myCDate(txtToDate.Value) Then
            clsCommon.MyMessageBoxShow(Me, "From Date can't be less than To Date.", Me.Text)
            isAllow = False
        End If
        Return isAllow
    End Function
    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        Try
            SaveData(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Dim isNewEntry As Boolean = False
        If AllowToSave() Then
            Dim obj As New clsRCDFRateControl()
            obj.Code = txtCode.Value
            obj.Doc_Date = txtDocDate.Value
            obj.From_Date = txtFromDate.Value
            If txtToDate.Checked Then
                obj.To_Date = txtToDate.Value
            End If
            obj.Remarks = txtRemarks.Text
            obj.Comments = txtComments.Text
            obj.arrDetail = New List(Of clsRCDFRateControlDetail)
            If gv1 IsNot Nothing AndAlso gv1.Rows.Count > 0 Then
                Dim objTr As clsRCDFRateControlDetail
                For Each grow As GridViewRowInfo In gv1.Rows
                    objTr = New clsRCDFRateControlDetail()
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells("colItemCode").Value)
                    objTr.UOM = clsCommon.myCstr(grow.Cells("colUOM").Value)
                    objTr.Rate = clsCommon.myCDecimal(grow.Cells("colRate").Value)
                    objTr.Tolerance = clsCommon.myCDecimal(grow.Cells("colTolerance").Value)
                    If clsCommon.myLen(clsCommon.myCstr(grow.Cells("colItemCode").Value)) > 0 Then
                        obj.arrDetail.Add(objTr)
                    End If
                Next
            End If

            If clsCommon.myLen(txtCode.Value) <= 0 Then
                isNewEntry = True
            End If
            If clsRCDFRateControl.SaveData(obj, isNewEntry, isPost) Then
                If Not isPost Then
                    clsCommon.MyMessageBoxShow(Me, "Data save successfully.", Me.Text)
                    btnsave.Text = "Update"
                Else
                    clsCommon.MyMessageBoxShow(Me, "Data posted successfully.", Me.Text)
                End If

            End If
            LoadData(obj.Code, NavigatorType.Current)
        End If
    End Sub

    Private Sub gv1_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserAddedRow
        Try
            For i As Integer = 0 To gv1.Rows.Count - 1
                gv1.Rows(0).Cells(0).Value = 1
                If i <> 0 Then
                    gv1.Rows(i).Cells(colSN).Value = i + 1
                End If
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            For ii As Integer = 1 To gv1.Rows.Count
                gv1.Rows(ii - 1).Cells(colSN).Value = ii
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_UserDeletingRow(sender As Object, e As GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        Try
            If clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then
                e.Cancel = True
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        Try
            If clsCommon.MyMessageBoxShow(Me, "Are you sure to delete?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                If clsRCDFRateControl.DeleteData(txtCode.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully.", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            SaveData(True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Try
            Dim Qry As String = " Select ROW_NUMBER() Over (Order By (Select 1)) As 'S.No.',TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.Code,TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.UOM,TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.Min_Rate As 'Min Rate',TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.Max_Rate As 'Max Rate' from TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM 
                                  Inner Join TSPL_RCDF_RATE_CONTROL_DETAIL On TSPL_RCDF_RATE_CONTROL_DETAIL.Code=TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.Code And TSPL_RCDF_RATE_CONTROL_DETAIL.Pk_ID=TSPL_RCDF_RATE_CONTROL_DETAIL_ALL_UOM.Against_PK_Id
                                  Where TSPL_RCDF_RATE_CONTROL_DETAIL.Code='" + clsCommon.myCstr(txtCode.Value) + "' And TSPL_RCDF_RATE_CONTROL_DETAIL.Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim frm As New FrmFreeGrid()
                frm.ReportID = Form_ID
                frm.dt = dt
                frm.ShowDialog()
            Else
                clsCommon.MyMessageBoxShow(Me, "Data not found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class