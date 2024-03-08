Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

'' created by richa Agarwal on 15 Jan,2020
Public Class rptDetailedCardReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub
    Private Sub rptDetailedCardReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        txtLocation.arrValueMember = Nothing
        txtMultiCustomer.arrValueMember = Nothing
        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            TemplateGridview = Gv1
            Dim qry As String = ""
            Dim dt As New DataTable

            qry = "Select final.Cust_Code as Booth,max(final.Route_No) as Route,max(final.Short_Description) as Card,sum(final.Booking_Qty) as Quantity,sum(final.Amount_with_Tax ) as Price,convert(varchar,max(final.CardSale_FROM_DATE ),103)+' - '+ convert(varchar,max(final.CardSale_TO_DATE ),103) as [TIME PERIOD],'Created' as [STATUS ID],max(final.Zone_Code) as LOC,max(final.Counter_No) as CNTRNO,'' as DDNO,convert(varchar,Document_Date ,103) as Document_Date ,Payment_Mode as Payment,max(final.Modified_By) as [Last Modified By],convert(varchar,max(final.Modified_Date),103) as [Last Modified Date],max(final.Reference_No ) as [Transaction Ref Id]  from " & Environment.NewLine & _
                " ( Select TSPL_BOOKING_DETAIL.Cust_Code ,TSPL_BOOKING_DETAIL.route_no,TSPL_ITEM_MASTER.Short_Description ,TSPL_BOOKING_DETAIL.Booking_Qty,TSPL_BOOKING_DETAIL.Amount_with_Tax  * TSPL_CARD_SALE.No_Of_Days as Amount_with_Tax ,TSPL_BOOKING_DETAIL.Loc_Code,TSPL_CUSTOMER_MASTER.Zone_Code   ,TSPL_BOOKING_MATSER.Document_Date ,TSPL_BOOKING_PAYMENT_MODE_DETAIL.Payment_Mode  ,TSPL_BOOKING_MATSER.Modified_By,TSPL_BOOKING_MATSER.Modified_Date,TSPL_ITEM_MASTER.Item_Code " & Environment.NewLine & _
                " , TSPL_BOOKING_MATSER.CardSale_From_Date, TSPL_BOOKING_MATSER.CardSale_TO_DATE,isnull(TSPL_BOOKING_MATSER.Counter_No,'') as Counter_No ,isnull(TSPL_BOOKING_MATSER.Reference_No   ,'') as Reference_No from TSPL_BOOKING_MATSER" & Environment.NewLine & _
            " left outer join TSPL_BOOKING_DETAIL on tspl_booking_matser.Document_No =TSPL_BOOKING_DETAIL.Document_No " & Environment.NewLine & _
            " left Outer Join TSPL_ITEM_MASTER on tspl_Item_Master.Item_code=TSPL_BOOKING_DETAIL.Item_Code " & Environment.NewLine & _
            "left outer join TSPL_BOOKING_PAYMENT_MODE_DETAIL  on TSPL_BOOKING_PAYMENT_MODE_DETAIL.Document_No =TSPL_BOOKING_DETAIL.Document_No  " & Environment.NewLine & _
            " left outer join TSPL_CARD_SALE on TSPL_CARD_SALE.Card_No= TSPL_BOOKING_MATSER .Card_SALE_No " & Environment.NewLine & _
            " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code= TSPL_BOOKING_DETAIL.Cust_Code " & Environment.NewLine & _
            "where from_Screen_Code='BOOK-DS_FSH' and TSPL_BOOKING_MATSER.Posted =1 and isnull(TSPL_BOOKING_MATSER.Against_Booking_No ,'')=''  " & _
            " and  convert(date,TSPL_BOOKING_MATSER.Document_Date ,103)>=convert(date,'" + fromDate.Value + "',103) AND convert(date,TSPL_BOOKING_MATSER.Document_Date,103)<=convert(date,'" + ToDate.Value + "',103) " & Environment.NewLine & _
            " )Final where 1=1 "

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and Final.Loc_Code in (" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
            End If
            If txtMultiCustomer.arrValueMember IsNot Nothing AndAlso txtMultiCustomer.arrValueMember.Count > 0 Then
                qry += " and final.Cust_code in(" + clsCommon.GetMulcallString(txtMultiCustomer.arrValueMember) + ")"
            End If

          

            qry += " group by final.Cust_Code,final.route_no  ,final.Item_Code  ,final.Payment_Mode ,final.Document_Date ,final.Loc_Code  order by convert(date,final.Document_Date,103)  "

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
                RadPageView1.SelectedPage = RadPageViewPage2
                Gv1.EnableFiltering = True
                'Gv1.Columns("Amount").FormatString = "{0:n2}"
                'Gv1.Columns("ToParty").IsVisible = False
                'Gv1.Columns("Cheque/DD No").IsVisible = False
                'Gv1.Columns("Cheque Date").IsVisible = False
                'Gv1.Columns("Bank/Branch").IsVisible = False
                'Gv1.Columns("Receipt No").IsVisible = False
                'Gv1.Columns("Receipt Type").IsVisible = False


                'Dim summaryRowItem As New GridViewSummaryRowItem()
                'Dim Amount As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
                'summaryRowItem.Add(Amount)
                'Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                Gv1.BestFitColumns()

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
            'clsCommon.myLen(clsUserMgtCode.rptDetailedCardReport) > 0 Then
            If clsCommon.myLen("") > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData("", "", objCommonVar.CurrentUserCode), clsGridLayout)
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
        Dim qry As String = " select Location_Code as Code, Location_Desc as Name from TSPL_LOCATION_MASTER where  Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("TransDetailedCardReport", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs) Handles rmsaveLayout.Click
        Dim ReportID As String = ""
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
        clsGridLayout.DeleteData("", objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub


    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If Gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

            If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
            End If

            If txtMultiCustomer.arrDispalyMember IsNot Nothing AndAlso txtMultiCustomer.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Customer : " + clsCommon.GetMulcallStringWithComma(txtMultiCustomer.arrDispalyMember))
            End If
         

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, "")
                clsCommon.MyExportToExcelGrid("Detailed Card Report", Gv1, arrHeader, Me.Text)
            Else
                transportSql.applyExportTemplate(Gv1, "")
                clsCommon.MyExportToPDF("Detailed Card Report", Gv1, arrHeader, Me.Text, "", objCommonVar.CurrentUserCode)
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

    Private Sub txtMultiCustomer__My_Click(sender As Object, e As EventArgs) Handles txtMultiCustomer._My_Click
        Dim qry As String = " select cust_code as [Code], Customer_Name as [Name] from tspl_customer_master "
        txtMultiCustomer.arrValueMember = clsCommon.ShowMultipleSelectForm("CustMulSelect", qry, "Code", "Name", txtMultiCustomer.arrValueMember, txtMultiCustomer.arrDispalyMember)
    End Sub



End Class
