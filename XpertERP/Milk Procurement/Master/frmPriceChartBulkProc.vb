'-----------------Created By Panakj Jha on 31/07/2014 Against Ticket no:  BM00000003275
'--------------For Bulk Procurement Price Chart Master
Imports common
Imports System.Data.SqlClient

Public Class frmPriceChartBulkProc
    Inherits FrmMainTranScreen
#Region "Variables"
    Dim IsItemMilkType As Integer = 0
    Dim IsPriceChartGradeWise As Integer = 0
    Dim AllowCreateBulkProcPriceChartItemWise As Integer = 0
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Dim userCode, companyCode As String
    Public isCellValueChangedOpen = False
    Const colSLNo As String = "colSLNo"
    Const colMilkGradeCode As String = "colMilkGradeCode"
    Const colMilkGradeType As String = "colMilkGradeType"
    Const colFatWeigtage As String = "colFatWeigtage"
    Const colSNFWeigtage As String = "colSNFWeigtage"
    Const colFatPercentage As String = "colFatPercentage"
    Const colSNFPercentage As String = "colSNFPercentage"
    Const colFillDeduction As String = "colFillDeduction"
    Const colStandardRate As String = "colStandardRate"
    Const colTolerance As String = "colTolerance"
    Const colRemarks As String = "colRemarks"
    Const colTotalSolidRate As String = "colTotalSolidRate"
    Const colTotalSolidUOM As String = "colTotalSolidUOM"
    Const colRowType As String = "colRowType"
    Dim BulkPricePostedData As Boolean
    Dim AddDaysFOrExpiryDate As Integer
    Dim StandardRateWithZero As Boolean = False
    Dim isItemWise As Integer = 0
    Dim SettBulkProcurementApplyTotalSoidRate As Boolean = False
    Dim SettApplyCalculateWeightInLtr As Boolean = False
    Dim ApplyTolerance As Boolean = False
    Dim ApplyBothtsrateAndFatRateinBulkProcurement As Boolean = False
    Dim repoRowType As GridViewComboBoxColumn = New GridViewComboBoxColumn()
    Private IsInsideLoadData As Boolean = False

