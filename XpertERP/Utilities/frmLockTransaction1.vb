Imports common
Imports System.Data.SqlClient
Public Class FrmLockTransaction1
    Inherits FrmMainTranScreen

    Public Const colLocationCode As String = "colLocationCode"
    Public Const colLocationName As String = "colLocationName"
    Public Const colModuleCode As String = "colModuleCode"
    Public Const colModuleName As String = "colModuleName"
    Public Const colTransactionCode As String = "colTransactionCode"
    Public Const colTransactionName As String = "colTransactionName"
    Public Const colLock As String = "colLock"
    Public Const colFromDate As String = "colFromDate"
    Public Const colToDate As String = "colToDate"

    Dim AllowLockTransactionUserwise As Integer = 0

    Private Sub FrmLockTransaction1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AllowLockTransactionUserwise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowLockTransactionUserwise, clsFixedParameterCode.AllowLockTransactionUserwise, Nothing))
        If AllowLockTransactionUserwise = 1 Then
            btnLockUser.Visible = True
        End If
        chkLocationCode.IsChecked = True
        SetUserMgmtNew()
        dtpToDate1.Text = clsCommon.GETSERVERDATE
    End Sub
    Private Sub SetUserMgmtNew()
        '' Anubhooti 31-July-2014 BM00000003131
        'MyBase.SetUserMgmt(clsUserMgtCode.lockTransaction)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        btnLock.Visible = MyBase.isPostFlag
    End Sub
    Private Sub chkLocationCode_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationCode.ToggleStateChanged
        BlankControls()
    End Sub
    Private Sub chkLocationSegment_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocationSegment.ToggleStateChanged
        BlankControls()
    End Sub
    Sub BlankControls()
        Try
            txtLocationMult.arrValueMember = Nothing
            LoadData()
        Catch ex As Exception
        End Try
    End Sub
    Sub LoadBlankGrid()
        dgvDetails.DataSource = Nothing
        dgvDetails.Columns.Clear()
        Dim Loc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Loc = New GridViewTextBoxColumn()
        Loc.HeaderText = "Location Code"
        Loc.Name = colLocationCode
        Loc.Width = 100
        Loc.ReadOnly = True
        Loc.IsVisible = False
        dgvDetails.MasterTemplate.Columns.Add(Loc)

        Loc = New GridViewTextBoxColumn()
        Loc.HeaderText = "Location"
        Loc.Name = colLocationName
        Loc.Width = 200
        Loc.ReadOnly = True
        dgvDetails.MasterTemplate.Columns.Add(Loc)

        Dim moduleC As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        moduleC = New GridViewTextBoxColumn()
        moduleC.FormatString = ""
        moduleC.HeaderText = "Module Code"
        moduleC.Name = colModuleCode
        moduleC.Width = 100
        moduleC.ReadOnly = True
        moduleC.IsVisible = False
        dgvDetails.MasterTemplate.Columns.Add(moduleC)

        Dim ModuleCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ModuleCode.HeaderText = "Module"
        ModuleCode.Name = colModuleName
        ModuleCode.Width = 200
        ModuleCode.ReadOnly = True
        ModuleCode.IsVisible = True
        dgvDetails.MasterTemplate.Columns.Add(ModuleCode)

        Dim ProgramCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ProgramCode = New GridViewTextBoxColumn()
        ProgramCode.FormatString = ""
        ProgramCode.HeaderText = "Transaction Code"
        ProgramCode.Name = colTransactionCode
        ProgramCode.Width = 100
        ProgramCode.ReadOnly = True
        ProgramCode.IsVisible = False
        dgvDetails.MasterTemplate.Columns.Add(ProgramCode)


        Dim Transaction As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Transaction.FormatString = ""
        Transaction.HeaderText = "Transaction"
        Transaction.Name = colTransactionName
        Transaction.Width = 200
        Transaction.ReadOnly = True
        'Transaction.IsVisible = False
        dgvDetails.MasterTemplate.Columns.Add(Transaction)

        Dim Lock As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        Lock.FormatString = ""
        Lock.HeaderText = "Lock"
        Lock.Name = colLock
        Lock.Width = 100
        Lock.ReadOnly = False
        dgvDetails.MasterTemplate.Columns.Add(Lock)

        Dim startDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        startDate = New GridViewDateTimeColumn()
        startDate.CustomFormat = "dd/MM/yyyy"
        startDate.FormatString = "{0:dd/MM/yyyy}"
        startDate.HeaderText = "From"
        startDate.Name = colFromDate
        startDate.Width = 151
        startDate.ReadOnly = True
        startDate.IsVisible = False
        dgvDetails.MasterTemplate.Columns.Add(startDate)

        Dim endDate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        endDate = New GridViewDateTimeColumn()
        endDate.CustomFormat = "dd/MM/yyyy"
        endDate.FormatString = "{0:dd/MM/yyyy}"
        endDate.HeaderText = "To"
        endDate.Name = colToDate
        endDate.Width = 151
        'endDate.ReadOnly = True
        dgvDetails.MasterTemplate.Columns.Add(endDate)

        dgvDetails.AllowDeleteRow = False
        dgvDetails.AllowAddNewRow = False
        dgvDetails.ShowGroupPanel = False
        dgvDetails.AllowColumnReorder = False
        dgvDetails.AllowRowReorder = False
        dgvDetails.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        dgvDetails.MasterTemplate.ShowRowHeaderColumn = False
        dgvDetails.ShowFilteringRow = True
        dgvDetails.EnableFiltering = True
    End Sub
    Private Sub dgvDetails_CellEndEdit(sender As Object, e As GridViewCellEventArgs) Handles dgvDetails.CellEndEdit
        If e.Column.Name = "colToDate" Then
            Dim cellValue As String = dgvDetails.Rows(e.RowIndex).Cells(e.Column.Index).Value.ToString()
            Dim enteredDate As Date
            If Date.TryParse(cellValue, enteredDate) Then
                If enteredDate > Date.Now Then
                    MessageBox.Show("The date cannot be in the future. Reverting to today's date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    dgvDetails.Rows(e.RowIndex).Cells(e.Column.Index).Value = Date.Now.Date ' Set to current date
                End If
            End If
        End If
    End Sub
    Public Sub LoadData()
        Try
            Dim qry As String = "select TabLoc.Code,TabLoc.Name, Module.Program_Code as ModuleCode,case when Module.Re_Name is null then Module.Program_Name else Module.Re_Name end as ModuleName,TSPL_PROGRAM_MASTER.Program_Code as TransactionCode,case when TSPL_PROGRAM_MASTER.Re_Name is null then TSPL_PROGRAM_MASTER.Program_Name else TSPL_PROGRAM_MASTER.Re_Name end as TransactionName"
            If chkLocationSegment.IsChecked Then
                qry += ",cast(TSPL_LOCK_LOCATION_SEGMENT.Is_Locked as bit) as Is_Locked,TSPL_LOCK_LOCATION_SEGMENT.Start_Date,TSPL_LOCK_LOCATION_SEGMENT.End_Date "
            Else
                qry += ",cast(TSPL_LOCK_LOCATION.Is_Locked as bit) as Is_Locked,TSPL_LOCK_LOCATION.Start_Date,TSPL_LOCK_LOCATION.End_Date "
            End If
            qry += " from TSPL_PROGRAM_MASTER 
left outer join  TSPL_PROGRAM_MASTER as SubModule on SubModule.Program_Code=TSPL_PROGRAM_MASTER.Parent_Code
left outer join  TSPL_PROGRAM_MASTER as Module on Module.Program_Code=SubModule.Parent_Code " + Environment.NewLine + ""
            If chkLocationSegment.IsChecked Then
                qry += "Left outer join ("
                If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                    qry += "Select Segment_code as Code, Description as Name  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
                Else
                    qry += "select '' as Code,'' as Name"
                End If
                qry += " ) TabLoc on 2=2 "
                qry += "
left outer join TSPL_LOCK_LOCATION_SEGMENT on TSPL_LOCK_LOCATION_SEGMENT.Trans_Name=TSPL_PROGRAM_MASTER.Program_Code and TSPL_LOCK_LOCATION_SEGMENT.Location_Segment_Code=TabLoc.Code
where  TSPL_PROGRAM_MASTER.Lock_Location_Segment=1 "
            Else
                qry += "Left outer join ("
                If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                    qry += "Select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical' and Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
                Else
                    qry += "select '' as Code,'' as Name"
                End If
                qry += " ) TabLoc on 2=2  
left outer join TSPL_LOCK_LOCATION on TSPL_LOCK_LOCATION.Trans_Name=TSPL_PROGRAM_MASTER.Program_Code and TSPL_LOCK_LOCATION.Location_Code=TabLoc.Code
where TSPL_PROGRAM_MASTER.Lock_Location=1"
            End If
            qry += " order by  TabLoc.Code,Module.SNo,TSPL_PROGRAM_MASTER.SNo "
            LoadBlankGrid()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            dgvDetails.AutoGenerateColumns = False
            dgvDetails.DataSource = dt
            dgvDetails.Columns(colLocationCode).FieldName = "Code"
            dgvDetails.Columns(colLocationName).FieldName = "Name"
            dgvDetails.Columns(colModuleCode).FieldName = "ModuleCode"
            dgvDetails.Columns(colModuleName).FieldName = "ModuleName"
            dgvDetails.Columns(colTransactionCode).FieldName = "TransactionCode"
            dgvDetails.Columns(colTransactionName).FieldName = "TransactionName"
            dgvDetails.Columns(colLock).FieldName = "Is_Locked"
            dgvDetails.Columns(colFromDate).FieldName = "Start_Date"
            dgvDetails.Columns(colToDate).FieldName = "End_Date"
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dgvDetails.CurrentRow = dgvDetails.Rows(0)
                If AllowLockTransactionUserwise Then
                    For Each dr As DataRow In dt.Rows
                        Dim strSql As String = ""
                        If chkLocationSegment.IsChecked = True Then
                            strSql = "select TSPL_LOCK_LOCATION_SEGMENT_USER.user_code,User_Name,Todate 
from TSPL_LOCK_LOCATION_SEGMENT_USER 
left outer join tspL_USER_MASTER on TSPL_LOCK_LOCATION_SEGMENT_USER.User_Code=tspL_USER_MASTER.User_Code 
where Location_Segment_Code  ='" & clsCommon.myCstr(dr("Code")) & "'   and Module_Name='" & clsCommon.myCstr(dr("ModuleCode")) & "' 
and Trans_Name='" & clsCommon.myCstr(dr("TransactionCode").Value) & "'"
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strSql)
                            Dim Arr As List(Of clsLockTransactionLocationSegmentUserwise) = Nothing
                            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                Arr = New List(Of clsLockTransactionLocationSegmentUserwise)
                                For Each dr1 As DataRow In dt1.Rows
                                    Dim obj As clsLockTransactionLocationSegmentUserwise = New clsLockTransactionLocationSegmentUserwise()
                                    obj.Status = 1
                                    obj.User_Code = dr1("user_code")
                                    obj.User_Name = dr1("User_Name")
                                    obj.ToDate = dr1("Todate")
                                    Arr.Add(obj)
                                Next
                            End If
                            dgvDetails.CurrentRow.Cells(colTransactionCode).Tag = Arr
                        Else
                            strSql = "select TSPL_LOCK_LOCATION_USER.user_code,User_Name,Todate 
