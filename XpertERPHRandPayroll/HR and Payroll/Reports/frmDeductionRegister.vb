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
        btnGenrate.Visible = False
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
        fndPayperiod.Value = ""
        lblpayperiod.Text = ""
        btnGenrate.Enabled = True
        gv1.DataSource = Nothing
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        fndDeductioncode.Value = ""
        fndLocation.Value = ""
        fndDeductioncode.Value = ""
        lbldeduction.Text = ""
        lbldeduction.Text = ""
        lblLocationName.Text = ""
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
        lblpayperiod.Text = clsPayPeriodMaster.GetName(txtFromPP.Value, Nothing)
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", Me.Text)
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
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", Me.Text)
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
    Private Sub fndLocationCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        fndPayperiod.Value = clsLocation.getFinder("Location_Type='Physical'", Me.fndPayperiod.Value, isButtonClicked)
        If clsCommon.myLen(fndPayperiod.Value) > 0 Then
            lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndPayperiod.Value & "'")
            'SetDiplayMemberSingleFinder(fndLocationCode, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
        Else
            lblLocationName.Text = ""
        End If
    End Sub

    Private Sub fndDivisionCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        Dim qry As String = "select DEVISION_CODE as Code, DEVISION_NAME as Name, DESCRIPTION as Description from TSPL_DEVISION_MASTER"
        fndDeductioncode.Value = clsCommon.ShowSelectForm("DEVISION_MASTER", qry, "Code", "", fndDeductioncode.Value, "DEVISION_CODE", isButtonClicked)
        If clsCommon.myLen(fndDeductioncode.Value) > 0 Then
            lblDivisionName.Text = clsDBFuncationality.getSingleValue("select DEVISION_NAME from TSPL_DEVISION_MASTER where DEVISION_CODE='" & fndDeductioncode.Value & "'")
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
                'If MulttxtFromPP.arrValueMember IsNot Nothing AndAlso MulttxtFromPP.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Pay Period: " + clsCommon.GetMulcallString(MulttxtFromPP.arrValueMember))
                'End If
                ''If fndLocationCode.Value IsNot Nothing AndAlso fndLocationCode.Value.Count > 0 Then
                'If MultfndLocationCode.arrValueMember IsNot Nothing AndAlso MultfndLocationCode.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Location: " + clsCommon.GetMulcallString(MultfndLocationCode.arrValueMember))
                'End If
                'If MultfndDivisionCode.arrValueMember IsNot Nothing AndAlso MultfndDivisionCode.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Division : " + clsCommon.GetMulcallStringWithComma(MultfndDivisionCode.arrValueMember))
                'End If
                'If MultfndEmployee.arrValueMember IsNot Nothing AndAlso MultfndEmployee.arrValueMember.Count > 0 Then
                '    arrHeader.Add("Employee : " + clsCommon.GetMulcallStringWithComma(MultfndEmployee.arrValueMember))
                'End If

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


    Private Sub fndEmployee__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean)
        fndLocation.Value = clsEmployeeMaster.getFinder("", Me.fndPayperiod.Value, isButtonClicked)
        If clsCommon.myLen(fndLocation.Value) > 0 Then
            lbldeduction.Text = clsDBFuncationality.getSingleValue("select emp_name from tspl_employee_master where emp_code='" & fndLocation.Value & "'")
        Else
            lbldeduction.Text = ""
        End If
    End Sub

    Private Sub MultfndLocationCode__My_Click(sender As Object, e As EventArgs) Handles MultfndLocationCode._My_Click
        Dim qry As String = " Select Location_Code As Code,location_Desc As Name ,Add1 + ' ' + Add2 + Add3 + ' ' + Add4 As [Address],City_Code As [City Code],State As [State],Pin_Code As [Pin Code]," &
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

    Private Sub RadLabel1_Click(sender As Object, e As EventArgs) Handles RadLabel1.Click

    End Sub

    Private Sub fndPayperiod__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndPayperiod._MYValidating
        Dim qry As String = "SELECT PAY_PERIOD_CODE AS 'Code',(DATEDIFF(DAY,date_from,date_to)+1) as 'Total days', " _
         & " PAY_PERIOD_NAME as 'Name' FROM TSPL_PAYPERIOD_MASTER  "
        fndPayperiod.Value = clsCommon.ShowSelectForm("PAY_HEAD", qry, "Code", "", fndPayperiod.Value, "Code", isButtonClicked)
        lblpayperiod.Text = clsDBFuncationality.getSingleValue("select PAY_PERIOD_NAME from TSPL_PAYPERIOD_MASTER where PAY_PERIOD_CODE='" & fndPayperiod.Value & "'")

    End Sub

    Private Sub fndLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndLocation._MYValidating
        Dim qry As String = " Select Location_Code As Code,location_Desc As Name ,Add1 + ' ' + Add2 + Add3 + ' ' + Add4 As [Address],City_Code As [City Code],State As [State],Pin_Code As [Pin Code],
                           Country ,Telphone,Email,Location_Type AS [Location Type],Loc_Status AS [Loc Status] ,Loc_Segment_Code As [Loc Segment Code],Type As [Type] From TSPL_LOCATION_MASTER "
        'Dim whrcls As String = "   Location_Type='Physical' and IsMainPlant='1' and Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ")"
        Dim WhrCls As String = " Location_Type='Physical' and IsMainPlant='1' and Loc_Segment_Code in (" + objCommonVar.strCurrUserLocationsSegment + ") "
        fndLocation.Value = clsCommon.ShowSelectForm("location", qry, "Code", " ", fndLocation.Value, "Code", isButtonClicked)
        lblLocationName.Text = clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" & fndLocation.Value & "'")
        'SetDiplayMemberSingleFinder(fndLocationCode, "Location_Desc", "TSPL_LOCATION_MASTER", "Location_Code")
    End Sub

    Private Sub fndDeductioncode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndDeductioncode._MYValidating
        Dim qry As String = "select PAY_HEAD_CODE as Code ,PAY_HEAD_NAME as Name,HEAD_TYPE,SUB_HEAD_TYPE,CALC_BASIS from TSPL_PAYHEAD_MASTER "
        Dim whrcls As String = "  ISEARNING=0  "
        fndDeductioncode.Value = clsCommon.ShowSelectForm("deduction", qry, "Code", whrcls, fndDeductioncode.Value, "code", isButtonClicked)
        lbldeduction.Text = clsDBFuncationality.getSingleValue("select PAY_HEAD_NAME from TSPL_PAYHEAD_MASTER where PAY_HEAD_CODE='" & fndDeductioncode.Value & "'")
    End Sub

    Private Sub brngo_Click(sender As Object, e As EventArgs) Handles brngo.Click
        LoadGrid()
    End Sub
    Sub LoadGrid()
        Try
            If clsCommon.myLen(fndPayperiod.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Pay Period ")
                Exit Sub
            End If
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Location ")
                Exit Sub
            End If
            If clsCommon.myLen(fndDeductioncode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Deduction Code")
                Exit Sub
            End If

            Dim totalVgsli As Double = 0
            Dim qry As String = "  select GS.Pay_Period_Code as [Pay Period],Final.* from (SELECT Sex,
                 LIC_No, Policy_No,
                 SALARY_GENERATION_CODE,EMP_CODE AS EMP_CODE,EMPLOYEE_NAME,FATHERS_NAME AS [Father Name],Birth_date AS [Date of Birth],Joining_date as [Joining Date],LOCATION_DESC AS Location, SUM(gsli) AS vgsli FROM (SELECT ACD.SEX,
                 ACD.LIC_No,acd.Policy_No,
                 ACD.SALARY_GENERATION_CODE,ACD.EMP_CODE,ACD.PAY_HEAD_CODE,EMPLOYEE_NAME,FATHERS_NAME,[Working City],Birth_date,Joining_date,Designation,Department,LOCATION_DESC,DEVISION_NAME, BANK_NAME, Bank_Branch,Bank_Branch_Name,PAYMENT_MODE, ACD.PAYPERIOD_DAYS, ACD.PRESENT_DAYS, ACD.PAYABLE_DAYS, ACD.HOLIDAY_DAYS,ACD.WEEKLY_OFF_DAYS,ACD.LEAVE_DAYS ,PF_NO,ESI_NO,BANK_ACC_NO,RELIEVING_DATE,ACD.OT_HOURS, CASE WHEN  ACD.PAY_HEAD_CODE ='" + fndDeductioncode.Value + "' THEN acd.Payable_Amount else 0 END  AS gsli FROM  ( SELECT T2.SEX,
                 t2.LIC_No,t2.Policy_No,
                 T1.SALARY_GENERATION_CODE,T1.EMP_CODE,T2.EMP_NAME AS EMPLOYEE_NAME,T2.FATHERS_NAME,T2.WORKING_LOCATION_CODE,Working_City.City_Name as [Working City],T3.DESIGNATION_DESC AS DESIGNATION,T4.DEPARTMENT_NAME AS DEPARTMENT, T1.DEVISION_CODE,T1.LOCATION_CODE,LOC.LOCATION_DESC,Dev.DEVISION_NAME,T2.Bank_Name,T2.Bank_Branch,T2.Bank_Branch_Name,T2.PAYMENT_MODE_New as PAYMENT_MODE,T2.Birth_date,T2.Joining_date,T2.RELIEVING_DATE,T2.BRANCH_CODE,T1.PAY_HEAD_CODE , T1.ACTUAL_AMOUNT,T1.Payable_Amount,T1.PAYPERIOD_DAYS ,T1.PRESENT_DAYS ,T1.PAYABLE_DAYS,T1.HOLIDAY_DAYS,T1.WEEKLY_OFF_DAYS,T1.LEAVE_DAYS,T2.PF_NO,T2.ESI_NO,T2.BANK_ACC_NO,T1.OT_HOURS FROM (SELECT T1.SALARY_GENERATION_CODE,T2.EMP_CODE,T2.PAY_HEAD_CODE,T2.ACTUAL_AMOUNT,T2.Payable_Amount,T5.PAYPERIOD_DAYS,T5.PRESENT_DAYS,T5.PAYABLE_DAYS,T5.HOLIDAY_DAYS,T5.LEAVE_DAYS, (T5.PAYPERIOD_DAYS-T5.PRESENT_DAYS-T5.HOLIDAY_DAYS-T5.LEAVE_DAYS-T5.ABSENT_DAYS) as WEEKLY_OFF_DAYS,T1.LOCATION_CODE,T1.DEVISION_CODE,T2.OT_HOURS FROM TSPL_GENERATE_SALARY T1  INNER JOIN TSPL_GENERATE_SALARY_PAYHEADS T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE  inner JOIN TSPL_GENERATE_SALARY_ATTENDANCE T5 ON T5.Emp_code =T2.Emp_code  AND T1.SALARY_GENERATION_CODE=T5.SALARY_GENERATION_CODE  WHERE 2=2  AND T1.PAY_PERIOD_CODE in ( '" + fndPayperiod.Value + "') AND T1.LOCATION_CODE in ( '" + fndLocation.Value + "')  ) T1 LEFT JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.EMP_CODE=T2.EMP_CODE  LEFT JOIN TSPL_DESIGNATION_MASTER T3 ON T2.DESIGNATION=T3.DESIGNATION_ID LEFT JOIN TSPL_DEPARTMENT_MASTER T4 ON T2.DEPARTMENT_CODE=T4.DEPARTMENT_CODE LEFT JOIN TSPL_DEVISION_MASTER Dev ON T1.DEVISION_CODE=Dev.DEVISION_CODE LEFT JOIN TSPL_LOCATION_MASTER LOC ON T1.LOCATION_CODE=LOC.LOCATION_CODE LEFT JOIN TSPL_BANK_MASTER Bank ON T2.BANK_CODE=Bank.BANK_CODE  left join TSPL_City_MASTER as Working_City on Working_City.City_Code  =T2.WORKING_City_CODE) AS ACD) AS X where 2=2   GROUP BY x.[Working City] , X.SALARY_GENERATION_CODE,X.EMP_CODE,X.EMPLOYEE_NAME,X.DESIGNATION,X.DEPARTMENT,X.DEVISION_NAME ,X.PAYPERIOD_DAYS,X.PRESENT_DAYS,X.PAYABLE_DAYS,X.PF_NO,X.BANK_ACC_NO,X.Birth_date,X.HOLIDAY_DAYS,X.WEEKLY_OFF_DAYS, x.SEX,
                 x.LIC_No,x.Policy_No,
                 X.LEAVE_DAYS,X.ESI_NO,X.FATHERS_NAME,X.Bank_Name,X.Bank_Branch,X.Bank_Branch_Name,X.PAYMENT_MODE,X.Joining_date,X.RELIEVING_DATE,X.LOCATION_DESC) as Final  left join TSPL_GENERATE_SALARY_ATTENDANCE GSA ON Final.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE  AND Final.EMP_CODE=GSA.EMP_CODE left join TSPL_GENERATE_SALARY GS ON Final.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE  left join TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE  LEFT JOIN TSPL_EMPLOYEE_STATUS EMPStatus on GSA.EMP_STATUS_CODE=EMPStatus.EMP_STATUS_CODE  LEFT JOIN (select TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_01,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then (case when HEAD_VALUE>0.01 then 0.01*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_10,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>0.01 then 0.01*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EDLI_AC_21,  max(CASE WHEN SUB_HEAD_TYPE='EPF' then ACTUAL_AMOUNT ELSE 0 END) AS EPF_Amount_AC_01,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then CoEPS_AMT_AC10 ELSE 0 END) AS Pension_Amount_AC_10,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and Actual_Amount>0 then (Actual_Amount-(case when HEAD_VALUE>PF_MAX_LM then CoEPS_AMT_AC10*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else CoEPS_AMT_AC10 end)) ELSE 0 END) AS Diff_Amount_AC_01,  (max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*1.1/100 AS Admin_Amt_AC_02,  (max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>0.01 then 0.01*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*0.5/100 AS EDLI_Amt_AC_21,  max(CASE WHEN SUB_HEAD_TYPE='EPF' then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS ELSE 0 END) AS PF_MAX_LM,  max(CoEPF_RATE_AC01) as CoEPF_RATE_AC01,max(CoEPF_AMT_AC01) as CoEPF_AMT_AC01,max(CoEPS_RATE_AC10) as CoEPS_RATE_AC10,  max(CoEPS_AMT_AC10) as CoEPS_AMT_AC10,max(EDLI_RATE_AC21) as EDLI_RATE_AC21,  max(EDLI_AMT_AC21) as EDLI_AMT_AC21,  max(CASE WHEN SUB_HEAD_TYPE='EMPESI' AND ACTUAL_AMOUNT>0 then HEAD_VALUE ELSE 0 END) as ESI_HEAD_VALUE,  max(CASE WHEN SUB_HEAD_TYPE='EMPESI' then ACTUAL_AMOUNT ELSE 0 END) AS ESI_Amount,  max(Co_ESI_RATE) as Co_ESI_RATE,max(Co_ESI_AMT) as Co_ESI_AMT  from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where SUB_HEAD_TYPE in ('EPF','EMPESI')  group by TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE) AS GSP ON Final.SALARY_GENERATION_CODE=GSP.SALARY_GENERATION_CODE  AND Final.EMP_CODE=GSP.EMP_CODE LEFT JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=Final.EMP_CODE  ORDER BY Final.EMP_CODE,PPM.DATE_FROM
"
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2.Rows.Count > 0 AndAlso dt2.Columns.Contains("vgsli") Then
                For Each row As DataRow In dt2.Rows
                    If Not row("vgsli") Is DBNull.Value Then
                        totalVgsli += clsCommon.myCdbl(row("vgsli"))
                    End If
                Next
            End If

            If totalVgsli > 0 Then
                gv1.DataSource = Nothing
                gv1.Rows.Clear()
                gv1.Columns.Clear()
                gv1.GroupDescriptors.Clear()
                gv1.MasterTemplate.SummaryRowsBottom.Clear()
                gv1.MasterView.Refresh()
                gv1.DataSource = dt2
                For ii As Integer = 0 To gv1.Columns.Count - 1
                    gv1.Columns(ii).ReadOnly = True
                Next
                RadPageView1.SelectedPage = RadPageViewPage2
                gv1.EnableFiltering = True
                SetGridFormat()
                gv1.BestFitColumns()
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ' ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub SetGridFormat()
        gv1.AutoExpandGroups = True
        gv1.ShowGroupPanel = True
        gv1.ShowRowHeaderColumn = False
        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True


        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).BestFit()
        Next
        gv1.Columns("Pay Period").Name = "Pay Period"
        gv1.Columns("Pay Period").IsVisible = True '

        gv1.Columns("SALARY_GENERATION_CODE").HeaderText = "SALARY GENERATION CODE"
        gv1.Columns("SALARY_GENERATION_CODE").Width = 500
        gv1.Columns("SALARY_GENERATION_CODE").IsVisible = False

        gv1.Columns("EMP_CODE").HeaderText = "EMP CODE"
        gv1.Columns("EMP_CODE").Width = 500
        gv1.Columns("EMP_CODE").IsVisible = True

        gv1.Columns("EMPLOYEE_NAME").HeaderText = "EMPLOYEE NAME"
        gv1.Columns("EMPLOYEE_NAME").Width = 500
        gv1.Columns("EMPLOYEE_NAME").IsVisible = True

        gv1.Columns("LIC_No").HeaderText = "LIC Id"
        gv1.Columns("LIC_No").Width = 500
        gv1.Columns("LIC_No").IsVisible = True

        gv1.Columns("Policy_No").HeaderText = "Policy No"
        gv1.Columns("Policy_No").Width = 500
        gv1.Columns("Policy_No").IsVisible = False


        gv1.Columns("Father Name").HeaderText = "Father Name"
        gv1.Columns("Father Name").Width = 200
        gv1.Columns("Father Name").IsVisible = True

        gv1.Columns("Sex").HeaderText = "Gender"
        gv1.Columns("Sex").IsVisible = True

        gv1.Columns("Date of Birth").HeaderText = "Date of Birth"
        gv1.Columns("Date of Birth").IsVisible = True

        gv1.Columns("Joining Date").HeaderText = "Joining Date"
        gv1.Columns("Joining Date").IsVisible = True



        gv1.Columns("Location").HeaderText = "Location"
        gv1.Columns("Location").IsVisible = False




        gv1.Columns("vgsli").HeaderText = "Deduction"
        gv1.Columns("vgsli").IsVisible = True
        gv1.Columns("vgsli").FormatString = "{0:n3}"

        gv1.ShowGroupPanel = True
        gv1.MasterTemplate.AutoExpandGroups = True

    End Sub

    Private Sub btnprint_Click(sender As Object, e As EventArgs) Handles btnprint.Click
        Try
            If clsCommon.myLen(fndPayperiod.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Pay Period ")
                Exit Sub
            End If
            If clsCommon.myLen(fndLocation.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Location ")
                Exit Sub
            End If
            If clsCommon.myLen(fndDeductioncode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Please select Deduction Code")
                Exit Sub
            End If

            Dim totalVgsli As Double = 0
            Dim qry As String = "  SELECT CONCAT('" + fndDeductioncode.Value + "','  ','DEDUCTION OF EMPLOYEES FOR PERIOD',' ') + CONCAT(GS.Pay_Period_Code, '-', (final.GENERATE_DATE)) AS [Pay Period]
                    ,Final.* from (SELECT Sex,Designation,Membership_id,Special_desc,
                     LIC_No, Policy_no,generate_date,
                     SALARY_GENERATION_CODE,EMP_CODE AS EMP_CODE,EMPLOYEE_NAME,FATHERS_NAME AS [Father Name],Birth_date AS [Date of Birth],Joining_date as [Joining Date],LOCATION_DESC AS Location, SUM(gsli) AS vgsli FROM (SELECT ACD.SEX,ACD.Membership_id,ACD.Special_desc,
                     ACD.LIC_No,acd.Policy_no,acd.GENERATE_DATE,
                     ACD.SALARY_GENERATION_CODE,ACD.EMP_CODE,ACD.PAY_HEAD_CODE,EMPLOYEE_NAME,FATHERS_NAME,[Working City],Birth_date,Joining_date,Designation,Department,LOCATION_DESC,DEVISION_NAME, BANK_NAME, Bank_Branch,Bank_Branch_Name,PAYMENT_MODE, ACD.PAYPERIOD_DAYS, ACD.PRESENT_DAYS, ACD.PAYABLE_DAYS, ACD.HOLIDAY_DAYS,ACD.WEEKLY_OFF_DAYS,ACD.LEAVE_DAYS ,PF_NO,ESI_NO,BANK_ACC_NO,RELIEVING_DATE,ACD.OT_HOURS, CASE WHEN  ACD.PAY_HEAD_CODE ='" + fndDeductioncode.Value + "' THEN acd.Payable_Amount else 0 END  AS gsli FROM  ( SeleCT T2.SEX,t2.Membership_id,t2.Special_desc,
                     t2.LIC_No,t2.Policy_no,t1.GENERATE_DATE,
                     T1.SALARY_GENERATION_CODE,T1.EMP_CODE,T2.EMP_NAME AS EMPLOYEE_NAME,T2.FATHERS_NAME,T2.WORKING_LOCATION_CODE,Working_City.City_Name as [Working City],T3.DESIGNATION_DESC AS DESIGNATION,T4.DEPARTMENT_NAME AS DEPARTMENT, T1.DEVISION_CODE,T1.LOCATION_CODE,LOC.LOCATION_DESC,Dev.DEVISION_NAME,T2.Bank_Name,T2.Bank_Branch,T2.Bank_Branch_Name,T2.PAYMENT_MODE_New as PAYMENT_MODE,T2.Birth_date,T2.Joining_date,T2.RELIEVING_DATE,T2.BRANCH_CODE,T1.PAY_HEAD_CODE , T1.ACTUAL_AMOUNT,T1.Payable_Amount,T1.PAYPERIOD_DAYS ,T1.PRESENT_DAYS ,T1.PAYABLE_DAYS,T1.HOLIDAY_DAYS,T1.WEEKLY_OFF_DAYS,T1.LEAVE_DAYS,T2.PF_NO,T2.ESI_NO,T2.BANK_ACC_NO,T1.OT_HOURS FROM (SELECT YEAR(t1.GENERATE_DATE)GENERATE_DATE,T1.SALARY_GENERATION_CODE,T2.EMP_CODE,T2.PAY_HEAD_CODE,T2.ACTUAL_AMOUNT,T2.Payable_Amount,T5.PAYPERIOD_DAYS,T5.PRESENT_DAYS,T5.PAYABLE_DAYS,T5.HOLIDAY_DAYS,T5.LEAVE_DAYS, (T5.PAYPERIOD_DAYS-T5.PRESENT_DAYS-T5.HOLIDAY_DAYS-T5.LEAVE_DAYS-T5.ABSENT_DAYS) as WEEKLY_OFF_DAYS,T1.LOCATION_CODE,T1.DEVISION_CODE,T2.OT_HOURS FROM TSPL_GENERATE_SALARY T1  INNER JOIN TSPL_GENERATE_SALARY_PAYHEADS T2 ON T1.SALARY_GENERATION_CODE=T2.SALARY_GENERATION_CODE  inner JOIN TSPL_GENERATE_SALARY_ATTENDANCE T5 ON T5.Emp_code =T2.Emp_code  AND T1.SALARY_GENERATION_CODE=T5.SALARY_GENERATION_CODE  WHERE 2=2  AND T1.PAY_PERIOD_CODE in ( '" + fndPayperiod.Value + "') AND T1.LOCATION_CODE in ( '" + fndLocation.Value + "')  ) T1 LEFT JOIN TSPL_EMPLOYEE_MASTER T2 ON T1.EMP_CODE=T2.EMP_CODE  LEFT JOIN TSPL_DESIGNATION_MASTER T3 ON T2.DESIGNATION=T3.DESIGNATION_ID LEFT JOIN TSPL_DEPARTMENT_MASTER T4 ON T2.DEPARTMENT_CODE=T4.DEPARTMENT_CODE LEFT JOIN TSPL_DEVISION_MASTER Dev ON T1.DEVISION_CODE=Dev.DEVISION_CODE LEFT JOIN TSPL_LOCATION_MASTER LOC ON T1.LOCATION_CODE=LOC.LOCATION_CODE LEFT JOIN TSPL_BANK_MASTER Bank ON T2.BANK_CODE=Bank.BANK_CODE  left join TSPL_City_MASTER as Working_City on Working_City.City_Code  =T2.WORKING_City_CODE) AS ACD) AS X where 2=2   GROUP BY x.[Working City] , X.SALARY_GENERATION_CODE,X.EMP_CODE,X.EMPLOYEE_NAME,X.DESIGNATION,X.DEPARTMENT,X.DEVISION_NAME ,X.PAYPERIOD_DAYS,X.PRESENT_DAYS,X.PAYABLE_DAYS,X.PF_NO,X.BANK_ACC_NO,X.Birth_date,X.HOLIDAY_DAYS,X.WEEKLY_OFF_DAYS, x.SEX,x.GENERATE_DATE,
                     x.LIC_No,x.policy_no,x.membership_id,x.special_desc,
                     X.LEAVE_DAYS,X.ESI_NO,X.FATHERS_NAME,X.Bank_Name,X.Bank_Branch,X.Bank_Branch_Name,X.PAYMENT_MODE,X.Joining_date,X.RELIEVING_DATE,X.LOCATION_DESC) as Final  left join TSPL_GENERATE_SALARY_ATTENDANCE GSA ON Final.SALARY_GENERATION_CODE=GSA.SALARY_GENERATION_CODE  AND Final.EMP_CODE=GSA.EMP_CODE left join TSPL_GENERATE_SALARY GS ON Final.SALARY_GENERATION_CODE=GS.SALARY_GENERATION_CODE  left join TSPL_PAYPERIOD_MASTER PPM ON GS.PAY_PERIOD_CODE=PPM.PAY_PERIOD_CODE  LEFT JOIN TSPL_EMPLOYEE_STATUS EMPStatus on GSA.EMP_STATUS_CODE=EMPStatus.EMP_STATUS_CODE  LEFT JOIN (select TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_01,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then (case when HEAD_VALUE>0.01 then 0.01*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EPF_AC_10,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>0.01 then 0.01*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END) AS Salary_EDLI_AC_21,  max(CASE WHEN SUB_HEAD_TYPE='EPF' then ACTUAL_AMOUNT ELSE 0 END) AS EPF_Amount_AC_01,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPS_AMT_AC10>0 then CoEPS_AMT_AC10 ELSE 0 END) AS Pension_Amount_AC_10,  max(CASE WHEN SUB_HEAD_TYPE='EPF' and Actual_Amount>0 then (Actual_Amount-(case when HEAD_VALUE>PF_MAX_LM then CoEPS_AMT_AC10*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else CoEPS_AMT_AC10 end)) ELSE 0 END) AS Diff_Amount_AC_01,  (max(CASE WHEN SUB_HEAD_TYPE='EPF' and ACTUAL_AMOUNT>0 then (case when HEAD_VALUE>PF_MAX_LM then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*1.1/100 AS Admin_Amt_AC_02,  (max(CASE WHEN SUB_HEAD_TYPE='EPF' and CoEPF_AMT_AC01>0 then (case when HEAD_VALUE>0.01 then 0.01*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS else HEAD_VALUE end) ELSE 0 END))*0.5/100 AS EDLI_Amt_AC_21,  max(CASE WHEN SUB_HEAD_TYPE='EPF' then PF_MAX_LM*PAYABLE_DAYS/TSPL_GENERATE_SALARY_ATTENDANCE.PAYPERIOD_DAYS ELSE 0 END) AS PF_MAX_LM,  max(CoEPF_RATE_AC01) as CoEPF_RATE_AC01,max(CoEPF_AMT_AC01) as CoEPF_AMT_AC01,max(CoEPS_RATE_AC10) as CoEPS_RATE_AC10,  max(CoEPS_AMT_AC10) as CoEPS_AMT_AC10,max(EDLI_RATE_AC21) as EDLI_RATE_AC21,  max(EDLI_AMT_AC21) as EDLI_AMT_AC21,  max(CASE WHEN SUB_HEAD_TYPE='EMPESI' AND ACTUAL_AMOUNT>0 then HEAD_VALUE ELSE 0 END) as ESI_HEAD_VALUE,  max(CASE WHEN SUB_HEAD_TYPE='EMPESI' then ACTUAL_AMOUNT ELSE 0 END) AS ESI_Amount,  max(Co_ESI_RATE) as Co_ESI_RATE,max(Co_ESI_AMT) as Co_ESI_AMT  from TSPL_GENERATE_SALARY_PAYHEADS inner join TSPL_GENERATE_SALARY_ATTENDANCE on TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.SALARY_GENERATION_CODE and  TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE=TSPL_GENERATE_SALARY_ATTENDANCE.EMP_CODE where SUB_HEAD_TYPE in ('EPF','EMPESI')  group by TSPL_GENERATE_SALARY_PAYHEADS.SALARY_GENERATION_CODE,TSPL_GENERATE_SALARY_PAYHEADS.EMP_CODE) AS GSP ON Final.SALARY_GENERATION_CODE=GSP.SALARY_GENERATION_CODE  AND Final.EMP_CODE=GSP.EMP_CODE LEFT JOIN TSPL_EMPLOYEE_MASTER ON TSPL_EMPLOYEE_MASTER.EMP_CODE=Final.EMP_CODE  where vgsli>0  ORDER BY Final.EMP_CODE,PPM.DATE_FROM
                    "
            Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt2.Rows.Count > 0 AndAlso dt2.Columns.Contains("vgsli") Then
                For Each row As DataRow In dt2.Rows
                    If Not row("vgsli") Is DBNull.Value Then
                        totalVgsli += clsCommon.myCdbl(row("vgsli"))
                    End If
                Next
            End If

            If totalVgsli > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                If clsCommon.CompairString(clsCommon.myCstr(fndDeductioncode.Value), "KKK") <> CompairStringResult.Equal Then
                    frmCRV.funreport(CrystalReportFolder.HRPayroll, dt2, "rptDeductionRegister", "Sale Order")
                Else
                    frmCRV.funreport(CrystalReportFolder.HRPayroll, dt2, "rptDeductionRegisterkkk", "Sale Order")
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "No Data Found to Display", Me.Text)
                Exit Sub
            End If

            ' ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

End Class
