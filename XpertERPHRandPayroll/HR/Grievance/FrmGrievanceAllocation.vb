Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports Telerik.WinControls.Data
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine
Public Class FrmGrievanceAllocation
    Inherits FrmMainTranScreen


    Dim sQuery As String = ""

    Const colSno As String = "colSno"
    Const colGrievanceLogging_Code As String = "colGrievanceLogging_Code"
    Const colDescription As String = "colDescription"
    Const colLogging_Date As String = "colLogging_Date"
    Const colGrievanceTypeCode As String = "colGrievanceTypeCode"
    Const colGrievanceTypeName As String = "colGrievanceTypeName"
    Const colAppliedBy_Code As String = "colAppliedBy_Code"
    Const colAppliedBy_Name As String = "colAppliedBy_Name"

    Const colFrmDept_Code As String = "colFrmDept_Code"
    Const colFrmDept_Name As String = "colFrmDept_Name"

    Const colForDept_Code As String = "colForDept_Code"
    Const colForDept_Name As String = "colForDept_Name"

    Const colAllocated_To As String = "colAllocated_To"
    Const colAllocated_To_Name As String = "colAllocated_To_Name"
    Const colAllocated_Date As String = "colAllocated_Date"

    Const colRemarks As String = "colRemarks"

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False


    Sub AddNew()
        ' isNewEntry = True
        btnsave.Text = "Save"
        'DtpDocDate.MinDate = Nothing
        'DtpFromDate.MinDate = Nothing
        'DtpTodate.MinDate = Nothing
        gv1.DataSource = Nothing
        BtnSelect.Text = "Select"
        btnsave.Enabled = True
        BtnPost.Enabled = True
        btnDelete.Enabled = True


        gv1.Rows.Clear()
        isCellValueChangedOpen = False
        'gv1.Columns.Clear()

        '  Me.dtpDocDate.Value = clsCommon.GETSERVERDATE()
        ' Me.cboShift.SelectedIndex = -1
        UcAttachment1.BlankAllControls()
    End Sub

    Public Sub LoadData(ByVal strDoc As String, ByVal strRoute_Code As String, ByVal trans As SqlTransaction, ByVal navType As NavigatorType)
        Try
            'AddNew()
            BtnSave.Text = "Save"
            BtnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            LoadBlankGrid()
            Dim objList As List(Of ClsGrievanceAllocation)
            
            objList = ClsGrievanceAllocation.GetData(trans)

            ' If clsCommon.myLen(obj.DOC_DATE) > 0 Then
            'DtpDocDate.Value = obj.DOC_DATE
            'End If
            'fndMccCode.Value = obj.MCC_CODE
            '  cboShift.SelectedValue = obj.SHIFT
            gv1.Rows.Clear()
            For Each obj As ClsGrievanceAllocation In objList
                If Not IsNothing(obj) Then

                    'If obj.POSTED = ERPTransactionStatus.Approved Then
                    '    BtnSave.Enabled = False
                    '    btnPost.Enabled = False
                    '    btnDelete.Enabled = False
                    'End If
                    'End If
                    'LoadBlankGrid()
                    isCellValueChangedOpen = True
                    'For Each obj1 As ClsGrievanceAllocation In ClsGrievanceAllocation.ObjList
                    gv1.Rows.AddNew()

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGrievanceLogging_Code).Value = obj.Greivance_Logging_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDescription).Value = obj.Description
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLogging_Date).Value = obj.Logging_Date
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGrievanceTypeCode).Value = obj.Grievance_Type_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colGrievanceTypeName).Value = obj.Grievance_Type_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAppliedBy_Code).Value = obj.Applied_By_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAppliedBy_Name).Value = obj.Applied_By_Name
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFrmDept_Code).Value = obj.Frm_Dpt_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colFrmDept_Name).Value = obj.Frm_Dpt_Name

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colForDept_Code).Value = obj.For_Dpt_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colForDept_Name).Value = obj.For_Dpt_Name

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = obj.Remarks
                    If clsCommon.myLen(obj.Allocated_to) <= 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAllocated_Date).Value = Nothing
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAllocated_To).Value = obj.Allocated_to
                    Else
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAllocated_Date).Value = obj.Allocated_Date
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAllocated_To).Value = obj.Allocated_to
                    End If
                    
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colAllocated_To_Name).Value = obj.Allocated_to_Name

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = obj.Remarks
                    If clsCommon.myLen(obj.DOC_CODE) > 0 Then
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSno).Value = True
                    End If
                    isCellValueChangedOpen = False
                    'Next
                    UcAttachment1.LoadData(obj.DOC_CODE)
                End If
            Next

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmGrievanceAllocation_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso BtnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            AddNew()
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.MilkTruckSheet)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        BtnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmGrievanceAllocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        AddNew()
        LoadData("", "", Nothing, NavigatorType.Current)
    End Sub
    Public Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim repoSNO As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSNO.FormatString = ""
        repoSNO.HeaderText = "Select"
        repoSNO.Name = colSno
        repoSNO.Width = 50
        repoSNO.IsVisible = True
        repoSNO.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoSNO)

        Dim repoGriev_Logging_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGriev_Logging_Code.FormatString = ""
        repoGriev_Logging_Code.HeaderText = "Grievance Logging Code"
        repoGriev_Logging_Code.Name = colGrievanceLogging_Code
        ' repoGriev_Logging_Code.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoGriev_Logging_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoGriev_Logging_Code.Width = 150
        repoGriev_Logging_Code.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGriev_Logging_Code)


        Dim repoDescription As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDescription.FormatString = ""
        repoDescription.HeaderText = "Description"
        repoDescription.Name = colDescription
        'repoDescription.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoDescription.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDescription.Width = 200
        repoDescription.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDescription)


        Dim repoLogging_Date As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLogging_Date.FormatString = ""
        repoLogging_Date.HeaderText = "Logging Date"
        repoLogging_Date.Name = colLogging_Date
        'repoDescription.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoDescription.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLogging_Date.Width = 150
        repoLogging_Date.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLogging_Date)



        Dim repoGrievanceTypeCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGrievanceTypeCode.FormatString = ""
        repoGrievanceTypeCode.HeaderText = "Grievance Type Code"
        repoGrievanceTypeCode.Name = colGrievanceTypeCode
        ' repoGrievanceTypeCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoGrievanceTypeCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoGrievanceTypeCode.Width = 150
        repoGrievanceTypeCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGrievanceTypeCode)


        Dim repoGrievanceTypeName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoGrievanceTypeName.FormatString = ""
        repoGrievanceTypeName.HeaderText = "Grievance Type Name"
        repoGrievanceTypeName.Name = colGrievanceTypeName
        'repoGrievanceTypeName.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoGrievanceTypeName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoGrievanceTypeName.Width = 200
        repoGrievanceTypeName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoGrievanceTypeName)

        Dim repoAppliedByCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAppliedByCode.FormatString = ""
        repoAppliedByCode.HeaderText = "Applied By Code"
        repoAppliedByCode.Name = colAppliedBy_Code
        ' repoAppliedByCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoAppliedByCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAppliedByCode.Width = 100
        repoAppliedByCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAppliedByCode)


        Dim repoAppliedByName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAppliedByName.FormatString = ""
        repoAppliedByName.HeaderText = "Applied By Name"
        repoAppliedByName.Name = colAppliedBy_Name
        'repoAppliedByName.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoAppliedByName.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAppliedByName.Width = 200
        repoAppliedByName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAppliedByName)


        Dim repoFrm_Dept_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFrm_Dept_Code.FormatString = ""
        repoFrm_Dept_Code.HeaderText = "From Department Code"
        repoFrm_Dept_Code.Name = colFrmDept_Code
        'repoFrm_Dept_Code.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoFrm_Dept_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFrm_Dept_Code.Width = 100
        repoFrm_Dept_Code.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFrm_Dept_Code)


        Dim repoFrm_Dept_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFrm_Dept_Name.FormatString = ""
        repoFrm_Dept_Name.HeaderText = "From Department Name"
        repoFrm_Dept_Name.Name = colFrmDept_Name
        'repoFrm_Dept_Name.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoFrm_Dept_Name.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFrm_Dept_Name.Width = 200
        repoFrm_Dept_Name.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFrm_Dept_Name)


        Dim repoFor_Dept_Code As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFor_Dept_Code.FormatString = ""
        repoFor_Dept_Code.HeaderText = "For Department Code"
        repoFor_Dept_Code.Name = colForDept_Code
        'repoFor_Dept_Code.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoFor_Dept_Code.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFor_Dept_Code.Width = 100
        repoFor_Dept_Code.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFor_Dept_Code)


        Dim repoFor_Dept_Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFor_Dept_Name.FormatString = ""
        repoFor_Dept_Name.HeaderText = "For Department Name"
        repoFor_Dept_Name.Name = colForDept_Name
        'repoFor_Dept_Name.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        'repoFor_Dept_Name.TextImageRelation = TextImageRelation.TextBeforeImage
        repoFor_Dept_Name.Width = 200
        repoFor_Dept_Name.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFor_Dept_Name)


        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 200
        repoRemarks.IsVisible = True
        repoRemarks.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoRemarks)


        Dim repoAllocateTo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAllocateTo.FormatString = ""
        repoAllocateTo.HeaderText = "Allocate To"
        repoAllocateTo.Name = colAllocated_To
        repoAllocateTo.TextImageRelation = TextImageRelation.TextBeforeImage
        repoAllocateTo.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        repoAllocateTo.Width = 150
        repoAllocateTo.IsVisible = True
        repoAllocateTo.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoAllocateTo)

        Dim repoAllocateToName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoAllocateToName.FormatString = ""
        repoAllocateToName.HeaderText = "Allocate To Name"
        repoAllocateToName.Name = colAllocated_To_Name
        repoAllocateToName.Width = 150
        repoAllocateToName.IsVisible = True
        repoAllocateToName.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoAllocateToName)


        Dim repoAllocateDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repoAllocateDate.FormatString = ""
        repoAllocateDate.HeaderText = "Allocate Date"
        repoAllocateDate.Name = colAllocated_Date
        repoAllocateDate.Width = 100
        repoAllocateDate.IsVisible = True
        repoAllocateDate.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAllocateDate)


        gv1.AllowDeleteRow = True

        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.AllowAddNewRow = False
        gv1.EnableSorting = True
        gv1.EnableFiltering = True
        gv1.EnableAlternatingRowColor = True
        gv1.AutoSizeRows = False
        gv1.AllowRowResize = True
        gv1.VerticalScrollState = ScrollState.AlwaysShow
        gv1.HorizontalScrollState = ScrollState.AlwaysShow
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.ShowFilteringRow = True
        ReStoreGridLayout()
    End Sub

    Private Sub fndMccCode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)

    End Sub


    Sub SaveData()
        Dim trans As SqlTransaction = Nothing
        Try
            If (AllowToSave()) Then
                trans = clsDBFuncationality.GetTransactin()
                Dim objList As New List(Of ClsGrievanceAllocation)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myCdbl(grow.Cells(colSno).Value) = 1 Then
                        Dim objHead As ClsGrievanceAllocation
                        objHead = New ClsGrievanceAllocation
                        objHead.Greivance_Logging_Code = clsCommon.myCstr(grow.Cells(colGrievanceLogging_Code).Value)
                        objHead.Allocated_to = clsCommon.myCstr(grow.Cells(colAllocated_To).Value)
                        objHead.Allocated_Date = clsCommon.myCDate(grow.Cells(colAllocated_Date).Value)
                        objHead.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)


                        objList.Add(objHead)

                    End If

                Next
                If ClsGrievanceAllocation.SaveData(objList, trans) Then
                    trans.Commit()
                    UcAttachment1.SaveData(objList(0).DOC_CODE)
                    clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(objList(0).DOC_CODE, "", Nothing, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            

            'If gv1.Rows.Count <= 1 Then
            '    clsCommon.MyMessageBoxShow("Please Check Atleast One row to save.")
            '    Return False
            'End If



            Dim grid_vlc_Count As Integer = 0
            For Each row As GridViewRowInfo In gv1.Rows
                If row.Cells(colSno).Value = True Then
                    grid_vlc_Count += 1
                End If
            Next
            If grid_vlc_Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please Check Atleast One row to save.", Me.Text)
                Return False
            End If
            UcCustomFields1.AllowToSave()
            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        Return True
    End Function

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        AddNew()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        SaveData()
    End Sub

  
    Sub DeleteData()
        'Try
        '    Dim Reason As String = ""
        '    If (myMessages.deleteConfirm()) Then
        '        If clsCancelLog.CheckForReasonOnDelete() Then
        '            '' REASON FOR DELETE 
        '            Dim frm As New FrmFreeTxtBox1
        '            frm.Text = "Remarks for Delete"
        '            frm.ShowDialog()
        '            If clsCommon.myLen(frm.strRmks) <= 0 Then
        '                Exit Sub
        '            Else
        '                Reason = frm.strRmks
        '            End If
        '        End If

        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
        Try
            For Each row As GridViewRowInfo In gv1.Rows
                Dim sQuery As String = "delete from TSPL_Grievance_Logging_Allocation where grievance_logging_code='" & clsCommon.myCstr(row.Cells(colGrievanceLogging_Code).Value) & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery)
            Next
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
        clsCommon.MyMessageBoxShow(Me, "Deleted Successfully.", Me.Text)
    End Sub

    
    Private Sub BtnsaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnsaveLayout.Click
        gv1.MasterTemplate.FilterDescriptors.Clear()
        Dim obj As New clsGridLayout()
        obj.ReportID = MyBase.Form_ID
        obj.UserID = objCommonVar.CurrentUserCode
        obj.GridLayout = New MemoryStream()
        gv1.SaveLayout(obj.GridLayout)
        obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
        obj.GridColumns = gv1.ColumnCount
        If obj.SaveData() Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
        End If

        ''richa agarwal regarding memory leakage
        obj.GridLayout.Close()
        obj.GridLayout.Dispose()
        ''---------------
    End Sub

    Private Sub BtnDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            'If clsCommon.myLen("LoadinMainGrid") > 0 Then
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
            'End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click
        Try
            If BtnSelect.Text = "Unselect" Then
                For Each row As GridViewRowInfo In gv1.Rows
                    row.Cells(colSno).Value = False
                    BtnSelect.Text = "Select"
                Next
            Else
                For Each row As GridViewRowInfo In gv1.Rows
                    row.Cells(colSno).Value = True
                    BtnSelect.Text = "Unselect"
                Next
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isCellValueChangedOpen = False Then
                isCellValueChangedOpen = True
                If e.Column Is gv1.Columns(colAllocated_To) Then
                    GetdEmployee_code()
                    'If clsCommon.myLen(gv1.CurrentRow.Cells(colAllocated_To).Value) > 0 Then
                    '    gv1.CurrentRow.Cells(colAllocated_Date).Value = clsCommon.GETSERVERDATE()
                    'Else
                    '    gv1.CurrentRow.Cells(colAllocated_Date).Value = Nothing
                    'End If
                End If
                gv1.CurrentRow.Cells(colAllocated_Date).Value = clsCommon.GETSERVERDATE()
                isCellValueChangedOpen = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub

    Private Sub GetdEmployee_code()
        Dim obj As clsEmployeeMaster = clsEmployeeMaster.FinderForEmployee("", True)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.EMP_CODE) > 0 Then
            gv1.CurrentRow.Cells(colAllocated_To).Value = obj.EMP_CODE
            gv1.CurrentRow.Cells(colAllocated_To_Name).Value = obj.Emp_Name
        Else
            gv1.CurrentRow.Cells(colAllocated_To).Value = ""
            gv1.CurrentRow.Cells(colAllocated_To_Name).Value = ""
        End If
    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        Try
            For Each row As GridViewRowInfo In gv1.Rows
                Dim sQuery As String = "Update TSPL_Grievance_Logging_Allocation set posted=1,posting_date='" & clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd-MMM-yyyy hh:mm:ss tt") & "' where grievance_logging_code='" & clsCommon.myCstr(row.Cells(colGrievanceLogging_Code).Value) & "'"
                clsDBFuncationality.ExecuteNonQuery(sQuery)
            Next
            clsCommon.MyMessageBoxShow(Me, "Posted Successfully.", Me.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.ToString, Me.Text)
        End Try
    End Sub
End Class
