Imports System.Data
Imports System.Data.SqlClient
Imports common

Public Class FrmCustomerMasterReport
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim sql As String
    Dim ds As DataSet
    Dim dr As SqlDataReader
    Dim dt As Date = Date.Today
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim i As New Decimal
    Dim j As New Decimal
#End Region
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CustomerDetails)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '      btnSave.Visible = MyBase.isModifyFlag
        ' btnPrint.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmCustomerMasterReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            funPrint()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funreset()
        End If
    End Sub


    Private Sub FrmCustomerMasterReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        'AddHandler fndFromCustomer.txtValue.KeyPress, AddressOf FromCustomer_KeyPress
        'AddHandler fndToCustomer.txtValue.KeyPress, AddressOf ToCustomer_KeyPress
        'fndFromCustomer.txtValue.MaxLength = 12
        'fndToCustomer.txtValue.MaxLength = 12
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        'For New Report Outlet Details ----
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        ddlvisi.SelectedIndex = 0
        ddlvisi.Items.Add("Both")
        ddlvisi.Items.Add("With Visi")
        ddlvisi.Items.Add("Without Visi")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")

    End Sub


    'Private Sub FromCustomer_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    fndFromCustomer.txtValue.CharacterCasing = CharacterCasing.Upper
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub
    'Private Sub ToCustomer_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    fndToCustomer.txtValue.CharacterCasing = CharacterCasing.Upper
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub
    Private Sub funPrint()
        'Dim f1 As String = fndFromCustomer.txtValue.Text
        'Dim t1 As String = fndToCustomer.txtValue.Text
        'funReport(f1, t1)
        '<<<<<<---- For OutLet Details --->>>>>>>>>>
        Try
            Dim qry As String
            Dim fromdate As String = clsCommon.myCDate(dtpFromdate1.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim filterVisi As String = ""
            If ddlvisi.SelectedIndex = 0 Then
                filterVisi = ""
            ElseIf ddlvisi.SelectedIndex = 1 Then
                filterVisi = " and tspl_customer_master.Cust_Code  in (select Customer_Id  from TSPL_VISI_MASTER where TSPL_VISI_MASTER .Customer_Id <>'' )"
            ElseIf ddlvisi.SelectedIndex = 2 Then
                filterVisi = "and tspl_customer_master.Cust_Code not in (select Customer_Id  from TSPL_VISI_MASTER where TSPL_VISI_MASTER .Customer_Id <>'' )"
            End If
            qry = "select '" + ddlvisi.Text + "' as visitype, '" & fromdate & "'as FromDate,'" & Todate & "' as Todate,tspl_customer_master.Cust_Code ,tspl_customer_master.Customer_Name ,tspl_customer_master.Cust_Category_Code,Route_No ,tspl_customer_master.Created_Date  as startdate,(case when TSPL_CUSTOMER_MASTER .Credit_Customer ='N' then 'No' else 'Yes'end )as CreditParty,(case when tspl_customer_master.Status ='Y' then 'InActive'else 'Active' end)as Satatus,(select Count(*)from TSPL_VISI_MASTER where TSPL_VISI_MASTER .Customer_Id =TSPL_CUSTOMER_MASTER .Cust_Code )as NumOfVisi,TSPL_CUSTOMER_MASTER  .Comp_Code as CompCode ,TSPL_COMPANY_MASTER.Comp_Name as CompName, TSPL_COMPANY_MASTER.Logo_Img as Image1, TSPL_COMPANY_MASTER.Logo_Img2 as Image2,(select TSPL_COMPANY_MASTER.ADD1 + case when len(TSPL_COMPANY_MASTER.add2)> 0 then ',' else '' end + TSPL_COMPANY_MASTER.add2 +case when len(TSPL_COMPANY_MASTER.add3)> 0 then ','else '' end +TSPL_COMPANY_MASTER.add3+case when len(TSPL_COMPANY_MASTER.City_Code)> 0 then ',' else '' end +TSPL_COMPANY_MASTER.City_Code +case when len(TSPL_COMPANY_MASTER.STATE)> 0 then ',' else '' end  +TSPL_COMPANY_MASTER.STATE )  as Compaddress from tspl_customer_master left outer join tspl_Company_master on TSPL_CUSTOMER_MASTER .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code  where Convert(date,TSPL_CUSTOMER_MASTER .Created_Date,103)>=CONVERT(Date,'" & dtpFromdate1.Value & "',103) and  Convert(date,TSPL_CUSTOMER_MASTER .Created_Date,103)<=CONVERT(Date,'" & dtpToDate.Value & "',103)"
            qry += filterVisi
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Record Found")
            Else

                frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "OutletDetails", "Outlet Details")
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
       



    End Sub

    'Public Sub funReport(ByVal f1 As String, ByVal t1 As String)
    '    Try
    '        'f1 as [FCust],t1 as[TCust]
    '        If f1 = "" And t1 = "" Then
    '            ds = connectSql.RunSQLReturnDS("select Cust_Code ,Customer_Name,Add1,Add2,Add3 ,Cust_Category_Code,Cust_Group_Code,Price_Code,Terms_Code,Cust_Account,Tax_Group,Credit_Limit from TSPL_CUSTOMER_MASTER ")
    '        ElseIf f1 = "" And t1 <> "" Then
    '            common.clsCommon.MyMessageBoxShow("select From Customer Code")
    '            Exit Sub
    '        ElseIf f1 <> "" And t1 = "" Then
    '            common.clsCommon.MyMessageBoxShow("select To Customer Code")
    '            Exit Sub
    '        Else
    '            ds = connectSql.RunSQLReturnDS("select '" + f1 + "' as [FCG],'" + t1 + "' as [TCG], Cust_Code ,Customer_Name,Add1,Add2,Add3 ,Cust_Category_Code,Cust_Group_Code,Price_Code,Terms_Code,Cust_Account,Tax_Group,Credit_Limit from TSPL_CUSTOMER_MASTER where Cust_Code between '" + f1 + "'and '" + t1 + "'")
    '        End If
    '        Dim dt As DataTable
    '        dt = ds.Tables(0)
    '        FrmSalerReport.funreport(dt, "CustomerDetails", "Customer Details Report")


    '        ''Dim rptshow As Boolean
    '        ''If dt.Rows.Count > 0 Then
    '        ''    Dim rpdoc As New ReportDocument()
    '        ''    Dim strpath = Application.StartupPath
    '        ''    Dim strreportpath As String = strpath + "\Reports\CustomerDetails.rpt"
    '        ''    rpdoc.Load(strreportpath)
    '        ''    rpdoc.SetDataSource(dt)
    '        ''    Me.CrystalReportViewer1.ReportSource = rpdoc
    '        ''    rptshow = True
    '        ''    Me.Show()
    '        ''Else
    '        ''    MsgBox("No Data Found", MsgBoxStyle.Information, "Customer Details")
    '        ''    Me.Close()
    '        ''    rptshow = False
    '        ''End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    'Private Sub fndFromCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndFromCustomer.ConnectionString = connectSql.SqlCon()
    '    fndFromCustomer.Query = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name] from tspl_customer_master"
    '    fndFromCustomer.ValueToSelect = "Customer Code"
    '    fndFromCustomer.ValueToSelect1 = "Customer Name"
    '    fndFromCustomer.Caption = "Customer Details"
    'End Sub

    'Private Sub fndToCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    fndToCustomer.ConnectionString = connectSql.SqlCon()
    '    fndToCustomer.Query = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name] from tspl_customer_master"
    '    fndToCustomer.ValueToSelect = "Customer Code"
    '    fndToCustomer.ValueToSelect1 = "Customer Name"
    '    fndToCustomer.Caption = "Customer Details"
    'End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub
    Sub funreset()
        'fndFromCustomer.txtValue.Text = ""
        'fndToCustomer.txtValue.Text = ""
        dtpFromdate1.Value = clsCommon.GETSERVERDATE()
        dtpToDate.Value = clsCommon.GETSERVERDATE()
        ddlvisi.SelectedIndex = 0
    End Sub

    'Private Sub fndFromCustomer_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndFromCustomer.txtValue.Text <> "" Then
    '        dr = connectSql.RunSqlReturnDR("select Cust_Code from tspl_customer_master where Cust_Code='" + fndFromCustomer.txtValue.Text + "'")
    '        Dim s As String
    '        While dr.Read()
    '            s = dr(0).ToString()
    '        End While
    '        If s <> fndFromCustomer.txtValue.Text Then
    '            common.clsCommon.MyMessageBoxShow("Customer  doesn't exist")
    '            fndFromCustomer.txtValue.Text = ""
    '            fndFromCustomer.txtValue.Focus()
    '        Else

    '        End If
    '    Else

    '    End If
    'End Sub

    'Private Sub fndToCustomer_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If fndToCustomer.txtValue.Text <> "" Then
    '        dr = connectSql.RunSqlReturnDR("select Cust_Code from tspl_customer_master where Cust_Code='" + fndToCustomer.txtValue.Text + "'")
    '        Dim s As String
    '        While dr.Read()
    '            s = dr(0).ToString()
    '        End While
    '        If s <> fndToCustomer.txtValue.Text Then
    '            common.clsCommon.MyMessageBoxShow("Customer  doesn't exist")
    '            fndToCustomer.txtValue.Text = ""
    '            fndToCustomer.txtValue.Focus()
    '        Else

    '        End If
    '    Else

    '    End If
    'End Sub


    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "CUST-DTL-RPT"
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
    '            'btnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            'btndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function
End Class
