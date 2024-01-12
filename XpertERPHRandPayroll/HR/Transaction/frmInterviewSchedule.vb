' ----------------- Created By Anubhooti On 12-Aug-2014 Against -------------------- '
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
Imports System.IO
Imports XpertERPEngine

Public Class FrmInterviewSchedule
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Dim isInsideLoadData As Boolean = False
    Dim isFlag As Boolean = False
#Region "Interview Details"
    Const ColSNo As String = "S No."
    Const ColIntrCode As String = "Interviewer Code"
    Const ColName As String = "Name"
    Const ColEmail As String = "Email"
    Const ColDeptCode As String = "Department Code"
    Const ColDeptName As String = "Department Name"
    Const ColLocationCode As String = "Location Code"
    Const ColLocationName As String = "Location Name"
    Const ColSubLocationCode As String = "Sub Location Code"
    Const ColDesignationCode As String = "Designation Code"
    Const ColDesignationName As String = "Designation Name"
    Const ColRoundCode As String = "Round Code"
    Const ColRoundName As String = "Round Name"
    Const ColStartTime As String = "Start Time"
    Const ColEndTime As String = "End Time"
    Const ColDesp As String = "Description"
#End Region
#Region "Functions"
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmInterviewSchedule)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnpost.Visible = MyBase.isPostFlag
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim SNoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SNoCode.FormatString = ""
        SNoCode.HeaderText = "S No."
        SNoCode.Name = ColSNo
        SNoCode.Width = 40
        SNoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(SNoCode)

        Dim IntrCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        IntrCode.FormatString = ""
        IntrCode.HeaderText = "Interviewer Code"
        IntrCode.Name = ColIntrCode
        IntrCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        IntrCode.TextImageRelation = TextImageRelation.TextBeforeImage
        IntrCode.Width = 150
        IntrCode.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(IntrCode)

        Dim Name As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Name.FormatString = ""
        Name.HeaderText = "Name"
        Name.Name = ColName
        Name.Width = 125
        Name.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Name)

        Dim Email As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Email.FormatString = ""
        Email.HeaderText = "Email"
        Email.Name = ColEmail
        Email.Width = 125
        Email.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(Email)

        Dim DeptCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DeptCode.FormatString = ""
        DeptCode.HeaderText = "Department Code"
        DeptCode.Name = ColDeptCode
        DeptCode.Width = 100
        DeptCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DeptCode)

        Dim DeptName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DeptName.FormatString = ""
        DeptName.HeaderText = "Department Name"
        DeptName.Name = ColDeptName
        DeptName.Width = 125
        DeptName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DeptName)

        Dim SubDes As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SubDes.FormatString = ""
        SubDes.HeaderText = "Designation Code"
        SubDes.Name = ColDesignationCode
        SubDes.Width = 100
        SubDes.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(SubDes)

        Dim DesgName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DesgName.FormatString = ""
        DesgName.HeaderText = "Designation Name"
        DesgName.Name = ColDesignationName
        DesgName.Width = 125
        DesgName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(DesgName)

        Dim LocCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LocCode.FormatString = ""
        LocCode.HeaderText = "Location Code"
        LocCode.Name = ColLocationCode
        LocCode.Width = 100
        'LocCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(LocCode)

        Dim LocName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LocName.FormatString = ""
        LocName.HeaderText = "Location Name"
        LocName.Name = ColLocationName
        LocName.Width = 125
        LocName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(LocName)

        'Dim SubLocCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'SubLocCode.FormatString = ""
        'SubLocCode.HeaderText = "Sub Location Code"
        'SubLocCode.Name = ColSubLocationCode
        'SubLocCode.Width = 100
        ''SubLocCode.ReadOnly = True
        'gv1.MasterTemplate.Columns.Add(SubLocCode)


        Dim RoundCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RoundCode.FormatString = ""
        RoundCode.HeaderText = "Round Code"
        RoundCode.Name = ColRoundCode
        RoundCode.Width = 100
        RoundCode.HeaderImage = Global.XpertERPHRandPayroll.My.Resources.Resources.search4
        RoundCode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv1.MasterTemplate.Columns.Add(RoundCode)

        Dim RoundName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        RoundName.FormatString = ""
        RoundName.HeaderText = "Round Name"
        RoundName.Name = ColRoundName
        RoundName.Width = 125
        RoundName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(RoundName)

        Dim StartTime As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        'StartTime.FormatString = "{0:dd/MM/yyyy hh:mm tt}"
        StartTime.Format = DateTimePickerFormat.Custom
        StartTime.FormatString = "{0:dd/MMM/yyyy hh:mm tt}"
        StartTime.CustomFormat = "dd/MMM/yyyy hh:mm tt"
        StartTime.HeaderText = "Start Time"
        StartTime.Name = ColStartTime
        StartTime.Width = 150
        gv1.MasterTemplate.Columns.Add(StartTime)

        Dim EndTime As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        'EndTime.FormatString = "{0:dd/MM/yyyy hh:mm tt}"
        EndTime.Format = DateTimePickerFormat.Custom
        EndTime.FormatString = "{0:dd/MMM/yyyy hh:mm tt}"
        EndTime.CustomFormat = "dd/MMM/yyyy hh:mm tt"
        EndTime.HeaderText = "End Time"
        EndTime.Name = ColEndTime
        EndTime.Width = 150
        gv1.MasterTemplate.Columns.Add(EndTime)

        Dim Desp As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Desp.FormatString = ""
        Desp.HeaderText = "Description"
        Desp.Name = ColDesp
        Desp.Width = 100
        Desp.MaxLength = 200
        gv1.MasterTemplate.Columns.Add(Desp)

        clsCustomFieldGrid.LoadBlankGrid(gv1, MyBase.ArrDetailFields)

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
    Function AllowToSave() As Boolean
        Try
            'Dim Roundarr As New List(Of String)
            If clsCommon.myLen(txtcode.Value) <= 0 Then
                txtcode.Focus()
                Throw New Exception("Applicant code can not be left blank")
            End If
            Dim GridRow As Integer = 0
            Dim LineNo As Integer = 0
            Dim Start_Date As DateTime
            Dim End_Date As DateTime
            For Each grow As GridViewRowInfo In gv1.Rows
                'If clsCommon.myLen(grow.Cells(ColSNo).Value) <= 0 Then
                '    Continue For
                'End If
                LineNo += 1
                If clsCommon.myLen(grow.Cells(ColIntrCode).Value) > 0 Then
                    If clsCommon.myLen(grow.Cells(ColLocationCode).Value) <= 0 Then
                        Throw New Exception("please fill location code at line no." + clsCommon.myCstr(LineNo) + "")
                    End If
                    'If clsCommon.myLen(grow.Cells(ColSubLocationCode).Value) <= 0 Then
                    '    Throw New Exception("please fill sub location code at line no." + LineNo + "")
                    'End If
                    If clsCommon.myLen(grow.Cells(ColRoundCode).Value) <= 0 Then
                        Throw New Exception("please fill round code at line no." + clsCommon.myCstr(LineNo) + "")
                    End If

                    If clsCommon.myLen(grow.Cells(ColStartTime).Value) <= 0 Then
                        Throw New Exception("please fill start time at line no." + clsCommon.myCstr(LineNo) + "")
                    End If
                    If clsCommon.myLen(grow.Cells(ColEndTime).Value) <= 0 Then
                        Throw New Exception("please fill end time at line no." + clsCommon.myCstr(LineNo) + "")
                    End If
                    Start_Date = clsCommon.myCDate(grow.Cells(ColStartTime).Value)
                    End_Date = clsCommon.myCDate(grow.Cells(ColEndTime).Value)
                    If Start_Date > End_Date Then
                        Throw New Exception("Start date time can not be greater than from End date time at line no." + clsCommon.myCstr(LineNo) + "")
                    End If
                    GridRow = GridRow + 1
                End If

            Next
            For i As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(i).Cells("Interviewer Code").Value) > 0 Then
                    Dim Round As String = clsCommon.myCstr(gv1.Rows(i).Cells("Round Code").Value)
                    Dim InterVw As String = clsCommon.myCstr(gv1.Rows(i).Cells("Interviewer Code").Value)
                    For j As Integer = i + 1 To gv1.Rows.Count - 1
                        Dim SecondRound As String = clsCommon.myCstr(gv1.Rows(j).Cells("Round Code").Value)
                        Dim SecInterVw As String = clsCommon.myCstr(gv1.Rows(j).Cells("Interviewer Code").Value)
                        If clsCommon.CompairString(Round, SecondRound) = CompairStringResult.Equal AndAlso clsCommon.CompairString(InterVw, SecInterVw) = CompairStringResult.Equal Then
                            Throw New Exception("Please check ! Duplicate rounds (" + Round + ") mapped with interviwer (" + InterVw + ") in grid")
                        End If
                    Next
                End If
            Next
            If GridRow <= 0 Then
                Throw New Exception("Enter at least one interviewer detail")
            End If
            'Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "code not found to delete", Me.Text)
            Exit Sub
        End If

        funDelete()
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (ClsInterviewSchedule.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Sub funReset()
        isNewEntry = True
        'txtcode.MyReadOnly = False
        txtcode.Value = Nothing
        txtcode.MyReadOnly = False
        'txtappcode.Value = Nothing
        UcRequisitionDetail1.AppCode = ""
        UcRequisitionDetail1.RefreshData()
        UsLock1.Status = ERPTransactionStatus.Pending
        txtcode.Focus()
        Me.gv1.Rows.Clear()
        Me.gv1.Rows.AddNew()
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnpost.Enabled = False
        btnDelete.Enabled = False
    End Sub
    Sub OpenInterviewerCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = ""
        '"select isnull(EMP_CODE,'') As [Code],isnull(Emp_Name,'') As [Name],isnull(EMail_ID,'') As Email,isnull(DEPARTMENT_CODE,'') As [DEPARTMENT CODE],isnull(Designation,'') As [Designation Code] from TSPL_EMPLOYEE_MASTER "
        'gv1.CurrentRow.Cells(ColIntrCode).Value = clsCommon.ShowSelectForm("Para", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(ColIntrCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(ColIntrCode).Value = clsEmployeeMaster.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(ColIntrCode).Value), isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(ColIntrCode).Value) > 0 Then
            qry = "select isnull(EMP_CODE,'') As [Interviewer Code],isnull(Emp_Name,'') As [Name],isnull(EMail_ID,'') As Email,isnull(DEPARTMENT_CODE,'') As [DEPARTMENT CODE],isnull(Designation,'') As [Designation Code] from TSPL_EMPLOYEE_MASTER  WHERE Emp_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColIntrCode).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'gv1.CurrentRow.Cells(ColIntrCode).Value = clsCommon.myCstr(dt.Rows(0)("Parameter_Code"))
                gv1.CurrentRow.Cells(ColName).Value = clsCommon.myCstr(dt.Rows(0)("Name"))
                gv1.CurrentRow.Cells(ColEmail).Value = clsCommon.myCstr(dt.Rows(0)("Email"))
                gv1.CurrentRow.Cells(ColDeptCode).Value = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT CODE"))
                gv1.CurrentRow.Cells(ColDeptName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT DEPARTMENT_NAME FROM TSPL_DEPARTMENT_MASTER WHERE DEPARTMENT_CODE  ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColDeptCode).Value) + "'"))
                gv1.CurrentRow.Cells(ColDesignationCode).Value = clsCommon.myCstr(dt.Rows(0)("Designation Code"))
                gv1.CurrentRow.Cells(ColDesignationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Designation_Desc FROM TSPL_DESIGNATION_MASTER WHERE Designation_id ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColDesignationCode).Value) + "'"))
            End If
        End If
    End Sub
    Sub OpenRoundCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "Select DISTINCT  TSPL_HR_ROUND_MASTER.Round_Code As Code,TSPL_HR_ROUND_MASTER.Round_Name AS [Round Name] From TSPL_HR_REQUISITION " & _
                "left outer join TSPL_HR_APPLICANT_ENTRY on TSPL_HR_REQUISITION.Requisition_Code =TSPL_HR_APPLICANT_ENTRY.Requisition_Code " & _
                "left outer join TSPL_HR_PROFILE_DETAIL on TSPL_HR_PROFILE_DETAIL.Profile_Code  =TSPL_HR_REQUISITION.Profile_Code  " & _
                "left outer join TSPL_HR_ROUND_MASTER on TSPL_HR_ROUND_MASTER.Round_Code   =TSPL_HR_PROFILE_DETAIL.Round_Code  "

        gv1.CurrentRow.Cells(ColRoundCode).Value = clsCommon.ShowSelectForm("HRRoundIS", qry, "Code", "TSPL_HR_APPLICANT_ENTRY.APPLICANT_CODE ='" + txtcode.Value + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(ColRoundCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(ColRoundCode).Value) > 0 Then
            qry = "Select Round_Name From TSPL_HR_ROUND_MASTER Where Round_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColRoundCode).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(ColRoundName).Value = clsCommon.myCstr(dt.Rows(0)("Round_Name"))
            End If
        End If
    End Sub
    Sub OpenLocationCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "" ' "Select Location_Code As Code ,Location_Desc As Desp,Add1 + '' + Add2 + '' + Add3 + Add4 As Address,City_Code As City,State  From TSPL_LOCATION_MASTER "
        'gv1.CurrentRow.Cells(ColLocationCode).Value = clsCommon.ShowSelectForm("Para", qry, "Code", "", clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationCode).Value), "Code", isButtonClick)
        gv1.CurrentRow.Cells(ColLocationCode).Value = clsLocation.getFinder("", clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationCode).Value), isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(ColLocationCode).Value) > 0 Then
            qry = "Select Location_Code As [Location Code] ,Location_Desc As [Location Name],Add1 + '' + Add2 + '' + Add3 + Add4 As Address,City_Code As City,State  From TSPL_LOCATION_MASTER   WHERE Location_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationCode).Value) + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ' gv1.CurrentRow.Cells(ColSubLocationCode).Value =""
                gv1.CurrentRow.Cells(ColLocationCode).Value = clsCommon.myCstr(dt.Rows(0)("Location Code"))
                gv1.CurrentRow.Cells(ColLocationName).Value = clsCommon.myCstr(dt.Rows(0)("Location Name"))
            End If
        End If
    End Sub
    Sub OpenSubLocCodeList(ByVal isButtonClick As Boolean)
        Dim qry As String = "Select Location_Code As Code ,Location_Desc As Desp,Add1 + '' + Add2 + '' + Add3 + Add4 As Address,City_Code As City,State  From TSPL_LOCATION_MASTER "
        gv1.CurrentRow.Cells(ColSubLocationCode).Value = clsCommon.ShowSelectForm("Para", qry, "Code", "Is_Sub_Location ='Y' And Main_Location_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationCode).Value) + "'", clsCommon.myCstr(gv1.CurrentRow.Cells(ColSubLocationCode).Value), "Code", isButtonClick)
        If clsCommon.myLen(gv1.CurrentRow.Cells(ColSubLocationCode).Value) > 0 Then
            qry = "Select Location_Code As Code ,Location_Desc As Desp,Add1 + '' + Add2 + '' + Add3 + Add4 As Address,City_Code As City,State  From TSPL_LOCATION_MASTER   WHERE Main_Location_Code ='" + clsCommon.myCstr(gv1.CurrentRow.Cells(ColLocationCode).Value) + "' AND Is_Sub_Location ='Y'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv1.CurrentRow.Cells(ColSubLocationCode).Value = clsCommon.myCstr(dt.Rows(0)("Code"))
                'gv1.CurrentRow.Cells(ColSubLocationName).Value = clsCommon.myCstr(dt.Rows(0)("Desp"))
            End If
        End If
    End Sub
    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            txtcode.MyReadOnly = True
            'btnSave.Enabled = True
            'btnDelete.Enabled = True
            'btnpost.Enabled = True
            isNewEntry = False
            '  Dim Parameter_Name As String
            'funReset()
            Dim obj As New ClsInterviewSchedule()
            obj = ClsInterviewSchedule.GetData(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Applicant_Code) > 0) Then

                isNewEntry = False
                btnsave.Text = "Update"
                btnDelete.Enabled = True
                btnpost.Enabled = True
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnpost.Enabled = False
                    btnDelete.Enabled = False
                End If
                UsLock1.Status = obj.Posted

                txtcode.Value = obj.Applicant_Code
                'txtappcode.Value = obj.Applicant_Code

                UcRequisitionDetail1.AppCode = txtcode.Value
                UcRequisitionDetail1.RefreshData()

                txtcode.MyReadOnly = True
                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadBlankGrid()
                    For Each objTr As ClsInterviewScheduleDetail In obj.ObjList
                        gv1.Rows.AddNew()
                        ii = ii + 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.S_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColIntrCode).Value = objTr.Interviewer_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDesp).Value = objTr.Schedule_Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColStartTime).Value = objTr.Start_Time
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEndTime).Value = objTr.End_Time
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColRoundCode).Value = objTr.Round_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationCode).Value = objTr.Location_Code
                        If clsCommon.myLen(objTr.Location_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Designation_Desc FROM TSPL_DESIGNATION_MASTER WHERE Designation_id ='" + clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationCode).Value) + "'"))
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationName).Value = ""
                        End If

                        ' gv1.Rows(gv1.Rows.Count - 1).Cells(ColSubLocationCode).Value = objTr.Sub_location_Code
                        Dim RoundName As String = clsDBFuncationality.getSingleValue("Select Round_Name From TSPL_HR_ROUND_MASTER Where Round_code ='" + objTr.Round_Code + "'")
                        If clsCommon.myLen(RoundName) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColRoundName).Value = RoundName
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColRoundName).Value = ""
                        End If

                        Dim qry As String = "select isnull(EMP_CODE,'') As [Interviewer Code],isnull(Emp_Name,'') As Name,isnull(EMail_ID,'') As Email,isnull(DEPARTMENT_CODE,'') As [DEPARTMENT CODE],isnull(Designation,'') AS Designation from TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + objTr.Interviewer_Code + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDeptCode).Value = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT CODE"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColName).Value = clsCommon.myCstr(dt.Rows(0)("Name"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColEmail).Value = clsCommon.myCstr(dt.Rows(0)("Email"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDesignationCode).Value = clsCommon.myCstr(dt.Rows(0)("Designation"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDesignationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Designation_Desc FROM TSPL_DESIGNATION_MASTER WHERE Designation_id ='" + clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(ColDesignationCode).Value) + "'"))
                        End If

                    Next
                End If
            Else
                isNewEntry = True
                'UcRequisitionDetail1.AppCode = ""
                'UcRequisitionDetail1.RefreshData()
                Me.gv1.Rows.Clear()
                Me.gv1.Rows.AddNew()
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Sub LoadDataForNav(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Try
            txtcode.MyReadOnly = True
            isNewEntry = False
           
            Dim obj As New ClsInterviewSchedule()
            obj = ClsInterviewSchedule.GetDataForNav(strCode, NavTyep)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Applicant_Code) > 0) Then

                isNewEntry = False
                btnsave.Text = "Update"
                btnDelete.Enabled = True
                btnpost.Enabled = True
                If obj.Posted = ERPTransactionStatus.Approved Then
                    btnsave.Enabled = False
                    btnpost.Enabled = False
                    btnDelete.Enabled = False
                End If
                UsLock1.Status = obj.Posted

                txtcode.Value = obj.Applicant_Code

                UcRequisitionDetail1.AppCode = txtcode.Value
                UcRequisitionDetail1.RefreshData()

                txtcode.MyReadOnly = True
                Dim ii As Int16 = 0
                If obj.ObjList IsNot Nothing AndAlso obj.ObjList.Count > 0 Then
                    LoadBlankGrid()
                    For Each objTr As ClsInterviewScheduleDetail In obj.ObjList
                        gv1.Rows.AddNew()
                        ii = ii + 1
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColSNo).Value = objTr.S_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColIntrCode).Value = objTr.Interviewer_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColDesp).Value = objTr.Schedule_Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColStartTime).Value = objTr.Start_Time
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColEndTime).Value = objTr.End_Time
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColRoundCode).Value = objTr.Round_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationCode).Value = objTr.Location_Code
                        If clsCommon.myLen(objTr.Location_Code) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Designation_Desc FROM TSPL_DESIGNATION_MASTER WHERE Designation_id ='" + clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationCode).Value) + "'"))
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColLocationName).Value = ""
                        End If

                        Dim RoundName As String = clsDBFuncationality.getSingleValue("Select Round_Name From TSPL_HR_ROUND_MASTER Where Round_code ='" + objTr.Round_Code + "'")
                        If clsCommon.myLen(RoundName) > 0 Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColRoundName).Value = RoundName
                        Else
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColRoundName).Value = ""
                        End If

                        Dim qry As String = "select isnull(EMP_CODE,'') As [Interviewer Code],isnull(Emp_Name,'') As Name,isnull(EMail_ID,'') As Email,isnull(DEPARTMENT_CODE,'') As [DEPARTMENT CODE],isnull(Designation,'') AS Designation from TSPL_EMPLOYEE_MASTER WHERE EMP_CODE='" + objTr.Interviewer_Code + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDeptCode).Value = clsCommon.myCstr(dt.Rows(0)("DEPARTMENT CODE"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColName).Value = clsCommon.myCstr(dt.Rows(0)("Name"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColEmail).Value = clsCommon.myCstr(dt.Rows(0)("Email"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDesignationCode).Value = clsCommon.myCstr(dt.Rows(0)("Designation"))
                            gv1.Rows(gv1.Rows.Count - 1).Cells(ColDesignationName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Designation_Desc FROM TSPL_DESIGNATION_MASTER WHERE Designation_id ='" + clsCommon.myCstr(gv1.Rows(gv1.Rows.Count - 1).Cells(ColDesignationCode).Value) + "'"))
                        End If

                    Next
                End If
            Else
                isNewEntry = True
                Me.gv1.Rows.Clear()
                Me.gv1.Rows.AddNew()
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub
    Public Sub Save()

        If AllowToSave() Then
            Dim arr As New List(Of ClsInterviewSchedule)
            Dim obj As New ClsInterviewSchedule()
            obj.Applicant_Code = txtcode.Value
            'obj.Applicant_Code = Me.txtappcode.Value

            obj.ObjList = New List(Of ClsInterviewScheduleDetail)
            For Each grow As GridViewRowInfo In gv1.Rows
                'If clsCommon.myLen(grow.Cells(ColSNo).Value) <= 0 Then
                '    Continue For
                'End If
                Dim objTr As New ClsInterviewScheduleDetail()
                If clsCommon.myLen(grow.Cells(ColIntrCode).Value) > 0 Then
                    'objTr.Schedule_Code = clsCommon.myCstr(Me.txtcode.Value)
                    objTr.S_No = clsCommon.myCstr(grow.Cells(ColSNo).Value)
                    objTr.Schedule_Description = clsCommon.myCstr(grow.Cells(ColDesp).Value)
                    objTr.Interviewer_Code = clsCommon.myCstr(grow.Cells(ColIntrCode).Value)
                    objTr.Location_Code = clsCommon.myCstr(grow.Cells(ColLocationCode).Value)
                    'objTr.Sub_location_Code = clsCommon.myCstr(grow.Cells(ColSubLocationCode).Value)
                    objTr.Round_Code = clsCommon.myCstr(grow.Cells(ColRoundCode).Value)
                    objTr.Start_Time = clsCommon.myCstr(grow.Cells(ColStartTime).Value)
                    objTr.End_Time = clsCommon.myCstr(grow.Cells(ColEndTime).Value)
                    obj.ObjList.Add(objTr)
                End If
            Next
            arr.Add(obj)
            'Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If (ClsInterviewSchedule.SaveData(arr)) Then
                If Not isFlag Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                    LoadData(obj.Applicant_Code, NavigatorType.Current)
                    btnsave.Text = "Update"
                    btnDelete.Enabled = True
                End If
            Else
                btnsave.Text = "Save"
                btnDelete.Enabled = False
            End If

        End If
    End Sub
    Sub PostData()
        Try
            Dim msg As String = ""
            Dim qry As String = ""
            Dim dt As DataTable = Nothing
            Dim Applicant_Code As String = ""
            isFlag = True
            If clsCommon.myLen(txtcode.Value) > 0 Then
                Applicant_Code = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) AS Applicant_Code from TSPL_HR_INTERVIEW_SCHEDULE where Applicant_Code='" + txtcode.Value + "'"))
                If Applicant_Code > 0 Then
                    If (myMessages.postConfirm()) Then
                        Save()
                        If (ClsInterviewSchedule.PostData(MyBase.Form_ID, txtcode.Value)) Then
                            msg = "Successfully Posted"
                            common.clsCommon.MyMessageBoxShow(Me, msg, Me.Text)
                            LoadData(txtcode.Value, NavigatorType.Current)
                        End If
                    End If
                Else
                    Throw New Exception("You cannot post this entry before entering applicant code")
                End If

            Else
                Throw New Exception("Applicant code not found to Post")
            End If
            'isFlag = False
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isFlag = False
        End Try
    End Sub
#End Region

    Private Sub gv1_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellDoubleClick
        'If gv1.Rows.Count > 0 Then
        '    Dim frm As FrmSelectEmployee = New FrmSelectEmployee()
        '    frm.strSegment = clsCommon.myCstr(gv1.CurrentRow.Cells(ColIntrCode).Value)
        '    frm.arrC = TryCast(gv1.CurrentRow.Tag, ArrayList)
        '    frm.ShowDialog()
        '    If Not frm.isCencelButtonClicked AndAlso frm.arrC IsNot Nothing Then
        '        gv1.CurrentRow.Tag = frm.arrC
        '        Dim strC As String = String.Empty
        '        Dim strD As String = String.Empty

        '        For Each Str As String In frm.arrC
        '            If clsCommon.myLen(strC) > 0 Then
        '                strC += ","
        '            End If
        '            strC += Str

        '        Next
        '        For Each StrN As String In frm.arrD
        '            If clsCommon.myLen(strD) > 0 Then
        '                strD += ","
        '            End If
        '            strD += StrN
        '        Next
        '        gv1.CurrentRow.Cells(ColIntrCode).Value = strC
        '        gv1.CurrentRow.Cells(ColName).Value = strD
        '    End If
        'End If
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If isInsideLoadData = False Then
                If e.Column Is gv1.Columns(ColIntrCode) Then
                    OpenInterviewerCodeList(False)
                ElseIf e.Column Is gv1.Columns(ColLocationCode) Then
                    OpenLocationCodeList(False)
                ElseIf e.Column Is gv1.Columns(ColSubLocationCode) Then
                    OpenSubLocCodeList(False)
                ElseIf e.Column Is gv1.Columns(ColRoundCode) Then
                    OpenRoundCodeList(False)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv1_CurrentColumnChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(ColSNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                'gv1.Rows(gvQualification.Rows.Count - 1).Cells(ColCourseType).Value = FullType
                gv1.CurrentRow = gv1.Rows(intCurrRow)
                'gv1.CurrentRow.Cells(ColSNo).Value = intCurrRow
            End If
        End If
    End Sub

    

    Private Sub gv1_UserAddedRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(ColSNo).Value = i + 1
            End If
        Next
    End Sub

   
  
    Private Sub FrmInterviewSchedule_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnnew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnpost.Enabled Then
            PostData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmInterviewSchedule_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()

        LoadBlankGrid()
        isNewEntry = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P Post Trasnaction")
        funReset()

        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub txtcode__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        'Try
        '    'If clsCommon.myLen(txtcode.Value) > 0 Then
        '    '    UcRequisitionDetail1.AppCode = txtcode.Value
        '    '    UcRequisitionDetail1.RefreshData()
        '    LoadData(txtcode.Value, NavType)
        '    'End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
        Dim obj As New ClsApplicantEntry()

        Try
            Dim qst As String = "select count(*) from TSPL_HR_APPLICANT_ENTRY where APPLICANT_CODE='" + txtcode.Value + "' AND Posted =1"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtcode.MyReadOnly = False
            Else
                txtcode.MyReadOnly = True
            End If
            Dim ISAppCode As Integer
            Dim AppCode As String = clsCommon.myCstr(txtcode.Value)
            obj = ClsApplicantEntry.GetPostedData(AppCode, NavType)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.APPLICANT_CODE) > 0) Then
                ISAppCode = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select COUNT(*) From TSPL_HR_INTERVIEW_SCHEDULE WHERE APPLICANT_CODE='" + obj.APPLICANT_CODE + "'"))
                txtcode.Value = clsCommon.myCstr(obj.APPLICANT_CODE)
                UcRequisitionDetail1.AppCode = obj.APPLICANT_CODE
                UcRequisitionDetail1.RefreshData()
                If ISAppCode > 0 Then
                    LoadDataForNav(txtcode.Value, NavType)
                Else
                    isNewEntry = True
                    Me.gv1.Rows.Clear()
                    Me.gv1.Rows.AddNew()
                    btnsave.Text = "Save"
                    btnsave.Enabled = True
                    btnpost.Enabled = False
                    btnDelete.Enabled = False
                    UsLock1.Status = ERPTransactionStatus.Pending
                End If

            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtcode._MYValidating
        'Dim str As String = "SELECT APPLICANT_CODE AS Code  FROM TSPL_HR_INTERVIEW_SCHEDULE Where Applicant_Code ='" + txtcode.Value + "' "
        'Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        'If no = 0 AndAlso isButtonClicked = False Then
        '    txtcode.MyReadOnly = False
        'Else
        '    txtcode.MyReadOnly = True
        'End If
        'If txtcode.MyReadOnly OrElse isButtonClicked Then
        '    Dim qry As String = "SELECT APPLICANT_CODE AS Code, Requisition_Code,Applicant_Date,Applicant_Date_Of_Birth,First_Name + '' + Middle_Name + '' + Last_Name As Name ,TELEPHONE_NO,Email  FROM TSPL_HR_APPLICANT_ENTRY  "
        '    txtcode.Value = clsCommon.ShowSelectForm("ISApp", qry, "Code", "Posted = 1", txtcode.Value, "", isButtonClicked)
        '    If clsCommon.myLen(txtcode.Value) > 0 Then
        '        Dim AppCode As String = clsCommon.myCstr(txtcode.Value)
        '        LoadData(txtcode.Value, NavigatorType.Current)
        '        txtcode.Value = AppCode
        '        UcRequisitionDetail1.AppCode = AppCode
        '        UcRequisitionDetail1.RefreshData()
        '    Else
        '        UcRequisitionDetail1.AppCode = ""
        '        UcRequisitionDetail1.RefreshData()
        '        funReset()
        '    End If
        'End If
        'Dim qry As String = "SELECT APPLICANT_CODE AS Code, Requisition_Code,Applicant_Date,Applicant_Date_Of_Birth,First_Name + '' + Middle_Name + '' + Last_Name As Name ,TELEPHONE_NO,Email  FROM TSPL_HR_APPLICANT_ENTRY  "
        'txtcode.Value = clsCommon.ShowSelectForm("ISApp", qry, "Code", "Posted = 1", txtcode.Value, "", isButtonClicked)
        txtcode.Value = ClsApplicantEntry.GetFinder(" POSTED =1 and Short=1", txtcode.Value, isButtonClicked)
        If clsCommon.myLen(txtcode.Value) > 0 Then
            Dim AppCode As String = clsCommon.myCstr(txtcode.Value)
            LoadData(txtcode.Value, NavigatorType.Current)
            txtcode.Value = AppCode
            UcRequisitionDetail1.AppCode = AppCode
            UcRequisitionDetail1.RefreshData()
        Else
            UcRequisitionDetail1.AppCode = ""
            UcRequisitionDetail1.RefreshData()
            funReset()
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub btnnew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funReset()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Private Sub txtappcode__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean)

    End Sub

   
    
    Private Sub btnpost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnpost.Click
        If clsCommon.myLen(txtcode.Value) > 0 Then
            'If (myMessages.postConfirm()) Then
            PostData()
            'End If
        Else
            clsCommon.MyMessageBoxShow(Me, "code not found to post", Me.Text)
        End If
    End Sub

    Private Sub gv1_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
End Class
