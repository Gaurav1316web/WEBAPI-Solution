'--25/06/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Public Class frmGeneralHolidays
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Save()
    End Sub

    Public Sub Save()
        If AllowToSave() Then
            Dim obj As New clsGeneralHolidays()
            obj.Code = txtCode.Value
            obj.Description = txtDescription.Text
            obj.ATTENDANCE_CODE = txtAttendCode.Value
            obj.HOLIDAY_DATE = dtpHolidayDate.Value
            If chkIsNationalNational.Checked Then
                obj.NATIONAL_HOLIDAY = 1
            Else
                obj.NATIONAL_HOLIDAY = 0
            End If
            obj.Location_Code = fndLocation.Value
            obj.Division = FndDivision.Value

            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)
                LoadData(obj.Code, NavigatorType.Current)
                'Else
                '    common.clsCommon.MyMessageBoxShow("This '" & obj.Code & "' already exist ")
            End If

        End If
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        txtCode.MyReadOnly = True
        btnsave.Enabled = True
        btndelete.Enabled = True
        Dim obj As New clsGeneralHolidays()
        obj = clsGeneralHolidays.GetData(strCode, NavTyep)
        If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Code) > 0) Then
            funReset()
            isNewEntry = False
            btnsave.Text = "Update"
            txtCode.Value = obj.Code
            txtDescription.Text = obj.Description
            txtAttendCode.Value = obj.ATTENDANCE_CODE
            lblAttendName.Text = obj.ATTENDANCE_NAME
            dtpHolidayDate.Value = obj.HOLIDAY_DATE
            fndLocation.Value = obj.Location_Code
            FndDivision.Value = obj.Division
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc  from TSPL_LOCATION_MASTER where Location_Code ='" & fndLocation.Value & "'")
            lblDivisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME  from TSPL_DEVISION_MASTER where DEVISION_CODE ='" & FndDivision.Value & "'")
            If obj.NATIONAL_HOLIDAY = 1 Then
                chkIsNationalNational.Checked = True
            Else
                chkIsNationalNational.Checked = False
            End If
        End If

    End Sub

    Function AllowToSave() As Boolean
        'If clsCommon.myLen(txtCode.Value) <= 0 Then
        '    myMessages.blankValue("Code")
        '    txtCode.Focus()
        '    Return False
        'Else
        If clsCommon.myLen(txtAttendCode.Value) <= 0 Then
            myMessages.blankValue(Me, "Attendence Code", Me.Text)
            txtAttendCode.Focus()
            Return False
        ElseIf clsCommon.myLen(clsCommon.myCstr(dtpHolidayDate.Value)) <= 0 Then
            myMessages.blankValue(Me, "Holiday Date ", Me.Text)
            dtpHolidayDate.Focus()
            Return False
        End If
        Dim strchk As String = "select GHOLIDAY_CODE from TSPL_GENERAL_HOLIDAYS where ATTENDANCE_CODE ='" + txtAttendCode.Value + "'  and HOLIDAY_DATE= '" + clsCommon.GetPrintDate(dtpHolidayDate.Value, "dd/MMM/yyyy") + "' and GHOLIDAY_CODE <> '" + txtCode.Value + "' "
        Dim GHOLIDAY_CODE As String = clsDBFuncationality.getSingleValue(strchk)
        If clsCommon.myLen(GHOLIDAY_CODE) > 0 Then
            clsCommon.MyMessageBoxShow(Me, "Selected Date is Already included in General Holiday Code : " + GHOLIDAY_CODE + " for the same Attendance. Date can save on Single Attendance Only Once.", Me.Text)
            Return False
        End If

        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtCode.Value) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "You Cannot Delete Record", Me.Text)
            Exit Sub
        End If
        'Dim discCode As String
        'discCode = clsDBFuncationality.getSingleValue("select Discount_Code  from TSPL_SHIPMENT_DETAILS  where Discount_Code ='" & txtCode.Value & "'")
        'If clsCommon.myLen(discCode) > 0 Then
        '    common.clsCommon.MyMessageBoxShow("This record can't be deleted.It is used in another process")
        '    Exit Sub
        'End If

        funDelete()
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
                If (clsGeneralHolidays.DeleteData(txtCode.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ", Me.Text)
                    funReset()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtcode.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub frmGeneralHolidays_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        dtpHolidayDate.Value = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmGeneralHolidays)
        If Not (MyBase.isReadFlag) Then

            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        btndelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        funReset()
    End Sub

    Sub funReset()
        isNewEntry = True
        txtCode.MyReadOnly = False
        txtCode.Value = Nothing
        txtCode.Focus()
        txtAttendCode.Value = Nothing
        lblAttendName.Text = ""
        txtDescription.Text = ""
        fndLocation.Value = ""
        FndDivision.Value = ""
        lblLocationName.Text = ""
        lblDivisionName.Text = ""
        dtpHolidayDate.Value = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy")
        chkIsNationalNational.Checked = False
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        funClose()

    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCode._MYValidating
        Dim str As String = "select count(*) from TSPL_GENERAL_HOLIDAYS where GHOLIDAY_CODE ='" + txtCode.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtCode.MyReadOnly = False
            'txtCode.Value = ""
            '' common.clsCommon.MyMessageBoxShow("Value doesn't exist ")
        Else
            txtCode.MyReadOnly = True
        End If
        If txtCode.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select GHOLIDAY_CODE as Code, ATTENDANCE_CODE as 'Attendance Code', DESCRIPTION as Description from TSPL_GENERAL_HOLIDAYS"
            txtCode.Value = clsCommon.ShowSelectForm("GEN_HOLIDAYS", qry, "Code", "", txtCode.Value, "GHOLIDAY_CODE", isButtonClicked)
            If txtCode.Value <> "" Then
                LoadData(txtCode.Value, NavigatorType.Current)
            Else
                funReset()
            End If
        End If
    End Sub

    Private Sub txtCode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtCode._MYNavigator
        Try
            LoadData(txtCode.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub frmGeneralHolidays_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnNew.Enabled Then
            funReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            Save()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub txtAttendCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtAttendCode._MYValidating
        Dim qry As String = " select ATTENDANCE_CODE As Code, ATTENDANCE_NAME as Name from TSPL_ATTENDANCE_MASTER "
        txtAttendCode.Value = clsCommon.ShowSelectForm("ATT_FIND", qry, "Code", "", txtAttendCode.Value, "", isButtonClicked)
        lblAttendName.Text = clsAttendanceMaster.GetName(txtAttendCode.Value)
    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        fndLocation.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocation.Value, isButtonClicked)
        lblLocationName.Text = clsLocation.GetName(fndLocation.Value, Nothing)
    End Sub

    Private Sub FndDivision__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndDivision._MYValidating
        FndDivision.Value = clsDevisionMaster.getFinder("", Me.FndDivision.Value, isButtonClicked)
        lblDivisionName.Text = clsDevisionMaster.GetName(FndDivision.Value, Nothing)
    End Sub
End Class
