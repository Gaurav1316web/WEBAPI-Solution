Imports common

Public Class FrmBoothCommissionMaster
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Dim InActiveDoc As Boolean = False
    Const colSNo As String = "colSNo"
    Const colICode As String = "colICode"
    Const colIName As String = "colIName"
    Const colCRate As String = "colCRate"
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnpost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmBoothCommissionMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        CreateTable()
        AddNew()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        FrmClose()
    End Sub
    Private Sub FrmClose()
        Me.Close()
    End Sub
    Private Sub AddNew()
        isNewEntry = True
        txtDocNo.Value = ""
        txtUOM.Value = ""
        txtperDayQty.Text = 0
        txtRemark.Text = ""
        txtComment.Text = ""
        UsLock1.Status = common.ERPTransactionStatus.Pending
        chkInActive.Checked = False
        chkInActive.Enabled = False
        chkMobileUser.Checked = True
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = txtFromDate.Value
        txtDate.Value = txtToDate.Value
        LoadBlankGrid()
    End Sub
    Private Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "SNo"
        repoNumBox.Name = colSNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)

        Dim repoItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemCode.FormatString = ""
        repoItemCode.HeaderText = "Item Code"
        repoItemCode.Name = colICode
        repoItemCode.IsVisible = True
        repoItemCode.ReadOnly = False
        repoItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemCode)

        Dim repoItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoItemName.FormatString = ""
        repoItemName.HeaderText = "Item Name"
        repoItemName.Name = colIName
        repoItemName.IsVisible = True
        repoItemName.ReadOnly = True
        repoItemName.Width = 200
        repoItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoItemName)
        Dim repoCommissionRate = New GridViewDecimalColumn()
        repoCommissionRate.FormatString = "{0:n4}"
        repoCommissionRate.HeaderText = "Commission Rate"
        repoCommissionRate.Name = colCRate
        repoCommissionRate.Width = 120
        repoCommissionRate.Minimum = 0
        repoCommissionRate.ShowUpDownButtons = False
        repoCommissionRate.Step = 0
        repoCommissionRate.DecimalPlaces = 4
        repoCommissionRate.IsVisible = True
        'repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCommissionRate)


        gv1.Enabled = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.AllowDeleteRow = True
        gv1.BestFitColumns()
        gv1.Rows.AddNew()
    End Sub
    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_Code", "Varchar(30) Not null Primary key")
        coll.Add("Document_Date", "date not null")
        coll.Add("From_Date", "date not null")
        coll.Add("To_Date", "date NULL")
        coll.Add("Is_Mobile_User", "Integer NOT NULL")
        coll.Add("Inactive", "Integer NOT NULL")
        coll.Add("InActive_Date", "datetime null")
        coll.Add("Commision_UOM", "varchar(12) NOT NULL")
        coll.Add("Min_Per_Day_Qty", "decimal(18,2) NOT NULL")
        coll.Add("Remark", "varchar(200) NULL")
        coll.Add("Comment", "varchar(200) NULL")
        coll.Add("Posted", "integer NULL")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "datetime  Not NULL")
        coll.Add("Modified_By", "varchar(12)  Not NULL")
        coll.Add("Modified_Date", "datetime  Not NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BOOTH_COMMISSION_MASTER", coll, "", True, False, "", "Document_No", "Document_Date", True)
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_Code", "Varchar(30) Not null references TSPL_BOOTH_COMMISSION_MASTER(Document_Code)")
        coll.Add("Line_No", "integer Not Null")
        coll.Add("Item_Code", "Varchar(50) NOT NULL references TSPL_ITEM_MASTER(Item_Code)")
        coll.Add("Commission_Rate", "decimal(18,4) not null")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BOOTH_COMMISSION_DETAIL", coll, "", True, False, "TSPL_BOOTH_COMMISSION_MASTER", "Document_No", "", True)
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) Then
                        gv1.CurrentRow.Cells(colICode).Value = clsItemMaster.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), False)
                        gv1.CurrentRow.Cells(colIName).Value = clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" & gv1.CurrentRow.Cells(colICode).Value & "' ")
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colSNo).Value = ii
        Next
    End Sub
    Sub SetGridFocus()
        If gv1.CurrentCell IsNot Nothing Then
            If gv1.CurrentCell.ColumnInfo.Name = colICode Then
                gv1.CurrentColumn = gv1.Columns(colCRate)
            ElseIf gv1.CurrentCell.ColumnInfo.Name = colCRate Then
                gv1.CurrentRow = gv1.Rows(gv1.Rows.Count - 1)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
        End If
    End Sub
    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            RefeshSNO()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colSNo).Value = clsCommon.myCDecimal(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Try
            Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
            txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster@CustomerWiseSalesReport", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                Dim whrClas As String = "2=2"
                Dim qry As String = "select Document_Code as Code,Document_Date,From_Date,To_Date,Commision_UOM,Inactive,Is_Mobile_User,Min_Per_Day_Qty,Created_By,Created_Date,Modified_By,Modified_Date,Posted,Posted_Date from TSPL_BOOTH_COMMISSION_MASTER"
                LoadData(clsCommon.myCstr(clsCommon.ShowSelectForm("fndDTHComm", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked, "Document_Date")), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BOOTH_COMMISSION_MASTER where Document_Code='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtDocNo.Value), NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            isInsideLoadData = True
            Dim obj As New clsBoothCommissionMaster()
            obj = clsBoothCommissionMaster.GetData(strCode, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then

                LoadBlankGrid()
                AddNew()
                isNewEntry = False
                chkInActive.Checked = IIf(obj.Inactive = 1, True, False)
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnpost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btndelete.Enabled = False
                    If chkInActive.Checked Then
                        InActiveDoc = True
                        chkInActive.Enabled = False
                    Else
                        chkInActive.Enabled = True
                    End If
                Else
                    btnsave.Enabled = True
                    btnpost.Enabled = True
                    btndelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtFromDate.Value = obj.From_Date
                If obj.To_Date.HasValue Then
                    txtToDate.Checked = True
                    txtToDate.Value = obj.To_Date
                End If
                txtUOM.Value = obj.Commision_UOM
                txtperDayQty.Text = obj.Min_Per_Day_Qty
                txtRemark.Text = obj.Remark
                txtComment.Text = obj.Comment
                chkMobileUser.Checked = IIf(obj.Is_Mobile_User = 1, True, False)


                Dim sl As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsBoothCommissionDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSNo).Value = objTr.Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Name
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCRate).Value = objTr.Commission_Rate
                        gv1.Rows.AddNew()
                    Next
                    'GV1.Rows.AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try

    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Function GetTRData() As List(Of clsBoothCommissionDetail)
        Dim Arr As New List(Of clsBoothCommissionDetail)
        Try
            For ii As Integer = 0 To gv1.RowCount - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colICode).Value) > 0 Then
                    Dim objTr As New clsBoothCommissionDetail()
                    objTr.Line_No = clsCommon.myCdbl(gv1.Rows(ii).Cells(colSNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
                    objTr.Commission_Rate = clsCommon.myCDecimal(gv1.Rows(ii).Cells(colCRate).Value)
                    Arr.Add(objTr)
                End If
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try


        Return Arr
    End Function
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtUOM.Value) <= 0 Then
                Throw New Exception("Please Select Commission UOM")
            End If
            If clsCommon.myCdbl(txtperDayQty.Text) <= 0 Then
                Throw New Exception("Please Enter Min Per Day Qty")
            End If

            For dblrow As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(dblrow).Cells(colICode).Value)) > 0 Then
                    Dim strICode As String = clsCommon.myCstr(gv1.Rows(dblrow).Cells(colICode).Value)
                    For dblinnerRow As Integer = dblrow + 1 To gv1.Rows.Count - 1
                        If clsCommon.myLen(clsCommon.myCstr(gv1.Rows(dblinnerRow).Cells(colICode).Value)) > 0 Then
                            Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(dblinnerRow).Cells(colICode).Value)
                            If clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal Then
                                Throw New Exception("Duplicate item found at row no [" & clsCommon.myCstr(dblinnerRow + 1) & "]")
                            End If
                        End If

                    Next
                End If

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsBoothCommissionMaster()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.From_Date = txtFromDate.Value
                If txtToDate.Checked Then
                    obj.To_Date = txtToDate.Value
                End If

                obj.Commision_UOM = txtUOM.Value
                obj.Min_Per_Day_Qty = txtperDayQty.Text
                obj.Is_Mobile_User = IIf(chkMobileUser.Checked, 1, 0)
                obj.Inactive = IIf(chkInActive.Checked, 1, 0)
                obj.Remark = txtRemark.Text
                obj.Comment = txtComment.Text
                obj.Arr = GetTRData()
                obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                LoadData(obj.Document_Code, NavigatorType.Current)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()

    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("You Cannot Delete Record")
                Exit Sub
            End If
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If clsBoothCommissionMaster.DeleteData(txtDocNo.Value) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnpost_Click(sender As Object, e As EventArgs) Handles btnpost.Click
        PostData(txtDocNo.Value)
    End Sub
    Private Sub PostData(ByVal strCode As String)
        Try
            If clsCommon.myLen(strCode) <= 0 Then
                Throw New Exception("No document found to post")
            End If
            If clsCommon.MyMessageBoxShow(Me, "Post the Current Document [" + strCode + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                clsBoothCommissionMaster.PostData(clsCommon.myCstr(strCode))
                clsCommon.MyMessageBoxShow(Me, "Data posted successfully", Me.Text)
                LoadData(clsCommon.myCstr(strCode), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmBoothCommissionMaster_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.KeyCode = Keys.Enter Then
            SetGridFocus()
        End If
    End Sub

    Private Sub chkInActive_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkInActive.ToggleStateChanged
        Try
            If chkInActive.Enabled AndAlso chkInActive.Checked AndAlso Not InActiveDoc Then
                If clsCommon.MyMessageBoxShow(Me, "Inactive the current Document [" + txtDocNo.Value + "]" + Environment.NewLine + "Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    Dim strQry As String = "Update tspl_Booth_Commission_Master set Inactive=1,InActive_Date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy hh:mm tt") & "' where Document_Code='" & txtDocNo.Value & "'"
                    clsDBFuncationality.ExecuteNonQuery(strQry)
                    InActiveDoc = True
                    clsCommon.MyMessageBoxShow(Me, "Inactive Successfully")
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
End Class