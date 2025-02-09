Imports System.Data.SqlClient
Imports common
Public Class FrmBoothRouteMapping
#Region "Variable"
    Dim isNewEntry As Boolean = False
    Dim EnableProductSaleForJPR As Boolean = False
    Const ColSNo As String = "ColSNo"
    Const colBoothCode As String = "colBoothCode"
    Const colBoothName As String = "colBoothName"
    Const colPrevRoute As String = "colPrevRoute"
    Const colRouteDesc As String = "colRouteDesc"
    Private isCellValueChangedOpen As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private selectedRow As GridViewRowInfo = Nothing
#End Region
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBookingProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnReverse.Visible = False
        If MyBase.isExport = True Then
            btnImport.Enabled = True
            btnExport.Enabled = True
        Else
            btnImport.Enabled = False
            btnExport.Enabled = False
        End If
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        Else
            btnReverse.Enabled = False
        End If
        If btnSave.Visible = True Then
            btnImport.Enabled = True
        Else
            btnImport.Enabled = False
        End If
    End Sub
    Private Sub FrmBoothRouteMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        UcAttachment1.Form_ID = MyBase.Form_ID
        ' UcAttachment1.isDeleteTheAttachment = False
        'UcAttachment1.settAutoAttachment = True
        UcAttachment1.MandatoryPDFFileAny = False
        EnableProductSaleForJPR = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.EnableProductSaleForJPR, clsFixedParameterCode.EnableProductSaleForJPR, Nothing)) = 1, True, False)
        AddNew()
        CreateTable()
        Create_Default_Master()
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub
    Public Sub AddNew()
        UcAttachment1.BlankAllControls()
        isNewEntry = True
        txtDocNo.Value = ""
        UsLock1.Status = ERPTransactionStatus.Pending
        txtSupplyDate.Value = clsCommon.GETSERVERDATE()
        txtRouteNo.Value = ""
        cmbShiftType.Text = "Morning"
        cmbItemType.Text = "Milk"
        txtRemark.Text = ""
        btnSave.Enabled = True
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnShowHistory.Enabled = False
        If EnableProductSaleForJPR Then
            lblItemType.Visible = True
            cmbItemType.Visible = True
        Else
            lblItemType.Visible = False
            cmbItemType.Visible = False
        End If
        LoadBlankGrid()
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoSno As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSno = New GridViewDecimalColumn()
        repoSno.FormatString = ""
        repoSno.HeaderText = "SLNo"
        repoSno.Name = ColSNo
        repoSno.Width = 40
        repoSno.ReadOnly = True
        repoSno.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoSno)
        Dim repoBoothCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBoothCode.FormatString = ""
        repoBoothCode.HeaderText = "Booth Code"
        repoBoothCode.Name = colBoothCode
        repoBoothCode.Width = 180
        repoBoothCode.HeaderImage = My.Resources.search4
        repoBoothCode.IsVisible = True
        repoBoothCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoBoothCode)
        Dim repoBoothName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBoothName.FormatString = ""
        repoBoothName.HeaderText = "Booth Name"
        repoBoothName.Name = colBoothName
        repoBoothName.Width = 200
        repoBoothName.IsVisible = True
        repoBoothName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBoothName)
        Dim repoCurrentRoute As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCurrentRoute.FormatString = ""
        repoCurrentRoute.HeaderText = "Prev Route"
        repoCurrentRoute.Name = colPrevRoute
        repoCurrentRoute.Width = 120
        repoCurrentRoute.IsVisible = True
        repoCurrentRoute.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCurrentRoute)
        Dim repoNewRoute As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNewRoute.FormatString = ""
        repoNewRoute.HeaderText = "Route Name"
        repoNewRoute.Name = colRouteDesc
        repoNewRoute.Width = 120
        repoNewRoute.IsVisible = True
        repoNewRoute.ReadOnly = True
        'repoTextBox.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoNewRoute)
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.AllowRowReorder = True
        gv1.ShowGroupPanel = False
        gv1.EnableFiltering = False
        gv1.EnableSorting = False
        gv1.EnableGrouping = False
        gv1.AllowColumnChooser = True
        gv1.AllowColumnReorder = False
        gv1.Rows.AddNew()
    End Sub
    Private Sub CreateTable()
        Dim coll As Dictionary(Of String, String)
        coll = New Dictionary(Of String, String)()
        coll.Add("Document_No", "Varchar(30) Not null Primary key")
        coll.Add("Supply_Date", "date not null")
        coll.Add("Shift_Type", "varchar(12)  Not NULL")
        coll.Add("Route_No", "varchar(12) NOT NULL REFERENCES TSPL_ROUTE_MASTER (Route_No)")
        coll.Add("Item_Type", "varchar(12) NOT NULL")
        coll.Add("Remark", "varchar(200) NULL")
        coll.Add("Posted", "integer NULL")
        coll.Add("Created_By", "varchar(12)  Not NULL")
        coll.Add("Created_Date", "datetime  Not NULL")
        coll.Add("Modified_By", "varchar(12)  Not NULL")
        coll.Add("Modified_Date", "datetime  Not NULL")
        coll.Add("Posted_By", "varchar(12) NULL")
        coll.Add("Posted_Date", "datetime NULL")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BOOTH_ROUTE_MAPPING_HEAD", coll, "", True, False, "", "Document_No", "Supply_Date")
        coll = New Dictionary(Of String, String)()
        coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
        coll.Add("Document_No", "Varchar(30) Not null references TSPL_BOOTH_ROUTE_MAPPING_HEAD(Document_No)")
        coll.Add("Serial_No", "integer Not Null")
        coll.Add("Booth_Code", "Varchar(12) NOT NULL references TSPL_CUSTOMER_MASTER(Cust_Code)")
        coll.Add("Prev_Route_No", "varchar(12) NOT NULL REFERENCES TSPL_ROUTE_MASTER (Route_No)")
        clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_BOOTH_ROUTE_MAPPING_DETAIL", coll, "", True, False, "TSPL_BOOTH_ROUTE_MAPPING_HEAD", "Document_No", "")
    End Sub
    Private Sub FrmBoothRouteMapping_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If gv1.CurrentColumn Is gv1.Columns(colBoothCode) AndAlso e.KeyCode = Keys.Enter Then
            gv1.CurrentColumn = gv1.Columns(colBoothCode)
            setGridFocus()
        End If
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub
    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            If txtDocNo.MyReadOnly OrElse isButtonClicked Then
                Dim whrClas As String = "2=2"
                Dim qry As String = "select Document_No as Code,Supply_Date,Shift_Type,Route_No,Item_Type,Remark,Created_By,Created_Date,Modified_By,Modified_Date,Posted,Posted_Date from TSPL_BOOTH_ROUTE_MAPPING_HEAD"
                LoadData(clsCommon.myCstr(clsCommon.ShowSelectForm("ICLBDMST", qry, "Code", whrClas, txtDocNo.Value, "Code", isButtonClicked, "Supply_Date")), NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_BOOTH_ROUTE_MAPPING_HEAD where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCDecimal(clsDBFuncationality.getSingleValue(qry))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(clsCommon.myCstr(txtDocNo.Value), NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtRouteNo.Value) <= 0 Then
                Throw New Exception("Plese Select Route.")
                Return False
            End If
            For i As Integer = 0 To gv1.Rows.Count - 1
                If (clsCommon.CompairString(gv1.Rows(i).Cells(colBoothCode).Value, Nothing) = CompairStringResult.Equal) Then
                    Continue For
                End If
                Dim BoothCode As String = clsCommon.myCstr(gv1.Rows(i).Cells(colBoothCode).Value)
                For j As Integer = 0 To gv1.Rows.Count - 1
                    Dim strInnerBCode As String = clsCommon.myCstr(gv1.Rows(j).Cells(colBoothCode).Value)
                    If j = i Then
                        Continue For
                    End If
                    If clsCommon.CompairString(BoothCode, strInnerBCode) = CompairStringResult.Equal Then
                        Throw New Exception("Same Booth Exist at Row No " + clsCommon.myCstr(i + 1) + " And " + clsCommon.myCstr(j + 1))
                    End If
                Next
            Next
            UcAttachment1.AllowToSave()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            If (AllowToSave()) Then
                SaveData()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SaveData()
        Try
            Dim obj As New clsBoothRouteMappingHead()
            obj.Document_No = txtDocNo.Value
            obj.Supply_Date = txtSupplyDate.Value
            obj.Shift_Type = cmbShiftType.Text
            obj.Item_Type = cmbItemType.Text
            obj.Remark = txtRemark.Text
            obj.Route_No = txtRouteNo.Value
            obj.Arr = GetTRData()
            obj.SaveData(obj, isNewEntry)
            UcAttachment1.SaveData(obj.Document_No)
            clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
            LoadData(obj.Document_No, NavigatorType.Current)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Function GetTRData() As List(Of clsBoothRouteMappingDetail)
        Dim Arr As New List(Of clsBoothRouteMappingDetail)
        For ii As Integer = 0 To gv1.RowCount - 1
            If clsCommon.myLen(gv1.Rows(ii).Cells(colBoothCode).Value) > 0 Then
                Dim objTr As New clsBoothRouteMappingDetail()
                objTr.Serial_No = ii + 1
                objTr.Booth_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colBoothCode).Value)
                objTr.Prev_Route_No = clsCommon.myCstr(gv1.Rows(ii).Cells(colPrevRoute).Value)
                Arr.Add(objTr)
            End If
        Next
        Return Arr
    End Function
    Private Sub setGridFocus()
        Dim intCurrRow As Integer = gv1.CurrentRow.Index
        gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
        If intCurrRow = gv1.Rows.Count - 1 Then
            gv1.Rows.AddNew()
            gv1.CurrentRow = gv1.Rows(intCurrRow)
        End If
        If clsCommon.myLen(gv1.CurrentRow.Cells(colBoothCode).Value) > 0 Then
            If gv1.CurrentColumn Is gv1.Columns(colBoothCode) Then
                gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                gv1.CurrentColumn = gv1.Columns(colBoothCode)
            End If
        End If
    End Sub
    Public Sub LoadData(ByVal strDocNo As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            Dim obj As New clsBoothRouteMappingHead()
            obj = clsBoothRouteMappingHead.GetData(strDocNo, NavTyep, Nothing)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                isNewEntry = False
                LoadBlankGrid()
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                    btnDelete.Enabled = False
                Else
                    btnSave.Enabled = True
                    btnPost.Enabled = True
                    btnDelete.Enabled = True
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                txtDocNo.Value = obj.Document_No
                txtSupplyDate.Value = obj.Supply_Date
                txtRouteNo.Value = obj.Route_No
                lblRouteDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & obj.Route_No & "' "))
                cmbShiftType.Text = obj.Shift_Type
                cmbItemType.Text = obj.Item_Type
                txtRemark.Text = obj.Remark
                Dim sl As Integer = 1
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clsBoothRouteMappingDetail In obj.Arr
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.Serial_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBoothCode).Value = objTr.Booth_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBoothName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(objTr.Booth_Code) + "' "))
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colPrevRoute).Value = objTr.Prev_Route_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" + clsCommon.myCstr(objTr.Prev_Route_No) + "' "))
                        sl += 1
                        gv1.Rows.AddNew()
                    Next
                End If
                UcAttachment1.LoadData(obj.Document_No)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) >= 0 Then
                DeleteData()
            Else
                Throw New Exception("Document not found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub DeleteData()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsBoothRouteMappingHead.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    AddNew()
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) >= 0 Then
                PostData()
            Else
                Throw New Exception("Document not found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub PostData()
        Try
            If (myMessages.postConfirm()) Then
                If (clsBoothRouteMappingHead.PostData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully ", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnShowHistory_Click(sender As Object, e As EventArgs) Handles btnShowHistory.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Document", Me.Text)
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(txtDocNo.Value, "Document_No", "TSPL_BOOTH_ROUTE_MAPPING_HEAD")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) >= 0 Then
                ReverseData()
            Else
                Throw New Exception("Document not found!")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub ReverseData()
        Try
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub
    Sub CloseForm()
        Me.Close()
    End Sub
    Private Sub SplitContainer2_Panel1_Paint(sender As Object, e As PaintEventArgs) Handles SplitContainer2.Panel1.Paint
    End Sub
    Private Sub btnCC_Click(sender As Object, e As EventArgs) Handles btnCC.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                txtDocNo.Value = ""
                txtSupplyDate.Value = clsCommon.GETSERVERDATE()
                isNewEntry = True
                btnSave.Enabled = True
                UsLock1.Status = ERPTransactionStatus.Pending

                clsCommon.MyMessageBoxShow(Me, "Document Copied Successfully", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub gv1_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCDecimal(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub
    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colBoothCode) Then
                        OpenBoothList(False)
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenBoothList(ByVal isButtonClick As Boolean)
        Dim whrCls As String = " TSPL_CUSTOMER_MASTER.Status='N' "
        gv1.CurrentRow.Cells(colBoothCode).Value = clsCustomerMaster.getFinder(whrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colBoothCode).Value), False)
        gv1.CurrentRow.Cells(colBoothName).Value = clsCustomerMasterNew.GetName(clsCommon.myCstr(gv1.CurrentRow.Cells(colBoothCode).Value), Nothing)
        gv1.CurrentRow.Cells(colPrevRoute).Value = clsDBFuncationality.getSingleValue("select Route_No from TSPL_CUSTOMER_MASTER where Cust_Code='" & gv1.CurrentRow.Cells(colBoothCode).Value & "' ")
        gv1.CurrentRow.Cells(colRouteDesc).Value = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & gv1.CurrentRow.Cells(colPrevRoute).Value & "' ")
    End Sub
    Private Sub txtRouteNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtRouteNo._MYValidating
        Try
            Dim qry As String = "select Route_No as Code,Route_Desc,City_Code,Status from TSPL_ROUTE_MASTER "
            txtRouteNo.Value = clsCommon.ShowSelectForm("fndRootMaster@BoothRouteMapping", qry, "Code", "Status='A'", txtRouteNo.Value, "Code", isButtonClicked)
            lblRouteDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & txtRouteNo.Value & "' "))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Private Sub gv1_RowsChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles gv1.RowsChanged
    '    Try
    '        If gv1.Rows.Count > 0 Then
    '            Dim i As Integer = 1
    '            For Each grow As GridViewRowInfo In gv1.Rows
    '                grow.Cells(ColSNo).Value = i
    '                i += 1
    '            Next
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Private Sub UpdateSlNo()
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(i).Cells(ColSNo).Value = i + 1 ' Update sequence based on row index
        Next
    End Sub
    Private Sub btnGImport_Click(sender As Object, e As EventArgs) Handles btnGImport.Click
        GridImport()
    End Sub
    Public Sub GridImport()
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim obj As New List(Of clsBoothRouteMappingDetail)
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Booth Code", "Route No") Then
                Dim linno As Integer = 0
                Dim TempNewRecord As Boolean = False
                Try
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim Arr As New clsBoothRouteMappingDetail()
                        linno += 1
                        If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Booth Code").Value))) Then
                            Continue For
                        Else
                            Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_Customer_Master where Cust_Code='" + clsCommon.myCstr(grow.Cells("Booth Code").Value) + "'"))
                            If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Booth Code").Value)) = CompairStringResult.Equal Then
                                Arr.Booth_Code = clsCommon.myCstr(grow.Cells("Booth Code").Value)
                            Else
                                Continue For
                            End If
                        End If
                        If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Route No").Value))) Then
                            Continue For
                        Else
                            Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from TSPL_Route_Master where Route_No='" + clsCommon.myCstr(grow.Cells("Route No").Value) + "'"))
                            If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Route No").Value)) = CompairStringResult.Equal Then
                                Arr.Prev_Route_No = clsCommon.myCstr(grow.Cells("Route No").Value)
                            Else
                                Continue For
                            End If
                        End If
                        obj.Add(Arr)
                    Next
                    clsCommon.ProgressBarHide()
                    If clsCommon.MyMessageBoxShow(Me, "Total Correct Document [" + clsCommon.myCstr(obj.Count) + "] out of [" + clsCommon.myCstr(linno) + "] Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                        Dim sl As Integer = 1
                        'AddNew()
                        LoadBlankGrid()
                        If obj IsNot Nothing AndAlso obj.Count > 0 Then
                            isInsideLoadData = True
                            For Each objTr As clsBoothRouteMappingDetail In obj
                                gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = sl
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBoothCode).Value = objTr.Booth_Code
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colBoothName).Value = clsCustomerMasterNew.GetName(objTr.Booth_Code, Nothing)
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colPrevRoute).Value = objTr.Prev_Route_No
                                gv1.Rows(gv1.Rows.Count - 1).Cells(colRouteDesc).Value = clsDBFuncationality.getSingleValue("select Route_Desc from TSPL_ROUTE_MASTER where Route_No='" & objTr.Prev_Route_No & "' ")
                                sl += 1
                                gv1.Rows.AddNew()
                            Next
                            isInsideLoadData = False
                        End If
                        common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Else
                        common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Failed", Me.Text, MessageBoxButtons.OK)
                    End If
                    clsCommon.ProgressBarHide()
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            Else
                clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format", Me.Text)
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGExport_Click(sender As Object, e As EventArgs) Handles btnGExport.Click
        GridExport()
    End Sub
    Private Sub GridExport()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                Dim str As String = "select Booth_Code as [Booth Code],Prev_Route_No as [Route No] from TSPL_BOOTH_ROUTE_MAPPING_DETAIL "
                Dim whrCls As String = " and Document_No='" + txtDocNo.Value + "' "
                ListImpExpColumnsMandatory = New List(Of String)({"Booth Code", "Route No"})
                transportSql.ExporttoExcel(str, whrCls, Me)
            Else
                Throw New Exception("Please Select Document")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnBImport_Click(sender As Object, e As EventArgs) Handles btnBImport.Click
        BulkImport()
    End Sub
    Private Sub BulkImport()
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim obj As New List(Of clsBoothRouteMappingHead)
            Dim lststr As New List(Of String)
            Dim currentdate As Date = Date.Today
            If transportSql.importExcel(gv, "Route NO", "Supply Date", "Shift Type", "Booth Code") Then
                Dim linno As Integer = 0
                Dim TempNewRecord As Boolean = False
                Dim dtError As New DataTable
                dtError.Columns.Add("RowNo", GetType(Integer))
                dtError.Columns.Add("Error", GetType(String))
                Try
                    clsCommon.ProgressBarShow()
                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim Arr As New clsBoothRouteMappingHead()
                        linno += 1
                        clsCommon.ProgressBarPercentUpdate(linno, gv.Rows.Count, "Validating Data...")
                        Try
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Route No").Value))) Then
                                Throw New Exception(" Empty Route No at Line No.[" + clsCommon.myCstr(linno) + "]")
                            Else
                                Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from TSPL_Route_Master where Route_No='" + clsCommon.myCstr(grow.Cells("Route No").Value) + "'"))
                                If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Route No").Value)) = CompairStringResult.Equal Then
                                    Arr.Route_No = clsCommon.myCstr(grow.Cells("Route No").Value)
                                Else
                                    Throw New Exception("Invalid Route No:[" + clsCommon.myCstr(grow.Cells("Booth Code").Value) + "] at Line No.[" + clsCommon.myCstr(linno) + "]")
                                End If
                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Supply Date").Value))) Then
                                Throw New Exception(" Empty Supply Date at Line No.[" + clsCommon.myCstr(linno) + "]")
                            Else
                                Arr.Supply_Date = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("Supply Date").Value), "dd/MMM,yyyy")
                            End If
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Shift Type").Value))) Then
                                Throw New Exception(" Empty Shift Type at Line No.[" + clsCommon.myCstr(linno) + "]")
                            Else
                                If clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Shift Type").Value), "Morning") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(grow.Cells("Shift Type").Value), "Evening") = CompairStringResult.Equal Then
                                    Arr.Shift_Type = clsCommon.myCstr(grow.Cells("Shift Type").Value)
                                Else
                                    Throw New Exception(" Invalid Shift Type at Line No.[" + clsCommon.myCstr(linno) + "], Shift Type should be Morning or Evening ")

                                End If
                            End If
                            'If EnableProductSaleForJPR Then
                            'End If
                            Arr.Item_Type = "Milk"
                            Arr.Remark = "Bulk Import by User " + objCommonVar.CurrentUserCode
                            Arr.Arr = New List(Of clsBoothRouteMappingDetail)
                            Dim objtr As New clsBoothRouteMappingDetail
                            If (String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells("Booth Code").Value))) Then
                                Throw New Exception(" Empty Booth Code at Line No.[" + clsCommon.myCstr(linno) + "]")
                            Else
                                Dim str As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_Customer_Master where Cust_Code='" + clsCommon.myCstr(grow.Cells("Booth Code").Value) + "'"))
                                If clsCommon.CompairString(str, clsCommon.myCstr(grow.Cells("Booth Code").Value)) = CompairStringResult.Equal Then
                                    objtr.Booth_Code = clsCommon.myCstr(grow.Cells("Booth Code").Value)
                                    Dim prev_Route As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 TSPL_Booth_Route_Mapping_Head.Route_No from TSPL_Booth_Route_Mapping_Head
