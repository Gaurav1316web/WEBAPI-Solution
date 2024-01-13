'--16/07/2013--form Add By- Pradeep Sharma ---------
'' Anubhooti(3-July-2014) Added Export Permission Against BM00000003016 ''''''''
'' Anubhooti(11-July-2014) Added Export (Clubed)Button BM00000003137 ''''''''

Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports XpertERPEngine
Imports Telerik.WinControls.UI

Public Class frmDeductionRegister
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const ReportID As String = "DeductionRegister"


#Region "Variable"
    Private isInsideLoadData As Boolean = False
    Dim Qry As String
    Dim DT As DataTable
    Dim DT_Details As DataTable
    Dim sQuery As String = String.Empty

#End Region
    Sub LoadData()
        Try
            'If clsCommon.myLen(MulttxtFromPP.arrValueMember) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please fill the Pay Period First. ")
            '    txtFromPP.Focus()
            '    Exit Sub
            'End If
            'If clsCommon.myLen(txtTopp.Value) <= 0 Then
            '    clsCommon.MyMessageBoxShow("Please fill the To Pay Period First. ")
            '    txtTopp.Focus()
            '    Exit Sub
            'End If
            txtFromPP.MyReadOnly = True
            isInsideLoadData = True
            btnGenrate.Enabled = False
            'DT = clsDeductionDetails.GetRegisterDT(txtFromPP.Value, txtTopp.Value).Copy()
            DT_Details = clsDeductionDetails.GetRegisterDTDetailed(MulttxtFromPP.arrValueMember, MultfndLocationCode.arrValueMember, MultfndDivisionCode.arrValueMember, MultfndEmployee.arrValueMember, Qry)



            'Dim qry As String = ""
            'qry += " SELECT Ded.DEDUCTION_CODE as [Document Code] ,Dedd.PAY_HEAD_CODE as [Deduction Code] ,PHM.PAY_HEAD_NAME as [Deduction Name] ," & _
            '       " Dedd.EMP_CODE  as [Employee Code],EMP.Emp_Name as [Employee Name],EMP.Location_Code as [Location Code],Loc.Location_Desc as [Location Name] ,EMP.DEVISION_CODE as   [Division Code],Div.Devision_Name as [Division Name],pp.PAY_PERIOD_CODE as [Pay Period Code],pp.DESCRIPTION,Dedd.deduction_AMOUNT as [Deduction Amount]  FROM TSPL_DEDUCTION Ded " & _
            '       " INNER JOIN TSPL_DEDUCTION_DETAIL Dedd ON Ded.DEDUCTION_CODE=Dedd.DEDUCTION_CODE " & _
            '       " LEFT JOIN TSPL_PAYPERIOD_MASTER PP ON Ded.PAY_PERIOD_CODE=PP.PAY_PERIOD_CODE " & _
            '       " LEFT JOIN TSPL_EMPLOYEE_MASTER EMP ON Dedd.EMP_CODE=EMP.EMP_CODE " & _
            '       " LEFT JOIN TSPL_PAYHEAD_MASTER PHM ON Dedd.PAY_HEAD_CODE=PHM.PAY_HEAD_CODE " & _
            '       " left join TSPL_LOCATION_MASTER Loc on EMP.LOCATION_CODE=Loc.Location_Code " & _
            '       " left join TSPL_DEVISION_MASTER Div on EMP.DEVISION_CODE=Div.DEVISION_CODE where 2=2"

            'If clsCommon.myLen(txtFromPP.Value) > 0 AndAlso clsCommon.myLen(txtTopp.Value) > 0 Then
            '    qry += " and PP.DATE_FROM BETWEEN "
            '    qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + txtFromPP.Value + "') AND "
            '    qry += " (SELECT DATE_FROM FROM TSPL_PAYPERIOD_MASTER WHERE PAY_PERIOD_CODE='" + txtTopp.Value + "') "
            'End If

            'If MultfndLocationCode.arrValueMember IsNot Nothing AndAlso MultfndLocationCode.arrValueMember.Count > 0 Then
            '    qry += " and EMP.Location_Code  in (" + clsCommon.GetMulcallString(MultfndLocationCode.arrValueMember) + ") "
            'End If

            'If MultfndDivisionCode.arrValueMember IsNot Nothing AndAlso MultfndDivisionCode.arrValueMember.Count > 0 Then
            '    qry += " and EMP.DEVISION_CODE  in (" + clsCommon.GetMulcallString(MultfndDivisionCode.arrValueMember) + ") "
            'End If

            'If MultfndEmployee.arrValueMember IsNot Nothing AndAlso MultfndEmployee.arrValueMember.Count > 0 Then
            '    qry += " and EMP.EMP_CODE  in (" + clsCommon.GetMulcallString(MultfndEmployee.arrValueMember) + ") "
            'End If
            'DT = clsDBFuncationality.GetDataTable(qry)



            Dim DeductionfroPivotQry As String = "select case when SLONO=1 then '' else ',' end + '['+ [Deduction Code] + ']' from ( select [Deduction Code] , row_number() over (order by [Deduction Code]) as SLONO   from (select distinct [Deduction Code]   from ("
            DeductionfroPivotQry += "" & Qry & ""
            DeductionfroPivotQry += ") as final ) as test ) as final for xml path('') "
            Dim strDeductionfroPivot As String = clsDBFuncationality.getSingleValue(DeductionfroPivotQry)

            Dim DeductionfroPivotOuterQry As String = "select case when SLONO=1 then '' else ',' end + 'sum(isnull(['+ [Deduction Code] + '],0)) as  [' + [Deduction Code]+']' from ( select [Deduction Code] , row_number() over (order by [Deduction Code]) as SLONO   from (select distinct [Deduction Code]   from  ("
            DeductionfroPivotOuterQry += "" & Qry & ""
            DeductionfroPivotOuterQry += ") as final ) as test ) as final for xml path('') "
            Dim strDeductionfroPivotOuter As String = clsDBFuncationality.getSingleValue(DeductionfroPivotOuterQry)


            Dim DeductionfroPivotOuterSUMQry As String = "select case when SLONO=1 then '' else '+' end + 'sum(isnull(['+ [Deduction Code] + '],0))  '  from ( select [Deduction Code] , row_number() over (order by [Deduction Code]) as SLONO   from (select distinct [Deduction Code]  from  (  "
            DeductionfroPivotOuterSUMQry += "" & Qry & ""
            DeductionfroPivotOuterSUMQry += ") as final ) as test ) as final for xml path('') "
            Dim strDeductionfroPivotOuterSum As String = clsDBFuncationality.getSingleValue(DeductionfroPivotOuterSUMQry)





            If chkPivot.Checked Then

                Dim pivotquery As String = "select [Document Code],[Employee Code],[Employee Name] ,[Location Code],[Location Name],[Division Code],[Division Name],[Pay Period Code],DESCRIPTION," & strDeductionfroPivotOuter & " ," & strDeductionfroPivotOuterSum & "  as [Total Deduction] from ("
                pivotquery += " select max([Document Code])as [Document Code], [Deduction Code] ,  max([Deduction Name]) as  [Deduction Name],[Employee Code],max([Employee Name]) as [Employee Name],max([Location Code]) as [Location Code],max([Location Name])as [Location Name],max([Division Code]) as [Division Code] ,max([Division Name]) as [Division Name] ,([Pay Period Code]) as [Pay Period Code] ,max(DESCRIPTION) as DESCRIPTION ,sum([Deduction Amount])as [Deduction Amount] from  ( "
                pivotquery += " " & Qry & " "
                pivotquery += ") as final group by [Deduction Code] ,[Employee Code] ,[Pay Period Code] "
                pivotquery += "  )as final "
                pivotquery += " pivot (sum([Deduction Amount]) FOR [Deduction Code] IN (" & strDeductionfroPivot & "))AS pvt1 group by [Document Code],[Employee Code],[Employee Name] ,[Location Code],[Location Name],[Division Code],[Division Name],[Pay Period Code],DESCRIPTION  "


                Dim dtgv As New DataTable
                dtgv = clsDBFuncationality.GetDataTable(pivotquery)
                If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
                    gv1.DataSource = Nothing
                    gv1.Rows.Clear()
                    gv1.Columns.Clear()
                    gv1.DataSource = dtgv
                    gv1.GroupDescriptors.Clear()
                    gv1.MasterTemplate.SummaryRowsTop.Clear()
                    Dim summaryRowItem As New GridViewSummaryRowItem()
                    Dim item1 As New GridViewSummaryItem("Total Deduction", "{0:F2}", GridAggregateFunction.Sum)
                    summaryRowItem.Add(item1)
                    gv1.MasterTemplate.SummaryRowsTop.Add(summaryRowItem)
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv1.MasterTemplate.BestFitColumns()
                    ReStoreGridLayout()
                    btnGenrate.Enabled = True

                End If

            Else
                gv1.DataSource = DT_Details
                gv1.MasterTemplate.SummaryRowsTop.Clear()
                Dim summaryRowItem As New GridViewSummaryRowItem()
                Dim item1 As New GridViewSummaryItem("Deduction Amount", "{0:F2}", GridAggregateFunction.Sum)
                summaryRowItem.Add(item1)
                gv1.MasterTemplate.SummaryRowsTop.Add(summaryRowItem)
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.MasterTemplate.BestFitColumns()
                ReStoreGridLayout()
                btnGenrate.Enabled = True
            End If



        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            btnGenrate.Enabled = True
        End Try
    End Sub

    Private Sub frmDeductionRegister_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnGenrate, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Adding New ")
    End Sub

    Private Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmDeductionRegister)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        '' Preeti Gupta Added Export Permission  ''''''''
        'btnExpoExl.Visible = MyBase.isExport
        'btnExpoPDF.Visible = MyBase.isExport
        btnExport.Visible = MyBase.isExport
        btnGenrate.Visible = MyBase.isModifyFlag
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        funReset()
    End Sub

    Sub funReset()
        'txtFromPP.MyReadOnly = False
        MulttxtFromPP.arrValueMember = Nothing
        'txtFromPP.Focus()
        'lblFrompp.Text = ""
        'txtTopp.Value = Nothing
        'lblTopp.Text = ""
        MultfndLocationCode.arrValueMember = Nothing
        MultfndDivisionCode.arrValueMember = Nothing
        'lblLocationName.Text = ""
        'lblDivisionName.Text = ""
        MultfndEmployee.arrValueMember = Nothing
        'txtemployeeName.text=""
        chkPivot.Checked = False

        btnGenrate.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        funClose()
    End Sub

    Sub funClose()
        Me.Close()
    End Sub

    Private Sub txtCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromPP._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtFromPP.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtFromPP.Value, "", isButtonClicked)
        lblFrompp.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)
    End Sub

    Private Sub txtCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If (e.KeyChar = Chr(39)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub frmDeductionRegister_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.C Then
            funClose()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            funReset()
        End If
    End Sub

    Private Sub btnGenrate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenrate.Click
        PageSetupReport_ID = ReportID + IIf(chkPivot.Checked = True, "P", "")
        TemplateGridview = gv1
        LoadData()
    End Sub
    Private Sub RadMenuItemSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSave.Click
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(PageSetupReport_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(PageSetupReport_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Sub RadMenuItemDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemDelete.Click
        clsGridLayout.DeleteData(PageSetupReport_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub btnExpoExl_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Deduction Report")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToExcel("Deduction Register", gv1, arr, "Salary Register")
        clsCommon.MyExportToExcelGrid("Deduction Register", gv1, arr, "Deduction Register", False)

    End Sub

    Private Sub btnExpoPDF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim arr As New List(Of String)()
        arr.Add(objCommonVar.CurrentCompanyName)
        arr.Add("Deduction Report (Detail)")
        arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        clsCommon.MyExportToPDF("Deduction Register", gv1, arr, "Deduction Register", False)

    End Sub

#Region "grid operations"

    Private Sub SetupMasterForAutoGenerateHierarchy()
        Using Me.gv1.DeferRefresh()
            Me.gv1.AutoGenerateHierarchy = True
            Me.gv1.MasterTemplate.Reset()
            Me.gv1.TableElement.RowHeight = 20
            Me.gv1.DataSource = DT
            Me.gv1.MasterTemplate.Columns("DEDUCTION_CODE").HeaderText = "DEDUCTION CODE"
            Me.gv1.MasterTemplate.Columns("DEDUCTION_DATE").HeaderText = "DEDUCTION DATE"
            Me.gv1.MasterTemplate.Columns("DEDUCTION_DATE").FormatString = "{0:  dd/MMM/yyyy}"
            Me.gv1.MasterTemplate.Columns("DEDUCTION_AMOUNT").HeaderText = "DEDUCTION AMOUNT"
            Me.gv1.MasterTemplate.Columns("DEDUCTION_REMARKS").HeaderText = "REMARKS"
            Me.gv1.MasterTemplate.Columns("PAY_PERIOD_CODE").HeaderText = "PAY PERIOD CODE"
            Me.gv1.MasterTemplate.Columns("PAY_PERIOD_NAME").HeaderText = "PAY PERIOD NAME"
            Me.gv1.MasterTemplate.Columns("EMP_CODE").HeaderText = "EMPLOYEE CODE"
            Me.gv1.MasterTemplate.Columns("Emp_Name").HeaderText = "EMPLOYEE NAME"
            Me.gv1.MasterTemplate.Columns("DEDUCTION_BY").HeaderText = "DEDUCTION BY CODE"
            Me.gv1.MasterTemplate.Columns("DEDUCTION_BY_NAME").HeaderText = "DEDUCTION BY NAME"
            Me.gv1.MasterTemplate.Columns("POSTED").HeaderText = "IS APPROVED"
            Me.gv1.MasterTemplate.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill

            Dim template As New GridViewTemplate()
            template.DataSource = DT_Details
            Me.gv1.Templates.Add(template)
            template.AllowAddNewRow = False
            template.Columns("DEDUCTION_CODE").HeaderText = "DEDUCTION CODE"
            template.Columns("PAY_HEAD_CODE").HeaderText = "PAY HEAD CODE"
            template.Columns("PAY_HEAD_NAME").HeaderText = "PAY HEAD NAME"
            template.Columns("EMP_CODE").HeaderText = "EMPLOYEE CODE"
            template.Columns("Emp_Name").HeaderText = "EMPLOYEE NAME"
            template.Columns("DEDUCTION_AMOUNT").HeaderText = "DEDUCTION AMOUNT"
            template.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill
            template.ReadOnly = True

            Dim relation As New GridViewRelation(gv1.MasterTemplate, template)
            relation.RelationName = "DEDUCTION_RE"
            relation.ParentColumnNames.Add("DEDUCTION_CODE")
            relation.ChildColumnNames.Add("DEDUCTION_CODE")
            Me.gv1.Relations.Add(relation)

            'gv1.EnableCustomFiltering = True
            'gv1.EnableCustomGrouping = True
            'gv1.EnableCustomSorting = True
            'gv1.EnableFiltering = True
            'gv1.EnableGrouping = True
            'gv1.EnableSorting = True
            'gv1.ShowFilteringRow = True
        End Using
    End Sub

#End Region

    Private Sub txtTopp__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTopp._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
            & " PAY_PERIOD_NAME as 'Pay Period Name' FROM TSPL_PAYPERIOD_MASTER  "
        txtTopp.Value = clsCommon.ShowSelectForm("TSPL_PAYPERIOD_MASTER", qry, "Code", "POSTED=1 AND FREEZED=0", txtTopp.Value, "", isButtonClicked)
        lblTopp.Text = clsPayPeriodMaster.GetName(txtTopp.Value, Nothing)
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Deduction Report")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        ''clsCommon.MyExportToExcel("Deduction Register", gv1, arr, "Salary Register")
        'If gv1.Rows.Count <= 0 Then
        '    gv1.Focus()
        '    clsCommon.MyMessageBoxShow("Data not found.")
        'Else
        '    clsCommon.MyExportToExcelGrid("Deduction Register", gv1, arr, "Deduction Register", False)
        'End If
        ExportGrid(EnumExportTo.Excel)
    End Sub

    Private Sub btnPDF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPDF.Click
        'Dim arr As New List(Of String)()
        'arr.Add(objCommonVar.CurrentCompanyName)
        'arr.Add("Deduction Report (Detail)")
        'arr.Add("as on :-" + clsCommon.GETSERVERDATE() + " ")
        'clsCommon.MyExportToPDF("Deduction Register", gv1, arr, "Deduction Register", False)
        ExportGrid(EnumExportTo.PDF)
    End Sub
    'Public Sub SetDiplayMemberSingleFinder(ByVal Fnd As common.UserControls.txtFinder, ByVal Col_Name As String, ByVal tb_name As String, ByVal val_col_Name As String)
    '    Try
    '        Dim arr_list As String = ""
    '        sQuery = "select  " & Col_Name & " from " & tb_name & " where " & val_col_Name & " in ('" & Fnd.Value & "')"

    '        Dim dt As DataTable = clsDBFuncationality.GetDataTable(sQuery)
    '        Dim arrList As New ArrayList
    '        For Each row As DataRow In dt.Rows
    '            arr_list = IIf(clsCommon.myLen(arr_list) <= 0, row(0), arr_list & "," & row(0))
    '            'arrList.Add(row(0))
    '        Next
    '        Fnd.Value = arr_list
    '    Catch ex As Exception
    '        clsCommon.MyMessageBoxShow(ex.ToString)
    '    End Try
    'End Sub
    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocationCode._MYValidating
        fndLocationCode.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndLocationCode.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocationCode.Value & "'")
            'SetDiplayMemberSingleFinder(fndLocationCode, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub fndDivisionCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDivisionCode._MYValidating
        Dim qry As String = "select DEVISION_CODE as Code, DEVISION_NAME as Name, DESCRIPTION as Description from TSPL_DEVISION_MASTER"
        fndDivisionCode.Value = clsCommon.ShowSelectForm("DEVISION_MASTER", qry, "Code", "", fndDivisionCode.Value, "DEVISION_CODE", isButtonClicked)
        If clsCommon.myLen(fndDivisionCode.Value) > 0 Then
            lblDivisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME from TSPL_DEVISION_MASTER where DEVISION_CODE='" & fndDivisionCode.Value & "'")
        Else
            lblDivisionName.Text = ""
        End If
    End Sub

    Private Sub ExportGrid(ByVal exporter As EnumExportTo)
        Try
            If gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()

                'arrHeader.Add("Date :" + clsCommon.GETSERVERDATE() + " ")
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.frmDeductionRegister & "'"))
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                'arrHeader.Add(("From Pay Period: " + txtFromPP.Value) + " To Pay Period " + txtTopp.Value)
                If MulttxtFromPP.arrValueMember IsNot Nothing AndAlso MulttxtFromPP.arrValueMember.Count > 0 Then
                    arrHeader.Add("Pay Period: " + clsCommon.GetMulcallString(MulttxtFromPP.arrValueMember))
                End If
                'If fndLocationCode.Value IsNot Nothing AndAlso fndLocationCode.Value.Count > 0 Then
                If MultfndLocationCode.arrValueMember IsNot Nothing AndAlso MultfndLocationCode.arrValueMember.Count > 0 Then
                    arrHeader.Add("Location: " + clsCommon.GetMulcallString(MultfndLocationCode.arrValueMember))
                End If
                If MultfndDivisionCode.arrValueMember IsNot Nothing AndAlso MultfndDivisionCode.arrValueMember.Count > 0 Then
                    arrHeader.Add("Division : " + clsCommon.GetMulcallStringWithComma(MultfndDivisionCode.arrValueMember))
                End If
                If MultfndEmployee.arrValueMember IsNot Nothing AndAlso MultfndEmployee.arrValueMember.Count > 0 Then
                    arrHeader.Add("Employee : " + clsCommon.GetMulcallStringWithComma(MultfndEmployee.arrValueMember))
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
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
                    'transportSql.exportdataChilRows(gv1, filePath, filePath.Substring(filePath.LastIndexOf("\") + 1, filePath.Length - filePath.LastIndexOf("\") - 1), , arrHeader)
                    'common.clsCommon.MyMessageBoxShow("Exported Successfully.")
                    'Process.Start(filePath)
                Else
                    transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Deduction Register", gv1, arrHeader, "Deduction Register", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub fndEmployee__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndEmployee._MYValidating
        fndEmployee.Value = clsEmployeeMaster.getFinder("", Me.fndLocationCode.Value, isButtonClicked)
        If clsCommon.myLen(fndEmployee.Value) > 0 Then
            txtEmployeeName.Text = clsDBFuncationality.getSingleValue("select emp_name from tspl_employee_master where emp_code='" & fndEmployee.Value & "'")
        Else
            txtEmployeeName.Text = ""
        End If
    End Sub

    Private Sub MultfndLocationCode__My_Click(sender As Object, e As EventArgs) Handles MultfndLocationCode._My_Click
        Dim qry As String = " Select Location_Code As Code,location_Desc As Name ,Add1 + ' ' + Add2 + Add3 + ' ' + Add4 As [Address],City_Code As [City Code],State As [State],Pin_Code As [Pin Code]," & _
                          " Country ,Telphone,Email,Location_Type AS [Location Type],Loc_Status AS [Loc Status] ,Loc_Segment_Code As [Loc Segment Code],Type As [Type] From TSPL_LOCATION_MASTER  where Location_Type='Physical'"

        MultfndLocationCode.arrValueMember = clsCommon.ShowMultipleSelectForm("LocMulPF", qry, "Code", "Name", MultfndLocationCode.arrValueMember, MultfndLocationCode.arrDispalyMember)
    End Sub

    Private Sub MultfndEmployee__My_Click(sender As Object, e As EventArgs) Handles MultfndEmployee._My_Click
        Dim qry As String = " select EMP_CODE as [Code],Emp_Name as [Name],Designation,Add1,Add2,Pin_Code as [Pin Code],Phone,Birth_date as [Date Of Birth],Cash,Card_No as [Card No],Joining_date as [Joining Date],Emp_type as [Employee Type],ExDate [Expiry Date],Emp_Status as [Employee Status],RELIEVING_DATE as [Releving Date],Payroll_Code as [Payroll Code],Empty_Ex as [Empty Ex],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_Code as [Company Code],GL_Account as [GL Account],EMail_ID as [Email ID],SEX,MARITAL_STATUS as [Marital Status],ANNIVERSARY_DATE as [Anniversary Date],DEPARTMENT_CODE as [Department Code],OCCUPATION_CODE as [Occupation Code],DEVISION_CODE as [Division Code],GRADE_CODE as [Grade Code],LOCATION_CODE as [Branch Code],ATTENDANCE_CODE as [Attandance Code],PAYMENT_MODE_NEW as [Payment Mode],BANK_ACC_NO as [Bank Account No],BANK_CODE as [Bank Code],CONFIRMATION_DATE as [Confirmation Date],PROBATION_END_DATE as [Probation End Date],SHIFT_CODE as [Shift Code],RELIEVING_DATE as [Relieving Date],LEAVING_REASON as [Leaving Reason],CAST_CATEGORY_CODE as [Cast Category Code],RELIGION_CODE as [Religion Code],PRESENT_COUNTRY_CODE as [Present Country Code],PRESENT_STATE_CODE as [Present State Code,PRESENT_CITY_CODE as [Present City Code],PRESENT_MOBILE_NO as [Present Mobile No],PERMA_COUNTRY_CODE as [Permanent Country Code],PERMA_STATE_CODE as [Permanent State Code],PERMA_CITY_CODE as [Permanent City Code],PERMA_PHONE_NO as [Permanent Phone No],PERMA_MOBILE_NO as [Permanent Mobile No],PERMA_PIN_CODE as [Permanent Pin Code],PAN_NO as [Pan No],PASPORT_NO as [Passport No],DESCRIPTION as [Description],FATHERS_NAME as [Fathers Name],MOTHERS_NAME as [Mothers Name],SPOUSE_NAME as [Spouse Name],ISESI as [Is ESI],ESI_NO as [ESI No],ESI_DISPENSARY as [ESI Dispensary],ISPF as [Is PF],PF_NO as [PF No],PF_NO_DEPT_FILE as [PF No Department File],WARD_CIRCLE as [Ward Circle],ISRESTRICT_PF as [Is Restrict PF],ISZERO_PENSION as [Is Zero Pension],ISDIRECTOR as [Is Director],ISZERO_PT as [Is Zero PT],EARNING_CODE as [Earning Code],UNIT_COST as [Unit Cost],BILLING_RATE as [Billing Rate],APPLY_ALL_CUST as [Apply All Customer],USER_CODE as [User Code],COMMENTS as [Comments],Franchise_Code as [Franchise Code],RESIGNATION_SUBMIT_DATE as [Resignation Submission Date],NOTICE_PERIOD_IN_DAYS as [Notice Period In Days],EMPLOYMENT_NATURE as [Employment Nature],CONV_TYPE as [Conveyance Type],VENDOR_CODE as [Vendor Code],Agency_Code as [Agency Code],ISNULL(HRApplicant_Code,'') AS [HRApplicant Code],Age_For_Pension AS [Age For Pension] from tspl_employee_master "
        MultfndEmployee.arrValueMember = clsCommon.ShowMultipleSelectForm("EmpMulPF", qry, "Code", "Name", MultfndEmployee.arrValueMember, MultfndEmployee.arrDispalyMember)
    End Sub

    Private Sub MultfndDivisionCode__My_Click(sender As Object, e As EventArgs) Handles MultfndDivisionCode._My_Click
        Dim qry As String = " select DEVISION_CODE as Code,DEVISION_NAME as Name from TSPL_DEVISION_MASTER"
        MultfndDivisionCode.arrValueMember = clsCommon.ShowMultipleSelectForm("DivMulSel", qry, "Code", "Name", MultfndDivisionCode.arrValueMember, MultfndDivisionCode.arrDispalyMember)
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        funReset()
    End Sub

    Private Sub MulttxtFromPP__My_Click(sender As Object, e As EventArgs) Handles MulttxtFromPP._My_Click
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
          & " PAY_PERIOD_NAME as 'Name' FROM TSPL_PAYPERIOD_MASTER  "
        MulttxtFromPP.arrValueMember = clsCommon.ShowMultipleSelectForm("MulttxtFromPP", qry, "Code", "Name", MulttxtFromPP.arrValueMember, MulttxtFromPP.arrDispalyMember)
    End Sub
End Class
