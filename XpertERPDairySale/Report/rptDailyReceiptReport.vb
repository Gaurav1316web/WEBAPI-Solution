Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
''Check in richa 22/06/20202
'' created by richa Agarwal on 15 Jan,2020
Public Class rptDailyreceiptReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim ReportID As String = ""
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub rptDailyreceiptReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        RadSplitExp.Items("rmiExcelSale").Visibility = ElementVisibility.Collapsed
        RadSplitExp.Items("rmiPDFSale").Visibility = ElementVisibility.Collapsed
        RadSplitExp.Items("rmiExcelCollection").Visibility = ElementVisibility.Collapsed
        RadSplitExp.Items("rmiPDFCollection").Visibility = ElementVisibility.Collapsed
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtLocation.arrValueMember = Nothing
        txtMultiCustomer.arrValueMember = Nothing
        txtUser.arrValueMember = Nothing
        cmbCustomerType.Text = "Select"
        cmbInstitutionType.Text = "Select"
        cmbInstitutionType.Enabled = False
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        gvCS_CD.DataSource = Nothing
        gvCS_CD.Rows.Clear()
        gvCS_CD.Columns.Clear()
        gv_Sale.DataSource = Nothing
        gv_Sale.Rows.Clear()
        gv_Sale.Columns.Clear()
        gv_Collection.DataSource = Nothing
        gv_Collection.Rows.Clear()
        gv_Collection.Columns.Clear()
        RadSplitExp.Items("rmiExcelSale").Visibility = ElementVisibility.Collapsed
        RadSplitExp.Items("rmiPDFSale").Visibility = ElementVisibility.Collapsed
        RadSplitExp.Items("rmiExcelCollection").Visibility = ElementVisibility.Collapsed
        RadSplitExp.Items("rmiPDFCollection").Visibility = ElementVisibility.Collapsed
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + clsCommon.myCstr(IIf(chkZoneWisePayment.Checked = True, "Z", "")) + clsCommon.myCstr(IIf(chkZoneWiseSuppliesRemittence.Checked = True, "SR", "")) + clsCommon.myCstr(IIf(chkSaleandRealisationDetail.Checked = True, "SRD", "")) + clsCommon.myCstr(IIf(chkCardSaleCollectionDetail.Checked = True, "CDC", ""))

            If chkZoneWisePayment.Checked = True OrElse chkZoneWiseSuppliesRemittence.Checked = True OrElse chkSaleandRealisationDetail.Checked = True OrElse chkCardSaleCollectionDetail.Checked = True Then
                ReportID = ""
                TemplateGridview = Nothing
            Else
                ReportID = MyBase.Form_ID
                TemplateGridview = Gv1
            End If

            'One checkbox checked at a time
            Dim IntCheckBoxCheckedCount As Integer = 0
            If chkZoneWisePayment.Checked = True Then
                IntCheckBoxCheckedCount += 1
            End If
            If chkZoneWiseSuppliesRemittence.Checked = True Then
                IntCheckBoxCheckedCount += 1
            End If
            If chkSaleandRealisationDetail.Checked = True Then
                IntCheckBoxCheckedCount += 1
            End If
            If chkCardSaleCollectionDetail.Checked = True Then
                IntCheckBoxCheckedCount += 1
            End If
            If IntCheckBoxCheckedCount > 1 Then
                Throw New Exception("Select only one check box at a time Zone Wise Payment / Zone Wise Supplies and Remittence / Sale and Realisation Detail / Card Sale and Collection Detail ")
            End If

            Dim strWhere As String = ""
            Dim strUserWhere As String = ""

            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                strWhere += " and tspl_customer_master.Cust_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")"
            End If

            Dim chkCustCategoryMappInUserMaster As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count ( distinct CUSTOMER_CATEGORY) as CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')"))
            If chkCustCategoryMappInUserMaster = True Then
                strWhere += " and TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (  select  distinct CUSTOMER_CATEGORY from TSPL_CUSTOMER_MASTER where TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY in (select Customer_Category from TSPL_USER_CUSTOMER_CATEGORY where USER_Code = '" + objCommonVar.CurrentUserCode + "')) "
            End If

            If txtUser.arrValueMember IsNot Nothing AndAlso txtUser.arrValueMember.Count > 0 Then
                strUserWhere += " and TSPL_RECEIPT_HEADER.Created_By in(" + clsCommon.GetMulcallString(txtUser.arrValueMember) + ")"
            End If

            If clsCommon.CompairString(cmbCustomerType.Text, "LMS") = CompairStringResult.Equal Then
                strWhere += " and ISNULL(TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE ,'')='LMS' "
                If clsCommon.CompairString(cmbInstitutionType.Text, "Institution SO") = CompairStringResult.Equal Then
                    strWhere += " and ISNULL(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY ,'')='Institution SO' "
                ElseIf clsCommon.CompairString(cmbInstitutionType.Text, "Institution CR") = CompairStringResult.Equal Then
                    strWhere += " and ISNULL(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY ,'')='Institution CR' "
                    'Else
                    '    qry += " and ISNULL(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY ,'')<>'Others' "
                End If
            ElseIf clsCommon.CompairString(cmbCustomerType.Text, "Marketing") = CompairStringResult.Equal Then
                strWhere += " and ISNULL(TSPL_CUSTOMER_MASTER.CUST_CATEGORY_CODE ,'')='MKT' "
                strWhere += " and ISNULL(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY ,'')='Others' "
            End If

            If clsCommon.CompairString(cmbCustomerType.Text, "Select") = CompairStringResult.Equal Then
                If clsCommon.CompairString(cmbInstitutionType.Text, "Institution SO") = CompairStringResult.Equal Then
                    strWhere += " and ISNULL(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY ,'')='Institution SO' "
                ElseIf clsCommon.CompairString(cmbInstitutionType.Text, "Institution CR") = CompairStringResult.Equal Then
                    strWhere += " and ISNULL(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY ,'')='Institution CR' "
                End If
            End If



            Dim qry As String = ""
            Dim dt As New DataTable
            Dim strPayment As String = clsDBFuncationality.getSingleValue("  Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) Select   STUFF((Select distinct ',' + QUOTENAME(TSPL_PAYMENT_CODE.Payment_Desc ) as Alies_Name FROM TSPL_PAYMENT_CODE order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strPaymentSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME(TSPL_PAYMENT_CODE.Payment_Desc) +',0))' +' as ' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc) as Alies_Name  FROM TSPL_PAYMENT_CODE order by Alies_Name  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
            Dim strTotaPayment As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME(TSPL_PAYMENT_CODE.Payment_Desc) +',0))'  as Alies_Name  FROM TSPL_PAYMENT_CODE order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

            gvCS_CD.DataSource = Nothing
            gvCS_CD.Rows.Clear()
            gvCS_CD.Columns.Clear()
            gvCS_CD.GroupDescriptors.Clear()
            gvCS_CD.MasterTemplate.SummaryRowsBottom.Clear()
            gvCS_CD.MasterView.Refresh()

            gv_Sale.DataSource = Nothing
            gv_Sale.Rows.Clear()
            gv_Sale.Columns.Clear()
            gv_Sale.GroupDescriptors.Clear()
            gv_Sale.MasterTemplate.SummaryRowsBottom.Clear()
            gv_Sale.MasterView.Refresh()

            gv_Collection.DataSource = Nothing
            gv_Collection.Rows.Clear()
            gv_Collection.Columns.Clear()
            gv_Collection.GroupDescriptors.Clear()
            gv_Collection.MasterTemplate.SummaryRowsBottom.Clear()
            gv_Collection.MasterView.Refresh()

            Gv1.DataSource = Nothing
            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()
            Gv1.MasterView.Refresh()

            If chkCardSaleCollectionDetail.Checked = True Then
                Dim dtSpell1 As New DataTable
                Dim dtSpell2 As New DataTable
                Dim qrySpell1 As String
                Dim qrySpell2 As String
                If Month(fromDate.Value) <> Month(ToDate.Value) Then
                    clsCommon.MyMessageBoxShow(Me, "From Date to To Date month must be same for Spell Wise Report.", Me.Text)
                    Exit Sub
                End If
                Dim MonthStartDate As Date = New DateTime(Year(fromDate.Value), Month(fromDate.Value), 1)
                Dim MonthEndDate As Date = clsDBFuncationality.getSingleValue("select EOMONTH(convert(date,'" + MonthStartDate + "',103))")
                Dim ItemInUse As String = ""
                ItemInUse = " TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code where (TSPL_ITEM_MASTER.Alies_Name !='' or TSPL_ITEM_MASTER.Alies_Name is null) "
                ItemInUse += " and Scheme_Item='N' And Convert(Date, TSPL_BOOKING_MATSER.Created_Date,103) >= convert(date,'" + MonthStartDate + "',103) and  convert(date, TSPL_BOOKING_MATSER.Created_Date,103) <=  convert(date,'" + MonthEndDate + "',103) "
                ItemInUse += " and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH' and TSPL_BOOKING_MATSER.booking_type='CD' "
                ItemInUse += strWhere
                ItemInUse += " order by Alies_Name "

                Dim strItem2WithSum As String = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) +',0))' + ' as ' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "    FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                Dim strItem2 As String = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( TSPL_ITEM_MASTER.Alies_Name) as Alies_Name FROM " + ItemInUse + "   FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")

                If String.IsNullOrEmpty(strItem2) Then
                    clsCommon.MyMessageBoxShow(Me, "No Item Found to Display", Me.Text)
                    Exit Sub
                End If

                If String.IsNullOrEmpty(strPayment) Then
                    clsCommon.MyMessageBoxShow(Me, "No Payment Code Found to Display", Me.Text)
                    Exit Sub
                End If
                ',Created_Date as Document_Date
                qrySpell1 = "select Convert(varchar,Created_Date,103) as [Date]," + strItem2WithSum + ",sum(AdvanceAmount) as [Total Amount]" &
                    "," + strPaymentSum + ",(" + strTotaPayment + ") as [Total] from ( " &
                    " select item_code,Created_Date,zzz.Description,sum(qty) as qty,sum(AdvanceAmount) as AdvanceAmount " &
                      " ,sum(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Amount ,0)) AS RemittedAmount ,TSPL_BOOKING_PAYMENT_MODE_DETAIL.Payment_Mode " &
                     " from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (date,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_DETAIL.Scheme_Item " &
                     " , TSPL_BOOKING_DETAIL.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description]  , TSPL_BOOKING_DETAIL.Booking_Qty as Qty " &
                      " , isnull(TSPL_BOOKING_MATSER .AdvanceAmount,0) as AdvanceAmount   " &
                     " From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No  " &
                      " Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  " &
                      "  left join tspl_Card_Sale on tspl_Card_Sale.Card_No=TSPL_BOOKING_MATSER.card_sale_no " &
                        " Where  tspl_Card_Sale.isFirstSpell=1  " + strWhere + " And Convert(Date, TSPL_BOOKING_MATSER.Created_Date,103) >= convert(date,'" + MonthStartDate + "',103) and  convert(date, TSPL_BOOKING_MATSER.Created_Date,103) <=  convert(date,'" + MonthEndDate + "',103)   and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH' and TSPL_BOOKING_MATSER.booking_type='CD' )zzz  " &
                     " Left OUTER Join TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No And isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>''  " &
                    " Where zzz.Scheme_Item ='N' group by zzz.Created_Date,zzz.item_code " &
                     " ,zzz.Description,TSPL_BOOKING_PAYMENT_MODE_DETAIL.Payment_Mode  " &
                     " ) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot pivot (  sum(RemittedAmount) for Payment_Mode in (" + strPayment + " ) ) as zpivot1  group by zpivot1.Created_Date order by Convert(date,zpivot1.Created_Date,103)  "

                qrySpell2 = "select Convert(varchar,Created_Date,103) as [Date]," + strItem2WithSum + ",sum(AdvanceAmount) as [Total Amount]" &
                   "," + strPaymentSum + ",(" + strTotaPayment + ") as [Total] from ( " &
                   " select item_code,Created_Date,zzz.Description,sum(qty) as qty,sum(AdvanceAmount) as AdvanceAmount " &
                     " ,sum(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Amount ,0)) AS RemittedAmount ,TSPL_BOOKING_PAYMENT_MODE_DETAIL.Payment_Mode " &
                    " from  (Select TSPL_BOOKING_MATSER.Document_No, Convert (date,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date,TSPL_BOOKING_DETAIL.Scheme_Item " &
                    " , TSPL_BOOKING_DETAIL.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description]  , TSPL_BOOKING_DETAIL.Booking_Qty as Qty " &
                     " , isnull(TSPL_BOOKING_MATSER .AdvanceAmount,0) as AdvanceAmount   " &
                    " From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No  " &
                     " Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  " &
                     "  left join tspl_Card_Sale on tspl_Card_Sale.Card_No=TSPL_BOOKING_MATSER.card_sale_no " &
                       " Where  tspl_Card_Sale.isSecondSpell=1  " + strWhere + " And Convert(Date, TSPL_BOOKING_MATSER.Created_Date,103) >= convert(date,'" + MonthStartDate + "',103) and  convert(date, TSPL_BOOKING_MATSER.Created_Date,103) <=  convert(date,'" + MonthEndDate + "',103)   and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH' and TSPL_BOOKING_MATSER.booking_type='CD' )zzz  " &
                    " Left OUTER Join TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No And isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>''  " &
                   " Where zzz.Scheme_Item ='N' group by zzz.Created_Date,zzz.item_code " &
                    " ,zzz.Description,TSPL_BOOKING_PAYMENT_MODE_DETAIL.Payment_Mode  " &
                    " ) as s pivot (  sum(Qty) for Description in ( " + strItem2 + " ) ) as zpivot pivot (  sum(RemittedAmount) for Payment_Mode in (" + strPayment + " ) ) as zpivot1  group by zpivot1.Created_Date order by Convert(date,zpivot1.Created_Date,103)  "

                dtSpell1 = clsDBFuncationality.GetDataTable(qrySpell1)
                dtSpell2 = clsDBFuncationality.GetDataTable(qrySpell2)

                If (dtSpell1 IsNot Nothing AndAlso dtSpell1.Rows.Count > 0) OrElse (dtSpell2 IsNot Nothing AndAlso dtSpell2.Rows.Count > 0) Then
                    'Dim Spell1StartDate As Date = New DateTime(Year(fromDate.Value), Month(fromDate.Value), 1)
                    'Dim Spell1EndDate As Date = New DateTime(Year(fromDate.Value), Month(fromDate.Value), 10)
                    Dim dtData As DataTable = dtSpell1.Copy()
                    dtData.Merge(dtSpell2, True, MissingSchemaAction.Ignore)
                    Dim dtResult As DataTable = dtSpell1.Clone()

                    Dim RowRate As DataRow = dtResult.NewRow
                    RowRate("Date") = clsCommon.myCstr("Rate (Rs)")
                    dtResult.Rows.Add(RowRate)

                    '''''Spell 1 Data
                    Dim RowSpell1 As DataRow = dtResult.NewRow
                    RowSpell1("Date") = clsCommon.myCstr("1st Spell")
                    dtResult.Rows.Add(RowSpell1)

                    'Dim dtSpell1Data As DataTable = Nothing
                    'Dim dr1 As DataRow() = dtData.Select(" Document_Date>=#" + Spell1StartDate.ToString("yyyy-MM-dd") + "# and Document_Date<=#" + Spell1EndDate.ToString("yyyy-MM-dd") + "#")
                    'If dr1 IsNot Nothing AndAlso dr1.Length > 0 Then
                    '    dtSpell1Data = dr1.CopyToDataTable()
                    '    dtResult.Merge(dtSpell1Data, True, MissingSchemaAction.Ignore)

                    '    'Column wise total
                    '    Dim RowTotal1 As DataRow = dtResult.NewRow
                    '    RowTotal1("Document Date") = clsCommon.myCstr("Total")
                    '    For j As Integer = 2 To dtSpell1Data.Columns.Count - 1
                    '        Dim TempColTotal As Double = clsCommon.myCdbl(dtSpell1Data.Compute("SUM([" + dtSpell1Data.Columns(j).ColumnName.ToString() + "])", ""))
                    '        RowTotal1(dtSpell1Data.Columns(j).ColumnName.ToString()) = TempColTotal
                    '    Next
                    '    dtResult.Rows.Add(RowTotal1)

                    'End If
                    If (dtSpell1 IsNot Nothing AndAlso dtSpell1.Rows.Count > 0) Then
                        dtResult.Merge(dtSpell1, True, MissingSchemaAction.Ignore)
                        'Column wise total
                        Dim RowTotal1 As DataRow = dtResult.NewRow
                        RowTotal1("Date") = clsCommon.myCstr("Total")
                        For j As Integer = 1 To dtSpell1.Columns.Count - 1
                            Dim TempColTotal As Double = clsCommon.myCdbl(dtSpell1.Compute("SUM([" + dtSpell1.Columns(j).ColumnName.ToString() + "])", ""))
                            RowTotal1(dtSpell1.Columns(j).ColumnName.ToString()) = TempColTotal
                        Next
                        dtResult.Rows.Add(RowTotal1)
                    End If


                    'Spell 2 Data
                    Dim RowSpell2 As DataRow = dtResult.NewRow
                    RowSpell2("Date") = clsCommon.myCstr("2st Spell")
                    dtResult.Rows.Add(RowSpell2)

                    'Dim dtSpell2Data As DataTable = Nothing
                    'Dim dr2 As DataRow() = dtData.Select(" Document_Date>#" + Spell1EndDate.ToString("yyyy-MM-dd") + "#")
                    'If dr2 IsNot Nothing AndAlso dr2.Length > 0 Then
                    '    dtSpell2Data = dr2.CopyToDataTable()
                    '    dtResult.Merge(dtSpell2Data, True, MissingSchemaAction.Ignore)

                    '    'Column wise total
                    '    Dim RowTotal2 As DataRow = dtResult.NewRow
                    '    RowTotal2("Document Date") = clsCommon.myCstr("Total")
                    '    For j As Integer = 2 To dtSpell2Data.Columns.Count - 1
                    '        Dim TempColTotal As Double = clsCommon.myCdbl(dtSpell2Data.Compute("SUM([" + dtSpell2Data.Columns(j).ColumnName.ToString() + "])", ""))
                    '        RowTotal2(dtSpell2Data.Columns(j).ColumnName.ToString()) = TempColTotal
                    '    Next
                    '    dtResult.Rows.Add(RowTotal2)

                    'End If
                    If (dtSpell2 IsNot Nothing AndAlso dtSpell2.Rows.Count > 0) Then
                        dtResult.Merge(dtSpell2, True, MissingSchemaAction.Ignore)
                        'Column wise total
                        Dim RowTotal1 As DataRow = dtResult.NewRow
                        RowTotal1("Date") = clsCommon.myCstr("Total")
                        For j As Integer = 1 To dtSpell2.Columns.Count - 1
                            Dim TempColTotal As Double = clsCommon.myCdbl(dtSpell2.Compute("SUM([" + dtSpell2.Columns(j).ColumnName.ToString() + "])", ""))
                            RowTotal1(dtSpell2.Columns(j).ColumnName.ToString()) = TempColTotal
                        Next
                        dtResult.Rows.Add(RowTotal1)
                    End If

                    'Grand Total
                    Dim GrandTotal As DataRow = dtResult.NewRow
                    GrandTotal("Date") = "G.Total"

                    For j As Integer = 1 To dtData.Columns.Count - 1
                        Dim TempColTotal As Double = clsCommon.myCdbl(dtData.Compute("SUM([" + dtData.Columns(j).ColumnName.ToString() + "])", ""))
                        GrandTotal(dtData.Columns(j).ColumnName.ToString()) = TempColTotal
                    Next
                    dtResult.Rows.Add(GrandTotal)

                    'dtResult.Columns.Remove("Document_Date")

                    dt = Nothing
                    dt = dtResult.Copy()

                    'Gv1.DataSource = dt
                    'For ii As Integer = 0 To Gv1.Columns.Count - 1
                    '    Gv1.Columns(ii).ReadOnly = True
                    'Next
                    'RadPageView1.SelectedPage = RadPageViewPage3
                    'Gv1.EnableFiltering = True
                    'Gv1.BestFitColumns()
                    gvCS_CD.DataSource = dt
                    'gv_Sale.DataSource = dt
                    'gv_Collection.DataSource = dt
                    For ii As Integer = 0 To gvCS_CD.Columns.Count - 1
                        gvCS_CD.Columns(ii).ReadOnly = True
                    Next

                    gvCS_CD.EnableFiltering = True
                    gvCS_CD.BestFitColumns()

                    'Card Sale Abstract'''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim dtCDAbstract As New DataTable
                    Dim QryCDAbstract As String = ""
                    QryCDAbstract = " select zzz.Description as [Type of Milk],sum(qty) as Quantity,max(Item_Rate) as Rate,sum(AdvanceAmount) as Amount " &
                    " from  (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Scheme_Item " &
                    " , TSPL_BOOKING_DETAIL.Item_Code as Item_Code, TSPL_ITEM_MASTER.Alies_Name As [Description] ,TSPL_BOOKING_DETAIL.Item_Rate , TSPL_BOOKING_DETAIL.Booking_Qty as Qty " &
                     " , isnull(TSPL_BOOKING_MATSER .AdvanceAmount,0) as AdvanceAmount   " &
                    " From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No  " &
                     " Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  " &
                     "  left join tspl_Card_Sale on tspl_Card_Sale.Card_No=TSPL_BOOKING_MATSER.card_sale_no " &
                       " Where  1=1  " + strWhere + " And Convert(Date, TSPL_BOOKING_MATSER.Created_Date,103) >= convert(date,'" + MonthStartDate + "',103) and  convert(date, TSPL_BOOKING_MATSER.Created_Date,103) <=  convert(date,'" + MonthEndDate + "',103)   and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH' and TSPL_BOOKING_MATSER.booking_type='CD' )zzz  " &
                    " Left OUTER Join TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No And isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>''  " &
                   " Where zzz.Scheme_Item ='N' group by zzz.Description "

                    dtCDAbstract = clsDBFuncationality.GetDataTable(QryCDAbstract)
                    If (dtCDAbstract IsNot Nothing AndAlso dtCDAbstract.Rows.Count > 0) Then
                        gv_Sale.DataSource = dtCDAbstract
                        For ii As Integer = 0 To gv_Sale.Columns.Count - 1
                            gv_Sale.Columns(ii).ReadOnly = True
                        Next
                        gv_Sale.EnableFiltering = True
                        Dim summaryRowItem As New GridViewSummaryRowItem()
                        Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Amount)
                        Dim Quantity As New GridViewSummaryItem("Quantity", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Quantity)
                        gv_Sale.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                        gv_Sale.BestFitColumns()
                    End If


                    'Collection Abstract'''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim dtCollectionAbstract As New DataTable
                    Dim QryCollectionAbstract As String = ""

                    QryCollectionAbstract = "select ROW_NUMBER () over (order by zzz.Created_Date) As SNo, Convert(varchar,zzz.Created_Date,103) as Date " &
                        ",zzz.cust_code as [Booth],sum(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.Amount ,0)) AS Amount,max(tspl_payment_code.Payment_Desc) as [Payment Mode]" &
                        " from TSPL_BOOKING_PAYMENT_MODE_DETAIL  Left OUTER Join  (Select TSPL_BOOKING_DETAIL.cust_code,TSPL_BOOKING_MATSER.Document_No, Convert (date,TSPL_BOOKING_MATSER.Created_Date,103) as Created_Date " &
                        " ,TSPL_BOOKING_DETAIL.Scheme_Item, TSPL_BOOKING_DETAIL.Item_Code as Item_Code From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No   Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code    left join tspl_Card_Sale on tspl_Card_Sale.Card_No=TSPL_BOOKING_MATSER.card_sale_no   " &
                          " Where  1=1  " + strWhere + " And Convert(Date, TSPL_BOOKING_MATSER.Created_Date,103) >= convert(date,'" + MonthStartDate + "',103) and  convert(date, TSPL_BOOKING_MATSER.Created_Date,103) <=  convert(date,'" + MonthEndDate + "',103)   and isnull(TSPL_BOOKING_MATSER .Against_Booking_No ,'')='' and TSPL_BOOKING_MATSER.From_Screen_Code ='BOOK-DS_FSH' and TSPL_BOOKING_MATSER.booking_type='CD' )zzz  " &
                        "  ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No And isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>''  " &
                         " left join tspl_payment_code on tspl_payment_code.payment_code=TSPL_BOOKING_PAYMENT_MODE_DETAIL.payment_mode" &
                        " Where zzz.Scheme_Item ='N' group by zzz.Created_Date,zzz.cust_code ,TSPL_BOOKING_PAYMENT_MODE_DETAIL.Payment_Mode"

                    dtCollectionAbstract = clsDBFuncationality.GetDataTable(QryCollectionAbstract)
                    If (dtCollectionAbstract IsNot Nothing AndAlso dtCollectionAbstract.Rows.Count > 0) Then
                        gv_Collection.DataSource = dtCollectionAbstract
                        For ii As Integer = 0 To gv_Collection.Columns.Count - 1
                            gv_Collection.Columns(ii).ReadOnly = True
                        Next
                        gv_Collection.EnableFiltering = True
                        Dim summaryRowItem1 As New GridViewSummaryRowItem()
                        Dim Amount1 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem1.Add(Amount1)
                        gv_Collection.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem1)
                        gv_Collection.BestFitColumns()
                    End If

                    RadPageView1.SelectedPage = RadPageViewPage3
                    RadSplitExp.Items("rmiExcelSale").Visibility = ElementVisibility.Visible
                    RadSplitExp.Items("rmiPDFSale").Visibility = ElementVisibility.Visible
                    RadSplitExp.Items("rmiExcelCollection").Visibility = ElementVisibility.Visible
                    RadSplitExp.Items("rmiPDFCollection").Visibility = ElementVisibility.Visible
                    Exit Sub
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                    End If

                ElseIf chkSaleandRealisationDetail.Checked = True Then
                    'strPayment = clsDBFuncationality.getSingleValue("  Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) Select STUFF((Select distinct ',' + QUOTENAME(TSPL_PAYMENT_CODE.Payment_Desc ) as Alies_Name FROM TSPL_PAYMENT_CODE order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    'strPaymentSum = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME(TSPL_PAYMENT_CODE.Payment_Desc) +',0))' +' as ' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc) as Alies_Name  FROM TSPL_PAYMENT_CODE order by Alies_Name  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    'strTotaPayment = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME(TSPL_PAYMENT_CODE.Payment_Desc) +',0))'  as Alies_Name  FROM TSPL_PAYMENT_CODE order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

                    If String.IsNullOrEmpty(strPayment) Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                    End If

                ' qry = "  select ROW_NUMBER () over (order by Booking_Type) As SNo,Booking_Type as Particulars,sum([Total In Ltr]) as [Qty],sum(DocumentAmount) as [Amount]," + strPaymentSum + ",(" + strTotaPayment + ") as [Total]" &
                '  " from (select (case when ss.Booking_Type='FN' then 'Forenoon Sales' when ss.Booking_Type='PS' then 'Parlour Sales' when ss.Booking_Type='CD' then 'Card Sales' when ss.Booking_Type='CASH' then 'Cash Sales' when ss.Booking_Type='SO' then 'Special Order Sales' when ss.Booking_Type='CR' then 'Credit Sales' when ss.Booking_Type='UP' then 'Up Country Sales' else ss.Booking_Type end) as Booking_Type, ss.[Total In Ltr],ss.DocumentAmount, rr.Amount, rr.Payment_Desc from (select Cust_Code,Booking_Type " &
                ' " ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr],cast(SUM(DocumentAmount) as decimal(18,2)) as DocumentAmount from " &
                ' " (select max(DocumentAmount) as DocumentAmount,zzz.Cust_Code,zzz.Booking_Type ,sum(QtyLtr) as QtyLtr from " &
                '  " (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item " &
                '" ,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " &
                '     " Left Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   " &
                '      " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code  " &
                '   " where 2=2  And convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + ToDate.Value + "',103)   " + strWhere + " " &
                ' " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No " &
                '  ",zzz.Cust_Code,zzz.Booking_Type " &
                '"  ) as s where 1=1 group by s.Cust_Code,s.Booking_Type)ss " &
                '" Left outer Join  " &
                '" ( select isnull(TSPL_RECEIPT_HEADER.Cust_code,'') as Cust_code ,sum(TSPL_RECEIPT_HEADER.RECEIPT_AMOUNT) AS Amount " &
                ' " ,isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') as Payment_Desc from TSPL_RECEIPT_HEADER  " &
                ' " Left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code  left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code  left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=TSPL_RECEIPT_HEADER.Payment_Code  " &
                ' " where convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>= convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " " &
                '                " Group by isnull(TSPL_RECEIPT_HEADER.Cust_code,''),isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') " &
                '   " ) as rr on rr.Cust_code=ss.Cust_code ) final " &
                '  " pivot (  sum(Amount) for Payment_Desc in (" + strPayment + ") ) As zpivot group by Booking_Type order by  Booking_Type "

                'qry = "  select ROW_NUMBER () over (order by Booking_Type) As SNo,Booking_Type as Particulars,sum([Total In Ltr]) as [Qty],sum(DocumentAmount) as [Amount]," + strPaymentSum + ",(" + strTotaPayment + ") as [Total]" &
                '     " from (select (case when ss.Booking_Type='FN' then 'Forenoon Sales' when ss.Booking_Type='PS' then 'Parlour Sales' when ss.Booking_Type='CD' then 'Card Sales' when ss.Booking_Type='CASH' then 'Cash Sales' when ss.Booking_Type='SO' then 'Special Order Sales' when ss.Booking_Type='CR' then 'Credit Sales' when ss.Booking_Type='UP' then 'Up Country Sales' else ss.Booking_Type end) as Booking_Type, ss.[Total In Ltr],ss.DocumentAmount,(case when ss.Booking_Type='CD' then ss.CD_Receipt else rr.amount end) as Amount, rr.Payment_Desc from (select Cust_Code,Booking_Type " &
                '    " ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr],cast(SUM(DocumentAmount) as decimal(18,2)) as DocumentAmount,sum(CD_Receipt) as CD_Receipt from " &
                '    " (select max(DocumentAmount) as DocumentAmount,zzz.Cust_Code,zzz.Booking_Type ,sum(QtyLtr) as QtyLtr,max(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.amount,0)) as CD_Receipt from " &
                '     " (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item " &
                '    " ,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " &
                '        " Left Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   " &
                '         " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code  " &
                '      " where 2=2  And convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + ToDate.Value + "',103)   " + strWhere + " " &
                '    " )zzz Left OUTER Join TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No And isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>'' and	zzz.Booking_Type='CD' where zzz.Scheme_Item='N' group by zzz.Document_No " &
                '     ",zzz.Cust_Code,zzz.Booking_Type " &
                '    "  ) as s where 1=1 group by s.Cust_Code,s.Booking_Type)ss " &
                '    " Left outer Join  " &
                '    " ( select isnull(TSPL_RECEIPT_HEADER.Cust_code,'') as Cust_code ,sum(TSPL_RECEIPT_HEADER.RECEIPT_AMOUNT) AS Amount " &
                '    " ,isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') as Payment_Desc from TSPL_RECEIPT_HEADER  " &
                '    " Left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code  left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code  left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=TSPL_RECEIPT_HEADER.Payment_Code  " &
                '    " where convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>= convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " " &
                '                   " Group by isnull(TSPL_RECEIPT_HEADER.Cust_code,''),isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') " &
                '      " ) as rr on rr.Cust_code=ss.Cust_code and ss.Booking_Type<>'CD' ) final " &
                '     " pivot (  sum(Amount) for Payment_Desc in (" + strPayment + ") ) As zpivot group by Booking_Type order by  Booking_Type "

                'qry = "  select ROW_NUMBER () over (order by Booking_Type) As SNo,Booking_Type as Particulars,sum([Total In Ltr]) as [Qty],sum(DocumentAmount) as [Amount]," + strPaymentSum + ",(" + strTotaPayment + ") as [Total]" &
                '     " from (select Booking_Type,sum([Total In Ltr]) as [Total In Ltr],sum(DocumentAmount) as DocumentAmount,SUM(amount) as amount,Payment_Desc from (select (case when ss.Booking_Type='FN' then 'Forenoon Sales' when ss.Booking_Type='PS' then 'Parlour Sales' when ss.Booking_Type='CD' then 'Card Sales' when ss.Booking_Type='CASH' then 'Cash Sales' when ss.Booking_Type='SO' then 'Special Order Sales' when ss.Booking_Type='CR' then 'Credit Sales' when ss.Booking_Type='UP' then 'Up Country Sales' else ss.Booking_Type end) as Booking_Type, ss.[Total In Ltr],ss.DocumentAmount,(case when ss.Booking_Type='CD' then ss.CD_Receipt else rr.amount end) as Amount, (case when ss.Booking_Type='CD' then isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') else rr.Payment_Desc end) as Payment_Desc from (select Cust_Code,Booking_Type " &
                '    " ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr],cast(SUM(DocumentAmount) as decimal(18,2)) as DocumentAmount,sum(CD_Receipt) as CD_Receipt,CD_payment_mode from " &
                '    " (select max(DocumentAmount) as DocumentAmount,zzz.Cust_Code,zzz.Booking_Type ,sum(QtyLtr) as QtyLtr,max(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.amount,0)) as CD_Receipt,max(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.payment_mode,'')) as CD_payment_mode from " &
                '     " (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item " &
                '    " ,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " &
                '        " Left Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   " &
                '         " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code  " &
                '      " where 2=2  And convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + ToDate.Value + "',103)   " + strWhere + " " &
                '    " )zzz Left OUTER Join TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =zzz.Document_No And isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>'' and	zzz.Booking_Type='CD' where zzz.Scheme_Item='N' group by zzz.Document_No " &
                '     ",zzz.Cust_Code,zzz.Booking_Type " &
                '    "  ) as s where 1=1 group by s.Cust_Code,s.Booking_Type,CD_payment_mode)ss " &
                '    " left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=ss.CD_payment_mode   " &
                '    " Left outer Join ( select isnull(TSPL_RECEIPT_HEADER.Cust_code,'') as Cust_code ,sum(TSPL_RECEIPT_HEADER.RECEIPT_AMOUNT) AS Amount " &
                '    " ,isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') as Payment_Desc from TSPL_RECEIPT_HEADER  " &
                '    " Left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code  left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code  left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=TSPL_RECEIPT_HEADER.Payment_Code  " &
                '    " where convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>= convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " " &
                '                   " Group by isnull(TSPL_RECEIPT_HEADER.Cust_code,''),isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') " &
                '      " ) as rr on rr.Cust_code=ss.Cust_code and ss.Booking_Type<>'CD' )xx group by Booking_Type,Payment_Desc ) final " &
                '     " pivot (  sum(Amount) for Payment_Desc in (" + strPayment + ") ) As zpivot group by Booking_Type order by  Booking_Type "

                qry = "  select ROW_NUMBER () over (order by Booking_Type) As SNo,Booking_Type as Particulars,sum([Total In Ltr]) as [Qty],sum(DocumentAmount) as [Amount]," + strPaymentSum + ",(" + strTotaPayment + ") as [Total] from ( " &
                  " select (case when BookingData.Booking_Type='FN' then 'Forenoon Sales' when BookingData.Booking_Type='PS' then 'Parlour Sales' when BookingData.Booking_Type='CD' then 'Card Sales' when BookingData.Booking_Type='CASH' then 'Cash Sales' when BookingData.Booking_Type='SO' then 'Special Order Sales' when BookingData.Booking_Type='CR' then 'Credit Sales' when BookingData.Booking_Type='UP' then 'Up Country Sales' else BookingData.Booking_Type end) as Booking_Type " &
                  ",[Total In Ltr],DocumentAmount,Receipt,TSPL_PAYMENT_CODE.payment_desc " &
                  " from ( select Booking_Type  ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr],cast(SUM(DocumentAmount) as decimal(18,2)) as DocumentAmount " &
                " from ( select Document_No,max(DocumentAmount) as DocumentAmount,zzz.Booking_Type ,sum(QtyLtr) as QtyLtr " &
                " from  (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item  ,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  Left Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No    left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code   where TSPL_BOOKING_DETAIL.Scheme_Item='N' and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)>= convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_BOOKING_MATSER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " " &
                  " )zzz  group by zzz.Document_No,zzz.Booking_Type   ) as s where 1=1 group by s.Booking_Type " &
                  " )BookingData " &
                  " left outer join  " &
                  " (select Booking_Type,sum(Receipt) as Receipt,payment_code from (select Booking_Type " &
                  ",sum(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.amount,0)) as Receipt,max(isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.payment_mode,'')) as Payment_Code " &
                  " from (select Document_No,zzz.Booking_Type " &
                  " from  (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_DETAIL.Scheme_Item  From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  Left Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No where TSPL_BOOKING_DETAIL.Scheme_Item='N' and convert(date,TSPL_BOOKING_MATSER.Document_Date,103)>= convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_BOOKING_MATSER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " " &
                  " ) zzz  group by zzz.Document_No ,zzz.Booking_Type   ) as s " &
                " Left OUTER Join TSPL_BOOKING_PAYMENT_MODE_DETAIL ON TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =s.Document_No " &
                   " And isnull( TSPL_BOOKING_PAYMENT_MODE_DETAIL.Against_Receipt_No ,'')<>'' and	s.Booking_Type='CD' " &
                   " where 1=1 group by s.Booking_Type,isnull(TSPL_BOOKING_PAYMENT_MODE_DETAIL.payment_mode,'') " &
                    " union all " &
                " select yy.Booking_Type,sum(isnull(rr.Amount,0)) as Receipt,isnull(rr.Payment_Code,'') as Payment_Code " &
                "  from ( " &
                " select Booking_Type,cust_code from " &
                 " ( select Document_No,zzz.Cust_Code,zzz.Booking_Type " &
                 " from  (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.Cust_Code,TSPL_BOOKING_MATSER.Booking_Type,TSPL_BOOKING_DETAIL.Scheme_Item   " &
                 " From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code  Left Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No where TSPL_BOOKING_DETAIL.Scheme_Item='N' and convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)>= convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_BOOKING_MATSER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " " &
                 " ) zzz group by zzz.Document_No ,zzz.Cust_Code,zzz.Booking_Type   )xx group by Cust_Code,Booking_Type " &
                " )yy Left outer Join ( select isnull(TSPL_RECEIPT_HEADER.Cust_code,'') as Cust_code ,sum(TSPL_RECEIPT_HEADER.RECEIPT_AMOUNT) AS Amount   " &
                    ",TSPL_PAYMENT_CODE.Payment_Code " &
                    " from TSPL_RECEIPT_HEADER   Left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code  left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code  left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=TSPL_RECEIPT_HEADER.Payment_Code  where convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>= convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " Group by isnull(TSPL_RECEIPT_HEADER.Cust_code,''),TSPL_PAYMENT_CODE.Payment_Code " &
                " ) as rr on rr.Cust_code=yy.Cust_code and yy.Booking_Type<>'CD' " &
                    " group by Booking_Type,isnull(Payment_Code,''))ReceiptData group by Booking_Type,payment_code " &
                ")ReceiptDataFinal on BookingData.booking_type=ReceiptDataFinal.Booking_Type " &
                "	left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.payment_code=ReceiptDataFinal.payment_code " &
                " ) final  pivot (  sum(Receipt) for Payment_Desc in (" + strPayment + ") ) As zpivot group by Booking_Type order by Booking_Type "

            ElseIf chkZoneWiseSuppliesRemittence.Checked = True Then
                    'strPayment = clsDBFuncationality.getSingleValue("  DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc ) as Alies_Name FROM TSPL_PAYMENT_CODE order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    'strPaymentHeading = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'isnull(' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc) +',0)' +' as ' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc) as Alies_Name  FROM TSPL_PAYMENT_CODE order by Alies_Name  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    'strTotaPayment = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'isnull(' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc) +',0)'  as Alies_Name  FROM TSPL_PAYMENT_CODE order by Alies_Name  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")

                    'strPaymentSum = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME(TSPL_PAYMENT_CODE.Payment_Desc) +',0))' +' as ' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc) as Alies_Name  FROM TSPL_PAYMENT_CODE order by Alies_Name  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    'strTotaPayment = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct '+' +'Sum(isnull(' + QUOTENAME(TSPL_PAYMENT_CODE.Payment_Desc) +',0))'  as Alies_Name  FROM TSPL_PAYMENT_CODE order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  ")
                    If String.IsNullOrEmpty(strPayment) Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                    End If

                'qry = "  select ROW_NUMBER () over (order by Zone_Code) As SNo,Zone_Code as [Zone],[Total In Ltr] as [Qty],DocumentAmount as [Amount]," + strPaymentHeading + ",(" + strTotaPayment + ") as [Total],DocumentAmount-(" + strTotaPayment + ") as [Vendors Not Paid]" &
                '  " from (select ss.Zone_Code, ss.[Total In Ltr],ss.DocumentAmount, rr.Amount, rr.Payment_Desc from (select ZONE AS  Zone_Code " &
                ' " ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr],cast(SUM(DocumentAmount) as decimal(18,2)) as DocumentAmount from " &
                ' " (select max(DocumentAmount) as DocumentAmount,zzz.Zone_Code as [Zone]  ,sum(QtyLtr) as QtyLtr from " &
                '  " (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item,TSPL_CUSTOMER_MASTER.Zone_Code " &
                '" ,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/TSPL_ITEM_UOM_DETAILltr.Conversion_Factor END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " &
                '     " Left Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   " &
                '      " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code  " &
                '   " where 2=2  And convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + ToDate.Value + "',103)   " + strWhere + " " &
                ' " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No " &
                '  ",zzz.Zone_Code " &
                '"  ) as s where 1=1 group by s.Zone)ss " &
                '" Left Join  " &
                '" ( select isnull(tspl_customer_master.Zone_Code,'') as Zone_Code ,sum(TSPL_RECEIPT_HEADER.RECEIPT_AMOUNT) AS Amount " &
                ' " ,isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') as Payment_Desc from TSPL_RECEIPT_HEADER  " &
                ' " Left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code  left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code  left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=TSPL_RECEIPT_HEADER.Payment_Code  " &
                ' " where convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>= convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " " &
                '                " Group by isnull(tspl_customer_master.Zone_Code,''),isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') " &
                '   " ) as rr on rr.Zone_Code=ss.Zone_Code ) final " &
                '  " pivot (  sum(Amount) for Payment_Desc in (" + strPayment + ") ) As zpivot  order by  Zone_Code "

                qry = "  select ROW_NUMBER () over (order by Zone_Code) As SNo,Zone_Code as [Zone],sum([Total In Ltr]) as [Qty],sum(DocumentAmount) as [Amount]," + strPaymentSum + ",(" + strTotaPayment + ") as [Total],sum(DocumentAmount)-(" + strTotaPayment + ") as [Vendors Not Paid]" &
                  " from (select isnull(tspl_customer_master.zone_code,'') as zone_code, ss.[Total In Ltr],ss.DocumentAmount, rr.Amount, rr.Payment_Desc from (select Cust_Code " &
                 " ,cast(SUM(QtyLtr) as decimal(18,2)) as [Total In Ltr],cast(SUM(DocumentAmount) as decimal(18,2)) as DocumentAmount from " &
                 " (select max(DocumentAmount) as DocumentAmount,Cust_Code  ,sum(QtyLtr) as QtyLtr from " &
                  " (Select TSPL_BOOKING_MATSER.Document_No,TSPL_BOOKING_DETAIL.DocumentAmount,TSPL_BOOKING_DETAIL.Scheme_Item,TSPL_BOOKING_DETAIL.Cust_Code " &
                " ,(CASE WHEN TSPL_BOOKING_DETAIL.Booking_Qty=0 THEN 0 ELSE (TSPL_BOOKING_DETAIL.Booking_Qty*TSPL_ITEM_UOM_DETAILUOM.Conversion_Factor)/coalesce(TSPL_ITEM_UOM_DETAILltr.Conversion_Factor,TSPL_ITEM_UOM_DETAILKG.Conversion_Factor) END) AS QtyLtr From TSPL_BOOKING_DETAIL Left Outer Join TSPL_BOOKING_MATSER On TSPL_BOOKING_MATSER.Document_No = TSPL_BOOKING_DETAIL.Document_No Left Outer Join TSPL_CUSTOMER_MASTER On TSPL_CUSTOMER_MASTER.Cust_Code = TSPL_BOOKING_DETAIL.Cust_Code Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code = TSPL_BOOKING_DETAIL.Item_Code " &
                     " Left Join TSPL_DELIVERY_NOTE_MASTER_FRESHSALE ON TSPL_DELIVERY_NOTE_MASTER_FRESHSALE.Booking_No = TSPL_BOOKING_MATSER.Document_No   " &
                      " left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILLTR on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILLTR.Item_Code and TSPL_ITEM_UOM_DETAILLTR.UOM_Code='LTR' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILKG on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILKG.Item_Code and TSPL_ITEM_UOM_DETAILKG.UOM_Code='KG' left join TSPL_ITEM_UOM_DETAIL TSPL_ITEM_UOM_DETAILUOM on TSPL_BOOKING_DETAIL.Item_Code=TSPL_ITEM_UOM_DETAILUOM.Item_Code and TSPL_BOOKING_DETAIL.Unit_code =TSPL_ITEM_UOM_DETAILUOM.UOM_Code  " &
                   " where 2=2  And convert(date, TSPL_BOOKING_MATSER.Document_Date,103) >= convert(date,'" + fromDate.Value + "',103) and  convert(date, TSPL_BOOKING_MATSER.Document_Date,103) <= convert(date,'" + ToDate.Value + "',103)   " + strWhere + " " &
                 " )zzz where zzz.Scheme_Item='N' group by zzz.Document_No " &
                  ",zzz.Cust_Code " &
                "  ) as s where 1=1 group by s.Cust_Code)ss " &
                " Left outer Join  " &
                " ( select TSPL_RECEIPT_HEADER.Cust_code ,sum(TSPL_RECEIPT_HEADER.RECEIPT_AMOUNT) AS Amount " &
                 " ,isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') as Payment_Desc from TSPL_RECEIPT_HEADER  " &
                 " Left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code  left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code  left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=TSPL_RECEIPT_HEADER.Payment_Code  " &
                 " where convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>= convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " " &
                                " Group by TSPL_RECEIPT_HEADER.Cust_code,isnull(TSPL_PAYMENT_CODE.Payment_Desc,'') " &
                   " ) as rr on rr.Cust_code=ss.Cust_code left outer join tspl_customer_master on tspl_customer_master.Cust_code=ss.Cust_code ) final " &
                  " pivot (  sum(Amount) for Payment_Desc in (" + strPayment + ") ) As zpivot  group by zpivot.Zone_Code order by  zpivot.Zone_Code "

            ElseIf chkZoneWisePayment.Checked = True Then
                    'strPayment = clsDBFuncationality.getSingleValue("  Declare @colsScheme As NVARCHAR(MAX),@query  As NVARCHAR(MAX) Select   STUFF((Select distinct ',' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc ) as Alies_Name FROM TSPL_PAYMENT_CODE order by Alies_Name FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    'strPaymentSum = clsDBFuncationality.getSingleValue(" DECLARE @colsScheme AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) SELECT   STUFF((SELECT distinct ',' +'Sum(isnull(' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc) +',0))' +' as ' + QUOTENAME( TSPL_PAYMENT_CODE.Payment_Desc) as Alies_Name  FROM TSPL_PAYMENT_CODE order by Alies_Name  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'') ")
                    If String.IsNullOrEmpty(strPayment) Then
                    clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                    Exit Sub
                    End If


                    qry = "select ROW_NUMBER () over (order by [Zone]) As SNo,[Zone],   " + strPaymentSum + " from ( select TSPL_RECEIPT_HEADER.Receipt_No,isnull(tspl_customer_master.Zone_Code,'') as Zone ,TSPL_RECEIPT_HEADER.RECEIPT_AMOUNT AS Amount,TSPL_PAYMENT_CODE.Payment_Desc from TSPL_RECEIPT_HEADER " & Environment.NewLine &
                    " left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code " &
                    " left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code " &
                    " left outer join TSPL_PAYMENT_CODE on TSPL_PAYMENT_CODE.Payment_Code=TSPL_RECEIPT_HEADER.Payment_Code " &
                    " where convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " "
                    qry += " ) as s pivot (  sum(Amount) for Payment_Desc in ( " + strPayment + " ) ) as zpivot " &
                                " group by zpivot.[Zone] order by zpivot.[Zone] "

                Else
                    qry = " select TSPL_RECEIPT_HEADER.Receipt_No as [Receipt No],TSPL_RECEIPT_HEADER.Receipt_Type as [Receipt Type],TSPL_RECEIPT_HEADER.created_By as [User Name],convert(varchar,TSPL_RECEIPT_HEADER.Receipt_Date,103) AS [Date], " & Environment.NewLine &
            " TSPL_RECEIPT_HEADER.Cust_code as [BoothId],tspl_customer_master.Zone_Code as ZONE,tspl_customer_master.Route_Desc AS [ROUTE],isnull(tspl_customer_master.OldName,'') AS BNAME," & Environment.NewLine &
            " tspl_customer_master.CUSTomer_name as ALLOTEE ,'Company' as ToParty,TSPL_RECEIPT_HEADER.Cheque_No as [Cheque/DD No],TSPL_RECEIPT_HEADER.Cheque_Date as [Cheque Date],TSPL_BANK_MASTER.DESCRIPTION   [Bank/Branch], case when TSPL_RECEIPT_HEADER.created_By='AxisBank' then 'AXISBANK'  when TSPL_RECEIPT_HEADER.created_By='ESewa' then 'ESEWA' " & Environment.NewLine &
            " when TSPL_RECEIPT_HEADER.created_By='TWALLET' then 'TWALLET'  ELSE TSPL_RECEIPT_HEADER.payment_code + case when len(tspl_customer_master.Zone_Code)>0 then  '_'+tspl_customer_master.Zone_Code else '' end END as [Payment Method Type]," & Environment.NewLine &
            " case when ISNULL(TSPL_CUSTOMER_MASTER.CUSTOMER_CATEGORY ,'')='Others' then 'Marketing' else 'LMS' end as Type,TSPL_RECEIPT_HEADER.RECEIPT_AMOUNT AS Amount from TSPL_RECEIPT_HEADER " & Environment.NewLine &
            " left outer join tspl_customer_master on tspl_customer_master.cust_code=TSPL_RECEIPT_HEADER.Cust_code " &
            " left outer join TSPL_BANK_MASTER on tspl_bank_master.BANK_CODE =TSPL_RECEIPT_HEADER .Bank_Code " &
            " where TSPL_RECEIPT_HEADER.receipt_type<>'A' and convert(date,TSPL_RECEIPT_HEADER.Receipt_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_RECEIPT_HEADER.Receipt_Date,103)<=convert(date,'" + ToDate.Value + "',103)  " + strWhere + " " + strUserWhere
                qry += " order by TSPL_RECEIPT_HEADER.Receipt_Date  "
            End If

            dt = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Gv1.DataSource = dt
                For ii As Integer = 0 To Gv1.Columns.Count - 1
                    Gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                Dim summaryRowItem As New GridViewSummaryRowItem()
                If chkZoneWisePayment.Checked = True OrElse chkZoneWiseSuppliesRemittence.Checked = True OrElse chkSaleandRealisationDetail.Checked = True Then
                    For i As Integer = 2 To Gv1.Columns.Count - 1
                        Dim Amount As New GridViewSummaryItem(clsCommon.myCstr(Gv1.Columns(i).Name), "{0:F2}", GridAggregateFunction.Sum)
                        summaryRowItem.Add(Amount)
                    Next
                Else
                    Gv1.Columns("Amount").FormatString = "{0:n2}"
                    Gv1.Columns("ToParty").IsVisible = False
                    Gv1.Columns("Cheque/DD No").IsVisible = False
                    Gv1.Columns("Cheque Date").IsVisible = False
                    Gv1.Columns("Bank/Branch").IsVisible = False
                    Gv1.Columns("Receipt No").IsVisible = False
                    Gv1.Columns("Receipt Type").IsVisible = False

                    Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(Amount)
                End If

                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.BestFitColumns()
                RadSplitExp.Items("rmiExcelSale").Visibility = ElementVisibility.Collapsed
                RadSplitExp.Items("rmiPDFSale").Visibility = ElementVisibility.Collapsed
                RadSplitExp.Items("rmiExcelCollection").Visibility = ElementVisibility.Collapsed
                RadSplitExp.Items("rmiPDFCollection").Visibility = ElementVisibility.Collapsed
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
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        'Dim ReportID As String = clsUserMgtCode.rptDailyReceiptReport
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
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
            common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
        End If
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo, Optional ByVal ReportAbstractType As String = "")
        Try
            Dim StrReportName As String = ""
            If chkCardSaleCollectionDetail.Checked = True Then
                If clsCommon.CompairString(ReportAbstractType, "Sale") = CompairStringResult.Equal Then
                    If gv_Sale.Rows.Count <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                        Exit Sub
                    End If
                ElseIf clsCommon.CompairString(ReportAbstractType, "Collection") = CompairStringResult.Equal Then
                    If gv_Collection.Rows.Count <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                        Exit Sub
                    End If
                Else
                    If gvCS_CD.Rows.Count <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                        Exit Sub
                    End If
                End If
            ElseIf Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If


            If chkZoneWisePayment.Checked = True Then
                StrReportName = clsCommon.myCstr(chkZoneWisePayment.Text)
            ElseIf chkZoneWiseSuppliesRemittence.Checked = True Then
                StrReportName = clsCommon.myCstr(chkZoneWiseSuppliesRemittence.Text)
            ElseIf chkSaleandRealisationDetail.Checked = True Then
                StrReportName = clsCommon.myCstr(chkSaleandRealisationDetail.Text)
            ElseIf chkCardSaleCollectionDetail.Checked = True Then
                If clsCommon.CompairString(ReportAbstractType, "Sale") = CompairStringResult.Equal Then
                    StrReportName = "Card Sale Abstract"
                ElseIf clsCommon.CompairString(ReportAbstractType, "Collection") = CompairStringResult.Equal Then
                    StrReportName = "Card Collection Abstract"
                Else
                    StrReportName = clsCommon.myCstr(chkCardSaleCollectionDetail.Text)
                End If
            Else
                StrReportName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptDailyReceiptReport & "'"))
            End If



            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & StrReportName)
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            'If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If

            If txtMultiCustomer.arrDispalyMember IsNot Nothing AndAlso txtMultiCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            End If
            If txtUser.arrDispalyMember IsNot Nothing AndAlso txtUser.arrDispalyMember.Count > 0 Then
                arrHeader.Add("User : " + clsCommon.GetMulcallStringWithComma(txtUser.arrDispalyMember))
            End If
            If chkCardSaleCollectionDetail.Checked = True Then
                If clsCommon.CompairString(ReportAbstractType, "Sale") = CompairStringResult.Equal Then
                    If exporter = EnumExportTo.Excel Then
                        clsCommon.MyExportToExcelGrid(StrReportName, gv_Sale, arrHeader, StrReportName)
                    Else
                        clsCommon.MyExportToPDF(StrReportName, gv_Sale, arrHeader, StrReportName, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    End If
                ElseIf clsCommon.CompairString(ReportAbstractType, "Collection") = CompairStringResult.Equal Then
                    If exporter = EnumExportTo.Excel Then
                        clsCommon.MyExportToExcelGrid(StrReportName, gv_Collection, arrHeader, StrReportName)
                    Else
                        clsCommon.MyExportToPDF(StrReportName, gv_Collection, arrHeader, StrReportName, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    End If
                Else
                    If exporter = EnumExportTo.Excel Then
                        clsCommon.MyExportToExcelGrid(StrReportName, gvCS_CD, arrHeader, StrReportName)
                    Else
                        clsCommon.MyExportToPDF(StrReportName, gvCS_CD, arrHeader, StrReportName, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                    End If
                End If
            ElseIf chkZoneWisePayment.Checked = True OrElse chkZoneWiseSuppliesRemittence.Checked = True OrElse chkSaleandRealisationDetail.Checked = True Then
                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid(StrReportName, Gv1, arrHeader, StrReportName)
                Else
                    clsCommon.MyExportToPDF(StrReportName, Gv1, arrHeader, StrReportName, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, ReportID)
                    clsCommon.MyExportToExcelGrid(StrReportName, Gv1, arrHeader, StrReportName)
                Else
                    transportSql.applyExportTemplate(Gv1, ReportID)
                    clsCommon.MyExportToPDF(StrReportName, Gv1, arrHeader, StrReportName, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
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

    Private Sub RmiExcelSale_Click(sender As Object, e As EventArgs) Handles rmiExcelSale.Click
        Export(EnumExportTo.Excel, "Sale")
    End Sub

    Private Sub RmiPDFSale_Click(sender As Object, e As EventArgs) Handles rmiPDFSale.Click
        Export(EnumExportTo.PDF, "Sale")
    End Sub

    Private Sub RmiExcelCollection_Click(sender As Object, e As EventArgs) Handles rmiExcelCollection.Click
        Export(EnumExportTo.Excel, "Collection")
    End Sub

    Private Sub RmiPDFCollection_Click(sender As Object, e As EventArgs) Handles rmiPDFCollection.Click
        Export(EnumExportTo.PDF, "Collection")
    End Sub

    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSelect", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub


    Private Sub cmbCustomerType_SelectedIndexChanged(sender As Object, e As UI.Data.PositionChangedEventArgs) Handles cmbCustomerType.SelectedIndexChanged
        If clsCommon.CompairString(cmbCustomerType.Text, "Marketing") <> CompairStringResult.Equal Then
            cmbInstitutionType.Enabled = True
            ''cmbInstitutionType.Text = "Select"
        Else
            cmbInstitutionType.Enabled = False
            cmbInstitutionType.Text = "Select"
        End If
    End Sub

    Private Sub txtUser__My_Click(sender As Object, e As EventArgs) Handles txtUser._My_Click
        Dim qry As String = " select distinct TSPL_RECEIPT_HEADER.Created_By as Code,isnull(TSPL_USER_MASTER.User_Name,'') as Name  from TSPL_RECEIPT_HEADER left outer join TSPL_USER_MASTER on  TSPL_RECEIPT_HEADER.Created_By=TSPL_USER_MASTER.User_Code  "
        txtUser.arrValueMember = clsCommon.ShowMultipleSelectForm("UserMulSelect", qry, "Code", "Name", txtUser.arrValueMember, txtUser.arrDispalyMember)

    End Sub

    Private Sub chkZoneWisePayment_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkZoneWisePayment.ToggleStateChanged
        If chkZoneWisePayment.Checked = True OrElse chkZoneWiseSuppliesRemittence.Checked = True OrElse chkSaleandRealisationDetail.Checked = True OrElse chkCardSaleCollectionDetail.Checked = True Then
            txtUser.arrValueMember = Nothing
            txtUser.Enabled = False
        Else
            txtUser.Enabled = True
        End If
    End Sub

    Private Sub chkZoneWiseSuppliesRemittence_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkZoneWiseSuppliesRemittence.ToggleStateChanged
        If chkZoneWisePayment.Checked = True OrElse chkZoneWiseSuppliesRemittence.Checked = True OrElse chkSaleandRealisationDetail.Checked = True OrElse chkCardSaleCollectionDetail.Checked = True Then
            txtUser.arrValueMember = Nothing
            txtUser.Enabled = False
        Else
            txtUser.Enabled = True
        End If
    End Sub

    Private Sub chkSaleandRealisationDetail_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkSaleandRealisationDetail.ToggleStateChanged
        If chkZoneWisePayment.Checked = True OrElse chkZoneWiseSuppliesRemittence.Checked = True OrElse chkSaleandRealisationDetail.Checked = True OrElse chkCardSaleCollectionDetail.Checked = True Then
            txtUser.arrValueMember = Nothing
            txtUser.Enabled = False
        Else
            txtUser.Enabled = True
        End If
    End Sub

    Private Sub chkCardSaleCollectionDetail_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkCardSaleCollectionDetail.ToggleStateChanged
        If chkZoneWisePayment.Checked = True OrElse chkZoneWiseSuppliesRemittence.Checked = True OrElse chkSaleandRealisationDetail.Checked = True OrElse chkCardSaleCollectionDetail.Checked = True Then
            txtUser.arrValueMember = Nothing
            txtUser.Enabled = False
        Else
            txtUser.Enabled = True
        End If
    End Sub


End Class
