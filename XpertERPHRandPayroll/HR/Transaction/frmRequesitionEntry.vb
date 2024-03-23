Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports common
Imports System.Net.Mail.MailAddress
Imports System.Text.RegularExpressions
Imports XpertERPEngine
Public Class FrmRequesitionEntry
    '--Preeti gupta-ticket no[BM00000003465,BM00000003708,BM00000003730,BM00000003708]
    Inherits FrmMainTranScreen
    Dim isnewentry As Boolean
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isFlag As Boolean = False
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()
    Private Sub butnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnsave.Click
        savedata()
    End Sub
    Function allowtosave()
        
        ' Ticket No : TEC/19/03/19-000456 

        If clsCommon.myLen(clsCommon.myCstr(txtdescription.Text)) <= 0 Or clsCommon.myLen(clsCommon.myCstr(txtdescription.Text)) > 100 Then
            myMessages.blankValue(Me, "Description ", Me.Text)
            txtdescription.Focus()
            txtdescription.Select()
            Errorcontrol.SetError(txtdescription, "Description ")
            Return False
        Else
            Errorcontrol.ResetError(txtdescription)
        End If

        If txtprofile.Value = "" Then
            myMessages.blankValue(Me, "Profile ", Me.Text)
            txtprofile.Focus()
            txtprofile.Select()
            Errorcontrol.SetError(txtprofile, "Profile")
            Return False
        Else
            Errorcontrol.ResetError(txtprofile)
        End If

        If Txtemployeetype.Value = "" Then
            myMessages.blankValue(Me, "Employee Type ", Me.Text)
            Txtemployeetype.Focus()
            Txtemployeetype.Select()
            Errorcontrol.SetError(Txtemployeetype, "Employee Type")
            Return False
        Else
            Errorcontrol.ResetError(Txtemployeetype)
        End If

        If clsCommon.myLen(txtDepartmentCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Department ", Me.Text)
            txtDepartmentCode.Focus()
            txtDepartmentCode.Select()
            Errorcontrol.SetError(Txtlocation, "Department")
            Return False
        Else
            Errorcontrol.ResetError(txtDepartmentCode)
        End If


        If Txtlocation.Value = "" Then
            myMessages.blankValue(Me, "Location ", Me.Text)
            Txtlocation.Focus()
            Txtlocation.Select()
            Errorcontrol.SetError(Txtlocation, "Location")
            Return False
        Else
            Errorcontrol.ResetError(Txtlocation)
        End If
        'If Txtdesignation.Value = "" Then
        '    myMessages.blankValue("Designation ")
        '    Txtdesignation.Focus()
        '    Txtdesignation.Select()
        '    Errorcontrol.SetError(Txtdesignation, "Designation")
        '    Return False
        'Else
        '    Errorcontrol.ResetError(Txtemployeetype)
        'End If
        If clsCommon.myLen(Txtjobtitle.Value) <= 0 Then
            myMessages.blankValue(Me, "Job title", Me.Text)
            Txtjobtitle.Focus()
            Txtjobtitle.Select()
            Errorcontrol.SetError(Txtjobtitle, "Job title")
            Return False
        Else
            Errorcontrol.ResetError(Txtjobtitle)
        End If

        If clsCommon.myLen(TxtIndustry.Value) <= 0 Then
            myMessages.blankValue(Me, "Job title", Me.Text)
            TxtIndustry.Focus()
            TxtIndustry.Select()
            Errorcontrol.SetError(TxtIndustry, "Industry")
            Return False
        Else
            Errorcontrol.ResetError(TxtIndustry)
        End If

        If clsCommon.myLen(TxtVertical.Value) <= 0 Then
            myMessages.blankValue(Me, "Vertical", Me.Text)
            TxtVertical.Focus()
            TxtVertical.Select()
            Errorcontrol.SetError(TxtVertical, "Vertical")
            Return False
        Else
            Errorcontrol.ResetError(TxtVertical)
        End If


        If (clsCommon.CompairString(ddhiringtype.Text, "") = CompairStringResult.Equal) Then
            myMessages.blankValue(Me, "Hiring Type ", Me.Text)
            ddhiringtype.Focus()
            ddhiringtype.Select()
            Errorcontrol.SetError(ddhiringtype, "Hiring Type")
            Return False
        Else
            Errorcontrol.ResetError(ddhiringtype)
        End If

        If ddctcrange.Text = "" Then
            myMessages.blankValue(Me, "CTC Range ", Me.Text)
            ddctcrange.Focus()
            ddctcrange.Select()
            Errorcontrol.SetError(ddctcrange, "CTC Range")
            Return False
        Else
            Errorcontrol.ResetError(ddctcrange)
        End If

        If ddgender.Text = "" Then
            myMessages.blankValue(Me, "Gender ", Me.Text)
            ddgender.Focus()
            ddgender.Select()
            Errorcontrol.SetError(ddgender, "Gender")
            Return False
        Else
            Errorcontrol.ResetError(ddgender)
        End If

        If txtNoPost.Text = "" Then
            myMessages.blankValue(Me, "No. of post ", Me.Text)
            txtNoPost.Focus()
            txtNoPost.Select()
            Errorcontrol.SetError(txtNoPost, "No. of post")
            Return False
        Else
            Errorcontrol.ResetError(txtNoPost)
        End If
        
       
        'If Txtrecommendedby.Value = "" Then
        '    clsCommon.MyMessageBoxShow("Recommended by cannot be blank")
        '    Txtrecommendedby.Focus()
        '    Return False
        'End If
        'If txtDepartmentCode.Value = "" Then
        '    clsCommon.MyMessageBoxShow("Department  cannot be blank")
        '    txtDepartmentCode.Focus()
        '    Return False

        'End If
        'If Txtlocation.Value = "" Then
        '    clsCommon.MyMessageBoxShow("Location cannot be blank")
        '    Txtlocation.Focus()
        '    Return False
        'End If
        'If txtsublocationCode.Value = "" Then
        '    clsCommon.MyMessageBoxShow("Sub location cannot be blank")
        '    Txtjobtitle.Focus()
        '    Return False
        'End If
        'If Txtdesignation.Value = "" Then
        '    clsCommon.MyMessageBoxShow("Designation cannot be blank")
        '    txtsublocationCode.Focus()
        '    Return False
        'End If
        'If Txtjobtitle.Value = "" Then
        '    clsCommon.MyMessageBoxShow("Job Title cannot be blank")
        '    Txtjobtitle.Focus()
        '    Return False
        'End If

        'If Txtemployeetype.Value = "" Then
        '    clsCommon.MyMessageBoxShow("Employee type cannot be blank")
        '    Txtemployeetype.Focus()
        '    Return False
        'End If
        'If ddminexpmonth.Text = "" Then
        '    clsCommon.MyMessageBoxShow("Minimum Experience Month cannot be blank")
        '    Return False
        'End If

        'If ddmaxexpmonth.Text = "" Then
        '    clsCommon.MyMessageBoxShow("Maxmiunm Experience Month cannot be blank")
        '    Return False
        'End If

        'If ddagemonth.Text = "" Then
        '    clsCommon.MyMessageBoxShow("Age Month cannot be blank")
        '    Return False
        'End If

        'If ddctcrange.Text = "" Then
        '    clsCommon.MyMessageBoxShow("CTC Range cannot be blank")
        '    Return False
        'End If
        'If ddhiringtype.Text = "" Then
        '    clsCommon.MyMessageBoxShow("Hiring Type cannot be blank")

        '    Return False
        'End If
        'If ddminexp.Text = "" Then
        '    clsCommon.MyMessageBoxShow("Min Experience cannot be blank")

        '    Return False
        'End If
        'If ddmaxexp.Text = "" Then
        '    clsCommon.MyMessageBoxShow("Max Experience cannot be blank")

        '    Return False
        'End If
        'If ddgender.Text = "" Then
        '    clsCommon.MyMessageBoxShow("Gender cannot be blank")

        '    Return False
        'End If

        Return True
    End Function
    Sub savedata()
        Try
            If (allowtosave()) Then
                'cbgQual.Update()
                butnsave.Focus()

              


                Dim obj As clsrequisitionentryDeatils
                Dim arr As New List(Of clsrequisitionentryDeatils)
                Dim entry As String
                Dim count As Integer = 0
                Dim i As Integer = 0
                Dim qry As String = "select count(*) from tspl_hr_requisition  where Requisition_Code ='" + txtcode.Value + "'"
                count = clsDBFuncationality.getSingleValue(qry)
                If count = 0 Then
                    isnewentry = True
                Else
                    isnewentry = False

                End If


                Dim Req As New clsrequisitionentry
                Req.code = clsCommon.myCstr(txtcode.Value)
                Req.Req_Date = txtReqDateDate.Text
                Req.description = clsCommon.myCstr(txtdescription.Text)
                Req.initiated_by = clsCommon.myCstr(txtinitiated.Text)
                Req.profile = txtprofile.Value
                Req.recommended_by = Txtrecommendedby.Value
                Req.Department = txtDepartmentCode.Value
                Req.location = Txtlocation.Value
                'Req.sub_location = clsCommon.myCstr(txtsublocationCode.Value)
                Req.designation = clsCommon.myCstr(Txtdesignation.Value)
                Req.jobtitle = Txtjobtitle.Value
                Req.Emp_type = Txtemployeetype.Value
                Req.no_of_post = clsCommon.myCdbl(txtNoPost.Text)
                Req.ctc_range = clsCommon.myCstr(ddctcrange.Text)
                Req.Hiring_type = ddhiringtype.Text
                Req.minimumexperience = clsCommon.myCdbl(ddminexp.Text)
                Req.maximumexperience = clsCommon.myCdbl(ddmaxexp.Text)
                Req.MaxExpMonth = clsCommon.myCdbl(ddmaxexpmonth.Text)
                Req.MinExpMonth = clsCommon.myCdbl(ddminexpmonth.Text)
                Req.age_yr = clsCommon.myCdbl(ddageyr.Text)
                Req.age_month = clsCommon.myCdbl(ddagemonth.Text)
                Req.gender = clsCommon.myCstr(ddgender.Text)

                Req.Industry_Code = clsCommon.myCstr(TxtIndustry.Value)
                Req.Vertical_Code = clsCommon.myCstr(TxtVertical.Value)
                Req.ApprovedStatus = clsCommon.myCstr(IIf(UsLock1.Status = ERPTransactionStatus.Approved, 1, 0))
                Req.ClosedStatus = clsCommon.myCstr(IIf(UsLock1.Status = ERPTransactionStatus.Close, 1, 0))
                UsLock1.Status = ERPTransactionStatus.Close
                If ((cbgQual.CheckedValue.Count) <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select atleast one qualification ", Me.Text)
                    cbgQual.Focus()
                    Return
                End If

                If cbgQual.CheckedValue.Count > 0 Then
                    'Dim objct As New clsrequisitionentryDeatils
                    'Dim arrlst As ArrayList = cbgQual.CheckedValue
                    'Dim arr As New List(Of clsrequisitionentryDeatils)
                    'For Each Str As String In arrlst

                    '    objct.QualificationCode = Str
                    '    arr.Add(objct)
                    'Next
                    For i = 0 To cbgQual.CheckedValue.Count - 1
                        obj = New clsrequisitionentryDeatils()
                        obj.QualificationCode = clsCommon.myCstr(cbgQual.CheckedValue(i))
                        arr.Add(obj)
                    Next
                    If clsrequisitionentry.savedata(Req, isnewentry, arr) Then
                        clsCommon.MyMessageBoxShow(Me,"Data saved successfully", Me.Text)
                        entry = Req.code
                        getdata(Req.code, NavigatorType.Current)
                        butnsave.Text = "Update"
                        butndelete.Enabled = True
                    Else
                        butnsave.Text = "Save"
                        butndelete.Enabled = False

                    End If

                End If
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try

    End Sub
    Sub getdata(ByVal entry As String, ByVal navigatortype As NavigatorType)
        Try
            Dim obj As clsrequisitionentry = clsrequisitionentry.getdata(entry, navigatortype)
            If obj IsNot Nothing Then
                Dim ProfileName As String = ""
                Dim Department = ""
                Dim EmployeeType As String = ""
                Dim RecommendedBy As String = ""
                Dim LocationName As String = ""
                Dim DesignationaName As String = ""
                Dim JobTitleName As String = ""

                txtcode.Value = obj.code
                txtdescription.Text = obj.description
                txtReqDateDate.Text = obj.Req_Date
                txtinitiated.Text = obj.initiated_by
                txtprofile.Value = obj.profile
                txtProfileName.Text = obj.profile
               
                RecommendedBy = clsDBFuncationality.getSingleValue("select Emp_Name from tspl_employee_master where EMP_CODE='" + obj.recommended_by + "'")
                Department = clsDBFuncationality.getSingleValue("select DEPARTMENT_NAME from TSPL_DEPARTMENT_MASTER where DEPARTMENT_CODE='" + obj.Department + "'")
                LocationName = clsDBFuncationality.getSingleValue("select Location_Desc   from TSPL_LOCATION_MASTER where Location_Code='" + obj.location + "'")
                DesignationaName = clsDBFuncationality.getSingleValue("select Designation_Desc from TSPL_DESIGNATION_MASTER where Designation_id='" + obj.designation + "'")
                JobTitleName = clsDBFuncationality.getSingleValue("select Job_Title  from TSPL_HR_JOB_TITLE where Job_Title_Code='" + obj.jobtitle + "'")
                EmployeeType = clsDBFuncationality.getSingleValue("select Name from TSPL_HR_EMP_TYPE_MASTER where Code='" + obj.Emp_type + "'")

                Txtrecommendedby.Value = obj.recommended_by
                txtRecommendedName.Text = RecommendedBy

                txtDepartmentCode.Value = obj.Department
                txtDepartmentName.Text = Department

                Txtlocation.Value = obj.location
                txtLocationName.Text = LocationName

                Txtemployeetype.Value = obj.Emp_type
                txtEmployeeName.Text = EmployeeType

                'txtsublocationCode.Value = obj.sub_location
                'txtSubLocationName.Text = LocationName

                Txtdesignation.Value = obj.designation
                txtDesignationName.Text = DesignationaName

                Txtjobtitle.Value = obj.jobtitle
                txtjobTitleCode.Text = JobTitleName

                txtNoPost.Text = obj.no_of_post
                ddctcrange.Text = obj.ctc_range
                ddhiringtype.Text = obj.Hiring_type
                ddmaxexp.Text = obj.maximumexperience
                ddminexp.Text = obj.minimumexperience
                ddmaxexpmonth.Text = obj.MaxExpMonth
                ddminexpmonth.Text = obj.MinExpMonth
                ddgender.Text = obj.gender
                ddageyr.Text = obj.age_yr

                ddagemonth.Text = obj.age_month

               


                TxtIndustry.Value = obj.Industry_Code
                If clsCommon.myLen(obj.Industry_Code) > 0 Then
                    LblIndustry.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Name FROM TSPL_HR_INDUSTRY_TYPE_MASTER WHERE Code='" & TxtIndustry.Value & "'"))
                Else
                    LblIndustry.Text = ""
                End If
                TxtVertical.Value = obj.Vertical_Code
                If clsCommon.myLen(obj.Vertical_Code) > 0 Then
                    LblVertical.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Name FROM TSPL_HR_VERTICAL_MASTER WHERE Code='" & TxtVertical.Value & "'"))
                Else
                    LblVertical.Text = ""
                End If

                'Dim list As ArrayList = New ArrayList()
                'If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then
                '    For Each objtr As clsrequisitionentryDeatils In obj.arr
                '        list = (objtr.QualificationCode)
                '    Next

                'End If
                cbgQual.CheckedValue = obj.arrqul
                'Dim ClosedStatus, ApprovedStatus As String
                'ClosedStatus = clsDBFuncationality.getSingleValue("select Closed_Status from TSPL_HR_REQUISITION where Requisition_Code='" + obj.code + "'")
                'ApprovedStatus = clsDBFuncationality.getSingleValue("select Approved_Status from TSPL_HR_REQUISITION where Requisition_Code='" + obj.code + "'")
                'If clsCommon.myCstr(ApprovedStatus) = True Or clsCommon.myCstr(ClosedStatus) = True Then
                '    butnsave.Enabled = False
                '    butndelete.Enabled = False
                '    'UsLock1.Status = ERPTransactionStatus.Pending
                'End If
                '' Anubhooti (Below Code shifted, if obj is empty)
                txtcode.MyReadOnly = True
                butnsave.Text = "Update"
                butnsave.Enabled = True
                butndelete.Enabled = True
                UsLock1.Status = IIf(obj.ApprovedStatus = "True", ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
                If obj.ApprovedStatus = "True" Then
                    butnsave.Enabled = False
                    butndelete.Enabled = False
                End If
                If obj.ClosedStatus = "True" Then
                    UsLock1.Status = ERPTransactionStatus.Close
                    butnsave.Enabled = False
                    butndelete.Enabled = False
                End If
            End If

            '' Anubhooti (Code Commented if obj is empty)

            'txtcode.MyReadOnly = True
            'butnsave.Text = "Update"
            'butnsave.Enabled = True
            'butndelete.Enabled = True

            'UsLock1.Status = IIf(obj.ApprovedStatus = "True", ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            'If obj.ApprovedStatus = "True" Then
            '    butnsave.Enabled = False
            '    butndelete.Enabled = False
            'End If
            'If obj.ClosedStatus = "True" Then
            '    UsLock1.Status = ERPTransactionStatus.Close
            '    butnsave.Enabled = False
            '    butndelete.Enabled = False
            'End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Sub funDelete()
        Try
            If (myMessages.deleteConfirm()) Then
                If (clsrequisitionentry.DeleteData(txtcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    resetdata()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "code not found to delete", Me.Text)
            Exit Sub
        End If

        funDelete()
    End Sub
       Sub resetdata()
        txtcode.Value = ""
        txtLocationName.Text = ""
        txtdescription.Text = ""
        Txtrecommendedby.Value = ""
        txtRecommendedName.Text = ""
        txtprofile.Value = ""
        txtProfileName.Text = ""
        Txtemployeetype.Value = ""
        txtEmployeeName.Text = ""
        txtDepartmentCode.Value = ""
        txtDepartmentName.Text = ""
        Txtlocation.Value = ""
        Txtlocation.Name = ""
        'txtSubLocation.Text = ""
        txtSubLocationName.Text = ""
        Txtdesignation.Value = ""
        txtDesignationName.Text = ""
        Txtjobtitle.Value = ""
        txtjobTitleCode.Text = ""
        txtNoPost.Text = ""
        ddctcrange.SelectedValue = "0-5000"
        ddhiringtype.SelectedValue = "New"
        ddmaxexp.Text = ""
        ddminexp.Text = ""
        ddmaxexpmonth.Text = ""
        ddminexpmonth.Text = ""
        ddgender.SelectedValue = "Male"
        ddagemonth.Text = ""
        ddageyr.Text = ""
        'cbgQual.DataSource = Nothing
        txtcode.MyReadOnly = False
        butnsave.Text = "Save"
        cbgQual.UnCheckedAll()
        txtsublocationCode.Value = ""
        butndelete.Enabled = False
        butnsave.Enabled = True
        TxtIndustry.Value = ""
        TxtVertical.Value = ""
        LblIndustry.Text = ""
        LblVertical.Text = ""
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub
    Public Sub LoadDepartment()
        Dim strquery As String = "Select Qualification_Code as [Code], Qualification_Name as [Name] from TSPL_HR_QUALIFICATION_MASTER"
        cbgQual.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgQual.ValueMember = "Code"
        cbgQual.DisplayMember = "Description"
    End Sub

    Private Sub butndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butndelete.Click
        DeleteData()

    End Sub

    Private Sub butnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnreset.Click
        resetdata()
    End Sub

    Private Sub butnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butnclose.Click
        Me.Close()
    End Sub

    Private Sub txtprofile__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtprofile._MYValidating
        Dim qry As String

        qry = "select Profile_Code As Code,Profile_Name As [Profile Name] from TSPL_HR_PROFILE_MASTER "
        txtprofile.Value = clsCommon.ShowSelectForm("ReqPro", qry, "Code", "", txtprofile.Value, "", isButtonClicked)

        If clsCommon.myLen(txtprofile.Value) > 0 Then
            txtProfileName.Text = clsDBFuncationality.getSingleValue("select Profile_Name  from TSPL_HR_PROFILE_MASTER where Profile_Code ='" + txtprofile.Value + "' ")
        Else
            txtProfileName.Text = ""
        End If
    End Sub
    Private Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.RequesitionEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        butnsave.Visible = MyBase.isModifyFlag
        butndelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub Txtrecommendedby__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles Txtrecommendedby._MYValidating
        'Dim qry As String

        'qry = "select * from tspl_employee_master "
        'Txtrecommendedby.Value = clsCommon.ShowSelectForm("Employee_Master", qry, "EMP_CODE", "", Txtrecommendedby.Value, "", isButtonClicked)
        Txtrecommendedby.Value = clsEmployeeMaster.getFinder("", Txtrecommendedby.Value, isButtonClicked)

        If clsCommon.myLen(Txtrecommendedby.Value) > 0 Then
            txtRecommendedName.Text = clsDBFuncationality.getSingleValue("select Emp_Name  from tspl_employee_master where EMP_CODE ='" + Txtrecommendedby.Value + "' ")
        Else
            txtRecommendedName.Text = ""
        End If
    End Sub

    Private Sub Txtdepartment__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDepartmentCode._MYValidating
        'Dim qry As String

        ' qry = "select * from TSPL_DEPARTMENT_MASTER "
        'txtDepartmentCode.Value = clsCommon.ShowSelectForm("Department_Master", qry, "DEPARTMENT_CODE", "", txtDepartmentCode.Value, "", isButtonClicked)
        txtDepartmentCode.Value = clsDepartmentMaster.getFinder("", txtDepartmentCode.Value, isButtonClicked)

        If clsCommon.myLen(txtDepartmentCode.Value) > 0 Then
            txtDepartmentName.Text = clsDBFuncationality.getSingleValue("select DEPARTMENT_NAME  from TSPL_DEPARTMENT_MASTER where DEPARTMENT_CODE ='" + txtDepartmentCode.Value + "' ")
        Else
            txtDepartmentName.Text = ""
        End If
    End Sub

    Private Sub Txtlocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles Txtlocation._MYValidating
        'Dim qry As String

        ' qry = "select Location_Code ,Location_Desc   from TSPL_LOCATION_MASTER  "
        'Txtlocation.Value = clsCommon.ShowSelectForm("Location_master", qry, "Location_Code", "", Txtlocation.Value, "", isButtonClicked)
        Txtlocation.Value = clsLocation.getFinder(" Is_Sub_Location ='N' AND Is_Section ='N' AND Rejected_Type ='N' ", Txtlocation.Value, isButtonClicked)

        If clsCommon.myLen(Txtlocation.Value) > 0 Then
            txtLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code ='" + Txtlocation.Value + "' ")
        Else
            txtLocationName.Text = ""
            txtsublocationCode.Value = ""
            txtSubLocationName.Text = ""
        End If
    End Sub

    Private Sub Txtdesignation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles Txtdesignation._MYValidating
        Dim qry As String

        qry = "select Designation_id AS Code,Designation_Desc AS [Designation Description] from TSPL_DESIGNATION_MASTER "
        Txtdesignation.Value = clsCommon.ShowSelectForm("ReqDesig", qry, "Code", "", Txtdesignation.Value, "", isButtonClicked)

        If clsCommon.myLen(Txtdesignation.Value) > 0 Then
            txtDesignationName.Text = clsDBFuncationality.getSingleValue("select Designation_Desc from TSPL_DESIGNATION_MASTER where Designation_id ='" + Txtdesignation.Value + "' ")
        Else
            txtDesignationName.Text = ""
        End If
    End Sub

    Private Sub Txtjobtitle__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles Txtjobtitle._MYValidating
        Dim qry As String

        qry = "select Job_Title_Code As Code ,Job_Title As [Job Title] from TSPL_HR_JOB_TITLE  "
        Txtjobtitle.Value = clsCommon.ShowSelectForm("ReqJT", qry, "Code", "", Txtjobtitle.Value, "", isButtonClicked)

        If clsCommon.myLen(Txtjobtitle.Value) > 0 Then
            txtjobTitleCode.Text = clsDBFuncationality.getSingleValue("select Job_Title from TSPL_HR_JOB_TITLE where Job_Title_Code ='" + Txtjobtitle.Value + "' ")
        Else
            txtjobTitleCode.Text = ""
        End If
    End Sub

    Private Sub Txtemployeetype__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles Txtemployeetype._MYValidating
        Dim qry As String

        qry = "select Code,Name from TSPL_HR_EMP_TYPE_MASTER "
        Txtemployeetype.Value = clsCommon.ShowSelectForm("ReqEmpT", qry, "Code", "", Txtemployeetype.Value, "", isButtonClicked)

        If clsCommon.myLen(Txtemployeetype.Value) > 0 Then
            txtEmployeeName.Text = clsDBFuncationality.getSingleValue("select Name from TSPL_HR_EMP_TYPE_MASTER where Code ='" + Txtemployeetype.Value + "' ")
        Else
            txtEmployeeName.Text = ""
        End If
    End Sub

    Private Sub txtsublocationCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtsublocationCode._MYValidating
        'Dim qry As String

        ' qry = "select Location_Code ,Location_Desc   from TSPL_LOCATION_MASTER  "
        'txtsublocationCode.Value = clsCommon.ShowSelectForm("Loc_Master", qry, "Location_Code", "Location_Code ='" + Txtlocation.Value + "'AND Is_Sub_Location ='Y'", txtsublocationCode.Value, "", isButtonClicked)
        txtsublocationCode.Value = clsLocation.getFinder(" Is_Sub_Location ='Y' And Main_Location_Code ='" + Txtlocation.Value + "'", txtsublocationCode.Value, isButtonClicked)

        If clsCommon.myLen(txtsublocationCode.Value) > 0 Then
            txtSubLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code ='" + txtsublocationCode.Value + "' ")
        Else
            txtSubLocationName.Text = ""
        End If
    End Sub

    Private Sub FrmRequesitionEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(butnsave, "Press Alt+S for Save/Update Transaction")
        ButtonToolTip.SetToolTip(butndelete, "Press Alt+D Delete Transaction")
        ButtonToolTip.SetToolTip(butnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(butnreset, "Press Alt+N Adding New Transaction")
        txtReqDateDate.Text = clsCommon.GETSERVERDATE()
        txtinitiated.Text = objCommonVar.CurrentUserCode
        LoadDepartment()
        resetdata()
    End Sub
    Private Sub txtcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        getdata(txtcode.Value, NavType)
    End Sub
    Private Sub txtcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcode._MYValidating
        'Dim Qry As String

        'Qry = "select * from TSPL_HR_REQUISITION "
        'txtcode.Value = clsCommon.ShowSelectForm("req_Entry", Qry, "Requisition_Code", "", txtcode.Value, "", isButtonClicked)

        Dim str As String = "select count(*) from TSPL_HR_REQUISITION where Requisition_Code ='" + txtcode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtcode.MyReadOnly = False
        Else
            txtcode.MyReadOnly = True
        End If

        If txtcode.MyReadOnly OrElse isButtonClicked Then
            txtcode.Value = clsrequisitionentry.getFinder("", txtcode.Value, isButtonClicked)
            getdata(txtcode.Value, NavigatorType.Current)
        End If
        'If clsCommon.myLen(txtcode.Value) > 0 Then
        '    getdata(txtcode.Value, NavigatorType.Current)
        'End If
    End Sub
    Private Sub FrmRequesitionEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso butnreset.Enabled Then
            resetdata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso butnsave.Enabled Then
            savedata()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso butndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
    Private Sub ddminexp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ddminexp.KeyPress

        If Not e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub
    Private Sub ddminexpmonth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ddminexpmonth.KeyPress
        If Not e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub
    Private Sub ddmaxexp_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ddmaxexp.KeyPress
        If Not e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub
    Private Sub ddmaxexpmonth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ddmaxexpmonth.KeyPress
        If Not e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub
    Private Sub ddageyr_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ddageyr.KeyPress
        If Not e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub
    Private Sub ddagemonth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ddagemonth.KeyPress
        If Not e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub
    Private Sub txtNoPost_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNoPost.KeyPress
        If Not e.KeyChar <> "-" Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtIndustry__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtIndustry._MYValidating
        TxtIndustry.Value = ClsHRIndustryType.getFinder("", TxtIndustry.Value, isButtonClicked)
        TxtVertical.Value = ""
        LblVertical.Text = ""
        If clsCommon.myLen(TxtIndustry.Value) > 0 Then
            LblIndustry.Text = clsDBFuncationality.getSingleValue("select Name from TSPL_HR_INDUSTRY_TYPE_MASTER where Code ='" + TxtIndustry.Value + "' ")
        Else
            LblIndustry.Text = ""
        End If
    End Sub

    Private Sub TxtVertical__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles TxtVertical._MYValidating
        If clsCommon.myLen(TxtIndustry.Value) > 0 Then
            TxtVertical.Value = ClsHRVerticalMaster.getFinder(" Industry_Code = '" & TxtIndustry.Value & "'", TxtVertical.Value, isButtonClicked)
            If clsCommon.myLen(TxtVertical.Value) > 0 Then
                LblVertical.Text = clsDBFuncationality.getSingleValue("SELECT Name FROM TSPL_HR_VERTICAL_MASTER WHERE Code ='" + TxtVertical.Value + "' ")
            Else
                LblVertical.Text = ""
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "Please select industry code first", Me.Text)
        End If
    End Sub
End Class
