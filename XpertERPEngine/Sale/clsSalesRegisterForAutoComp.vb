Imports common
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports Telerik.WinControls
'================Create by Preeti Gupta=========
Public Class clsSalesRegisterForAutoComp
    Public Shared Function GetReportDataReader(ByVal obj As clsSaleRegisterForAutoCompParameterType) As SqlDataReader
        Dim strRunQuery As String = ""
        strRunQuery = GetReportDataQuery(obj)
        Dim rd As SqlDataReader = clsDBFuncationality.GetDataReader(strRunQuery)
        Return rd
    End Function
    Public Shared Function GetReportDataQuery(ByVal obj As clsSaleRegisterForAutoCompParameterType) As String
        Dim strRunQuery As String = ""
        Dim strPivotForFinalOuterQuery As String = ""
        Dim qryList As ArrayList
        Dim qryListDocInfoLevel As ArrayList



        Dim strMainDocInfoLevel As String = Nothing

        Dim strMain As String = Nothing

        If obj.ReportType = "Document Info Level" Then
            qryListDocInfoLevel = ReturnQueryforDocumentInfoLevel(obj)
            strMainDocInfoLevel = qryListDocInfoLevel(0)
            strPivotForFinalOuterQuery = qryListDocInfoLevel(1)
        Else
            qryList = ReturnQuery(obj)
            strMain = qryList(0)
            strPivotForFinalOuterQuery = qryList(1)

        End If

        If obj.ReportType = "Document Info Level" Then

            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMainDocInfoLevel += " and xx.[Item Code] in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Trans_Type_List IsNot Nothing AndAlso obj.Trans_Type_List.Count > 0 Then
                strMainDocInfoLevel += " and xx.[Trans Type] in (" + clsCommon.GetMulcallString(obj.Trans_Type_List) + ") "
            End If
            If obj.State_List IsNot Nothing AndAlso obj.State_List.Count > 0 Then
                strMainDocInfoLevel += " and Loc.State in (" + clsCommon.GetMulcallString(obj.State_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMainDocInfoLevel += " and xx.[Location Code] in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strMainDocInfoLevel += " and xx.[Customer Code] in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If obj.Item_Group_List IsNot Nothing AndAlso obj.Item_Group_List.Count > 0 Then
                strMainDocInfoLevel += " and coalesce(Item_Group.Item_Group,'') in (" + clsCommon.GetMulcallString(obj.Item_Group_List) + ") "
            End If

            ''richa agarwal 4 Apr,2018
            '' Done by Panch raj against Ticket No:BM00000007277
            'If obj.Cust_Group_Code_List IsNot Nothing AndAlso obj.Cust_Group_Code_List.Count > 0 Then
            '    strMainDocInfoLevel += " and coalesce(Cust.Cust_Group_Code,'') in (" + clsCommon.GetMulcallString(obj.Cust_Group_Code_List) + ") "
            'End If

            Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

            If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And obj.Cust_Group_Code_List Is Nothing Then
                strMainDocInfoLevel += " and coalesce(Cust.Cust_Group_Code,'') in (select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "')"
            ElseIf obj.Cust_Group_Code_List IsNot Nothing AndAlso obj.Cust_Group_Code_List.Count > 0 Then
                strMainDocInfoLevel += " and coalesce(Cust.Cust_Group_Code,'') in (" + clsCommon.GetMulcallString(obj.Cust_Group_Code_List) + ") "
            End If

            If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And obj.Customer_Code_List Is Nothing Then
                strMainDocInfoLevel += " and xx.[Customer Code] in  (select DISTINCT Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL WHERE User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "' "
                If obj.Cust_Group_Code_List IsNot Nothing AndAlso obj.Cust_Group_Code_List.Count > 0 Then
                    strMainDocInfoLevel += " and TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code in (" + clsCommon.GetMulcallString(obj.Cust_Group_Code_List) + ") ) "
                Else
                    strMainDocInfoLevel += " ) "
                End If
            End If

            ''----------------
            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strMainDocInfoLevel += " and xx.[Document No] = '" & obj.Document_Code & "' "
            End If
            If clsCommon.myLen(obj.Other_Cond) > 0 Then
                strMainDocInfoLevel += obj.Other_Cond
            End If
        Else

            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMain += " and xx.[Item Code] in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            'If obj.Trans_Type_List IsNot Nothing AndAlso obj.Trans_Type_List.Count > 0 Then
            '    strMain += " and xx.[Trans Type] in (" + clsCommon.GetMulcallString(obj.Trans_Type_List) + ") "
            'End If
            If obj.State_List IsNot Nothing AndAlso obj.State_List.Count > 0 Then
                strMain += " and Loc.State in (" + clsCommon.GetMulcallString(obj.State_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMain += " and xx.[Location Code] in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strMain += " and xx.[Customer Code] in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If obj.Item_Group_List IsNot Nothing AndAlso obj.Item_Group_List.Count > 0 Then
                strMain += " and coalesce(Item_Group.Item_Group,'') in (" + clsCommon.GetMulcallString(obj.Item_Group_List) + ") "
            End If

            ''richa agarwal 4 Apr,2018
            '' Done by Panch raj against Ticket No:BM00000007277
            'If obj.Cust_Group_Code_List IsNot Nothing AndAlso obj.Cust_Group_Code_List.Count > 0 Then
            '    strMain += " and coalesce(Cust.Cust_Group_Code,'') in (" + clsCommon.GetMulcallString(obj.Cust_Group_Code_List) + ") "
            'End If

            Dim dtCustGroup As DataTable = clsDBFuncationality.GetDataTable("select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "'")

            If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And obj.Cust_Group_Code_List Is Nothing Then
                strMain += " and coalesce(Cust.Cust_Group_Code,'') in (select distinct Cust_Group_Code from TSPL_CUSTOMER_GROUP_MAPPING where User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "')"
            ElseIf obj.Cust_Group_Code_List IsNot Nothing AndAlso obj.Cust_Group_Code_List.Count > 0 Then
                strMain += " and coalesce(Cust.Cust_Group_Code,'') in (" + clsCommon.GetMulcallString(obj.Cust_Group_Code_List) + ") "
            End If

            If dtCustGroup IsNot Nothing AndAlso dtCustGroup.Rows.Count > 0 And obj.Customer_Code_List Is Nothing Then
                strMain += " and xx.[Customer Code] in  (select DISTINCT Cust_Code from TSPL_CUSTOMER_GROUP_MAPPING_DETAIL WHERE User_Code ='" & clsCommon.myCstr(objCommonVar.CurrentUserCode) & "' "
                If obj.Cust_Group_Code_List IsNot Nothing AndAlso obj.Cust_Group_Code_List.Count > 0 Then
                    strMain += " and TSPL_CUSTOMER_GROUP_MAPPING_DETAIL.Cust_Group_Code in (" + clsCommon.GetMulcallString(obj.Cust_Group_Code_List) + ") ) "
                Else
                    strMain += " ) "
                End If
            End If

            ''----------------

            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strMain += " and xx.[Document No] = '" & obj.Document_Code & "' "
            End If
            If clsCommon.myLen(obj.Other_Cond) > 0 Then
                strMain += obj.Other_Cond
            End If
        End If

        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowParticluarColumnInSalesRegisterForGopalJee, clsFixedParameterCode.ShowParticluarColumnInSalesRegisterForGopalJee, Nothing)) = 1 Then
            If obj.ReportType = "Total Sale" Then
                strRunQuery = " select sum(Amount) as Amount,sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final"
            ElseIf obj.ReportType = "Location Wise" Then
                strRunQuery = "select [Warehouse Code] ,[Warehouse Name] ,sum(Amount) as Amount,sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Warehouse Code] ,[Warehouse Name] "
            ElseIf obj.ReportType = "Item Group Wise" Then
                strRunQuery = "select [Warehouse Code] ,[Warehouse Name] ,[Product Group Code],[Product Group Description],sum(Amount) as Amount,sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ")  as Final group by [Warehouse Code] ,[Warehouse Name] ,[Product Group Code] ,[Product Group Description] "
            ElseIf obj.ReportType = "Customer Group Wise" Then
                strRunQuery = "select [Warehouse Code] ,[Warehouse Name] ,[Product Group Code]  ,[Product Group Description] ,[Customer Group Code],[Customer Group Description],sum(Amount) as Amount,sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ")  as Final group by [Warehouse Code] ,[Warehouse Name] ,[Product Group Code] ,[Product Group Description] ,[Customer Group Code],[Customer Group Description]"
            ElseIf obj.ReportType = "Item Wise" Then
                strRunQuery = "select [Warehouse Code] ,[Warehouse Name],[Product Group Code] ,[Product Group Description],[Customer Group Code],[Customer Group Description],[Product Code] ,[Product Name],sum(Amount) as Amount,sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Warehouse Code] ,[Warehouse Name],[Product Group Code]  ,[Product Group Description] ,[Customer Group Code],[Customer Group Description],[Product Code] ,[Product Name] "
            ElseIf obj.ReportType = "Customer Wise" Then
                strRunQuery = "select [Warehouse Code],[Warehouse Name],[product Group Code],[Product Group Description],[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],[Product Code],[Product Name],sum(Amount) as Amount,sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ")  as final group by [Warehouse Code],[warehouse Name],[Product Group Code] ,[Product Group Description] ,[Customer Group Code],[Customer Group Description],[Product Code] ,[Product Name],[Customer Code],[Customer Name]"
            ElseIf obj.ReportType = "Document Wise" Then
                strRunQuery = "select [Trans Type],[Document_date],[Document No],max([Narration]) as [Narration], [Warehouse Code],  [Warehouse Name], [Customer Group Code], [Customer Group Description],[Customer Code],[Customer Name],max([Customer GST State Code]) as [Customer GST State Code] ,sum(Amount) as Amount ,sum([Total Tax Amount]) as [Total Tax Amount], sum([Total Amount]) as [Total Amount] from (" & strMain & ") as final group by [Document No],[Warehouse Code] ,[Warehouse Name] ,[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],[Document_date],[Trans Type] order by convert(date,[Document_Date],103),[Document No]"
            ElseIf obj.ReportType = "Document Detail" Then
                strRunQuery = strMain & "order by convert(date,[Document_Date],103),[Document No]"
            End If
        Else
            If obj.ReportType = "Total Sale" Then
                strRunQuery = "select sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final"
            ElseIf obj.ReportType = "Location Wise" Then
                strRunQuery = "select [Location Code],[Location Name],sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name]"
            ElseIf obj.ReportType = "Item Group Wise" Then
                strRunQuery = "select [Location Code],[Location Name],[Item Group Code],[Item Group Description],sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description]"
            ElseIf obj.ReportType = "Customer Group Wise" Then
                strRunQuery = "select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description]"
            ElseIf obj.ReportType = "Item Wise" Then
                'KUNAL > WHOLLY JOY > TICKET : VERBAL DISCUSSION > DATE : 3 -JAN -2016 
                strRunQuery = "select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name], SUM([Quantity]) [Quantity], MAX([UOM]) [UOM] ,sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name] ,  [UOM]  "
            ElseIf obj.ReportType = "Customer Wise" Then
                strRunQuery = "select [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],[Item Code],[Item Name],sum([Sale Amount]) as [Total Sale Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount] from (" & strMain & ") as Final group by [Location Code],[Location Name],[Item Group Code],[Item Group Description],[Customer Group Code],[Customer Group Description],[Item Code],[Item Name],[Customer Code],[Customer Name]"
            ElseIf obj.ReportType = "Document Wise" Then
                If obj.QuickLoad Then
                    strRunQuery = "select [Document No],[Document_date],[Trans Type],max([Narration]) as [Narration],[Location Code],[Location Name],max([Location State]) as [Location State],max([GST State Code]) as [GST State Code],max([Dispatch Location GSTIN No]) as [Dispatch Location GSTIN No],[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],max([City Code]) as [City Code],max([Place of Supply]) as [Place of Supply],max([Customer GST State Code]) as [Customer GST State Code],max([Invoice Type]) as [Invoice Type],max([TIN No]) as [TIN No],max([GR No]) as [GR No],max([GR Date]) as [GR Date],max([WayBill No]) as [WayBill No],max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],sum([Sale Amount]) as [Total Sale Amount],sum([Sale Amount GST]) as [Total Sale Amount GST],sum([Discount Amount]) as [Discount Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount],max([AR Document No]) as [AR Document No], max([AR Document Amt]) as [AR Document Amt],max([AR Document Discount Amt]) as [AR Document Discount Amt] , case when max(coalesce([AR Amount Before Tax],0))>0 then  max([AR Amount Before Tax]) else  min([AR Amount Before Tax]) end as [AR Amount Before Tax],max([AR Total Tax]) as [AR Total Tax],max([AR Total Add Charge]) as [AR Total Add Charge],max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],sum([Nill Rate Amount]) as [Nill Rate Amount],sum([Exempted Amount]) as [Exempted Amount],sum([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],max([LUT No]) as [LUT No]  from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],[Document_date],[Trans Type] order by convert(date,[Document_Date],103),[Document No]"
                Else
                    strRunQuery = "select [Document No],[Document_date],[Trans Type],max([Narration]) as [Narration],[Location Code],[Location Name],max([Location State]) as [Location State],max([GST State Code]) as [GST State Code],max([Dispatch Location GSTIN No]) as [Dispatch Location GSTIN No],[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],max([City Code]) as [City Code],max([Place of Supply]) as [Place of Supply],max([Customer GST State Code]) as [Customer GST State Code],max([Invoice Type]) as [Invoice Type],max([TIN No]) as [TIN No],max([GR No]) as [GR No],max([GR Date]) as [GR Date],max([WayBill No]) as [WayBill No],max([Transporter Code]) as [Transporter Code],max([Transporter Name]) as [Transporter Name],sum([Sale Amount]) as [Total Sale Amount],sum([Sale Amount GST]) as [Total Sale Amount GST],sum([Discount Amount]) as [Discount Amount],sum([Additional Amount]) as [Total Additional Amount],sum([Total Tax Amount]) as [Total Tax Amount],sum([Total Amount] ) as [Total Amount],max([AR Document No]) as [AR Document No], max([AR Document Amt]) as [AR Document Amt],max([AR Document Discount Amt]) as [AR Document Discount Amt] , case when max(coalesce([AR Amount Before Tax],0))>0 then  max([AR Amount Before Tax]) else  min([AR Amount Before Tax]) end as [AR Amount Before Tax],max([AR Total Tax]) as [AR Total Tax],max([AR Total Add Charge]) as [AR Total Add Charge],max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],sum([Nill Rate Amount]) as [Nill Rate Amount],sum([Exempted Amount]) as [Exempted Amount],sum([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision] ,max([LUT No]) as [LUT No] from (" & strMain & ") as Final group by [Document No],[Location Code],[Location Name],[Customer Group Code],[Customer Group Description],[Customer Code],[Customer Name],[Document_date],[Trans Type] order by convert(date,[Document_Date],103),[Document No]"
                End If

            ElseIf obj.ReportType = "Document Detail" Then
                If obj.QuickLoad Then
                    strRunQuery = strMain & "  order by convert(date,[Document_Date],103),[Document No]"
                Else
                    strRunQuery = strMain & " order by convert(date,[Document_Date],103),[Document No]"
                End If

            ElseIf obj.ReportType = "Document Info Level" Then
                strRunQuery = strMainDocInfoLevel & "order by convert(date,[Document_Date],103),[Document No]"
            End If
        End If


        Return strRunQuery
    End Function
    Public Shared Function ReturnQuery(ByVal obj As clsSaleRegisterForAutoCompParameterType) As ArrayList
        Dim Stock_uom As Boolean = obj.stockinguom
        Dim From_Date As Date = obj.From_Date
        Dim To_Date As Date = obj.To_Date
        Dim Unit_Code As String = obj.Unit_Code
        Dim QryLst As New ArrayList

        Dim strCodeColumn As String = ""
        Dim strCodeColumnForVirtual As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""
        Dim strPivotForFinalOuterQuery As String = ""
        Dim strCategoryTable As String = ""
        Dim MIS_Item_Group As String = GetMIS_ITem_GroupColumn()
        Dim dtCategory As New DataTable
        Dim qryStarted As Boolean = False
        Dim ItemCategoryCondQry As String = GetTrasactionItemCodeQry(obj)
        ''====================Monika 11/04/2017
        If obj.rbtnCategorySected OrElse (clsCommon.CompairString(obj.ReportType, "Document Info Level") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.ReportType, "Document Detail") = CompairStringResult.Equal) Then ''if categories are checked from screen only then show as pivot.Or if document detail/info report opened then category pivot run
            dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Desc' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += ","
                        strCodeColumnForVirtual += ","
                        strCodeColumnMax += ","
                        strCodeDescColumn += ","
                        strCodeDescColumnMax += ","
                    End If
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumnForVirtual += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"
                Next
                strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
                " select * from ( " + Environment.NewLine & _
                " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+' Desc' as Item_Category_CodeDesc " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
                " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
                " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
                " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
                " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
                " where 2=2 " + Environment.NewLine & _
                " )xx" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
                " ) Pivt" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " (" + Environment.NewLine & _
                " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
                " ) Pivt1 " + Environment.NewLine & _
                " ) xxx group by Item_Code "
                ''End of Category Table start now.
            End If
        End If
        ''and TSPL_ITEM_MASTER.Item_Code in (" & ItemCategoryCondQry & " )
        ''Virtual Category Table start now.


        Dim strMCCMaterial As String = ""
        Dim qryTaxQuery As String = ""
        'Dim strPivotForOuter As String

        qryTaxQuery = GetTaxQuery(obj)


        Dim dtTax As DataTable
        If clsCommon.myLen(qryTaxQuery) > 0 Then
            dtTax = clsDBFuncationality.GetDataTable(qryTaxQuery)
            If dtTax.Rows.Count <= 0 Then
                qryTaxQuery = "select 'EXEMPTED' as Tax1"
                dtTax = clsDBFuncationality.GetDataTable(qryTaxQuery)
            End If
        Else
            qryTaxQuery = "select 'EXEMPTED' as Tax1"
            dtTax = clsDBFuncationality.GetDataTable(qryTaxQuery)
            'dtTax = New DataTable
            'dtTax.Columns.Add("Tax1")
        End If

        '' create strPivotForOuterQuery
        Dim strPivotForOuterQuery As String = ","
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strPivotForOuterQuery = strPivotForOuterQuery & "sum(isnull(final.[" & clsCommon.myCstr(dr.Item("TAX1")) & "],0)) as [" & clsCommon.myCstr(dr.Item("TAX1")) & "]"
            Else
                strPivotForOuterQuery = strPivotForOuterQuery & "sum(isnull(final.[" & clsCommon.myCstr(dr.Item("TAX1")) & "],0)) as [" & clsCommon.myCstr(dr.Item("TAX1")) & "],"
            End If

        Next

        strPivotForFinalOuterQuery = ","
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strPivotForFinalOuterQuery = strPivotForFinalOuterQuery & "xx.[" & clsCommon.myCstr(dr.Item("TAX1")) & "]"
            Else
                strPivotForFinalOuterQuery = strPivotForFinalOuterQuery & "xx.[" & clsCommon.myCstr(dr.Item("TAX1")) & "],"
            End If
        Next
        obj.strPivotForFinalOuterQuery = strPivotForFinalOuterQuery

        Dim strPivotForFinalOuterPercentQuery As String
        '' update strPivotForFinalOuterPercentQuery
        strPivotForFinalOuterPercentQuery = ","
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strPivotForFinalOuterPercentQuery = strPivotForFinalOuterPercentQuery & "xx.[" & clsCommon.myCstr(dr.Item("TAX1")) & "%]"
            Else
                strPivotForFinalOuterPercentQuery = strPivotForFinalOuterPercentQuery & "xx.[" & clsCommon.myCstr(dr.Item("TAX1")) & "%],"
            End If
        Next

        Dim strPivotForTransfer_In As String


        ''update strPivotForTransfer_In
        strPivotForTransfer_In = ","
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strPivotForTransfer_In = strPivotForTransfer_In & "0 as [" & clsCommon.myCstr(dr.Item("TAX1")) & "]"
            Else
                strPivotForTransfer_In = strPivotForTransfer_In & "0 as [" & clsCommon.myCstr(dr.Item("TAX1")) & "],"
            End If
        Next


        Dim strPivotFortRANSFER_INPercentQuery As String '= clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotFortRANSFER_INPercent))

        '' update strPivotFortRANSFER_INPercentQuery
        strPivotFortRANSFER_INPercentQuery = ","
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strPivotFortRANSFER_INPercentQuery = strPivotFortRANSFER_INPercentQuery & "0 as [" & clsCommon.myCstr(dr.Item("TAX1")) & "%]"
            Else
                strPivotFortRANSFER_INPercentQuery = strPivotFortRANSFER_INPercentQuery & "0 as [" & clsCommon.myCstr(dr.Item("TAX1")) & "%],"
            End If
        Next


        Dim strPivotFoGrouprOuterQuery As String '= clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuter))
        '' update strPivotFoGrouprOuterQuery
        strPivotFoGrouprOuterQuery = ""
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strPivotFoGrouprOuterQuery = strPivotFoGrouprOuterQuery & "max(isnull(final.[" & clsCommon.myCstr(dr.Item("TAX1")) & "%],0)) as [" & clsCommon.myCstr(dr.Item("TAX1")) & "%]"
            Else
                strPivotFoGrouprOuterQuery = strPivotFoGrouprOuterQuery & "max(isnull(final.[" & clsCommon.myCstr(dr.Item("TAX1")) & "%],0)) as [" & clsCommon.myCstr(dr.Item("TAX1")) & "%],"
            End If
        Next

        Dim strPivotForOuterQueryforBulk As String '' = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterForBulk))
        '' update strPivotForOuterQueryforBulk
        strPivotForOuterQueryforBulk = ","
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strPivotForOuterQueryforBulk = strPivotForOuterQueryforBulk & "0 as [" & clsCommon.myCstr(dr.Item("TAX1")) & "]"
            Else
                strPivotForOuterQueryforBulk = strPivotForOuterQueryforBulk & "0 as [" & clsCommon.myCstr(dr.Item("TAX1")) & "],"
            End If
        Next

        Dim strDoublePivotForOuterQueryforBulk As String ' = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForOuterForBulk))
        '' update strDoublePivotForOuterQueryforBulk
        strDoublePivotForOuterQueryforBulk = ","
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strDoublePivotForOuterQueryforBulk = strDoublePivotForOuterQueryforBulk & "0 as [" & clsCommon.myCstr(dr.Item("TAX1")) & "%]"
            Else
                strDoublePivotForOuterQueryforBulk = strDoublePivotForOuterQueryforBulk & "0 as [" & clsCommon.myCstr(dr.Item("TAX1")) & "%],"
            End If
        Next

        Dim strPivotForInnerQuery As String '= clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))

        '' update strPivotForInnerQuery
        strPivotForInnerQuery = ""
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strPivotForInnerQuery = strPivotForInnerQuery & "[" & clsCommon.myCstr(dr.Item("TAX1")) & "]"
            Else
                strPivotForInnerQuery = strPivotForInnerQuery & "[" & clsCommon.myCstr(dr.Item("TAX1")) & "],"
            End If
        Next

        Dim strPivotForInnerQueryNoTax As String '= clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))

        '' update strPivotForInnerQueryNoTax
        strPivotForInnerQueryNoTax = ""
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strPivotForInnerQueryNoTax = strPivotForInnerQueryNoTax & "Null as [" & clsCommon.myCstr(dr.Item("TAX1")) & "]"
            Else
                strPivotForInnerQueryNoTax = strPivotForInnerQueryNoTax & "Null as [" & clsCommon.myCstr(dr.Item("TAX1")) & "],"
            End If
        Next



        Dim strDoublePivotForInnerQuery As String '= clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))

        ' update strDoublePivotForInnerQuery
        strDoublePivotForInnerQuery = ""
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strDoublePivotForInnerQuery = strDoublePivotForInnerQuery & "[" & clsCommon.myCstr(dr.Item("TAX1")) & "%]"
            Else
                strDoublePivotForInnerQuery = strDoublePivotForInnerQuery & "[" & clsCommon.myCstr(dr.Item("TAX1")) & "%],"
            End If
        Next



        Dim strDoublePivotForInnerQueryNoTax As String '= clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))
        ' update strDoublePivotForInnerQueryNoTax
        strDoublePivotForInnerQueryNoTax = ""
        For Each dr As DataRow In dtTax.Rows
            If dtTax.Rows.IndexOf(dr) = dtTax.Rows.Count - 1 Then
                strDoublePivotForInnerQueryNoTax = strDoublePivotForInnerQueryNoTax & "Null as [" & clsCommon.myCstr(dr.Item("TAX1")) & "%]"
            Else
                strDoublePivotForInnerQueryNoTax = strDoublePivotForInnerQueryNoTax & "Null as [" & clsCommon.myCstr(dr.Item("TAX1")) & "%],"
            End If
        Next

        Dim qryQC As String = ""
        qryQC = " select Item_Code,MAX(Fat_Per) as Fat_Per,MAX(SNF_Per) as SNF_Per from (" & _
                " select Item_QCP.Item_Code,Item_QCP.Code as Parameter_Code,(case when QCP.Type='FAT' then Item_QCP.Actual_Range end) as Fat_Per," & _
                " (case when QCP.Type='SNF' then Item_QCP.Actual_Range  end) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QCP " & _
                " left join TSPL_PARAMETER_MASTER QCP  on Item_QCP.Code=QCP.Code) as QC group by Item_Code"

        Dim qryKG As String = ""
        qryKG = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG'"
        Dim qryStock As String = ""
        qryStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "

        '' query for transaction  UOM conversion
        Dim qryTransStock As String = ""
        If clsCommon.myLen(Unit_Code) <= 0 AndAlso Not Stock_uom Then
            qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "
        ElseIf clsCommon.myLen(Unit_Code) <= 0 AndAlso Stock_uom Then
            qryTransStock = "select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code "
        Else
            qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & Unit_Code & "'"
        End If
        '===================Added By Preeti Gupta===================
        Dim qryRateStock As String = ""
        ' =============================================================
        Dim CompGstinNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select gstreg_no from TSPL_COMPANY_MASTER where Comp_Code='" & objCommonVar.CurrentCompanyCode & "'"))

        '' end query for transaction  UOM conversion
        ' BM00000008464 BM00000008484
        '' query for structure and item group custom field========================BM00000008352
        '==============added by preeti Gupta against ticket No[BM00000009864]
        Dim strItemGroup As String = ""
        strItemGroup = " select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join (" & _
                       " select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'  " & _
                       " and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code" & _
                       " left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value "

        Dim strSDCommonQuery As String = ""
        Dim strTaxColumns As String = ""
        Dim strSDJoinQry As String = ""
        Dim strSDTaxRate As String = ""
        Dim strSDTaxRateColumn As String = ""
        Dim strSDTaxRateBlankColumn As String = ""
        strSDTaxRateBlankColumn = " '' as _Type ,"
        strSDTaxRateColumn = "  ttr._Type ,"
        Dim strSDEndQry As String = ",TSPL_SD_SALE_INVOICE_DETAIL.TAX1+'%' as Tax1_Rate"

        ' ''===================added by preeti gupta against ticket no[BM00000009858]
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowParticluarColumnInSalesRegisterForGopalJee, clsFixedParameterCode.ShowParticluarColumnInSalesRegisterForGopalJee, Nothing)) = 1 Then
            strMCCMaterial = " select [Trans Type],[Document_date],[Document No],[Narration],[Location Code]as [Warehouse Code], [Location Name] as [Warehouse Name],Cust.Cust_Group_Code as [Customer Group Code],Cust_Group.Cust_Group_Desc as [Customer Group Description],[Customer Code],[Customer Name],Item_Group.Item_Group as [Product Group Code],Item_Group.Group_Description as [Product Group Description],Vehicle_Code as [Vehicle Code],Vehicle_No as [Vehicle No] "

            strMCCMaterial += " , [Item Code] as [Product Code],[Item Name] as [Product Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(case when coalesce(TransStock.Conversion_Factor,1)=0 then 1 else coalesce(TransStock.Conversion_Factor,1) end) as Numeric(18,3)) as [Pack Sold]," & IIf(clsCommon.myLen(Unit_Code) <= 0, IIf(Stock_uom = True, "TransStock.UOM_Code", "xx.[UOM]"), "'" & Unit_Code & "'") & " as [Unit of sale],"
            If clsCommon.myLen(Unit_Code) <= 0 AndAlso Not Stock_uom Then
                strMCCMaterial += " cast(([Item Cost]*Stock_SU.Conversion_Factor)/(case when coalesce(rate_stock_su.Conversion_Factor,1)=0 then 1 else coalesce(rate_stock_su.Conversion_Factor,1) end) as Numeric(18,3))  as [Rate] "
            Else
                strMCCMaterial += "  cast(( case when isnull(Rate_Stock_SU.Conversion_Factor,0)<=0 then ([Item Cost]) else ([Item Cost] * Rate_select_SU.Conversion_Factor)/ Rate_Stock_SU.Conversion_Factor end )"
                strMCCMaterial += " as Numeric(18,3)) as [Rate]"
            End If
            strMCCMaterial += " ,Amount " + strPivotForFinalOuterQuery + " ,[Total Tax Amount], [Total Amount] as [Total Amount],'' as Remarks,''as Relation,'' as CTYPE "

        Else
            strMCCMaterial = " select case when len([Form Type])>0 then [Form Type] else   _Type end  as [Form Type],[Trans Type],[Location Code],[Location Name],Loc.State as [Location State],GSTState.GST_STATE_Code as [GST State Code],loc.GSTNO as [Dispatch Location GSTIN No], (CASE WHEN [Invoice Type]='T' THEN 'Tax' when [Invoice Type]='R' then 'Retail' when [Invoice Type]='N' then 'None' else [Invoice Type] end) as [Invoice Type],[Document No],[Document_date],[Narration],Vehicle_Code as [Vehicle Code],Vehicle_No as [Vehicle No],cast(Additional_Charge as numeric(18,2)) as [Additional Amount],[Customer Code],[Customer Name],[Customer Address],Cust.Struct_Code,xx.[Registered],[Composition],[City Code],[Place of Supply],[Customer GST State Code],Cust.Cust_Group_Code as [Customer Group Code],Cust_Group.Cust_Group_Desc as [Customer Group Description],Cust.Zone_Code as [Customer Zone Code],Zone.Description as [Customer Zone Description], [Parent Customer No],[Parent Customer Code], [Parent Customer Name],coalesce(Cust.State_Code,Cust_Loc.State_Code) as [Customer State Code],coalesce(Cust.State_Name,Cust_Loc.State_Name) as [Customer State Desc],coalesce(Cust.Tin_No,cust_loc.Tin_No) as [Tin No],Item_Group.Item_Group as [Item Group Code],Item_Group.Group_Description as [Item Group Description] "
            If clsCommon.myLen(strCategoryTable) > 0 Then
                strMCCMaterial += "," + strCodeColumnForVirtual + "," + strCodeDescColumn
            End If
            strMCCMaterial += " , [Item Code],[Item Name],[HSN Code],cast(([Quantity]*Stock_SU.Conversion_Factor)/(case when coalesce(TransStock.Conversion_Factor,1)=0 then 1 else coalesce(TransStock.Conversion_Factor,1) end) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, IIf(Stock_uom = True, "TransStock.UOM_Code", "xx.[UOM]"), "'" & Unit_Code & "'") & " as [UOM],"
            If clsCommon.myLen(Unit_Code) <= 0 AndAlso Not Stock_uom Then
                strMCCMaterial += " cast(([Item Cost]*Stock_SU.Conversion_Factor)/(case when coalesce(rate_stock_su.Conversion_Factor,1)=0 then 1 else coalesce(rate_stock_su.Conversion_Factor,1) end) as Numeric(18,3))  as [Item Rate] "
            Else
                strMCCMaterial += "  cast(( case when isnull(Rate_Stock_SU.Conversion_Factor,0)<=0 then ([Item Cost]) else ([Item Cost] * Rate_select_SU.Conversion_Factor)/ Rate_Stock_SU.Conversion_Factor end )"
                strMCCMaterial += " as Numeric(18,3)) as [Item Rate]"
            End If
            strMCCMaterial += " ,case when [trans type] in ('Bulk Sale Trade','Bulk Sale','Bulk Sale Return','MCC Transfer','SS','Tanker Dispatch Return','MCC Tanker Dispatch Return') then [Fat Per] else Item.STD_FATPER end as [FAT %],case when [trans type] in ('Bulk Sale Trade','Bulk Sale','Bulk Sale Return','MCC Transfer','SS','Tanker Dispatch Return','MCC Tanker Dispatch Return') then [SNF Per] else Item.STD_SNFPer end as [SNF %],(case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast(([Quantity]* (case when [trans type] in ('Bulk Sale Trade','Bulk Sale','Bulk Sale Return','MCC Transfer','SS','Tanker Dispatch Return','MCC Tanker Dispatch Return') then [Fat Per] else Item.STD_FATPER end) *Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as numeric(18,3)) end) as [FAT KG],(case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast(([Quantity]* (case when [trans type] in ('Bulk Sale Trade','Bulk Sale','Bulk Sale Return','MCC Transfer','SS','Tanker Dispatch Return','MCC Tanker Dispatch Return') then [SNF Per] else Item.STD_SNFPer end) *Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Numeric(18,3)) end) as [SNF KG],Amount,[Discount Per] as [Discount %],  (coalesce( [Discount Amount],0)-coalesce([Scheme Amount],0))  as [Discount Amount],[Scheme Amount],[Amount Less Discount]  as [Amount Less Discount]" + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + ", "
            strMCCMaterial += "  case  when [trans type] in  ('Fresh Sale','Fresh Sale Return')  then  [Amount Less Discount] + coalesce( [Scheme Amount],0)   else ([Total Amount]-[Total Tax Amount]) end as [Sale Amount], " & _
            " case  when [trans type] in ('Fresh Sale','Fresh Sale Return') then  [Amount Less Discount]  else ([Total Amount]-[Total Tax Amount]+MANDI_TAX_AMT) end  as [Sale Amount GST] ,  "
            '"case when [trans type] in ('Fresh Sale Return','Fresh Sale') then isnull( [Scheme Amount],0)  else 0 end + case when [trans type]='Fresh Sale Return' then [Amount Less Discount] + ((coalesce( [Discount Amount],0)-coalesce([Scheme Amount],0)) )  else ([Total Amount]-[Total Tax Amount]) end as [Sale Amount], " & _
            '"case when [trans type] in ('Fresh Sale Return','Fresh Sale') then isnull( [Scheme Amount],0)  else 0 end +([Total Amount]-[Total Tax Amount]+MANDI_TAX_AMT) + case when [trans type]='Fresh Sale Return' then  ((coalesce( [Discount Amount],0)-coalesce([Scheme Amount],0)) )  else 0 end  as [Sale Amount GST], " & _
            strMCCMaterial += "[Total Tax Amount], (cast(Additional_Charge as numeric(18,2))+[Total Amount]) as [Total Amount], " & _
            " [AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax]+ case when (coalesce([Total Tax Amount],0)<>0 or [Scheme Amount]<=0) and [Document No]<>'SRFS-003/15-16/000006' then 0 else coalesce([AR Document Discount Amt],0)  end as [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge], "
            strMCCMaterial += "  case when [trans type] in ('CSA Sale','CSA Sale Return')  then (case when coalesce(item.GSOC_Acct,'')<>'' then  left( item.GSOC_Acct, Len( item.GSOC_Acct)-3)+  TSPL_LOCATION_MASTER.Loc_Segment_Code else '' end)   " + Environment.NewLine + _
            " else case when [trans type] in ('Transfer','Transfer Return') then (case when coalesce(item.Stock_Transfer_AC,'')<>'' then  left( item.Stock_Transfer_AC, Len( item.Stock_Transfer_AC)-3)+  TSPL_LOCATION_MASTER.Loc_Segment_Code else '' end)  " + Environment.NewLine + _
            " else left(Item.Sales_Account, Len(Item.Sales_Account)-3)+  TSPL_LOCATION_MASTER.Loc_Segment_Code end  end as [Sales Account], " & _
            " [GR No],case when len (isnull([GR No],''))>0 then convert(varchar,[GR Date],103) else '' end as [GR Date],[WayBill No],[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount], [Price Code], case when Sampling=0 then  'N' else case when sampling=1 then'Y' end end as sampling, Scheme_Item as [Scheme Type],[Invoice Type GST], " & _
            " '" & CompGstinNo & "' [GSTIN No Company],[GSTIN no Customer],[Nill Rate Amount],[Exempted Amount],[Non GST Supply],[Reverse Charge],[Export Type],Port,[Shipping Bill No],[Shipping Bill Date],[Original Invoice No],[Original Invoice Date],[Reason for Revision], [LUT No] "
        End If


        '' ''richa agarwal add merchant trade trans_type in below qry BM00000008390 (Applied For DCC Also) 
        strMCCMaterial += " from ( "

        '' add csa transfer 
        If obj.Trans_Type_List.Contains("CSA Sale") AndAlso obj.PickCSASaleFromSalePatti = False Then
            Dim stCSACommonQuery As String = " select '' as Narration,TSPL_CSA_TRANSFER_HEAD.Against_Form as [Formtype],'CSA Sale' as  Trans_Type,TSPL_CSA_TRANSFER_HEAD.Status ,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Bill_To_Location," & _
                                              " TSPL_CSA_TRANSFER_HEAD.Cust_Code as Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,'CSA' as Invoice_Type,TSPL_CSA_TRANSFER_HEAD.Doc_Code as Document_Code ," & _
                                              " convert(varchar,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103 ) as Document_Date , TSPL_CSA_TRANSFER_DETAIL.Item_Code , " & _
                                              " TSPL_CSA_TRANSFER_DETAIL.Qty ,TSPL_CSA_TRANSFER_DETAIL.Unit_code ,TSPL_CSA_TRANSFER_DETAIL.Transfer_Rate*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Item_Cost , " & _
                                              " coalesce(TSPL_CSA_TRANSFER_DETAIL.transfer_Rate*TSPL_CSA_TRANSFER_DETAIL.Qty,0)*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Amount ,coalesce(TSPL_CSA_TRANSFER_DETAIL.Disc_Per,0) as Disc_Per ,coalesce(TSPL_CSA_TRANSFER_DETAIL.Disc_Amt,0)*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Disc_Amt ,0 as [Scheme Amount] " & _
                                              ", (coalesce(TSPL_CSA_TRANSFER_DETAIL.transfer_Rate*TSPL_CSA_TRANSFER_DETAIL.Qty,0)-coalesce(TSPL_CSA_TRANSFER_DETAIL.Disc_Amt,0))*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Amt_Less_Discount ,0 as Total_Tax_Amt ,cast((TSPL_CSA_TRANSFER_DETAIL.Transfer_Rate * TSPL_CSA_TRANSFER_DETAIL.Qty) as Numeric(18,2))*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Total_Amt, " & _
                                              " TSPL_CSA_TRANSFER_HEAD.Vehicle_Id as Vehicle_Code,TSPL_VEHICLE_MASTER.Number as Vehicle_No,0 as Additional_Charge, " & _
                                              " '' as [AR Document No],0 as [AR Document Amt],0 as [AR Document Discount Amt], 0 as [AR Amount Before Tax],0 as [AR Total Tax],0 as [AR Total Add Charge],TSPL_CSA_TRANSFER_HEAD.GR_No as [GR No],TSPL_CSA_TRANSFER_HEAD.gr_date as [GR Date],TSPL_CSA_TRANSFER_HEAD.WayBill_No as [WayBill No],TSPL_CSA_TRANSFER_HEAD.Transport_Id as [Transporter Code] , case when len(TSPL_CSA_TRANSFER_HEAD.transporter_name_manual) > 0 then TSPL_CSA_TRANSFER_HEAD.transporter_name_manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],TSPL_CSA_TRANSFER_DETAIL.MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],TSPL_CSA_TRANSFER_HEAD.Created_By ,TSPL_CSA_TRANSFER_HEAD.Modify_By,TSPL_CSA_TRANSFER_DETAIL.Unit_code as RATE_UOM,0 as Conv_Factor, " & _
                                              " 'Delivery Challan' as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],TSPL_CUSTOMER_MASTER.GSTNO as [GSTIN No Customer],(case when TSPL_CSA_TRANSFER_HEAD.Total_Tax_Amt<=0 and TSPL_CSA_TRANSFER_HEAD.Tax_Group<>'EXEMPTED' then coalesce(TSPL_CSA_TRANSFER_DETAIL.transfer_Rate*TSPL_CSA_TRANSFER_DETAIL.Qty,0)*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) else 0 end) as [Nill Rate Amount],(case when TSPL_CSA_TRANSFER_HEAD.Tax_Group='EXEMPTED' then coalesce(TSPL_CSA_TRANSFER_DETAIL.transfer_Rate*TSPL_CSA_TRANSFER_DETAIL.Qty,0)*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) else 0 end) as [Exempted Amount],cast(null as numeric(18,2)) as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],(CASE WHEN TAXM1.TYPE='M' THEN TSPL_CSA_TRANSFER_DETAIL.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='M' THEN TSPL_CSA_TRANSFER_DETAIL.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='M' THEN TSPL_CSA_TRANSFER_DETAIL.TAX3_AMT ELSE 0 END) AS MANDI_TAX_AMT, "

            strSDEndQry = " from TSPL_CSA_TRANSFER_DETAIL " & _
                        " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE =TSPL_CSA_TRANSFER_DETAIL.DOC_CODE " & _
                        " left join TSPL_VEHICLE_MASTER on TSPL_CSA_TRANSFER_HEAD.Vehicle_Id=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                        " left join TSPL_TRANSPORT_MASTER on TSPL_CSA_TRANSFER_HEAD.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id " & _
                        " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CSA_TRANSFER_HEAD.Cust_Code " & _
                        " LEFT JOIN TSPL_TAX_MASTER TAXM1 ON TSPL_CSA_TRANSFER_DETAIL.TAX1=TAXM1.TAX_CODE " & _
                        " LEFT JOIN TSPL_TAX_MASTER TAXM2 ON TSPL_CSA_TRANSFER_DETAIL.TAX2=TAXM2.TAX_CODE " & _
                        " LEFT JOIN TSPL_TAX_MASTER TAXM3 ON TSPL_CSA_TRANSFER_DETAIL.TAX3=TAXM3.TAX_CODE "

            strSDJoinQry = " where 'CSA Sale' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                        " and convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) <= convert(date,('" & To_Date & "'),103) "
            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_CSA_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_CSA_TRANSFER_HEAD.From_Location_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strSDJoinQry += " and TSPL_CSA_TRANSFER_HEAD.Doc_Code = '" & obj.Document_Code & "' "
            End If
            If qryStarted = True Then
                strMCCMaterial += " union all " '' base union 2
            End If
            qryStarted = True
            strMCCMaterial += " select max(final._Type) as _Type, max(final.FormType) as [Form Type],Trans_Type  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date],max(final.Narration) as [Narration],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name],max(TSPL_CUSTOMER_MASTER .GST_Registered) as [Registered],max(TSPL_CUSTOMER_MASTER .GST_COMPOSITION) as [Composition],max(TSPL_CUSTOMER_MASTER .City_Code) as [City Code],max(TSPL_CITY_MASTER .City_Name) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], "

            ''Monika QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
            strMCCMaterial += " 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount],   " & _
                              " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount], [Price Code],final.Created_By ,final.Modify_By,final. RATE_UOM,final. Conv_Factor,0 as Sampling,'N' as Scheme_Item, " & _
                              " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],'' as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT"
            strMCCMaterial += " from ("
            'strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX1 ,0 as TAX1_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX1+'%' as Tax1Rate"
            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            '' query for no tax applied& strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry &

            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & "  and (coalesce(TSPL_CSA_TRANSFER_DETAIL.tax1,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax3,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_CSA_TRANSFER_DETAIL.tax5,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_CSA_TRANSFER_DETAIL.tax7,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_CSA_TRANSFER_DETAIL.tax9,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax10,'')='') )t  "
            '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            '' query for tax1 applied
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX1 ,0 as TAX1_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX1+'%' as Tax1Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax1 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T' "
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and TSPL_CSA_TRANSFER_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX2 ,0 as TAX2_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX2+'%' as Tax2Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax2 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_CSA_TRANSFER_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX3 ,0 as TAX3_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX3+'%' as Tax3Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax3 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_CSA_TRANSFER_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX4 ,0 as TAX4_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX4_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX4+'%' as Tax4Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax4 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX4_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_CSA_TRANSFER_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX5 ,0 as TAX5_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX5_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX5+'%' as Tax5Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax5 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX5_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_CSA_TRANSFER_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX6 ,0 as TAX6_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX6_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX6+'%' as Tax6Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax6 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX6_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_CSA_TRANSFER_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX7 ,0 as TAX7_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX7_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX7+'%' as Tax7Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax7 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX7_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_CSA_TRANSFER_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX8 ,0 as TAX8_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX8_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX8+'%' as Tax8Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax8 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX8_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_CSA_TRANSFER_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX9_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX9+'%' as Tax9Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax9 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX9_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_CSA_TRANSFER_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX10 ,0 as TAX10_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX10_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX10+'%' as Tax10Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_CSA_TRANSFER_DETAIL.tax10 and ttr.tax_Rate=TSPL_CSA_TRANSFER_DETAIL.TAX10_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & stCSACommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_CSA_TRANSFER_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " )final"
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
            strMCCMaterial += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                              " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "

            'added by stuti on 01/05/2017
            strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If

            strMCCMaterial += " group by  final.Trans_Type,final.Status  ,final.Document_Code ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount], [Price Code],final.Created_By ,final.Modify_By,final. RATE_UOM,final.Conv_Factor" '', " + strPivotFoGrouprOuterQuery + " ,QC.FAT_Per,QC.SNF_Per
        End If
        '' end of csa transfer

        '' base union 3
        If obj.Trans_Type_List.Contains("Bulk Sale") OrElse obj.Trans_Type_List.Contains("Bulk Sale Trade") Then
            If qryStarted = True Then
                strMCCMaterial += " union all"
            End If
            qryStarted = True
            strMCCMaterial += " select '' as _Type,'' as [Form Type], case when InvoiceAgainst='Against Dispatch Trade' then 'Bulk Sale Trade'  else 'Bulk Sale' end as [Trans Type] ,coalesce(TSPL_LOCATION_MASTER.Main_Location_Code,TSPL_LOCATION_MASTER.Location_Code) as [Location Code],TSPL_INVOICE_Master_BULKSALE.Posted as Status,coalesce(Main_Loc.Location_Desc,TSPL_LOCATION_MASTER.location_desc) as [Location Name] ,'Invoice' as [Invoice Type] ,TSPL_INVOICE_Master_BULKSALE.Document_No as [Document No] ,convert(varchar,TSPL_INVOICE_Master_BULKSALE.Document_Date,103) as [Document_date],'' as [Narration],case when isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')='' then TSPL_INVOICE_DETAIL_BULKSALE.TradeTanker_No else isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')  end as Vehicle_Code,case when isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')='' then TSPL_INVOICE_DETAIL_BULKSALE.TradeTanker_No else isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')  end as Vehicle_No,(case when ROW_NUMBER() over (partition by TSPL_INVOICE_Master_BULKSALE.Document_No order by TSPL_INVOICE_DETAIL_BULKSALE.Item_Code )=1 then coalesce(TSPL_INVOICE_Master_BULKSALE.RoundOffAmount,0) else 0 end) as Additional_Charge, " & _
                              " TSPL_INVOICE_Master_BULKSALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As [Customer Address] ,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER .GST_Registered as [Registered],TSPL_CUSTOMER_MASTER .GST_COMPOSITION as [Composition],TSPL_CUSTOMER_MASTER .City_Code as [City Code],TSPL_CITY_MASTER .City_Name as [Place of Supply],TSPL_STATE_MASTER.GST_STATE_Code AS [Customer GST State Code] ,TSPL_CUSTOMER_MASTER.Parent_Customer_No as [Parent Customer No],"
            strMCCMaterial += " Parent_Master.Cust_Code as [Parent Customer Code],Parent_Master.Customer_Name as [Parent Customer Name], "
            strMCCMaterial += " TSPL_INVOICE_DETAIL_BULKSALE.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],tspl_item_master.HSN_Code as [HSN Code] ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as [Quantity] ,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code as [UOM],TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate as [Item Cost],TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer as [Fat Per] ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer as [SNF Per] ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatKG as [Fat Kg] ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFKG as [SNF KG] ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amount,0 as [Discount Per],0 as [Discount Amount],0 as [Scheme Amount],TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as [Amount Less Discount] " + strPivotForOuterQueryforBulk + " " + strDoublePivotForOuterQueryforBulk + ",0 as [Total Tax Amount],(TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount) as [Total Amount], " & _
                              " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                              " TSPL_Customer_Invoice_Head.Discount_Amount as [AR Document Discount Amt], " & _
                              " (TSPL_Customer_Invoice_Head.Document_Total-TSPL_Customer_Invoice_Head.total_tax-TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                              " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge],'' as [GR No],null as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],'' as  [Delivery No]  ,'' as  [Shipment No],'' as [Booking No],0 AS MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],TSPL_INVOICE_Master_BULKSALE.Created_By as Created_By ,TSPL_INVOICE_Master_BULKSALE.Modified_By as Modify_By,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code as RATE_UOM,0 as Conv_Factor ,0 as Sampling,'N' as Scheme_Item, " & _
                              " 'Bill of Supply' as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],TSPL_CUSTOMER_MASTER.GSTNO as [GSTIN No Customer],(TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount) as [Nill Rate Amount],cast(null as numeric(18,2)) as [Exempted Amount],cast(null as numeric(18,2)) as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],0 AS MANDI_TAX_AMT " & _
                              " from TSPL_INVOICE_DETAIL_BULKSALE "

            strMCCMaterial += " left outer join TSPL_INVOICE_Master_BULKSALE on TSPL_INVOICE_Master_BULKSALE.Document_No =TSPL_INVOICE_DETAIL_BULKSALE.Document_No "
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_INVOICE_Master_BULKSALE.Location_Code"
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as Main_Loc on TSPL_LOCATION_MASTER.Main_Location_Code =Main_Loc.Location_Code"
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =TSPL_INVOICE_Master_BULKSALE.Customer_Code"
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
            strMCCMaterial += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                              " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =TSPL_INVOICE_DETAIL_BULKSALE.Item_Code"
            strMCCMaterial += " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_INVOICE_Master_BULKSALE.Document_No  where 2=2 " & _
                                " and (case when InvoiceAgainst='Against Dispatch Trade' then 'Bulk Sale Trade'  else 'Bulk Sale' end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                                " and convert(date,TSPL_INVOICE_Master_BULKSALE.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_INVOICE_Master_BULKSALE.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

            ''====Monika
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMCCMaterial += " and TSPL_INVOICE_Master_BULKSALE.Location_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMCCMaterial += " and TSPL_INVOICE_DETAIL_BULKSALE.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strMCCMaterial += " and TSPL_INVOICE_Master_BULKSALE.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            ''=======end here
        End If
        '' base union 4
        If obj.Trans_Type_List.Contains("Misc Sale") Then
            If qryStarted = True Then
                strMCCMaterial += " union all"
            End If
            qryStarted = True
            strMCCMaterial += " select max(final._Type) as _Type,max(final.Formtype) as [Form Type],case when Trans_Type ='FS' then 'Fresh Sale' when Trans_Type ='CSA' then 'CSA Sale' when Trans_Type='PS' then 'Product Sale' when Trans_Type='MCC' then 'MCC Sale' when Trans_Type='Exp' then 'Export Sale'when Trans_Type='Bulk Sale' then 'Bulk Sale' when Trans_Type ='SS' then 'Misc Sale' WHEN (Trans_Type ='SD' or Trans_Type ='ALL') then 'General Sale' else Trans_Type  end  as [Trans Type],final.Loc_Code  as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.shipment_No  as [Document No],final.Document_Date as [Document_date],max(final.Narration) as [Narration],'' as Vehicle_Code,'' as Vehicle_No,0 as Additional_Charge,final.cust_Code  as [Customer Code],MAX(final.CustAdd ) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name],max(TSPL_CUSTOMER_MASTER .GST_Registered) as [Registered],max(TSPL_CUSTOMER_MASTER .GST_COMPOSITION) as [Composition],max(TSPL_CUSTOMER_MASTER .City_Code) as [City Code],max(TSPL_CITY_MASTER .City_Name) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.shipped_Qty  as [Quantity],final.Unit_code as [UOM],final.price  as [Item Cost], "

            ''Monika QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
            strMCCMaterial += " 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.ItemAmt as Amount ,final.DiscountPer  as [Discount Per],final.TotalDiscountAmt   as [Discount Amount],final.[Scheme Amount]   as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.TotalTaxAmt  as [Total Tax Amount],final.Doc_Amt  as [Total Amount], " & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] ,[Cash Scheme Code] ,  [Cash Scheme Amount], [Price Code],final.Created_By ,final.Modify_By,final. RATE_UOM,final.Conv_Factor ,0 as Sampling,'N' as Scheme_Item, " & _
                " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],'' as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT " & _
                " from ("

            Dim strScarpCommonQry As String = ""
            strScarpCommonQry = " select TSPL_SCRAPINVOICE_HEAD.Description as Narration,'' as Formtype,'SS' as Trans_Type,TSPL_SCRAPINVOICE_HEAD.ispost as Status ,TSPL_SCRAPINVOICE_HEAD.Loc_Code,TSPL_SCRAPINVOICE_HEAD.cust_Code " & _
                                " ,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd, " & _
                                " case when isnull(Is_CashSale,'N')='Y' then 'Cash Sale Invoice' when isnull(Is_Scrap,'N')='Y' then 'Scrap Sale Invoice' else 'Misc Sale Invoice' end as Invoice_Type,TSPL_SCRAPINVOICE_HEAD.invoice_No as shipment_No ,convert(varchar,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103 ) as Document_Date , " & _
                                " TSPL_SCRAPINVOICE_DETAIL.Item_Code ,TSPL_SCRAPINVOICE_DETAIL.shipped_Qty ,TSPL_SCRAPINVOICE_DETAIL.Unit_code ," & _
                                " TSPL_SCRAPINVOICE_DETAIL.price ,0 as InvoiceFatPer ,0 as InvoiceSNFPer ,0 as InvoiceFatKG ,0 as InvoiceSNFKG , " & _
                                " TSPL_SCRAPINVOICE_DETAIL.ItemAmt ,TSPL_SCRAPINVOICE_DETAIL.DiscountPer,0 as [Scheme Amount] ,TSPL_SCRAPINVOICE_DETAIL.ItemNetAmt as Amt_less_Discount, " & _
                                " TSPL_SCRAPINVOICE_DETAIL.TotalDiscountAmt , " & _
                                " TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt ,TSPL_SCRAPINVOICE_DETAIL.TotalAmt+Case when TSPL_SCRAPINVOICE_DETAIL.line_No=1 then coalesce(TSPL_SCRAPINVOICE_HEAD.add_Amt,0) else 0 end as Doc_Amt,'' as Vehicle_Code,'' as Vehicle_No,Case when TSPL_SCRAPINVOICE_DETAIL.line_No=1 then coalesce(TSPL_SCRAPINVOICE_HEAD.add_Amt,0) else 0 end as Additional_Charge," & _
                                " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                                " TSPL_Customer_Invoice_Head.Discount_Amount as [AR Document Discount Amt], " & _
                                " TSPL_Customer_Invoice_Head.amount_less_Discount as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                                " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge], " & _
                                " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                                " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'' as [GR No],NULL as [GR Date],'' as [WayBill No],'' as [Transporter Code] ,'' as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],0 AS MRP,  '' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code], '' as Created_By ,'' as Modify_By ,TSPL_SCRAPINVOICE_DETAIL.Unit_code as RATE_UOM,0 as Conv_Factor," & _
                                " (case when TSPL_SCRAPINVOICE_HEAD.Is_Taxable=1 then 'Tax Invoice' else 'Bill Of Supply' end) as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],TSPL_CUSTOMER_MASTER.GSTNO as [GSTIN no Customer],(case when TSPL_SCRAPINVOICE_HEAD.Total_Tax_Amt<=0 and TSPL_SCRAPINVOICE_HEAD.Tax_Group<>'EXEMPTED' then (TSPL_SCRAPINVOICE_DETAIL.TotalAmt+Case when TSPL_SCRAPINVOICE_DETAIL.line_No=1 then coalesce(TSPL_SCRAPINVOICE_HEAD.add_Amt,0) else 0 end) else null end) as [Nill Rate Amount],(case when TSPL_SCRAPINVOICE_HEAD.Tax_Group='EXEMPTED' then (TSPL_SCRAPINVOICE_DETAIL.TotalAmt+Case when TSPL_SCRAPINVOICE_DETAIL.line_No=1 then coalesce(TSPL_SCRAPINVOICE_HEAD.add_Amt,0) else 0 end) else null end) as [Exempted Amount],0 as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],0 AS MANDI_TAX_AMT,"

            strSDEndQry = " from TSPL_SCRAPINVOICE_DETAIL  " & _
                          " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No " & _
                          " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.invoice_No " & _
                          " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SCRAPINVOICE_HEAD.Cust_Code " & _
                          " left outer join TSPL_SCRAPSALE_HEAD on TSPL_SCRAPINVOICE_HEAD.shipment_No=TSPL_SCRAPSALE_HEAD.shipment_No "

            strSDJoinQry = " where 2 = 2 AND 'Misc Sale' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                          " and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= convert(date,('" & To_Date & "'),103) "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SCRAPINVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SCRAPINVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SCRAPINVOICE_HEAD.cust_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strSDJoinQry += " and TSPL_SCRAPINVOICE_HEAD.invoice_No = '" & obj.Document_Code & "' "
            End If

            'sanjay
            If clsCommon.CompairString(objCommonVar.CurrentCompanyCode, "UDL") = CompairStringResult.Equal AndAlso clsCommon.myLen(obj.MiscSaleSubCategory) > 0 Then
                If obj.MiscSaleSubCategory = "S" Then
                    strSDJoinQry += " and ISNULL(IS_SCRAP,'N')='Y' "
                ElseIf obj.MiscSaleSubCategory = "C" Then
                    strSDJoinQry += " and ISNULL(Is_CashSale,'N')='Y' "
                End If
            End If
            'sanjay

            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            '' query for no tax applied



            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax3,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_SCRAPINVOICE_DETAIL.tax5,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_SCRAPINVOICE_DETAIL.tax7,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_SCRAPINVOICE_DETAIL.tax9,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax10,'')='') )t "
            '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            '' query for tax1 applied
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX1 ,TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax1 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax1<>'')"
            strMCCMaterial += " s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX2 ,TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax2 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax2<>'' )"
            strMCCMaterial += " s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = "TSPL_SCRAPINVOICE_DETAIL.TAX3 ,TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt , TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax3 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += "  select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax3<>'' )"
            strMCCMaterial += " s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX4 ,TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax4 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax4<>'' )"
            strMCCMaterial += " s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX5 ,TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax5 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax5<>'' )"
            strMCCMaterial += " s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX6 ,TSPL_SCRAPINVOICE_DETAIL.TAX6_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX6_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax6 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX6_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax6<>'' )"
            strMCCMaterial += " s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX7 ,TSPL_SCRAPINVOICE_DETAIL.TAX7_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX7_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax7 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX7_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax7<>'' )"
            strMCCMaterial += " s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + ")) t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX8 ,TSPL_SCRAPINVOICE_DETAIL.TAX8_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX8_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax8 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX8_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax8<>'' )"
            strMCCMaterial += "  s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX9 ,TSPL_SCRAPINVOICE_DETAIL.TAX9_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX9_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax9 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX9_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += "  select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax9<>'')"
            strMCCMaterial += " s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX10 ,TSPL_SCRAPINVOICE_DETAIL.TAX10_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX10_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPINVOICE_DETAIL.tax10 and ttr.tax_Rate=TSPL_SCRAPINVOICE_DETAIL.TAX10_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strScarpCommonQry & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax10<>'')"
            strMCCMaterial += " s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += " ) final left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Loc_Code  " & _
                " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code  " & _
                " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.cust_Code   " & _
                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No " & _
                " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "


            ''Monika
            ''strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
            'added by stuti on 01/05/2017
            strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

            ''=Monika
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Loc_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strMCCMaterial += " and final.cust_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            ''==end here

            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.shipment_No  ,final.Item_Code ,final.Loc_Code  ,final.cust_Code  ,final.shipped_Qty  ,final.TotalTaxAmt  ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.price  ,final.ItemAmt  ,final.DiscountPer,final.TotalDiscountAmt  ,final.Amt_less_Discount   ,final.Amt_Less_Discount ,final.Doc_Amt,final.[Scheme Amount],Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No] ,MRP ,  [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount], [Price Code],final.Created_By ,final.Modify_By,final.RATE_UOM,final. Conv_Factor " ' ," + strPivotFoGrouprOuterQuery + "  ,QC.FAT_Per,QC.SNF_Per
            '' query for return
        End If
        '=====================Added by preeti Gupta Against Ticket No [ADV/30/04/18-000027]=================
        If obj.Trans_Type_List.Contains("Fresh Sale Return") OrElse obj.Trans_Type_List.Contains("CSA Sale Return") OrElse obj.Trans_Type_List.Contains("Product Sale Return") OrElse obj.Trans_Type_List.Contains("MCC Sale Return") OrElse obj.Trans_Type_List.Contains("Export Sale Return") OrElse obj.Trans_Type_List.Contains("General Sale Return") Then

            Dim strSDRCommonQuery As String = ""
            strSDRCommonQuery = " select (case when len(TSPL_SD_SALE_RETURN_HEAD.Description)<=0 then TSPL_SD_SALE_RETURN_HEAD.Comments else TSPL_SD_SALE_RETURN_HEAD.Description end) as Narration,'' as Formtype,(CASE WHEN TSPL_SD_SALE_RETURN_HEAD.Trans_Type='ALL' THEN 'SDR' ELSE TSPL_SD_SALE_RETURN_HEAD.Trans_Type+'R' END) as Trans_Type,TSPL_SD_SALE_RETURN_HEAD.Status ,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location, " & _
                              " TSPL_SD_SALE_RETURN_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,COALESCE(TSPL_SD_SALE_RETURN_HEAD.Document_Type,TSPL_SD_SALE_RETURN_HEAD.Invoice_Type) AS Invoice_Type,TSPL_SD_SALE_RETURN_HEAD.Document_Code , " & _
                              " convert(varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103 ) as Document_Date , TSPL_SD_SALE_RETURN_DETAIL.Item_Code,TSPL_SD_SALE_RETURN_DETAIL.Line_No , " & _
                              " -TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code ,TSPL_SD_SALE_RETURN_DETAIL.Item_Cost , " & _
                              " -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Amount,0) as Amount ,TSPL_SD_SALE_RETURN_DETAIL.Disc_Per ,case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0)=0 then -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0)  + case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then 1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) else 0 end else -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0) end as Disc_Amt,case when coalesce(FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then -1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) end  as [Scheme Amount] , " & _
                              " -(Amount- case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' then Total_Disc_Amt else Total_Disc_Amt end  + case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type<>'FS' then case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then Item_Net_Amt*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) else 0 end else 0 end)  as Amt_Less_Discount , " & _
                              " -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0) as Total_Tax_Amt ,-(Amount+coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0)- case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' then TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt else coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0) end )  as Total_Amt,TSPL_SD_SALE_RETURN_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Number as Vehicle_No,-(case when TSPL_SD_SALE_RETURN_DETAIL.Line_No=1 then (coalesce(TSPL_SD_SALE_RETURN_HEAD.Total_Add_Charge,0)+coalesce(TSPL_SD_SALE_RETURN_HEAD.RoundOffAmount,0)) else 0 end) as Additional_Charge, " & _
                              " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],-TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                              " -TSPL_Customer_Invoice_Head.Discount_Amount-coalesce(TSPL_SD_SALE_RETURN_HEAD.headDisc_AMt,0) as [AR Document Discount Amt], " & _
                              " -TSPL_Customer_Invoice_Head.amount_less_Discount as [AR Amount Before Tax],-TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                              " -(TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge], " & _
                              " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                              " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,TSPL_SD_SALE_RETURN_HEAD.GRNo as [GR No],TSPL_SD_SHIPMENT_HEAD.gr_date as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],TSPL_SD_SALE_RETURN_DETAIL.Delivery_Code as [Delivery No],Against_Shipment_No as [Shipment No],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No as [Booking No],TSPL_SD_SALE_RETURN_DETAIL.MRP, TSPL_SD_SALE_RETURN_DETAIL.Scheme_Code ,TSPL_SD_SALE_RETURN_DETAIL.Cash_Scheme_Code ,TSPL_SD_SALE_RETURN_DETAIL.Cash_Scheme_Amount*(-1) as Cash_Scheme_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Price_code ,'' as Created_By ,'' as Modify_By,TSPL_SD_SALE_RETURN_DETAIL.Unit_code as RATE_UOM,0 as Conv_Factor, TSPL_SD_SALE_RETURN_DETAIL.Sampling,TSPL_SD_SALE_RETURN_DETAIL.Scheme_Item," & _
                              " 'Credit Note' as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],TSPL_CUSTOMER_MASTER.GSTNO as [GSTIN no Customer],(case when TSPL_SD_SALE_RETURN_HEAD.Total_Tax_Amt<=0 and TSPL_SD_SALE_RETURN_HEAD.Tax_Group<>'EXEMPTED' then -(Amount+coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0)- case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' then 0 else coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0) end ) else null end) as [Nill Rate Amount],(case when TSPL_SD_SALE_RETURN_HEAD.Tax_Group='EXEMPTED' then -(Amount+coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0)- case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' then 0 else coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0) end ) else null end) as [Exempted Amount],0 as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date], " & _
                              "  case  when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='CSA' then  TSPL_SD_SALE_RETURN_DETAIL.Transfer_No else  TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code end  as [Original Invoice No],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [Original Invoice Date],TSPL_SD_SALE_RETURN_HEAD.Description as [Reason for Revision],'' as [LUT No],(CASE WHEN TAXM1.TYPE='M' THEN TSPL_SD_SALE_RETURN_DETAIL.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='M' THEN TSPL_SD_SALE_RETURN_DETAIL.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='M' THEN TSPL_SD_SALE_RETURN_DETAIL.TAX3_AMT ELSE 0 END) AS MANDI_TAX_AMT,"
            strSDEndQry = " from TSPL_SD_SALE_RETURN_DETAIL " & _
                                " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE " & _
                                " left join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_RETURN_HEAD.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                                " left join TSPL_Customer_Invoice_Head on TSPL_SD_SALE_RETURN_HEAD.Document_Code=  case when len(isnull(TSPL_Customer_Invoice_Head.Against_Sale_Return_No,''))>0  then TSPL_Customer_Invoice_Head.Against_Sale_Return_No else TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return end " & _
                                " left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code " & _
                                " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No " & _
                                " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_SD_SALE_RETURN_DETAIL.Delivery_Code " & _
                                " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code " & _
                                " LEFT JOIN TSPL_TAX_MASTER TAXM1 ON TSPL_SD_SALE_RETURN_DETAIL.TAX1=TAXM1.TAX_CODE " & _
                                " LEFT JOIN TSPL_TAX_MASTER TAXM2 ON TSPL_SD_SALE_RETURN_DETAIL.TAX2=TAXM2.TAX_CODE " & _
                                " LEFT JOIN TSPL_TAX_MASTER TAXM3 ON TSPL_SD_SALE_RETURN_DETAIL.TAX3=TAXM3.TAX_CODE "

            strSDJoinQry = " WHERE 2=2 AND (case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='FS' then 'Fresh Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='CSA' then 'CSA Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='PS' then 'Product Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' then 'MCC Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='EXP' then 'Export Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='Bulk Sale' then 'Bulk Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='SS' then 'Misc Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type in ('SD','All') then 'General Sale Return' else TSPL_SD_SALE_RETURN_HEAD.trans_Type  end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                            " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SD_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strSDJoinQry += " and TSPL_SD_SALE_RETURN_HEAD.Document_Code = '" & obj.Document_Code & "' "
            End If

            '' base union 5
            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True
            strMCCMaterial += " select max(final._Type) as _Type, max(final.Formtype) as [Form Type],case when Trans_Type ='FSR' then 'Fresh Sale Return' when Trans_Type ='CSAR' then 'CSA Sale Return' when Trans_Type='PSR' then 'Product Sale Return' when Trans_Type='MCCR' then 'MCC Sale Return' when Trans_Type='EXPR' then 'Export Sale Return'when Trans_Type='Bulk Sale' then 'Bulk Sale Return' when Trans_Type ='SSR' then 'Misc Sale' when Trans_Type ='SDR' then 'General Sale Return' else trans_Type  end  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date],max(final.Narration) as [Narration],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name],max(TSPL_CUSTOMER_MASTER .GST_Registered) as [Registered],max(TSPL_CUSTOMER_MASTER .GST_COMPOSITION) as [Composition],max(TSPL_CUSTOMER_MASTER .City_Code) as [City Code],max(TSPL_CITY_MASTER .City_Name) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost],"

            ''Monika QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
            strMCCMaterial += "  0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount], " & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]  - case when (Trans_Type ='FSR' or Trans_Type ='PSR') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],final.[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, Scheme_Code as [Scheme Code] ,Cash_Scheme_Code as [Cash Scheme Code], Cash_Scheme_Amount as [Cash Scheme Amount] ,final.Price_code as [Price Code] ,final.Created_By ,final.Modify_By,final.RATE_UOM ,final.Conv_Factor ,final. Sampling,final.Scheme_Item," & _
                " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],'' as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT " & _
                " from ( "

            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            '' query for no tax applied
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax3,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax5,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax7,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax9,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax10,'')='') )t "

            strMCCMaterial += "   union all"
            '' query for tax1 applied
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX1 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt as TAX1_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax1 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX2 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt as TAX2_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax2 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX3 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt as TAX3_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax3 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax7 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX7_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt as TAX4_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax4 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt as TAX5_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax5 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"

            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX6 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt as TAX6_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX6_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax6 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX6_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX7 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt as TAX7_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX7_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax7 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX7_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX8 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt as TAX8_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX8_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax8 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX8_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX9 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt as TAX9_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX9_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax9 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX9_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX10 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt as TAX10_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX10_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_RETURN_DETAIL.tax10 and ttr.tax_Rate=TSPL_SD_SALE_RETURN_DETAIL.TAX10_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " )final"
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
            strMCCMaterial += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                              " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "

            ''Monika
            ''strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
            'added by stuti on 01/05/2017
            strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

            ''=========Monika
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            ''======end here

            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code,final.Line_No ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , Scheme_Code ,Cash_Scheme_Code , Cash_Scheme_Amount ,final.Price_code ,final.Created_By ,final.Modify_By ,final.RATE_UOM,final.Conv_Factor ,final. Sampling,final.Scheme_Item " '', " + strPivotFoGrouprOuterQuery + " ,QC.FAT_Per,QC.SNF_Per
        End If
        '' base union 6
        If obj.Trans_Type_List.Contains("Bulk Sale Return") Then

            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True
            '''' bulk sale return 
            strMCCMaterial += "  select '' as _Type,'' as [Form Type],'Bulk Sale Return' as [Trans Type] ,TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code as [Location Code],TSPL_SALE_RETURN_MASTER_BULKSALE.Posted as Status,TSPL_LOCATION_MASTER.Location_Desc as [Location Name] ,'Invoice' as [Invoice Type] ,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No as [Document No] ,convert(varchar,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) as [Document_date],'' as [Narration],'' as Vehicle_Code,'' as Vehicle_No,coalesce(-1 * TSPL_SALE_RETURN_MASTER_BULKSALE.roundoffamount,0) as Additional_Charge, " & _
                               " TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code as [Customer Code],TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As [Customer Address] ,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_CUSTOMER_MASTER .GST_Registered as [Registered],TSPL_CUSTOMER_MASTER .GST_COMPOSITION as [Composition],TSPL_CUSTOMER_MASTER .City_Code as [City Code],TSPL_CITY_MASTER .City_Name as [Place of Supply],TSPL_STATE_MASTER.GST_STATE_Code AS [Customer GST State Code] ,TSPL_CUSTOMER_MASTER.Parent_Customer_No as [Parent Customer No]," & _
                               " Parent_Master.Cust_Code as [Parent Customer Code],Parent_Master.Customer_Name as [Parent Customer Name] ," & _
                               " TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code as [Item Code],tspl_item_master.Item_Desc as [Item Name],tspl_item_master.HSN_Code as [HSN Code] ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceQty as [Quantity] ,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code as [UOM],TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceRate as [Item Cost],TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatPer as [Fat Per] ,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFPer as [SNF Per] ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatKG as [Fat Kg] ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFKG as [SNF KG]  ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as Amount,0 as [Discount Per],0 as [Discount Amount],0 as [Scheme Amount],-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as [Amount Less Discount],0 as [Total Tax Amount] " + strPivotForOuterQueryforBulk + " " + strDoublePivotForOuterQueryforBulk + ",-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as [Total Amount], " & _
                               " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],-1 * TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                               " TSPL_Customer_Invoice_Head.Discount_Amount as [AR Document Discount Amt], " & _
                               " -1 * (TSPL_Customer_Invoice_Head.Document_Total-TSPL_Customer_Invoice_Head.total_tax-TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                               " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge],'' as [GR No],NULL as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],'' as [Delivery No]  ,'' as  [Shipment No],'' as  [Booking No],0 AS MRP, '' as [Scheme Code] ,'' as  [Cash Scheme Code] , 0 as [Cash Scheme Amount], '' as [Price Code] ,TSPL_SALE_RETURN_MASTER_BULKSALE.Created_By as Created_By,TSPL_SALE_RETURN_MASTER_BULKSALE.Modified_By as Modify_By,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code as RATE_UOM,0 as Conv_Factor,0 as Sampling,'N' as Scheme_Item," & _
                               " 'Credit Note' as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],TSPL_CUSTOMER_MASTER.GSTNO as [GSTIN No Customer],-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as [Nill Rate Amount],0 as [Exempted Amount],0 as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo as [Original Invoice No],convert(varchar,TSPL_INVOICE_MASTER_BULKSALE.Document_Date,103) as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],0 AS MANDI_TAX_AMT " & _
                               " from TSPL_SALE_RETURN_DETAIL_BULKSALE "

            strMCCMaterial += " left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No " & _
                              " left join TSPL_INVOICE_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE.InvoiceNo= TSPL_INVOICE_MASTER_BULKSALE.Document_No "
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code"
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code"
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
            strMCCMaterial += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                              " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code"
            strMCCMaterial += " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_Return_No=TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No  where 2=2 " & _
                                " and 'Bulk Sale Return' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                                " and convert(date,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

            ''=========Monika
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMCCMaterial += " and TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMCCMaterial += " and TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strMCCMaterial += " and TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            ''=end here
        End If

        'Sanjay Misc Sale Return start

        If obj.Trans_Type_List.Contains("Misc Sale Return") Then
            strMCCMaterial += Environment.NewLine + " ------------------------- SANJAY START------------" + Environment.NewLine
            Dim strSDRCommonQuery As String = ""

            strSDRCommonQuery = " select '' as Narration,'' as Formtype,'MS-SR' as Trans_Type,TSPL_SCRAPSALE_HEAD_RETURN.ispost as Status ,TSPL_SCRAPSALE_HEAD_RETURN.Loc_Code, " & _
                              " TSPL_SCRAPSALE_HEAD_RETURN.Cust_Code AS Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,COALESCE(TSPL_SCRAPSALE_HEAD_RETURN.Invoice_Type ,'') AS Invoice_Type,TSPL_SCRAPSALE_HEAD_RETURN.Document_No , " & _
                              " convert(varchar,TSPL_SCRAPSALE_HEAD_RETURN.Created_Date,103 ) as Document_Date , TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code,TSPL_SCRAPSALE_DETAIL_RETURN.Line_No , " & _
                              " -TSPL_SCRAPSALE_DETAIL_RETURN.shipped_Qty as Qty ,TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code ,TSPL_SCRAPSALE_DETAIL_RETURN.price , " & _
                              " -coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.ItemAmt,0) as Amount ,TSPL_SCRAPSALE_DETAIL_RETURN.DiscountPer AS Disc_Per ,- coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.TotalDiscountAmt,0) as Disc_Amt,0  as [Scheme Amount] , " & _
                              " TSPL_SCRAPSALE_HEAD_RETURN.Amount_Less_Discount  as Amt_Less_Discount , " & _
                              " -coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt,0) as Total_Tax_Amt ,-(TSPL_SCRAPSALE_DETAIL_RETURN.ItemAmt+coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt,0)- TSPL_SCRAPSALE_DETAIL_RETURN.TotalDiscountAmt) as Total_Amt,TSPL_SCRAPSALE_HEAD_RETURN.Vehicle_Code,TSPL_VEHICLE_MASTER.Number as Vehicle_No,-(case when TSPL_SCRAPSALE_DETAIL_RETURN.Line_No=1 then (coalesce(TSPL_SCRAPSALE_HEAD_RETURN.Add_Amt,0)+coalesce(TSPL_SCRAPSALE_HEAD_RETURN.RoundOffAmount,0)) else 0 end) as Additional_Charge, " & _
                              " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],-TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                              " -TSPL_Customer_Invoice_Head.Discount_Amount-coalesce(TSPL_SCRAPSALE_HEAD_RETURN.Discount_Amt,0) as [AR Document Discount Amt], " & _
                              " -TSPL_Customer_Invoice_Head.amount_less_Discount as [AR Amount Before Tax],-TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                              " -(TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge], " & _
                              " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                              " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'' as [GR No],'' as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],'' as [Delivery No],TSPL_SCRAPSALE_HEAD_RETURN.shipment_No as [Shipment No],'' as [Booking No],0 as MRP, '' as Scheme_Code ,'' as Cash_Scheme_Code ,0 as Cash_Scheme_Amount ,'' as Price_code ,'' as Created_By ,'' as Modify_By,TSPL_SCRAPSALE_DETAIL_RETURN.Unit_code as RATE_UOM,0 as Conv_Factor, '' as Sampling,'' as Scheme_Item," & _
                              " 'Credit Note' as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],TSPL_CUSTOMER_MASTER.GSTNO as [GSTIN no Customer],(case when TSPL_SCRAPSALE_HEAD_RETURN.Total_Tax_Amt<=0 and TSPL_SCRAPSALE_HEAD_RETURN.Tax_Group<>'EXEMPTED' then -(TSPL_SCRAPSALE_DETAIL_RETURN.ItemAmt+coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt,0)- coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.TotalDiscountAmt,0)) else null end) as [Nill Rate Amount],(case when TSPL_SCRAPSALE_HEAD_RETURN.Tax_Group='EXEMPTED' then -(TSPL_SCRAPSALE_DETAIL_RETURN.ItemAmt+coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.TotalTaxAmt,0)- coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.TotalDiscountAmt,0)) else null end) as [Exempted Amount],0 as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],TSPL_SCRAPSALE_HEAD_RETURN.invoice_No as [Original Invoice No],convert(varchar,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103) as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No] " & _
                              ",(CASE WHEN TAXM1.TYPE='M' THEN TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='M' THEN TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='M' THEN TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_AMT ELSE 0 END) AS MANDI_TAX_AMT,"



            strSDEndQry = " from TSPL_SCRAPSALE_DETAIL_RETURN "
            strSDEndQry += " left outer join TSPL_SCRAPSALE_HEAD_RETURN on TSPL_SCRAPSALE_HEAD_RETURN.Document_No =TSPL_SCRAPSALE_DETAIL_RETURN.Document_No " & _
                              " left join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPSALE_HEAD_RETURN.invoice_No= TSPL_SCRAPINVOICE_HEAD.invoice_No "
            strSDEndQry += " left join TSPL_VEHICLE_MASTER on TSPL_SCRAPSALE_HEAD_RETURN.Vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id "
            strSDEndQry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_SCRAPSALE_HEAD_RETURN.Loc_Code"
            strSDEndQry += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =TSPL_SCRAPSALE_HEAD_RETURN.Cust_Code"
            strSDEndQry += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
            strSDEndQry += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                              " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "
            strSDEndQry += " left outer join tspl_item_master on tspl_item_master.Item_Code =TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code"
            strSDEndQry += " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.AgainstScrapReturn=TSPL_SCRAPSALE_HEAD_RETURN.Document_No  "
            strSDEndQry += " LEFT JOIN TSPL_TAX_MASTER TAXM1 ON TSPL_SCRAPSALE_DETAIL_RETURN.TAX1=TAXM1.TAX_CODE " & _
                                " LEFT JOIN TSPL_TAX_MASTER TAXM2 ON TSPL_SCRAPSALE_DETAIL_RETURN.TAX2=TAXM2.TAX_CODE " & _
                                " LEFT JOIN TSPL_TAX_MASTER TAXM3 ON TSPL_SCRAPSALE_DETAIL_RETURN.TAX3=TAXM3.TAX_CODE "

            strSDJoinQry = " WHERE 2=2 AND 'Misc Sale Return' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                            " and convert(date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SCRAPSALE_HEAD_RETURN.Return_ship_Date,103) <= convert(date,('" & To_Date & "'),103) "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SCRAPSALE_DETAIL_RETURN.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SCRAPSALE_HEAD_RETURN.Loc_code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SCRAPSALE_HEAD_RETURN.Cust_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strSDJoinQry += " and TSPL_SCRAPSALE_HEAD_RETURN.Document_No = '" & obj.Document_Code & "' "
            End If


            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True


            ''''''skg start

            strMCCMaterial += " select max(final._Type) as _Type, max(final.Formtype) as [Form Type],'Misc Sale Return' as [Trans Type],final.Loc_code as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_No as [Document No],final.Document_Date as [Document_date],max(final.Narration) as [Narration],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name],max(TSPL_CUSTOMER_MASTER .GST_Registered) as [Registered],max(TSPL_CUSTOMER_MASTER .GST_COMPOSITION) as [Composition],max(TSPL_CUSTOMER_MASTER .City_Code) as [City Code],max(TSPL_CITY_MASTER .City_Name) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.Qty as [Quantity],final.Unit_code as [UOM],final.price as [Item Cost],"

            ''Monika QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
            strMCCMaterial += "  0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],- final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount], " & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]  - case when (Trans_Type ='FSR' or Trans_Type ='PSR') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],final.[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, Scheme_Code as [Scheme Code] ,Cash_Scheme_Code as [Cash Scheme Code], Cash_Scheme_Amount as [Cash Scheme Amount] ,final.Price_code as [Price Code] ,final.Created_By ,final.Modify_By,final.RATE_UOM ,final.Conv_Factor ,final. Sampling,final.Scheme_Item," & _
                " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],'' as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT " & _
                " from ( "

            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            '' query for no tax applied
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax1,'')='' and coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax2,'')='' " & _
                              " and coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax3,'')='' and coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax4,'')='' and " & _
                              " coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax5,'')='' and coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax6,'')='' and " & _
                              " coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax7,'')='' and coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax8,'')='' and " & _
                              " coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax9,'')='' and coalesce(TSPL_SCRAPSALE_DETAIL_RETURN.tax10,'')='') )t "

            strMCCMaterial += "   union all"
            '' query for tax1 applied
            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX1 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Amt as TAX1_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax1 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX1_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX2 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Amt as TAX2_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax2 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX2_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX3 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Amt as TAX3_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax3 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX3_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax7 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX7_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX4 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Amt as TAX4_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax4 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX4_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX5 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Amt as TAX5_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax5 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX5_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"

            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX6 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX6_Amt as TAX6_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX6_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax6 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX6_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX7 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX7_Amt as TAX7_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX7_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax7 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX7_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX8 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX8_Amt as TAX8_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX8_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax8 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX8_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX9 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX9_Amt as TAX9_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX9_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax9 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX9_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_SCRAPSALE_DETAIL_RETURN.TAX10 ,-TSPL_SCRAPSALE_DETAIL_RETURN.TAX10_Amt as TAX10_Amt,TSPL_SCRAPSALE_DETAIL_RETURN.TAX10_Rate ,TSPL_SCRAPSALE_DETAIL_RETURN.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SCRAPSALE_DETAIL_RETURN.tax10 and ttr.tax_Rate=TSPL_SCRAPSALE_DETAIL_RETURN.TAX10_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SCRAPSALE_DETAIL_RETURN.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " )final"
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Loc_code "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
            strMCCMaterial += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                              " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "


            strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Loc_code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If


            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_No,final.Item_Code,final.Line_No ,final.Loc_Code,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code,final.price,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , Scheme_Code ,Cash_Scheme_Code , Cash_Scheme_Amount ,final.Price_code ,final.Created_By ,final.Modify_By ,final.RATE_UOM,final.Conv_Factor ,final. Sampling,final.Scheme_Item "

            ''''''skg end

            strMCCMaterial += Environment.NewLine + " ------------------------- SANJAY END------------" + Environment.NewLine
        End If


        'Sanjay Misc Sale Return end

        If obj.Trans_Type_List.Contains("MCC Transfer") Then

            '' base union 7
            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True
            strMCCMaterial += "   select * " & _
                " from (Select '' as _Type, '' as [Form Type],'MCC Transfer' as [Trans Type] ,TSPL_MCC_Dispatch_Challan.MCC_Code as  [Location Code],TSPL_MCC_Dispatch_Challan.isPosted as " _
                & " Status, sendr.location_desc as  [Location Name],'MCC Transfer' as [Invoice Type],TSPL_MCC_Dispatch_Challan.Chalan_NO as [Document No] , " _
                & " convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103 ) as [Document_date],'' as [Narration], '' as Vehicle_Code,tm.tanker_NO as Vehicle_No,0  as Additional_Charge , " _
                & " TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code as [Customer Code],'' AS [Customer Address],  recv.Location_Desc  as [Customer Name],recv.Registered as [Registered],'' as [Composition],'' as [City Code],recv.City_Code as [Place of Supply],TSPL_STATE_MASTER.GST_STATE_Code AS [Customer GST State Code] ,'' as [Parent Customer No],'' as [Parent Customer Code]," _
                & " '' as [Parent Customer Name], TSPL_MCC_Dispatch_Challan.Item_Code as [Item Code], TSPL_MCC_Dispatch_Challan.Item_Desc as [Item Name],TSPL_ITEM_MASTER.HSN_Code as [HSN Code] , TSPL_MCC_Dispatch_Challan.Net_Qty  as [Quantity] " _
                & " ,TSPL_MCC_Dispatch_Challan.UOM_Code as  [UOM] ,(case when coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)=0 then 0 else round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) " _
                & " * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * " _
                & " coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100)))/coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,1) ,2) end) as  [Item Cost] ,  cast(t_FAT_Recd.Param_Field_Value as numeric(10,2)) as [FAT Per]" _
                & " ,cast(t_SNF_Recd.Param_Field_Value as numeric(10,2)) as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100) " _
                & " as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100) as [SNF KG],amount as  Amount ,0" _
                & " as [Discount Per]  ,0 as [Discount Amount],0 as [Scheme Amount] ,  amount as  [Amount Less Discount]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & ",   0 as [Total Tax Amount] " _
                & " ,amount as   [Total Amount],'' as [AR Document No],0 as [AR Document Amt]" _
                & " ,0 as [AR Document Discount Amt],0 as [AR Amount Before Tax]," _
                & " 0 as [AR Total Tax],0 as [AR Total Add Charge],'' as [GR No],null as [GR Date],'' as [WayBill No]," _
                & " TSPL_MCC_Dispatch_Challan.tanker_No as [Transporter Code],tm.description as [Transporter Name],'' as  [Delivery No],'' as [Shipment No],'' as [Booking No],0 AS MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],'' as Created_By ,'' as Modify_By ,TSPL_MCC_Dispatch_Challan.UOM_Code as RATE_UOM,0 as Conv_Factor,0 as Sampling,'N' as Scheme_Item, " & _
                " case when TSPL_MCC_Dispatch_Challan.GSTStatus='L'  then  'Delivery Challan' else 'Bill of Supply' end  as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],recv.GstNo as  [GSTIN No Customer],cast(null as numeric(18,2)) as [Nill Rate Amount],cast(null as numeric(18,2)) as [Exempted Amount],cast(null as numeric(18,2)) as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],0 AS MANDI_TAX_AMT from TSPL_MCC_Dispatch_Challan " _
                & "   LEFT JOIN tspl_location_Master  sendr ON sendr.Location_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE " _
                & " left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MCC_Dispatch_Challan.mcc_Code left join tspl_Location_master recv on " _
                & " recv.location_code=TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code left join TSPL_STATE_MASTER on recv.State=TSPL_STATE_MASTER.STATE_CODE " _
                & " left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MCC_Dispatch_Challan.Item_Code Left Outer Join " _
                & " (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.* From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On " _
                & " TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') " _
                & " t_SNF_Recd On t_SNF_Recd.Chalan_NO   = TSPL_MCC_Dispatch_Challan.Chalan_NO   Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*" _
                & " From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = " _
                & " TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.Chalan_No " _
                & " = TSPL_MCC_Dispatch_Challan.Chalan_NO where 2=2 " & _
                  " AND 'MCC Transfer' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                  " and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <= convert(date,('" & To_Date & "'),103) )t "

        End If
        ''richa agarwal 25 May,2017
        If obj.Trans_Type_List.Contains("Tanker Dispatch Return") Then

            '' base union 8
            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True
            strMCCMaterial += "   select * from (Select '' as _Type, '' as [Form Type],'Tanker Dispatch Return' as [Trans Type] ,TSPL_MCC_Dispatch_Challan.MCC_Code as  [Location Code],TSPL_MCC_Dispatch_Challan.isPosted as " _
                & " Status, sendr.location_desc as  [Location Name],'Tanker Dispatch Return' as [Invoice Type],TSPL_MCC_Dispatch_Challan_Return.Document_No as [Document No] , " _
                & " convert(varchar,TSPL_MCC_Dispatch_Challan_Return.Document_Date,103 ) as [Document_date],TSPL_MCC_Dispatch_Challan_Return.Remarks as [Narration], '' as Vehicle_Code,tm.tanker_NO as Vehicle_No,0  as Additional_Charge , " _
                & " TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code as [Customer Code],'' AS [Customer Address],  recv.Location_Desc  as [Customer Name],sendr.Registered as [Registered],'' as [Composition],sendr.City_Code as [City Code],sendr.City_Code as [Place of Supply],TSPL_STATE_MASTER.GST_STATE_Code AS [Customer GST State Code] ,'' as [Parent Customer No],'' as [Parent Customer Code]," _
                & " '' as [Parent Customer Name], TSPL_MCC_Dispatch_Challan.Item_Code as [Item Code], TSPL_MCC_Dispatch_Challan.Item_Desc as [Item Name],TSPL_ITEM_MASTER.HSN_Code as [HSN Code] , TSPL_MCC_Dispatch_Challan.Net_Qty * -1  as [Quantity] " _
                & " ,TSPL_MCC_Dispatch_Challan.UOM_Code as  [UOM] ,round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) " _
                & " * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * " _
                & " coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100)))/coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,1) ,2) as  [Item Cost] ,  t_FAT_Recd.Param_Field_Value as [FAT Per]" _
                & " ,t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100) * -1 " _
                & " as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100) * -1 as [SNF KG],amount * -1 as  Amount ,0" _
                & " as [Discount Per]  ,0 as [Discount Amount],0 as [Scheme Amount] ,  amount * -1 as  [Amount Less Discount]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & ",   0 as [Total Tax Amount] " _
                & " ,amount * -1 as   [Total Amount],'' as [AR Document No],0 as  [AR Document Amt]" _
                & " ,0 as [AR Document Discount Amt],0 as [AR Amount Before Tax]," _
                & " 0 as [AR Total Tax],0 as [AR Total Add Charge],'' as [GR No],null as [GR Date],'' as [WayBill No]," _
                & " TSPL_MCC_Dispatch_Challan.tanker_No as [Transporter Code],tm.description as [Transporter Name],'' as  [Delivery No],'' as [Shipment No],'' as [Booking No],0 AS MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],'' as Created_By ,'' as Modify_By,TSPL_MCC_Dispatch_Challan.UOM_Code as RATE_UOM,0 as Conv_Factor,0 as Sampling,'N' as Scheme_Item,'Credit Note' as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],recv.GSTNO as [GSTIN No Customer],cast(null as numeric(18,2)) as [Nill Rate Amount],cast(null as numeric(18,2)) as [Exempted Amount],cast(null as numeric(18,2)) as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],0 AS MANDI_TAX_AMT from TSPL_MCC_Dispatch_Challan  " _
                & " LEFT JOIN tspl_location_Master  sendr ON sendr.Location_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE " _
                & " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.State_Code=sendr.state left join tspl_Location_master recv on " _
                & " recv.location_code=TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code  left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MCC_Dispatch_Challan.Item_Code " _
                & " left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No Left Outer Join " _
                & " (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.* From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On " _
                & " TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') " _
                & " t_SNF_Recd On t_SNF_Recd.Chalan_NO   = TSPL_MCC_Dispatch_Challan.Chalan_NO   Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*" _
                & " From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = " _
                & " TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.Chalan_No " _
                & " = TSPL_MCC_Dispatch_Challan.Chalan_NO left outer join TSPL_MCC_Dispatch_Challan_Return on TSPL_MCC_Dispatch_Challan_Return.Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO where 2=2 and TSPL_MCC_Dispatch_Challan_Return.Challan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO " & _
                  " AND 'Tanker Dispatch Return' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                  " and convert(date,TSPL_MCC_Dispatch_Challan_Return.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_MCC_Dispatch_Challan_Return.Document_Date,103) <= convert(date,('" & To_Date & "'),103) )t "

        End If
        If obj.Trans_Type_List.Contains("MCC Tanker Dispatch Return") OrElse obj.Trans_Type_List.Contains("Tanker Dispatch Return") Then

            '' base union 9
            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True
            strMCCMaterial += "   select * from (Select '' as _Type, '' as [Form Type],'MCC Tanker Dispatch Return' as [Trans Type] ,TSPL_MCC_Tanker_Dispatch_Return_head.MCC_Code as  [Location Code],TSPL_MCC_Tanker_Dispatch_Return_head.isPosted as " _
                & " Status, sendr.location_desc as  [Location Name],'MCC Tanker Dispatch Return' as [Invoice Type],TSPL_MCC_Tanker_Dispatch_Return_head.Return_NO as [Document No] , " _
                & " convert(varchar,TSPL_MCC_Tanker_Dispatch_Return_head.Return_Date ,103 ) as [Document_date],'' as [Narration], '' as Vehicle_Code,tm.tanker_NO as Vehicle_No,0  as Additional_Charge , " _
                & " TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code as [Customer Code],'' AS [Customer Address],  recv.Location_Desc  as [Customer Name],recv.Registered as [Registered],'' as [Composition],'' as [City Code],sendr.City_Code as [Place of Supply],TSPL_STATE_MASTER.GST_STATE_Code AS [Customer GST State Code] ,'' as [Parent Customer No],'' as [Parent Customer Code]," _
                & " '' as [Parent Customer Name], TSPL_MCC_Tanker_Dispatch_Return_head.Item_Code as [Item Code], TSPL_MCC_Tanker_Dispatch_Return_head.Item_Desc as [Item Name],TSPL_ITEM_MASTER.HSN_Code as [HSN Code] , TSPL_MCC_Tanker_Dispatch_Return_head.Net_Qty * -1  as [Quantity] " _
                & " ,TSPL_MCC_Tanker_Dispatch_Return_head.UOM_Code as  [UOM] ,round(((TSPL_MCC_Tanker_Dispatch_Return_head.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) " _
                & " * coalesce(TSPL_MCC_Tanker_Dispatch_Return_head.Net_Qty,0)/100)) +(TSPL_MCC_Tanker_Dispatch_Return_head.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * " _
                & " coalesce(TSPL_MCC_Tanker_Dispatch_Return_head.Net_Qty,0)/100)))/coalesce(TSPL_MCC_Tanker_Dispatch_Return_head.Net_Qty,1) ,2) as  [Item Cost] ,  t_FAT_Recd.Param_Field_Value as [FAT Per]" _
                & " ,t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Tanker_Dispatch_Return_head.Net_Qty,0)/100) * -1 " _
                & " as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Tanker_Dispatch_Return_head.Net_Qty,0)/100) * -1 as [SNF KG],TSPL_MCC_Tanker_Dispatch_Return_head.Amount * -1 as  Amount ,0" _
                & " as [Discount Per]  ,0 as [Discount Amount],0 as [Scheme Amount] ,  TSPL_MCC_Tanker_Dispatch_Return_head.Amount * -1 as  [Amount Less Discount]   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & ",   0 as [Total Tax Amount] " _
                & " ,TSPL_MCC_Tanker_Dispatch_Return_head.Amount * -1 as   [Total Amount],'' as [AR Document No],0 as  [AR Document Amt]" _
                & " ,0 as [AR Document Discount Amt],0 as [AR Amount Before Tax]," _
                & " 0 as [AR Total Tax],0 as [AR Total Add Charge],'' as [GR No],null as [GR Date],'' as [WayBill No]," _
                & " TSPL_MCC_Tanker_Dispatch_Return_head.tanker_No as [Transporter Code],tm.description as [Transporter Name],'' as  [Delivery No],'' as [Shipment No],'' as [Booking No],0 AS MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],'' as Created_By ,'' as Modify_By,TSPL_MCC_Tanker_Dispatch_Return_head.UOM_Code as RATE_UOM,0 as Conv_Factor,0 as Sampling,'N' as Scheme_Item,'Credit Note' as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],recv.GSTNO as [GSTIN no Customer],cast(null as numeric(18,2)) as [Nill Rate Amount],cast(null as numeric(18,2)) as [Exempted Amount],cast(null as numeric(18,2)) as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],0 AS MANDI_TAX_AMT from TSPL_MCC_Dispatch_Challan  " _
                & "  LEFT JOIN tspl_location_Master  sendr ON sendr.Location_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE " _
                & " left join TSPL_STATE_MASTER on sendr.State=TSPL_STATE_MASTER.STATE_CODE left join tspl_Location_master recv on " _
                & " recv.location_code=TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code " _
                & "  left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_MCC_Dispatch_Challan.Item_Code Left Outer Join " _
                & " (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.* From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On " _
                & " TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') " _
                & " t_SNF_Recd On t_SNF_Recd.Chalan_NO   = TSPL_MCC_Dispatch_Challan.Chalan_NO   Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*" _
                & " From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = " _
                & " TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.Chalan_No " _
                & " = TSPL_MCC_Dispatch_Challan.Chalan_NO left outer join TSPL_MCC_Tanker_Dispatch_Return_head on TSPL_MCC_Tanker_Dispatch_Return_head.Chalan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO where 2=2 and TSPL_MCC_Tanker_Dispatch_Return_head.Chalan_No =TSPL_MCC_Dispatch_Challan.Chalan_NO " & _
                  " AND 'MCC Tanker Dispatch Return' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                  " and convert(date,TSPL_MCC_Tanker_Dispatch_Return_head.Return_Date ,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_MCC_Tanker_Dispatch_Return_head.Return_Date ,103) <= convert(date,('" & To_Date & "'),103) )t "

        End If

        If obj.Trans_Type_List.Contains("Transfer") Then

            ''----------------------- end of richa agarwal
            Dim strTranferCommonQuery As String = ""
            strTranferCommonQuery = " select TSPL_TRANSFER_ORDER_HEAD.Description as Narration,case when TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF = 1 then 'F' else '' End as Formtype,'Transfer' as Trans_Type,TSPL_TRANSFER_ORDER_HEAD.Status ,TSPL_TRANSFER_ORDER_HEAD.From_Location as Bill_To_Location, " & _
                                    " COALESCE(GIT_Main_Loc.Location_Code,To_Loc.Location_Code) as To_Location,TSPL_TRANSFER_ORDER_HEAD.Transfer_Type as Invoice_Type,TSPL_TRANSFER_ORDER_HEAD.Document_No , " & _
                                    " convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103 ) as Document_Date , TSPL_TRANSFER_ORDER_DETAIL.Item_Code , " & _
                                    " (case when TSPL_TRANSFER_ORDER_DETAIL.In_Qty>0 then TSPL_TRANSFER_ORDER_DETAIL.In_Qty else TSPL_TRANSFER_ORDER_DETAIL.Out_Qty end) as Qty, " & _
                                    " TSPL_TRANSFER_ORDER_DETAIL.Unit_code ,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost , " & _
                                    " TSPL_TRANSFER_ORDER_DETAIL.Amount ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt ,0 as [Scheme Amount], " & _
                                    " (TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Amt_Less_Discount ,TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt ,(TSPL_TRANSFER_ORDER_DETAIL.Amount+TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Total_Amt,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code,COALESCE(TSPL_VEHICLE_MASTER.Number,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No) as Vehicle_No,0 as Additional_Charge, " & _
                                    " '' as [AR Document No],0 as  [AR Document Amt],0 as [AR Document Discount Amt],0 as  [AR Amount Before Tax],0 as [AR Total Tax],0 as [AR Total Add Charge],TSPL_TRANSFER_ORDER_HEAD.GR_No as [GR No],TSPL_TRANSFER_ORDER_HEAD.gr_date as [GR Date],TSPL_TRANSFER_ORDER_HEAD.WayBill_No as [WayBill No],TSPL_TRANSFER_ORDER_HEAD.Transport_Id as [Transporter Code],case when len(TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual) > 0 then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end  as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],0 AS MRP, '' as [Scheme Code] ,'' as  [Cash Scheme Code] , 0 as [Cash Scheme Amount], '' as [Price Code],'' as Created_By,'' as Modified_By,TSPL_TRANSFER_ORDER_DETAIL.Unit_code as RATE_UOM,0 as Conv_Factor, " & _
                                    " (CASE WHEN TSPL_TRANSFER_ORDER_HEAD.For_Repair=1 then 'Delivery Challan'  " & _
                                    " when From_LOC.STATE=GIT_Main_Loc.TO_STATE THEN  'Delivery Challan' " & _
                                    " WHEN TSPL_TRANSFER_ORDER_HEAD.TOTAL_TAX_AMT>0 AND TSPL_TAX_MASTER.TYPE='IGST' then 'Tax Invoice-Stock Transfer' else 'Bill of Supply' end) as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],'' as [GSTIN no Customer],(case when TSPL_TRANSFER_ORDER_Head.Total_Tax_Amt<=0 and TSPL_TRANSFER_ORDER_Head.Tax_Group<>'EXEMPTED' then (TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) else null end) as [Nill Rate Amount],(case when TSPL_TRANSFER_ORDER_HEAD.Tax_Group='EXEMPTED' then (TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) else null end) as [Exempted Amount],0 as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],'' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],(CASE WHEN TAXM1.TYPE='M' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='M' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='M' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX3_AMT ELSE 0 END) AS MANDI_TAX_AMT,"

            strSDEndQry = " from TSPL_TRANSFER_ORDER_DETAIL left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
                          " left join TSPL_VEHICLE_MASTER on TSPL_TRANSFER_ORDER_Head.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
                          " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSFER_ORDER_HEAD.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id " & _
                          " left join ( select  max(Location_Code) as Location_Code,MAX(STATE) AS TO_STATE,GIT_Location from TSPL_LOCATION_MASTER where GIT_Location is not null " & _
                          " group by GIT_Location ) GIT_Main_Loc on TSPL_TRANSFER_ORDER_HEAD.To_Location=GIT_Main_Loc.GIT_Location  " & _
                          " left join TSPL_LOCATION_MASTER as To_Loc on TSPL_TRANSFER_ORDER_HEAD.TO_LOCATION=To_Loc.LOCATION_CODE " & _
                          " LEFT join TSPL_LOCATION_MASTER as  From_LOC ON TSPL_TRANSFER_ORDER_HEAD.FROM_LOCATION=From_LOC.LOCATION_CODE " & _
                          " left join TSPL_TAX_MASTER on  TSPL_TRANSFER_ORDER_HEAD.TAX1=TSPL_TAX_MASTER.TAX_CODE " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAXM1 ON TSPL_TRANSFER_ORDER_DETAIL.TAX1=TAXM1.TAX_CODE " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAXM2 ON TSPL_TRANSFER_ORDER_DETAIL.TAX2=TAXM2.TAX_CODE " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAXM3 ON TSPL_TRANSFER_ORDER_DETAIL.TAX3=TAXM3.TAX_CODE "

            strSDJoinQry = "  where Transfer_Type <>'I' AND 'Transfer' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                          " and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_TRANSFER_ORDER_HEAD.From_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strSDJoinQry += " and GIT_Main_Loc.Location_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strSDJoinQry += " and TSPL_TRANSFER_ORDER_HEAD.Document_No = '" & obj.Document_Code & "' "
            End If
            '' base union 10
            '' transaction unit conversion
            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True
            strMCCMaterial += " select max(final._Type) as _Type, max(Formtype) as [Form Type],Trans_type  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_No as [Document No],final.Document_Date as [Document_date],max(Narration) as [Narration],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.To_Location as [Customer Code],max(TSPL_CUSTOMER_MASTER.add1+  ' ' + TSPL_CUSTOMER_MASTER.add2 + ' ' + ' ' + TSPL_CUSTOMER_MASTER.Add3) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Location_Desc) as [Customer Name],max(TSPL_CUSTOMER_MASTER.Registered) as [Registered],'' as [Composition],max(TSPL_CUSTOMER_MASTER .City_Code) as [City Code],max(TSPL_CUSTOMER_MASTER .City_Code) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,'' as [Parent Customer No] ,'' as [Parent Customer Code],'' as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], "

            ''Monika QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
            strMCCMaterial += " 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount]," & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP,  [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount],  [Price Code],final.Created_By as Created_By ,final.Modified_By as Modify_By,final.RATE_UOM,final.Conv_Factor,0 as Sampling,'N' as Scheme_Item, " & _
                " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max(TSPL_CUSTOMER_MASTER.GSTNO) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],'' as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT " & _
                " from ( "
            'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,0 as TAX1_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "

            '' query for no tax applied
            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax1,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax3,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax5,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax7,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax9,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax10,'')='') )t "
            '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " union all "
            '' quert for no tax applied
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,TSPL_TRANSFER_ORDER_DETAIL.TAX1_AMT,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax1 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX2 ,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax2 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX3 ,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax3 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX4 ,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax4 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX5 ,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax5 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"

            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX6 ,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax6 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX7 ,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax7 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX8 ,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax8 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax9 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX10 ,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax10 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"

            strMCCMaterial += " )final"
            ''-------------------------
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Location_Code =final.To_location " & _
                              " left join TSPL_STATE_MASTER on TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE"
            strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

            ''=Monika
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            ''end here

            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_No ,final.Item_Code ,final.Bill_To_Location ,final.To_Location ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP,[Scheme Code] ,[Cash Scheme Code] ,[Cash Scheme Amount], [Price Code] ,final.Created_By ,final.Modified_By,final. RATE_UOM,final.Conv_Factor" '',QC.FAT_Per,QC.SNF_Per
            ''richa
        End If
        strMCCMaterial += Environment.NewLine + " --- QUERY FOR TRANSFER RETURN---------------------- ADDED BY RICHA AGARWAL ------------" + Environment.NewLine

        If obj.Trans_Type_List.Contains("Transfer Return") Then

            Dim strTranferReturnCommonQuery As String = ""
            strTranferReturnCommonQuery = " select (case when len(TSPL_TRANSFER_ORDER_HEAD.description)<=0 then TSPL_TRANSFER_ORDER_HEAD.Remarks else TSPL_TRANSFER_ORDER_HEAD.description end) as Narration,case	when TSPL_TRANSFER_ORDER_HEAD.Is_AgainstFormF = 1 then 'F' else '' End as Formtype,'Transfer Return' as Trans_Type,TSPL_TRANSFER_ORDER_HEAD.Status ,  TSPL_TRANSFER_ORDER_HEAD.From_Location as Bill_To_Location, " & _
                                    " coalesce(GIT_Main_Loc.Location_Code,To_Loc.Location_Code) as To_Location,TSPL_TRANSFER_ORDER_HEAD.Transfer_Type as Invoice_Type,TSPL_TRANSFER_RETURN.Document_No , " & _
                                    " convert(varchar,TSPL_TRANSFER_RETURN.Document_Date,103 ) as Document_Date , TSPL_TRANSFER_ORDER_DETAIL.Item_Code , " & _
                                    " -(case when TSPL_TRANSFER_ORDER_DETAIL.In_Qty>0 then TSPL_TRANSFER_ORDER_DETAIL.In_Qty else TSPL_TRANSFER_ORDER_DETAIL.Out_Qty end) as Qty, " & _
                                    " TSPL_TRANSFER_ORDER_DETAIL.Unit_code ,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost , " & _
                                    " -TSPL_TRANSFER_ORDER_DETAIL.Amount AS Amount ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per ,-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt as Disc_Amt ,0 as [Scheme Amount], " & _
                                    " -(TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Amt_Less_Discount ,-TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt as Total_Tax_Amt ,-(TSPL_TRANSFER_ORDER_DETAIL.Amount+TSPL_TRANSFER_ORDER_DETAIL.Total_Tax_Amt-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Total_Amt,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code,COALESCE(TSPL_VEHICLE_MASTER.Number,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No) as Vehicle_No,0 as Additional_Charge, " & _
                                    " '' as [AR Document No],0 as  [AR Document Amt],0 as [AR Document Discount Amt],0 as  [AR Amount Before Tax],0 as [AR Total Tax],0 as [AR Total Add Charge],TSPL_TRANSFER_ORDER_HEAD.GR_No as [GR No],TSPL_TRANSFER_ORDER_HEAD.gr_date as [GR Date],TSPL_TRANSFER_ORDER_HEAD.WayBill_No as [WayBill No],TSPL_TRANSFER_ORDER_HEAD.Transport_Id as [Transporter Code],case when len(TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual) > 0 then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],0 AS MRP, '' as [Scheme Code] , '' as [Cash Scheme Code] ,0 as   [Cash Scheme Amount],  '' as [Price Code],'' as Created_By ,'' as Modified_By,TSPL_TRANSFER_ORDER_DETAIL.Unit_code as RATE_UOM,0 as Conv_Factor," & _
                                    " (CASE WHEN From_LOC.STATE=GIT_Main_Loc.TO_STATE THEN  'Delivery Challan' WHEN TSPL_TRANSFER_ORDER_HEAD.TOTAL_TAX_AMT>0 AND TSPL_TAX_MASTER.TYPE='IGST' then 'Tax Invoice-Stock Transfer' else 'Bill of Supply' end) as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],'' as [GSTIN no Customer],(case when TSPL_TRANSFER_ORDER_Head.Total_Tax_Amt<=0 and TSPL_TRANSFER_ORDER_Head.Tax_Group<>'EXEMPTED' then -(TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) else null end) as [Nill Rate Amount],(case when TSPL_TRANSFER_ORDER_HEAD.Tax_Group='EXEMPTED' then -(TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) else null end) as [Exempted Amount],0 as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],TSPL_TRANSFER_ORDER_HEAD.Document_No as [Original Invoice No],Convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],(CASE WHEN TAXM1.TYPE='M' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='M' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='M' THEN TSPL_TRANSFER_ORDER_DETAIL.TAX3_AMT ELSE 0 END) AS MANDI_TAX_AMT,"

            strSDEndQry = " from TSPL_TRANSFER_ORDER_DETAIL left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
                " Left Outer Join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_RETURN.Transfer_No=TSPL_TRANSFER_ORDER_HEAD.Document_No " & _
                          " left join TSPL_VEHICLE_MASTER on TSPL_TRANSFER_ORDER_Head.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
                          " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSFER_ORDER_HEAD.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id " & _
                          " left join ( select  max(Location_Code) as Location_Code,max(state) as TO_STATE,GIT_Location from TSPL_LOCATION_MASTER where GIT_Location is not null " & _
                          " group by GIT_Location ) GIT_Main_Loc on TSPL_TRANSFER_ORDER_HEAD.To_Location=GIT_Main_Loc.GIT_Location  " & _
                          " left join TSPL_LOCATION_MASTER as To_Loc on TSPL_TRANSFER_ORDER_HEAD.TO_LOCATION=To_Loc.LOCATION_CODE " & _
                          " LEFT join TSPL_LOCATION_MASTER as  From_LOC ON TSPL_TRANSFER_ORDER_HEAD.FROM_LOCATION=From_LOC.LOCATION_CODE " & _
                          " left join TSPL_TAX_MASTER on  TSPL_TRANSFER_ORDER_HEAD.TAX1=TSPL_TAX_MASTER.TAX_CODE " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAXM1 ON TSPL_TRANSFER_ORDER_DETAIL.TAX1=TAXM1.TAX_CODE " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAXM2 ON TSPL_TRANSFER_ORDER_DETAIL.TAX2=TAXM2.TAX_CODE " & _
                          " LEFT JOIN TSPL_TAX_MASTER TAXM3 ON TSPL_TRANSFER_ORDER_DETAIL.TAX3=TAXM3.TAX_CODE "

            strSDJoinQry = " where TSPL_TRANSFER_ORDER_HEAD.Transfer_Type ='O' AND 'Transfer Return' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                          " and convert(date,TSPL_TRANSFER_RETURN.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_TRANSFER_RETURN.Document_Date,103) <= convert(date,('" & To_Date & "'),103) AND ISNULL(TSPL_TRANSFER_RETURN.Document_No ,'')<>'' "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_TRANSFER_ORDER_HEAD.From_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strSDJoinQry += " and  GIT_Main_Loc.Location_Code  in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strSDJoinQry += " and TSPL_TRANSFER_RETURN.Document_No = '" & obj.Document_Code & "' "
            End If
            '' base union 11
            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True
            strMCCMaterial += " select max(final._Type) as _Type,max(Formtype) as [Form Type],Trans_type  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER.Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_No as [Document No],final.Document_Date as [Document_date],max(Narration) as [Narration],Vehicle_Code,Vehicle_No,final.Additional_Charge, final.To_Location as [Customer Code],max(TSPL_CUSTOMER_MASTER.add1+  ' ' + TSPL_CUSTOMER_MASTER.add2 + ' ' + ' ' + TSPL_CUSTOMER_MASTER.Add3) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER.Location_Desc) as [Customer Name],max(TSPL_LOCATION_MASTER.Registered) as [Registered],'' as [Composition],max(TSPL_LOCATION_MASTER.City_Code) as [City Code],max(TSPL_LOCATION_MASTER.City_Code) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,'' as [Parent Customer No] ,'' as [Parent Customer Code],'' as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], "


            ''Monika QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
            strMCCMaterial += " 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount]," & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount],  [Price Code] ,final.Created_By as Created_By ,final.Modified_By as Modify_By,final. RATE_UOM,final. Conv_Factor,0 as Sampling,'N' as Scheme_Item, " & _
                " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max(TSPL_LOCATION_MASTER.GSTNO) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],'' as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT " & _
                " from ( "
            'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,0 as TAX1_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "

            '' query for no tax applied
            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax1,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax3,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax5,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax7,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax9,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax10,'')='') )t "

            strMCCMaterial += Environment.NewLine + " union all "
            '' quert for no tax applied
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX1_AMT AS TAX1_AMT ,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax1 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX2 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt AS TAX2_Amt ,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax2 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX3 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt AS TAX3_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax3 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX4 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt AS TAX4_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax4 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX5 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt AS TAX5_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax5 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"

            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX6 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt AS TAX6_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax6 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX7 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt AS TAX7_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax7 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX8 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt AS TAX8_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax8 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt AS TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax9 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX10 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt AS TAX10_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_TRANSFER_ORDER_DETAIL.tax10 and ttr.tax_Rate=TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t" & _
             " )final" & _
             " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location " & _
             " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code " & _
             " left outer join TSPL_LOCATION_MASTER as TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Location_Code =final.To_location " & _
             " left join TSPL_STATE_MASTER on TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "



            strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

            ''Monika
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            ''=End here

            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_No ,final.Item_Code ,final.Bill_To_Location ,final.To_Location ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP,  [Scheme Code] ,  [Cash Scheme Code] , [Cash Scheme Amount],  [Price Code] ,final.Created_By ,final.Modified_By,final. RATE_UOM,final.Conv_Factor" + Environment.NewLine '',QC.FAT_Per,QC.SNF_Per
        End If
        strMCCMaterial += Environment.NewLine + " --- QUERY FOR TRANSFER RETURN END---------------------- ADDED BY RICHA AGARWAL ------------" + Environment.NewLine
        If obj.Include_MCCFarmerSale = True Then
            If obj.Trans_Type_List.Contains("MCC Sale Farmer") Then
                strSDCommonQuery = " select TSPL_MCC_SALE_FARMER_HEAD.Remarks as Narration,'' as Formtype,'MCC Sale Farmer' as Trans_Type ," & _
                                   " TSPL_MCC_SALE_FARMER_HEAD.Status ,TSPL_MCC_SALE_FARMER_HEAD.Bill_To_Location, " & _
                                   " TSPL_MCC_SALE_FARMER_HEAD.Farmer_Code as Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2  As CustAdd, " & _
                                   " TSPL_MCC_SALE_FARMER_HEAD.Invoice_Type AS Invoice_Type,TSPL_MCC_SALE_FARMER_HEAD.Document_Code , " & _
                                   " convert(varchar,TSPL_MCC_SALE_FARMER_HEAD.Document_Date,103 ) as Document_Date , TSPL_MCC_Sale_Farmer_Detail.Item_Code,TSPL_MCC_Sale_Farmer_Detail.Line_No , " & _
                                   " TSPL_MCC_Sale_Farmer_Detail.Qty as Qty,TSPL_MCC_Sale_Farmer_Detail.Unit_code , " & _
                                   " TSPL_MCC_Sale_Farmer_Detail.Item_Cost*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0 " & _
                                   " then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) as Item_Cost , " & _
                                   " (TSPL_MCC_Sale_Farmer_Detail.Amount *(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) - case when TSPL_MCC_SALE_FARMER_HEAD.trans_type='FS' then coalesce(TSPL_MCC_Sale_Farmer_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) else 0 end) as AMount, " & _
                                   " TSPL_MCC_Sale_Farmer_Detail.Disc_Per ,(TSPL_MCC_Sale_Farmer_Detail.Total_Disc_Amt*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) - case when TSPL_MCC_SALE_FARMER_HEAD.trans_type='FS' then coalesce(TSPL_MCC_Sale_Farmer_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) else 0 end) as Disc_Amt, " & _
                                   " (case when coalesce(TSPL_MCC_Sale_Farmer_Detail.FOC_Item,0)=1 or coalesce(TSPL_MCC_Sale_Farmer_Detail.sampling,0)=1 or coalesce(TSPL_MCC_Sale_Farmer_Detail.Scheme_Item,'')='Y' then Item_Net_Amt*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) end) as [Scheme Amount] , " & _
                                   " ((Amount-Total_Disc_Amt)*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end)- case when TSPL_MCC_SALE_FARMER_HEAD.trans_type='AS' then coalesce(TSPL_MCC_Sale_Farmer_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) else 0 end) as Amt_Less_Discount , " & _
                                   " TSPL_MCC_Sale_Farmer_Detail.Total_Tax_Amt*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) as Total_Tax_AMt , " & _
                                   " (Amount+TSPL_MCC_Sale_Farmer_Detail.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0 then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) as Total_Amt, " & _
                                   " TSPL_MCC_SALE_FARMER_HEAD.Vehicle_Code as Vehicle_Code ,TSPL_MCC_SALE_FARMER_HEAD.VEHICLENO as Vehicle_No, " & _
                                   " (case when TSPL_MCC_Sale_Farmer_Detail.Line_No=1 then (TSPL_MCC_SALE_FARMER_HEAD.Total_Add_Charge+coalesce(TSPL_MCC_SALE_FARMER_HEAD.RoundOffAmount,0))*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) else 0 end) as Additional_Charge, " & _
                                   " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0 then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) as [AR Document Amt]," & _
                                   " TSPL_Customer_Invoice_Head.Discount_Amount*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) as [AR Document Discount Amt], " & _
                                   " TSPL_Customer_Invoice_Head.amount_less_Discount*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) as [AR Amount Before Tax]," & _
                                   " TSPL_Customer_Invoice_Head.total_tax*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) as [AR Total Tax]," & _
                                   " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount)*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) as [AR Total Add Charge], " & _
                                   " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                                   " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,TSPL_MCC_SALE_FARMER_HEAD.GRNo as [GR No]," & _
                                   " cast(null as date) as [GR Date],TSPL_MCC_SALE_FARMER_HEAD.WayBillNo as [WayBill No],'' as [Transporter Code]," & _
                                   " '' as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No], " & _
                                   " TSPL_MCC_Sale_Farmer_Detail.MRP ,TSPL_MCC_Sale_Farmer_Detail.Scheme_Code as [Scheme Code] ," & _
                                   " TSPL_MCC_Sale_Farmer_Detail.Cash_Scheme_Code as [Cash Scheme Code] ,TSPL_MCC_Sale_Farmer_Detail.Cash_Scheme_Amount as [Cash Scheme Amount],TSPL_MCC_Sale_Farmer_Detail.Price_code as [Price Code],'' as Created_By, " & _
                                   " '' as  Modify_By ,TSPL_MCC_Sale_Farmer_Detail.RATE_UOM,TSPL_MCC_Sale_Farmer_Detail.Conv_Factor,TSPL_MCC_Sale_Farmer_Detail.Sampling," & _
                                   " TSPL_MCC_Sale_Farmer_Detail.Scheme_Item ,(case when TSPL_MCC_SALE_FARMER_HEAD.Is_Taxable=1 then 'Tax Invoice' else 'Bill Of Supply' end) as [Invoice Type GST]," & _
                                   " ' " & CompGstinNo & "' as [GSTIN No Company],'' as [GSTIN No Customer], (case when TSPL_MCC_SALE_FARMER_HEAD.Total_Tax_Amt<=0 and TSPL_MCC_SALE_FARMER_HEAD.Tax_Group<>'EXEMPTED'  then ((Amount+TSPL_MCC_Sale_Farmer_Detail.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end)) else null end) as [Nill Rate Amount]," & _
                                   " (case when TSPL_MCC_SALE_FARMER_HEAD.Tax_Group='EXEMPTED' then ((Amount+TSPL_MCC_Sale_Farmer_Detail.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end)) else null end) as [Exempted Amount]," & _
                                   " 0 as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,TSPL_MCC_SALE_FARMER_HEAD.Sale_Invoice_No as [Shipping Bill No],convert(varchar,TSPL_MCC_SALE_FARMER_HEAD.Inv_Date,103) as [Shipping Bill Date]," & _
                                   " '' as [Original Invoice No],'' as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No], " & _
                                   " (CASE WHEN TAXM1.TYPE='M' THEN TSPL_MCC_Sale_Farmer_Detail.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='M' THEN TSPL_MCC_Sale_Farmer_Detail.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='M' THEN TSPL_MCC_Sale_Farmer_Detail.TAX3_AMT ELSE 0 END) AS MANDI_TAX_AMT, "

                strSDEndQry = " from TSPL_MCC_Sale_Farmer_Detail " & _
                              " left outer join TSPL_MCC_SALE_FARMER_HEAD on TSPL_MCC_SALE_FARMER_HEAD.Document_Code =TSPL_MCC_Sale_Farmer_Detail.DOCUMENT_CODE " & _
                              " left join TSPL_VEHICLE_MASTER on TSPL_MCC_SALE_FARMER_HEAD.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                              " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_MCC_SALE_FARMER_HEAD.Document_Code " & _
                              " /*left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_MCC_Sale_Farmer_Detail.Delivery_Code " & _
                              " left join TSPL_SD_SHIPMENT_HEAD on TSPL_MCC_SALE_FARMER_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code */ " & _
                              " LEFT OUTER JOIN TSPL_MP_MASTER as TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.MP_Code = TSPL_MCC_SALE_FARMER_HEAD.Farmer_Code " & _
                              " /*LEFT JOIN TSPL_EX_COMMERCIAL_INVOICE_HEAD ON TSPL_MCC_SALE_FARMER_HEAD.Against_Com_Inv_No=TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code " & _
                              " LEFT JOIN TSPL_MCC_SALE_FARMER_HEAD SupplInvoice ON TSPL_MCC_SALE_FARMER_HEAD.Invoice_No_For_Supplementary=SupplInvoice.Document_Code */ " & _
                              " LEFT JOIN TSPL_TAX_MASTER TAXM1 ON TSPL_MCC_Sale_Farmer_Detail.TAX1=TAXM1.TAX_CODE " & _
                              " LEFT JOIN TSPL_TAX_MASTER TAXM2 ON TSPL_MCC_Sale_Farmer_Detail.TAX2=TAXM2.TAX_CODE " & _
                              " LEFT JOIN TSPL_TAX_MASTER TAXM3 ON TSPL_MCC_Sale_Farmer_Detail.TAX3=TAXM3.TAX_CODE "
                strSDJoinQry = " where (case when TSPL_MCC_SALE_FARMER_HEAD.Trans_Type='MCC' then 'MCC Sale Farmer' else  TSPL_MCC_SALE_FARMER_HEAD.Trans_Type end) " & _
                              " in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                              " and  convert(date,TSPL_MCC_SALE_FARMER_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) " & _
                              " and convert(date,TSPL_MCC_SALE_FARMER_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

                '' filter conditions
                If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                    strSDJoinQry += " and TSPL_MCC_Sale_Farmer_Detail.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
                End If
                If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                    strSDJoinQry += " and TSPL_MCC_SALE_FARMER_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
                End If

                If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                    strSDJoinQry += " and TSPL_MCC_SALE_FARMER_HEAD.FARMER_CODE in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
                End If
                If clsCommon.myLen(obj.Document_Code) > 0 Then
                    strSDJoinQry += " and TSPL_MCC_SALE_FARMER_HEAD.Document_Code = '" & obj.Document_Code & "' "
                End If


                If qryStarted = True Then
                    strMCCMaterial += " union all "
                End If
                qryStarted = True
                strMCCMaterial += " select max(final._Type) as _Type , max(final.FormType) as [Form Type],case when Trans_Type ='FS' then 'Fresh Sale' when Trans_Type ='CSA' then 'CSA Sale' when Trans_Type='PS' then 'Product Sale' when Trans_Type='MCC' then 'MCC Sale' when Trans_Type='EX' then 'Export Sale'when Trans_Type='Bulk Sale' then 'Bulk Sale' when Trans_Type ='SS' then 'Misc Sale' when Trans_Type='MT' then 'Merchant Trade' WHEN Trans_Type ='SD' then 'General Sale' else  Trans_Type end  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date], max(final.Narration) as [Narration],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) AS [Customer Address] ,max(TSPL_CUSTOMER_MASTER .MP_Name) as [Customer Name],'' as [Registered],'' as [Composition],max(TSPL_CUSTOMER_MASTER .City_Code) as [City Code],max(TSPL_CITY_MASTER .City_Name) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,'' as [Parent Customer No] ,'' as [Parent Customer Code],'' as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], "

                ''Monika  QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
                strMCCMaterial += " 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount],   " & _
                    " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]- case when (Trans_Type ='FS') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] ,[Cash Scheme Code] , [Cash Scheme Amount], [Price Code],final.Created_By,final.Modify_By ,final. RATE_UOM,final. Conv_Factor,final.Sampling,final.Scheme_Item, " & _
                    " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],'' as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT"
                strMCCMaterial += " from ("
                'strTaxColumns = " TSPL_MCC_SALE_FARMER_HEAD.TAX1 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_MCC_SALE_FARMER_HEAD.TAX1_Amt as Tax1_Amt, TSPL_MCC_SALE_FARMER_HEAD.TAX1_Rate,TSPL_MCC_SALE_FARMER_HEAD.TAX1+'%' as Tax1Rate"
                strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
                '' query for no tax applied

                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & "  and (coalesce(TSPL_MCC_Sale_Farmer_Detail.tax1,'')='' and coalesce(TSPL_MCC_Sale_Farmer_Detail.tax2,'')='' " & _
                                  " and coalesce(TSPL_MCC_Sale_Farmer_Detail.tax3,'')='' and coalesce(TSPL_MCC_Sale_Farmer_Detail.tax4,'')='' and " & _
                                  " coalesce(TSPL_MCC_Sale_Farmer_Detail.tax5,'')='' and coalesce(TSPL_MCC_Sale_Farmer_Detail.tax6,'')='' and " & _
                                  " coalesce(TSPL_MCC_Sale_Farmer_Detail.tax7,'')='' and coalesce(TSPL_MCC_Sale_Farmer_Detail.tax8,'')='' and " & _
                                  " coalesce(TSPL_MCC_Sale_Farmer_Detail.tax9,'')='' and coalesce(TSPL_MCC_Sale_Farmer_Detail.tax10,'')='') )t "

                '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + ") " & _

                strMCCMaterial += "   union all"
                '' query for tax1 applied
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX1 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX1_Amt as Tax1_Amt, TSPL_MCC_Sale_Farmer_Detail.TAX1_Rate,TSPL_MCC_Sale_Farmer_Detail.TAX1+'%' as Tax1Rate"

                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax1 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX1_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and TSPL_MCC_Sale_Farmer_Detail.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "   union all"
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX2 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX2_Amt as Tax2_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX2_Rate, TSPL_MCC_Sale_Farmer_Detail.TAX2+'%' as Tax2Rate"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax2 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX2_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Farmer_Detail.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX3 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX3_Amt as Tax3_Amt, TSPL_MCC_Sale_Farmer_Detail.TAX3_Rate, TSPL_MCC_Sale_Farmer_Detail.TAX3+'%' as Tax3Rate"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax3 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX3_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Farmer_Detail.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "   union all"
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX4 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX4_Amt as Tax4_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX4_Rate, TSPL_MCC_Sale_Farmer_Detail.TAX4+'%' as Tax4Rate"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax4 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX4_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Farmer_Detail.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX5 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX5_Amt as Tax5_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX5_Rate, TSPL_MCC_Sale_Farmer_Detail.TAX5+'%' as Tax5Rate"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax5 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX5_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Farmer_Detail.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX6 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX6_Amt as Tax6_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX6_Rate, TSPL_MCC_Sale_Farmer_Detail.TAX6+'%' as Tax6Rate"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax6 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX6_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Farmer_Detail.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX7 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX7_Amt as Tax7_AMt,TSPL_MCC_Sale_Farmer_Detail.TAX7_Rate, TSPL_MCC_Sale_Farmer_Detail.TAX7+'%' as Tax7Rate"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax7 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX7_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Farmer_Detail.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX8 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX8_Amt as Tax8_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX8_Rate, TSPL_MCC_Sale_Farmer_Detail.TAX8+'%' as Tax8Rate"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax8 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX8_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Farmer_Detail.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX9 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX9_Amt as Tax9_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX9_Rate, TSPL_MCC_Sale_Farmer_Detail.TAX9+'%' as Tax9Rate"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax9 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX9_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Farmer_Detail.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strTaxColumns = " TSPL_MCC_Sale_Farmer_Detail.TAX10 ,(case when coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_SALE_FARMER_HEAD.convrate,0) end) * TSPL_MCC_Sale_Farmer_Detail.TAX10_Amt as Tax10_Amt,TSPL_MCC_Sale_Farmer_Detail.TAX10_Rate,TSPL_MCC_Sale_Farmer_Detail.TAX10+'%' as Tax10Rate"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Farmer_Detail.tax10 and ttr.tax_Rate=TSPL_MCC_Sale_Farmer_Detail.TAX10_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Farmer_Detail.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += " )final"
                strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
                strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
                strMCCMaterial += " left outer join TSPL_MP_MASTER AS TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .MP_CODE =final.Customer_Code "
                'strMCCMaterial += " LEFT OUTER JOIN TSPL_MP_MASTER as Parent_Master ON Parent_Master.MP_CODE=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
                strMCCMaterial += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                                  " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State_Code=TSPL_STATE_MASTER.STATE_CODE "


                ''Monika
                ''strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "

                'added by stuti on 01/05/2017
                strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

                If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                    strMCCMaterial += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
                End If
                If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                    strMCCMaterial += " and final.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
                End If
                If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                    strMCCMaterial += " and final.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
                End If
                ''end here

                strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code,final.Line_No ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount] , [Price Code] ,final.Created_By,final.Modify_By,final.RATE_UOM,final.Conv_Factor,final.Sampling,final.Scheme_Item" '', " + strPivotFoGrouprOuterQuery + " ,QC.FAT_Per,QC.SNF_Per

            End If
            If obj.Trans_Type_List.Contains("MCC Sale Return Farmer") Then

                Dim strSDRCommonQuery As String = ""
                strSDRCommonQuery = " select (case when len(TSPL_MCC_Sale_Return_Head_Farmer.Description)<=0 then TSPL_MCC_Sale_Return_Head_Farmer.Comments else TSPL_MCC_Sale_Return_Head_Farmer.Description end) as Narration,'' as Formtype,'MCC Sale Return Farmer' as Trans_Type,TSPL_MCC_Sale_Return_Head_Farmer.Status ,TSPL_MCC_Sale_Return_Head_Farmer.Bill_To_Location, " & _
                                  " TSPL_MCC_Sale_Return_Head_Farmer.Farmer_Code as Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2  As CustAdd,COALESCE(TSPL_MCC_Sale_Return_Head_Farmer.Document_Type,TSPL_MCC_Sale_Return_Head_Farmer.Invoice_Type) AS Invoice_Type,TSPL_MCC_Sale_Return_Head_Farmer.Document_Code , " & _
                                  " convert(varchar,TSPL_MCC_Sale_Return_Head_Farmer.Document_Date,103 ) as Document_Date , TSPL_MCC_Sale_Return_Detail_Farmer.Item_Code,TSPL_MCC_Sale_Return_Detail_Farmer.Line_No , " & _
                                  " -TSPL_MCC_Sale_Return_Detail_Farmer.Qty as Qty ,TSPL_MCC_Sale_Return_Detail_Farmer.Unit_code ,TSPL_MCC_Sale_Return_Detail_Farmer.Item_Cost , " & _
                                  " -coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Amount,0) as Amount ,TSPL_MCC_Sale_Return_Detail_Farmer.Disc_Per ,case when coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Disc_Amt,0)=0 then -coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Disc_Amt,0)  + case when coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.FOC_Item,0)=1 or coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.sampling,0)=1  then 1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_MCC_Sale_Return_Head_Farmer.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_Sale_Return_Head_Farmer.convrate,0) end) else 0 end else -coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Disc_Amt,0) end as Disc_Amt,case when coalesce(FOC_Item,0)=1 or coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.sampling,0)=1  then -1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_MCC_Sale_Return_Head_Farmer.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_Sale_Return_Head_Farmer.convrate,0) end) end  as [Scheme Amount] , " & _
                                  " -(Amount- case when TSPL_MCC_Sale_Return_Head_Farmer.Trans_Type='FS' then Total_Disc_Amt else Total_Disc_Amt end  + case when TSPL_MCC_Sale_Return_Head_Farmer.Trans_Type<>'FS' then case when coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.FOC_Item,0)=1 or coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.sampling,0)=1  then Item_Net_Amt*(case when coalesce(TSPL_MCC_Sale_Return_Head_Farmer.convrate,0)<=0  then 1 else coalesce(TSPL_MCC_Sale_Return_Head_Farmer.convrate,0) end) else 0 end else 0 end)  as Amt_Less_Discount , " & _
                                  " -coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Tax_Amt,0) as Total_Tax_Amt ,-(Amount+coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Tax_Amt,0)- case when TSPL_MCC_Sale_Return_Head_Farmer.Trans_Type='FS' then TSPL_MCC_Sale_Return_Detail_Farmer.Total_Disc_Amt else coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Disc_Amt,0) end )  as Total_Amt,TSPL_MCC_Sale_Return_Head_Farmer.Vehicle_Code,TSPL_VEHICLE_MASTER.Number as Vehicle_No,-(case when TSPL_MCC_Sale_Return_Detail_Farmer.Line_No=1 then (coalesce(TSPL_MCC_Sale_Return_Head_Farmer.Total_Add_Charge,0)+coalesce(TSPL_MCC_Sale_Return_Head_Farmer.RoundOffAmount,0)) else 0 end) as Additional_Charge, " & _
                                  " '' as [AR Document No],0 as [AR Document Amt]," & _
                                  " 0 as [AR Document Discount Amt], " & _
                                  " 0 as [AR Amount Before Tax],0 as [AR Total Tax], " & _
                                  " 0 as [AR Total Add Charge], " & _
                                  " '' as Against_Sale_No,'' as Against_Sale_Return_No,'' as AgainstScrap, " & _
                                  " '' as Against_VCGL,'' as Against_MCC_Material_Sale_Return,TSPL_MCC_Sale_Return_Head_Farmer.GRNo as [GR No],cast(null as date) as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],TSPL_MCC_Sale_Return_Detail_Farmer.Delivery_Code as [Delivery No],'' as [Shipment No],'' as [Booking No],TSPL_MCC_Sale_Return_Detail_Farmer.MRP, TSPL_MCC_Sale_Return_Detail_Farmer.Scheme_Code ,TSPL_MCC_Sale_Return_Detail_Farmer.Cash_Scheme_Code ,TSPL_MCC_Sale_Return_Detail_Farmer.Cash_Scheme_Amount*(-1) as Cash_Scheme_Amount ,TSPL_MCC_Sale_Return_Detail_Farmer.Price_code ,'' as Created_By ,'' as Modify_By,TSPL_MCC_Sale_Return_Detail_Farmer.Unit_code as RATE_UOM,0 as Conv_Factor, TSPL_MCC_Sale_Return_Detail_Farmer.Sampling,TSPL_MCC_Sale_Return_Detail_Farmer.Scheme_Item," & _
                                  " 'Credit Note' as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],'' as [GSTIN no Customer],(case when TSPL_MCC_Sale_Return_Head_Farmer.Total_Tax_Amt<=0 and TSPL_MCC_Sale_Return_Head_Farmer.Tax_Group<>'EXEMPTED' then -(Amount+coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Tax_Amt,0)- case when TSPL_MCC_Sale_Return_Head_Farmer.Trans_Type='FS' then 0 else coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Disc_Amt,0) end ) else null end) as [Nill Rate Amount],(case when TSPL_MCC_Sale_Return_Head_Farmer.Tax_Group='EXEMPTED' then -(Amount+coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Tax_Amt,0)- case when TSPL_MCC_Sale_Return_Head_Farmer.Trans_Type='FS' then 0 else coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.Total_Disc_Amt,0) end ) else null end) as [Exempted Amount],0 as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port,'' as [Shipping Bill No],'' as [Shipping Bill Date],TSPL_MCC_Sale_Return_Detail_Farmer.Invoice_Code as [Original Invoice No],convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) as [Original Invoice Date],TSPL_MCC_Sale_Return_Head_Farmer.Description as [Reason for Revision],'' as [LUT No],(CASE WHEN TAXM1.TYPE='M' THEN TSPL_MCC_Sale_Return_Detail_Farmer.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='M' THEN TSPL_MCC_Sale_Return_Detail_Farmer.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='M' THEN TSPL_MCC_Sale_Return_Detail_Farmer.TAX3_AMT ELSE 0 END) AS MANDI_TAX_AMT,"
                strSDEndQry = " from TSPL_MCC_Sale_Return_Detail_Farmer " & _
                                    " left outer join TSPL_MCC_Sale_Return_Head_Farmer on TSPL_MCC_Sale_Return_Head_Farmer.Document_Code =TSPL_MCC_Sale_Return_Detail_Farmer.DOCUMENT_CODE " & _
                                    " left join TSPL_VEHICLE_MASTER on TSPL_MCC_Sale_Return_Head_Farmer.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                                    " left join TSPL_MCC_SALE_FARMER_HEAD as TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_MCC_Sale_Return_Detail_Farmer.Invoice_Code " & _
                                    " LEFT OUTER JOIN TSPL_MP_MASTER as TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.MP_Code = TSPL_MCC_Sale_Return_Head_Farmer.Farmer_Code " & _
                                    " LEFT JOIN TSPL_TAX_MASTER TAXM1 ON TSPL_MCC_Sale_Return_Detail_Farmer.TAX1=TAXM1.TAX_CODE " & _
                                    " LEFT JOIN TSPL_TAX_MASTER TAXM2 ON TSPL_MCC_Sale_Return_Detail_Farmer.TAX2=TAXM2.TAX_CODE " & _
                                    " LEFT JOIN TSPL_TAX_MASTER TAXM3 ON TSPL_MCC_Sale_Return_Detail_Farmer.TAX3=TAXM3.TAX_CODE "

                strSDJoinQry = " WHERE 2=2 AND (case when TSPL_MCC_Sale_Return_Head_Farmer.Trans_Type='MCC' then 'MCC Sale Return Farmer' else  TSPL_MCC_Sale_Return_Head_Farmer.Trans_Type end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                                " and convert(date,TSPL_MCC_Sale_Return_Head_Farmer.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_MCC_Sale_Return_Head_Farmer.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

                '' filter conditions
                If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                    strSDJoinQry += " and TSPL_MCC_Sale_Return_Detail_Farmer.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
                End If
                If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                    strSDJoinQry += " and TSPL_MCC_Sale_Return_Head_Farmer.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
                End If
                ''=========================Added by preeti Gupta against Ticket No[KDI/08/05/18-000305]====================
                'If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                '    strSDJoinQry += " and TSPL_MCC_Sale_Return_Head_Farmer.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
                'End If
                If clsCommon.myLen(obj.Document_Code) > 0 Then
                    strSDJoinQry += " and TSPL_MCC_Sale_Return_Head_Farmer.Document_Code = '" & obj.Document_Code & "' "
                End If

                '' base union 5
                If qryStarted = True Then
                    strMCCMaterial += " union all "
                End If
                qryStarted = True
                strMCCMaterial += " select max(final._Type) as _Type, max(final.Formtype) as [Form Type],case when Trans_Type ='FSR' then 'Fresh Sale Return' when Trans_Type ='CSAR' then 'CSA Sale Return' when Trans_Type='PSR' then 'Product Sale Return' when Trans_Type='MCCR' then 'MCC Sale Return' when Trans_Type='EXPR' then 'Export Sale Return'when Trans_Type='Bulk Sale' then 'Bulk Sale Return' when Trans_Type ='SSR' then 'Misc Sale' when Trans_Type ='SDR' then 'General Sale Return' else trans_Type  end  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date],max(final.Narration) as [Narration],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name],max(TSPL_CUSTOMER_MASTER .GST_Registered) as [Registered],max(TSPL_CUSTOMER_MASTER .GST_COMPOSITION) as [Composition],max(TSPL_CUSTOMER_MASTER .City_Code) as [City Code],max(TSPL_CITY_MASTER .City_Name) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost],"

                ''Monika QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
                strMCCMaterial += "  0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount], " & _
                    " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]  - case when (Trans_Type ='FSR' or Trans_Type ='PSR') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],final.[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, Scheme_Code as [Scheme Code] ,Cash_Scheme_Code as [Cash Scheme Code], Cash_Scheme_Amount as [Cash Scheme Amount] ,final.Price_code as [Price Code] ,final.Created_By ,final.Modify_By,final.RATE_UOM ,final.Conv_Factor ,final. Sampling,final.Scheme_Item," & _
                    " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],'' as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT " & _
                    " from ( "

                strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
                '' query for no tax applied
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax1,'')='' and coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax2,'')='' " & _
                                  " and coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax3,'')='' and coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax4,'')='' and " & _
                                  " coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax5,'')='' and coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax6,'')='' and " & _
                                  " coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax7,'')='' and coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax8,'')='' and " & _
                                  " coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax9,'')='' and coalesce(TSPL_MCC_Sale_Return_Detail_Farmer.tax10,'')='') )t "

                strMCCMaterial += "   union all"
                '' query for tax1 applied
                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX1 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX1_Amt as TAX1_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX1_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX1+'%' as tax1rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax1 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX1_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"

                strMCCMaterial += "   union all"
                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX2 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX2_Amt as TAX2_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX2_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX2+'%' as tax2rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax2 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX2_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX3 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX3_Amt as TAX3_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX3_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX3+'%' as tax3rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax3 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX3_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax7 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX7_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "   union all"
                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX4 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX4_Amt as TAX4_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX4_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX4+'%' as tax4rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax4 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX4_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"
                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX5 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX5_Amt as TAX5_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX5_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX5+'%' as tax5rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax5 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX5_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"

                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"

                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX6 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX6_Amt as TAX6_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX6_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX6+'%' as tax6rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax6 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX6_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"

                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX7 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX7_Amt as TAX7_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX7_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX7+'%' as tax7rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax7 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX7_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"

                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX8 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX8_Amt as TAX8_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX8_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX8+'%' as tax8rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax8 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX8_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"

                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX9 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX9_Amt as TAX9_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX9_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX9+'%' as tax9rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax9 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX9_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += "  union all"

                strTaxColumns = " TSPL_MCC_Sale_Return_Detail_Farmer.TAX10 ,-TSPL_MCC_Sale_Return_Detail_Farmer.TAX10_Amt as TAX10_Amt,TSPL_MCC_Sale_Return_Detail_Farmer.TAX10_Rate ,TSPL_MCC_Sale_Return_Detail_Farmer.TAX10+'%' as tax10rate  "
                strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_MCC_Sale_Return_Detail_Farmer.tax10 and ttr.tax_Rate=TSPL_MCC_Sale_Return_Detail_Farmer.TAX10_Rate and ttr._type<>'OH' and ttr._type<>'OH' and ttr.Tax_Type='S'"
                strMCCMaterial += " select * from (" & strSDRCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_MCC_Sale_Return_Detail_Farmer.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
                strMCCMaterial += " )final"
                strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
                strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
                strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
                strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
                strMCCMaterial += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                                  " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "

                ''Monika
                ''strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
                'added by stuti on 01/05/2017
                strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

                ''=========Monika
                If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                    strMCCMaterial += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
                End If
                If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                    strMCCMaterial += " and final.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
                End If
                If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                    strMCCMaterial += " and final.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
                End If
                ''======end here

                strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code,final.Line_No ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , Scheme_Code ,Cash_Scheme_Code , Cash_Scheme_Amount ,final.Price_code ,final.Created_By ,final.Modify_By ,final.RATE_UOM,final.Conv_Factor ,final. Sampling,final.Scheme_Item " '', " + strPivotFoGrouprOuterQuery + " ,QC.FAT_Per,QC.SNF_Per

            End If
        End If
        '' Debit/Credit Note
        If obj.Include_Debit_Credit Then
            Dim strDebitCreditCommonQuery As String = ""
            strDebitCreditCommonQuery = " select TSPL_Customer_Invoice_Head.Description as Narration,'' as Formtype, 'Debit/Credit' as Trans_Type  ,TSPL_Customer_Invoice_Head.Status ,TSPL_Customer_Invoice_Head.Loc_Code as Bill_To_Location, " & _
                                        " TSPL_Customer_Invoice_Head.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd," & _
                                        " TSPL_Customer_Invoice_Head.Document_Type AS Invoice_Type,TSPL_Customer_Invoice_Head.Document_No as Document_Code , " & _
                                        " convert(varchar,TSPL_Customer_Invoice_Head.Document_Date,103 ) as Document_Date , '' as Item_Code,TSPL_Customer_Invoice_detail.SNo as Line_No, " & _
                                        " 0 as Qty,'' as Unit_code , 0 as Item_Cost ,TSPL_Customer_Invoice_detail.Amount* (case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end) *(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end) as AMount, " & _
                                        " TSPL_Customer_Invoice_detail.Discount_Per as Disc_Per ,TSPL_Customer_Invoice_detail.Discount*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end) as Disc_Amt, 0 as [Scheme Amount] , " & _
                                        " ((TSPL_Customer_Invoice_detail.Amount-TSPL_Customer_Invoice_detail.Discount)*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end)) as Amt_Less_Discount , " & _
                                        " TSPL_Customer_Invoice_detail.Total_Tax*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end) as Total_Tax_AMt ," & _
                                        " (TSPL_Customer_Invoice_detail.Amount+TSPL_Customer_Invoice_detail.Total_Tax-TSPL_Customer_Invoice_detail.Discount)*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end) as Total_Amt, '' as Vehicle_Code ,'' as Vehicle_No, " & _
                                        " (case when TSPL_Customer_Invoice_detail.SNo=1 then (TSPL_Customer_Invoice_Head.Total_Add_Charge+coalesce(TSPL_Customer_Invoice_Head.RoundOffAmount,0))*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end) else 0 end) as  Additional_Charge, " & _
                                        " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end) as [AR Document Amt]," & _
                                        " TSPL_Customer_Invoice_Head.Discount_Amount*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end)  as [AR Document Discount Amt], " & _
                                        " TSPL_Customer_Invoice_Head.amount_less_Discount*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end) as [AR Amount Before Tax]," & _
                                        " TSPL_Customer_Invoice_Head.total_tax*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end) as [AR Total Tax], " & _
                                        " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount)*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end) as [AR Total Add Charge], " & _
                                        " '' as Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,'' as AgainstScrap,'' as Against_VCGL,'' as Against_MCC_Material_Sale_Return,'' as [GR No],'' as [GR Date],'' as [WayBill No],'' as [Transporter Code]," & _
                                        " '' as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No], 0 as MRP ,'' as [Scheme Code] ,0 as [Cash Scheme Code] ,0 as [Cash Scheme Amount],'' as [Price Code],TSPL_Customer_Invoice_Head.Created_By as Created_By,TSPL_Customer_Invoice_Head.Modify_By as Modify_By ," & _
                                        " '' as RATE_UOM,0 as Conv_Factor,'' as Sampling,'' as Scheme_Item , (case when TSPL_Customer_Invoice_Head.Document_Type='D' then 'Debit Note' when TSPL_Customer_Invoice_Head.Document_Type='C' then 'Credit Note' else 'Invoice' end) as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],TSPL_CUSTOMER_MASTER.GSTNO as [GSTIN No Customer], " & _
                                        " (case when TSPL_Customer_Invoice_Head.Total_Tax<=0 and TSPL_Customer_Invoice_Head.Tax_Group<>'EXEMPTED' then ((Amount+TSPL_Customer_Invoice_detail.Total_Tax-TSPL_Customer_Invoice_detail.Discount)*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end)) else null end) as [Nill Rate Amount]," & _
                                        " (case when TSPL_Customer_Invoice_Head.Tax_Group='EXEMPTED' then ((Amount+TSPL_Customer_Invoice_detail.Total_Tax-TSPL_Customer_Invoice_detail.Discount)*(case when TSPL_Customer_Invoice_Head.Document_Type='D' then 1 else -1 end)*(case when coalesce(TSPL_Customer_Invoice_Head.convrate,0)<=0  then 1 else coalesce(TSPL_Customer_Invoice_Head.convrate,0) end)) else null end) as [Exempted Amount], " & _
                                        " 0 as [Non GST Supply],'N' as [Reverse Charge],'' as [Export Type],'' as Port, " & _
                                        " '' as [Shipping Bill No],'' as [Shipping Bill Date],TSPL_Customer_Invoice_Head.RefDocNo as [Original Invoice No],convert(varchar,RefInvoice.Document_Date,103) as [Original Invoice Date],'' as [Reason for Revision],'' as [LUT No],0 AS MANDI_TAX_AMT, "

            strSDEndQry = " from TSPL_Customer_Invoice_detail " & _
                          " left outer join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Document_No =TSPL_Customer_Invoice_detail.Document_No " & _
                          " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_Customer_Invoice_Head.Customer_Code" & _
                          " left join TSPL_Customer_Invoice_Head RefInvoice on TSPL_Customer_Invoice_Head.RefDocNo=RefInvoice.Document_No "

            strSDJoinQry = " where TSPL_Customer_Invoice_Head.Document_Type in ('D','C')  " & _
                           " and len(coalesce(TSPL_Customer_Invoice_Head.Against_Asset_Disposal,''))<=0 " & _
                           " and len(coalesce(TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,''))<=0  " & _
                           " and len(coalesce(TSPL_Customer_Invoice_Head.Against_Sale_No,''))<=0" & _
                           " and len(coalesce(TSPL_Customer_Invoice_Head.Against_Sale_Return_No,''))<=0  " & _
                           " and len(coalesce(TSPL_Customer_Invoice_Head.Against_Security_Receipt_No,''))<=0  " & _
                           " and len(coalesce(TSPL_Customer_Invoice_Head.Against_Service_Visit_Code,''))<=0  " & _
                           " and len(coalesce(TSPL_Customer_Invoice_Head.AgainstScrap,''))<=0  " & _
                           " and len(coalesce(TSPL_Customer_Invoice_Head.AgainstScrapReturn,''))<=0 " & _
                           " and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_Customer_Invoice_Head.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "
            '' filter conditions
            'If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            '    strSDJoinQry += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            'End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_Customer_Invoice_Head.Loc_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strSDJoinQry += " and  TSPL_Customer_Invoice_Head.Customer_Code  in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strSDJoinQry += " and TSPL_Customer_Invoice_Head.Document_No = '" & obj.Document_Code & "' "
            End If
            '' base union 11
            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True

            strMCCMaterial += " select max(final._Type) as _Type , max(final.FormType) as [Form Type],case when Trans_Type ='FS' then 'Fresh Sale' when Trans_Type ='CSA' then 'CSA Sale' when Trans_Type='PS' then 'Product Sale' when Trans_Type='MCC' then 'MCC Sale' when Trans_Type='EX' then 'Export Sale'when Trans_Type='Bulk Sale' then 'Bulk Sale' when Trans_Type ='SS' then 'Misc Sale' when Trans_Type='MT' then 'Merchant Trade' WHEN (Trans_Type ='SD' Or Trans_Type ='All' ) then 'General Sale' else  Trans_Type end  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date], max(final.Narration) as [Narration],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) AS [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name],max(TSPL_CUSTOMER_MASTER .GST_Registered) as [Registered],max(TSPL_CUSTOMER_MASTER .GST_COMPOSITION) as [Composition],max(TSPL_CUSTOMER_MASTER .City_Code) as [City Code],max(TSPL_CITY_MASTER .City_Name) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], "

            ''Monika  QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
            strMCCMaterial += " 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount],   " & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]- case when (Trans_Type ='FS') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] ,[Cash Scheme Code] , [Cash Scheme Amount], [Price Code],final.Created_By,final.Modify_By ,final. RATE_UOM,final. Conv_Factor,final.Sampling,final.Scheme_Item, " & _
                " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],'' as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT " & _
                " from ( "
            'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,0 as TAX1_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "

            '' query for no tax applied
            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & " and (coalesce(TSPL_Customer_Invoice_Detail.tax1,'')='' and coalesce(TSPL_Customer_Invoice_Detail.tax2,'')='' " & _
                              " and coalesce(TSPL_Customer_Invoice_Detail.tax3,'')='' and coalesce(TSPL_Customer_Invoice_Detail.tax4,'')='' and " & _
                              " coalesce(TSPL_Customer_Invoice_Detail.tax5,'')='' and coalesce(TSPL_Customer_Invoice_Detail.tax6,'')='' and " & _
                              " coalesce(TSPL_Customer_Invoice_Detail.tax7,'')='' and coalesce(TSPL_Customer_Invoice_Detail.tax8,'')='' and " & _
                              " coalesce(TSPL_Customer_Invoice_Detail.tax9,'')='' and coalesce(TSPL_Customer_Invoice_Detail.tax10,'')='') )t "

            strMCCMaterial += Environment.NewLine + " union all "
            '' quert for no tax applied
            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX1 ,-TSPL_Customer_Invoice_Detail.TAX1_AMT AS TAX1_AMT ,TSPL_Customer_Invoice_Detail.TAX1_Rate ,TSPL_Customer_Invoice_Detail.TAX1+'%' as tax1rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax1 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX1_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX2 ,-TSPL_Customer_Invoice_Detail.TAX2_Amt AS TAX2_Amt ,TSPL_Customer_Invoice_Detail.TAX2_Rate ,TSPL_Customer_Invoice_Detail.TAX2+'%' as tax2rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax2 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX2_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX3 ,-TSPL_Customer_Invoice_Detail.TAX3_Amt AS TAX3_Amt,TSPL_Customer_Invoice_Detail.TAX3_Rate ,TSPL_Customer_Invoice_Detail.TAX3+'%' as tax3rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax3 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX3_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX4 ,-TSPL_Customer_Invoice_Detail.TAX4_Amt AS TAX4_Amt,TSPL_Customer_Invoice_Detail.TAX4_Rate ,TSPL_Customer_Invoice_Detail.TAX4+'%' as tax4rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax4 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX4_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX5 ,-TSPL_Customer_Invoice_Detail.TAX5_Amt AS TAX5_Amt,TSPL_Customer_Invoice_Detail.TAX5_Rate ,TSPL_Customer_Invoice_Detail.TAX5+'%' as tax5rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax5 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX5_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"

            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX6 ,-TSPL_Customer_Invoice_Detail.TAX6_Amt AS TAX6_Amt,TSPL_Customer_Invoice_Detail.TAX6_Rate ,TSPL_Customer_Invoice_Detail.TAX6+'%' as tax6rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax6 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX6_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX7 ,-TSPL_Customer_Invoice_Detail.TAX7_Amt AS TAX7_Amt,TSPL_Customer_Invoice_Detail.TAX7_Rate ,TSPL_Customer_Invoice_Detail.TAX7+'%' as tax7rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax7 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX7_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX8 ,-TSPL_Customer_Invoice_Detail.TAX8_Amt AS TAX8_Amt,TSPL_Customer_Invoice_Detail.TAX8_Rate ,TSPL_Customer_Invoice_Detail.TAX8+'%' as tax8rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax8 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX8_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX9 ,-TSPL_Customer_Invoice_Detail.TAX9_Amt AS TAX9_Amt,TSPL_Customer_Invoice_Detail.TAX9_Rate ,TSPL_Customer_Invoice_Detail.TAX9+'%' as tax9rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax9 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX9_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"

            strTaxColumns = " TSPL_Customer_Invoice_Detail.TAX10 ,-TSPL_Customer_Invoice_Detail.TAX10_Amt AS TAX10_Amt,TSPL_Customer_Invoice_Detail.TAX10_Rate ,TSPL_Customer_Invoice_Detail.TAX10+'%' as tax10rate  "
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_Customer_Invoice_Detail.tax10 and ttr.tax_Rate=TSPL_Customer_Invoice_Detail.TAX10_Rate and ttr._type<>'OH' and ttr.Tax_Type='T'"
            strMCCMaterial += " select * from (" & strDebitCreditCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_Customer_Invoice_Detail.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t" & _
             " )final" & _
             " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location " & _
             " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code " & _
             " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code =final.Customer_Code " & _
             " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No " & _
             " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
             " left join TSPL_STATE_MASTER on TSPL_LOCATION_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "

            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code,final.Line_No ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount] , [Price Code] ,final.Created_By,final.Modify_By,final.RATE_UOM,final.Conv_Factor,final.Sampling,final.Scheme_Item" '', " + strPivotFoGrouprOuterQuery + " ,QC.FAT_Per,QC.SNF_Per

        End If

        If obj.Trans_Type_List.Contains("Fresh Sale") OrElse obj.Trans_Type_List.Contains("Product Sale") OrElse obj.Trans_Type_List.Contains("MCC Sale") OrElse obj.Trans_Type_List.Contains("Export Sale") OrElse obj.Trans_Type_List.Contains("CSA Sale") OrElse obj.Trans_Type_List.Contains("Merchant Trade") OrElse obj.Trans_Type_List.Contains("General Sale") Then
            strSDCommonQuery = " select TSPL_SD_SALE_INVOICE_HEAD.Description as Narration,case when TSPL_SD_SALE_INVOICE_HEAD.Against_C_Form = 1 then 'C' else '' End as Formtype,case when ISNUll(TSPL_SD_SALE_INVOICE_HEAD.Document_Type,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Document_Type else  CASE WHEN TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='ALL' THEN 'SD' ELSE TSPL_SD_SALE_INVOICE_HEAD.Trans_Type END end as Trans_Type  ,TSPL_SD_SALE_INVOICE_HEAD.Status ,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location, " & _
                           " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,COALESCE(TSPL_SD_SALE_INVOICE_HEAD.Document_Type,TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type) AS Invoice_Type,TSPL_SD_SALE_INVOICE_HEAD.Document_Code , " & _
                           " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103 ) as Document_Date , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Line_No , " & _
                           " (case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 then 0 else TSPL_SD_SALE_INVOICE_DETAIL.Qty end) as Qty, " & _
                           " TSPL_SD_SALE_INVOICE_DETAIL.Unit_code , TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as Item_Cost , " & _
                           " (TSPL_SD_SALE_INVOICE_DETAIL.Amount *(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)- case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as AMount, " & _
                           " TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per ,(TSPL_SD_SALE_INVOICE_detail.Total_Disc_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) - case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Disc_Amt, " & _
                           " (case when coalesce(TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_INVOICE_DETAIL.sampling,0)=1 or coalesce(TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,'')='Y' then Item_Net_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as [Scheme Amount] , " & _
                           " ((Amount-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)- case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='AS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Amt_Less_Discount , " & _
                           " TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Total_Tax_AMt , " & _
                           " (Amount+TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Total_Amt, " & _
                           " case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='PS' then ''  when ManualVehicle <> '' then '' else TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code end as Vehicle_Code , case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='PS' then TSPL_SD_SALE_INVOICE_HEAD.VehicleNo  when ManualVehicle <> '' then ManualVehicle else COALESCE(TSPL_VEHICLE_MASTER.Number,TSPL_SD_SALE_INVOICE_HEAD.VEHICLENO) end as Vehicle_No, " & _
                           " (case when TSPL_SD_SALE_INVOICE_DETAIL.Line_No=1 then (TSPL_SD_SALE_INVOICE_HEAD.Total_Add_Charge+coalesce(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0))*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as  Additional_Charge, " & _
                           " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as [AR Document Amt]," & _
                           " TSPL_Customer_Invoice_Head.Discount_Amount*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as [AR Document Discount Amt], " & _
                           " TSPL_Customer_Invoice_Head.amount_less_Discount*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as [AR Amount Before Tax]," & _
                           " TSPL_Customer_Invoice_Head.total_tax*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as [AR Total Tax], " & _
                           " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as [AR Total Add Charge], " & _
                           " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                           " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,TSPL_SD_SALE_INVOICE_HEAD.GRNo as [GR No],tspl_sd_shipment_head.gr_date as [GR Date],TSPL_SD_SALE_INVOICE_HEAD.WayBillNo as [WayBill No],TSPL_SD_SHIPMENT_HEAD.transport_id as [Transporter Code],case when len(TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual) > 0 then TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual else TSPL_SD_SHIPMENT_HEAD.Transporter_Name end as [Transporter Name],(case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='PS' then TSPL_SD_SHIPMENT_HEAD.Delivery_Code_PS else  TSPL_SD_SALE_INVOICE_DETAIL.Delivery_Code end) as [Delivery No]  ,Shipment_Code as [Shipment No],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no as [Booking No], TSPL_SD_SALE_INVOICE_DETAIL.MRP ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Code as [Scheme Code] ,TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Code as [Cash Scheme Code] ," & _
                           " TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as [Cash Scheme Amount], TSPL_SD_SALE_INVOICE_DETAIL.Price_code as [Price Code],'' as Created_By,'' as Modify_By ,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM,TSPL_SD_SALE_INVOICE_DETAIL.Conv_Factor,tspl_sd_sale_invoice_detail.Sampling,tspl_sd_sale_invoice_detail.Scheme_Item ," & _
                           " (case when TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='S' then 'Supplementry Invoice' when TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then 'Credit Note' when TSPL_SD_SALE_INVOICE_HEAD.Is_Taxable=1 then 'Tax Invoice' else 'Bill Of Supply' end) as [Invoice Type GST],'" & CompGstinNo & "' as [GSTIN No Company],TSPL_CUSTOMER_MASTER.GSTNO as [GSTIN no Customer], " & _
                           " (case when TSPL_SD_SALE_INVOICE_HEAD.Total_Tax_Amt<=0 and TSPL_SD_SALE_INVOICE_HEAD.Tax_Group<>'EXEMPTED' then ((Amount+TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end)) else null end) as [Nill Rate Amount]," & _
                           " (case when TSPL_SD_SALE_INVOICE_HEAD.Tax_Group='EXEMPTED' then ((Amount+TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0 and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end)) else null end) as [Exempted Amount]," & _
                           " 0 as [Non GST Supply],'N' as [Reverse Charge],coalesce(TSPL_SD_SALE_INVOICE_HEAD.Document_Type,'') as [Export Type],TSPL_SD_SALE_INVOICE_HEAD.Loading_Port as Port," & _
                           " TSPL_SD_SALE_INVOICE_HEAD.Against_Com_Inv_No as [Shipping Bill No],convert(varchar,TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Date,103) as [Shipping Bill Date]," & _
                           " TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary as [Original Invoice No],convert(varchar,SupplInvoice.Document_Date,103) as [Original Invoice Date],SupplInvoice.Description as [Reason for Revision],TSPL_SD_SALE_INVOICE_HEAD.LUT_No as [LUT No],(CASE WHEN TAXM1.TYPE='M' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX1_AMT ELSE 0 END+CASE WHEN TAXM2.TYPE='M' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX2_AMT ELSE 0 END+CASE WHEN TAXM3.TYPE='M' THEN TSPL_SD_SALE_INVOICE_DETAIL.TAX3_AMT ELSE 0 END) AS MANDI_TAX_AMT,"

            strSDEndQry = " from TSPL_SD_SALE_INVOICE_DETAIL " & _
                               " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
                               " left join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_INVOICE_HEAD.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                               " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                               " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_SD_SALE_INVOICE_DETAIL.Delivery_Code " & _
                               " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " & _
                               " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & _
                               " LEFT JOIN TSPL_EX_COMMERCIAL_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_HEAD.Against_Com_Inv_No=TSPL_EX_COMMERCIAL_INVOICE_HEAD.Document_Code " & _
                               " LEFT JOIN TSPL_SD_SALE_INVOICE_HEAD SupplInvoice ON TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary=SupplInvoice.Document_Code " & _
                               " LEFT JOIN TSPL_TAX_MASTER TAXM1 ON TSPL_SD_SALE_INVOICE_DETAIL.TAX1=TAXM1.TAX_CODE " & _
                               " LEFT JOIN TSPL_TAX_MASTER TAXM2 ON TSPL_SD_SALE_INVOICE_DETAIL.TAX2=TAXM2.TAX_CODE " & _
                               " LEFT JOIN TSPL_TAX_MASTER TAXM3 ON TSPL_SD_SALE_INVOICE_DETAIL.TAX3=TAXM3.TAX_CODE "

            strSDJoinQry = "  where 2=2 " & If(obj.PickCSASaleFromSalePatti = False, " and  isnull(TSPL_SD_SALE_INVOICE_HEAD.trans_type,'ALL') not in ('CSA') ", "") & "   " & _
                               " and (case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='FS' then 'Fresh Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' then 'Product Sale' " & _
                               " when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' then 'MCC Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type <>'MT' then 'Export Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type ='MT' then 'Merchant Trade' WHEN (TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='SD' or TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='ALl') then 'General Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='CSA' then 'CSA Sale' " & _
                               " else  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                               " and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

            '' filter conditions
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If

            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strSDJoinQry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            If clsCommon.myLen(obj.Document_Code) > 0 Then
                strSDJoinQry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" & obj.Document_Code & "' "
            End If
            '' base union 12
            If qryStarted = True Then
                strMCCMaterial += " union all "
            End If
            qryStarted = True
            strMCCMaterial += " select max(final._Type) as _Type , max(final.FormType) as [Form Type],case when Trans_Type ='FS' then 'Fresh Sale' when Trans_Type ='CSA' then 'CSA Sale' when Trans_Type='PS' then 'Product Sale' when Trans_Type='MCC' then 'MCC Sale' when Trans_Type='EX' then 'Export Sale'when Trans_Type='Bulk Sale' then 'Bulk Sale' when Trans_Type ='SS' then 'Misc Sale' when Trans_Type='MT' then 'Merchant Trade' WHEN Trans_Type ='SD' then 'General Sale' else  Trans_Type end  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date], max(final.Narration) as [Narration],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) AS [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name],max(TSPL_CUSTOMER_MASTER .GST_Registered) as [Registered],max(TSPL_CUSTOMER_MASTER .GST_COMPOSITION) as [Composition],max(TSPL_CUSTOMER_MASTER .City_Code) as [City Code],max(TSPL_CITY_MASTER .City_Name) as [Place of Supply],max(TSPL_STATE_MASTER.GST_STATE_Code) as [Customer GST State Code] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],max(tspl_item_master.HSN_Code) as [HSN Code],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], "

            ''Monika  QC.FAT_Per as [Fat Per],QC.SNF_Per as [SNF Per]
            strMCCMaterial += " 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount],   " & _
                " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]- case when (Trans_Type ='FS') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] ,[Cash Scheme Code] , [Cash Scheme Amount], [Price Code],final.Created_By,final.Modify_By ,final. RATE_UOM,final. Conv_Factor,final.Sampling,final.Scheme_Item, " & _
                " max([Invoice Type GST]) as [Invoice Type GST],max([GSTIN No Company]) as [GSTIN No Company],max([GSTIN No Customer]) as [GSTIN No Customer],max([Nill Rate Amount]) as [Nill Rate Amount],max([Exempted Amount]) as [Exempted Amount],max([Non GST Supply]) as [Non GST Supply],max([Reverse Charge]) as [Reverse Charge],max([Export Type]) as [Export Type],max(Port) as Port,max([Shipping Bill No]) as [Shipping Bill No],max([Shipping Bill Date]) as [Shipping Bill Date],max([Original Invoice No]) as [Original Invoice No],max([Original Invoice Date]) as [Original Invoice Date],max([Reason for Revision]) as [Reason for Revision],max([LUT No]) as [LUT No],max(MANDI_TAX_AMT) as MANDI_TAX_AMT"
            strMCCMaterial += " from ("
            'strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as Tax1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate,TSPL_SD_SALE_INVOICE_DETAIL.TAX1+'%' as Tax1Rate"
            strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
            '' query for no tax applied

            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateBlankColumn & strTaxColumns & strSDEndQry & strSDJoinQry & "  and (coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='' " & _
                              " and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax3,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax4,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax5,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax6,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax7,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax8,'')='' and " & _
                              " coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax9,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax10,'')='') )t "

            '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + ") " & _

            strMCCMaterial += "   union all"
            '' query for tax1 applied
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate,TSPL_SD_SALE_INVOICE_DETAIL.TAX1+'%' as Tax1Rate"

            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax1 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & "  and TSPL_SD_SALE_INVOICE_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX2 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_HEAD.TAX2_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax2_Amt,TSPL_SD_SALE_INVOICE_HEAD.TAX2_Rate, TSPL_SD_SALE_INVOICE_HEAD.TAX2+'%' as Tax2Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax2 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX3 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3+'%' as Tax3Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax3 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "   union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax4_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX4+'%' as Tax4Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax4 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax5_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX5+'%' as Tax5Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax5 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX6 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax6_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX6+'%' as Tax6Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax6 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX7 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax7_AMt,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX7+'%' as Tax7Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax7 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX8 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax8_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX8+'%' as Tax8Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax8 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX9 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax9_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX9+'%' as Tax9Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax9 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += "  union all"
            strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX10 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt*(case when len(TSPL_SD_SALE_INVOICE_HEAD.Invoice_No_For_Supplementary)>0and TSPL_SD_SALE_INVOICE_HEAD.Supplementary_Type='C' then -1 else 1 end) as Tax10_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.TAX10+'%' as Tax10Rate"
            strSDTaxRate = " left join TSPL_TAX_RATES ttr  on ttr.tax_Code=TSPL_SD_SALE_INVOICE_DETAIL.tax10 and ttr.tax_Rate=TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate and ttr._type<>'OH' and ttr.Tax_Type='S'"
            strMCCMaterial += " select * from (" & strSDCommonQuery & strSDTaxRateColumn & strTaxColumns & strSDEndQry & strSDTaxRate & strSDJoinQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
            strMCCMaterial += " )final"
            strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
            strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
            strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
            strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
            strMCCMaterial += " left outer join TSPL_CITY_MASTER on TSPL_CITY_MASTER .City_Code =TSPL_CUSTOMER_MASTER.City_Code " & _
                              " LEFT JOIN TSPL_STATE_MASTER ON TSPL_CUSTOMER_MASTER.State=TSPL_STATE_MASTER.STATE_CODE "


            ''Monika
            ''strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "

            'added by stuti on 01/05/2017
            strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

            ''Monika
            If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
            End If
            If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
            End If
            If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
                strMCCMaterial += " and final.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
            End If
            ''end here

            strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code,final.Line_No ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount] , [Price Code] ,final.Created_By,final.Modify_By,final.RATE_UOM,final.Conv_Factor,final.Sampling,final.Scheme_Item" '', " + strPivotFoGrouprOuterQuery + " ,QC.FAT_Per,QC.SNF_Per

        End If
        strMCCMaterial += ") xx"
        strMCCMaterial += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =xx.[Location Code] "
        strMCCMaterial += " left outer join (select ITEM.Item_Code,ITEM.Item_Desc,ITEM.Structure_Code,SA.Sales_Account, isnull(CA.GSOC_Acct,'') as GSOC_Acct,Item.STD_FATPER,Item.STD_SNFPer,SA.Stock_Transfer_AC from TSPL_ITEM_MASTER Item left join TSPL_SALES_ACCOUNTS SA on Item.Sale_Class_Code=SA.Sales_Class_Code left join TSPL_CUSTOMER_ACCOUNT_SET  CA on Item.Cust_Account =CA.Cust_Account ) Item on Item.Item_Code =xx.[Item Code] "
        ''--------------------------
        '' transaction unit conversion
        strMCCMaterial += " left join (" & qryTransStock & ") as  TransStock on xx.[Item Code]=TransStock.Item_Code "
        If Not Stock_uom Then
            strMCCMaterial += " and TransStock.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " "
        End If
        ''end transaction unit conversion
        strMCCMaterial += " left join (" & strItemGroup & ") as Item_Group on Item.Structure_Code=Item_Group.Structure_Code "
        'strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Location_Code =final.To_location "
        strMCCMaterial += " left join (" & qryStock & ") as Stock_SU on xx.[Item Code]=Stock_SU.Item_Code and xx.[UOM]=Stock_SU.UOM_Code "
        '===============================Added By Preeti Gupta===================================
        strMCCMaterial += " left join (" & qryStock & ") as Rate_Stock_SU on xx.[Item Code]=Rate_Stock_SU.Item_Code and isnull(xx.[RATE_UOM],xx.UOM) =Rate_Stock_SU.UOM_Code  "
        strMCCMaterial += " left join (" & qryTransStock & ") as Rate_select_SU on xx.[Item Code]=Rate_select_SU.Item_Code  "
        If Not Stock_uom Then
            strMCCMaterial += " and Rate_select_SU.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & ""
        End If
        ' ============================================================================================
        strMCCMaterial += " left join (" & qryKG & ") as StockKG on xx.[Item Code]=StockKG.Item_Code  "
        strMCCMaterial += " left join (select Cust_Code,Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_CUSTOMER_MASTER.Struct_Code,TSPL_CUSTOMER_MASTER.Tin_No,TSPL_CUSTOMER_MASTER.state as State_Code,tspl_State_Master.State_Name from TSPL_CUSTOMER_MASTER left join tspl_State_Master on tspl_State_Master.state_Code=TSPL_CUSTOMER_MASTER.state) as Cust on xx.[Customer Code]=Cust.Cust_Code  left join (select location_Code as Cust_Code,Tin_No,State_Code,State_Name from TSPL_location_MASTER left join tspl_State_Master on tspl_State_Master.state_Code=TSPL_location_MASTER.state) as Cust_Loc on xx.[Customer Code]=Cust_Loc.Cust_Code  "
        strMCCMaterial += " left join (select Cust_Group_Code,Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER) as Cust_Group on Cust.Cust_Group_Code=Cust_Group.Cust_Group_Code "
        strMCCMaterial += " left join (select Zone_Code,Description from TSPL_ZONE_MASTER) as Zone on Cust.Zone_Code=Zone.Zone_Code " & _
                          " left join TSPL_LOCATION_MASTER as Loc on Loc.Location_Code=xx.[Location Code] " & _
                          " left join TSPL_STATE_MASTER GSTState on loc.State=GSTState.STATE_CODE "
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMCCMaterial += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=xx.[Item Code]"
        End If

        ''==========Monika
        'strMCCMaterial += " left outer join (" + qryQC + ")QC on QC.item_code=xx.[Item Code] "
        ''================end here=====

        strMCCMaterial += " where 2 = 2  "
        ''convert(date,xx.Document_Date,103) >= convert(date,('" + From_Date + "'),103) and convert(date,xx.Document_Date,103) <= convert(date,('" + To_Date + "'),103) 
        ''===========Monika
        If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strMCCMaterial += " and xx.[Location Code] in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
        End If
        If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strMCCMaterial += " and xx.[Item Code] in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
        End If
        If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strMCCMaterial += " and xx.[Customer Code] in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
        End If
        ''========end here

        QryLst.Add(strMCCMaterial)


        'Dim fullpath As String = "D:\RMWebBrowser\RMWebBrowser\bin\Debug" & "\Muhil.xml"
        'Dim fileinfo As System.IO.FileInfo = New System.IO.FileInfo(fullpath)
        'Dim XMLWriter As System.Xml.XmlTextWriter = New System.Xml.XmlTextWriter(fullpath, System.Text.Encoding.UTF8)
        'XMLWriter.Formatting = Xml.Formatting.Indented
        'XMLWriter.WriteStartDocument()
        'XMLWriter.WriteElementString("Name", strMCCMaterial)
        QryLst.Add(strPivotForFinalOuterQuery)
        'clsCommon.MyMessageBoxShow("DOne")
        'Return Nothing
        Return QryLst
    End Function
    Public Shared Function ReturnQueryforDocumentInfoLevel(ByVal obj As clsSaleRegisterForAutoCompParameterType) As ArrayList
        Dim stock_uom As Boolean = obj.stockinguom
        Dim From_Date As Date = obj.From_Date
        Dim To_Date As Date = obj.To_Date
        Dim Unit_Code As String = obj.Unit_Code
        Dim QryLst As New ArrayList

        Dim strCodeColumn As String = ""
        Dim strCodeColumnForVirtual As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""
        Dim strPivotForFinalOuterQuery As String = ""
        Dim strCategoryTable As String = ""
        Dim MIS_Item_Group As String = GetMIS_ITem_GroupColumn()
        Dim dtCategory As New DataTable
        ''===========below If cond.===Add by moinka 12/04/2017
        If obj.rbtnCategorySected OrElse (clsCommon.CompairString(obj.ReportType, "Document Info Level") = CompairStringResult.Equal OrElse clsCommon.CompairString(obj.ReportType, "Document Detail") = CompairStringResult.Equal) Then ''if categories are checked from screen only then show as pivot.Or if document detail/info report opened then category pivot run
            dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+' Description' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")
            If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
                For ii As Integer = 0 To dtCategory.Rows.Count - 1
                    If ii <> 0 Then
                        strCodeColumn += ","
                        strCodeColumnForVirtual += ","
                        strCodeColumnMax += ","
                        strCodeDescColumn += ","
                        strCodeDescColumnMax += ","
                    End If
                    strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumnForVirtual += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                    strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                    strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"
                Next
                strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
                " select * from ( " + Environment.NewLine & _
                " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+' Description' as Item_Category_CodeDesc " + Environment.NewLine & _
                " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
                " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
                " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
                " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
                " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
                " where 2=2 " + Environment.NewLine & _
                " )xx" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
                " ) Pivt" + Environment.NewLine & _
                " Pivot " + Environment.NewLine & _
                " (" + Environment.NewLine & _
                " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
                " ) Pivt1 " + Environment.NewLine & _
                " ) xxx group by Item_Code "
                ''End of Category Table start now.
            End If
        End If

        ''Virtual Category Table start now.


        Dim strMCCMaterial As String = ""
        Dim qryTaxQuery As String = ""
        Dim strPivotForOuter As String
        Dim strPivotForOuterMax As String
        'Dim lstTables As New List(Of String)
        'lstTables.Add("TSPL_SD_SALE_INVOICE_DETAIL")
        'lstTables.Add("TSPL_SCRAPINVOICE_DETAIL")
        'lstTables.Add("TSPL_SD_SALE_RETURN_DETAIL")
        'lstTables.Add("TSPL_TRANSFER_ORDER_DETAIL")
        'lstTables.Add("TSPL_CSA_TRANSFER_DETAIL")
        qryTaxQuery = GetTaxQuery(obj)

        'strPivotForOuter = " select distinct (select Distinct ',sum(isnull(final.'+tax1+',0)) as '+TAX1 from ( " & qryTaxQuery
        strPivotForOuter = "select distinct (select Distinct ',sum(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuter))
        '===============================================
        strPivotForOuterMax = "select distinct (select Distinct ',max(isnull(final.['+tax1+'],0)) as ['+TAX1+']' from ( " & qryTaxQuery
        strPivotForOuterMax += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQueryMAX As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterMax))
        '============================

        Dim strPivotForFinalOuter As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',xx.['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForFinalOuterQuery = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))

        Dim strPivotForFinalOuterPercent As String
        strPivotForFinalOuterPercent = " select distinct (select  Distinct ',xx.['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForFinalOuterPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotForFinalOuterPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuterPercent))

        Dim strPivotForTransfer_In As String
        strPivotForFinalOuter = ""
        strPivotForFinalOuter = " select distinct (select Distinct ',0 as ['+tax1+']' from ( " & qryTaxQuery
        strPivotForFinalOuter += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        strPivotForTransfer_In = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForFinalOuter))

        Dim strPivotFortRANSFER_INPercent As String
        strPivotFortRANSFER_INPercent = " select distinct (select  Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotFortRANSFER_INPercent += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"
        Dim strPivotFortRANSFER_INPercentQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotFortRANSFER_INPercent))
        '===========

        Dim strPivotForGroupOuter As String
        strPivotForGroupOuter = "select SUBSTRING(ax,2,len(Ax)) from ("
        'strPivotForGroupOuter += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery
        strPivotForGroupOuter += " select distinct (select Distinct ',max(isnull(final.['+tax1+'%'+'],0)) as ['+TAX1+'%'+']' from ( " & qryTaxQuery

        strPivotForGroupOuter += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"
        Dim strPivotFoGrouprOuterQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForGroupOuter))



        Dim strPivotForOuterForBulk As String
        strPivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+TAX1+']' from ( " & qryTaxQuery

        strPivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strPivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForOuterForBulk))

        Dim strDoublePivotForOuterForBulk As String

        strDoublePivotForOuterForBulk = " select distinct (select Distinct ',0 as ['+tax1+'%'+']' from ( " & qryTaxQuery


        strDoublePivotForOuterForBulk += " )aa where len(isnull(TAX1,''))>0 for xml path('') )"

        Dim strDoublePivotForOuterQueryforBulk As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForOuterForBulk))


        Dim strPivotForInner As String
        strPivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strPivotForInner += " select distinct (select Distinct ',['+tax1+']' from ( " & qryTaxQuery

        strPivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strPivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))

        '' taxcolumns for no tax 
        strPivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strPivotForInner += " select distinct (select Distinct ',Null as ['+tax1+']' from ( " & qryTaxQuery

        strPivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strPivotForInnerQueryNoTax As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strPivotForInner))


        Dim strDoublePivotForInner As String
        strDoublePivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strDoublePivotForInner += " select distinct (select Distinct ',['+tax1+'%'+']' from ( " & qryTaxQuery

        strDoublePivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strDoublePivotForInnerQuery As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))

        '' tax rate columns for no tax 
        strDoublePivotForInner = "select SUBSTRING(ax,2,len(Ax)) from ("
        strDoublePivotForInner += " select distinct (select Distinct ',Null as ['+tax1+'%'+']' from ( " & qryTaxQuery

        strDoublePivotForInner += " )a where len(isnull(TAX1,''))>0 for xml path('') )ax)Axx"

        Dim strDoublePivotForInnerQueryNoTax As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(strDoublePivotForInner))


        Dim qryQC As String = ""
        qryQC = " select Item_Code,MAX(Fat_Per) as Fat_Per,MAX(SNF_Per) as SNF_Per from (" & _
                " select Item_QCP.Item_Code,Item_QCP.Code as Parameter_Code,(case when QCP.Type='FAT' then Item_QCP.Actual_Range end) as Fat_Per," & _
                " (case when QCP.Type='SNF' then Item_QCP.Actual_Range  end) as SNF_Per from TSPL_ITEM_QC_PARAMETER_MASTER Item_QCP " & _
                " left join TSPL_PARAMETER_MASTER QCP  on Item_QCP.Code=QCP.Code) as QC group by Item_Code"

        Dim qryKG As String = ""
        qryKG = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG'"
        'qryKG = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='KG'"
        'qryKG = " select distinct TSPL_ITEM_UOM_DETAIL.Item_Code,coalesce(Weigt1.Container_UOM,Weigt2.Container_UOM) as UOM_Code, " & _
        '        " (case when Weigt1.Contained_UOM='KG' then round(Weigt1.Contained_Qty/Weigt1.Container_Qty,4) " & _
        '        " when Weigt2.Container_UOM='KG' then round(Weigt2.Container_Qty/Weigt2.Contained_Qty,4) else 0 end)*TSPL_ITEM_MASTER.Weight_Value as Conversion_Factor " & _
        '        " from TSPL_ITEM_UOM_DETAIL " & _
        '        " left join TSPL_ITEM_MASTER on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code " & _
        '        " left join (select distinct Container_Qty,Container_UOM,Contained_Qty,Contained_UOM,Product_Type from TSPL_WEIGHT_CONVERSION where Contained_UOM='KG') Weigt1 " & _
        '        " on TSPL_ITEM_MASTER.Weight_UOM=Weigt1.Container_UOM  and (TSPL_ITEM_MASTER.Product_Type=Weigt1.Product_Type or Weigt1.Product_Type='All') " & _
        '        " left join (select distinct Container_Qty,Container_UOM,Contained_Qty,Contained_UOM,Product_Type from TSPL_WEIGHT_CONVERSION where Container_UOM='KG') Weigt2 " & _
        '        " on TSPL_ITEM_MASTER.Weight_UOM=Weigt2.Contained_UOM and (TSPL_ITEM_MASTER.Product_Type=Weigt2.Product_Type or Weigt2.Product_Type='All')  " & _
        '        " where  2=2 and coalesce(Weigt1.Container_UOM,Weigt2.Container_UOM) is not null and coalesce(Weigt1.Container_UOM,Weigt2.Container_UOM) in ('KG','Ltr')"

        '' done by Panch Raj from Preeti User Id
        'qryKG = " select distinct TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Weight_UOM," & _
        '        " max(case when TSPL_ITEM_MASTER.Weight_UOM='KG' then 1 " & _
        '        " else  coalesce(Weigt1.Contained_Qty/Weigt1.Container_Qty,Weigt2.Container_Qty/Weigt2.Contained_Qty)end)*TSPL_ITEM_MASTER.Weight_Value " & _
        '        " as Conversion_Factor from TSPL_ITEM_MASTER " & _
        '        " left join (select distinct Container_Qty,Container_UOM,Contained_Qty,Contained_UOM,Product_Type from TSPL_WEIGHT_CONVERSION ) Weigt1 " & _
        '        " on TSPL_ITEM_MASTER.Weight_UOM=Weigt1.Container_UOM and Weigt1.Contained_UOM='KG'  " & _
        '        " and (TSPL_ITEM_MASTER.Product_Type=Weigt1.Product_Type or Weigt1.Product_Type='All') " & _
        '        " left join (select distinct Container_Qty,Container_UOM,Contained_Qty,Contained_UOM,Product_Type from TSPL_WEIGHT_CONVERSION ) Weigt2 " & _
        '        " on TSPL_ITEM_MASTER.Weight_UOM=Weigt2.Contained_UOM and  Weigt2.Container_UOM='KG' " & _
        '        " and (TSPL_ITEM_MASTER.Product_Type=Weigt2.Product_Type or Weigt2.Product_Type='All')" & _
        '        " where  2=2 and len(coalesce(TSPL_ITEM_MASTER.Weight_UOM,''))>0 group by TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Weight_UOM,TSPL_ITEM_MASTER.Weight_Value"
        Dim qryStock As String = ""
        qryStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "

        '' query for transaction  UOM conversion
        Dim qryTransStock As String = ""
        If clsCommon.myLen(Unit_Code) <= 0 AndAlso Not stock_uom Then
            qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL "
        ElseIf clsCommon.myLen(Unit_Code) <= 0 AndAlso stock_uom Then
            qryTransStock = "select Item_Code,max(UOM_Code) as UOM_Code,max(Conversion_Factor) as Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  Stocking_Unit='Y'  group by Item_Code "
        Else
            qryTransStock = "select Item_Code,UOM_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where UOM_Code='" & Unit_Code & "'"
        End If
        '===================Added By Preeti Gupta===================
        Dim qryRateStock As String = ""
        ' =============================================================

        '' end query for transaction  UOM conversion
        ' BM00000008464 BM00000008484
        '' query for structure and item group custom field========================BM00000008352
        '==============added by preeti Gupta against ticket No[BM00000009864,BM00000007115]
        Dim strItemGroup As String = ""
        strItemGroup = " select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join (" & _
                       " select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'  " & _
                       " and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code" & _
                       " left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value "

        Dim strSDCommonQuery As String = ""
        Dim strTaxColumns As String = ""
        Dim strSDEndQry As String = ",TSPL_SD_SALE_INVOICE_DETAIL.TAX1+'%' as Tax1_Rate"
        strSDCommonQuery = " select case when ISNUll(TSPL_SD_SALE_INVOICE_HEAD.Document_Type,'')<>'' then TSPL_SD_SALE_INVOICE_HEAD.Document_Type else  CASE WHEN TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='ALL' THEN 'SD' ELSE TSPL_SD_SALE_INVOICE_HEAD.Trans_Type END end as Trans_Type  ,TSPL_SD_SALE_INVOICE_HEAD.Status ,TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location, " & _
                           " TSPL_SD_SALE_INVOICE_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,COALESCE(TSPL_SD_SALE_INVOICE_HEAD.Document_Type,TSPL_SD_SALE_INVOICE_HEAD.Invoice_Type) AS Invoice_Type,TSPL_SD_SALE_INVOICE_HEAD.Document_Code , " & _
                           " convert(varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103 ) as Document_Date , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code,TSPL_SD_SALE_INVOICE_DETAIL.Line_No , " & _
                           " TSPL_SD_SALE_INVOICE_DETAIL.Qty ,TSPL_SD_SALE_INVOICE_DETAIL.Unit_code , TSPL_SD_SALE_INVOICE_DETAIL.Item_Cost*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as Item_Cost , " & _
                           " TSPL_SD_SALE_INVOICE_DETAIL.Amount *(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)- case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end as AMount,TSPL_SD_SALE_INVOICE_DETAIL.Disc_Per ,TSPL_SD_SALE_INVOICE_detail.Total_Disc_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) - case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end as Disc_Amt,case when coalesce(TSPL_SD_SALE_INVOICE_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_INVOICE_DETAIL.sampling,0)=1 or coalesce(TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item,'')='Y' then Item_Net_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) end as [Scheme Amount] , " & _
                           " (TSPL_SD_SALE_INVOICE_detail.Amount-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end)- case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='AS' then coalesce(TSPL_SD_SALE_INVOICE_Detail.Cash_Scheme_Amount,0)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end as Amt_Less_Discount , " & _
                           " TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as Total_Tax_AMt ,(TSPL_SD_SALE_INVOICE_DETAIL.Amount+TSPL_SD_SALE_INVOICE_DETAIL.Total_Tax_Amt-Total_Disc_Amt)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as Total_Amt,case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='PS' then '' else TSPL_SD_SALE_INVOICE_HEAD.Vehicle_Code end as Vehicle_Code , case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='PS' then TSPL_SD_SALE_INVOICE_HEAD.VehicleNo else COALESCE(TSPL_VEHICLE_MASTER.Number,TSPL_SD_SALE_INVOICE_HEAD.VEHICLENO) end as Vehicle_No,(case when TSPL_SD_SALE_INVOICE_DETAIL.Line_No=1 then (TSPL_SD_SALE_INVOICE_HEAD.Total_Add_Charge+coalesce(TSPL_SD_SALE_INVOICE_HEAD.RoundOffAmount,0))*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) else 0 end) as  Additional_Charge, " & _
                           " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Document Amt]," & _
                           " TSPL_Customer_Invoice_Head.Discount_Amount*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Document Discount Amt], " & _
                           " TSPL_Customer_Invoice_Head.amount_less_Discount*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Total Tax], " & _
                           " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount)*(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) as [AR Total Add Charge], " & _
                           " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                           " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,TSPL_SD_SALE_INVOICE_HEAD.GRNo as [GR No],TSPL_SD_SHIPMENT_HEAD.gr_date as [GR Date],TSPL_SD_SALE_INVOICE_HEAD.WayBillNo as [WayBill No],TSPL_SD_SHIPMENT_HEAD.transport_id as [Transporter Code] ,case when len(TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual) > 0 then TSPL_SD_SHIPMENT_HEAD.Transporter_Name_Manual else TSPL_SD_SHIPMENT_HEAD.Transporter_Name end as [Transporter Name],(case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='PS' then TSPL_SD_SHIPMENT_HEAD.Delivery_Code_PS else  TSPL_SD_SALE_INVOICE_DETAIL.Delivery_Code end) as [Delivery No]  ,Shipment_Code as [Shipment No],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.booking_no as [Booking No], TSPL_SD_SALE_INVOICE_DETAIL.MRP ,TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Code as [Scheme Code] ,TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Code as [Cash Scheme Code] ,TSPL_SD_SALE_INVOICE_DETAIL.Cash_Scheme_Amount as [Cash Scheme Amount], TSPL_SD_SALE_INVOICE_DETAIL.Price_code as [Price Code],case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='PS' then TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code else case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no  else '' end end   as Ref_Doc_no," & _
                            " case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='PS' then  TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_date  " & _
                             " when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_date else NULL end  as Ref_doc_date," & _
                            "  case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='PS' then TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Created_By  when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then tspl_item_price_master.Created_By else '' end  Created_By , " & _
                            "case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='PS' then TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Modify_By  when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then tspl_item_price_master.Modify_By else '' end  as Modify_By , " & _
                            " case when TSPL_SD_SALE_INVOICE_HEAD.trans_type='PS' then TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Modify_date when TSPL_SD_SALE_INVOICE_HEAD.trans_type='FS' then tspl_item_price_master.Modify_Date else NULL end  as Modify_date ,TSPL_SD_SALE_INVOICE_DETAIL.RATE_UOM,TSPL_SD_SALE_INVOICE_DETAIL.Conv_Factor,tspl_sd_sale_invoice_detail.Sampling,tspl_sd_sale_invoice_detail.Scheme_Item ,"

        strSDEndQry = " from TSPL_SD_SALE_INVOICE_DETAIL " & _
                           " left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code =TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
                           " left join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_INVOICE_HEAD.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                           " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_SD_SALE_INVOICE_HEAD.Document_Code " & _
                           " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No=TSPL_SD_SALE_INVOICE_DETAIL.Delivery_Code " & _
                           " left join TSPL_DELIVERY_NOTE_Detail_FRESHSALE on TSPL_DELIVERY_NOTE_Detail_FRESHSALE.document_no=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.document_no  and TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Item_Code =TSPL_SD_SALE_INVOICE_DETAIL.Item_Code " & _
                           " left join tspl_item_price_master on tspl_item_price_master.Item_Price_ID in (Select  Item_Price_ID from  ( " & _
                           " Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " & _
                           " Start_Date Desc) as RowNo, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " & _
                           " Item_Basic_Price,Item_Basic_Net,Price_Code from TSPL_ITEM_PRICE_MASTER  left  outer join " & _
                           " TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " & _
                           " TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_Date and  " & _
                           " TSPL_ITEM_PRICE_MASTER.Price_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Price_code and UOM=TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Unit_code and TSPL_ITEM_PRICE_MASTER.item_code=TSPL_DELIVERY_NOTE_Detail_FRESHSALE.Item_Code AND Location_Code=TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Location_Code ) XXXE WHERE RowNo=1  )" & _
                           " left join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No=TSPL_SD_SHIPMENT_HEAD.Document_Code " & _
                           " left join TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE on TSPL_DELIVERY_ORDER_HEAD_PRODUCTSALE.Document_Code =TSPL_SD_SHIPMENT_head.Delivery_Code_ps " & _
                           " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " & _
                           " where  isnull(TSPL_SD_SALE_INVOICE_HEAD.trans_type,'ALL') not in ('CSA')  " & _
                           " and (case when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='FS' then 'Fresh Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='PS' then 'Product Sale' " & _
                           " when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='MCC' then 'MCC Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type <>'MT' then 'Export Sale' when TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type ='MT' then 'Merchant Trade' WHEN TSPL_SD_SALE_INVOICE_HEAD.Trans_Type ='SD' then 'General Sale' " & _
                           " else  TSPL_SD_SALE_INVOICE_HEAD.Trans_Type end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                           " and  convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

        '' filter conditions
        If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
        End If
        If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
        End If

        If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
        End If
        If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDEndQry += " and TSPL_SD_SALE_INVOICE_HEAD.Document_Code = '" & obj.Document_Code & "' "
        End If
        '===================added by preeti gupta against ticket no[BM00000009858]
        If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowParticluarColumnInSalesRegisterForGopalJee, clsFixedParameterCode.ShowParticluarColumnInSalesRegisterForGopalJee, Nothing)) = 1 Then
            strMCCMaterial = " select [Trans Type],[Document_date],[Document No],[Location Code]as [Warehouse Code], [Location Name] as [Warehouse Name],Cust.Cust_Group_Code as [Customer Group Code],Cust_Group.Cust_Group_Desc as [Customer Group Description],[Customer Code],[Customer Name],Item_Group.Item_Group as [Product Group Code],Item_Group.Group_Description as [Product Group Description],Vehicle_Code as [Vehicle Code],Vehicle_No as [Vehicle No] "

            strMCCMaterial += " , [Item Code] as [Product Code],[Item Name] as [Product Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(case when coalesce(TransStock.Conversion_Factor,1)=0 then 1 else coalesce(TransStock.Conversion_Factor,1) end) as Numeric(18,3)) as [Pack Sold]," & IIf(clsCommon.myLen(Unit_Code) <= 0, IIf(stock_uom = True, "TransStock.UOM_Code", "xx.[UOM]"), "'" & Unit_Code & "'") & " as [Unit of sale],"
            If clsCommon.myLen(Unit_Code) <= 0 AndAlso Not stock_uom Then
                strMCCMaterial += " cast(([Item Cost]*Stock_SU.Conversion_Factor)/(case when coalesce(rate_stock_su.Conversion_Factor,1)=0 then 1 else coalesce(rate_stock_su.Conversion_Factor,1) end) as Numeric(18,3))  as [Rate] "
            Else
                strMCCMaterial += "  cast(( case when isnull(Rate_Stock_SU.Conversion_Factor,0)<=0 then ([Item Cost]) else ([Item Cost] * Rate_select_SU.Conversion_Factor)/ Rate_Stock_SU.Conversion_Factor end )"
                strMCCMaterial += " as Numeric(18,3)) as [Rate]"
            End If
            strMCCMaterial += " ,Amount " + strPivotForFinalOuterQuery + " ,[Total Tax Amount], [Total Amount] as [Total Amount],'' as Remarks,''as Relation,'' as CTYPE "

        Else
            strMCCMaterial = " select [Trans Type],[Location Code],[Location Name],Loc.State as [Location State],loc.TIN_No as [Dispatch Location Tin No], (CASE WHEN [Invoice Type]='T' THEN 'Tax' when [Invoice Type]='R' then 'Retail' when [Invoice Type]='N' then 'None' else [Invoice Type] end) as [Invoice Type],[Document No],[Document_date],Vehicle_Code as [Vehicle Code],Vehicle_No as [Vehicle No],cast(Additional_Charge as numeric(18,2)) as [Additional Amount],[Customer Code],[Customer Address],Cust.Struct_Code,[Customer Name],Cust.Cust_Group_Code as [Customer Group Code],Cust_Group.Cust_Group_Desc as [Customer Group Description],Cust.Zone_Code as [Customer Zone Code],Zone.Description as [Customer Zone Description], [Parent Customer No],[Parent Customer Code], [Parent Customer Name],coalesce(Cust.State_Code,Cust_Loc.State_Code) as [Customer State Code],coalesce(Cust.State_Name,Cust_Loc.State_Name) as [Customer State Desc],coalesce(Cust.Tin_No,cust_loc.Tin_No) as [Tin No],Item_Group.Item_Group as [Item Group Code],Item_Group.Group_Description as [Item Group Description] "
            If clsCommon.myLen(strCategoryTable) > 0 Then
                ''richa agarwal to avoid ambiguous error
                '  strMCCMaterial += "," + strCodeColumn + "," + strCodeDescColumn
                strMCCMaterial += "," + strCodeColumnForVirtual + "," + strCodeDescColumn
            End If
            ' BM00000008438 BM00000008391
            'strMCCMaterial += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(case when coalesce(TransStock.Conversion_Factor,1)=0 then 1 else coalesce(TransStock.Conversion_Factor,1) end) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & " as [UOM],[Item Cost] as [Item Rate],[Fat Per] as [FAT %],[SNF Per] as [SNF %],cast(([Quantity]*[Fat Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as numeric(18,3)) as [FAT KG],cast(([Quantity]*[SNF Per]*Stock_SU.Conversion_Factor)/(100*coalesce(StockKG.Conversion_Factor,1)) as Numeric(18,3)) as [SNF KG],Amount,[Discount Per] as [Discount %],  (coalesce( [Discount Amount],0)-coalesce([Scheme Amount],0))  as [Discount Amount],[Scheme Amount],[Amount Less Discount]  as [Amount Less Discount]" + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + ",case when [trans type]='Fresh Sale Retturn' then [Amount Less Discount] + case when coalesce([Total Tax Amount],0)<0 then 0 else coalesce( [Scheme Amount],0) end else ([Total Amount]-[Total Tax Amount] + case when coalesce([Total Tax Amount],0)>0 then 0 else coalesce( [Scheme Amount],0) end) end as [Sale Amount],[Total Tax Amount], (cast(Additional_Charge as numeric(18,2))+[Total Amount]) as [Total Amount], " & _
            strMCCMaterial += " , [Item Code],[Item Name],cast(([Quantity]*Stock_SU.Conversion_Factor)/(case when coalesce(TransStock.Conversion_Factor,1)=0 then 1 else coalesce(TransStock.Conversion_Factor,1) end) as Numeric(18,3)) as [Quantity]," & IIf(clsCommon.myLen(Unit_Code) <= 0, IIf(stock_uom = True, "TransStock.UOM_Code", "xx.[UOM]"), "'" & Unit_Code & "'") & " as [UOM],"
            If clsCommon.myLen(Unit_Code) <= 0 AndAlso Not stock_uom Then
                strMCCMaterial += " cast(([Item Cost]*Stock_SU.Conversion_Factor)/(case when coalesce(rate_stock_su.Conversion_Factor,1)=0 then 1 else coalesce(rate_stock_su.Conversion_Factor,1) end) as Numeric(18,3))  as [Item Rate] "
            Else
                strMCCMaterial += "  cast(( case when isnull(Rate_Stock_SU.Conversion_Factor,0)<=0 then ([Item Cost]) else ([Item Cost] * Rate_select_SU.Conversion_Factor)/ Rate_Stock_SU.Conversion_Factor end )"
                strMCCMaterial += " as Numeric(18,3)) as [Item Rate]"
            End If
            strMCCMaterial += " , [Fat Per] as [FAT %],[SNF Per] as [SNF %],(case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast(([Quantity]* (case when [trans type] in ('Bulk Sale Trade','Bulk Sale','Bulk Sale Return','MCC Transfer','SS','Tanker Dispatch Return','MCC Tanker Dispatch Return') then [Fat Per] else QC.fat_per end) *Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as numeric(18,3)) end) as [FAT KG],(case when coalesce(StockKG.Conversion_Factor,0)=0 then 0 else cast(([Quantity]* (case when [trans type] in ('Bulk Sale Trade','Bulk Sale','Bulk Sale Return','MCC Transfer','SS','Tanker Dispatch Return','MCC Tanker Dispatch Return') then [SNF Per] else snf_per end) *Stock_SU.Conversion_Factor)/(coalesce(StockKG.Conversion_Factor,1)*100) as Numeric(18,3)) end) as [SNF KG],Amount,[Discount Per] as [Discount %],  (coalesce( [Discount Amount],0)-coalesce([Scheme Amount],0))  as [Discount Amount],[Scheme Amount],[Amount Less Discount]  as [Amount Less Discount]" + strPivotForFinalOuterQuery + " " + strPivotForFinalOuterPercentQuery + ",case when [trans type]='Fresh Sale Return' then [Amount Less Discount]  else ([Total Amount]-[Total Tax Amount] + case when coalesce([Total Tax Amount],0)>0 then 0 else coalesce( [Scheme Amount],0) end) end as [Sale Amount],[Total Tax Amount], (cast(Additional_Charge as numeric(18,2))+[Total Amount]) as [Total Amount], " & _
            " [AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax]+ case when (coalesce([Total Tax Amount],0)<>0 or [Scheme Amount]<=0) and [Document No]<>'SRFS-003/15-16/000006' then 0 else coalesce([AR Document Discount Amt],0)  end as [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge], "
            ''richa agarwal change to show csa sales account for csa sale and csa sale return
            'left(Item.Sales_Account, Len(Item.Sales_Account)-3)+  TSPL_LOCATION_MASTER.Loc_Segment_Code as [Sales Account],
            strMCCMaterial += "  case when [trans type] in ('CSA Sale','CSA Sale Return') then case when item.GSOC_Acct<>'' then  left( item.GSOC_Acct, Len( item.GSOC_Acct)-3)+  TSPL_LOCATION_MASTER.Loc_Segment_Code else '' end  else  left(Item.Sales_Account, Len(Item.Sales_Account)-3)+  TSPL_LOCATION_MASTER.Loc_Segment_Code  end as [Sales Account], " & _
            " xx.[GR No],case when convert(date,xx.[GR Date],103)='01/Jan/1990' or convert(date,xx.[GR Date],103)='01/Jan/0001' then NULL else  convert(varchar,xx.[GR Date],103) end as [GR Date]  ,[WayBill No],[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount], [Price Code], case when Sampling=0 then  'N' else case when sampling=1 then'Y' end end as sampling, Scheme_Item as [Scheme Type],xx.Ref_Doc_no as [Ref Doc No],convert(varchar,xx.Ref_doc_date,103) as [Ref Doc Date],Created_By.User_Name as [Created By],Modify_By.User_Name as [Posted By],Convert(varchar,xx.Modify_date,103) as [Modify date] "
        End If


        ''richa agarwal add merchant trade trans_type in below qry BM00000008390 (Applied For DCC Also)
        strMCCMaterial += " from (select case when Trans_Type ='FS' then 'Fresh Sale' when Trans_Type ='CSA' then 'CSA Sale' when Trans_Type='PS' then 'Product Sale' when Trans_Type='MCC' then 'MCC Sale' when Trans_Type='EX' then 'Export Sale'when Trans_Type='Bulk Sale' then 'Bulk Sale' when Trans_Type ='SS' then 'Misc Sale' when Trans_Type='MT' then 'Merchant Trade' WHEN Trans_Type ='SD' then 'General Sale' else  Trans_Type end  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) AS [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQueryMAX + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount],   " & _
            " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]- case when (Trans_Type ='FS') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] ,[Cash Scheme Code] , [Cash Scheme Amount], [Price Code],final.Ref_Doc_no,final.Ref_doc_date,final.Created_By,final.Modify_By,final.Modify_date ,final. RATE_UOM,final. Conv_Factor,final.Sampling,final.Scheme_Item "
        strMCCMaterial += " from ("
        'strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as Tax1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate,TSPL_SD_SALE_INVOICE_DETAIL.TAX1+'%' as Tax1Rate"
        strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
        '' query for no tax applied
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & "  and (coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax1,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax3,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax5,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax7,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax9,'')='' and coalesce(TSPL_SD_SALE_INVOICE_DETAIL.tax10,'')='') )t "

        '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + ") " & _

        strMCCMaterial += "   union all"
        '' query for tax1 applied
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX1 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Amt as Tax1_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX1_Rate,TSPL_SD_SALE_INVOICE_DETAIL.TAX1+'%' as Tax1Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & "  and TSPL_SD_SALE_INVOICE_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX2 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Amt as Tax2_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX2_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX2+'%' as Tax2Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX3 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Amt as Tax3_Amt, TSPL_SD_SALE_INVOICE_DETAIL.TAX3_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX3+'%' as Tax3Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX4 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Amt as Tax4_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX4_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX4+'%' as Tax4Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX5 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Amt as Tax5_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX5_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX5+'%' as Tax5Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX6 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Amt as Tax6_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX6_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX6+'%' as Tax6Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX7 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Amt as Tax7_AMt,TSPL_SD_SALE_INVOICE_DETAIL.TAX7_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX7+'%' as Tax7Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX8 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Amt as Tax8_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX8_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX8+'%' as Tax8Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX9 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Amt as Tax9_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX9_Rate, TSPL_SD_SALE_INVOICE_DETAIL.TAX9+'%' as Tax9Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_SD_SALE_INVOICE_DETAIL.TAX10 ,(case when coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_INVOICE_HEAD.convrate,0) end) * TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Amt as Tax10_Amt,TSPL_SD_SALE_INVOICE_DETAIL.TAX10_Rate,TSPL_SD_SALE_INVOICE_DETAIL.TAX10+'%' as Tax10Rate"
        strMCCMaterial += " select * from (" & strSDCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_INVOICE_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " )final"
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
        strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
        'strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
        'added by stuti on 01/05/2017
        strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

        strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code,final.Line_No ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount] , [Price Code] ,final.Ref_Doc_no,final.Ref_doc_date,final.Created_By,final.Modify_By,final.Modify_date,final.Ref_Doc_no,final.Ref_doc_date,final.RATE_UOM,final.Conv_Factor,final.Sampling,final.Scheme_Item " '', " + strPivotFoGrouprOuterQuery + "

        '' add csa transfer 
        Dim stCSACommonQuery As String = " select  'CSA Sale' as  Trans_Type,TSPL_CSA_TRANSFER_HEAD.Status ,TSPL_CSA_TRANSFER_HEAD.From_Location_Code as Bill_To_Location," & _
                                          " TSPL_CSA_TRANSFER_HEAD.Cust_Code as Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,'CSA' as Invoice_Type,TSPL_CSA_TRANSFER_HEAD.Doc_Code as Document_Code ," & _
                                          " convert(varchar,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103 ) as Document_Date , TSPL_CSA_TRANSFER_DETAIL.Item_Code , " & _
                                          " TSPL_CSA_TRANSFER_DETAIL.Qty ,TSPL_CSA_TRANSFER_DETAIL.Unit_code ,TSPL_CSA_TRANSFER_DETAIL.Transfer_Rate*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Item_Cost , " & _
                                          " coalesce(TSPL_CSA_TRANSFER_DETAIL.transfer_Rate*TSPL_CSA_TRANSFER_DETAIL.Qty,0)*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Amount ,coalesce(TSPL_CSA_TRANSFER_DETAIL.Disc_Per,0) as Disc_Per ,coalesce(TSPL_CSA_TRANSFER_DETAIL.Disc_Amt,0)*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Disc_Amt ,0 as [Scheme Amount] " & _
                                          ", (coalesce(TSPL_CSA_TRANSFER_DETAIL.transfer_Rate*TSPL_CSA_TRANSFER_DETAIL.Qty,0)-coalesce(TSPL_CSA_TRANSFER_DETAIL.Disc_Amt,0))*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Amt_Less_Discount ,0 as Total_Tax_Amt ,cast((TSPL_CSA_TRANSFER_DETAIL.Transfer_Rate * TSPL_CSA_TRANSFER_DETAIL.Qty) as Numeric(18,2))*(case when coalesce(TSPL_CSA_TRANSFER_Head.convrate,0)<=0  then 1 else coalesce(TSPL_CSA_TRANSFER_Head.convrate,0) end) as Total_Amt, " & _
                                          " TSPL_CSA_TRANSFER_HEAD.Vehicle_Id as Vehicle_Code,TSPL_VEHICLE_MASTER.Number as Vehicle_No,0 as Additional_Charge, " & _
                                          " '' as [AR Document No],0 as [AR Document Amt],0 as [AR Document Discount Amt], 0 as [AR Amount Before Tax],0 as [AR Total Tax],0 as [AR Total Add Charge],TSPL_CSA_TRANSFER_HEAD.GR_No as [GR No],TSPL_CSA_TRANSFER_HEAD.gr_date as [GR Date],TSPL_CSA_TRANSFER_HEAD.WayBill_No as [WayBill No],TSPL_CSA_TRANSFER_HEAD.Transport_Id as [Transporter Code],case when len(TSPL_CSA_TRANSFER_HEAD.transporter_name_manual) > 0 then TSPL_CSA_TRANSFER_HEAD.transporter_name_manual else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],TSPL_CSA_TRANSFER_DETAIL.MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],TSPL_CSA_TRANSFER_HEAD.DOC_CODE as Ref_Doc_no,TSPL_CSA_TRANSFER_HEAD.Transfer_Date as Ref_doc_date,TSPL_CSA_TRANSFER_HEAD.Created_By ,TSPL_CSA_TRANSFER_HEAD.Modify_By,TSPL_CSA_TRANSFER_HEAD.Modify_date,NULL as RATE_UOM,0 as Conv_Factor, "

        strSDEndQry = " from TSPL_CSA_TRANSFER_DETAIL " & _
                    " left outer join TSPL_CSA_TRANSFER_HEAD on TSPL_CSA_TRANSFER_HEAD.DOC_CODE =TSPL_CSA_TRANSFER_DETAIL.DOC_CODE " & _
                    " left join TSPL_VEHICLE_MASTER on TSPL_CSA_TRANSFER_HEAD.Vehicle_Id=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                    " left join TSPL_TRANSPORT_MASTER on TSPL_CSA_TRANSFER_HEAD.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id " & _
                    " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_CSA_TRANSFER_HEAD.Cust_Code " & _
                    " where 'CSA Sale' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                    " and convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_CSA_TRANSFER_HEAD.Transfer_Date,103) <= convert(date,('" & To_Date & "'),103) "
        '' filter conditions
        If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_CSA_TRANSFER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
        End If
        If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_CSA_TRANSFER_HEAD.From_Location_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
        End If

        If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_CSA_TRANSFER_HEAD.Cust_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
        End If
        If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDEndQry += " and TSPL_CSA_TRANSFER_HEAD.Doc_Code = '" & obj.Document_Code & "' "
        End If

        strMCCMaterial += " union all "
        strMCCMaterial += " select Trans_Type  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount],   " & _
                          " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] , [Cash Scheme Code] , [Cash Scheme Amount], [Price Code],final.Ref_Doc_no,final.Ref_doc_date,final.Created_By ,final.Modify_By,final.Modify_date,final. RATE_UOM,final. Conv_Factor,0 as Sampling,'N' as Scheme_Item "
        strMCCMaterial += " from ("
        'strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX1 ,0 as TAX1_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX1+'%' as Tax1Rate"
        strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
        '' query for no tax applied
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & "  and (coalesce(TSPL_CSA_TRANSFER_DETAIL.tax1,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax3,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_CSA_TRANSFER_DETAIL.tax5,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_CSA_TRANSFER_DETAIL.tax7,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_CSA_TRANSFER_DETAIL.tax9,'')='' and coalesce(TSPL_CSA_TRANSFER_DETAIL.tax10,'')='') )t  "
        '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        '' query for tax1 applied
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX1 ,0 as TAX1_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX1_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX1+'%' as Tax1Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & "  and TSPL_CSA_TRANSFER_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for Tax1Rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX2 ,0 as TAX2_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX2_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX2+'%' as Tax2Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & " and TSPL_CSA_TRANSFER_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX3 ,0 as TAX3_Amt, TSPL_CSA_TRANSFER_DETAIL.TAX3_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX3+'%' as Tax3Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & " and TSPL_CSA_TRANSFER_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX4 ,0 as TAX4_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX4_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX4+'%' as Tax4Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & " and TSPL_CSA_TRANSFER_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX5 ,0 as TAX5_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX5_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX5+'%' as Tax5Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & " and TSPL_CSA_TRANSFER_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX6 ,0 as TAX6_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX6_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX6+'%' as Tax6Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & " and TSPL_CSA_TRANSFER_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX7 ,0 as TAX7_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX7_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX7+'%' as Tax7Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & " and TSPL_CSA_TRANSFER_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX8 ,0 as TAX8_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX8_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX8+'%' as Tax8Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & " and TSPL_CSA_TRANSFER_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX9 ,0 as TAX9_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX9_Rate, TSPL_CSA_TRANSFER_DETAIL.TAX9+'%' as Tax9Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & " and TSPL_CSA_TRANSFER_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_CSA_TRANSFER_DETAIL.TAX10 ,0 as TAX10_Amt,TSPL_CSA_TRANSFER_DETAIL.TAX10_Rate,TSPL_CSA_TRANSFER_DETAIL.TAX10+'%' as Tax10Rate"
        strMCCMaterial += " select * from (" & stCSACommonQuery & strTaxColumns & strSDEndQry & " and TSPL_CSA_TRANSFER_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " )final"
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
        strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
        'strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
        'added by stuti on 01/05/2017
        strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

        strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount], [Price Code],final.Ref_Doc_no,final.Ref_doc_date,final.Created_By ,final.Modify_By,final.Modify_date,final. RATE_UOM,final.Conv_Factor" '', " + strPivotFoGrouprOuterQuery + "

        '' end of csa transfer


        strMCCMaterial += " union all"
        strMCCMaterial += " select case when InvoiceAgainst='Against Dispatch Trade' then 'Bulk Sale Trade'  else 'Bulk Sale' end as Trans_Type ,coalesce(TSPL_LOCATION_MASTER.Main_Location_Code,TSPL_LOCATION_MASTER.Location_Code) as Bill_To_Location,TSPL_INVOICE_Master_BULKSALE.Posted,coalesce(Main_Loc.Location_Desc,TSPL_LOCATION_MASTER.location_desc) as Location_Desc ,'Invoice' as Invoice_type ,TSPL_INVOICE_Master_BULKSALE.Document_No as Document_code ,convert(varchar,TSPL_INVOICE_Master_BULKSALE.Document_Date,103) Document_Date,case when isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')='' then TSPL_INVOICE_DETAIL_BULKSALE.TradeTanker_No else isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')  end as Vehicle_Code,case when isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')='' then TSPL_INVOICE_DETAIL_BULKSALE.TradeTanker_No else isnull(TSPL_INVOICE_DETAIL_BULKSALE.Tanker_Code ,'')  end as Vehicle_No,(case when ROW_NUMBER() over (partition by TSPL_INVOICE_Master_BULKSALE.Document_No order by TSPL_INVOICE_DETAIL_BULKSALE.Item_Code )=1 then coalesce(TSPL_INVOICE_Master_BULKSALE.RoundOffAmount,0) else 0 end) as Additional_Charge, " & _
                          " TSPL_INVOICE_Master_BULKSALE.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Parent_Customer_No,"
        strMCCMaterial += " Parent_Master.Cust_Code as Parent_Customer_Code,Parent_Master.Customer_Name as Parent_Cust_Name, "
        strMCCMaterial += " TSPL_INVOICE_DETAIL_BULKSALE.Item_Code,tspl_item_master.Item_Desc ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceQty as Qty ,TSPL_INVOICE_DETAIL_BULKSALE.Unit_code,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceRate as Item_cost,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatPer ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFPer ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceFatKG ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceSNFKG  ,TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amount,0 as Disc_per,0 as Disc_Amt,0 as [Scheme Amount],TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount as Amt_less_Discount,0 as Total_tax_amt " + strPivotForOuterQueryforBulk + " " + strDoublePivotForOuterQueryforBulk + ",(TSPL_INVOICE_DETAIL_BULKSALE.InvoiceAmount) as Total_Amt, " & _
                          " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                          " TSPL_Customer_Invoice_Head.Discount_Amount as [AR Document Discount Amt], " & _
                          " (TSPL_Customer_Invoice_Head.Document_Total-TSPL_Customer_Invoice_Head.total_tax-TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                          " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge],'' as [GR No],NULL as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],'' as  [Delivery No]  ,'' as  [Shipment No],'' as [Booking No],0 AS MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],TSPL_Dispatch_BulkSale.Document_No as Ref_Doc_no ,TSPL_Dispatch_BulkSale.Document_Date as Ref_doc_date,TSPL_BulkSalePrice_MASTER.Created_By ,TSPL_BulkSalePrice_MASTER.Modified_By ,TSPL_BulkSalePrice_MASTER.Modified_Date ,NULL as RATE_UOM,0 as Conv_Factor ,0 as Sampling,'N' as Scheme_Item " & _
                          " from TSPL_INVOICE_DETAIL_BULKSALE "

        strMCCMaterial += " left outer join TSPL_INVOICE_Master_BULKSALE on TSPL_INVOICE_Master_BULKSALE.Document_No =TSPL_INVOICE_DETAIL_BULKSALE.Document_No "
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_INVOICE_Master_BULKSALE.Location_Code"
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as Main_Loc on TSPL_LOCATION_MASTER.Main_Location_Code =Main_Loc.Location_Code"
        strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =TSPL_INVOICE_Master_BULKSALE.Customer_Code"
        strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
        strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =TSPL_INVOICE_DETAIL_BULKSALE.Item_Code"
        strMCCMaterial += " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_No=TSPL_INVOICE_Master_BULKSALE.Document_No "
        strMCCMaterial += " left join TSPL_Dispatch_BulkSale on TSPL_Dispatch_BulkSale.Document_No =TSPL_INVOICE_DETAIL_BULKSALE.Dispatch_Code "
        strMCCMaterial += " left join TSPL_BulkSalePrice_MASTER on TSPL_BulkSalePrice_MASTER.Price_Code =TSPL_Dispatch_BulkSale.Price_Code  where 2=2 " & _
                            " and (case when InvoiceAgainst='Against Dispatch Trade' then 'Bulk Sale Trade'  else 'Bulk Sale' end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                            " and convert(date,TSPL_INVOICE_Master_BULKSALE.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_INVOICE_Master_BULKSALE.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "
        strMCCMaterial += " union all"
        strMCCMaterial += " select case when Trans_Type ='FS' then 'Fresh Sale' when Trans_Type ='CSA' then 'CSA Sale' when Trans_Type='PS' then 'Product Sale' when Trans_Type='MCC' then 'MCC Sale' when Trans_Type='Exp' then 'Export Sale'when Trans_Type='Bulk Sale' then 'Bulk Sale' when Trans_Type ='SS' then 'Misc Sale' WHEN Trans_Type ='SD' then 'General Sale' else Trans_Type  end  as [Trans Type],final.Loc_Code  as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.shipment_No  as [Document No],final.Document_Date as [Document_date],'' as Vehicle_Code,'' as Vehicle_No,0 as Additional_Charge,final.cust_Code  as [Customer Code],MAX(final.CustAdd ) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.shipped_Qty  as [Quantity],final.Unit_code as [UOM],final.price  as [Item Cost], 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.ItemAmt ,final.DiscountPer  as [Discount Per],final.TotalDiscountAmt   as [Discount Amount],final.[Scheme Amount]   as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.TotalTaxAmt  as [Total Tax Amount],final.Doc_Amt  as [Total Amount], " & _
            " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No],MRP , [Scheme Code] ,[Cash Scheme Code] ,  [Cash Scheme Amount], [Price Code],final.Ref_Doc_no,final.Ref_doc_date,final.Created_By ,final.Modify_By,final.Modify_date,final. RATE_UOM,final.Conv_Factor ,0 as Sampling,'N' as Scheme_Item  from ("

        Dim strScarpCommonQry As String = ""
        strScarpCommonQry = " select 'SS' as Trans_Type,TSPL_SCRAPINVOICE_HEAD.ispost as Status ,TSPL_SCRAPINVOICE_HEAD.Loc_Code,TSPL_SCRAPINVOICE_HEAD.cust_Code " & _
                            " ,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd, " & _
                            " TSPL_SCRAPINVOICE_HEAD.Invoice_Type,TSPL_SCRAPINVOICE_HEAD.invoice_No as shipment_No ,convert(varchar,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103 ) as Document_Date , " & _
                            " TSPL_SCRAPINVOICE_DETAIL.Item_Code ,TSPL_SCRAPINVOICE_DETAIL.shipped_Qty ,TSPL_SCRAPINVOICE_DETAIL.Unit_code ," & _
                            " TSPL_SCRAPINVOICE_DETAIL.price ,0 as InvoiceFatPer ,0 as InvoiceSNFPer ,0 as InvoiceFatKG ,0 as InvoiceSNFKG , " & _
                            " TSPL_SCRAPINVOICE_DETAIL.ItemAmt ,TSPL_SCRAPINVOICE_DETAIL.DiscountPer,0 as [Scheme Amount] ,TSPL_SCRAPINVOICE_DETAIL.ItemNetAmt as Amt_less_Discount, " & _
                            " TSPL_SCRAPINVOICE_DETAIL.TotalDiscountAmt , " & _
                            " TSPL_SCRAPINVOICE_DETAIL.TotalTaxAmt ,TSPL_SCRAPINVOICE_DETAIL.TotalAmt+Case when TSPL_SCRAPINVOICE_DETAIL.line_No=1 then coalesce(TSPL_SCRAPINVOICE_HEAD.add_Amt,0) else 0 end as Doc_Amt,'' as Vehicle_Code,'' as Vehicle_No,Case when TSPL_SCRAPINVOICE_DETAIL.line_No=1 then coalesce(TSPL_SCRAPINVOICE_HEAD.add_Amt,0) else 0 end as Additional_Charge," & _
                            " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                            " TSPL_Customer_Invoice_Head.Discount_Amount as [AR Document Discount Amt], " & _
                            " TSPL_Customer_Invoice_Head.amount_less_Discount as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                            " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge], " & _
                            " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                            " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,'' as [GR No],NULL as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],0 AS MRP,  '' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],'' as Ref_Doc_no,NULL as Ref_doc_date, '' as Created_By ,'' as Modify_By,NULL as Modify_date ,NULL as RATE_UOM,0 as Conv_Factor,"

        strSDEndQry = " from TSPL_SCRAPINVOICE_DETAIL  " & _
                      " left outer join TSPL_SCRAPINVOICE_HEAD on TSPL_SCRAPINVOICE_HEAD.invoice_No =TSPL_SCRAPINVOICE_DETAIL.invoice_No " & _
                      " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.AgainstScrap=TSPL_SCRAPINVOICE_HEAD.invoice_No " & _
                      " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SCRAPINVOICE_HEAD.Cust_Code  where 2=2 " & _
                      " AND 'Misc Sale' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                      " and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SCRAPINVOICE_HEAD.shipment_Date,103) <= convert(date,('" & To_Date & "'),103) "

        '' filter conditions
        If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_SCRAPINVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
        End If
        If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_SCRAPINVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
        End If

        If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_SCRAPINVOICE_HEAD.cust_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
        End If
        If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDEndQry += " and TSPL_SCRAPINVOICE_HEAD.invoice_No = '" & obj.Document_Code & "' "
        End If
        strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
        '' query for no tax applied

        strMCCMaterial += " select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & " and (coalesce(TSPL_SCRAPINVOICE_DETAIL.tax1,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax3,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_SCRAPINVOICE_DETAIL.tax5,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_SCRAPINVOICE_DETAIL.tax7,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_SCRAPINVOICE_DETAIL.tax9,'')='' and coalesce(TSPL_SCRAPINVOICE_DETAIL.tax10,'')='') )t "
        '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " union all "
        '' query for tax1 applied
        strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX1 ,TSPL_SCRAPINVOICE_DETAIL.TAX1_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX1_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX1+'%' as tax1rate  "
        strMCCMaterial += " select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax1<>'')"
        strMCCMaterial += " s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"

        strMCCMaterial += " union all "
        strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX2 ,TSPL_SCRAPINVOICE_DETAIL.TAX2_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX2_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX2+'%' as tax2rate  "
        strMCCMaterial += " select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax2<>'' )"
        strMCCMaterial += " s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " union all "
        strTaxColumns = "TSPL_SCRAPINVOICE_DETAIL.TAX3 ,TSPL_SCRAPINVOICE_DETAIL.TAX3_Amt , TSPL_SCRAPINVOICE_DETAIL.TAX3_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX3+'%' as tax3rate  "
        strMCCMaterial += "  select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax3<>'' )"
        strMCCMaterial += " s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " union all "
        strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX4 ,TSPL_SCRAPINVOICE_DETAIL.TAX4_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX4_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX4+'%' as tax4rate  "
        strMCCMaterial += " select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax4<>'' )"
        strMCCMaterial += " s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " union all "
        strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX5 ,TSPL_SCRAPINVOICE_DETAIL.TAX5_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX5_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX5+'%' as tax5rate  "
        strMCCMaterial += " select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax5<>'' )"
        strMCCMaterial += " s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " union all "
        strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX6 ,TSPL_SCRAPINVOICE_DETAIL.TAX6_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX6_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX6+'%' as tax6rate  "
        strMCCMaterial += " select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & "  and  TSPL_SCRAPINVOICE_DETAIL.tax6<>'' )"
        strMCCMaterial += " s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " union all "
        strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX7 ,TSPL_SCRAPINVOICE_DETAIL.TAX7_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX7_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX7+'%' as tax7rate  "
        strMCCMaterial += " select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax7<>'' )"
        strMCCMaterial += " s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + ")) t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " union all "
        strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX8 ,TSPL_SCRAPINVOICE_DETAIL.TAX8_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX8_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX8+'%' as tax8rate  "
        strMCCMaterial += " select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax8<>'' )"
        strMCCMaterial += "  s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"

        strMCCMaterial += " union all "
        strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX9 ,TSPL_SCRAPINVOICE_DETAIL.TAX9_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX9_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX9+'%' as tax9rate  "
        strMCCMaterial += "  select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax9<>'')"
        strMCCMaterial += " s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " union all "
        strTaxColumns = " TSPL_SCRAPINVOICE_DETAIL.TAX10 ,TSPL_SCRAPINVOICE_DETAIL.TAX10_Amt ,TSPL_SCRAPINVOICE_DETAIL.TAX10_Rate, TSPL_SCRAPINVOICE_DETAIL.TAX10+'%' as tax10rate  "
        strMCCMaterial += " select * from (" & strScarpCommonQry & strTaxColumns & strSDEndQry & " and  TSPL_SCRAPINVOICE_DETAIL.tax10<>'')"
        strMCCMaterial += " s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"

        strMCCMaterial += " ) final left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Loc_Code  left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code  left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.cust_Code   LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
        'strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
        'added by stuti on 01/05/2017
        strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

        strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.shipment_No  ,final.Item_Code ,final.Loc_Code  ,final.cust_Code  ,final.shipped_Qty  ,final.TotalTaxAmt  ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.price  ,final.ItemAmt  ,final.DiscountPer,final.TotalDiscountAmt  ,final.Amt_less_Discount   ,final.Amt_Less_Discount ,final.Doc_Amt,final.[Scheme Amount],Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name] , [Delivery No]  , [Shipment No], [Booking No] ,MRP ,  [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount], [Price Code],final.Ref_Doc_no,final.Ref_doc_date,final.Created_By ,final.Modify_By,final.Modify_date,final.RATE_UOM,final. Conv_Factor " ' ," + strPivotFoGrouprOuterQuery + " 
        '' query for return

        Dim strSDRCommonQuery As String = ""
        'strSDRCommonQuery = " select (CASE WHEN TSPL_SD_SALE_RETURN_HEAD.Trans_Type='ALL' THEN 'SDR' ELSE TSPL_SD_SALE_RETURN_HEAD.Trans_Type+'R' END) as Trans_Type,TSPL_SD_SALE_RETURN_HEAD.Status ,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location, " & _
        '                    " TSPL_SD_SALE_RETURN_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,COALESCE(TSPL_SD_SALE_RETURN_HEAD.Document_Type,TSPL_SD_SALE_RETURN_HEAD.Invoice_Type) AS Invoice_Type,TSPL_SD_SALE_RETURN_HEAD.Document_Code , " & _
        '                    " convert(varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103 ) as Document_Date , TSPL_SD_SALE_RETURN_DETAIL.Item_Code,TSPL_SD_SALE_RETURN_DETAIL.Line_No , " & _
        '                    " -TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code ,TSPL_SD_SALE_RETURN_DETAIL.Item_Cost , " & _
        '                    " -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Amount,0) as Amount ,TSPL_SD_SALE_RETURN_DETAIL.Disc_Per ,case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0)=0 then -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0)  + case when coalesce(FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then 1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) else 0 end else -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0) end as Disc_Amt,case when coalesce(FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then -1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) end  as [Scheme Amount] , " & _
        '                    " -(Amount-Total_Disc_Amt+case when coalesce(FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then Item_Net_Amt*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) else 0 end)  as Amt_Less_Discount , " & _
        '                    " -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0) as Total_Tax_Amt ,-(Amount+coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0)-coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0))  as Total_Amt,TSPL_SD_SALE_RETURN_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Number as Vehicle_No,-(case when TSPL_SD_SALE_RETURN_DETAIL.Line_No=1 then (coalesce(TSPL_SD_SALE_RETURN_HEAD.Total_Add_Charge,0)+coalesce(TSPL_SD_SALE_RETURN_HEAD.RoundOffAmount,0)) else 0 end) as Additional_Charge, " & _
        '                    " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],-TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
        '                    " -TSPL_Customer_Invoice_Head.Discount_Amount-coalesce(TSPL_SD_SALE_RETURN_HEAD.headDisc_AMt,0) as [AR Document Discount Amt], " & _
        '                    " -TSPL_Customer_Invoice_Head.amount_less_Discount as [AR Amount Before Tax],-TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
        '                    " -(TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge], " & _
        '                    " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
        '                    " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,TSPL_SD_SALE_RETURN_HEAD.GRNo as [GR No],'' as [WayBill No],'' as [Transporter Name],TSPL_SD_SALE_RETURN_DETAIL.Delivery_Code as [Delivery No],Against_Shipment_No as [Shipment No],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No as [Booking No],TSPL_SD_SALE_RETURN_DETAIL.MRP, "

        strSDRCommonQuery = " select (CASE WHEN TSPL_SD_SALE_RETURN_HEAD.Trans_Type='ALL' THEN 'SDR' ELSE TSPL_SD_SALE_RETURN_HEAD.Trans_Type+'R' END) as Trans_Type,TSPL_SD_SALE_RETURN_HEAD.Status ,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location, " & _
                          " TSPL_SD_SALE_RETURN_HEAD.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd,COALESCE(TSPL_SD_SALE_RETURN_HEAD.Document_Type,TSPL_SD_SALE_RETURN_HEAD.Invoice_Type) AS Invoice_Type,TSPL_SD_SALE_RETURN_HEAD.Document_Code , " & _
                          " convert(varchar,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103 ) as Document_Date , TSPL_SD_SALE_RETURN_DETAIL.Item_Code,TSPL_SD_SALE_RETURN_DETAIL.Line_No , " & _
                          " -TSPL_SD_SALE_RETURN_DETAIL.Qty as Qty ,TSPL_SD_SALE_RETURN_DETAIL.Unit_code ,TSPL_SD_SALE_RETURN_DETAIL.Item_Cost , " & _
                          " -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Amount,0) as Amount ,TSPL_SD_SALE_RETURN_DETAIL.Disc_Per ,case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0)=0 then -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0)  + case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then 1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) else 0 end else -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0) end as Disc_Amt,case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then -1*coalesce(Item_Net_Amt,0)*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) end  as [Scheme Amount] , " & _
                          " -(Amount- case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' then Total_Disc_Amt else Total_Disc_Amt end  + case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type<>'FS' then case when coalesce(TSPL_SD_SALE_RETURN_DETAIL.FOC_Item,0)=1 or coalesce(TSPL_SD_SALE_RETURN_DETAIL.sampling,0)=1  then Item_Net_Amt*(case when coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0)<=0  then 1 else coalesce(TSPL_SD_SALE_RETURN_Head.convrate,0) end) else 0 end else 0 end)  as Amt_Less_Discount , " & _
                          " -coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0) as Total_Tax_Amt ,-(Amount+coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Tax_Amt,0)- case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' then 0 else coalesce(TSPL_SD_SALE_RETURN_DETAIL.Total_Disc_Amt,0) end )  as Total_Amt,TSPL_SD_SALE_RETURN_HEAD.Vehicle_Code,TSPL_VEHICLE_MASTER.Number as Vehicle_No,-(case when TSPL_SD_SALE_RETURN_DETAIL.Line_No=1 then (coalesce(TSPL_SD_SALE_RETURN_HEAD.Total_Add_Charge,0)+coalesce(TSPL_SD_SALE_RETURN_HEAD.RoundOffAmount,0)) else 0 end) as Additional_Charge, " & _
                          " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],-TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                          " -TSPL_Customer_Invoice_Head.Discount_Amount-coalesce(TSPL_SD_SALE_RETURN_HEAD.headDisc_AMt,0) as [AR Document Discount Amt], " & _
                          " -TSPL_Customer_Invoice_Head.amount_less_Discount as [AR Amount Before Tax],-TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                          " -(TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge], " & _
                          " TSPL_Customer_Invoice_Head.Against_Sale_No,TSPL_Customer_Invoice_Head.Against_Sale_Return_No,TSPL_Customer_Invoice_Head.AgainstScrap, " & _
                          " TSPL_Customer_Invoice_Head.Against_VCGL,TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return,TSPL_SD_SALE_RETURN_HEAD.GRNo as [GR No],TSPL_SD_SHIPMENT_HEAD.gr_date as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],TSPL_SD_SALE_RETURN_DETAIL.Delivery_Code as [Delivery No],Against_Shipment_No as [Shipment No],TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No as [Booking No],TSPL_SD_SALE_RETURN_DETAIL.MRP, TSPL_SD_SALE_RETURN_DETAIL.Scheme_Code ,TSPL_SD_SALE_RETURN_DETAIL.Cash_Scheme_Code ,TSPL_SD_SALE_RETURN_DETAIL.Cash_Scheme_Amount*(-1) as Cash_Scheme_Amount ,TSPL_SD_SALE_RETURN_DETAIL.Price_code ,'' as Ref_Doc_no,NULL as Ref_doc_date,'' as Created_By ,'' as Modify_By,NULL as Modify_date,NULL as RATE_UOM,0 as Conv_Factor, TSPL_SD_SALE_RETURN_DETAIL.Sampling,TSPL_SD_SALE_RETURN_DETAIL.Scheme_Item,"
        strSDEndQry = " from TSPL_SD_SALE_RETURN_DETAIL " & _
                            " left outer join TSPL_SD_SALE_RETURN_HEAD on TSPL_SD_SALE_RETURN_HEAD.Document_Code =TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE " & _
                            " left join TSPL_VEHICLE_MASTER on TSPL_SD_SALE_RETURN_HEAD.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id " & _
                            " left join TSPL_Customer_Invoice_Head on TSPL_SD_SALE_RETURN_HEAD.Document_Code=  case when len(isnull(TSPL_Customer_Invoice_Head.Against_Sale_Return_No,''))>0  then TSPL_Customer_Invoice_Head.Against_Sale_Return_No else TSPL_Customer_Invoice_Head.Against_MCC_Material_Sale_Return end " & _
                            " left join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code = TSPL_SD_SALE_RETURN_DETAIL.Invoice_Code " & _
                            " left outer join TSPL_SD_SHIPMENT_HEAD on TSPL_SD_SHIPMENT_HEAD.document_code=TSPL_SD_SALE_INVOICE_HEAD.Against_Shipment_No " & _
                            " left join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE on TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Document_No = TSPL_SD_SALE_RETURN_DETAIL.Delivery_Code " & _
                            " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER ON TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_RETURN_HEAD.Customer_Code WHERE 2=2 " & _
                            " AND (case when TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='FS' then 'Fresh Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='CSA' then 'CSA Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='PS' then 'Product Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' then 'MCC Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='EXP' then 'Export Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type='Bulk Sale' then 'Bulk Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type ='SS' then 'Misc Sale Return' when TSPL_SD_SALE_RETURN_HEAD.Trans_Type in ('SD','All') then 'General Sale Return' else TSPL_SD_SALE_RETURN_HEAD.trans_Type  end) in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                            " and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SD_SALE_RETURN_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

        '' filter conditions
        If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_SD_SALE_RETURN_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
        End If
        If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
        End If

        If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_SD_SALE_RETURN_HEAD.Customer_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
        End If
        If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDEndQry += " and TSPL_SD_SALE_RETURN_HEAD.Document_Code = '" & obj.Document_Code & "' "
        End If

        strMCCMaterial += " union all "

        strMCCMaterial += " select case when Trans_Type ='FSR' then 'Fresh Sale Return' when Trans_Type ='CSAR' then 'CSA Sale Return' when Trans_Type='PSR' then 'Product Sale Return' when Trans_Type='MCCR' then 'MCC Sale Return' when Trans_Type='EXPR' then 'Export Sale Return'when Trans_Type='Bulk Sale' then 'Bulk Sale Return' when Trans_Type ='SSR' then 'Misc Sale' when Trans_Type ='SDR' then 'General Sale Return' else trans_Type  end  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_Code as [Document No],final.Document_Date as [Document_date],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.Customer_Code as [Customer Code],MAX(final.CustAdd) As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Customer_Name) as [Customer Name] ,max(TSPL_CUSTOMER_MASTER .Parent_Customer_No) as [Parent Customer No] ,max(Parent_Master.Cust_Code) as [Parent Customer Code],max(Parent_Master.Customer_Name) as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount], " & _
            " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]  - case when (Trans_Type ='FSR' or Trans_Type ='PSR') and [AR Document Amt]>0 then coalesce(final.[Scheme Amount],0) else 0 end ) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, Scheme_Code ,Cash_Scheme_Code , Cash_Scheme_Amount ,final.Price_code,final.Ref_Doc_no,final.Ref_doc_date ,final.Created_By ,final.Modify_By,final.Modify_date,final.RATE_UOM ,final.Conv_Factor ,final. Sampling,final.Scheme_Item from ( "

        strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
        '' query for no tax applied
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and (coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax1,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax3,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax5,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax7,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax9,'')='' and coalesce(TSPL_SD_SALE_RETURN_DETAIL.tax10,'')='') )t "

        strMCCMaterial += "   union all"
        '' query for tax1 applied
        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX1 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX1_Amt as TAX1_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX1_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX1+'%' as tax1rate  "
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"

        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX2 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX2_Amt as TAX2_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX2_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX2+'%' as tax2rate  "
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX3 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX3_Amt as TAX3_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX3_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX3+'%' as tax3rate  "
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX4 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX4_Amt as TAX4_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX4_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX4+'%' as tax4rate  "
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX5 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX5_Amt as TAX5_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX5_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX5+'%' as tax5rate  "

        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX6 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX6_Amt as TAX6_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX6_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX6+'%' as tax6rate  "
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX7 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX7_Amt as TAX7_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX7_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX7+'%' as tax7rate  "
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX8 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX8_Amt as TAX8_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX8_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX8+'%' as tax8rate  "
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX9 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX9_Amt as TAX9_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX9_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX9+'%' as tax9rate  "
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_SD_SALE_RETURN_DETAIL.TAX10 ,-TSPL_SD_SALE_RETURN_DETAIL.TAX10_Amt as TAX10_Amt,TSPL_SD_SALE_RETURN_DETAIL.TAX10_Rate ,TSPL_SD_SALE_RETURN_DETAIL.TAX10+'%' as tax10rate  "
        strMCCMaterial += " select * from (" & strSDRCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_SD_SALE_RETURN_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " )final"
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =final.Customer_Code "
        strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
        'strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
        'added by stuti on 01/05/2017
        strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

        strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_Code ,final.Item_Code,final.Line_No ,final.Bill_To_Location ,final.Customer_Code ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP , Scheme_Code ,Cash_Scheme_Code , Cash_Scheme_Amount ,final.Price_code ,final.Ref_Doc_no,final.Ref_doc_date,final.Created_By ,final.Modify_By,final.Modify_date ,final.RATE_UOM,final.Conv_Factor ,final. Sampling,final.Scheme_Item " '', " + strPivotFoGrouprOuterQuery + " 

        strMCCMaterial += " union all "

        '''' bulk sale return 
        strMCCMaterial += "  select 'Bulk Sale Return' as Trans_Type ,TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code as Bill_To_Location,TSPL_SALE_RETURN_MASTER_BULKSALE.Posted,TSPL_LOCATION_MASTER.Location_Desc ,'Invoice' as Invoice_type ,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No as Document_code ,convert(varchar,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) Document_Date,'' as Vehicle_Code,'' as Vehicle_No,coalesce(-1 * TSPL_SALE_RETURN_MASTER_BULKSALE.roundoffamount,0) as Additional_Charge, " & _
                           " TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code,TSPL_CUSTOMER_MASTER.Add1 + ' ' + TSPL_CUSTOMER_MASTER.Add2 + ' ' + TSPL_CUSTOMER_MASTER.Add3 As CustAdd ,TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Parent_Customer_No," & _
                           " Parent_Master.Cust_Code as Parent_Customer_Code,Parent_Master.Customer_Name as Parent_Cust_Name ," & _
                           " TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code,tspl_item_master.Item_Desc ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceQty as Qty ,TSPL_SALE_RETURN_DETAIL_BULKSALE.Unit_code,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceRate as Item_cost,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatPer ,TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFPer ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceFatKG as InvoiceFatKG ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceSNFKG as InvoiceSNFKG  ,-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as Amount,0 as Disc_per,0 as Disc_Amt,0 as [Scheme Amount],-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as Amt_less_Discount,0 as Total_tax_amt " + strPivotForOuterQueryforBulk + " " + strDoublePivotForOuterQueryforBulk + ",-TSPL_SALE_RETURN_DETAIL_BULKSALE.InvoiceAmount as Total_Amt, " & _
                           " TSPL_Customer_Invoice_Head.Document_No as [AR Document No],-1 * TSPL_Customer_Invoice_Head.Document_Total [AR Document Amt]," & _
                           " TSPL_Customer_Invoice_Head.Discount_Amount as [AR Document Discount Amt], " & _
                           " -1 * (TSPL_Customer_Invoice_Head.Document_Total-TSPL_Customer_Invoice_Head.total_tax-TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Amount Before Tax],TSPL_Customer_Invoice_Head.total_tax as [AR Total Tax], " & _
                           " (TSPL_Customer_Invoice_Head.total_Add_Charge+TSPL_Customer_Invoice_Head.RoundOffAmount) as [AR Total Add Charge],'' as [GR No],NULL as [GR Date],'' as [WayBill No],'' as [Transporter Code],'' as [Transporter Name],'' as [Delivery No]  ,'' as  [Shipment No],'' as  [Booking No],0 AS MRP, '' as [Scheme Code] ,'' as  [Cash Scheme Code] , 0 as [Cash Scheme Amount], '' as [Price Code],'' as Ref_Doc_no,NULL as Ref_doc_date ,'' as Created_By ,'' as Modified_By,NULL as Modify_date,NULL as RATE_UOM,0 as Conv_Factor,0 as Sampling,'N' as Scheme_Item" & _
                           " from TSPL_SALE_RETURN_DETAIL_BULKSALE "

        strMCCMaterial += " left outer join TSPL_SALE_RETURN_MASTER_BULKSALE on TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No =TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No "
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_SALE_RETURN_MASTER_BULKSALE.Location_Code"
        strMCCMaterial += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Cust_Code =TSPL_SALE_RETURN_MASTER_BULKSALE.Customer_Code"
        strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No"
        strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code"
        strMCCMaterial += " left join TSPL_Customer_Invoice_Head on TSPL_Customer_Invoice_Head.Against_Sale_Return_No=TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No  where 2=2 " & _
                            " and 'Bulk Sale Return' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                            " and convert(date,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

        Dim strTranferCommonQuery As String = ""
        strTranferCommonQuery = " select 'Transfer' as Trans_Type,TSPL_TRANSFER_ORDER_HEAD.Status ,TSPL_TRANSFER_ORDER_HEAD.From_Location as Bill_To_Location, " & _
                                " GIT_Main_Loc.Location_Code as To_Location,TSPL_TRANSFER_ORDER_HEAD.Transfer_Type as Invoice_Type,TSPL_TRANSFER_ORDER_HEAD.Document_No , " & _
                                " convert(varchar,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103 ) as Document_Date , TSPL_TRANSFER_ORDER_DETAIL.Item_Code , " & _
                                " (case when TSPL_TRANSFER_ORDER_DETAIL.In_Qty>0 then TSPL_TRANSFER_ORDER_DETAIL.In_Qty else TSPL_TRANSFER_ORDER_DETAIL.Out_Qty end) as Qty, " & _
                                " TSPL_TRANSFER_ORDER_DETAIL.Unit_code ,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost , " & _
                                " TSPL_TRANSFER_ORDER_DETAIL.Amount ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt ,0 as [Scheme Amount], " & _
                                " (TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Amt_Less_Discount ,0 as Total_Tax_Amt ,(TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Total_Amt,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code,COALESCE(TSPL_VEHICLE_MASTER.Number,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No) as Vehicle_No,0 as Additional_Charge, " & _
                                " '' as [AR Document No],0 as  [AR Document Amt],0 as [AR Document Discount Amt],0 as  [AR Amount Before Tax],0 as [AR Total Tax],0 as [AR Total Add Charge],TSPL_TRANSFER_ORDER_HEAD.GR_No as [GR No],TSPL_TRANSFER_ORDER_HEAD.gr_date as [GR Date],TSPL_TRANSFER_ORDER_HEAD.WayBill_No as [WayBill No],TSPL_TRANSFER_ORDER_HEAD.Transport_Id as [Transporter Code],case when len(TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual) > 0 then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual  else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],0 AS MRP, '' as [Scheme Code] ,'' as  [Cash Scheme Code] , 0 as [Cash Scheme Amount], '' as [Price Code],'' as Ref_Doc_no,NULL as Ref_doc_date,'' as Created_By,'' as Modified_By,NULL as Modify_date,NULL as RATE_UOM,0 as Conv_Factor, "

        strSDEndQry = " from TSPL_TRANSFER_ORDER_DETAIL left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
                      " left join TSPL_VEHICLE_MASTER on TSPL_TRANSFER_ORDER_Head.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
                      " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSFER_ORDER_HEAD.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id " & _
                      " left join ( select  max(Location_Code) as Location_Code,GIT_Location from TSPL_LOCATION_MASTER where GIT_Location is not null " & _
                      " group by GIT_Location ) GIT_Main_Loc on TSPL_TRANSFER_ORDER_HEAD.To_Location=GIT_Main_Loc.GIT_Location  where Transfer_Type <>'I' " & _
                      " AND 'Transfer' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                      " and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) "

        '' filter conditions
        If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
        End If
        If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_TRANSFER_ORDER_HEAD.From_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
        End If

        If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDEndQry += " and GIT_Main_Loc.Location_Code in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
        End If
        If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDEndQry += " and TSPL_TRANSFER_ORDER_HEAD.Document_No = '" & obj.Document_Code & "' "
        End If
        strMCCMaterial += " union all "
        strMCCMaterial += "   select *,0 as Sampling,'N' as Scheme_Item from (Select 'MCC Transfer' as Trans_Type ,TSPL_MCC_Dispatch_Challan.MCC_Code as  Bill_To_Location,TSPL_MCC_Dispatch_Challan.isPosted as " _
            & " Status, sendr.location_desc as  location_desc,'MCC Transfer' as Invoice_Type,TSPL_MCC_Dispatch_Challan.Chalan_NO as PI_NO , " _
            & " convert(varchar,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103 ) as PI_Date, '' as vehicledesc,tm.tanker_NO as Vehicle_No,0  as Additional_Charge , " _
            & " TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code as Customer_Code,'' AS [CustAdd],  recv.Location_Desc  as Customer_Name ,'' as [Parent Vendor No],'' as [Parent Vendor Code]," _
            & " '' as [Parent Vendor Name], TSPL_MCC_Dispatch_Challan.Item_Code, TSPL_MCC_Dispatch_Challan.Item_Desc , TSPL_MCC_Dispatch_Challan.Net_Qty  as Qty " _
            & " ,TSPL_MCC_Dispatch_Challan.UOM_Code as  Unit_code ,round(((TSPL_MCC_Dispatch_Challan.FAT_RATE *(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) " _
            & " * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100)) +(TSPL_MCC_Dispatch_Challan.SNF_RATE  *(coalesce(cast(t_SNF_Recd.Param_Field_Value as float),0) * " _
            & " coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100)))/coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,1) ,2) as  Item_Cost ,  t_FAT_Recd.Param_Field_Value as [FAT Per]" _
            & " ,t_SNF_Recd.Param_Field_Value as [SNF Per],(coalesce(cast(t_FAT_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100) " _
            & " as [FAT KG],(coalesce(cast(t_Snf_Recd.Param_Field_Value as float),0) * coalesce(TSPL_MCC_Dispatch_Challan.Net_Qty,0)/100) as [SNF KG],amount as  Amount ,0" _
            & " as Disc_Per  ,0 as Disc_Amt,0 as [Scheme Amount] ,  amount as  Amt_Less_Discount   " & strPivotForTransfer_In & "" & strPivotFortRANSFER_INPercentQuery & ",   0 as Total_Tax_Amt " _
            & " ,amount as   Total_Amt,TSPL_vendor_Invoice_Head.Document_No as [AR Document No],TSPL_vendor_Invoice_Head.Document_Total [AR Document Amt]" _
            & " ,TSPL_vendor_Invoice_Head.Discount_Amount as [AR Document Discount Amt],TSPL_vendor_Invoice_Head.amount_less_Discount as [AR Amount Before Tax]," _
            & " TSPL_vendor_Invoice_Head.total_tax as [AR Total Tax],TSPL_vendor_Invoice_Head.total_Add_Charge as [AR Total Add Charge],'' as [GRNO],null as [GRN Date],'' as Way_BillNo," _
            & "  TSPL_MCC_Dispatch_Challan.tanker_No,tm.description,'' as  [delivery No],'' as [Shiping No],'' as [Booking No],0 AS MRP,'' as  [Scheme Code] ,'' as [Cash Scheme Code] , 0 as [Cash Scheme Amount],'' as [Price Code],'' as Ref_Doc_no,NULL as Ref_doc_date,'' as Created_By ,'' as Modified_By,NULL as Modify_date,NULL as RATE_UOM,0 as Conv_Factor from TSPL_MCC_Dispatch_Challan  left outer  join TSPL_MILK_TRANSFER_IN on TSPL_MILK_TRANSFER_IN.Dispatch_Challan_No " _
            & " =TSPL_MCC_Dispatch_Challan.Chalan_NO  LEFT JOIN tspl_location_Master  sendr ON sendr.Location_Code=TSPL_MCC_Dispatch_Challan.MCC_CODE " _
            & " left join tspl_Location_master on tspl_Location_master.location_code=TSPL_MCC_Dispatch_Challan.mcc_Code left join tspl_Location_master recv on " _
            & " recv.location_code=TSPL_MCC_Dispatch_Challan.Mcc_Or_Plant_Code  left join TSPL_vendor_Invoice_Head on  TSPL_vendor_Invoice_Head.vendor_Invoice_No" _
            & " =TSPL_MILK_TRANSFER_IN.Receipt_Challan_No left join tspl_tanker_Master tm on tm.tanker_no=TSPL_MCC_Dispatch_Challan.tanker_No Left Outer Join " _
            & " (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.* From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On " _
            & " TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'SNF') " _
            & " t_SNF_Recd On t_SNF_Recd.Chalan_NO   = TSPL_MCC_Dispatch_Challan.Chalan_NO   Left Outer Join (Select TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.*" _
            & " From TSPL_MCC_Dispatch_Challan Left Outer Join TSPL_Mcc_Dispatch_Chalan_Parameter_Detail On TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Chalan_No  = " _
            & " TSPL_MCC_Dispatch_Challan.Chalan_NO  where TSPL_Mcc_Dispatch_Chalan_Parameter_Detail.Param_Type = 'FAT' ) t_FAT_Recd On t_FAT_Recd.Chalan_No " _
            & " = TSPL_MCC_Dispatch_Challan.Chalan_NO where 2=2 " & _
              " AND 'MCC Transfer' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
              " and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_MCC_Dispatch_Challan.Dispatch_Date,103) <= convert(date,('" & To_Date & "'),103) )t "
        '' transaction unit conversion
        strMCCMaterial += " union all "
        strMCCMaterial += " select Trans_type  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER .Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_No as [Document No],final.Document_Date as [Document_date],Vehicle_Code,Vehicle_No,final.Additional_Charge,final.To_Location as [Customer Code],'' As [Customer Address] ,max(TSPL_CUSTOMER_MASTER .Location_Desc) as [Customer Name] ,'' as [Parent Customer No] ,'' as [Parent Customer Code],'' as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount]," & _
            " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],final.[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP,  [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount],  [Price Code],final.Ref_Doc_no,final.Ref_doc_date,final.Created_By ,final.Modified_By,final.Modify_date,final.RATE_UOM,final.Conv_Factor,0 as Sampling,'N' as Scheme_Item from ( "
        'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,0 as TAX1_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "

        '' query for no tax applied
        strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and (coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax1,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax3,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax5,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax7,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax9,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax10,'')='') )t "
        '" pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += " union all "
        '' quert for no tax applied
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,TSPL_TRANSFER_ORDER_DETAIL.TAX1_AMT,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX2 ,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX2+'%' as tax2rate  "
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX3 ,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX3+'%' as tax3rate  "
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX4 ,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX4+'%' as tax4rate  "
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX5 ,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX5+'%' as tax5rate  "

        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX6 ,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX6+'%' as tax6rate  "
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX7 ,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX7+'%' as tax7rate  "
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX8 ,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX8+'%' as tax8rate  "
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX10 ,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX10+'%' as tax10rate  "
        strMCCMaterial += " select * from (" & strTranferCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t"

        strMCCMaterial += " )final"
        ''-------------------------
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location "
        strMCCMaterial += " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code "
        strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Location_Code =final.To_location "
        'strMCCMaterial += " left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
        'strMCCMaterial += " LEFT OUTER JOIN TSPL_CUSTOMER_MASTER as Parent_Master ON Parent_Master.Cust_Code=TSPL_CUSTOMER_MASTER.Parent_Customer_No "
        'added by stuti on 01/05/2017
        strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

        strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_No ,final.Item_Code ,final.Bill_To_Location ,final.To_Location ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP,[Scheme Code] ,[Cash Scheme Code] ,[Cash Scheme Amount], [Price Code] ,final.Ref_Doc_no,final.Ref_doc_date,final.Created_By ,final.Modified_By,final.Modify_date,final. RATE_UOM,final.Conv_Factor"
        ''richa

        strMCCMaterial += Environment.NewLine + " --- QUERY FOR TRANSFER RETURN---------------------- ADDED BY RICHA AGARWAL ------------" + Environment.NewLine


        Dim strTranferReturnCommonQuery As String = ""
        strTranferReturnCommonQuery = " select 'Transfer Return' as Trans_Type,TSPL_TRANSFER_ORDER_HEAD.Status ,  TSPL_TRANSFER_ORDER_HEAD.From_Location as Bill_To_Location, " & _
                                " GIT_Main_Loc.Location_Code as To_Location,TSPL_TRANSFER_ORDER_HEAD.Transfer_Type as Invoice_Type,TSPL_TRANSFER_RETURN.Document_No , " & _
                                " convert(varchar,TSPL_TRANSFER_RETURN.Document_Date,103 ) as Document_Date , TSPL_TRANSFER_ORDER_DETAIL.Item_Code , " & _
                                " -(case when TSPL_TRANSFER_ORDER_DETAIL.In_Qty>0 then TSPL_TRANSFER_ORDER_DETAIL.In_Qty else TSPL_TRANSFER_ORDER_DETAIL.Out_Qty end) as Qty, " & _
                                " TSPL_TRANSFER_ORDER_DETAIL.Unit_code ,TSPL_TRANSFER_ORDER_DETAIL.Item_Cost , " & _
                                " -TSPL_TRANSFER_ORDER_DETAIL.Amount AS Amount ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Per ,TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt ,0 as [Scheme Amount], " & _
                                " -(TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Amt_Less_Discount ,0 as Total_Tax_Amt ,-(TSPL_TRANSFER_ORDER_DETAIL.Amount-TSPL_TRANSFER_ORDER_DETAIL.Disc_Amt) as Total_Amt,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Code,COALESCE(TSPL_VEHICLE_MASTER.Number,TSPL_TRANSFER_ORDER_HEAD.Vehicle_Mannual_No) as Vehicle_No,0 as Additional_Charge, " & _
                                " '' as [AR Document No],0 as  [AR Document Amt],0 as [AR Document Discount Amt],0 as  [AR Amount Before Tax],0 as [AR Total Tax],0 as [AR Total Add Charge],TSPL_TRANSFER_ORDER_HEAD.GR_No as [GR No],TSPL_TRANSFER_ORDER_HEAD.gr_date as [GR Date],TSPL_TRANSFER_ORDER_HEAD.WayBill_No as [WayBill No],TSPL_TRANSFER_ORDER_HEAD.Transport_Id as [Transporter Code],case when len(TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual) > 0 then TSPL_TRANSFER_ORDER_HEAD.Transporter_Name_Manual  else TSPL_TRANSPORT_MASTER.Transporter_Name end as [Transporter Name],'' as [Delivery No]  ,'' as [Shipment No],'' as [Booking No],0 AS MRP, '' as [Scheme Code] , '' as [Cash Scheme Code] ,0 as   [Cash Scheme Amount],  '' as [Price Code],'' as Ref_Doc_no,NULL as Ref_doc_date,'' as Created_By ,'' as Modified_By,NULL as Modify_date,NULL as RATE_UOM,0 as Conv_Factor,"

        strSDEndQry = " from TSPL_TRANSFER_ORDER_DETAIL left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No =TSPL_TRANSFER_ORDER_DETAIL.Document_No " & _
            " Left Outer Join TSPL_TRANSFER_RETURN on TSPL_TRANSFER_RETURN.Transfer_No=TSPL_TRANSFER_ORDER_HEAD.Document_No " & _
                      " left join TSPL_VEHICLE_MASTER on TSPL_TRANSFER_ORDER_Head.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id  " & _
                      " left join TSPL_TRANSPORT_MASTER on TSPL_TRANSFER_ORDER_HEAD.Transport_Id=TSPL_TRANSPORT_MASTER.Transport_Id " & _
                      " left join ( select  max(Location_Code) as Location_Code,GIT_Location from TSPL_LOCATION_MASTER where GIT_Location is not null " & _
                      " group by GIT_Location ) GIT_Main_Loc on TSPL_TRANSFER_ORDER_HEAD.To_Location=GIT_Main_Loc.GIT_Location  where TSPL_TRANSFER_ORDER_HEAD.Transfer_Type ='O'  " & _
                      " AND 'Transfer Return' in (" & clsCommon.GetMulcallString(obj.Trans_Type_List) & ") " & _
                      " and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <= convert(date,('" & To_Date & "'),103) AND ISNULL(TSPL_TRANSFER_RETURN.Document_No ,'')<>'' "

        '' filter conditions
        If obj.Item_Code_List IsNot Nothing AndAlso obj.Item_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_TRANSFER_ORDER_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(obj.Item_Code_List) + ") "
        End If
        If obj.Location_Code_List IsNot Nothing AndAlso obj.Location_Code_List.Count > 0 Then
            strSDEndQry += " and TSPL_TRANSFER_ORDER_HEAD.From_Location in (" + clsCommon.GetMulcallString(obj.Location_Code_List) + ") "
        End If

        If obj.Customer_Code_List IsNot Nothing AndAlso obj.Customer_Code_List.Count > 0 Then
            strSDEndQry += " and  GIT_Main_Loc.Location_Code  in (" + clsCommon.GetMulcallString(obj.Customer_Code_List) + ") "
        End If
        If clsCommon.myLen(obj.Document_Code) > 0 Then
            strSDEndQry += " and TSPL_TRANSFER_RETURN.Document_No = '" & obj.Document_Code & "' "
        End If

        strMCCMaterial += " union all "
        strMCCMaterial += " select Trans_type  as [Trans Type],final.Bill_To_Location as [Location Code],final.Status  ,max(TSPL_LOCATION_MASTER.Location_Desc) as [Location Name] ,(final.Invoice_Type) as [Invoice Type],final.Document_No as [Document No],final.Document_Date as [Document_date],Vehicle_Code,Vehicle_No,final.Additional_Charge, final.To_Location as [Customer Code],'' As [Customer Address] ,max(TSPL_CUSTOMER_MASTER.Location_Desc) as [Customer Name] ,'' as [Parent Customer No] ,'' as [Parent Customer Code],'' as [Parent Customer Name], final.Item_Code as [Item Code],max(tspl_item_master.Item_Desc) as [Item Name],final.Qty as [Quantity],final.Unit_code as [UOM],final.Item_Cost as [Item Cost], 0 as [Fat Per],0 as [SNF Per],0 as [Fat Kg],0 as [SNF KG],final.Amount,final.Disc_Per as [Discount Per],final.Disc_Amt as [Discount Amount],final.[Scheme Amount] as [Scheme Amount],final.Amt_Less_Discount  as [Amount Less Discount] " + strPivotForOuterQuery + ", " + strPivotFoGrouprOuterQuery + " ,final.Total_Tax_Amt as [Total Tax Amount],final.Total_Amt as [Total Amount]," & _
            " [AR Document No], [AR Document Amt],[AR Document Discount Amt],([AR Document Amt]-[AR Total Tax]-[AR Total Add Charge]) as  [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],[GR No],[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP, [Scheme Code] , [Cash Scheme Code] ,  [Cash Scheme Amount],  [Price Code],final.Ref_Doc_no,final. Ref_doc_date ,final.Created_By ,final.Modified_By,final.Modify_date,final. RATE_UOM,final. Conv_Factor,0 as Sampling,'N' as Scheme_Item from ( "
        'strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,0 as TAX1_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "

        '' query for no tax applied
        strTaxColumns = strPivotForInnerQueryNoTax & "," & strDoublePivotForInnerQueryNoTax
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and (coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax1,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax2,'')='' " & _
                          " and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax3,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax4,'')='' and " & _
                          " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax5,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax6,'')='' and " & _
                          " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax7,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax8,'')='' and " & _
                          " coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax9,'')='' and coalesce(TSPL_TRANSFER_ORDER_DETAIL.tax10,'')='') )t "

        strMCCMaterial += Environment.NewLine + " union all "
        '' quert for no tax applied
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX1 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX1_AMT AS TAX1_AMT ,TSPL_TRANSFER_ORDER_DETAIL.TAX1_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX1+'%' as tax1rate  "
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax1<>'' )s pivot (sum(tax1_amt) for tax1 in (" + strPivotForInnerQuery + "))t pivot (min(tax1_rate) for tax1rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX2 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX2_Amt AS TAX2_Amt ,TSPL_TRANSFER_ORDER_DETAIL.TAX2_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX2+'%' as tax2rate  "
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax2<>'' )s pivot (sum(tax2_amt) for tax2 in (" + strPivotForInnerQuery + "))t pivot (min(tax2_rate) for tax2rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX3 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX3_Amt AS TAX3_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX3_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX3+'%' as tax3rate  "
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax3<>'' )s pivot (sum(tax3_amt) for tax3 in (" + strPivotForInnerQuery + "))t pivot (min(tax3_rate) for tax3rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "   union all"
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX4 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX4_Amt AS TAX4_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX4_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX4+'%' as tax4rate  "
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax4<>'' )s pivot (sum(tax4_amt) for tax4 in (" + strPivotForInnerQuery + "))t pivot (min(tax4_rate) for tax4rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"
        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX5 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX5_Amt AS TAX5_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX5_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX5+'%' as tax5rate  "

        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax5<>'' )s pivot (sum(tax5_amt) for tax5 in (" + strPivotForInnerQuery + "))t pivot (min(tax5_rate) for tax5rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX6 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX6_Amt AS TAX6_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX6_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX6+'%' as tax6rate  "
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax6<>'')s pivot (sum(tax6_amt) for tax6 in (" + strPivotForInnerQuery + "))t pivot (min(tax6_rate) for tax6rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX7 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX7_Amt AS TAX7_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX7_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX7+'%' as tax7rate  "
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax7<>'' )s pivot (sum(tax7_amt) for tax7 in (" + strPivotForInnerQuery + "))t pivot (min(tax7_rate) for tax7rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX8 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX8_Amt AS TAX8_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX8_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX8+'%' as tax8rate  "
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax8<>'' )s pivot (sum(tax8_amt) for tax8 in (" + strPivotForInnerQuery + "))t pivot (min(tax8_rate) for tax8rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX9 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX9_Amt AS TAX9_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX9_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX9+'%' as tax9rate  "
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax9<>'')s pivot (sum(tax9_amt) for tax9 in (" + strPivotForInnerQuery + "))t pivot (min(tax9_rate) for tax9rate in (" + strDoublePivotForInnerQuery + "))t"
        strMCCMaterial += "  union all"

        strTaxColumns = " TSPL_TRANSFER_ORDER_DETAIL.TAX10 ,-TSPL_TRANSFER_ORDER_DETAIL.TAX10_Amt AS TAX10_Amt,TSPL_TRANSFER_ORDER_DETAIL.TAX10_Rate ,TSPL_TRANSFER_ORDER_DETAIL.TAX10+'%' as tax10rate  "
        strMCCMaterial += " select * from (" & strTranferReturnCommonQuery & strTaxColumns & strSDEndQry & " and TSPL_TRANSFER_ORDER_DETAIL.tax10<>'' )s pivot (sum(tax10_amt) for tax10 in (" + strPivotForInnerQuery + "))t pivot (min(tax10_rate) for tax10rate in (" + strDoublePivotForInnerQuery + "))t" & _
         " )final" & _
        " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =final.Bill_To_Location " & _
         " left outer join tspl_item_master on tspl_item_master.Item_Code =final.Item_Code " & _
         " left outer join TSPL_LOCATION_MASTER as TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Location_Code =final.To_location "
        '" left outer join " & "(" & qryQC & ") as QC" & " on QC.Item_Code =final.Item_Code "
        'added by stuti on 01/05/2017
        strMCCMaterial += " where convert(date,final.Document_Date,103) >= convert(date,('" & From_Date & "'),103) and convert(date,final.Document_Date,103) <= convert(date,('" & To_Date & "'),103)"

        strMCCMaterial += " group by  final.Trans_Type,final .Status  ,final.Document_No ,final.Item_Code ,final.Bill_To_Location ,final.To_Location ,final.Qty ,final.Total_Tax_Amt ,final.Invoice_Type ,final.Document_Date ,final.Unit_code ,final.Item_Cost ,final.Amount ,final.Disc_Per ,final.Disc_Amt,final.[Scheme Amount] ,final.Amt_Less_Discount ,final.Total_Amt,Vehicle_Code,Vehicle_No,final.Additional_Charge,[AR Document No], [AR Document Amt],[AR Document Discount Amt], [AR Amount Before Tax],[AR Total Tax],[AR Total Add Charge],[GR No],final.[GR Date],[WayBill No],final.[Transporter Code],[Transporter Name], [Delivery No]  , [Shipment No], [Booking No],MRP,  [Scheme Code] ,  [Cash Scheme Code] , [Cash Scheme Amount],  [Price Code] ,final.Ref_Doc_no,final. Ref_doc_date,final.Created_By ,final.Modified_By,final.Modify_date,final. RATE_UOM,final.Conv_Factor" + Environment.NewLine

        strMCCMaterial += Environment.NewLine + " --- QUERY FOR TRANSFER RETURN END---------------------- ADDED BY RICHA AGARWAL ------------" + Environment.NewLine


        strMCCMaterial += ") xx"
        '===============Added by preeti Gupta ===
        strMCCMaterial += " left join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code =xx.[Location Code] "
        strMCCMaterial += " left join TSPL_USER_MASTER as Created_By on Created_By.user_Code =xx.Created_By "
        strMCCMaterial += " left join TSPL_USER_MASTER as Modify_By on Modify_By.user_Code =xx.Modify_By "
        '=======================================
        ''richa agarwal change to show csa sales account for csa sale and csa sale return
        '' strMCCMaterial += " left outer join (select ITEM.Item_Code,ITEM.Item_Desc,ITEM.Structure_Code,SA.Sales_Account from TSPL_ITEM_MASTER Item left join TSPL_SALES_ACCOUNTS SA on Item.Sale_Class_Code=SA.Sales_Class_Code) Item on Item.Item_Code =xx.[Item Code] "
        strMCCMaterial += " left outer join (select ITEM.Item_Code,ITEM.Item_Desc,ITEM.Structure_Code,SA.Sales_Account, isnull(CA.GSOC_Acct,'') as GSOC_Acct from TSPL_ITEM_MASTER Item left join TSPL_SALES_ACCOUNTS SA on Item.Sale_Class_Code=SA.Sales_Class_Code left join TSPL_CUSTOMER_ACCOUNT_SET  CA on Item.Cust_Account =CA.Cust_Account ) Item on Item.Item_Code =xx.[Item Code] "
        ''--------------------------
        '' transaction unit conversion
        strMCCMaterial += " inner join (" & qryTransStock & ") as  TransStock on xx.[Item Code]=TransStock.Item_Code  "
        If Not stock_uom Then
            strMCCMaterial += " and TransStock.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & ""
        End If
        ''end transaction unit conversion
        strMCCMaterial += " left join (" & strItemGroup & ") as Item_Group on Item.Structure_Code=Item_Group.Structure_Code "
        'strMCCMaterial += " left outer join TSPL_LOCATION_MASTER as TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER .Location_Code =final.To_location "
        strMCCMaterial += " left join (" & qryStock & ") as Stock_SU on xx.[Item Code]=Stock_SU.Item_Code and xx.[UOM]=Stock_SU.UOM_Code "
        '===============================Added By Preeti Gupta===================================
        strMCCMaterial += " left join (" & qryStock & ") as Rate_Stock_SU on xx.[Item Code]=Rate_Stock_SU.Item_Code and isnull(xx.[RATE_UOM],xx.UOM) =Rate_Stock_SU.UOM_Code  "
        strMCCMaterial += " inner join (" & qryTransStock & ") as Rate_select_SU on xx.[Item Code]=Rate_select_SU.Item_Code  "
        If Not stock_uom Then
            strMCCMaterial += " and Rate_select_SU.UOM_Code=" & IIf(clsCommon.myLen(Unit_Code) <= 0, "xx.[UOM]", "'" & Unit_Code & "'") & ""
        End If
        ' ============================================================================================
        strMCCMaterial += " left join (" & qryKG & ") as StockKG on xx.[Item Code]=StockKG.Item_Code  "
        strMCCMaterial += " left join (select Cust_Code,Cust_Group_Code,TSPL_CUSTOMER_MASTER.Zone_Code,TSPL_CUSTOMER_MASTER.Struct_Code,TSPL_CUSTOMER_MASTER.Tin_No,TSPL_CUSTOMER_MASTER.state as State_Code,tspl_State_Master.State_Name from TSPL_CUSTOMER_MASTER left join tspl_State_Master on tspl_State_Master.state_Code=TSPL_CUSTOMER_MASTER.state) as Cust on xx.[Customer Code]=Cust.Cust_Code  left join (select location_Code as Cust_Code,Tin_No,State_Code,State_Name from TSPL_location_MASTER left join tspl_State_Master on tspl_State_Master.state_Code=TSPL_location_MASTER.state) as Cust_Loc on xx.[Customer Code]=Cust_Loc.Cust_Code  "
        strMCCMaterial += " left join (select Cust_Group_Code,Cust_Group_Desc from TSPL_CUSTOMER_GROUP_MASTER) as Cust_Group on Cust.Cust_Group_Code=Cust_Group.Cust_Group_Code "
        strMCCMaterial += " left join (select Zone_Code,Description from TSPL_ZONE_MASTER) as Zone on Cust.Zone_Code=Zone.Zone_Code " & _
                          " left join TSPL_LOCATION_MASTER as Loc on Loc.Location_Code=xx.[Location Code] "
        If clsCommon.myLen(strCategoryTable) > 0 Then
            strMCCMaterial += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=xx.[Item Code]"
        End If
        ''==========Monika
        strMCCMaterial += " left outer join (" + qryQC + ")QC on QC.item_code=xx.[Item Code] "
        ''================end here=====
        strMCCMaterial += " where 2 = 2  and  convert(date,xx.Document_Date,103) >= convert(date,('" + From_Date + "'),103) and convert(date,xx.Document_Date,103) <= convert(date,('" + To_Date + "'),103) " ' + clsCommon.myCstr(IIf(clsCommon.myLen(txtUOM.Value) > 0, "and xx.[UOM]='" + txtUOM.Value + "' ", ""))
        QryLst.Add(strMCCMaterial)

        Return QryLst
    End Function
    Public Shared Function GetTaxQuery(ByVal lstTables As List(Of String), ByVal lstTableDocDateCols As List(Of String), ByVal obj As clsSaleRegisterForAutoCompParameterType) As String
        Dim qry As String = String.Empty
        If Not lstTables Is Nothing AndAlso lstTables.Count > 0 Then
            For Each TableName As String In lstTables
                For intloop As Integer = 1 To 10
                    If clsCommon.myLen(qry) <= 0 Then
                        qry = "select distinct TAX" & intloop & " from " & TableName & " where cast(" & lstTableDocDateCols.Item(lstTables.IndexOf(TableName)) & " as Date) between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' and coalesce(Tax" & intloop & ",'')<>''"
                    Else
                        qry = qry & " Union  " & "select distinct TAX" & intloop & " from " & TableName & " where cast(" & lstTableDocDateCols.Item(lstTables.IndexOf(TableName)) & " as Date) between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "' and coalesce(Tax" & intloop & ",'')<>''"
                    End If
                Next
            Next
        Else
            Return qry
        End If
        Return qry
    End Function
    Public Shared Function GetReportData(ByVal obj As clsSaleRegisterForAutoCompParameterType) As DataTable
        Dim strRunQuery As String = ""
        strRunQuery = GetReportDataQuery(obj)
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strRunQuery)
        Return dt

    End Function
    Public Shared Function GetMIS_ITem_GroupColumn() As String
        Dim MIS_Item_Group As String = ""
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, Nothing))
        Return MIS_Item_Group
    End Function
    Public Shared Function GetTrasactionItemCodeQry(ByVal obj As clsSaleRegisterForAutoCompParameterType) As String
        Dim qry As String = ""
        Dim FirstQuery As Boolean = False
        'qry = " SELECT DISTINCT xxx.Code,  xxx.Name ,xxx.[Document Code],xxx.[Document Date],xxx.[Customer Code] FROM (" + Environment.NewLine
        If obj.Trans_Type_List.Contains("General Sale") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code From TSPL_SD_SALE_INVOICE_DETAIL INNER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE Where Trans_Type in ('ALL','SD')  and Cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date) between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Fresh Sale") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code From TSPL_SD_SALE_INVOICE_DETAIL INNER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE Where Trans_Type='FS'  and cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date) between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Product Sale") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code From TSPL_SD_SALE_INVOICE_DETAIL INNER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE Where Trans_Type='PS'  and cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date) between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If
        If obj.Trans_Type_List.Contains("MCC Sale") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code From TSPL_SD_SALE_INVOICE_DETAIL INNER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE Where Trans_Type='MCC'  and cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date) between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("CSA Sale") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code From TSPL_SD_SALE_INVOICE_DETAIL INNER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE Where Trans_Type='CSA'  and cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date) between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If
        If obj.Trans_Type_List.Contains("Export Sale") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code From TSPL_SD_SALE_INVOICE_DETAIL INNER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE Where TSPL_SD_SALE_INVOICE_HEAD.Trans_Type='EXP' and TSPL_SD_SALE_INVOICE_HEAD.Document_Type ='EX'  and cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date) between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Merchant Trade") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT distinct TSPL_SD_SALE_INVOICE_DETAIL.Item_Code From TSPL_SD_SALE_INVOICE_DETAIL INNER JOIN TSPL_SD_SALE_INVOICE_HEAD ON TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_INVOICE_HEAD.DOCUMENT_CODE Where Trans_Type='EXP'  and TSPL_SD_SALE_INVOICE_HEAD.Document_Type ='MT' and cast(TSPL_SD_SALE_INVOICE_HEAD.Document_Date as Date) between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Bulk Sale") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT distinct TSPL_INVOICE_DETAIL_BULKSALE.Item_Code FROM TSPL_INVOICE_DETAIL_BULKSALE inner join TSPL_INVOICE_MASTER_BULKSALE on  TSPL_INVOICE_DETAIL_BULKSALE.Document_No=TSPL_INVOICE_MASTER_BULKSALE.Document_No where cast(TSPL_INVOICE_MASTER_BULKSALE.Document_Date as date)  between '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Transfer") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " select DISTINCT Item_Code from TSPL_TRANSFER_ORDER_DETAIL INNER JOIN TSPL_TRANSFER_ORDER_HEAD ON TSPL_TRANSFER_ORDER_DETAIL.Document_No=TSPL_TRANSFER_ORDER_HEAD.Document_No WHERE CAST(TSPL_TRANSFER_ORDER_HEAD.Document_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Misc Sale") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " select DISTINCT Item_Code from TSPL_SCRAPINVOICE_DETAIL INNER JOIN TSPL_SCRAPINVOICE_HEAD ON TSPL_SCRAPINVOICE_DETAIL.invoice_No=TSPL_SCRAPINVOICE_HEAD.invoice_No" & _
                  " WHERE CAST(TSPL_SCRAPINVOICE_HEAD.shipment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Misc Sale") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " select DISTINCT Item_Code from TSPL_SCRAPINVOICE_DETAIL INNER JOIN TSPL_SCRAPINVOICE_HEAD ON TSPL_SCRAPINVOICE_DETAIL.invoice_No=TSPL_SCRAPINVOICE_HEAD.invoice_No" & _
                  " WHERE CAST(TSPL_SCRAPINVOICE_HEAD.shipment_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If
        '======Added by preeti Gupta Against Ticket No[ADV/30/04/18-000027]==========
        If obj.Trans_Type_List.Contains("General Sale Return") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT DISTINCT TSPL_SD_SALE_RETURN_DETAIL.Item_Code FROM TSPL_SD_SALE_RETURN_DETAIL INNER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Trans_Type in ('ALL','SD') and  CAST(TSPL_SD_SALE_RETURN_HEAD.Document_Date AS DATE) BETWEEN  '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If
        If obj.Trans_Type_List.Contains("Fresh Sale Return") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT DISTINCT TSPL_SD_SALE_RETURN_DETAIL.Item_Code FROM TSPL_SD_SALE_RETURN_DETAIL INNER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Trans_Type='FS' and  CAST(TSPL_SD_SALE_RETURN_HEAD.Document_Date AS DATE) BETWEEN  '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Product Sale Return") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT DISTINCT TSPL_SD_SALE_RETURN_DETAIL.Item_Code FROM TSPL_SD_SALE_RETURN_DETAIL INNER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Trans_Type='PS' and  CAST(TSPL_SD_SALE_RETURN_HEAD.Document_Date AS DATE) BETWEEN  '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If
        If obj.Trans_Type_List.Contains("MCC Sale Return") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT DISTINCT TSPL_SD_SALE_RETURN_DETAIL.Item_Code FROM TSPL_SD_SALE_RETURN_DETAIL INNER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Trans_Type='MCC' and  CAST(TSPL_SD_SALE_RETURN_HEAD.Document_Date AS DATE) BETWEEN  '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If
        If obj.Trans_Type_List.Contains("CSA Sale Return") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT DISTINCT TSPL_SD_SALE_RETURN_DETAIL.Item_Code FROM TSPL_SD_SALE_RETURN_DETAIL INNER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Trans_Type='CSA' and  CAST(TSPL_SD_SALE_RETURN_HEAD.Document_Date AS DATE) BETWEEN  '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Export Sale Return") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT DISTINCT TSPL_SD_SALE_RETURN_DETAIL.Item_Code FROM TSPL_SD_SALE_RETURN_DETAIL INNER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Trans_Type='EXP' and  CAST(TSPL_SD_SALE_RETURN_HEAD.Document_Date AS DATE) BETWEEN  '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("CSA Sale Patti Return") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT DISTINCT TSPL_SD_SALE_RETURN_DETAIL.Item_Code FROM TSPL_SD_SALE_RETURN_DETAIL INNER JOIN TSPL_SD_SALE_RETURN_HEAD ON TSPL_SD_SALE_RETURN_DETAIL.DOCUMENT_CODE=TSPL_SD_SALE_RETURN_HEAD.Document_Code WHERE TSPL_SD_SALE_RETURN_HEAD.Trans_Type='CPR' and  CAST(TSPL_SD_SALE_RETURN_HEAD.Document_Date AS DATE) BETWEEN  '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If

        If obj.Trans_Type_List.Contains("Bulk Sale Return") Then
            If FirstQuery Then
                qry = qry & " Union All "
            End If
            FirstQuery = True
            qry = qry & " SELECT DISTINCT TSPL_SALE_RETURN_DETAIL_BULKSALE.Item_Code FROM TSPL_SALE_RETURN_DETAIL_BULKSALE INNER JOIN TSPL_SALE_RETURN_MASTER_BULKSALE ON TSPL_SALE_RETURN_DETAIL_BULKSALE.Document_No=TSPL_SALE_RETURN_MASTER_BULKSALE.Document_No WHERE CAST(TSPL_SALE_RETURN_MASTER_BULKSALE.Document_Date AS DATE) BETWEEN '" & clsCommon.GetPrintDate(obj.From_Date, "dd-MMM-yyyy") & "' and '" & clsCommon.GetPrintDate(obj.To_Date, "dd-MMM-yyyy") & "'"
        End If
        Return qry

    End Function

    Public Shared Function GetTaxQuery(ByVal obj As clsSaleRegisterForAutoCompParameterType) As String
        Dim qryTaxQuery As String = ""
        Dim lstTables As New List(Of String)
        Dim lstTableDocDateCols As New List(Of String)
        If obj.Trans_Type_List.Contains("Fresh Sale") OrElse obj.Trans_Type_List.Contains("Product Sale") OrElse obj.Trans_Type_List.Contains("MCC Sale") OrElse obj.Trans_Type_List.Contains("Export Sale") OrElse obj.Trans_Type_List.Contains("CSA Sale") OrElse obj.Trans_Type_List.Contains("Merchant Trade") OrElse obj.Trans_Type_List.Contains("General Sale") Then
            lstTables.Add("TSPL_SD_SALE_INVOICE_HEAD")
            lstTableDocDateCols.Add("Document_Date")
        End If
        If obj.Trans_Type_List.Contains("Misc Sale") Then
            lstTables.Add("TSPL_SCRAPINVOICE_HEAD")
            lstTableDocDateCols.Add("shipment_Date")
        End If
        '=====================Added by preeti Gupta Against Ticket No [ADV/30/04/18-000027]=================
        If obj.Trans_Type_List.Contains("Fresh Sale Return") OrElse obj.Trans_Type_List.Contains("CSA Sale Return") OrElse obj.Trans_Type_List.Contains("Product Sale Return") OrElse obj.Trans_Type_List.Contains("MCC Sale Return") OrElse obj.Trans_Type_List.Contains("Export Sale Return") OrElse obj.Trans_Type_List.Contains("General Sale Return") OrElse obj.Trans_Type_List.Contains("Bulk Sale Return") Then
            lstTables.Add("TSPL_SD_SALE_RETURN_HEAD")
            lstTableDocDateCols.Add("Document_Date")
        End If

        If obj.Trans_Type_List.Contains("Transfer") OrElse obj.Trans_Type_List.Contains("Transfer Return") Then
            lstTables.Add("TSPL_TRANSFER_ORDER_HEAD")
            lstTableDocDateCols.Add("Document_Date")
        End If

        If obj.Trans_Type_List.Contains("CSA Sale") Then
            lstTables.Add("TSPL_CSA_TRANSFER_HEAD")
            lstTableDocDateCols.Add("Transfer_Date")
        End If

        'sanjay
        If obj.Trans_Type_List.Contains("Misc Sale Return") Then
            lstTables.Add("TSPL_SCRAPSALE_HEAD_RETURN")
            lstTableDocDateCols.Add("Return_ship_Date")
        End If
        'sanjay

        qryTaxQuery = GetTaxQuery(lstTables, lstTableDocDateCols, obj)
        Return qryTaxQuery
    End Function
    Public Shared Function GetAllSaleTransactionTypeQuery(Optional ByVal DocFinder As Boolean = False) As String
        Dim qry As String
        If DocFinder = True Then
            qry = " SELECT DISTINCT xxx.Code,  xxx.Name " & If(DocFinder = False, "", ",xxx.[Document Code],xxx.[Document Date],xxx.[Customer Code]") & " FROM (" + Environment.NewLine & _
                       " SELECT DISTINCT 'SD' As Code , 'General Sale' As Name " & If(DocFinder = False, "", ",Document_Code as [Document Code],Document_Date  as [Document Date],Customer_Code as [Customer Code]") & " From  TSPL_SD_SALE_INVOICE_HEAD Where Trans_Type='ALL' " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT Trans_Type+ISNULL(Document_Type,'')  As Code ,CASE " & _
                       " WHEN Trans_Type ='CSA' THEN 'CSA Sale' " & _
                       " WHEN Trans_Type ='EXP' AND Document_Type ='EX' THEN 'Export Sale' " & _
                       " WHEN Trans_Type ='FS' THEN 'Fresh Sale'  " & _
                       " WHEN Trans_Type ='MCC' THEN 'MCC Sale' " & _
                       " WHEN Trans_Type ='PS' THEN 'Product Sale' " & _
                       " WHEN Trans_Type ='EXP' AND Document_Type ='MT' THEN 'Merchant Trade' " & _
                       " WHEN Trans_Type ='ALL' THEN 'Standard Sale' " & _
                       " End " & _
                       " As Name " & If(DocFinder = False, "", ",Document_Code as [Document Code],Document_Date  as [Document Date],Customer_Code as [Customer Code]") & " FROM TSPL_SD_SALE_INVOICE_HEAD Where Trans_Type <>'ALL'  union all select distinct 'CSA' as Code,'CSA Sale' as Name " & If(DocFinder = False, "", ",DOC_CODE as [Document Code],TRANSFER_DATE  as [Document Date],CUST_CODE as [Customer Code]") & " from tspl_csa_transfer_head " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT 'BS'  As Code ,'Bulk Sale' As Name " & If(DocFinder = False, "", ",Document_No as [Document Code],Document_Date  as [Document Date],Customer_Code as [Customer Code]") & " FROM TSPL_INVOICE_MASTER_BULKSALE  " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT 'BST'  As Code ,'Bulk Sale Trade' As Name " & If(DocFinder = False, "", ",Document_No as [Document Code],Document_Date  as [Document Date],Customer_Code as [Customer Code]") & " FROM TSPL_INVOICE_MASTER_BULKSALE " & _
                       " UNION ALL " + Environment.NewLine & _
                       " Select DISTINCT 'SS'  As Code ,'Misc Sale' As Name " & If(DocFinder = False, "", ",invoice_No as [Document Code],posting_Date  as [Document Date],cust_Code as [Customer Code]") & " FROM TSPL_SCRAPINVOICE_HEAD " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT 'BSR'  As Code ,'Bulk Sale Return' As Name " & If(DocFinder = False, "", ",Document_No as [Document Code],Document_Date  as [Document Date],Customer_Code as [Customer Code]") & " FROM TSPL_SALE_RETURN_MASTER_BULKSALE " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT  DISTINCT 'SDR'  As Code ,'General Sale Return'  As Name " & If(DocFinder = False, "", ",Document_Code as [Document Code],Document_Date  as [Document Date],Customer_Code as [Customer Code]") & " From TSPL_SD_SALE_RETURN_HEAD Where Trans_Type='ALL' " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT Trans_Type+'R' As Code ,CASE " & _
                       " WHEN Trans_Type ='CSA' THEN 'CSA Sale Return' " & _
                       " WHEN Trans_Type ='EXP' THEN 'Export Sale Return' " & _
                       " WHEN Trans_Type ='FS'  THEN 'Fresh Sale Return'  " & _
                       " WHEN Trans_Type ='MCC' THEN 'MCC Sale Return' " & _
                       " WHEN Trans_Type ='PS'  THEN 'Product Sale Return' " & _
                       " WHEN Trans_Type ='CPR'  THEN 'CSA Sale Patti Return' " & _
                       " END " & _
                       " As Name " & If(DocFinder = False, "", ",Document_Code as [Document Code],Document_Date  as [Document Date],Customer_Code as [Customer Code]") & " FROM TSPL_SD_SALE_RETURN_HEAD Where Trans_Type not in ('ALL','CPR') " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT 'Transfer'  As Code ,'Transfer' As Name " & If(DocFinder = False, "", ",Document_No as [Document Code],Document_Date  as [Document Date],'' as [Customer Code]") & " FROM TSPL_TRANSFER_ORDER_HEAD " & _
                        " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT 'Transfer Return'  As Code ,'Transfer Return' As Name " & If(DocFinder = False, "", ",Document_No as [Document Code],Document_Date  as [Document Date],'' as [Customer Code]") & " FROM TSPL_TRANSFER_RETURN " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT 'MCC Transfer' As Code ,'MCC Transfer' As Name " & If(DocFinder = False, "", ",Receipt_Challan_No as [Document Code],Receipt_Challan_Date  as [Document Date],'' as [Customer Code]") & " FROM TSPL_MILK_TRANSFER_IN " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT 'Tanker Dispatch Return' As Code ,'Tanker Dispatch Return' As Name " & If(DocFinder = False, "", ",Document_No as [Document Code],Document_Date  as [Document Date],'' as [Customer Code]") & " FROM TSPL_MCC_Dispatch_Challan_Return " & _
                       " UNION ALL " + Environment.NewLine & _
                       " SELECT DISTINCT 'MCC Tanker Dispatch Return' As Code ,'MCC Tanker Dispatch Return' As Name " & If(DocFinder = False, "", ",Return_NO as [Document Code],Return_Date  as [Document Date],'' as [Customer Code]") & " FROM TSPL_MCC_Tanker_Dispatch_Return_head " & _
                       " ) xxx "
        Else
            'qry = " select 'BS' as Code,'Bulk Sale' as Name " & Environment.NewLine &
            '      " union all" & Environment.NewLine & _
            '      " select 'BSR','Bulk Sale Return' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'BST' as Code,'Bulk Sale Trade' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'CSA' as Code,'CSA Sale' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'CSAR' as Code,'CSA Sale Return' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'EXPEX','Export Sale' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'EXPMT' as Code,'Merchant Trade' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'FS' as Code,'Fresh Sale' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'FSR','Fresh Sale Return' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'MCC','MCC Sale' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'MCC Transfer','MCC Transfer' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'MCCR','MCC Sale Return' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'PS' as Code,'Product Sale' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'PSR','Product Sale Return' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'SS' as Code,'Misc Sale' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'Tanker Dispatch Return','Tanker Dispatch Return' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'MCC Tanker Dispatch Return','MCC Tanker Dispatch Return' as Name " & Environment.NewLine & _
            '      " Union All" & Environment.NewLine & _
            '      " select 'Transfer' as Code,'Transfer' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'Transfer Return' as Code,'Transfer Return' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'MCCFS' as Code,'MCC Sale Farmer' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'MCCFSR' as Code,'MCC Sale Return Farmer' as Name " & Environment.NewLine & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'MS-SR' as Code,'Misc Sale Return' as Name " & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'SD' as Code,'General Sale' as Name " & _
            '      " Union All " & Environment.NewLine & _
            '      " select 'SDR' as Code,'General Sale Return' as Name "




            qry = " select 'SD' as Code,'General Sale' as Name " & _
                  " Union All " & Environment.NewLine & _
                  " select 'SDR' as Code,'General Sale Return' as Name "


        End If

        Return qry
    End Function
    
