Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'================Created by Preeti Gupta ================
'========update by preeti gupta ticket no[BM00000007795]=======
Public Class FrmVendorSecurity
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False


    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptVendorSecurity)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnexport.Visible = MyBase.isExport
    End Sub

    Sub LoadVendor()
        Dim qry As String = " select TSPL_VENDOR_MASTER.Vendor_Code as [Code], TSPL_VENDOR_MASTER.Vendor_Name as [Name] from TSPL_VENDOR_MASTER  where TSPL_VENDOR_MASTER.Status='N' "
        cbgVendor.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVendor.ValueMember = "Code"
        cbgVendor.DisplayMember = "Name"

    End Sub

    Sub Print(ByVal IsPrint As Exporter)
        Try
            If fromDate.Value > ToDate.Value Then
                common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
                fromDate.Focus()
                Exit Sub
            End If
            If ChkVendorSelect.IsChecked AndAlso cbgVendor.CheckedValue.Count = 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select atleast single Vendor or select all.", Me.Text)
                Exit Sub
            End If

            If rbtnSecurity.IsChecked Then
                ShowSecurity(IsPrint)
            ElseIf rbtnSaving.IsChecked Then
                ShowSaving()
            Else
                ShowRetention(IsPrint)
            End If

            'If rbtnSecurity.IsChecked Then
            '    ShowSecurity(IsPrint)
            'Else
            '    ShowSaving()
            'End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub



    Private Sub ShowSaving()
        'clsVedorInvoiceHead.GetVendorSecurity(rdbDetail.IsChecked, fromDate.Value, ToDate.Value, IIf(ChkVendorSelect.IsChecked, cbgVendor.CheckedValue, Nothing), txtVendorGroupMult.arrValueMember, txtLocationMult.arrValueMember)
        Dim BaseQry As String = "select x.Document_No,convert(varchar, x.Document_Date,103) as Document_Date,x.DocumentType,x.Vendor_Code,TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VENDOR_MASTER.Vendor_Name,x.Document_Amt from (
select TSPL_VENDOR_INVOICE_HEAD.Document_No,TSPL_VENDOR_INVOICE_HEAD.Posting_Date as Document_Date,'Saving' DocumentType,TSPL_VENDOR_INVOICE_HEAD.Vendor_Code,TSPL_VENDOR_INVOICE_HEAD.Document_Total as Document_Amt
from TSPL_PAYMENT_PROCESS_SAVING
left outer join TSPL_VENDOR_INVOICE_HEAD on TSPL_VENDOR_INVOICE_HEAD.Document_No=TSPL_PAYMENT_PROCESS_SAVING.AP_Invoice_No
where TSPL_VENDOR_INVOICE_HEAD.Posting_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and  TSPL_VENDOR_INVOICE_HEAD.Posting_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' "
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            BaseQry += " and TSPL_VENDOR_INVOICE_HEAD.Loc_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
        End If
        If ChkVendorSelect.IsChecked Then
            BaseQry += " and TSPL_VENDOR_INVOICE_HEAD.Vendor_Code  in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
        End If
        BaseQry += " union all
Select TSPL_PAYMENT_HEADER.Payment_No as Document_No,TSPL_PAYMENT_HEADER.Payment_Date as Document_Date,'Payment' as DocumentType,TSPL_PAYMENT_HEADER.Vendor_Code,-1*TSPL_PAYMENT_HEADER.Payment_Amount as Document_Amt from TSPL_PAYMENT_HEADER 
where isnull(TSPL_PAYMENT_HEADER.Saving,0)=1 and Payment_Type='OA' and 
TSPL_PAYMENT_HEADER.Payment_Date>='" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(fromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' and  TSPL_PAYMENT_HEADER.Payment_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(ToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "'"
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            BaseQry += " and TSPL_PAYMENT_HEADER.Location_GL_Code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")"
        End If
        If ChkVendorSelect.IsChecked Then
            BaseQry += " and TSPL_PAYMENT_HEADER.Vendor_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
        End If
        BaseQry += ")x 
