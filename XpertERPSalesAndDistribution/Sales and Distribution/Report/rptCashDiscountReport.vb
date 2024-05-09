Imports Microsoft.VisualBasic
Imports System
Imports XpertERPEngine
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
Public Class rptCashDiscountReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.CashDiscount)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub


    Private Sub rptCashDiscountReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtTodate.Value = clsCommon.GETSERVERDATE()
        rbtnPosted.IsChecked = True
        chkCustAll.IsChecked = True
        chklocAll.IsChecked = True
        Loadlocation()
        LoadCustomer()
        LoadCompany()
        rbtnCompanySelect.IsChecked = True
        Dim arrDBName As New ArrayList()
        arrDBName.Add(objCommonVar.CurrDatabase)
        cbgCompany.CheckedValue = arrDBName

    End Sub
    Sub LoadCompany()
        Dim qry As String = "SELECT Comp_Code as [Company Code],Comp_Name as [Company Name],DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0"
        Dim ArrHideColumn As New List(Of String)
        ArrHideColumn.Add("DataBase_Name")
        cbgCompany.ArrHideColumns = ArrHideColumn
        cbgCompany.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCompany.ValueMember = "DataBase_Name"
        cbgCompany.DisplayMember = "Company Name"
    End Sub

    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code as [Customer Code],Customer_Name as [Customer Name],Customer_Class as [Customer Class] from TSPL_CUSTOMER_MASTER"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub
    Sub reset()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtTodate.Value = clsCommon.GETSERVERDATE()
        rbtnPosted.IsChecked = True
        chkCustAll.IsChecked = True
        LoadCustomer()
        LoadCompany()
        rbtnCompanyAll.IsChecked = True
        Loadlocation()
        chklocAll.IsChecked = True
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            LoadData()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    Sub Loadlocation()
        'Dim qry As String = "select Location_Code as Code,Location_Desc  as Name from TSPL_LOCATION_MASTER where Location_Type <> 'Logical'  order by Location_Code"
        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        cbglocation.DataSource = clsLocation.GetLocationSegments()
        'cbglocation.ValueMember = "Code"
        'cbglocation.DisplayMember = "Description"

        cbglocation.ValueMember = "Code"
        cbglocation.DisplayMember = "Name"
    End Sub
    Sub LoadData()

        If chkChkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one customer")
        End If
        If chklocSelect.IsChecked = True AndAlso cbglocation.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one Location or select ALL")
        End If
        'If chkItemSelect1.IsChecked AndAlso cbgItem1.CheckedValue.Count <= 0 Then
        '    Throw New Exception("Please select at least one item")
        'End If

        If rbtnCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count <= 0 Then
            Throw New Exception("Please select at least one company")

        End If
        Dim qry As String = "select '" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + "' as [StartDate],'" + clsCommon.GetPrintDate(txtTodate.Value, "dd/MM/yyyy") + "' as [EndDate], Sale_Invoice_Date as [Invoice Date] ,TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No as [Invoice No],Route_No AS [Route No],Route_Desc ,Cust_Name, "
        qry += " (select sum(case when Unit_code='FC' then  Invoice_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor else MRP_Amt end) from TSPL_SALE_INVOICE_DETAIL left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code =TSPL_SALE_INVOICE_DETAIL.Item_Code  where TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No ) as  [Quantity] ,Inv_Discount_Amt as [Disc Amount], "
        qry += " (select SUM(Total_Item_Amt ) from TSPL_SALE_INVOICE_DETAIL where TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) as [Amount], "
        qry += " TSPL_SALE_INVOICE_HEAD.Comp_Code as [CompCode],TSPL_COMPANY_MASTER.Comp_Name as [Company Name] , (Case when len(TSPL_COMPANY_MASTER.Add2)>0 then Add1+' ,'+Add2 when LEN(TSPL_COMPANY_MASTER.Add3 )>0 then Add1+' ,'+Add2 +' ,'+Add3 else Add1 end )as [Address1],TSPL_COMPANY_MASTER.Logo_Img as [Logo1], TSPL_COMPANY_MASTER.Logo_Img2 as [Logo2] from TSPL_SALE_INVOICE_HEAD "
        qry += " left outer join TSPL_COMPANY_MASTER on TSPL_SALE_INVOICE_HEAD.Comp_Code =TSPL_COMPANY_MASTER.Comp_Code where 2=2 "

        If rbtnPosted.IsChecked = True Then
            qry += " and Is_Post='Y' "
        End If
        qry += "  and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date>='" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "' and TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date<='" + clsCommon.GetPrintDate(txtTodate.Value, "dd/MMM/yyyy") + "'"

        If chkChkSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
            qry += " and TSPL_SALE_INVOICE_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        End If

        If chklocSelect.IsChecked AndAlso cbglocation.CheckedValue.Count > 0 Then
            qry += " and TSPL_SALE_INVOICE_Head.location in (" + clsCommon.GetMulcallString(cbglocation.CheckedValue) + ")"
        End If
        'If chkItemSelect1.IsChecked AndAlso cbgItem1.CheckedValue.Count > 0 Then
        '    qry += " and TSPL_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(cbgItem1.CheckedValue) + ")"
        'End If

        'If rbtnCompanySelect.IsChecked AndAlso cbgCompany.CheckedValue.Count > 0 Then
        '    qry += " and TSPL_SALE_INVOICE_HEAD.Comp_Code in (" + clsCommon.GetMulcallString(cbgCompany.CheckedValue) + ")"
        'End If

        Dim dt As DataTable = Nothing
        dt = clsDBFuncationality.GetDataTable(qry)
        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            Throw New Exception("No Data Found to Display")
            Exit Sub
        Else
            Dim frmcrystal As New frmCrystalReportViewer()
            frmcrystal.funreport(CrystalReportFolder.SalesReport, clsDBFuncationality.GetDataTable(qry), "crptCashDiscountReport", "Cash Discount Report")

        End If
    End Sub
    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub chkCustAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustAll.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustAll.IsChecked
    End Sub

    Private Sub rbtnCompanyAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCompanyAll.ToggleStateChanged
        cbgCompany.Enabled = Not rbtnCompanyAll.IsChecked
    End Sub

    Private Sub chklocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chklocAll.ToggleStateChanged
        cbglocation.Enabled = Not chklocAll.IsChecked
    End Sub
End Class
