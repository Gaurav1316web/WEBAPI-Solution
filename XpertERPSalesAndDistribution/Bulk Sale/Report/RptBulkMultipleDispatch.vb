
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptBulkMultipleDispatch
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptBulkMultipleDispatch)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub txtLocation1Mult__My_Click(sender As Object, e As EventArgs) Handles txtLocation1Mult._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER  "
        txtLocation1Mult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtLocation1Mult.arrValueMember, txtLocation1Mult.arrDispalyMember)
    End Sub

    Private Sub RptBulkMultipleDispatch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
       
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        gv.Rows.Clear()

    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBulkMultipleDispatch & "'"))
            If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation1Mult.arrValueMember)
                arrHeader.Add(("Location : " + strLocationName + " "))
            Else
                arrHeader.Add(("Location : All"))
            End If
            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomerMult.arrValueMember))
            Else
                arrHeader.Add(("Customer : All"))
            End If
            If TxtMultiCustomerGroup.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerGroup.arrValueMember))
            Else
                arrHeader.Add(("Customer Group : All"))
            End If

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Bulk Dispatch", gv, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Bulk Dispatch", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Try
            If gv.Rows.Count > 0 Then

                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptBulkMultipleDispatch & "'"))

                If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
                    Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation1Mult.arrValueMember)
                    arrHeader.Add((" Location : " + strLocationName + " "))
                Else
                    arrHeader.Add((" Location : All"))
                End If
                If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                    arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomerMult.arrDispalyMember))
                End If

                If TxtMultiCustomerGroup.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerGroup.arrValueMember.Count > 0 Then
                    arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(TxtMultiCustomerGroup.arrValueMember))
                Else
                    arrHeader.Add(("Customer Group : All"))
                End If

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
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Sub loadReport()

        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If

        If clsCommon.myCDate(txtFromDate.Value) < objCommonVar.GSTApplicableDate AndAlso clsCommon.myCDate(txtToDate.Value) > objCommonVar.GSTApplicableDate Then
            clsCommon.MyMessageBoxShow("Please Select From Date and To date range without GST or within GST", Me.Text)
            Exit Sub
        End If

        Dim sQuery As String = " select  Cast(1 as BIT) as 'Check',TSPL_Dispatch_BulkSale.Document_No  as DispatchNo,convert(varchar ,TSPL_Dispatch_BulkSale.Document_Date,103) as DispatchDate ,QC_Code ,Tanker_Code ,TSPL_Dispatch_BulkSale.Location_Code , TSPL_LOCATION_MASTER.Location_Desc , TSPL_COMPANY_MASTER.Comp_Name as CompName,TSPL_Dispatch_BulkSale.Customer_Code,  TSPL_CUSTOMER_MASTER.Customer_Name as CustName,cust_category_desc,TSPL_CUSTOMER_MASTER.Add1 as Customer_Add1,TSPL_CUSTOMER_MASTER.add2 as   customer_Add2,TSPL_CUSTOMER_MASTER.Add3 as customer_Add3,TSPL_CUSTOMER_MASTER.Cust_Group_Code as Customer_Group,TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc   , TSPL_Dispatch_BulkSale.Total_Amt as DocAmt    from TSPL_Dispatch_BulkSale " & _
 " left outer join TSPL_LOCATION_MASTER on TSPL_Dispatch_BulkSale.Location_Code  =TSPL_LOCATION_MASTER.Location_Code  " & _
