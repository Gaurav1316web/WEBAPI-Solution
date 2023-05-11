Imports common
Imports System.IO
Public Class RptMPIDReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    '===========================created by Preeti Gupta=====Against Ticket No[8096]=========
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptMPIDReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        BtnPrint.Visible = MyBase.isPrintFlag
    End Sub
    Public Function LoadPrintQuery(ByVal strMPCode) As String
       
        Dim Qry As String = " select TSPL_MP_MASTER.VLC_Code,TSPL_VILLAGE_MASTER.village_name,TSPL_MP_MASTER.MP_Picture,TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img, TSPL_MP_MASTER.MP_Code as [MP Code] , TSPL_MP_MASTER.MP_Code_VLC_Uploader as [MP VLC Code],TSPL_MP_MASTER.MP_Name as [MP Name]," & _
                                "TSPL_MP_MASTER.Village_Code as [Village Code],TSPL_MP_MASTER.Father_Name as [Father Name] ,TSPL_MP_MASTER.Add1 as [Address1],TSPL_MP_MASTER.Add2 as [Address2]," & _
                                " TSPL_MP_MASTER.Zila as [Zila],TSPL_MP_MASTER.tehsil as [Tehsil],TSPL_MP_MASTER.City_code as [City Code],TSPL_CITY_MASTER.City_Name as [City Name] ," & _
                                " TSPL_MP_MASTER.State_Code as [State Code] ,TSPL_STATE_MASTER.STATE_NAME as [State Name],TSPL_MP_MASTER.Country_code as [Country Code] ," & _
                                " TSPL_MP_MASTER.Pin_code as [Pin Code] ,TSPL_MP_MASTER.Telphone ,TSPL_MP_MASTER.email as [Email],TSPL_MP_MASTER.fax as [Fax]," & _
                                 "TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code  )>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_STATE_MASTER_For_Comp.STATE_NAME  )>0 then TSPL_STATE_MASTER_For_Comp.STATE_NAME  else '' end +    case when LEN(TSPL_COMPANY_MASTER.Tin_No  )>0 then ', '+TSPL_COMPANY_MASTER.Tin_No else ' ' end  as Comp_address," & _
                                " convert(varchar,TSPL_MP_MASTER.dob,103) as [DOB] from TSPL_MP_MASTER" & _
                                " left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_MP_MASTER.City_code" & _
                                " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_MP_MASTER.State_Code " & _
                                 " left join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code =TSPL_MP_MASTER.Village_Code " & _
                                " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MP_MASTER.Comp_Code  left join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Comp on TSPL_STATE_MASTER_For_Comp.STATE_CODE =TSPL_COMPANY_MASTER.State " & _
                                " Where 2=2  and TSPL_MP_MASTER.MP_Code in   ('" + strMPCode + "') "
        Return Qry
    End Function
    Sub loadData()


        ArrInvoice_Arr = New ArrayList


        Dim MPCode As String = ""

        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.CompairString(clsCommon.myCBool(grow.Cells(0).Value), True) = CompairStringResult.Equal Then
                MPCode = MPCode + "','" + clsCommon.myCstr(grow.Cells("MP Code").Value)
                End If
        Next

        If clsCommon.myLen(MPCode) > 0 AndAlso clsCommon.myCstr(MPCode).Substring(0, 3) = "','" Then
            MPCode = MPCode.Substring(3, MPCode.Length - 3)
        End If

        'If txtFromDate.Value > txtToDate.Value Then
        '    common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
        '    txtFromDate.Focus()
        '    Exit Sub
        '    End If
        Dim Qry As String = LoadPrintQuery(MPCode)

            'Qry += "   ('" + InvoiceNo + "')  and TSPL_SD_SHIPMENT_DETAIL.Scheme_Item='N'"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)

        If dt.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funsubreportWithdt(CrystalReportFolder.MilkProcurement, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptMPIDReport", "MP ID Report", "")
            frmCRV = Nothing
        Else
            clsCommon.MyMessageBoxShow("No Data found to print")
            'frmCrystalReportViewer.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptMPIDReport", "Fresh Invoice Statement", "rptCompanyAddress.rpt", "FreshHeader.rpt", clsERPFuncationality.CompanyAddresInvoiceHeader())
        End If
    End Sub
    Public Sub loadReport()

        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date")
            txtFromDate.Focus()
            Exit Sub
        End If
        
        Dim sQuery As String = " select  Cast(1 as BIT) as 'Check',TSPL_MP_MASTER.MP_Code as [MP Code] , TSPL_MP_MASTER.MP_Code_VLC_Uploader as [MP VLC Code]," & _
                                " TSPL_MP_MASTER.MP_Name as [MP Name],TSPL_MP_MASTER.Village_Code as [Village Code],TSPL_VILLAGE_MASTER.village_name as [Village Name],TSPL_MP_MASTER.Father_Name as [Father Name] ,TSPL_MP_MASTER.Add1 as [Address1]," & _
                                " TSPL_MP_MASTER.Add2 as [Address2],TSPL_MP_MASTER.Zila as [Zila],TSPL_MP_MASTER.tehsil as [Tehsil],TSPL_MP_MASTER.City_code as [City Code]," & _
                                "  TSPL_CITY_MASTER.City_Name as [City Name] ,TSPL_MP_MASTER.State_Code as [State Code] ,TSPL_STATE_MASTER.STATE_NAME as [State Name]," & _
                                "  TSPL_MP_MASTER.Country_code as [Country Code] ,TSPL_MP_MASTER.Pin_code as [Pin Code] ,TSPL_MP_MASTER.Telphone ,TSPL_MP_MASTER.email as [Email]," & _
                                " TSPL_MP_MASTER.fax as [Fax],convert(varchar,TSPL_MP_MASTER.dob,103) as [DOB] from TSPL_MP_MASTER" & _
                                " left join TSPL_CITY_MASTER on TSPL_CITY_MASTER.City_Code =TSPL_MP_MASTER.City_code" & _
                                " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE =TSPL_MP_MASTER.State_Code " & _
                                 " left join TSPL_VILLAGE_MASTER on TSPL_VILLAGE_MASTER.Village_Code =TSPL_MP_MASTER.Village_Code " & _
                                " left join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code =TSPL_MP_MASTER.Comp_Code  left join TSPL_STATE_MASTER as TSPL_STATE_MASTER_For_Comp on TSPL_STATE_MASTER_For_Comp.STATE_CODE =TSPL_COMPANY_MASTER.State  where 2=2"

        If txtMulMPCode.arrValueMember IsNot Nothing AndAlso txtMulMPCode.arrValueMember.Count > 0 Then
            sQuery += " AND MP_Code in (" + clsCommon.GetMulcallString(txtMulMPCode.arrValueMember) + ") " + Environment.NewLine
        End If


        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            'FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found")
        End If

        gv.BestFitColumns()
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

        gv.Columns("vehicleNo").IsVisible = True
        gv.Columns("vehicleNo").Width = 100
        gv.Columns("vehicleNo").HeaderText = "Vehicle No "
        gv.Columns("vehicleNo").ReadOnly = False

        gv.Columns("Comp_Name").IsVisible = False
        gv.Columns("Comp_Name").Width = 100
        gv.Columns("Comp_Name").HeaderText = "Comp Name"

        gv.Columns("Comp_address").IsVisible = False
        gv.Columns("Comp_address").Width = 100
        gv.Columns("Comp_address").HeaderText = "Comp address"


        gv.Columns("InvoiceNo").IsVisible = True
        gv.Columns("InvoiceNo").Width = 100
        gv.Columns("InvoiceNo").HeaderText = "Sale Invoice No."



        gv.Columns("InvoiceDate").IsVisible = True
        gv.Columns("InvoiceDate").Width = 100
        gv.Columns("InvoiceDate").HeaderText = " Date"
        gv.Columns("InvoiceDate").FormatString = "{0:d}"

        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 100
        gv.Columns("Location_Desc").HeaderText = "Location"

        gv.Columns("CustName").IsVisible = True
        gv.Columns("CustName").Width = 150
        gv.Columns("CustName").HeaderText = "Customer"

        gv.Columns("Customer_Add1").IsVisible = True
        gv.Columns("Customer_Add1").Width = 100
        gv.Columns("Customer_Add1").HeaderText = " Customer Address"

        gv.Columns("DocAmt").IsVisible = True
        gv.Columns("DocAmt").Width = 100
        gv.Columns("DocAmt").HeaderText = "Amount"

        gv.Columns("Loc_Add1").IsVisible = False
        gv.Columns("Loc_Add1").Width = 100
        gv.Columns("Loc_Add1").HeaderText = "Location Address1"
        gv.Columns("Loc_Add2").IsVisible = False
        gv.Columns("Loc_Add2").Width = 100
        gv.Columns("Loc_Add2").HeaderText = "Location Address2" '

        gv.Columns("Loc_Add3").IsVisible = False
        gv.Columns("Loc_Add3").Width = 100
        gv.Columns("Loc_Add3").HeaderText = "Location Address3"

        gv.Columns("Logo_Img").IsVisible = False
        gv.Columns("Logo_Img").Width = 100
        gv.Columns("Logo_Img").HeaderText = "Logo Img1"
        gv.Columns("Logo_Img2").IsVisible = False
        gv.Columns("Logo_Img2").Width = 100
        gv.Columns("Logo_Img2").HeaderText = "Logo Img2"

        gv.Columns("Compaddress2").IsVisible = False
        gv.Columns("Compaddress2").Width = 100
        gv.Columns("Compaddress2").HeaderText = "Company address2"
        gv.Columns("Compaddress3").IsVisible = False
        gv.Columns("Compaddress3").Width = 100
        gv.Columns("Compaddress3").HeaderText = "Company address3"



        gv.Columns("customer_Add2").IsVisible = False
        gv.Columns("customer_Add2").Width = 100
        gv.Columns("customer_Add2").HeaderText = "Customer address2"


        gv.Columns("customer_Add3").IsVisible = False
        gv.Columns("customer_Add3").Width = 100
        gv.Columns("customer_Add3").HeaderText = "Customer address3"

        gv.Columns("cust_category_desc").IsVisible = True
        gv.Columns("cust_category_desc").Width = 150
        gv.Columns("cust_category_desc").HeaderText = "Customer Category"



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("DocAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try

            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptMPIDReport & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")


                If txtMulMPCode.arrValueMember IsNot Nothing AndAlso txtMulMPCode.arrValueMember.Count > 0 Then
                    arrHeader.Add("ID Report : " + clsCommon.GetMulcallStringWithComma(txtMulMPCode.arrValueMember))
                Else
                    arrHeader.Add(("ID Report: All"))
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
                    clsCommon.MyExportToPDF(Me.Text, gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
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
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        txtMulMPCode.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
    End Sub
    Private Sub RptMPIDReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub BtnPrint_Click(sender As Object, e As EventArgs) Handles BtnPrint.Click
        loadData()
    End Sub

    Private Sub btnUnSelect_Click(sender As Object, e As EventArgs) Handles btnUnSelect.Click
        If clsCommon.CompairString(btnUnSelect.Text, "UnSelect All") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = False

            Next
            btnUnSelect.Text = "Select All"
        Else
            For Each grow As GridViewRowInfo In gv.Rows
                grow.Cells(0).Value = True
            Next
            btnUnSelect.Text = "UnSelect All"
        End If
    End Sub

    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Print(EnumExportTo.Excel)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub txtMulMPCode__My_Click(sender As Object, e As EventArgs) Handles txtMulMPCode._My_Click
        Dim qry As String = "select MP_Code as Code,MP_Name as Name  from TSPL_MP_MASTER"
        txtMulMPCode.arrValueMember = clsCommon.ShowMultipleSelectForm("MP", qry, "Code", "Name", txtMulMPCode.arrValueMember, txtMulMPCode.arrDispalyMember)
    End Sub

    Private Sub RptMPIDReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
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
End Class
