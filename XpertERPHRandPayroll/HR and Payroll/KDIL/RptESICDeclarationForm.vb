'====shivani Tyagi
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports XpertERPEngine
Imports System.IO
Public Class RptESICDeclarationForm
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptESICDeclarationForm)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub fndEmployeeCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndEmployeeCode._MYValidating
        Try
            Dim qry As String = "select EMP_CODE , Emp_Name  from TSPL_Employee_Master   "
            fndEmployeeCode.Value = clsCommon.ShowSelectForm("TSPL_Employee_Master", qry, "EMP_CODE", " ISESI ='1'", fndEmployeeCode.Value, "", isButtonClicked)
            If clsCommon.myLen(fndEmployeeCode.Value) > 0 Then
                lblEmployeeName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Emp_Name  FROM TSPL_Employee_Master Where EMP_CODE='" + fndEmployeeCode.Value + "'"))
            Else
                lblEmployeeName.Text = ""
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadReport()
        Try
            Dim sQry As String
            sQry = "select ESI_NO  ,upper (Emp_Name) as Emp_Name ,FATHERS_NAME,Birth_date ,case when MARITAL_STATUS ='Single' then 'U' when MARITAL_STATUS='Married' then 'M'  end as MARITAL_STATUS,case when sex='MALE' then 'M' when Sex='FEMALE' then 'F' end as sex ,Add1,add2,Pin_Code ,Phone, DATENAME (MONTH ,CONVERT(date,Birth_date,103))as BirthMonth, DATENAME (DAY  ,CONVERT(date,Birth_date,103))as BirthDay,DATENAME (YEAR   ,CONVERT(date,Birth_date,103))as BirthYEAR      from TSPL_Employee_Master where ISESI ='1' and Emp_Name ='" & lblEmployeeName.Text & "' "
            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(sQry)
            If dtgv IsNot Nothing AndAlso dtgv.Rows.Count > 0 Then
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtgv, "crptESICForm-1Declaration", "ESIC Declaration Form")
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

    Private Sub RptESICDeclarationForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            SetUserMgmtNew()
            ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
            ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
