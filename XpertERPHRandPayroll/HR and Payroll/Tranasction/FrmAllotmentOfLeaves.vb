Imports common
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports Telerik.WinControls

'============created by shivani Tyagi
'==== shivani Tyagi against [BM00000008225]
Public Class FrmAllotmentOfLeaves
    Inherits FrmMainTranScreen
    Const colCheck As String = "colCheck"
    Const colLineNo As String = "LineNo"
    Const colEmpCode As String = "colEmpCode"
    Const colEmpName As String = "colEmpName"
    Const colTotal As String = "colTotal"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isnewentry As Boolean = False
    Dim isInsideLoad As Boolean = False
    Dim isCellValueChange As Boolean = False
    Dim Qry As String

    Private Function AllowToSave() As Boolean

        If clsCommon.myLen(fndLocation.Value) <= 0 Then
            RadMessageBox.Show("Please select Location Code")
            fndLocation.Focus()
            Return False
        End If
        If clsCommon.myLen(fndPayPeriod.Value) <= 0 Then
            RadMessageBox.Show("Please select Pay Period Code")
            fndPayPeriod.Focus()
            Return False
        End If
        GridTotal()

        Dim arrempcode As New List(Of String)
        arrempcode = New List(Of String)
        Dim count As Integer = 0
        For Each grow As GridViewRowInfo In gvSalary.Rows
            If clsCommon.myLen(grow.Cells(colEmpCode).Value) > 0 Then
                If clsCommon.CompairString(cboAllotmentType.SelectedValue, "A") = CompairStringResult.Equal AndAlso clsCommon.myCBool(grow.Cells(colCheck).Value) = True Then
                    count += 1
                End If
                If Not arrempcode.Contains(clsCommon.myCstr(grow.Cells(colEmpCode).Value)) Then
                    arrempcode.Add(clsCommon.myCstr(grow.Cells(colEmpCode).Value))
                End If
                'If (clsCommon.myCdbl(grow.Cells(colTotal).Value) <= 0) Then
                '    clsCommon.MyMessageBoxShow("Total Should not be greater than 0 at row no " + clsCommon.myCstr(grow.Index + 1) + "")
                '    Return False
                'End If
                For jj As Integer = (grow.Index + 1) To gvSalary.Rows.Count - 1
                    If clsCommon.myLen(grow.Cells(colEmpCode).Value) > 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(grow.Cells(colEmpCode).Value), clsCommon.myCstr(gvSalary.Rows(jj).Cells(colEmpCode).Value)) = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow(Me, "Duplicate Employees not allowed,see at row no. " + clsCommon.myCstr(jj + 1) + "")
                        Return False
                    End If
                Next
                '========changes by shivani against ticket no [BM00000008638]
                '===========================
                'Dim qry1 As String = "select max(isnull(TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE,0)) as LVALLOTMENT_CODE from TSPL_LEAVE_ALLOTMENT left join TSPL_LEAVE_ALLOTMENTDETAIL on TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE =TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE  WHERE PAY_PERIOD_CODE ='" + fndPayPeriod.Value + "' AND Location_Code ='" + fndLocation.Value + "'  AND Allotment_Type = '" + cboAllotmentType.SelectedValue + "' and Document_Type ='L' AND TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE = '" + clsCommon.myCstr(grow.Cells(colEmpCode).Value) + "' and TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE not in ('" & fndCode.Value & "')"
                'Dim check As String = ""
                'check = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1))
                'If clsCommon.myLen(check) > 0 Then
                '    clsCommon.MyMessageBoxShow("Dublicate Employee not allowed for same Payperiod Code ,'" + clsCommon.myCstr(grow.Cells(colEmpCode).Value) + "' already exist for " + fndPayPeriod.Value + "  ")
                '    Return False
                'End If

                '=====================================
                If clsCommon.CompairString(cboDocType.SelectedValue, "O") = CompairStringResult.Equal Then

                    If (clsCommon.CompairString(cboAllotmentType.SelectedValue, "A") = CompairStringResult.Equal AndAlso clsCommon.myCBool(grow.Cells(colCheck).Value) = True) OrElse clsCommon.CompairString(cboAllotmentType.SelectedValue, "I") = CompairStringResult.Equal Then
                        Dim count1 As Decimal = 0
                        count1 = clsLeaveAllotment.GetOpeningBalance(fndCode.Value, clsCommon.myCstr(grow.Cells(colEmpCode).Value), Nothing)
                        If count1 > 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Opening Balance already  exist for employee '" + clsCommon.myCstr(grow.Cells(colEmpCode).Value) + "' at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                            Return False
                        End If
                    End If
                End If
                '==============================================
            End If
        Next

        If clsCommon.CompairString(cboAllotmentType.SelectedValue, "A") = CompairStringResult.Equal AndAlso count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast one employee for All Case.", Me.Text)
            Return False
        End If
        If arrempcode Is Nothing OrElse arrempcode.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select atleast one employee in grid.", Me.Text)
            Return False
        End If


        'Dim qry As String = "select distinct EMP_CODE  from TSPL_LEAVE_ALLOTMENTDETAIL "
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'If dt.Rows.Count > 0 Then

        'End If
        Return True
    End Function

    Sub SaveData()
        Try
            If AllowToSave() Then
                Dim obj As New clsLeaveAllotment
                obj.LVALLOTMENT_CODE = fndCode.Value
                obj.ALLOTMENT_DATE = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")
                obj.Location_Code = fndLocation.Value
                obj.Division_Code = fndDivision.Value
                obj.ALLOTMENT_REMARKS = txtDescription.Text
                obj.PAY_PERIOD_CODE = fndPayPeriod.Value
                obj.Document_Type = cboDocType.SelectedValue
                obj.Allotment_Type = cboAllotmentType.SelectedValue

                obj.ObjList = New List(Of clsLeaveAllotmentDetails)
                For Each grow As GridViewRowInfo In gvSalary.Rows

                    Dim objTr As New clsLeaveAllotmentDetails()
                    If clsCommon.CompairString(clsCommon.myCstr(cboAllotmentType.SelectedValue), "A") = CompairStringResult.Equal AndAlso clsCommon.myCBool(grow.Cells("colCheck").Value) = False Then
                        Continue For
                    End If
                    If clsCommon.myLen(grow.Cells(colEmpCode).Value) > 0 AndAlso clsCommon.myCdbl(grow.Cells(colTotal).Value) > 0 Then
                        obj.EMP_CODE = clsCommon.myCstr(grow.Cells(colEmpCode).Value) ''Bcz Of Not null of empCode in header table

                        For jj As Integer = 0 To gvSalary.Columns.Count - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colCheck) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colEmpCode) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colEmpName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colTotal) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colLineNo) <> CompairStringResult.Equal Then
                                objTr = New clsLeaveAllotmentDetails()
                                objTr.Document_Type = "PIVOT"
                                objTr.Emp_Code = clsCommon.myCstr(grow.Cells(colEmpCode).Value)
                                objTr.LEAVE_CODE = clsCommon.myCstr(gvSalary.Columns(jj).Name)
                                objTr.ALLOTED_LEAVE = clsCommon.myCdbl(grow.Cells(jj).Value)


                                If clsCommon.myLen(objTr.Emp_Code) > 0 Then
                                    obj.ObjList.Add(objTr)
                                End If
                            End If
                        Next ''end column loop
                    End If ''end emp code


                Next
                If (obj.SaveData(obj, isnewentry)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.LVALLOTMENT_CODE, NavigatorType.Current)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub fndCode__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndCode._MYNavigator
        LoadData(fndCode.Value, NavType)
    End Sub
    Sub LoadGridColumns()
        gvSalary.DataSource = Nothing
        gvSalary.Rows.Clear()
        gvSalary.Columns.Clear()
        Dim Check As New GridViewCheckBoxColumn
        Dim LineNo As New GridViewTextBoxColumn
        Dim EmpCode As New GridViewTextBoxColumn
        Dim EmpName As New GridViewTextBoxColumn
        Dim Total As New GridViewDecimalColumn

        Check.FormatString = ""
        Check.HeaderText = ""
        Check.Name = colCheck
        Check.Width = 30
        Check.ReadOnly = False
        Check.IsVisible = False
        Check.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(Check)

        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 60
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalary.Columns.Add(LineNo)

        EmpCode.FormatString = ""
        EmpCode.HeaderText = "Employee Code"
        EmpCode.Name = colEmpCode
        EmpCode.Width = 120
        EmpCode.ReadOnly = False
        EmpCode.WrapText = True
        EmpCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalary.Columns.Add(EmpCode)

        EmpName.FormatString = ""
        EmpName.HeaderText = "Employee Name"
        EmpName.Name = colEmpName
        EmpName.Width = 220
        EmpName.ReadOnly = True
        EmpName.WrapText = True
        EmpName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvSalary.Columns.Add(EmpName)

        Dim Qry As String = "select LEAVE_CODE ,LEAVE_NAME  from tspl_leave_master"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                Total = New GridViewDecimalColumn
                Total.FormatString = "{0:n2}"
                Total.HeaderText = clsCommon.myCstr(dr("LEAVE_NAME"))
                Total.Name = clsCommon.myCstr(dr("LEAVE_CODE"))
                Total.Width = 50
                Total.ReadOnly = False
                Total.DecimalPlaces = 2
                Total.Minimum = 0
                Total.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
                gvSalary.Columns.Add(Total)

            Next
        End If
        Total = New GridViewDecimalColumn
        Total.FormatString = "{0:n2}"
        Total.DecimalPlaces = 2
        Total.HeaderText = "Total"
        Total.Name = colTotal
        Total.Width = 50
        Total.Minimum = 0
        Total.ReadOnly = True
        Total.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvSalary.Columns.Add(Total)



        gvSalary.AllowDeleteRow = True
        gvSalary.EnableFiltering = True
        gvSalary.AllowAddNewRow = False
        gvSalary.ShowGroupPanel = False
        gvSalary.AllowColumnReorder = True
        gvSalary.AllowRowReorder = False
        gvSalary.EnableSorting = False
        gvSalary.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvSalary.MasterTemplate.ShowRowHeaderColumn = False
        gvSalary.TableElement.TableHeaderHeight = 40
        gvSalary.BestFitColumns()

    End Sub
    Private Sub LoadData(ByVal strCode As String, ByVal NavType As NavigatorType)
        isInsideLoad = True
        Try
            fndCode.MyReadOnly = True
            btnsave.Enabled = True
            btndelete.Enabled = True
            Dim obj As New clsLeaveAllotment()
            obj = clsLeaveAllotment.GetData(strCode, NavType)

            Dim arrCode As New List(Of String)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.LVALLOTMENT_CODE) > 0) Then
                Reset()
                isnewentry = False
                btnsave.Text = "Update"
                fndCode.Value = obj.LVALLOTMENT_CODE
                fndPayPeriod.Value = obj.PAY_PERIOD_CODE
                lblPayPeriodName.Text = obj.PayPer_Name
                lblLocationDesc.Text = obj.Location_Desc
                fndLocation.Value = obj.Location_Code
                fndDivision.Value = obj.Division_Code
                txtDate.Value = obj.ALLOTMENT_DATE
                lblDivisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME from tspl_devision_master where DEVISION_CODE='" & fndDivision.Value & "'")
                cboDocType.SelectedValue = obj.Document_Type
                cboAllotmentType.SelectedValue = obj.Allotment_Type
                txtDescription.Text = obj.ALLOTMENT_REMARKS
                Dim ii As Int16 = 0
                arrCode = New List(Of String)

                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadGridColumns()
                    For Each objTr As clsLeaveAllotmentDetails In obj.ObjList
                        If Not arrCode.Contains(objTr.Emp_Code) Then
                            arrCode.Add(objTr.Emp_Code)
                        Else
                            Continue For
                        End If
                        gvSalary.Rows.AddNew()
                        ii = ii + 1
                        gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = ii
                        gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colEmpCode).Value = objTr.Emp_Code
                        gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colEmpName).Value = clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where Emp_code='" & clsCommon.myCstr(gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colEmpCode).Value) & "'")
                        For Each obj1 As clsLeaveAllotmentDetails In obj.ObjList
                            If clsCommon.CompairString(obj1.Emp_Code, objTr.Emp_Code) = CompairStringResult.Equal Then
                                For jj As Integer = 0 To gvSalary.Columns.Count - 1
                                    If clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), obj1.LEAVE_CODE) = CompairStringResult.Equal Then 'AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colEmpCode) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colEmpName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colTotal) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colLineNo) <> CompairStringResult.Equal
                                        Dim leavname As String = obj1.LEAVE_CODE
                                        gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colCheck).Value = True
                                        gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(jj).Value = obj1.ALLOTED_LEAVE
                                        Exit For
                                    End If
                                Next
                            End If
                        Next ''for column loop


                    Next
                End If
                GridTotal()
                If clsCommon.CompairString(obj.Allotment_Type, "A") = CompairStringResult.Equal Then
                    FirstTimeLoadEmp(False, arrCode)
                    gvSalary.Columns(colEmpCode).ReadOnly = True
                    gvSalary.Columns(colCheck).IsVisible = True
                    chkread.Visible = True
                Else
                    gvSalary.Columns(colEmpCode).ReadOnly = False
                    gvSalary.Columns(colCheck).IsVisible = False
                    chkread.Visible = False
                End If





            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoad = False
        End Try
    End Sub

    Private Sub fndCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndCode._MYValidating
        Dim str As String = "select count(*) from TSPL_LEAVE_ALLOTMENT where LVALLOTMENT_CODE ='" + fndCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        Dim whrCls As String = Nothing
        If no = 0 Then
            fndCode.MyReadOnly = False

        Else
            fndCode.MyReadOnly = True
        End If
        If fndCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select * from (select TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE as [Code],TSPL_LEAVE_ALLOTMENT.ALLOTMENT_DATE as [Date],TSPL_LEAVE_ALLOTMENT.Location_Code as [Location] ,TSPL_LEAVE_ALLOTMENT.Division as [Division],TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE as [Pay Period],case when TSPL_LEAVE_ALLOTMENT.Document_Type='L' then 'Leave Allotment' else 'Opening Balance' end as [Document Type],case when TSPL_LEAVE_ALLOTMENT.Allotment_Type='I' then 'Individual' else 'All' end as [Allotment Type] ,max(TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE ) as [Employee Code]   from TSPL_LEAVE_ALLOTMENT left join TSPL_LEAVE_ALLOTMENTDETAIL on TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE=TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE group by TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE,TSPL_LEAVE_ALLOTMENT.ALLOTMENT_DATE,TSPL_LEAVE_ALLOTMENT.Location_Code,TSPL_LEAVE_ALLOTMENT.Division,TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE,TSPL_LEAVE_ALLOTMENT.Document_Type,TSPL_LEAVE_ALLOTMENT.Allotment_Type) as final"
            'fndCode.Value = clsCommon.ShowSelectForm("LeaveAllotment", qry, "Code", whrCls, fndCode.Value, "LVALLOTMENT_CODE", isButtonClicked)
            fndCode.Value = clsCommon.ShowSelectForm("LeaveAllotment", qry, "Code", "", fndCode.Value, "Code", isButtonClicked)
            If clsCommon.myLen(fndCode.Value) > 0 Then
                LoadData(fndCode.Value, NavigatorType.Current)
            Else
                Reset()
            End If
        End If
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim qry As String = " select LOCATION_CODE as [Code],LOCATION_DESC from TSPL_LOCATION_MASTER  "
        fndLocation.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("EMP", qry, "Code", "Location_Type='PHYSICAL'", fndLocation.Value, "Code", isButtonClicked))
        If clsCommon.myLen(clsCommon.myCstr(fndLocation.Value)) > 0 Then
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select LOCATION_DESC from TSPL_LOCATION_MASTER where LOCATION_CODE='" & fndLocation.Value & "'"))
        End If
    End Sub

    Private Sub fndDivision__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDivision._MYValidating
        Dim qry As String = " select DEVISION_CODE as [Code],DEVISION_NAME as [Name]  from TSPL_DEVISION_MASTER "
        fndDivision.Value = clsCommon.myCstr(clsCommon.ShowSelectForm("DIV", qry, "Code", "", fndDivision.Value, "Code", isButtonClicked))
        If clsCommon.myLen(clsCommon.myCstr(fndDivision.Value)) > 0 Then
            lblDivisionName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select DEVISION_NAME  from TSPL_DEVISION_MASTER where DEVISION_CODE ='" & fndDivision.Value & "'"))
        End If
    End Sub

    Private Sub fndPayPeriod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPayPeriod._MYValidating
        fndPayPeriod.Value = clsPayPeriodMaster.getFinder("POSTED=1 and FREEZED=0 and convert(date, date_from,103) <= Convert (date,SYSDATETIME(),103)", fndPayPeriod.Value, isButtonClicked)
        lblPayPeriodName.Text = clsPayPeriodMaster.GetName(fndPayPeriod.Value, Nothing)
        Dim objPayrollSett As clsPayrollSetting = clsPayrollSetting.GetPayrollSetting(fndLocation.Value, Nothing)
        If objPayrollSett Is Nothing Then
            Exit Sub
        End If
        If clsCommon.myLen(fndPayPeriod.Value) > 0 Then
            'LoadDefaultDataInGrid()
        End If
    End Sub
    Sub LoadDocumentType()
        isInsideLoad = True
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "O"
        dr("Name") = "Opening Balance"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "L"
        dr("Name") = "Leave Allotment"
        dt.Rows.Add(dr)

        cboDocType.DataSource = dt
        cboDocType.ValueMember = "Code"
        cboDocType.DisplayMember = "Name"
        isInsideLoad = False
    End Sub
    Sub LoadAllotmentType()
        isInsideLoad = True
        Dim dt As DataTable = New DataTable
        dt.Columns.Add("Code")
        dt.Columns.Add("Name")

        Dim dr As DataRow = dt.NewRow
        dr("Code") = "I"
        dr("Name") = "Individual"
        dt.Rows.Add(dr)

        dr = dt.NewRow
        dr("Code") = "A"
        dr("Name") = "All"
        dt.Rows.Add(dr)

        cboAllotmentType.DataSource = dt
        cboAllotmentType.ValueMember = "Code"
        cboAllotmentType.DisplayMember = "Name"
        isInsideLoad = False
    End Sub
    Sub Reset()
        fndCode.Value = ""
        fndCode.MyReadOnly = False
        isnewentry = True
        fndCode.Value = Nothing
        fndPayPeriod.Value = Nothing
        lblPayPeriodName.Text = ""
        fndLocation.Value = Nothing
        lblLocationDesc.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
        fndDivision.Value = ""
        lblDivisionName.Text = ""
        cboDocType.SelectedValue = "L"
        cboAllotmentType.SelectedValue = "I"
        txtDate.Value = clsCommon.GETSERVERDATE()
        chkread.Visible = False
        gvSalary.Rows.Clear()
        gvSalary.Rows.AddNew()
        fndCode.Focus()

    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Reset()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub funDelete()
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
                If (clsLeaveAllotment.DeleteData(fndCode.Value)) Then
                    'saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    Reset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        funDelete()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmAllotmentOfLeaves_HelpRequested(sender As Object, hlpevent As HelpEventArgs) Handles Me.HelpRequested

    End Sub

    Private Sub FrmAllotmentOfLeaves_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            delete()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()

        End If
    End Sub


    Private Sub FrmAllotmentOfLeaves_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDocumentType()
        LoadAllotmentType()
        LoadGridColumns()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        ''buttontooltip left
    End Sub


    Private Sub cboDocType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboDocType.SelectedValueChanged
        If isInsideLoad Then
            Exit Sub
        End If


    End Sub

    Private Sub cboAllotmentType_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboAllotmentType.SelectedValueChanged
        If isInsideLoad Then
            Exit Sub
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboAllotmentType.SelectedValue), "A") = CompairStringResult.Equal Then
            gvSalary.Columns(colEmpCode).ReadOnly = True
            gvSalary.Columns(colCheck).IsVisible = True
            FirstTimeLoadEmp(True, Nothing)
            chkread.Visible = True
        Else
            gvSalary.Columns(colEmpCode).ReadOnly = False
            gvSalary.Columns(colCheck).IsVisible = False
            chkread.Visible = False
        End If
    End Sub
    Sub FirstTimeLoadEmp(ByVal isFirstTimeLoad As Boolean, ByVal arrEmpCode As List(Of String))
        Dim qry As String = "select EMP_CODE ,Emp_Name  from TSPL_EMPLOYEE_MASTER where LOCATION_CODE = '" & fndLocation.Value & "'"
        If clsCommon.myLen(fndDivision.Value) > 0 Then
            qry += " and DEVISION_CODE ='" & fndDivision.Value & "' "
        End If
        qry += " and isnull(Emp_Code,'') not in (select isnull(TSPL_LEAVE_ALLOTMENTDETAIL.emp_code,'') from TSPL_LEAVE_ALLOTMENTDETAIL left outer join TSPL_LEAVE_ALLOTMENT on TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE=TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE where TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE<>'" & fndCode.Value & "' and TSPL_LEAVE_ALLOTMENT.Location_Code='" & fndLocation.Value & "' and TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE='" & fndPayPeriod.Value & "' and TSPL_LEAVE_ALLOTMENT.Allotment_Type='" & cboAllotmentType.SelectedValue & "')"
        If isFirstTimeLoad Then
            gvSalary.Rows.Clear()
        Else
            qry += " and emp_code not in (" + clsCommon.GetMulcallString(arrEmpCode) + ") "
        End If
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvSalary.Rows.AddNew()
                gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = gvSalary.Rows.Count
                gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colEmpCode).Value = dr("EMP_CODE")
                gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colEmpName).Value = dr("Emp_Name")
                gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colCheck).Value = isFirstTimeLoad
            Next
        End If
    End Sub
    Sub OpenEmpList(ByVal isButtonClick As Boolean)
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            Dim qry As String = "select dISTINCT TSPL_EMPLOYEE_STATUS.EMP_CODE as [Code] ,Emp_Name,TSPL_EMPLOYEE_MASTER.Designation  from TSPL_EMPLOYEE_STATUS left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_EMPLOYEE_STATUS.EMP_CODE"
            Dim whrCls As String = "WORKING_STATUS='Working' and TSPL_EMPLOYEE_STATUS.Location_Code='" & fndLocation.Value & "'"
            gvSalary.CurrentRow.Cells(colCheck).Value = False
            gvSalary.CurrentRow.Cells(colEmpCode).Value = clsCommon.ShowSelectForm("fndnder", qry, "Code", whrCls, clsCommon.myCstr(gvSalary.CurrentRow.Cells(colEmpCode).Value), "Code", isButtonClick)
            gvSalary.CurrentRow.Cells(colEmpName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Emp_Name from TSPL_EMPLOYEE_MASTER where emp_code='" & clsCommon.myCstr(gvSalary.CurrentRow.Cells(colEmpCode).Value) & "'"))
        End If
    End Sub
    Private Sub gvSalary_CellEndEdit(sender As Object, e As GridViewCellEventArgs) Handles gvSalary.CellEndEdit
        Try

            If Not isCellValueChange Then
                isCellValueChange = True
                If e.Column Is gvSalary.Columns(colEmpCode) Then
                    OpenEmpList(False)
                End If
                isCellValueChange = False
            End If

        Catch ex As Exception
            isCellValueChange = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub GridTotal()
        Dim SUM As Decimal = 0.0
        For ii As Integer = 0 To gvSalary.Rows.Count - 1
            SUM = 0
            For jj As Integer = 0 To gvSalary.Columns.Count - 1
                If clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colCheck) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colEmpCode) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colEmpName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colTotal) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colLineNo) <> CompairStringResult.Equal Then
                    SUM += clsCommon.myCdbl(gvSalary.Rows(ii).Cells(jj).Value)
                End If

            Next ''end column loop
            gvSalary.Rows(ii).Cells(colTotal).Value = SUM
        Next ''end row loop
    End Sub

    Private Sub gvSalary_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvSalary.CellValueChanged
        Try
            If Not isCellValueChange Then
                If clsCommon.CompairString(cboAllotmentType.SelectedValue, "A") = CompairStringResult.Equal Then
                    ''before cellindex-4 there is predefine columns in grid and in between that we add dynamic column so here use index for it,
                    ''if do any change in grid please make sure for ALL condition this event also work.
                    If Not e.Column Is gvSalary.Columns(colCheck) AndAlso Not e.Column Is gvSalary.Columns(colLineNo) AndAlso Not e.Column Is gvSalary.Columns(colEmpCode) AndAlso Not e.Column Is gvSalary.Columns(colEmpName) AndAlso Not e.Column Is gvSalary.Columns(colTotal) Then
                        isCellValueChange = True
                        For Each grow As GridViewRowInfo In gvSalary.Rows
                            For jj As Integer = 0 To gvSalary.Columns.Count - 1
                                If clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colCheck) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colEmpCode) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colEmpName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colTotal) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(jj).Name), colLineNo) <> CompairStringResult.Equal Then
                                    grow.Cells(jj).Value = clsCommon.myCdbl(gvSalary.CurrentRow.Cells(jj).Value)
                                End If
                            Next ''end column loop
                        Next
                        isCellValueChange = False
                    End If
                End If
                If Not e.Column Is gvSalary.Columns(colCheck) AndAlso Not e.Column Is gvSalary.Columns(colLineNo) AndAlso Not e.Column Is gvSalary.Columns(colEmpCode) AndAlso Not e.Column Is gvSalary.Columns(colEmpName) AndAlso Not e.Column Is gvSalary.Columns(colTotal) Then
                    isCellValueChange = True
                    GridTotal()
                    isCellValueChange = False
                End If
            End If
        Catch ex As Exception
            isCellValueChange = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvSalary_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvSalary.CurrentColumnChanged
        If gvSalary.RowCount > 0 Then
            Dim intCurrRow As Integer = gvSalary.CurrentRow.Index
            gvSalary.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvSalary.Rows.Count - 1 Then
                gvSalary.Rows.AddNew()
                gvSalary.CurrentRow = gvSalary.Rows(intCurrRow)
            End If
        End If

    End Sub

    Private Sub chkread_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkread.ToggleStateChanged
        If chkread.Checked = True Then

            For i As Integer = 0 To gvSalary.Rows.Count - 1
                gvSalary.Rows(i).Cells("colCheck").Value = True
            Next
        ElseIf chkread.Checked = False Then

            For i As Integer = 0 To gvSalary.Rows.Count - 1
                gvSalary.Rows(i).Cells("colCheck").Value = False
            Next

        End If
    End Sub
    '====================added by shivani tyagi against ticket[BM00000008638]
    Sub ImportIndividual(ByVal Allotment_Type As String)
        If clsCommon.myLen(Allotment_Type) <= 0 Or (clsCommon.CompairString(Allotment_Type, "I") <> CompairStringResult.Equal And clsCommon.CompairString(Allotment_Type, "A") <> CompairStringResult.Equal) Then
            clsCommon.MyMessageBoxShow(Me, "Invalid Import Type", Me.Text)
            Exit Sub
        End If
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim currentdate As Date = Date.Today
        Dim arr() As String
        Dim qry As String = " select STUFF((Select Distinct ','+LEAVE_CODE from TSPL_LEAVE_MASTER For XML Path('')),1,1,'')"
        Dim strSelect As String = clsDBFuncationality.getSingleValue(qry)

        Dim arrParam() As String = {"Code", "Date", "Document Type", "Pay Period Code", "Location Code", "Division Code", "Employee Code", "Employee Name"}
        arr = strSelect.Split(",")
        For Each strarr As String In arr
            ReDim Preserve arrParam(arrParam.Length)
            arrParam(arrParam.Length - 1) = strarr
        Next

        If transportSql.importExcel(gv, arrParam) Then


            Try

                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsLeaveAllotment()
                    obj.ObjList = New List(Of clsLeaveAllotmentDetails)
                    Dim objtr As New clsLeaveAllotmentDetails()
                    Dim strName As String = Nothing
                    Dim strCode As String = clsCommon.myCstr(grow.Cells("Code").Value)
                    obj.LVALLOTMENT_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Date").Value)
                    obj.ALLOTMENT_DATE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Document Type").Value)
                    If clsCommon.CompairString(strCode, "O") = CompairStringResult.Equal Then
                        obj.Document_Type = strCode
                    ElseIf clsCommon.CompairString(strCode, "L") = CompairStringResult.Equal Then
                        obj.Document_Type = strCode
                    Else
                        obj.Document_Type = "O"
                    End If

                    obj.Allotment_Type = "I"

                    strCode = clsCommon.myCstr(grow.Cells("Pay Period Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Pay Period Code can not be blank or incorrect.")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_PAYPERIOD_MASTER  where PAY_PERIOD_CODE ='" & strCode & "'")
                            If strName <= 0 Then
                                Throw New Exception("Pay Period Code (" & strCode & ") does not exist in Payperiod  Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.PAY_PERIOD_CODE = strCode

                    obj.EMP_CODE = clsCommon.myCstr(grow.Cells("Employee Code").Value)


                    strCode = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Location Code can not be blank or incorrect.")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_LOCATION_MASTER  where Location_Code ='" & strCode & "'")
                            If strName <= 0 Then
                                Throw New Exception("Location Code (" & strCode & ") does not exist in Location  Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.Location_Code = strCode
                    obj.Location_Desc = clsLocation.GetName(obj.Location_Code, Nothing)

                    strCode = clsCommon.myCstr(grow.Cells("Division Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Division Code can not be blank or incorrect.")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_DEVISION_MASTER  where DEVISION_CODE ='" & strCode & "'")
                            If strName <= 0 Then
                                Throw New Exception("Division Code (" & strCode & ") does not exist in Division  Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.Division_Code = strCode

                    '==============================pivot columns code=====================

                    objtr = New clsLeaveAllotmentDetails()
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        For Each Str As String In arr
                            If clsCommon.myLen(Str) > 0 Then
                                objtr = New clsLeaveAllotmentDetails()
                                objtr.ALLOTED_LEAVE = clsCommon.myCdbl(grow.Cells(Str).Value)
                                strCode = clsCommon.myCstr(grow.Cells("Employee Code").Value)
                                If (String.IsNullOrEmpty(strCode)) OrElse clsCommon.myLen(strCode) <= 0 Then
                                    Throw New Exception("Employee Code  can not be blank or incorrect.")
                                Else
                                    If strCode.Length > 0 Then
                                        strName = clsDBFuncationality.getSingleValue("select COUNT(*) from Tspl_Employee_Master  where Emp_Code ='" & strCode & "'")
                                        If strName <= 0 Then
                                            Throw New Exception("Employee Code (" & strCode & ") does not exist in Employee Master  . Please make it entry first.")
                                        End If
                                    End If
                                End If
                                objtr.Emp_Code = strCode
                                objtr.LEAVE_CODE = Str
                                objtr.LEAVE_NAME = clsLeaveMaster.GetName(objtr.LEAVE_CODE, Nothing)
                                objtr.LVALLOTMENT_CODE = clsCommon.myCstr(grow.Cells("Code").Value)
                                objtr.Document_Type = "PIVOT"

                                obj.ObjList.Add(objtr)
                            End If
                            '=============

                            If clsCommon.CompairString(obj.Document_Type, "O") = CompairStringResult.Equal Then
                                If clsCommon.CompairString(obj.Allotment_Type, "I") = CompairStringResult.Equal Then
                                    Dim count1 As Decimal = 0
                                    count1 = clsLeaveAllotment.GetOpeningBalance(objtr.LVALLOTMENT_CODE, clsCommon.myCstr(grow.Cells("Employee Code").Value), Nothing)
                                    If count1 > 0 Then
                                        Throw New Exception("Opening Balance already  exist for employee '" + clsCommon.myCstr(grow.Cells("Employee Code").Value) + "'")

                                    End If
                                End If
                            End If
                        Next
                    End If
                    Dim qry1 As String = "SELECT Count(*) FROM TSPL_LEAVE_ALLOTMENT where LVALLOTMENT_CODE= '" & obj.LVALLOTMENT_CODE & "'"
                    Dim check As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check = 0 Then
                        obj.SaveData(obj, True)
                    Else
                        obj.SaveData(obj, False)
                    End If
                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If

        Me.Controls.Remove(gv)
        '--------------------------

    End Sub
    Sub ImportALL(ByVal Allotment_Type As String)
        If clsCommon.myLen(Allotment_Type) <= 0 Or (clsCommon.CompairString(Allotment_Type, "I") <> CompairStringResult.Equal And clsCommon.CompairString(Allotment_Type, "A") <> CompairStringResult.Equal) Then
            clsCommon.MyMessageBoxShow(Me, "Invalid Import Type", Me.Text)
            Exit Sub
        End If
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)

        Dim currentdate As Date = Date.Today
        Dim arr() As String
        Dim qry As String = " select STUFF((Select Distinct ','+LEAVE_CODE from TSPL_LEAVE_MASTER For XML Path('')),1,1,'')"
        Dim strSelect As String = clsDBFuncationality.getSingleValue(qry)

        Dim arrParam() As String = {"Date", "Document Type", "Pay Period Code", "Location Code", "Division Code"}
        arr = strSelect.Split(",")
        For Each strarr As String In arr
            ReDim Preserve arrParam(arrParam.Length)
            arrParam(arrParam.Length - 1) = strarr
        Next

        If transportSql.importExcel(gv, arrParam) Then
            Try
                clsCommon.ProgressBarShow()
                For Each grow As GridViewRowInfo In gv.Rows
                    Dim obj As New clsLeaveAllotment()
                    obj.ObjList = New List(Of clsLeaveAllotmentDetails)
                    Dim objtr As New clsLeaveAllotmentDetails()
                    Dim strName As String = Nothing
                    Dim strCode As String = Nothing


                    strCode = clsCommon.myCstr(grow.Cells("Date").Value)
                    obj.ALLOTMENT_DATE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Document Type").Value)
                    If clsCommon.CompairString(strCode, "O") = CompairStringResult.Equal Then
                        obj.Document_Type = strCode
                    ElseIf clsCommon.CompairString(strCode, "L") = CompairStringResult.Equal Then
                        obj.Document_Type = strCode
                    Else
                        obj.Document_Type = "O"
                    End If

                    obj.Allotment_Type = "A"
                    obj.EMP_CODE = "H10001"
                    strCode = clsCommon.myCstr(grow.Cells("Pay Period Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Pay Period Code can not be blank or incorrect.")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_PAYPERIOD_MASTER  where PAY_PERIOD_CODE ='" & strCode & "'")
                            If strName <= 0 Then
                                Throw New Exception("Pay Period Code (" & strCode & ") does not exist in Payperiod  Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.PAY_PERIOD_CODE = strCode

                    strCode = clsCommon.myCstr(grow.Cells("Location Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Location Code can not be blank or incorrect.")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_LOCATION_MASTER  where Location_Code ='" & strCode & "'")
                            If strName <= 0 Then
                                Throw New Exception("Location Code (" & strCode & ") does not exist in Location  Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.Location_Code = strCode
                    obj.Location_Desc = clsLocation.GetName(obj.Location_Code, Nothing)

                    strCode = clsCommon.myCstr(grow.Cells("Division Code").Value)
                    If strCode.Length > 30 Or (String.IsNullOrEmpty(strCode)) Then
                        Throw New Exception("Division Code can not be blank or incorrect.")
                    Else
                        If strCode.Length > 0 Then
                            strName = clsDBFuncationality.getSingleValue("select COUNT(*) from TSPL_DEVISION_MASTER  where DEVISION_CODE ='" & strCode & "'")
                            If strName <= 0 Then
                                Throw New Exception("Division Code (" & strCode & ") does not exist in Division  Master  . Please make it entry first.")
                            End If
                        End If
                    End If
                    obj.Division_Code = strCode

                    '==============================pivot columns code=====================
                    Dim qry2 As String = "select EMP_CODE  from tspl_employee_master where LOCATION_CODE ='" + obj.Location_Code + "' and DEVISION_CODE ='" + obj.Division_Code + "'"
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry2)

                    objtr = New clsLeaveAllotmentDetails()
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            For Each dr As DataRow In dt.Rows
                                objtr.Emp_Code = clsCommon.myCstr(dr("EMP_CODE"))
                                For Each Str As String In arr
                                    If clsCommon.myLen(Str) > 0 Then
                                        obj.ObjList = New List(Of clsLeaveAllotmentDetails)
                                        objtr = New clsLeaveAllotmentDetails()
                                        objtr.ALLOTED_LEAVE = clsCommon.myCdbl(grow.Cells(Str).Value)
                                        objtr.Emp_Code = clsCommon.myCstr(dr("EMP_CODE"))
                                        objtr.LEAVE_CODE = Str
                                        objtr.LEAVE_NAME = clsLeaveMaster.GetName(objtr.LEAVE_CODE, Nothing)
                                        objtr.Document_Type = "PIVOT"
                                        obj.ObjList.Add(objtr)

                                        Dim qry1 As String = "select max(isnull(TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE,0)) as LVALLOTMENT_CODE from TSPL_LEAVE_ALLOTMENT left join TSPL_LEAVE_ALLOTMENTDETAIL on TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE =TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE  WHERE PAY_PERIOD_CODE ='" + obj.PAY_PERIOD_CODE + "' AND Location_Code ='" + obj.Location_Code + "'  AND Allotment_Type ='A' and Document_Type ='" + obj.Document_Type + "' "
                                        Dim check As String = ""
                                        check = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry1))
                                        If check Is Nothing Or clsCommon.myLen(check) = 0 Then
                                            obj.LVALLOTMENT_CODE = clsERPFuncationality.GetNextCode(Nothing, obj.ALLOTMENT_DATE, clsDocType.LeaveAllotment, "", obj.Location_Code)
                                            If clsCommon.myLen(obj.LVALLOTMENT_CODE) <= 0 Then
                                                Throw New Exception("Error in Code Genration")
                                            End If
                                        Else
                                            obj.LVALLOTMENT_CODE = check
                                        End If

                                        Dim COLL As New Hashtable
                                        clsCommon.AddColumnsForChange(COLL, "PAY_PERIOD_CODE", obj.PAY_PERIOD_CODE)
                                        clsCommon.AddColumnsForChange(COLL, "Location_Code", obj.Location_Code)
                                        clsCommon.AddColumnsForChange(COLL, "EMP_CODE", obj.EMP_CODE)
                                        clsCommon.AddColumnsForChange(COLL, "Division", obj.Division_Code)
                                        clsCommon.AddColumnsForChange(COLL, "Document_Type", obj.Document_Type, True)
                                        clsCommon.AddColumnsForChange(COLL, "Allotment_Type", obj.Allotment_Type, True)
                                        clsCommon.AddColumnsForChange(COLL, "ALLOTMENT_DATE", clsCommon.GetPrintDate(obj.ALLOTMENT_DATE, "dd/MMM/yyyy"))
                                        clsCommon.AddColumnsForChange(COLL, "LVALLOTMENT_CODE", obj.LVALLOTMENT_CODE)
                                        clsCommon.AddColumnsForChange(COLL, "Modified_By", objCommonVar.CurrentUserCode)
                                        clsCommon.AddColumnsForChange(COLL, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy"))

                                        If clsCommon.myLen(check) <= 0 Then
                                            clsCommon.AddColumnsForChange(COLL, "Created_By", objCommonVar.CurrentUserCode)
                                            clsCommon.AddColumnsForChange(COLL, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy"))
                                            clsCommonFunctionality.UpdateDataTable(COLL, "TSPL_LEAVE_ALLOTMENT", OMInsertOrUpdate.Insert, "")
                                        Else
                                            clsCommonFunctionality.UpdateDataTable(COLL, "TSPL_LEAVE_ALLOTMENT", OMInsertOrUpdate.Update, "TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE='" + obj.LVALLOTMENT_CODE + "'")
                                        End If

                                        ''=================DETAIL INSERT
                                        Dim isSaved As Boolean = True
                                        COLL = New Hashtable
                                        clsCommon.AddColumnsForChange(COLL, "LEAVE_CODE", objtr.LEAVE_CODE)
                                        clsCommon.AddColumnsForChange(COLL, "ALLOTED_LEAVE", objtr.ALLOTED_LEAVE)
                                        clsCommon.AddColumnsForChange(COLL, "Emp_Code", objtr.Emp_Code)
                                        clsCommon.AddColumnsForChange(COLL, "Modified_By", objCommonVar.CurrentUserCode)
                                        clsCommon.AddColumnsForChange(COLL, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy"))

                                        Dim whrcls As String = ""
                                        whrcls = " and emp_code='" + objtr.Emp_Code + "' "

                                        qry = "SELECT Count(*) FROM TSPL_LEAVE_ALLOTMENTDETAIL where LVALLOTMENT_CODE = '" & obj.LVALLOTMENT_CODE & "' and LEAVE_CODE = '" & objtr.LEAVE_CODE & "' " + whrcls + " "
                                        Dim check11 As Integer = clsDBFuncationality.getSingleValue(qry)

                                        If check11 = 0 Then
                                            clsCommon.AddColumnsForChange(COLL, "LVALLOTMENT_CODE", obj.LVALLOTMENT_CODE)
                                            clsCommon.AddColumnsForChange(COLL, "Created_By", objCommonVar.CurrentUserCode)
                                            clsCommon.AddColumnsForChange(COLL, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Nothing), "dd/MMM/yyyy"))
                                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(COLL, "TSPL_LEAVE_ALLOTMENTDETAIL", OMInsertOrUpdate.Insert, "")
                                        Else
                                            isSaved = isSaved AndAlso clsCommonFunctionality.UpdateDataTable(COLL, "TSPL_LEAVE_ALLOTMENTDETAIL", OMInsertOrUpdate.Update, " LVALLOTMENT_CODE = '" & obj.LVALLOTMENT_CODE & "' and LEAVE_CODE = '" & objtr.LEAVE_CODE & "' " + whrcls + " ")
                                        End If
                                    End If ''STR COND

                                Next ''ARR LOOP
                            Next ''dr loop
                        End If ''DT COND
                    End If

                    '==============================end here=================================


                Next
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If

        Me.Controls.Remove(gv)
        '--------------------------

    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        ImportIndividual("I")
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        Dim Leave As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select STUFF((Select Distinct ', ['+LEAVE_CODE+']' from TSPL_LEAVE_MASTER For XML Path('')),1,1,'')"))
        Dim sumLeave As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select STUFF((Select Distinct ', sum(['+(LEAVE_CODE)+']) as'+' ['+LEAVE_CODE +']' from TSPL_LEAVE_MASTER For XML Path('')),1,1,'')"))
        str = "select * from (select dd. LVALLOTMENT_CODE as [Code], max(ALLOTMENT_DATE)  as [Date],max(Document_type) as [Document Type] ,max(PAY_PERIOD_CODE) as [Pay Period Code],max(Location_Code) as [Location Code],max(Division) as [Division Code] ,max(EMP_CODE) as [Employee Code],max(Emp_Name) as [Employee Name]," + sumLeave + " from (" & _
              " select * from(Select  convert(decimal(18,2),ALLOTED_LEAVE) as ALLOTED_LEAVE ,TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE,ALLOTMENT_DATE,Document_type,TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE,TSPL_LEAVE_ALLOTMENT.Location_Code,TSPL_LEAVE_ALLOTMENT.Division ,TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE ,Emp_Name,TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE as AllotLeave,TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE   from TSPL_LEAVE_ALLOTMENT left join TSPL_LEAVE_ALLOTMENTDETAIL on TSPL_LEAVE_ALLOTMENTDETAIL.LVALLOTMENT_CODE =TSPL_LEAVE_ALLOTMENT.LVALLOTMENT_CODE left join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE= TSPL_LEAVE_ALLOTMENTDETAIL.EMP_CODE left join TSPL_PAYPERIOD_MASTER on TSPL_PAYPERIOD_MASTER.PAY_PERIOD_CODE =TSPL_LEAVE_ALLOTMENT.PAY_PERIOD_CODE	 left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_LEAVE_ALLOTMENT.Location_Code left join TSPL_DEVISION_MASTER on TSPL_DEVISION_MASTER.DEVISION_CODE =TSPL_LEAVE_ALLOTMENT.Division left join TSPL_LEAVE_MASTER on TSPL_LEAVE_MASTER.LEAVE_CODE =TSPL_LEAVE_ALLOTMENTDETAIL.LEAVE_CODE where Allotment_Type ='I'" & _
              " ) as mm  Pivot(  sum(ALLOTED_LEAVE) FOR [AllotLeave] IN (" + Leave + "))pp)as dd group by LVALLOTMENT_CODE)h"
        transportSql.ExporttoExcelForPivot(str, Me)
    End Sub

    Private Sub RadButton1_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            'If clsCommon.CompairString(cboDocType.SelectedValue, "O") = CompairStringResult.Equal Then
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                RadMessageBox.Show(Me, "Please select Location Code")
                fndLocation.Focus()
                Exit Sub
            End If

            LoadGridColumns()
            Dim qry As String = "select EMP_CODE ,Emp_Name  from TSPL_EMPLOYEE_MASTER where LOCATION_CODE = '" + fndLocation.Value + "'  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    gvSalary.Rows.AddNew()
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colLineNo).Value = gvSalary.Rows.Count
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colEmpCode).Value = dr("EMP_CODE")
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colEmpName).Value = dr("Emp_Name")
                    gvSalary.Rows(gvSalary.Rows.Count - 1).Cells(colCheck).Value = True
                Next
                If clsCommon.CompairString(cboDocType.SelectedValue, "O") = CompairStringResult.Equal Then
                    For ii As Integer = 0 To gvSalary.Columns.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(ii).Name), colCheck) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(ii).Name), colEmpCode) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(ii).Name), colEmpName) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(ii).Name), colTotal) <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvSalary.Columns(ii).Name), colLineNo) <> CompairStringResult.Equal Then
                            For jj As Integer = 0 To gvSalary.Rows.Count - 1
                                Dim dclAllotedDays As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Alloted_Days from TSPL_LEAVE_SETTING where LEAVE_CODE='" + clsCommon.myCstr(gvSalary.Columns(ii).Name) + "'"))
                                If dclAllotedDays <= 0 Then
                                    Exit For
                                End If
                                gvSalary.Rows(jj).Cells(ii).Value = dclAllotedDays
                            Next
                        End If
                    Next
                End If

                GridTotal()
            End If
            'Else
            '    Throw New Exception("Only for opening Type Document")
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim gvImport As New RadGridView()
        Me.Controls.Add(gvImport)
        Try
            Dim Strs As List(Of String) = New List(Of String)
            Dim Qry As String = "select LEAVE_CODE ,LEAVE_NAME  from tspl_leave_master"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            Strs.Add("Line No")
            Strs.Add("Employee Code")
            Strs.Add("Employee Name")
            If dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Strs.Add(clsCommon.myCstr(dr("LEAVE_NAME")))
                Next
            End If
            Strs.Add("Total")
            transportSql.importExcel(gvImport, Strs.ToArray())
            Dim TempTotal As Double = 0

            For ii As Integer = 0 To gvImport.RowCount - 1
                Dim strICode As String = clsCommon.myCstr(gvImport.Rows(ii).Cells("Employee Code").Value)
                For jj As Integer = 0 To gvSalary.RowCount - 1
                    Dim strICodeGrid As String = clsCommon.myCstr(gvSalary.Rows(jj).Cells(colEmpCode).Value)
                    If clsCommon.CompairString(strICode, strICodeGrid) = CompairStringResult.Equal Then
                        gvSalary.CurrentRow = gvSalary.Rows(jj)

                        If dt.Rows.Count > 0 Then
                            TempTotal = 0
                            For Each dr As DataRow In dt.Rows
                                gvSalary.Rows(jj).Cells(clsCommon.myCstr(dr("LEAVE_CODE"))).Value = clsCommon.myCdbl(gvImport.Rows(ii).Cells(clsCommon.myCstr(dr("LEAVE_NAME"))).Value)
                                TempTotal += clsCommon.myCdbl(gvImport.Rows(ii).Cells(clsCommon.myCstr(dr("LEAVE_NAME"))).Value)
                            Next
                            gvSalary.Rows(jj).Cells(colTotal).Value = TempTotal
                        End If

                        Exit For
                    End If
                Next
            Next



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            Me.Controls.Remove(gvImport)
        End Try
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            transportSql.QuickExportToExcel(gvSalary, "", "Allotment of Leave")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
