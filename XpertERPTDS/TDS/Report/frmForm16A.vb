Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.WinControls.Enumerations
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Text.RegularExpressions
Imports common
Imports XpertERPEngine
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms

Public Class FrmForm16A
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    ' Dim arr As New ArrayList()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.Form16AReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        '      btnSave.Visible = MyBase.isModifyFlag
        '       btnAuth.Visible = MyBase.isPostFlag
        '        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmForm16A_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SetUserMgmtNew()
        'AddHandler fndVendorNumber.txtValue.Leave, AddressOf fndVendorNumber_Leave
        'AddHandler fndVendorNumber.txtValue.KeyPress, AddressOf fndVendorNumber_KeyPress

        'AddHandler fndFromDocumentNo.txtValue.Leave, AddressOf fndFromDocumentNo_Leave
        'AddHandler fndFromDocumentNo.txtValue.KeyPress, AddressOf fndFromDocumentNo_KeyPress

        'AddHandler fndToDocumentNo.txtValue.Leave, AddressOf fndToDocumentNo_Leave
        'AddHandler fndToDocumentNo.txtValue.KeyPress, AddressOf fndToDocumentNo_KeyPress
        dtpconFromdate.Value = Date.Today()
        dtpdatefrom.Value = Date.Today()
        dtpcontodate.Value = Date.Today()
        dtpdateto.Value = Date.Today()
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
        LoadLocation()
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()
        LoadDocument()
        chkDocumentAll.IsChecked = True
        chkLocAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P Print the Report")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R Reset the Window")

        'rbtnOriginal.IsChecked = True
    End Sub
    Public Sub LoadLocation()
        'Dim Qry As String = "select Location_Code as Code, Location_Desc as Description from TSPL_LOCATION_MASTER Where Location_Type='Physical'"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Sub LoadDocument()
        Dim strqry As String = "select Document_No as [Document No],Document_Type as [Document Type],Document_Date as [Document Date],Vendor_Code as [Vendor Code],Remit_TDS as [Remit TDS] from TSPL_REMITTANCE_ENTRY left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code =TSPL_REMITTANCE_ENTRY.Remittance_Code where Document_Type='I' and Remit_TDS is not null order by TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code"
        cbgDocument.DataSource = clsDBFuncationality.GetDataTable(strqry)
        cbgDocument.ValueMember = "Document No"
        cbgDocument.DisplayMember = "Document Date"

    End Sub
    'Private Sub fndVendorNumber_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndVendorNumber.Load
    '    fndVendorNumber.Query = "select Vendor_Code as [Vendor Code],Nature_Of_Deduction as [Nature Of Deduction],PAN,Vendor_TYpe as [Vendor Type],Status from TSPL_TDS_VENDOR_DETAILS"
    '    fndVendorNumber.ConnectionString = connectSql.SqlCon()
    '    fndVendorNumber.Caption = "TSPL TDS VENDOR DETAILS"
    '    fndVendorNumber.ValueToSelect = "Vendor Code"
    '    fndVendorNumber.ValueToSelect1 = "Vendor Type"
    '    fndVendorNumber.txtValue.MaxLength = 15
    'End Sub
    ''Private Sub fndVendorNumber_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''    If fndVendorNumber.txtValue.Text = "" Then
    ''    Else
    ''        Try
    ''            Dim strA_code As String = "select Vendor_Code from TSPL_TDS_VENDOR_DETAILS where Vendor_Code='" + fndVendorNumber.txtValue.Text + "'"
    ''            Dim dr As SqlDataReader
    ''            Dim strvalue As String
    ''            dr = connectSql.RunSqlReturnDR(strA_code)
    ''            While dr.Read()
    ''                strvalue = dr(0).ToString()
    ''            End While
    ''            If strvalue <> "" Then
    ''            Else : strA_code = ""
    ''                common.clsCommon.MyMessageBoxShow("This Vendor Code does not exist in the table")
    ''                fndVendorNumber.txtValue.Text = ""
    ''                fndVendorNumber.Focus()
    ''            End If
    ''        Catch ex As Exception
    ''            myMessages.myExceptions(ex)
    ''        End Try
    ''    End If
    ''End Sub
    '' Added BY abhishek Kumar as On 11/june/2012 For New Finder 
    Private Sub VendorNameNew__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndVendorNameNew._MYValidating
        Dim qry As String = "select Vendor_Code as [Code],Nature_Of_Deduction as [Nature Of Deduction],PAN,Vendor_TYpe as [Vendor Type],Status from TSPL_TDS_VENDOR_DETAILS"
        fndVendorNameNew.Value = clsCommon.ShowSelectForm("Vendor Code", qry, "Code", "", fndVendorNameNew.Value, "Code", isButtonClicked)


    End Sub
    'Private Sub fndVendorNumber_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '    If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
    '        e.Handled = True
    '    End If
    'End Sub

    Private Sub fndFromDocumentNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndFromDocumentNo.Query = "select Document_No as [Document No],Document_Type as [Document Type],Document_Date as [Document Date],Vendor_Code as [Vendor Code],Remit_TDS as [Remit TDS] from TSPL_REMITTANCE_ENTRY where Document_Type='I' and Remit_TDS is not null order by TSPL_REMITTANCE_ENTRY.Vendor_Code"
        ''fndFromDocumentNo.Query = "select Document_No as [Document No],Document_Type as [Document Type],Document_Date as [Document Date],Vendor_Code as [Vendor Code],Remit_TDS as [Remit TDS] from TSPL_REMITTANCE_ENTRY where Document_Type='I' and Remit_TDS is not null "
        'fndFromDocumentNo.ConnectionString = connectSql.SqlCon()
        'fndFromDocumentNo.Caption = "TSPL REMITTANCE"
        'fndFromDocumentNo.ValueToSelect = "Document No"
        'fndFromDocumentNo.ValueToSelect1 = "Vendor code"
        'fndFromDocumentNo.txtValue.MaxLength = 15
    End Sub
    Private Sub fndFromDocumentNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If fndFromDocumentNo.txtValue.Text = "" Then
        'Else
        '    Try
        '        Dim strA_code As String = "select Document_No from TSPL_REMITTANCE where Document_No='" + fndFromDocumentNo.txtValue.Text + "' and Document_Type='I' and Remit_TDS is not null"
        '        Dim dr As SqlDataReader
        '        Dim strvalue As String
        '        dr = connectSql.RunSqlReturnDR(strA_code)
        '        While dr.Read()
        '            strvalue = dr(0).ToString()
        '        End While
        '        If strvalue <> "" Then
        '        Else : strA_code = ""
        '            common.clsCommon.MyMessageBoxShow("This Document Number does not exist in the table")
        '            fndFromDocumentNo.txtValue.Text = ""
        '            fndFromDocumentNo.Focus()
        '        End If
        '    Catch ex As Exception
        '        myMessages.myExceptions(ex)
        '    End Try
        'End If
    End Sub
    Private Sub fndFromDocumentNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub
    Private Sub fndToDocumentNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'fndToDocumentNo.Query = "select Document_No as [Document No],Document_Type as [Document Type],Document_Date as [Document Date],Vendor_Code as [Vendor Code],Remit_TDS as [Remit TDS] from TSPL_REMITTANCE_ENTRY where Document_Type='I' and Remit_TDS is not null order by TSPL_REMITTANCE_ENTRY.Vendor_Code"
        'fndToDocumentNo.ConnectionString = connectSql.SqlCon()
        'fndToDocumentNo.Caption = "TSPL REMITTANCE"
        'fndToDocumentNo.ValueToSelect = "Document No"
        'fndToDocumentNo.ValueToSelect1 = "Vendor code"
    End Sub
    Private Sub fndToDocumentNo_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If fndToDocumentNo.txtValue.Text = "" Then
        'Else
        '    Try
        '        Dim strA_code As String = "select Document_No from TSPL_REMITTANCE where Document_No='" + fndToDocumentNo.txtValue.Text + "' and Document_Type='I' and Remit_TDS is not null"
        '        Dim dr As SqlDataReader
        '        Dim strvalue As String
        '        dr = connectSql.RunSqlReturnDR(strA_code)
        '        While dr.Read()
        '            strvalue = dr(0).ToString()
        '        End While
        '        If strvalue <> "" Then
        '        Else : strA_code = ""
        '            common.clsCommon.MyMessageBoxShow("This Document Number does not exist in the table")
        '            fndToDocumentNo.txtValue.Text = ""
        '            fndToDocumentNo.Focus()
        '        End If
        '    Catch ex As Exception
        '        myMessages.myExceptions(ex)
        '    End Try
        'End If
    End Sub
    Private Sub fndToDocumentNo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub rbtnOriginal_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rbtnOriginal.MouseClick
        fndVendorNameNew.Enabled = True
        RadGroupBox4.Enabled = True

        chkDocumentAll.Enabled = True
        chkDocumentSelect.Enabled = True
        chkDocumentAll.IsChecked = True

        dtpdatefrom.Enabled = True
        dtpdateto.Enabled = True
        dtpconFromdate.Enabled = False
        dtpcontodate.Enabled = False
    End Sub

    Private Sub rbtnDuplicate_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rbtnDuplicate.MouseClick
        fndVendorNameNew.Enabled = True
        RadGroupBox4.Enabled = False
        cbgDocument.Enabled = False

        chkDocumentAll.Enabled = False
        chkDocumentSelect.Enabled = False

        dtpconFromdate.Enabled = False
        dtpcontodate.Enabled = False
        dtpdatefrom.Enabled = False
        dtpdateto.Enabled = False
    End Sub
    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub
    Public Sub reset()
        'rbtnOriginal.IsChecked = True
        fndVendorNameNew.Enabled = True
        chkLocAll.IsChecked = True
        dtpconFromdate.Enabled = True
        dtpcontodate.Enabled = True
        dtpdatefrom.Enabled = True
        dtpdateto.Enabled = True
        fndVendorNameNew.Value = ""

        dtpconFromdate.Value = Date.Today()
        dtpcontodate.Value = Date.Today()
        dtpdatefrom.Value = Date.Today()
        dtpdateto.Value = Date.Today()
        rbtnOriginal.IsChecked = False
        rbtnConsolidation.IsChecked = False
        rbtnDuplicate.IsChecked = False

        cbgDocument.Enabled = False
        chkDocumentAll.Enabled = True
        chkDocumentSelect.Enabled = True
        chkDocumentAll.IsChecked = True
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub
    Sub Print()
        'Dim str() As String = {fndVendorNumber.txtValue.Text, fndFromDocumentNo.txtValue.Text, fndToDocumentNo.txtValue.Text}
        'arr.Add(str)
        Try

            Dim sqlDocTotal As String = "select amt_to_remit,document_Amount from TSPL_REMITTANCE_ENTRY left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code=TSPL_REMITTANCE_ENTRY.Remittance_Code  where vendor_code='" + fndVendorNameNew.Value + "' "

            If chkDocumentSelect.IsChecked Then
                sqlDocTotal += " and document_no in (" + clsCommon.GetMulcallString(cbgDocument.CheckedValue) + ")"
            End If
            Dim DocTotal As Decimal = 0.0
            Dim valDoc As Decimal
            Dim dtdoctot As DataTable
            dtdoctot = clsDBFuncationality.GetDataTable(sqlDocTotal)
            If dtdoctot.Rows.Count > 0 Then
                For i As Integer = 0 To dtdoctot.Rows.Count - 1
                    valDoc = dtdoctot.Rows(i)("document_Amount")
                    DocTotal = DocTotal + valDoc
                Next
            End If
            'Dim dr As SqlDataReader
            'Dim DocTotal As Decimal = 0.0
            'Dim valDoc As Decimal
            'dr = connectSql.RunSqlReturnDR(sqlDocTotal)
            'While dr.Read()
            '    valDoc = dr(1).ToString()
            '    DocTotal = DocTotal + valDoc
            'End While
            Dim strDocTotal As String = Convert.ToString(DocTotal)



            Dim sql As String
            If chkDocumentSelect.IsChecked Then
                sql = " select amt_to_remit,document_Amount from TSPL_REMITTANCE_ENTRY left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code=TSPL_REMITTANCE_ENTRY.Remittance_Code where vendor_code='" + fndVendorNameNew.Value + "' and document_no in (" + clsCommon.GetMulcallString(cbgDocument.CheckedValue) + ")"
            Else
                sql = "select amt_to_remit,document_Amount from TSPL_REMITTANCE_ENTRY left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code=TSPL_REMITTANCE_ENTRY.Remittance_Code where vendor_code='" + fndVendorNameNew.Value + "' "
            End If
            'If fndFromDocumentNo.txtValue.Text <> "" And fndToDocumentNo.txtValue.Text <> "" Then
            '    sql = "select amt_to_remit,document_Amount from TSPL_REMITTANCE_ENTRY where vendor_code='" + fndVendorNumber.txtValue.Text + "' and document_no>='" + fndFromDocumentNo.txtValue.Text + "'  and document_no<='" + fndToDocumentNo.txtValue.Text + "'"
            'Else
            '    sql = "select amt_to_remit,document_Amount from TSPL_REMITTANCE_ENTRY where vendor_code='" + fndVendorNumber.txtValue.Text + "' "
            'End If
            Dim dtremtot As DataTable
            Dim RemTotal As Decimal = 0.0
            Dim val As Decimal
            dtremtot = clsDBFuncationality.GetDataTable(sql)
            If dtremtot.Rows.Count > 0 Then
                For i As Integer = 0 To dtremtot.Rows.Count - 1
                    val = dtremtot.Rows(i)("amt_to_remit")
                    RemTotal = RemTotal + val
                Next
            End If




            'Dim dr1 As SqlDataReader
            'Dim RemTotal As Decimal = 0.0
            'Dim val As Decimal
            'dr1 = connectSql.RunSqlReturnDR(sql)
            'While dr1.Read()
            '    val = dr1(0).ToString()
            '    RemTotal = RemTotal + val
            'End While
            Dim strval As String = Convert.ToString(RemTotal)






            If fndVendorNameNew.Value <> "" Then


                ''            Dim qry As String = "SELECT TSPL_COMPANY_MASTER.Comp_Code, TSPL_REMITTANCE_ENTRY.BSR_Code, TSPL_REMITTANCE_ENTRY.Cheque_No, " & _
                ''                      "TSPL_REMITTANCE_ENTRY.Challan_No,TSPL_REMITTANCE_ENTRY.Amt_to_remit, TSPL_REMITTANCE_ENTRY.Document_No, TSPL_REMITTANCE_ENTRY.Document_Date, " & _
                ''                      "TSPL_REMITTANCE_ENTRY.Actual_TDS_Base, TSPL_REMITTANCE_ENTRY.Calculated_TDS_Base, TSPL_REMITTANCE_ENTRY.Calculated_TDS," & _
                ''                      "TSPL_REMITTANCE_ENTRY.Actual_Total_TDS, TSPL_REMITTANCE_ENTRY.Calculated_Total_TDS, TSPL_REMITTANCE_ENTRY.Branch_Code, " & _
                ''           " TSPL_REMITTANCE_ENTRY.Quarter, TSPL_REMITTANCE_ENTRY.Remittance_Date, TSPL_COMPANY_MASTER.Comp_Name, " & _
                ''                      "TSPL_COMPANY_MASTER.Add1 AS compAdd1, TSPL_COMPANY_MASTER.Add2 AS compAdd2, TSPL_COMPANY_MASTER.Add3 AS compAdd3," & _
                ''                      "TSPL_COMPANY_MASTER.State AS compState, TSPL_COMPANY_MASTER.Pincode AS comppin, TSPL_COMPANY_MASTER.Tin_No AS compTin, " & _
                ''                    " TSPL_COMPANY_MASTER.Pan_No AS compPan,TSPL_COMPANY_MASTER.Tan_No AS compTan, TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction, TSPL_TDS_VENDOR_DETAILS.PAN AS venPan, " & _
                ''            "TSPL_TDS_VENDOR_DETAILS.Vendor_TYpe, TSPL_TDS_VENDOR_DETAILS.Status, TSPL_VENDOR_MASTER.Vendor_Name, " & _
                ''                      "TSPL_VENDOR_MASTER.Add1 AS venAdd1, TSPL_VENDOR_MASTER.Add2 AS venAdd2, TSPL_VENDOR_MASTER.Add3 AS venAdd3, " & _
                ''                      "TSPL_VENDOR_MASTER.City_Code_Desc AS venCity, TSPL_VENDOR_MASTER.State AS venState, " & _
                ''                      "TSPL_REMITTANCE_ENTRY.Vendor_Code AS RemVenCode, TSPL_REMITTANCE_ENTRY.Fiscal_Year ,TSPL_TDS_RESP_PERSON.Person_Name, TSPL_TDS_RESP_PERSON.Father_Name, TSPL_TDS_RESP_PERSON.Designation,  TSPL_TDS_RESP_PERSON.City , TSPL_TDS_FINANCIAL_YEAR.From_Date, TSPL_TDS_FINANCIAL_YEAR.To_Date ,'" + strval + "' as Remittotal ,'" + strDocTotal + "' as DocTotal " & _
                ''"FROM         TSPL_REMITTANCE_ENTRY INNER JOIN " & _
                ''                      "TSPL_COMPANY_MASTER ON TSPL_REMITTANCE_ENTRY.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code INNER JOIN " & _
                ''                      "TSPL_TDS_VENDOR_DETAILS ON TSPL_REMITTANCE_ENTRY.Vendor_Code = TSPL_TDS_VENDOR_DETAILS.Vendor_Code INNER JOIN " & _
                ''                      "TSPL_VENDOR_MASTER ON TSPL_REMITTANCE_ENTRY.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code INNER JOIN " & _
                ''                      "TSPL_TDS_RESP_PERSON ON TSPL_REMITTANCE_ENTRY.Branch_Code = TSPL_TDS_RESP_PERSON.Branch_Code  INNER JOIN   TSPL_TDS_FINANCIAL_YEAR ON TSPL_REMITTANCE_ENTRY.Fiscal_Year = TSPL_TDS_FINANCIAL_YEAR.Year_Name  "


                Dim qry As String = "SELECT TSPL_COMPANY_MASTER.Comp_Code, TSPL_REMITTANCE_ENTRY.BSR_Code, TSPL_REMITTANCE_ENTRY.Cheque_No, " & _
                         "TSPL_REMITTANCE_ENTRY.Challan_No,TSPL_REMITTANCE_ENTRY.Amt_to_remit,TSPL_REMITTANCE_ENTRY.Remittance_Code, TSPL_REMITTANCE_ENTRY_DETAIL.Document_No, TSPL_REMITTANCE_ENTRY_DETAIL.Document_Date, " & _
                         "TSPL_REMITTANCE_ENTRY_DETAIL.Actual_TDS_Base, TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_TDS_Base, TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_TDS," & _
                         "TSPL_REMITTANCE_ENTRY_DETAIL.Actual_Total_TDS, TSPL_REMITTANCE_ENTRY_DETAIL.Calculated_Total_TDS, TSPL_REMITTANCE_ENTRY.Branch_Code, " & _
              " TSPL_REMITTANCE_ENTRY_DETAIL.Quarter, TSPL_REMITTANCE_ENTRY.Remittance_Date, TSPL_COMPANY_MASTER.Comp_Name, " & _
                         "TSPL_COMPANY_MASTER.Add1 AS compAdd1, TSPL_COMPANY_MASTER.Add2 AS compAdd2, TSPL_COMPANY_MASTER.Add3 AS compAdd3," & _
                         "TSPL_COMPANY_MASTER.State AS compState, TSPL_COMPANY_MASTER.Pincode AS comppin, TSPL_COMPANY_MASTER.Tin_No AS compTin, " & _
                       " TSPL_COMPANY_MASTER.Pan_No AS compPan,TSPL_COMPANY_MASTER.Tan_No AS compTan, TSPL_TDS_VENDOR_DETAILS.Nature_Of_Deduction, TSPL_TDS_VENDOR_DETAILS.PAN AS venPan, " & _
               "TSPL_TDS_VENDOR_DETAILS.Vendor_TYpe, TSPL_TDS_VENDOR_DETAILS.Status, TSPL_VENDOR_MASTER.Vendor_Name, " & _
                         "TSPL_VENDOR_MASTER.Add1 AS venAdd1, TSPL_VENDOR_MASTER.Add2 AS venAdd2, TSPL_VENDOR_MASTER.Add3 AS venAdd3, " & _
                         "TSPL_VENDOR_MASTER.City_Code_Desc AS venCity, TSPL_VENDOR_MASTER.State AS venState, " & _
                         "TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code AS RemVenCode, TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year ,TSPL_TDS_RESP_PERSON.Person_Name, TSPL_TDS_RESP_PERSON.Father_Name, TSPL_TDS_RESP_PERSON.Designation,  TSPL_TDS_RESP_PERSON.City , TSPL_TDS_FINANCIAL_YEAR.From_Date, TSPL_TDS_FINANCIAL_YEAR.To_Date ,'" + strval + "' as Remittotal ,'" + strDocTotal + "' as DocTotal " & _
            "FROM         TSPL_REMITTANCE_ENTRY  left outer join TSPL_REMITTANCE_ENTRY_DETAIL on TSPL_REMITTANCE_ENTRY_DETAIL.Remittance_Code=TSPL_REMITTANCE_ENTRY.Remittance_Code left outer JOIN " & _
                         "TSPL_COMPANY_MASTER ON TSPL_REMITTANCE_ENTRY.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code left outer JOIN " & _
                         "TSPL_TDS_VENDOR_DETAILS ON TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code = TSPL_TDS_VENDOR_DETAILS.Vendor_Code left outer JOIN " & _
                         "TSPL_VENDOR_MASTER ON TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code = TSPL_VENDOR_MASTER.Vendor_Code left outer JOIN " & _
                         "TSPL_TDS_RESP_PERSON ON TSPL_REMITTANCE_ENTRY.Branch_Code = TSPL_TDS_RESP_PERSON.Branch_Code  left outer JOIN   TSPL_TDS_FINANCIAL_YEAR ON TSPL_REMITTANCE_ENTRY_DETAIL.Fiscal_Year = TSPL_TDS_FINANCIAL_YEAR.Year_Name  " & _
                         "left outer join TSPL_REMITTANCE on TSPL_REMITTANCE.Remittance_Main_Code =TSPL_REMITTANCE_ENTRY.Remittance_Code "

                If rbtnOriginal.IsChecked = True Then
                    If chkDocumentAll.IsChecked Then
                        'qry += " where TSPL_REMITTANCE_ENTRY.Vendor_Code = '" + fndVendorNumber.txtValue.Text + "' and TSPL_REMITTANCE_ENTRY.report_status IS NULL or TSPL_REMITTANCE_ENTRY.report_status='N' and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103) >=convert(date,'" + dtpdatefrom.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103)<=convert(date,'" + dtpdateto.Value + "',103) order by TSPL_REMITTANCE_ENTRY.Vendor_Code "
                        qry += " where TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code = '" + fndVendorNameNew.Value + "'  and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103) >=convert(date,'" + dtpdatefrom.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103)<=convert(date,'" + dtpdateto.Value + "',103) and (TSPL_REMITTANCE_ENTRY.report_status IS NULL or TSPL_REMITTANCE_ENTRY.report_status ='' or TSPL_REMITTANCE_ENTRY.report_status='N') order by TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code "
                        'qry += " where TSPL_REMITTANCE_ENTRY.Vendor_Code = '" + fndVendorNumber.txtValue.Text + "'  and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103) >=convert(date,'" + dtpdatefrom.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103)<=convert(date,'" + dtpdateto.Value + "',103)  "
                        'qry += " where TSPL_REMITTANCE_ENTRY.Vendor_Code = '" + fndVendorNumber.txtValue.Text + "' and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103) >=convert(date,'" + dtpdatefrom.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103)<=convert(date,'" + dtpdateto.Value + "',103)"
                    ElseIf chkDocumentSelect.IsChecked Then
                        'qry += " where TSPL_REMITTANCE_ENTRY.Vendor_Code = '" + fndVendorNumber.txtValue.Text + "' and TSPL_REMITTANCE_ENTRY.report_status IS NULL or TSPL_REMITTANCE_ENTRY.report_status='N' and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103) >=convert(date,'" + dtpdatefrom.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103)<=convert(date,'" + dtpdateto.Value + "',103) and TSPL_REMITTANCE_ENTRY.document_no >= '" + fndFromDocumentNo.txtValue.Text + "' and TSPL_REMITTANCE_ENTRY.document_no <= '" + fndToDocumentNo.txtValue.Text + "' order by TSPL_REMITTANCE_ENTRY.Vendor_Code "
                        qry += " where TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code = '" + fndVendorNameNew.Value + "'  and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103) >=convert(date,'" + dtpdatefrom.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103)<=convert(date,'" + dtpdateto.Value + "',103) and TSPL_REMITTANCE_ENTRY_DETAIL.document_no in (" + clsCommon.GetMulcallString(cbgDocument.CheckedValue) + ") and (TSPL_REMITTANCE_ENTRY.report_status IS NULL or TSPL_REMITTANCE_ENTRY.report_status ='' or TSPL_REMITTANCE_ENTRY.report_status='N') order by TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code "
                        'qry += " where TSPL_REMITTANCE_ENTRY.Vendor_Code = '" + fndVendorNumber.txtValue.Text + "'  and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103) >=convert(date,'" + dtpdatefrom.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103)<=convert(date,'" + dtpdateto.Value + "',103) and TSPL_REMITTANCE_ENTRY.document_no >= '" + fndFromDocumentNo.txtValue.Text + "' and TSPL_REMITTANCE_ENTRY.document_no <= '" + fndToDocumentNo.txtValue.Text + "'  "
                        'qry += " where TSPL_REMITTANCE_ENTRY.Vendor_Code = '" + fndVendorNumber.txtValue.Text + "' and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103) >=convert(date,'" + dtpdatefrom.Value + "',103) and convert(date,TSPL_REMITTANCE_ENTRY.remittance_date,103)<=convert(date,'" + dtpdateto.Value + "',103) and TSPL_REMITTANCE_ENTRY.document_no >= '" + fndFromDocumentNo.txtValue.Text + "' and TSPL_REMITTANCE_ENTRY.document_no <= '" + fndToDocumentNo.txtValue.Text + "'"
                    End If

                    If chkLocSelect.IsChecked Then
                        If cbgLocation.CheckedValue.Count <= 0 Then
                            common.clsCommon.MyMessageBoxShow("Please select one location ", Me.Text)
                            Return
                        End If
                        qry += "and substring(TSPL_REMITTANCE.Branch_GL_AC ,LEN(TSPL_REMITTANCE.Branch_GL_AC )-2,3)  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
                    End If
                     
                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport1(CrystalReportFolder.TDS, dt, "Form16a04", "Form 16 Report")

                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For i As Integer = 0 To dt.Rows.Count - 1
                            Dim query As String = "update TSPL_REMITTANCE_ENTRY set report_status='Y' where Remittance_Code='" + common.clsCommon.myCstr(dt.Rows(i).Item("Remittance_Code")) + "'"
                            connectSql.RunSql(query)
                        Next
                    End If
 
                ElseIf rbtnDuplicate.IsChecked = True Then
                    '  qry += " where TSPL_REMITTANCE_ENTRY.Vendor_Code = '" + fndVendorNumber.txtValue.Text + "'"
                    qry += " where TSPL_REMITTANCE_ENTRY_DETAIL.Vendor_Code = '" + fndVendorNameNew.Value + "'"
                    If chkLocSelect.IsChecked Then
                        If cbgLocation.CheckedValue.Count <= 0 Then
                            common.clsCommon.MyMessageBoxShow("Please select one location ", Me.Text)
                            Return
                        End If
                        qry += "and substring(TSPL_REMITTANCE.Branch_GL_AC ,LEN(TSPL_REMITTANCE.Branch_GL_AC )-2,3)  in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "

                        'qry += "and TSPL_LOCATION_MASTER.Loc_Segment_Code  IN (" + clsCommon.GetMulcallString(locationArr) + ") "
                    End If

                    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                    Dim frmCrystalReportViewer As New frmCrystalReportViewer
                    frmCrystalReportViewer.funreport(CrystalReportFolder.TDS, dt, "Form16a04D", "Form 16 Report")

                End If

            Else

                common.clsCommon.MyMessageBoxShow("Select the value of Vendor Code", Me.Text)
            End If


            'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            'TDSReportViewer.funreport(dt, "Form16a04", "Form 16 Report")

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rbtnConsolidation_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles rbtnConsolidation.MouseClick
        RadGroupBox4.Enabled = False
        cbgDocument.Enabled = False
        chkDocumentAll.Enabled = False
        chkDocumentSelect.Enabled = False
        fndVendorNameNew.Enabled = True
        dtpconFromdate.Enabled = True
        dtpcontodate.Enabled = True

        dtpdatefrom.Enabled = False
        dtpdateto.Enabled = False
    End Sub

    'This will check the authorization of user to access the screen.If authorize then it will allow user to access the screen.
    'Private Function funSetUserAccess() As Boolean
    '    Try

    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "FRM16-RPT"
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

    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access

    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception
    '        myMessages.myExceptions(er)
    '    End Try
    'End Function



    Private Sub chkDocumentAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkDocumentAll.ToggleStateChanged
        cbgDocument.Enabled = Not chkDocumentAll.IsChecked
    End Sub


    Private Sub fndVendorNameNew_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles fndVendorNameNew.KeyPress
        If Microsoft.VisualBasic.Asc(e.KeyChar) = 39 Then
            e.Handled = True
        End If
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub FrmForm16A_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C AndAlso MyBase.isDeleteFlag Then
            Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso MyBase.isDeleteFlag Then
            reset()
        ElseIf e.Control AndAlso e.KeyCode = Keys.P AndAlso MyBase.isDeleteFlag Then
            Print()
        End If
    End Sub
End Class
