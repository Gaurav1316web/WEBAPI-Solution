'Created By---> Monika
'Created Date--->21/04/2014
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Data.OleDb
Imports System.Drawing
Imports System.IO
Imports System.Data.SqlClient
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExportToExcelML
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports common
Imports System.Threading
Public Class FrmBankGuaranteeMaster1
    Inherits FrmMainTranScreen

#Region "Variable Declaration"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As New clsBankGuaranteeMaster()
    Public strPaymentNo As String = Nothing
#End Region

    Sub Reset()
        txtcode.Value = ""
        txtextndreminder.Text = ""
        txtdocdate.Text = ""
        txtdesc.Text = ""
        txtstrtdate.Value = clsCommon.GETSERVERDATE()
        txtenddate.Value = clsCommon.GETSERVERDATE()
        txtextnddate.Value = clsCommon.GETSERVERDATE()
        txtvendorcode.Value = ""
        txtvendrname.Text = ""
        txtfdCode.Value = ""
        fndReceiving.Value = ""
        txtfddesc.Text = ""
        txtamount.Text = ""
        txtreminderdays.Text = ""
        txtremarks.Text = ""
        txtamount.Value = 0
        ddltype.Text = "Vendor"
        GetBankGuaranteeType()
        CmbGuaranteeType.SelectedValue = "RC"
        LblVenCode.Text = "Vendor Code"
        txtfdCode.MendatroryField = True
        fndReceiving.MendatroryField = True
        UsLock1.Status = ERPTransactionStatus.Open

        txtvendorcode.Enabled = True
        txtvendrname.Enabled = True
        txtenddate.Enabled = True
        txtextnddate.Enabled = True
        txtstrtdate.Enabled = True
        ddltype.Enabled = True

        txtdocdate.Value = clsCommon.GETSERVERDATE()
        txtcode.MyReadOnly = False
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnpost.Enabled = False
        btnsave.Text = "Save"
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmBankGuaranteeMaster1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag
        btnpost.Visible = MyBase.isPostFlag

    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Sub SaveData()
        obj = New clsBankGuaranteeMaster()
        obj.code = clsCommon.myCstr(txtcode.Value)
        obj.docdate = clsCommon.myCDate(txtdocdate.Text)
        obj.desc = txtdesc.Text.Replace("'", "`")

        If clsCommon.myLen(obj.desc) > 300 Then
            obj.desc = obj.desc.Substring(0, 300)
        End If

        obj.strtdate = clsCommon.myCDate(txtstrtdate.Value)
        obj.enddate = clsCommon.myCDate(txtenddate.Value)
        obj.extnddate = clsCommon.myCDate(txtextnddate.Value)
        obj.bankcode = txtfdCode.Value
        obj.bankdesc = txtfddesc.Text.Replace("'", "`")
        obj.vndrcode = txtvendorcode.Value
        obj.vndrname = txtvendrname.Text.Replace("'", "`")
        obj.amount = clsCommon.myCdbl(txtamount.Text)
        obj.rimnder = "0" 'clsCommon.myCdbl(txtreminderdays.Text)
        obj.remarks = txtremarks.Text.Replace("'", "`")
        obj.extndreminder = "0" 'txtextndreminder.Text
        '' Anubhooti 24-Sep-2014 BM00000004063
        obj.Type = clsCommon.myCstr(ddltype.Text)
        obj.Bank_Guarantee_Type = clsCommon.myCstr(CmbGuaranteeType.SelectedValue)

        '========Rohit
        obj.Receiving_code = clsCommon.myCstr(fndReceiving.Value)
        Try
            Convert.ToDecimal(obj.rimnder)
        Catch ex As Exception
            obj.rimnder = "0"
        End Try

        Try
            Convert.ToDecimal(obj.extndreminder)
        Catch ex As Exception
            obj.extndreminder = "0"
        End Try

        If clsCommon.myLen(obj.remarks) > 200 Then
            obj.remarks = obj.remarks.Substring(0, 200)
        End If

        If btnsave.Text <> "Save" Then
            Dim qry As String = "select count(*) from tspl_bank_guarantee_master where docno='" + txtcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check = 0 Then
                clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
                Reset()
                Return
            End If
        End If

        If AllowToSave() Then
            Dim isSaved As Boolean = clsBankGuaranteeMaster.SaveData(obj, IIf(btnsave.Text = "Save", True, False))
            If isSaved Then
                txtcode.Value = obj.code
                common.clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                btnsave.Text = "Update"
                txtcode.MyReadOnly = True
                btndelete.Enabled = True
                btnpost.Enabled = True
            Else
                btnsave.Text = "Save"
                txtcode.MyReadOnly = False
                btndelete.Enabled = False
                btnpost.Enabled = False
                common.clsCommon.MyMessageBoxShow("Data Could Not Saved", Me.Text)
            End If
            If obj.post = "Y" Then
                UsLock1.Status = ERPTransactionStatus.Approved
                btnsave.Enabled = False
                btndelete.Enabled = False
                btnpost.Enabled = False
            ElseIf obj.post = "N" Then
                UsLock1.Status = ERPTransactionStatus.Pending
            End If
        End If

    End Sub

    Function AllowToSave() As Boolean
        Try
            'If clsCommon.myLen(txtcode.Value) = 0 Then
            '    clsCommon.MyMessageBoxShow("Please Enter Guarantee Code", Me.Text)
            '    txtcode.Focus()
            '    txtcode.Select()
            '    Return False
            'End If

            If clsCommon.myLen(fndReceiving.Value) <= 0 And CmbGuaranteeType.SelectedValue = "RT" Then
                clsCommon.MyMessageBoxShow("Please Enter Receiving Guarantee Code", Me.Text)
                fndReceiving.Focus()
                fndReceiving.Select()
                Return False
            End If

            Dim amount As Double = clsDBFuncationality.getSingleValue("select (coalesce(amount,0)-coalesce(amt,0)) from tspl_bank_guarantee_master  Left join (select SUM(amount) as amt ,receiving_code from tspl_bank_guarantee_master where receiving_code is not null group by receiving_code) tt on tt.receiving_code=tspl_bank_guarantee_master.docno where DocNo='" & fndReceiving.Value & "' ")
            If amount < clsCommon.myCdbl(txtamount.Text) And clsCommon.myCstr(CmbGuaranteeType.SelectedValue) = "RT" Then
                clsCommon.MyMessageBoxShow("Please Enter Amount Less then Receiving Amount [" & amount & "] ", Me.Text)
                txtamount.Focus()
                txtamount.Select()
                Return False
            End If

            If clsCommon.myLen(txtstrtdate.Value) = 0 Then
                clsCommon.MyMessageBoxShow("Please Enter Start Date Of Guarantee", Me.Text)
                txtstrtdate.Focus()
                txtstrtdate.Select()
                Return False
            End If

            If clsCommon.myLen(txtenddate.Value) = 0 Then
                clsCommon.MyMessageBoxShow("Please Enter End Date Of Guarantee", Me.Text)
                txtenddate.Focus()
                txtenddate.Select()
                Return False
            End If

            '' Anubhooti 26-Sep-2014
            If clsCommon.myLen(txtvendorcode.Value) > 0 Then
                If clsCommon.CompairString(ddltype.Text, "Vendor") = CompairStringResult.Equal Then
                    Dim VenCode As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_VENDOR_MASTER  Where Vendor_Code  ='" + clsCommon.myCstr(txtvendorcode.Value) + "'"))
                    If VenCode = 0 Then
                        clsCommon.MyMessageBoxShow("Please check ! Vendor code does not exist")
                        txtvendorcode.Focus()
                        txtvendorcode.Select()
                        Return False
                    End If
                ElseIf clsCommon.CompairString(ddltype.Text, "Customer") = CompairStringResult.Equal Then
                    Dim CustCode As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Count(*) As Row  From TSPL_CUSTOMER_MASTER  Where Cust_Code  ='" + clsCommon.myCstr(txtvendorcode.Value) + "'"))
                    If CustCode = 0 Then
                        clsCommon.MyMessageBoxShow("Please check ! Customer code does not exist")
                        txtvendorcode.Focus()
                        txtvendorcode.Select()
                        Return False
                    End If
                End If
            End If
            ''
            '==========Rohit=28-Oct-2014============
            If clsCommon.myLen(CmbGuaranteeType.SelectedValue) <= 0 Then
                clsCommon.MyMessageBoxShow("Please select Guarantee Type.")
                CmbGuaranteeType.Select()
                Return False
            End If
            '==============================
            If clsCommon.myCdbl(txtamount.Text) <= 0 Then
                clsCommon.MyMessageBoxShow("Please Enter Guarantee Amount", Me.Text)
                txtamount.Focus()
                txtamount.Select()
                Return False
            End If


            If clsCommon.myCDate(txtextnddate.Value) < clsCommon.myCDate(txtenddate.Value) Then
                clsCommon.MyMessageBoxShow("Extended Date Should Be Greater Or Equal To End Date", Me.Text)
                txtextnddate.Focus()
                txtextnddate.Select()
                Return False
            End If



            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If AllowToSave() Then SaveData()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        If clsCommon.myLen(txtcode.Value) = 0 Then
            clsCommon.MyMessageBoxShow("Please Select Guarantee Code For Deletion", Me.Text)
            txtcode.Focus()
            txtcode.Select()
            Return
        End If

        Dim qry As String = "select count(*) from tspl_bank_guarantee_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and docno='" + txtcode.Value + "'"
        Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
        If check <= 0 Then
            clsCommon.MyMessageBoxShow("No Such Record Found For Deletion", Me.Text)
            Return
        End If

        If Not (common.clsCommon.MyMessageBoxShow("Delete the Guarantee Code " + txtcode.Value + "" + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = DialogResult.Yes) Then
            Return
        End If

        Dim isdelete As Boolean = clsBankGuaranteeMaster.DeleteData(txtcode.Value)

        If isdelete Then
            clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
            Reset()
        Else
            clsCommon.MyMessageBoxShow("Data Not Deleted", Me.Text)
        End If
    End Sub

    Private Sub btnpost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If clsCommon.myLen(txtcode.Value) = 0 Then
                clsCommon.MyMessageBoxShow("Please Select Guarantee Code For Posting", Me.Text)
                txtcode.Focus()
                txtcode.Select()
                Return
            End If

            Dim qry As String = "select count(*) from tspl_bank_guarantee_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and docno='" + txtcode.Value + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check <= 0 Then
                clsCommon.MyMessageBoxShow("No Such Record Found For Posting", Me.Text)
                Return
            End If

            qry = "update tspl_bank_guarantee_master set status='Y' where comp_code='" + objCommonVar.CurrentCompanyCode + "' and docno='" + txtcode.Value + "'"
            clsDBFuncationality.getSingleValue(qry)
            clsCommon.MyMessageBoxShow("Data Posted Successfully", Me.Text)

            UsLock1.Status = ERPTransactionStatus.Approved
            btnsave.Enabled = False
            btndelete.Enabled = False
            btnpost.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FrmBankGuaranteeMaster1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()

        txtcode.Focus()
        txtcode.Select()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnpost, "Press Alt+P for Post")
        If clsCommon.myLen(strPaymentNo) > 0 Then
            LoadData(strPaymentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub txtcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtcode._MYNavigator
        Try
            Dim qry As String = "select count(*) from tspl_bank_guarantee_master where docno='" + txtcode.Value + "' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                txtcode.MyReadOnly = True
            ElseIf check <= 0 Then
                txtcode.MyReadOnly = False
            End If

            LoadData(txtcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub txtcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtcode._MYValidating
        Dim qry As String = ""

        If clsCommon.myLen(txtcode.Value) > 0 Then
            qry = "select count(*) from tspl_bank_guarantee_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and docno='" + txtcode.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check <= 0 Then
                Return
            End If
        End If
        qry = "select distinct tspl_bank_guarantee_master.docno as GuaranteeCode,tspl_bank_guarantee_master.Description,tspl_bank_guarantee_master.Start_Date as [Start Date],tspl_bank_guarantee_master.end_date as [End Date],tspl_bank_guarantee_master.bank_code as [FD A/C No.],tspl_bank_master.description as [Bank Name],tspl_bank_guarantee_master.vendor_code as [Vendor Code],tspl_vendor_master.vendor_name as [Vendor Name],tspl_bank_guarantee_master.amount as [Guarantee Amount],tspl_bank_guarantee_master.reminder_days as [Enddate Reminder before Days],tspl_bank_guarantee_master.Extnd_Reminder_Days as [Extended Reminder Days],tspl_bank_guarantee_master.Remarks from tspl_bank_guarantee_master left outer join tspl_bank_master on tspl_bank_master.bank_code=tspl_bank_guarantee_master.bank_code left outer join tspl_vendor_master on tspl_vendor_master.vendor_code=tspl_bank_guarantee_master.vendor_code "
        Dim whrClas As String = " tspl_bank_guarantee_master.comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        'Reset()
        txtcode.Value = clsCommon.ShowSelectForm("BNKFND", qry, "GuaranteeCode", whrClas, txtcode.Value, "GuaranteeCode", isButtonClicked)
        LoadData(txtcode.Value, NavigatorType.Current)
    End Sub

    Public Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        obj = clsBankGuaranteeMaster.GetData(strCode, NavTyep)

        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.code) > 0 Then
            txtcode.Value = obj.code
            txtdocdate.Text = obj.docdate
            txtdesc.Text = obj.desc
            txtstrtdate.Value = obj.strtdate
            txtenddate.Value = obj.enddate
            txtextnddate.Value = obj.extnddate
            txtfdCode.Value = obj.bankcode
            txtfddesc.Text = obj.bankdesc
            txtvendorcode.Value = obj.vndrcode
            txtvendrname.Text = obj.vndrname
            txtamount.Text = obj.amount
            txtreminderdays.Text = obj.rimnder
            txtremarks.Text = obj.remarks
            txtextndreminder.Text = obj.extndreminder
            '' Anubhooti 24-Sep-2014 BM00000004063
            ddltype.Text = clsCommon.myCstr(obj.Type)
            CmbGuaranteeType.SelectedValue = clsCommon.myCstr(obj.Bank_Guarantee_Type)
            If obj.Bank_Guarantee_Type = "RT" Then
                txtvendorcode.Enabled = False
                txtvendrname.Enabled = False
                txtenddate.Enabled = False
                txtextnddate.Enabled = False
                txtstrtdate.Enabled = False
                ddltype.Enabled = False
            Else
                txtvendorcode.Enabled = True
                txtvendrname.Enabled = True
                txtenddate.Enabled = True
                txtextnddate.Enabled = True
                txtstrtdate.Enabled = True
                ddltype.Enabled = True
            End If
            If clsCommon.CompairString(ddltype.Text, "Vendor") = CompairStringResult.Equal Then
                LblVenCode.Text = "Vendor Code"
            ElseIf clsCommon.CompairString(ddltype.Text, "Customer") = CompairStringResult.Equal Then
                LblVenCode.Text = "Customer Code"
            End If
            txtcode.MyReadOnly = True
            If obj.post = "Y" Then
                UsLock1.Status = ERPTransactionStatus.Approved
                btnsave.Enabled = False
                btndelete.Enabled = False
                btnpost.Enabled = False
            Else
                UsLock1.Status = ERPTransactionStatus.Pending
                btnsave.Text = "Update"
                btnsave.Enabled = True
                btndelete.Enabled = True
                btnpost.Enabled = True
            End If
        End If
    End Sub

    Public Sub GetBankGuaranteeType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "RC"
        dr("Name") = "Receiving"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "RT"
        dr("Name") = "Return"
        dt.Rows.Add(dr)

        CmbGuaranteeType.DataSource = dt
        CmbGuaranteeType.ValueMember = "Code"
        CmbGuaranteeType.DisplayMember = "Name"

    End Sub

    Private Sub txtfdCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtfdCode._MYValidating
        'Dim qry As String = "select distinct bank_code as Code,Description,(add1+' '+add2+' '+add3+' '+add4) as Address,City,State,inactive as Status,bankaccnumber as [Bank A/C No.],bankacc as [Bank Account],cheque_validity_in_days as [Validity Of Cheque] from TSPL_BANK_MASTER"
        'txtfdCode.Value = clsCommon.ShowSelectForm("BANKFND", qry, "Code", "", txtfdCode.Value, "", isButtonClicked)

        'If clsCommon.myLen(txtfdCode.Value) > 0 Then
        '    txtfddesc.Text = clsDBFuncationality.getSingleValue("select distinct description from TSPL_BANK_MASTER where bank_code='" + txtfdCode.Value + "'")
        'End If
        '' Anubhooti 24-Sep-2014
        Dim whrcls As String = ""
        Dim qry As String = clsERPFuncationality.glbankquery(whrcls)
        'txtfdCode.Value = clsVendorBankMaster.GetFinder("", txtfdCode.Value, isButtonClicked)
        txtfdCode.Value = clsBankMaster.getFinder("", txtfdCode.Value, isButtonClicked)
        txtfddesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_bank_master where bank_code='" & txtfdCode.Value & "'"))
        'If clsCommon.myLen(txtfdCode.Value) > 0 Then
        '    '    If clsCommon.CompairString(ddltype.Text, "Vendor") = CompairStringResult.Equal Then
        '    '        txtvendorcode.Value = ""
        '    '        txtvendrname.Text = ""
        '    '    End If
        '    '    Dim obj As clsVendorBankMaster = clsVendorBankMaster.GetData(txtfdCode.Value, NavigatorType.Current)
        '    '    If obj Is Nothing Then
        '    '        Exit Sub
        '    '    End If
        '    '    txtfddesc.Text = obj.Bank_Name
        '    'Else
        '    '    txtfddesc.Text = ""
        '    '    If clsCommon.CompairString(ddltype.Text, "Vendor") = CompairStringResult.Equal Then
        '    '        txtvendorcode.Value = ""
        '    '        txtvendrname.Text = ""
        '    '    End If

        'End If
    End Sub

    Private Sub FinderBasedOnType(ByVal isButtonClick As Boolean, ByVal Type As String)
        Dim qry As String = ""
        Dim whrcls As String = ""

        '' Anubhooti 24-Sep-2014
        If clsCommon.CompairString(Type, "Vendor") = CompairStringResult.Equal Then
            qry = "select distinct TSPL_VENDOR_MASTER.vendor_code as Code,TSPL_VENDOR_MASTER.vendor_name as [Vendor Name],(TSPL_VENDOR_MASTER.add1+' '+TSPL_VENDOR_MASTER.add2+' '+TSPL_VENDOR_MASTER.add3) as Address,TSPL_VENDOR_GROUP.group_desc as [Vendor Group],TSPL_VENDOR_MASTER.Email,TSPL_VENDOR_MASTER.contact_person_name as [COntact Person],TSPL_VENDOR_MASTER.contact_person_phone as [Contact No.] from TSPL_VENDOR_MASTER left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.ven_group_code=TSPL_VENDOR_MASTER.vendor_group_code "
            ' whrcls = " TSPL_VENDOR_MASTER.bank_code='" + txtfdCode.Value + "'"
            whrcls = "  TSPL_VENDOR_MASTER.Status='N'"
        ElseIf clsCommon.CompairString(Type, "Customer") = CompairStringResult.Equal Then
            qry = " SELECT DISTINCT TSPL_CUSTOMER_MASTER.Cust_Code  AS Code,TSPL_CUSTOMER_MASTER.Customer_Name  AS [Customer Name],(TSPL_CUSTOMER_MASTER.Add1 +' '+TSPL_CUSTOMER_MASTER.Add2 +' '+TSPL_CUSTOMER_MASTER.Add3) AS [Customer Address],TSPL_CUSTOMER_MASTER.Email  AS [Email],TSPL_CUSTOMER_MASTER.Contact_Person_Name  AS [Contact Person],TSPL_CUSTOMER_MASTER.Contact_Person_Phone AS [Contact Persone No.],TSPL_CUSTOMER_MASTER.Contact_Person_Email AS [Contact Person Email] FROM TSPL_CUSTOMER_MASTER "
        End If


        txtvendorcode.Value = clsCommon.ShowSelectForm("VNDRFND", qry, "Code", whrcls, txtvendorcode.Value, "", isButtonClick)

        If clsCommon.myLen(txtvendorcode.Value) > 0 Then
            If clsCommon.CompairString(Type, "Vendor") = CompairStringResult.Equal Then
                txtvendrname.Text = clsDBFuncationality.getSingleValue("select distinct vendor_name from tspl_vendor_master where vendor_code='" + txtvendorcode.Value + "'")
                Dim e As New System.EventArgs
                FndReceiving__MYValidating("", e, False)
            ElseIf clsCommon.CompairString(Type, "Customer") = CompairStringResult.Equal Then
                txtvendrname.Text = clsDBFuncationality.getSingleValue("select Isnull(Customer_Name,'') As Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtvendorcode.Value + "'")
            End If
        Else
            txtvendrname.Text = ""
        End If
    End Sub
    Private Sub txtvendorcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtvendorcode._MYValidating
        'If clsCommon.myLen(txtfdCode.Value) = 0 Then
        '    clsCommon.MyMessageBoxShow("Please Select FD Account First", Me.Text)
        '    txtfdCode.Focus()
        '    txtfdCode.Select()
        '    Return
        'End If

        'Dim qry As String = "select distinct TSPL_VENDOR_MASTER.vendor_code as Code,TSPL_VENDOR_MASTER.vendor_name as [Vendor Name],(TSPL_VENDOR_MASTER.add1+' '+TSPL_VENDOR_MASTER.add2+' '+TSPL_VENDOR_MASTER.add3) as Address,TSPL_VENDOR_GROUP.group_desc as [Vendor Group],TSPL_VENDOR_MASTER.Email,TSPL_VENDOR_MASTER.contact_person_name as [COntact Person],TSPL_VENDOR_MASTER.contact_person_phone as [Contact No.] from TSPL_VENDOR_MASTER left outer join TSPL_VENDOR_GROUP on TSPL_VENDOR_GROUP.ven_group_code=TSPL_VENDOR_MASTER.vendor_group_code "
        'Dim whrcls As String = " TSPL_VENDOR_MASTER.bank_code='" + txtfdCode.Value + "'"
        'txtvendorcode.Value = clsCommon.ShowSelectForm("VNDRFND", qry, "Code", whrcls, txtvendorcode.Value, "", isButtonClicked)

        'If clsCommon.myLen(txtvendorcode.Value) > 0 Then
        '    txtvendrname.Text = clsDBFuncationality.getSingleValue("select distinct vendor_name from tspl_vendor_master where vendor_code='" + txtvendorcode.Value + "'")
        'End If

        If clsCommon.CompairString(ddltype.Text, "Vendor") = CompairStringResult.Equal Then
            'If clsCommon.myLen(txtfdCode.Value) = 0 Then
            '    clsCommon.MyMessageBoxShow("Please Select FD Account First", Me.Text)
            '    txtvendorcode.Value = ""
            '    txtfdCode.Focus()
            '    'txtfdCode.Select()
            '    Exit Sub
            'End If
            FinderBasedOnType(isButtonClicked, "Vendor")

        ElseIf clsCommon.CompairString(ddltype.Text, "Customer") = CompairStringResult.Equal Then
            FinderBasedOnType(isButtonClicked, "Customer")
        End If
    End Sub

    Private Sub txtamount_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs)
        'Try
        '    'Convert.ToDecimal(txtamount.Text)
        'Catch ex As Exception
        '    clsCommon.MyMessageBoxShow(ex.Message)
        '    txtamount.Text = "0"
        'End Try
    End Sub

    Private Sub txtreminderdays_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtreminderdays.Validating
        Try
            Convert.ToDecimal(txtreminderdays.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            txtreminderdays.Text = "0"
        End Try
    End Sub

    Private Sub txtextnddate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtextnddate.Validating
        Try
            Convert.ToDateTime(txtextnddate.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            txtextnddate.Value = ""
            Return
        End Try
    End Sub

    Private Sub txtstrtdate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtstrtdate.Validating
        Try
            Convert.ToDateTime(txtstrtdate.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            txtstrtdate.Value = clsCommon.GETSERVERDATE()
        End Try
    End Sub

    Private Sub txtenddate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtenddate.Validating
        Try
            Convert.ToDateTime(txtenddate.Value)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            txtenddate.Value = ""
        End Try
    End Sub

    Private Sub txtdocdate_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtdocdate.Validating
        Try
            Convert.ToDateTime(txtdocdate.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            txtdocdate.Value = clsCommon.GETSERVERDATE()
        End Try
    End Sub

    Private Sub txtextndreminder_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtextndreminder.Validating
        Try
            Convert.ToDecimal(txtextndreminder.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
            txtextndreminder.Text = "0"
        End Try
    End Sub

    Private Sub FrmBankGuaranteeMaster1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnsave.Enabled = True Then
            If AllowToSave() Then SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnpost.Enabled = True Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btndelete.Enabled = True Then
            DeleteData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub ddltype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddltype.SelectedIndexChanged
        If clsCommon.CompairString(ddltype.Text, "Vendor") = CompairStringResult.Equal Then
            LblVenCode.Text = "Vendor Code"
            txtvendorcode.Value = ""
            txtvendrname.Text = ""
            txtfdCode.MendatroryField = True
        ElseIf clsCommon.CompairString(ddltype.Text, "Customer") = CompairStringResult.Equal Then
            LblVenCode.Text = "Customer Code"
            txtvendorcode.Value = ""
            txtvendrname.Text = ""
            txtfdCode.MendatroryField = False
        End If
    End Sub

    Private Sub FndReceiving__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndReceiving._MYValidating
        Dim qry As String = ""

        If clsCommon.myLen(txtcode.Value) > 0 Then
            qry = "select count(*) from tspl_bank_guarantee_master where comp_code='" + objCommonVar.CurrentCompanyCode + "' and docno='" + fndReceiving.Value + "'"
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check <= 0 Then
                Return
            End If
        End If
        qry = "select distinct tspl_bank_guarantee_master.docno as GuaranteeCode,tspl_bank_guarantee_master.Description,tspl_bank_guarantee_master.Start_Date " _
        & " as [Start Date],tspl_bank_guarantee_master.end_date as [End Date],tspl_bank_guarantee_master.extended_date as [Extended Date],tspl_bank_guarantee_master.bank_code as [FD A/C No.],tspl_bank_master.description " _
        & " as [Bank Name],tspl_bank_guarantee_master.vendor_code as [Vendor Code],tspl_vendor_master.vendor_name as [Vendor Name],tspl_bank_guarantee_master." _
        & "amount as [Guarantee Amount],tspl_bank_guarantee_master.reminder_days as [Enddate Reminder before Days],tspl_bank_guarantee_master.Extnd_Reminder_Days as " _
        & " [Extended Reminder Days],tspl_bank_guarantee_master.Remarks from tspl_bank_guarantee_master " _
        & " Left join (select SUM(amount) as amt ,receiving_code from tspl_bank_guarantee_master where receiving_code is not null group by receiving_code) tt on tt.receiving_code=tspl_bank_guarantee_master.docno " _
        & " left outer join tspl_bank_master on tspl_bank_master.bank_code=tspl_bank_guarantee_master.bank_code left outer join tspl_vendor_master on " _
        & " tspl_vendor_master.vendor_code=tspl_bank_guarantee_master.vendor_code "
        Dim whrClas As String = " tspl_bank_guarantee_master.comp_code='" + objCommonVar.CurrentCompanyCode + "' and bank_guarantee_type='RC' and (coalesce(amount,0)-coalesce(amt,0))>0"
        'Reset()
        If clsCommon.myLen(txtvendorcode.Value) > 0 Then
            whrClas &= " and tspl_bank_guarantee_master.vendor_code='" & txtvendorcode.Value & "'"
        End If
        fndReceiving.Value = clsCommon.ShowSelectForm("BNKFNDR", qry, "GuaranteeCode", whrClas, txtcode.Value, "GuaranteeCode", isButtonClicked)
        qry = qry & " where " & whrClas & " and tspl_bank_guarantee_master.docno='" & fndReceiving.Value & "'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 Then
            txtvendorcode.Value = dt.Rows(0)("vendor code")
            txtvendrname.Text = dt.Rows(0)("vendor name")
            txtenddate.Value = dt.Rows(0)("end date")
            txtextnddate.Value = dt.Rows(0)("Extended date")
            txtstrtdate.Value = dt.Rows(0)("start date")

            txtvendorcode.Enabled = False
            txtvendrname.Enabled = False
            txtenddate.Enabled = False
            txtextnddate.Enabled = False
            txtstrtdate.Enabled = False
            ddltype.Enabled = False
        End If
    End Sub

    Private Sub CmbGuaranteeType_SelectedIndexChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles CmbGuaranteeType.SelectedIndexChanged
        Try
            If clsCommon.myCstr(CmbGuaranteeType.SelectedValue) = "RT" Then
                LblReceivingCode.Visible = True
                fndReceiving.Visible = True
            Else
                LblReceivingCode.Visible = False
                fndReceiving.Visible = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
        End Try
    End Sub
End Class
