Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


Public Class rptCustomerWiseSalesReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Dim MIS_Item_Group As String = ""
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        ' Month wise record 
        'fromDate.Format = DateTimePickerFormat.Custom
        'fromDate.CustomFormat = "MMM/yyyy"
        'ToDate.Format = DateTimePickerFormat.Custom
        'ToDate.CustomFormat = "MMM/yyyy"
        ' year wise record 
        'fromDate.Format = DateTimePickerFormat.Custom
        'fromDate.CustomFormat = "yyyy"
        'ToDate.Format = DateTimePickerFormat.Custom
        'ToDate.CustomFormat = "yyyy"

    End Sub
    Sub Reset()

        txtCustomer.arrValueMember = Nothing
        txtCustomer.arrDispalyMember = Nothing
        txtZone.arrValueMember = Nothing
        txtZone.arrDispalyMember = Nothing
        txtUOM.Value = ""
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        'RadGroupBox3.Enabled = True
        'txtUOM.Enabled = True
        'txtCustomer.Enabled = True
        'txtZone.Enabled = True
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub GetReportGridID()
        Dim VarID As String = ""
        If rdbDay.Checked = True Then
            VarID += "_D"
        ElseIf rdbMonth.Checked = True Then
            VarID += "_M"
        ElseIf rdbYear.Checked = True Then
            VarID += "_Y"
        End If
        Gv1.VarID = VarID

    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            GetReportGridID()
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            If clsCommon.myLen(txtUOM.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select UOM first", Me.Text)
                Exit Sub
            End If

            Dim strItemGroup As String = ""
            strItemGroup = " select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join (" &
                       " select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'  " &
                       " and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code" &
                       " left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value "

            Dim strDateForPivot As String = ""
            Dim strDateWithIsNull As String = ""
            Dim strDateForTotal As String = ""
            Dim whr As String = " and 2= 2 "
            Dim dateFormatewise As String = ""
            If rdbWithSchemeAndSample.Checked = True Then
                whr += " "
            Else
                whr += " and isnull (TSPL_SD_SALE_INVOICE_HEAD.IsSampling,0) = 0 and TSPL_SD_SALE_INVOICE_DETAIL.Scheme_Item  = 'N'"
            End If

            If rdbDay.Checked = True Then
                strDateForPivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',['+thedate+']'  from (select  convert (varchar,thedate,103) as thedate from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "')    ) XXX order by convert (date, thedate,103) asc   For XML Path('')),1,1,'') "))
                If clsCommon.myLen(strDateForPivot) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                strDateWithIsNull = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ',isnull (['+thedate+'],0) as ['+thedate+'] '  from (select  convert (varchar,thedate,103) as thedate from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "')    ) XXX order by convert (date, thedate,103) asc   For XML Path('')),1,1,'') "))
                strDateForTotal = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select '+ isnull (['+thedate+'],0)  ' from (select  convert (varchar,thedate,103) as thedate from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "')    ) XXX order by convert (date, thedate,103) asc   For XML Path('')),1,1,'') "))
                whr += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '" + fromDate.Value + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + ToDate.Value + "',103)   "
                dateFormatewise = "  convert (varchar,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  "
            ElseIf rdbMonth.Checked = True Then
                'strDateForPivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Declare @colsMonthYear As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy")) + "',103) union all select dateadd(month,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value.AddMonths(-1), "dd-MMM-yyyy")) + "',103))
                '                  select  STUFF((Select  ',' + QUOTENAME(convert(varchar,DATENAME(MONTH,dates_cte.Date)) 
                '                  + '-' +convert(varchar,Year(dates_cte.Date))) as Alies_Name
                '                  FROM dates_cte order by  Year(dates_cte.Date),MONTH(dates_cte.Date)
                '                  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))
                strDateForPivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ',['+thedate+']'  from (select  distinct  convert (varchar,DATENAME(month,(thedate)))+ '-'+ convert (varchar, year((thedate))) as thedate, month((thedate)) as MonthNo, year((thedate)) as YearNo from ExplodeDates ('" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo, MonthNo asc   For XML Path('')),1,1,'') "))
                If clsCommon.myLen(strDateForPivot) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                'strDateWithIsNull = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Declare @colsMonthYear As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy")) + "',103) union all select dateadd(month,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value.AddMonths(-1), "dd-MMM-yyyy")) + "',103))
                '                  select  STUFF((Select  ',isnull(' + QUOTENAME(convert(varchar,DATENAME(MONTH,dates_cte.Date)) 
                '                  + '-' +convert(varchar,Year(dates_cte.Date))) + ',0) as ' + QUOTENAME(convert(varchar,DATENAME(MONTH,dates_cte.Date)) 
                '                  + '-' +convert(varchar,Year(dates_cte.Date))) as Alies_Name
                '                  FROM dates_cte order by  Year(dates_cte.Date),MONTH(dates_cte.Date)
                '                  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))
                strDateWithIsNull = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ',isnull (['+thedate+'],0) as ['+thedate+'] '  from (select  distinct  convert (varchar,DATENAME(month,(thedate)))+ '-'+ convert (varchar, year((thedate))) as thedate, month((thedate)) as MonthNo, year((thedate)) as YearNo from ExplodeDates ('" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo, MonthNo asc   For XML Path('')),1,1,'') "))

                'strDateForTotal = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Declare @colsMonthYear As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd-MMM-yyyy")) + "',103) union all select dateadd(month,1,date) from dates_cte where convert(date,date,103)<convert(date,'" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value.AddMonths(-1), "dd-MMM-yyyy")) + "',103))
                '                  select  STUFF((Select  '+ isnull(' + QUOTENAME(convert(varchar,DATENAME(MONTH,dates_cte.Date)) 
                '                  + '-' +convert(varchar,Year(dates_cte.Date))) + ',0) '  as Alies_Name
                '                  FROM dates_cte order by  Year(dates_cte.Date),MONTH(dates_cte.Date)
                '                  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')"))
                strDateForTotal = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select '+ isnull (['+thedate+'],0)  '  from (select  distinct  convert (varchar,DATENAME(month,(thedate)))+ '-'+ convert (varchar, year((thedate))) as thedate, month((thedate)) as MonthNo, year((thedate)) as YearNo from ExplodeDates ('" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo, MonthNo asc   For XML Path('')),1,1,'') "))

                Dim EndDayOfToDate As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select day( EOMONTH('" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd-MMM-yyyy")) + "'))"))
                whr += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '01-" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "yyyy")) + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '" + EndDayOfToDate + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "MMM")) + "-" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "yyyy")) + "',103)   "
                dateFormatewise = " convert (varchar, DATENAME( month ,TSPL_SD_SALE_INVOICE_HEAD.Document_Date)) + '-'+ convert (varchar, Year (TSPL_SD_SALE_INVOICE_HEAD.Document_Date))  "

            ElseIf rdbYear.Checked = True Then
                'strDateForPivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Declare @colsYear As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'01-Jan-" + fromDate.Text + "',103) union all select dateadd(month,1,date) from dates_cte where convert(date,date,103)<convert(date,'30-Nov-" + ToDate.Text + "',103))
                '                  select  STUFF((Select   ',' + QUOTENAME( 
                '                  convert(varchar,Year(dates_cte.Date))) as Alies_Name
                '                  FROM (select distinct convert(varchar,Year(dates_cte.Date)) as Date from dates_cte) as dates_cte order by   Year(dates_cte.Date) asc
                '                  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
                strDateForPivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ',['+thedate+']'  from (select  distinct   convert (varchar, year((thedate))) as thedate, year((thedate)) as YearNo from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo asc   For XML Path('')),1,1,'') "))
                If clsCommon.myLen(strDateForPivot) <= 0 Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                End If
                'strDateWithIsNull = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Declare @colsYear As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'01-Jan-" + fromDate.Text + "',103) union all select dateadd(month,1,date) from dates_cte where convert(date,date,103)<convert(date,'30-Nov-" + ToDate.Text + "',103))
                '                  select  STUFF((Select   ', isnull (' + QUOTENAME( 
                '                  convert(varchar,Year(dates_cte.Date))) + ',0 ) as ' + QUOTENAME( 
                '                  convert(varchar,Year(dates_cte.Date)))  as Alies_Name
                '                  FROM (select distinct convert(varchar,Year(dates_cte.Date)) as Date from dates_cte) as dates_cte order by   Year(dates_cte.Date) asc
                '                  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
                strDateWithIsNull = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ',isnull (['+thedate+'],0) as ['+thedate+'] '  from (select  distinct   convert (varchar, year((thedate))) as thedate, year((thedate)) as YearNo from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo asc   For XML Path('')),1,1,'') "))
                'strDateForTotal = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Declare @colsYear As NVARCHAR(MAX),@query  As NVARCHAR(MAX) with dates_cte(Date) as (select convert(date,'01-Jan-" + fromDate.Text + "',103) union all select dateadd(month,1,date) from dates_cte where convert(date,date,103)<convert(date,'30-Nov-" + ToDate.Text + "',103))
                '                  select  STUFF((Select   '+ isnull (' + QUOTENAME( 
                '                  convert(varchar,Year(dates_cte.Date))) + ',0 ) '   as Alies_Name
                '                  FROM (select distinct convert(varchar,Year(dates_cte.Date)) as Date from dates_cte) as dates_cte order by   Year(dates_cte.Date) asc
                '                  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') "))
                strDateForTotal = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select '+ isnull (['+thedate+'],0)  '  from (select  distinct   convert (varchar, year((thedate))) as thedate, year((thedate)) as YearNo from ExplodeDates ( '" + clsCommon.myCstr(clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy")) + "', '" + clsCommon.myCstr(clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy")) + "' )    ) XXX order by YearNo asc   For XML Path('')),1,1,'') "))
                whr += " and convert (date, TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) > =convert (date, '01-Jan-" + fromDate.Text + "',103)  and  convert (date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103)  < = convert (date, '31-Dec-" + ToDate.Text + "',103)   "
                dateFormatewise = "  convert (varchar, Year (TSPL_SD_SALE_INVOICE_HEAD.Document_Date))  "

            End If



            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                whr += " and TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ") "
            End If
            If txtZone.arrValueMember IsNot Nothing AndAlso txtZone.arrValueMember.Count > 0 Then
                whr += " and TSPL_CUSTOMER_MASTER.Zone_Code in (" + clsCommon.GetMulcallString(txtZone.arrValueMember) + ") "
            End If

            If txtItemGroup.arrValueMember IsNot Nothing AndAlso txtItemGroup.arrValueMember.Count > 0 Then
                whr += " and coalesce(Item_Group.Item_Group,'') in (" + clsCommon.GetMulcallString(txtItemGroup.arrValueMember) + ") "
            End If
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                whr += " and TSPL_SD_SALE_INVOICE_DETAIL.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")  "
            End If

            qry = "     select Customer_Code as [Customer Code] , Customer_Name as [Customer Name], Zone_Code as [Zone Code],Zone_Name as [Zone Name] , [Mobile No],[RSM Code],[RSM Name],[ZSM Code],[ZSM Name],[ASM Code],[ASM Name],[ASO Code],[ASO Name] ,Uom, " + strDateForTotal + " as [Grand Total] , " + strDateWithIsNull + "  from (
                        select final .Document_Date, final.Customer_Code, max(final.Customer_Name) as Customer_Name , max( final.Zone_Code )  as Zone_Code , max(final.Zone_Name) as  Zone_Name,max(final.Phone1)  as  [Mobile No],max([RSM Code]) as [RSM Code],max([RSM Name]) AS [RSM Name],max([ZSM Code]) as [ZSM Code],max([ZSM Name]) AS [ZSM Name],max([ASM Code]) as [ASM Code],max([ASM Name]) AS [ASM Name],max([ASO Code]) as [ASO Code],max([ASO Name]) AS [ASO Name] ,'" + clsCommon.myCstr(txtUOM.Value) + "' as Uom ,  sum(Final_Qty) as Qty  from (
                        select   TSPL_SD_SALE_INVOICE_HEAD.Document_Code, " + dateFormatewise + " as Document_Date, TSPL_SD_SALE_INVOICE_HEAD.Customer_Code ,TSPL_SD_SALE_INVOICE_DETAIL.Qty, TSPL_CUSTOMER_MASTER.Customer_Name ,TSPL_CUSTOMER_MASTER.Zone_Code , TSPL_ZONE_MASTER.Description as Zone_Name ,TSPL_CUSTOMER_MASTER.Phone1,isnull(TSPL_CUSTOMER_MASTER.RSM,'') as [RSM Code],isnull(RSM.EMP_NAME,'') AS [RSM Name],isnull(TSPL_CUSTOMER_MASTER.ZSM,'') as [ZSM Code],isnull(ZSM.EMP_NAME,'') AS [ZSM Name],isnull(TSPL_CUSTOMER_MASTER.ASM,'') as [ASM Code],isnull(ASM.EMP_NAME,'') AS [ASM Name],isnull(TSPL_CUSTOMER_MASTER.ASO,'') as [ASO Code],isnull(ASO.EMP_NAME,'') AS [ASO Name]  , TSPL_SD_SALE_INVOICE_DETAIL.Item_Code, TSPL_SD_SALE_INVOICE_DETAIL.Unit_code, convert (decimal(18,2) , (TSPL_SD_SALE_INVOICE_DETAIL.Qty * Stocking_Conversion_Factor.Conversion_Factor ) / nullif (Target_Conversion_Factor.Conversion_Factor,0) ) as Final_Qty from TSPL_SD_SALE_INVOICE_DETAIL left outer join TSPL_SD_SALE_INVOICE_HEAD on TSPL_SD_SALE_INVOICE_HEAD.Document_Code  = TSPL_SD_SALE_INVOICE_DETAIL.Document_Code
                        left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_SD_SALE_INVOICE_HEAD.Customer_Code
                        left outer join TSPL_ZONE_MASTER on TSPL_ZONE_MASTER.Zone_Code = TSPL_CUSTOMER_MASTER.Zone_Code 
                        left outer join (Select TSPL_ITEM_UOM_DETAIL.ITem_Code,Conversion_Factor from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.UOM_Code = '" + txtUOM.Value + "' ) as Target_Conversion_Factor on Target_Conversion_Factor.Item_Code = TSPL_SD_SALE_INVOICE_DETAIL.Item_Code
                        left outer join TSPL_ITEM_UOM_DETAIL as Stocking_Conversion_Factor on TSPL_SD_SALE_INVOICE_DETAIL.item_Code = Stocking_Conversion_Factor.Item_Code and TSPL_SD_SALE_INVOICE_DETAIL.Unit_code = Stocking_Conversion_Factor.UOM_Code
                        left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.item_code=TSPL_SD_SALE_INVOICE_DETAIL.Item_Code 
                        left join (" & strItemGroup & ") as Item_Group on TSPL_ITEM_MASTER.Structure_Code=Item_Group.Structure_Code 
                        LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER AS RSM ON RSM.EMP_CODE=TSPL_CUSTOMER_MASTER.RSM
                        LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER AS ZSM ON ZSM.EMP_CODE=TSPL_CUSTOMER_MASTER.ZSM
                        LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER AS ASM ON ASM.EMP_CODE=TSPL_CUSTOMER_MASTER.ASM
                        LEFT OUTER JOIN TSPL_EMPLOYEE_MASTER AS ASO ON ASO.EMP_CODE=TSPL_CUSTOMER_MASTER.ASO
                        Where   TSPL_SD_SALE_INVOICE_HEAD.Trans_Type IN ('FS', 'PS') and TSPL_SD_SALE_INVOICE_HEAD.Screen_Type='DS'   " + whr + " 
                        ) final group by final .Document_Date, final.Customer_Code
                        ) XFinal
                        pivot ( sum(XFinal.Qty) for Document_Date  in (" + strDateForPivot + ")  ) as Pivo;  "

            dt = clsDBFuncationality.GetDataTable(qry)
            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                ' Gv1.Columns("Trans Type").IsVisible = False
                RadPageView1.SelectedPage = RadPageViewPage2


                If Gv1.Rows.Count > 0 Then
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    For i As Integer = 6 To Gv1.Columns.Count - 1
                        Dim aa = Gv1.Columns(i).HeaderText()
                        Dim item1 As New GridViewSummaryItem(aa, "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(item1)
                    Next

                    Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                    Gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
                End If
                Gv1.EnableFiltering = True
                Gv1.BestFitColumns()

                'RadGroupBox3.Enabled = False
                'txtUOM.Enabled = False
                'txtCustomer.Enabled = False
                'txtZone.Enabled = False

            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
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

    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptCustomerWiseSalesReport & "'"))

            If rdbDay.Checked = True Then
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Report Type : " & "Day wise")
            ElseIf rdbMonth.Checked = True Then
                arrHeader.Add(("Range: " + clsCommon.GetPrintDate(fromDate.Value, "MMM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "MMM/yyyy")) + " ")
                arrHeader.Add("Report Type : " & "Monthly")
            ElseIf rdbYear.Checked = True Then
                arrHeader.Add(("Range: " + clsCommon.GetPrintDate(fromDate.Value, "yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "yyyy")) + " ")
                arrHeader.Add("Report Type : " & "Yearly")
            End If
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtZone.arrDispalyMember IsNot Nothing AndAlso txtZone.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Zone : " + clsCommon.GetMulcallStringWithComma(txtZone.arrDispalyMember))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Customer wise Sales Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Customer wise Sales Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error, Me.Text)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("fndUOMMaster@CustomerWiseSalesReport", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " Select Cust_Code as Code, Customer_Name as Name from TSPL_CUSTOMER_MASTER "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustomerCode@CustWiseSaleRpt", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub txtZone__My_Click(sender As Object, e As EventArgs) Handles txtZone._My_Click
        Dim qry As String = " select Zone_Code  as Code, Description as Name from TSPL_ZONE_MASTER "
        txtZone.arrValueMember = clsCommon.ShowMultipleSelectForm("ZoneCode@CustWiseSaleRpt", qry, "Code", "Name", txtZone.arrValueMember, txtZone.arrDispalyMember)
    End Sub

    Private Sub rdbDay_CheckedChanged(sender As Object, e As EventArgs) Handles rdbDay.CheckedChanged
        Try
            If rdbDay.Checked = True Then
                fromDate.ShowUpDown = False
                ToDate.ShowUpDown = False
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "dd/MM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "dd/MM/yyyy"
            ElseIf rdbMonth.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "MMM/yyyy"
                fromDate.ShowUpDown = True
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "MMM/yyyy"
                ToDate.ShowUpDown = True
            ElseIf rdbYear.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "yyyy"
                fromDate.ShowUpDown = True
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "yyyy"
                ToDate.ShowUpDown = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbMonth_CheckedChanged(sender As Object, e As EventArgs) Handles rdbMonth.CheckedChanged
        Try
            If rdbDay.Checked = True Then
                fromDate.ShowUpDown = False
                ToDate.ShowUpDown = False
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "dd/MM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "dd/MM/yyyy"
            ElseIf rdbMonth.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "MMM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "MMM/yyyy"
            ElseIf rdbYear.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "yyyy"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub rdbYear_CheckedChanged(sender As Object, e As EventArgs) Handles rdbYear.CheckedChanged
        Try
            If rdbDay.Checked = True Then
                fromDate.ShowUpDown = False
                ToDate.ShowUpDown = False
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "dd/MM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "dd/MM/yyyy"
            ElseIf rdbMonth.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "MMM/yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "MMM/yyyy"
            ElseIf rdbYear.Checked = True Then
                fromDate.Format = DateTimePickerFormat.Custom
                fromDate.CustomFormat = "yyyy"
                ToDate.Format = DateTimePickerFormat.Custom
                ToDate.CustomFormat = "yyyy"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " &
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " &
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtItemGroup._My_Click
        Dim qry As String = " select Value as [Code],Description as Name from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
        txtItemGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("CWSRItemGroup", qry, "Code", "Name", txtItemGroup.arrValueMember, txtItemGroup.arrDispalyMember)
    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("CWSRItem", qry, "Item_Code", "Item_Code", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
End Class
