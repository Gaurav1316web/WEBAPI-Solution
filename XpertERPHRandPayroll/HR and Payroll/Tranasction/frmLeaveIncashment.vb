Imports common
Imports Telerik.WinControls.UI
Imports XpertERPEngine
Imports XpertERPEngineFine
Public Class frmLeaveIncashment
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

#End Region
    Private Sub SetUserMgmtNew()
        Me.Form_ID = clsUserMgtCode.frmLeaveIncashment
        MyBase.SetUserMgmt(clsUserMgtCode.frmLeaveIncashment)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPost.Visible = MyBase.isPostFlag
        btnPrint.Visible = MyBase.isPrintFlag
        If MyBase.isReverse Then
            btnReverse.Enabled = True
        End If
    End Sub

    Private Sub frmLeaveIncashment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        AddNew()
    End Sub
    Private Sub AddNew()

        LoadBlankGrid()
        LoadDocType()
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
        gv1.DataSource = Nothing
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
        repoEmpCode.ReadOnly = False
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
        gv1.Enabled = True
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = True
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        gv1.BestFitColumns()
        gv1.Rows.AddNew()
    End Sub
    Private Sub LoadDocType()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "Leave Incashment"
        dr("Name") = "Leave Incashment"
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
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled Then
            'SaveData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            'funClose()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled Then
            'PostData()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        funClose()
    End Sub
    Private Sub funClose()
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        Try
            AddNew()
            isInsideLoadData = True



            isInsideLoadData = False
        Catch ex As Exception
            isInsideLoadData = False
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

    End Sub

    Private Sub btnPost_Click(sender As Object, e As EventArgs) Handles btnPost.Click

    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click

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
                        Dim Whrcls As String = " Emp_Status='Active' "
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
            ControlField(False)
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
            'Dim qry As String = "select count(*) from TSPL_DA_ARREAR where Document_Code='" + txtDocNo.Value + "' "
            'Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            'If check > 0 Then
            '    txtDocNo.MyReadOnly = True
            'ElseIf check <= 0 Then
            '    txtDocNo.MyReadOnly = False
            'End If
            'LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtDocNo._MYValidating
        Try
            'Dim qry As String = "select TSPL_DA_ARREAR.Document_Code as DocumentCode,convert(varchar(12),TSPL_DA_ARREAR.Document_date,103) as DocumentDate,TSPL_DA_ARREAR.Arrear_Date,TSPL_DA_ARREAR.Location_Code from TSPL_DA_ARREAR "
            ''Dim whrClas As String = " TSPL_DEMAND_BOOKING_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' "
            'Reset()
            'LoadData(clsCommon.ShowSelectForm("DAArreardocfnd", qry, "DocumentCode", "", txtDocNo.Value, "Document_date DESC", isButtonClicked, " TSPL_DA_ARREAR.Document_date "), NavigatorType.Current)
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
End Class