from TSPL_LOCK_LOCATION_USER 
left outer join  tspL_USER_MASTER on TSPL_LOCK_LOCATION_USER.User_Code=tspL_USER_MASTER.User_Code 
where Location_Code  ='" & clsCommon.myCstr(dr("Code")) & "' and Module_Name='" & clsCommon.myCstr(dr("ModuleCode").Value) & "' 
and Trans_Name='" & clsCommon.myCstr(dr("TransactionCode")) & "'"
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(strSql)
                            Dim Arr As List(Of clsLockTransactionLocationUserwise) = Nothing
                            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                Arr = New List(Of clsLockTransactionLocationUserwise)
                                For Each dr1 As DataRow In dt1.Rows
                                    Dim obj As clsLockTransactionLocationUserwise = New clsLockTransactionLocationUserwise()
                                    obj.Status = 1
                                    obj.User_Code = dr1("user_code")
                                    obj.User_Name = dr1("User_Name")
                                    obj.ToDate = dr1("Todate")
                                    Arr.Add(obj)
                                Next
                            End If
                            dgvDetails.CurrentRow.Cells(colTransactionCode).Tag = Arr
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub OpenUser()
        If clsCommon.myCBool(dgvDetails.CurrentRow.Cells(colLock).Value) Then
            Dim frm As frmLockLoctionUserwise = New frmLockLoctionUserwise()
            frm.strLocCode = clsCommon.myCstr(dgvDetails.CurrentRow.Cells(colLocationCode).Value)
            frm.strLocname = clsLocation.GetName(clsCommon.myCstr(dgvDetails.CurrentRow.Cells(colLocationCode).Value), Nothing)
            frm.strModule = clsCommon.myCstr(dgvDetails.CurrentRow.Cells(colModuleCode).Value)
            frm.strTransName = clsCommon.myCstr(dgvDetails.CurrentRow.Cells(colTransactionCode).Value)
            If chkLocationSegment.IsChecked = True Then
                frm.blnLocationwsie = False
                frm.arr1 = TryCast(dgvDetails.CurrentRow.Cells(colTransactionCode).Tag, List(Of clsLockTransactionLocationSegmentUserwise))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    dgvDetails.CurrentRow.Cells(colTransactionCode).Tag = frm.arr1
                End If
            Else
                frm.blnLocationwsie = True
                frm.arr = TryCast(dgvDetails.CurrentRow.Cells(colTransactionCode).Tag, List(Of clsLockTransactionLocationUserwise))
                frm.ShowDialog()
                If Not frm.isCencelButtonClicked Then
                    dgvDetails.CurrentRow.Cells(colTransactionCode).Tag = frm.arr
                End If
            End If
        End If
    End Sub
    Private Sub dgvDetails_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles dgvDetails.CellDoubleClick
        Try
            If AllowLockTransactionUserwise = 1 Then
                If e.Column Is dgvDetails.Columns(colTransactionName) Then
                    OpenUser()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnLock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLock.Click
        Dim LIneNo As Integer = 0
        For Each grow As GridViewRowInfo In dgvDetails.Rows
            LIneNo = LIneNo + 1
            If clsCommon.myCBool(grow.Cells(colLock).Value) = True Then
                If clsCommon.myLen(grow.Cells(colFromDate).Value) = 0 AndAlso dtpFromdate1.Text <> "" Then
                    grow.Cells(colFromDate).Value = dtpFromdate1.Text
                End If
                If clsCommon.myLen(grow.Cells(colFromDate).Value) = 0 Then
                    common.clsCommon.MyMessageBoxShow("Select Start Date at Line '" + clsCommon.myCstr(LIneNo) + "'")
                    Exit Sub
                End If
                If clsCommon.myLen(grow.Cells(colToDate).Value) = 0 Then
                    common.clsCommon.MyMessageBoxShow("Select To Date at Line '" + clsCommon.myCstr(LIneNo) + "'")
                    Exit Sub
                End If
                If clsCommon.myLen(grow.Cells(colFromDate).Value) > 0 AndAlso clsCommon.myLen(grow.Cells(colToDate).Value) > 0 Then
                    If grow.Cells(colFromDate).Value > grow.Cells(colToDate).Value Then
                        common.clsCommon.MyMessageBoxShow("Start Date Can Not be Greater Than End Date At Line No " + clsCommon.myCstr(LIneNo) + "")
                        Exit Sub
                    End If
                End If
            End If
        Next

        LockTrans()
    End Sub
    Public Sub LockTrans()
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim qry As String
        Try
            If chkLocationSegment.IsChecked Then
                If txtLocationMult.arrValueMember Is Nothing OrElse txtLocationMult.arrValueMember.Count <= 0 Then
                    qry = "Delete from TSPL_LOCK_LOCATION_SEGMENT_USER  "
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "Delete from TSPL_LOCK_LOCATION_SEGMENT"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                Else
                    qry = "Delete from TSPL_LOCK_LOCATION_SEGMENT_USER Where  Location_Segment_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    qry = "Delete from TSPL_LOCK_LOCATION_SEGMENT Where Location_Segment_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
                    clsDBFuncationality.ExecuteNonQuery(qry, trans)
                End If
                Dim arr As New List(Of clsLockTransactionLocationSegmentwise)
                For Each grow As GridViewRowInfo In dgvDetails.Rows
                    Dim obj As New clsLockTransactionLocationSegmentwise
                    obj.Location_Segment_Code = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                    obj.Module_Name = clsCommon.myCstr(grow.Cells(colModuleCode).Value)
                    obj.Trans_Name = clsCommon.myCstr(grow.Cells(colTransactionCode).Value)
                    obj.Is_Locked = clsCommon.myCBool(grow.Cells(colLock).Value)
                    obj.Start_Date = Nothing
                    If clsCommon.myLen(grow.Cells(colFromDate).Value) > 0 Then
                        obj.Start_Date = clsCommon.myCDate(grow.Cells(colFromDate).Value)
                    End If
                    obj.End_Date = Nothing
                    If clsCommon.myLen(grow.Cells(colToDate).Value) > 0 Then
                        obj.End_Date = clsCommon.myCDate(grow.Cells(colToDate).Value)
                    End If
                    arr.Add(obj)

                    If clsCommon.myCBool(grow.Cells(colLock).Value) Then
                        Dim arr1 As New List(Of clsLockTransactionLocationSegmentUserwise)
                        Dim ArrUser As List(Of clsLockTransactionLocationSegmentUserwise) = Nothing
                        ArrUser = TryCast(grow.Cells(colTransactionCode).Tag, List(Of clsLockTransactionLocationSegmentUserwise))
                        If ArrUser IsNot Nothing Then
                            For Each objtr As clsLockTransactionLocationSegmentUserwise In ArrUser
                                objtr.Location_Segment_Code = obj.Location_Segment_Code
                                objtr.Module_Name = obj.Module_Name
                                objtr.Trans_Name = obj.Trans_Name
                                arr1.Add(objtr)
                            Next
                            clsLockTransactionLocationSegmentUserwise.SaveData("", "", arr1, trans)
                        End If
                    End If
                Next
                clsLockTransactionLocationSegmentwise.SaveData("", "", arr, trans)
            Else
                If txtLocationMult.arrValueMember Is Nothing OrElse txtLocationMult.arrValueMember.Count <= 0 Then
                    Dim strQ = "Delete from TSPL_LOCK_LOCATION "
                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                    strQ = "Delete from TSPL_LOCK_LOCATION_USER "
                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                Else
                    Dim strQ = "Delete from TSPL_LOCK_LOCATION Where  Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                    strQ = "Delete from TSPL_LOCK_LOCATION_USER Where  Location_Code  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
                    clsDBFuncationality.ExecuteNonQuery(strQ, trans)
                End If
                Dim arr As New List(Of clsLockTransactionLocationwise)
                For Each grow As GridViewRowInfo In dgvDetails.Rows
                    Dim obj As New clsLockTransactionLocationwise
                    obj.Location_Code = clsCommon.myCstr(grow.Cells(colLocationCode).Value)
                    obj.Module_Name = clsCommon.myCstr(grow.Cells(colModuleCode).Value)
                    obj.Trans_Name = clsCommon.myCstr(grow.Cells(colTransactionCode).Value)
                    obj.Is_Locked = clsCommon.myCBool(grow.Cells(colLock).Value)
                    obj.Start_Date = Nothing
                    If clsCommon.myLen(grow.Cells(colFromDate).Value) > 0 Then
                        obj.Start_Date = clsCommon.myCDate(grow.Cells(colFromDate).Value)
                    End If
                    obj.End_Date = Nothing
                    If clsCommon.myLen(grow.Cells(colToDate).Value) > 0 Then
                        obj.End_Date = clsCommon.myCDate(grow.Cells(colToDate).Value)
                    End If
                    arr.Add(obj)
                    Dim arr1 As New List(Of clsLockTransactionLocationUserwise)
                    If clsCommon.myCBool(grow.Cells(colLock).Value) Then
                        Dim ArrUser As List(Of clsLockTransactionLocationUserwise) = Nothing
                        ArrUser = TryCast(grow.Cells(colTransactionCode).Tag, List(Of clsLockTransactionLocationUserwise))
                        If ArrUser IsNot Nothing Then
                            For Each objtr As clsLockTransactionLocationUserwise In ArrUser
                                objtr.Location_Code = obj.Location_Code
                                objtr.Module_Name = obj.Module_Name
                                objtr.Trans_Name = obj.Trans_Name
                                objtr.User_Code = objtr.User_Code
                                arr1.Add(objtr)
                            Next
                            clsLockTransactionLocationUserwise.SaveData("", "", arr1, trans)
                        End If
                    End If
                Next
                clsLockTransactionLocationwise.SaveData("", "", arr, trans)
            End If
            trans.Commit()
            common.clsCommon.MyMessageBoxShow("Locked Successfully")
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub chkread_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkread.ToggleStateChanged
        For i As Integer = 0 To dgvDetails.Rows.Count - 1
            dgvDetails.Rows(i).Cells(colLock).Value = chkread.Checked
        Next
    End Sub
    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Try
            Dim qry As String
            If chkLocationSegment.IsChecked = True Then
                qry = " Select Segment_code as Code, Description as Name  from TSPL_GL_SEGMENT_CODE where Seg_No=7 "
                txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("MulLoc", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
            Else
                qry = " Select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
                txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("MulLoc", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
            End If
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        If dtpFromdate1.Text <> "" Then
            For i As Integer = 0 To dgvDetails.Rows.Count - 1
                dgvDetails.Rows(i).Cells(colFromDate).Value = dtpFromdate1.Text
            Next
        End If
        If dtpToDate1.Text <> "" Then
            For i As Integer = 0 To dgvDetails.Rows.Count - 1
                dgvDetails.Rows(i).Cells(colToDate).Value = dtpToDate1.Text
            Next
        End If
    End Sub
    Private Sub btnLockUser_Click(sender As Object, e As EventArgs) Handles btnLockUser.Click
        Try
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION_SEGMENT_USER ")
            clsDBFuncationality.ExecuteNonQuery("Delete from TSPL_LOCK_LOCATION_USER ")
            clsCommon.MyMessageBoxShow("All User Locked successfully")
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