left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=x.Vendor_Code
left join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=x.Vendor_Code
where 2=2 "
        If ChkVendorSelect.IsChecked Then
            BaseQry += " and TSPL_VENDOR_MASTER.Vendor_Group_Code in (" + clsCommon.GetMulcallString(cbgVendor.CheckedValue) + ")"
        End If

        Dim qry As String = ""
        If rdbSummary.IsChecked Then
            qry = "select xx.Vendor_Code,max(xx.VLC_Code_VLC_Uploader) as VLC_Code_VLC_Uploader,max(Vendor_Name) as Vendor_Name,sum(Document_Amt) as Document_Amt  from (" + BaseQry + ")xx group by xx.Vendor_Code"
        Else
            qry = BaseQry + " order by x.Document_Date "

        End If
        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            Gv1.DataSource = Nothing

            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv

            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()

            FormatGridSavingDetails()

            If rdbSummary.IsChecked Then
                Gv1.Tag = "Summary"
            Else
                Gv1.Tag = "Details"
            End If

            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If ChkVendorSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgVendor.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Vendor: " + strLocationName + " "))
            End If

        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

    Private Sub ShowRetention(ByVal IsPrint As Exporter)
        Dim qry As String = clsVedorInvoiceHead.GetVendorRetention(rdbDetail.IsChecked, fromDate.Value, ToDate.Value, IIf(ChkVendorSelect.IsChecked, cbgVendor.CheckedValue, Nothing), txtVendorGroupMult.arrValueMember, txtLocationMult.arrValueMember)
        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            Gv1.DataSource = Nothing

            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv

            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()

            'FormatGridDetails()
            FormatGridRetention()

            If rdbSummary.IsChecked Then
                Gv1.Tag = "Summary"
            Else
                Gv1.Tag = "Details"
            End If
            '===============added by shivani against ticket[BM00000008613]  Ticket No : KDI/03/01/19-000447 By Prabhakar add Summary rpt File
            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If ChkVendorSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgVendor.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Vendor: " + strLocationName + " "))
            End If
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Vendor Security Report" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text)
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Vendor Security Report" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Security Level", True)
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

    Private Sub ShowSecurity(ByVal IsPrint As Exporter)
        Dim qry As String = clsVedorInvoiceHead.GetVendorSecurity(rdbDetail.IsChecked, fromDate.Value, ToDate.Value, IIf(ChkVendorSelect.IsChecked, cbgVendor.CheckedValue, Nothing), txtVendorGroupMult.arrValueMember, txtLocationMult.arrValueMember)
        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            Gv1.DataSource = Nothing

            Gv1.Rows.Clear()
            Gv1.Columns.Clear()
            Gv1.DataSource = dtgv

            Gv1.GroupDescriptors.Clear()
            Gv1.MasterTemplate.SummaryRowsBottom.Clear()

            FormatGridDetails()

            If rdbSummary.IsChecked Then
                Gv1.Tag = "Summary"
            Else
                Gv1.Tag = "Details"
            End If
            '===============added by shivani against ticket[BM00000008613]  Ticket No : KDI/03/01/19-000447 By Prabhakar add Summary rpt File
            If btnReferesh = False Then
                Dim frmCRV As New frmCrystalReportViewer()
                If rdbDetail.IsChecked = True Then
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtgv, "rptVendorSecurity", "Vendor Security")
                Else
                    frmCRV.funreport(CrystalReportFolder.Purchase, dtgv, "rptVendorSecurity_summary", "Vendor Security")
                End If
                frmCRV = Nothing
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()
            Gv1.MasterTemplate.AllowAddNewRow = False


            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strTemp As String = ""
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If ChkVendorSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgVendor.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Vendor: " + strLocationName + " "))
            End If
            If IsPrint = Exporter.Excel Then
                clsCommon.MyExportToExcelGrid("Vendor Security Report" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text)
            ElseIf IsPrint = Exporter.PDF Then
                clsCommon.MyExportToPDF("Vendor Security Report" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, "Security Level", True)
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
    End Sub

    Sub FormatGridRetention()

        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next
        If rdbDetail.IsChecked Then
            Gv1.Columns("RowNo").IsVisible = True
            Gv1.Columns("RowNo").Width = 30
            Gv1.Columns("RowNo").HeaderText = "S No."

            Gv1.Columns("DocDate").IsVisible = True
            Gv1.Columns("DocDate").Width = 100
            Gv1.Columns("DocDate").HeaderText = "Doc Date"

            Gv1.Columns("Document_No").IsVisible = True
            Gv1.Columns("Document_No").Width = 100
            Gv1.Columns("Document_No").HeaderText = "Doc No"

            Gv1.Columns("Document_Type").IsVisible = False
            Gv1.Columns("Document_Type").Width = 100
            Gv1.Columns("Document_Type").HeaderText = "Doc Type"

            Gv1.Columns("Is_Retention").IsVisible = True
            Gv1.Columns("Is_Retention").Width = 100
            Gv1.Columns("Is_Retention").HeaderText = "Retention Type"

            Gv1.Columns("Vendor_Code").IsVisible = True
            Gv1.Columns("Vendor_Code").Width = 100
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 200
            Gv1.Columns("Vendor_Name").HeaderText = " Vendor Name"

            Gv1.Columns("Opening").IsVisible = True
            Gv1.Columns("Opening").Width = 100
            Gv1.Columns("Opening").HeaderText = "Opening Balance"

            Gv1.Columns("DrAmt").IsVisible = True
            Gv1.Columns("DrAmt").Width = 150
            Gv1.Columns("DrAmt").HeaderText = "Debit"

            Gv1.Columns("CrAmt").IsVisible = True
            Gv1.Columns("CrAmt").Width = 100
            Gv1.Columns("CrAmt").HeaderText = "Credit"

            Gv1.Columns("Closing").IsVisible = True
            Gv1.Columns("Closing").Width = 100
            Gv1.Columns("Closing").HeaderText = "Closing Balance"

            Gv1.Columns("Loc_Code").IsVisible = True
            Gv1.Columns("Loc_Code").Width = 100
            Gv1.Columns("Loc_Code").HeaderText = "Location Code"

            Gv1.Columns("Description").IsVisible = True
            Gv1.Columns("Description").Width = 100
            Gv1.Columns("Description").HeaderText = "Location Name"

            Gv1.Columns("Vendor_Group_Code").IsVisible = True
            Gv1.Columns("Vendor_Group_Code").Width = 100
            Gv1.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            Gv1.Columns("Group_Desc").IsVisible = True
            Gv1.Columns("Group_Desc").Width = 100
            Gv1.Columns("Group_Desc").HeaderText = "Vendor Group Name"

            'Gv1.Columns("SecurityDepositType").IsVisible = False
            'Gv1.Columns("SecurityDepositType").Width = 100
            'Gv1.Columns("SecurityDepositType").HeaderText = "Security Type"

            'Gv1.Columns("Narration").IsVisible = True
            'Gv1.Columns("Narration").Width = 100
            'Gv1.Columns("Narration").HeaderText = "Narration"
        ElseIf rdbSummary.IsChecked Then

            Gv1.Columns("Vendor_Code").IsVisible = True
            Gv1.Columns("Vendor_Code").Width = 100
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 200
            Gv1.Columns("Vendor_Name").HeaderText = " Vendor Name"

            Gv1.Columns("Opening").IsVisible = True
            Gv1.Columns("Opening").Width = 100
            Gv1.Columns("Opening").HeaderText = "Opening Balance"

            Gv1.Columns("DrAmt").IsVisible = True
            Gv1.Columns("DrAmt").Width = 150
            Gv1.Columns("DrAmt").HeaderText = "Debit"

            Gv1.Columns("CrAmt").IsVisible = True
            Gv1.Columns("CrAmt").Width = 100
            Gv1.Columns("CrAmt").HeaderText = "Credit"

            Gv1.Columns("Closing").IsVisible = True
            Gv1.Columns("Closing").Width = 100
            Gv1.Columns("Closing").HeaderText = "Closing Balance"

            Gv1.Columns("Vendor_Group_Code").IsVisible = True
            Gv1.Columns("Vendor_Group_Code").Width = 100
            Gv1.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            Gv1.Columns("Group_Desc").IsVisible = True
            Gv1.Columns("Group_Desc").Width = 100
            Gv1.Columns("Group_Desc").HeaderText = "Vendor Group Name"

        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        Dim item1 As New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim TotalClosing As New GridViewSummaryItem()
        TotalClosing.FormatString = "{0:F2}"
        TotalClosing.Name = "Closing"
        TotalClosing.AggregateExpression = "sum(Opening) + sum(CrAmt) - sum(DrAmt)"
        summaryRowItem.Add(TotalClosing)

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub


    Sub FormatGridSavingDetails()
        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next
        If rdbDetail.IsChecked Then

            Gv1.Columns("Document_No").IsVisible = True
            Gv1.Columns("Document_No").Width = 100
            Gv1.Columns("Document_No").HeaderText = "Document No"

            Gv1.Columns("Document_Date").IsVisible = True
            Gv1.Columns("Document_Date").Width = 100
            Gv1.Columns("Document_Date").HeaderText = "Document Date"


            Gv1.Columns("DocumentType").IsVisible = True
            Gv1.Columns("DocumentType").Width = 100
            Gv1.Columns("DocumentType").HeaderText = "Document Type"

            Gv1.Columns("Vendor_Code").IsVisible = False
            Gv1.Columns("Vendor_Code").Width = 100
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
            Gv1.Columns("VLC_Code_VLC_Uploader").Width = 100
            Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 200
            Gv1.Columns("Vendor_Name").HeaderText = " DCS Name"



            Gv1.Columns("Document_Amt").IsVisible = True
            Gv1.Columns("Document_Amt").Width = 150
            Gv1.Columns("Document_Amt").HeaderText = "Amount"

        ElseIf rdbSummary.IsChecked Then

            Gv1.Columns("Vendor_Code").IsVisible = False
            Gv1.Columns("Vendor_Code").Width = 100
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True
            Gv1.Columns("VLC_Code_VLC_Uploader").Width = 100
            Gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 200
            Gv1.Columns("Vendor_Name").HeaderText = " DCS Name"

            Gv1.Columns("Document_Amt").IsVisible = True
            Gv1.Columns("Document_Amt").Width = 150
            Gv1.Columns("Document_Amt").HeaderText = "Amount"


        End If



        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        'Changed By : Prabhakar ' 
        Dim item1 As New GridViewSummaryItem("Document_Amt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)



        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub FormatGridDetails()
        Gv1.TableElement.TableHeaderHeight = 20
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next
        If rdbDetail.IsChecked Then
            Gv1.Columns("RowNo").IsVisible = True
            Gv1.Columns("RowNo").Width = 30
            Gv1.Columns("RowNo").HeaderText = "S No."

            Gv1.Columns("DocDate").IsVisible = True
            Gv1.Columns("DocDate").Width = 100
            Gv1.Columns("DocDate").HeaderText = "Doc Date"

            Gv1.Columns("Document_No").IsVisible = True
            Gv1.Columns("Document_No").Width = 100
            Gv1.Columns("Document_No").HeaderText = "Doc No"

            Gv1.Columns("Document_Type").IsVisible = False
            Gv1.Columns("Document_Type").Width = 100
            Gv1.Columns("Document_Type").HeaderText = "Doc Type"

            Gv1.Columns("Is_Security").IsVisible = True
            Gv1.Columns("Is_Security").Width = 100
            Gv1.Columns("Is_Security").HeaderText = "Security Type"

            Gv1.Columns("Vendor_Code").IsVisible = True
            Gv1.Columns("Vendor_Code").Width = 100
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 200
            Gv1.Columns("Vendor_Name").HeaderText = " Vendor Name"

            Gv1.Columns("Opening").IsVisible = True
            Gv1.Columns("Opening").Width = 100
            Gv1.Columns("Opening").HeaderText = "Opening Balance"

            Gv1.Columns("DrAmt").IsVisible = True
            Gv1.Columns("DrAmt").Width = 150
            Gv1.Columns("DrAmt").HeaderText = "Debit"

            Gv1.Columns("CrAmt").IsVisible = True
            Gv1.Columns("CrAmt").Width = 100
            Gv1.Columns("CrAmt").HeaderText = "Credit"

            Gv1.Columns("Closing").IsVisible = True
            Gv1.Columns("Closing").Width = 100
            Gv1.Columns("Closing").HeaderText = "Closing Balance"

            Gv1.Columns("Loc_Code").IsVisible = True
            Gv1.Columns("Loc_Code").Width = 100
            Gv1.Columns("Loc_Code").HeaderText = "Location Code"

            Gv1.Columns("Description").IsVisible = True
            Gv1.Columns("Description").Width = 100
            Gv1.Columns("Description").HeaderText = "Location Name"

            Gv1.Columns("Vendor_Group_Code").IsVisible = True
            Gv1.Columns("Vendor_Group_Code").Width = 100
            Gv1.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            Gv1.Columns("Group_Desc").IsVisible = True
            Gv1.Columns("Group_Desc").Width = 100
            Gv1.Columns("Group_Desc").HeaderText = "Vendor Group Name"

            'Gv1.Columns("SecurityDepositType").IsVisible = False
            'Gv1.Columns("SecurityDepositType").Width = 100
            'Gv1.Columns("SecurityDepositType").HeaderText = "Security Type"

            'Gv1.Columns("Narration").IsVisible = True
            'Gv1.Columns("Narration").Width = 100
            'Gv1.Columns("Narration").HeaderText = "Narration"
        ElseIf rdbSummary.IsChecked Then

            Gv1.Columns("Vendor_Code").IsVisible = True
            Gv1.Columns("Vendor_Code").Width = 100
            Gv1.Columns("Vendor_Code").HeaderText = "Vendor Code"

            Gv1.Columns("Vendor_Name").IsVisible = True
            Gv1.Columns("Vendor_Name").Width = 200
            Gv1.Columns("Vendor_Name").HeaderText = " Vendor Name"

            Gv1.Columns("Opening").IsVisible = True
            Gv1.Columns("Opening").Width = 100
            Gv1.Columns("Opening").HeaderText = "Opening Balance"

            Gv1.Columns("DrAmt").IsVisible = True
            Gv1.Columns("DrAmt").Width = 150
            Gv1.Columns("DrAmt").HeaderText = "Debit"

            Gv1.Columns("CrAmt").IsVisible = True
            Gv1.Columns("CrAmt").Width = 100
            Gv1.Columns("CrAmt").HeaderText = "Credit"

            Gv1.Columns("Closing").IsVisible = True
            Gv1.Columns("Closing").Width = 100
            Gv1.Columns("Closing").HeaderText = "Closing Balance"

            Gv1.Columns("Vendor_Group_Code").IsVisible = True
            Gv1.Columns("Vendor_Group_Code").Width = 100
            Gv1.Columns("Vendor_Group_Code").HeaderText = "Vendor Group Code"

            Gv1.Columns("Group_Desc").IsVisible = True
            Gv1.Columns("Group_Desc").Width = 100
            Gv1.Columns("Group_Desc").HeaderText = "Vendor Group Name"

        End If

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0
        'Changed By : Prabhakar ' 
        Dim item1 As New GridViewSummaryItem("Opening", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("DrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("CrAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        'Dim item4 As New GridViewSummaryItem("Closing", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item4)
        '===========
        Dim TotalClosing As New GridViewSummaryItem()
        TotalClosing.FormatString = "{0:F2}"
        TotalClosing.Name = "Closing"
        TotalClosing.AggregateExpression = "sum(Opening) + sum(CrAmt) - sum(DrAmt)"
        summaryRowItem.Add(TotalClosing)

        'Dim item1 As New GridViewSummaryItem("crateqty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)
        'Dim item2 As New GridViewSummaryItem("crateqtyrecd", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item2)
        'Dim item3 As New GridViewSummaryItem("Short_Excess", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item3)

        'gv.GroupDescriptors.Add(New GridGroupByExpression("DOC_DATE as Item format ""{0}: {1}"" Group By DOC_DATE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))

        Gv1.ShowGroupPanel = False
        Gv1.MasterTemplate.AutoExpandGroups = True

        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        LoadVendor()
        ChkVendorAll.CheckState = CheckState.Checked
        rdbSummary.IsChecked = True
        Gv1.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Dim obj As New clsGridLayout()
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            obj = New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridColumns = Gv1.ColumnCount
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
    End Sub

    Private Sub FrmVendorSecurity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadVendor()

        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N ")
        Reset()
    End Sub

    Private Sub FrmVendorSecurity_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub
    Enum Exporter
        Excel = 0
        PDF = 1
        Print = 2
        Refresh = 3
    End Enum
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        btnReferesh = True
        PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSecurity.IsChecked, "", "S") + IIf(rdbSummary.IsChecked = True, "S", "D")
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptVendorSecurity & "'"))
            If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
                arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
            End If
            If txtVendorGroupMult.arrValueMember IsNot Nothing AndAlso txtVendorGroupMult.arrValueMember.Count > 0 Then
                arrHeader.Add("Vendor Group : " + clsCommon.GetMulcallStringWithComma(txtVendorGroupMult.arrDispalyMember))
            End If
            If ChkVendorSelect.IsChecked Then
                Dim strLocationName As String = ""
                For Each StrName As String In cbgVendor.CheckedDisplayMember
                    If clsCommon.myLen(strLocationName) > 0 Then
                        strLocationName += ", "
                    End If
                    strLocationName += StrName
                Next
                Dim strLocationCode As String = ""
                For Each StrCode As String In cbgVendor.CheckedValue
                    If clsCommon.myLen(strLocationCode) > 0 Then
                        strLocationCode += ", "
                    End If
                    strLocationCode += StrCode
                Next
                arrHeader.Add(("Vendor: " + strLocationName + " "))
            End If
            If exporter = EnumExportTo.Excel Then
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(Gv1, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Vendor " + IIf(rbtnSaving.IsChecked, "Saving", "Security") + "  Report" + IIf(rdbDetail.IsChecked, "( Detail )", "( Summary )"), Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ChkVendorAll_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles ChkVendorAll.ToggleStateChanged
        cbgVendor.Enabled = Not ChkVendorAll.IsChecked
    End Sub

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If rdbDetail.IsChecked Then
            Dim strAPInvoice As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_Type").Value)
            If clsCommon.CompairString(strAPInvoice, "AP Invoice") = CompairStringResult.Equal Then
                If clsCommon.myLen(Gv1.CurrentRow.Cells("Document_No").Value) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmVendorService, Gv1.CurrentRow.Cells("Document_No").Value)
                End If
            End If
            Dim strPaymentEntry As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_Type").Value)
            If clsCommon.CompairString(strPaymentEntry, "Payment Entry") = CompairStringResult.Equal Then
                If clsCommon.myLen(Gv1.CurrentRow.Cells("Document_No").Value) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.PaymentEntryNew, Gv1.CurrentRow.Cells("Document_No").Value)
                End If
            End If
            Dim strBankReverse As String = clsCommon.myCstr(Gv1.CurrentRow.Cells("Document_Type").Value)
            If clsCommon.CompairString(strBankReverse, "Bank Reverse") = CompairStringResult.Equal Then
                If clsCommon.myLen(Gv1.CurrentRow.Cells("Document_No").Value) > 0 Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.reverseTransaction, Gv1.CurrentRow.Cells("Document_No").Value)
                End If
            End If
        End If

    End Sub

    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        Dim qry As String = " select Segment_code as Code ,Description as Name  from TSPL_GL_SEGMENT_CODE where Segment_name ='Locations' and Segment_code <>''"
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("Loc", qry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
        'FrmPendingRequisitionQty.SetDiplayMember(txtLocationMult, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub txtVendorGroupMult__My_Click(sender As Object, e As EventArgs) Handles txtVendorGroupMult._My_Click
        Dim qry As String = "select Ven_Group_Code as Code,Group_Desc as Name  from TSPL_VENDOR_GROUP"
        txtVendorGroupMult.arrValueMember = clsCommon.ShowMultipleSelectForm("VendorGroup", qry, "Code", "Name", txtVendorGroupMult.arrValueMember, txtVendorGroupMult.arrDispalyMember)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        btnReferesh = False
        Print(Exporter.Refresh)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        Export(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        Export(EnumExportTo.PDF)
    End Sub

    Private Sub rbtnSecurity_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles rbtnSecurity.ToggleStateChanged, rbtnSaving.ToggleStateChanged
        btnPrint.Visible = rbtnSecurity.IsChecked
    End Sub
End Class