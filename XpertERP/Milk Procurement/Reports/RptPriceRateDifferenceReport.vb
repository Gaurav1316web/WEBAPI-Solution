Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
'==================created by shivani==========against ticket no[BM00000008832]==========>
Public Class RptPriceRateDifferenceReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private Sub TxtMultiMCC__My_Click(sender As Object, e As EventArgs) Handles TxtMultiMCC._My_Click
        Dim qry As String = "select MCC_Code as [Code] ,MCC_NAME as [Name] from TSPL_MCC_MASTER"
        TxtMultiMCC.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMCC", qry, "Code", "Name", TxtMultiMCC.arrValueMember, TxtMultiMCC.arrDispalyMember)
    End Sub

    Private Sub TxtMultiState__My_Click(sender As Object, e As EventArgs) Handles TxtMultiState._My_Click
        Dim qry As String = "SELECT STATE_CODE as [Code],STATE_NAME as [Name]  from TSPL_STATE_MASTER"
        TxtMultiState.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeState", qry, "Code", "Name", TxtMultiState.arrValueMember, TxtMultiState.arrDispalyMember)
    End Sub

    Private Sub TxtMultiPriceCode__My_Click(sender As Object, e As EventArgs) Handles TxtMultiPriceCode._My_Click
        Dim qry As String = "SELECT distinct Price_Code as [Code], Price_Type as [Name] from TSPL_MILK_PRICE_MASTER"
        TxtMultiPriceCode.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypePriceCode", qry, "Code", "Name", TxtMultiPriceCode.arrValueMember, TxtMultiPriceCode.arrDispalyMember)
    End Sub
    Sub load_report()
        Dim variable1 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select STUFF((Select ', ['+Charge_CODE+']' from (Select Distinct Charge_CODE from TSPL_FAT_SNF_UPLOADER_Chart_Detail) XXX For XML Path('')),1,1,'')"))
        Dim variable2 As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("Select ((Select ',isnull(['+Charge_CODE+'],0) as ['+Charge_CODE+']' from (Select Distinct Charge_CODE from TSPL_FAT_SNF_UPLOADER_Chart_Detail  ) XXX For XML Path('')))"))
        If txtFromDate.Value > txtToDate.Value Then
            common.clsCommon.MyMessageBoxShow("From date can not be greater then to Date", Me.Text)
            txtFromDate.Focus()
            Exit Sub
        End If

        'Ticket No-BHA/21/11/18-000690
        If clsCommon.myLen(variable1) = 0 Then
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
            Exit Sub
        End If

        Dim fromDate As String = txtFromDate.Value
        Dim Todate As String = txtToDate.Value
        Dim strqry As String = Nothing
        strqry = " (select MCC_Code,MCC_NAME,State_Code ,STATE_NAME,Price_Code, Effective_Date, Inactive_Date, Declared_Rate, Effective_Rate,convert(decimal(18,3), Diff) as Diff  " + variable2 + " from (select MCC_Code,MCC_NAME,State_Code ,STATE_NAME,Price_Code, Declared_Rate, Effective_Rate, Diff,Charge_CODE , Inactive_Date, Effective_Date,Rate from(SELECT DISTINCT TSPL_MILK_PRICE_MASTER.Price_Code,TSPL_FAT_SNF_UPLOADER_MASTER.code,TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code,MCC_NAME ,Charge_CODE,TSPL_FAT_SNF_UPLOADER_Chart_Detail.Rate ,( CONVERT(VARCHAR,Effective_Date,103)) AS Effective_Date ,(CONVERT(VARCHAR,Inactive_Date,103)) AS Inactive_Date ,(Milk_Rate ) AS Effective_Rate  ,(Declared_Rate) AS  Declared_Rate,(isnull(Declared_Rate,0)-isnull(Milk_Rate,0)) as Diff,tspl_mcc_master.State_Code ,STATE_NAME     FROM TSPL_FAT_SNF_UPLOADER_MASTER LEFT JOIN TSPL_MILK_PRICE_MASTER ON TSPL_MILK_PRICE_MASTER.Price_Code = TSPL_FAT_SNF_UPLOADER_MASTER.Price_Code LEFT JOIN TSPL_FAT_SNF_UPLOADER_MCC ON TSPL_FAT_SNF_UPLOADER_MCC.Code = TSPL_FAT_SNF_UPLOADER_MASTER.Code" & _
                 " left join TSPL_FAT_SNF_UPLOADER_Chart_Detail on TSPL_FAT_SNF_UPLOADER_Chart_Detail.Price_CODE = TSPL_FAT_SNF_UPLOADER_MASTER.code " & _
                 " left join tspl_mcc_master on tspl_mcc_master.MCC_Code =TSPL_FAT_SNF_UPLOADER_MCC.MCC_Code " & _
                 " left join TSPL_STATE_MASTER on TSPL_STATE_MASTER.STATE_CODE = tspl_mcc_master.State_Code " & _
                 " where 2=2 and convert(date,Effective_Date,103) >=convert(date,'" + fromDate + "',103) and convert(date,Effective_Date,103)<=convert(date,'" + Todate + "',103) and Price_Type = 'MCC'"
        If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
            strqry += " and TSPL_MCC_MASTER.MCC_Code in  (" + clsCommon.GetMulcallString(TxtMultiMCC.arrValueMember) + ") "
        End If
        If TxtMultiPriceCode.arrValueMember IsNot Nothing AndAlso TxtMultiPriceCode.arrValueMember.Count > 0 Then
            strqry += " and TSPL_MILK_PRICE_MASTER.Price_Code in  (" + clsCommon.GetMulcallString(TxtMultiPriceCode.arrValueMember) + ") "
        End If
        If TxtMultiState.arrValueMember IsNot Nothing AndAlso TxtMultiState.arrValueMember.Count > 0 Then
            strqry += " and TSPL_STATE_MASTER.STATE_CODE in  (" + clsCommon.GetMulcallString(TxtMultiState.arrValueMember) + ") "
        End If

        strqry += " )as m  )  final  pivot (sum(final.Rate) for final.Charge_CODE in ( " + variable1 + " ))t) "
        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(strqry)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then

            If dtgv.Columns.Count > 10 Then
                For i As Integer = dtgv.Columns.Count - 1 To 10 Step -1
                    Dim count As Decimal = Nothing
                    Dim count1 As Decimal = Nothing
                    Dim columname As String = clsCommon.myCstr(dtgv.Columns(i).ColumnName)
                    count = IIf(IsDBNull(dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)>0 ")), 0, dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)>0 "))
                    count1 = IIf(IsDBNull(dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)<0 ")), 0, dtgv.Compute("sum([" + columname + "])", " isnull([" + columname + "],0)<0 "))
                    If count = 0 AndAlso count1 = 0 Then
                        dtgv.Columns.RemoveAt(i)
                    End If
                Next
            End If

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            FormatGrid()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()


        gv.TableElement.TableHeaderHeight = 25
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 1 To gv.Columns.Count - 10
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = False

        Next

        
        gv.Columns("MCC_Code").IsVisible = True
        gv.Columns("MCC_Code").Width = 100
        gv.Columns("MCC_Code").HeaderText = "MCC Code"

        gv.Columns("MCC_NAME").IsVisible = True
        gv.Columns("MCC_NAME").Width = 100
        gv.Columns("MCC_NAME").HeaderText = "MCC Name"

        gv.Columns("State_Code").IsVisible = True
        gv.Columns("State_Code").Width = 80
        gv.Columns("State_Code").HeaderText = " State Code"

        gv.Columns("STATE_NAME").IsVisible = True
        gv.Columns("STATE_NAME").Width = 100
        gv.Columns("STATE_NAME").HeaderText = "State Name"

        gv.Columns("Price_Code").IsVisible = True
        gv.Columns("Price_Code").Width = 80
        gv.Columns("Price_Code").HeaderText = "Price Code"

        gv.Columns("Declared_Rate").IsVisible = True
        gv.Columns("Declared_Rate").Width = 80
        gv.Columns("Declared_Rate").HeaderText = "Declared Rate"

        gv.Columns("Effective_Rate").IsVisible = True
        gv.Columns("Effective_Rate").Width = 80
        gv.Columns("Effective_Rate").HeaderText = "Effective Rate"

        gv.Columns("Diff").IsVisible = True
        gv.Columns("Diff").Width = 80
        gv.Columns("Diff").HeaderText = "Difference"

       
        gv.Columns("Inactive_Date").IsVisible = True
        gv.Columns("Inactive_Date").Width = 100
        gv.Columns("Inactive_Date").HeaderText = "Closing date of price code"

        gv.Columns("Effective_Date").IsVisible = True
        gv.Columns("Effective_Date").Width = 100
        gv.Columns("Effective_Date").HeaderText = "Effective date of price code"
        Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim intCount As Integer = 0
            gv.ShowGroupPanel = False
            gv.MasterTemplate.AutoExpandGroups = True
            gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
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
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        load_report()
    End Sub
    Sub Reset()
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub BtnReset_Click(sender As Object, e As EventArgs) Handles BtnReset.Click
        Reset()
    End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")

                If TxtMultiMCC.arrValueMember IsNot Nothing AndAlso TxtMultiMCC.arrValueMember.Count > 0 Then
                    Dim strLocationName As String = clsCommon.GetMulcallStringWithComma(TxtMultiMCC.arrValueMember)
                    arrHeader.Add(("MCC : " + strLocationName + " "))

                End If
                If TxtMultiPriceCode.arrValueMember IsNot Nothing AndAlso TxtMultiPriceCode.arrValueMember.Count > 0 Then
                    Dim strvendor As String = clsCommon.GetMulcallStringWithComma(TxtMultiPriceCode.arrValueMember)
                    arrHeader.Add(("Price Code : " + strvendor + " "))

                End If
                If TxtMultiState.arrValueMember IsNot Nothing AndAlso TxtMultiState.arrValueMember.Count > 0 Then
                    Dim stritem As String = clsCommon.GetMulcallStringWithComma(TxtMultiState.arrValueMember)
                    arrHeader.Add(("State : " + stritem + " "))

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
                    clsCommon.MyExportToPDF("Price Rate Difference Report", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub


    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.RptPriceRateDifferenceReport)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

        RadSplitButton1.Visible = MyBase.isExport
    End Sub
    Private Sub RptPriceRateDifferenceReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")
        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)

        Reset()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub rmiExcel_Click(sender As Object, e As EventArgs) Handles rmiExcel.Click
        print(EnumExportTo.Excel)
    End Sub

    Private Sub rmiPDF_Click(sender As Object, e As EventArgs) Handles rmiPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
