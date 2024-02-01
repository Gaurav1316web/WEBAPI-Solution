Imports common
Imports System.Data.SqlClient

'' Created By Abhishek Kumar as on 26 Nov 2012
'' Update By Abhishek Kuamr as on 27 Nov 2012 For Loc Code and Locdesc
Public Class FrmUnpostedJV
    Inherits FrmMainTranScreen

    Private Sub FrmUnpostedJV_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadSourceCode()
        Reset()
    End Sub
    Sub LoadSourceCode()
        Dim qry As String = "select SourceCode as Code,SourceDescription  from TSPL_GL_SOURCECODE  "
        cbgSourceCode.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgSourceCode.ValueMember = "Code"
        cbgSourceCode.DisplayMember = "SourceDescription "
    End Sub
    Sub Reset()
        'dtpFrmDate.Value = clsCommon.GETSERVERDATE
        If System.DateTime.Now.Date.Month >= 1 AndAlso System.DateTime.Now.Date.Month <= 3 Then
            dtpFrmDate.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year - 1))
        Else
            dtpFrmDate.Value = clsCommon.myCDate("01/04/" + clsCommon.myCstr(System.DateTime.Now.Date.Year))
        End If
        dtpToDate.Value = clsCommon.GETSERVERDATE
        chkSourceCodeAll.IsChecked = True
        chkStatusPending.Checked = False
        RadPageView1.SelectedPage = RadPageViewPage1
        LoadSourceCode()
    End Sub


    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmUnpostedJV)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Printreport(True)
    End Sub
    Public Sub Printreport(ByVal isPrint As Boolean)
        Try
            Dim qry As String
            Dim dtCompany As DataTable
            Dim fromdate As String = clsCommon.myCDate(dtpFrmDate.Value, "dd/MM/yyyy")
            Dim Todate As String = clsCommon.myCDate(dtpToDate.Value, "dd/MM/yyyy")
            Dim SourcCode As ArrayList = cbgSourceCode.CheckedValue
            Dim address As String

            Dim SCode As String = ""

            If chkSourceCodeSelect.IsChecked = True AndAlso cbgSourceCode.CheckedValue.Count > 0 Then
                SCode = ("'" + clsCommon.GetMulcallString(cbgSourceCode.CheckedValue) + "'")
                SCode = SCode.Replace("'", "")
            End If



            Dim CompanyQry As String = "select  TSPL_COMPANY_MASTER.Comp_Name,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,(ISNULL(tspl_company_Master.ADD1,'') + case when len(RTRIM(ISNULL(tspl_company_Master.Add2,'')))>0 then +', '+tspl_company_Master.Add2 else '' end+ case when LEN(RTRIM(IsNull(tspl_company_Master.ADD3,'')))>0 then + ', '+tspl_company_Master.ADD3 else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.City_Code,'')))>0 then  + ', '+tspl_company_Master.City_Code else '' end + case when len(RTRIM(ISNULL(tspl_company_Master.State,'')))>0 then  + ', '+tspl_company_Master.State else '' end ) as CompanyAddress from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'"
            dtCompany = clsDBFuncationality.GetDataTable(CompanyQry)
            address = clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress"))
            If isPrint = True Then
                qry = " select '" + SCode + "' as SourceCode,Final_Query.*,(select Location_Desc From tspl_Location_Master where Location_code=final_query.Location)as LocDesc from  "

            Else
                qry = "  Select Final_Query.[Voucher No], Final_Query.[Voucher Date], Final_Query.[Source Code], Final_Query.[Source Doc No], Final_Query.[Source Doc No], Final_Query.[Total Debit Amt], Final_Query.[Total Credit Amt],Final_Query.[Diffrence Amt] ,Final_Query.Status   from "
            End If
            qry += " (select xx.*,(case when xx.[Source Code] ='SD-IN' then (select location From tspl_sale_invoice_Head where tspl_sale_invoice_head.sale_Invoice_No= xx.[Source Doc No])when xx.[Source Code]='MM-TF' then (select From_location From tspl_transfer_head where  tspl_transfer_head .Transfer_No= xx.[Source Doc No])else '' end ) as Location from  "
            qry += " ( Select '" + fromdate + "' as FilterFromDate,'" + Todate + "' as FilterToDate,'" + clsCommon.myCstr(dtCompany.Rows(0)("Comp_Name")) + "' as CompanyName,'" + clsCommon.myCstr(dtCompany.Rows(0)("CompanyAddress")) + "' as CompanyAddress, Voucher_No as [Voucher No],Convert(varchar(12),Voucher_Date,103) as [Voucher Date],Source_Code as [Source Code],Source_Doc_No as [Source Doc No],Convert(varchar(12),Source_Doc_Date,103) as [Source Doc Date],Total_Debit_Amt as [Total Debit Amt] ,Total_Credit_Amt as [Total Credit Amt] ,(Total_Debit_Amt -Total_Credit_Amt )as [Diffrence Amt],case when Authorized='A'then 'Y' else 'N' end as Status,(select Logo_Img from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img,(select Logo_Img2  from TSPL_COMPANY_MASTER where Comp_Code='" + objCommonVar.CurrentCompanyCode + "') as Logo_Img2   from TSPL_JOURNAL_MASTER where 2= 2  "
            If chkStatusPending.Checked = True Then
                qry += " and (Total_Debit_Amt <> Total_Credit_Amt OR Authorized <> 'A' ) "
            Else
                qry += " and Total_Debit_Amt <> Total_Credit_Amt  "
            End If
            qry += " and Voucher_Date >=Convert(Date,'" + dtpFrmDate.Value + "',103) and Voucher_Date <=Convert(Date,'" + dtpToDate.Value + "',103)"
            If chkSourceCodeSelect.IsChecked Then
                If cbgSourceCode.CheckedValue.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please select atleast One Source Code", Me.Text)
                    Return

                End If
                qry += " and Source_Code in (" + clsCommon.GetMulcallString(cbgSourceCode.CheckedValue) + ")"
            End If
            qry += " )as xx) as Final_Query "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
            Else
                ' dt = clsDBFuncationality.GetDataTable(qry)
                If isPrint = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.GeneralLedger, dt, "Unposted JV", "Unposted JV")
                    frmCRV = Nothing
                Else
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.DataSource = dt
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    RadPageView1.SelectedPage = RadPageViewPage3
                    gv1.BestFitColumns()
                End If

            End If

        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub chkSourceCodeAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSourceCodeAll.ToggleStateChanged
        cbgSourceCode.Enabled = False
    End Sub

    Private Sub chkSourceCodeSelect_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkSourceCodeSelect.ToggleStateChanged
        cbgSourceCode.Enabled = True
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Printreport(False)
    End Sub

    Private Sub chkStatusPending_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkStatusPending.ToggleStateChanged
        Try
            If chkStatusPending.Checked = True Then
                btnPrint.Enabled = False
            Else
                btnPrint.Enabled = True
            End If
        Catch ex As Exception

        End Try

    End Sub
End Class