End Class
Public Class clsSaleRegisterForAutoCompParameterType
    Public stockinguom As Boolean = False
    Public ReportType As String
    Public From_Date As Date
    Public To_Date As Date
    Public Item_Code_List As ArrayList = Nothing
    Public Trans_Type_List As ArrayList = Nothing
    Public State_List As ArrayList = Nothing
    Public Location_Code_List As ArrayList = Nothing
    Public Customer_Code_List As ArrayList = Nothing
    Public Item_Group_List As ArrayList = Nothing
    Public Cust_Group_Code_List As ArrayList = Nothing
    Public Document_Code As String = ""
    Public Unit_Code As String = ""
    Public Other_Cond As String = ""
    Public strPivotForFinalOuterQuery As String = ""
    Public MiscSaleSubCategory As String = ""
    ''-----------------------Monika 11/04/2017
    Public rbtnCategorySected As Boolean = True
    Public Doc_Code_List As ArrayList = Nothing
    Public Acc_Code_List As ArrayList = Nothing
    Public Include_Debit_Credit As Boolean = False
    Public Include_MCCFarmerSale As Boolean = False
    Public QuickLoad As Boolean = True
    Public ShowMismatchDoc As Boolean = False
    Public PickCSASaleFromSalePatti As Boolean = False


    Public Shared Function GetReportData(ByVal obj As clsSaleRegisterForAutoCompParameterType) As DataTable
        Return clsSalesRegisterForAutoComp.GetReportData(obj)
    End Function
End Class

