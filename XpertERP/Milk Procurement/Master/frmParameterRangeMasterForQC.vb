
'---------BM00000003263
'================BM00000003353

'======NOTE========when call for production module, trans_id='production' then 'tspl_parameter_master' used otherwise 'tspl_qc_log_sheet_master' use for parameters.
Imports common
Imports System.Data.SqlClient
Public Class frmParameterRangeMasterForQC
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim AllowDeduction_Pers As Boolean = False
    Dim SetItemWiseQualityCheckInGeneralPurchase As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Const colCode As String = "Code"
    Const colDesc As String = "Description"
    Const colLower As String = "Lower Range"
    Const colUpper As String = "Upper Range"
    Const colDate As String = "Effective Date"
    Const colStatus As String = "Status"
    Const colValue1 As String = "Value1"
    Const colValue2 As String = "Value2"
    Const colQcStatus As String = "QcStatus"
    Const colShowinDigitalAnalyzer As String = "colShowinDigitalAnalyzer"
    Const coltextinAnalyzer As String = "coltextinAnalyzer"
    Const colAnalyzerIndex As String = "colAnalyzerIndex"
    Const colDeductionPers As String = "colDeductionPers"
    Const colNature As String = "colNature"
    Const colDedLower As String = "colDedLower"
    Const colDedUpper As String = "colDedUpper"
    Const colDedRatio As String = "colDedRatio"
    Const colDedLower2 As String = "colDedLower2"
    Const colDedUpper2 As String = "colDedUpper2"
    Const colDedRatio2 As String = "colDedRatio2"
    Const colDedLower3 As String = "colDedLower3"
    Const colDedUpper3 As String = "colDedUpper3"
    Const colDedRatio3 As String = "colDedRatio3"
    Const colDes As String = "colDescription"
    Dim isLoadData As Boolean = False
    Dim isValueChanged As Boolean = True

    Dim FORMTYPE As String = Nothing
    Dim trans_id As String = "PRODUCTION"
    Dim LoadReadOnly As Boolean = True

#End Region

#Region "User Defined Functions and Subroutines"

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub
    Public Sub New()
        InitializeComponent()
    End Sub
