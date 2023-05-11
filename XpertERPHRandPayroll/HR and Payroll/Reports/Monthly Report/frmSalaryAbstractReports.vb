'--01/08/2013--form Add By- Pradeep Sharma ---------
Imports common
Imports System.Data
Imports System.Drawing.Image
Imports System.Byte
Imports System.Data.SqlClient
Imports XpertERPEngine

Public Class frmSalaryAbstractReport
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

    Private Sub frmSalaryAbstractReports_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnSave, "Press Alt+P for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")

    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSalaryAbstractReport)
        'If Not (MyBase.isReadFlag) Then
        '    Throw New Exception("Permission Denied")
        '    Me.Close()
        '    Exit Sub
        'End If
        '' Anubhooti 23-July-2014 (BM00000003141)
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
            
            'Dim strQry As String
            'Dim Qry As String = ""
            'If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            '    strQry = "max(Location_Desc)"
            'Else
            '    strQry = "'All'"
            'End If

            If clsCommon.myLen(txtFromPP.Value) <= 0 Then
                Throw New Exception("Please select pay period")
            End If

            Dim LocAddress As String = ""
            Dim LocationFirstTime As Integer = 0
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count = 1 Then
                LocationFirstTime += 1
                LocAddress = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT Add1 + ' ' + Add2 + ' ' + Add3 + ' ' + add4 As [Address] FROM TSPL_LOCATION_MASTER WHERE Location_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"))
            Else
                LocAddress = objCommonVar.CurrentCompanyName
            End If

            Qry = ""
            Qry += " select Logo_Img,* from("
            Qry += " SELECT '" & LocAddress & "' as  Location_Desc,T2.PAY_PERIOD_CODE,T3.PAY_HEAD_NAME AS EARNING_HEAD,SUM(T1.ACTUAL_AMOUNT) AS EARNING_AMOUNT  ,max(TSPL_EMPLOYEE_MASTER.Comp_Code)as  Comp_Code FROM TSPL_GENERATE_SALARY_PAYHEADS T1 INNER JOIN "
            Qry += " TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE "
            Qry += " INNER JOIN TSPL_PAYHEAD_MASTER T3 ON T1.PAY_HEAD_CODE=T3.PAY_HEAD_CODE "
            Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =T2.LOCATION_CODE"
            Qry += " Left Outer Join TSPL_EMPLOYEE_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE =t1.EMP_CODE "
            Qry += " WHERE T2.PAY_PERIOD_CODE='" & txtFromPP.Value & "'"
            'If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            '    Qry += " and TSPL_LOCATION_MASTER.Location_Code='" & fndLocationCode.Value & "' "
            'End If
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                Qry += " and TSPL_LOCATION_MASTER.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
            End If
            If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                Qry += " and TSPL_EMPLOYEE_MASTER.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
            End If
            Qry += " GROUP BY T2.PAY_PERIOD_CODE,T3.PAY_HEAD_NAME "
            Qry += " )as t   left join TSPL_company_Master on TSPL_company_Master.comp_Code=t.Comp_Code"

            Dim Hader_Info As DataTable = clsDBFuncationality.GetDataTable(Qry)
         

            If Hader_Info.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found")
            Else
                Dim dtFinal As DataTable = New DataTable
                dtFinal.Columns.Add("Company_Name", GetType(String))
                dtFinal.Columns.Add("Logo_Img", GetType(Byte()))
                dtFinal.Columns.Add("PAY_PERIOD_CODE", GetType(String))
                dtFinal.Columns.Add("Location_Desc", GetType(String))
                dtFinal.Columns.Add("EARNING_HEAD", GetType(String))
                dtFinal.Columns.Add("EARNING_AMOUNT", GetType(Double))
                dtFinal.Columns.Add("DEDUCTION_HEAD", GetType(String))
                dtFinal.Columns.Add("DEDUCTION_AMOUNT", GetType(Double))
                dtFinal.AcceptChanges()

                Dim DrFinal As DataRow = dtFinal.NewRow()
                Dim DrDT As DataRow
                Dim DrDT1 As DataRow

              

                Qry = ""
                Qry += " select Logo_Img,* from("
                Qry += " SELECT '" & LocAddress & "' as Location_Desc,T2.PAY_PERIOD_CODE,T3.PAY_HEAD_NAME AS EARNING_HEAD,SUM(T1.ACTUAL_AMOUNT) AS EARNING_AMOUNT,max(TSPL_EMPLOYEE_MASTER.Comp_Code)as  Comp_Code FROM TSPL_GENERATE_SALARY_PAYHEADS T1 INNER JOIN "
                Qry += " TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE"
                Qry += " INNER JOIN TSPL_PAYHEAD_MASTER T3 ON T1.PAY_HEAD_CODE=T3.PAY_HEAD_CODE "
                Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =T2.LOCATION_CODE"
                Qry += " Left Outer Join TSPL_EMPLOYEE_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE =T1.EMP_CODE "
                Qry += " WHERE T2.PAY_PERIOD_CODE='" & txtFromPP.Value & "' AND T3.ISEARNING=1 "
                'If clsCommon.myLen(fndLocationCode.Value) > 0 Then
                '    Qry += " and TSPL_LOCATION_MASTER.Location_Code='" & fndLocationCode.Value & "' "
                'End If
                If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_LOCATION_MASTER.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
                End If
                If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_EMPLOYEE_MASTER.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
                End If
                Qry += " GROUP BY T2.PAY_PERIOD_CODE,T3.PAY_HEAD_NAME"
                Qry += " )as t   left join TSPL_company_Master on TSPL_company_Master.comp_Code=t.Comp_Code"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

                

                Qry = ""
                Qry += " select Logo_Img,* from("
                Qry += " SELECT '" & LocAddress & "' as Location_Desc,T2.PAY_PERIOD_CODE,T3.PAY_HEAD_NAME AS DEDUCTION_HEAD,SUM(T1.ACTUAL_AMOUNT) AS DEDUCTION_AMOUNT,max(TSPL_EMPLOYEE_MASTER.Comp_Code)as  Comp_Code FROM TSPL_GENERATE_SALARY_PAYHEADS T1 INNER JOIN "
                Qry += " TSPL_GENERATE_SALARY T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE"
                Qry += " INNER JOIN TSPL_PAYHEAD_MASTER T3 ON T1.PAY_HEAD_CODE=T3.PAY_HEAD_CODE "
                Qry += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =T2.LOCATION_CODE"
                Qry += " Left Outer Join TSPL_EMPLOYEE_MASTER On TSPL_EMPLOYEE_MASTER.EMP_CODE =T1.EMP_CODE "
                Qry += " WHERE T2.PAY_PERIOD_CODE='" & txtFromPP.Value & "' AND T3.ISEARNING=0 "
                ' If clsCommon.myLen(fndLocationCode.Value) > 0 Then
                '     Qry += "   and TSPL_LOCATION_MASTER.Location_Code='" & fndLocationCode.Value & "' "
                'End If
                If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_LOCATION_MASTER.LOCATION_CODE  in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ") "
                End If
                If txtDivisionMult.arrValueMember IsNot Nothing AndAlso txtDivisionMult.arrValueMember.Count > 0 Then
                    Qry += " and TSPL_EMPLOYEE_MASTER.DEVISION_CODE in (" & clsCommon.GetMulcallString(txtDivisionMult.arrValueMember) & " )"
                End If
                Qry += " GROUP BY T2.PAY_PERIOD_CODE,T3.PAY_HEAD_NAME"
                Qry += " )as t   left join TSPL_company_Master on TSPL_company_Master.comp_Code=t.Comp_Code"
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(Qry)


                Dim Counter As Int16 = dt.Rows.Count
                If dt1.Rows.Count > dt.Rows.Count Then
                    Counter = dt1.Rows.Count
                End If

                For ii As Int16 = 0 To Counter

                    If dt.Rows.Count > ii Then
                        DrDT = dt.Rows(ii)
                    Else
                        DrDT = dt.NewRow()
                    End If

                    If dt1.Rows.Count > ii Then
                        DrDT1 = dt1.Rows(ii)
                    Else
                        DrDT1 = dt1.NewRow()
                    End If

                    DrFinal = dtFinal.NewRow()

                    DrFinal.Item("PAY_PERIOD_CODE") = txtFromPP.Value
                    DrFinal.Item("Company_Name") = objCommonVar.CurrentCompanyName
                    If clsCommon.myLen(DrDT("Logo_Img")) > 0 Then
                        DrFinal.Item("Logo_Img") = DrDT("Logo_Img")


                    End If
                    DrFinal.Item("Location_Desc") = clsCommon.myCstr(DrDT("Location_Desc"))
                    If clsCommon.myLen(DrDT("EARNING_HEAD")) > 0 Then
                        DrFinal.Item("EARNING_HEAD") = clsCommon.myCstr(DrDT("EARNING_HEAD"))
                        DrFinal.Item("EARNING_AMOUNT") = clsCommon.myCdbl(DrDT("EARNING_AMOUNT"))
                    End If

                    If clsCommon.myLen(DrDT1("DEDUCTION_HEAD")) > 0 Then
                        DrFinal.Item("DEDUCTION_HEAD") = clsCommon.myCstr(DrDT1("DEDUCTION_HEAD"))
                        DrFinal.Item("DEDUCTION_AMOUNT") = clsCommon.myCdbl(DrDT1("DEDUCTION_AMOUNT"))
                    End If
                    dtFinal.Rows.Add(DrFinal)
                Next

                dtFinal.AcceptChanges()

                Dim frmcrystal As New frmCrystalReportViewer()
                frmcrystal.funreport(CrystalReportFolder.HRPayroll, dtFinal, "crpSalaryAbstractReport", "Salary Abstract Report")

            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtSalStruct__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtSalStruct._MYValidating
        Dim qry As String = "SELECT SALARY_STRUCTURE_CODE as Code,SALARY_STRUCTURE_NAME FROM TSPL_SALARY_STRUCTURE  "
        txtSalStruct.Value = clsCommon.ShowSelectForm("TSPL_SALARY_STRUCTURE", qry, "Code", "", txtSalStruct.Value, "", isButtonClicked)
        lblSalStruct.Text = clsSalaryStructure.GetData(txtSalStruct.Value, Nothing).SALARY_STRUCTURE_NAME

    End Sub

    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
        Else
            lblLocationName.Text = ""
        End If

    End Sub

    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Dim qry As String = " select Location_Code as Code,Location_Desc as [Name] from TSPL_LOCATION_MASTER where LOCATION_CODE IN (select DISTINCT LOCATION_CODE from TSPL_GENERATE_SALARY where PAY_PERIOD_CODE='" & txtFromPP.Value & "') "
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMSAbs", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
    End Sub

    Private Sub txtDivisionMult__My_Click(sender As Object, e As EventArgs) Handles txtDivisionMult._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        txtDivisionMult.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMAbs", qry, "Code", "Name", txtDivisionMult.arrValueMember, txtDivisionMult.arrDispalyMember)
    End Sub
End Class
