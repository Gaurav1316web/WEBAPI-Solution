''created on 30/09/2016
'' Work Done agaist ticket no. BHA/05/12/18-000742
Imports common
Imports Telerik
Imports Telerik.WinControls
Imports Telerik.WinControls.UI
Imports System.IO

Public Class RptCustomerEffective_ItemRate
    Inherits FrmMainTranScreen

#Region "variables"
    Dim ButtonToolTip As New ToolTip()
#End Region

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptCustomerEffective_ItemRate)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        btnExport.Visible = MyBase.isExport
    End Sub

    Private Sub RptCustomerEffective_ItemRate_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.C Then
            btnClose.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.G Then
            btnGo.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            BtnReset.PerformClick()
        End If
    End Sub
    'done by stuti on 06/10/2016 against ticket no BM00000009951
    Private Sub RptCustomerEffective_ItemRate_Load(sender As Object, e As EventArgs) Handles Me.Load
        SetUserMgmtNew()
        FunReset()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C for close window.")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+G for refresh record.")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N for reset window.")
    End Sub

    Private Sub RadMenuItem6_Click(sender As Object, e As EventArgs) Handles RadMenuItem6.Click
        ''export
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()

            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCustomerEffective_ItemRate & "'"))

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
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        FunReset()
    End Sub

    Private Sub FunReset()
        txtFromDate.Text = clsCommon.GETSERVERDATE()
        txtToDate.Text = clsCommon.GETSERVERDATE()
        txtTotalRow.Text = "Total Rows: 0"
        ChkExcisablePrice.Checked = False
        chkStockingUOM.Checked = False
        chkPriceCodeWise.Checked = False
        gv.DataSource = Nothing
        gv.Rows.Clear()
        gv.Columns.Clear()

        txtCustomerMult.arrDispalyMember = Nothing
        txtCustomerMult.arrValueMember = Nothing
        txtCustomerGroup.arrDispalyMember = Nothing
        txtCustomerGroup.arrValueMember = Nothing
        TxtMultiItem.arrValueMember = Nothing
        TxtMultiItem.arrDispalyMember = Nothing
        TxtMultiProductGroup.arrDispalyMember = Nothing
        TxtMultiProductGroup.arrValueMember = Nothing
        TxtMultiLocation.arrDispalyMember = Nothing
        TxtMultiLocation.arrValueMember = Nothing
        TxtMultiPriceCode.arrValueMember = Nothing
        TxtMultiPriceCode.arrDispalyMember = Nothing

        RadPageView1.SelectedPage = RadPageViewPage1
        RbtnLatest.Checked = True
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Dim dt As New DataTable()
        Dim qry As String = Nothing
        Try
            'If txtFromDate.Text Is Nothing OrElse clsCommon.myLen(txtFromDate.Text) <= 0 OrElse Not IsDate(txtFromDate.Text) Then
            '    txtFromDate.Focus()
            '    txtFromDate.Select()
            '    Throw New Exception("Please select As on date.")
            'End If

            'If txtToDate.Text Is Nothing OrElse clsCommon.myLen(txtToDate.Text) <= 0 OrElse Not IsDate(txtToDate.Text) Then
            '    txtToDate.Focus()
            '    txtToDate.Select()
            '    Throw New Exception("Please select To date.")
            'End If

            'If clsCommon.myCDate(txtToDate.Text) < clsCommon.myCDate(txtFromDate.Text) Then
            '    txtToDate.Focus()
            '    txtToDate.Select()
            '    Throw New Exception("To date must be greater than From date.")
            'End If
            ' Ticket No : BHA/12/10/18-000620 By prabhakar -- for Add column Modified By , Modified On , Item cost and diffrence 
            Dim xPriceCode As String = "Price_CodeNon"
            If ChkExcisablePrice.Checked Then
                xPriceCode = "Price_Code"
            End If

            qry = "Select "
            If chkPriceCodeWise.Checked = False Then
                qry += " xx.Code ,xx.Name,"
            End If
            qry += " xx.Type ,xx.[Price Code] ,xx.[Price Name] ,xx.LocationCode ,xx.LocationName ,xx.GroupName ,xx.[Product Code] ,xx.[Product Name],max(xx.[Product Unit] ) as [Product Unit],convert(decimal(18,2),max(xx.Rate)) as Rate"
            'If chkPriceCodeWise.Checked = True Then
            qry += ",convert(decimal(18,2),max(yy.[Rate])) as [Previous Rate],convert(decimal(18,2),isnull(max(xx.Rate),0)-isnull(max(yy.[Rate]),0)) as [Rate Diff]"
            'End If

            qry += ",max (xx.Item_Cost) as [Item Cost] , convert(decimal(18,2),max(xx.Rate) - max (xx.Item_Cost)) as [Difference],convert(varchar,max(xx.RateWef ),103) as RateWef,convert(decimal(18,2),max(xx.Discount )) as Discount,max(xx.[Discount Type] ) as [Discount Type] ,convert(varchar,max(xx.DissWef ),103) as DissWef,convert(decimal(18,2),max(xx.NetRate )) as NetRate, max( xx.[Modified By]) as [Modified By] ,max(xx.[Modified On]) as [Modified On],max(TSPL_ITEM_PRICE_PLAN_DETAIL.Plan_Code ) as [Price Plan Code]  from ( "
            qry += " select final.Item_Price_ID as Item_Price_ID,final.[Code],(final.[Name]) as [Name],(final.[Type]) as [Type],final.[Price Code],(final.[Price Name]) as [Price Name],final.[LocationCode],(final.[LocationName]) as [LocationName],(final.[GroupName]) as [GroupName],final.[Product Code],(final.[Product Name]) as [Product Name],(final.[Product Unit]) as [Product Unit] " & _
                ",final.[Rate],Final.Item_Cost,(final.[RateWef]) as [RateWef],final.[Discount],(final.[Discount Type]) as [Discount Type],(final.DissWef) as DissWef,( round(case when final.[Discount Type]='By Value' then ISNULL(final.rate,0)-isnull(final.Discount,0) else (ISNULL(final.rate,0) - ((ISNULL(final.rate,0)*isnull(final.Discount,0))/100)) end,2)) as [NetRate], final.[Modified By],final.[Modified On],final.Against_Plan_TR_Code from ( "
            qry += " select row_number() over( partition by TSPL_ITEM_PRICE_MASTER.price_code,TSPL_ITEM_PRICE_MASTER.location_code"
            If chkPriceCodeWise.Checked = False Then
                qry += ",tspl_customer_master.Cust_Code "
            End If
            qry += ",TSPL_ITEM_PRICE_MASTER.item_code --,(convert(date,TSPL_ITEM_PRICE_MASTER.[Start_Date],103)) " & Environment.NewLine
            qry += "order by TSPL_ITEM_PRICE_MASTER.price_code,TSPL_ITEM_PRICE_MASTER.location_code"
            If chkPriceCodeWise.Checked = False Then
                qry += ",tspl_customer_master.Cust_Code "
            End If
            qry += ",TSPL_ITEM_PRICE_MASTER.item_code,(convert(date,TSPL_ITEM_PRICE_MASTER.[Start_Date],103)) desc) as sno," & _
                   "  TSPL_ITEM_PRICE_MASTER.against_Plan_Tr_Code, TSPL_ITEM_PRICE_MASTER.Item_Price_ID, " & _
                     " tspl_customer_master.Cust_Code as [Code],(tspl_customer_master.Customer_Name) as [Name],(case when tspl_customer_master.IsDistributor='Y' then 'Distributor' else '' end) as [Type],TSPL_ITEM_PRICE_MASTER.price_code as [Price Code],(select top 1 (TSPL_PRICE_COMPONENT_MAPPING.price_code_desc) as price_code_desc from TSPL_PRICE_COMPONENT_MAPPING where TSPL_PRICE_COMPONENT_MAPPING.price_code=TSPL_ITEM_PRICE_MASTER.price_code) as [Price Name],TSPL_ITEM_PRICE_MASTER.location_code as [LocationCode],(tspl_location_master.location_desc) as [LocationName] " & _
                            " ,(TSPL_ITEM_MASTER.CSA_TYPE) as [GroupName],TSPL_ITEM_PRICE_MASTER.Item_Code as [Product Code],(TSPL_ITEM_MASTER.Item_Desc) as [Product Name]"
            If RbtnLatest.Checked = True Then
                qry += " ,TSPL_ITEM_UOM_Detail.UOM_Code as [Product Unit]"
            Else
                qry += " ,TSPL_ITEM_PRICE_MASTER.UOM as  [Product Unit] "
            End If


            qry += " ,(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price) as [Rate],TSPL_ITEM_UOM_DETAIL.Item_Cost , convert(date,TSPL_ITEM_PRICE_MASTER.[Start_Date],103) as [RateWef], "

            qry += " ( round(case when isnull(Scheme.CashDisc_Amount,0)<>0 then isnull(Scheme.CashDisc_Amount,0) else ((ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)*isnull(Scheme.CasdDisc_Percentage,0))/100) end,2) ) as [Discount],( case when isnull(Scheme.CasdDisc_Percentage,0)<>0 then 'By Percentage' when isnull(Scheme.CashDisc_Amount,0)<>0 then 'By Value' else 'NA' end) as [Discount Type], " & _
                            " Scheme.effct_date as DissWef ,( round(case when isnull(Scheme.CashDisc_Amount,0)>0 then ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)-   isnull(Scheme.CashDisc_Amount,0) else ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)- ((ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)*isnull(Scheme.CasdDisc_Percentage,0))/100) end,2)) as [NetRate], "
            qry += " tspl_User_Master_Modified_Name.User_Name as [Modified By], convert (varchar,TSPL_ITEM_PRICE_MASTER.Modify_Date ,103) as [Modified On]  from TSPL_ITEM_PRICE_MASTER inner join TSPL_CUSTOMER_MASTER on TSPL_ITEM_PRICE_MASTER.Price_Code=tspl_customer_master." + xPriceCode + " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code " & _
                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code "
            If chkStockingUOM.Checked = True Then
                qry += "  and Stocking_Unit='Y' "
            ElseIf RbtnLatest.Checked = True Then
                qry += "  and Default_UOM=1 "
            End If
            qry += " and TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            " (select case when len(isnull(TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date,''))<=0 then TSPL_SCHEME_MASTER_NEW.Start_Date else TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date end as effct_date,TSPL_SCHEME_BENEFICIARY.Cust_Code,TSPL_SCHEME_DETAIL_NEW.MainItem_Code as Item_Code,TSPL_SCHEME_DETAIL_NEW.MainUnit_Code as Unit_Code,TSPL_SCHEME_DETAIL_NEW.CashDisc_Amount,TSPL_SCHEME_DETAIL_NEW.CasdDisc_Percentage from TSPL_SCHEME_MASTER_NEW " & _
            " left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_BENEFICIARY.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_MASTER_NEW.Scheme_Code=TSPL_SCHEME_DETAIL_NEW.Scheme_Code where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Discount' )Scheme on Scheme.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code and TSPL_ITEM_MASTER.Item_Code=Scheme.Item_Code and TSPL_ITEM_PRICE_MASTER.UOM= case when len(isnull(Scheme.Unit_Code,''))<=0 then tspl_item_master.unit_code else Scheme.Unit_Code end " & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.location_code=TSPL_ITEM_PRICE_MASTER.Location_Code " & _
            " left outer join tspl_customer_location_mapping on tspl_customer_location_mapping.customer_code=tspl_customer_master.Cust_Code and tspl_customer_location_mapping.Location_Code=TSPL_ITEM_PRICE_MASTER.Location_code" & _
                " left outer Join tspl_User_Master as tspl_User_Master_Modified_Name  on tspl_User_Master_Modified_Name.User_Code = TSPL_ITEM_PRICE_MASTER.Modify_By " & _
            " where 1=1  and  TSPL_ITEM_PRICE_MASTER.Is_Active=1 and TSPL_item_master.Active=1 " & _
            " and  convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date ,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103)  and convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date ,103)<=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103)  " ''and convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' ,103)

            ''where TSPL_ITEM_PRICE_MASTER.UOM= (case when len(isnull(Scheme.Unit_Code,''))<=0 then tspl_item_master.unit_code else Scheme.Unit_Code end)

            Dim count As Integer = 0
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_PRICE_MASTER.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") and ( "
                For Each str1 As String In TxtMultiLocation.arrValueMember
                    count = count + 1
                    If count = 1 Then
                        qry += " (case when TSPL_ITEM_PRICE_MASTER.[Type]='T' then 1 else tspl_customer_location_mapping.location_code end) in (case when TSPL_ITEM_PRICE_MASTER.[Type]='T' then 1 else '" + str1 + "' end) " + Environment.NewLine
                    Else
                        qry += " OR (case when TSPL_ITEM_PRICE_MASTER.[Type]='T' then 1 else tspl_customer_location_mapping.location_code end) in (case when TSPL_ITEM_PRICE_MASTER.[Type]='T' then 1 else '" + str1 + "' end) " + Environment.NewLine
                    End If
                Next
                qry += " ) "
            End If

            If TxtMultiPriceCode.arrValueMember IsNot Nothing AndAlso TxtMultiPriceCode.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_PRICE_MASTER.price_code in (" + clsCommon.GetMulcallString(TxtMultiPriceCode.arrValueMember) + ") " + Environment.NewLine
            End If

            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                qry += " and tspl_customer_master.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ") " + Environment.NewLine
            End If

            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                qry += " and tspl_customer_master.cust_code in (" + clsCommon.GetMulcallString(txtCustomerMult.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                qry += " and tspl_item_master.item_code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiProductGroup.arrValueMember IsNot Nothing AndAlso TxtMultiProductGroup.arrValueMember.Count > 0 Then
                qry += " and tspl_item_master.CSA_TYPE in (" + clsCommon.GetMulcallString(TxtMultiProductGroup.arrValueMember) + ") " + Environment.NewLine
            End If

            If chkStockingUOM.Checked = True Then
                qry += "  and Stocking_Unit='Y' "
            ElseIf RbtnLatest.Checked = True Then
                qry += "  and Default_UOM=1 "
            End If

            qry += " )final  "
            If chkStockingUOM.Checked = True OrElse RbtnLatest.Checked = True Then
                qry += " where final.sno=1  "
            End If
            ''group by final.[Code],final.[Price Code],final.[LocationCode],final.[Product Code]
            qry += " ) as xx left outer join TSPL_ITEM_PRICE_PLAN_DETAIL on  TSPL_ITEM_PRICE_PLAN_DETAIL.plan_tr_code=xx.Against_Plan_TR_Code "

            ''''''''''''''''''''''''''''
            'If RbtnLatest.Checked = True Then
            'If chkPriceCodeWise.Checked = True Then
            qry += " LEFT outer JOIN (select final2.Item_Price_ID as Item_Price_ID,final2.[Code],(final2.[Name]) as [Name],(final2.[Type]) as [Type],final2.[Price Code],(final2.[Price Name]) as [Price Name],final2.[LocationCode],(final2.[LocationName]) as [LocationName],(final2.[GroupName]) as [GroupName],final2.[Product Code],(final2.[Product Name]) as [Product Name],(final2.[Product Unit]) as [Product Unit] " & _
            ",final2.[Rate],final2.Item_Cost,(final2.[RateWef]) as [RateWef],final2.[Discount],(final2.[Discount Type]) as [Discount Type],(final2.DissWef) as DissWef,( round(case when final2.[Discount Type]='By Value' then ISNULL(final2.rate,0)-isnull(final2.Discount,0) else (ISNULL(final2.rate,0) - ((ISNULL(final2.rate,0)*isnull(final2.Discount,0))/100)) end,2)) as [NetRate], final2.[Modified By],final2.[Modified On],final2.Against_Plan_TR_Code from ( "
            qry += " select row_number() over(partition by TSPL_ITEM_PRICE_MASTER.price_code,TSPL_ITEM_PRICE_MASTER.location_code"
            If chkPriceCodeWise.Checked = False Then
                qry += " ,tspl_customer_master.Cust_Code "
            End If
            qry += ",TSPL_ITEM_PRICE_MASTER.item_code --,(convert(date,TSPL_ITEM_PRICE_MASTER.[Start_Date],103)) " & Environment.NewLine
            qry += " order by TSPL_ITEM_PRICE_MASTER.price_code,TSPL_ITEM_PRICE_MASTER.location_code"
            If chkPriceCodeWise.Checked = False Then
                qry += ",tspl_customer_master.Cust_Code "
            End If
            qry += ",TSPL_ITEM_PRICE_MASTER.item_code,(convert(date,TSPL_ITEM_PRICE_MASTER.[Start_Date],103)) desc) as sno," & _
                   "  TSPL_ITEM_PRICE_MASTER.against_Plan_Tr_Code, TSPL_ITEM_PRICE_MASTER.Item_Price_ID, " & _
                     " tspl_customer_master.Cust_Code as [Code],(tspl_customer_master.Customer_Name) as [Name],(case when tspl_customer_master.IsDistributor='Y' then 'Distributor' else '' end) as [Type],TSPL_ITEM_PRICE_MASTER.price_code as [Price Code],(select top 1 (TSPL_PRICE_COMPONENT_MAPPING.price_code_desc) as price_code_desc from TSPL_PRICE_COMPONENT_MAPPING where TSPL_PRICE_COMPONENT_MAPPING.price_code=TSPL_ITEM_PRICE_MASTER.price_code) as [Price Name],TSPL_ITEM_PRICE_MASTER.location_code as [LocationCode],(tspl_location_master.location_desc) as [LocationName] " & _
                            " ,(TSPL_ITEM_MASTER.CSA_TYPE) as [GroupName],TSPL_ITEM_PRICE_MASTER.Item_Code as [Product Code],(TSPL_ITEM_MASTER.Item_Desc) as [Product Name]"
            If RbtnLatest.Checked = True Then
                qry += " ,TSPL_ITEM_UOM_Detail.UOM_Code as [Product Unit]"
            Else
                qry += " ,TSPL_ITEM_PRICE_MASTER.UOM as  [Product Unit] "
            End If


            qry += " ,(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price) as [Rate],TSPL_ITEM_UOM_DETAIL.Item_Cost , convert(date,TSPL_ITEM_PRICE_MASTER.[Start_Date],103) as [RateWef], "

            qry += " ( round(case when isnull(Scheme.CashDisc_Amount,0)<>0 then isnull(Scheme.CashDisc_Amount,0) else ((ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)*isnull(Scheme.CasdDisc_Percentage,0))/100) end,2) ) as [Discount],( case when isnull(Scheme.CasdDisc_Percentage,0)<>0 then 'By Percentage' when isnull(Scheme.CashDisc_Amount,0)<>0 then 'By Value' else 'NA' end) as [Discount Type], " & _
                            " Scheme.effct_date as DissWef ,( round(case when isnull(Scheme.CashDisc_Amount,0)>0 then ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)-   isnull(Scheme.CashDisc_Amount,0) else ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)- ((ISNULL(TSPL_ITEM_PRICE_MASTER.Item_Selling_Price,0)*isnull(Scheme.CasdDisc_Percentage,0))/100) end,2)) as [NetRate], "
            qry += " tspl_User_Master_Modified_Name.User_Name as [Modified By], convert (varchar,TSPL_ITEM_PRICE_MASTER.Modify_Date ,103) as [Modified On]  from TSPL_ITEM_PRICE_MASTER inner join TSPL_CUSTOMER_MASTER on TSPL_ITEM_PRICE_MASTER.Price_Code=tspl_customer_master." + xPriceCode + " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_PRICE_MASTER.Item_Code " & _
                " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code "
            If chkStockingUOM.Checked = True Then
                qry += "  and Stocking_Unit='Y' "
            ElseIf RbtnLatest.Checked = True Then
                qry += "  and Default_UOM=1 "
            End If
            qry += " and TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code left outer join " & _
            " (select case when len(isnull(TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date,''))<=0 then TSPL_SCHEME_MASTER_NEW.Start_Date else TSPL_SCHEME_MASTER_NEW.MaxlimitStart_Date end as effct_date,TSPL_SCHEME_BENEFICIARY.Cust_Code,TSPL_SCHEME_DETAIL_NEW.MainItem_Code as Item_Code,TSPL_SCHEME_DETAIL_NEW.MainUnit_Code as Unit_Code,TSPL_SCHEME_DETAIL_NEW.CashDisc_Amount,TSPL_SCHEME_DETAIL_NEW.CasdDisc_Percentage from TSPL_SCHEME_MASTER_NEW " & _
            " left outer join TSPL_SCHEME_BENEFICIARY on TSPL_SCHEME_BENEFICIARY.Scheme_Code=TSPL_SCHEME_MASTER_NEW.Scheme_Code left outer join TSPL_SCHEME_DETAIL_NEW on TSPL_SCHEME_MASTER_NEW.Scheme_Code=TSPL_SCHEME_DETAIL_NEW.Scheme_Code where TSPL_SCHEME_MASTER_NEW.Scheme_Type='Discount' )Scheme on Scheme.Cust_Code=TSPL_CUSTOMER_MASTER.Cust_Code and TSPL_ITEM_MASTER.Item_Code=Scheme.Item_Code and TSPL_ITEM_PRICE_MASTER.UOM= case when len(isnull(Scheme.Unit_Code,''))<=0 then tspl_item_master.unit_code else Scheme.Unit_Code end " & _
            " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.location_code=TSPL_ITEM_PRICE_MASTER.Location_Code " & _
            " left outer join tspl_customer_location_mapping on tspl_customer_location_mapping.customer_code=tspl_customer_master.Cust_Code and tspl_customer_location_mapping.Location_Code=TSPL_ITEM_PRICE_MASTER.Location_code" & _
                " left outer Join tspl_User_Master as tspl_User_Master_Modified_Name  on tspl_User_Master_Modified_Name.User_Code = TSPL_ITEM_PRICE_MASTER.Modify_By " & _
            " where 1=1  and  TSPL_ITEM_PRICE_MASTER.Is_Active=1 and TSPL_item_master.Active=1 " & _
            " and  convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date ,103)<convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103)  " ''and convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' ,103)
            '" and  convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date ,103)>=convert(date,'" + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MMM/yyyy") + "',103)  and convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date ,103)<=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "',103)  " ''and convert(date,TSPL_ITEM_PRICE_MASTER.Start_Date,103) <=convert(date,'" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' ,103)
            ''where TSPL_ITEM_PRICE_MASTER.UOM= (case when len(isnull(Scheme.Unit_Code,''))<=0 then tspl_item_master.unit_code else Scheme.Unit_Code end)

            Dim count1 As Integer = 0
            If TxtMultiLocation.arrValueMember IsNot Nothing AndAlso TxtMultiLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_PRICE_MASTER.Location_Code in (" + clsCommon.GetMulcallString(TxtMultiLocation.arrValueMember) + ") and ( "
                For Each str1 As String In TxtMultiLocation.arrValueMember
                    count1 = count1 + 1
                    If count1 = 1 Then
                        qry += " (case when TSPL_ITEM_PRICE_MASTER.[Type]='T' then 1 else tspl_customer_location_mapping.location_code end) in (case when TSPL_ITEM_PRICE_MASTER.[Type]='T' then 1 else '" + str1 + "' end) " + Environment.NewLine
                    Else
                        qry += " OR (case when TSPL_ITEM_PRICE_MASTER.[Type]='T' then 1 else tspl_customer_location_mapping.location_code end) in (case when TSPL_ITEM_PRICE_MASTER.[Type]='T' then 1 else '" + str1 + "' end) " + Environment.NewLine
                    End If
                Next
                qry += " ) "
            End If

            If TxtMultiPriceCode.arrValueMember IsNot Nothing AndAlso TxtMultiPriceCode.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_PRICE_MASTER.price_code in (" + clsCommon.GetMulcallString(TxtMultiPriceCode.arrValueMember) + ") " + Environment.NewLine
            End If

            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                qry += " and tspl_customer_master.Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ") " + Environment.NewLine
            End If

            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                qry += " and tspl_customer_master.cust_code in (" + clsCommon.GetMulcallString(txtCustomerMult.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiItem.arrValueMember IsNot Nothing AndAlso TxtMultiItem.arrValueMember.Count > 0 Then
                qry += " and tspl_item_master.item_code in (" + clsCommon.GetMulcallString(TxtMultiItem.arrValueMember) + ") " + Environment.NewLine
            End If
            If TxtMultiProductGroup.arrValueMember IsNot Nothing AndAlso TxtMultiProductGroup.arrValueMember.Count > 0 Then
                qry += " and tspl_item_master.CSA_TYPE in (" + clsCommon.GetMulcallString(TxtMultiProductGroup.arrValueMember) + ") " + Environment.NewLine
            End If

            If chkStockingUOM.Checked = True Then
                qry += "  and Stocking_Unit='Y' "
            ElseIf RbtnLatest.Checked = True Then
                qry += "  and Default_UOM=1 "
            End If

            qry += " )final2 "

            If chkStockingUOM.Checked = True OrElse RbtnLatest.Checked = True Then
                qry += " where final2.sno=1  "
            End If

            qry += ") as yy ON xx.Item_Price_ID =yy.Item_Price_ID and xx.Type=yy.Type and xx.[Price Code] =yy.[Price Code] and xx.LocationCode=yy.LocationCode and xx.[Product Code]=yy.[Product Code] and xx.[Product unit]=yy.[Product unit]    "
            'End If
            ''''''''''''''''

            qry += " group by xx.Type ,xx.[Price Code] ,xx.[Price Name] ,xx.LocationCode ,xx.LocationName ,xx.GroupName ,xx.[Product Code] ,xx.[Product Name],xx.[Product unit]  "
            If chkPriceCodeWise.Checked = False Then
                qry += ",xx.Code ,xx.Name  "
            End If
            If RbtnLatest.Checked = False Then
                qry += " ,xx.Rate "
            End If

            dt = New DataTable()
            dt = clsDBFuncationality.GetDataTable(qry)

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            txtTotalRow.Text = "Total Rows: 0"

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                gv.DataSource = dt
                gv.ReadOnly = True

                gv.MasterTemplate.ShowRowHeaderColumn = False
                For ii As Integer = 0 To gv.Columns.Count - 1
                    gv.Columns(ii).Width = 100
                Next

                txtTotalRow.Text = "Total Rows: " + clsCommon.myCstr(gv.MasterView.Rows.Count())

                gv.AllowAddNewRow = False
                gv.ShowGroupPanel = False
                gv.AllowColumnReorder = True
                gv.AllowRowReorder = False
                gv.EnableSorting = True
                gv.EnableFiltering = True
                gv.ShowFilteringRow = True
                gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
                gv.MasterTemplate.ShowRowHeaderColumn = False
                gv.TableElement.TableHeaderHeight = 40
                gv.AllowDeleteRow = False

                ReStoreGridLayout()
            Else
                Throw New Exception("No record found.")
            End If
        Catch ex As Exception
            txtTotalRow.Text = "Total Rows: 0"
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.close()
        gc.collect()
    End Sub

    Private Sub RadMenuItem7_Click(sender As Object, e As EventArgs) Handles RadMenuItem7.Click
        ''save layout
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub RadMenuItem8_Click(sender As Object, e As EventArgs) Handles RadMenuItem8.Click
        ''delete layout
        If clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode) Then
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
        End If
    End Sub

    Private Sub txtCustomerMult__My_Click(sender As Object, e As EventArgs) Handles txtCustomerMult._My_Click
        Dim qry As String = Nothing
        If txtCustomerGroup.arrValueMember IsNot Nothing Then
            qry = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master where Cust_Group_Code in (" + clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember) + ")   order by Cust_Code"
            txtCustomerMult.arrValueMember = clsCommon.ShowMultipleSelectForm("CustEFVTFND", qry, "Code", "Name", txtCustomerMult.arrValueMember, txtCustomerMult.arrDispalyMember)
        Else
            qry = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master order by Cust_Code"
            txtCustomerMult.arrValueMember = clsCommon.ShowMultipleSelectForm("CustEFVTFND", qry, "Code", "Name", txtCustomerMult.arrValueMember, txtCustomerMult.arrDispalyMember)
        End If
    End Sub

    Private Sub TxtMultiItem__My_Click(sender As Object, e As EventArgs) Handles TxtMultiItem._My_Click
        Dim qry As String = "Select Item_Code as Code,Item_Desc as Name from TSPL_ITEM_MASTER "
        TxtMultiItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemEFCTLST", qry, "Code", "Name", TxtMultiItem.arrValueMember, TxtMultiItem.arrDispalyMember)
    End Sub

    Private Sub TxtMultiProductGroup__My_Click(sender As Object, e As EventArgs) Handles TxtMultiProductGroup._My_Click
        Dim qry As String = " select distinct code,Name  from( select 'None' as Code,'None' as Name union all select 'CPD-DESI GHEE' as Code,'CPD-DESI GHEE' as Name union all select 'BULK -DESI GHEE' as Code,'BULK-DESI GHEE' as Name union all select 'CPD-OTHER' as Code,'CPD-OTHER' as Name union all select 'BULK-OTHER' as Code,'BULK-OTHER' as Name union all select distinct CSA_TYPE as Code,CSA_TYPE as Name  from TSPL_ITEM_MASTER ) xx"
        TxtMultiProductGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("GroupEFCTFND", qry, "Code", "Name", TxtMultiProductGroup.arrValueMember, TxtMultiProductGroup.arrDispalyMember)
    End Sub

    Private Sub gv_FilterChanged(sender As Object, e As GridViewCollectionChangedEventArgs) Handles gv.FilterChanged
        If gv.Rows.Count > 0 Then
            txtTotalRow.Text = "Total Rows: " + clsCommon.myCstr(gv.MasterView.Rows.Count())
        End If
    End Sub

    Private Sub btnPDF_Click(sender As Object, e As EventArgs) Handles btnPDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

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
            transportSql.applyExportTemplate(gv, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Effective Rate Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub TxtMultiLocation__My_Click(sender As Object, e As EventArgs) Handles TxtMultiLocation._My_Click
        Dim qry As String = "select Location_Code as [Code] ,Location_Desc as [Name] from TSPL_LOCATION_MASTER where Location_Type ='Physical'"
        TxtMultiLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LOCFND", qry, "Code", "Name", TxtMultiLocation.arrValueMember, TxtMultiLocation.arrDispalyMember)
    End Sub

    Private Sub TxtMultiPriceCode__My_Click(sender As Object, e As EventArgs) Handles TxtMultiPriceCode._My_Click
        Dim qry As String = "select distinct Price_Code as Code,price_code_desc as Name from TSPL_PRICE_COMPONENT_MAPPING"
        TxtMultiPriceCode.arrValueMember = clsCommon.ShowMultipleSelectForm("PriceFND", qry, "Code", "Name", TxtMultiPriceCode.arrValueMember, TxtMultiPriceCode.arrDispalyMember)
    End Sub

  
    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        Dim strQry As String = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub
End Class
