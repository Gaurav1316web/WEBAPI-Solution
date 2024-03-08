Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptPaymentRegister
    Inherits FrmMainTranScreen
    Dim arrLoc As String = Nothing

    Private Sub LOCATIONRIGTHS()
        Try
            Dim obj As New clsMCCCodes()
            obj = clsMCCCodes.GetData()

            If obj.arrLocCodes IsNot Nothing AndAlso clsCommon.myLen(obj.arrLocCodes) > 0 Then
                arrLoc = obj.arrLocCodes
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptMCCShiftReportRouteWise)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
    End Sub
    Private Sub chkBankAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkBankAll.ToggleStateChanged
        cbgBank.Enabled = Not chkBankAll.IsChecked
    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub chkVSPAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkVSPAll.ToggleStateChanged
        cbgVSP.Enabled = Not chkVSPAll.IsChecked
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        Load_Report()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmEXCEL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmEXCEL.Click
        print(EnumExportTo.Excel)
    End Sub

   
    Private Sub rmPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
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

    Private Sub RptPaymentRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadMCC()

        LoadBank()
        LoadVSP()
        Reset()
    End Sub

    Sub LoadMCC()
        Dim qry As String = Nothing
        Dim dt As DataTable = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then
            qry = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER where MCC_Code in (" + arrLoc + ") "
            dt = clsDBFuncationality.GetDataTable(qry)
        End If

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            btnGo.Enabled = False
        Else
            cbgMCC.DataSource = dt
            cbgMCC.ValueMember = "Code"
            cbgMCC.DisplayMember = "Name"
        End If

    End Sub
    Sub LoadVSP()
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP' "
        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Code"
        cbgVSP.DisplayMember = "Name"

    End Sub
    Sub LoadBank()
        Dim qry As String = "select BANK_CODE as [Code] ,DESCRIPTION as [Name] from TSPL_BANK_MASTER "
        cbgBank.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgBank.ValueMember = "Code"
        cbgBank.DisplayMember = "Name"

    End Sub

    Public Sub Load_Report()
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        If chkBankSelect.IsChecked AndAlso cbgBank.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Bank or select all.", Me.Text)
            Exit Sub
        End If
       
        If chkVSPSelect.IsChecked AndAlso cbgVSP.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single VSP or select all.", Me.Text)
            Exit Sub
        End If
        Dim sQuery As String = "select TSPL_VENDOR_MASTER.Vendor_Code as VSP_CODE,Vendor_Name as Vsp_name,TSPL_MILK_RECEIPT_DETAIL.VLC_CODE+', Name - '+VLC_Name as VLC_CODE,TSPL_VLC_MASTER_HEAD.VLC_Name,convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) as shift_date ,case when TSPL_MILK_RECEIPT_HEAD.SHIFT='M' then 'Morning'  else 'Evening' end as  Shift_type,TSPL_MILK_RECEIPT_DETAIL.TYPE,TSPL_MILK_RECEIPT_DETAIL.SAMPLE_NO,MILK_WEIGHT as Qty,TSPL_MILK_SAMPLE_DETAIL.FAT,TSPL_MILK_SAMPLE_DETAIL.SNF,convert(decimal(18,3),TSPL_MILK_SAMPLE_DETAIL.FAT*Qty /100) as Fat_KG,convert(Decimal(18,3),TSPL_MILK_SAMPLE_DETAIL.snf*Qty /100) as SNF_KG,RATE,amount,TSPL_MILK_RECEIPT_DETAIL.ROUTE_CODE +', Name -'+Route_name as ROUTE_CODE,TSPL_mcc_ROUTE_MASTER.Route_name,TSPL_VLC_MASTER_HEAD.Village_Code,Village_Name,TSPL_MILK_RECEIPT_DETAIL.MCC_Code+' Name- '+MCC_NAME as MCC_Code ,TSPL_MCC_MASTER .MCC_NAME  "
        sQuery += " from TSPL_MILK_RECEIPT_DETAIL   inner join TSPL_MILK_RECEIPT_HEAD  on TSPL_MILK_RECEIPT_HEAD.doc_code=TSPL_MILK_RECEIPT_DETAIL.DOC_CODE     inner join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER.Form_Type='VSP' and TSPL_MILK_RECEIPT_DETAIL .VSP_CODE =TSPL_VENDOR_MASTER .Vendor_Code left join (select FAT,snf,VLC_DOC_CODE,milk_receipt_code,Qty,RATE,AMOUNT from TSPL_MILK_SAMPLE_DETAIL inner join TSPL_MILK_SAMPLE_HEAD on TSPL_MILK_SAMPLE_DETAIL.DOC_CODE=TSPL_MILK_SAMPLE_HEAD.DOC_CODE) TSPL_MILK_SAMPLE_DETAIL on  TSPL_MILK_receipt_DETAIL.VLC_DOC_CODE=TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE  and TSPL_MILK_SAMPLE_DETAIL.MILK_RECEIPT_CODE=TSPL_MILK_RECEIPT_HEAD.DOC_CODE"
        sQuery += " AND TSPL_MILK_SAMPLE_DETAIL.VLC_DOC_CODE=TSPL_MILK_RECEIPT_DETAIL.VLC_DOC_CODE left join TSPL_mcc_ROUTE_MASTER  on TSPL_mcc_ROUTE_MASTER.Route_code=TSPL_MILK_RECEIPT_DETAIL.route_code left join TSPL_VLC_MASTER_HEAD  on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MILK_RECEIPT_DETAIL.VLC_CODE "
        sQuery += "left join TSPL_VILLAGE_MASTER vlm on vlm.Village_Code=TSPL_VLC_MASTER_HEAD.Village_Code left join TSPL_MCC_MASTER  on TSPL_MCC_MASTER.MCC_Code= TSPL_MILK_RECEIPT_HEAD.MCC_CODE where 2=2 "

        sQuery += " and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_MILK_RECEIPT_HEAD.DOC_DATE,103) <=convert(date,'" + txtToDate.Value + "' ,103)"

        If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
            sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
        End If
        If chkBankSelect.IsChecked And cbgBank.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_MILK_RECEIPT_DETAIL.VLC_CODE in (" + clsCommon.GetMulcallString(cbgBank.CheckedValue) + ")  "
        End If
      
        If chkVSPSelect.IsChecked And cbgVSP.CheckedValue.Count > 0 Then
            sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
        End If
        sQuery += "order by shift_date,Shift_type"

        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub

    Sub FormatGrid()
        'Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False
            'If chkcatewise.Checked AndAlso ii > 18 Then
            '    gv.Columns(ii).IsVisible = True
            '    gv.Columns(ii).Width = 100
            'End If
        Next

        gv.Columns("VSP_CODE").IsVisible = True
        gv.Columns("VSP_CODE").Width = 100
        gv.Columns("VSP_CODE").HeaderText = " VSP Code"



        gv.Columns("Vsp_name").IsVisible = True
        gv.Columns("Vsp_name").Width = 100
        gv.Columns("Vsp_name").HeaderText = " VSP Name"

        gv.Columns("shift_date").IsVisible = True
        gv.Columns("shift_date").Width = 100
        gv.Columns("shift_date").HeaderText = " Date"
        gv.Columns("shift_date").FormatString = "{0:d}"

        gv.Columns("BANK_CODE").IsVisible = False
        gv.Columns("BANK_CODE").Width = 100
        gv.Columns("BANK_CODE").HeaderText = " VLC Code"

        gv.Columns("DESCRIPTION").IsVisible = False
        gv.Columns("DESCRIPTION").Width = 100
        gv.Columns("DESCRIPTION").HeaderText = "VLC Name"

        gv.Columns("Shift_type").IsVisible = True
        gv.Columns("Shift_type").Width = 80
        gv.Columns("Shift_type").HeaderText = "Shift"

        gv.Columns("TYPE").IsVisible = True
        gv.Columns("TYPE").Width = 80
        gv.Columns("TYPE").HeaderText = "Type"

        gv.Columns("SAMPLE_NO").IsVisible = True
        gv.Columns("SAMPLE_NO").Width = 50
        gv.Columns("SAMPLE_NO").HeaderText = "Sample No"

        gv.Columns("Qty").IsVisible = True
        gv.Columns("Qty").Width = 100
        gv.Columns("Qty").HeaderText = "Quantity"

        gv.Columns("FAT").IsVisible = True
        gv.Columns("FAT").Width = 100
        gv.Columns("FAT").HeaderText = "FAT"

        gv.Columns("SNF").IsVisible = True
        gv.Columns("SNF").Width = 100
        gv.Columns("SNF").HeaderText = "SNF"

        gv.Columns("Fat_KG").IsVisible = True
        gv.Columns("Fat_KG").Width = 100
        gv.Columns("Fat_KG").HeaderText = "TFAT"

        gv.Columns("SNF_KG").IsVisible = True
        gv.Columns("SNF_KG").Width = 100
        gv.Columns("SNF_KG").HeaderText = "TSNF"

        gv.Columns("RATE").IsVisible = True
        gv.Columns("RATE").Width = 100
        gv.Columns("RATE").HeaderText = "Rate"

        gv.Columns("amount").IsVisible = True
        gv.Columns("amount").Width = 100
        gv.Columns("amount").HeaderText = "Amount"

        gv.Columns("Village_Code").IsVisible = False
        gv.Columns("Village_Code").Width = 100
        gv.Columns("Village_Code").HeaderText = "Village Code"

        gv.Columns("Village_Name").IsVisible = False
        gv.Columns("Village_Name").Width = 100
        gv.Columns("Village_Name").HeaderText = "Village Name"

        gv.Columns("ROUTE_CODE").IsVisible = False
        gv.Columns("ROUTE_CODE").Width = 100
        gv.Columns("ROUTE_CODE").HeaderText = "Route Code"

        gv.Columns("Village_Name").IsVisible = False
        gv.Columns("Village_Name").Width = 100
        gv.Columns("Village_Name").HeaderText = "Route Name"

        gv.Columns("MCC_Code").IsVisible = False
        gv.Columns("MCC_Code").Width = 100
        gv.Columns("MCC_Code").HeaderText = "MCC Code"
        'Try
        '    gv.Columns("Category").IsVisible = True
        '    gv.Columns("Category").Width = 200
        '    gv.Columns("Category").HeaderText = "Category"
        'Catch exx As Exception
        'End Try

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("FAT", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("SNF", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Fat_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)
        Dim item5 As New GridViewSummaryItem("SNF_KG", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item5)
        Dim item6 As New GridViewSummaryItem("RATE", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item6)
        Dim item7 As New GridViewSummaryItem("amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item7)
        gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))
        gv.GroupDescriptors.Add(New GridGroupByExpression("BANK_CODE as Item format ""{0}: {1}"" Group By BANK_CODE"))

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadMCC()
        LoadBank()
        LoadVSP()
        chkMCCAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        chkBankAll.CheckState = CheckState.Checked

        chkVSPAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1


    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
            If chkMCCSelect.IsChecked Then
                Dim strMCCName As String = ""
                For Each StrName As String In cbgMCC.CheckedDisplayMember
                    If clsCommon.myLen(strMCCName) > 0 Then
                        strMCCName += ", "
                    End If
                    strMCCName += StrName
                Next
                Dim strMCCCode As String = ""
                For Each StrCode As String In cbgMCC.CheckedValue
                    If clsCommon.myLen(strMCCCode) > 0 Then
                        strMCCCode += ", "
                    End If
                    strMCCCode += StrCode
                Next


                arrHeader.Add((" MCC Name: " + strMCCName + " "))
            End If
            If chkVSPSelect.IsChecked Then
                Dim stVSPName As String = ""
                For Each StrName As String In cbgVSP.CheckedDisplayMember
                    If clsCommon.myLen(stVSPName) > 0 Then
                        stVSPName += ", "
                    End If
                    stVSPName += StrName
                Next
                Dim strVSPCode As String = ""
                For Each StrCode As String In cbgVSP.CheckedValue
                    If clsCommon.myLen(strVSPCode) > 0 Then
                        strVSPCode += ", "
                    End If
                    strVSPCode += StrCode
                Next
                arrHeader.Add(("VSP Name: " + stVSPName + " "))
            End If
            If chkBankSelect.IsChecked Then
                Dim stBankName As String = ""
                For Each StrName As String In cbgBank.CheckedDisplayMember
                    If clsCommon.myLen(stBankName) > 0 Then
                        stBankName += ", "
                    End If
                    stBankName += StrName
                Next
                Dim strBankCode As String = ""
                For Each StrCode As String In cbgBank.CheckedValue
                    If clsCommon.myLen(strBankCode) > 0 Then
                        strBankCode += ", "
                    End If
                    strBankCode += StrCode
                Next
                arrHeader.Add(("VSP Name: " + stBankName + " "))
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Milk Shift Report (Route Wise)", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Milk Shift Report (Route Wise)", gv, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    
End Class
