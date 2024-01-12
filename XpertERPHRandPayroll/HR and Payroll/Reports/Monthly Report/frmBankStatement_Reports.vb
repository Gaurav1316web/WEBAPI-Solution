'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmBankStatement_Reports
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
#Region "Variable"
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Dim Qry As String

#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        PrintData()
    End Sub

    Private Sub frmBankStatement_Reports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBankStatement_Reports)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            'Me.Close()
            'Exit Function
        End If
        '' Anubhooti 23-July-2014 (BM00000003141)
        'btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub LoadData()
        qry = ""
        qry = " SELECT DISTINCT TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE, TSPL_EMPLOYEE_MASTER .Emp_Name FROM TSPL_GENERATE_SALARY "
        qry += " LEFT OUTER JOIN TSPL_GENERATE_SALARY_ATTENDANCE  ON TSPL_GENERATE_SALARY_ATTENDANCE  .SALARY_GENERATION_CODE = TSPL_GENERATE_SALARY .SALARY_GENERATION_CODE "
        Qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE= TSPL_EMPLOYEE_MASTER .EMP_CODE "
        Qry += " Left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code= TSPL_GENERATE_SALARY.Location_Code"
        Qry += " where TSPL_GENERATE_SALARY.PAY_PERIOD_CODE ='" + txtFromPP.Value + "' "
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            Qry += " and TSPL_LOCATION_MASTER.Location_Code='" & FndLocationCode.Value & "' "
        End If
        Qry += " ORDER BY TSPL_GENERATE_SALARY_ATTENDANCE .EMP_CODE "
     
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "EMP_CODE"
        cbgLocation.DisplayMember = "Emp_Name"
    End Sub
    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)
        LoadData()
      
    End Sub

    Sub PrintData()
        Try
            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select Pay Period.", Me.Text)
                Return
            End If

            If cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "Please Select AtLeast Single Employee Or Select All", Me.Text)
                Return
            End If

            Dim Qry As String = ""
            Qry = ""

            Qry += " select T1.EMP_CODE ,T3.Emp_Name ,T3.BANK_CODE,T3.BANK_ACC_NO , T1.NET_SALARY , T2.PAY_PERIOD_CODE, T4.DESCRIPTION AS Bank_Name, "
            Qry += " (ISNULL(T4.ADD1,'')+' '+ ISNULL(T4.ADD2,'')+' '+ ISNULL(T4.ADD3,'')+' '+ ISNULL(T4.ADD4,'')+' '+ ISNULL(T4.CITY,'')+' '+ ISNULL(T4.STATE ,'')+' '+ ISNULL(T4.COUNTRY,'')+' '+ ISNULL(T4.POSTAL,'')) AS Bank_Add, "
            Qry += " T4.PHONE ,T4.FAX, '" + objCommonVar.CurrentCompanyName + "' as Company_Name,Logo_Img,Location_Desc ,T2.Location_Code  from TSPL_GENERATE_SALARY_ATTENDANCE T1 "
            Qry += " left outer join TSPL_GENERATE_SALARY T2 on  T2 .SALARY_GENERATION_CODE = T1.SALARY_GENERATION_CODE "
           
            Qry += " LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER T3 ON T3.EMP_CODE = T1.EMP_CODE "
            Qry += " left Outer join tspl_company_Master  on tspl_company_Master.Comp_Code =T3.Comp_Code  "

            Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = T2.LOCATION_CODE "
            Qry += " left outer join TSPL_BANK_MASTER T4 on T3.BANK_CODE  = T4.BANK_CODE "
            Qry += " WHERE T1.EMP_CODE  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") AND T2.PAY_PERIOD_CODE = '" + txtFromPP.Value + "' "
            Dim DT As DataTable = clsDBFuncationality.GetDataTable(Qry)

            If DT.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            Else
                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, DT, "crptBankStatement", "Employee Pay Slip Report")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles FndLocationCode._MYValidating
        FndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.FndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(FndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & FndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If
        Loaddata()
    End Sub

End Class
