Imports common
Public Class rptDairySaleRegisterReport
    Inherits FrmMainTranScreen
    Dim isSchemeItem As Boolean = False
    Dim strQry As String = ""
    Dim dt As DataTable
    Private Sub txtCustomerGroup__My_Click(sender As Object, e As EventArgs) Handles txtCustomerGroup._My_Click
        strQry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtCustomerGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomerGroup.arrValueMember, txtCustomerGroup.arrDispalyMember)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        strQry = " select Cust_Code as [code],Customer_Name as [Name] from TSPL_CUSTOMER_MASTER"
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtItemCode__My_Click(sender As Object, e As EventArgs) Handles txtItemCode._My_Click
        strQry = "Select TSPL_ITEM_MASTER.Item_Code As Code,  TSPL_ITEM_MASTER.Item_Desc As Name From TSPL_ITEM_MASTER "
        txtItemCode.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtItemCode.arrValueMember, txtItemCode.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        strQry = "select Location_Code  as [Code],Location_Desc as [Name] from TSPL_LOCATION_MASTER"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
        'FrmPendingRequisitionQty.SetDiplayMember(txtLocation, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        strQry = "Select TSPL_ZONE_MASTER.Zone_Code As Code,  TSPL_ZONE_MASTER.Description As Name From TSPL_ZONE_MASTER "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        Print(Exporter.Refresh)
    End Sub
    Private Sub Print(ByVal IsPrint As Exporter)
        'Ticket no-ERO/26/09/18-000403 Client - Erode,show Alies Name value in grid instead of Short description from Item master
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            If chkScheme.Checked = True And chkSample.Checked = True Then
                Throw New Exception("Select only one check box at a time Scheme/Sample")
            End If
            'If clsCommon.CompairString(ddlInvocieType.SelectedValue, "Taxable") = CompairStringResult.Equal Or clsCommon.CompairString(ddlInvocieType.SelectedValue, "Non Taxable") = CompairStringResult.Equal Then
            '    ddlInvocieType.Select()
            '    Throw New Exception("Select Invoice Type")
            'End If

            If clsCommon.CompairString(cmbUnit.Text, "Select") = CompairStringResult.Equal Then
                cmbUnit.Select()
                Throw New Exception("Select UOM")
            End If


            Gv1.MasterTemplate.SummaryRowsBottom.Clear()

            Dim MainQuery As String = String.Empty
            Dim strWhrClause As String = String.Empty
            Dim strWhrClause2 As String = String.Empty
            Dim strWhrClauseReturn As String = String.Empty
            Dim itemCode As String = String.Empty
            Dim MainQueryForScheme As String = String.Empty
            itemCode = " and 2=2 "

            If chkSample.Checked = True Then
                strWhrClause2 += " and issampling=1 "
                strWhrClauseReturn += " and issampling=1 "
            ElseIf chkScheme.Checked = True Then
                strWhrClause2 += " and Scheme_Item='Y' and issampling=0 "
                strWhrClauseReturn += " and Scheme_Item='Y' and issampling=0  "
            Else
                strWhrClause2 += " and Scheme_Item='N' and issampling=0  "
                strWhrClauseReturn += " and Scheme_Item='N' and issampling=0  "
            End If
            strWhrClause2 += " and TSPL_ITEM_MASTER.Alies_Name !='' and TSPL_ITEM_MASTER.Alies_Name is not null and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'  and convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "
            strWhrClauseReturn += " and TSPL_ITEM_MASTER.Alies_Name !='' and TSPL_ITEM_MASTER.Alies_Name is not null and TSPL_SD_SALE_RETURN_HEAD.Screen_Type='DS' and convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  convert(date, TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "' "

            If txtCustomerGroup.arrValueMember IsNot Nothing AndAlso txtCustomerGroup.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomerGroup.arrValueMember)
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
                strWhrClauseReturn += " and TSPL_CUSTOMER_MASTER.Cust_Group_Code in (" + ss + ")  "
            End If

            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtCustomer.arrValueMember)
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
                strWhrClauseReturn += " and TSPL_CUSTOMER_MASTER.Cust_Code in (" + ss + ")  "
            End If

            If txtItemCode.arrValueMember IsNot Nothing AndAlso txtItemCode.arrValueMember.Count > 0 Then

                itemCode = itemCode + " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClause2 += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
                strWhrClauseReturn += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItemCode.arrValueMember) + ")  "
            End If

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtLocation.arrValueMember)
                strWhrClause2 += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
                strWhrClauseReturn += " and TSPL_LOCATION_MASTER.Location_Code in (" + ss + ")  "
            End If

            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                Dim ss As String = clsCommon.GetMulcallString(txtZone.arrValueMember)
                strWhrClause2 += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
                strWhrClauseReturn += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + ss + ")  "
            End If


            'If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
            '    strWhrClause2 += " and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = '" + cmbUnit.SelectedValue + "' "
            '    strWhrClauseReturn += " and TSPL_SD_SALE_RETURN_DETAIL.Unit_code = '" + cmbUnit.SelectedValue + "'  "
            'End If

            If clsCommon.CompairString(ddlInvocieType.SelectedValue, "Taxable") = CompairStringResult.Equal Then
                strWhrClause2 += " and TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable =1  "
                strWhrClauseReturn += " and TSPL_SD_SALE_RETURN_HEAD.Is_Taxable =1  "
            ElseIf clsCommon.CompairString(ddlInvocieType.SelectedValue, "Non Taxable") = CompairStringResult.Equal Then
                strWhrClause2 += " and TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable =0  "
                strWhrClauseReturn += " and TSPL_SD_SALE_RETURN_HEAD.Is_Taxable =0  "
            End If

            Dim ItemInUse As String = ""
            ItemInUse = " TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code Left Outer Join TSPL_VEHICLE_MASTER On TSPL_VEHICLE_MASTER.Vehicle_Id = TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code where TSPL_ITEM_MASTER.Alies_Name !='' and TSPL_ITEM_MASTER.Alies_Name is not null "

            If chkSample.Checked = True Then
                ItemInUse += " and issampling=1 "
            ElseIf chkScheme.Checked = True Then
                ItemInUse += " and Scheme_Item='Y' and issampling=0 "
            Else
                ItemInUse += " and Scheme_Item='N' and issampling=0 "
            End If

            ItemInUse += strWhrClause2
            ItemInUse += " order by Alies_Name "

            Dim strSchemeItem As String = "  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name+'(S)') as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "
            strSchemeItem = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strSchemeItem))
            If String.IsNullOrEmpty(strSchemeItem) Then
                clsCommon.MyMessageBoxShow(Me, "Please set Alies Name in item master", Me.Text)
                Exit Sub
            End If

            Dim strItem As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + '0 as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")

            Dim strItem2 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")

            Dim strSumItemOnly As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' +' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name  FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")

            Dim strGrandTotalWithoutScheme As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))'  as Alies_Name  FROM " + ItemInUse + "  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

            Dim strItem2WithSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' + ' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

            Dim query As String = ""

            MainQuery = " select  [Customer Name],[Group],[Cust Group Desc],[Zone],UOM ,RSM,ZSM,ASM,ASO, " + strItem2WithSum + " , (" + strGrandTotalWithoutScheme + ") as [Grand Total] from (select zzz.[Customer Name],zzz.Description,zzz.Cust_Group_Code as [Group], max(zzz.[Cust Group Desc]) as [Cust Group Desc],zzz.Zone_Code as [Zone] ,zzz.UOM,zzz.RSM,zzz.ZSM,zzz.ASM,zzz.ASO,sum(qty) as qty from  ( "
            MainQuery += " Select TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location AS Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As [Customer Name], TSPL_SD_SALE_INVOICE_DETAIL.Item_Code as Item_Code, "
            MainQuery += "'" + clsCommon.myCstr(cmbUnit.SelectedValue) + "' as UOM,TSPL_SD_SALE_INVOICE_HEAD.Route_No as [Route No] ,TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,"
            MainQuery += " ISNULL(round(case when coalesce(CONVERTED_UOM.Conversion_factor,0)=0 then 0 else  (TSPL_SD_SALE_INVOICE_DETAIL.Qty*StockingUnit.Conversion_Factor /coalesce(CONVERTED_UOM.Conversion_factor,1)) end,2),0) as Qty "
            MainQuery += ", TSPL_SD_SALE_INVOICE_HEAD.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],EMRSM.Emp_Name as RSM,EMZSM.Emp_Name as ZSM,EMASM.Emp_Name as ASM,EMASO.Emp_Name as ASO From TSPL_SD_SALE_INVOICE_DETAIL Left Outer Join TSPL_SD_SALE_INVOICE_HEAD On TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code left join tspl_employee_master as EMRSM on EMRSM.EMP_CODE=TSPL_CUSTOMER_MASTER.RSM left join tspl_employee_master as EMZSM on EMZSM.EMP_CODE=TSPL_CUSTOMER_MASTER.ZSM left join tspl_employee_master as EMASM on EMASM.EMP_CODE=TSPL_CUSTOMER_MASTER.ASM left join tspl_employee_master as EMASO on EMASO.EMP_CODE=TSPL_CUSTOMER_MASTER.ASO "
            MainQuery += "  LEFT OUTER JOIN tspl_item_uom_detail as StockingUnit on StockingUnit.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and StockingUnit.UOM_CODE=TSPL_SD_SALE_INVOICE_DETAIL.UNIT_CODE "
            MainQuery += " inner join TSPL_ITEM_UOM_DETAIL AS CONVERTED_UOM ON CONVERTED_UOM.Item_Code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code and CONVERTED_UOM.UOM_CODE='" + clsCommon.myCstr(cmbUnit.SelectedValue) + "'"
            MainQuery += " where 2=2 " + strWhrClause2 + ""
            MainQuery += " union all Select TSPL_SD_SALE_RETURN_DETAIL.Scheme_Item, TSPL_ITEM_MASTER.Sku_Seq, TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location AS Location_Code, TSPL_LOCATION_MASTER.Location_Desc, TSPL_SD_SALE_RETURN_HEAD.Customer_Code As [Customer Code], TSPL_CUSTOMER_MASTER.Customer_Name As [Customer Name], TSPL_SD_SALE_RETURN_DETAIL.Item_Code as Item_Code,"
            MainQuery += "'" + clsCommon.myCstr(cmbUnit.SelectedValue) + "' as UOM "
            MainQuery += ",TSPL_SD_SALE_RETURN_HEAD.Route_No as [Route No] ,TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_CUSTOMER_MASTER.Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code ,"
            MainQuery += "-ISNULL(round(case when coalesce(CONVERTED_UOM.Conversion_factor,0)=0 then 0 else  (TSPL_SD_SALE_RETURN_DETAIL.Qty*StockingUnit.Conversion_Factor /coalesce(CONVERTED_UOM.Conversion_factor,1)) end,2),0) as Qty "
            MainQuery += ", TSPL_SD_SALE_RETURN_HEAD.Document_Date As [Order Date],TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Desc as [Cust Group Desc],EMRSM.Emp_Name as RSM,EMZSM.Emp_Name as ZSM,EMASM.Emp_Name as ASM,EMASO.Emp_Name as ASO From TSPL_SD_SALE_RETURN_DETAIL Left Outer Join TSPL_SD_SALE_RETURN_HEAD On TSPL_SD_SALE_RETURN_HEAD.Document_Code  = TSPL_SD_SALE_RETURN_DETAIL.Document_Code Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code  Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_SD_SALE_RETURN_DETAIL.Item_Code  Left Outer Join TSPL_LOCATION_MASTER On TSPL_LOCATION_MASTER.Location_Code = TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location left outer join TSPL_CUSTOMER_GROUP_MASTER on TSPL_CUSTOMER_GROUP_MASTER.Cust_Group_Code=TSPL_CUSTOMER_MASTER.Cust_Group_Code left join tspl_employee_master as EMRSM on EMRSM.EMP_CODE=TSPL_CUSTOMER_MASTER.RSM left join tspl_employee_master as EMZSM on EMZSM.EMP_CODE=TSPL_CUSTOMER_MASTER.ZSM left join tspl_employee_master as EMASM on EMASM.EMP_CODE=TSPL_CUSTOMER_MASTER.ASM left join tspl_employee_master as EMASO on EMASO.EMP_CODE=TSPL_CUSTOMER_MASTER.ASO "
            MainQuery += "  LEFT OUTER JOIN tspl_item_uom_detail as StockingUnit on StockingUnit.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code and StockingUnit.UOM_CODE=TSPL_SD_SALE_RETURN_DETAIL.UNIT_CODE "
            MainQuery += " inner join TSPL_ITEM_UOM_DETAIL AS CONVERTED_UOM ON CONVERTED_UOM.Item_Code=TSPL_SD_SALE_RETURN_DETAIL.Item_Code and CONVERTED_UOM.UOM_CODE='" + clsCommon.myCstr(cmbUnit.SelectedValue) + "'"
            MainQuery += " inner join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_RETURN_HEAD.against_invoice_no"
            MainQuery += " where 2=2 " + strWhrClauseReturn + ""
            MainQuery += " )zzz where 1=1 group by zzz.[Customer Name],zzz.Description,zzz.Cust_Group_Code,zzz.Zone_Code,zzz.UOM,zzz.RSM,zzz.ZSM,zzz.ASM,zzz.ASO 	) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot group by zpivot.[Customer Name],zpivot.[Group],zpivot.[Cust Group Desc],zpivot.[Zone],zpivot.UOM,zpivot.RSM,zpivot.ZSM,zpivot.ASM,zpivot.ASO "

            query = MainQuery


            Dim dtgv As New DataTable
            dtgv = clsDBFuncationality.GetDataTable(query)
            Gv1.DataSource = Nothing

            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv
            Gv1.BestFitColumns()

            If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            RadPageView1.SelectedPage = RadPageViewPage2

            Dim item As Integer = 0
            item = 9
            If Gv1.Rows.Count > 0 Then
                Dim summaryRowItem As New GridViewSummaryRowItem()
                For i As Integer = item To Gv1.Columns.Count - 1
                    Dim aa = Gv1.Columns(i).HeaderText()
                    Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)


                Next

                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom

            End If

            ' Hide Column when footer grand total <= 0
            For i As Integer = item To Gv1.Columns.Count - 5
                Dim grandTotal As Decimal = 0
                For j As Integer = 0 To Gv1.Rows.Count - 1
                    Dim columnValue As Object = String.Empty
                    columnValue = Gv1.Rows(j).Cells(i).Value
                    If (Not IsDBNull(Gv1.Rows(j).Cells(i).Value) AndAlso columnValue IsNot Nothing) Then
                        grandTotal = grandTotal + clsCommon.myCdbl(Gv1.Rows(j).Cells(i).Value)
                    End If
                Next
                If (clsCommon.myCdbl(grandTotal) > 0) Then
                    Gv1.Columns(i).IsVisible = True
                Else
                    Gv1.Columns(i).IsVisible = False
                End If
            Next

            Gv1.Columns("Customer Name").IsPinned = True
            Gv1.Columns("Zone").IsPinned = True
            Gv1.Columns("UOM").IsPinned = True
            If chkScheme.Checked = False AndAlso chkSample.Checked = False Then
                Gv1.Columns("RSM").IsVisible = True
                Gv1.Columns("ZSM").IsVisible = True
                Gv1.Columns("ASM").IsVisible = True
                Gv1.Columns("ASO").IsVisible = True

                Gv1.Columns("RSM").IsPinned = True
                Gv1.Columns("ZSM").IsPinned = True
                Gv1.Columns("ASM").IsPinned = True
                Gv1.Columns("ASO").IsPinned = True
            Else
                Gv1.Columns("RSM").IsVisible = False
                Gv1.Columns("ZSM").IsVisible = False
                Gv1.Columns("ASM").IsVisible = False
                Gv1.Columns("ASO").IsVisible = False

                Gv1.Columns("RSM").IsPinned = False
                Gv1.Columns("ZSM").IsPinned = False
                Gv1.Columns("ASM").IsPinned = False
                Gv1.Columns("ASO").IsPinned = False
            End If

            Gv1.Columns("Group").IsVisible = False
            Gv1.Columns("Cust Group Desc").IsVisible = False
            Gv1.Columns(Gv1.Columns.Count - 1).IsPinned = True
            Gv1.Columns(Gv1.Columns.Count - 1).PinPosition = PinnedColumnPosition.Right

            For i As Integer = item To Gv1.Columns.Count - 1
                Gv1.Columns(i).FormatString = "{0:n2}"
            Next

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub rptDairySaleRegisterReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        LoadInvoiceType()
        LoadUnit()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        txtCustomer.arrValueMember = Nothing
        txtCustomerGroup.arrValueMember = Nothing
        txtItemCode.arrValueMember = Nothing
        txtLocation.arrValueMember = Nothing
        txtZone.arrValueMember = Nothing
        cmbUnit.Text = "Select"
        LoadInvoiceType()
        fromDate.Value = clsCommon.GETSERVERDATE()
        ToDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadUnit()
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("Code") = ""
        dr("Description") = "Select"
        dt.Rows.InsertAt(dr, 0)
        cmbUnit.DataSource = Nothing
        cmbUnit.DataSource = dt
        cmbUnit.DisplayMember = "Description"
        cmbUnit.ValueMember = "Code"
    End Sub

    Private Sub RadMenuItem1_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        'Try

        '    If Gv1.Rows.Count > 0 Then
        '        Dim arrHeader As List(Of String) = New List(Of String)()
        '        arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        '        arrHeader.Add("Dairy Sale Register Report")

        '        clsCommon.MyExportToExcelGrid("Dairy Sale Register Report", Gv1, arrHeader, "Dairy Sale Register Report")
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try

        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            'arrHeader.Add("Age as of: " + clsCommon.GetPrintDate(dtpAgeof.Value, "dd/MM/yyyy") + " cutoff Date " + clsCommon.GetPrintDate(dtpCutoffDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDairySaleRegisterReport & "'"))
            'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
            '    arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    'Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs)
    '    Try
    '        If Gv1.Rows.Count > 0 Then
    '            Dim arrHeader As List(Of String) = New List(Of String)()
    '            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
    '            arrHeader.Add("Dairy Sale Register Report")
    '            clsCommon.MyExportToPDF("Dairy Sale Register Report", Gv1, arrHeader, "Dairy Sale Register Report")
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
  
    Sub LoadInvoiceType()
        dt = New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Rows.Add("All")
        dt.Rows.Add("Taxable")
        dt.Rows.Add("Non Taxable")

        ddlInvocieType.DataSource = dt
        ddlInvocieType.ValueMember = "Code"
        ddlInvocieType.DisplayMember = "Code"
    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDairySaleRegisterReport & "'"))

            If txtCustomerGroup.arrDispalyMember IsNot Nothing AndAlso txtCustomerGroup.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer Group : " + clsCommon.GetMulcallStringWithComma(txtCustomerGroup.arrDispalyMember))
            End If
            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtItemCode.arrDispalyMember IsNot Nothing AndAlso txtItemCode.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItemCode.arrDispalyMember))
            End If
            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If
            If txtZone.arrDispalyMember IsNot Nothing AndAlso txtZone.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrDispalyMember))
            End If
            If clsCommon.myLen(ddlInvocieType.Text) > 0 Then
                arrHeader.Add("Invoice Type : " + ddlInvocieType.Text)
            End If
            If clsCommon.myLen(cmbUnit.Text) > 0 Then
                arrHeader.Add("UOM : " + cmbUnit.Text)
            End If

           

            clsCommon.MyExportToPDF("Dairy Sale Register Report", Gv1, arrHeader, "Dairy Sale Register Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
