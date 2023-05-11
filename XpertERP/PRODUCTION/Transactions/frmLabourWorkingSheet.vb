'-Created by--[Pankaj kumar Chaudhary]-Against Ticket No-[BM00000001755]
Imports common
Imports System.Data.SqlClient

Public Class FrmLabourWorkingSheet
    Inherits FrmMainTranScreen

#Region "Variables"
    Private qry As String = ""
    Private dt As DataTable
    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colLineNo As String = "COLLNO"
    Const colMachineNo As String = "COLMachineNO"
    Const colProcess As String = "COLProcess"
    Const colICode As String = "COLICODE"
    Const colCapacity As String = "COLCapacity"
    Const colRunTime As String = "COLRunTime"
    Const colQty As String = "COLQTY"
    Const colIn1Minute As String = "InOneMInute"
    Const colInRunTime As String = "MFGDATE"
    Const colComment As String = "Comment"
    
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public strDocumentNo As String = ""
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.adjust)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmLabourWorkingSheet_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        SetLength()
        LoadBlankGrid()
        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        ElseIf clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        Else
            AddNew()
        End If
    End Sub

    Sub SetLength()
        txtDocNo.MyMaxLength = 30
    End Sub

    Private Sub txtOperator__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtOperator._MYValidating
        qry = "Select EMP_CODE as Code, Emp_Name as Name from TSPL_EMPLOYEE_MASTER"
        txtOperator.Value = clsCommon.ShowSelectForm("FinderOper", qry, "Code", "", txtOperator.Value, "Code", True)
        lblOperatorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select Emp_Name from TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + txtOperator.Value + "'"))
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "S No."
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoMachineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMachineNo.FormatString = ""
        repoMachineNo.HeaderText = "Machine No"
        repoMachineNo.Name = colMachineNo
        repoMachineNo.Width = 100
        gv1.MasterTemplate.Columns.Add(repoMachineNo)

        Dim repoProcess As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoProcess.FormatString = ""
        repoProcess.HeaderText = "Process"
        repoProcess.Name = colProcess
        repoProcess.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoProcess.TextImageRelation = TextImageRelation.TextBeforeImage
        repoProcess.Width = 100
        gv1.MasterTemplate.Columns.Add(repoProcess)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoCapacity As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCapacity.FormatString = ""
        repoCapacity.HeaderText = "Capacity"
        repoCapacity.Name = colCapacity
        repoCapacity.Width = 100
        repoCapacity.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCapacity)

        Dim repoRunTIme As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRunTIme.FormatString = ""
        repoRunTIme.HeaderText = "Run Time"
        repoRunTIme.Name = colRunTime
        repoRunTIme.Width = 100
        gv1.MasterTemplate.Columns.Add(repoRunTIme)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Quantity"
        repoQty.Name = colQty
        repoQty.Width = 100
        gv1.MasterTemplate.Columns.Add(repoQty)

        Dim repoIn1Min As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoIn1Min.FormatString = ""
        repoIn1Min.HeaderText = "In One Min."
        repoIn1Min.Name = colIn1Minute
        repoIn1Min.Width = 100
        repoIn1Min.Minimum = 0
        repoIn1Min.ReadOnly = True
        repoIn1Min.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoIn1Min)

        Dim repoInRunTIme As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoInRunTIme.FormatString = ""
        repoInRunTIme.HeaderText = "In Run Time"
        repoInRunTIme.Name = colInRunTime
        repoInRunTIme.Width = 100
        repoInRunTIme.Minimum = 0
        repoInRunTIme.ReadOnly = True
        repoInRunTIme.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInRunTIme)


        Dim repoComment As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoComment.FormatString = ""
        repoComment.HeaderText = "Comment"
        repoComment.Name = colComment
        repoComment.Width = 250
        gv1.MasterTemplate.Columns.Add(repoComment)


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

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        'btnPost.Enabled = True
        btnDelete.Enabled = True
        txtDate.Focus()
        gv1.Rows.AddNew()
    End Sub

    Sub BlankAllControls()
        txtOperator.Value = ""
        lblOperatorName.Text = ""
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        'UsLock1.Status = ERPTransactionStatus.Pending
        txtOperator.Value = ""
        lblOperatorName.Text = ""
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colProcess) Then
                        OpenProcess(False)
                    ElseIf e.Column Is gv1.Columns(colICode) Then
                        OpenItem(False)
                        gv1.CurrentRow.Cells(colCapacity).Value = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select Capacity from TSPL_PROCESS_DETAIL WHERE Process_Code='" + gv1.CurrentRow.Cells(colProcess).Value + "' AND Item_Code='" + gv1.CurrentRow.Cells(colICode).Value + "'"))
                        gv1.CurrentRow.Cells(colIn1Minute).Value = clsCommon.myCdbl(clsCommon.myCdbl(gv1.CurrentRow.Cells(colCapacity).Value) / 60)
                    ElseIf e.Column Is gv1.Columns(colRunTime) Then
                        gv1.CurrentRow.Cells(colInRunTime).Value = clsCommon.myCdbl(gv1.CurrentRow.Cells(colRunTime).Value) * clsCommon.myCdbl(gv1.CurrentRow.Cells(colIn1Minute).Value)
                    End If
                    lblEfficiency.Text = clsCommon.myCstr(GetEfficiency())
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
            isCellValueChangedOpen = False
        End Try
    End Sub

    Sub OpenProcess(ByVal isButtonClick As Boolean)
        qry = "Select Distinct Process_Code as Code, Process_Desc as Description from TSPL_PROCESS_MASTER"
        gv1.CurrentRow.Cells(colProcess).Value = clsCommon.ShowSelectForm("ProcessFinderLabour", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(colProcess).Value), "Code", isButtonClick)
    End Sub

    Sub OpenItem(ByVal isButtonClick As Boolean)
        Dim Process As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colProcess).Value)
        qry = "Select Item_Code as Code, Capacity as Capacity from TSPL_PROCESS_DETAIL"
        Dim WhrCls As String = "Process_Code='" + Process + "'"
        Dim arr As List(Of String) = GetItem(Process)
        If arr.Count > 0 Then
            WhrCls += " AND Item_Code NOT IN (" + clsCommon.GetMulcallString(GetItem(clsCommon.myCstr(gv1.CurrentRow.Cells(colProcess).Value))) + ")"
        End If
        gv1.CurrentRow.Cells(colICode).Value = clsCommon.ShowSelectForm("ItemFinderLabour", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), "Code", isButtonClick)
    End Sub

    Private Function GetItem(ByVal strProcessCode As String) As List(Of String)
        Dim arr As New List(Of String)
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.CompairString(grow.Cells(colProcess).Value, strProcessCode) = CompairStringResult.Equal Then
                arr.Add(clsCommon.myCstr(grow.Cells(colICode).Value))
            End If
        Next
        Return arr
    End Function


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


    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean)
        Dim qry As String = ""
        Dim whrclas As String = ""
        qry = "select Location_Code ,Location_Desc from TSPL_LOCATION_MASTER "
        txtOperator.Value = clsCommon.ShowSelectForm("AdjStoreLocation", qry, "Location_Code", whrclas, txtOperator.Value, "", isButtonClicked)
        lblOperatorName.Text = clsDBFuncationality.getSingleValue("select Location_Desc   from TSPL_LOCATION_MASTER where Location_Code='" + txtOperator.Value + "' ")
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        '===================Added by preeti Gupta==============
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        '===========================================================
        If clsCommon.myLen(txtOperator.Value) <= 0 Then
            txtOperator.Focus()
            Throw New Exception("Please select Operator")
        End If

        Dim Count As Integer = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If clsCommon.myLen(grow.Cells(colProcess).Value) > 0 Then
                If clsCommon.myLen(grow.Cells(colICode).Value) <= 0 Then
                    Throw New Exception("Please select item in row no " + clsCommon.myCstr(grow.Index + 1) + "")
                End If
                If clsCommon.myLen(grow.Cells(colRunTime).Value) <= 0 Then
                    Throw New Exception("Please enter 'RUN Time' in row no " + clsCommon.myCstr(grow.Index + 1) + "")
                End If
            End If
            Count += 1
        Next
        If Count <= 0 Then
            Throw New Exception("Please enter atleast single item.")
        End If
        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then
                Dim arr As New List(Of clsLabourWorkingSheet)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colProcess).Value) > 0 Then
                        Dim obj As New clsLabourWorkingSheet()
                        obj.Document_No = clsCommon.myCstr(txtDocNo.Value)
                        obj.Document_Date = clsCommon.myCstr(txtDate.Value)
                        obj.Employee = clsCommon.myCstr(txtOperator.Value)
                        obj.Machine_No = clsCommon.myCstr(grow.Cells(colMachineNo).Value)
                        obj.Process_Code = clsCommon.myCstr(grow.Cells(colProcess).Value)
                        obj.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                        obj.Capacity = clsCommon.myCdbl(grow.Cells(colCapacity).Value)
                        obj.Run_Time = clsCommon.myCdbl(grow.Cells(colRunTime).Value)
                        obj.Quantity = clsCommon.myCdbl(grow.Cells(colQty).Value)
                        obj.In_One_Minute = clsCommon.myCdbl(grow.Cells(colIn1Minute).Value)
                        obj.In_Run_Time = clsCommon.myCdbl(grow.Cells(colInRunTime).Value)
                        obj.Comment = clsCommon.myCstr(grow.Cells(colComment).Value)
                        arr.Add(obj)
                    End If
                Next
                clsLabourWorkingSheet.SaveData(arr, isNewEntry)
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                strDocumentNo = arr.Item(0).Document_No
                LoadData(strDocumentNo, NavigatorType.Current)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            isInsideLoadData = True
            isNewEntry = False
            BlankAllControls()
            LoadBlankGrid()
            Dim arr As New List(Of clsLabourWorkingSheet)
            arr = clsLabourWorkingSheet.GetData(strCode, NavTyep, Nothing)
            Dim Count As Integer = 0
            For Each obj As clsLabourWorkingSheet In arr
                If Count = 0 Then
                    txtDocNo.Value = obj.Document_No
                    txtDate.Value = obj.Document_Date
                    txtOperator.Value = obj.Employee
                End If
                gv1.Rows.AddNew()
                gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCstr(Count + 1)
                gv1.CurrentRow.Cells(colMachineNo).Value = clsCommon.myCstr(obj.Machine_No)
                gv1.CurrentRow.Cells(colProcess).Value = clsCommon.myCstr(obj.Process_Code)
                gv1.CurrentRow.Cells(colICode).Value = clsCommon.myCstr(obj.Item_Code)
                gv1.CurrentRow.Cells(colCapacity).Value = clsCommon.myCdbl(obj.Capacity)
                gv1.CurrentRow.Cells(colRunTime).Value = clsCommon.myCdbl(obj.Run_Time)
                gv1.CurrentRow.Cells(colQty).Value = clsCommon.myCdbl(obj.Quantity)
                gv1.CurrentRow.Cells(colIn1Minute).Value = clsCommon.myCdbl(obj.In_One_Minute)
                gv1.CurrentRow.Cells(colInRunTime).Value = clsCommon.myCdbl(obj.In_Run_Time)
                gv1.CurrentRow.Cells(colComment).Value = clsCommon.myCstr(obj.Comment)
                Count += 1
            Next
            gv1.Rows.AddNew()
            lblEfficiency.Text = clsCommon.myCstr(GetEfficiency())
            btnSave.Text = "Update"
            isNewEntry = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
                If (clsLabourWorkingSheet.DeleteData(txtDocNo.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub FrmLabourWorkingSheet_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F2 Then
            isCellValueChangedOpen = True
            If gv1.CurrentColumn Is gv1.Columns(colICode) Then
                gv1.CurrentColumn = gv1.Columns(colCapacity)
                OpenProcess(True)
                gv1.CurrentColumn = gv1.Columns(colICode)
            End If
            isCellValueChangedOpen = False
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            CloseForm()
        End If
    End Sub

    Private Sub txtDocNo__MYNavigator1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_LABOUR_WORKING_SHEET where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    
    Private Sub txtDocNo__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "SELECT Distinct Document_No AS [Document], Document_Date as [Date], Employee as [Operator] FROM  TSPL_LABOUR_WORKING_SHEET  "
        Dim whrClas As String = " 1=1"

        txtDocNo.Value = clsCommon.ShowSelectForm("LWSDocFinder", qry, "Document", whrClas, txtDocNo.Value, "Document", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
    End Sub

    Private Sub btnAddNew_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Function GetEfficiency() As Double
        Dim TotalQty As Double = 0
        Dim TotalInRunTime As Double
        Dim TotalRunTIme As Double = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            TotalQty += clsCommon.myCdbl(grow.Cells(colQty).Value)
            TotalRunTIme += clsCommon.myCdbl(grow.Cells(colRunTime).Value)
            TotalInRunTime += clsCommon.myCdbl(grow.Cells(colInRunTime).Value)
        Next
        lblRunTime.Text = clsCommon.myCstr(TotalRunTIme)
        lblQty.Text = clsCommon.myCstr(TotalQty)
        lblInRunTime.Text = clsCommon.myCstr(TotalInRunTime)
        Return (TotalQty * 100) / TotalInRunTime
    End Function
End Class
