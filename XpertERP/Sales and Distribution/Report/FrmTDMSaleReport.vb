'--------------------------------Last modify By - Dipti ------------------------------------
'--------------------------------Last modify date - 29/01/2013-------------------------------------
'---------At the form load only that company will keep checked by whom user get logged in------
' for bug no BM00000000900
'--UPdation By--[Pankaj Kumar Chaudhary]--Against Ticket No--[BM00000001230]
Imports Telerik.WinControls
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports System.Data.SqlClient
Imports System.Data
Imports common

Public Class FrmTDMSaleReport

    'update by vipin for matching the Item Qty with Net Sale Detail Report's Qty on 06/06/2012
    'update by vipin for 23/10/12
    'update by vipin for sale return invoice date on 29/10/2012


    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colSelect As String = "SELECT"
    Const colCompCode As String = "COMPCODE"
    Const colCompName As String = "COMPNAME"
    Const colDataBaseName As String = "DATABASE"
    Dim refreshGrid As String = "F"
    Dim strPost, strInterPost, strReturnPost As String

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.SaleReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
           
        End If
        'btnSave.Visible = MyBase.isModifyFlag
        'btnPost.Visible = MyBase.isPostFlag
        'btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmTDMSaleReport_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            'print(false,)
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
        rdbSale.IsChecked = True

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
       
        cbgLocation.DataSource = clsLocation.GetLocationSegments()
        cbgLocation.ValueMember = "Code"
        cbgLocation.DisplayMember = "Name"
    End Sub

    Sub Loadchannel()
        Dim qry As String = " select Channel_Id as Code,Channel_Name  as Description from TSPL_CHANNEL_MASTER "
        cbgchannel.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgchannel.ValueMember = "Code"
        cbgchannel.DisplayMember = "Description"
    End Sub

    Sub LoadRoute()
        Dim qry As String = "select route_no,route_desc from TSPL_ROUTE_MASTER order by route_no"
        cbgroute.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgroute.ValueMember = "route_no"
        cbgroute.DisplayMember = "route_desc"

    End Sub

    Sub LoadTemplate()
        Dim qry As String = " select distinct Tmplate_Id as [Template ID] , Description from TSPL_CUSTOMER_TEMPLATE_MASTER "
        cgvtemplate.DataSource = clsDBFuncationality.GetDataTable(qry)
        cgvtemplate.ValueMember = "Template ID"
        cgvtemplate.DisplayMember = "Description"
    End Sub


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
        print(False, 2)

    End Sub



    Sub print(ByVal chk As Boolean, ByVal exporter As EnumExportTo)
        Try

            Dim locationArr As ArrayList = Nothing
            Dim RouteArr As ArrayList = Nothing
            Dim CustomerArr As ArrayList = Nothing
            Dim CategoryArr As ArrayList = Nothing
            Dim ChannelArr As ArrayList = Nothing
            Dim TemplateArr As ArrayList = Nothing
            ' Dim CompanyArr As ArrayList

            If chkLocSelect.IsChecked = True AndAlso cbgLocation.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one location")
                Return
            ElseIf chkLocSelect.IsChecked AndAlso cbgLocation.CheckedValue.Count > 0 Then
                locationArr = cbgLocation.CheckedValue
            End If
            If chkSelectRoute.IsChecked = True AndAlso cbgroute.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Route")
                Return
            ElseIf chkSelectRoute.IsChecked AndAlso cbgroute.CheckedValue.Count > 0 Then
                RouteArr = cbgroute.CheckedValue
            End If
            If chkselectcustomer.IsChecked = True AndAlso cbgCustomer.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Customer")
                Return
            ElseIf chkselectcustomer.IsChecked AndAlso cbgCustomer.CheckedValue.Count > 0 Then
                CustomerArr = cbgCustomer.CheckedValue

            End If
            If chkcategoryselect.IsChecked = True AndAlso cbgcategory.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Category")
                Return
            ElseIf chkcategoryselect.IsChecked AndAlso cbgcategory.CheckedValue.Count > 0 Then
                CategoryArr = cbgcategory.CheckedValue
            End If

            If chkchanselect.IsChecked = True AndAlso cbgchannel.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Channel ")
                Return
            ElseIf chkchanselect.IsChecked AndAlso cbgchannel.CheckedValue.Count > 0 Then
                ChannelArr = cbgchannel.CheckedValue
            End If

            If chktempselect.IsChecked = True AndAlso cgvtemplate.CheckedValue.Count = 0 Then
                common.clsCommon.MyMessageBoxShow("Please select atleast one Template")
                Return
            ElseIf chktempselect.IsChecked AndAlso cgvtemplate.CheckedValue.Count > 0 Then
                TemplateArr = cgvtemplate.CheckedValue
            End If



            Dim qry As String = ""
            Dim qry1 As String = ""
            Dim finalqry As String = ""
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
            Dim postingdata As String = ""
            Dim qtyReturn As String = ""
            Dim strInterQty, strReturnQty, strTransferQty, strSQL1Group, strSQL2Group, strSQL3Group, strSQL4Group, strTrannsfer, strTransPost, strInterPost As String
            strTransferQty = ""
            strInterPost = ""
            strSQL4Group = ""
            strInterQty = ""
            strTransPost = ""
            strSQL3Group = ""
            strReturnQty = ""
            strSQL2Group = ""
            strSQL1Group = ""
            If rdoalldata.IsChecked = True Then
                strPost = ""
                strReturnPost = ""
                strTransPost = ""
                strInterPost = ""
            ElseIf rdoposted.IsChecked = True Then
                strPost = " and Is_Post='Y' "
                strReturnPost = " and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Is_Post='Y'"
                strTransPost = " and Post='Y'    "
                strInterPost = " and Is_Post=1    "
            End If

            If ddlvisi.Text = "Both" Then
                visifilter = " "

            ElseIf ddlvisi.Text = "With Visi" Then
                'visifilter = "  and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Visi_Id<>''"
                visifilter = "  and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code in (select " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER.Customer_Id from " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER where " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER.Customer_Id <> '')"
            Else
                'visifilter = "  and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Visi_Id=''"
                visifilter = "  and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code not in (select " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER.Customer_Id from " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER where " + clsCommon.ReplicateDBString + "TSPL_VISI_MASTER.Customer_Id <> '')"
            End If





            '-----------------By Vipin (for reducing the qty of SaleReturn from Sale Invoice)----------------------------



            '-------for return on sale invoice

            If ddlconversion.Text = "Converted" Then
                conversion = "Converted"
                qryqty = "isnull((isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1) /  isnull( (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  ),0) as qty  "
                strReturnQty = "- (isnull(((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0))  as Qty  "
                strTransferQty = "(isnull(((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code))* (case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.UOM ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0))  as Qty  "
                strInterQty = "- (isnull(((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) /  (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.QTy,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  ),0))  as Qty  "

            ElseIf ddlconversion.Text = "RAW" Then
                conversion = "RAW"
                qryqty = "(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then convert(decimal(18,2),(isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))) else  convert(decimal(18,2),(isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1))) end  ) as Qty  "
                strReturnQty = " - ((case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then convert(decimal(18,2),(isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0))) else  convert(decimal(18,2),(isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0))) end  ) )   as Qty  "
                strTransferQty = " ((case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.UOM ='FC' then convert(decimal(18,2),(isnull(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0))) else  convert(decimal(18,2),(isnull(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0))) end  ) )   as Qty  "
                strInterQty = " - ((case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FC' then convert(decimal(18,2),(isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0))) else  convert(decimal(18,2),(isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0))) end  ) )   as Qty  "

            ElseIf ddlconversion.Text = "80z" Then
                conversion = "8Oz"
                qryqty = " isnull((( isnull((select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1) / isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) ,1)  ) *isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),0)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  )),0) as Qty "
                strReturnQty = " - (isnull((( (select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0) ) as   Qty "
                strTransferQty = " - (isnull((( (select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   *(case when " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.UOM ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0) ) as   Qty "
                strInterQty = " - (isnull((( (select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) / (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   ) *(select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,0)) end  )),0) ) as   Qty "

            End If

            'If ddlconversion.Text = "Converted" Then
            '    conversion = "Converted"
            '    qryconqty = "isnull((isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1) /  isnull( (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  ),0)   "
            '    qtyReturn = "isnull((isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1) /  isnull( (select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1))* (case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  ),0)    "
            '    'qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) then  (" + qryconqty + "-(select top 1 " + qtyReturn + " as Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code) )  else  " + qryconqty + "    end )AS QTY "

            '    qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No   and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code) then  (" + qryconqty + "-(select top 1 " + qtyReturn + " as Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code) )  else  " + qryconqty + "    end )AS QTY "

            'ElseIf ddlconversion.Text = "RAW" Then
            '    conversion = "RAW"
            '    qryconqty = "(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  )  "
            '    qtyReturn = "(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  )  "
            '    'qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) then  (" + qryconqty + "-(select top 1  " + qtyReturn + " as Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code)  )  else  " + qryconqty + "    end )AS QTY "
            '    qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code) then  (" + qryconqty + "-(select top 1  " + qtyReturn + " as Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code)  )  else  " + qryconqty + "    end )AS QTY "

            'ElseIf ddlconversion.Text = "80z" Then
            '    conversion = "8Oz"
            '    qryconqty = " isnull((( isnull((select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1) / isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) ,1)  ) *isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),0)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Invoice_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  )),0)  "
            '    qtyReturn = " isnull((( isnull((select " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='fb'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),1) / isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='con'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code) ,1)  ) *isnull((select distinct " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.conversion_factor from " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL where " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.uom_code='8oz'and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.item_code =" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.item_code),0)   *(case when " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code ='FC' then (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0))* (isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) else  (isnull(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Return_Qty,0) / isnull(" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Conversion_Factor,1)) end  )),0)  "
            '    'qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No) then  (" + qryconqty + "-(select top 1  " + qtyReturn + " as Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code) )  else  " + qryconqty + "    end )AS QTY "
            '    qryqty = "( case when " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No=(select distinct " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code) then  (" + qryconqty + "-(select top 1  " + qtyReturn + " as Return_Qty from  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD left outer join  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No where " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Sale_Invoice_No  and " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.unit_code=" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Unit_code) )  else  " + qryconqty + "    end )AS QTY "
            'End If








            '-----------------------------------------------------------------------------------------

            If ddltype.Text = "SKU" Then
                If rdbShipping.IsChecked Then
                    strSQL1Group = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP_amt * Conversion_Factor) ) +  ' ) ' "
                    strSQL2Group = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP_amt * Conversion_Factor) ) +  ' ) ' "
                    strSQL3Group = "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) '"
                    strSQL4Group = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP_amt * Conversion_Factor) ) +  ' ) ' "

                Else
                    strSQL1Group = "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code "
                    strSQL2Group = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code  "
                    strSQL3Group = "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code "
                    strSQL4Group = "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code  "

                End If
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"
                strOrderBy = " where xxx.QTY<>0  Order By xxx.OrderBy"

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

            ElseIf ddltype.Text = "Flavour" Then
                If rdbShipping.IsChecked Then
                    strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP_amt * Conversion_Factor) ) +  ' ) ' "
                    strSQL2Group = " TSPL_ITEM_DETAILS.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP_amt * Conversion_Factor) ) +  ' ) ' "
                    strSQL3Group = " TSPL_ITEM_DETAILS.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) ' "
                    strSQL4Group = " TSPL_ITEM_DETAILS.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) ' "

                Else
                    strSQL1Group = "TSPL_ITEM_DETAILS.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  "
                    strSQL2Group = " TSPL_ITEM_DETAILS.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  "
                    strSQL3Group = " TSPL_ITEM_DETAILS.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  "
                    strSQL4Group = " TSPL_ITEM_DETAILS.Class_Desc +'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq)+')'  "

                End If

                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Flavour_Seq"
                strOrderBy = " Order By xxx.OrderBy"
                If ddlcategory.Text = "HOS" Then
                    head1 = "Flavour-Wise"
                    head2 = "HOS"
                    TDMCOdecolumn = "Level2_User_code"
                    group1 = "Level1_User_Code"
                    additional = "(HOS)"
                ElseIf ddlcategory.Text = "TDM" Then
                    head1 = "Flavour-Wise"
                    head2 = "TDM"
                    TDMCOdecolumn = "Level3_User_code"
                    group1 = "Level2_User_Code"
                    additional = "(TDM)"
                ElseIf ddlcategory.Text = "ADC" Then
                    head1 = "Flavour-Wise"
                    head2 = "ADC"
                    TDMCOdecolumn = "Level4_User_code"
                    group1 = "Level3_User_Code"
                    additional = "(ADC)"
                ElseIf ddlcategory.Text = "CE" Then
                    head1 = "Flavour-Wise"
                    head2 = "CE"
                    TDMCOdecolumn = "Level5_User_code"
                    group1 = "Level4_User_Code"
                    additional = "(CE)"

                ElseIf ddlcategory.Text = "SalesMan" Then
                    head1 = "Flavour-Wise"
                    head2 = "SalesMan"
                    TDMCOdecolumn = "Salesman_Code"
                    group1 = "Level5_User_Code"
                    additional = "(SalesMan)"
                End If

            ElseIf ddltype.Text = "Pack" Then
                If rdbShipping.IsChecked Then
                    strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP_amt * Conversion_Factor) ) +  ' ) ' "
                    strSQL2Group = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')'  + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP_amt * Conversion_Factor) ) +  ' ) ' "
                    strSQL3Group = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) ' "
                    strSQL4Group = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')' + ' ( ' +  convert(varchar(20),convert(decimal(18,2),MRP * Conversion_Factor) ) +  ' ) ' "

                Else
                    strSQL1Group = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')'  "
                    strSQL2Group = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')'  "
                    strSQL3Group = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')'  "
                    strSQL4Group = "TSPL_ITEM_DETAILS_1.Class_Desc+'('+convert(varchar," + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq)+')'  "

                End If
                strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Pack_Seq"
                strOrderBy = " Order By xxx.OrderBy"

                If ddlcategory.Text = "HOS" Then
                    head1 = "Pack-Wise"
                    head2 = "HOS"
                    TDMCOdecolumn = "Level2_User_code"
                    group1 = "Level1_User_Code"
                    additional = "(HOS)"
                ElseIf ddlcategory.Text = "TDM" Then
                    head1 = "Pack-Wise"
                    head2 = "TDM"
                    TDMCOdecolumn = "Level3_User_code"
                    group1 = "Level2_User_Code"
                    additional = "(TDM)"
                ElseIf ddlcategory.Text = "ADC" Then
                    head1 = "Pack-Wise"
                    head2 = "ADC"
                    TDMCOdecolumn = "Level4_User_code"
                    group1 = "Level3_User_Code"
                    additional = "(ADC)"
                ElseIf ddlcategory.Text = "CE" Then
                    head1 = "Pack-Wise"
                    head2 = "CE"
                    TDMCOdecolumn = "Level5_User_code"
                    group1 = "Level4_User_Code"
                    additional = "(CE)"

                ElseIf ddlcategory.Text = "SalesMan" Then
                    head1 = "Pack-Wise"
                    head2 = "SalesMan"
                    TDMCOdecolumn = "Salesman_Code"
                    group1 = "Level5_User_Code"
                    additional = "(SalesMan)"

                End If
                End If
                'strOrderColumn = " " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Sku_Seq"

                'strOrderBy = " where xxx.QTY<>0  Order By xxx.OrderBy"


            qry = "  Select  Vehicle_No,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as PrintDate, '" + ddlvisi.Text + "' as visi, '" + objCommonVar.CurrentCompanyName + "' as CurrentComp, (Select Add1  from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as CurrentCompAdd,'" + conversion + "' as conversion, '" + dtpFromdate.Value + "' as fromdate,'" + dtptodate.Value + "' as todate,'" + head1 + "' as heading,'" + head2 + "' as heading2, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No) AS invoiceno,convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date,103) as InvoiceDate, " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc , TSPL_ITEM_DETAILS_1.Class_Desc as Class_Desc1 , " & _
            "" & strSQL1Group & " as grouping, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.location) as location, (" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc) as LocDesc, (" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_No) AS route , (" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_Desc ) as routedesc, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code) as cust, (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name) as custdesc,(" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code) as CustType, " + qryqty + ", (" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." + TDMCOdecolumn + ") AS TDMCOde, (" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name) AS TDMName, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add1," + strOrderColumn + " as  OrderBy," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Desc," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.Item_Desc FROM  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL " & _
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
" WHERE    (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour') and (TSPL_ITEM_DETAILS_1.Class_Name = 'size')  AND (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) >= CONVERT(date,'" + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + "', 103))   " & _
"  AND (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy") + " ', 103)) " + strPost + "  " + visifilter + "  "
                '"  AND (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_Date, 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy") + " ', 103)) and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Is_Post='y'  " + visifilter + "  "


            qry1 = "SELECT  TSPL_SALE_INVOICE_HEAD.Vehicle_No,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' AS PrintDate,'' as visi, " & _
           " '" + objCommonVar.CurrentCompanyName + "' AS CurrentComp, " & _
           "(Select Add1  from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "') AS CurrentCompAdd, " & _
           "'" + conversion + "' AS conversion,   " & _
           "'" + dtpFromdate.Value + "' as fromdate,'" + dtptodate.Value + "' as todate, " & _
           "'" + head1 + "' as heading,'" + head2 + "' as heading2,  " & _
           "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No) AS invoiceno," & _
           "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date) as InvoiceDate, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc, " & _
           "TSPL_ITEM_DETAILS_1.Class_Desc AS Class_Desc1,  " & _
           "" & strSQL2Group & "  AS grouping, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location AS location, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc AS LocDesc, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_No AS route,  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_Desc AS routedesc, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code AS cust, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Name AS custdesc, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code AS CustType, " & _
           "  " & strReturnQty & " , " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." & TDMCOdecolumn & " AS TDMCOde,  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name AS TDMName, " & _
           " TSPL_COMPANY_MASTER_1.Comp_Name,  " & _
           "TSPL_COMPANY_MASTER_1.Add1, " & strOrderColumn & " AS OrderBy, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Code," & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Desc, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code, " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Desc " & _
           "FROM " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER FULL OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER RIGHT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD LEFT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER AS emp RIGHT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD LEFT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD." & TDMCOdecolumn & " = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE LEFT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING.Salesman_Code ON  " & _
           "emp.EMP_CODE = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Salesman_Code ON  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Invoice_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Sale_Invoice_No LEFT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER AS TSPL_COMPANY_MASTER_1 ON  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Comp_Code = TSPL_COMPANY_MASTER_1.Comp_Code LEFT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Cust_Code = " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code ON  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Location LEFT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.Route_No = " + clsCommon.ReplicateDBString + "TSPL_ROUTE_MASTER.Route_No LEFT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ROUTE_TYPE ON TSPL_ROUTE_TYPE.Route_Type_Id = TSPL_ROUTE_MASTER.Type FULL OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL RIGHT OUTER JOIN  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL LEFT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code ON  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code AND  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Unit_code LEFT OUTER JOIN " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS AS TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code ON  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Sale_Return_No ON  " & _
           "" + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_DETAIL.Item_Code " & _
           " WHERE    (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour') and " & _
           "(TSPL_ITEM_DETAILS_1.Class_Name = 'size')  AND " & _
           "(CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date, 103) >= CONVERT(date,'" + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + "', 103))   " & _
           "  AND (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.Sale_Return_Date, 103) <= CONVERT(date, '" + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy") + " ', 103))   " & _
           " " + strReturnPost + "     "


                strTrannsfer = " Union All  Select Vehicle_No,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "'as PrintDate, 'Both' as visi, " & _
                "'" + objCommonVar.CurrentCompanyName + "' as CurrentComp, " & _
                "(Select Add1  from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as CurrentCompAdd, " & _
                "'" + conversion + "' as conversion, " & _
                "'" + dtpFromdate.Value + "' as fromdate,'" + dtptodate.Value + "' as todate,'" + head1 + "' as heading,'" + head2 + "' as heading2, " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No) AS invoiceno, " & _
                "convert(date," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date,103) as InvoiceDate, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc , TSPL_ITEM_DETAILS_1.Class_Desc as Class_Desc1 , " & _
                "" & strSQL3Group & " as grouping, " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location) as location, (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.FromLoc_Desc) as LocDesc,  " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No) AS route , (" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_Desc ) as routedesc, " & _
                "(case when TSPL_LOCATION_MASTER.GIT_Type='Y' then TSPL_LOCATION_MASTER_1.Location_Code else TSPL_LOCATION_MASTER.Location_Code end) as cust, " & _
                "(case when TSPL_LOCATION_MASTER.GIT_Type='Y' then TSPL_LOCATION_MASTER_1.Location_Desc else TSPL_LOCATION_MASTER.Location_Desc end) as custdesc, " & _
                "'' as CustType, " & strTransferQty & "  , " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." & TDMCOdecolumn & ") AS TDMCOde, " & _
                "(" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name) AS TDMName, " & _
                "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add1, " & _
                "" & strOrderColumn & " as  OrderBy, " & _
                "'' as Channel_Code,'' as Channel_Desc," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Desc FROM  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD  left outer join " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL  ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_No = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Transfer_No  " & _
                "LEFT outer join  " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code " & _
                "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Salesmancode = " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING.Salesman_Code  " & _
                "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD." & TDMCOdecolumn & " = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE " & _
                "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code " & _
                "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code   " & _
                "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Comp_Code  " & _
                "left Outer Join  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on   " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location  =" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code  " & _
                "left outer join  TSPL_LOCATION_MASTER as  TSPL_LOCATION_MASTER_1 on " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.To_Location  =TSPL_LOCATION_MASTER_1.GIT_Location " & _
                "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Item_Code    and  " & _
                "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_TRANSFER_DETAIL.Uom WHERE    (" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour') and " & _
                "(TSPL_ITEM_DETAILS_1.Class_Name = 'size')  AND (CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, 103) >= CONVERT(date,'" + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MM/yyyy") + "', 103))     AND  " & _
                "(CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Transfer_Date, 103) <=CONVERT(date,'" + clsCommon.GetPrintDate(dtptodate.Value, "dd/MM/yyyy") + "', 103)) and  " & _
                "TSPL_TRANSFER_HEAD.Location_Type='Physical' and Transfer_Type='LO' and Reference_Doc_No =''  " & strTransPost & "   "

            Dim strInter = " Union All  Select  Vehicle_No,'" + clsCommon.GetPrintDate(clsCommon.GETSERVERDATE) + "' as PrintDate, 'Both' as visi, " & _
            "'" + objCommonVar.CurrentCompanyName + "' as CurrentComp, " & _
            "(Select Add1  from TSPL_COMPANY_MASTER WHERE Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as CurrentCompAdd, " & _
            "'" + conversion + "' as conversion, " & _
            "'" + dtpFromdate.Value + "' as fromdate,'" + dtptodate.Value + "' as todate,'" + head1 + "' as heading,'" + head2 + "' as heading2, " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No) AS invoiceno, " & _
            " convert(date," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date,103) as InvoiceDate, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Desc , TSPL_ITEM_DETAILS_1.Class_Desc as Class_Desc1 , " & _
            "" & strSQL4Group & "  as grouping, " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.location) as location, " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Desc) as LocDesc, (" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_No) AS route , " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_Desc ) as routedesc, (" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.cust_code) as cust, " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Name) as custdesc,(" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Type_Code) as CustType, " & _
            "" & strInterQty & "  , " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD." & TDMCOdecolumn & ") AS TDMCOde, " & _
            "(" + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.Emp_Name) AS TDMName, " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Name, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Add1, " & strOrderColumn & " as  OrderBy, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Code," + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Desc, " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code," + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Desc FROM  " & _
            "" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL  " & _
            "INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_No = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Document_No    " & _
            "INNER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_MASTER.Item_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Salesman_Code = " + clsCommon.ReplicateDBString + "TSPL_SALESMAN_MAPPING.Salesman_Code   " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL." & TDMCOdecolumn & " = " + clsCommon.ReplicateDBString + "TSPL_EMPLOYEE_MASTER.EMP_CODE  " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Item_Code   " & _
            "inner  JOIN " + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS as TSPL_ITEM_DETAILS_1 ON " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code = TSPL_ITEM_DETAILS_1.Item_Code " & _
            "LEFT OUTER JOIN " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER on " + clsCommon.ReplicateDBString + "TSPL_COMPANY_MASTER.Comp_Code = " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Comp_Code  " & _
             "left Outer Join  " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER on   " + clsCommon.ReplicateDBString + "TSPL_LOCATION_MASTER.Location_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Location " & _
              "Left Outer Join " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER on  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Cust_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Cust_Code " & _
              "left outer join " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL on " + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.Item_Code= " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Item_Code    and " & _
              "" + clsCommon.ReplicateDBString + "TSPL_ITEM_UOM_DETAIL.UOM_Code=" + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_DETAIL.Unit_code WHERE  " & _
              "(" + clsCommon.ReplicateDBString + "TSPL_ITEM_DETAILS.Class_Name = 'flavour') and (TSPL_ITEM_DETAILS_1.Class_Name = 'size')  AND " & _
              "CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date, 103) >= '" + clsCommon.GetPrintDate(dtpFromdate.Value, "dd/MMM/yyyy") + "'      AND " & _
              "CONVERT(date, " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.Document_Date, 103) <= '" + clsCommon.GetPrintDate(dtptodate.Value, "dd/MMM/yyyy") + "'    " & strInterPost & ""



                If chkLocSelect.IsChecked = True Then

                    qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_DETAIL.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    qry1 += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                    strTrannsfer += " and  " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.From_Location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "
                strInter += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.location in(Select Location_Code  from TSPL_LOCATION_MASTER Where Loc_Segment_Code in (" + clsCommon.GetMulcallString(cbgLocation.CheckedValue) + ")) "

                End If


                If chkcategoryselect.IsChecked = True Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
                    qry1 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Customer_Class in  (" + clsCommon.GetMulcallString(cbgcategory.CheckedValue) + ")"
                End If


                If chkSelectRoute.IsChecked = True Then
                qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_No in  (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
                qry1 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_No in  (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
                    strTrannsfer += " and " + clsCommon.ReplicateDBString + "TSPL_TRANSFER_HEAD.Route_No in  (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"
                strInter += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Route_No in  (" + clsCommon.GetMulcallString(cbgroute.CheckedValue) + ")"

                End If

                If chkchanselect.IsChecked = True Then
                    qry += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Code in  (" + clsCommon.GetMulcallString(cbgchannel.CheckedValue) + ")"
                    qry1 += " and " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_MASTER.Channel_Code in  (" + clsCommon.GetMulcallString(cbgchannel.CheckedValue) + ")"
                    strTrannsfer = ""
                End If

                If chktempall.IsChecked = True Then
                    If chkselectcustomer.IsChecked = True Then
                        qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                        qry1 += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                        strTrannsfer = ""
                    End If
                ElseIf chktempselect.IsChecked = True Then


                    qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                    qry1 += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in  ( select distinct  " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Cust_Id from " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER where " + clsCommon.ReplicateDBString + "TSPL_CUSTOMER_TEMPLATE_MASTER.Tmplate_Id in (" + ((clsCommon.GetMulcallString(cgvtemplate.CheckedValue))) + ")) "
                    strTrannsfer = ""

                    If chkselectcustomer.IsChecked = True Then
                        qry += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_INVOICE_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                        qry1 += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "
                    strTrannsfer = ""
                    qry1 += " and  " + clsCommon.ReplicateDBString + "TSPL_SALE_RETURN_INTER_HEAD.cust_code in(" + (clsCommon.GetMulcallString(cbgCustomer.CheckedValue)) + ") "

                    End If

                End If



                If chkTransfer.Checked = False Then
                    strTrannsfer = ""
            End If

            If chkIC.Checked = False Then
                strInter = ""
            End If

            finalqry = clsCommon.GetQueryWithAllSelectedDataBase(qry + " Union All " + qry1 + strTrannsfer + strInter, GetSelectedDatabase(), False)


                '-----------------for grid code----------------------
            Dim strItemCodestring, strItemCode, strMainItemCode, strmainItemCodeString, strsum As String
            Dim itemcount As String = ""
            strItemCode = ""
            strItemCodestring = ""
            strmainItemCodeString = ""
            strsum = ""
                If ddltype.Text = "SKU" Then
                    itemcount = " select  distinct grouping,OrderBy  from (" + finalqry + ") abc order by OrderBy "

                ElseIf ddltype.Text = "Flavour" Then
                    itemcount = " select  distinct grouping ,OrderBy from (" + finalqry + ") abc order by OrderBy "

                ElseIf ddltype.Text = "Pack" Then
                    itemcount = " select  distinct grouping,OrderBy  from (" + finalqry + ") abc order by OrderBy "
                End If


                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(itemcount)

                'Dim dr As SqlDataReader = connectSql.RunSqlReturnDR(itemcount)

                Dim arritem As New ArrayList
                'While dr.Read
                If (dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0) Then
                    For Each dr As DataRow In dt1.Rows
                        strItemCode = CStr(dr(0).ToString())
                        strItemCodestring = strItemCodestring & "[" & strItemCode & "]" & ","
                        arritem.Add(strItemCode)
                        strMainItemCode = CStr(dr(0).ToString())
                        strmainItemCodeString = strmainItemCodeString & "  isnull(" & "[" & strItemCode & "]" & ",0)  " & "as  " & "[" & strItemCode & "]" & ","
                        'strsum = strItemCode + "+"

                        strsum = strsum & "  isnull(" & "[" & strItemCode & "]" & ",0)" & "+"
                    Next
                End If
                ' End While


                If strItemCode <> "" Then
                    strItemCodestring = strItemCodestring.Substring(0, strItemCodestring.Length - 1)
                    strmainItemCodeString = strmainItemCodeString.Substring(0, strmainItemCodeString.Length - 1)
                    strsum = strsum.Substring(0, strsum.Length - 1)
                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                    Exit Sub
                End If

            Dim strmain As String = ""


                If ddlprinttype.Text = "Detail" Then
                strmain = " select * from ( select  comp_name as [Company Name],locdesc as [Depot], TDMName as Name,Vehicle_No,Route as [Route],routedesc as [Route Desc],invoiceno as [Invoice],InvoiceDate as [Invoice Date],cust as [Customer],custdesc as [Customer Desc] ," & strmainItemCodeString & ",(" + strsum + ")as Total  from(select Vehicle_No,Comp_Name,locDesc,TDMName,Route,routedesc,invoiceno,InvoiceDate,cust,custdesc,sum(Qty) as Qty,grouping from (" + finalqry + ")aaa group by aaa.grouping,aaa.Comp_Name,aaa.locDesc,aaa.TDMName,aaa.Route,aaa.routedesc,aaa.invoiceno,aaa.InvoiceDate,aaa.cust,aaa.custdesc,aaa.Vehicle_No) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 ) as final order by final.[Invoice Date] "
            ElseIf ddlprinttype.Text = "Summary" Then
                If rdbShipping.IsChecked Then
                    strmain = " select  comp_name as [Company Name], TDMName as Name,Vehicle_No,Route as [Route],routedesc as [Route Desc],cust as [Customer],custdesc as [Customer Desc] ," & strmainItemCodeString & ",(" + strsum + ")as Total  from(select Vehicle_No,Comp_Name,TDMName,Route,routedesc,cust,custdesc,sum(Qty) as Qty,grouping from (" + finalqry + ")aaa group by aaa.grouping,aaa.Comp_Name,aaa.TDMName,aaa.Route,aaa.routedesc,aaa.cust,aaa.custdesc,aaa.Vehicle_No) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 "
                Else
                    strmain = " select  comp_name as [Company Name], TDMName as Name,Route as [Route],routedesc as [Route Desc],cust as [Customer],custdesc as [Customer Desc] ," & strmainItemCodeString & " ,(" + strsum + ")as Total from(select Comp_Name,TDMName,Route,routedesc,cust,custdesc,sum(Qty) as Qty,grouping from (" + finalqry + ")aaa group by aaa.grouping,aaa.Comp_Name,aaa.TDMName,aaa.Route,aaa.routedesc,aaa.cust,aaa.custdesc) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 "
                End If
            ElseIf ddlprinttype.Text = "Routewise" Then
                strmain = " select  comp_name as [Company Name],Route as [Route],routedesc as [Route Desc] ," & strmainItemCodeString & ",(" + strsum + ")as Total  from(select Comp_Name,Route,routedesc,sum(Qty) as Qty,grouping from (" + finalqry + ")aaa group by aaa.grouping,aaa.Comp_Name,aaa.Route,aaa.routedesc) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 "
            ElseIf ddlprinttype.Text = "Hierarchywise" Then
                strmain = " select   TDMName as Name ," & strmainItemCodeString & ",(" + strsum + ")as Total  from(select TDMName,sum(Qty) as Qty,grouping from (" + finalqry + ")aaa group by aaa.grouping,aaa.TDMName) down pivot (SUM(qty) FOR grouping IN ( " & strItemCodestring & ")) AS pvt1 "
            End If

            'strmain = "select * from (" + strmain + ") as final order by final.[Invoice Date] "

            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(strmain)
            If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                gv.DataSource = dtgv
                gv.MasterTemplate.SummaryRowsBottom.Clear()

            End If
            gridformat(arritem)

            RadPageView1.SelectedPage = View


            '-----------------------------------------------------
            finalqry += "  " + strOrderBy + ""

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(finalqry)
            If chk = True Then
                Dim str As String = "Sale Volume Report"

                If refreshGrid = "F" Then


                    Dim arr As New List(Of String)()
                    If ddlprinttype.Text = "Detail" Then
                        arr.Add(objCommonVar.CurrentCompanyName)
                        arr.Add("Sale Volume Report (Detail)")
                        arr.Add("" + head2 + "-Wise")
                        arr.Add("Convertion :" + conversion + "      Report Type:" + head1 + "")
                        arr.Add("Start Date:-" + dtpFromdate.Value + "")
                        arr.Add("End Date:-" + dtptodate.Value + "")
                        If clsCommon.myLen(locationArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(locationArr))
                        End If

                        If clsCommon.myLen(RouteArr) > 0 Then
                            arr.Add("Route Segment : " + clsCommon.GetMulcallString(RouteArr))
                        End If

                        If clsCommon.myLen(CustomerArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(CustomerArr))
                        End If

                        If clsCommon.myLen(CategoryArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(CategoryArr))
                        End If
                        If clsCommon.myLen(ChannelArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(ChannelArr))
                        End If
                        If clsCommon.myLen(TemplateArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(TemplateArr))
                        End If
                        ' clsCommon.MyExportToExcel(str, gv, arr, "SaleVolumeReportDetail")

                        If exporter = EnumExportTo.Excel Then
                            clsCommon.MyExportToExcelGrid(str, gv, arr, "SaleVolumeReportDetail")

                        Else
                            clsCommon.MyExportToPDF(str, gv, arr, "SaleVolumeReportDetail", True)
                        End If
                    ElseIf ddlprinttype.Text = "Summary" Then
                        arr.Add(objCommonVar.CurrentCompanyName)
                        arr.Add("Sale Volume Report  (Summary)")
                        arr.Add("" + head2 + "-Wise")
                        arr.Add("Convertion :" + conversion + "      Report Type:" + head1 + "")
                        arr.Add("Start Date:-" + dtpFromdate.Value + "")
                        arr.Add("End Date:-" + dtptodate.Value + "")
                        If clsCommon.myLen(locationArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(locationArr))
                        End If

                        If clsCommon.myLen(RouteArr) > 0 Then
                            arr.Add("Route Segment : " + clsCommon.GetMulcallString(RouteArr))
                        End If

                        If clsCommon.myLen(CustomerArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(CustomerArr))
                        End If

                        If clsCommon.myLen(CategoryArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(CategoryArr))
                        End If
                        If clsCommon.myLen(ChannelArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(ChannelArr))
                        End If
                        If clsCommon.myLen(TemplateArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(TemplateArr))
                        End If
                        ' clsCommon.MyExportToExcel(str, gv, arr, "SaleVolumeReportSummary")

                        If exporter = EnumExportTo.Excel Then
                            clsCommon.MyExportToExcelGrid(str, gv, arr, "SaleVolumeReportSummary")

                        Else
                            clsCommon.MyExportToPDF(str, gv, arr, "SaleVolumeReportSummary", True)
                        End If
                    ElseIf ddlprinttype.Text = "Routewise" Then
                        arr.Add(objCommonVar.CurrentCompanyName)
                        arr.Add("Sale Volume Report  (Routewise)")
                        arr.Add("" + head2 + "-Wise")
                        arr.Add("Convertion :" + conversion + "      Report Type:" + head1 + "")
                        arr.Add("Start Date:-" + dtpFromdate.Value + "")
                        arr.Add("End Date:-" + dtptodate.Value + "")
                        If clsCommon.myLen(locationArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(locationArr))
                        End If

                        If clsCommon.myLen(RouteArr) > 0 Then
                            arr.Add("Route Segment : " + clsCommon.GetMulcallString(RouteArr))
                        End If

                        If clsCommon.myLen(CustomerArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(CustomerArr))
                        End If

                        If clsCommon.myLen(CategoryArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(CategoryArr))
                        End If
                        If clsCommon.myLen(ChannelArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(ChannelArr))
                        End If
                        If clsCommon.myLen(TemplateArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(TemplateArr))
                        End If
                        ' clsCommon.MyExportToExcel(str, gv, arr, "SaleVolumeReportRoutewise")

                        If exporter = EnumExportTo.Excel Then
                            clsCommon.MyExportToExcelGrid(str, gv, arr, "SaleVolumeReportRoutewise")

                        Else
                            clsCommon.MyExportToPDF(str, gv, arr, "SaleVolumeReportRoutewise", True)
                        End If
                    ElseIf ddlprinttype.Text = "Hierarchywise" Then
                        arr.Add(objCommonVar.CurrentCompanyName)
                        arr.Add("Sale Volume Report  (Hierarchywise)")
                        arr.Add("" + head2 + "-Wise")
                        arr.Add("Convertion :" + conversion + "      Report Type:" + head1 + "")
                        arr.Add("Start Date:-" + dtpFromdate.Value + "")
                        arr.Add("End Date:-" + dtptodate.Value + "")
                        If clsCommon.myLen(locationArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(locationArr))
                        End If

                        If clsCommon.myLen(RouteArr) > 0 Then
                            arr.Add("Route Segment : " + clsCommon.GetMulcallString(RouteArr))
                        End If

                        If clsCommon.myLen(CustomerArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(CustomerArr))
                        End If

                        If clsCommon.myLen(CategoryArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(CategoryArr))
                        End If
                        If clsCommon.myLen(ChannelArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(ChannelArr))
                        End If
                        If clsCommon.myLen(TemplateArr) > 0 Then
                            arr.Add("Location Segment : " + clsCommon.GetMulcallString(TemplateArr))
                        End If
                        ' clsCommon.MyExportToExcel(str, gv, arr, "SaleVolumeReportHierarchywise")

                        If exporter = EnumExportTo.Excel Then
                            clsCommon.MyExportToExcelGrid(str, gv, arr, "SaleVolumeReportHierarchywise")

                        Else
                            clsCommon.MyExportToPDF(str, gv, arr, "SaleVolumeReportHierarchywise", True)
                        End If
                    End If
                End If

            Else

                If refreshGrid = "F" Then
                    If rdocolumn.IsChecked = True Then

                        If ddlprinttype.Text = "Detail" Then
                            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "salevolumereport", " Sales Volume Report")
                        ElseIf ddlprinttype.Text = "Summary" Then
                            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "salevolumereportColSummary", " Sales Volume Report")
                        ElseIf ddlprinttype.Text = "Routewise" Then
                            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "salevolumereportColRoute", " Sales Volume Report")
                        ElseIf ddlprinttype.Text = "Hierarchywise" Then
                            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "salevolumereportcolHier", " Sales Volume Report")
                        End If


                        'Dim frm As New SalesReportViewer()
                        'frm.funreport(dt, "salevolumereport", " Sales Volume Report")
                    ElseIf rdorow.IsChecked = True Then


                        If ddlprinttype.Text = "Detail" Then
                            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "salevolumeRowise", " Sales Volume Report")
                        ElseIf ddlprinttype.Text = "Summary" Then
                            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "SaleVolumeRowiseRowSummary", " Sales Volume Report")
                        ElseIf ddlprinttype.Text = "Routewise" Then
                            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "SaleVolumeRowiseRowRoute", " Sales Volume Report")
                        ElseIf ddlprinttype.Text = "Hierarchywise" Then
                            frmCrystalReportViewer.funreport(CrystalReportFolder.SalesReport, dt, "SaleVolumeRowiseRowHier", " Sales Volume Report")
                        End If


                        'Dim frm As New SalesReportViewer()
                        'frm.funreport(dt, "salevolumeRowise", " Sales Volume Report")
                    End If
                End If

            End If
            refreshGrid = "F"
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try


    End Sub



    Public Sub gridformat(ByVal arritem As ArrayList)
        Try
            'gv.DataSource = Nothing
            'gv.Rows.Clear()
            'gv.Columns.Clear()
            Dim intType As Integer
            Dim strItemCode As String
            'Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0

            gv.TableElement.TableHeaderHeight = 40
            gv.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
                gv.Columns(ii).IsVisible = False
            Next

            If ddlprinttype.Text = "Detail" Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                If rdbShipping.IsChecked Then
                    gv.Columns("Vehicle_No").IsVisible = True
                    gv.Columns("Vehicle_No").Width = 150
                    gv.Columns("Vehicle_No").HeaderText = "Vehicle"
                Else
                    gv.Columns("Vehicle_No").IsVisible = False
                    gv.Columns("Vehicle_No").Width = 150
                    gv.Columns("Vehicle_No").HeaderText = "Vehicle"
                End If
               

                gv.Columns("Company Name").IsVisible = True
                gv.Columns("Company Name").Width = 150
                gv.Columns("Company Name").HeaderText = "Company Name"

                gv.Columns("Depot").IsVisible = True
                gv.Columns("Depot").Width = 200
                gv.Columns("Depot").HeaderText = "Depot"


                gv.Columns("Name").IsVisible = True
                gv.Columns("Name").Width = 150
                gv.Columns("Name").HeaderText = "Name"

                gv.Columns("Route").IsVisible = True
                gv.Columns("Route").Width = 50
                gv.Columns("Route").HeaderText = "Route"

                gv.Columns("Route Desc").IsVisible = True
                gv.Columns("Route Desc").Width = 200
                gv.Columns("Route Desc").HeaderText = "Route Desc"

                gv.Columns("Invoice").IsVisible = True
                gv.Columns("Invoice").Width = 70
                gv.Columns("Invoice").HeaderText = "Invoice"


                gv.Columns("Invoice Date").IsVisible = True
                gv.Columns("Invoice Date").Width = 100
                gv.Columns("Invoice Date").FormatString = "{0:d}"
                gv.Columns("Invoice Date").HeaderText = "Invoice Date"

                gv.Columns("Customer").IsVisible = True
                gv.Columns("Customer").Width = 100
                gv.Columns("Customer").HeaderText = "Customer"

                gv.Columns("Customer Desc").IsVisible = True
                gv.Columns("Customer Desc").Width = 100
                gv.Columns("Customer Desc").HeaderText = "Customer Desc"

                gv.Columns("Total").IsVisible = True
                gv.Columns("Total").Width = 70
                gv.Columns("Total").HeaderText = "Total"


                If rdbSale.IsChecked = True Then
                    For ii As Integer = 10 To gv.Columns.Count - 1
                        intCount = intCount + 1
                        strItemCode = gv.Columns(ii).FieldName
                        Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item16)
                    Next
                Else
                    For ii As Integer = 10 To gv.Columns.Count - 1
                        intCount = intCount + 1
                        strItemCode = gv.Columns(ii).FieldName
                        Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item16)
                    Next
                End If
                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                'For j As Integer = 0 To arritem.Count - 1
                '    gv.Columns(arritem.Item(j)).IsVisible = True
                '    gv.Columns(arritem.Item(j)).Width = 100
                '    gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

                'Next

            ElseIf ddlprinttype.Text = "Summary" Then
                If rdbShipping.IsChecked Then
                    gv.Columns("Vehicle_No").IsVisible = True
                    gv.Columns("Vehicle_No").Width = 150
                    gv.Columns("Vehicle_No").HeaderText = "Vehicle"

                    intType = 7
                Else
                    intType = 6
                End If

                gv.Columns("Company Name").IsVisible = True
                gv.Columns("Company Name").Width = 150
                gv.Columns("Company Name").HeaderText = "Company Name"


                gv.Columns("Name").IsVisible = True
                gv.Columns("Name").Width = 200
                gv.Columns("Name").HeaderText = "Name"

                gv.Columns("Route").IsVisible = True
                gv.Columns("Route").Width = 50
                gv.Columns("Route").HeaderText = "Route"

                gv.Columns("Route Desc").IsVisible = True
                gv.Columns("Route Desc").Width = 200
                gv.Columns("Route Desc").HeaderText = "Route Desc"


                gv.Columns("Customer").IsVisible = True
                gv.Columns("Customer").Width = 100
                gv.Columns("Customer").HeaderText = "Customer"

                gv.Columns("Customer Desc").IsVisible = True
                gv.Columns("Customer Desc").Width = 200
                gv.Columns("Customer Desc").HeaderText = "Customer Desc"

                gv.Columns("Total").IsVisible = True
                gv.Columns("Total").Width = 70
                gv.Columns("Total").HeaderText = "Total"

               
                gv.GroupDescriptors.Add(New GridGroupByExpression("Route  as Route  format ""{0}: {1}"" Group By Route"))
                gv.GroupDescriptors.Add(New GridGroupByExpression("[Route Desc]  as RouteDesc  format ""{0}: {1}"" Group By [Route Desc]"))

                gv.ShowGroupPanel = False
                gv.MasterTemplate.AutoExpandGroups = True

                Dim summaryRowItem As New GridViewSummaryRowItem()

                For ii As Integer = intType To gv.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gv.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                

                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            ElseIf ddlprinttype.Text = "Routewise" Then


                Dim summaryRowItem As New GridViewSummaryRowItem()
                gv.Columns("Company Name").IsVisible = True
                gv.Columns("Company Name").Width = 150
                gv.Columns("Company Name").HeaderText = "Company Name"


                gv.Columns("Route").IsVisible = True
                gv.Columns("Route").Width = 50
                gv.Columns("Route").HeaderText = "Route"

                gv.Columns("Route Desc").IsVisible = True
                gv.Columns("Route Desc").Width = 200
                gv.Columns("Route Desc").HeaderText = "Route Desc"

                gv.Columns("Total").IsVisible = True
                gv.Columns("Total").Width = 70
                gv.Columns("Total").HeaderText = "Total"


                For ii As Integer = 3 To gv.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gv.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

            ElseIf ddlprinttype.Text = "Hierarchywise" Then
                gv.Columns("Name").IsVisible = True
                gv.Columns("Name").Width = 150
                gv.Columns("Name").HeaderText = "Name"

                gv.Columns("Total").IsVisible = True
                gv.Columns("Total").Width = 70
                gv.Columns("Total").HeaderText = "Total"


                Dim summaryRowItem As New GridViewSummaryRowItem()

                For ii As Integer = 1 To gv.Columns.Count - 1
                    intCount = intCount + 1
                    strItemCode = gv.Columns(ii).FieldName
                    Dim item16 As New GridViewSummaryItem("" & strItemCode & "", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item16)
                Next
                gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)


            End If

            For j As Integer = 0 To arritem.Count - 1
                gv.Columns(arritem.Item(j)).IsVisible = True
                gv.Columns(arritem.Item(j)).Width = 100
                gv.Columns(arritem.Item(j)).HeaderText = arritem.Item(j)

            Next
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
        ddlcategory.Text = "HOS"
        ddltype.Text = "SKU"
        ddlconversion.Text = "RAW"
        ddlprinttype.Text = "Detail"
        SetDataBaseGrid()
        LoadCustomer()
        LoadLocation()
        LoadRoute()
        LoadCustomerCategory()
        LoadTemplate()
        Loadchannel()
        chkTransfer.Checked = False
        chkIC.Checked = False
        'rbtnAllCompany.IsChecked = True
        chkLocAll.IsChecked = True
        chkAllRoute.IsChecked = True
        chkchannellall.IsChecked = True
        rdbSale.IsChecked = True

        chkcategoryall.IsChecked = True
        rbtnSelectCompany.IsChecked = True
        For ii As Integer = 0 To gvDB.Rows.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvDB.Rows(ii).Cells(colCompCode).Value), objCommonVar.CurrentCompanyCode) = CompairStringResult.Equal Then
                gvDB.Rows(ii).Cells(colSelect).Value = 1
            End If
        Next
        ddlvisi.Text = "Both"
        chktempall.IsChecked = True
        chkallcustomer.IsChecked = True
        rdocolumn.IsChecked = True
        rdoposted.IsChecked = True
        'cboCustomerClass.Text = "All"
        RadPageView1.SelectedPage = Filter

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


    Private Sub chktempall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chktempall.ToggleStateChanged
        cgvtemplate.Enabled = Not chktempall.IsChecked
    End Sub

    Private Sub chkAllRoute_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkAllRoute.ToggleStateChanged
        cbgroute.Enabled = Not chkAllRoute.IsChecked
    End Sub

   

    Private Sub chkchannellall_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkchannellall.ToggleStateChanged
        cbgchannel.Enabled = Not chkchannellall.IsChecked
    End Sub

    Private Sub rdocolumn_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdocolumn.ToggleStateChanged
        If rdocolumn.IsChecked = True Then
            lblType.Visible = True
            ddltype.Visible = True
        End If
    End Sub

    Private Sub rdorow_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdorow.ToggleStateChanged
        If rdorow.IsChecked = True Then
            lblType.Visible = False
            ddltype.Visible = False
            ddltype.Text = "SKU"
        End If
    End Sub

    Private Sub gv_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)

    End Sub

    Private Sub gv_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs)
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    
    
    Private Sub btnrefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnrefresh.Click
        refreshGrid = "T"
        print(False, 2)

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        print(True, EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        print(True, EnumExportTo.PDF)
    End Sub
End Class


