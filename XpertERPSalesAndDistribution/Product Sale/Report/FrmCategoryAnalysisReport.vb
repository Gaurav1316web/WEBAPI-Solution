Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop


Public Class FrmCategoryAnalysisReport
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public arrTransaction As ArrayList
    Dim dtCategory As DataTable
    Dim Categorytype As ArrayList
    Dim Categoryvalues As ArrayList
#End Region

    Public Sub New()
        InitializeComponent()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
        LoadCategory()
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Sub LoadCategory()
        rbtnCategoryAll.IsChecked = True
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub

    Private Sub GVcategorydata()
        Categorytype = New ArrayList()
        Categoryvalues = New ArrayList()
        Try
            If rbtnCategorySelect.IsChecked Then
                For ii As Integer = 0 To gvCategory.RowCount - 1
                    If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                        Categorytype.Add(gvCategory.Rows(ii).Cells("CODE").Value)
                        Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                        If arr IsNot Nothing AndAlso arr.Count > 0 Then
                            For Each strInn As String In arr.Keys
                                Categoryvalues.Add(strInn)
                            Next
                        Else
                            Dim qry As String = " select TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE from TSPL_ITEM_CATEGORY_LEVEL_VALUES where TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE='" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "'"
                            Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry)
                            If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                                For Each dr As DataRow In dt1.Rows
                                    Categoryvalues.Add(clsCommon.myCstr(dr("CODE").ToString()))
                                Next
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
            Categorytype = Nothing
            Categoryvalues = Nothing
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub SetUserMgmtNew()

        ' MyBase.SetUserMgmt(clsUserMgtCode.stockRecoBatch)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If

        btnExport.Visible = MyBase.isExport
        'btnQuickExport.Visible = MyBase.isExport

    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
            'DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub


    Private Sub LoadData(ByVal isPrintCrystalReport As Integer)
        Try
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True
            GVcategorydata()
            '=====1st Date========
            Dim strd As String = "01/" + clsCommon.myCstr(fromDate.Value.Month) + "/" + clsCommon.myCstr(fromDate.Value.Year)
            Dim From_Date As Date = clsCommon.myCDate(strd) 'fromDate.Value.AddYears(-1)

            Dim strmd As String = Nothing
            Dim days As Integer = 0
            days = DateTime.DaysInMonth(CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "yyyy"))), CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value, "MM"))))
            strmd = clsCommon.myCstr(clsCommon.myCstr(days) + "/" + clsCommon.GetPrintDate(fromDate.Value, "MMM") + "/" + clsCommon.GetPrintDate(fromDate.Value, "yyyy"))
            Dim To_Date As Date = clsCommon.myCDate(strmd)
            '============================
            '=====2nd Date========
            Dim strd2 As String = "01/" + clsCommon.myCstr(fromDate.Value.Month) + "/" + clsCommon.myCstr(fromDate.Value.Year)
            Dim From_Date2 As Date = clsCommon.myCDate(strd2).AddMonths(-1) 'fromDate.Value.AddYears(-1)

            Dim strmd2 As String = Nothing
            Dim days2 As Integer = 0
            days2 = DateTime.DaysInMonth(CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value.AddMonths(-1), "yyyy"))), CInt(clsCommon.myCdbl(clsCommon.GetPrintDate(fromDate.Value.AddMonths(-1), "MM"))))
            strmd2 = clsCommon.myCstr(clsCommon.myCstr(days2) + "/" + clsCommon.GetPrintDate(fromDate.Value.AddMonths(-1), "MMM") + "/" + clsCommon.GetPrintDate(fromDate.Value.AddMonths(-1), "yyyy"))
            Dim To_Date2 As Date = clsCommon.myCDate(strmd2)
            '============================
            '=====Fy Date========
            Dim Months As Integer = 0
            Months = clsCommon.myCdbl(fromDate.Value.Month)
            Dim strfy As String = Nothing
            Dim strtdfy As String = Nothing
            Dim From_Datefy As Date
            Dim To_Datefy As Date

            Dim strfLastFY As String = Nothing
            Dim strTLastFY As String = Nothing
            Dim From_Lastfy As Date
            Dim To_Lastfy As Date

            If Months <= 3 Then
                strfy = "01/" + "04" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(-1).Year)
                From_Datefy = clsCommon.myCDate(strfy)
                strtdfy = "31/" + "03" + "/" + clsCommon.myCstr(fromDate.Value.Year)
                To_Datefy = clsCommon.myCDate(strtdfy)

                strfLastFY = "01/" + "04" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(-2).Year)
                From_Lastfy = clsCommon.myCDate(strfLastFY)
                strTLastFY = "31/" + "03" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(-1).Year)
                To_Lastfy = clsCommon.myCDate(strTLastFY)

            Else
                strfy = "01/" + "04" + "/" + clsCommon.myCstr(fromDate.Value.Year)
                From_Datefy = clsCommon.myCDate(strfy)
                strtdfy = "31/" + "03" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(+1).Year)
                To_Datefy = clsCommon.myCDate(strtdfy)

                strfLastFY = "01/" + "04" + "/" + clsCommon.myCstr(fromDate.Value.AddYears(-1).Year)
                From_Lastfy = clsCommon.myCDate(strfLastFY)
                strTLastFY = "31/" + "03" + "/" + clsCommon.myCstr(fromDate.Value.Year)
                To_Lastfy = clsCommon.myCDate(strTLastFY)

            End If

           
            Dim strCategoryTable As String = ""

            Dim qry As String = Nothing
            Dim strpivot As String = Nothing
            Dim strpivotposition As String = Nothing
            Dim NoMaxqry As String = Nothing
            Dim NoMaxposition As String = Nothing

            If Categorytype.Count > 0 Then
                qry = "select stuff((select ','+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']' from TSPL_ITEM_CATEGORY_LEVEL WHERE TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(Categorytype).Replace(",", "','") + "') for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                qry = "select stuff((select ','+'Max('+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']) as [' +TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION+']' from TSPL_ITEM_CATEGORY_LEVEL WHERE TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(Categorytype).Replace(",", "','") + "') for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivotposition = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                NoMaxqry = "select stuff((select ','+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']' from TSPL_ITEM_CATEGORY_LEVEL WHERE TSPL_ITEM_CATEGORY_LEVEL.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(Categorytype).Replace(",", "','") + "') for xml path('') ), 1, 1, '') Category_Type_Desc"
                NoMaxposition = clsCommon.myCstr(clsDBFuncationality.getSingleValue(NoMaxqry))
            Else
                qry = "select stuff((select ','+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']' from TSPL_ITEM_CATEGORY_LEVEL for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivot = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                qry = "select stuff((select ','+'Max('+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']) as [' +TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION+']' from TSPL_ITEM_CATEGORY_LEVEL for xml path('') ), 1, 1, '') Category_Type_Desc"
                strpivotposition = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                NoMaxqry = "select stuff((select ','+'[' + TSPL_ITEM_CATEGORY_LEVEL.DESCRIPTION +']' from TSPL_ITEM_CATEGORY_LEVEL  for xml path('') ), 1, 1, '') Category_Type_Desc"
                NoMaxposition = clsCommon.myCstr(clsDBFuncationality.getSingleValue(NoMaxqry))
            End If


            strCategoryTable = "select Item_Code,max(Item_Desc) AS Item_Desc," + strpivotposition + "   from (" + Environment.NewLine & _
            " select * from ( " + Environment.NewLine & _
            " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
            " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
            " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
            " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
            " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
            " where 2=2 "
            If Categorytype.Count > 0 Then
                strCategoryTable += "and TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE IN('" + clsCommon.GetMulcallStringWithComma(Categorytype).Replace(",", "','") + "') "
            End If
            strCategoryTable += " )xx" + Environment.NewLine & _
        " Pivot " + Environment.NewLine & _
        " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strpivot + ")" + Environment.NewLine & _
        " ) Pivt" + Environment.NewLine & _
        " ) xxx group by Item_Code "

            ''End of Category Table start now.
            'End If

            Dim StrQuery As String = Nothing

            Dim StrInnerqry As String = Nothing
            StrInnerqry = " select Customer_Code,Amount as Total_Amt,Document_Date ,Bill_To_Location,Trans_Type,TSPL_SD_SALE_INVOICE_DETAIL.Item_Code from TSPL_SD_SALE_INVOICE_HEAD  LEFT OUTER JOIN TSPL_SD_SALE_INVOICE_DETAIL ON TSPL_SD_SALE_INVOICE_HEAD.Document_Code=TSPL_SD_SALE_INVOICE_DETAIL.DOCUMENT_CODE " & _
                          " union all " & _
                          " select Customer_Code,Amount as Total_Amt,Document_Date,Location_Code as Bill_To_Location,'BS' AS Trans_Type,TSPL_Dispatch_DETAIL_BULKSALE.Item_Code from TSPL_Dispatch_BulkSale LEFT OUTER JOIN TSPL_Dispatch_DETAIL_BULKSALE ON TSPL_Dispatch_BulkSale.Document_No=TSPL_Dispatch_DETAIL_BULKSALE.Document_No " & _
                          " union all " & _
                          " select TSPL_SD_SALE_RETURN_HEAD.Customer_Code , -(coalesce(TSPL_SD_SALE_RETURN_DETAIL.Amount,0)-isnull(TSPL_SD_SALE_RETURN_DETAIL.Damage_Amount ,0)) as Total_Amt ,TSPL_SD_SALE_RETURN_HEAD.Document_Date ,TSPL_SD_SALE_RETURN_HEAD.Bill_To_Location ,TSPL_SD_SALE_RETURN_HEAD.Trans_Type,TSPL_SD_SALE_RETURN_DETAIL.Item_Code from TSPL_SD_SALE_RETURN_HEAD left outer join TSPL_SD_SALE_RETURN_DETAIL on TSPL_SD_SALE_RETURN_HEAD.Document_Code = TSPL_SD_SALE_RETURN_DETAIL.Document_Code  where  Scheme_Item = 'N' "

            StrQuery = "select Cust_Code as [Customer Code] ,dddd .CA ,max(COUNTRY) AS Country,MAX(STATE) AS State,MAX(CITY) AS City,max([Location]) as [Location]," + strpivotposition + ",Item_Code as PRODUCT, sum(dddd.[Current Month Actual Sales]) as [Current Month Actual Sales], sum([Last Month Actual Sales]) as [Last Month Actual Sales],sum(dddd.[Current Year Actual Sales]) as [Current Year Actual Sales],sum([Last Year Actual Sales]) as [Last Year Actual Sales] from ( "

            StrQuery += "select TSPL_SD_SALE_INVOICE_HEAD.Trans_Type,TSPL_SD_SALE_INVOICE_HEAD.Item_Code,TSPL_CUSTOMER_MASTER.Cust_Code , TSPL_CUSTOMER_MASTER.Customer_Name AS CA,TSPL_CUSTOMER_MASTER.Country AS COUNTRY,TSPL_STATE_MASTER.STATE_NAME as STATE,TSPL_CITY_MASTER.City_Name AS CITY,TSPL_LOCATION_MASTER.Location_Desc AS [Location] "
            ' StrQuery += "select MAX(TSPL_CUSTOMER_MASTER.Customer_Name) AS CA,MAX(TSPL_CUSTOMER_MASTER.Country) AS COUNTRY,MAX(TSPL_STATE_MASTER.STATE_NAME) as STATE,MAX(TSPL_CITY_MASTER.City_Name) AS CITY,MAX(TSPL_LOCATION_MASTER.Location_Desc) AS [Location]"
            If clsCommon.myLen(strCategoryTable) > 0 Then
                'StrQuery += "," + strpivotposition + ""
                StrQuery += "," + NoMaxposition + ""
            End If
            ' Ticket No : ERO/24/04/19-000572 By Prabhakar
            StrQuery += ",TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Current Month Actual Sales],0 as [Last Month Actual Sales],0 " & _
                           " as [Current Year Actual Sales],0 as [Last Year Actual Sales] " & _
                           " from  ( " + StrInnerqry + " ) as TSPL_SD_SALE_INVOICE_HEAD " & _
                           "left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  " & _
                         "  left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code " & _
                           " left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location  " & _
                            " left outer join TSPL_STATE_MASTER ON TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State " & _
                            " left outer join TSPL_CITY_MASTER ON TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code "
            If clsCommon.myLen(strCategoryTable) > 0 Then
                StrQuery += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_SD_SALE_INVOICE_HEAD.Item_Code"
            End If
            StrQuery += " where 1=1 and CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=convert(date,'" + From_Date + "',103) AND CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + To_Date + "',103) "
            ' "  GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " ' ,DATEPART(MM,TSPL_SD_SALE_INVOICE_HEAD.Document_Date) "

            StrQuery += " UNION ALL "
            StrQuery += " select TSPL_SD_SALE_INVOICE_HEAD.Trans_Type,TSPL_SD_SALE_INVOICE_HEAD.Item_Code,TSPL_CUSTOMER_MASTER.Cust_Code ,TSPL_CUSTOMER_MASTER.Customer_Name AS CA,TSPL_CUSTOMER_MASTER.Country AS COUNTRY,TSPL_STATE_MASTER.STATE_NAME as STATE,TSPL_CITY_MASTER.City_Name AS CITY,TSPL_LOCATION_MASTER.Location_Desc AS [Location]"
            ' StrQuery += "select MAX(TSPL_CUSTOMER_MASTER.Customer_Name) AS CA,MAX(TSPL_CUSTOMER_MASTER.Country) AS COUNTRY,MAX(TSPL_STATE_MASTER.STATE_NAME) as STATE,MAX(TSPL_CITY_MASTER.City_Name) AS CITY,MAX(TSPL_LOCATION_MASTER.Location_Desc) AS [Location]"
            If clsCommon.myLen(strCategoryTable) > 0 Then
                ' StrQuery += "," + strpivotposition + ""
                StrQuery += "," + NoMaxposition + ""
            End If
            StrQuery += ",0 as [Current Month Actual Sales],TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Last Month Actual Sales],0 as [Current Year Actual Sales],0 as [Last Year Actual Sales]" & _
              " from  ( " + StrInnerqry + " ) as TSPL_SD_SALE_INVOICE_HEAD " & _
             "left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  " & _
              " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code " & _
               " left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location  " & _
              "  left outer join TSPL_STATE_MASTER ON TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State " & _
              "  left outer join TSPL_CITY_MASTER ON TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code "
            If clsCommon.myLen(strCategoryTable) > 0 Then
                StrQuery += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_SD_SALE_INVOICE_HEAD.Item_Code"
            End If
            StrQuery += "  where 1=1 and CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=convert(date,'" + From_Date2 + "',103) AND CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + To_Date2 + "',103) "
            ' "  GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Customer_Code " ',DATEPART(MM,TSPL_SD_SALE_INVOICE_HEAD.Document_Date) "

            StrQuery += " UNION ALL "
            ' StrQuery += "select MAX(TSPL_CUSTOMER_MASTER.Customer_Name) AS CA,MAX(TSPL_CUSTOMER_MASTER.Country) AS COUNTRY,MAX(TSPL_STATE_MASTER.STATE_NAME) as STATE,MAX(TSPL_CITY_MASTER.City_Name) AS CITY,MAX(TSPL_LOCATION_MASTER.Location_Desc) AS [Location]"
            StrQuery += " select TSPL_SD_SALE_INVOICE_HEAD.Trans_Type,TSPL_SD_SALE_INVOICE_HEAD.Item_Code,TSPL_CUSTOMER_MASTER.Cust_Code , TSPL_CUSTOMER_MASTER.Customer_Name AS CA,TSPL_CUSTOMER_MASTER.Country AS COUNTRY,TSPL_STATE_MASTER.STATE_NAME as STATE,TSPL_CITY_MASTER.City_Name AS CITY,TSPL_LOCATION_MASTER.Location_Desc AS [Location]"
            If clsCommon.myLen(strCategoryTable) > 0 Then
                'StrQuery += "," + strpivotposition + ""
                StrQuery += "," + NoMaxposition + ""
            End If
            StrQuery += ",0 as [Current Month Actual Sales],0 as [Last Month Actual Sales],TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Current Year Actual Sales] ,0 as [Last Year Actual Sales]" & _
                   " from  ( " + StrInnerqry + " ) as TSPL_SD_SALE_INVOICE_HEAD " & _
                   "left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  " & _
                    " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code " & _
                    " left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
                    " left outer join TSPL_STATE_MASTER ON TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State " & _
                    "  left outer join TSPL_CITY_MASTER ON TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code  "
            If clsCommon.myLen(strCategoryTable) > 0 Then
                StrQuery += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_SD_SALE_INVOICE_HEAD.Item_Code"
            End If
            StrQuery += " where 1=1 and CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=convert(date,'" + From_Datefy + "',103) AND CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + To_Datefy + "',103)  "
            ' " GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Customer_Code) dddd " ' ,DATEPART(yyyy,TSPL_SD_SALE_INVOICE_HEAD.Document_Date) ) dddd "

            StrQuery += " UNION ALL "
            ' StrQuery += "select MAX(TSPL_CUSTOMER_MASTER.Customer_Name) AS CA,MAX(TSPL_CUSTOMER_MASTER.Country) AS COUNTRY,MAX(TSPL_STATE_MASTER.STATE_NAME) as STATE,MAX(TSPL_CITY_MASTER.City_Name) AS CITY,MAX(TSPL_LOCATION_MASTER.Location_Desc) AS [Location]"
            StrQuery += " select TSPL_SD_SALE_INVOICE_HEAD.Trans_Type,TSPL_SD_SALE_INVOICE_HEAD.Item_Code,TSPL_CUSTOMER_MASTER.Cust_Code , TSPL_CUSTOMER_MASTER.Customer_Name AS CA,TSPL_CUSTOMER_MASTER.Country AS COUNTRY,TSPL_STATE_MASTER.STATE_NAME as STATE,TSPL_CITY_MASTER.City_Name AS CITY,TSPL_LOCATION_MASTER.Location_Desc AS [Location]"
            If clsCommon.myLen(strCategoryTable) > 0 Then
                'StrQuery += "," + strpivotposition + ""
                StrQuery += "," + NoMaxposition + ""
            End If
            StrQuery += ",0 as [Current Month Actual Sales],0 as [Last Month Actual Sales],0 as [Current Year Actual Sales] ,TSPL_SD_SALE_INVOICE_HEAD.Total_Amt as [Last Year Actual Sales] " & _
                   " from  ( " + StrInnerqry + " ) as TSPL_SD_SALE_INVOICE_HEAD " & _
                   " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SD_SALE_INVOICE_HEAD.Customer_Code  " & _
                    " left outer join TSPL_TERMS_MASTER on TSPL_TERMS_MASTER.Terms_Code=TSPL_CUSTOMER_MASTER.Terms_Code " & _
                    " left outer join TSPL_LOCATION_MASTER ON TSPL_LOCATION_MASTER.Location_Code=TSPL_SD_SALE_INVOICE_HEAD.Bill_To_Location " & _
                    " left outer join TSPL_STATE_MASTER ON TSPL_STATE_MASTER.STATE_CODE=TSPL_CUSTOMER_MASTER.State " & _
                    "  left outer join TSPL_CITY_MASTER ON TSPL_CITY_MASTER.City_Code=TSPL_CUSTOMER_MASTER.City_Code  "
            If clsCommon.myLen(strCategoryTable) > 0 Then
                StrQuery += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=TSPL_SD_SALE_INVOICE_HEAD.Item_Code"
            End If
            StrQuery += " where 1=1 and CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) >=convert(date,'" + From_Lastfy + "',103) AND CONVERT(date,TSPL_SD_SALE_INVOICE_HEAD.Document_Date,103) <=convert(date,'" + To_Lastfy + "',103) ) as dddd Where 1=1 "
            ' " GROUP BY TSPL_SD_SALE_INVOICE_HEAD.Customer_Code) dddd " ' ,DATEPART(yyyy,TSPL_SD_SALE_INVOICE_HEAD.Document_Date) ) dddd "


            If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
                StrQuery += " and dddd.Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
            End If
            If txtCustomer.arrValueMember IsNot Nothing AndAlso txtCustomer.arrValueMember.Count > 0 Then
                StrQuery += " and dddd.Cust_Code in (" + clsCommon.GetMulcallString(txtCustomer.arrValueMember) + ")" + Environment.NewLine
            End If
            StrQuery += "  group by Cust_Code, dddd .CA ,dddd.COUNTRY,dddd.STATE,dddd.CITY,dddd.[Location],dddd.Item_Code," + NoMaxposition + " "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
            If dt.Rows.Count > 0 Then
                gv1.DataSource = dt
                gv1.ShowRowHeaderColumn = False
                gv1.AllowAddNewRow = False
                gv1.AllowDeleteRow = False
                gv1.ReadOnly = True
                gv1.BestFitColumns()
                ReStoreGridLayout()
                'gv1.Columns.Width = 100
                'gv1.Columns.WrapText = True
                'gv1.Columns.ReadOnly = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim CMAS As New GridViewSummaryItem("Current Month Actual Sales", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(CMAS)
                Dim LMAC As New GridViewSummaryItem("Last Month Actual Sales", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(LMAC)
                Dim CYAS As New GridViewSummaryItem("Current Year Actual Sales", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(CYAS)
                Dim LYAC As New GridViewSummaryItem("Last Year Actual Sales", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(LYAC)
                gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
            End If
            RadPageView1.SelectedPage = RadPageViewPage2

        Catch ex As Exception

            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)

        End Try

    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            LoadData(0)

            Dim arrHeader As List(Of String) = New List(Of String)()
            '  arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Category Analysis Report", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Category Analysis Report", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click

        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmCategoryAnalysisReport & "'"))

            'Dim sfd As SaveFileDialog = New SaveFileDialog()
            'Dim filePath As String
            'sfd.FileName = Me.Text
            'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
            'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            '    filePath = sfd.FileName
            'Else
            '    Exit Sub
            'End If
            'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
            'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
            'Process.Start(filePath)
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmCategoryAnalysisReport & "'"))

            If txtCustomer.arrDispalyMember IsNot Nothing AndAlso txtCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomer.arrDispalyMember))
            End If
            If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            End If
            If rbtnCategorySelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvCategory.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Category : " + strLoca)
            End If


            clsCommon.MyExportToPDF(Me.Text, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        Reset()
    End Sub

    Sub Reset()
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        fromDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.arrValueMember = Nothing
        txtTransaction.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        UnCheckedAll(gvCategory)
        gv1.MasterTemplate.SummaryRowsBottom.Clear()

        RadPageViewPage2.Text = "Report"

    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)

        txtTransaction.Enabled = val

    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        End If
    End Sub
    Private Sub TreeView_NodeCheckedChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.TreeNodeCheckedEventArgs)
        TreeCheckBoxes(e.Node, e.Node.Checked)
    End Sub

    Public Sub TreeCheckBoxes(ByVal CurrentNode As RadTreeNode, ByVal val As Boolean)
        For Each Ctr As RadTreeNode In CurrentNode.Nodes
            Ctr.Checked = val
            TreeCheckBoxes(Ctr, val)
        Next
    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " select Code,Name,InOutType as [In/Out Type],Type from TSPL_INVENTORY_SOURCE_CODE where 2=2 "

        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSe", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Private Sub RadButton8_Click(sender As Object, e As EventArgs)
        Try
            LoadData(2)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Shared Sub ExportBulkData(ByVal qry As String, ByVal arrVisibleColumAndCaption As Dictionary(Of String, String), ByVal strReportNameInSaveDialog As String)
        'clsCommon.ProgressBarPercentShow()
        clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            If arrVisibleColumAndCaption Is Nothing OrElse arrVisibleColumAndCaption.Count <= 0 Then
                Throw New Exception("Please provice column and caption for export")
            End If

            Dim rowsPerSheet As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.QuickExport, clsFixedParameterCode.MaxRowsForQuickExport, Nothing))

            Dim FilePath As String = "C:\ERPTempFolder"
            Dim IsExists As Boolean = System.IO.Directory.Exists(FilePath)
            If Not IsExists Then
                System.IO.Directory.CreateDirectory(FilePath)
            End If
            strReportNameInSaveDialog += clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss")
            FilePath = "C:\ERPTempFolder\" + strReportNameInSaveDialog.Replace("/", "_").Replace("\\", "_") + ".xlsx"

            Dim intTotalRows As Integer = 0
            Dim intSheetCounter As Integer = 1
            Dim intReaderCounter As Integer = 0
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            Dim ResultsData As DataTable = Nothing
            Dim c As Integer = 0
            Dim firstTime As Boolean = True

            'Get the Columns names, types, this will help when we need to format the cells in the excel sheet.
            Dim dtSchema As DataTable = reader.GetSchemaTable()
            'Dim listCols = New List(Of DataColumn)()

            If dtSchema IsNot Nothing Then
                ResultsData = New DataTable()
                For Each drow As DataRow In dtSchema.Rows
                    Dim columnName As String = clsCommon.myCstr(drow("ColumnName"))
                    If arrVisibleColumAndCaption.ContainsKey(columnName) Then
                        Dim column = New DataColumn(columnName, DirectCast(drow("DataType"), Type))
                        column.Unique = CBool(drow("IsUnique"))
                        column.AllowDBNull = CBool(drow("AllowDBNull"))
                        column.AutoIncrement = CBool(drow("IsAutoIncrement"))
                        column.Caption = arrVisibleColumAndCaption(columnName)
                        'listCols.Add(column)
                        ResultsData.Columns.Add(column)
                    End If
                Next
            End If
            Dim rowData(rowsPerSheet, ResultsData.Columns.Count) As Object
            Dim workBook As Microsoft.Office.Interop.Excel.Workbook = Nothing

            While reader.Read()
                intReaderCounter += 1
                clsCommon.ProgressBarUpdate("Fatching Data for Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                For i As Integer = 0 To ResultsData.Columns.Count - 1
                    rowData(c, i) = reader(ResultsData.Columns(i).ColumnName)
                Next
                c += 1
                If c = rowsPerSheet Then
                    clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                    workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
                    c = 0
                    ResultsData.Clear()
                    firstTime = False
                    intSheetCounter += 1
                End If
            End While

            If c <> 0 Then

                clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
            End If

            workBook = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()

            If intReaderCounter > 0 Then
                clsCommon.ProgressBarUpdate("Data exported.Opening File " + FilePath + ".Please wait...")
                Process.Start(FilePath)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Shared Function ExportToOxml(ByVal intSheetNo As Integer, ByVal firstTime As Boolean, ByVal RowsToWrite As Integer, ByVal Schema As DataTable, ByVal rawData(,) As Object, ByVal FilePath As String, ByRef wbook As Microsoft.Office.Interop.Excel.Workbook) As Microsoft.Office.Interop.Excel.Workbook
        Try
            Dim dt As New System.Data.DataTable()
            For i As Integer = 0 To Schema.Columns.Count - 1
                dt.Columns.Add("Column" & (i + 1))
                If clsCommon.myLen(Schema.Columns(i).Caption) > 0 Then
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).Caption
                Else
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).ColumnName
                End If
            Next

            Dim excel As New Microsoft.Office.Interop.Excel.Application
            If wbook Is Nothing Then
                Dim wBook1 As Microsoft.Office.Interop.Excel.Workbook = Nothing
                wbook = wBook1
                wbook = excel.Workbooks.Add()
            Else
                wbook = excel.Workbooks.Open(FilePath)
            End If
            Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
            Dim GridCurrentRowIndex As Int64 = -1
            Dim GridLastSavedRowIndex As Int64 = -1
            wSheet = wbook.Sheets.Add(, , 1)
            wbook.ActiveSheet.Move(After:=wbook.Sheets(wbook.Sheets.Count))
            If firstTime Then
                Try
                    CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet2"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet3"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                Catch ex As Exception
                End Try
            End If
            wSheet.Name = "Sheet" & intSheetNo

            Dim jk As Integer = 0
            For i As Integer = 0 To Schema.Columns.Count - 1
                jk += 1
                Dim MyType As TypeCode = Type.GetTypeCode(Schema.Columns(i).DataType)
                If MyType = TypeCode.String Then
                    wSheet.Range(ColumnIndexToColumnLetter(jk) & ":" & ColumnIndexToColumnLetter(jk)).Cells.NumberFormat = "@"
                End If
            Next

            Dim colnum As Integer = -1
            Dim PrevCol As Integer = -1
            Dim ColNums(0 To Schema.Columns.Count - 1) As Integer

            Dim colIndex As Integer = 1
            Dim rowIndex As Integer = 1

            Dim dc As System.Data.DataColumn
            colIndex = 0
            For Each dc In Schema.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex, colIndex) = dc.Caption
            Next

            Dim LastColumn As String = ColumnIndexToColumnLetter(Schema.Columns.Count)
            Dim Lastrow As Integer = RowsToWrite

            Dim row As Integer = 0
            Dim col As Integer = 0

            wSheet.Range("A2", LastColumn & (Lastrow + 1)).Value2 = rawData
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            wSheet.Columns.AutoFit()
            CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Select()
            excel.DisplayAlerts = False
            wbook.SaveAs(FilePath, , , , , , Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive)
            wbook.Close(True)

            excel.Quit()
            excel = Nothing
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return wbook
    End Function

    Private Shared Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = [String].Empty
        Dim [mod] As Integer = 0
        While div > 0
            [mod] = (div - 1) Mod 26
            colLetter = (Convert.ToChar(65 + [mod])).ToString & colLetter
            div = CInt((div - [mod]) / 26)
        End While
        Return colLetter
    End Function


    Private Sub LoadDataInGridViaDataReader(ByVal qry As String)
        'clsCommon.ProgressBarPercentShow()
        'clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            If reader Is Nothing OrElse Not reader.HasRows Then
                Throw New Exception("No Data found")
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim dtTest As New DataTable
            dtTest.Load(reader)
            gv1.DataSource = dtTest
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Sub txtCustomer__My_Click(sender As Object, e As EventArgs) Handles txtCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSel", qry, "Code", "Name", txtCustomer.arrValueMember, txtCustomer.arrDispalyMember)
    End Sub

    Private Sub BtnGo_Click(sender As Object, e As EventArgs) Handles BtnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            gv1.EnableFiltering = True
            LoadData(0)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

  
    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub
    Private Sub rbtnCategoryAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged
        If rbtnCategoryAll.IsChecked = True Then
            CheckedAll(gvCategory)
            gvCategory.Enabled = False
        Else
            UnCheckedAll(gvCategory)
            gvCategory.Enabled = True
        End If
    End Sub

    Private Sub rbtnCategorySelect_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnCategorySelect.ToggleStateChanged
        If rbtnCategoryAll.IsChecked = True Then
            CheckedAll(gvCategory)
            gvCategory.Enabled = False
        Else
            UnCheckedAll(gvCategory)
            gvCategory.Enabled = True
        End If
    End Sub

    Private Sub RadMenuItemSett1_Click(sender As Object, e As EventArgs) Handles RadMenuItemSett1.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub
End Class