#End Region

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmPriceChartBulkProc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddDaysFOrExpiryDate = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ExpiryDaysBulkProcurementPriceChart, clsFixedParameterCode.ExpiryDaysBulkProcurementPriceChart, Nothing))
        IsItemMilkType = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isItemMilkType, clsFixedParameterCode.isItemMilkType, Nothing))
        IsPriceChartGradeWise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isPriceChartGradeWise, clsFixedParameterCode.isPriceChartGradeWise, Nothing))
        AllowCreateBulkProcPriceChartItemWise = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.isCreateBulkProcPriceChartItemWise, clsFixedParameterCode.isCreateBulkProcPriceChartItemWise, Nothing))
        StandardRateWithZero = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcPriceChartStandardRateWithZero, clsFixedParameterCode.BulkProcPriceChartStandardRateWithZero, Nothing)) = 1, True, False)
        SettBulkProcurementApplyTotalSoidRate = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.BulkProcurementApplyTotalSoidRate, clsFixedParameterCode.BulkProcurementApplyTotalSoidRate, Nothing)) > 0)
        SettApplyCalculateWeightInLtr = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyCalculateWeightInLtr, clsFixedParameterCode.ApplyCalculateWeightInLtr, Nothing)) > 0)
        ApplyBothtsrateAndFatRateinBulkProcurement = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyBothtsrateAndFatRateinBulkProcurement, clsFixedParameterCode.ApplyBothtsrateAndFatRateinBulkProcurement, Nothing)) = 1, True, False)
        ApplyTolerance = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ApplyTolerance, clsFixedParameterCode.ApplyTolerance, Nothing)) = 1, True, False)
        SetUserMgmtNew()
        Reset()
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        If IsItemMilkType = 1 Then
            Panel1.Visible = True
            If chkPriceGradeWise.Checked Then
                SplitContainer2.SplitterDistance = 50
                RadGroupBox1.Visible = True
                btnImportDetail.Visibility = ElementVisibility.Visible
                btnExportDetail.Visibility = ElementVisibility.Visible
            Else
                btnImportDetail.Visibility = ElementVisibility.Collapsed
                btnExportDetail.Visibility = ElementVisibility.Collapsed
            End If
        End If
        BulkPricePostedData = IIf(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowBulkProcItemPostedData, clsFixedParameterCode.AllowBulkProcItemPostedData, Nothing)) = 1, True, False)
        btnPost.Visible = MyBase.isPostFlag AndAlso BulkPricePostedData
        ItemWiseGradWiseEnableDisable()
        If IsPriceChartGradeWise = 1 OrElse AllowCreateBulkProcPriceChartItemWise = 1 Then
            If IsItemMilkType = 0 Then
                clsCommon.MyMessageBoxShow(" if [Create Bulk Procurement price chart-Itemwise] Or [Is Price Chart Grade Wise]  setting ON then [Is Item Milk Type] setting should be ON.")
                Me.Close()
            End If
        End If

        pnlTotalSolid.Visible = SettBulkProcurementApplyTotalSoidRate
        pnlUOM.Visible = SettBulkProcurementApplyTotalSoidRate OrElse SettApplyCalculateWeightInLtr
        If SettBulkProcurementApplyTotalSoidRate Then
            RadGroupBox1.Visible = False
            SplitContainer3.Panel2Collapsed = True
            chkPriceGradeWise.Visible = False
        End If
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmPriceChartBulkProc)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag

        If btnsave.Visible = True Then
            RmExport.Enabled = True
            RmImport.Enabled = True
        Else
            RmExport.Enabled = False
            RmImport.Enabled = False
        End If
        btndelete.Visible = MyBase.isDeleteFlag

    End Sub

    Sub Reset()
        LoadBlankGrid()
        'If AllowCreateBulkProcPriceChartItemWise = 1 Then
        '    chkPriceGradeWise.Checked = True
        'Else
        '    chkPriceGradeWise.Checked = False
        'End If

        fndcode.Value = ""
        txtMilktypeCode.Value = ""
        lblMilkType.Text = ""
        dtpEffectiveDate.Value = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(), "dd/MM/yyyy hh:mm:ss tt")
        dtpExpiryDate.Value = DateAdd(DateInterval.Day, AddDaysFOrExpiryDate, dtpEffectiveDate.Value)
        txtfatPercentage.Value = 0
        TxtFatWeightage.Value = 0
        txtsnfPercentage.Value = 0
        TxtSNFWeightage.Value = 0
        txtStanadardrate.Value = 0
        txtTolerance.Value = 0
        fndVendor.Value = ""
        lblVendorName.Text = ""
        txtPricedate.Text = clsCommon.GETSERVERDATE()
        fndcode.MyReadOnly = False
        btnsave.Text = "&Save"
        btnsave.Enabled = True
        btndelete.Enabled = False
        chkDefaultForTankerDispatch.Checked = False
        gvPriceChart.Rows.AddNew()
        isItemWise = 0
        txtTotalSolidRate.Value = 0
        txtUOM.Value = ""
    End Sub

    Private Sub frmPriceChartBulkProc_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Sub LoadBlankGrid()
        gvPriceChart.Rows.Clear()
        gvPriceChart.Columns.Clear()

        Dim repoSL As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSL.Name = colSLNo
        repoSL.Width = 60
        repoSL.HeaderText = "Line No."
        repoSL.ReadOnly = True
        repoSL.IsVisible = False
        gvPriceChart.MasterTemplate.Columns.Add(repoSL)

        Dim repocode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repocode.Name = colMilkGradeCode
        repocode.Width = 150
        If AllowCreateBulkProcPriceChartItemWise = 1 Then
            repocode.HeaderText = "Item Code"
        Else
            repocode.HeaderText = "Milk Grade Code"
        End If

        repocode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repocode.TextImageRelation = TextImageRelation.TextBeforeImage
        gvPriceChart.MasterTemplate.Columns.Add(repocode)


        repocode = New GridViewTextBoxColumn()
        repocode.Name = colMilkGradeType
        repocode.Width = 100
        If AllowCreateBulkProcPriceChartItemWise = 1 Then
            repocode.HeaderText = "Item Desc"
        Else
            repocode.HeaderText = "Grade Type"
        End If

        repocode.IsVisible = True
        gvPriceChart.MasterTemplate.Columns.Add(repocode)

        If ApplyBothtsrateAndFatRateinBulkProcurement = True Then
            repoRowType.FormatString = ""
            repoRowType.HeaderText = "Type"
            repoRowType.Name = colRowType
            repoRowType.Width = 80
            repoRowType.ReadOnly = False
            repoRowType.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft

            gvPriceChart.MasterTemplate.Columns.Add(repoRowType)

            repoRowType.DataSource = GetRowType()
            repoRowType.ValueMember = "Code"
            repoRowType.DisplayMember = "Code"
        End If

        Dim repoFatWeightage As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatWeightage.Name = colFatWeigtage
        repoFatWeightage.Width = 100
        repoFatWeightage.FormatString = "{0:n2}"
        repoFatWeightage.HeaderText = "Fat Weightage"
        repoFatWeightage.DecimalPlaces = 2
        repoFatWeightage.ReadOnly = False
        gvPriceChart.MasterTemplate.Columns.Add(repoFatWeightage)

        Dim repoSNFWeightage As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFWeightage.Name = colSNFWeigtage
        repoSNFWeightage.Width = 100
        repoSNFWeightage.HeaderText = "SNF Weightage"
        repoSNFWeightage.FormatString = "{0:n2}"
        repoSNFWeightage.ReadOnly = False
        repoSNFWeightage.DecimalPlaces = 2
        gvPriceChart.MasterTemplate.Columns.Add(repoSNFWeightage)

        Dim repoFatpercent As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoFatpercent.Name = colFatPercentage
        repoFatpercent.Width = 100
        repoFatpercent.HeaderText = "Fat Percentage"
        repoFatpercent.FormatString = "{0:n2}"
        repoFatpercent.ReadOnly = False
        repoFatpercent.DecimalPlaces = 2
        gvPriceChart.MasterTemplate.Columns.Add(repoFatpercent)

        Dim repoSNFpercent As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoSNFpercent.Name = colSNFPercentage
        repoSNFpercent.Width = 100
        repoSNFpercent.HeaderText = "SNF Percentage"
        repoSNFpercent.FormatString = "{0:n2}"
        repoSNFpercent.ReadOnly = False
        repoSNFpercent.DecimalPlaces = 2
        gvPriceChart.MasterTemplate.Columns.Add(repoSNFpercent)

        Dim ShowBtn As New GridViewCommandColumn()
        ShowBtn.FormatString = ""
        ShowBtn.UseDefaultText = True
        ShowBtn.DefaultText = "SNF Deduction"
        ShowBtn.HeaderText = ""
        ShowBtn.Name = colFillDeduction
        ShowBtn.FieldName = colFillDeduction
        ShowBtn.Width = 100
        ShowBtn.IsVisible = (IsPriceChartGradeWise = 0)
        ShowBtn.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPriceChart.MasterTemplate.Columns.Add(ShowBtn)

        Dim repoStandardRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoStandardRate.Name = colStandardRate
        repoStandardRate.Width = 100
        repoStandardRate.HeaderText = "Standard Rate"
        repoStandardRate.FormatString = ""
        repoStandardRate.ReadOnly = False
        repoStandardRate.DecimalPlaces = 2
        gvPriceChart.MasterTemplate.Columns.Add(repoStandardRate)

        Dim repoTolerance As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTolerance.Name = colTolerance
        repoTolerance.Width = 100
        repoTolerance.HeaderText = "Tolerance"
        repoTolerance.FormatString = "{0:n2}"
        repoTolerance.ReadOnly = False
        repoTolerance.DecimalPlaces = 2
        gvPriceChart.MasterTemplate.Columns.Add(repoTolerance)
        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 100
        repoRemarks.HeaderText = "Remark"
        repoRemarks.FormatString = "{0:n2}"
        repoRemarks.ReadOnly = False
        repoRemarks.IsVisible = False
        gvPriceChart.MasterTemplate.Columns.Add(repoRemarks)

        If ApplyBothtsrateAndFatRateinBulkProcurement = True Then
            Dim repoTSRate As GridViewDecimalColumn = New GridViewDecimalColumn()
            repoTSRate.Name = colTotalSolidRate
            repoTSRate.Width = 100
            repoTSRate.HeaderText = "Total Solid Rate"
            repoTSRate.FormatString = "{0:n2}"
            repoTSRate.ReadOnly = False
            repoTSRate.DecimalPlaces = 2
            gvPriceChart.MasterTemplate.Columns.Add(repoTSRate)

            Dim repoTSUOM As GridViewTextBoxColumn = New GridViewTextBoxColumn()
            repoTSUOM.Name = colTotalSolidUOM
            repoTSUOM.Width = 60
            repoTSUOM.HeaderText = "TS UOM"
            repoTSUOM.ReadOnly = False
            repoTSUOM.IsVisible = True
            gvPriceChart.MasterTemplate.Columns.Add(repoTSUOM)
        End If

        gvPriceChart.AllowDeleteRow = True
        gvPriceChart.AllowAddNewRow = False
        gvPriceChart.ShowGroupPanel = False
        gvPriceChart.AllowColumnReorder = True
        gvPriceChart.AllowRowReorder = True
        gvPriceChart.EnableSorting = True
        gvPriceChart.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvPriceChart.MasterTemplate.ShowRowHeaderColumn = False

        'gvPriceChart.EnableFiltering = True

    End Sub

    Private Sub gvFATDed_CommandCellClick(sender As Object, e As EventArgs) Handles gvPriceChart.CommandCellClick
        Try
            If gvPriceChart.CurrentColumn Is gvPriceChart.Columns(colFillDeduction) Then
                Dim frm As New frmPriceChartPlanMasterTSDDCFDeduction()
                frm.ArrDed = gvPriceChart.CurrentRow.Cells(colSNFPercentage).Tag
                frm.SNFFrom = 0
                frm.SNFTo = gvPriceChart.CurrentRow.Cells(colSNFPercentage).Value
                frm.ShowDialog()
                If frm.isOK Then
                    gvPriceChart.CurrentRow.Cells(colSNFPercentage).Tag = frm.ArrDed
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Public Function GetRowType() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = Nothing

        'If Not chkEmpty.Checked Then
        dr = dt.NewRow()
        dr("Code") = "Total Solid"
        dt.Rows.Add(dr)
        'End If

        dr = dt.NewRow()
        dr("Code") = "FAT/SNF"
        dt.Rows.Add(dr)

        Return dt
    End Function
    Sub SaveData()

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmPriceChartBulkProc, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If

        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            If AllowToSave() Then
                Dim obj As New clsPriceChartBulkProc
                obj.IsPrice_GradeWise = IIf(chkPriceGradeWise.Checked = True, 1, 0)
                obj.IsPrice_ItemWise = IIf(chkPriceItemWise.Checked = True, 1, 0)
                obj.Price_Code = fndcode.Value
                obj.Price_Date = txtPricedate.Value
                obj.Snf_Percentage = txtsnfPercentage.Value
                obj.Fat_Percentage = txtfatPercentage.Value
                obj.Snf_Weightage = TxtSNFWeightage.Value
                obj.Fat_Weightage = TxtFatWeightage.Value
                obj.Standard_Rate = txtStanadardrate.Value
                obj.Tolerance = clsCommon.myCdbl(txtTolerance.Value)
                obj.vendor_code = clsCommon.myCstr(fndVendor.Value)
                obj.vendor_desc = clsCommon.myCstr(lblVendorName.Text)
                obj.Total_Solid_Rate = txtTotalSolidRate.Value
                obj.Total_Solid_Unit_Code = txtUOM.Value
                obj.IsDefaultForTankerDispatch = clsCommon.myCdbl(IIf(chkDefaultForTankerDispatch.Checked = True, 1, 0))
                obj.Milk_Type_Code = txtMilktypeCode.Value
                obj.effective_Date = dtpEffectiveDate.Value
                obj.ExpiryDate = dtpExpiryDate.Value
                If IsItemMilkType = 1 AndAlso IsPriceChartGradeWise = 1 Then
                    obj.Arr = New List(Of clspriceCodeBulkProcDetail)
                    For Each grow As GridViewRowInfo In gvPriceChart.Rows
                        Dim objTr As New clspriceCodeBulkProcDetail()
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSLNo).Value)
                        objTr.Milk_Grade_code = clsCommon.myCstr(grow.Cells(colMilkGradeCode).Value)
                        objTr.Snf_Weightage = clsCommon.myCdbl(grow.Cells(colSNFWeigtage).Value)
                        objTr.Fat_Weightage = clsCommon.myCdbl(grow.Cells(colFatWeigtage).Value)
                        objTr.Snf_Percentage = clsCommon.myCdbl(grow.Cells(colSNFPercentage).Value)
                        objTr.Fat_Percentage = clsCommon.myCdbl(grow.Cells(colFatPercentage).Value)
                        objTr.Standard_Rate = clsCommon.myCdbl(grow.Cells(colStandardRate).Value)
                        objTr.Tolerance = clsCommon.myCdbl(grow.Cells(colTolerance).Value)
                        objTr.Remarks = clsCommon.myCdbl(grow.Cells(colRemarks).Value)

                        If (clsCommon.myLen(objTr.Milk_Grade_code) > 0) Then
                            obj.Arr.Add(objTr)
                        End If
                    Next
                ElseIf IsItemMilkType = 1 AndAlso AllowCreateBulkProcPriceChartItemWise = 1 OrElse (ApplyBothtsrateAndFatRateinBulkProcurement = True AndAlso AllowCreateBulkProcPriceChartItemWise = 1) Then
                    obj.ArrItemWise = New List(Of clspriceCodeBulkProcDetailItemWise)
                    For Each grow As GridViewRowInfo In gvPriceChart.Rows
                        Dim objTr As New clspriceCodeBulkProcDetailItemWise()
                        objTr.Line_No = clsCommon.myCdbl(grow.Cells(colSLNo).Value)
                        objTr.Milk_Grade_code = clsCommon.myCstr(grow.Cells(colMilkGradeCode).Value)
                        objTr.Snf_Weightage = clsCommon.myCdbl(grow.Cells(colSNFWeigtage).Value)
                        objTr.Fat_Weightage = clsCommon.myCdbl(grow.Cells(colFatWeigtage).Value)
                        objTr.Snf_Percentage = clsCommon.myCdbl(grow.Cells(colSNFPercentage).Value)
                        objTr.Fat_Percentage = clsCommon.myCdbl(grow.Cells(colFatPercentage).Value)
                        objTr.Standard_Rate = clsCommon.myCdbl(grow.Cells(colStandardRate).Value)
                        objTr.Tolerance = clsCommon.myCdbl(grow.Cells(colTolerance).Value)
                        objTr.arrSNFDeduction = TryCast(grow.Cells(colSNFPercentage).Tag, Dictionary(Of Decimal, Decimal))
                        If (ApplyBothtsrateAndFatRateinBulkProcurement = True AndAlso AllowCreateBulkProcPriceChartItemWise = 1) Then
                            objTr.PriceType = clsCommon.myCstr(grow.Cells(colRowType).Value)
                            objTr.TotalSolidRate = clsCommon.myCdbl(grow.Cells(colTotalSolidRate).Value)
                            objTr.TotalSolidUOM = clsCommon.myCstr(grow.Cells(colTotalSolidUOM).Value)
                        End If
                        If (clsCommon.myLen(objTr.Milk_Grade_code) > 0) Then
                            obj.ArrItemWise.Add(objTr)
                        End If
                    Next
                End If
                Dim qry As Integer = clsDBFuncationality.getSingleValue("select count(Price_Code) from TSPL_Bulk_Price_MASTER where Price_Code='" + obj.Price_Code + "'", trans)
                If (qry = 0) Then
                    isNewEntry = True
                Else
                    isNewEntry = False
                End If
                If (clsPriceChartBulkProc.SaveData(obj, isNewEntry, trans)) Then
                    If isNewEntry Then
                        clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
                        LoadData(obj.Price_Code, NavigatorType.Current)
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully", Me.Text)
                        LoadData(obj.Price_Code, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()

    End Sub

    Sub PostData()
        Try
            Dim msg As String = ""
            If (myMessages.postConfirm()) Then

                If (clsPriceChartBulkProc.PostData(MyBase.Form_ID, fndcode.Value)) Then
                    msg = "Successfully Posted"
                Else

                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndcode.Value, NavigatorType.Current)


            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        funPrint()
    End Sub

    Public Sub funPrint()
        Try
            If clsCommon.myLen(fndcode.Value) <= 0 Then
                clsCommon.MyMessageBoxShow("Select Document No ")
            End If
            Dim query = "select Price_Code,convert(varchar,Price_Date,103)as Price_Date,TSPL_Bulk_Price_MASTER.Standard_Rate,TSPL_Bulk_Price_MASTER.Tolerance,isnull(convert(varchar,Effective_Date,103),convert(varchar,getdate(),103))as Effective_Date,TSPL_Bulk_Price_MASTER.Fat_Percentage,TSPL_Bulk_Price_MASTER.Snf_Percentage,TSPL_Bulk_Price_MASTER.Fat_Weightage,TSPL_Bulk_Price_MASTER.Snf_Weightage,TSPL_Bulk_Price_MASTER.Created_By,convert(varchar,TSPL_Bulk_Price_MASTER.Created_Date,103) as Created_Date,Modified_By,Modified_Date,Posted,Posted_By,convert(varchar,Posted_Date,103) as Posted_Date,TSPL_Bulk_Price_MASTER.comp_code,TSPL_COMPANY_MASTER.Logo_Img,TSPL_COMPANY_MASTER.Logo_Img2,TSPL_COMPANY_MASTER.Add1 + case When TSPL_COMPANY_MASTER.Add2='' Then '' else ', '+ Convert(Varchar(50),TSPL_COMPANY_MASTER.Add2, 103) End + Case When TSPL_COMPANY_MASTER.Add3='' Then '' Else ', '+ COnvert( Varchar,TSPL_COMPANY_MASTER.Add3,103) end + case When TSPL_COMPANY_MASTER.City_Code ='' then '' else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.City_Code, 103) end+ Case When TSPL_COMPANY_MASTER.State='' Then '' else ', '+Convert(Varchar, TSPL_COMPANY_MASTER.State) end +  Case When TSPL_COMPANY_MASTER.Pincode='' Then '' Else ', '+ Convert(Varchar,TSPL_COMPANY_MASTER.Pincode, 103)  end  as CompAdd from TSPL_Bulk_Price_MASTER left outer join TSPL_COMPANY_MASTER on tspl_company_master.comp_code=TSPL_Bulk_Price_MASTER.comp_code where Price_Code='" & fndcode.Value & "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
            If dt.Rows.Count > 0 Then
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkProcItemPrice", "Bulk Price Master")
                frmCRV = Nothing
            Else
                ' clsCommon.MyMessageBoxShow("No data found to Print")
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        If SettBulkProcurementApplyTotalSoidRate Then
            If txtTotalSolidRate.Value <= 0 Then
                txtTotalSolidRate.Focus()
                Throw New Exception("Please define total solid rate")
            End If
            If clsCommon.myLen(txtUOM.Value) <= 0 Then
                txtUOM.Focus()
                Throw New Exception("Please define total solid UOM")
            End If
        ElseIf SettApplyCalculateWeightInLtr Then
            If clsCommon.myLen(txtUOM.Value) <= 0 Then
                txtUOM.Focus()
                Throw New Exception("Please define UOM")
            End If
        ElseIf (IsItemMilkType = 0 AndAlso IsPriceChartGradeWise = 0 AndAlso AllowCreateBulkProcPriceChartItemWise = 0) OrElse (IsItemMilkType = 1 AndAlso IsPriceChartGradeWise = 0 AndAlso AllowCreateBulkProcPriceChartItemWise = 0) Then
            If clsCommon.myCdbl(TxtFatWeightage.Value) <= 0 Then
                TxtFatWeightage.Focus()
                Throw New Exception("Fat Weightage cannot be Zero or blank")
            End If
            If clsCommon.myCdbl(TxtSNFWeightage.Value) <= 0 Then
                TxtSNFWeightage.Focus()
                Throw New Exception("SNF Weightage cannot be Zero or blank")
            End If
            Dim totalW As Double = clsCommon.myCdbl(TxtSNFWeightage.Text) + clsCommon.myCdbl(TxtFatWeightage.Text)
            If totalW <> 100 Then
                Throw New Exception("Total FAT and SNF Weightage must be 100")
            End If
            If clsCommon.myCdbl(txtfatPercentage.Value) <= 0 Then
                txtfatPercentage.Focus()
                Throw New Exception("Fat Percentage cannot be Zero or blank")
            End If
            If clsCommon.myCdbl(txtsnfPercentage.Value) <= 0 Then
                txtsnfPercentage.Focus()
                Throw New Exception("SNF Percentage cannot be Zero or blank")
            End If
            If StandardRateWithZero = False Then
                If clsCommon.myCdbl(txtStanadardrate.Value) <= 0 Then
                    txtStanadardrate.Focus()
                    Throw New Exception("Standard Rate cannot be Zero or blank")
                End If
            End If
            If IsItemMilkType = 1 Then
                If clsCommon.myLen(txtMilktypeCode.Value) <= 0 Then
                    txtMilktypeCode.Focus()
                    Throw New Exception("Please Select Milk type Code")
                End If
            End If
        ElseIf (IsItemMilkType = 1 AndAlso IsPriceChartGradeWise = 1) OrElse (IsItemMilkType = 1 AndAlso AllowCreateBulkProcPriceChartItemWise = 1) OrElse (ApplyBothtsrateAndFatRateinBulkProcurement = True AndAlso AllowCreateBulkProcPriceChartItemWise = 1) Then
            If clsCommon.myLen(txtMilktypeCode.Value) <= 0 Then
                txtMilktypeCode.Focus()
                Throw New Exception("Please Select Milk type Code")
            End If
            For ii As Integer = 0 To gvPriceChart.Rows.Count - 1
                Dim strMilkGrade As String = clsCommon.myCstr(gvPriceChart.Rows(ii).Cells(colMilkGradeType).Value)
                Dim dblSnfWeigtage As Double = clsCommon.myCdbl(gvPriceChart.Rows(ii).Cells(colSNFWeigtage).Value)
                Dim dblFatWeightage As Double = clsCommon.myCdbl(gvPriceChart.Rows(ii).Cells(colFatWeigtage).Value)
                Dim dblSnfPercentage As Double = clsCommon.myCdbl(gvPriceChart.Rows(ii).Cells(colSNFPercentage).Value)
                Dim dblFatPercetage As Double = clsCommon.myCdbl(gvPriceChart.Rows(ii).Cells(colFatPercentage).Value)
                Dim dblStandardrate As Double = clsCommon.myCdbl(gvPriceChart.Rows(ii).Cells(colStandardRate).Value)
                Dim dblTolerance As Double = clsCommon.myCdbl(gvPriceChart.Rows(ii).Cells(colTolerance).Value)
                Dim dblTotalSolidrate As Double = 0
                Dim strTotalSolidUOM As String = String.Empty
                If (ApplyBothtsrateAndFatRateinBulkProcurement = True AndAlso AllowCreateBulkProcPriceChartItemWise = 1) Then
                    dblTotalSolidrate = clsCommon.myCdbl(gvPriceChart.Rows(ii).Cells(colTotalSolidRate).Value)
                    strTotalSolidUOM = clsCommon.myCstr(gvPriceChart.Rows(ii).Cells(colTotalSolidUOM).Value)
                End If
                If clsCommon.myLen(strMilkGrade) > 0 Then
                    For jj As Integer = 0 To gvPriceChart.Rows.Count - 1
                        If jj = ii Then
                            Continue For
                        End If
                        Dim strInnerMilkGrade As String = clsCommon.myCstr(gvPriceChart.Rows(jj).Cells(colMilkGradeType).Value)
                        If clsCommon.CompairString(strMilkGrade, strInnerMilkGrade) = CompairStringResult.Equal Then
                            If AllowCreateBulkProcPriceChartItemWise = 1 Then
                                Throw New Exception("Same Item Code Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1))
                            Else
                                Throw New Exception("Same Milk Grade Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1))
                            End If

                        End If
                    Next
                    If (ApplyBothtsrateAndFatRateinBulkProcurement = True AndAlso AllowCreateBulkProcPriceChartItemWise = 1) Then
                        If clsCommon.CompairString(clsCommon.myCstr(gvPriceChart.Rows(ii).Cells(colRowType).Value), "Total Solid") <> CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dblFatWeightage) <= 0 Then
                                Throw New Exception("Fat Weightage cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                            If clsCommon.myCdbl(dblSnfWeigtage) <= 0 Then
                                Throw New Exception("SNF Weightage cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                            Dim totalW As Double = clsCommon.myCdbl(dblSnfWeigtage) + clsCommon.myCdbl(dblFatWeightage)
                            If totalW <> 100 Then
                                Throw New Exception("Total FAT and SNF Weightage must be 100 for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                            If clsCommon.myCdbl(dblFatPercetage) <= 0 Then
                                Throw New Exception("Fat Percentage cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                            If clsCommon.myCdbl(dblSnfPercentage) <= 0 Then
                                Throw New Exception("SNF Percentage cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                            If StandardRateWithZero = False Then
                                If clsCommon.myCdbl(dblStandardrate) <= 0 Then
                                    Throw New Exception("Standard Rate cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                                End If
                            End If
                        ElseIf clsCommon.CompairString(clsCommon.myCstr(gvPriceChart.Rows(ii).Cells(colRowType).Value), "Total Solid") = CompairStringResult.Equal Then
                            If clsCommon.myCdbl(dblTotalSolidrate) <= 0 Then
                                Throw New Exception("Total Solid Rate cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                            If clsCommon.myLen(clsCommon.myCstr(strTotalSolidUOM)) <= 0 Then
                                Throw New Exception("TS UOM cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If
                    Else
                        If clsCommon.myCdbl(dblFatWeightage) <= 0 Then
                            Throw New Exception("Fat Weightage cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                        If clsCommon.myCdbl(dblSnfWeigtage) <= 0 Then
                            Throw New Exception("SNF Weightage cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                        Dim totalW As Double = clsCommon.myCdbl(dblSnfWeigtage) + clsCommon.myCdbl(dblFatWeightage)
                        If totalW <> 100 Then
                            Throw New Exception("Total FAT and SNF Weightage must be 100 for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                        If clsCommon.myCdbl(dblFatPercetage) <= 0 Then
                            Throw New Exception("Fat Percentage cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                        If clsCommon.myCdbl(dblSnfPercentage) <= 0 Then
                            Throw New Exception("SNF Percentage cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                        End If
                        If StandardRateWithZero = False Then
                            If clsCommon.myCdbl(dblStandardrate) <= 0 Then
                                Throw New Exception("Standard Rate cannot be Zero or blank for Grade " + strMilkGrade + ". At Line No" + clsCommon.myCstr(ii + 1))
                            End If
                        End If
                    End If
                    
                End If
            Next
        End If
        Return True
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)

        Dim obj As clsPriceChartBulkProc = clsPriceChartBulkProc.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            LoadBlankGrid()
            fndcode.Value = obj.Price_Code
            txtPricedate.Value = obj.Price_Date
            txtfatPercentage.Value = obj.Fat_Percentage
            TxtFatWeightage.Value = obj.Fat_Weightage
            txtsnfPercentage.Value = obj.Snf_Percentage
            TxtSNFWeightage.Value = obj.Snf_Weightage
            txtStanadardrate.Value = obj.Standard_Rate
            txtTolerance.Value = clsCommon.myCdbl(obj.Tolerance)
            chkDefaultForTankerDispatch.Checked = IIf(obj.IsDefaultForTankerDispatch = 1, True, False)
            fndVendor.Value = obj.vendor_code
            lblVendorName.Text = obj.vendor_desc
            fndcode.MyReadOnly = True
            txtMilktypeCode.Value = obj.Milk_Type_Code
            lblMilkType.Text = clsMilkTypeMaster.getMilkTypeName(txtMilktypeCode.Value, Nothing)
            If clsCommon.myLen(obj.effective_Date) > 0 Then
                dtpEffectiveDate.Value = obj.effective_Date
            End If
            If clsCommon.myLen(obj.ExpiryDate) > 0 Then
                dtpExpiryDate.Value = obj.ExpiryDate
            Else
                dtpExpiryDate.Value = DateAdd(DateInterval.Day, AddDaysFOrExpiryDate, dtpEffectiveDate.Value)
            End If
            chkPriceGradeWise.Checked = IIf(obj.IsPrice_GradeWise = 1, True, False)
            chkPriceItemWise.Checked = IIf(obj.IsPrice_ItemWise = 1, True, False)
            txtTotalSolidRate.Value = obj.Total_Solid_Rate
            txtUOM.Value = obj.Total_Solid_Unit_Code
            If IsItemMilkType = 1 AndAlso IsPriceChartGradeWise = 1 Then

                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As clspriceCodeBulkProcDetail In obj.Arr
                        gvPriceChart.Rows.AddNew()
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colSLNo).Value = objTr.Line_No
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colMilkGradeCode).Value = objTr.Milk_Grade_code
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colMilkGradeType).Value = clsMilkGradeMaster.getMilkGradeType(objTr.Milk_Grade_code, Nothing)
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colSNFWeigtage).Value = objTr.Snf_Weightage
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colFatWeigtage).Value = objTr.Fat_Weightage
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colSNFPercentage).Value = objTr.Snf_Percentage
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colFatPercentage).Value = objTr.Fat_Percentage
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colStandardRate).Value = objTr.Standard_Rate
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colTolerance).Value = objTr.Tolerance
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                    Next
                End If
            ElseIf IsItemMilkType = 1 AndAlso AllowCreateBulkProcPriceChartItemWise = 1 OrElse (ApplyBothtsrateAndFatRateinBulkProcurement = True AndAlso AllowCreateBulkProcPriceChartItemWise = 1) Then
                If obj.ArrItemWise IsNot Nothing AndAlso obj.ArrItemWise.Count > 0 Then
                    For Each objTr As clspriceCodeBulkProcDetailItemWise In obj.ArrItemWise
                        gvPriceChart.Rows.AddNew()
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colSLNo).Value = objTr.Line_No
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colMilkGradeCode).Value = objTr.Milk_Grade_code
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colMilkGradeType).Value = clsItemMaster.GetItemName(objTr.Milk_Grade_code, Nothing)
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colSNFWeigtage).Value = objTr.Snf_Weightage
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colFatWeigtage).Value = objTr.Fat_Weightage
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colSNFPercentage).Value = objTr.Snf_Percentage
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colSNFPercentage).Tag = objTr.arrSNFDeduction
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colFatPercentage).Value = objTr.Fat_Percentage
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colStandardRate).Value = objTr.Standard_Rate
                        gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colTolerance).Value = objTr.Tolerance
                        If (ApplyBothtsrateAndFatRateinBulkProcurement = True AndAlso AllowCreateBulkProcPriceChartItemWise = 1) Then
                            gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colRowType).Value = objTr.PriceType
                            gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colTotalSolidRate).Value = objTr.TotalSolidRate
                            gvPriceChart.Rows(gvPriceChart.Rows.Count - 1).Cells(colTotalSolidUOM).Value = objTr.TotalSolidUOM
                        End If
                    Next
                End If
            End If
            btnsave.Text = "&Update"
            btndelete.Enabled = True
            If (clsCommon.myCdbl(obj.Posted)) = 1 Then
                btnPost.Enabled = False
                btnsave.Enabled = False
                btndelete.Enabled = False
            Else
                btnPost.Enabled = True
                btnsave.Enabled = True
                btndelete.Enabled = True
            End If

        End If
        ItemWiseGradWiseEnableDisable()
    End Sub

    Private Sub DeleteData()
        Try
            If (deleteConfirm()) Then
                If (clsPriceChartBulkProc.DeleteData(fndcode.Value)) Then
                    common.clsCommon.MyMessageBoxShow("Data Deleted Successfully ")
                    Reset()
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        SaveData()
    End Sub

    Private Sub btndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndelete.Click
        DeleteData()
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnnew.Click
        Reset()
    End Sub

    Private Sub fndcode__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndcode._MYNavigator
        Try
            Dim strMilkType As String = Nothing
            Dim qry As String = Nothing
            If IsItemMilkType = 1 Then
                strMilkType = " and TSPL_Bulk_Price_MASTER.milk_type_code <> '' "
            End If
            If AllowCreateBulkProcPriceChartItemWise = 1 Then
                strMilkType += " and IsPrice_ItemWise =1"
            End If
            qry = "select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + fndcode.Value + "' and Comp_Code='" + objCommonVar.CurrentCompanyCode + "' " & strMilkType & ""
            Dim check As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry))
            If check > 0 Then
                fndcode.MyReadOnly = True
            ElseIf check <= 0 Then
                fndcode.MyReadOnly = False
            End If

            LoadData(fndcode.Value, NavType)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub fndcode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndcode._MYValidating
        Dim strMilkType As String = Nothing
        Dim qry As String = Nothing
        If IsItemMilkType = 1 Then
            strMilkType = "  TSPL_Bulk_Price_MASTER.milk_type_code <> '' "
        End If
        If AllowCreateBulkProcPriceChartItemWise = 1 Then
            strMilkType += " and IsPrice_ItemWise =1 "
        End If
        qry = " select TSPL_Bulk_Price_MASTER.Price_Code as [Code] ,TSPL_Bulk_Price_MASTER.Price_Date as [Price Date] ,TSPL_Bulk_Price_MASTER.Fat_Weightage as [FAT Weightage] ,TSPL_Bulk_Price_MASTER.Snf_Weightage as [SNF Weightage] ,TSPL_Bulk_Price_MASTER.Fat_Percentage as [FAT Percentage] ,TSPL_Bulk_Price_MASTER.Snf_Percentage as [SNF Percentage] ,TSPL_Bulk_Price_MASTER.Standard_Rate as [Standard Rate] ,TSPL_Bulk_Price_MASTER.Vendor_Code as [Vendor Code] ,TSPL_Bulk_Price_MASTER.Vendor_Desc as [Vendor Desc] ,TSPL_Bulk_Price_MASTER.Comp_Code as [Company Code] ,TSPL_Bulk_Price_MASTER.Created_By as [Created By] ,TSPL_Bulk_Price_MASTER.Created_Date as [Created Date] ,TSPL_Bulk_Price_MASTER.Modified_By as [Modified By] ,TSPL_Bulk_Price_MASTER.Modified_Date as [Modified Date],case isnull(TSPL_Bulk_Price_MASTER.Posted,0) when '0' Then 'Pending' When '1' Then 'Approved' else '' end as 'Status'  From TSPL_Bulk_Price_MASTER "
        fndcode.Value = clsCommon.ShowSelectForm("BulkPriceMASTER", qry, "Code", strMilkType, fndcode.Value, "", isButtonClicked)
        LoadData(fndcode.Value, NavigatorType.Current)
    End Sub

    Private Sub RmImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmImport.Click
        Dim gv As New RadGridView()
        Dim IsNewEntry As Boolean
        Dim trans As SqlTransaction = Nothing
        Me.Controls.Add(gv)
        If IsItemMilkType = 1 And IsPriceChartGradeWise = 0 Then
            If transportSql.importExcel(gv, "Price Code", "Price Date", "Fat Weightage", "SNF Weightage", "Fat Percentage", "SNF Percentage", "Standard Rate", "Vendor Code", "Tolerance", "Is Default For Tanker Dispatch", "effective Date", "Milk Type Code") Then
                Dim linno As Integer = 0
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    connectSql.OpenConnection()


                    For Each grow As GridViewRowInfo In gv.Rows

                        Dim obj As New clsPriceChartBulkProc
                        Dim effective_Date As String = Nothing
                        Dim strPriceDate As String
                        Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                        If clsCommon.myLen(grow.Cells("Price Date").Value) > 0 AndAlso IsDate(grow.Cells("Price Date").Value) Then
                            strPriceDate = clsCommon.myCDate(grow.Cells("Price Date").Value)
                        Else
                            strPriceDate = ""
                        End If
                        Dim DblFatWeightage As Double = clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                        Dim DblFatPercentage As Double = clsCommon.myCdbl(grow.Cells("Fat Percentage").Value)
                        Dim DblSnfWeightage As Double = clsCommon.myCdbl(grow.Cells("SNF Weightage").Value)
                        Dim DblSnfPercentage As Double = clsCommon.myCdbl(grow.Cells("SNF Percentage").Value)
                        Dim DblStandardRate As Double = clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                        Dim DblTolerance As Double = clsCommon.myCdbl(grow.Cells("Tolerance").Value)
                        Dim vendorCode As String = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                        Dim IsDefaultForTankerDispatch As Integer = 0
                        If clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "0") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "1") = CompairStringResult.Equal Then
                            IsDefaultForTankerDispatch = clsCommon.myCdbl(grow.Cells("Is Default For Tanker Dispatch").Value)
                        ElseIf clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Yes") = CompairStringResult.Equal Then
                            IsDefaultForTankerDispatch = 1
                        Else
                            IsDefaultForTankerDispatch = 0
                        End If
                        'effective_Date = clsCommon.GetPrintDate(clsCommon.myCDate(grow.Cells("effective Date").Value), "dd/MMM/yyyy")
                        If clsCommon.myLen(grow.Cells("effective Date").Value) > 0 AndAlso IsDate(grow.Cells("effective Date").Value) Then
                            effective_Date = clsCommon.myCDate(grow.Cells("effective Date").Value)
                        Else
                            effective_Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
                        End If
                        obj.effective_Date = effective_Date
                        Dim Milk_Type_Code As String = clsCommon.myCstr(grow.Cells("Milk Type Code").Value)

                        linno += 1

                        If (String.IsNullOrEmpty(strPriceCode)) Or clsCommon.myLen(strPriceCode) > 30 Then
                            Throw New Exception("Length of Price Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Price_Code = strPriceCode

                        If (String.IsNullOrEmpty(strPriceDate)) Or clsCommon.myLen(strPriceDate) < 0 Then
                            Throw New Exception("Price Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Price_Date = strPriceDate

                        If clsCommon.myCdbl(DblFatWeightage) <= 0 Then
                            Throw New Exception("Fat Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Fat_Weightage = DblFatWeightage

                        If clsCommon.myCdbl(DblSnfWeightage) <= 0 Then
                            Throw New Exception("SNF Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Snf_Weightage = DblSnfWeightage

                        If clsCommon.myCdbl(DblFatPercentage) <= 0 Then
                            Throw New Exception("Fat Percentage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Fat_Percentage = DblFatPercentage

                        If clsCommon.myCdbl(DblSnfPercentage) <= 0 Then
                            Throw New Exception("SNF Percentage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Snf_Percentage = DblSnfPercentage

                        If clsCommon.myCdbl(DblStandardRate) <= 0 Then
                            Throw New Exception("Standard Rate should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Standard_Rate = DblStandardRate

                        If clsCommon.myLen(strPriceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' ", trans) > 0 Then
                            IsNewEntry = False
                        Else
                            IsNewEntry = True

                        End If
                        obj.vendor_code = vendorCode
                        obj.Tolerance = clsCommon.myCdbl(DblTolerance)
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))
                        clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
                        clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
                        clsCommon.AddColumnsForChange(coll, "Fat_Percentage", obj.Fat_Percentage)
                        clsCommon.AddColumnsForChange(coll, "Snf_Percentage", obj.Snf_Percentage)
                        clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
                        clsCommon.AddColumnsForChange(coll, "tolerance", obj.Tolerance)
                        clsCommon.AddColumnsForChange(coll, "Vendor_code", obj.vendor_code)
                        clsCommon.AddColumnsForChange(coll, "effective_Date", clsCommon.GetPrintDate(obj.effective_Date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Milk_Type_Code", obj.Milk_Type_Code)
                        If clsCommon.myLen(obj.vendor_code) > 0 Then
                            obj.vendor_desc = clsVendorMaster.GetName(obj.vendor_code, trans)
                        Else
                            obj.vendor_desc = ""
                        End If
                        clsCommon.AddColumnsForChange(coll, "Vendor_desc", obj.vendor_desc)
                        clsCommon.AddColumnsForChange(coll, "IsDefaultForTankerDispatch", obj.IsDefaultForTankerDispatch)
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        If IsNewEntry Then
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code.ToUpper())
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Update, "TSPL_Bulk_Price_MASTER.Price_Code='" + obj.Price_Code + "'", trans)
                        End If
                        If IsDefaultForTankerDispatch = 1 Then
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_Bulk_Price_MASTER set IsDefaultForTankerDispatch=0 where  Price_Code<>'" & obj.Price_Code.ToUpper() & "'", trans)
                        End If
                    Next
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    myMessages.myExceptions(ex)
                End Try
            End If
        ElseIf IsItemMilkType = 1 And IsPriceChartGradeWise = 1 Then
            If transportSql.importExcel(gv, "Price Code", "Price Date", "Is Default For Tanker Dispatch", "effective Date", "Milk Type Code") Then
                Dim linno As Integer = 0
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    connectSql.OpenConnection()

                    For Each grow As GridViewRowInfo In gv.Rows
                        Dim obj As New clsPriceChartBulkProc
                        Dim strPriceDate As String
                        Dim effective_Date As String = Nothing
                        Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                        If clsCommon.myLen(grow.Cells("Price Date").Value) > 0 AndAlso IsDate(grow.Cells("Price Date").Value) Then
                            strPriceDate = clsCommon.myCDate(grow.Cells("Price Date").Value)
                        Else
                            strPriceDate = ""
                        End If
                        Dim IsDefaultForTankerDispatch As Integer = 0
                        If clsCommon.myLen(grow.Cells("effective Date").Value) > 0 AndAlso IsDate(grow.Cells("effective Date").Value) Then
                            effective_Date = clsCommon.myCDate(grow.Cells("effective Date").Value)
                        Else
                            effective_Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy")
                        End If
                        obj.effective_Date = effective_Date
                        Dim Milk_Type_Code As String = clsCommon.myCstr(grow.Cells("Milk Type Code").Value)
                        obj.Milk_Type_Code = Milk_Type_Code
                        If clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "0") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "1") = CompairStringResult.Equal Then
                            IsDefaultForTankerDispatch = clsCommon.myCdbl(grow.Cells("Is Default For Tanker Dispatch").Value)
                        ElseIf clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Yes") = CompairStringResult.Equal Then
                            IsDefaultForTankerDispatch = 1
                        Else
                            IsDefaultForTankerDispatch = 0
                        End If
                        linno += 1

                        If (String.IsNullOrEmpty(strPriceCode)) Or clsCommon.myLen(strPriceCode) > 30 Then
                            Throw New Exception("Length of Price Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Price_Code = strPriceCode

                        If (String.IsNullOrEmpty(strPriceDate)) Or clsCommon.myLen(strPriceDate) < 0 Then
                            Throw New Exception("Price Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Price_Date = strPriceDate

                        If clsCommon.myLen(strPriceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' ", trans) > 0 Then
                            IsNewEntry = False
                        Else
                            IsNewEntry = True

                        End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))
                        clsCommon.AddColumnsForChange(coll, "effective_Date", clsCommon.GetPrintDate(obj.effective_Date, "dd/MMM/yyyy"))
                        clsCommon.AddColumnsForChange(coll, "Milk_Type_Code", obj.Milk_Type_Code, True)
                        clsCommon.AddColumnsForChange(coll, "IsDefaultForTankerDispatch", obj.IsDefaultForTankerDispatch)
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        If IsNewEntry Then
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code.ToUpper())
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Update, "TSPL_Bulk_Price_MASTER.Price_Code='" + obj.Price_Code + "'", trans)
                        End If
                        If IsDefaultForTankerDispatch = 1 Then
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_Bulk_Price_MASTER set IsDefaultForTankerDispatch=0 where  Price_Code<>'" & obj.Price_Code.ToUpper() & "'", trans)
                        End If
                    Next
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    myMessages.myExceptions(ex)
                End Try
            End If
        Else
            If transportSql.importExcel(gv, "Price Code", "Price Date", "Fat Weightage", "SNF Weightage", "Fat Percentage", "SNF Percentage", "Standard Rate", "Vendor Code", "Tolerance", "Is Default For Tanker Dispatch") Then
                Dim linno As Integer = 0
                Try
                    trans = clsDBFuncationality.GetTransactin()
                    connectSql.OpenConnection()


                    For Each grow As GridViewRowInfo In gv.Rows

                        Dim obj As New clsPriceChartBulkProc
                        Dim strPriceDate As String
                        Dim strPriceCode As String = clsCommon.myCstr(grow.Cells("Price Code").Value)
                        If clsCommon.myLen(grow.Cells("Price Date").Value) > 0 AndAlso IsDate(grow.Cells("Price Date").Value) Then
                            strPriceDate = clsCommon.myCDate(grow.Cells("Price Date").Value)
                        Else
                            strPriceDate = ""
                        End If
                        Dim DblFatWeightage As Double = clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                        Dim DblFatPercentage As Double = clsCommon.myCdbl(grow.Cells("Fat Percentage").Value)
                        Dim DblSnfWeightage As Double = clsCommon.myCdbl(grow.Cells("SNF Weightage").Value)
                        Dim DblSnfPercentage As Double = clsCommon.myCdbl(grow.Cells("SNF Percentage").Value)
                        Dim DblStandardRate As Double = clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                        Dim DblTolerance As Double = clsCommon.myCdbl(grow.Cells("Tolerance").Value)
                        Dim vendorCode As String = clsCommon.myCstr(grow.Cells("Vendor Code").Value)
                        Dim IsDefaultForTankerDispatch As Integer = 0
                        If clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "0") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "1") = CompairStringResult.Equal Then
                            IsDefaultForTankerDispatch = clsCommon.myCdbl(grow.Cells("Is Default For Tanker Dispatch").Value)
                        ElseIf clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Y") = CompairStringResult.Equal OrElse clsCommon.CompairString(grow.Cells("Is Default For Tanker Dispatch").Value, "Yes") = CompairStringResult.Equal Then
                            IsDefaultForTankerDispatch = 1
                        Else
                            IsDefaultForTankerDispatch = 0
                        End If


                        linno += 1

                        If (String.IsNullOrEmpty(strPriceCode)) Or clsCommon.myLen(strPriceCode) > 30 Then
                            Throw New Exception("Length of Price Code should be max. 30 character At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Price_Code = strPriceCode

                        If (String.IsNullOrEmpty(strPriceDate)) Or clsCommon.myLen(strPriceDate) < 0 Then
                            Throw New Exception("Price Date should not be left blank At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Price_Date = strPriceDate

                        If clsCommon.myCdbl(DblFatWeightage) <= 0 Then
                            Throw New Exception("Fat Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Fat_Weightage = DblFatWeightage

                        If clsCommon.myCdbl(DblSnfWeightage) <= 0 Then
                            Throw New Exception("SNF Weightage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Snf_Weightage = DblSnfWeightage

                        If clsCommon.myCdbl(DblFatPercentage) <= 0 Then
                            Throw New Exception("Fat Percentage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Fat_Percentage = DblFatPercentage

                        If clsCommon.myCdbl(DblSnfPercentage) <= 0 Then
                            Throw New Exception("SNF Percentage should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Snf_Percentage = DblSnfPercentage

                        If clsCommon.myCdbl(DblStandardRate) <= 0 Then
                            Throw New Exception("Standard Rate should not be left blank or zero At Line No. " + clsCommon.myCstr(linno) + ".")
                        End If
                        obj.Standard_Rate = DblStandardRate

                        If clsCommon.myLen(strPriceCode) > 0 AndAlso clsDBFuncationality.getSingleValue("Select count(*) from TSPL_Bulk_Price_MASTER where Price_Code='" + strPriceCode + "' ", trans) > 0 Then
                            IsNewEntry = False
                        Else
                            IsNewEntry = True

                        End If
                        obj.vendor_code = vendorCode
                        obj.Tolerance = clsCommon.myCdbl(DblTolerance)
                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Price_Date", clsCommon.GetPrintDate(obj.Price_Date, "dd/MMM/yyyy hh:mm tt"))
                        clsCommon.AddColumnsForChange(coll, "Fat_Weightage", obj.Fat_Weightage)
                        clsCommon.AddColumnsForChange(coll, "Snf_Weightage", obj.Snf_Weightage)
                        clsCommon.AddColumnsForChange(coll, "Fat_Percentage", obj.Fat_Percentage)
                        clsCommon.AddColumnsForChange(coll, "Snf_Percentage", obj.Snf_Percentage)
                        clsCommon.AddColumnsForChange(coll, "Standard_Rate", obj.Standard_Rate)
                        clsCommon.AddColumnsForChange(coll, "tolerance", obj.Tolerance)
                        clsCommon.AddColumnsForChange(coll, "Vendor_code", obj.vendor_code)

                        If clsCommon.myLen(obj.vendor_code) > 0 Then
                            obj.vendor_desc = clsVendorMaster.GetName(obj.vendor_code, trans)
                        Else
                            obj.vendor_desc = ""
                        End If
                        clsCommon.AddColumnsForChange(coll, "Vendor_desc", obj.vendor_desc)
                        clsCommon.AddColumnsForChange(coll, "IsDefaultForTankerDispatch", obj.IsDefaultForTankerDispatch)
                        clsCommon.AddColumnsForChange(coll, "Comp_Code", objCommonVar.CurrentCompanyCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_By", objCommonVar.CurrentUserCode)
                        clsCommon.AddColumnsForChange(coll, "Modified_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                        If IsNewEntry Then
                            clsCommon.AddColumnsForChange(coll, "Created_By", objCommonVar.CurrentUserCode)
                            clsCommon.AddColumnsForChange(coll, "Created_Date", clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MMM/yyyy"))
                            clsCommon.AddColumnsForChange(coll, "Price_Code", obj.Price_Code.ToUpper())
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            clsCommonFunctionality.UpdateDataTable(coll, "TSPL_Bulk_Price_MASTER", OMInsertOrUpdate.Update, "TSPL_Bulk_Price_MASTER.Price_Code='" + obj.Price_Code + "'", trans)
                        End If
                        If IsDefaultForTankerDispatch = 1 Then
                            clsDBFuncationality.ExecuteNonQuery("update TSPL_Bulk_Price_MASTER set IsDefaultForTankerDispatch=0 where  Price_Code<>'" & obj.Price_Code.ToUpper() & "'", trans)
                        End If
                    Next
                    trans.Commit()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    myMessages.myExceptions(ex)
                End Try
            End If
        End If

        Me.Controls.Remove(gv)
    End Sub

    Private Sub RmExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RmExport.Click
        Dim str As String
        Dim Qry As String = ""
        If IsItemMilkType = 1 And IsPriceChartGradeWise = 0 Then
            str = "select Price_Code as [Price Code],Price_Date As [Price Date] ,Fat_Weightage as [Fat Weightage],snf_Weightage as [SNF Weightage],Fat_Percentage as[Fat Percentage],Snf_Percentage as [SNF Percentage],Standard_Rate as [Standard Rate],vendor_code as [Vendor Code],Tolerance,IsDefaultForTankerDispatch as [Is Default For Tanker Dispatch],effective_Date as [effective Date],Milk_Type_Code as [Milk Type Code]  from TSPL_Bulk_Price_MASTER"
        ElseIf IsItemMilkType = 1 And IsPriceChartGradeWise = 1 Then
            str = "select Price_Code as [Price Code],Price_Date As [Price Date] ,IsDefaultForTankerDispatch as [Is Default For Tanker Dispatch] ,effective_Date as [effective Date],Milk_Type_Code as [Milk Type Code] from TSPL_Bulk_Price_MASTER"
        Else
            str = "select Price_Code as [Price Code],Price_Date As [Price Date] ,Fat_Weightage as [Fat Weightage],snf_Weightage as [SNF Weightage],Fat_Percentage as[Fat Percentage],Snf_Percentage as [SNF Percentage],Standard_Rate as [Standard Rate],vendor_code as [Vendor Code],Tolerance,IsDefaultForTankerDispatch as [Is Default For Tanker Dispatch]  from TSPL_Bulk_Price_MASTER"
        End If
        ListImpExpColumnsMandatory = New List(Of String)({"Price Code", "Milk Grade code", "Fat Weightage", "Snf Weightage", "Fat Percentage", "Snf Percentage", "Standard Rate"})
        ListImpExpColumnsSuperMandatory = New List(Of String)({"Price Code"})
        transportSql.ExporttoExcel(str, "", "", Me, ListImpExpColumnsMandatory, ListImpExpColumnsSuperMandatory, MyBase.Form_ID)
    End Sub

    Private Sub fndVendor__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndVendor._MYValidating
        fndVendor.Value = clsVendorMaster.getFinder("", fndVendor.Value, isButtonClicked)
        If clsCommon.myLen(fndVendor.Value) > 0 Then
            lblVendorName.Text = clsVendorMaster.GetName(fndVendor.Value, Nothing)
        End If
    End Sub

    Private Sub TxtFatWeightage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtFatWeightage.TextChanged

        Try

            If clsCommon.myCdbl(TxtFatWeightage.Text) > 100 Then
                TxtFatWeightage.Text = 100
            End If
            TxtSNFWeightage.Text = (100 - clsCommon.myCdbl(TxtFatWeightage.Text))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TxtSNFWeightage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtSNFWeightage.TextChanged
        Try
            If clsCommon.myCdbl(TxtSNFWeightage.Text) > 100 Then
                TxtSNFWeightage.Text = 100
            End If
            TxtFatWeightage.Text = (100 - clsCommon.myCdbl(TxtSNFWeightage.Text))
        Catch ex As Exception

        End Try
    End Sub

    Private Sub gvPriceChart_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles gvPriceChart.CellValueChanged
        If Not isCellValueChangedOpen Then
            isCellValueChangedOpen = True
            If e.Column Is gvPriceChart.Columns(colMilkGradeCode) Then
                If AllowCreateBulkProcPriceChartItemWise = 1 Then
                    OpenICodeList(False)
                    'gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value = clsItemMaster.FinderForItem("", "", True, "", "")   '.getFinder("", gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value, False)
                    'gvPriceChart.CurrentRow.Cells(colMilkGradeType).Value = clsMilkGradeMaster.getMilkGradeType(clsCommon.myCstr(gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value), Nothing)
                Else
                    gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value = clsMilkGradeMaster.getFinder("", gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value, False)
                    gvPriceChart.CurrentRow.Cells(colMilkGradeType).Value = clsMilkGradeMaster.getMilkGradeType(clsCommon.myCstr(gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value), Nothing)
                End If
            ElseIf e.Column Is gvPriceChart.Columns(colTolerance) Then
                If ApplyTolerance Then
                    Dim StandardRate As Decimal = 0
                    Dim PerVal As Decimal = 0

                    StandardRate = gvPriceChart.CurrentRow.Cells(colStandardRate).Value
                    PerVal = StandardRate * (5 / 100)
                    If gvPriceChart.CurrentRow.Cells(colTolerance).Value > PerVal Then
                        If clsCommon.MyMessageBoxShow(Me, "Tolerance is greater then 5% of Standard Rate, Do You want to Continue?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
                            Dim Reason As String = ""
                            Dim frm As New FrmFreeTxtBox1
                            frm.Text = "Remarks for Tolerance"
                            frm.ShowDialog()
                            If clsCommon.myLen(frm.strRmks) <= 0 Then
                                Exit Sub
                            Else
                                Reason = frm.strRmks
                            End If
                            gvPriceChart.CurrentRow.Cells(colRemarks).Value = Reason
                        Else
                            gvPriceChart.CurrentRow.Cells(colTolerance).Value = 0
                        End If
                    End If

                End If


            ElseIf e.Column Is gvPriceChart.Columns(colRowType) Then
                If (ApplyBothtsrateAndFatRateinBulkProcurement = True AndAlso AllowCreateBulkProcPriceChartItemWise = 1) Then
                    If clsCommon.CompairString(clsCommon.myCstr(gvPriceChart.CurrentRow.Cells(colRowType).Value), "Total Solid") = CompairStringResult.Equal Then
                        gvPriceChart.CurrentRow.Cells(colFatPercentage).ReadOnly = True
                        gvPriceChart.CurrentRow.Cells(colSNFPercentage).ReadOnly = True
                        gvPriceChart.CurrentRow.Cells(colFatWeigtage).ReadOnly = True
                        gvPriceChart.CurrentRow.Cells(colSNFWeigtage).ReadOnly = True
                        gvPriceChart.CurrentRow.Cells(colStandardRate).ReadOnly = True
                        gvPriceChart.CurrentRow.Cells(colTolerance).ReadOnly = True
                        gvPriceChart.CurrentRow.Cells(colTotalSolidRate).ReadOnly = False
                        gvPriceChart.CurrentRow.Cells(colTotalSolidUOM).ReadOnly = False
                        gvPriceChart.CurrentRow.Cells(colFatPercentage).Value = 0
                        gvPriceChart.CurrentRow.Cells(colSNFPercentage).Value = 0
                        gvPriceChart.CurrentRow.Cells(colFatWeigtage).Value = 0
                        gvPriceChart.CurrentRow.Cells(colSNFWeigtage).Value = 0
                        gvPriceChart.CurrentRow.Cells(colStandardRate).Value = 0
                        gvPriceChart.CurrentRow.Cells(colTolerance).Value = 0
                    Else
                        gvPriceChart.CurrentRow.Cells(colTotalSolidRate).ReadOnly = True
                        gvPriceChart.CurrentRow.Cells(colTotalSolidUOM).ReadOnly = True
                        gvPriceChart.CurrentRow.Cells(colFatPercentage).ReadOnly = False
                        gvPriceChart.CurrentRow.Cells(colSNFPercentage).ReadOnly = False
                        gvPriceChart.CurrentRow.Cells(colFatWeigtage).ReadOnly = False
                        gvPriceChart.CurrentRow.Cells(colSNFWeigtage).ReadOnly = False
                        gvPriceChart.CurrentRow.Cells(colStandardRate).ReadOnly = False
                        gvPriceChart.CurrentRow.Cells(colTolerance).ReadOnly = False
                        gvPriceChart.CurrentRow.Cells(colTotalSolidRate).Value = 0
                        gvPriceChart.CurrentRow.Cells(colTotalSolidUOM).Value = ""
                    End If
                Else
                    gvPriceChart.CurrentRow.Cells(colFatPercentage).ReadOnly = False
                    gvPriceChart.CurrentRow.Cells(colSNFPercentage).ReadOnly = False
                    gvPriceChart.CurrentRow.Cells(colFatWeigtage).ReadOnly = False
                    gvPriceChart.CurrentRow.Cells(colSNFWeigtage).ReadOnly = False
                    gvPriceChart.CurrentRow.Cells(colStandardRate).ReadOnly = False
                    gvPriceChart.CurrentRow.Cells(colTolerance).ReadOnly = False
                End If
            ElseIf e.Column Is gvPriceChart.Columns(colTotalSolidUOM) Then
                If (ApplyBothtsrateAndFatRateinBulkProcurement = True AndAlso AllowCreateBulkProcPriceChartItemWise = 1) Then
                        If clsCommon.CompairString(gvPriceChart.CurrentRow.Cells(colRowType).Value, "Total Solid") = CompairStringResult.Equal Then
                            OpenUOMList(False)
                        End If
                End If
            End If
        End If
        isCellValueChangedOpen = False
    End Sub
    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gvPriceChart.CurrentRow.Cells(colTotalSolidUOM).Value = clsCommon.ShowSelectForm("TotalSoliDUomFinder", qry, "Code", whrCls, clsCommon.myCstr(gvPriceChart.CurrentRow.Cells(colTotalSolidUOM).Value), "Code", isButtonClick)

        End If
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForItem(clsCommon.myCstr(gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value), "", True, isButtonClick, "", "", " isnull(product_type,'')='MI'")
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value = obj.Item_Code
            gvPriceChart.CurrentRow.Cells(colMilkGradeType).Value = obj.Item_Desc
        Else
            gvPriceChart.CurrentRow.Cells(colMilkGradeCode).Value = ""
            gvPriceChart.CurrentRow.Cells(colMilkGradeType).Value = ""
            'SetBlankOfItemColumns()
        End If
    End Sub

    Private Sub gvPriceChart_CurrentColumnChanged(sender As Object, e As CurrentColumnChangedEventArgs) Handles gvPriceChart.CurrentColumnChanged
        If gvPriceChart.RowCount > 0 Then
            Dim intCurrRow As Integer = gvPriceChart.CurrentRow.Index
            gvPriceChart.CurrentRow.Cells(colSLNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gvPriceChart.Rows.Count - 1 Then
                gvPriceChart.Rows.AddNew()
                gvPriceChart.CurrentRow = gvPriceChart.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub txtMilktypeCode__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtMilktypeCode._MYValidating
        Dim whr As String = ""
        txtMilktypeCode.Value = clsMilkTypeMaster.getFinder(whr, txtMilktypeCode.Value, isButtonClicked)
        If clsCommon.myLen(txtMilktypeCode.Value) > 0 Then
            lblMilkType.Text = clsMilkTypeMaster.getMilkType(txtMilktypeCode.Value, Nothing)
        End If
    End Sub

    Private Sub btnExportDetail_Click(sender As Object, e As EventArgs) Handles btnExportDetail.Click
        Try
            Dim qry = "select count(*) from TSPL_BULK_PRICE_DETAIL"
            Dim check As Integer = clsDBFuncationality.getSingleValue(qry)
            If check > 0 Then
                qry = "select Price_Code as [Price Code],Milk_Grade_code as [Milk Grade code],Fat_Weightage as [Fat Weightage],Snf_Weightage as [Snf Weightage],Fat_Percentage as [Fat Percentage],Snf_Percentage as [Snf Percentage],Standard_Rate as [Standard Rate],Tolerance from TSPL_BULK_PRICE_DETAIL"
            Else
                qry = "select '' as [Price Code] ,'' as [Milk Grade code],0 as [Fat Weightage],0 as [Snf Weightage],0 as [Fat Percentage],0 as [Snf Percentage] ,0 as [Standard Rate],0 as Tolerance from TSPL_BULK_PRICE_DETAIL"
            End If
            transportSql.ExporttoExcel(qry, Me)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub btnInportDetail_Click(sender As Object, e As EventArgs) Handles btnImportDetail.Click
        Try
            Dim gv As New RadGridView()
            Me.Controls.Add(gv)
            Dim issaved As Boolean = True

            If transportSql.importExcel(gv, "Price Code", "Milk Grade code", "Fat Weightage", "Snf Weightage", "Fat Percentage", "Snf Percentage", "Standard Rate", "Tolerance") Then
                clsCommon.ProgressBarShow()
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try

                    Dim Price_Code As String = Nothing
                    Dim Milk_Grade_code As String = Nothing
                    Dim Fat_Weightage As Double = 0
                    Dim Snf_Weightage As Double = 0
                    Dim Fat_Percentage As Double = 0
                    Dim Snf_Percentage As Double = 0
                    Dim Standard_Rate As Double = 0
                    Dim Tolerance As Double = 0

                    Dim counter As Integer = 0

                    For Each grow As GridViewRowInfo In gv.Rows
                        Price_Code = clsCommon.myCstr(grow.Cells("Price Code").Value)
                        If clsCommon.myLen(Price_Code) > 0 Then
                            Dim qry As String = "select Price_Code from TSPL_Bulk_Price_MASTER where Price_Code='" + Price_Code + "'"
                            Price_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry, trans))
                        Else
                            If clsCommon.myLen(Price_Code) <= 0 Then
                                Throw New Exception("Please Fill Price Code in header part")
                            End If
                        End If

                        Milk_Grade_code = clsCommon.myCstr(grow.Cells("Milk Grade code").Value)
                        If clsCommon.myLen(Milk_Grade_code) <= 0 Then
                            Throw New Exception("Please Fill Milk Grade Code")
                        End If

                        Fat_Weightage = clsCommon.myCdbl(grow.Cells("Fat Weightage").Value)
                        Snf_Weightage = clsCommon.myCdbl(grow.Cells("Snf Weightage").Value)
                        Fat_Percentage = clsCommon.myCdbl(grow.Cells("Fat Percentage").Value)
                        Snf_Percentage = clsCommon.myCdbl(grow.Cells("Snf Percentage").Value)
                        Standard_Rate = clsCommon.myCdbl(grow.Cells("Standard Rate").Value)
                        Tolerance = clsCommon.myCdbl(grow.Cells("Tolerance").Value)

                        If clsCommon.myCdbl(Fat_Weightage) <= 0 Then
                            Throw New Exception("Fat Weightage cannot be Zero or blank for Grade " + Milk_Grade_code + ". At Line No" + clsCommon.myCstr(counter))
                        End If
                        If clsCommon.myCdbl(Snf_Weightage) <= 0 Then
                            Throw New Exception("SNF Weightage cannot be Zero or blank for Grade " + Milk_Grade_code + ". At Line No" + clsCommon.myCstr(counter))
                        End If
                        Dim totalW As Double = clsCommon.myCdbl(Snf_Weightage) + clsCommon.myCdbl(Fat_Weightage)
                        If totalW <> 100 Then
                            Throw New Exception("Total FAT and SNF Weightage must be 100 for Grade " + Milk_Grade_code + ". At Line No" + clsCommon.myCstr(counter))
                        End If
                        If clsCommon.myCdbl(Fat_Percentage) <= 0 Then
                            Throw New Exception("Fat Percentage cannot be Zero or blank for Grade " + Milk_Grade_code + ". At Line No" + clsCommon.myCstr(counter))
                        End If
                        If clsCommon.myCdbl(Snf_Percentage) <= 0 Then
                            Throw New Exception("SNF Percentage cannot be Zero or blank for Grade " + Milk_Grade_code + ". At Line No" + clsCommon.myCstr(counter))
                        End If
                        If clsCommon.myCdbl(Standard_Rate) <= 0 Then
                            Throw New Exception("Standard Rate cannot be Zero or blank for Grade " + Milk_Grade_code + ". At Line No" + clsCommon.myCstr(counter))
                        End If


                        'If counter = 0 Then
                        Dim check As Integer = clsDBFuncationality.getSingleValue("select count(*) from tspl_bulk_price_detail WHERE Price_Code ='" + Price_Code + "' and Milk_Grade_code='" + Milk_Grade_code + "'", trans)
                        'End If

                        Dim coll As New Hashtable()
                        clsCommon.AddColumnsForChange(coll, "Price_Code", Price_Code)
                        clsCommon.AddColumnsForChange(coll, "Milk_Grade_code", Milk_Grade_code)
                        'clsCommon.AddColumnsForChange(coll, "Line_No", Line_No)
                        clsCommon.AddColumnsForChange(coll, "Fat_Weightage", Fat_Weightage)
                        clsCommon.AddColumnsForChange(coll, "Snf_Weightage", Snf_Weightage)
                        clsCommon.AddColumnsForChange(coll, "Fat_Percentage", Fat_Percentage)
                        clsCommon.AddColumnsForChange(coll, "Snf_Percentage", Snf_Percentage)
                        clsCommon.AddColumnsForChange(coll, "Standard_Rate", Standard_Rate)
                        clsCommon.AddColumnsForChange(coll, "Tolerance", Tolerance)

                        If check <= 0 Then
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_price_detail", OMInsertOrUpdate.Insert, "", trans)
                        Else
                            issaved = issaved AndAlso clsCommonFunctionality.UpdateDataTable(coll, "tspl_bulk_price_detail", OMInsertOrUpdate.Update, " tspl_bulk_price_detail.Price_Code='" + Price_Code + "' and tspl_bulk_price_detail.Milk_Grade_code='" + Milk_Grade_code + "'", trans)
                        End If

                        counter += 1
                    Next
                    trans.Commit()
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow("Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                Catch ex As Exception
                    trans.Rollback()
                    clsCommon.ProgressBarHide()
                    Throw New Exception(ex.Message)
                End Try
            End If
            Me.Controls.Remove(gv)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub chkPriceGradeWise_ToggleStateChanged(sender As Object, args As StateChangedEventArgs) Handles chkPriceGradeWise.ToggleStateChanged
        If Not SettBulkProcurementApplyTotalSoidRate Then
            If chkPriceGradeWise.Checked Then
                SplitContainer2.SplitterDistance = 50
                RadGroupBox1.Visible = True
                btnImportDetail.Visibility = ElementVisibility.Visible
                btnExportDetail.Visibility = ElementVisibility.Visible
            Else
                RadGroupBox1.Visible = False
                SplitContainer2.SplitterDistance = 324
                btnImportDetail.Visibility = ElementVisibility.Collapsed
                btnExportDetail.Visibility = ElementVisibility.Collapsed
            End If
        End If
    End Sub

    Public Sub ItemWiseGradWiseEnableDisable()
        ' Ticket No : BHA/26/06/18-000090
        If AllowCreateBulkProcPriceChartItemWise = 1 AndAlso IsPriceChartGradeWise = 1 Then
            clsCommon.MyMessageBoxShow("[Create Bulk Procurement price chart-Itemwise] and [Is Price Chart Grade Wise] Both setting On.Please close one setting .... ")
            Me.Close()
        ElseIf AllowCreateBulkProcPriceChartItemWise = 1 Or (AllowCreateBulkProcPriceChartItemWise = 1 And ApplyBothtsrateAndFatRateinBulkProcurement = True) Then
            chkPriceItemWise.Checked = True
            chkPriceItemWise.Enabled = False
            chkPriceGradeWise.Checked = False
            chkPriceGradeWise.Visible = False
            chkPriceItemWise.Visible = True
            SplitContainer2.SplitterDistance = IIf(SettApplyCalculateWeightInLtr, 80, 50)
            RadGroupBox1.Visible = True
        Else
            chkPriceItemWise.Checked = False
            chkPriceGradeWise.Checked = False
            chkPriceGradeWise.Visible = True
            chkPriceItemWise.Visible = False
        End If
    End Sub

    Private Sub txtUOM__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtUOM._MYValidating
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Name from TSPL_UNIT_MASTER"
        txtUOM.Value = clsCommon.ShowSelectForm("BMPC@TSUOM", qry, "Code", "", txtUOM.Value, "Code", isButtonClicked)
    End Sub

End Class