left join TSPL_Booth_Route_Mapping_Detail on TSPL_Booth_Route_Mapping_Detail.Document_No=TSPL_Booth_Route_Mapping_Head.Document_No
where TSPL_Booth_Route_Mapping_Detail.Booth_Code='" + objtr.Booth_Code + "'
order by TSPL_Booth_Route_Mapping_Head.Document_No desc"))
                                    If (String.IsNullOrEmpty(prev_Route)) Then
                                        Dim strR As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from TSPL_Customer_Master where Cust_Code='" + clsCommon.myCstr(grow.Cells("Booth Code").Value) + "'"))
                                        objtr.Prev_Route_No = strR
                                    Else
                                        objtr.Prev_Route_No = prev_Route
                                    End If
                                    Arr.Arr.Add(objtr)
                                Else
                                    Throw New Exception(" Invalid Booth Code:[" + clsCommon.myCstr(grow.Cells("Booth Code").Value) + "] at Line No.[" + clsCommon.myCstr(linno) + "]")
                                End If
                            End If
                            If Not lststr.Contains(clsCommon.myCstr(grow.Cells("Booth Code").Value)) Then
                                lststr.Add(clsCommon.myCstr(grow.Cells("Booth Code").Value))
                            Else
                                Throw New Exception("Duplicate Booth Code:[" + clsCommon.myCstr(grow.Cells("Booth Code").Value) + "] at Line No.[" + clsCommon.myCstr(linno) + "]")
                            End If
                            obj.Add(Arr)
                        Catch ex As Exception
                            Dim dr As DataRow = dtError.NewRow()
                            dr("RowNo") = linno
                            dr("Error") = ex.Message
                            dtError.Rows.Add(dr)
                        End Try
                    Next
                    clsCommon.ProgressBarHide()
                    If clsCommon.MyMessageBoxShow(Me, "Total Correct Document [" + clsCommon.myCstr(obj.Count) + "] out of [" + clsCommon.myCstr(linno) + "] Are You Sure.", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then

                        Dim lstBRMobj As New List(Of clsBoothRouteMappingHead)

                        Dim groupedRoutes = From route In obj
                                            Group route By key = New With {
                                Key route.Route_No,
                                Key route.Supply_Date,
                                Key route.Shift_Type
                            } Into Group
                                            Select New With {
                                .Route_No = key.Route_No,
                                .Supply_Date = key.Supply_Date,
                                .Shift_Type = key.Shift_Type,
                                .Routes = Group.ToList()
                            }
                        For Each group In groupedRoutes
                            Dim BRMObj As New clsBoothRouteMappingHead
                            BRMObj.Route_No = group.Route_No
                            BRMObj.Supply_Date = clsCommon.GetPrintDate(group.Supply_Date, "dd/MMM/yyyy")
                            BRMObj.Shift_Type = group.Shift_Type
                            BRMObj.Item_Type = "Milk"
                            BRMObj.Remark = "Bulk Import by User [" + objCommonVar.CurrentUserCode + "]"
                            BRMObj.Arr = New List(Of clsBoothRouteMappingDetail)
                            Dim slno As Integer = 1
                            For Each route In group.Routes
                                For Each objarr In route.Arr
                                    Dim objTr As New clsBoothRouteMappingDetail
                                    objTr.Serial_No = slno
                                    objTr.Booth_Code = objarr.Booth_Code
                                    objTr.Prev_Route_No = objarr.Prev_Route_No
                                    BRMObj.Arr.Add(objTr)
                                    slno += 1
                                Next
                            Next
                            lstBRMobj.Add(BRMObj)
                        Next
                        clsBoothRouteMappingHead.SaveBulkImport(lstBRMobj, False)

                        common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                    Else
                        If dtError.Rows.Count > 0 Then
                            Dim ff As New FrmFreeGrid
                            ff.ReportID = "Bulk_Import_Booth_Route_Mapping"
                            ff.Text = "Booth_Route_Mapping Errors"
                            ff.dt = dtError
                            ff.ShowDialog()
                        End If
                        common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Failed", Me.Text, MessageBoxButtons.OK)
                    End If
                    clsCommon.ProgressBarHide()
                Catch ex As Exception
                    clsCommon.ProgressBarHide()
                    clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
                End Try
            Else
                clsCommon.MyMessageBoxShow(Me, "Excel Sheet is not in expected format", Me.Text)
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub Create_Default_Master()
        Try
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_BOOTH_ROUTE_MAPPING_HEAD")) = 0 Then
                Dim obj As New List(Of clsBoothRouteMappingHead)
                Dim strMainQry = "select Route_No as [Route No],'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE, "dd/MMM/yyyy") + "' as [Supply Date],'Evening' as [Shift Type],Cust_Code as [Booth Code]  from TSPL_CUSTOMER_MASTER where Status='N' and IsDistributor='N' order by [Route No]"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(strMainQry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For Each dr As DataRow In dt.Rows
                        Dim Arr As New clsBoothRouteMappingHead()
                        Arr.Route_No = clsCommon.myCstr(dr("Route No"))
                        Arr.Supply_Date = clsCommon.myCDate(dr("Supply Date"))
                        Arr.Shift_Type = clsCommon.myCstr(dr("Shift Type"))
                        Arr.Item_Type = "Milk"
                        Arr.Remark = "Bulk Import by User " + objCommonVar.CurrentUserCode
                        Arr.Arr = New List(Of clsBoothRouteMappingDetail)
                        Dim objtr As New clsBoothRouteMappingDetail
                        objtr.Booth_Code = clsCommon.myCstr(dr("Booth Code"))
                        objtr.Prev_Route_No = clsCommon.myCstr(dr("Route No"))
                        Arr.Arr.Add(objtr)
                        obj.Add(Arr)
                    Next
                End If
                Dim lstBRMobj As New List(Of clsBoothRouteMappingHead)
                Dim groupedRoutes = From route In obj
                                    Group route By key = New With {
                                    Key route.Route_No,
                                    Key route.Supply_Date,
                                    Key route.Shift_Type
                                } Into Group
                                    Select New With {
                                    .Route_No = key.Route_No,
                                    .Supply_Date = key.Supply_Date,
                                    .Shift_Type = key.Shift_Type,
                                    .Routes = Group.ToList()
                                }
                For Each group In groupedRoutes
                    Dim BRMObj As New clsBoothRouteMappingHead
                    BRMObj.Route_No = group.Route_No
                    BRMObj.Supply_Date = clsCommon.GetPrintDate(group.Supply_Date, "dd/MMM/yyyy")
                    BRMObj.Shift_Type = group.Shift_Type
                    BRMObj.Item_Type = "Milk"
                    BRMObj.Remark = "Created by default"
                    BRMObj.Arr = New List(Of clsBoothRouteMappingDetail)
                    Dim slno As Integer = 1
                    For Each route In group.Routes
                        For Each objarr In route.Arr
                            Dim objTr As New clsBoothRouteMappingDetail
                            objTr.Serial_No = slno
                            objTr.Booth_Code = objarr.Booth_Code
                            objTr.Prev_Route_No = objarr.Prev_Route_No
                            BRMObj.Arr.Add(objTr)
                            slno += 1
                        Next
                    Next
                    lstBRMobj.Add(BRMObj)
                Next
                clsBoothRouteMappingHead.SaveBulkImport(lstBRMobj, True)
                'Else
                '    Throw New Exception("Booth Route Mapping master must be empty")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadBExport_Click(sender As Object, e As EventArgs) Handles RadBExport.Click
        BulkExport()
    End Sub
    Private Sub BulkExport()
        Try
            Dim str As String = "select Route_No as [Route No],Supply_Date as [Supply Date],Shift_Type as[Shift Type],Booth_Code as [Booth Code] from TSPL_Booth_Route_Mapping_Head
left join TSPL_Booth_Route_Mapping_Detail on TSPL_Booth_Route_Mapping_Detail.Document_No=TSPL_Booth_Route_Mapping_Head.Document_No"
            Dim whrCls As String = " "
            ListImpExpColumnsMandatory = New List(Of String)({"Route No", "Supply Date", "Shift Type", "Booth Code"})
            transportSql.ExporttoExcel(str, whrCls, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


End Class