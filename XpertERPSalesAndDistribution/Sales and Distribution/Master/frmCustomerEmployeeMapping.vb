
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports System.Globalization
Imports common
Imports XpertERPEngine
Public Class frmCustomerEmployeeMapping
    Inherits FrmMainTranScreen
    Dim strCustVenPass As String = ""
    Private Sub frmCustomerEmployeeMapping_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub fndcustomer1__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndcustomer._MYValidating
        Dim str As String = "select count(*) from tspl_customer_master where cust_code ='" + fndcustomer.Value + "' "
        Dim no As Integer = CInt(connectSql.RunScalar(str))
        Dim no1 As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndEmployee.Value = ""
            txtempdesc.Text = ""
            btnsave.Text = "Save"
            btndelete.Enabled = False

            fndcustomer.MyReadOnly = False

            fndcustomer.Value = ""
        Else
            fndcustomer.MyReadOnly = True
        End If
        If fndcustomer.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select tspl_customer_master.cust_code as Code,tspl_customer_master.customer_name as Description, TSPL_CUSTOMER_EMPLOYEE_MAPPING.Employee_Code,tspl_employee_master.Emp_Name
from tspl_customer_master
left outer join TSPL_CUSTOMER_EMPLOYEE_MAPPING on TSPL_CUSTOMER_EMPLOYEE_MAPPING.Cust_Code=tspl_customer_master.Cust_Code
left outer join tspl_employee_master on tspl_employee_master.EMP_CODE=TSPL_CUSTOMER_EMPLOYEE_MAPPING.Employee_Code
"
            fndcustomer.Value = clsCommon.ShowSelectForm("CustomrMastrFND", qry, "Code", " tspl_customer_master.Status ='N' ", fndcustomer.Value, "Code", isButtonClicked)
            txtcustdes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + fndcustomer.Value + "'"))
        End If
        funfill()
    End Sub



    Private Sub fndEmployee__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndEmployee._MYValidating
        'Dim qry As String = "select vendor_code as Code,vendor_name as Description from tspl_vendor_master where Vendor_Code not in (select Vendor_Code from TSPL_CUSTOMER_VENDOR_MAPPING )"
        Dim qry As String = "select Emp_code as Code,emp_name as Description from tspl_employee_master  "

        Dim whr As String = "  emp_Code not in (select Employee_Code from TSPL_CUSTOMER_EMPLOYEE_MAPPING ) and tspl_employee_master.emp_status='Inactive'"
        fndEmployee.Value = clsCommon.ShowSelectForm("VendorMastrFND", qry, "Code", whr, fndEmployee.Value, "Code", isButtonClicked)
        txtempdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select emp_name from tspl_employee_master where emp_code='" + fndEmployee.Value + "' and tspl_employee_master.emp_Status='Inactive'"))

    End Sub

    'Private Sub fndcustomer1__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles fndcustomer._MYNavigator
    '    Dim qst As String = "select cust_code as Code,customer_name as Description from tspl_customer_master  where tspl_customer_master.Status ='N'  "
    '    Select Case NavigatorType
    '        Case NavigatorType.Current
    '            '  qst += "and assign_to='" + txtassign.Value + "' "
    '            ' qst += "and job_code in ('" + txtcode1.Value + "')"
    '        Case NavigatorType.Next
    '            qst += "and cust_code in (select min(cust_code) from tspl_customer_master where cust_code>'" + fndcustomer.Value + "' ) "
    '        Case NavigatorType.First
    '            qst += "and cust_code in (select MIN(cust_code) from tspl_customer_master )"
    '        Case NavigatorType.Last
    '            qst += "and cust_code in (select Max(cust_code) from tspl_customer_master )"
    '        Case NavigatorType.Previous
    '            qst += "and cust_code in (select max(cust_code) from tspl_customer_master where cust_code<'" + fndcustomer.Value + "'  )"
    '    End Select
    '    ' fun_gridfill()
    '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
    '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
    '        fndcustomer.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
    '        txtcustdes.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
    '    End If
    '    'TextChanged()
    '    If fndcustomer.Value IsNot Nothing Then
    '        btndelete.Enabled = True
    '    Else
    '        btndelete.Enabled = False

    '    End If
    '    funfill()
    'End Sub
    Public Sub funfill()
        Try
            fndEmployee.Value = ""
            txtempdesc.Text = ""
            'Dim str As String = "select TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code,tspl_vendor_master.vendor_name from TSPL_CUSTOMER_VENDOR_MAPPING left outer join tspl_vendor_master on TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code= tspl_vendor_master.vendor_code   where cust_code = '" + fndcustomer.Value + "' and "

            Dim str As String = "select TSPL_CUSTOMER_EMPLOYEE_MAPPING.Employee_Code,tspl_employee_master.Emp_Name 
