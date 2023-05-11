Imports common
Imports XpertERPEngine
Imports Telerik.WinControls

Public Class FrmCustomersListReport

    Inherits FrmMainTranScreen

    Private Sub FrmCustomersListReport_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        LoadCustomer()
        LoadData()
        Reset()

        '--------Dne By Monika 16/04/2014---------
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        '---------End--------------------------------------
        RadPageView1.SelectedPage = RadPageView1.Pages(0)
    End Sub
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmCustomersListReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
    End Sub
    Sub LoadCustomer()
        Dim qry As String = "select Cust_Code,Customer_Name, Phone1 as [Phone No],Coalesce(Add1,'') + coalesce(Add2,'') + coalesce( add3,'')  as [Address]," _
                & " TSPL_CUSTOMER_MASTER.city_code as [City],region_code as [Region],State,Tin_No as [Tin No] from TSPL_CUSTOMER_MASTER " _
                & "   Left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code order by Cust_Code"
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Cust_Code"
        cbgCustomer.DisplayMember = "Customer_Name"
    End Sub



    Sub LoadData()


        Dim qry As String
        Try
            If chkCustomer_select.IsChecked And cbgCustomer.CheckedValue.Count <= 0 Then
                clsCommon.MyMessageBoxShow("please select atleast one or all Customers")


            Else

                qry = "select TSPL_CUSTOMER_MASTER.Cust_Code as OutLet,TSPL_CUSTOMER_MASTER. Customer_Name as CustomerName , " & _
                        "TSPL_CUSTOMER_MASTER. Contact_Person_Name as ContactPerson, " & _
                        "(case when TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CUSTOMER_MASTER.Distributor_Code then TSPL_CUSTOMER_MASTER.Customer_Name else 'Null' end) " & _
                        "as  Distributor ,TSPL_EMPLOYEE_MASTER.Emp_Name   as ServiceDealer, Phone1 as [Phone No],Coalesce(TSPL_CUSTOMER_MASTER.Add1,'') + coalesce(TSPL_CUSTOMER_MASTER.Add2,'')" _
                        & " + coalesce(TSPL_CUSTOMER_MASTER. add3,'')  as [Address],TSPL_CUSTOMER_MASTER.city_code as [City],region_code as [Region],State,Tin_No as [Tin No],Lst_No as [Lst No] from TSPL_CUSTOMER_MASTER " & _
                        "left outer join TSPL_EMPLOYEE_MASTER on " & _
                        " TSPL_CUSTOMER_MASTER.Service_Dealer_Code = TSPL_EMPLOYEE_MASTER.EMP_CODE Left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code where 2=2 "
                If chkCustomer_select.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
                    qry += " and  TSPL_CUSTOMER_MASTER.Cust_Code in ( " + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
                End If

                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable(qry)
                gv1.DataSource = Nothing
                gv1.Columns.Clear()
                gv1.Rows.Clear()
                gv1.GroupDescriptors.Clear()
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)

                    Exit Sub

                End If
                gv1.DataSource = dt
                RadPageView1.Visible = True
                RadPageView1.SelectedPage = RadPageViewPage1
                chkCustomer_all.IsChecked = True
                SetGridFormationOFgv()
            End If
            'Dim dt As DataTable
            'dt = clsDBFuncationality.GetDataTable(qry)
            'gv1.DataSource = Nothing
            'gv1.Columns.Clear()
            'gv1.Rows.Clear()
            'gv1.GroupDescriptors.Clear()
            'If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            '    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)

            '    Exit Sub

            'End If
            'gv1.DataSource = dt
            'RadPageView1.Visible = True
            'RadPageView1.SelectedPage = RadPageViewPage1
            'chkCustomer_all.IsChecked = True

        Catch ex As Exception

        End Try
    End Sub


    Sub SetGridFormationOFgv()
        Try




            ' Dim strItemCode, head2 As String
            gv1.TableElement.TableHeaderHeight = 40
            gv1.MasterTemplate.ShowRowHeaderColumn = False
            gv1.MasterTemplate.ShowColumnHeaders = True

            For ii As Integer = 0 To gv1.Columns.Count - 1
                gv1.Columns(ii).ReadOnly = True
                gv1.Columns(ii).IsVisible = False
            Next

            gv1.Columns("OutLet").IsVisible = True
            gv1.Columns("OutLet").Width = 100
            gv1.Columns("OutLet").HeaderText = "OutLet"

            gv1.Columns("CustomerName").IsVisible = True
            gv1.Columns("CustomerName").Width = 100
            gv1.Columns("CustomerName").HeaderText = "Customer Name"

            gv1.Columns("ContactPerson").IsVisible = True
            gv1.Columns("ContactPerson").Width = 100
            gv1.Columns("ContactPerson").HeaderText = "Contact Person"

            gv1.Columns("Distributor").IsVisible = True
            gv1.Columns("Distributor").Width = 80
            gv1.Columns("Distributor").HeaderText = "Distributor"

            gv1.Columns("ServiceDealer").IsVisible = True
            gv1.Columns("ServiceDealer").Width = 100
            gv1.Columns("ServiceDealer").HeaderText = "Service Dealer"

            gv1.Columns("City").IsVisible = True
            gv1.Columns("City").Width = 80

            gv1.Columns("Address").IsVisible = True
            gv1.Columns("Address").Width = 180
            'gv1.Columns("Town").HeaderText = "Town"

            gv1.Columns("Tin No").IsVisible = True
            gv1.Columns("Tin No").Width = 80

            gv1.Columns("Lst No").IsVisible = True
            gv1.Columns("Lst No").Width = 80

            gv1.Columns("Phone No").IsVisible = True
            gv1.Columns("Phone No").Width = 100

            gv1.Columns("State").IsVisible = True
            gv1.Columns("State").Width = 80

            gv1.Columns("Region").IsVisible = True
            gv1.Columns("Region").Width = 100

            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try
    End Sub

    Private Sub Export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Export.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.Excel)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub PDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles PDF.Click
        If gv1.Rows.Count > 0 Then
            ExportToExcel(EnumExportTo.PDF)
        Else
            RadMessageBox.Show("No Data Found to Display", Me.Text)
        End If
    End Sub

    Private Sub chkCustomer_all_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomer_all.ToggleStateChanged
        cbgCustomer.Enabled = Not chkCustomer_all.IsChecked
        cbgCustomer.Refresh()

    End Sub

    Private Sub ExportToExcel(ByVal exporter As EnumExportTo)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = ""
            arrHeader.Add(strtemp)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Customer List Report", gv1, arrHeader, Me.Text)

            Else
                clsCommon.MyExportToPDF("Customer List Report", gv1, arrHeader, "Customer List Report", True)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click


        LoadData()


    End Sub

    Private Sub btnreset1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Click, btnreset1.Click
        RadPageView1.SelectedPage = RadPageViewPage1
        chkCustomer_all.IsChecked = True

        '--------Dne By Monika 16/04/2014---------
        cbgCustomer.CheckedValue = Nothing
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        '---------End--------------------------------------

    End Sub



    Private Sub btnclose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose1.Click
        Me.Close()
    End Sub


    Private Sub chkCustomer_select_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkCustomer_select.ToggleStateChanged
        cbgCustomer.Enabled = True
    End Sub

    Private Sub RadPageViewPage1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles RadPageViewPage1.Paint

    End Sub

    Private Sub RadPageView1_SelectedPageChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadPageView1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub


End Class
