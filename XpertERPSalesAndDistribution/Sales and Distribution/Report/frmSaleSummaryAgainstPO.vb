Imports XpertERPEngine
Imports common
Imports Telerik.WinControls.UI
Imports Telerik.WinControls.Data
Imports Telerik.Data
Imports Telerik.WinControls.Enumerations
Imports Telerik.WinControls
Public Class frmSaleSummaryAgainstPO
    Inherits FrmMainTranScreen
    Friend WithEvents gv1 As New common.UserControls.MyRadGridView
    Public Sub loadCustomerCode()
        Dim qry11 As String = "SELECT  Cust_Code,Customer_Name FROM TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry11)
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Customer_Name"
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmSaleSummaryAgainstPO)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub frmSaleSummaryAgainstPO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        loadCustomerCode()
        LoadLocation()
        reset()
    End Sub
    Sub LoadLocation()
        Dim qry As String = "select Location_code , Location_desc  from TSPL_LOCATION_MASTER"
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Location_Code"
        cbgLocation.DisplayMember = "Location_desc"
    End Sub
    Private Sub reset()
        dtpFrmDate.Value = clsCommon.GETSERVERDATE
        dtpToDate.Value = clsCommon.GETSERVERDATE
        chkChapterAll.IsChecked = True
        MyRadioButton4.IsChecked = True
        cbgCustomer.CheckedAll()
        cbgLocation.CheckedAll()
        cbgCustomer.Enabled = False
        cbgLocation.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.Columns.Clear()

    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Sub print()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strLoc As String = ""
        Dim strDateRange As String = ""
        Try
            qry = "Select '" + Format(dtpFrmDate.Value(), "dd/MM/yyyy").ToString() + " To " + Format(dtpToDate.Value(), "dd/MM/yyyy").ToString() + "' as dtrng,  TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Pincode ,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,'" & objCommonVar.CurrentUser & "' as User_Name ,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone ,TSPL_CUSTOMER_MASTER.Tin_No  ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date  ,TSPL_SD_SALE_INVOICE_HEAD.Document_Code ,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No ,TSPL_SD_SALE_INVOICE_HEAD.GRNo ,TSPL_SD_SALE_INVOICE_HEAD.GEDate   from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  where TSPL_SD_SALE_INVOICE_HEAD.Total_Amt>0  "



            If cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If
            If cbgLocation.CheckedValue.Count > 0 Then
                strLoc += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                strLoc = ""

            End If

            strDateRange = " and CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103) "

            qry += strDateRange
            qry += strLoc
            qry += strCustomer


            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            'gv1.DataSource = dt
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, dt, "rptSaleSummaryAgainstPO", "Sale Summary Against PO")
        Catch ex As Exception

            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try


    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("Start Date Can Not Be Greater Then End Date")
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If cbgCustomer.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Customer")
            cbgCustomer.Focus()
            Exit Sub
        End If
        If cbgLocation.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Location")
            cbgLocation.Focus()
            Exit Sub
        End If
        print()


    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        reset()
    End Sub

    Private Sub RadPanel1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPanel1.Paint

    End Sub

    Private Sub chkChapterSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkChapterSelect.ToggleStateChanged
        If chkChapterSelect.IsChecked() Then
            cbgCustomer.UnCheckedAll()

            cbgCustomer.Enabled = True
        Else
            cbgCustomer.UnCheckedAll()
            cbgCustomer.Enabled = False
        End If

    End Sub

    Private Sub MyRadioButton3_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton3.ToggleStateChanged
        If MyRadioButton3.IsChecked() Then
            cbgLocation.UnCheckedAll()
            cbgLocation.Enabled = True
        Else
            cbgLocation.UnCheckedAll()
            cbgLocation.Enabled = False
        End If
    End Sub

    Private Sub chkChapterAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkChapterAll.ToggleStateChanged
        If chkChapterAll.IsChecked() Then
            cbgCustomer.CheckedAll()
            cbgCustomer.Enabled = False
        Else
            cbgCustomer.UnCheckedAll()

            cbgCustomer.Enabled = True
        End If

    End Sub

    Private Sub MyRadioButton4_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles MyRadioButton4.ToggleStateChanged
        If MyRadioButton4.IsChecked() Then
            cbgLocation.CheckedAll()
            cbgLocation.Enabled = False
        Else
            cbgLocation.UnCheckedAll()

            cbgLocation.Enabled = True
        End If
    End Sub

    Private Sub btnReferesh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReferesh.Click
        If dtpFrmDate.Value > dtpToDate.Value Then
            common.clsCommon.MyMessageBoxShow("Start Date Can Not Be Greater Then End Date")
            dtpFrmDate.Focus()
            Exit Sub
        End If
        If cbgCustomer.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Customer")
            cbgCustomer.Focus()
            Exit Sub
        End If
        If cbgLocation.CheckedDisplayMember.Count < 1 Then
            MessageBox.Show("Please Select At least One Location")
            cbgLocation.Focus()
            Exit Sub
        End If
        Referesh()
    End Sub

    Sub Referesh()
        Dim qry As String = ""
        Dim strCustomer As String = ""
        Dim strLoc As String = ""
        Dim strDateRange As String = ""
        Try


            qry = "Select '" + Format(dtpFrmDate.Value(), "dd/MM/yyyy").ToString() + " To " + Format(dtpToDate.Value(), "dd/MM/yyyy").ToString() + "' as dtrng,  TSPL_COMPANY_MASTER.Comp_Code ,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Add1,TSPL_COMPANY_MASTER.Add2,TSPL_COMPANY_MASTER.Add3,TSPL_COMPANY_MASTER.Pincode ,TSPL_COMPANY_MASTER.Phone1,TSPL_COMPANY_MASTER.Phone2,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_CUSTOMER_MASTER.Customer_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Name,TSPL_CUSTOMER_MASTER.Contact_Person_Phone ,TSPL_CUSTOMER_MASTER.Tin_No  ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date  ,TSPL_SD_SALE_INVOICE_HEAD.Document_Code ,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No ,TSPL_SD_SALE_INVOICE_HEAD.GRNo ,TSPL_SD_SALE_INVOICE_HEAD.GEDate   from TSPL_SD_SALE_INVOICE_HEAD left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location  left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_SD_SALE_INVOICE_HEAD.Comp_Code left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  where TSPL_SD_SALE_INVOICE_HEAD.Total_Amt>0  "



            If cbgCustomer.CheckedValue.Count > 0 Then
                strCustomer += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
            Else
                strCustomer = ""

            End If
            If cbgLocation.CheckedValue.Count > 0 Then
                strLoc += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in  (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
            Else
                strLoc = ""

            End If

            strDateRange = " and CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)>=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpFrmDate.Value, "dd/MMM/yyyy") + "', 103) AND CONVERT(DATE, TSPL_SD_SALE_INVOICE_HEAD.Document_Date, 103)<=CONVERT(DATE, '" + clsCommon.GetPrintDate(dtpToDate.Value, "dd/MMM/yyyy") + "', 103) "

            qry += strDateRange
            qry += strLoc
            qry += strCustomer


            Dim dt As New DataTable
            dt = clsDBFuncationality.GetDataTable(qry)
            gv.DataSource = Nothing
            gv.Columns.Clear()
            gv.Rows.Clear()
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                Exit Sub
            Else
                gv.DataSource = dt
                SetGridFormatOFGV()
                RadPageView1.SelectedPage = RadPageViewPage2
            End If
        Catch ex As Exception

        End Try


    End Sub
    Sub SetGridFormatOFGV()


        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next
        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 120
        gv.Columns("Location_Desc").HeaderText = "Location"

        gv.Columns("Document_Date").IsVisible = True
        gv.Columns("Document_Date").Width = 120
        gv.Columns("Document_Date").HeaderText = "Invoicet Date"

        gv.Columns("Document_Code").IsVisible = True
        gv.Columns("Document_Code").Width = 120
        gv.Columns("Document_Code").HeaderText = "Invoice Code"

        gv.Columns("Cust_PO_No").IsVisible = True
        gv.Columns("Cust_PO_No").Width = 120
        gv.Columns("Cust_PO_No").HeaderText = "PO No"


        gv.Columns("Total_Amt").IsVisible = True
        gv.Columns("Total_Amt").Width = 120
        gv.Columns("Total_Amt").HeaderText = "Total Amt"

        gv.Columns("GRNo").IsVisible = True
        gv.Columns("GRNo").Width = 120
        gv.Columns("GRNo").HeaderText = "GRN No"

        gv.Columns("GEDate").IsVisible = True
        gv.Columns("GEDate").Width = 120
        gv.Columns("GEDate").HeaderText = "GRN Date"

        gv.Columns("Customer_Code").IsVisible = False
        gv.Columns("Customer_Code").Width = 120
        gv.Columns("Customer_Code").HeaderText = "Customer Code"


        gv.GroupDescriptors.Add(New GridGroupByExpression("Customer_Code as Item format ""{0}: {1}"" Group By Customer_Code"))


        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True




    End Sub

    Private Sub gv_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellDoubleClick
        If clsCommon.myLen(gv.CurrentRow.Cells("Document_Code").Value >= 0) Then
            clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSNSaleInvoice, gv.CurrentRow.Cells("Document_Code").Value)
        End If
    End Sub
End Class
