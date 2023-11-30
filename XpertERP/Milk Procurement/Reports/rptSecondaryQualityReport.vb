Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Public Class RptSecondaryQualityReport
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()
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
        ''MyBase.SetUserMgmt(clsUserMgtCode.rptSecondaryQuality)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        RadSplitButton1.Visible = MyBase.isExport
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
    Sub LoadPlant()
        'Dim qry As String = " select Location_Code as Code ,Location_Desc Name from TSPL_LOCATION_MASTER where Type  ='Plant'  "
        Dim qry As String = Nothing
        If clsCommon.myLen(arrLoc) > 0 Then

            qry = "select TSPL_LOCATION_MASTER.Location_Code as [Code] ,TSPL_LOCATION_MASTER .Location_Desc as [Name] from TSPL_LOCATION_MASTER where isnull(CSA_Type,'N') ='N' and (isnull(GIT_Type,'N')='N' or isnull(GIT_Type,'N')='') and isnull(Is_Consumption_Location,0) =0  and isnull(Rejected_type,'N') ='N'  and Type  ='Plant' and Location_Code in (" + arrLoc + ") "

        Else
            btnGo.Enabled = False
        End If
        cbgPlant.DataSource = clsDBFuncationality.GetDataTable(qry)
        cbgPlant.ValueMember = "Code"
        cbgPlant.DisplayMember = "Name"

    End Sub
    'Sub LoadTanker()
    '    Dim qry As String = "select distinct TSPL_QUALITY_CHECK.Tanker_No as [Tanker No.],TSPL_TANKER_MASTER.Tanker_Name as [Tanker Name]  from TSPL_QUALITY_CHECK   left outer join TSPL_TANKER_MASTER on TSPL_TANKER_MASTER.Tanker_No =TSPL_QUALITY_CHECK.Tanker_No   "
    '    cbgTanker.DataSource = clsDBFuncationality.GetDataTable(qry)
    '    cbgTanker.ValueMember = "Tanker No."
    '    cbgTanker.DisplayMember = "Tanker Name"

    'End Sub
    Sub Reset()
        LOCATIONRIGTHS()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
        LoadMCC()
        LoadPlant()
        'LoadTanker()
        chkMCCAll.CheckState = CheckState.Checked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
        PlantAll.CheckState = CheckState.Checked
        If PlantAll.IsChecked Then
            cbgPlant.CheckedAll()
        Else
            cbgPlant.UnCheckedAll()
        End If
        'TankerAll.CheckState = CheckState.Checked
        gv.DataSource = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub RptSecondaryQualityReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnGo, "Press Alt+R Refresh ")

        ButtonToolTip.SetToolTip(BtnReset, "Press Alt+N Adding New")
        RadPageView1.SelectedPage = RadPageViewPage1
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = txtToDate.Value.AddMonths(-1)
       
        'LoadTanker()
        Reset()

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
        If PlantSelect.IsChecked AndAlso cbgPlant.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast single Plant or select all.", Me.Text)
            Exit Sub
        End If
        'If TankerSelect.IsChecked AndAlso cbgTanker.CheckedValue.Count = 0 Then
        '    clsCommon.MyMessageBoxShow("Please select atleast single Tanker or select all.")
        '    Exit Sub
        'End If
        '  Dim UOM As String = clsDBFuncationality.getSingleValue("select LEFT(  xx.Fields,len(xx.Fields)-1) as Param   from (select     ( select  distinct '[' + Description +'],'    from TSPL_PARAMETER_MASTER FOR XML PATH ('')) Fields) xx ")

        '' Changes by Parteek on 05/12/2017 for UDL data show on Secondary Setting QC Sreen
        Dim sQuery As String = ""
        sQuery = "select TSPL_SECONDARY_SETTING_QC_HEAD.qc_no as [Primary QC No],TSPL_SECONDARY_SETTING_QC_HEAD.document_no as [Secondary QC No],TSPL_SECONDARY_SETTING_QC_HEAD.Gate_Entry_No,TSPL_SECONDARY_SETTING_QC_HEAD.Vendor_Code as [Vendor Code],TSPL_SECONDARY_SETTING_QC_HEAD.Vendor_Desc as [Vendor Name] "
        sQuery += " ,TSPL_SECONDARY_SETTING_QC_HEAD.location_Code as [Location Code],TSPL_SECONDARY_SETTING_QC_HEAD.location_desc as [Locarion Name],TSPL_SECONDARY_SETTING_QC_HEAD.Tanker_No as [Tanker No],TSPL_SECONDARY_SETTING_QC_DETAIL.QCFatPer as [Primary QC Fat %],TSPL_SECONDARY_SETTING_QC_DETAIL.QCSNFPer as [Primary QC SNF %] "
        sQuery += " ,TSPL_SECONDARY_SETTING_QC_DETAIL.FatPer as [Secondary Fat %],TSPL_SECONDARY_SETTING_QC_DETAIL.SNFPer as [Secondary SNF %]"
        sQuery += " ,(isnull(convert(decimal(18,2),TSPL_SECONDARY_SETTING_QC_DETAIL.FatPer),0)-isnull(convert(decimal(18,2),TSPL_SECONDARY_SETTING_QC_DETAIL.QCFatPer),0)) as [Fat % Variance]"
        sQuery += " ,(isnull(convert(decimal(18,2),TSPL_SECONDARY_SETTING_QC_DETAIL.SNFPer),0)-isnull(convert(decimal(18,2),TSPL_SECONDARY_SETTING_QC_DETAIL.QCSNFPer),0)) as [SNF % Variance]"
        sQuery += " ,TSPL_SECONDARY_SETTING_QC_DETAIL.NetWeight,TSPL_SECONDARY_SETTING_QC_DETAIL.AdditinalWeightper as[Additional Weight %]  ,TSPL_SECONDARY_SETTING_QC_DETAIL.CalculatedAdditionalWeight as [Calculated Additional Weight],TSPL_SECONDARY_SETTING_QC_DETAIL.CHAMBER_DESC as [Chember Description] "
        sQuery += " from TSPL_SECONDARY_SETTING_QC_HEAD"
        sQuery += " left outer join TSPL_SECONDARY_SETTING_QC_DETAIL on TSPL_SECONDARY_SETTING_QC_DETAIL.Document_No=TSPL_SECONDARY_SETTING_QC_HEAD.Document_No"
        sQuery += " left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_SECONDARY_SETTING_QC_HEAD.location_Code  "
        sQuery += " left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code  =TSPL_SECONDARY_SETTING_QC_HEAD.Vendor_Code "
        sQuery += " LEFT Outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=TSPL_SECONDARY_SETTING_QC_HEAD.Gate_Entry_No "
        sQuery += " where 2=2 "
        sQuery += " and convert(date,TSPL_SECONDARY_SETTING_QC_HEAD.QC_Out_Date_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_SECONDARY_SETTING_QC_HEAD.QC_Out_Date_Time,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        If cbgMCC.CheckedValue.Count > 0 Then
            sQuery += "and ((TSPL_Weighment_Detail.Dispatched_From_Mcc  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") and isnull(TSPL_Weighment_Detail.Dispatched_From_Mcc,'')<>'')"
        End If
        If cbgPlant.CheckedValue.Count > 0 Then
            sQuery += "or (TSPL_LOCATION_MASTER.Location_Code IN (" + clsCommon.GetMulcallString(cbgPlant.CheckedValue) + ") and isnull(TSPL_Weighment_Detail.Dispatched_From_Mcc,'')='') )"
        End If
        '' END

        'If TankerSelect.IsChecked And cbgTanker.CheckedValue.Count > 0 Then
        '    sQuery += "and TSPL_QUALITY_CHECK.Tanker_No IN (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ") "
        'End If

        'sQuery += " FROM ("
        'sQuery += " SELECT "
        'sQuery += "   '' as [S.No],Convert(varchar,TSPL_QUALITY_CHECK.QC_Out_Date_Time,103)  as [Date],TSPL_QUALITY_CHECK.QC_Out_Date_Time  as DocDate,TSPL_QUALITY_CHECK.QC_No  as [QC No.] ,TSPL_LOCATION_MASTER.Location_Desc as [Plant],TSPL_QUALITY_CHECK.Tanker_No as [Tanker],case when ISNULL(TSPL_QUALITY_CHECK.Doc_Type,'')='BulkProc' then TSPL_QUALITY_CHECK.Vendor_Desc else TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Desc end as [Vendor/MCC],isnull(TSPL_Weighment_Detail.Net_Weight,0) as Qty ,Param_Field_Desc as [PARAM],Param_Field_Value as [Value] from TSPL_QC_Parameter_Detail left outer join TSPL_QUALITY_CHECK on TSPL_QUALITY_CHECK.QC_No=TSPL_QC_Parameter_Detail.QC_No left outer join TSPL_LOCATION_MASTER  on TSPL_LOCATION_MASTER .Location_Code =TSPL_QUALITY_CHECK.location_Code  left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code  =TSPL_QUALITY_CHECK.Vendor_Code LEFT Outer join TSPL_Weighment_Detail on TSPL_Weighment_Detail.Gate_Entry_No=TSPL_QUALITY_CHECK.Gate_Entry_No"
        'sQuery += " where 2=2 "
        'sQuery += " and convert(date,TSPL_QUALITY_CHECK.QC_Out_Date_Time,103)>=convert(date,'" + txtFromDate.Value + "',103) and convert(date,TSPL_QUALITY_CHECK.QC_Out_Date_Time,103) <=convert(date,'" + txtToDate.Value + "' ,103)"
        'If cbgMCC.CheckedValue.Count > 0 Then
        '    sQuery += "and ((TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Code  IN (" + clsCommon.GetMulcallString(cbgMCC.CheckedValue) + ") and isnull(TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Code,'')<>'')"
        'End If
        'If cbgPlant.CheckedValue.Count > 0 Then
        '    sQuery += "or (TSPL_LOCATION_MASTER.Location_Code IN (" + clsCommon.GetMulcallString(cbgPlant.CheckedValue) + ") and isnull(TSPL_QUALITY_CHECK.Dispatched_From_Mcc_Code,'')='') )"
        'End If
        ''If TankerSelect.IsChecked And cbgTanker.CheckedValue.Count > 0 Then
        ''    sQuery += "and TSPL_QUALITY_CHECK.Tanker_No IN (" + clsCommon.GetMulcallString(cbgTanker.CheckedValue) + ") "
        ''End If

        'sQuery += " ) as s  "
        'sQuery += "   Pivot("

        'sQuery += " max(value) "
        'sQuery += "FOR [Param] IN (" + UOM + ")"
        'sQuery += " )AS xx order by DocDate "
        

        


        Dim dtgv As New DataTable
        dtgv = clsDBFuncationality.GetDataTable(sQuery)
        If dtgv IsNot Nothing And dtgv.Rows.Count > 0 Then
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()
            gv.DataSource = dtgv
            gv.BestFitColumns()
           
            gv.TableElement.TableHeaderHeight = 25
            gv.MasterTemplate.ShowRowHeaderColumn = False
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = True
            Next
            gv.GroupDescriptors.Clear()
            gv.MasterTemplate.SummaryRowsBottom.Clear()
            RadPageView1.SelectedPage = RadPageViewPage2
        Else
            clsCommon.MyMessageBoxShow("No Data Found", Me.Text)
        End If
        ReStoreGridLayout()
    End Sub
    Sub FormatGrid()
        '  Dim strItemCode, head2 As String

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

        gv.Columns("S.No").IsVisible = True
        gv.Columns("S.No").Width = 30
        gv.Columns("S.No").HeaderText = " S.No"



        gv.Columns("Date").IsVisible = True
        gv.Columns("Date").Width = 100
        gv.Columns("Date").HeaderText = " Date"
        gv.Columns("Date").FormatString = "{0:d}"

        gv.Columns("Plant").IsVisible = True
        gv.Columns("Plant").Width = 100
        gv.Columns("Plant").HeaderText = " Plant"


        gv.Columns("Tanker").IsVisible = False
        gv.Columns("Tanker").Width = 100
        gv.Columns("Tanker").HeaderText = "Tanker"

        gv.Columns("MCC").IsVisible = False
        gv.Columns("MCC").Width = 100
        gv.Columns("MCC").HeaderText = "MCC"

        gv.Columns("Qty").IsVisible = True
        gv.Columns("Qty").Width = 80
        gv.Columns("Qty").HeaderText = "Qty"

        'gv.Columns("PARAM").IsVisible = True
        'gv.Columns("PARAM").Width = 80
        'gv.Columns("PARAM").HeaderText = "PARAM"

       
        'Dim summaryRowItem As New GridViewSummaryRowItem()
        'Dim intCount As Integer = 0

        'Dim item1 As New GridViewSummaryItem("Qty", "{0:F2}", GridAggregateFunction.Sum)
        'summaryRowItem.Add(item1)

        'gv.GroupDescriptors.Add(New GridGroupByExpression("MCC_Code as Item format ""{0}: {1}"" Group By MCC_Code"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("ROUTE_CODE as Item format ""{0}: {1}"" Group By ROUTE_CODE"))
        'gv.GroupDescriptors.Add(New GridGroupByExpression("VLC_CODE as Item format ""{0}: {1}"" Group By VLC_CODE"))

        'gv.ShowGroupPanel = False
        'gv.MasterTemplate.AutoExpandGroups = True

        'gv.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

    End Sub

    Private Sub chkMCCAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkMCCAll.ToggleStateChanged
        cbgMCC.Enabled = Not chkMCCAll.IsChecked
        If chkMCCAll.IsChecked Then
            cbgMCC.CheckedAll()
        Else
            cbgMCC.UnCheckedAll()
        End If
    End Sub

    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Load_Report()
    End Sub

    Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
        Reset()
    End Sub

    Private Sub rmEcxel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmEcxel.Click
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))

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

                If PlantSelect.IsChecked Then
                    Dim strPlantName As String = ""
                    For Each StrName As String In cbgPlant.CheckedDisplayMember
                        If clsCommon.myLen(strPlantName) > 0 Then
                            strPlantName += ", "
                        End If
                        strPlantName += StrName
                    Next
                    Dim strPlantCode As String = ""
                    For Each StrCode As String In cbgPlant.CheckedValue
                        If clsCommon.myLen(strPlantCode) > 0 Then
                            strPlantCode += ", "
                        End If
                        strPlantCode += StrCode
                    Next
                    arrHeader.Add((" Plant Name: " + strPlantName + " "))
                End If



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
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmiSaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiSaveLayout.Click
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
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information", Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub rmiDeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rmiDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

    Private Sub RptSecondaryQualityReport_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
       
    End Sub

    Private Sub RptSecondaryQualityReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            Load_Report()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.R Then
            Reset()
        End If
    End Sub

    Private Sub PlantAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles PlantAll.ToggleStateChanged
        cbgPlant.Enabled = Not PlantAll.IsChecked
        If PlantAll.IsChecked Then
            cbgPlant.CheckedAll()
        Else
            cbgPlant.UnCheckedAll()
        End If
    End Sub

    'Private Sub TankerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs)
    '    cbgTanker.Enabled = Not TankerAll.IsChecked
    'End Sub
    Sub print(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy")) + " ")
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & "'"))
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

                If PlantSelect.IsChecked Then
                    Dim strPlantName As String = ""
                    For Each StrName As String In cbgPlant.CheckedDisplayMember
                        If clsCommon.myLen(strPlantName) > 0 Then
                            strPlantName += ", "
                        End If
                        strPlantName += StrName
                    Next
                    Dim strPlantCode As String = ""
                    For Each StrCode As String In cbgPlant.CheckedValue
                        If clsCommon.myLen(strPlantCode) > 0 Then
                            strPlantCode += ", "
                        End If
                        strPlantCode += StrCode
                    Next
                    arrHeader.Add((" Plant Name: " + strPlantName + " "))
                End If

                If exporter = EnumExportTo.Excel Then
                    clsCommon.MyExportToExcelGrid("Secondary Quality", gv, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Secondary Quality", gv, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            Else
                common.clsCommon.MyMessageBoxShow("No Data Found to Export.", Me.Text)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub rmPDF_Click(sender As Object, e As EventArgs) Handles rmPDF.Click
        print(EnumExportTo.PDF)
    End Sub
End Class