" left outer join TSPL_COMPANY_MASTER on  tspl_company_Master.Comp_Code = TSPL_Dispatch_BulkSale.comp_code " & _
" left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =TSPL_Dispatch_BulkSale.Customer_Code " & _
" left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
" left outer join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_COMPANY_MASTER.State " & _
" left outer join TSPL_CITY_MASTER  as TSPL_CITY_MASTER_For_Comp on TSPL_CITY_MASTER_For_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code  " & _
" left join TSPL_CUSTOMER_CATEGORY_MASTER on TSPL_CUSTOMER_CATEGORY_MASTER.cust_category_code =TSPL_CUSTOMER_MASTER.cust_category_code  " & _
" left join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code =TSPL_CUSTOMER_MASTER.Cust_Group_Code " & _
 " where    convert(date,TSPL_Dispatch_BulkSale.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_Dispatch_BulkSale.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) and TSPL_Dispatch_BulkSale.Posted = 1"
        If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") "
        End If
        If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
            sQuery += "and TSPL_Dispatch_BulkSale.customer_code in (" + clsCommon.GetMulcallString(txtCustomerMult.arrValueMember) + ") " + Environment.NewLine
        End If
       
        If TxtMultiCustomerGroup.arrValueMember IsNot Nothing AndAlso TxtMultiCustomerGroup.arrValueMember.Count > 0 Then
            sQuery += " AND TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + clsCommon.GetMulcallString(TxtMultiCustomerGroup.arrValueMember) + ") " + Environment.NewLine
        End If
        sQuery += " order by convert(date,TSPL_Dispatch_BulkSale.Document_Date,103) "
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.EnableFiltering = True
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
        Next

        gv.Columns("Check").IsVisible = True
        gv.Columns("Check").Width = 100
        gv.Columns("Check").HeaderText = " "
        gv.Columns("Check").ReadOnly = False

        gv.Columns("DispatchNo").IsVisible = True
        gv.Columns("DispatchNo").Width = 100
        gv.Columns("DispatchNo").HeaderText = "Document No"


        gv.Columns("DispatchDate").IsVisible = True
        gv.Columns("DispatchDate").Width = 100
        gv.Columns("DispatchDate").HeaderText = " Date"
        gv.Columns("DispatchDate").FormatString = "{0:d}"

        gv.Columns("QC_Code").IsVisible = True
        gv.Columns("QC_Code").Width = 100
        gv.Columns("QC_Code").HeaderText = "QC Code"

        gv.Columns("Tanker_Code").IsVisible = True
        gv.Columns("Tanker_Code").Width = 100
        gv.Columns("Tanker_Code").HeaderText = "Tanker Code"

        gv.Columns("Location_Code").IsVisible = True
        gv.Columns("Location_Code").Width = 100
        gv.Columns("Location_Code").HeaderText = "Location Code"


        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 100
        gv.Columns("Location_Desc").HeaderText = "Location Name"

        gv.Columns("Customer_Code").IsVisible = True
        gv.Columns("Customer_Code").Width = 150
        gv.Columns("Customer_Code").HeaderText = "Customer Code"


        gv.Columns("CustName").IsVisible = True
        gv.Columns("CustName").Width = 150
        gv.Columns("CustName").HeaderText = "Customer Name"

        gv.Columns("Customer_Add1").IsVisible = True
        gv.Columns("Customer_Add1").Width = 100
        gv.Columns("Customer_Add1").HeaderText = " Customer Address"

        gv.Columns("DocAmt").IsVisible = True
        gv.Columns("DocAmt").Width = 100
        gv.Columns("DocAmt").HeaderText = "Amount"

        gv.Columns("cust_category_desc").IsVisible = False
        gv.Columns("cust_category_desc").Width = 150
        gv.Columns("cust_category_desc").HeaderText = "Customer Category"

        gv.Columns("Customer_Add1").IsVisible = False
        gv.Columns("Customer_Add1").Width = 100
        gv.Columns("Customer_Add1").HeaderText = "Customer address1"

        gv.Columns("customer_Add2").IsVisible = False
        gv.Columns("customer_Add2").Width = 100
        gv.Columns("customer_Add2").HeaderText = "Customer address2"


        gv.Columns("customer_Add3").IsVisible = False
        gv.Columns("customer_Add3").Width = 100
        gv.Columns("customer_Add3").HeaderText = "Customer address3"


        gv.Columns("Customer_Group").IsVisible = True
        gv.Columns("Customer_Group").Width = 150
        gv.Columns("Customer_Group").HeaderText = "Customer Group Code"

        gv.Columns("Cust_Group_Desc").IsVisible = True
        gv.Columns("Cust_Group_Desc").Width = 150
        gv.Columns("Cust_Group_Desc").HeaderText = "Customer Group Name"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("DocAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)



        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        loadData()

    End Sub

    Sub loadData()


        ArrInvoice_Arr = New ArrayList


        Dim InvoiceNo As String = ""

        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                InvoiceNo = InvoiceNo + "','" + clsCommon.myCstr(grow.Cells("DispatchNo").Value)
            End If
        Next

        If clsCommon.myLen(InvoiceNo) > 0 AndAlso clsCommon.myCstr(InvoiceNo).Substring(0, 3) = "','" Then
            InvoiceNo = InvoiceNo.Substring(3, InvoiceNo.Length - 3)
        End If




        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
      
        Dim objDBS As New FrmDispatchBulkSale
        Dim Qry As String = objDBS.GetPrintQuery()
        Qry += " where 1=1 and TSPL_Dispatch_BulkSale .Document_No in('" + InvoiceNo + "')"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        If dt.Rows.Count > 0 Then

            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptDispatchBulkSale", "Milk Sales Dispatch", clsCommon.myCDate(dt.Rows(0)("Dispatch_date")), "rptCompanyAddress.rpt")
            frmCRV = Nothing
        End If



    End Sub
    
    Public Function LoadPrintQuery(ByVal strinvoiceNo) As String
        Dim Qry As String = " Select TSPL_COMPANY_MASTER.Comp_Name as Company_Name,TSPL_CUSTOMER_MASTER.Add1 as ConAddress1,TSPL_CUSTOMER_MASTER.Add2 as ConAddress2," & _
        " TSPL_CUSTOMER_MASTER.Add3 as ConAddress3,TSPL_LOCATION_MASTER.Add1  as Address1,TSPL_LOCATION_MASTER.Add2 as Address2,TSPL_LOCATION_MASTER.Add3 as Address3, " & _
        " TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0  " & _
        " then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_CITY_MASTER_fOR_Comp.City_Name)>0 then ', '+TSPL_CITY_MASTER_fOR_Comp.City_Name else ' ' " & _
        " end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then ', '+ TSPL_STATE_MASTER_For_Comp.STATE_NAME else ' ' end  + case when len(TSPL_COMPANY_MASTER.Pincode    )>0 " & _
        " then ', Pin Code - '+ cast(TSPL_COMPANY_MASTER.Pincode as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Tin_No     )>0 then ', Tin No - '+ cast(TSPL_COMPANY_MASTER.Tin_No as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.CINNo      )>0 then " & _
        " ', CIN No - '+ cast(TSPL_COMPANY_MASTER.CINNo as varchar)  else ' ' end  + case when len(TSPL_COMPANY_MASTER.Fax     )>0 then ',Fax '+ TSPL_COMPANY_MASTER.Fax else '' end   + Case when len(ISNULL(TSPL_COMPANY_MASTER.Phone1,''))>0 and " & _
        " TSPL_COMPANY_MASTER.Phone1='(+__)__________' then '' else ' ,Phone'+TSPL_COMPANY_MASTER.Phone1 end   +  Case When   ISNULL(TSPL_COMPANY_MASTER.Phone2,'')<>'(+__)__________' Then '  '+ TSPL_COMPANY_MASTER.Phone2 Else'' End   + case when len(TSPL_COMPANY_MASTER.Email    )>0 then " & _
        " ',Email - '+ TSPL_COMPANY_MASTER.Email else '' end  as Company_Address, TSPL_COMPANY_MASTER .Pincode as PinNo,TSPL_COMPANY_MASTER.CINNo as CinNo,TSPL_Dispatch_BulkSale.Document_No as DispatchNo,Convert(varchar,TSPL_Dispatch_BulkSale.Document_Date,106) as Dispatchdate, " & _
        " '' as Suppliersref,TSPL_Dispatch_BulkSale.Document_No  AS DespatchDocumentNo, CityMaster.City_Name as Despatchedthrough,TSPL_CUSTOMER_MASTER.Customer_Name as Consignee,TSPL_CUSTOMER_MASTER.Add1 +case when len(TSPL_CUSTOMER_MASTER.Add2)>0 then ', '+TSPL_CUSTOMER_MASTER.Add2 else '' end +case  " & _
        " when LEN(isnull(TSPL_CUSTOMER_MASTER.Add3,''))>0 then ', '+isnull(TSPL_CUSTOMER_MASTER.Add3,'') else ' ' end as Consignee_Address,0 as SL_No,  TSPL_Dispatch_BulkSale.Tanker_Code  TankerNo,TSPL_Dispatch_Detail_BulkSale.Qty  as MilkQty,TSPL_Dispatch_Detail_BulkSale.FatPer  as Fatper,TSPL_Dispatch_Detail_BulkSale.SNFPer " & _
        " as Snfper, TSPL_Dispatch_Detail_BulkSale.NetMilkRate  as Rate,  TSPL_Dispatch_Detail_BulkSale.Amount  as Amount,TSPL_Dispatch_BulkSale.Created_By as CreatedBy,TSPL_Dispatch_BulkSale.Modified_By as ModifiedBy, TSPL_Dispatch_BulkSale.Total_Amt as DocumentAmount,TSPL_Dispatch_Detail_BulkSale.StandardRate ,TSPL_CUSTOMER_MASTER.Tin_No as " & _
        " ConsigneeTinno,isnull(TSPL_CUSTOMER_MASTER.PIN_Code,'') as ConsigneePin from TSPL_Dispatch_Detail_BulkSale  Left Outer Join TSPL_Dispatch_BulkSale  on TSPL_Dispatch_Detail_BulkSale.Document_No =TSPL_Dispatch_BulkSale.Document_No Left Outer Join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_Dispatch_BulkSale.Comp_Code   " & _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_Dispatch_BulkSale.Location_Code  Left Outer Join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_Dispatch_BulkSale.Customer_Code  LEFT OUTER JOIN TSPL_CITY_MASTER  AS TSPL_CITY_MASTER_fOR_Comp ON TSPL_CITY_MASTER_fOR_Comp.City_Code =TSPL_COMPANY_MASTER.City_Code " & _
        " LEFT OUTER JOIN TSPL_STATE_MASTER AS TSPL_STATE_MASTER_For_Comp  ON TSPL_STATE_MASTER_For_Comp.STATE_CODE  =TSPL_COMPANY_MASTER.State  left outer join TSPL_CITY_MASTER as CityMaster on CityMaster.City_Code=TSPL_CUSTOMER_MASTER .City_Code  where 1=1 and TSPL_Dispatch_BulkSale .Document_No in  ('" + strinvoiceNo + "')"

        Return Qry
    End Function

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.close()
    End Sub

    Private Sub RptBulkMultipleDispatch_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            loadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            loadData()
        End If
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv.MasterView.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv.MasterView.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub


    Private Sub txtCustomerMult__My_Click(sender As Object, e As EventArgs) Handles txtCustomerMult._My_Click
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master order by Cust_Code"
        txtCustomerMult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtCustomerMult.arrValueMember, txtCustomerMult.arrDispalyMember)
    End Sub

    Private Sub TxtMultiCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles TxtMultiCustomerGroup._My_Click
        Dim qry As String = "select Cust_Group_Code as [Code],Cust_Group_Desc as [Name] from TSPL_CUSTOMER_GROUP_MASTER"
        TxtMultiCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CustGroup", qry, "Code", "Name", TxtMultiCustomerGroup.arrValueMember, TxtMultiCustomerGroup.arrDispalyMember)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
