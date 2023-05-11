Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports System.Text
Imports common
Public Class FrmSecurityDeposit1
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub FrmSecurityDeposit1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpFromdate1.Value = System.DateTime.Now.Date()
        dtpToDate.Value = System.DateTime.Now.Date()
        LoadCustomer()
        SetUserMgmtNew()
        loadlocation()
        chkCustomerAll.IsChecked = True
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N Adding New Trasnaction")
        ButtonToolTip.SetToolTip(btnPrint, "Press Ctrl+P for Print ")
        'cbgLocation.DataSource = clsLocation.GetLocationSegments()

    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmSecurityDeposit1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub
    Private Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Cust_Group_Code as [Customer Group] from TSPL_CUSTOMER_MASTER order by  Cust_Code"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Private Sub chkCustomerSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerSelect.ToggleStateChanged
        cbgCustomer.Enabled = True 'Not chkCustomerAll.IsChecked
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Print()
    End Sub
    Sub loadlocation()
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub
    Sub Print()
        Try
            Dim LocFilter As String = ""
            Dim CustomerFilter As String = ""
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                LocFilter = clsCommon.GetMulcallString(cbgLocation.CheckedValue)
                LocFilter = LocFilter.Replace("'", "")
            End If
            If chkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                CustomerFilter = clsCommon.GetMulcallString(cbgCustomer.CheckedValue)
                CustomerFilter = CustomerFilter.Replace("'", "")
            End If



            Dim StrQuery As String = "select '" + LocFilter + "' as LocFilter,'" + CustomerFilter + "' as CustFilter,'" + dtpFromdate1.Value.Date + "' as [StartDate],'" + dtpToDate.Value.Date + "' as [EndDate], H.Cust_Code as [Code],max(H.Customer_Name) as [name],isnull(sum(case when L.Applied_Amount <0 then Applied_Amount*-1 end),0) as Debit,isnull(sum(case when L.Applied_Amount >=0 then Applied_Amount end),0) as Credit  from TSPL_RECEIPT_DETAIL L inner join tspl_receipt_header H on L.Receipt_No=H.Receipt_No left outer join TSPL_GL_ACCOUNTS  on TSPL_GL_ACCOUNTS.Account_Code   =H.Dr_Account  " & _
                                " where (H.Receipt_Post_Date  >= CONVERT(DATE,'" + dtpFromdate1.Value.Date + "',103)) AND (H.Receipt_Post_Date  <= CONVERT(DATE,'" + dtpToDate.Value.Date + "',103))and  H.Receipt_Type='M' and H.SecurityDeposit='Y' and H.Posted='Y'  "

            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("Please select at least one location")
                Return
            End If
            If chkCustomerSelect.IsChecked = True Then
                If cbgCustomer.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("Please Select Atleast One Customer.")
                    Return
                End If
                StrQuery += " and H.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")  "
            End If
            If chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count >= 0 Then
                StrQuery += "and TSPL_GL_ACCOUNTS.Account_Seg_Code7 in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")"
            End If
            StrQuery += "group by H.Cust_Code "

            StrQuery = "Select *, Case When Debit>0 Then 0 else 1 END as OrderDrCr from (" + StrQuery + ") XXX ORDER BY OrderDrCr"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.SalesReport, dt, "crptSecurityDeposit", "Security Deposit Report")
            frmCRV = Nothing
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        funreset()
    End Sub
    Sub funreset()
        dtpFromdate1.Value = System.DateTime.Now.Date()
        dtpToDate.Value = System.DateTime.Now.Date()
        chkCustomerAll.IsChecked = True
        LoadCustomer()
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub FrmSecurityDeposit1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.P Then
            Print()
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

    Private Sub chkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = False
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged, chkLocSelect.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub
End Class
