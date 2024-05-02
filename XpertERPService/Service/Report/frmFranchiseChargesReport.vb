Imports common
Imports XpertERPEngine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource

Public Class FrmFranchiseChargesReport
    Inherits FrmMainTranScreen
    Private Sub rbtnvAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnvAll.ToggleStateChanged, rbtnvselect.ToggleStateChanged
        cbgCustomer.Enabled = rbtnvselect.IsChecked

    End Sub

    Private Sub rbtniall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtniall.ToggleStateChanged, rbtniselect.ToggleStateChanged
        cbgItem.Enabled = rbtniselect.IsChecked
    End Sub

    Private Sub FrmFranchiseChargesReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        fromDate.Text = clsCommon.GETSERVERDATE()
        ToDate.Text = clsCommon.GETSERVERDATE()
        rbtnvAll.IsChecked = True
        rbtniall.IsChecked = True
        LoadFranchise()
        LoadItem()
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmFranchiseChargesReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub
    Public Sub LoadFranchise()
        Dim strquery As String = "select vendor_code as [Franchise Code], vendor_Name as [Franchise Name],(add1+' '+add2+' '+add3) as Address,city_code_desc as City,vendor_group_code_desc as [Vendor Group],vendor_account_desc as [Franchise Account Desc],bank_code_desc as [Bank Name] from TSPL_VENDOR_MASTER where franchise_yn='Y' and comp_code='" + objCommonVar.CurrentCompanyCode + "'"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Franchise Code"
        cbgCustomer.DisplayMember = "Franchise Name"
    End Sub

    Public Sub LoadItem()
        Dim qry As String = ""
        'If rbtnvselect.IsChecked = True Then
        '    qry = " select TSPL_ITEM_FRANCHISE_MAPPING.item_code as [Item Code] ,TSPL_ITEM_MASTER.item_Desc as Description,TSPL_ITEM_MASTER.unit_code as Unit,TSPL_ITEM_MASTER.Rate,TSPL_ITEM_MASTER.structure_desc as [Structure] from TSPL_ITEM_FRANCHISE_MAPPING left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_ITEM_FRANCHISE_MAPPING.item_code and TSPL_ITEM_FRANCHISE_MAPPING.vendor_code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
        'Else
        qry = " select item_code as [Item Code] ,item_Desc as Description,unit_code as Unit,Rate,structure_desc as [Structure] from TSPL_ITEM_MASTER order by Item_Code "
        'End If
        cbgItem.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgItem.ValueMember = "Item Code"
        cbgItem.DisplayMember = "Description"
    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        fromDate.Text = clsCommon.GETSERVERDATE()
        ToDate.Text = clsCommon.GETSERVERDATE()
        rbtnvAll.IsChecked = True
        rbtniall.IsChecked = True
        LoadFranchise()
        LoadItem()
        GV.DataSource = Nothing
        GV.Rows.Clear()
        GV.Columns.Clear()
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PrintData(Exporter.Print)
    End Sub

    Sub PrintData(ByVal Isprint As Exporter)
        Try
            GV.DataSource = Nothing
            GV.Rows.Clear()
            GV.Columns.Clear()

            Dim itemcode As String = ""
            Dim vcode As String = ""

            If rbtnvselect.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Select atleast one Franchise Code")
                cbgCustomer.Focus()
                cbgCustomer.Select()
                Return
            ElseIf cbgCustomer.CheckedValue.Count > 0 Then
                vcode = " and TSPL_ITEM_FRANCHISE_MAPPING.vendor_code in (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ")"
            End If

            If rbtniselect.IsChecked = True AndAlso cbgItem.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow("Select atleast one Item Code")
                cbgItem.Focus()
                cbgItem.Select()
                Return
            ElseIf cbgItem.CheckedValue.Count > 0 Then
                itemcode = " and TSPL_ITEM_FRANCHISE_MAPPING.item_code in (" + clsCommon.GetMulcallString(cbgItem.CheckedValue) + ")"
            End If

            Dim qry As String
            qry = "select TSPL_VENDOR_MASTER.comp_code,TSPL_COMPANY_MASTER.comp_name,TSPL_COMPANY_MASTER.add1,TSPL_COMPANY_MASTER.add2,TSPL_COMPANY_MASTER.add3,TSPL_ITEM_FRANCHISE_MAPPING.vendor_code,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_ITEM_FRANCHISE_MAPPING.Charge_Cat_Code,TSPL_Charge_Category.Description,TSPL_ITEM_FRANCHISE_MAPPING.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_FRANCHISE_MAPPING.Charges from TSPL_ITEM_FRANCHISE_MAPPING left outer join TSPL_Charge_Category on TSPL_Charge_Category.Charge_Cat_Code=TSPL_ITEM_FRANCHISE_MAPPING.Charge_Cat_Code left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_FRANCHISE_MAPPING.Item_Code left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_ITEM_FRANCHISE_MAPPING.vendor_code and TSPL_VENDOR_MASTER.comp_code='" + objCommonVar.CurrentCompanyCode + "' left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.comp_code=TSPL_VENDOR_MASTER.comp_code where 1=1 " + vcode + " " + itemcode + ""

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                GV.DataSource = Nothing
                GV.Rows.Clear()
                GV.Columns.Clear()
                Throw New Exception("No Data Found to Display")
            End If

            If Isprint = Exporter.Print Then
                Dim frmCrystalReportViewer As New frmCrystalReportViewer
                frmCrystalReportViewer.funreport(CrystalReportFolder.ServiceReport, dt, "crptFranchiseChargeReport", "Franchise Charges Report")
            End If

            If Isprint = Exporter.Refresh Then
                GV.DataSource = dt
                FormatGrid()
            End If

            qry = "select Comp_Name,(Add1+' '+Add2+' '+Add3) as address,(Phone1+','+phone2) as phone,Tin_No,CST_LST from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(clsCommon.myCstr(dt1.Rows(0)("Comp_Name")))
            arrHeader.Add(clsCommon.myCstr(dt1.Rows(0)("address")))
            arrHeader.Add("Phone No.: " + clsCommon.myCstr(dt1.Rows(0)("phone")))
            arrHeader.Add("TIN No. : " + clsCommon.myCstr(dt1.Rows(0)("tin_no")) + "     CST No.:" + clsCommon.myCstr(dt1.Rows(0)("CST_LST")))

            If Isprint = Exporter.Excel Then
                GV.DataSource = dt
                FormatGrid()
                clsCommon.MyExportToExcelGrid("Franchise Charges Report", GV, arrHeader, "Call Basis Rate For Franchisee")
            End If

            If Isprint = Exporter.PDF Then
                GV.DataSource = dt
                FormatGrid()
                clsCommon.MyExportToPDF("Franchise Charges Report", GV, arrHeader, "Call Basis Rate For Franchisee", True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub FormatGrid()
        GV.TableElement.TableHeaderHeight = 40
        GV.MasterTemplate.ShowRowHeaderColumn = False

        For ii As Integer = 0 To GV.Columns.Count - 1
            GV.Columns(ii).ReadOnly = True
            GV.Columns(ii).IsVisible = False
        Next

        GV.Columns("comp_code").IsVisible = False
        GV.Columns("comp_name").IsVisible = False
        GV.Columns("add1").IsVisible = False
        GV.Columns("add2").IsVisible = False
        GV.Columns("add3").IsVisible = False
        GV.Columns("vendor_code").IsVisible = False
        GV.Columns("charge_cat_code").IsVisible = False

        GV.Columns("vendor_name").Width = 160
        GV.Columns("vendor_name").IsVisible = True
        GV.Columns("vendor_name").HeaderText = "Franchisee"

        GV.Columns("description").Width = 160
        GV.Columns("description").IsVisible = True
        GV.Columns("description").HeaderText = "Description"

        GV.Columns("item_code").Width = 80
        GV.Columns("item_code").IsVisible = True
        GV.Columns("item_code").HeaderText = "Item Code"

        GV.Columns("item_desc").Width = 160
        GV.Columns("item_desc").IsVisible = True
        GV.Columns("item_desc").HeaderText = "Item Description"

        GV.Columns("charges").Width = 60
        GV.Columns("charges").IsVisible = True
        GV.Columns("charges").HeaderText = "Charges"

        GV.MasterTemplate.AutoExpandGroups = True
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        PrintData(Exporter.Refresh)
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        PrintData(Exporter.Excel)
    End Sub

    Enum Exporter
        Excel = 0
        PDF = 1
        Refresh = 2
        Print = 3
    End Enum

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        PrintData(Exporter.PDF)
    End Sub
End Class
