Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO

Public Class RptVSPItemIssue
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim tmpValLoad As Boolean = True
    'preeti gupta ticket no.[]
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptVSPItemIssue)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub RptVSPItemIssue_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown

    End Sub

    Private Sub RptVSPItemIssue_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        rdbBoth.IsChecked = True
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        'ButtonToolTip.SetToolTip(btnPrint, "Press Alt+P For Print")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        'LoadMCC()
        'LoadVSP()
        Reset()
    End Sub
    Sub Reset()
        'txtToDate.Value = clsCommon.GETSERVERDATE()
        'txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LOCATIONRIGTHS()
        rbtnSummary.CheckState = CheckState.Checked
        LoadMCC()
        LoadVSP()
        chkMCCAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        chkVSPAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Sub LoadVSP()
        Dim qry As String = "select Vendor_Code as [Code] ,Vendor_Name as [Name]  from TSPL_VENDOR_MASTER  where Form_Type ='VSP' "
        cbgVSP.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgVSP.ValueMember = "Code"
        cbgVSP.DisplayMember = "Name"

    End Sub
    Sub LoadMCC()
        'Dim qry As String = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER  "
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
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
    Public Sub Load_Report()
        Dim sQuery As String
        'Dim companyADD, CompName, CompCode As String
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If
        If chkMCCSelect.IsChecked AndAlso cbgMCC.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single MCC or select all.", Me.Text)
            Exit Sub
        End If
        
        If chkVSPSelect.IsChecked AndAlso cbgVSP.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please select atleast single VSP or select all.", Me.Text)
            Exit Sub
        End If
      


        'sQuery = ""
        'sQuery += " select   TSPL_COMPANY_MASTER.add1 +case when len(TSPL_COMPANY_MASTER.add2)>0 then ', '+TSPL_COMPANY_MASTER.add2 else '' end +case when LEN(isnull(TSPL_COMPANY_MASTER.Add3,''))>0 then ', '+isnull(TSPL_COMPANY_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_COMPANY_MASTER.City_Code)>0 then ', '+TSPL_COMPANY_MASTER.City_Code else ' ' end + case when len(TSPL_COMPANY_MASTER.State )>0 then TSPL_COMPANY_MASTER.State else '' end  as comp_address from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        'Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        'companyADD = dt1.Rows(0).Item("comp_address")

        'sQuery = ""
        'sQuery += " select   TSPL_COMPANY_MASTER.Comp_Name  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        'Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        'CompName = dt2.Rows(0).Item("Comp_Name")


        'sQuery = ""
        'sQuery += " select   TSPL_COMPANY_MASTER.comp_code  from TSPL_COMPANY_MASTER where  Comp_Code = '" + objCommonVar.CurrentCompanyCode + "' "
        'Dim dt5 As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        'CompCode = dt5.Rows(0).Item("Comp_Code")

        Dim fromDate As String = txtFromDate.Value
        Dim Todate As String = txtToDate.Value

        sQuery = ""

        If rbtnSummary.IsChecked = True Then
            sQuery += "select  Max(convert(varchar,final.Doc_Date,103)) As Doc_Date,max(final .[MCC Code] ) as [MCC Code], Max(final .[MCC Name] ) As [MCC Name],max(final.VSP) as VSP_Code, Max(final.[VSP Name] ) As [VSP Name],max(final.[Item Code]) as [Item Code],max(final.[Item Desc]) as [Item Desc] ,max(final.Doc_Type) as Doc_Type,isnull(sum(Issued_Qty),0) as Issued_Qty,isnull(sum(Issued_Qty_againstret),0) as Issued_Qty_againstret,isnull(sum(final.Amount),0) as Amount,Convert(decimal(18,2),case when sum(Balance_Qty)=0 then 0 else (sum(final.Amount) / sum(Balance_Qty)) end) As Cost,sum(Balance_Qty) as Balance_Qty from (select Max(TSPL_VSPItem_HEAD.Doc_Date) As Doc_Date,    Max(TSPL_VSPItem_HEAD.From_Location) As MCC,    (TSPL_VSPItem_HEAD.Issue_To) As VSP,    Max(TSPL_VENDOR_MASTER.Vendor_Name) As [VSP Name],    Max(TSPL_VSPItem_HEAD.Doc_Date) As Date,    Max(TSPL_VSPItem_HEAD.From_Location) As [MCC Code],    Max(TSPL_MCC_MASTER.MCC_NAME) As [MCC Name],    (TSPL_VSPItem_DETAIL.Item_Code) As [Item Code],    Max(TSPL_ITEM_MASTER.Item_Desc) As [Item Desc],  case when Doc_Type ='Issue' then sum(TSPL_VSPItem_DETAIL.Issued_Qty) end as Issued_Qty  ,case when Doc_Type ='Return' then sum(TSPL_VSPItem_DETAIL.Issued_Qty_againstret) end as Issued_Qty_againstret, case when Doc_Type ='Issue' then isnull(sum(TSPL_VSPItem_DETAIL.Issued_Qty),0) else 0 end- case when Doc_Type ='Return' then isnull(sum(TSPL_VSPItem_DETAIL.Issued_Qty_againstret),0) else 0 end as Balance_Qty ,max(Doc_Type) as Doc_Type,max(Issue_To)  as Issue_To ,case when Doc_Type ='Issue' then Sum(TSPL_VSPItem_DETAIL.Amount) else (-1)*Sum(TSPL_VSPItem_DETAIL.Amount) end As Amount  from TSPL_VSPItem_DETAIL "
            sQuery += "left outer join  TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD.Doc_No =TSPL_VSPItem_DETAIL.Doc_No "
            sQuery += "Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code =      TSPL_VSPItem_HEAD.From_Location    Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code =      TSPL_VSPItem_HEAD.Issue_To And TSPL_VENDOR_MASTER.Form_Type = 'VSP'    Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code =      TSPL_VSPItem_DETAIL.Item_Code"
            sQuery += "   where 2 = 2 and TSPL_VSPItem_HEAD.status=1 and convert(date,Doc_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Doc_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
            If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
                sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
            If chkVSPSelect.IsChecked And cbgVSP.CheckedValue.Count > 0 Then
                sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
            End If
            sQuery += "group by TSPL_MCC_MASTER.MCC_Code,    TSPL_VSPItem_DETAIL.Item_Code,Doc_Type ,Issue_To ) as final group by Issue_To,[Item Code] "
            sQuery += "  order by max(convert(date,doc_date,103))"

        ElseIf rbtnDetail.IsChecked = True Then
            sQuery += "select TSPL_VSPItem_HEAD.Doc_No,TSPL_VSPItem_HEAD. Doc_Date,    (TSPL_VSPItem_HEAD.From_Location) As MCC,    (TSPL_VSPItem_HEAD.Issue_To) As VSP,    (TSPL_VENDOR_MASTER.Vendor_Name) As [VSP Name],       (TSPL_VSPItem_HEAD.From_Location) As [MCC Code],    (TSPL_MCC_MASTER.MCC_NAME) As [MCC Name],   "
            sQuery += "  (TSPL_VSPItem_DETAIL.Item_Code) As [Item Code],    (TSPL_ITEM_MASTER.Item_Desc) As [Item Desc],  case when Doc_Type ='Issue' then (TSPL_VSPItem_DETAIL.Issued_Qty) end as Issued_Qty  ,case when Doc_Type ='Return' then (TSPL_VSPItem_DETAIL.Issued_Qty_againstret) end as Issued_Qty_againstret,( case when Doc_Type ='Issue' then isnull(TSPL_VSPItem_DETAIL.Issued_Qty,0) else 0 end)-(case when Doc_Type ='Return' then isnull(TSPL_VSPItem_DETAIL.Issued_Qty_againstret,0)else 0 end) as Balance_Qty,(Doc_Type) as Doc_Type,(Issue_To)  "
            sQuery += " as Issue_To ,case when Doc_Type ='Issue' then TSPL_VSPItem_DETAIL.Amount else (-1)*Amount end As Amount,convert(decimal(18,2),case when (Issued_Qty)=0 then 0 else (Amount / Issued_Qty) end) As Cost from TSPL_VSPItem_DETAIL left outer join  TSPL_VSPItem_HEAD on TSPL_VSPItem_HEAD.Doc_No =TSPL_VSPItem_DETAIL.Doc_No Left Outer Join TSPL_MCC_MASTER On TSPL_MCC_MASTER.MCC_Code =      TSPL_VSPItem_HEAD.From_Location   "
            sQuery += " Left Outer Join TSPL_VENDOR_MASTER On TSPL_VENDOR_MASTER.Vendor_Code =      TSPL_VSPItem_HEAD.Issue_To And TSPL_VENDOR_MASTER.Form_Type = 'VSP'    Left Outer Join TSPL_ITEM_MASTER On TSPL_ITEM_MASTER.Item_Code =      TSPL_VSPItem_DETAIL.Item_Code   where 2 = 2 and TSPL_VSPItem_HEAD.status=1 and convert(date,Doc_Date,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,Doc_Date,103) <=convert(date,'" + txtToDate.Value + "' ,103) "
            If cbgMCC.CheckedValue.Count > 0 Then 'chkMCCSelect.IsChecked And
                sQuery += "and TSPL_MCC_MASTER.MCC_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") "
            End If
            If chkVSPSelect.IsChecked And cbgVSP.CheckedValue.Count > 0 Then
                sQuery += " and TSPL_VENDOR_MASTER.Vendor_Code  in (" + clsCommon.GetMulcallString(cbgVSP.CheckedValue) + ")"
            End If
            sQuery += "  order by (convert(date,doc_date,103))"


        End If

        '================================================


        ' Dim dtgv As New DataTable
        Dim dtgv As DataTable = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            'If btnReferesh = False Then
            '    MilkProcurementReportViewer.funreport(dtgv, "MCCShiftReport(RouteWise)", "Milk Shift Report (Route Wise)")
            'End If

            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            'tmpValLoad = False
            clsCommon.MyMessageBoxShow(Me, "No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()


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
        If rbtnSummary.IsChecked = True Then
            'gv.Columns("Doc_Date").IsVisible = False
            'gv.Columns("Doc_Date").Width = 100
            'gv.Columns("Doc_Date").HeaderText = " Date"
            'gv.Columns("Doc_Date").FormatString = "{0:d}"

            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = " MCC Code"



            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = " MCC Name"

            gv.Columns("VSP_Code").IsVisible = True
            gv.Columns("VSP_Code").Width = 100
            gv.Columns("VSP_Code").HeaderText = " VSP Code"

            gv.Columns("VSP Name").IsVisible = True
            gv.Columns("VSP Name").Width = 100
            gv.Columns("VSP Name").HeaderText = "VSP Name"

            gv.Columns("Item Code").IsVisible = True
            gv.Columns("Item Code").Width = 80
            gv.Columns("Item Code").HeaderText = "Item Code"

            gv.Columns("Item Desc").IsVisible = True
            gv.Columns("Item Desc").Width = 150
            gv.Columns("Item Desc").HeaderText = "Item Desc"

            'gv.Columns("Doc_Type").IsVisible = False
            'gv.Columns("Doc_Type").Width = 50
            'gv.Columns("Doc_Type").HeaderText = "Doc Type"





            gv.Columns("Amount").IsVisible = True
            gv.Columns("Amount").Width = 100
            gv.Columns("Amount").HeaderText = "Amount"

            gv.Columns("Balance_Qty").IsVisible = True
            gv.Columns("Balance_Qty").Width = 100
            gv.Columns("Balance_Qty").HeaderText = "Balance Qty"

            gv.Columns("Cost").IsVisible = True
            gv.Columns("Cost").Width = 100
            gv.Columns("Cost").HeaderText = "Cost"

            If rdbIssue.IsChecked = True Then
                gv.Columns("Issued_Qty").IsVisible = True
                gv.Columns("Issued_Qty").Width = 100
                gv.Columns("Issued_Qty").HeaderText = "Issued Qty"
            End If
            If rdbReturn.IsChecked = True Then
                gv.Columns("Issued_Qty_againstret").IsVisible = True
                gv.Columns("Issued_Qty_againstret").Width = 100
                gv.Columns("Issued_Qty_againstret").HeaderText = "Return Qty"
            End If
            If rdbBoth.IsChecked = True Then
                gv.Columns("Issued_Qty").IsVisible = True
                gv.Columns("Issued_Qty").Width = 100
                gv.Columns("Issued_Qty").HeaderText = "Issued Qty"
                gv.Columns("Issued_Qty_againstret").IsVisible = True
                gv.Columns("Issued_Qty_againstret").Width = 100
                gv.Columns("Issued_Qty_againstret").HeaderText = "Return Qty"
            End If
        End If
        '==============
        If rbtnDetail.IsChecked = True Then
            gv.Columns("Doc_No").IsVisible = True
            gv.Columns("Doc_No").Width = 100
            gv.Columns("Doc_No").HeaderText = "Document No."


            gv.Columns("Doc_Date").IsVisible = True
            gv.Columns("Doc_Date").Width = 100
            gv.Columns("Doc_Date").HeaderText = " Date"
            gv.Columns("Doc_Date").FormatString = "{0:d}"

            gv.Columns("MCC Code").IsVisible = True
            gv.Columns("MCC Code").Width = 100
            gv.Columns("MCC Code").HeaderText = " MCC Code"



            gv.Columns("MCC Name").IsVisible = True
            gv.Columns("MCC Name").Width = 100
            gv.Columns("MCC Name").HeaderText = " MCC Name"

            gv.Columns("VSP").IsVisible = True
            gv.Columns("VSP").Width = 100
            gv.Columns("VSP").HeaderText = " VSP Code"

            gv.Columns("VSP Name").IsVisible = True
            gv.Columns("VSP Name").Width = 100
            gv.Columns("VSP Name").HeaderText = "VSP Name"

            gv.Columns("Item Code").IsVisible = True
            gv.Columns("Item Code").Width = 80
            gv.Columns("Item Code").HeaderText = "Item Code"

            gv.Columns("Item Desc").IsVisible = True
            gv.Columns("Item Desc").Width = 150
            gv.Columns("Item Desc").HeaderText = "Item Desc"

            gv.Columns("Doc_Type").IsVisible = False
            gv.Columns("Doc_Type").Width = 50
            gv.Columns("Doc_Type").HeaderText = "Doc Type"

            gv.Columns("Balance_Qty").IsVisible = True
            gv.Columns("Balance_Qty").Width = 100
            gv.Columns("Balance_Qty").HeaderText = "Balance Qty"


            gv.Columns("Amount").IsVisible = True
            gv.Columns("Amount").Width = 100
            gv.Columns("Amount").HeaderText = "Amount"

            gv.Columns("Cost").IsVisible = True
            gv.Columns("Cost").Width = 100
            gv.Columns("Cost").HeaderText = "Cost"

            If rdbIssue.IsChecked = True Then
                gv.Columns("Issued_Qty").IsVisible = True
                gv.Columns("Issued_Qty").Width = 100
                gv.Columns("Issued_Qty").HeaderText = "Issued Qty"
            End If
            If rdbReturn.IsChecked = True Then
                gv.Columns("Issued_Qty_againstret").IsVisible = True
                gv.Columns("Issued_Qty_againstret").Width = 100
                gv.Columns("Issued_Qty_againstret").HeaderText = "Return Qty"
            End If
            If rdbBoth.IsChecked = True Then
                gv.Columns("Issued_Qty").IsVisible = True
                gv.Columns("Issued_Qty").Width = 100
                gv.Columns("Issued_Qty").HeaderText = "Issued Qty"
                gv.Columns("Issued_Qty_againstret").IsVisible = True
                gv.Columns("Issued_Qty_againstret").Width = 100
                gv.Columns("Issued_Qty_againstret").HeaderText = "Return Qty"
            End If
        End If
        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim intCount As Integer = 0

        Dim item1 As New GridViewSummaryItem("Issued_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item1)
        Dim item2 As New GridViewSummaryItem("Issued_Qty_againstret", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item2)
        Dim item3 As New GridViewSummaryItem("Balance_Qty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item3)
        Dim item4 As New GridViewSummaryItem("Amount", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(item4)

        gv.ShowGroupPanel = False
        gv.MasterTemplate.AutoExpandGroups = True

        gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub
    'Sub print(ByVal exporter As EnumExportTo)
    '    Try
    '        Dim arrHeader As List(Of String) = New List(Of String)()
    '        Dim CompName As String = clsDBFuncationality.getSingleValue("Select Comp_Name from TSPL_COMPANY_MASTER Where Comp_Code='" + objCommonVar.CurrentCompanyCode + "'")
    '        arrHeader.Add(CompName)
    '        arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
    '        If chkMCCSelect.IsChecked Then
    '            Dim strMCCName As String = ""
    '            For Each StrName As String In cbgMCC.CheckedDisplayMember
    '                If clsCommon.myLen(strMCCName) > 0 Then
    '                    strMCCName += ", "
    '                End If
    '                strMCCName += StrName
    '            Next
    '            Dim strMCCCode As String = ""
    '            For Each StrCode As String In cbgMCC.CheckedValue
    '                If clsCommon.myLen(strMCCCode) > 0 Then
    '                    strMCCCode += ", "
    '                End If
    '                strMCCCode += StrCode
    '            Next
    '            arrHeader.Add((" MCC Name: " + strMCCName + " "))
    '        End If


    '        If chkVSPSelect.IsChecked Then
    '            Dim stVSPName As String = ""
    '            For Each StrName As String In cbgVSP.CheckedDisplayMember
    '                If clsCommon.myLen(stVSPName) > 0 Then
    '                    stVSPName += ", "
    '                End If
    '                stVSPName += StrName
    '            Next
    '            Dim strVSPCode As String = ""
    '            For Each StrCode As String In cbgVSP.CheckedValue
    '                If clsCommon.myLen(strVSPCode) > 0 Then
    '                    strVSPCode += ", "
    '                End If
    '                strVSPCode += StrCode
    '            Next
    '            arrHeader.Add(("VSP Name: " + stVSPName + " "))
    '        End If

    '        If exporter = EnumExportTo.Excel Then
    '            clsCommon.MyExportToExcelGrid("VSP ITEM ISSUE REPORT", gv, arrHeader, Me.Text)
    '        Else
    '            clsCommon.MyExportToPDF("VSP ITEM ISSUE REPORT", gv, arrHeader, Me.Text, True)
    '        End If
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
    '    End Try
    'End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
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

                If exporter = EnumExportTo.Excel Then
                    'Dim sfd As SaveFileDialog = New SaveFileDialog()
                    'Dim filePath As String
                    'sfd.FileName = Me.Text
                    'sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    'If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                    '    filePath = sfd.FileName
                    'Else
                    '    Exit Sub
                    'End If
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("VSP ITEM ISSUE REPORT", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(rbtnSummary.IsChecked = True, "S", "D")
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
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

    Private Sub rmDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")

    End Sub

    Private Sub rmSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
