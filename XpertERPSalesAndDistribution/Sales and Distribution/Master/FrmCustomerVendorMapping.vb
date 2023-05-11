'--preeti gupta-ticket no.[BM00000003133]
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

Public Class FrmCustomerVendorMapping
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim strCustVenPass As String = ""

    Public formtype As String = Nothing
    '' Anubhooti 12-Mar-2015 (Fetch Alies Name On Vendor Finder)
    Private Sub fndvendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndvendor._MYValidating
        'Dim qry As String = "select vendor_code as Code,vendor_name as Description from tspl_vendor_master where Vendor_Code not in (select Vendor_Code from TSPL_CUSTOMER_VENDOR_MAPPING )"
        Dim qry As String = "select vendor_code as Code,vendor_name as Description,ISNULL(TSPL_VENDOR_MASTER.alies_name,'') As [Alies Name] from tspl_vendor_master  "

        Dim whr As String = "  Vendor_Code not in (select Vendor_Code from TSPL_CUSTOMER_VENDOR_MAPPING ) and tspl_vendor_master.Status='N'"
        fndvendor.Value = clsCommon.ShowSelectForm("VendorMastrFND", qry, "Code", whr, fndvendor.Value, "Code", isButtonClicked)
        txtvendesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + fndvendor.Value + "' and tspl_vendor_master.Status='N'"))

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Sub SaveData()
        Try
            If fndcustomer.Value = "" Then
                myMessages.blankValue("Customer Code")
                RadGroupBox1.Focus()
            ElseIf fndvendor.Value = "" Then
                myMessages.blankValue("Vendor Code")
                fndvendor.Focus()
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


    'Funtion for insertion of data
    Public Sub funinsert()
        Try

            Dim qry As String = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + fndcustomer.Value + "','" + fndvendor.Value + "') "
            connectSql.RunSql(qry)
            myMessages.insert()
            btnsave.Text = "Update"
            btndelete.Enabled = True

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub



    'Funtion for updation  of data
    Public Sub funupdate()
        Try

            Dim qry As String = "update TSPL_CUSTOMER_VENDOR_MAPPING set vendor_code='" + fndvendor.Value + "' where cust_code='" + fndcustomer.Value + "'"
            connectSql.RunSql(qry)
            myMessages.update()
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub


    'Function for deletion of data
    Public Sub fundelete()
        Try
            Dim qry As String = "delete from TSPL_CUSTOMER_VENDOR_MAPPING where cust_code='" + fndcustomer.Value + "'"
            connectSql.RunSql(qry)
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try

    End Sub



    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If fndcustomer.Value = "" Then
            myMessages.blankValue("Customer Code")
        ElseIf myMessages.deleteConfirm() Then
            fundelete()
            myMessages.delete()
            btnsave.Text = "Save"
            btndelete.Enabled = False
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        funreset()
    End Sub

    'It will fill all controls in screen if find any existing data in table 
    Public Sub funfill()
        Try
            fndvendor.Value = ""
            txtvendesc.Text = ""
            'Dim str As String = "select TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code,tspl_vendor_master.vendor_name from TSPL_CUSTOMER_VENDOR_MAPPING left outer join tspl_vendor_master on TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code= tspl_vendor_master.vendor_code   where cust_code = '" + fndcustomer.Value + "' and "

            Dim str As String = "select TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code,tspl_vendor_master.vendor_name from TSPL_CUSTOMER_VENDOR_MAPPING left outer join tspl_vendor_master on TSPL_CUSTOMER_VENDOR_MAPPING.vendor_code= tspl_vendor_master.vendor_code  where cust_code = '" + fndcustomer.Value + "'"
            Dim dr As DataTable = clsDBFuncationality.GetDataTable(str)

            If dr IsNot Nothing AndAlso dr.Rows.Count > 0 Then
                fndvendor.Value = dr.Rows(0)(0).ToString()
                txtvendesc.Text = dr.Rows(0)(1).ToString()
            End If

             
            If clsCommon.myLen(fndvendor.Value) > 0 Then
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnsave.Text = "Update"

            Else
                btnsave.Text = "Save"
                btnsave.Enabled = True
                btndelete.Enabled = False
                fndvendor.Value = ""
                txtvendesc.Text = ""
            End If
        Catch ex As Exception

            myMessages.myExceptions(ex)
        End Try
    End Sub



    'It will reset all the controls in screens
    Public Sub funreset()
        fndcustomer.MyReadOnly = False
        fndcustomer.Value = ""
        fndvendor.Value = ""
        txtcustdes.Text = ""
        txtvendesc.Text = ""
        btnsave.Text = "Save"
        btndelete.Enabled = False
    End Sub
    Public Sub SetLength()
        fndcustomer.MyMaxLength = 12
        txtcustdes.MaxLength = 200

    End Sub


    Private Sub FrmCustomerVendorMapping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetLength()
        funreset()
        SetUserMgmtNew()
        ToolTip1.SetToolTip(btnnew, "New")
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ' ButtonToolTip.SetToolTip(btnPost, "Press Alt+P for  Post")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnnew, "Press Alt+N Adding New ")
        '  ButtonToolTip.SetToolTip(btnPrint, "Press Alt+R for Print Preview")




        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomerVendorMapping)

        If clsCommon.CompairString(formtype, clsUserMgtCode.CustomerVendorMapping) = CompairStringResult.Equal Then
            'MyBase.SetUserMgmt(clsUserMgtCode.CustomerVendorMapping)
            strCustVenPass = clsUserMgtCode.CustomerVendorMapping
        ElseIf clsCommon.CompairString(formtype, clsUserMgtCode.CustomerVendorMappingVendor) = CompairStringResult.Equal Then
            'MyBase.SetUserMgmt(clsUserMgtCode.CustomerVendorMappingVendor)
            strCustVenPass = clsUserMgtCode.CustomerVendorMappingVendor
        End If
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        ' btnPost.Visible = MyBase.isPostFlag
        If btnsave.Visible = True Then
            rmExport.Enabled = True
            rmImport.Enabled = True
        Else
            rmExport.Enabled = False
            rmImport.Enabled = False
        End If
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub



    '---------------------Added By -----Pankaj Kumar-------------on--29/03/2012-------------
    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CST-VNDR-MAP"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    '    '-----------------------------------Code Ends Here-------------------------------
    'End Function


    Private Sub FrmCustomerVendorMapping_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag Then
            'PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnsave.Enabled Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub

    Private Sub fndcustomer__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcustomer._MYValidating
        'Dim str1 As String = "select count(*) from TSPL_CUSTOMER_VENDOR_MAPPING where cust_code='" + fndcustomer.Value + "' "
        'Dim i As Integer = CInt(connectSql.RunScalar(str1))
        'If i = 0 Then
        '    fndvendor.Value = ""
        '    txtvendesc.Text = ""
        '    btnsave.Text = "Save"
        '    btndelete.Enabled = False

        'End If
        Dim str As String = "select count(*) from tspl_customer_master where cust_code ='" + fndcustomer.Value + "' "
        Dim no As Integer = CInt(connectSql.RunScalar(str))
        Dim no1 As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            fndvendor.Value = ""
            txtvendesc.Text = ""
            btnsave.Text = "Save"
            btndelete.Enabled = False

            fndcustomer.MyReadOnly = False

            fndcustomer.Value = ""
        Else
            fndcustomer.MyReadOnly = True
        End If
        If fndcustomer.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select tspl_customer_master.cust_code as Code,tspl_customer_master.customer_name as Description, TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name" + Environment.NewLine + _
            "from tspl_customer_master" + Environment.NewLine + _
            "left outer join TSPL_CUSTOMER_VENDOR_MAPPING on TSPL_CUSTOMER_VENDOR_MAPPING.Cust_Code=tspl_customer_master.Cust_Code" + Environment.NewLine + _
            "left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_CUSTOMER_VENDOR_MAPPING.Vendor_Code"
            fndcustomer.Value = clsCommon.ShowSelectForm("CustomrMastrFND", qry, "Code", " tspl_customer_master.Status ='N' ", fndcustomer.Value, "Code", isButtonClicked)
            txtcustdes.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + fndcustomer.Value + "'"))
        End If
        funfill()
    End Sub

    Private Sub fndcustomer__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavigatorType As common.NavigatorType) Handles fndcustomer._MYNavigator
        Dim qst As String = "select cust_code as Code,customer_name as Description from tspl_customer_master  where tspl_customer_master.Status ='N'  "
        Select Case NavigatorType
            Case NavigatorType.Current
                '  qst += "and assign_to='" + txtassign.Value + "' "
                ' qst += "and job_code in ('" + txtcode1.Value + "')"
            Case NavigatorType.Next
                qst += "and cust_code in (select min(cust_code) from tspl_customer_master where cust_code>'" + fndcustomer.Value + "' ) "
            Case NavigatorType.First
                qst += "and cust_code in (select MIN(cust_code) from tspl_customer_master )"
            Case NavigatorType.Last
                qst += "and cust_code in (select Max(cust_code) from tspl_customer_master )"
            Case NavigatorType.Previous
                qst += "and cust_code in (select max(cust_code) from tspl_customer_master where cust_code<'" + fndcustomer.Value + "'  )"
        End Select
        ' fun_gridfill()
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qst)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            fndcustomer.Value = clsCommon.myCstr(dt.Rows(0)("Code"))
            txtcustdes.Text = clsCommon.myCstr(dt.Rows(0)("Description"))
        End If
        'TextChanged()
        If fndcustomer.Value IsNot Nothing Then
            btndelete.Enabled = True
        Else
            btndelete.Enabled = False

        End If
        funfill()
    End Sub

    '' Anubhooti 22-Sep-2014
    Private Sub rmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmImport.Click
        Dim CustMapEntry As Double = 0
        Dim VenMapEntry As Double = 0
        Dim DuplicateEntry As String = ""
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Customer Code", "Vendor Code") Then
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

                    Dim Vendor_Code As String = ""
                    Vendor_Code = clsCommon.myCstr(grow.Cells("Vendor Code").Value)

                    If clsCommon.myLen(Vendor_Code) > 0 Then
                        Dim CustQry As String = "select Count(*) As Row from TSPL_VENDOR_MASTER where Vendor_Code  ='" + Vendor_Code + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(CustQry, trans)
                        If check <= 0 Then
                            Throw New Exception("Please check ! Vendor Code (" & clsCommon.myCstr(Vendor_Code) & ") does not exists in vendor master at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        'VenMapEntry = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code ='" + Vendor_Code + "'", trans))
                        'If VenMapEntry = 1 Then
                        '    Dim CustName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Isnull(Cust_Code,'' ) As Cust_Code from TSPL_CUSTOMER_VENDOR_MAPPING where Vendor_Code ='" + Vendor_Code + "'", trans))
                        '    Throw New Exception("Please check ! this vendor (" & Vendor_Code & ") is already mapped with customer (" & CustName & ") at line no. " + clsCommon.myCstr(linno) + ".")
                        'End If
                    Else
                        Throw New Exception("Vendor code can not be left blank at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    Vendor_Code = Vendor_Code
                    ''
                    Dim NewEntry As Boolean
                    Dim NewCheck As Double = 0
                    Dim qry As String = ""
                    If clsCommon.myLen(Cust_Code) > 0 AndAlso clsCommon.myLen(Vendor_Code) > 0 Then
                        NewCheck = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("SELECT COUNT(*) AS Row FROM TSPL_CUSTOMER_VENDOR_MAPPING where Cust_Code='" & Cust_Code & "'", trans))
                        If NewCheck > 0 Then
                            NewEntry = False
                        Else
                            NewEntry = True
                        End If
                    End If
                    If NewEntry = True Then
                        qry = "insert into TSPL_CUSTOMER_VENDOR_MAPPING values('" + Cust_Code + "','" + Vendor_Code + "') "
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    ElseIf NewEntry = False Then
                        qry = "update TSPL_CUSTOMER_VENDOR_MAPPING set vendor_code='" + Vendor_Code + "' where cust_code='" + Cust_Code + "'"
                        'connectSql.RunSql(qry)
                        clsDBFuncationality.ExecuteNonQuery(qry, trans)
                    End If

                Next
                DuplicateEntry = "select Vendor_Code, SUM(1) as Repeated from TSPL_CUSTOMER_VENDOR_MAPPING group by Vendor_Code having SUM(1) > 1 "
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(DuplicateEntry, trans)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Throw New Exception("Please check ! vendor (" & clsCommon.myCstr(dt.Rows(0)("Vendor_Code")) & ") repeated " & clsCommon.myCstr(dt.Rows(0)("Repeated")) & " times.")
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

    Private Sub rmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExport.Click
        Dim str As String
        str = " SELECT Cust_Code AS [Customer Code],Vendor_Code  As [Vendor Code] FROM TSPL_CUSTOMER_VENDOR_MAPPING "
        transportSql.ExporttoExcel(str, Me)
    End Sub
End Class
