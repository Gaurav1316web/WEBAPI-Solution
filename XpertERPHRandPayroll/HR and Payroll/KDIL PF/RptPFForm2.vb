'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports XpertERPEngine
Public Class RptPFForm2
    Inherits FrmMainTranScreen
    'Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptPFForm2)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub fndEmployeeCode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub fndDivisionCode__MY_click(sender As Object, e As EventArgs) Handles fndDivisionCode._My_Click
        Try
            Dim qry As String = "select DEVISION_CODE as Code, DEVISION_NAME as Name, DESCRIPTION as Description from TSPL_DEVISION_MASTER"
            fndDivisionCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulDev", qry, "Code", "Name", fndDivisionCode.arrValueMember, fndDivisionCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadReport()
        Try
            '===update by preeti gupta Against ticket No [BM00000008642]=======
            ''changes by shivani against[BM00000008704]
            Dim sQry As String
            '        sQry = "select upper (Emp_Name) as Emp_Name ,FATHERS_NAME,Birth_date , MARITAL_STATUS, sex ,Add1,add2,Joining_date  ,Phone,BANK_ACC_NO   from TSPL_Employee_Master where ISpf ='1' and Emp_Name ='" & lblEmployeeName.Text & "' "
            sQry = "Select * from (select upper (Emp_Name) as Emp_Name ,FATHERS_NAME,Birth_date , MARITAL_STATUS, sex ,Add1,add2,Joining_date  ,Phone,EMP_CODE,TSPL_Employee_Master.PF_No,BANK_ACC_NO, 'Common' as Col1 from TSPL_Employee_Master where ISpf ='1' "
            If FndEmployeeMult.arrValueMember IsNot Nothing AndAlso FndEmployeeMult.arrValueMember.Count > 0 Then
                sQry += " and EMP_CODE in (" + clsCommon.GetMulcallString(FndEmployeeMult.arrValueMember) + "))  XXX LEFT OUTER JOIN "
            Else
                sQry += " and EMP_CODE not in (" + clsCommon.GetMulcallString(FndEmployeeMult.arrValueMember) + "))  XXX LEFT OUTER JOIN "
            End If
            sQry += " (Select 'Common' as col1, '1' as Col2 UNION Select 'Common' as col1, '2' as Col2) YYY ON YYY.col1=XXX.Col1 "
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQry)
            If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptPFForm2", "PF Form2")
            Else
                clsCommon.MyMessageBoxShow(Me, "Data Not Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        LoadReport()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub RptPFForm2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ' ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            'ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndLocationCode__MY_click(sender As Object, e As EventArgs) Handles fndLocationCode._My_Click
        Try
            'fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
            Dim whrcls As String = ""
            Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER "
            If clsCommon.myLen(whrcls) <= 0 Then
                whrcls = " LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY )"
            End If
            fndLocationCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulLoc", qry, "Code", "Name", fndLocationCode.arrValueMember, fndLocationCode.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FndEmployeeMult__My_Click(sender As Object, e As EventArgs) Handles FndEmployeeMult._My_Click
        Try
            Dim qry As String = "select EMP_CODE , Emp_Name as Name,DEVISION_CODE,TSPL_Employee_Master.Location_Code from TSPL_Employee_Master left join TSPL_Location_Master on  TSPL_Location_Master.Location_code=TSPL_Employee_Master.Location_Code where ISpf ='1' and TSPL_LOCATION_MASTER.Location_Code in (" & clsCommon.GetMulcallString(fndLocationCode.arrValueMember) & ") "
            FndEmployeeMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "EMP_CODE", "Name", FndEmployeeMult.arrValueMember, FndEmployeeMult.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
