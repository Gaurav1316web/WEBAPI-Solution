'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmForm10PF
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

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Form5PF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmForm5_PF)
        'If Not (MyBase.isReadFlag) Then
        '    common.clsCommon.MyMessageBoxShow("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        'btnSave.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)


    End Sub

    Sub PrintData()
        Try


            Dim Qry As String = ""

            Qry = ""
            Qry += " SELECT '" & objCommonVar.CurrentCompanyName & "' AS Company_Name,'" & objCommonVar.CurrLocationName & "' AS Company_Address, "
            Qry += " '" & Me.txtFromPP.Value & "' as Pay_Period_Code,T1.EMP_CODE,T1.Emp_Name,T1.PF_NO,T1.SEX,T1.MARITAL_STATUS,"
            Qry += " (CASE WHEN T1.SEX='FeMale' and T1.MARITAL_STATUS='MARRIED' THEN T1.SPOUSE_NAME ELSE T1.FATHERS_NAME END ) AS FATHER_NAME,"
            Qry += " CONVERT(DATE,T1.Birth_date,103) AS DATE_OF_BIRTH,CONVERT(DATE,T1.RELIEVING_DATE,103) AS RELIEVING_DATE,LEAVING_REASON "
            Qry += " FROM TSPL_EMPLOYEE_MASTER T1 WHERE CONVERT(date,RELIEVING_DATE,103) BETWEEN "
            Qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + txtFromPP.Value + "')"
            Qry += " AND (SELECT DATE_TO FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + txtFromPP.Value + "')"
            Qry += " ORDER BY T1.EMP_CODE"
            Dim dtFinal As DataTable
            dtFinal = clsDBFuncationality.GetDataTable(Qry)
            Dim frmcrsytal As New frmCrystalReportViewer()
            frmcrsytal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crpForm10PF", "Form 10(PF)")


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



End Class
