'--------Created By Monika 14/07/2014---------BM00000003051
Imports common
Imports System.Data.SqlClient

Public Class FrmFormIssueReceiptEntry
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim Errorcontrol As clsErrorControl = New clsErrorControl()

    Const colSno As String = "SNO"
    Const colFormserialno As String = "SerialNo"
    Const colIssueDate As String = "IssueDate"
    Const colVendorCode As String = "VendorCode"
    Const colVendorName As String = "VendorName"
    Const colCustomerCode As String = "CustCode"
    Const colCustomerName As String = "CustName"
    Const colBillNo As String = "BillNo"
    Const colBillDate As String = "BillDate"
    Const colAmount As String = "Amount"
    Const colPONo As String = "PO_NO"
    Const colsaleinvoiceno As String = "SO_No"

    Dim isInsideLoaddata As Boolean = False
    Dim isValuechanged As Boolean = True

    Dim repoissuedate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
    Dim repopono As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim reposono As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repovendorcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repovname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repocustcode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    Dim repocname As GridViewTextBoxColumn = New GridViewTextBoxColumn()
    
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmFormIssueReceiptEntry)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
            Exit Sub
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Sub Reset()
        rbtncustomer.IsChecked = False
        rbtnvendor.IsChecked = True
        rbtnissue.IsChecked = False
        rbtnrecv.IsChecked = False
        txtformseriescode.Value = ""
        txtformcode.Text = ""
        txtformdesc.Text = ""
        txtformtype.Text = ""

        gv.Rows.Clear()

        btnsave.Text = "&Save"
        btndelete.Enabled = False

        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
    End Sub

    Private Sub FrmFormIssueReceiptEntry_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadBlankGrid()
        Reset()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S/U for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New Trasnaction")
    End Sub

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim reposno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposno.FormatString = ""
        reposno.Name = colSno
        reposno.Width = 60
        reposno.ReadOnly = True
        reposno.HeaderText = "S.No."
        gv.MasterTemplate.Columns.Add(reposno)

        Dim reposerialno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reposerialno.FormatString = ""
        reposerialno.Name = colFormserialno
        reposerialno.Width = 120
        reposerialno.ReadOnly = True
        reposerialno.HeaderText = "Form Serial No."
        gv.MasterTemplate.Columns.Add(reposerialno)


        repoissuedate.FormatString = "{0:d}"
        repoissuedate.Name = colIssueDate
        repoissuedate.Width = 60
        repoissuedate.HeaderText = "Issue Date"
        gv.MasterTemplate.Columns.Add(repoissuedate)

        
        repovendorcode.FormatString = ""
        repovendorcode.Name = colVendorCode
        repovendorcode.Width = 110
        repovendorcode.HeaderText = "Vendor Code"
        repovendorcode.HeaderImage = Global.XpertERPPurchase.My.Resources.Resources.search4
        repovendorcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repovendorcode)

        
        repovname.FormatString = ""
        repovname.Name = colVendorName
        repovname.Width = 230
        repovname.ReadOnly = True
        repovname.HeaderText = "Vendor Name"
        gv.MasterTemplate.Columns.Add(repovname)

        
        repocustcode.FormatString = ""
        repocustcode.Name = colCustomerCode
        repocustcode.Width = 110
        repocustcode.HeaderText = "Customer Code"
        repocustcode.HeaderImage = Global.XpertERPPurchase.My.Resources.Resources.search4
        repocustcode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocustcode)


        repocname.FormatString = ""
        repocname.Name = colCustomerName
        repocname.Width = 230
        repocname.ReadOnly = True
        repocname.HeaderText = "Customer Name"
        gv.MasterTemplate.Columns.Add(repocname)

        Dim repobillno As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repobillno.FormatString = ""
        repobillno.Name = colBillNo
        repobillno.Width = 100
        repobillno.HeaderText = "Bill No."
        gv.MasterTemplate.Columns.Add(repobillno)

        Dim repobilldate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repobilldate.FormatString = "{0:d}"
        repobilldate.Name = colBillDate
        repobilldate.Width = 60
        repobilldate.HeaderText = "Bill Date"
        gv.MasterTemplate.Columns.Add(repobilldate)

        Dim repoamt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoamt.FormatString = ""
        repoamt.Name = colAmount
        repoamt.Width = 70
        repoamt.DecimalPlaces = 2
        repoamt.HeaderText = "Amount"
        gv.MasterTemplate.Columns.Add(repoamt)

        repopono.FormatString = ""
        repopono.Name = colPONo
        repopono.Width = 80
        repopono.HeaderText = "PO No."
        repopono.HeaderImage = Global.XpertERPPurchase.My.Resources.Resources.search4
        repopono.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repopono)


        reposono.FormatString = ""
        reposono.Name = colsaleinvoiceno
        reposono.Width = 80
        reposono.HeaderText = "Sale Invoice No."
        'reposno.IsVisible = False
        reposono.HeaderImage = Global.XpertERPPurchase.My.Resources.Resources.search4
        reposono.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(reposono)

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = False
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.TableElement.TableHeaderHeight = 40
    End Sub

    Function AllowToSave() As Boolean
        Try
            If clsCommon.myLen(txtformseriescode.Value) <= 0 Then
                Errorcontrol.SetError(txtformcode, "Please fill Form Code")
                txtformseriescode.Focus()
                txtformseriescode.Select()
                Throw New Exception("Please fill Form Code")
            Else
                Errorcontrol.ResetError(txtformcode)
            End If

            If Not rbtnissue.IsChecked AndAlso Not rbtnrecv.IsChecked Then
                Errorcontrol.SetError(RadGroupBox3, "Please select any one option from Issue/Receipt Entry")
                RadGroupBox3.Focus()
                RadGroupBox3.Select()
                Throw New Exception("Please select any one option from Issue/Receipt Entry")
            Else
                Errorcontrol.ResetError(RadGroupBox3)
            End If

            If Not rbtncustomer.IsChecked AndAlso Not rbtnvendor.IsChecked Then
                Errorcontrol.SetError(RadGroupBox4, "Please select any one type from Customer/Vendor")
                RadGroupBox4.Focus()
                RadGroupBox4.Select()
                Throw New Exception("Please select any one type from Customer/Vendor")
            Else
                Errorcontrol.ResetError(RadGroupBox4)
            End If

            Dim formserialno As String = ""
            Dim iss_date As Date = Nothing
            Dim vcode As String = ""
            Dim billno As String = ""
            Dim billdt As Date = Nothing
            Dim amt As Decimal = 0
            Dim po_no As String = ""
            Dim so_no As String = ""
            Dim title As String = ""

            formserialno = clsCommon.myCstr(gv.Rows(0).Cells(0).Value)

            If clsCommon.myCstr(formserialno) <= 0 Then
                gv.Focus()
                gv.Select()
                Throw New Exception("There is no data found in grid for saving")
            End If

            Dim count As Integer = 1

            For Each grow As GridViewRowInfo In gv.Rows
                formserialno = clsCommon.myCstr(grow.Cells(colFormserialno).Value)
                iss_date = clsCommon.myCDate(grow.Cells(colIssueDate).Value)
                If clsCommon.CompairString(iss_date, "12:00:00 AM") = CompairStringResult.Equal Then
                    iss_date = clsCommon.GETSERVERDATE()
                End If

                billno = clsCommon.myCstr(grow.Cells(colBillNo).Value)
                billdt = clsCommon.myCDate(grow.Cells(colBillDate).Value)
                If clsCommon.CompairString(billdt, "12:00:00 AM") = CompairStringResult.Equal Then
                    billdt = clsCommon.GETSERVERDATE()
                End If

                amt = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                If rbtnvendor.IsChecked Then
                    vcode = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                ElseIf rbtncustomer.IsChecked Then
                    vcode = clsCommon.myCstr(grow.Cells(colCustomerCode).Value)
                End If
                If rbtnissue.IsChecked Then
                    title = "Issue"
                Else
                    title = "Receipt"
                End If
                po_no = clsCommon.myCstr(grow.Cells(colPONo).Value)
                so_no = clsCommon.myCstr(grow.Cells(colsaleinvoiceno).Value)

                If clsCommon.myLen(formserialno) > 0 Then
                    If clsCommon.myLen(iss_date) <= 0 Or clsCommon.CompairString(iss_date, "12:00:00 AM") = CompairStringResult.Equal Then
                        Errorcontrol.SetError(gv, "Please fill " + title + " Date")
                        Throw New Exception("Please fill " + title + " Date at row no. " + clsCommon.myCstr(count) + "")
                    Else
                        Errorcontrol.ResetError(gv)
                    End If

                    If clsCommon.myLen(vcode) <= 0 AndAlso rbtnvendor.IsChecked Then
                        Errorcontrol.SetError(gv, "Please Select Vendor Code")
                        Throw New Exception("Please Select Vendor Code at row no. " + clsCommon.myCstr(count) + "")
                    Else
                        Errorcontrol.ResetError(gv)
                    End If

                    If clsCommon.myLen(vcode) <= 0 AndAlso rbtncustomer.IsChecked Then 'clsCommon.myLen(billno) > 0 AndAlso 
                        Errorcontrol.SetError(gv, "Please Select Customer Code")
                        Throw New Exception("Please Select Customer Code at row no. " + clsCommon.myCstr(count) + "")
                    Else
                        Errorcontrol.ResetError(gv)
                    End If

                    If clsCommon.myLen(billno) > 0 AndAlso (clsCommon.myLen(billdt) <= 0 Or clsCommon.CompairString(billdt, "12:00:00 AM") = CompairStringResult.Equal) Then
                        Errorcontrol.SetError(gv, "Please fill bill date")
                        Throw New Exception("Please fill bill date at row no. " + clsCommon.myCstr(count) + "")
                    Else
                        Errorcontrol.ResetError(gv)
                    End If

                    If clsCommon.myLen(billno) > 0 AndAlso clsCommon.myCdbl(amt) <= 0 Then
                        Errorcontrol.SetError(gv, "Please fill bill amount")
                        Throw New Exception("Please fill bill amount at row no. " + clsCommon.myCstr(count) + "")
                    Else
                        Errorcontrol.ResetError(gv)
                    End If

                    If clsCommon.myLen(billno) > 0 AndAlso clsCommon.myLen(po_no) <= 0 AndAlso clsCommon.myLen(so_no) <= 0 Then
                        Errorcontrol.SetError(gv, "Please fill any of Purchase Order/Sale Invoice No.")
                        Throw New Exception("Please fill any of Purchase Order/Sale Invoice No. at row no. " + clsCommon.myCstr(count) + "")
                    Else
                        Errorcontrol.ResetError(gv)
                    End If
                End If

                count += 1
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub SaveData()
        Try

            If MyBase.isModifyonPasswordFlag Then
                If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.FrmFormIssueReceiptEntry, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
                Else
                    Return
                End If
            End If
            Dim obj As New clsFormIssueReceiptEntry()

            obj.Docno = clsCommon.myCstr(txtformseriescode.Value)
            obj.Formcode = clsCommon.myCstr(txtformcode.Text)

            Dim arr As New List(Of clsFormIssueReceiptEntry)

            For Each grow As GridViewRowInfo In gv.Rows
                Dim objtr As New clsFormIssueReceiptEntry()

                objtr.sno = CInt(grow.Cells(colSno).Value)
                objtr.formserialno = clsCommon.myCstr(grow.Cells(colFormserialno).Value)
                objtr.issuedate = clsCommon.myCDate(grow.Cells(colIssueDate).Value)
                objtr.vendorcode = clsCommon.myCstr(grow.Cells(colVendorCode).Value)
                objtr.custcode = clsCommon.myCstr(grow.Cells(colCustomerCode).Value)
                objtr.billno = clsCommon.myCstr(grow.Cells(colBillNo).Value)
                objtr.billdate = clsCommon.myCDate(grow.Cells(colBillDate).Value)
                objtr.amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                objtr.POno = clsCommon.myCstr(grow.Cells(colPONo).Value)
                objtr.saleinvoiceno = clsCommon.myCstr(grow.Cells(colsaleinvoiceno).Value)
                If rbtnissue.IsChecked Then
                    objtr.iss_rcv = "ISSUE"
                Else
                    objtr.iss_rcv = "RECEIPT"
                End If

                If rbtncustomer.IsChecked Then
                    objtr.vendorcustomer_type = "CUSTOMER"
                Else
                    objtr.vendorcustomer_type = "VENDOR"
                End If

                If clsCommon.myLen(objtr.formserialno) > 0 Then
                    arr.Add(objtr)
                End If
            Next


            If arr IsNot Nothing AndAlso arr.Count > 0 Then
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                If clsFormIssueReceiptEntry.SaveData(obj, arr, trans) Then
                    If btnsave.Text = "&Save" Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                    End If
                    btnsave.Text = "&Update"
                    btndelete.Enabled = True

                    UcAttachment1.SaveData(txtformseriescode.Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        Try
            If clsCommon.myLen(txtformseriescode.Value) <= 0 Then
                Errorcontrol.SetError(txtformcode, "Please select form code for deletion")
                txtformseriescode.Focus()
                txtformseriescode.Select()
                Throw New Exception("Please select form code for deletion")
            Else
                Errorcontrol.ResetError(txtformcode)
            End If

            Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
            If clsFormIssueReceiptEntry.DeleteData(txtformseriescode.Value, trans) Then
                clsCommon.MyMessageBoxShow("Data Deleted Successfully", Me.Text)
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtformseriescode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtformseriescode._MYValidating
        txtformseriescode.Value = clsFormSerialNoMaster.GetFinder("", txtformseriescode.Value, isButtonClicked)

        If clsCommon.myLen(txtformseriescode.Value) > 0 Then
            txtformcode.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_code from TSPL_FORM_SERIAL_NO_MASTER where doc_no='" + txtformseriescode.Value + "'"))
            txtformdesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_name from TSPL_FORM_MASTER where form_code='" + txtformcode.Text + "'"))
            txtformtype.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select form_type from TSPL_FORM_MASTER where form_code='" + txtformcode.Text + "'"))
            LoadData()
        Else
            Reset()
        End If
    End Sub

    Sub LoadData()
        Try
            Dim obj As clsFormIssueReceiptEntry = clsFormIssueReceiptEntry.GetData(txtformseriescode.Value)

            gv.Rows.Clear()
            gv.Rows.AddNew()
            isInsideLoaddata = True
            'If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Docno) > 0 Then
            If obj.arr IsNot Nothing AndAlso obj.arr.Count > 0 Then

                For Each objtr As clsFormIssueReceiptEntry In obj.arr
                    gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = objtr.sno
                    gv.Rows(gv.Rows.Count - 1).Cells(colFormserialno).Value = objtr.formserialno
                    gv.Rows(gv.Rows.Count - 1).Cells(colIssueDate).Value = objtr.issuedate
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorCode).Value = objtr.vendorcode
                    gv.Rows(gv.Rows.Count - 1).Cells(colVendorName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + objtr.vendorcode + "' and form_type='ALL'"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colCustomerCode).Value = objtr.custcode
                    gv.Rows(gv.Rows.Count - 1).Cells(colCustomerName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + objtr.custcode + "'"))
                    gv.Rows(gv.Rows.Count - 1).Cells(colBillNo).Value = objtr.billno
                    gv.Rows(gv.Rows.Count - 1).Cells(colBillDate).Value = objtr.billdate
                    gv.Rows(gv.Rows.Count - 1).Cells(colAmount).Value = objtr.amount
                    gv.Rows(gv.Rows.Count - 1).Cells(colPONo).Value = objtr.POno
                    gv.Rows(gv.Rows.Count - 1).Cells(colsaleinvoiceno).Value = objtr.saleinvoiceno

                    rbtnissue.IsChecked = False
                    rbtnrecv.IsChecked = False
                    If objtr.iss_rcv = "ISSUE" Then
                        rbtnissue.IsChecked = True
                    End If

                    If objtr.vendorcustomer_type = "VENDOR" Then
                        rbtnvendor.IsChecked = True
                    Else
                        rbtncustomer.IsChecked = True
                    End If
                    gv.Rows.AddNew()
                Next
                'End If

                btnsave.Text = "&Update"
                btndelete.Enabled = True

                UcAttachment1.LoadData(txtformseriescode.Value)
            Else
                Dim qry As String = "select Prefix,Start_No,End_No from TSPL_FORM_SERIAL_NO_MASTER where doc_no='" + txtformseriescode.Value + "' and form_code='" + txtformcode.Text + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                gv.Rows.Clear()
                gv.Rows.AddNew()
                Dim sno As Integer = 1

                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    For i As Decimal = clsCommon.myCdbl(dt.Rows(0)("Start_No")) To clsCommon.myCdbl(dt.Rows(0)("End_No"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colSno).Value = clsCommon.myCstr(sno)
                        gv.Rows(gv.Rows.Count - 1).Cells(colFormserialno).Value = clsCommon.myCstr(dt.Rows(0)("Prefix")) + clsCommon.myCstr(i)
                        gv.Rows(gv.Rows.Count - 1).Cells(colIssueDate).Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                        gv.Rows.AddNew()
                        sno += 1
                    Next
                End If
            End If

            isInsideLoaddata = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Reset()
    End Sub

    Private Sub FrmFormIssueReceiptEntry_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyData = Keys.N Then
            btnNew.PerformClick()
        End If
        If e.KeyCode = Keys.F2 AndAlso gv.CurrentCell Is gv.Columns(colPONo) Then
            OpenPO(True)
        End If
        If e.KeyCode = Keys.F2 AndAlso gv.CurrentCell Is gv.Columns(colsaleinvoiceno) Then
            OpenSO(True)
        End If
        If e.KeyCode = Keys.F2 AndAlso gv.CurrentCell Is gv.Columns(colVendorCode) Then
            OpenVendorCode(True)
        End If
        If e.KeyCode = Keys.F2 AndAlso gv.CurrentCell Is gv.Columns(colCustomerCode) Then
            OpenCustomerCode(True)
        End If
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        If Not isInsideLoaddata Then
            If isValuechanged Then
                isValuechanged = False
                If e.Column Is gv.Columns(colVendorCode) Then
                    OpenVendorCode(False)
                End If
                If e.Column Is gv.Columns(colPONo) Then
                    OpenPO(False)
                End If
                If e.Column Is gv.Columns(colsaleinvoiceno) Then
                    OpenSO(False)
                End If
                If e.Column Is gv.Columns(colCustomerCode) Then
                    OpenCustomerCode(False)
                End If
            End If
            isValuechanged = True
        End If
    End Sub

    Sub OpenCustomerCode(ByVal isButtonClicked As Boolean)
        Dim vendorcode As String = ""
        vendorcode = clsCustomerMaster.getFinder(" ", clsCommon.myCstr(gv.CurrentRow.Cells(colCustomerCode).Value), isButtonClicked)

        If clsCommon.myLen(vendorcode) > 0 Then
            gv.CurrentRow.Cells(colCustomerCode).Value = vendorcode
            gv.CurrentRow.Cells(colCustomerName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select customer_name from tspl_customer_master where cust_code='" + vendorcode + "'"))
        Else
            gv.CurrentRow.Cells(colCustomerCode).Value = ""
            gv.CurrentRow.Cells(colCustomerName).Value = ""
        End If
    End Sub

    Sub OpenVendorCode(ByVal isButtonClicked As Boolean)
        Dim vendorcode As String = ""
        vendorcode = clsVendorMaster.getFinder(" TSPL_VENDOR_MASTER.form_type='ALL'", clsCommon.myCstr(gv.CurrentRow.Cells(colVendorCode).Value), isButtonClicked)

        If clsCommon.myLen(vendorcode) > 0 Then
            gv.CurrentRow.Cells(colVendorCode).Value = vendorcode
            gv.CurrentRow.Cells(colVendorName).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select vendor_name from tspl_vendor_master where vendor_code='" + vendorcode + "' and form_type='ALL'"))
        Else
            gv.CurrentRow.Cells(colVendorCode).Value = ""
            gv.CurrentRow.Cells(colVendorName).Value = ""
        End If
    End Sub

    Sub OpenPO(ByVal isButtonClicked As Boolean)
        Dim qry As String = "select a.PurchaseOrder_No as [Code],a.PurchaseOrder_Date as [Date],a.Vendor_Code as [Vendor Code],a.Vendor_Name as [Vendor Name],a.Terms_Code as [PO Terms],a.Mode_Of_Transport as [Mode of Transport],(isnull(a.PO_Total_Amt,0)-isnull(a.amount,0)) as Amount from (select TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No,TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_Date,TSPL_PURCHASE_ORDER_HEAD.Vendor_Code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_PURCHASE_ORDER_HEAD.PO_Total_Amt,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.amount,TSPL_PURCHASE_ORDER_HEAD.Terms_Code,TSPL_PURCHASE_ORDER_HEAD.Mode_Of_Transport from TSPL_PURCHASE_ORDER_HEAD left outer join TSPL_VENDOR_MASTER on TSPL_PURCHASE_ORDER_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code left outer join TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST on TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.purchaseorder_no=TSPL_PURCHASE_ORDER_HEAD.PurchaseOrder_No)a"
        Dim whrcls As String = " (isnull(a.PO_Total_Amt,0)-isnull(a.amount,0))>0 and a.vendor_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colVendorCode).Value) + "'"
        Dim xvalue As String = ""
        xvalue = clsCommon.ShowSelectForm("POFND", qry, "Code", whrcls, clsCommon.myCstr(gv.CurrentRow.Cells(colPONo).Value), "Code", isButtonClicked)

        If clsCommon.myLen(xvalue) > 0 Then
            gv.CurrentRow.Cells(colPONo).Value = xvalue
        Else
            gv.CurrentRow.Cells(colPONo).Value = ""
        End If
    End Sub

    Sub OpenSO(ByVal isButtonCLicked As Boolean)
        Dim qry As String = "select a.Document_Code as [Code],a.Document_Date as [Date],a.Customer_Code as [Customer Code],a.Customer_Name as [Customer Name],a.Terms_Code as [PO Terms],a.Bill_To_Location as [Location],(isnull(a.Total_Amt,0)-isnull(a.amount,0)) as Amount from (select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.amount,TSPL_SD_SALE_INVOICE_HEAD.Terms_Code,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_CUSTOMER_MASTER on TSPL_SD_SALE_INVOICE_HEAD.Customer_Code=TSPL_CUSTOMER_MASTER.Cust_Code left outer join TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST on TSPL_ROAD_PERMIT_ISSUE_RECEIVED_LIST.sale_invoice_no=TSPL_SD_SALE_INVOICE_HEAD.Document_Code)a "
        Dim whrcls As String = " (isnull(a.Total_Amt,0)-isnull(a.amount,0))>0 and a.customer_code='" + clsCommon.myCstr(gv.CurrentRow.Cells(colCustomerCode).Value) + "'"
        Dim xvalue As String = ""
        xvalue = clsCommon.ShowSelectForm("SOFND", qry, "Code", whrcls, clsCommon.myCstr(gv.CurrentRow.Cells(colPONo).Value), "Code", isButtonCLicked)

        If clsCommon.myLen(xvalue) > 0 Then
            gv.CurrentRow.Cells(colPONo).Value = xvalue
        Else
            gv.CurrentRow.Cells(colPONo).Value = ""
        End If
    End Sub

    Private Sub rbtncustomer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtncustomer.ToggleStateChanged, rbtnvendor.ToggleStateChanged
        If rbtncustomer.IsChecked Then
            repovendorcode.IsVisible = False
            repovname.IsVisible = False
            repocustcode.IsVisible = True
            repocname.IsVisible = True
        ElseIf rbtnvendor.IsChecked Then
            repovendorcode.IsVisible = True
            repovname.IsVisible = True
            repocustcode.IsVisible = False
            repocname.IsVisible = False
        End If
    End Sub

    Private Sub rbtnissue_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnissue.ToggleStateChanged, rbtnrecv.ToggleStateChanged
        If rbtnissue.IsChecked Then
            repoissuedate.HeaderText = "Issue Date"
        ElseIf rbtnrecv.IsChecked Then
            repoissuedate.HeaderText = "Receive Date"
        End If
    End Sub
End Class
