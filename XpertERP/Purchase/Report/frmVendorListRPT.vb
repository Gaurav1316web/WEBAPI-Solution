'--------------created by Monika-------------BM00000003620
Imports common
Imports System.Data.SqlClient
Imports System.IO
Public Class FrmVendorListRPT
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmVendorListRPT)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnexport.Visible = MyBase.isExport
        btnprint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub chkAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAll.ToggleStateChanged, chkSelect.ToggleStateChanged
        cbggroup.Enabled = chkSelect.IsChecked
    End Sub

    Private Sub chlvendorall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chlvendorall.ToggleStateChanged, chkvendorselect.ToggleStateChanged
        cbgvendor.Enabled = chkvendorselect.IsChecked
    End Sub

    Private Sub FrmVendorListRPT_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            btnprint.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmVendorListRPT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()
        LoadVendors()
        LoadVendorGroups()

        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R for reset window")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for view report")
    End Sub

    Private Sub FunReset()
        gv.Columns.Clear()
        gv.DataSource = Nothing
        chkAll.IsChecked = True
        chlvendorall.IsChecked = True
        cbggroup.Enabled = False
        cbgvendor.Enabled = False

        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub LoadVendors()
        Dim qry As String = "select vendor_code as Code,vendor_name as Description from tspl_vendor_master where Status='N' "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbgvendor.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cbgvendor.DataSource = dt
            cbgvendor.DisplayMember = "Description"
            cbgvendor.ValueMember = "Code"
        End If
    End Sub

    Private Sub LoadVendorGroups()
        Dim qry As String = "select ven_group_code as Code,group_desc as Description from TSPL_VENDOR_GROUP"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        cbggroup.DataSource = Nothing
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            cbggroup.DataSource = dt
            cbggroup.DisplayMember = "Description"
            cbggroup.ValueMember = "Code"
        End If
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Print()
    End Sub

    Private Sub Print()
        Try
            If chkSelect.IsChecked AndAlso cbggroup.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast one vendor group.")
            End If

            If chkvendorselect.IsChecked AndAlso cbgvendor.CheckedValue.Count <= 0 Then
                Throw New Exception("Select atleast one vendor")
            End If

            Dim qry As String = " select Vendor_Code as [Code],Vendor_Name as [Vendor Name],tspl_vendor_master.Add1,tspl_vendor_master.Add2,tspl_vendor_master.Add3,Closing_Date as [Closing Date],Vendor_Group_Code as [Vendor Group Code],Vendor_Group_Code_Desc as [Vendor Group Description],tspl_vendor_master.City_Code as [City Code],City_Code_Desc as [City Description],tspl_vendor_master.State_Code as [State Code],TSPL_STATE_MASTER.STATE_NAME as [State Name],Country,Phone1,Phone2,Fax,Email,WebSite,Contact_Person_Name as [Contact Person Name],Contact_Person_Phone as [Contact Person Phone],Contact_Person_Fax as [Contact Person FAX],Contact_Person_Website as [Contact Person Website],Contact_Person_Email as [Contact Person Email],Terms_Code as [Terms Code],Terms_Code_Desc as [Terms Code Description],Vendor_Account as [Vendor Account],Vendor_Account_Desc as [Vendor Account Description],Payment_Code as [Payment Code],Payment_Code_Desc as [Payment Code Description],tspl_vendor_master.Bank_Code as [Bank Code],tspl_vendor_master.Bank_Code_Desc as [Bank Description],tspl_vendor_master.IFSC_Code as [Bank IFSC],TSPL_Vendor_Bank_Branch_Details.Branch_Name as [Branch Name],tspl_vendor_master.Account_No as [Bank A/C No.], case when tspl_vendor_master.Account_Type='Cur'then 'Current' else case when tspl_vendor_master.Account_Type='Sav'then 'Saving' else" & _
                                " case when tspl_vendor_master.Account_Type='Cas'then 'Cash' else case when tspl_vendor_master.Account_Type='Cre'then 'Credit' else" & _
                                 " case when tspl_vendor_master.Account_Type='Loa'then 'Loan' else case when tspl_vendor_master.Account_Type='Oth'then 'Others' else tspl_vendor_master.Account_Type end end end end end end as [Bank A/C Type],Tax_Group as [Tax Group],Tax_Group_Desc as [Tax Group Description],Ven_Type_Code as [Vendor Type Code],Ven_Type_Desc as [Vendor Type Description],TAX1,TAX1_Rate as [TAX1 Rate],TAX2,TAX2_Rate as [Tax2 Rate],TAX3,TAX3_Rate as [Tax3 Rate],TAX4,TAX4_Rate as [Tax4 Rate],TAX5,TAX5_Rate as [Tax5 Rate],TAX6,TAX6_Rate as [Tax6 Rate],TAX7,TAX7_Rate as [Tax7 Rate],TAX8,TAX8_Rate as [Tax8 Rate],TAX9,TAX9_Rate as [Tax9 Rate],TAX10,TAX10_Rate as [Tax10 Rate],Service_Tax_No as [Service Tax No],Tin_No as [TIN No],Lst_No as [LST No],(select case when Status='N' then 'Active' else 'In Active' end ) as Status,OnHold as [On Hold],Transporter,Remarks1,Remarks2,Additional1,Additional2,Additional3,Credit_Limit as [Credit Limit],tspl_vendor_master.Created_By as [Created By],tspl_vendor_master.Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],tspl_vendor_master.Comp_Code as [Company Code],CST,ECC,Range,Collectorate,PAN,Is_Gross_Receipt as [Is Gross Receipt],Inter_Branch as [Inter Branch],CURRENCY_CODE as [Currency Code],franchise_yn as [Is Franchise] ,tspl_vendor_master.GSTFinalNo as [GSTIN No],TSPL_STATE_MASTER.GST_STATE_Code as [GST State Code],case when  tspl_vendor_master.GSTRegistered =1 then 'Yes' else 'No' end as Registered  from tspl_vendor_master left outer join TSPL_STATE_MASTER  on TSPL_STATE_MASTER.STATE_CODE = tspl_vendor_master.State_Code left join TSPL_Vendor_Bank_MASTER on TSPL_Vendor_Bank_MASTER.Bank_Code =tspl_vendor_master.Bank_Code" & _
                                " left join TSPL_Vendor_Bank_Branch_Details on TSPL_Vendor_Bank_Branch_Details.Bank_Code =TSPL_Vendor_Bank_MASTER.Bank_Code " & _
                                " and tspl_vendor_master.IFSC_Code =TSPL_Vendor_Bank_Branch_Details.Bank_IFSC_Code   where tspl_vendor_master.Status='N' "

            If chkSelect.IsChecked Then
                qry += " and vendor_group_code in (" + clsCommon.GetMulcallString(cbggroup.CheckedValue) + ")"
            End If
            If chkvendorselect.IsChecked Then
                qry += " and vendor_code in (" + clsCommon.GetMulcallString(cbgvendor.CheckedValue) + ")"
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt

                FormatGrid()
                ReStoreGridLayout()
                RadPageView1.SelectedPage = RadPageViewPage2
            Else
                Throw New Exception("No Data Found.")
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub FormatGrid()
        'Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).Width = 100
        Next

        gv.Columns(1).Width = 250
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        Print()
        FunExport(EnumExportTo.Excel)
        'If gv.Rows.Count > 0 Then
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        '    arrHeader.Add("Vendor List Report")

        '    clsCommon.MyExportToExcelGrid("Vendor List Report", gv, arrHeader, "Vendor List Report")
        'End If
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        Print()
        FunExport(EnumExportTo.PDF)
        'If gv.Rows.Count > 0 Then
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add("Vendor List Report")
        '    clsCommon.MyExportToPDF("Vendor List Report", gv, arrHeader, "Vendor List Report")
        'End If
    End Sub

    Private Sub FunExport(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Age as of: " + clsCommon.GetPrintDate(dtpAgeof.Value, "dd/MM/yyyy") + " cutoff Date " + clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmVendorListRPT & "'"))
            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If

            If chkSelect.IsChecked Then
                If cbggroup.CheckedDisplayMember IsNot Nothing AndAlso cbggroup.CheckedDisplayMember.Count > 0 Then
                    arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(cbggroup.CheckedDisplayMember))
                End If
            End If

            If chkvendorselect.IsChecked Then
                If cbgvendor.CheckedDisplayMember IsNot Nothing AndAlso cbgvendor.CheckedDisplayMember.Count > 0 Then
                    arrHeader.Add("Vendor : " + clsCommon.GetMulcallStringWithComma(cbgvendor.CheckedDisplayMember))
                End If
            End If

            If exporter = EnumExportTo.Excel Then
                'Dim sfd As SaveFileDialog = New SaveFileDialog()
                'Dim filePath As String
                'sfd.FileName = Me.Text
                'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '    filePath = sfd.FileName
                'Else
                '    Exit Sub
                'End If
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                'Process.Start(filePath)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor List Report", gv, arrHeader, "Vendor List Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub


    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv.Columns.Count - 1 Step ii + 1
                        gv.Columns(ii).IsVisible = False
                        gv.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub


    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
End Class