from TSPL_CUSTOMER_EMPLOYEE_MAPPING 
left outer join tspl_employee_master on tspl_employee_master.EMP_CODE= TSPL_CUSTOMER_EMPLOYEE_MAPPING.Employee_Code where cust_code ='" + fndcustomer.Value + "'"
            Dim dr As DataTable = clsDBFuncationality.GetDataTable(str)

            If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                fndEmployee.Value = dr.Rows(0)(0).ToString()
                txtempdesc.Text = dr.Rows(0)(1).ToString()
            End If


            If clsCommon.myLen(fndEmployee.Value) > 0 Then
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnsave.Text = "Update"

            Else
                btnsave.Text = "Save"
                btnsave.Enabled = True
                btndelete.Enabled = False
                fndEmployee.Value = ""
                txtempdesc.Text = ""
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub funreset()
        fndcustomer.MyReadOnly = False
        fndcustomer.Value = ""
        fndEmployee.Value = ""
        txtcustdes.Text = ""
        txtempdesc.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
    Private Sub btnnew_Click(sender As Object, e As EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If fndcustomer.Value = "" Then
                myMessages.blankValue(Me, "Customer Code", Me.Text)
                RadGroupBox2.Focus()
            ElseIf fndEmployee.Value = "" Then
                myMessages.blankValue(Me, "Employee Code", Me.Text)
                fndEmployee.Focus()
            Else
                If clsCommon.myLen(strCustVenPass) > 0 Then
                    If MyBase.isModifyonPasswordFlag Then
                        If clsPasswordCheckForMasters.CheckMasterPwd(strCustVenPass, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                        Else
                            Return
                        End If
                    End If
                End If

                If btnsave.Text = "Save" Then
                    funinsert()
                ElseIf btnsave.Text = "Update" Then
                    funupdate()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Public Sub funupdate()
        Try

            Dim qry As String = "update TSPL_CUSTOMER_EMPLOYEE_MAPPING set employee_code='" + fndEmployee.Value + "' where cust_code='" + fndcustomer.Value + "'"
            connectSql.RunSql(qry)
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub funinsert()
        Try

            Dim qry As String = "insert into TSPL_CUSTOMER_EMPLOYEE_MAPPING values('" + fndcustomer.Value + "','" + fndEmployee.Value + "') "
            connectSql.RunSql(qry)
            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndcustomer.Value, "TSPL_CUSTOMER_EMPLOYEE_MAPPING", "Cust_Code", Nothing)

            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btndelete_Click(sender As Object, e As EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndcustomer.Value = "" Then
            myMessages.blankValue(Me, "Customer Code", Me.Text)
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            funreset()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
    End Sub
    Public Sub fundelete()
        Try
            'clsCommonFunctionality.SaveDeletedData(objCommonVar.CurrentUserCode, fndcustomer.Value, "TSPL_CUSTOMER_EMPLOYEE_MAPPING", "Cust_Code", Nothing)

            clsCommonFunctionality.SaveHistoryData(objCommonVar.CurrentUserCode, fndcustomer.Value, "TSPL_CUSTOMER_EMPLOYEE_MAPPING", "Cust_Code", Nothing)

            Dim qry As String = "delete from TSPL_CUSTOMER_EMPLOYEE_MAPPING where cust_code='" + fndcustomer.Value + "'"
            connectSql.RunSql(qry)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmImport_Click(sender As Object, e As EventArgs) Handles rmImport.Click
        Dim CustMapEntry As Double = 0
        Dim VenMapEntry As Double = 0
        Dim DuplicateEntry As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Customer Code", "Employee Code") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                clsCommon.ProgressBarShow()
                'Dim obj As New ClsCostCenter()

                trans = clsDBFuncationality.GetTransactin()
                For Each grow As GridViewRowInfo In gv.Rows

                    linno += 1

                    Dim Cust_Code As String = ""
                    Cust_Code = clsCommon.myCstr(grow.Cells("Customer Code").Value)

                    If clsCommon.myLen(Cust_Code) > 0 Then
                        Dim CustQry As String = "select Count(*) As Row from TSPL_CUSTOMER_MASTER where Cust_Code ='" + Cust_Code + "'"
                        Dim checkCust As Integer = clsDBFuncationality.getSingleValue(CustQry, trans)
                        If checkCust <= 0 Then
                            Throw New Exception("Please check ! Customer Code (" & clsCommon.myCstr(Cust_Code) & ") does not exists in customer master at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        'CustMapEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row from TSPL_CUSTOMER_VENDOR_MAPPING where Cust_Code ='" + Cust_Code + "'", trans))
                        'If CustMapEntry = 1 Then
                        '    Dim VenName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Isnull(Vendor_Code,'' ) As Vendor_Code from TSPL_CUSTOMER_VENDOR_MAPPING where Cust_Code ='" + Cust_Code + "'", trans))
                        '    Throw New Exception("Please check ! this customer (" & Cust_Code & ") is already mapped with vendor (" & VenName & ") at line no. " + clsCommon.myCstr(linno) + ".")
                        'End If
                    Else
                        Throw New Exception("Customer code can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Cust_Code = Cust_Code

                    Dim employee_Code As String = ""
                    employee_Code = clsCommon.myCstr(grow.Cells("Employee Code").Value)

                    If clsCommon.myLen(employee_Code) > 0 Then
                        Dim CustQry As String = "select Count(*) As Row from tspl_employee_master where emp_Code  ='" + employee_Code + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(CustQry, trans)
                        If check <= 0 Then
                            Throw New Exception("Please check ! Employee Code (" & clsCommon.myCstr(employee_Code) & ") does not exists in Employee master at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        'VenMapEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code ='" + Vendor_Code + "'", trans))
                        'If VenMapEntry = 1 Then
                        '    Dim CustName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Isnull(Cust_Code,'' ) As Cust_Code from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code ='" + Vendor_Code + "'", trans))
                        '    Throw New Exception("Please check ! this vendor (" & Vendor_Code & ") is already mapped with customer (" & CustName & ") at line no. " + clsCommon.myCstr(linno) + ".")
                        'End If
                    Else
                        Throw New Exception("Employee code can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    employee_Code = employee_Code
                    ''
                    Dim NewEntry As Boolean
                    Dim NewCheck As Double = 0
                    Dim qry As String = ""
                    If clsCommon.myLen(Cust_Code) > 0 AndAlso clsCommon.myLen(employee_Code) > 0 Then
                        NewCheck = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) AS Row FROM TSPL_CUSTOMER_EMPLOYEE_MAPPING where Cust_Code='" & Cust_Code & "'", trans))
                        If NewCheck > 0 Then
                            NewEntry = False
                        Else
                            NewEntry = True
                        End If
                    End If
                    If NewEntry = True Then
                        qry = "insert into TSPL_CUSTOMER_EMPLOYEE_MAPPING values('" + Cust_Code + "','" + employee_Code + "') "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    ElseIf NewEntry = False Then
                        qry = "update TSPL_CUSTOMER_EMPLOYEE_MAPPING set employee_code='" + employee_Code + "' where cust_code='" + Cust_Code + "'"
                        'connectSql.RunSql(qry)
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                Next
                DuplicateEntry = "select Vendor_Code, SUM(1) as Repeated from TSPL_CUSTOMER_EMPLOYEE_MAPPING group by EMPLOYEE_Code having SUM(1) > 1 "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(DuplicateEntry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Please check ! employee (" & clsCommon.myCstr(dt.Rows(0)("EMPLOYEE_Code")) & ") repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
                End If

                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            End Try
        End If
        Me.Controls.Remove(gv)
    End Sub

    Private Sub rmExport_Click(sender As Object, e As EventArgs) Handles rmExport.Click
        Dim str As String
        str = " SELECT Cust_Code AS [Customer Code],Employee_Code  As [Employee Code] FROM TSPL_CUSTOMER_EMPLOYEE_MAPPING "
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub btnHistory_Click(sender As Object, e As EventArgs) Handles btnHistory.Click
        Try
            If clsCommon.myLen(fndcustomer.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No")
                Exit Sub
            End If
            clsERPFuncationalityOLD.ShowHistoryData(fndcustomer.Value, "Cust_code", "TSPL_CUSTOMER_EMPLOYEE_MAPPING")
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class