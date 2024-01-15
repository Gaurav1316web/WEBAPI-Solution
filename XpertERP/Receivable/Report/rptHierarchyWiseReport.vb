Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'================Created by Preeti Gupta ================
' Work done agaist ticket no. UDL/27/06/18-000198  by Parteek
Public Class RptHierarchyWiseReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.rptHierarchyWiseReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnExp.Visible = MyBase.isExport
    End Sub
    Sub Reset()
        ToDate.Value = clsCommon.GETSERVERDATE()
        fromDate.Value = ToDate.Value.AddMonths(-1)
        RadPageView1.SelectedPage = RadPageViewPage1
        RbtnDetail.IsChecked = True
        RdropGroupLevel.SelectedIndex = 0

    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        If fromDate.Value > ToDate.Value Then
            common.clsCommon.MyMessageBoxShow(Me, "From date can not be greater then to Date", Me.Text)
            fromDate.Focus()
            Exit Sub
        End If

        Gv1.ReadOnly = True
        Dim squeryClosing As String = String.Empty
        Dim MainQuery As String = String.Empty
        Dim strWhrClause As String = ""

        strWhrClause += " where 2=2 "
        '' Work done agaist ticket no. UDL/25/05/18-000174
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            ' Ticket No : UDL/29/05/18-000176
            strWhrClause += " and TSPL_JOURNAL_MASTER.segment_code in (" + clsCommon.GetMulcallString(txtLocationMult.arrValueMember) + ")  "
        End If
        If txtHierarchyMult.arrValueMember IsNot Nothing AndAlso txtHierarchyMult.arrValueMember.Count > 0 Then
            strWhrClause += " and TSPL_JOURNAL_DETAILS.hirerachy_code in (" + clsCommon.GetMulcallString(txtHierarchyMult.arrValueMember) + ")  "
        End If

        If txtMultCostFnd.arrValueMember IsNot Nothing AndAlso txtMultCostFnd.arrValueMember.Count > 0 Then
            strWhrClause += " and TSPL_JOURNAL_DETAILS.cost_centre_code in (" + clsCommon.GetMulcallString(txtMultCostFnd.arrValueMember) + ")  "
        End If

        If txtMultAccountCodefnd.arrValueMember IsNot Nothing AndAlso txtMultAccountCodefnd.arrValueMember.Count > 0 Then
            strWhrClause += " and TSPL_JOURNAL_DETAILS.account_code in (" + clsCommon.GetMulcallString(txtMultCostFnd.arrValueMember) + ")  "
        End If
        strWhrClause += " and TSPL_JOURNAL_DETAILS.hirerachy_code is not null"
        strWhrClause += " and  convert(date,TSPL_JOURNAL_MASTER.voucher_date,103)>=convert(date,'" & fromDate.Value & "',103) AND convert(date,TSPL_JOURNAL_MASTER.voucher_date,103)<=convert(date,'" & ToDate.Value & "',103)"

        If chkPosted.CheckState = CheckState.Checked Then
            strWhrClause += " and TSPL_JOURNAL_MASTER.authorized='A' "
        ElseIf chkUnposted.CheckState = CheckState.Checked Then
            strWhrClause += " and TSPL_JOURNAL_MASTER.authorized='N'  "
        Else

        End If
        Dim dtgv As New DataTable
        Dim StrGrpType As String = Nothing
        If RbtnDetail.IsChecked = True Then

            MainQuery = "select convert(varchar,TSPL_JOURNAL_MASTER.voucher_date,103) as [Date],TSPL_JOURNAL_DETAILS.hirerachy_code   as [Hierarchy Code]," & _
                        " TSPL_HIRERACHY_LEVEL_MASTER.description as [Hierarchy Name],TSPL_JOURNAL_DETAILS.cost_centre_code as [Cost Center Code]," & _
                        " TSPL_COST_CENTRE_FINANCIAL.cost_center_fin_Name as [Cost Center Name],TSPL_JOURNAL_MASTER.segment_code as [Location Code],TSPL_GL_SEGMENT_CODE.description as [Location Name]," & _
                        " TSPL_JOURNAL_DETAILS.account_code as [Account Code],TSPL_JOURNAL_DETAILS.account_desc as [Account Description]," & _
                        "  TSPL_JOURNAL_DETAILS.amount as [Amount],TSPL_JOURNAL_MASTER.Voucher_No as [Voucher No],TSPL_JOURNAL_MASTER.source_doc_no as [Source Doc No.],TSPL_JOURNAL_MASTER.source_code as [Source Code]," & _
                        "  TSPL_JOURNAL_MASTER.source_desc as [Source Description], TSPL_JOURNAL_MASTER.custvend_code as [Vendor Code],TSPL_JOURNAL_MASTER.CustVend_Name as [Vendor Name]," & _
                        "  TSPL_JOURNAL_MASTER.source_Narration as [Narration],TSPL_JOURNAL_MASTER.Remarks,TSPL_JOURNAL_MASTER.comments,TSPL_JOURNAL_DETAILS.description As [Detail Description]," & _
                        "  TSPL_JOURNAL_DETAILS.reference as [Detail Reference]" & _
                        " from TSPL_JOURNAL_MASTER " & _
                        " left join  TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No =TSPL_JOURNAL_MASTER.Voucher_No " & _
                        " left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_JOURNAL_DETAILS.Cost_Centre_Code " & _
                        " left join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code=TSPL_JOURNAL_DETAILS.Hirerachy_Code " & _
                         " left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.segment_code=TSPL_JOURNAL_MASTER.segment_code and seg_no=7 " & _
                        " " & strWhrClause & " "
        Else
            MainQuery = "select (TSPL_JOURNAL_DETAILS.hirerachy_code)   as [Hierarchy Code], (TSPL_HIRERACHY_LEVEL_MASTER.description) as [Hierarchy Name],(TSPL_JOURNAL_DETAILS.cost_centre_code) as Cost_Center, (TSPL_COST_CENTRE_FINANCIAL.cost_center_fin_Name) as [Cost Center Name], TSPL_JOURNAL_MASTER.custvend_code as [Vendor Code],TSPL_JOURNAL_MASTER.CustVend_Name as [Vendor Name] ,TSPL_GL_SEGMENT_CODE.description as Location,TSPL_JOURNAL_DETAILS.account_code as [Account Code],TSPL_JOURNAL_DETAILS.account_desc as [Account Description],  (TSPL_JOURNAL_DETAILS.amount) as [Amount] " & _
                         " from TSPL_JOURNAL_MASTER " & _
                         " left join  TSPL_JOURNAL_DETAILS on TSPL_JOURNAL_DETAILS.Voucher_No =TSPL_JOURNAL_MASTER.Voucher_No " & _
                         " left join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code=TSPL_JOURNAL_DETAILS.Cost_Centre_Code " & _
                         " left join TSPL_HIRERACHY_LEVEL_MASTER on TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code=TSPL_JOURNAL_DETAILS.Hirerachy_Code " & _
                          " left join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.segment_code=TSPL_JOURNAL_MASTER.segment_code and seg_no=7 " & _
                         " " & strWhrClause & "  "
        End If
        dtgv = clsDBFuncationality.GetDataTable(MainQuery)

        Gv1.DataSource = Nothing

        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        Gv1.DataSource = dtgv

        Gv1.GroupDescriptors.Clear()
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        If RbtnSummary.IsChecked = True Then
            If RdropGroupLevel.SelectedIndex = 0 Then
                Gv1.GroupDescriptors.Add(New GridGroupByExpression("Location AS Location format ""{0}: {1}"" Group By Location"))
            ElseIf RdropGroupLevel.SelectedIndex = 1 Then
                Gv1.GroupDescriptors.Add(New GridGroupByExpression("Location AS Location format ""{0}: {1}"" Group By Location"))
                Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Hierarchy Name] AS Name format ""{0}: {1}"" Group By [Hierarchy Name]"))
            Else
                Gv1.GroupDescriptors.Add(New GridGroupByExpression("Location AS Location format ""{0}: {1}"" Group By Location"))
                Gv1.GroupDescriptors.Add(New GridGroupByExpression("[Hierarchy Name] AS Name format ""{0}: {1}"" Group By [Hierarchy Name]"))
                Gv1.GroupDescriptors.Add(New GridGroupByExpression("Cost_Center AS Cost_Center format ""{0}: {1}"" Group By Cost_Center"))
            End If

        End If


        If dtgv Is Nothing OrElse dtgv.Rows.Count <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If
        RadPageView1.SelectedPage = RadPageViewPage2

        Gv1.MasterTemplate.AllowAddNewRow = False
        Gv1.BestFitColumns()
        Gv1.AutoExpandGroups = True
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        ReStoreGridLayout()
        Dim arrHeader As List(Of String) = New List(Of String)()
        Dim strTemp As String = ""
        arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptHierarchyWiseReport & "'"))
        arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy") + " ")
        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
       
        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
            arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
        End If
        If txtHierarchyMult.arrValueMember IsNot Nothing AndAlso txtHierarchyMult.arrValueMember.Count > 0 Then
            arrHeader.Add(" Hierarchy : " + clsCommon.GetMulcallStringWithComma(txtHierarchyMult.arrDispalyMember))
        End If

        If txtMultCostFnd.arrValueMember IsNot Nothing AndAlso txtMultCostFnd.arrValueMember.Count > 0 Then
            arrHeader.Add(" Cost Center : " + clsCommon.GetMulcallStringWithComma(txtMultCostFnd.arrDispalyMember))
        End If

        If txtMultAccountCodefnd.arrValueMember IsNot Nothing AndAlso txtMultAccountCodefnd.arrValueMember.Count > 0 Then
            arrHeader.Add(" Account  : " + clsCommon.GetMulcallStringWithComma(txtMultAccountCodefnd.arrDispalyMember))
        End If

        If IsPrint = Exporter.Excel Then
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToExcelGrid("Hierarchy Wise Report", Gv1, arrHeader, Me.Text)
        ElseIf IsPrint = Exporter.PDF Then
            transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Hierarchy Wise Report", Gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
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


    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID + IIf(RbtnSummary.IsChecked = True, "S", "D")
        TemplateGridview = Gv1
        Print(Exporter.Refresh)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    'Private Sub btnQuickExport_Click(sender As Object, e As EventArgs) Handles btnQuickExport.Click
    '    Try

    '        Dim arrHeader As List(Of String) = New List(Of String)()

    '        arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy"))
    '        arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)
    '        arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.rptHierarchyWiseReport & "'"))


    '        Dim sfd As SaveFileDialog = New SaveFileDialog()
    '        Dim filePath As String
    '        sfd.FileName = Me.Text
    '        sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
    '        If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
    '            filePath = sfd.FileName
    '        Else
    '            Exit Sub
    '        End If
    '        If txtLocationMult.arrValueMember IsNot Nothing AndAlso txtLocationMult.arrValueMember.Count > 0 Then
    '            arrHeader.Add(" Location : " + clsCommon.GetMulcallStringWithComma(txtLocationMult.arrDispalyMember))
    '        End If
    '        If txtHierarchyMult.arrValueMember IsNot Nothing AndAlso txtHierarchyMult.arrValueMember.Count > 0 Then
    '            arrHeader.Add(" Hierarchy : " + clsCommon.GetMulcallStringWithComma(txtHierarchyMult.arrDispalyMember))
    '        End If

    '        If txtMultCostFnd.arrValueMember IsNot Nothing AndAlso txtMultCostFnd.arrValueMember.Count > 0 Then
    '            arrHeader.Add(" Cost Center : " + clsCommon.GetMulcallStringWithComma(txtMultCostFnd.arrDispalyMember))
    '        End If

    '        If txtMultAccountCodefnd.arrValueMember IsNot Nothing AndAlso txtMultAccountCodefnd.arrValueMember.Count > 0 Then
    '            arrHeader.Add(" Account  : " + clsCommon.GetMulcallStringWithComma(txtMultAccountCodefnd.arrDispalyMember))
    '        End If
    '        transportSql.exportdataChilRows(Gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
    '        common.clsCommon.MyMessageBoxShow("Exported Successfully.")
    '        Process.Start(filePath)
    '    Catch ex As Exception
    '        common.clsCommon.MyMessageBoxShow(ex.Message)
    '    End Try


    'End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtLocationMult__My_Click(sender As Object, e As EventArgs) Handles txtLocationMult._My_Click
        strQry = " select Location_Code as [Code],Location_Desc as [Name],Loc_Segment_Code as [LocationSegmentCode] from TSPL_Location_MASTER   "
        txtLocationMult.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtLocationMult.arrValueMember, txtLocationMult.arrDispalyMember)
    End Sub

    Private Sub txtHierarchyMult__My_Click(sender As Object, e As EventArgs) Handles txtHierarchyMult._My_Click
        strQry = "select TSPL_HIRERACHY_LEVEL_MASTER.Hirerachy_Code as Code ,TSPL_HIRERACHY_LEVEL_MASTER.Description as Name  from TSPL_HIRERACHY_LEVEL_MASTER "
        txtHierarchyMult.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtHierarchyMult.arrValueMember, txtHierarchyMult.arrDispalyMember)
    End Sub

    Private Sub txtMultCostFnd__My_Click(sender As Object, e As EventArgs) Handles txtMultCostFnd._My_Click
        strQry = "select TSPL_COST_CENTRE_FINANCIAL.cost_center_fin_code as Code, TSPL_COST_CENTRE_FINANCIAL.cost_center_fin_name as Name from TSPL_COST_CENTRE_FINANCIAL "
        txtMultCostFnd.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtMultCostFnd.arrValueMember, txtMultCostFnd.arrDispalyMember)
    End Sub

    Private Sub txtMultAccountCodefnd__My_Click(sender As Object, e As EventArgs) Handles txtMultAccountCodefnd._My_Click
        strQry = "Select Cust_Group_Code as Code, Cust_Group_Desc as Name from TSPL_CUSTOMER_GROUP_MASTER"
        txtMultAccountCodefnd.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSel", strQry, "Code", "Name", txtMultAccountCodefnd.arrValueMember, txtMultAccountCodefnd.arrDispalyMember)
    End Sub

    Private Sub RptHierarchyWiseReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N ")
        Reset()
        RdropGroupLevel.SelectedIndex = 0
    End Sub

    Private Sub RptHierarchyWiseReport_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    'Private Sub RbtnSummary_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles RbtnSummary.ToggleStateChanged
    '    Try
    '        If RbtnSummary.IsChecked = True Then
    '            btnQuickExport.Visible = False
    '        Else
    '            btnQuickExport.Visible = True
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Print(Exporter.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        If (Gv1.Rows.Count <= 0) Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data To Export", Me.Text)
            Exit Sub
        End If
        Print(Exporter.PDF)
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

End Class