#End Region

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(FORMTYPE)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
            Exit Sub
        End If
        btnsave.Visible = MyBase.isModifyFlag
        btndelete.Visible = MyBase.isDeleteFlag

        trans_id = "PRODUCTION"
        If clsCommon.CompairString(FORMTYPE, clsUserMgtCode.frmQualityModuleParameterRangeMaster) = CompairStringResult.Equal Then
            trans_id = "STANDARD"
        End If
    End Sub

    Sub Reset()
        isLoadData = False
        isValueChanged = True
        gv.Rows.Clear()
        gv.Rows.AddNew()
        UcAttachment1.Form_ID = Me.Form_ID
        UcAttachment1.BlankAllControls()
        fndParameterCode.Value = ""
        lblParamDesc.Text = ""
        txtDeductionLRange.Text = 0
        txtDeductionURange.Text = 0
        txtDeductionRatio.Text = 0
        txtLowerRange.Text = 0
        txtUpperRange.Text = 0
        txtQcStatus.Text = ""
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        btnsave.Enabled = False
        btnNew.Enabled = False
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.CurrentRow = gv.Rows(0)
        txtDescription.Text = ""
    End Sub

    Sub LoadData(ByVal readOnlyLoad As Boolean)
        Try

            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Rows.AddNew()
            LoadReadOnly = readOnlyLoad
            Dim qry As String = "select count(*) from tspl_parameter_range_master_QC where trans_id='" + trans_id + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)

            If check > 0 Then
                qry = "select tspl_parameter_range_master_qc.qc_param_code as code,tspl_parameter_range_master_qc.lower_range,tspl_parameter_range_master_qc.qc_status,tspl_parameter_range_master_qc.upper_range,TSPL_QC_LOG_SHEET_MASTER.description,TSPL_QC_LOG_SHEET_MASTER.nature,tspl_parameter_range_master_qc.effective_date,tspl_parameter_range_master_qc.status,tspl_parameter_range_master_qc.value1,tspl_parameter_range_master_qc.value2,tspl_parameter_range_master_qc.Deduction_Per,tspl_parameter_range_master_qc.Deduction_lower_range, tspl_parameter_range_master_qc.Deduction_upper_range, tspl_parameter_range_master_qc.Deduction_Ratio,tspl_parameter_range_master_qc.Deduction_lower_range2, tspl_parameter_range_master_qc.Deduction_upper_range2, tspl_parameter_range_master_qc.Deduction_Ratio2,tspl_parameter_range_master_qc.Deduction_lower_range3, tspl_parameter_range_master_qc.Deduction_upper_range3, tspl_parameter_range_master_qc.Deduction_Ratio3,tspl_parameter_range_master_qc.Show_in_Analyzer,tspl_parameter_range_master_qc.Text_in_Analyzer,Analyzer_Index, tspl_parameter_range_master_qc.Description as descrip from tspl_parameter_range_master_qc left outer join TSPL_QC_LOG_SHEET_MASTER on tspl_parameter_range_master_qc.qc_param_code=TSPL_QC_LOG_SHEET_MASTER.code where tspl_parameter_range_master_qc.trans_id='" + trans_id + "'"
                If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                    qry = "select tspl_parameter_range_master_qc.qc_param_code as code,tspl_parameter_range_master_qc.lower_range,tspl_parameter_range_master_qc.qc_status,tspl_parameter_range_master_qc.upper_range,TSPL_QC_LOG_SHEET_MASTER.description,TSPL_QC_LOG_SHEET_MASTER.nature,tspl_parameter_range_master_qc.effective_date,tspl_parameter_range_master_qc.status,tspl_parameter_range_master_qc.value1,tspl_parameter_range_master_qc.value2,tspl_parameter_range_master_qc.Deduction_Per,tspl_parameter_range_master_qc.Deduction_lower_range, tspl_parameter_range_master_qc.Deduction_upper_range, tspl_parameter_range_master_qc.Deduction_Ratio,tspl_parameter_range_master_qc.Deduction_lower_range2, tspl_parameter_range_master_qc.Deduction_upper_range2, tspl_parameter_range_master_qc.Deduction_Ratio2,tspl_parameter_range_master_qc.Deduction_lower_range3, tspl_parameter_range_master_qc.Deduction_upper_range3, tspl_parameter_range_master_qc.Deduction_Ratio3,tspl_parameter_range_master_qc.Show_in_Analyzer,tspl_parameter_range_master_qc.Text_in_Analyzer,Analyzer_Index, tspl_parameter_range_master_qc.Description as descrip from tspl_parameter_range_master_qc left outer join TSPL_QC_LOG_SHEET_MASTER on tspl_parameter_range_master_qc.qc_param_code=TSPL_QC_LOG_SHEET_MASTER.code where tspl_parameter_range_master_qc.trans_id='" + trans_id + "'"
                End If
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                isLoadData = True
                If readOnlyLoad = False Then
                    LoadBlankGrid()
                    gv.ReadOnly = False
                    gv.AllowAddNewRow = True
                    gv.EnableFiltering = False
                    gv.EnableSorting = False
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        For Each dr As DataRow In dt.Rows
                            gv.Rows(gv.Rows.Count - 1).Cells(colNature).Value = clsCommon.myCstr(dr("nature"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colCode).Value = clsCommon.myCstr(dr("code"))

                            gv.Rows(gv.Rows.Count - 1).Cells(colDesc).Value = clsCommon.myCstr(dr("description"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colLower).Value = clsCommon.myCdbl(dr("lower_range"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colUpper).Value = clsCommon.myCdbl(dr("upper_range"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colStatus).Value = clsCommon.myCstr(dr("status"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colValue1).Value = clsCommon.myCstr(dr("value1"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colQcStatus).Value = clsCommon.myCstr(dr("Qc_Status"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colShowinDigitalAnalyzer).Value = clsCommon.myCstr(dr("Show_in_Analyzer"))
                            gv.Rows(gv.Rows.Count - 1).Cells(coltextinAnalyzer).Value = clsCommon.myCstr(dr("Text_in_Analyzer"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colAnalyzerIndex).Value = clsCommon.myCdbl(dr("Analyzer_Index"))

                            'gv.Rows(gv.Rows.Count - 1).Cells(colValue2).Value = clsCommon.myCstr(dr("value2"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDeductionPers).Value = clsCommon.myCdbl(dr("Deduction_Per"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDedLower).Value = clsCommon.myCdbl(dr("Deduction_lower_range"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDedUpper).Value = clsCommon.myCdbl(dr("Deduction_upper_range"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDedRatio).Value = clsCommon.myCdbl(dr("Deduction_Ratio"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDedLower2).Value = clsCommon.myCdbl(dr("Deduction_lower_range2"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDedUpper2).Value = clsCommon.myCdbl(dr("Deduction_upper_range2"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDedRatio2).Value = clsCommon.myCdbl(dr("Deduction_Ratio2"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDedLower3).Value = clsCommon.myCdbl(dr("Deduction_lower_range3"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDedUpper3).Value = clsCommon.myCdbl(dr("Deduction_upper_range3"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDedRatio3).Value = clsCommon.myCdbl(dr("Deduction_Ratio3"))
                            gv.Rows(gv.Rows.Count - 1).Cells(colDes).Value = clsCommon.myCstr(dr("descrip"))
                            Try
                                gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value = Convert.ToDateTime(dr("effective_date"))
                            Catch exx As Exception
                                gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                            End Try

                            Try
                                If gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value.ToString().Substring(6, 4) = "0001" Then
                                    gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                                End If
                            Catch exx As Exception
                                gv.Rows(gv.Rows.Count - 1).Cells(colDate).Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                            End Try
                            gv.Rows.AddNew()
                        Next

                        btnsave.Text = "Update"
                        btndelete.Enabled = True

                        gv.CurrentRow = gv.Rows(0)
                    End If
                Else
                    gv.Rows.Clear()
                    gv.Columns.Clear()
                    gv.DataSource = Nothing
                    gv.DataSource = dt
                    gv.BestFitColumns()
                    gv.ReadOnly = True
                    'gv.AllowAddNewRow = False
                    gv.AllowAddNewRow = True
                    gv.EnableFiltering = True
                    gv.EnableSorting = True
                    Dim icount As Integer = clsCommon.myCdbl(gv.Rows.Count - 1)
                    gv.TableElement.ScrollToRow(icount)
                End If

            End If
            isLoadData = False
        Catch ex As Exception
            isLoadData = False
        End Try
    End Sub

    Private Sub frmParameterRangeMasterForQC_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N Then
            LoadData(False)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnsave.Enabled Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btndelete.Enabled Then
            btndelete.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()

        End If

        If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colCode) Then
            isValueChanged = False
            OpenParameter(True)
            isValueChanged = True
        End If
        If e.KeyCode = Keys.F2 AndAlso gv.CurrentColumn IsNot Nothing AndAlso gv.CurrentColumn Is gv.Columns(colValue1) AndAlso clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value)) > 0 Then
            isValueChanged = False
            gv.CurrentCell.Value = clsCommon.myCstr(OpenParameterValueList(clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value), clsCommon.myCstr(gv.CurrentCell.Value)))
            isValueChanged = True
        End If
    End Sub

    Private Sub frmParameterRangeMasterForQC_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()

        AllowDeduction_Pers = IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.AllowDeductionPercentOnIncoming, clsFixedParameterCode.AllowDeductionPercentOnIncoming, Nothing)) = "1", True, False)
        SetItemWiseQualityCheckInGeneralPurchase = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ItemWiseQualityCheckInGeneralPurchase, clsFixedParameterCode.ItemWiseQualityCheckInGeneralPurchase, Nothing)) = 1)
        LoadBlankGrid()
        Reset()
        LoadData(True)

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N for reload data ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")

        RadPageViewPage2.Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.SelectedPage = RadPageViewPage1

    End Sub

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Name = colCode
        repocode.Width = 155
        repocode.HeaderText = "Parameter Code"
        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gv.MasterTemplate.Columns.Add(repocode)
        repocode = Nothing

        Dim reponame As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        reponame.FormatString = ""
        reponame.Name = colDesc
        reponame.Width = 215
        reponame.HeaderText = "Description"
        reponame.ReadOnly = True
        gv.MasterTemplate.Columns.Add(reponame)
        reponame = Nothing

        Dim repolower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repolower.Name = colLower
        repolower.Width = 80
        repolower.HeaderText = "Lower Range"
        repolower.FormatString = "{0:n3}"
        'repolower.ReadOnly = True
        repolower.DecimalPlaces = 3
        gv.MasterTemplate.Columns.Add(repolower)
        repolower = Nothing

        Dim repoupper As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoupper.Name = colUpper
        repoupper.Width = 80
        repoupper.HeaderText = "Upper Range"
        repoupper.FormatString = "{0:n3}"
        repoupper.DecimalPlaces = 3
        'repoupper.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoupper)
        repoupper = Nothing

        Dim repovalue1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repovalue1.Name = colValue1
        'repovalue1.DataSource = loadParamValueList()
        'repovalue1.DisplayMember = "Value"
        'repovalue1.ValueMember = "Value"
        repovalue1.Width = 180
        repovalue1.ReadOnly = True
        repovalue1.HeaderText = "Value"
        'repovalue1.MaxLength = 30
        gv.MasterTemplate.Columns.Add(repovalue1)
        repovalue1 = Nothing

        'Dim repovalue2 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'repovalue2.Name = colValue2
        'repovalue2.Width = 80
        'repovalue2.HeaderText = "Value-2"
        'repovalue2.MaxLength = 30
        'gv.MasterTemplate.Columns.Add(repovalue2)

        Dim repostatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repostatus.Name = colStatus
        repostatus.Width = 80
        repostatus.HeaderText = "Status(Yes/No)"
        repostatus.DataSource = LoadCombobox()
        repostatus.ValueMember = "Code"
        repostatus.DisplayMember = "Name"
        'repostatus.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repostatus)
        repostatus = Nothing

        Dim repoQCstatus As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoQCstatus.Name = colQcStatus
        repoQCstatus.Width = 80
        repoQCstatus.HeaderText = "QC Status"
        repoQCstatus.DataSource = LoadQCCombobox()
        repoQCstatus.ValueMember = "Code"
        repoQCstatus.DisplayMember = "Name"
        'repostatus.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoQCstatus)
        repoQCstatus = Nothing

        repolower = New GridViewDecimalColumn()
        repolower.Name = colDeductionPers
        repolower.Width = 80
        repolower.HeaderText = "Deduction%"
        repolower.FormatString = "{0:n3}"
        repolower.DecimalPlaces = 3
        repolower.WrapText = True
        repolower.IsVisible = AllowDeduction_Pers
        repolower.VisibleInColumnChooser = AllowDeduction_Pers
        gv.MasterTemplate.Columns.Add(repolower)
        repolower = Nothing

        Dim repodate As GridViewDateTimeColumn = New GridViewDateTimeColumn()
        repodate.FormatString = ""
        repodate.Name = colDate
        repodate.Width = 80
        repodate.HeaderText = "Effective Date"
        gv.MasterTemplate.Columns.Add(repodate)
        repodate = Nothing


        Dim repoQCShowinAnalyzer As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoQCShowinAnalyzer.Name = colShowinDigitalAnalyzer
        repoQCShowinAnalyzer.Width = 150
        repoQCShowinAnalyzer.HeaderText = "Show in Digital Analyzer"
        repoQCShowinAnalyzer.DataSource = LoadCombobox()
        repoQCShowinAnalyzer.ValueMember = "Code"
        repoQCShowinAnalyzer.DisplayMember = "Name"
        'repostatus.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoQCShowinAnalyzer)
        repoQCShowinAnalyzer = Nothing

        Dim repoQCTextinAnalyzer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoQCTextinAnalyzer.Name = coltextinAnalyzer
        repoQCTextinAnalyzer.Width = 150
        repoQCTextinAnalyzer.HeaderText = "Text in Digital Analyzer"
        gv.MasterTemplate.Columns.Add(repoQCTextinAnalyzer)
        repoQCTextinAnalyzer = Nothing

        Dim repoQCTextinAnalyzer_Index As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQCTextinAnalyzer_Index.Name = colAnalyzerIndex
        repoQCTextinAnalyzer_Index.Width = 20
        repoQCTextinAnalyzer_Index.HeaderText = "Analyzer Index"
        gv.MasterTemplate.Columns.Add(repoQCTextinAnalyzer_Index)
        repoQCTextinAnalyzer_Index = Nothing

        Dim repoNature As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoNature.Name = colNature
        repoNature.Width = 20
        repoNature.HeaderText = "Nature"
        repoNature.IsVisible = False
        gv.MasterTemplate.Columns.Add(repoNature)
        repoNature = Nothing

        Dim repoDedlower As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDedlower.Name = colDedLower
        repoDedlower.Width = 80
        repoDedlower.HeaderText = "Deduction Lower Range1"
        repoDedlower.FormatString = "{0:n3}"
        'repolower.ReadOnly = True
        repoDedlower.DecimalPlaces = 3
        gv.MasterTemplate.Columns.Add(repoDedlower)
        repoDedlower = Nothing

        Dim repoDedupper As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDedupper.Name = colDedUpper
        repoDedupper.Width = 80
        repoDedupper.HeaderText = "Deduction Upper Range1"
        repoDedupper.FormatString = "{0:n3}"
        repoDedupper.DecimalPlaces = 3
        'repoupper.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoDedupper)
        repoDedupper = Nothing

        repolower = New GridViewDecimalColumn()
        repolower.Name = colDedRatio
        repolower.Width = 80
        repolower.HeaderText = "Deduction Ratio1"
        repolower.FormatString = "{0:n3}"
        repolower.DecimalPlaces = 3
        repolower.WrapText = True
        gv.MasterTemplate.Columns.Add(repolower)
        repolower = Nothing

        Dim repoDedlower2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDedlower2.Name = colDedLower2
        repoDedlower2.Width = 80
        repoDedlower2.HeaderText = "Deduction Lower Range2"
        repoDedlower2.FormatString = "{0:n3}"
        repoDedlower2.DecimalPlaces = 3
        gv.MasterTemplate.Columns.Add(repoDedlower2)
        repoDedlower2 = Nothing

        Dim repoDedupper2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDedupper2.Name = colDedUpper2
        repoDedupper2.Width = 80
        repoDedupper2.HeaderText = "Deduction Upper Range2"
        repoDedupper2.FormatString = "{0:n3}"
        repoDedupper2.DecimalPlaces = 3
        gv.MasterTemplate.Columns.Add(repoDedupper2)
        repoDedupper2 = Nothing

        repolower = New GridViewDecimalColumn()
        repolower.Name = colDedRatio2
        repolower.Width = 80
        repolower.HeaderText = "Deduction Ratio2"
        repolower.FormatString = "{0:n3}"
        repolower.DecimalPlaces = 3
        repolower.WrapText = True
        gv.MasterTemplate.Columns.Add(repolower)
        repolower = Nothing

        Dim repoDedlower3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDedlower3.Name = colDedLower3
        repoDedlower3.Width = 80
        repoDedlower3.HeaderText = "Deduction Lower Range3"
        repoDedlower3.FormatString = "{0:n3}"
        repoDedlower3.DecimalPlaces = 3
        gv.MasterTemplate.Columns.Add(repoDedlower3)
        repoDedlower3 = Nothing

        Dim repoDedupper3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDedupper3.Name = colDedUpper3
        repoDedupper3.Width = 80
        repoDedupper3.HeaderText = "Deduction Upper Range3"
        repoDedupper3.FormatString = "{0:n3}"
        repoDedupper3.DecimalPlaces = 3
        gv.MasterTemplate.Columns.Add(repoDedupper3)
        repoDedupper3 = Nothing

        repolower = New GridViewDecimalColumn()
        repolower.Name = colDedRatio3
        repolower.Width = 80
        repolower.HeaderText = "Deduction Ratio3"
        repolower.FormatString = "{0:n3}"
        repolower.DecimalPlaces = 3
        repolower.WrapText = True
        gv.MasterTemplate.Columns.Add(repolower)
        repolower = Nothing

        'If clsFixedParameter.GetData(clsFixedParameterType.DisplayAllParameterinQualityCheck, clsFixedParameterCode.DisplayAllParameterinQualityCheck, Nothing) = "1" Then
        '    gv.Columns(colShowinDigitalAnalyzer).IsVisible = True
        '    gv.Columns(coltextinAnalyzer).IsVisible = True
        '    gv.Columns(colAnalyzerIndex).IsVisible = True
        'Else
        gv.Columns(colShowinDigitalAnalyzer).IsVisible = False
        gv.Columns(coltextinAnalyzer).IsVisible = False
        gv.Columns(colAnalyzerIndex).IsVisible = False
        ' End If

        gv.AllowDeleteRow = True
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.EnableFiltering = True
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False
        gv.Rows.AddNew()
    End Sub

    Function loadParamValueList() As DataTable
        Dim qry As String = "select 'Select/Show' as Value union All select 'Create New' as Value "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Function OpenParameterValueList(ByVal code As String, ByVal strValue As String) As String
        Dim strRetValue As String = String.Empty
        Dim table_name As String = "tspl_parameter_master"
        If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
            table_name = "TSPL_QC_LOG_SHEET_MASTER"
        End If

        Dim cnt As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(" select count(*) from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "))
        If cnt <> 0 Then
            Dim frm As FrmCheckBoxGrid = New FrmCheckBoxGrid()
            Dim strValueList() As String = strValue.Split(",")
            frm.qry = " select Value as Value from TSPL_PARAMEter_value_master where parameter_code='" & code & "' "
            frm.qry = frm.qry
            frm.arrValue = New List(Of String)
            For i As Integer = 0 To strValueList.Count - 1
                frm.arrValue.Add(strValueList(i))
            Next
            frm.ShowDialog()

            If frm.arrValue IsNot Nothing AndAlso frm.arrValue.Count > 0 Then
                For i As Integer = 0 To frm.arrValue.Count - 1
                    strRetValue = strRetValue & frm.arrValue(i).ToString
                    If i <> frm.arrValue.Count - 1 Then
                        strRetValue = strRetValue & ","
                    End If
                Next
            End If
        Else
            
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from " + table_name + " where code='" & code & "' and nature='A'")) > 0 Then
                clsCommon.MyMessageBoxShow(Me, "No record found.", Me.Text)
            End If
        End If
        Return strRetValue
    End Function

    Function LoadCombobox() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'YES' as Code,'YES' as Name union all select 'NO' as Code,'NO' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function
    Function LoadQCCombobox() As DataTable
        Dim qry As String = "select * from (select '' as Code,'None' as Name union all select 'OK' as Code,'OK' as Name union all select 'NOT OK' as Code,'NOT OK' as Name)a"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Function AllowToSave() As Boolean
        Try
            Dim table_name As String = "tspl_parameter_master"
            If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                table_name = "TSPL_QC_LOG_SHEET_MASTER"
            End If

            Dim code As String = ""
            Dim lrange As Decimal = Nothing
            Dim urange As Decimal = Nothing
            Dim status As String = Nothing
            Dim value1 As String = Nothing
            Dim value2 As String = Nothing
            Dim code1 As String = ""
            Dim lrange1 As Decimal = Nothing
            Dim urange1 As Decimal = Nothing
            Dim status1 As String = Nothing
            Dim Qcstatus As String = Nothing
            Dim value1_1 As String = Nothing
            Dim value2_1 As String = Nothing
            Dim Qcstatus1 As String = Nothing
            Dim ShowinDigitalAnalyzer As String = Nothing
            Dim Analyzer_index As Integer = 0
            Dim TextinDigitalAnalyzer As String = Nothing
            Dim ShowinDigitalAnalyzer1 As String = Nothing
            Dim TextinDigitalAnalyzer1 As String = Nothing
            Dim Analyzer_Index1 As Integer = 0
            Dim Deduction_lower_range As Decimal = Nothing
            Dim Deduction_upper_range As Decimal = Nothing
            Dim Deduction_lower_range2 As Decimal = Nothing
            Dim Deduction_upper_range2 As Decimal = Nothing
            Dim Deduction_lower_range3 As Decimal = Nothing
            Dim Deduction_upper_range3 As Decimal = Nothing
            code = clsCommon.myCstr(gv.Rows(0).Cells(colCode).Value)
            If clsCommon.myLen(code) <= 0 Then
                Throw New Exception("Please Fill Atleast One Parameter Range")
            End If
            Dim III As Double = clsCommon.myCdbl(gv.Rows(0).Cells(colLower).Value)

            For ii As Integer = 0 To gv.Rows.Count - 1
                code = clsCommon.myCstr(gv.Rows(ii).Cells(colCode).Value)
                lrange = clsCommon.myCdbl(gv.Rows(ii).Cells(colLower).Value)
                urange = clsCommon.myCdbl(gv.Rows(ii).Cells(colUpper).Value)
                status = clsCommon.myCstr(gv.Rows(ii).Cells(colStatus).Value)
                value1 = clsCommon.myCstr(gv.Rows(ii).Cells(colValue1).Value)
                Qcstatus = clsCommon.myCstr(gv.Rows(ii).Cells(colQcStatus).Value)
                ShowinDigitalAnalyzer = clsCommon.myCstr(gv.Rows(ii).Cells(colShowinDigitalAnalyzer).Value)
                TextinDigitalAnalyzer = clsCommon.myCstr(gv.Rows(ii).Cells(coltextinAnalyzer).Value)
                Analyzer_index = clsCommon.myCdbl(gv.Rows(ii).Cells(colAnalyzerIndex).Value)
                Deduction_lower_range = clsCommon.myCdbl(gv.Rows(ii).Cells(colDedLower).Value)
                Deduction_upper_range = clsCommon.myCdbl(gv.Rows(ii).Cells(colDedUpper).Value)
                Deduction_lower_range2 = clsCommon.myCdbl(gv.Rows(ii).Cells(colDedLower2).Value)
                Deduction_upper_range2 = clsCommon.myCdbl(gv.Rows(ii).Cells(colDedUpper2).Value)
                Deduction_lower_range3 = clsCommon.myCdbl(gv.Rows(ii).Cells(colDedLower3).Value)
                Deduction_upper_range3 = clsCommon.myCdbl(gv.Rows(ii).Cells(colDedUpper3).Value)
                ' value2 = clsCommon.myCstr(gv.Rows(ii).Cells(colValue2).Value)

                Dim qry As String = "select nature from " + table_name + " where code='" + code + "'"
                Dim nature As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                If nature = "A" AndAlso clsCommon.myLen(code) > 0 AndAlso (clsCommon.myLen(value1) <= 0 AndAlso clsCommon.myLen(value2) <= 0) Then
                    gv.CurrentRow = gv.Rows(ii)
                    Throw New Exception("Fill Value Of Parameter Nature(Alphanumeric) At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                If nature = "R" AndAlso clsCommon.myLen(code) > 0 AndAlso (clsCommon.myLen(lrange) = 0 Or clsCommon.myLen(urange) = 0) Then
                    gv.CurrentRow = gv.Rows(ii)
                    Throw New Exception("Fill Lower/Upper Range Of Parameter Nature(Range) At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If
                If nature = "B" AndAlso clsCommon.myLen(code) > 0 AndAlso (clsCommon.myLen(status) <= 0) Then
                    gv.CurrentRow = gv.Rows(ii)
                    Throw New Exception("Fill Status Of Parameter Nature(Boolean) At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                If clsCommon.myLen(code) > 0 AndAlso clsCommon.myCdbl(lrange) = 0 AndAlso clsCommon.myCdbl(urange) = 0 AndAlso clsCommon.myLen(status) <= 0 AndAlso clsCommon.myLen(value1) <= 0 AndAlso clsCommon.myLen(value2) <= 0 Then
                    gv.CurrentRow = gv.Rows(ii)
                    Throw New Exception("Please Fill All Information Likes Lower Range/Upper Range Or Value-1/Value-2 Or Status,Effective Date  At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                If clsCommon.myLen(code) > 0 AndAlso (clsCommon.myCdbl(lrange) <> 0 OrElse clsCommon.myCdbl(urange) <> 0) AndAlso clsCommon.myCdbl(lrange) > clsCommon.myCdbl(urange) Then
                    gv.CurrentRow = gv.Rows(ii)
                    Throw New Exception("Lower Range Should Be Less Than Upper Range At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                If clsCommon.myLen(Qcstatus) <= 0 AndAlso clsCommon.myLen(code) > 0 Then
                    gv.CurrentRow = gv.Rows(ii)
                    Throw New Exception("Please Select Qc Status (Ok/Not Ok) At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                If clsCommon.CompairString(clsCommon.myCstr(ShowinDigitalAnalyzer), "NO") = CompairStringResult.Equal AndAlso clsCommon.myLen(TextinDigitalAnalyzer) > 0 Then
                    gv.CurrentRow = gv.Rows(ii)
                    Throw New Exception("Please Select Show in Digital Analyzer (Yes/No) At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If

                If clsCommon.myLen(TextinDigitalAnalyzer) <= 0 AndAlso clsCommon.CompairString(clsCommon.myCstr(ShowinDigitalAnalyzer), "YES") = CompairStringResult.Equal Then
                    gv.CurrentRow = gv.Rows(ii)
                    Throw New Exception("Please Type Name in Text in Digital Analyzer Field At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If
                'Deduction Range
                If Deduction_lower_range > Deduction_upper_range Then
                    Throw New Exception("Deduction Lower Range1 Value Should Not be Greater than Upper Range1 Value At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If
                If Deduction_lower_range2 > Deduction_upper_range2 Then
                    Throw New Exception("Deduction Lower Range2 Value Should Not be Greater than Upper Range2 Value At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If
                If Deduction_lower_range3 > Deduction_upper_range3 Then
                    Throw New Exception("Deduction Lower Range3 Value Should Not be Greater than Upper Range3 Value At Row No. " + clsCommon.myCstr(CInt(ii) + 1) + "")
                End If
                For jj As Integer = 0 To gv.Rows.Count - 1
                    If jj = ii Then Continue For
                    code1 = clsCommon.myCstr(gv.Rows(jj).Cells(colCode).Value)
                    lrange1 = clsCommon.myCdbl(gv.Rows(jj).Cells(colLower).Value)
                    urange1 = clsCommon.myCdbl(gv.Rows(jj).Cells(colUpper).Value)
                    status1 = clsCommon.myCstr(gv.Rows(jj).Cells(colStatus).Value)
                    value1_1 = clsCommon.myCstr(gv.Rows(jj).Cells(colValue1).Value)
                    Qcstatus1 = clsCommon.myCstr(gv.Rows(jj).Cells(colQcStatus).Value)
                    ShowinDigitalAnalyzer1 = clsCommon.myCstr(gv.Rows(jj).Cells(colShowinDigitalAnalyzer).Value)
                    TextinDigitalAnalyzer1 = clsCommon.myCstr(gv.Rows(jj).Cells(coltextinAnalyzer).Value)
                    Analyzer_Index1 = clsCommon.myCdbl(gv.Rows(jj).Cells(colAnalyzerIndex).Value)
                    'value2_1 = clsCommon.myCstr(gv.Rows(ii).Cells(colValue2).Value)
                    'If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                    '    If clsCommon.myLen(code) > 0 AndAlso (clsCommon.CompairString(code, code1) = CompairStringResult.Equal) Then
                    '        gv.CurrentRow = gv.Rows(jj)
                    '        Throw New Exception("Duplicate value at row no. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                    '    End If
                    'Else
                    If clsCommon.myLen(code) > 0 AndAlso (clsCommon.CompairString(code, code1) = CompairStringResult.Equal) AndAlso (lrange >= lrange1 AndAlso lrange <= urange1) AndAlso (urange >= lrange1 AndAlso urange <= urange1) AndAlso (clsCommon.CompairString(status, status1) = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(value1, value1_1) = CompairStringResult.Equal) AndAlso (clsCommon.CompairString(Qcstatus, Qcstatus1) = CompairStringResult.Equal) Then
                        gv.CurrentRow = gv.Rows(jj)
                        Throw New Exception("Duplicate value at row no. " + clsCommon.myCstr(CInt(ii) + 1) + " and " + clsCommon.myCstr(CInt(jj) + 1) + "")
                    End If
                    If clsCommon.myLen(code) > 0 AndAlso (clsCommon.CompairString(Analyzer_index, Analyzer_Index1) = CompairStringResult.Equal) And clsCommon.myCdbl(Analyzer_Index1) <> 0 Then
                        gv.CurrentRow = gv.Rows(jj)
                        Throw New Exception("Duplicate Analyzer Index at row no. " + clsCommon.myCstr(CInt(jj) + 1) + "")
                    End If
                    'End If
                Next
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Function

    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmQualityModuleParameterRangeMaster, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim obj As New clsParameterRangeMasterForQC
        Dim arr As New List(Of clsParameterRangeMasterForQC)
        Try

            For Each grow As GridViewRowInfo In gv.Rows
                obj = New clsParameterRangeMasterForQC()

                If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                    obj.QC_Param_Code = clsCommon.myCstr(grow.Cells(colCode).Value)
                Else
                    obj.code = clsCommon.myCstr(grow.Cells(colCode).Value)
                End If

                obj.Trans_Id = trans_id
                obj.Lrange = clsCommon.myCdbl(grow.Cells(colLower).Value)
                obj.Urange = clsCommon.myCdbl(grow.Cells(colUpper).Value)
                obj.status = clsCommon.myCstr(grow.Cells(colStatus).Value)
                obj.value1 = clsCommon.myCstr(grow.Cells(colValue1).Value)
                obj.Qc_Status = clsCommon.myCstr(grow.Cells(colQcStatus).Value)
                obj.ShowinDigitalAnalyzer = clsCommon.myCstr(grow.Cells(colShowinDigitalAnalyzer).Value)
                obj.TextinDigitalAnalyzer = clsCommon.myCstr(grow.Cells(coltextinAnalyzer).Value)
                obj.Analyzer_Index = clsCommon.myCstr(grow.Cells(colAnalyzerIndex).Value)

                obj.Deduction_Per = clsCommon.myCdbl(grow.Cells(colDeductionPers).Value)
                obj.Deduction_lower_range = clsCommon.myCdbl(grow.Cells(colDedLower).Value)
                obj.Deduction_upper_range = clsCommon.myCdbl(grow.Cells(colDedUpper).Value)
                obj.Deduction_Ratio = clsCommon.myCdbl(grow.Cells(colDedRatio).Value)
                obj.Deduction_lower_range2 = clsCommon.myCdbl(grow.Cells(colDedLower2).Value)
                obj.Deduction_upper_range2 = clsCommon.myCdbl(grow.Cells(colDedUpper2).Value)
                obj.Deduction_Ratio2 = clsCommon.myCdbl(grow.Cells(colDedRatio2).Value)
                obj.Deduction_lower_range3 = clsCommon.myCdbl(grow.Cells(colDedLower3).Value)
                obj.Deduction_upper_range3 = clsCommon.myCdbl(grow.Cells(colDedUpper3).Value)
                obj.Deduction_Ratio3 = clsCommon.myCdbl(grow.Cells(colDedRatio3).Value)
                obj.Description = clsCommon.myCdbl(grow.Cells(colDes).Value)
                'obj.value2 = clsCommon.myCstr(grow.Cells(colValue2).Value)
                Try
                    obj.Eff_date = grow.Cells(colDate).Value
                Catch exx As Exception
                    obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                End Try

                Try
                    If obj.Eff_date.ToString().Substring(6, 4) = "0001" Then
                        obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    End If
                Catch exx As Exception
                    obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                End Try


                If clsCommon.myLen(obj.code) > 0 OrElse clsCommon.myLen(obj.QC_Param_Code) > 0 Then
                    arr.Add(obj)
                End If
            Next

            If clsParameterRangeMasterForQC.SaveData(arr, trans_id) Then
                clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)

                btnsave.Text = "Update"
                btndelete.Enabled = True

                LoadData(True)
            Else
                Reset()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            arr = Nothing
            obj = Nothing
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        If AllowToSave() Then SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        If clsCommon.myLen(gv.Rows(0).Cells(colCode).Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select records for deletion.", Me.Text)
            Return
        End If

        If myMessages.deleteConfirm() Then
            If clsParameterRangeMasterForQC.DeleteData(trans_id) Then
                myMessages.delete()
                Reset()
            End If
        End If
        
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub btnexport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexport.Click
        Try

            Dim qry As String = "select count(*) from tspl_parameter_range_master_qc where trans_id='" + trans_id + "'"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            Dim Whr As String = String.Empty
            '------------------------------------------------------------------------------------------------------------------------
            If check > 0 Then
                qry = "select tspl_parameter_range_master_qc.Code,tspl_parameter_master.Description,case when convert(varchar,tspl_parameter_range_master_QC.Effective_Date,103)='01/01/0001' then '' else tspl_parameter_range_master_QC.Effective_Date end  as Effective_Date ,tspl_parameter_range_master_qc.Lower_Range,tspl_parameter_range_master_qc.Upper_Range,tspl_parameter_range_master_qc.Status,tspl_parameter_range_master_qc.Value1,tspl_parameter_range_master_qc.Qc_status,tspl_parameter_range_master_qc.Deduction_Per,tspl_parameter_range_master_qc.Deduction_lower_range,tspl_parameter_range_master_qc.Deduction_upper_range,tspl_parameter_range_master_qc.Deduction_Ratio
                        ,tspl_parameter_range_master_qc.Deduction_lower_range2,tspl_parameter_range_master_qc.Deduction_upper_range2,tspl_parameter_range_master_qc.Deduction_Ratio2
                        ,tspl_parameter_range_master_qc.Deduction_lower_range3,tspl_parameter_range_master_qc.Deduction_upper_range3,tspl_parameter_range_master_qc.Deduction_Ratio3
                       from tspl_parameter_range_master_qc left outer join tspl_parameter_master on tspl_parameter_range_master_qc.code=tspl_parameter_master.code "

                If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                    qry = "select tspl_parameter_range_master_qc.QC_Param_Code as Code,TSPL_QC_LOG_SHEET_MASTER.Description,case when convert(varchar,tspl_parameter_range_master_QC.Effective_Date,103)='01/01/0001' then '' else tspl_parameter_range_master_QC.Effective_Date end  as Effective_Date ,tspl_parameter_range_master_qc.Lower_Range,tspl_parameter_range_master_qc.Upper_Range,tspl_parameter_range_master_qc.Status,tspl_parameter_range_master_qc.Value1,tspl_parameter_range_master_qc.Qc_status,tspl_parameter_range_master_qc.Deduction_Per,tspl_parameter_range_master_qc.Deduction_lower_range,tspl_parameter_range_master_qc.Deduction_upper_range,tspl_parameter_range_master_qc.Deduction_Ratio
                        ,tspl_parameter_range_master_qc.Deduction_lower_range2,tspl_parameter_range_master_qc.Deduction_upper_range2,tspl_parameter_range_master_qc.Deduction_Ratio2
                        ,tspl_parameter_range_master_qc.Deduction_lower_range3,tspl_parameter_range_master_qc.Deduction_upper_range3,tspl_parameter_range_master_qc.Deduction_Ratio3
                         from tspl_parameter_range_master_qc left outer join TSPL_QC_LOG_SHEET_MASTER on tspl_parameter_range_master_qc.QC_Param_Code=TSPL_QC_LOG_SHEET_MASTER.Code "
                    Whr += " and tspl_parameter_range_master_qc.QC_Param_Code <> '' "

                End If
            Else
                qry = "select '' as Code,'' as Description,'' as Effective_Date,'' as Lower_Range,'' as Upper_Range,'' as Status,'' as Value1 ,'' as Qc_status,0 as Deduction_Per,0 as Deduction_lower_range,0 as Deduction_upper_range,0 as Deduction_Ratio,0 as Deduction_lower_range2,0 as Deduction_upper_range2,0 as Deduction_Ratio2,0 as Deduction_lower_range3,0 as Deduction_upper_range3,0 as Deduction_Ratio3"
            End If
            '------------------------------------------------------------------------------------------------------------------------
            Whr += " and tspl_parameter_range_master_qc.trans_id='" + trans_id + "'"
            '------------------------------------------------------------------------------------------------------------------------
            If qry IsNot Nothing AndAlso clsCommon.myLen(qry) > 0 Then
                ListImpExpColumnsMandatory = New List(Of String)({"Code", "Qc_status"})
                ListImpExpColumnsSuperMandatory = New List(Of String)({"Code"})
                transportSql.ExporttoExcel(qry, Whr, "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
            End If


        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnimport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnimport.Click
        Dim arrParameterCode As New List(Of String)
        Dim gv1 As New RadGridView()
        Me.Controls.Add(gv1)
        Dim table_name As String = "tspl_parameter_master"
        If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
            table_name = "TSPL_QC_LOG_SHEET_MASTER"
        End If
        Dim obj As New clsParameterRangeMasterForQC

        
        Try
            If transportSql.importExcel(gv1, "Code", "Description", "Effective_Date", "Lower_Range", "Upper_Range", "Status", "Value1", "Qc_status", "Deduction_Per", "Deduction_lower_range", "Deduction_upper_range", "Deduction_Ratio", "Deduction_lower_range2", "Deduction_upper_range2", "Deduction_Ratio2", "Deduction_lower_range3", "Deduction_upper_range3", "Deduction_Ratio3") Then

                clsCommon.ProgressBarShow()
                Dim arr As New List(Of clsParameterRangeMasterForQC)
                Dim counter As Integer = 1

                For Each grow As GridViewRowInfo In gv1.Rows
                    obj = New clsParameterRangeMasterForQC
                    Dim qry As String = ""
                    obj.is_NewEntry = True
                    obj.code = clsCommon.myCstr(grow.Cells("Code").Value)
                    If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                        obj.code = Nothing
                        obj.QC_Param_Code = clsCommon.myCstr(grow.Cells("Code").Value)
                        If clsCommon.myLen(obj.QC_Param_Code) <= 0 Then
                            Throw New Exception("Please Fill Code/Description Of Parameter At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.QC_Param_Code) > 0 Then
                            qry = "select count(*) from " + table_name + " where Code='" + obj.QC_Param_Code + "'"
                            Dim check = clsDBFuncationality.getSingleValue(qry)

                            If check <= 0 Then
                                Throw New Exception("Parameter Code does not exist at line no. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                        If SetItemWiseQualityCheckInGeneralPurchase = True Then
                            If arrParameterCode.Contains(obj.QC_Param_Code) Then
                                Throw New Exception("Duplicate Parameter Code exist at line no. " + clsCommon.myCstr(counter) + "")
                            Else
                                arrParameterCode.Add(obj.QC_Param_Code)
                            End If

                        End If
                    Else
                        If clsCommon.myLen(obj.code) <= 0 Then
                            Throw New Exception("Please Fill Code/Description Of Parameter At Line No. " + clsCommon.myCstr(counter) + "")
                        End If
                        If clsCommon.myLen(obj.code) > 0 Then
                            qry = "select count(*) from " + table_name + " where code='" + obj.code + "'"
                            Dim check = clsDBFuncationality.getSingleValue(qry)

                            If check <= 0 Then
                                Throw New Exception("Parameter Code does not exist at line no. " + clsCommon.myCstr(counter) + "")
                            End If
                        End If
                    End If
                    obj.desc = clsCommon.myCstr(grow.Cells("Description").Value)


                    Try
                        obj.Eff_date = clsCommon.myCDate(grow.Cells("effective_date").Value, "dd/MMM/yyyy")
                    Catch exx As Exception
                        obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    End Try
                    Try
                        If obj.Eff_date.ToString().Substring(6, 4) = "0001" Then
                            obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                        End If
                    Catch exx As Exception
                        obj.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    End Try

                    obj.Lrange = clsCommon.myCdbl(grow.Cells("lower_range").Value)
                    obj.Urange = clsCommon.myCdbl(grow.Cells("upper_range").Value)
                    obj.status = clsCommon.myCstr(grow.Cells("status").Value)
                    obj.value1 = clsCommon.myCstr(grow.Cells("Value1").Value)
                    obj.value2 = Nothing ' clsCommon.myCstr(grow.Cells("Value2").Value)
                    obj.Qc_Status = clsCommon.myCstr(grow.Cells("Qc_status").Value)
                    obj.Trans_Id = clsCommon.myCstr(trans_id)

                    'If grow.Cells("Deduction_Per").Value Is Nothing OrElse Not IsNumeric(grow.Cells("Deduction_Per").Value) Then
                    '    Throw New Exception("Parameter Code does not exist at line no. " + clsCommon.myCstr(counter) + "")
                    'End If

                    obj.Deduction_Per = clsCommon.myCdbl(grow.Cells("Deduction_Per").Value)

                    obj.Deduction_lower_range = clsCommon.myCdbl(grow.Cells("Deduction_lower_range").Value)
                    obj.Deduction_upper_range = clsCommon.myCdbl(grow.Cells("Deduction_upper_range").Value)
                    obj.Deduction_Ratio = clsCommon.myCdbl(grow.Cells("Deduction_Ratio").Value)
                    obj.Deduction_lower_range2 = clsCommon.myCdbl(grow.Cells("Deduction_lower_range2").Value)
                    obj.Deduction_upper_range2 = clsCommon.myCdbl(grow.Cells("Deduction_upper_range2").Value)
                    obj.Deduction_Ratio2 = clsCommon.myCdbl(grow.Cells("Deduction_Ratio2").Value)
                    obj.Deduction_lower_range3 = clsCommon.myCdbl(grow.Cells("Deduction_lower_range3").Value)
                    obj.Deduction_upper_range3 = clsCommon.myCdbl(grow.Cells("Deduction_upper_range3").Value)
                    obj.Deduction_Ratio3 = clsCommon.myCdbl(grow.Cells("Deduction_Ratio3").Value)

                    qry = "select nature from " + table_name + " where code='" + obj.code + "'"
                    Dim nature As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If nature = "A" AndAlso clsCommon.myLen(obj.value1) <= 0 Then
                        obj.Lrange = 0
                        obj.Urange = 0
                        obj.status = ""
                        Throw New Exception("Please Fill Value1 Of Parameter Nature(Alphanumeric) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If nature = "R" AndAlso (clsCommon.myCdbl(obj.Lrange) <= 0 Or clsCommon.myCdbl(obj.Urange) <= 0) Then
                        obj.value2 = ""
                        obj.value1 = ""
                        obj.status = ""
                        Throw New Exception("Please Fill Lower/Upper Range Of Parameter Nature(Range) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If
                    If nature = "B" AndAlso (clsCommon.myLen(obj.status) <= 0) Then
                        obj.Lrange = 0
                        obj.Urange = 0
                        obj.value1 = ""
                        obj.value2 = ""
                        Throw New Exception("Please Fill Status Of Parameter Nature(Boolean) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If nature = "A" Then
                        obj.Lrange = 0
                        obj.Urange = 0
                        obj.status = ""
                    End If
                    If nature = "R" Then
                        obj.value2 = ""
                        obj.value1 = ""
                        obj.status = ""
                    End If
                    If nature = "B" Then
                        obj.Lrange = 0
                        obj.Urange = 0
                        obj.value1 = ""
                        obj.value2 = ""
                    End If

                    If clsCommon.myLen(obj.Qc_Status) <= 0 Then
                        Throw New Exception("Please Fill Qc Status (Ok/Not Ok) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If clsCommon.CompairString(obj.Qc_Status, "Ok") = CompairStringResult.Equal Or clsCommon.CompairString(obj.Qc_Status, "Not Ok") = CompairStringResult.Equal Then
                    Else

                        Throw New Exception("Please Fill Qc Status (Ok/Not Ok) At Line No. " + clsCommon.myCstr(counter) + "")
                    End If

                    If obj.Lrange > obj.Urange Then
                        Throw New Exception("Lower Range Value Should Not be Greater than Upper Range Value at row no. " + clsCommon.myCstr(counter) + "")
                    End If

                    If SetItemWiseQualityCheckInGeneralPurchase = True Then
                    Else
                        Dim strsQry As String = "Select lower_range,upper_range,Qc_Status,QC_Param_Code from tspl_parameter_range_master_QC where upper_range='" + clsCommon.myCstr(obj.Lrange) + "' and Qc_Status='" + obj.Qc_Status + "' and QC_Param_Code ='" + obj.QC_Param_Code + "'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(strsQry)
                        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            If clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("upper_range")), obj.Lrange) = CompairStringResult.Equal Then
                                Throw New Exception("Lower Range value is Already as upper Range value in database of row no. " + clsCommon.myCstr(counter) + "")
                            End If
                            If (clsCommon.myCdbl(dt.Rows(0)("lower_range")) >= obj.Lrange) AndAlso (clsCommon.myCdbl(dt.Rows(0)("upper_range")) <= obj.Lrange) Then
                                Throw New Exception("Lower Range value is Already Contain in database of row no. " + clsCommon.myCstr(counter) + "")
                            End If

                        End If
                        Dim Chk As Integer = 0
                        Chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(1) from tspl_parameter_range_master_QC where (lower_range>='" + clsCommon.myCstr(obj.Lrange) + "' and upper_range<='" + clsCommon.myCstr(obj.Urange) + "') and Qc_Status='" + obj.Qc_Status + "' and QC_Param_Code ='" + obj.QC_Param_Code + "'", Nothing))
                        If Chk > 0 Then
                            Throw New Exception("Parameter Range is Already in database of row no. " + clsCommon.myCstr(counter) + "")
                        End If
                    End If

                    'Deduction Range
                    If obj.Deduction_lower_range > obj.Deduction_upper_range Then
                        Throw New Exception("Deduction Lower Range1 Value Should Not be Greater than Upper Range1 Value at row no. " + clsCommon.myCstr(counter) + "")
                    End If
                    If obj.Deduction_lower_range2 > obj.Deduction_upper_range2 Then
                        Throw New Exception("Deduction Lower Range2 Value Should Not be Greater than Upper Range2 Value at row no. " + clsCommon.myCstr(counter) + "")
                    End If
                    If obj.Deduction_lower_range3 > obj.Deduction_upper_range3 Then
                        Throw New Exception("Deduction Lower Range3 Value Should Not be Greater than Upper Range3 Value at row no. " + clsCommon.myCstr(counter) + "")
                    End If

                    'strsQry = "Select Deduction_lower_range,Deduction_upper_range,Qc_Status,QC_Param_Code from tspl_parameter_range_master_QC where Deduction_upper_range='" + clsCommon.myCstr(obj.Deduction_lower_range) + "' and Qc_Status='" + obj.Qc_Status + "' and QC_Param_Code ='" + obj.QC_Param_Code + "'"
                    'dt = clsDBFuncationality.GetDataTable(strsQry)
                    'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    '    If clsCommon.CompairString(clsCommon.myCdbl(dt.Rows(0)("upper_range")), obj.Lrange) = CompairStringResult.Equal Then
                    '        Throw New Exception("Deduction Lower Range value is Already as upper Range value in database of row no. " + clsCommon.myCstr(counter) + "")
                    '    End If
                    '    If (clsCommon.myCdbl(dt.Rows(0)("Deduction_lower_range")) >= obj.Deduction_lower_range) AndAlso (clsCommon.myCdbl(dt.Rows(0)("Deduction_upper_range")) <= obj.Deduction_lower_range) Then
                    '        Throw New Exception("Deduction Lower Range value is Already Contain in database of row no. " + clsCommon.myCstr(counter) + "")
                    '    End If

                    'End If
                    'Chk = 0
                    'Chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("Select count(1) from tspl_parameter_range_master_QC where (Deduction_lower_range>='" + clsCommon.myCstr(obj.Deduction_lower_range) + "' and Deduction_upper_range<='" + clsCommon.myCstr(obj.Deduction_upper_range) + "') and Qc_Status='" + obj.Qc_Status + "' and QC_Param_Code ='" + obj.QC_Param_Code + "'", Nothing))
                    'If Chk > 0 Then
                    '    Throw New Exception("Deduction Parameter Range is Already in database of row no. " + clsCommon.myCstr(counter) + "")
                    'End If
                    'Deduction Range

                    If clsCommon.myLen(obj.code) > 0 Then
                        arr.Add(obj)
                    End If
                    If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                        If clsCommon.myLen(obj.QC_Param_Code) > 0 Then
                            arr.Add(obj)
                        End If
                    End If
                    counter += 1
                Next

                clsCommon.ProgressBarHide()

                If clsParameterRangeMasterForQC.SaveData(arr, trans_id) Then
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Successfully", Me.Text)
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Data Transfer", Me.Text)
                End If
                'Reset()
                LoadData(True)
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            obj = Nothing
            table_name = Nothing
            Me.Controls.Remove(gv1)
        End Try

    End Sub

    Private Sub gv_CellClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellClick
        If gv.CurrentColumn Is gv.Columns(colValue1) AndAlso clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value)) > 0 Then
            gv.CurrentCell.Value = clsCommon.myCstr(OpenParameterValueList(clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value), clsCommon.myCstr(gv.CurrentCell.Value)))
        End If
    End Sub

    Private Sub gv_CellEditorInitialized(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellEditorInitialized
        'Dim editor As RadDropDownListEditor = TryCast(e.ActiveEditor, RadDropDownListEditor)
        'If editor IsNot Nothing Then
        '    Dim element As RadDropDownListEditorElement = DirectCast(editor.EditorElement, RadDropDownListEditorElement)
        '    element.DataSource = OpenParameterValueList(gv.CurrentRow.Cells(colCode).Value)
        '    'element.DisplayMember = "Value"
        '    'element.ValueMember = "Value"
        '    'element.SelectedIndex = element.FindString(e.Value.ToString())
        'End If

    End Sub

    Private Sub gv_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv.CellFormatting
        Try
            If LoadReadOnly Then
                Exit Sub
            End If
            If e.Column Is gv.Columns(colCode) Then
                gv.CurrentRow.Cells(colLower).ReadOnly = True
                gv.CurrentRow.Cells(colUpper).ReadOnly = True
                gv.CurrentRow.Cells(colStatus).ReadOnly = True
                gv.CurrentRow.Cells(colValue1).ReadOnly = True
                gv.CurrentRow.Cells(colQcStatus).ReadOnly = True

                If clsCommon.myLen(gv.CurrentRow.Cells(colCode).Value) > 0 Then
                    Dim code As String = clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value)
                    'Dim qry As String = ""
                    'qry = "select nature from tspl_parameter_master where code='" + code + "'"
                    'If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                    '    qry = "select nature from TSPL_QC_LOG_SHEET_MASTER where code='" + code + "'"
                    'End If
                    Dim nature As String = clsCommon.myCstr(gv.CurrentRow.Cells(colNature).Value) 'clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

                    If clsCommon.CompairString(nature, "A") = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colValue1).ReadOnly = False
                        gv.CurrentRow.Cells(colQcStatus).ReadOnly = False
                        gv.CurrentRow.Cells(colLower).Value = Nothing
                        gv.CurrentRow.Cells(colUpper).Value = Nothing
                        gv.CurrentRow.Cells(colStatus).Value = Nothing
                    End If

                    If clsCommon.CompairString(nature, "R") = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colLower).ReadOnly = False
                        gv.CurrentRow.Cells(colUpper).ReadOnly = False
                        gv.CurrentRow.Cells(colQcStatus).ReadOnly = False
                        gv.CurrentRow.Cells(colValue1).Value = Nothing
                        gv.CurrentRow.Cells(colStatus).Value = Nothing
                    End If

                    If clsCommon.CompairString(nature, "B") = CompairStringResult.Equal Then
                        gv.CurrentRow.Cells(colStatus).ReadOnly = False
                        gv.CurrentRow.Cells(colQcStatus).ReadOnly = False
                        gv.CurrentRow.Cells(colLower).Value = Nothing
                        gv.CurrentRow.Cells(colUpper).Value = Nothing
                        gv.CurrentRow.Cells(colValue1).Value = Nothing
                    End If
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gv_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If Not isLoadData Then '------when on loaddata then it should not run
                If isValueChanged Then
                    If gv.CurrentRow.Index >= 0 Then
                        If gv.CurrentColumn Is gv.Columns(colCode) Then
                            isValueChanged = False
                            OpenParameter(False)
                            isValueChanged = True
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            isValueChanged = True
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
        
    End Sub

    Sub OpenParameter(ByVal isButtonClicked As Boolean)
        Dim code As String = ""
        If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
            code = clsPPLogSheetMaster.GetFinder(" trans_id='" + trans_id + "' ", "Code", isButtonClicked)
        Else
            Dim qry As String = "select Code,Description,Type,Nature from tspl_parameter_master"
            code = clsCommon.myCstr(clsCommon.ShowSelectForm("PMRFND", qry, "Code", "", clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value), "Code", isButtonClicked))
        End If


        If code IsNot Nothing AndAlso clsCommon.myLen(code) > 0 Then
            gv.CurrentRow.Cells(colCode).Value = code
            gv.CurrentRow.Cells(colNature).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from tspl_parameter_master where code='" + code + "'"))
            gv.CurrentRow.Cells(colDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from tspl_parameter_master where code='" + code + "'"))

            If clsCommon.CompairString(clsUserMgtCode.frmQualityModuleParameterRangeMaster, FORMTYPE) = CompairStringResult.Equal Then
                gv.CurrentRow.Cells(colNature).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select nature from TSPL_QC_LOG_SHEET_MASTER where code='" + code + "'"))
                gv.CurrentRow.Cells(colDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_QC_LOG_SHEET_MASTER where code='" + code + "'"))
            End If
        Else
            gv.CurrentRow.Cells(colCode).Value = ""
            gv.CurrentRow.Cells(colDesc).Value = ""
            gv.CurrentRow.Cells(colLower).Value = Nothing
            gv.CurrentRow.Cells(colUpper).Value = Nothing
            gv.CurrentRow.Cells(colStatus).Value = Nothing
            gv.CurrentRow.Cells(colValue1).Value = Nothing
            'gv.CurrentRow.Cells(colValue2).Value = Nothing
        End If
    End Sub

    Private Sub gv_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv.CurrentColumnChanged
        If gv.RowCount > 0 Then
            Dim intCurrRow As Integer = gv.CurrentRow.Index
            If intCurrRow = gv.Rows.Count - 1 Then
                gv.Rows.AddNew()
                gv.CurrentRow = gv.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow(Me, "Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
            Exit Sub
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        'gv.ReadOnly = False
        'gv.Rows.AddNew()
        LoadData(False)
    End Sub

    Private Sub gv_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv.CellDoubleClick
        Try

            If gv.Rows.Count > 0 Then

                Dim frm As New FrmChildParameterRangeMasterForQC1()
                frm.QC_Param_Code = clsCommon.myCstr(gv.CurrentRow.Cells(colCode).Value)
                frm.desc = clsCommon.myCstr(gv.CurrentRow.Cells(colDesc).Value)
                frm.Lrange = clsCommon.myCdbl(gv.CurrentRow.Cells("lower_range").Value)
                frm.Urange = clsCommon.myCdbl(gv.CurrentRow.Cells("upper_range").Value)
                frm.Lrange_Prev = clsCommon.myCdbl(gv.CurrentRow.Cells("lower_range").Value)
                frm.Urange_Prev = clsCommon.myCdbl(gv.CurrentRow.Cells("upper_range").Value)
                frm.Deduction_Per = clsCommon.myCdbl(gv.CurrentRow.Cells("Deduction_Per").Value)
                'frm.Deduction_lower_range = clsCommon.myCdbl(gv.CurrentRow.Cells("Deduction_lower_range").Value)
                'frm.Deduction_upper_range = clsCommon.myCdbl(gv.CurrentRow.Cells("Deduction_upper_range").Value)
                'frm.Deduction_Ratio = clsCommon.myCdbl(gv.CurrentRow.Cells("Deduction_Ratio").Value)
                frm.Status1 = clsCommon.myCstr(gv.CurrentRow.Cells("Status").Value)
                frm.Qc_Status = clsCommon.myCstr(gv.CurrentRow.Cells("qc_status").Value)
                frm.Qc_Status_prev = clsCommon.myCstr(gv.CurrentRow.Cells("qc_status").Value)
                frm.value1 = clsCommon.myCstr(gv.CurrentRow.Cells("Value1").Value)
                frm.description = clsCommon.myCstr(gv.CurrentRow.Cells("descrip").Value)
                If clsCommon.myLen(clsCommon.myCstr(gv.CurrentRow.Cells("effective_date").Value)) > 0 Then
                        frm.Eff_date = clsCommon.myCstr(gv.CurrentRow.Cells("effective_date").Value)
                    Else
                        frm.Eff_date = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy")
                    End If

                    frm.Trans_Id = trans_id
                    'frm.FORMTYPE = "QM-P-RNG"
                    frm.FORMTYPE = FORMTYPE
                    frm.WindowState = FormWindowState.Normal
                    frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
                    frm.ShowDialog()
                    LoadData(True)
                End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message(), Me.Text)
        End Try
    End Sub


    Private Sub btnUpdateDeduction_Click(sender As Object, e As EventArgs) Handles btnUpdateDeduction.Click
        If clsCommon.myLen(fndParameterCode.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Select Parameter first.", Me.Text)
            fndParameterCode.Focus()
            Exit Sub
        End If
        Dim isSaved As Boolean = True
        Dim trans As SqlTransaction = Nothing
        Try
            trans = clsDBFuncationality.GetTransactin()
            'Deduction Range
            If clsCommon.myCDecimal(txtDeductionLRange.Text) > clsCommon.myCDecimal(txtDeductionURange.Text) Then
                Throw New Exception("Deduction Lower Range Value Should Not be Greater than Upper Range Value")
            End If
            'Dim whr As String = " QC_Param_Code='" + fndParameterCode.Value + "' and Lower_range='" + clsCommon.myCstr(txtLowerRange.Value) + "'
            '                    and Upper_Range='" + clsCommon.myCstr(txtUpperRange.Value) + "'
            '                    and Qc_Status='" + txtQcStatus.Text + "'"
            Dim qry As String = "select Lower_range,Upper_range from TSPL_PARAMETER_RANGE_MASTER_QC where QC_Param_Code='" + fndParameterCode.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry, trans)
            Dim whr As String = ""
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                'whr = " QC_Param_Code='" + fndParameterCode.Value + "' and Lower_range='" + clsCommon.myCstr(dt.Rows(0).Item("Lower_range")) + "'
                '                and Upper_Range='" + clsCommon.myCstr(dt.Rows(0).Item("Upper_range")) + "'
                '                and Qc_Status='" + txtQcStatus.Text + "'"
                whr = " QC_Param_Code='" + fndParameterCode.Value + "' "
            Else
                Throw New Exception("Insert parameter first.")
            End If


            Dim coll As New Hashtable()
            clsCommon.AddColumnsForChange(coll, "Lower_range", txtLowerRange.Text)
            clsCommon.AddColumnsForChange(coll, "Upper_Range", txtUpperRange.Text)
            clsCommon.AddColumnsForChange(coll, "Deduction_lower_range", txtDeductionLRange.Text)
            clsCommon.AddColumnsForChange(coll, "Deduction_upper_range", txtDeductionURange.Text)
            clsCommon.AddColumnsForChange(coll, "Deduction_Method", IIf(rbtnDedMethodRatio.IsChecked, 0, 1))
            clsCommon.AddColumnsForChange(coll, "Deduction_Ratio", txtDeductionRatio.Text)
            clsCommon.AddColumnsForChange(coll, "Deduction_lower_range2", txtDeductionLRange2.Text)
            clsCommon.AddColumnsForChange(coll, "Deduction_upper_range2", txtDeductionURange2.Text)
            clsCommon.AddColumnsForChange(coll, "Deduction_Ratio2", txtDeductionRatio2.Text)
            clsCommon.AddColumnsForChange(coll, "Deduction_lower_range3", txtDeductionLRange3.Text)
            clsCommon.AddColumnsForChange(coll, "Deduction_upper_range3", txtDeductionURange3.Text)
            clsCommon.AddColumnsForChange(coll, "Deduction_Ratio3", txtDeductionRatio3.Text)
            clsCommon.AddColumnsForChange(coll, "Description", txtDescription.Text)
            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_PARAMETER_RANGE_MASTER_QC", OMInsertOrUpdate.Update, whr, trans)

            'Mapping
            qry = "delete from TSPL_PARAMETER_MAPPING_QC where QC_Param_Code='" + fndParameterCode.Value + "'"
            isSaved = isSaved AndAlso clsDBFuncationality.ExecuteNonQuery(qry, trans)

            If txtParameterMapping.arrValueMember IsNot Nothing AndAlso txtParameterMapping.arrValueMember.Count > 0 Then
                For i As Integer = 0 To txtParameterMapping.arrValueMember.Count - 1
                    Dim col As New Hashtable()
                    clsCommon.AddColumnsForChange(col, "QC_Param_Code", fndParameterCode.Value)
                    clsCommon.AddColumnsForChange(col, "Mapped_QC_Param_Code", txtParameterMapping.arrValueMember.Item(i))
                    clsCommonFunctionality.UpdateDataTable(col, "TSPL_PARAMETER_MAPPING_QC", OMInsertOrUpdate.Insert, "", trans)
                Next
            End If
            trans.Commit()
            clsCommon.MyMessageBoxShow(Me, "Update Successfully", Me.Text)
            LoadData(True)
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndParameterCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles fndParameterCode._MYValidating
        Try
            Dim qrry As String = "select TSPL_QC_LOG_SHEET_MASTER.Code,TSPL_QC_LOG_SHEET_MASTER.Description,TSPL_QC_LOG_SHEET_MASTER.Type,TSPL_QC_LOG_SHEET_MASTER.Nature
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Qc_Status
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Lower_Range,TSPL_PARAMETER_RANGE_MASTER_QC.Upper_Range
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Ratio
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range2
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range2,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Ratio2
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_lower_range3
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_upper_range3,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Ratio3
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Deduction_Method
            ,TSPL_PARAMETER_RANGE_MASTER_QC.Description as Descp
             from TSPL_QC_LOG_SHEET_MASTER
            left join TSPL_PARAMETER_RANGE_MASTER_QC on TSPL_PARAMETER_RANGE_MASTER_QC.QC_Param_Code=TSPL_QC_LOG_SHEET_MASTER.code
            where 1=1"
            Dim rows As DataRow = clsCommon.ShowSelectFormForRow("PMRFNDQC", qrry)

            If Not rows Is Nothing Then
                fndParameterCode.Value = clsCommon.myCstr(rows("Code"))
                lblParamDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select description from TSPL_QC_LOG_SHEET_MASTER where code='" + fndParameterCode.Value + "'"))
                txtDeductionLRange.Text = clsCommon.myCDecimal(rows("Deduction_lower_range"))
                txtDeductionURange.Text = clsCommon.myCDecimal(rows("Deduction_upper_range"))
                txtDeductionRatio.Text = clsCommon.myCDecimal(rows("Deduction_Ratio"))
                txtDeductionLRange2.Text = clsCommon.myCDecimal(rows("Deduction_lower_range2"))
                txtDeductionURange2.Text = clsCommon.myCDecimal(rows("Deduction_upper_range2"))
                txtDeductionRatio2.Text = clsCommon.myCDecimal(rows("Deduction_Ratio2"))
                txtDeductionLRange3.Text = clsCommon.myCDecimal(rows("Deduction_lower_range3"))
                txtDeductionURange3.Text = clsCommon.myCDecimal(rows("Deduction_upper_range3"))
                txtDeductionRatio3.Text = clsCommon.myCDecimal(rows("Deduction_Ratio3"))
                txtLowerRange.Text = clsCommon.myCDecimal(rows("Lower_Range"))
                txtUpperRange.Text = clsCommon.myCDecimal(rows("Upper_Range"))
                txtQcStatus.Text = clsCommon.myCstr(rows("Qc_Status"))
                txtDescription.Text = clsCommon.myCstr(rows("Descp"))
                rbtnDedMethodRatio.IsChecked = (clsCommon.myCDecimal(rows("Deduction_Method")) = 0)
                rbtnDedMethodFixed.IsChecked = (clsCommon.myCDecimal(rows("Deduction_Method")) = 1)

                Dim dtTrans As DataTable = clsDBFuncationality.GetDataTable("SELECT Mapped_QC_Param_Code FROM TSPL_PARAMETER_MAPPING_QC where QC_Param_Code='" + fndParameterCode.Value + "'")
                Dim arrTrans As New ArrayList
                For Each dr As DataRow In dtTrans.Rows
                    arrTrans.Add(clsCommon.myCstr(dr.Item("Mapped_QC_Param_Code")))
                Next
                If arrTrans IsNot Nothing AndAlso arrTrans.Count > 0 Then
                    txtParameterMapping.arrValueMember = arrTrans
                Else
                    txtParameterMapping.arrValueMember = Nothing
                End If
            Else
                txtParameterMapping.arrValueMember = Nothing
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub txtParameterMapping__My_Click(sender As Object, e As EventArgs) Handles txtParameterMapping._My_Click
        Try
            If clsCommon.myLen(fndParameterCode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow(Me, "Select Parameter first.", Me.Text)
                fndParameterCode.Focus()
                Exit Sub
            End If
            Dim qry As String = "select TSPL_QC_LOG_SHEET_MASTER.Code,TSPL_QC_LOG_SHEET_MASTER.Description,TSPL_QC_LOG_SHEET_MASTER.Type,TSPL_QC_LOG_SHEET_MASTER.Nature
             from TSPL_QC_LOG_SHEET_MASTER where TSPL_QC_LOG_SHEET_MASTER.Code <>'" + fndParameterCode.Value + "'"
            txtParameterMapping.arrValueMember = clsCommon.ShowMultipleSelectForm("@ParamMap", qry, "Code", "Description", txtParameterMapping.arrValueMember, txtParameterMapping.arrDispalyMember)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class
