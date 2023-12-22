'' Work done agaisnt ticket no SWA/10/07/18-000033
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class rptSubsidyCreditReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim isInsideLoadData As Boolean = False
    Public Shared ArrInvoice_Arr As New ArrayList()
    Private Sub SetUserMgmtNew()

        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub rptSubsidyCreditReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        Reset()
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
    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        loadReport()
    End Sub
    Sub Reset()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.DataSource = Nothing
        gv.Rows.Clear()
        txtLocation1Mult.arrValueMember = Nothing
        txtCustomerMult.arrValueMember = Nothing


    End Sub
    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
                Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(txtLocation1Mult.arrValueMember)
                arrHeader.Add((" Location : " + strLocationName + " "))
            Else
                arrHeader.Add((" Location : All"))
            End If

            If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
                arrHeader.Add(" Customer : " + clsCommon.GetMulcallStringWithComma(txtCustomerMult.arrValueMember))
            Else
                arrHeader.Add((" Customer: All"))
            End If

            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Subsidy Credit Note", gv, arrHeader, Me.Text)
            ElseIf exporter = EnumExportTo.PDF Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Subsidy Credit Note", gv, arrHeader, "Subsidy Credit Note", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub rmExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmExcel.Click
        print(EnumExportTo.Excel)
    End Sub
    Public Sub loadReport()

        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        'Sanjay 16/Aug/2018 Ticket no- SWA/16/08/18-000044
        Dim sQuery As String = " select TSPL_SUBSIDY_CRADIT_NOTE.Document_No as [Document No],convert(varchar(12),TSPL_SUBSIDY_CRADIT_NOTE.Document_Date,103) as [Document Date],TSPL_SUBSIDY_CRADIT_NOTE.Location,TSPL_GL_SEGMENT_CODE.Description as [Location Desc],TSPL_SUBSIDY_CRADIT_NOTE.Customer,TSPL_CUSTOMER_MASTER.Customer_Name as [Customer Name],TSPL_SUBSIDY_CRADIT_NOTE.ARNo,TSPL_JOURNAL_MASTER.Voucher_No as JENo,TSPL_Customer_Invoice_Head.Document_Total as Amount "
        sQuery += " from "
        sQuery += " TSPL_Customer_Invoice_Head "
        sQuery += " left outer join TSPL_JOURNAL_MASTER on TSPL_JOURNAL_MASTER.Source_Doc_No=TSPL_Customer_Invoice_Head.Document_No"
        sQuery += " left outer join TSPL_SUBSIDY_CRADIT_NOTE on TSPL_Customer_Invoice_Head.Against_Subsidy_No=TSPL_SUBSIDY_CRADIT_NOTE.Document_No"
        sQuery += " and TSPL_SUBSIDY_CRADIT_NOTE.ARNo=TSPL_Customer_Invoice_Head.Document_No "
        sQuery += " left outer join TSPL_CUSTOMER_MASTER on TSPL_CUSTOMER_MASTER.Cust_Code=TSPL_SUBSIDY_CRADIT_NOTE.Customer"
        sQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_SUBSIDY_CRADIT_NOTE.Location "
        sQuery += " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_LOCATION_MASTER.Loc_Segment_Code "
        sQuery += " where 2=2  and convert(date,TSPL_SUBSIDY_CRADIT_NOTE.Document_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SUBSIDY_CRADIT_NOTE.Document_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

        If txtLocation1Mult.arrValueMember IsNot Nothing AndAlso txtLocation1Mult.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_SUBSIDY_CRADIT_NOTE.Location   IN (" + clsCommon.GetMulcallString(txtLocation1Mult.arrValueMember) + ") "
        End If
        If txtCustomerMult.arrValueMember IsNot Nothing AndAlso txtCustomerMult.arrValueMember.Count > 0 Then
            sQuery += " and TSPL_SUBSIDY_CRADIT_NOTE.customer in (" + clsCommon.GetMulcallString(txtCustomerMult.arrValueMember) + ") " + Environment.NewLine
        End If
        sQuery += " order by convert(date,TSPL_SUBSIDY_CRADIT_NOTE.Document_Date,103) desc "

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage2
            gv.BestFitColumns()
            gv.ReadOnly = True
        Else
            gv.DataSource = Nothing
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub FrmPrintFreshInvoice_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            loadReport()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub txtCustomerMult__My_Click(sender As Object, e As EventArgs) Handles txtCustomerMult._My_Click
        Dim qry As String = "select Cust_Code as Code,Customer_Name as Name from TSPL_CUSTOMER_master order by Cust_Code"
        txtCustomerMult.arrValueMember = clsCommon.ShowMultipleSelectForm("Cust", qry, "Code", "Name", txtCustomerMult.arrValueMember, txtCustomerMult.arrDispalyMember)
    End Sub


    Private Sub txtLocation1Mult__My_Click(sender As Object, e As EventArgs) Handles txtLocation1Mult._My_Click
        Dim qry As String = " select distinct(Segment_code) as Code ,Description  from TSPL_GL_SEGMENT_CODE left outer join TSPL_LOCATION_MASTER on TSPL_GL_SEGMENT_CODE .Segment_code =TSPL_LOCATION_MASTER .Loc_Segment_Code "
        txtLocation1Mult.arrValueMember = clsCommon.ShowMultipleSelectForm("Loc", qry, "Code", "Description", txtLocation1Mult.arrValueMember, txtLocation1Mult.arrDispalyMember)
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
