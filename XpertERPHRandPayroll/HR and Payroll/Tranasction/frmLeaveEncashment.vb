Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmLeaveEncashment
#Region "Variables"
    Dim isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim isCellValueChangedOpen As Boolean = False
    Const colCheck As String = "colCheck"
    Const colLineNo As String = "colLineNo"
    Const colEmpCode As String = "colEmpCode"
    Const colempName As String = "colempName"
    Const colLeaveType As String = "colLeaveType"
    Const colLeaveName As String = "colLeaveName"
    Const colNoOfDays As String = "colNoOfDays"
    Const colAmt As String = "colAmt"
    Dim FYFromDate As Date
    Dim FYToDate As Date

#End Region
    Private Sub SetUserMgmtNew()
        Me.Form_ID = clsUserMgtCode.frmLeaveEncashment
        MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveEncashment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnReverse.Visible = False
        End If
    End Sub

    Private Sub frmLeaveIncashment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        CreateTab()
        AddNew()
    End Sub
    Private Sub AddNew()

        LoadBlankGrid()
        LoadDocType()
        LoadFinancialYear()
        'btnGo.Visible = False
        isNewEntry = True
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        UsLock1.Status = ERPTransactionStatus.Pending
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtLocationCode.Value = ""
        lblLocationDesc.Text = ""
        txtRemarks.Text = ""
        cmbDocType.SelectedValue = "Leave Incashment"
        ControlField(True)
    End Sub
    Private Sub ControlField(ByVal flag As Boolean)
        txtDate.Enabled = flag
        txtLocationCode.Enabled = flag
        lblLocationDesc.Enabled = flag
        cmbDocType.Enabled = flag

    End Sub
    Private Sub LoadBlankGrid()

        gv1.Rows.Clear()
        gv1.Columns.Clear()
        Dim RepoCheck As New GridViewCheckBoxColumn
        RepoCheck.FormatString = ""
        RepoCheck.Name = colCheck
        RepoCheck.Width = 50
        RepoCheck.ReadOnly = False
        RepoCheck.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(RepoCheck)
        Dim repoNumBox As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNumBox = New GridViewDecimalColumn()
        repoNumBox.FormatString = ""
        repoNumBox.HeaderText = "Line No"
        repoNumBox.Name = colLineNo
        repoNumBox.Width = 40
        repoNumBox.ReadOnly = True
        repoNumBox.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoNumBox)
        Dim repoEmpCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpCode.FormatString = ""
        repoEmpCode.HeaderText = "Employee Code"
        repoEmpCode.Name = colEmpCode
        repoEmpCode.HeaderImage = My.Resources.search4
        repoEmpCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoEmpCode.Width = 100
        repoEmpCode.IsVisible = True
        gv1.MasterTemplate.Columns.Add(repoEmpCode)
        Dim repoEmpName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoEmpName.FormatString = ""
        repoEmpName.HeaderText = "Employee Name"
        repoEmpName.Name = colempName
        repoEmpName.Width = 150
        repoEmpName.IsVisible = True
        repoEmpName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmpName)
        Dim repoLeaveType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLeaveType.FormatString = ""
        repoLeaveType.HeaderText = "Leave Type"
        repoLeaveType.Name = colLeaveType
        repoLeaveType.HeaderImage = My.Resources.search4
        repoLeaveType.TextImageRelation = TextImageRelation.TextBeforeImage
        repoLeaveType.Width = 100
        repoLeaveType.IsVisible = True
        repoLeaveType.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoLeaveType)
        Dim repoLeaveName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoLeaveName.FormatString = ""
        repoLeaveName.HeaderText = "Leave Name"
        repoLeaveName.Name = colLeaveName
        repoLeaveName.Width = 150
        repoLeaveName.IsVisible = True
        repoLeaveName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoLeaveName)
        Dim repoNoofDays As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoNoofDays.FormatString = "{0:n2}"
        repoNoofDays.HeaderText = "No of Days"
        repoNoofDays.Name = colNoOfDays
        repoNoofDays.Width = 150
        repoNoofDays.Minimum = 0
        repoNoofDays.ShowUpDownButtons = False
        repoNoofDays.DecimalPlaces = 2
        repoNoofDays.IsVisible = True
        repoNoofDays.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoNoofDays)
        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = "{0:n2}"
        repoAmt.HeaderText = "Amount"
        repoAmt.Name = colAmt
        repoAmt.Width = 150
        repoAmt.Minimum = 0
        repoAmt.ShowUpDownButtons = False
        repoAmt.Step = 0
        repoAmt.DecimalPlaces = 2
        repoAmt.IsVisible = True
        repoAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmt)
        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.Rows.AddNew()
    End Sub
    Private Sub LoadFinancialYear()
        Try
            Dim qry As String = ""
            qry = " Select '' as Code,'' as Name,NULL as Start_Dt,NULL as End_Dt, 1 as RI 
                    union all
                    Select Fiscal_Code as Code,Fiscal_Name as Name,Start_Date as Start_Dt,End_Date as End_Dt ,2 as RI from TSPL_Fiscal_Year_Master "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            CmbFinancialYear.DataSource = dt
            CmbFinancialYear.ValueMember = "Code"
            CmbFinancialYear.DisplayMember = "Name"
            'Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Private Sub LoadDocType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "Leave Encashment"
        dr("Name") = "Leave Encashment"
        dt.Rows.Add(dr)
        dr = dt.NewRow()
        dr("Code") = "Surrender leave"
        dr("Name") = "Surrender leave"
        dt.Rows.Add(dr)
        cmbDocType.DataSource = dt
        cmbDocType.ValueMember = "Code"
        cmbDocType.DisplayMember = "Name"
    End Sub
    Sub RefeshSNO()
        For ii As Integer = 1 To gv1.Rows.Count
            gv1.Rows(ii - 1).Cells(colLineNo).Value = ii
        Next
    End Sub
    Private Sub btnAddNew_Click(sender As Object, e As EventArgs) Handles btnAddNew.Click
        AddNew()
    End Sub

    Private Sub frmLeaveIncashment_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Enter AndAlso gv1.CurrentCell IsNot Nothing Then
            SetGridFocus()
        End If
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            PostData()
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
            End If
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        funClose()
    End Sub
    Private Sub funClose()
        Me.Close()
    End Sub
    Function AllowToSave() As Boolean
        Try
            Xtra.TransactionValidity(txtDate.Value)
            If clsCommon.myLen(txtLocationCode.Value) <= 0 Then
                Throw New Exception("Please select Locaiton")
            End If
            If clsCommon.myLen(cmbDocType.SelectedValue) <= 0 Then
                Throw New Exception("Please select DocType")
            End If
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(gv1.Rows(ii).Cells(colEmpCode).Value, Nothing) = CompairStringResult.Equal Then
                    Continue For
                End If
                Dim strEmpCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colEmpCode).Value)
                Dim strLeaveCode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colLeaveType).Value)
                Dim NoOfdays As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colNoOfDays).Value)
                If NoOfdays <= 0 Then
                    Throw New Exception("Please Fill No Of Days at Row No " + clsCommon.myCstr(ii + 1))
                End If
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If jj = ii Then
                        Continue For
                    End If
                    Dim innerEmpCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colEmpCode).Value)
                    Dim innerLeaveCode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colLeaveType).Value)
                    If clsCommon.CompairString(strEmpCode, innerEmpCode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strLeaveCode, innerLeaveCode) = CompairStringResult.Equal Then
                        Throw New Exception("Same Employee and Leave type Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1))
                    End If
                Next
            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveData()
    End Sub
    Private Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsLeaveEncashmentHead()
                obj.Document_Code = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                obj.Location_Code = txtLocationCode.Value
                obj.Doc_Type = cmbDocType.SelectedValue
                obj.Remarks = txtRemarks.Text
                obj.Arr = New List(Of clsLeaveEncashmentDetail)
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New clsLeaveEncashmentDetail
                    objTr.IsApplied = clsCommon.myCdbl(grow.Cells(colCheck).Value)
                    objTr.Emp_Code = clsCommon.myCstr(grow.Cells(colEmpCode).Value)
                    objTr.LEAVE_CODE = clsCommon.myCstr(grow.Cells(colLeaveType).Value)
                    objTr.No_of_Days = clsCommon.myCdbl(grow.Cells(colNoOfDays).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmt).Value)
                    If clsCommon.myLen(objTr.Emp_Code) > 0 Then
                        obj.Arr.Add(objTr)
                    End If
                Next
                If (obj.SaveData(obj, isNewEntry)) = True Then
                    clsCommon.MyMessageBoxShow(Me, "Data Save Successfully ", Me.Text)
                    LoadData(obj.Document_Code, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            AddNew()
            isInsideLoadData = True
            Dim obj As New clsLeaveEncashmentHead
            obj = clsLeaveEncashmentHead.GetData(strCode, NavType)
            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_Code) > 0) Then
                isNewEntry = False
                btnSave.Enabled = True
                btnPost.Enabled = True
                btnDelete.Enabled = True
                If obj.Posted = 1 Then
                    btnSave.Enabled = False
                    btnDelete.Enabled = False
                    btnPost.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Approved
                Else
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If
                txtDocNo.Value = obj.Document_Code
                txtDate.Value = obj.Document_Date
                txtLocationCode.Value = obj.Location_Code
                lblLocationDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocationCode.Value & "'")
                cmbDocType.SelectedValue = obj.Doc_Type
                txtRemarks.Text = obj.Remarks
                Dim rowcount As Integer = 0
                For Each items As clsLeaveEncashmentDetail In obj.Arr
                    gv1.Rows(rowcount).Cells(colCheck).Value = items.IsApplied
                    gv1.Rows(rowcount).Cells(colLineNo).Value = rowcount + 1
                    gv1.Rows(rowcount).Cells(colEmpCode).Value = items.Emp_Code
                    gv1.Rows(rowcount).Cells(colempName).Value = items.Emp_Name
                    gv1.Rows(rowcount).Cells(colLeaveType).Value = items.LEAVE_CODE
                    gv1.Rows(rowcount).Cells(colLeaveName).Value = items.LEAVE_Name
                    gv1.Rows(rowcount).Cells(colNoOfDays).Value = items.No_of_Days
                    gv1.Rows(rowcount).Cells(colAmt).Value = items.Amount
                    gv1.Rows.AddNew()
                    rowcount += 1
                Next
            End If
            isInsideLoadData = False
        Catch ex As Exception
            isInsideLoadData = False
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Private Sub DeleteData()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
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
                    If clsLeaveEncashmentHead.DeleteData(txtDocNo.Value) Then
                        saveCancelLog(Reason, "Delete", Nothing)
                        clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                        AddNew()
                    End If
                End If
            Else
                Throw New Exception("Please Select Document")
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

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click
        PostData()
    End Sub
    Private Sub PostData()
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If (myMessages.postConfirm()) Then

                    If clsLeaveEncashmentHead.PostData(txtDocNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Data Posted Successfully ", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            Else
                Throw New Exception("Please Select Document")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

    End Sub
    Sub SetGridFocus()
        Try
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
            If gv1.CurrentCell IsNot Nothing Then
                If gv1.CurrentCell.ColumnInfo.Name = colEmpCode Then
                    gv1.CurrentColumn = gv1.Columns(colLeaveType)
                ElseIf gv1.CurrentCell.ColumnInfo.Name = colLeaveType Then
                    gv1.CurrentColumn = gv1.Columns(colNoOfDays)
                ElseIf gv1.CurrentCell.ColumnInfo.Name = colNoOfDays Then
                    gv1.CurrentRow = gv1.Rows(intCurrRow + 1)
                    gv1.CurrentColumn = gv1.Columns(colEmpCode)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colEmpCode) Then
                        Dim qry As String = " select EMP_CODE as Code,Emp_Name as Name,Designation  from TSPL_EMPLOYEE_MASTER "
                        Dim Whrcls As String = " Emp_Status='Active' "
                        gv1.CurrentRow.Cells(colEmpCode).Value = clsCommon.ShowSelectForm("fndEmployeecode", qry, "Code", Whrcls, gv1.CurrentRow.Cells(colEmpCode).Value, "Code", True)
                        gv1.CurrentRow.Cells(colempName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where EMP_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colEmpCode).Value) + "' "))
                    ElseIf e.Column Is gv1.Columns(colLeaveType) Then
                        Dim qry As String = " Select LEAVE_CODE As Code,LEAVE_NAME As [Leave Name] from TSPL_LEAVE_MASTER "
                        Dim Whrcls As String = " "
                        gv1.CurrentRow.Cells(colLeaveType).Value = clsCommon.ShowSelectForm("fndleavecode", qry, "Code", Whrcls, gv1.CurrentRow.Cells(colLeaveType).Value, "Code", True)
                        gv1.CurrentRow.Cells(colLeaveName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select LEAVE_NAME from TSPL_LEAVE_MASTER where LEAVE_CODE='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colLeaveType).Value) + "' "))
                    ElseIf e.Column Is gv1.Columns(colNoOfDays) Then

                    ElseIf e.Column Is gv1.Columns(colAmt) Then

                    End If


                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim qry1 As String = " Select Start_Date,End_Date from TSPL_Fiscal_Year_Master where Fiscal_Code = '" + CmbFinancialYear.SelectedValue + "' "
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
            FYFromDate = clsCommon.GetPrintDate(dt1.Rows(0)("Start_Date"))
            FYToDate = clsCommon.GetPrintDate(dt1.Rows(0)("End_Date"))

            Dim qry As String = " Select MTA_CODE from TSPL_MONTHLY_ATTENDANCE where CONVERT(DATE,TSPL_MONTHLY_ATTENDANCE.Created_Date,103) >= convert(date,'" & clsCommon.GetPrintDate(FYFromDate, "dd/MMM/yyyy") & "',103)
                                  AND CONVERT(DATE,TSPL_MONTHLY_ATTENDANCE.Created_Date,103) <=convert(date,'" & clsCommon.GetPrintDate(FYToDate, "dd/MMM/yyyy") & "',103) "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            Dim codes As New List(Of String)

            For Each row As DataRow In dt.Rows
                codes.Add("'" & row("MTA_CODE").ToString() & "'")
            Next
            Dim finalString As String = "(" & String.Join(",", codes.ToArray()) & ")"

            Dim QryAttendance As String = " "
            QryAttendance = " SELECT EMP_CODE,SUM(Casual_Leave) AS CL FROM TSPL_MONTHLY_ATTENDANCE_DETAIL where MTA_CODE In (" + finalString + ")
                              GROUP BY EMP_CODE HAVING SUM(Casual_Leave) >= 15 order by EMP_CODE "

            Dim dtattend As DataTable = clsDBFuncationality.GetDataTable(QryAttendance)

            For Each EMPDETAIL As DataRow In dtattend.Rows
                Dim 
            Next
            'ControlField(False)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocationCode._MYValidating
        Try
            Dim strQuery As String = "select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            txtLocationCode.Value = clsCommon.ShowSelectForm("DALocation", strQuery, "Code", WhrCls, txtLocationCode.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsDBFuncationality.getSingleValue("select  Location_Desc  from TSPL_LOCATION_MASTER where Location_Code='" & txtLocationCode.Value & "'")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qry As String = "select count(*) from TSPL_Leave_Encashment_Head where Document_Code='" + txtDocNo.Value + "' "
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

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            Dim qry As String = "select TSPL_Leave_Encashment_Head.Document_Code as DocumentCode,convert(varchar(12),TSPL_Leave_Encashment_Head.Document_date,103) as DocumentDate,TSPL_Leave_Encashment_Head.Location_Code,TSPL_Leave_Encashment_Head.Doc_Type from TSPL_Leave_Encashment_Head "
            'Dim whrClas As String = " TSPL_DEMAND_BOOKING_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
            Reset()
            LoadData(clsCommon.ShowSelectForm("fndLeaveEncashDoc", qry, "DocumentCode", "", txtDocNo.Value, "Document_date DESC", isButtonClicked, " TSPL_Leave_Encashment_Head.Document_date "), NavigatorType.Current)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub gv1_UserDeletedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserDeletedRow
        Try
            RefeshSNO()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_UserAddedRow(sender As Object, e As GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub
    Private Sub CreateTab()
        Try
            Dim coll As Dictionary(Of String, String)
            coll = New Dictionary(Of String, String)()
            coll.Add("Document_Code", "Varchar(30) not null  PRIMARY KEY")
            coll.Add("Document_Date", "datetime not NULL")
            coll.Add("Location_Code", "VARCHAR(12) Not NULL references TSPL_LOCATION_MASTER(Location_Code)")
            coll.Add("Doc_Type", "varchar(20) Not NULL")
            coll.Add("Remarks", "varchar(200) NULL")
            coll.Add("Posted", "integer null")
            coll.Add("Created_By", "varchar(12) NOT NULL")
            coll.Add("Created_Date", "Datetime NOT NULL")
            coll.Add("Modified_By", "varchar(12) NOT NULL")
            coll.Add("Modified_Date", "Datetime NOT NULL")
            coll.Add("Posted_By", "varchar(12) NULL")
            coll.Add("Posted_Date", "Datetime NULL")
            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_Leave_Encashment_Head", coll, "", True, False, "", "Document_Code", "Document_Date", True)
            coll = New Dictionary(Of String, String)()
            coll.Add("PK_ID", "integer NOT NULL identity NOT FOR REPLICATION primary key")
            coll.Add("Document_Code", "Varchar(30) not null  REFERENCES TSPL_Leave_Encashment_Head(Document_Code)")
            coll.Add("IsApplied", "integer null ")
            coll.Add("Emp_Code", "VARCHAR(12) Not NULL REFERENCES TSPL_EMPLOYEE_MASTER(EMP_CODE)")
            coll.Add("LEAVE_CODE", "VARCHAR(30) NOT NULL REFERENCES TSPL_LEAVE_MASTER(LEAVE_CODE)")
            coll.Add("No_of_Days", "Decimal(18,2) null")
            coll.Add("Amount", "Decimal(18,2) null")

            clsCommonFunctionality.CreateOrAlterTable(True, False, "TSPL_Leave_Encashment_Detail", coll, "", True, False, "TSPL_Leave_Encashment_Head", "Document_Code", "", True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReverse_Click(sender As Object, e As EventArgs) Handles btnReverse.Click
        Try
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                If common.clsCommon.MyMessageBoxShow("Reverse and Unpost the Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then

                    If clsLeaveEncashmentHead.ReverseAndUnpost(txtDocNo.Value) Then
                        clsCommon.MyMessageBoxShow(Me, "Successfully Reversed/Unposted", Me.Text)
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            Else
                Throw New Exception("Please Select Document")
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class