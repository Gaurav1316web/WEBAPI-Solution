Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'==============================shivani Tyagi==============================>
Public Class RptEffectiveRateReport1
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.RptEffectiveRateReport1)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
        btnGo.Visible = MyBase.isPrintFlag
    End Sub
    Sub LoadPriceCode()
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr("Code") = "PC-BRANCH"
        dr("Name") = "PC-BRANCH"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "PC-DISTRIBUT"
        dr("Name") = "PC-DISTRIBUT"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "PC-RETAILER"
        dr("Name") = "PC-RETAILER"
        dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub
    Private Sub txtLocation1Mult__My_Click(sender As Object, e As EventArgs) Handles txtLocation1Mult._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Location_Type ='Physical'"
        txtLocation1Mult.arrValueMember = clsCommon.ShowMultipleSelectForm("bank", qry, "Code", "Name", txtLocation1Mult.arrValueMember, txtLocation1Mult.arrDispalyMember)
    End Sub

    Private Sub txtCustomerMult__My_Click(sender As Object, e As EventArgs) Handles txtCustomerMult._My_Click
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master order by Cust_Code"
        txtCustomerMult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtCustomerMult.arrValueMember, txtCustomerMult.arrDispalyMember)
    End Sub

    Private Sub TxtMultiItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiItem._My_Click
        Dim qry As String = "Select Item_Code as Code,Item_Desc as Name from TSPL_ITEM_MASTER "
        TxtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("Item", qry, "Code", "Name", TxtMultiItem.arrValueMember, TxtMultiItem.arrDispalyMember)
    End Sub

    Private Sub TxtMultiProductGroup__My_Click(sender As Object, e As EventArgs) Handles TxtMultiProductGroup._My_Click
        Dim qry As String = " select distinct code,Name  from( select 'None' as Code,'None' as Name union all select 'CPD-DESI GHEE' as Code,'CPD-DESI GHEE' as Name union all select 'BULK -DESI GHEE' as Code,'BULK-DESI GHEE' as Name union all select 'CPD-OTHER' as Code,'CPD-OTHER' as Name union all select 'BULK-OTHER' as Code,'BULK-OTHER' as Name union all select distinct CSA_TYPE as Code,CSA_TYPE as Name  from TSPL_ITEM_MASTER ) xx"
        TxtMultiProductGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("Group", qry, "Code", "Name", TxtMultiProductGroup.arrValueMember, TxtMultiProductGroup.arrDispalyMember)
    End Sub

    Private Sub RptEffectiveRateReport1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reset()
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        loadReport()
    End Sub
    Public Sub loadReport()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        Dim PriceComDes As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ', ['+Price_Comp_Desc+']' from (select Price_Comp_Desc  from TSPL_PRICE_COMPONENT_MASTER  ) XXX For XML Path('')),1,1,'')"))
        Dim PriceComDes1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',isnull(['+Price_Comp_Desc+'],0) as ['+Price_Comp_Desc+']' from (select Price_Comp_Desc  from TSPL_PRICE_COMPONENT_MASTER  ) XXX For XML Path('')),1,1,'')"))

        Dim qry As String = "Select Item_Price_ID ,CSA_TYPE,item_code,item_desc,UOM,location_code,Location_Desc,pp ,Item_MRP, " + PriceComDes1 + ",Item_Basic_Price ,Start_Date  from (select * from (select * from(select Item_Price_ID ,tspl_item_master.CSA_TYPE,TSPL_ITEM_PRICE_MASTER.item_code,item_desc,TSPL_ITEM_PRICE_MASTER.UOM,TSPL_ITEM_PRICE_MASTER.location_code,Location_Desc ,TSPL_ITEM_PRICE_MASTER. Price_Code  as pp,Item_MRP ,Item_Basic_Price ,Start_Date   from TSPL_ITEM_PRICE_MASTER " & _
                            " left join tspl_location_master on tspl_location_master.location_code = TSPL_ITEM_PRICE_MASTER.Location_Code " & _
                            " left join tspl_item_master on tspl_item_master.item_code= TSPL_ITEM_PRICE_MASTER .Item_Code " & _
                            "   right join (select distinct TSPL_PRICE_COMPONENT_MAPPING.Price_Code   from TSPL_PRICE_COMPONENT_MAPPING )as PriceMapping " & _
                            " on PriceMapping.Price_Code =TSPL_ITEM_PRICE_MASTER.Price_Code  where 2=2 and TSPL_ITEM_PRICE_MASTER.price_code = '" + cboType.SelectedValue + "' and  convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date ,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

        If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
            qry += " and TSPL_LOCATION_MASTER. Location_Code   IN (" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") "
        End If
        If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
            qry += "and tspl_customer_master.cust_code in (" + clsCommon.GetMulcallString(txtCustomerMult.arrValueMember) + ") " + Environment.NewLine
        End If
        If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
            qry += "and tspl_item_master.item_code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") " + Environment.NewLine
        End If
        If TxtMultiProductGroup.arrValueMember IsNot Nothing AndAlso TxtMultiProductGroup.arrValueMember.Count > 0 Then
            qry += "and tspl_item_master.CSA_TYPE in (" + clsCommon.GetMulcallString(TxtMultiProductGroup.arrValueMember) + ") " + Environment.NewLine
        End If
        qry += " ) as mm  left join " & _
            " (select Des, Amt,Price_Code as [Price Code],Item_Price_ID as id  from (select Price_Comp_Desc1 as Des ,Price_Amount1 as Amt,Price_Code,Item_Price_ID  from TSPL_ITEM_PRICE_MASTER " & _
             " union select Price_Comp_Desc2 as Des ,Price_Amount2 as Amt,Price_Code,Item_Price_ID from TSPL_ITEM_PRICE_MASTER " & _
              " union select Price_Comp_Desc3 as Des ,Price_Amount3 as Amt,Price_Code,Item_Price_ID from TSPL_ITEM_PRICE_MASTER " & _
               " union select Price_Comp_Desc4 as Des ,Price_Amount4 as Amt,Price_Code,Item_Price_ID from TSPL_ITEM_PRICE_MASTER " & _
               " union select Price_Comp_Desc5 as Des ,Price_Amount5 as Amt,Price_Code,Item_Price_ID from TSPL_ITEM_PRICE_MASTER )as ll where Des <> '')as ll on ll.[Price Code] = mm.pp and ll.id = Item_Price_ID) as ff pivot (sum(Amt) for Des in ( " + PriceComDes + " )) as pivott )final "
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()


            'Dim ddP1 As Decimal = clsCommon.myCdbl(dtgv.Compute("sum(Price_Amount1)", ""))
            'Dim ddP2 As Decimal = clsCommon.myCdbl(dtgv.Compute("sum(Price_Amount2)", ""))
            'Dim ddP3 As Decimal = clsCommon.myCdbl(dtgv.Compute("sum(Price_Amount3)", ""))
            'Dim ddP4 As Decimal = clsCommon.myCdbl(dtgv.Compute("sum(Price_Amount4)", ""))
            'Dim ddP5 As Decimal = clsCommon.myCdbl(dtgv.Compute("sum(Price_Amount5)", ""))

            gv.DataSource = dtgv

            'If ddP1 <= 0 Then
            '    gv.Columns("Price_Amount1").IsVisible = False
            '            End If
            'If ddP2 <= 0 Then
            '    gv.Columns("Price_Amount2").IsVisible = False
            '            End If
            'If ddP3 <= 0 Then
            '    gv.Columns("Price_Amount3").IsVisible = False

            '            End If
            'If ddP4 <= 0 Then
            '    gv.Columns("Price_Amount4").IsVisible = False

            '            End If
            'If ddP5 <= 0 Then
            '    gv.Columns("Price_Amount5").IsVisible = False
            '            End If

            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).Width = 100
        Next

        'gv.Columns("Cust_Code").IsVisible = True
        'gv.Columns("Cust_Code").Width = 100
        'gv.Columns("Cust_Code").HeaderText = "Distributor Code"
        'gv.Columns("Cust_Code").ReadOnly = False

        'gv.Columns("Customer_Name").IsVisible = True
        'gv.Columns("Customer_Name").Width = 100
        'gv.Columns("Customer_Name").HeaderText = "Distributor Name"

        gv.Columns("Item_Price_ID").IsVisible = False
        gv.Columns("Item_Price_ID").Width = 100
        gv.Columns("Item_Price_ID").HeaderText = "Item_Price_ID"



        gv.Columns("CSA_TYPE").IsVisible = True
        gv.Columns("CSA_TYPE").Width = 100
        gv.Columns("CSA_TYPE").HeaderText = "Product Group Code"


        gv.Columns("item_code").IsVisible = True
        gv.Columns("item_code").Width = 100
        gv.Columns("item_code").HeaderText = "Item Code"


        gv.Columns("item_desc").IsVisible = True
        gv.Columns("item_desc").Width = 100
        gv.Columns("item_desc").HeaderText = "Item Name"


        gv.Columns("UOM").IsVisible = True
        gv.Columns("UOM").Width = 100
        gv.Columns("UOM").HeaderText = "UOM"

        gv.Columns("location_code").IsVisible = True
        gv.Columns("location_code").Width = 150
        gv.Columns("location_code").HeaderText = "Location Code"


        gv.Columns("Location_Desc").IsVisible = True
        gv.Columns("Location_Desc").Width = 150
        gv.Columns("Location_Desc").HeaderText = "Location Name"

        gv.Columns("pp").IsVisible = True
        gv.Columns("pp").Width = 100
        gv.Columns("pp").HeaderText = "Price Code"

        gv.Columns("Item_MRP").IsVisible = True
        gv.Columns("Item_MRP").Width = 100
        gv.Columns("Item_MRP").HeaderText = "MRP"




        gv.Columns("Item_Basic_Price").IsVisible = True
        gv.Columns("Item_Basic_Price").Width = 100
        gv.Columns("Item_Basic_Price").HeaderText = "Net Rate"

        gv.Columns("Start_Date").IsVisible = True
        gv.Columns("Start_Date").Width = 100
        gv.Columns("Start_Date").HeaderText = "W.e.f"
        gv.Columns("Start_Date").FormatString = "{0:d}"

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        'Dim item1 As New GridViewSummaryItem("Price_Amount1", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        'View()
    End Sub
    Sub View()
        If gv.Rows.Count > 0 Then
            Dim view As New ColumnGroupsViewDefinition()

            view.ColumnGroups.Add(New GridViewColumnGroup(""))
            view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Cust_Code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Customer_Name").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("CSA_TYPE").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("item_code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("item_desc").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("UOM").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("location_code").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Location_Desc").Name)
            view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv.Columns("Price_Code").Name)

            view.ColumnGroups.Add(New GridViewColumnGroup("Rate"))
            view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Item_MRP").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Item_Basic_Price").Name)
            view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv.Columns("Start_Date").Name)
            gv.ViewDefinition = view
        End If

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
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            
            If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation1Mult.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomerMult.arrValueMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiItem.arrValueMember))
            Else
                arrHeader.Add(("Item : All"))
            End If
            If TxtMultiProductGroup.arrValueMember IsNot Nothing AndAlso TxtMultiProductGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Product Group : " + clsCommon.GetMulcallStringWithComma(TxtMultiProductGroup.arrValueMember))
            Else
                arrHeader.Add(("Product Group : All"))
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Effective Rate Report", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Effective Rate Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub
    Private Sub rmSavelayout_Click(sender As Object, e As EventArgs) Handles rmSavelayout.Click

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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadPriceCode()
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
    End Sub
    Private Sub rmDeleteLayut_Click(sender As Object, e As EventArgs) Handles rmDeleteLayut.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub
    Private Sub rmExcel_Click(sender As Object, e As EventArgs) Handles rmExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.RptEffectiveRateReport1 & "'"))

            If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation1Mult.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If
            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomerMult.arrDispalyMember))
            End If
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(TxtMultiItem.arrDispalyMember))
            End If

            If TxtMultiProductGroup.arrValueMember IsNot Nothing AndAlso TxtMultiProductGroup.arrValueMember.Count > 0 Then
                arrHeader.Add("Product Group : " + clsCommon.GetMulcallStringWithComma(TxtMultiProductGroup.arrDispalyMember))
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
            'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
