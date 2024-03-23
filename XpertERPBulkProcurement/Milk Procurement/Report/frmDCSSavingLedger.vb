Imports common
Public Class frmDCSSavingLedger

    Private Sub frmDCSSavingLedger_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            txtFromDate.Value = clsCommon.GETSERVERDATE()
            txtToDate.Value = clsCommon.GETSERVERDATE()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub txtBMC__My_Click(sender As Object, e As EventArgs) Handles txtBMC._My_Click
        Try
            Dim qry As String = "select TSPL_MCC_MASTER.MCC_Code as Code,TSPL_MCC_MASTER.MCC_NAME as Name from TSPL_MCC_MASTER inner join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_MCC_MASTER.MCC_Code"
            Dim whrCls As String = ""
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                whrCls += " and  Location_Code in (" & objCommonVar.strCurrUserLocations & ")  "
            End If
            txtBMC.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "DCSLDGR@BMC", qry, "Code", "", txtBMC.arrValueMember, Nothing)
            txtVSP.arrValueMember = Nothing
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtVSP__My_Click(sender As Object, e As EventArgs) Handles txtVSP._My_Click
        Try
            Dim qry As String = "select VSP_Code as DCSCode,TSPL_VENDOR_MASTER.Vendor_Name as DCSName,VLC_Code_VLC_Uploader as UploaderNo,VLC_Code as VLCCode,VLC_Name as VLCNAme from TSPL_VENDOR_MASTER
                                 inner join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_MASTER.Vendor_Code"
            If txtBMC.arrValueMember.Count > 0 Then
                qry += " where TSPL_VENDOR_MASTER.Form_Type='VSP' and  TSPL_VLC_MASTER_HEAD.MCC in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
            End If

            txtVSP.arrValueMember = clsCommon.ShowMultipleSelectForm(False, "DCSLDGR@DCS", qry, "DCSCode", "", txtVSP.arrValueMember, Nothing)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Try
            RadPageView1.SelectedPage = RadPageViewPage1
            txtBMC.arrValueMember = Nothing
            txtVSP.arrValueMember = Nothing
            Reset()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub Reset()
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterView.Refresh()
        gv1.Refresh()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            Dim Qry As String = GetQuery()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                Reset()
                gv1.DataSource = dt
                SetGridFormat()
                dt = Nothing
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub SetGridFormat()
        RadPageView1.SelectedPage = RadPageViewPage2
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = False
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next

        gv1.Columns("SNo").IsVisible = False

        gv1.Columns("VLC_Code_VLC_Uploader").HeaderText = "DCS Uploader Code"
        gv1.Columns("VLC_Code_VLC_Uploader").IsVisible = True

        gv1.Columns("VSP_Code").HeaderText = "DCS Code"
        gv1.Columns("VSP_Code").IsVisible = False

        gv1.Columns("VLC_Name").HeaderText = "DCS Name"
        gv1.Columns("VLC_Name").IsVisible = True

        If rbtnDetails.Checked Then
            gv1.Columns("Document_No").HeaderText = "Document No"
            gv1.Columns("Document_No").IsVisible = True

            gv1.Columns("DocuemntDate").HeaderText = "Document Date"
            gv1.Columns("DocuemntDate").IsVisible = True
        Else
            gv1.Columns("Document_No").HeaderText = "Document No"
            gv1.Columns("Document_No").IsVisible = False

            gv1.Columns("DocuemntDate").HeaderText = "Document Date"
            gv1.Columns("DocuemntDate").IsVisible = False
        End If

        gv1.Columns("OPBal").HeaderText = "Opening Balance"
        gv1.Columns("OPBal").IsVisible = True

        gv1.Columns("Amount").HeaderText = "Amount"
        gv1.Columns("Amount").IsVisible = True

        gv1.Columns("CLBal").HeaderText = "Closing Balance"
        gv1.Columns("CLBal").IsVisible = True


        'gv1.Columns("RI").IsVisible = False
        gv1.BestFitColumns()
        gv1.MasterTemplate.AutoExpandGroups = True

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim item As GridViewSummaryItem = New GridViewSummaryItem("Amount", "{0:n2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item)
        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Function GetQuery() As String
        Dim BaseQuery As String = Nothing
        Dim FinalQuery As String = Nothing

        BaseQuery = "Select TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VSP_Code ,TSPL_VLC_MASTER_HEAD.VLC_Name ,
                    TSPL_VENDOR_INVOICE_HEAD.Document_No,Convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103) As DocuemntDate,'CreditNote' as DocumentType  ,TSPL_VENDOR_INVOICE_HEAD.Document_Total As Amount,1 as RI
                    from TSPL_VENDOR_INVOICE_HEAD
                    Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_VENDOR_INVOICE_HEAD.Vendor_Code
                    Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                    where  TSPL_VENDOR_INVOICE_HEAD.Document_Type='C' " '--And Saving=1"

        If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
            BaseQuery += " and TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ")"
        End If
        If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
            BaseQuery += " and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ")"
        End If
        'BaseQuery += "      and Convert(date,TSPL_VENDOR_INVOICE_HEAD.Posting_Date,103)<=Convert(date,'" + txtToDate.Value + "',103) "
        BaseQuery += "    union all "
        BaseQuery += "    Select
                    TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader,TSPL_VLC_MASTER_HEAD.VSP_Code ,TSPL_VLC_MASTER_HEAD.VLC_Name ,
                    TSPL_PAYMENT_DETAIL.Document_No ,Convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103) As DocuemntDate,
                    'Payment' As DocumentType,TSPL_PAYMENT_HEADER.Payment_Amount As Amount,-1 as RI
                    from TSPL_PAYMENT_HEADER
                    Left Outer Join TSPL_PAYMENT_DETAIL On TSPL_PAYMENT_DETAIL.Payment_No=TSPL_PAYMENT_HEADER.Payment_No
                    Left Outer Join TSPL_VLC_MASTER_HEAD On TSPL_VLC_MASTER_HEAD.VSP_Code=TSPL_PAYMENT_HEADER.Vendor_Code
                    Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code=TSPL_VLC_MASTER_HEAD.MCC
                    Where TSPL_PAYMENT_HEADER.Payment_Type='PY' And TSPL_PAYMENT_HEADER.Posted=1 " '--and Saving=1 "

        If txtBMC.arrValueMember IsNot Nothing AndAlso txtBMC.arrValueMember.Count > 0 Then
            BaseQuery += " and TSPL_MCC_MASTER.MCC_Code in (" + clsCommon.GetMulcallString(txtBMC.arrValueMember) + ") "
        End If
        If txtVSP.arrValueMember IsNot Nothing AndAlso txtVSP.arrValueMember.Count > 0 Then
            BaseQuery += " and TSPL_VLC_MASTER_HEAD.VSP_Code in (" + clsCommon.GetMulcallString(txtVSP.arrValueMember) + ") "
        End If
        'BaseQuery += " and Convert(date,TSPL_PAYMENT_HEADER.Payment_Date,103)<=Convert(date,'" + txtToDate.Value + "',103) "

        FinalQuery = "with CTE as ( "
        FinalQuery += " select ROW_NUMBER() over (Partition by VSP_Code order by VSP_Code,DocuemntDate,DocumentType) as SNo, * "
        FinalQuery += "from (" + BaseQuery + ")xxx )"
        If rbtnSummary.Checked Then
            FinalQuery += "Select Max(xx.SNo)SNo,Max(xx.VLC_Code_VLC_Uploader)VLC_Code_VLC_Uploader,Max(xx.VSP_Code)VSP_Code,Max(VLC_Name)VLC_Name,Max(xx.Document_No)Document_No,Max(xx.DocumentType)DocumentType,Max(xx.DocuemntDate)DocuemntDate,Sum(xx.OPBal)OPBal,Sum(xx.Amount)Amount,( Sum(xx.OPBal)+(Sum(xx.Amount*xx.RI))) As CLBal"
        Else
            FinalQuery += "select xx.SNo,xx.VLC_Code_VLC_Uploader,xx.VSP_Code,xx.VLC_Name,xx.Document_No,xx.DocumentType,xx.DocuemntDate,xx.OPBal,xx.Amount ,( xx.OPBal+(xx.Amount*xx.RI)) as CLBal"
        End If
        FinalQuery+=" from (
                        select  *,(select isnull(sum(Amount*RI),0)  from CTE as CTEInner where CTEInner.VSP_Code=CTE.VSP_Code and  CTEInner.SNo<CTE.SNo ) as OPBal from CTE                         
                        )xx where Convert(date,xx.DocuemntDate,103)>=Convert(date,'" + txtFromDate.Value + "',103) and Convert(date,xx.DocuemntDate,103)<=Convert(date,'" + txtToDate.Value + "',103) "
        If rbtnSummary.Checked Then
            FinalQuery += " Group By xx.VSP_Code"
        End If
        Return FinalQuery
    End Function

    Private Sub btnExportExcel_Click(sender As Object, e As EventArgs) Handles btnExportExcel.Click
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnExportPDF_Click(sender As Object, e As EventArgs) Handles btnExportPDF.Click
        ExportGrid(EnumExportTo.PDF)
    End Sub


    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
                Exit Sub
            End If
            Dim strUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select City_Code from TSPL_COMPANY_MASTER where Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "))
            Dim strHeading As String = ""
            strHeading = objCommonVar.CurrentCompanyName
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("DCS Saving Ledger")
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid(strHeading, gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF(strHeading, gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK)
        End Try
    End Sub
End Class