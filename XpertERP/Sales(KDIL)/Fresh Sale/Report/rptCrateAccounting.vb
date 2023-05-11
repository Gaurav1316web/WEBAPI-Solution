Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common

Imports System.IO
Public Class rptCrateAccounting
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()

        'MyBase.SetUserMgmt(clsUserMgtCode.rptCrateAccounting)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Sub LoadLocation()
        Dim qry As String = " select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"

    End Sub

    Sub LoadCustomer()
        Dim qry As String = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgCustomer.ValueMember = "Code"
        cbgCustomer.DisplayMember = "Name"

    End Sub
    Private Sub ChkCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles ChkCustomerAll.ToggleStateChanged
        cbgCustomer.Enabled = Not ChkCustomerAll.IsChecked
    End Sub


    Private Sub chkAllLocation_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllLocation.ToggleStateChanged
        cbgLocation.Enabled = Not chkAllLocation.IsChecked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If ChkCustomerSelect.IsChecked Then
                Dim strCustomerName As String = ""
                For Each StrName As String In cbgCustomer.CheckedDisplayMember
                    If clsCommon.myLen(strCustomerName) > 0 Then
                        strCustomerName += ", "
                    End If
                    strCustomerName += StrName
                Next
                Dim strCustomerCode As String = ""
                For Each StrCode As String In cbgCustomer.CheckedValue
                    If clsCommon.myLen(strCustomerCode) > 0 Then
                        strCustomerCode += ", "
                    End If
                    strCustomerCode += StrCode
                Next

                arrHeader.Add(("Tanker Name: " + strCustomerName + " "))

            End If
            If chkSelectLocation.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgLocation.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgLocation.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next

                arrHeader.Add(("Vendor Name: " + strLocationName + " "))

            End If


            'If exporter = EnumExportTo.Excel Then
            '    clsCommon.MyExportToExcelGrid("Crate Accounting Report", Gv1, arrHeader, Me.Text)

            'Else
            '    clsCommon.MyExportToPDF("Crate Accounting Report", gv2, arrHeader, Me.Text, True)
            'End If

            If RadPageView1.SelectedPage Is RadPageViewPage3 Then

                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("Crate Accounting Report", gv2, arrHeader, Me.Text)
                End If
            End If
            If RadPageView1.SelectedPage Is RadPageViewPage2 Then

                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("Crate Accounting Report", Gv1, arrHeader, Me.Text)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Load_Report()
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        Dim success As Boolean = True
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then

            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & Gv1.Name
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                success = True
            End If

            gv2.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv2.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv2.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            success = success And obj.SaveData()
            If success Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & Gv1.Name, objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub
    Public Sub Load_Report()
        If fromDate.Value > ToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            fromDate.Focus()
            Exit Sub
        End If


        If chkSelectLocation.IsChecked AndAlso cbgLocation.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Location or select all.")
            Exit Sub
        End If
        If ChkCustomerSelect.IsChecked AndAlso cbgCustomer.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Customer or select all.")
            Exit Sub
        End If

        Dim Amount As String = clsDBFuncationality.getSingleValue("select Description  from TSPL_FIXED_PARAMETER   where Type='CrateValue'")

        Dim squery As String = "select final.*,final.Balance_Qty *(" + Amount + ") as BalanceAmount from(select max(convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)) as Doc_Date,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,max(TSPL_CUSTOMER_MASTER.Customer_Name) as Customer_Name"
        squery += ",TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location as Loc_Code,max(TSPL_LOCATION_MASTER.Location_Desc) as Location_Desc "

        squery += ",sum(isnull(TSPL_SD_SALE_INVOICE_HEAD.CrateQty,0)) as Out_Crate_Qty,sum(isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd,0)) as In_Crate_Qty,"
        squery += " sum(isnull(TSPL_SD_SALE_INVOICE_HEAD.CrateQty,0)) - sum(isnull(TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.CrateQtyRecd,0)) as Balance_Qty"
        squery += " from TSPL_SD_SALE_INVOICE_HEAD"
        squery += " left outer join TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE on TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Sale_Invoice_No =TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE"
        squery += " left outer join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No "
        squery += " left  outer join TSPL_SD_SALE_INVOICE_HEAD as TSPL_SD_SALE_INVOICE_HEAD_Customer on TSPL_SD_SALE_INVOICE_HEAD_Customer.Customer_Code=TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code and  TSPL_SD_SALE_INVOICE_HEAD_Customer.Bill_To_Location=TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code"
        squery += " left outer join TSPL_SD_SALE_INVOICE_HEAD as TSPL_SD_SALE_INVOICE_HEAD_Location on TSPL_SD_SALE_INVOICE_HEAD_Location.Bill_To_Location =TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code "
        squery += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code "
        squery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location"
        squery += " group by TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location"

        squery += ")final"
        squery += " where 2=2 and convert(date,final.Doc_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,final.Doc_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)"

        If chkSelectLocation.IsChecked And cbgLocation.CheckedValue.Count > 0 Then
            squery += "and final.Loc_Code  IN (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ") "
        End If
        If ChkCustomerSelect.IsChecked And cbgCustomer.CheckedValue.Count > 0 Then
            squery += "and final.Customer_Code  IN (" + clsCommon.GetMulcallString(cbgCustomer.CheckedValue) + ") "
        End If

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(squery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

        ReStoreGridLayout()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & Gv1.Name, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)

                End If

            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub ReStoreGridLayoutDetails()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then

                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv2.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1
                        gv2.Columns(ii).IsVisible = False
                        gv2.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv2.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)

                End If

            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub FormatGrid()


        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        Gv1.Columns("Customer_Code").IsVisible = True
        Gv1.Columns("Customer_Code").Width = 100
        Gv1.Columns("Customer_Code").HeaderText = "Customer Code"

        Gv1.Columns("Customer_Name").IsVisible = True
        Gv1.Columns("Customer_Name").Width = 100
        Gv1.Columns("Customer_Name").HeaderText = "Customer Name"


        Gv1.Columns("Loc_Code").IsVisible = False
        Gv1.Columns("Loc_Code").Width = 100
        Gv1.Columns("Loc_Code").HeaderText = " Loc_Code"


        Gv1.Columns("Location_Desc").IsVisible = True
        Gv1.Columns("Location_Desc").Width = 100
        Gv1.Columns("Location_Desc").HeaderText = "Location Desc"


        Gv1.Columns("Out_Crate_Qty").IsVisible = True
        Gv1.Columns("Out_Crate_Qty").Width = 100
        Gv1.Columns("Out_Crate_Qty").HeaderText = "Crate Qty Out"


        Gv1.Columns("In_Crate_Qty").IsVisible = True
        Gv1.Columns("In_Crate_Qty").Width = 100
        Gv1.Columns("In_Crate_Qty").HeaderText = "Crate Qty In"


        Gv1.Columns("Balance_Qty").IsVisible = True
        Gv1.Columns("Balance_Qty").Width = 100
        Gv1.Columns("Balance_Qty").HeaderText = "Balance Qty"


        Gv1.Columns("BalanceAmount").IsVisible = True
        Gv1.Columns("BalanceAmount").Width = 100
        Gv1.Columns("BalanceAmount").HeaderText = "Balance Amount"

        
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        'Dim item1 As New GridViewSummaryItem("QTY", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
       

        'gv.GroupDescriptors.Add(New GridGroupByExpression("DOC_DATE as Item format ""{0}: {1}"" Group By DOC_DATE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))


        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)



    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        LoadLocation()
        LoadCustomer()

        chkAllLocation.CheckState = CheckState.Checked
        ChkCustomerAll.CheckState = CheckState.Checked

        Gv1.DataSource = Nothing
        gv2.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub rptCrateAccounting_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+N Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+R ")
        Reset()
    End Sub

    Private Sub Gv1_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        Try
            'If e.ColumnIndex <= 0 Then

            Dim query As String = String.Empty

            Dim tdate As Date = clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")
            Dim fdate As Date = clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")

            ' Gv1.DataSource = Nothing
            Dim summaryRowItem As New GridViewSummaryRowItem()

            Dim LocCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Loc_Code").Value)
            Dim CustCode As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Customer_Code").Value)
            If e.Column.Name <> Gv1.Columns("Out_Crate_Qty").Name And e.Column.Name <> Gv1.Columns("In_Crate_Qty").Name Then
                Exit Sub
            End If
            

            If e.Column Is Gv1.Columns("Out_Crate_Qty") Then
                If clsCommon.myLen(Gv1.CurrentRow.Cells("Out_Crate_Qty").Value) > 0 Then
                    query = "select TSPL_SD_SALE_INVOICE_HEAD.Document_Code,TSPL_SD_SALE_INVOICE_HEAD.CrateQty  ,convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as Document_Date ,TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name  ,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location,TSPL_LOCATION_MASTER.Location_Desc ,TSPL_SD_SALE_INVOICE_HEAD.Description ,TSPL_SD_SALE_INVOICE_HEAD.Cust_PO_No,TSPL_SD_SALE_INVOICE_HEAD.Transporter_Name ,TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No   ,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt    from TSPL_SD_SALE_INVOICE_HEAD"
                    query += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_SD_SALE_INVOICE_HEAD.Customer_Code left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location"
                    query += " where 2=2 and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  ='" + CustCode + "' and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location='" + LocCode + "' and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)"


                End If
            End If

            If e.Column Is Gv1.Columns("In_Crate_Qty") Then
                If clsCommon.myLen(Gv1.CurrentRow.Cells("In_Crate_Qty").Value) > 0 Then
                    query = "select TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No,convert(varchar,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,103)  as Document_Date,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code ,TSPL_LOCATION_MASTER.Location_Desc,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Salesman_Name ,TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Vehicle_Code ,CrateQtyRecd     from TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE"
                    query += " left outer join TSPL_CRATE_RECEIVED_HEAD_FRESHSALE on TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_No =TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Document_No  "
                    query += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code "
                    query += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code "
                    query += "where 2=2 and TSPL_CRATE_RECEIVED_DETAIL_FRESHSALE.Customer_Code  ='" + CustCode + "' and TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Location_Code='" + LocCode + "' and convert(date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,103)>=convert(date,'" + fromDate.Value + "',103) and convert(date,TSPL_CRATE_RECEIVED_HEAD_FRESHSALE.Document_Date,103) <=convert(date,'" + ToDate.Value + "' ,103)"

                End If
            End If


            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                gv2.Visible = True
                gv2.DataSource = dt
                gv2.GroupDescriptors.Clear()
                gv2.MasterTemplate.SummaryRowsBottom.Clear()
                If e.Column Is Gv1.Columns("In_Crate_Qty") Then
                    FormatGridDetailsInQty()
                End If
                If e.Column Is Gv1.Columns("Out_Crate_Qty") Then
                    FormatGridDetails()
                End If
                gv2.ReadOnly = True
                RadPageView1.Visible = True
                ReStoreGridLayoutDetails()
                RadPageView1.SelectedPage = RadPageViewPage3
            Else
                clsCommon.MyMessageBoxShow("No Data Found")

            End If

            'End If
        Catch ex As Exception
            'Throw New Exception(ex.Message, Me.Text)

            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try

    End Sub
    Sub FormatGridDetailsInQty()
        ' Dim strItemCode, head2 As String

        gv2.TableElement.TableHeaderHeight = 25
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = False
        Next

        gv2.Columns("Document_No").IsVisible = True
        gv2.Columns("Document_No").Width = 100
        gv2.Columns("Document_No").HeaderText = " Document No"



        'gv2.Columns("CrateQty").IsVisible = True
        'gv2.Columns("CrateQty").Width = 100
        'gv2.Columns("CrateQty").HeaderText = "Crate Qty "

        gv2.Columns("Document_Date").IsVisible = True
        gv2.Columns("Document_Date").Width = 100
        gv2.Columns("Document_Date").HeaderText = " Date "


        gv2.Columns("Customer_Code").IsVisible = False
        gv2.Columns("Customer_Code").Width = 100
        gv2.Columns("Customer_Code").HeaderText = " Customer Code"

        gv2.Columns("Customer_Name").IsVisible = True
        gv2.Columns("Customer_Name").Width = 100
        gv2.Columns("Customer_Name").HeaderText = "Customer Name"

        
        gv2.Columns("Location_Code").IsVisible = False
        gv2.Columns("Location_Code").Width = 80
        gv2.Columns("Location_Code").HeaderText = "location Code"

        gv2.Columns("Location_Desc").IsVisible = True
        gv2.Columns("Location_Desc").Width = 80
        gv2.Columns("Location_Desc").HeaderText = "Location Description"

        gv2.Columns("Salesman_Name").IsVisible = True
        gv2.Columns("Salesman_Name").Width = 80
        gv2.Columns("Salesman_Name").HeaderText = "Salesman Name"

        gv2.Columns("Vehicle_Code").IsVisible = True
        gv2.Columns("Vehicle_Code").Width = 80
        gv2.Columns("Vehicle_Code").HeaderText = "Vehicle Code"

        gv2.Columns("CrateQtyRecd").IsVisible = True
        gv2.Columns("CrateQtyRecd").Width = 80
        gv2.Columns("CrateQtyRecd").HeaderText = "Qty In"

        

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        'gv2.GroupDescriptors.Add(New GridGroupByExpression("Location_Desc as Item format ""{0}: {1}"" Group By Location_Desc"))
        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True

        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub FormatGridDetails()
        ' Dim strItemCode, head2 As String

        gv2.TableElement.TableHeaderHeight = 25
        gv2.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv2.Columns.Count - 1
            gv2.Columns(ii).ReadOnly = True
            gv2.Columns(ii).IsVisible = False
        Next

        gv2.Columns("Document_Code").IsVisible = True
        gv2.Columns("Document_Code").Width = 100
        gv2.Columns("Document_Code").HeaderText = " Document Code"



        gv2.Columns("CrateQty").IsVisible = True
        gv2.Columns("CrateQty").Width = 100
        gv2.Columns("CrateQty").HeaderText = "Crate Qty "

        gv2.Columns("Document_Date").IsVisible = True
        gv2.Columns("Document_Date").Width = 100
        gv2.Columns("Document_Date").HeaderText = " Date "


        gv2.Columns("Customer_Code").IsVisible = False
        gv2.Columns("Customer_Code").Width = 100
        gv2.Columns("Customer_Code").HeaderText = " Customer Code"

        gv2.Columns("Customer_Name").IsVisible = True
        gv2.Columns("Customer_Name").Width = 100
        gv2.Columns("Customer_Name").HeaderText = "Customer Name"

        gv2.Columns("Bill_To_Location").IsVisible = False
        gv2.Columns("Bill_To_Location").Width = 80
        gv2.Columns("Bill_To_Location").HeaderText = "location Code"

        gv2.Columns("Location_Desc").IsVisible = True
        gv2.Columns("Location_Desc").Width = 80
        gv2.Columns("Location_Desc").HeaderText = "Location Description"

        gv2.Columns("Description").IsVisible = True
        gv2.Columns("Description").Width = 80
        gv2.Columns("Description").HeaderText = "Description"

        gv2.Columns("Cust_PO_No").IsVisible = True
        gv2.Columns("Cust_PO_No").Width = 80
        gv2.Columns("Cust_PO_No").HeaderText = "Cust PO No"

        gv2.Columns("Transporter_Name").IsVisible = True
        gv2.Columns("Transporter_Name").Width = 80
        gv2.Columns("Transporter_Name").HeaderText = "Transporter Name"

        gv2.Columns("Against_Shipment_No").IsVisible = True
        gv2.Columns("Against_Shipment_No").Width = 80
        gv2.Columns("Against_Shipment_No").HeaderText = "Against Shipment No"

        gv2.Columns("Total_Amt").IsVisible = True
        gv2.Columns("Total_Amt").Width = 50
        gv2.Columns("Total_Amt").HeaderText = "Total Amt"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        'gv2.GroupDescriptors.Add(New GridGroupByExpression("Location_Desc as Item format ""{0}: {1}"" Group By Location_Desc"))
        gv2.ShowGroupPanel = False
        gv2.MasterTemplate.AutoExpandGroups = True

        gv2.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub gv2_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellDoubleClick


        Dim strdoc As String = ""
        ' strdoc = clsCommon.myCstr(gv2.CurrentRow.Cells(0).Value)
        strdoc = clsCommon.myCstr(gv2.CurrentColumn.Name.ToString())
        If gv2.Columns.Contains("Document_Code") Then 'clsCommon.CompairString(strdoc, "Document Code") = CompairStringResult.Equal Then
            'If clsCommon.CompairString(gv2, "Document_Code") = CompairStringResult.Equal Then
            If clsCommon.myLen(gv2.CurrentRow.Cells("Document_Code").Value) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, gv2.CurrentRow.Cells("Document_Code").Value)
            End If
            'End If
        End If
        If gv2.Columns.Contains("Document_No") Then 'clsCommon.CompairString(strdoc, "Document Code") = CompairStringResult.Equal Then
            ' If clsCommon.CompairString(strdoc, "Document_No") = CompairStringResult.Equal Then
            If clsCommon.myLen(gv2.CurrentRow.Cells("Document_No").Value) > 0 Then
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCreateReceived, gv2.CurrentRow.Cells("Document_No").Value)
            End If
            'End If
        End If
        'If e.Column Is Gv1.Columns("In_Crate_Qty") Then
        '    If clsCommon.myLen(gv2.CurrentRow.Cells("Document_No").Value) > 0 Then
        '        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCreateReceived, gv2.CurrentRow.Cells("Document_No").Value)

        '    End If
        'End If
        'If e.Column Is Gv1.Columns("Out_Crate_Qty") Then
        '    If clsCommon.myLen(gv2.CurrentRow.Cells("Document_Code").Value) > 0 Then
        '        clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, gv2.CurrentRow.Cells("Document_Code").Value)
        '    End If
        'End If

        'End If
        'If clsCommon.myLen(gv2.CurrentRow.Cells("Document_No").Value) > 0 Then
        '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmInvoiceFreshSale, gv2.CurrentRow.Cells("Document_No").Value)

        'End If

    End Sub
End Class