Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO


Public Class rptPaymentPayable
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub
    Sub Reset()
        ' ToDate.Value = clsCommon.GETSERVERDATE()
        ' fromDate.Value = ToDate.Value.AddMonths(-1)
        'txtItem.arrValueMember = Nothing
        'txtLocation.arrValueMember = Nothing
        'txtTransaction.arrValueMember = Nothing
        'txtItemType.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PrintData(False)
    End Sub

    Public Sub PrintData(ByVal isPrint As Boolean)
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            'Dim strItemType As String = ""
            'If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
            '    strItemType = " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")"
            'End If
            qry = " select Final.* from (  select  TSPL_VENDOR_INVOICE_HEAD.Document_No as [Document No],Invoice_Entry_Date as [Date] , TSPL_VENDOR_INVOICE_HEAD.Loc_Code as [Location Code] , case when len( isnull( TSPL_Location_MASTER.Location_Desc,'') ) <= 0 then TSPL_GL_SEGMENT_CODE.Description else  TSPL_Location_MASTER.Location_Desc end  as [Location Desc] , TSPL_USER_MASTER.Segment_code as [Dept Code],TBL_DEPARTMENT.Description as [Dept Name] , TSPL_VENDOR_INVOICE_HEAD.Vendor_Code as  [Vendor Code], TSPL_VENDOR_MASTER.Vendor_Name as [Vendor Name], TSPL_VENDOR_INVOICE_HEAD.TapalNo as  [Tapal No], TSPL_VENDOR_INVOICE_HEAD.Document_Total - TSPL_VENDOR_INVOICE_HEAD.Cash_Discount_Amt - isnull (TSPL_PAYMENT_DETAIL.Applied_Amount,0)  as  [Amount to be Paid] , isnull (TSPL_PAYMENT_DETAIL.Applied_Amount,0) as [Paid Amount] , TSPL_VENDOR_INVOICE_HEAD.Document_Total - TSPL_VENDOR_INVOICE_HEAD.Cash_Discount_Amt    as [Document Amount] , TBL_PAYMENT_DETAIL_For_Mul_Payment.PaymentNo as [Payment No/Bank Transfer NO] , TBL_PAYMENT_DETAIL_For_Mul_Payment.PaymentMode as [Payment Mode] , TBL_PAYMENT_DETAIL_For_Mul_Payment.Cheque_no as [Cheque No] from TSPL_VENDOR_INVOICE_HEAD
                    left outer join TSPL_Location_MASTER on TSPL_Location_MASTER.Location_Code = TSPL_VENDOR_INVOICE_HEAD.Loc_Code
                    left outer join TSPL_GL_SEGMENT_CODE  on TSPL_GL_SEGMENT_CODE.Segment_code =  TSPL_VENDOR_INVOICE_HEAD.Loc_Code
                    left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code = TSPL_VENDOR_INVOICE_HEAD.Modify_By
                    left outer join (Select distinct Segment_code , Description from TSPL_GL_SEGMENT_CODE where Seg_No=3) as TBL_DEPARTMENT on TBL_DEPARTMENT.Segment_code = TSPL_USER_MASTER.Segment_code
                    LEFT OUTER JOIN TSPL_VENDOR_MASTER ON TSPL_VENDOR_INVOICE_HEAD.Vendor_Code=TSPL_VENDOR_MASTER.Vendor_Code  
                    left outer join (select Document_No ,  max(Original_Invoice_Amt) as Original_Invoice_Amt, sum (Applied_Amount ) as Applied_Amount from TSPL_PAYMENT_DETAIL where len (isnull (Document_No,'')) > 0 group by Document_No) TSPL_PAYMENT_DETAIL on TSPL_PAYMENT_DETAIL.Document_No = TSPL_VENDOR_INVOICE_HEAD.Document_No
                    left outer  join (SELECT  TSPL_PAYMENT_DETAIL2.Document_No , STUFF((SELECT distinct ',' + QUOTENAME(TSPL_PAYMENT_DETAIL.Payment_No) as MulPaymentNo   from TSPL_PAYMENT_DETAIL left outer join TSPL_PAYMENT_HEADER on  TSPL_PAYMENT_HEADER .Payment_No =TSPL_PAYMENT_DETAIL.Payment_No and len(isnull(TSPL_PAYMENT_DETAIL.Document_No,'')) >0  where TSPL_PAYMENT_DETAIL2.Document_No = TSPL_PAYMENT_DETAIL.Document_No and len(isnull (TSPL_PAYMENT_DETAIL.Document_No,'')) > 0  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  as PaymentNo ,STUFF (  (SELECT  ',' + QUOTENAME(TSPL_PAYMENT_HEADER.Payment_Code) as Payment_Code		
					from TSPL_PAYMENT_DETAIL left outer join TSPL_PAYMENT_HEADER on  TSPL_PAYMENT_HEADER .Payment_No =TSPL_PAYMENT_DETAIL.Payment_No and len(isnull(TSPL_PAYMENT_DETAIL.Document_No,'')) >0  where TSPL_PAYMENT_DETAIL2.Document_No = TSPL_PAYMENT_DETAIL.Document_No and len(isnull (TSPL_PAYMENT_DETAIL.Document_No,'')) > 0  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  as PaymentMode ,STUFF (  (SELECT  ',' + (TSPL_PAYMENT_HEADER.Cheque_no) as Cheque_no		
					from TSPL_PAYMENT_DETAIL left outer join TSPL_PAYMENT_HEADER on  TSPL_PAYMENT_HEADER .Payment_No =TSPL_PAYMENT_DETAIL.Payment_No and len(isnull(TSPL_PAYMENT_DETAIL.Document_No,'')) >0  where TSPL_PAYMENT_DETAIL2.Document_No = TSPL_PAYMENT_DETAIL.Document_No and len(isnull (TSPL_PAYMENT_DETAIL.Document_No,'')) > 0  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')  as Cheque_no   from TSPL_PAYMENT_DETAIL  as TSPL_PAYMENT_DETAIL2  where len(isnull(TSPL_PAYMENT_DETAIL2.Document_No,'')) >0  group by TSPL_PAYMENT_DETAIL2.Document_No ) TBL_PAYMENT_DETAIL_For_Mul_Payment on TBL_PAYMENT_DETAIL_For_Mul_Payment.Document_No = TSPL_PAYMENT_DETAIL.Document_No
                    where  TSPL_VENDOR_INVOICE_HEAD.is_For_TDS=0 and TSPL_VENDOR_INVOICE_HEAD.Invoice_Type in ('AP','VC') and TSPL_VENDOR_INVOICE_HEAD .ISProcurementDeduction =0  and TSPL_VENDOR_INVOICE_HEAD.Document_Type ='I' and TSPL_VENDOR_INVOICE_HEAD.Posting_Date is not null
                    and convert (date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) >= convert (date, '" + fromDate.Value + "',103)  and convert (date,TSPL_VENDOR_INVOICE_HEAD.Invoice_Entry_Date,103) <= convert (date, '" + ToDate.Value + "',103)  ) Final where 2=2 "

            If ChkCompelte.IsChecked = True Then
                qry += " and Final.[Amount to be Paid] = 0 "
            ElseIf ChkPending.IsChecked = True Then
                qry += " and Final.[Amount to be Paid] > 0 "
            End If

            If chkMilkBill.Checked = True Then

                qry += "  union All select '' as [Document No] , '' as Date, TBL_Plant.PlantCode as [Location Code], TBL_Plant.[PlantName] as [Location Desc],TSPL_USER_MASTER.Segment_code as [Dept Code],TBL_DEPARTMENT.Description as [Dept Name]  ,'' as  [Vendor Code],'Milk Bill' as [Vendor Name] , TBL_Plant.[PlantName] +' ' +  'MILK BILLS' +' ' + convert (varchar, TSPL_BANK_TRANSFER .To_Date,103)  as [Tapaal No] , TSPL_BANK_TRANSFER. Deposit_Amount as    [Amount to be Paid] ,  0 as  [Paid Amount], 0 as  [Document Amount], TSPL_BANK_TRANSFER.Transfer_No  as [Payment No/Bank Transfer NO] , TSPL_BANK_TRANSFER.Payment_Mode , TSPL_BANK_TRANSFER.Cheque_No from TSPL_BANK_TRANSFER 
                          left outer join (select  distinct  TSPL_BANK_TRANSFER_MCC.Transfer_No, Loc_Segment_Code as PlantCode, TSPL_GL_SEGMENT_CODE.description as [PlantName]  from TSPL_BANK_TRANSFER_MCC left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = TSPL_BANK_TRANSFER_MCC.MCC_Code inner join TSPL_GL_SEGMENT_CODE on TSPL_Location_MASTER.Loc_Segment_Code = TSPL_GL_SEGMENT_CODE.Segment_code  and TSPL_Location_MASTER.Rejected_Type = 'N' and TSPL_Location_MASTER.Location_Category='MCC' ) as TBL_Plant on TBL_Plant.Transfer_No = TSPL_BANK_TRANSFER.Transfer_No
                          left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code = TSPL_BANK_TRANSFER.Modify_By
                          left outer join (Select distinct Segment_code , Description from TSPL_GL_SEGMENT_CODE where Seg_No=3) as TBL_DEPARTMENT on TBL_DEPARTMENT.Segment_code = TSPL_USER_MASTER.Segment_code 
                          where From_Date is not null and To_Date  is not null and Post = 'P' and To_Date between  Convert (date,'" + fromDate.Value + "' ,103) and Convert (date, '" + ToDate.Value + "',103) "
            End If


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
                Gv1.Columns("Document No").IsVisible = False
                Gv1.Columns("Date").IsVisible = False
                Gv1.Columns("Location Code").IsVisible = False
                Gv1.Columns("Dept Code").IsVisible = False
                Gv1.Columns("Vendor Code").IsVisible = False

                Gv1.EnableFiltering = True
                Gv1.Columns("Amount to be Paid").FormatString = "{0:n2}"
                Gv1.Columns("Paid Amount").FormatString = "{0:n2}"
                Gv1.Columns("Document Amount").FormatString = "{0:n2}"
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim itemAmountToBePaid As New GridViewSummaryItem("Amount to be Paid", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemAmountToBePaid)

                Dim itemPaidAmount As New GridViewSummaryItem("Paid Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemPaidAmount)

                Dim itemPendingAmount As New GridViewSummaryItem("Document Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(itemPendingAmount)

                Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                Gv1.BestFitColumns()
                If isPrint = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreport(CrystalReportFolder.Purchase, qry, Nothing, "crptPaymentPayable", "Payment Approval")
                End If
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
    'Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select Code,Name from TSPL_INVENTORY_SOURCE_CODE "

    '    txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    'End Sub
    'Private Sub txtItem__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String
    '    qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
    '    txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    'End Sub
    'Private Sub txtLocation__My_Click(sender As Object, e As EventArgs)
    '    Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where Location_Type='Physical'"
    '    txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransMulSe@Batch", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    'End Sub
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptPaymentPayable & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            'If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            'End If
            'If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            'End If
            'If txtTransaction.arrDispalyMember IsNot Nothing AndAlso txtTransaction.arrDispalyMember.Count > 0 Then
            '    arrHeader.Add("Transaction : " + clsCommon.GetMulcallStringWithComma(txtTransaction.arrDispalyMember))
            'End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Payment Payable", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Payment Payable", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        PrintData(True)
    End Sub

    'Private Sub txtItemType__My_Click(sender As Object, e As EventArgs)
    '    txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypForBatchItemRep", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    'End Sub
End Class
