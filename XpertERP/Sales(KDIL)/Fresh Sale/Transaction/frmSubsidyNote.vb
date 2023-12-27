'' work done agaist ticket no. SWA/05/07/18-000031,SWA/10/07/18-000032
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports System.Data.SqlClient

Public Class frmSubsidyNote
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public IsLoadData As Boolean = False
    Public EntryNo As String = ""
    Public PeriodsSubsidy As Integer = 0
    Public PeriodofSubsidyCreditNote As Integer = 0

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click
        txtCustomerMult.arrValueMember = Nothing
        txtDocNo.Value = ""
        btnGo.Text = "Save"
        txtlocation.Value = ""
        LblLocDesp.Text = ""
        dtpPayment.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        rbtnCustomerSelect.IsChecked = True

    End Sub
    Private Sub rptCrateAccounting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R ")
        dtpPayment.Value = clsCommon.GETSERVERDATE()
        LoadCustomer()
        rbtnCustomerSelect.IsChecked = True
        btnGo.Enabled = False
        PeriodofSubsidyCreditNote = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Description from TSPL_FIXED_PARAMETER where Code='" & clsFixedParameterCode.PeriodofSubsidyCreditNote & "'"))
      
        '' Only Enable 15 and 30 
        Dim Qry As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" SELECT DAY(getdate()) AS [Day] "))
        Dim NExtDay As Integer = PeriodofSubsidyCreditNote + 15

        If clsCommon.CompairString(PeriodofSubsidyCreditNote, Qry) = CompairStringResult.Equal OrElse clsCommon.CompairString(Qry, NExtDay) = CompairStringResult.Equal Then
            btnGo.Enabled = True
        End If

    End Sub
    Private Sub txtlocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtlocation._MYValidating
        Try
            Dim qry As String = "select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
            Dim WhrCls As String = "Seg_No = '7' AND GIT='N'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  TSPL_LOCATION_MASTER.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtlocation.Value = clsCommon.ShowSelectForm("GLsegmentcode", qry, "Code", WhrCls, txtlocation.Value, "Code", isButtonClicked)
            If clsCommon.myLen(clsCommon.myCstr(txtlocation.Value)) > 0 Then
                LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
            Else
                LblLocDesp.Text = ""
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        txtCustomerMult.arrValueMember = Nothing
        txtlocation.Value = ""
        LblLocDesp.Text = ""
        txtDocNo.Value = ""
        btnGo.Text = "Save"
        dtpPayment.Value = clsCommon.GETSERVERDATE()
        rbtnCustomerAll.IsChecked = True
        LoadCustomer()
        rbtnCustomerSelect.IsChecked = True
      
    End Sub
    Private Sub txtCustomerMult__My_Click(sender As Object, e As EventArgs) Handles txtCustomerMult._My_Click
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master where IsDistributor='Y' order by Cust_Code"
        txtCustomerMult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtCustomerMult.arrValueMember, txtCustomerMult.arrDispalyMember)
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Dim issaved As Boolean = False
        Try
            Dim Dated As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")


            If clsCommon.myLen(txtlocation.Value) <= 0 Then
                Throw New Exception(" Select Location First")
            End If
            If rbtnCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
                Throw New Exception("Please select at least one customer")
            End If

            Dim DocumentDate As String = clsDBFuncationality.getSingleValue(" SELECT convert(varchar(12),Document_Date,103) as DocumentDate from TSPL_SUBSIDY_CRADIT_NOTE where convert(date,Document_Date,103)>=convert(date,'" & Dated & "',103) ", trans)
            Dim CustomerCode As String = clsDBFuncationality.getSingleValue(" SELECT Customer  from TSPL_SUBSIDY_CRADIT_NOTE where convert(date,Document_Date,103)>=convert(date,'" & Dated & "',103) and Customer in (" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") ", trans)
            If clsCommon.myLen(DocumentDate) AndAlso clsCommon.myLen(CustomerCode) > 0 Then
                btnGo.Enabled = False
                Throw New Exception("Already Create Credit Note. For Customer " & CustomerCode & "")
                Exit Sub
            End If

            Dim arrCust As New ArrayList()
            arrCust = cbgCustomer.CheckedValue

            Dim Sql As String = "SELECT MAX(document_No) as document_No from TSPL_SUBSIDY_CRADIT_NOTE "
            EntryNo = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Sql, trans))
            If clsCommon.myLen(EntryNo) > 0 Then
                EntryNo = clsCommon.incval(EntryNo)
            Else
                EntryNo = "CSN0000000001"
            End If

            Dim tmpList As New List(Of String)
            For Each value As String In arrCust
                createARInvoice(value, txtlocation.Value, dtpPayment.Value, EntryNo, trans)
            Next
            trans.Commit()
            fillData(EntryNo)
            If clsCommon.myLen(txtDocNo.Value) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "Data Saved", Me.Text)
            End If
        Catch ex As Exception
            trans.Rollback()
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub fillData(ByVal EntryNo As String)
        IsLoadData = True
        Dim i As Integer = 0
        Dim CountPost As Integer = 0
        Dim Qry As String = " select * from TSPL_SUBSIDY_CRADIT_NOTE where Document_No ='" + EntryNo + "' "
        Dim dt As DataTable
        Try
            dt = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                txtDocNo.Value = clsCommon.myCstr(dt.Rows(0).Item("Document_No"))
                dtpPayment.Value = clsCommon.myCDate(dt.Rows(0).Item("Document_Date"))
                txtlocation.Value = clsCommon.myCstr(dt.Rows(0).Item("Location"))
                LblLocDesp.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("SELECT ISNULL(Description,'') As Description FROM TSPL_GL_SEGMENT_CODE WHERE Segment_code ='" & clsCommon.myCstr(txtlocation.Value) & "'"))
                cbgCustomer.CheckedValue = GetCustomerCode()
                'btnGo.Enabled = False
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function GetCustomerCode() As ArrayList
        Dim qry As String = "select * from TSPL_SUBSIDY_CRADIT_NOTE where Document_No ='" + txtDocNo.Value + "' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim arr As ArrayList = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            arr = New ArrayList
            For Each dr As DataRow In dt.Rows
                arr.Add(clsCommon.myCstr(dr("Customer")))
            Next
        End If
        Return arr
    End Function
    Private Function createARInvoice(ByVal Customer As String, ByVal Location As String, ByVal DocumentDate As String, ByVal EntryNo As String, ByVal Trans As SqlTransaction) As Boolean
        Dim qrySubsidyAmt As String = " select SubsidyAmount from TSPL_CUSTOMER_MASTER where Cust_Code='" + Customer + "'"
        Dim CheckAmount As String = clsCommon.myLen(clsDBFuncationality.getSingleValue(qrySubsidyAmt, Trans))

        If clsCommon.CompairString(CheckAmount, "0") <> CompairStringResult.Equal Then

            Dim objCustInv As New clsCustomerInvoiceHead()

            objCustInv.Document_Date = DocumentDate
            objCustInv.Document_Type = "C"
            Dim qrySubsidy As String = " select SubsidyAmount from TSPL_CUSTOMER_MASTER where Cust_Code='" + Customer + "'"
            Dim FAmount As Double = clsDBFuncationality.getSingleValue(qrySubsidy, Trans)
            objCustInv.Document_Total = FAmount / 2

            objCustInv.Balance_Amt = FAmount / 2

            objCustInv.Customer_Code = Customer
            Dim qryCust_Name As String = " select Customer_name from TSPL_CUSTOMER_MASTER where Cust_Code='" + Customer + "'"
            objCustInv.Customer_Name = clsDBFuncationality.getSingleValue(qryCust_Name, Trans)
            objCustInv.Posting_Date = DocumentDate
            objCustInv.Description = "Against Transport Subsidy"
            objCustInv.Remarks = "Against Transport Subsidy"
            objCustInv.Against_Subsidy_No = EntryNo

            Dim qry As String = " select Cust_Account from TSPL_CUSTOMER_MASTER where Cust_Code='" + Customer + "'"
            objCustInv.Account_Set = clsDBFuncationality.getSingleValue(qry, Trans)

            objCustInv.loc_code = Location
            objCustInv.On_Hold = 0

            Dim dt As DataTable = clsDBFuncationality.GetDataTable("select Receivable_Control_acct,Receipts_Discount_acct from TSPL_CUSTOMER_ACCOUNT_SET where Cust_Account='" + objCustInv.Account_Set + "'", Trans)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                objCustInv.Customer_Control_AC = clsCommon.myCstr(dt.Rows(0)("Receivable_Control_acct"))
                objCustInv.Customer_Control_AC = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInv.Customer_Control_AC, Location, Trans)
            End If

            Dim counter As Integer = 1
            objCustInv.Arr = New List(Of clsCustomerInvoiceDetail)
            Dim objCustInvTR As clsCustomerInvoiceDetail = New clsCustomerInvoiceDetail()

            objCustInvTR.SNo = counter
            Dim qryAcccount_Code As String = " select SubSidy_Account from TSPL_CUSTOMER_ACCOUNT_SET left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Account=TSPL_CUSTOMER_ACCOUNT_SET.Cust_Account where TSPL_CUSTOMER_MASTER.Cust_Code='" + Customer + "'"
            objCustInvTR.GL_Account_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qryAcccount_Code, Trans))
            If clsCommon.myLen(objCustInvTR.GL_Account_Code) <= 0 Then
                Throw New Exception("Select SubSidy Account")
            End If

            objCustInvTR.GL_Account_Desc = clsGLAccount.GetName(objCustInvTR.GL_Account_Code, Trans)

            objCustInvTR.GL_Account_Code = clsERPFuncationality.ChangeGLAccountLocationSegment(objCustInvTR.GL_Account_Code, Location, Trans)

            Dim QrySubsidyAmount As String = " select SubSidyAmount from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.Cust_Code='" + Customer + "'"
            Dim FinalAmount As Double = clsDBFuncationality.getSingleValue(QrySubsidyAmount, Trans)

            objCustInvTR.Amount = FinalAmount / 2
            objCustInvTR.Total_Amount = FinalAmount / 2
            objCustInvTR.Amount_less_Discount = FinalAmount / 2


            objCustInv.Arr.Add(objCustInvTR)

            objCustInv.SaveData(objCustInv, True, Trans, clsUserMgtCode.frmSubsidyCreditNote)
            clsCustomerInvoiceHead.PostData(clsUserMgtCode.frmSubsidyCreditNote, objCustInv.Document_No, "", Trans)



            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Document_No", EntryNo)
            clsCommon.AddColumnsForChange(coll, "Document_Date", clsCommon.GetPrintDate(DocumentDate, "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "ARNo", objCustInv.Document_No)
            clsCommon.AddColumnsForChange(coll, "Location", Location)
            clsCommon.AddColumnsForChange(coll, "Customer", Customer)
            clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy"))
            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(Trans), "dd/MMM/yyyy"))
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SUBSIDY_CRADIT_NOTE", OMInsertOrUpdate.Insert, "", Trans)
       
        End If

        Return True

    End Function
    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCustomerAll.ToggleStateChanged, rbtnCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = rbtnCustomerSelect.IsChecked
    End Sub
    Sub LoadCustomer()
        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master where IsDistributor='Y'  and SubsidyAmount is not null "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try

            Dim qst As String = "select count(*) from TSPL_SUBSIDY_CRADIT_NOTE where Document_No='" + txtDocNo.Value + "'"
           
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            fillData(txtDocNo.Value)
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
      
        Dim qry As String = "select Document_No as DocumentNo,ARNo as [AR No], Customer, Location,convert(varchar,Document_Date,103) as [Document Date] from TSPL_SUBSIDY_CRADIT_NOTE "
        fillData(clsCommon.ShowSelectForm("CSN", qry, "DocumentNo", "", txtDocNo.Value, "convert(datetime,Document_Date,103) desc", isButtonClicked))

    End Sub
End Class