Imports common
Imports System.IO
Imports System.Net
Imports System.Net.Configuration
Imports System.Net.Mail
Imports System.Net.WebClient
Imports System.Xml
Imports System.Text.RegularExpressions
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Shared
'================Create by Sanjeet(11/01/2018) Gupta=========
Public Class FrmCostCentreConsumptionRpt
    Inherits FrmMainTranScreen
    Dim SelFromDate As String = Nothing
    Dim SelToDate As String = Nothing
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        RadSplitButton1.Visible = MyBase.isExport
    End Sub

    Private Sub FrmCostCentreConsumptionRpt_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        Reset()
    End Sub


    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        Dim qry As String = "Select Code,Description as Name from TSPL_COST_CENTER_TYPE_MASTER WHERE Department in(" + clsCommon.GetMulcallString(txtDepartment.arrValueMember) + ")"
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub

    Private Sub txtLocation__My_Click(sender As Object, e As EventArgs) Handles txtLocation._My_Click
        'Dim qry As String = " select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Name from TSPL_COST_CENTRE_HIRERACHY_DETAIL" + Environment.NewLine & _
        '                    " left outer join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code where TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL=1 "
        'qry += " ORDER BY TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE "
        Dim qry As String = "select Location_Code as Code,Location_Desc as Name from TSPL_LOCATION_MASTER where  Location_Type='Physical' "
        txtLocation.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtLocation.arrValueMember, txtLocation.arrDispalyMember)
    End Sub

    Private Sub txtplantUnit__My_Click(sender As Object, e As EventArgs) Handles txtplantUnit._My_Click
        'Dim qry As String = " select tspl_cost_centre_hirerachy_detail.cost_centre_hirerachy_code as code,tspl_cost_centre_financial.cost_center_fin_name as name from tspl_cost_centre_hirerachy_detail" + Environment.NewLine & _
        '                        " left outer join tspl_cost_centre_financial on tspl_cost_centre_hirerachy_detail.cost_centre_hirerachy_code=tspl_cost_centre_financial.cost_center_fin_code where tspl_cost_centre_hirerachy_detail.hirerachy_level=2 "
        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '    qry += " and tspl_cost_centre_hirerachy_detail.hirerachy_level_code1 in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        'End If
        'qry += " order by tspl_cost_centre_hirerachy_detail.cost_centre_hirerachy_code "
        Dim qry As String = " Select Code,Description as Name from TSPL_COST_CENTER_UNIT_MASTER "
        txtplantUnit.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtplantUnit.arrValueMember, txtplantUnit.arrDispalyMember)
    End Sub

    Private Sub txtDepartment__My_Click(sender As Object, e As EventArgs) Handles txtDepartment._My_Click
        'Dim qry As String = " select TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE as Code,TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Name as Name from TSPL_COST_CENTRE_HIRERACHY_DETAIL" + Environment.NewLine & _
        '                " left outer join TSPL_COST_CENTRE_FINANCIAL on TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE=TSPL_COST_CENTRE_FINANCIAL.Cost_Center_Fin_Code where TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL=3 "
        'If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
        '    qry += " AND TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE1 in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")"
        'End If
        'If txtplantUnit.arrValueMember IsNot Nothing AndAlso txtplantUnit.arrValueMember.Count > 0 Then
        '    qry += " AND TSPL_COST_CENTRE_HIRERACHY_DETAIL.HIRERACHY_LEVEL_CODE2 in(" + clsCommon.GetMulcallString(txtplantUnit.arrValueMember) + ")"
        'End If
        'qry += " ORDER BY TSPL_COST_CENTRE_HIRERACHY_DETAIL.COST_CENTRE_HIRERACHY_CODE "
        Dim qry As String = " select Segment_code as Code,Description as Name from TSPL_GL_SEGMENT_CODE where Seg_No=3 "
        txtDepartment.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulSel", qry, "Code", "Name", txtDepartment.arrValueMember, txtDepartment.arrDispalyMember)
    End Sub
    Private Sub Reset()
        txtfDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtLocation.arrValueMember = Nothing
        txtplantUnit.arrValueMember = Nothing
        txtDepartment.arrValueMember = Nothing
        txtItemType.arrValueMember = Nothing
        gvData.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub

    Private Sub Print(ByVal exporter As EnumExportTo)
        Try
            If gvData.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtfDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.FrmCostCentreConsumptionRpt & "'"))
                If txtLocation.arrDispalyMember IsNot Nothing AndAlso txtLocation.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Location : " + clsCommon.GetMulcallStringWithComma(txtLocation.arrDispalyMember))
                End If
                If txtplantUnit.arrDispalyMember IsNot Nothing AndAlso txtplantUnit.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Plant : " + clsCommon.GetMulcallStringWithComma(txtplantUnit.arrDispalyMember))
                End If
                If txtDepartment.arrDispalyMember IsNot Nothing AndAlso txtDepartment.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Department : " + clsCommon.GetMulcallStringWithComma(txtDepartment.arrDispalyMember))
                End If
                If txtItemType.arrDispalyMember IsNot Nothing AndAlso txtItemType.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item Type : " + clsCommon.GetMulcallStringWithComma(txtItemType.arrDispalyMember))
                End If
                If exporter = EnumExportTo.Excel Then

                    Dim sfd As SaveFileDialog = New SaveFileDialog()
                    Dim filePath As String
                    sfd.FileName = Me.Text
                    sfd.Filter = "Excel 97-2003 (*.xls) |*.xls;|Excel 2007 (*.xlsx)|*.xlsx;|CSV Files (*.csv) |*.csv"
                    If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        filePath = sfd.FileName
                    Else
                        Exit Sub
                    End If
                    clsCommon.MyExportToExcelGrid(Me.Text, gvData, arrHeader, Me.Text)
                    'Dim Export As New ExportToExcelML(gvData)
                    'Export.RunExport(filePath)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    clsCommon.MyExportToPDF(Me.Text, gvData, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        gvData.DataSource = Nothing
        gvData.Rows.Clear()

        Dim strFromDate As String = clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy")
        Dim strToDate As String = clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy")
        Dim StrQuery As String = Nothing
        ' Ticket No : BHA/22/11/18-000697 By Prabhakar ==================
        Dim strRecordExistOrNot As String = " select Count (*)  from TSPL_ISSUERETURN_HEAD " &
                                            " LEFT OUTER JOIN TSPL_ISSUERETURN_DETAIL ON TSPL_ISSUERETURN_HEAD.Doc_No=TSPL_ISSUERETURN_DETAIL.Doc_No " &
                                            " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_ISSUERETURN_HEAD.Dept " &
                                            " LEFT OUTER JOIN TSPL_COST_CENTER_TYPE_MASTER ON TSPL_COST_CENTER_TYPE_MASTER.CODE=TSPL_ISSUERETURN_HEAD.Cost_Center_Type " &
                                            " LEFT OUTER JOIN TSPL_COST_CENTER_UNIT_MASTER ON TSPL_COST_CENTER_UNIT_MASTER.CODE=TSPL_ISSUERETURN_HEAD.Cost_Center_Unit " &
                                            " WHERE convert(date,TSPL_ISSUERETURN_HEAD.Doc_Date,103)>='" + strFromDate + "' and convert(date,TSPL_ISSUERETURN_HEAD.Doc_Date,103)<='" + strToDate + "' and TSPL_COST_CENTER_UNIT_MASTER.Description is not null "
        Dim isRecordExistOrNot As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue(strRecordExistOrNot))
        If isRecordExistOrNot = 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
            Exit Sub
        End If
        '==================================================================
        Dim Strunit As String = " select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct(select distinct('['+isnull(TSPL_COST_CENTER_UNIT_MASTER.Description,'')+'],') from TSPL_COST_CENTER_UNIT_MASTER FOR XML PATH(''))as xvalue)a"
        Dim StrunitData As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(Strunit))
        Dim StrunitAdd As String = " select substring(a.xvalue,0,len(a.xvalue)-0) from (select distinct(select distinct('isnull(['+TSPL_COST_CENTER_UNIT_MASTER.Description+'],0)+') from TSPL_COST_CENTER_UNIT_MASTER FOR XML PATH(''))as xvalue)a"
        Dim StrunitAddData As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(StrunitAdd))
        If clsCommon.myLen(StrunitAddData) > 0 AndAlso clsCommon.myLen(StrunitData) > 0 Then

            StrQuery = " select  DEPT_COST_CENTRE.Dept_Name AS [DEPARTMENT],DEPT_COST_CENTRE.type AS  [Type],DEPT_COST_CENTRE.type_code AS [Cost Center No.]," + StrunitData + ", " + StrunitAddData + " as [Total (In Rs.)]"
            StrQuery += " FROM ( Select TSPL_GL_SEGMENT_CODE.Segment_code AS Dept_Code,TSPL_GL_SEGMENT_CODE.Description as Dept_Name,TSPL_COST_CENTER_TYPE_MASTER.Code AS Type_Code,TSPL_COST_CENTER_TYPE_MASTER.Description as Type  from TSPL_GL_SEGMENT_CODE inner join TSPL_COST_CENTER_TYPE_MASTER " + Environment.NewLine &
                        " on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_COST_CENTER_TYPE_MASTER.Department ) AS DEPT_COST_CENTRE " + Environment.NewLine &
                        " LEFT OUTER  join " + Environment.NewLine

            StrQuery += "(SELECT * FROM (select TSPL_COST_CENTER_TYPE_MASTER.Code,TSPL_COST_CENTER_TYPE_MASTER.Description as Type, TSPL_GL_SEGMENT_CODE.Segment_code,TSPL_GL_SEGMENT_CODE.Description as Department," + Environment.NewLine &
                     " TSPL_COST_CENTER_UNIT_MASTER.Description AS UNIT, isnull(TSPL_ISSUERETURN_DETAIL.Item_Net_Amt,0)*(case when  TSPL_ISSUERETURN_HEAD.Doc_Type='Return' then -1 else 1 end) AS Item_Net_Amt " + Environment.NewLine &
                     " from TSPL_ISSUERETURN_HEAD LEFT OUTER JOIN TSPL_ISSUERETURN_DETAIL ON TSPL_ISSUERETURN_HEAD.Doc_No=TSPL_ISSUERETURN_DETAIL.Doc_No " + Environment.NewLine &
                     " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_ISSUERETURN_HEAD.Dept" + Environment.NewLine &
                     " LEFT OUTER JOIN TSPL_COST_CENTER_TYPE_MASTER ON TSPL_COST_CENTER_TYPE_MASTER.CODE=TSPL_ISSUERETURN_HEAD.Cost_Center_Type " + Environment.NewLine &
                     " LEFT OUTER JOIN TSPL_COST_CENTER_UNIT_MASTER ON TSPL_COST_CENTER_UNIT_MASTER.CODE=TSPL_ISSUERETURN_HEAD.Cost_Center_Unit " + Environment.NewLine &
                     " WHERE convert(date,TSPL_ISSUERETURN_HEAD.Doc_Date,103)>='" + strFromDate + "' and convert(date,TSPL_ISSUERETURN_HEAD.Doc_Date,103)<='" + strToDate + "'" + Environment.NewLine

            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_ISSUERETURN_HEAD.From_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtplantUnit.arrValueMember IsNot Nothing AndAlso txtplantUnit.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_COST_CENTER_UNIT_MASTER.Code in(" + clsCommon.GetMulcallString(txtplantUnit.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_GL_SEGMENT_CODE.Segment_code in(" + clsCommon.GetMulcallString(txtDepartment.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                StrQuery += " and TSPL_COST_CENTER_TYPE_MASTER.Code in(" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")" + Environment.NewLine
            End If
            StrQuery += " ) AS DETAIL_DATA " + Environment.NewLine & _
                    " pivot ( SUM(Item_Net_Amt) FOR UNIT  in(" + StrunitData + ")) as Final_Query " + Environment.NewLine
            StrQuery += " ) MAIN ON MAIN.Department=DEPT_COST_CENTRE.Dept_Name AND  MAIN.Code= DEPT_COST_CENTRE.Type_Code ORDER BY  DEPT_COST_CENTRE.Dept_Name,DEPT_COST_CENTRE.Type_Code  "

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            Else
                RadPageView1.SelectedPage = RadPageViewPage2
                gvData.GroupDescriptors.Clear()
                gvData.MasterTemplate.SummaryRowsBottom.Clear()
                gvData.DataSource = dt
                gvData.GroupDescriptors.Add(New GridGroupByExpression("DEPARTMENT AS DEPARTMENT format ""{0}: {1}"" Group By DEPARTMENT"))
                SetGridFormationOFGV1()
                gvData.AutoExpandGroups = True
                gvData.ShowGroupPanel = False
                gvData.ShowRowHeaderColumn = False
                gvData.AllowAddNewRow = False
                gvData.AllowDeleteRow = False
                gvData.EnableFiltering = True
                gvData.ReadOnly = True
                gvData.ShowFilteringRow = True
                gvData.BestFitColumns()
            End If
        End If
    End Sub
    Sub SetGridFormationOFGV1()
        gvData.TableElement.TableHeaderHeight = 40
        gvData.MasterTemplate.ShowRowHeaderColumn = False
        Dim summaryRowItem As New GridViewSummaryRowItem()
        For ii As Integer = 0 To gvData.Columns.Count - 1
            If clsCommon.CompairString(clsCommon.myCstr(gvData.Columns(ii).Name), "DEPARTMENT") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvData.Columns(ii).Name), "Cost Center No.") <> CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(gvData.Columns(ii).Name), "Type") <> CompairStringResult.Equal Then
                Dim strcol As String = clsCommon.myCstr(gvData.Columns(ii).Name)
                Dim item1 As New GridViewSummaryItem(strcol, "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
            End If
        Next
        gvData.ShowGroupPanel = False
        gvData.MasterTemplate.AutoExpandGroups = True
        gvData.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
    End Sub

    Private Sub FrmCostCentreConsumptionRpt_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.R Then
            Print(Exporter.Refresh)
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub gvData_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvData.CellDoubleClick
        If e.Column IsNot gvData.Columns("DEPARTMENT") AndAlso e.Column IsNot gvData.Columns("Type") AndAlso e.Column IsNot gvData.Columns("[Cost Center No.]") Then
            gvDetail.DataSource = Nothing
            Dim StrDept As String = clsCommon.myCstr(gvData.CurrentRow.Cells("DEPARTMENT").Value)
            Dim StrTypeCode As String = clsCommon.myCstr(gvData.CurrentRow.Cells("Cost Center No.").Value)
            Dim StrUnitDesc As String = clsCommon.myCstr(gvData.CurrentColumn.Name)
            Dim qry As String = "select TSPL_GL_SEGMENT_CODE.Description as Department, " &
                " TSPL_COST_CENTER_UNIT_MASTER.Description as [Plant / Section],TSPL_COST_CENTER_TYPE_MASTER.Description as Type,TSPL_ISSUERETURN_HEAD.Doc_No as [Document No],convert(varchar(15),TSPL_ISSUERETURN_HEAD.Doc_Date,103) as [Document Date], TSPL_ISSUERETURN_DETAIL.Item_Code as [Item Code]," &
                " TSPL_ITEM_MASTER.Item_Desc AS [Item Name],(case when TSPL_ISSUERETURN_HEAD.Doc_Type='Return' then isnull(TSPL_ISSUERETURN_DETAIL.Issued_Qty,0)*-1 else isnull(TSPL_ISSUERETURN_DETAIL.Issued_Qty,0) end) AS Quantity,(case when TSPL_ISSUERETURN_HEAD.Doc_Type='Return' then isnull(TSPL_ISSUERETURN_DETAIL.Item_Net_Amt,0)*-1 else isnull(TSPL_ISSUERETURN_DETAIL.Item_Net_Amt,0) end) AS Value  from TSPL_ISSUERETURN_HEAD " &
                " LEFT OUTER JOIN TSPL_ISSUERETURN_DETAIL ON TSPL_ISSUERETURN_HEAD.Doc_No=TSPL_ISSUERETURN_DETAIL.Doc_No " &
                " LEFT OUTER JOIN TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code=TSPL_ISSUERETURN_DETAIL.Item_Code " &
                " left outer join TSPL_GL_SEGMENT_CODE on TSPL_GL_SEGMENT_CODE.Segment_code=TSPL_ISSUERETURN_HEAD.Dept  " &
                 " LEFT OUTER JOIN TSPL_COST_CENTER_TYPE_MASTER ON TSPL_COST_CENTER_TYPE_MASTER.CODE=TSPL_ISSUERETURN_HEAD.Cost_Center_Type  " &
                 " LEFT OUTER JOIN TSPL_COST_CENTER_UNIT_MASTER ON TSPL_COST_CENTER_UNIT_MASTER.CODE=TSPL_ISSUERETURN_HEAD.Cost_Center_Unit " &
                 " where TSPL_GL_SEGMENT_CODE.Description='" + StrDept + "' AND TSPL_COST_CENTER_TYPE_MASTER.Code='" + StrTypeCode + "' and TSPL_COST_CENTER_UNIT_MASTER.Description='" + StrUnitDesc + "' "
            If txtLocation.arrValueMember IsNot Nothing AndAlso txtLocation.arrValueMember.Count > 0 Then
                qry += " and TSPL_ISSUERETURN_HEAD.From_Location in(" + clsCommon.GetMulcallString(txtLocation.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtplantUnit.arrValueMember IsNot Nothing AndAlso txtplantUnit.arrValueMember.Count > 0 Then
                qry += " and TSPL_COST_CENTER_UNIT_MASTER.Code in(" + clsCommon.GetMulcallString(txtplantUnit.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtDepartment.arrValueMember IsNot Nothing AndAlso txtDepartment.arrValueMember.Count > 0 Then
                qry += " and TSPL_GL_SEGMENT_CODE.Segment_code in(" + clsCommon.GetMulcallString(txtDepartment.arrValueMember) + ")" + Environment.NewLine
            End If
            If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                qry += " and TSPL_COST_CENTER_TYPE_MASTER.Code in(" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")" + Environment.NewLine
            End If
            qry += " and convert(date,TSPL_ISSUERETURN_HEAD.Doc_Date,103)>='" + clsCommon.GetPrintDate(txtfDate.Value, "dd/MMM/yyyy") + "' and convert(date,TSPL_ISSUERETURN_HEAD.Doc_Date,103)<='" + clsCommon.GetPrintDate(txtToDate.Value, "dd/MMM/yyyy") + "' "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                RadPageView1.SelectedPage = RadPageViewPage3
                gvDetail.GroupDescriptors.Clear()
                gvDetail.MasterTemplate.SummaryRowsBottom.Clear()
                gvDetail.DataSource = dt
                ''gvDetail.GroupDescriptors.Add(New GridGroupByExpression("DEPARTMENT AS DEPARTMENT format ""{0}: {1}"" Group By DEPARTMENT"))
                'SetGridFormationOFGV1()
                gvDetail.TableElement.TableHeaderHeight = 40
                gvDetail.MasterTemplate.ShowRowHeaderColumn = False
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("Quantity", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                Dim item2 As New GridViewSummaryItem("Value", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item2)
                gvDetail.AutoExpandGroups = True
                gvDetail.ShowGroupPanel = False
                gvDetail.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
                gvDetail.ShowRowHeaderColumn = False
                gvDetail.ReadOnly = True
                gvDetail.AllowAddNewRow = False
                gvDetail.AllowDeleteRow = False
                gvDetail.EnableFiltering = True
                gvDetail.ShowFilteringRow = True
                gvDetail.BestFitColumns()
               

            End If

        End If

    End Sub


    Private Sub btnPDFExport_Click(sender As Object, e As EventArgs) Handles btnPDFExport.Click
        Print(EnumExportTo.PDF)
    End Sub

    Private Sub btnExcelExport_Click(sender As Object, e As EventArgs) Handles btnExcelExport.Click
        Print(EnumExportTo.Excel)
    End Sub
End Class
