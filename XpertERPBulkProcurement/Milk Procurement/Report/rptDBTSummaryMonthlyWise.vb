Imports common
Imports System.Data.SqlClient

Public Class rptDBTSummaryMonthlyWise
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim dt As DataTable = Nothing
    Private isNewEntry As Boolean = False
    Const colMonth As String = "colMonth"
    Const colDbtDCS As String = "colDbtDCS"
    Const colDbtFarmer As String = "colDbtFarmer"
    Const colDbtQty As String = "colDbtQty"
    Const colDbtAmt As String = "colDbtAmt"
    Const colRecoDCS As String = "colRecoDCS"
    Const colRecoQty As String = "colRecoQty"
    Const colRecoAmt As String = "colRecoAmt"
    Const colDiffDcs As String = "colDiffDcs"
    Const colDiffQty As String = "colDiffQty"
    Const colDiffAmt As String = "colDiffAmt"

#End Region

    Private Sub frmProjectMaster_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        If clsCommon.myLen(txtFinancialYear.Value) > 0 Then
            LoadData(txtFinancialYear.Value, NavigatorType.Current)
        Else
        End If
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Enabled = False
        txtToDate.Enabled = False
        LoadBlankGrid()
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            common.clsCommon.MyMessageBoxShow(Me, "Permission Denied", Me.Text)
            Me.Close()
            Exit Sub
        End If
    End Sub

    Private Sub txtFinancialYear__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtFinancialYear._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_Fiscal_Year_Master where Fiscal_Code='" + txtFinancialYear.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtFinancialYear.MyReadOnly = False
            Else
                txtFinancialYear.MyReadOnly = True
            End If

            LoadData(txtFinancialYear.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        ' AddNew()
        Dim obj As clsDBTSummaryYearWise = clsDBTSummaryYearWise.GetData(strCode, NavTyep)
        If obj IsNot Nothing Then
            isNewEntry = False
            txtFinancialYear.Value = obj.Fiscal_Code
            txtFromDate.Value = obj.Start_Date
            txtToDate.Value = obj.End_Date
        End If
    End Sub

    Private Sub txtMCC__My_Click(sender As Object, e As EventArgs) Handles txtMCC._My_Click
        Dim qry As String = "select distinct MCC_Code as Code,MCC_NAME as Name from TSPL_MCC_MASTER"
        txtMCC.arrValueMember = clsCommon.ShowMultipleSelectForm(Me.Form_ID, qry, "Code", "Name", txtMCC.arrValueMember, Nothing)
    End Sub
    Private Sub txtFinancialYear__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFinancialYear._MYValidating
        Dim str As String = "select count(*) from TSPL_Fiscal_Year_Master where Fiscal_Code ='" + txtFinancialYear.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 Then
            txtFinancialYear.MyReadOnly = False
        Else
            txtFinancialYear.MyReadOnly = True
        End If
        If txtFinancialYear.MyReadOnly OrElse isButtonClicked Then
            Dim qry As String = "select Fiscal_Code as Code,Fiscal_Name as Name from TSPL_Fiscal_Year_Master"
            Dim whrClas As String = " Comp_Code='" + objCommonVar.CurrentCompanyCode + "' "

            txtFinancialYear.Value = clsCommon.ShowSelectForm("TSPL_Fiscal_Year_Master", qry, "Code", whrClas, txtFinancialYear.Value, "", isButtonClicked)
            LoadData(txtFinancialYear.Value, NavigatorType.Current)
        End If


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        Me.Close()

    End Sub

    Private Sub frmProjectMaster_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.A Then
            'AddNew()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub
    Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing

        Dim repoMonth As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoMonth.FormatString = ""
        repoMonth.HeaderText = "Month"
        repoMonth.Name = colMonth
        repoMonth.Width = 90
        repoMonth.IsVisible = True
        repoMonth.IsPinned = True
        repoMonth.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMonth)

        Dim repoDbtDCS As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDbtDCS.FormatString = ""
        repoDbtDCS.HeaderText = "DCS"
        repoDbtDCS.Name = colDbtDCS
        repoDbtDCS.TextImageRelation = TextImageRelation.TextBeforeImage
        repoDbtDCS.Width = 80
        repoDbtDCS.IsVisible = True
        repoDbtDCS.IsPinned = True
        repoDbtDCS.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDbtDCS)

        Dim repoFarmer As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFarmer.FormatString = ""
        repoFarmer.HeaderText = "Farmer"
        repoFarmer.Name = colDbtFarmer
        repoFarmer.Width = 80
        repoFarmer.ReadOnly = True
        repoFarmer.IsPinned = True
        repoFarmer.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoFarmer)

        Dim repoDbtQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDbtQty.FormatString = ""
        repoDbtQty.HeaderText = "Qty"
        repoDbtQty.Name = colDbtQty
        repoDbtQty.Width = 105
        repoDbtQty.IsVisible = True
        repoDbtQty.IsPinned = True
        repoDbtQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDbtQty)

        Dim repoDbtAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDbtAmt.FormatString = ""
        repoDbtAmt.HeaderText = "Amt"
        repoDbtAmt.Name = colDbtAmt
        repoDbtAmt.Width = 105
        repoDbtAmt.ReadOnly = True
        repoDbtAmt.IsVisible = True
        repoDbtAmt.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoDbtAmt)

        Dim repoRecoDcs As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRecoDcs.FormatString = ""
        repoRecoDcs.HeaderText = "DCS"
        repoRecoDcs.Name = colRecoDCS
        repoRecoDcs.Width = 80
        repoRecoDcs.IsVisible = True
        repoRecoDcs.IsPinned = True
        repoRecoDcs.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRecoDcs)

        Dim repoRecoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRecoQty.FormatString = ""
        repoRecoQty.HeaderText = "Qty"
        repoRecoQty.Name = colRecoQty
        repoRecoQty.Width = 105
        repoRecoQty.IsVisible = True
        repoRecoQty.IsPinned = True
        repoRecoQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoRecoQty)

        Dim repoRecoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRecoAmt.FormatString = ""
        repoRecoAmt.HeaderText = "Amt"
        repoRecoAmt.Name = colRecoAmt
        repoRecoAmt.Width = 105
        repoRecoAmt.ReadOnly = True
        repoRecoAmt.IsVisible = True
        repoRecoAmt.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoRecoAmt)

        Dim repoDiffDcs As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDiffDcs.FormatString = ""
        repoDiffDcs.HeaderText = "DCS"
        repoDiffDcs.Name = colDiffDcs
        repoDiffDcs.Width = 80
        repoDiffDcs.IsVisible = True
        repoDiffDcs.IsPinned = True
        repoDiffDcs.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDiffDcs)


        Dim repoDiffQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiffQty.FormatString = ""
        repoDiffQty.HeaderText = "Qty"
        repoDiffQty.Name = colDiffQty
        repoDiffQty.Width = 105
        repoDiffQty.IsVisible = True
        repoDiffQty.IsPinned = True
        repoDiffQty.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoDiffQty)

        Dim repoDiffAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoDiffAmt.FormatString = ""
        repoDiffAmt.HeaderText = "Amt"
        repoDiffAmt.Name = colDiffAmt
        repoDiffAmt.Width = 105
        repoDiffAmt.ReadOnly = True
        repoDiffAmt.IsVisible = True
        repoDiffAmt.IsPinned = True
        gv1.MasterTemplate.Columns.Add(repoDiffAmt)

        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        View()
    End Sub

    Sub View()
        Try

            If gv1.Columns.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup(""))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colMonth).Name)
                view.ColumnGroups(0).IsPinned = True

                view.ColumnGroups.Add(New GridViewColumnGroup("DBT Entry"))
                view.ColumnGroups(1).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colDbtDCS).Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colDbtFarmer).Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colDbtQty).Name)
                view.ColumnGroups(1).Rows(0).ColumnNames.Add(gv1.Columns(colDbtAmt).Name)
                view.ColumnGroups(1).IsPinned = True

                view.ColumnGroups.Add(New GridViewColumnGroup("Reco"))
                view.ColumnGroups(2).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colRecoDCS).Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colRecoQty).Name)
                view.ColumnGroups(2).Rows(0).ColumnNames.Add(gv1.Columns(colRecoAmt).Name)
                view.ColumnGroups(2).IsPinned = True

                view.ColumnGroups.Add(New GridViewColumnGroup("Difference"))
                view.ColumnGroups(3).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffDcs).Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffQty).Name)
                view.ColumnGroups(3).Rows(0).ColumnNames.Add(gv1.Columns(colDiffAmt).Name)
                view.ColumnGroups(3).IsPinned = True
                gv1.ViewDefinition = view
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        funreset()
    End Sub

    Sub funreset()
        LoadBlankGrid()
        txtMCC.arrValueMember = Nothing
        txtFinancialYear.Value = ""
        txtToDate.Value = clsCommon.GETSERVERDATE()
        txtFromDate.Value = clsCommon.GETSERVERDATE()
        RadPageView1.SelectedPage = RadPageViewPage1
    End Sub
    Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
        If clsCommon.myLen(txtFinancialYear.Value) = 0 Then
            clsCommon.MyMessageBoxShow(Me, "Financial Year cannot be Blank", Me.Text)
            Exit Sub
        End If
        setGridData()
    End Sub
    Sub setGridData()
        Try
            gv1.DataSource = Nothing
            gv1.Rows.Clear()
            gv1.Columns.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterView.Refresh()
            LoadBlankGrid()
            Dim startDate As New DateTime(txtFromDate.Value.Year, txtFromDate.Value.Month, 1)
            Dim endDate As New DateTime(txtToDate.Value.Year, txtToDate.Value.Month, 1)
            Dim arrMonth As New List(Of String)
            Dim currentMonth As DateTime = startDate
            While currentMonth <= endDate
                Dim monthName As String = currentMonth.ToString("MMMM")
                arrMonth.Add(monthName)
                currentMonth = currentMonth.AddMonths(1)
            End While
            Dim arrMccCode As New List(Of String)
            Dim MccCode As String  = ""
            If txtMCC.arrValueMember IsNot Nothing Then
                For i = 0 To txtMCC.arrValueMember.Count - 1
                    arrMccCode.Add(txtMCC.arrValueMember(0))
                Next
            End If
            MccCode = clsCommon.GetMulcallString(arrMccCode)

            Dim DbtQry As String = "Select DateName(month, DateAdd(month, TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Month, 0) - 1)as Dbt_Month ,TSPL_MP_INCENTIVE_ENTRY_DETAIL.Cycle_Year as Dbt_Year, sum(TSPL_MP_INCENTIVE_ENTRY_DETAIL.Amount) as Dbt_Amt,
        Sum(TSPL_MP_INCENTIVE_ENTRY_DETAIL.Qty) as Dbt_Qty  ,count(distinct TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as Dbt_DCS,count(distinct TSPL_MP_MASTER.MP_Code_VLC_Uploader) as Dbt_Farmer_Code from TSPL_MP_INCENTIVE_ENTRY_DETAIL 
        left outer join TSPL_MP_INCENTIVE_ENTRY_HEAD on TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Code = TSPL_MP_INCENTIVE_ENTRY_DETAIL.Document_Code
        Left Outer Join TSPL_MP_MASTER On TSPL_MP_MASTER.MP_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.MP_Code   
        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_MP_INCENTIVE_ENTRY_DETAIL.VLC_Code
        left outer join  TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_MP_INCENTIVE_ENTRY_HEAD.MCC_Code
        where  convert(date,TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Date,103) >=convert(date,'" & txtFromDate.Text & "',103) and convert(date,TSPL_MP_INCENTIVE_ENTRY_HEAD.Document_Date,103) <=convert(date,'" & txtToDate.Text & "',103)"
            If arrMccCode.Count > 0 Then
                DbtQry += "and TSPL_MCC_MASTER.MCC_Code in (" & MccCode & ")"
            End If

            DbtQry += "group by Cycle_Month ,Cycle_Year order by Cycle_Year , Cycle_Month"

            Dim dtDbt As DataTable = clsDBFuncationality.GetDataTable(DbtQry)

            Dim RecoQry As String = "Select DateName(month, DateAdd(month, TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Cycle_Month, 0) - 1)as Reco_Month ,TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Cycle_Year as Reco_Year , sum(TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.MP_Amount) as Reco_Amt,
        Sum(TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Qty) as Reco_Qty  ,count(distinct TSPL_VLC_MASTER_HEAD.VLC_Code_VLC_Uploader) as Reco_DCS from TSPL_DCS_MP_INCENTIVE_RECO_DETAIL 
        left outer join TSPL_DCS_MP_INCENTIVE_RECO_HEAD on TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Code = TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.Document_Code
        left outer join TSPL_VLC_MASTER_HEAD on TSPL_VLC_MASTER_HEAD.VLC_Code=TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.VLC_Code
        left outer join  TSPL_MCC_MASTER on TSPL_MCC_MASTER.MCC_Code = TSPL_DCS_MP_INCENTIVE_RECO_DETAIL.MCC_Code
        where  convert(date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Date,103) >=convert(date,'" & txtFromDate.Text & "',103) and convert(date,TSPL_DCS_MP_INCENTIVE_RECO_HEAD.Document_Date,103) <=convert(date,'" & txtToDate.Text & "',103)"
            If arrMccCode.Count > 0 Then
                RecoQry += "and TSPL_MCC_MASTER.MCC_Code in (" & MccCode & ")"
            End If
            RecoQry += "group by Cycle_Month, Cycle_Year order by Cycle_Year , Cycle_Month"

            Dim dtReco As DataTable = clsDBFuncationality.GetDataTable(RecoQry)

            For i As Integer = 0 To arrMonth.Count - 1
                gv1.Rows.AddNew()
                If dtDbt.Rows.Count > 0 Then
                    For jj As Integer = 0 To dtDbt.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(dtDbt.Rows(jj)("Dbt_Month")), clsCommon.myCstr(arrMonth(i))) = CompairStringResult.Equal Then
                            gv1.Rows(i).Cells(colMonth).Value = clsCommon.myCstr(dtDbt.Rows(jj)("Dbt_Month"))
                            gv1.Rows(i).Cells(colDbtAmt).Value = clsCommon.myCstr(dtDbt.Rows(jj)("Dbt_Amt"))
                            gv1.Rows(i).Cells(colDbtQty).Value = clsCommon.myCstr(dtDbt.Rows(jj)("Dbt_Qty"))
                            gv1.Rows(i).Cells(colDbtDCS).Value = clsCommon.myCstr(dtDbt.Rows(jj)("Dbt_DCS"))
                            gv1.Rows(i).Cells(colDbtFarmer).Value = clsCommon.myCstr(dtDbt.Rows(jj)("Dbt_Farmer_Code"))
                            Exit For
                        Else
                            gv1.Rows(i).Cells(colMonth).Value = clsCommon.myCstr(arrMonth(i).ToString())
                            gv1.Rows(i).Cells(colDbtAmt).Value = "0.00"
                            gv1.Rows(i).Cells(colDbtQty).Value = "0.00"
                            gv1.Rows(i).Cells(colDbtDCS).Value = 0
                            gv1.Rows(i).Cells(colDbtFarmer).Value = 0
                        End If

                    Next
                Else
                    gv1.Rows(i).Cells(colMonth).Value = clsCommon.myCstr(arrMonth(i).ToString())
                    gv1.Rows(i).Cells(colDbtAmt).Value = "0.00"
                    gv1.Rows(i).Cells(colDbtQty).Value = "0.00"
                    gv1.Rows(i).Cells(colDbtDCS).Value = 0
                    gv1.Rows(i).Cells(colDbtFarmer).Value = 0

                End If

                If dtReco.Rows.Count > 0 Then
                    For kk As Integer = 0 To dtReco.Rows.Count - 1
                        If clsCommon.CompairString(clsCommon.myCstr(dtReco.Rows(kk)("Reco_Month")), clsCommon.myCstr(arrMonth(i))) = CompairStringResult.Equal Then
                            gv1.Rows(i).Cells(colRecoAmt).Value = clsCommon.myCstr(dtReco.Rows(kk)("Reco_Amt"))
                            gv1.Rows(i).Cells(colRecoQty).Value = clsCommon.myCstr(dtReco.Rows(kk)("Reco_Qty"))
                            gv1.Rows(i).Cells(colRecoDCS).Value = clsCommon.myCstr(dtReco.Rows(kk)("Reco_DCS"))
                            Exit For

                        Else
                            gv1.Rows(i).Cells(colRecoAmt).Value = "0.00"
                            gv1.Rows(i).Cells(colRecoQty).Value = "0.00"
                            gv1.Rows(i).Cells(colRecoDCS).Value = 0
                        End If
                    Next
                Else
                    gv1.Rows(i).Cells(colRecoAmt).Value = "0.00"
                    gv1.Rows(i).Cells(colRecoQty).Value = "0.00"
                    gv1.Rows(i).Cells(colRecoDCS).Value = 0

                End If
                gv1.Rows(i).Cells(colDiffDcs).Value = gv1.Rows(i).Cells(colRecoDCS).Value - gv1.Rows(i).Cells(colDbtDCS).Value
                gv1.Rows(i).Cells(colDiffAmt).Value = gv1.Rows(i).Cells(colRecoAmt).Value - gv1.Rows(i).Cells(colDbtAmt).Value
                gv1.Rows(i).Cells(colDiffQty).Value = gv1.Rows(i).Cells(colRecoQty).Value - gv1.Rows(i).Cells(colDbtQty).Value
            Next


            SetGridFormat()
            RadPageView1.SelectedPage = RadPageViewPage2
            ReStoreGridLayout()

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
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
    Sub SetGridFormat()
        For ii As Integer = 1 To gv1.Columns.Count - 1
            gv1.Columns(ii).BestFit()
            gv1.Columns(ii).TextAlignment = System.Drawing.ContentAlignment.MiddleRight

        Next
        gv1.GroupDescriptors.Clear()
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        gv1.MasterTemplate.BestFitColumns()
        gv1.EnableFiltering = True

        gv1.Columns(colMonth).Width = 90
        gv1.Columns(colDbtDCS).Width = 80
        gv1.Columns(colDbtFarmer).Width = 80
        gv1.Columns(colDbtAmt).Width = 105
        gv1.Columns(colDbtQty).Width = 105
        gv1.Columns(colRecoDCS).Width = 80
        gv1.Columns(colRecoAmt).Width = 105
        gv1.Columns(colRecoQty).Width = 105
        gv1.Columns(colDiffDcs).Width = 80
        gv1.Columns(colDiffAmt).Width = 105
        gv1.Columns(colDiffQty).Width = 105

        Dim summaryRowItem As New GridViewSummaryRowItem()
        Dim Dbt_DCS As New GridViewSummaryItem("colDbtDCS", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Dbt_DCS)
        Dim Dbt_Farmer_Code As New GridViewSummaryItem("colDbtFarmer", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Dbt_Farmer_Code)
        Dim Dbt_Amt As New GridViewSummaryItem("colDbtAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Dbt_Amt)
        Dim Dbt_Qty As New GridViewSummaryItem("colDbtQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Dbt_Qty)

        Dim Reco_DCS As New GridViewSummaryItem("colRecoDCS", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Reco_DCS)
        Dim Reco_Amt As New GridViewSummaryItem("colRecoAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Reco_Amt)
        Dim Reco_Qty As New GridViewSummaryItem("colRecoQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Reco_Qty)

        Dim Diff_DCS As New GridViewSummaryItem("colDiffDcs", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Diff_DCS)
        Dim Diff_Amt As New GridViewSummaryItem("colDiffAmt", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Diff_Amt)
        Dim Diff_Qty As New GridViewSummaryItem("colDiffQty", "{0:F2}", GridAggregateFunction.Sum)
        summaryRowItem.Add(Diff_Qty)

        gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        gv1.MasterView.SummaryRows(0).PinPosition = PinnedRowPosition.Bottom
        gv1.MasterTemplate.AutoExpandGroups = True
        gv1.AutoSizeRows = True
    End Sub
End Class