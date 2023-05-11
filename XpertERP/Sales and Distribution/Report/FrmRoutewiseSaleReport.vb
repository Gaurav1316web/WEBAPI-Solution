Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports common

'-------update by vipin for template finder on '05/07-2012'
'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 31/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------


Public Class FrmRoutewiseSaleReport




    Inherits FrmMainTranScreen
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.FrmRoutewiseSaleReport)
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow("Permission Denied")
            Me.Close()
        End If
    End Sub

    Private Sub FrmTDMSaleReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then
            'SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            reset()
        End If
    End Sub
    Private Sub FrmTDMSaleReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        reset()
        'ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        'ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        'ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+N for reset")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for Print ")










        'If Not clsCommon.CompairString(objCommonVar.CurrentUserCode, "ADMIN") = CompairStringResult.Equal Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If
    End Sub

    

    Sub LoadCustomer()
       

        Dim strquery As String = "select cust_code as [Customer Code], Customer_Name as [Customer Name] from tspl_customer_master "
        cbgCustomer.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgCustomer.ValueMember = "Customer Code"
        cbgCustomer.DisplayMember = "Customer Name"

    End Sub

    Sub LoadLocation()

        'Dim qry As String = " select Segment_code as Code, Description  from TSPL_GL_SEGMENT_CODE where Seg_No='7' "
        'cbgLocation.DataSource = clsDBFuncationality.GetDataTable(qry)
        'cbgLocation.ValueMember = "Code"
        'cbgLocation.DisplayMember = "Description"
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    'Sub Loadchannel()
    '    Dim qry As String = " select Channel_Id as Code,Channel_Name  as Description from TSPL_CHANNEL_MASTER "
    '    cbgchannel.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgchannel.ValueMember = "Code"
    '    cbgchannel.DisplayMember = "Description"
    'End Sub

    Sub LoadRoute()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"

    End Sub

    'Sub LoadTemplate()
    '    Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
    '    cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cgvtemplate.ValueMember = "Template ID"
    '    cgvtemplate.DisplayMember = "Description"
    'End Sub


    '''''''''''''''''''''''''Fills The Data OF Filter 'Company''''
    Sub SetDataBaseGrid()
        gvDB.Rows.Clear()
        gvDB.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.HeaderText = "Select"
        repoSelect.Name = colSelect
        repoSelect.Width = 50
        repoSelect.ReadOnly = False
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvDB.MasterTemplate.Columns.Add(repoSelect)

        Dim repoCompCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompCode.FormatString = ""
        repoCompCode.HeaderText = "Company Code"
        repoCompCode.Name = colCompCode
        repoCompCode.Width = 150
        repoCompCode.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompCode)

        Dim repoCompName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCompName.FormatString = ""
        repoCompName.HeaderText = "Company Name"
        repoCompName.Name = colCompName
        repoCompName.Width = 150
        repoCompName.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoCompName)

        Dim repoDB As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDB.FormatString = ""
        repoDB.HeaderText = "Database Name"
        repoDB.Name = colDataBaseName
        repoDB.IsVisible = False
        repoDB.ReadOnly = True
        gvDB.MasterTemplate.Columns.Add(repoDB)

        Dim qry As String = "SELECT Comp_Code,Comp_Name,DataBase_Name from TSPL_COMPANY_MASTER where len(isnull(DataBase_Name,''))>0 "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                gvDB.Rows.AddNew()
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colSelect).Value = False
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompCode).Value = clsCommon.myCstr(dr("Comp_Code"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colCompName).Value = clsCommon.myCstr(dr("Comp_Name"))
                gvDB.Rows(gvDB.Rows.Count - 1).Cells(colDataBaseName).Value = clsCommon.myCstr(dr("DataBase_Name"))
            Next
        End If
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    End Sub

    Private Function GetSelectedDatabase() As List(Of String)
        Dim arrDBName As New List(Of String)
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If ((clsCommon.myCBool(gvDB.Rows(ii).Cells(colSelect).Value)) OrElse rbtnAllCompany.IsChecked) Then
                arrDBName.Add(clsCommon.myCstr(gvDB.Rows(ii).Cells(colDataBaseName).Value))
            End If
        Next
        Return arrDBName
    End Function


    Sub LoadCustomerCategory()
        Dim qry As String = "select Cust_Type_Code as [Code],Cust_Type_Desc as [Name] from TSPL_CUSTOMER_type_master"
        cbgcategory.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgcategory.ValueMember = "Code"
        cbgcategory.DisplayMember = "Name"
    End Sub


    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        print()

    End Sub
    Sub print()
        Try


            Dim FMon As String = clsCommon.GetPrintDate(dtpFromdate.Value, "MM")
            Dim FMonName As String = clsCommon.GetPrintDate(dtpFromdate.Value, "MMM")

            Dim Tmon As String = clsCommon.GetPrintDate(dtptodate.Value, "MM")
            Dim TmonNAme As String = clsCommon.GetPrintDate(dtptodate.Value, "MMM")

            Dim FYear As String = clsCommon.GetPrintDate(dtpfyear.Value, "yyyy")
            Dim TYear As String = clsCommon.GetPrintDate(dtptyear.Value, "yyyy")



            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one location")
                Return

            ElseIf chkSelectRoute.IsChecked = True AndAlso cbgroute.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Route")
                Return
            ElseIf chkselectcustomer.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Customer")
                Return
            ElseIf chkcategoryselect.IsChecked = True AndAlso cbgcategory.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Category")
                Return
          
            End If

            Dim qry As String = ""
            Dim head1 As String = ""
            Dim head2 As String = ""
            Dim TDMCOdecolumn As String = ""
            Dim group1 As String = ""
            Dim additional As String = ""
            Dim strOrderColumn As String = ""
            Dim strOrderBy As String = ""
            Dim qryqty As String = ""
            Dim qryconqty As String = ""
            Dim conversion As String = ""
            Dim visifilter As String = ""












            '-----------------By Vipin (for reducing the qty of SaleReturn from Sale Invoice)----------------------------

            If ddlconversion.Text = "Converted" Then
                conversion = "Converted"
                qryconqty = "isnull(((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0)  "
                qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) then  (" + qryconqty + "-(select  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) )  else  " + qryconqty + "    end )AS QTY "

            ElseIf ddlconversion.Text = "RAW" Then
                conversion = "RAW"
                qryconqty = "(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )  "
                qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) then  (" + qryconqty + "-(select  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) )  else  " + qryconqty + "    end )AS QTY "


            ElseIf ddlconversion.Text = "80z" Then
                conversion = "8Oz"
                qryconqty = " isnull((( (select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0) "
                qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) then  (" + qryconqty + "-(select  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) )  else  " + qryconqty + "    end )AS QTY "
            End If
            '-----------------------------------------------------------------------------------------

            'If ddltype.Text = "SKU" Then

            If ddlcategory.Text = "HOS" Then
                head1 = "SKU-Wise"
                head2 = "HOS"
                TDMCOdecolumn = "Level2_User_code"
                group1 = "Level1_User_Code"
                additional = "(HOS)"

            ElseIf ddlcategory.Text = "TDM" Then
                head1 = "SKU-Wise"
                head2 = "TDM"
                TDMCOdecolumn = "Level3_User_code"
                group1 = "Level2_User_Code"
                additional = "(TDM)"
            ElseIf ddlcategory.Text = "ADC" Then
                head1 = "SKU-Wise"
                head2 = "ADC"
                TDMCOdecolumn = "Level4_User_code"
                group1 = "Level3_User_Code"
                additional = "(ADC)"
            ElseIf ddlcategory.Text = "CE" Then
                head1 = "SKU-Wise"
                head2 = "CE"
                TDMCOdecolumn = "Level5_User_code"
                group1 = "Level4_User_Code"
                additional = "(CE)"

            ElseIf ddlcategory.Text = "SalesMan" Then
                head1 = "SKU-Wise"
                head2 = "SalesMan"
                TDMCOdecolumn = "Salesman_Code"
                group1 = "Level5_User_Code"
                additional = "(SalesMan)"
            End If
            strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
            strOrderBy = " Order By " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
            strOrderBy = " where xxx.QTY<>0  Order By xxx.OrderBy"




            '-----------------------------------------------------------------------------------------


            Dim strqry As String = "  select  count(distinct route) as Rcount from  ( select  (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No) AS route , (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc ) as routedesc FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " & _
             " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  " & _
              "  INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code " & _
             " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING.Salesman_Code " & _
             "  LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL." + group1 + " = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
              " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
               "  inner  JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
            " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
            " left Outer Join  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on   " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location " & _
             " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code  " & _
            "     left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code    and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code" & _
            "  Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER on " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER.Customer_Id=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
            " WHERE    (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour') and (TSPL_ITEM_DETAILS_1.Class_Name = 'size')  AND (month(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103))) >= '" + FMon + "'   " & _
            "  AND (month(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103))) <= '" + Tmon + "' and (year(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103))) >= '" + FYear + "' and  (year(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)))<='" + TYear + "' and   " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='y'    "








            If chktempall.IsChecked = True Then
                If chkselectcustomer.IsChecked = True Then
                    strqry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            ElseIf chktempselect.IsChecked = True Then
                strqry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                If chkselectcustomer.IsChecked = True Then
                    strqry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            End If



            'If chkselectcustomer.IsChecked = True Then
            '    strqry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
            'End If
            If chkLocSelect.IsChecked = True Then
                strqry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If chkcategoryselect.IsChecked = True Then
                strqry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
            End If

            If chkSelectRoute.IsChecked = True Then
                strqry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
            End If

            strqry += "  )abc    "

            strqry = clsCommon.GetQueryWithAllSelectedDataBase(strqry, GetSelectedDatabase(), False)

            Dim strqry1 As String = "select SUM(Rcount)as Rcount from (" + strqry + ")mainqry"

            Dim routecount As Integer = clsDBFuncationality.getSingleValue(strqry1)



            '-------------------------------------------------------------------------------------------








            qry = "  select  (" + clsCommon.myCstr(routecount) + ") as routecount , '" + FMonName + "' as FMonName,'" + TmonNAme + "' as TmonNAme ,'" + FYear + "' as FYear,'" + TYear + "' as TYear,Mdate,Ydate,TDMCOde,TDMName,route,routedesc, sum(QTY) as QTY ,MAX(conversion) as conversion,MAX( visi) as visi,MAX(heading) as heading ,MAX(heading2) as heading2 ,MAX(Comp_Name) as  Comp_Name,MAX(Add1) as Add1  from (  Select  '" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as PrintDate,(month(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103))) as Mdate,year(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)) as Ydate, '' as visi, '" + objCommonVar.CurrentCompanyName + "' as CurrentComp, (Select Add1  from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as CurrentCompAdd,'" + conversion + "' as conversion, '" + dtpFromdate.Value + "' as fromdate,'" + dtptodate.Value + "' as todate,'" + head1 + "' as heading,'" + head2 + "' as heading2, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AS invoiceno, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date as InDate, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc , TSPL_ITEM_DETAILS_1.Class_Desc as Class_Desc1 , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code as grouping, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.location) as location, (" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc) as LocDesc, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No) AS route , (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_Desc ) as routedesc, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code) as cust, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name) as custdesc,(" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code) as CustType, " + qryqty + ", (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." + TDMCOdecolumn + ") AS TDMCOde, (" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name) AS TDMName, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add1," + strOrderColumn + " as  OrderBy," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " & _
             " INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  " & _
              "  INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code " & _
             " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING.Salesman_Code " & _
             "  LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL." + group1 + " = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
              " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
               "  inner  JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
            " LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code " & _
            " left Outer Join  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on   " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location " & _
             " Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code  " & _
            "     left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code    and " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code" & _
            "  Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER on " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER.Customer_Id=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code " & _
            " WHERE    (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour') and (TSPL_ITEM_DETAILS_1.Class_Name = 'size')  AND (month(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103))) >= '" + FMon + "'   " & _
            "  AND (month(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103))) <= '" + Tmon + "' and (year(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103))) >= '" + FYear + "' and  (year(convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103)))<='" + TYear + "' and   " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='y'  " + visifilter + "  "






            If chktempall.IsChecked = True Then
                If chkselectcustomer.IsChecked = True Then
                    qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            ElseIf chktempselect.IsChecked = True Then
                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                If chkselectcustomer.IsChecked = True Then
                    qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                End If
            End If


            'If chkselectcustomer.IsChecked = True Then
            '    qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
            'End If
            If chkLocSelect.IsChecked = True Then
                qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
            End If

            If chkcategoryselect.IsChecked = True Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
            End If

            If chkSelectRoute.IsChecked = True Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
            End If

            qry += "  )abc    group by route,routedesc,TDMCOde,TDMName,Mdate,Ydate"

            qry = clsCommon.GetQueryWithAllSelectedDataBase(qry, GetSelectedDatabase(), False)

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "RouteMonthwiseSaleRpt", "Routewise Monthly Sale Report")


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        reset()
    End Sub

    Public Sub reset()
        dtpFromdate.Value = clsCommon.GETSERVERDATE()
        dtptodate.Value = clsCommon.GETSERVERDATE()
        dtpfyear.Value = clsCommon.GETSERVERDATE()
        dtptyear.Value = clsCommon.GETSERVERDATE()
        ddlcategory.Text = "HOS"
        'ddltype.Text = "SKU"
        ddlconversion.Text = "Converted"
        SetDataBaseGrid()
        LoadCustomer()
        LoadLocation()
        LoadRoute()
        LoadCustomerCategory()
        'LoadTemplate()
        'Loadchannel()
        'rbtnAllCompany.IsChecked = True
        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next

        chkLocAll.IsChecked = True
        chkAllRoute.IsChecked = True
        'chkchannellall.IsChecked = True
        chkcategoryall.IsChecked = True
        'ddlvisi.Text = "Both"
        'chktempall.IsChecked = True
        chkallcustomer.IsChecked = True
        LoadTemplate()
        chktempall.IsChecked = True



        'cboCustomerClass.Text = "All"
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    'Private Sub ddltype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddltype.SelectedIndexChanged
    '    If ddltype.Text = "SKU" Then
    '        lblconversion.Visible = True
    '        ddlconversion.Visible = True
    '        ddlconversion.Text = "Converted"
    '    Else
    '        lblconversion.Visible = False
    '        ddlconversion.Visible = False
    '    End If
    'End Sub

    Private Sub chkallcustomer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkallcustomer.ToggleStateChanged
        cbgCustomer.Enabled = Not chkallcustomer.IsChecked
    End Sub

    Private Sub cboCustomerClass_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)
        LoadCustomer()
    End Sub

    Private Sub chkLocAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkLocAll.ToggleStateChanged
        cbgLocation.Enabled = Not chkLocAll.IsChecked
    End Sub

    Private Sub chkcategoryall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkcategoryall.ToggleStateChanged
        cbgcategory.Enabled = Not chkcategoryall.IsChecked
    End Sub

    Private Sub rbtnAllCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnAllCompany.ToggleStateChanged
        gvDB.Enabled = False
    End Sub

    Private Sub rbtnSelectCompany_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnSelectCompany.ToggleStateChanged
        gvDB.Enabled = True
    End Sub


    'Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
    '    cgvtemplate.Enabled = Not chktempall.IsChecked
    'End Sub

    Private Sub chkAllRoute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllRoute.ToggleStateChanged
        cbgroute.Enabled = Not chkAllRoute.IsChecked
    End Sub

    Private Sub RadRadioButton1_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)

    End Sub

    'Private Sub chkchannellall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
    '    cbgchannel.Enabled = Not chkchannellall.IsChecked
    'End Sub




    Sub LoadTemplate()
        Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
        cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvtemplate.ValueMember = "Template ID"
        cgvtemplate.DisplayMember = "Description"
    End Sub

    Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktempall.ToggleStateChanged
        cgvtemplate.Enabled = Not chktempall.IsChecked
    End Sub







    Private Sub ddlvisi_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs)

    End Sub

    Private Sub chkselectcustomer_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkselectcustomer.ToggleStateChanged

    End Sub
End Class
