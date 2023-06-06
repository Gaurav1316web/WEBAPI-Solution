Imports common
Imports System.Threading
Imports Telerik.WinControls.UI.Export
Imports Telerik.WinControls.UI.Export.ExcelML
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.ComponentModel

Public Class frmFATSNFGainLoss
    Inherits FrmMainTranScreen
#Region "Varaibels"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public isDataLoad As Boolean = False
    Public dtFrom As Date
    Public dtTo As Date
    Public strType As String
    Public arrLoc As Dictionary(Of String, Object) = Nothing
    Public arrItemGroup As ArrayList
    Public arrItem As ArrayList
    Public arrTransaction As ArrayList
    Public arrCat As Dictionary(Of String, Object) = Nothing
    Public Unit_Code As String = Nothing
    Public MRP_Wise As Boolean = False
    Dim dtCategory As DataTable
    Dim MIS_Item_Group As String
    Public arrItemType As ArrayList
    Dim arrBack As List(Of String)
    Dim isInsideLoadData As Boolean = False
    Dim FORMTYPE As String = Nothing

    Public objFilter As New clsStockRecoFilters
    Public _DS As New DataSet()
    Private m_FieldsToDisplay As String()
#End Region

    Public Property FieldsToDisplay() As String()
        Get
            Return m_FieldsToDisplay
        End Get
        Set(value As String())
            m_FieldsToDisplay = value
        End Set
    End Property

    Public ReadOnly Property ValueMember() As String
        Get
            Return "Trans_ID"
        End Get
    End Property

    <DefaultValue(False)> _
    Public ReadOnly Property ShowValueMember() As Boolean
        Get
            Return False
        End Get
    End Property

    Public Sub New(ByVal formid As String)
        InitializeComponent()
        FORMTYPE = formid
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmKPIReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''TEC/23/07/19-000952 by balwinder on 23/07/2019
        txtFromDate.Value = New DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)
        txtToDate.Value = clsCommon.GETSERVERDATE
        arrBack = New List(Of String)
        SetUserMgmtNew()
        LoadReportFATSNFType()
        LoadCategory()
        LoadUnit()
        LoadLocation()
        LoadType()
        ButtonToolTip.SetToolTip(RadButton2, "Press Alt+C Close the Window")
        RadPageView1.SelectedPage = RadPageViewPage1
        rbtnCategoryAll.IsChecked = True
        rbtnLocationAll.IsChecked = True
        cboType.SelectedValue = "Main Location Wise"
        btnPrint.Visible = False
        GetMIS_ITem_GroupColumn()

        vsb.Visible = False


        If isDataLoad Then
            txtFromDate.Value = dtFrom
            txtToDate.Value = dtTo
            cmbUnit.SelectedValue = Unit_Code
            cboFATSNF.SelectedValue = "B"
            txtItem.arrValueMember = arrItem
            txtTransaction.arrValueMember = arrTransaction
            txtItemGroup.arrValueMember = arrItemGroup
            If arrLoc IsNot Nothing AndAlso arrLoc.Count > 0 Then
                rbtnLocationSelect.IsChecked = True
                For Each str As String In arrLoc.Keys
                    For ii As Integer = 0 To gvLocation.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvLocation.Rows(ii).Cells("SEL").Value = True
                            gvLocation.Rows(ii).Tag = arrLoc(str)
                        End If
                    Next
                Next
            End If
            If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                rbtnCategorySelect.IsChecked = True
                For Each str As String In arrCat.Keys
                    For ii As Integer = 0 To gvCategory.RowCount - 1
                        If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                            gvCategory.Rows(ii).Cells("SEL").Value = True
                            gvCategory.Rows(ii).Tag = arrCat(str)
                        End If
                    Next
                Next
            End If
            cboType.SelectedValue = strType
            LoadData()
        End If

        If clsCommon.CompairString(FORMTYPE, "MIS-STLE-RPT") = CompairStringResult.Equal Then
            btnPrint.Visible = True
            cboType.Visible = False
            lblModeofTransport.Visible = False
            cboType.SelectedValue = "Document Wise Detail Ledger"
        End If
    End Sub

    Sub LoadLocation()
        gvLocation.DataSource = Nothing
        Dim whrCls As String = " and ((Is_Section='N' and Is_Sub_Location='N' and Location_Type IN ('Physical','Logical','Virtual') ) or (CSA_Type='Y') )"
        Dim qry As String = " select cast( 0 as bit) as SEL,Location_Code as CODE,Location_Desc as NAME from TSPL_LOCATION_MASTER where 1=1 " + whrCls + ""
        qry += " and  TSPL_LOCATION_MASTER.GIT_Type<>'Y' "
        qry += " order by Location_Code"
        gvLocation.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvLocation.Columns("SEL").ReadOnly = False
        gvLocation.Columns("SEL").Width = 30
        gvLocation.Columns("SEL").HeaderText = " "

        gvLocation.Columns("CODE").ReadOnly = True
        gvLocation.Columns("CODE").Width = 100
        gvLocation.Columns("CODE").HeaderText = "Code"

        gvLocation.Columns("NAME").ReadOnly = True
        gvLocation.Columns("NAME").Width = 200
        gvLocation.Columns("NAME").HeaderText = "Description"

        gvLocation.ShowGroupPanel = False
        gvLocation.AllowAddNewRow = False
        gvLocation.AllowColumnReorder = False
        gvLocation.AllowRowReorder = False
        gvLocation.EnableSorting = False
        gvLocation.ShowFilteringRow = True
        gvLocation.EnableFiltering = True
        gvLocation.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvLocation.MasterTemplate.ShowRowHeaderColumn = True
    End Sub

    Sub LoadReportFATSNFType()
        isInsideLoadData = True
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))
        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "B"
        dr("Name") = "Both"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MI"
        dr("Name") = "Milk"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "MP"
        dr("Name") = "Milk Product"
        dt.Rows.Add(dr)

        cboFATSNF.DataSource = dt
        cboFATSNF.ValueMember = "Code"
        cboFATSNF.DisplayMember = "Name"

        isInsideLoadData = False
    End Sub

    Sub LoadType()
        isInsideLoadData = True
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        Dim dr As DataRow = dt.NewRow()

        dr = dt.NewRow()
        dr("Code") = "Main Location Wise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Main Location and Location Wise"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Item And Location Wise"
        'dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "Document Wise"
        'dt.Rows.Add(dr)

        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Code"

        isInsideLoadData = False
    End Sub

    Sub LoadCategory()
        dtCategory = clsDBFuncationality.GetDataTable("select ITEM_CATEGORY_CODE AS CodeColumn,ITEM_CATEGORY_CODE+'DESC' as CodeDescColumn,DESCRIPTION as DescColumn  from TSPL_ITEM_CATEGORY_LEVEL order by CATEGORY_LEVEL")

        gvCategory.DataSource = Nothing
        Dim qry As String = "select cast( 0 as bit) as SEL,ITEM_CATEGORY_CODE AS Code,DESCRIPTION as NAME from TSPL_ITEM_CATEGORY_LEVEL ORDER BY CATEGORY_LEVEL"
        gvCategory.DataSource = clsDBFuncationality.GetDataTable(qry)

        gvCategory.Columns("SEL").ReadOnly = False
        gvCategory.Columns("SEL").Width = 30
        gvCategory.Columns("SEL").HeaderText = " "

        gvCategory.Columns("CODE").ReadOnly = True
        gvCategory.Columns("CODE").Width = 100
        gvCategory.Columns("CODE").HeaderText = "Code"

        gvCategory.Columns("NAME").ReadOnly = True
        gvCategory.Columns("NAME").Width = 200
        gvCategory.Columns("NAME").HeaderText = "Description"

        gvCategory.ShowGroupPanel = False
        gvCategory.AllowAddNewRow = False
        gvCategory.AllowColumnReorder = False
        gvCategory.AllowRowReorder = False
        gvCategory.EnableSorting = False
        gvCategory.ShowFilteringRow = True
        gvCategory.EnableFiltering = True
        gvCategory.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvCategory.MasterTemplate.ShowRowHeaderColumn = True

    End Sub

    Sub LoadUnit()
        Dim qry As String = "select Unit_Code as Code,Unit_Desc as Description from TSPL_UNIT_MASTER"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Dim dr As DataRow = dt.NewRow
        dr("Code") = ""
        dr("Description") = "Select"
        dt.Rows.InsertAt(dr, 0)
        cmbUnit.DataSource = Nothing
        cmbUnit.DataSource = dt
        cmbUnit.DisplayMember = "Description"
        cmbUnit.ValueMember = "Code"
    End Sub

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnQuickExport.Visible = MyBase.isExport
        btnPrint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmKPIReport_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag Then

        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isPostFlag Then
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            Close()
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton2.Click
        Me.Close()
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        Try
            gv1.EnableFiltering = True
            LoadData()
            TemplateGridview = gv1
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub SetGridDisplay(ByVal ds As DataSet, ByVal objFilter As clsStockRecoFilters)
        Dim ftd As New List(Of String)

        gv1.Rows.Clear()

        If Me.FieldsToDisplay IsNot Nothing AndAlso clsCommon.myLen(Me.FieldsToDisplay) > 0 Then
            ftd.AddRange(Me.FieldsToDisplay)
        Else
            Dim c As DataColumn
            For Each c In ds.Tables("_lookup_result").Columns
                If c.ColumnName = "_lookup_row_number" Then
                    Continue For
                End If
                ftd.Add(c.ColumnName)
            Next
        End If

        Dim s As String
        For Each s In ftd
            If s = Me.ValueMember AndAlso Not ShowValueMember Then
                Continue For
            End If

            Dim columnName As String = "ux" + s.Replace(" ", "")

            For Each dgc As GridViewColumn In gv1.Columns
                If dgc.Name = s Then
                    GoTo goAlreadyAdded
                End If
            Next

            gv1.Columns.Add(columnName)
            Dim c As GridViewColumn = gv1.Columns(columnName)
            c.Name = s
            c.IsVisible = True

goAlreadyAdded:
        Next

    End Sub

    Private Sub AdjustRowCount(ByVal ds As DataSet, ByVal objFilter As clsStockRecoFilters)
        Try
            If ds Is Nothing OrElse ds.Tables("_lookup_result") Is Nothing OrElse ds.Tables("_lookup_result").Rows.Count <= 0 Then
                Exit Sub
            End If
            Dim viewableRows As Long = CType(Math.Floor(CType((gv1.Height - gv1.TableElement.TableHeaderHeight) / gv1.TableElement.TableHeaderHeight, Decimal)), Long)

            If viewableRows <= 0 Then
                Exit Sub
            End If

            If ds IsNot Nothing AndAlso ds.Tables("_lookup_result") IsNot Nothing AndAlso ds.Tables("_lookup_result").Rows.Count > 0 Then
                Dim resultCount As Long = CType(ds.Tables("_lookup_result").Rows.Count, Long)
                If resultCount < viewableRows Then
                    viewableRows = resultCount
                End If
            End If

            If viewableRows <> gv1.Rows.Count Then
                gv1.Rows.Clear()
                If viewableRows > 0 Then
                    For ii As Integer = 0 To viewableRows - 1
                        gv1.Rows.AddNew()
                    Next
                    'gv1.Rows.Add(TryCast(viewableRows, Object))
                End If

                vsb.LargeChange = CType(viewableRows, Integer)

                vsb.Minimum = 0
                vsb.Maximum = CType(ds.Tables("_lookup_count").Rows(0)("_count") - 1, Integer)

                For Each r As GridViewRowInfo In gv1.Rows
                    r.Height = gv1.TableElement.TableHeaderHeight
                Next
            End If

            DisplayValues(ds, objFilter)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub DisplayValues(ByVal ds As DataSet, ByVal objFilter As clsStockRecoFilters)
        Try
            For r As Integer = 0 To gv1.Rows.Count - 1
                Dim absRow As Integer = r + vsb.Value

                Dim drx() As DataRow = ds.Tables("_lookup_result").Select(String.Format("_lookup_row_number = {0}", absRow))

                If absRow = CType(ds.Tables("_lookup_count").Rows(0)("_count") - 1, Integer) Then
                    MessageBox.Show("End Here")
                    Exit Sub
                End If

                If drx.Length = 0 Then
                    ds = clsInventoryMovement.GetDataSet_ForPartiallyLoad(absRow, objFilter)
                    drx = ds.Tables("_lookup_result").Select(String.Format("_lookup_row_number = {0}", absRow))
                End If


                If drx.Length = 1 Then
                    For Each c As GridViewColumn In gv1.Columns
                        gv1.Rows(r).Cells(c.Name).Value = drx(0)(c.Name) ''or c.fieldname
                        gv1.Columns(c.Name).Width = 80
                    Next
                Else
                    For Each c As GridViewColumn In gv1.Columns
                        gv1.Rows(r).Cells(c.Name).Value = Nothing
                    Next
                End If

            Next
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub gv1_Resize(sender As Object, e As EventArgs) Handles gv1.Resize
        AdjustRowCount(_DS, objFilter)
    End Sub

    Private Sub gv1_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles gv1.KeyDown
        Try
            If gv1.CurrentCell Is Nothing Then
                Exit Sub
            End If

            If e.KeyCode = Keys.Down Then
                If gv1.CurrentCell.RowIndex = gv1.Rows.Count - 1 Then
                    If vsb.Value <= vsb.Maximum - vsb.LargeChange Then
                        e.Handled = True
                        vsb.Value = vsb.Value + 1
                    End If
                End If

            ElseIf e.KeyCode = Keys.Up Then
                If gv1.CurrentCell.RowIndex = 0 Then
                    If vsb.Value > 0 Then
                        e.Handled = True
                        vsb.Value = vsb.Value - 1
                    End If
                End If

            ElseIf e.KeyCode = Keys.PageDown Then
                If gv1.CurrentCell.RowIndex = gv1.Rows.Count - 1 Then
                    If Not (vsb.Value + vsb.LargeChange > vsb.Maximum) Then
                        Dim NewValue As Integer = vsb.Value + vsb.LargeChange

                        If vsb.Maximum - NewValue > vsb.LargeChange Then
                            vsb.Value = NewValue
                        Else
                            vsb.Value = vsb.Maximum - vsb.LargeChange + 1
                        End If
                    End If


                End If
            ElseIf e.KeyCode = Keys.PageUp Then
                If gv1.CurrentCell.RowIndex = 0 Then
                    If Not (vsb.Value - vsb.LargeChange < 0) Then
                        vsb.Value -= vsb.LargeChange
                    Else
                        vsb.Value = 0
                    End If
                End If
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gv1_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles gv1.MouseWheel
        Try
            If e.Delta < 0 Then
                Dim rowCount As Long = CType(_DS.Tables("_lookup_count").Rows(0)("_count"), Long)

                If vsb.Value < rowCount - vsb.LargeChange Then
                    vsb.Value = ++vsb.Value
                End If
            ElseIf e.Delta > 0 Then
                If vsb.Value > 0 Then
                    vsb.Value = --vsb.Value
                End If
            End If
        Catch ex As Exception
            'clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub vsb_ValueChanged(sender As Object, e As EventArgs) Handles vsb.ValueChanged
        DisplayValues(_DS, objFilter)
    End Sub

    Private Sub LoadData()
        Dim ArrInnerOpClo As New Dictionary(Of String, String)
        Try
            gv1.DataSource = Nothing
            gv1.Columns.Clear()
            gv1.Rows.Clear()
            gv1.GroupDescriptors.Clear()
            gv1.MasterTemplate.SummaryRowsBottom.Clear()
            gv1.ShowGroupPanel = False
            gv1.EnableFiltering = True
            gv1.AllowAddNewRow = False
            If clsCommon.GetDateWithEndTime(txtToDate.Value) < clsCommon.GetDateWithStartTime(txtFromDate.Value) Then
                Throw New Exception("To Date cant be less than from date")
            End If
            clsCommon.ProgressBarShow()
            Dim OuterOpClo As String = ""

            Dim qry As String = GetBaseQuery(OuterOpClo, ArrInnerOpClo)
            Dim strFinalQry As String = ""
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Main Location Wise") = CompairStringResult.Equal Then
                strFinalQry = "select Main_Location_Code,MainLocationDesc,"
                strFinalQry += OuterOpClo
                strFinalQry += "  from (" + Environment.NewLine
                strFinalQry += " select Main_Location_Code,max(MainLocationDesc) as MainLocationDesc "
                For Each key As String In ArrInnerOpClo.Keys
                    strFinalQry += Environment.NewLine + ",Sum" + ArrInnerOpClo.Item(key) + " as " + key
                Next
                strFinalQry += " from (" + qry + ")xxx Group by Main_Location_Code"
                strFinalQry += " )xxxx" + Environment.NewLine
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Main Location and Location Wise") = CompairStringResult.Equal Then
                strFinalQry = "select Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp],"
                strFinalQry += OuterOpClo
                strFinalQry += "  from (" + Environment.NewLine
                strFinalQry += " select Main_Location_Code,max(MainLocationDesc) as MainLocationDesc, Location_Code,max([Loc Desp]) as [Loc Desp]"
                For Each key As String In ArrInnerOpClo.Keys
                    strFinalQry += Environment.NewLine + ",Sum" + ArrInnerOpClo.Item(key) + " as " + key
                Next
                strFinalQry += " from (" + qry + ")xxx Group by Main_Location_Code,Location_Code"
                strFinalQry += " )xxxx" + Environment.NewLine
            End If

            RadPageViewPage2.Text = "Report ( " + clsCommon.myCstr(cboType.SelectedValue) + " )"
            clsCommon.ProgressBarUpdate("Loading Data.Please Wait...")

            Dim dtr As System.Data.SqlClient.SqlDataReader = Nothing
            Try
                dtr = clsDBFuncationality.GetDataReader(strFinalQry, Nothing)
                If Not dtr.HasRows Then
                    Throw New Exception("No Data Found to Display")
                End If
                If dtr.HasRows Then
                    gv1.MasterTemplate.LoadFrom(dtr)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                dtr.Close()
            End Try

            EnableDisableCtrl(False)
            SetGridFormationOFGV1()
            RadPageView1.SelectedPage = RadPageViewPage2
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarHide()
            ArrInnerOpClo = Nothing
        End Try
    End Sub

    Private Sub gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gv1.CellDoubleClick
        Dim ArrInnerOpClo As New Dictionary(Of String, String)
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Main Location Wise") = CompairStringResult.Equal _
                OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Main Location and Location Wise") = CompairStringResult.Equal Then
                If clsCommon.myCdbl(gv1.CurrentCell.Value) <> 0 Then
                    Dim OuterOpClo As String = ""
                    Dim qry As String = GetBaseQuery(OuterOpClo, ArrInnerOpClo)
                    For Each key As String In ArrInnerOpClo.Keys
                        If clsCommon.CompairString(clsCommon.myCstr(gv1.Columns(gv1.CurrentCell.ColumnIndex).Name), key) = CompairStringResult.Equal Then
                            qry = "select Trans_Id,Trans_Type,Trans_Type_Name,Source_Doc_No,Punching_Date,Main_Location_Code,MainLocationDesc,Location_Code,[Loc Desp],Item_Code,Item_Desc,Stock_Qty,Stock_UOM,FatPer,convert(decimal(18,2),(case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end))*(case when (" + ArrInnerOpClo.Item(key) + ")<0 then -1 else 1 end ) as FAT_KG,SNFPer,convert(decimal(18,2),(case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end))*(case when (" + ArrInnerOpClo.Item(key) + ")<0 then -1 else 1 end ) as SNFKG from (" + Environment.NewLine + qry + Environment.NewLine + _
                            ")xxxxx Where " + ArrInnerOpClo.Item(key) + "<>0 and Main_Location_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Main_Location_Code").Value) + "' "
                            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Main Location and Location Wise") = CompairStringResult.Equal Then
                                qry += " and Location_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells("Location_Code").Value) + "'"
                            End If

                            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            gvDetail.DataSource = Nothing
                            gvDetail.DataSource = dt
                            gvDetail.BestFitColumns()
                            gvDetail.MasterTemplate.SummaryRowsBottom.Clear()
                            gvDetail.GroupDescriptors.Clear()
                            gvDetail.ReadOnly = True
                            gvDetail.EnableFiltering = True
                            gvDetail.ShowGroupPanel = False

                            gvDetail.Columns("FatPer").IsVisible = True
                            gvDetail.Columns("FatPer").Width = 100
                            gvDetail.Columns("FatPer").HeaderText = "FAT %"
                            gvDetail.Columns("FatPer").FormatString = "{0:n2}"

                            gvDetail.Columns("FAT_KG").IsVisible = True
                            gvDetail.Columns("FAT_KG").Width = 100
                            gvDetail.Columns("FAT_KG").HeaderText = "FAT KG"
                            gvDetail.Columns("FAT_KG").FormatString = "{0:n2}"

                            gvDetail.Columns("SNFPer").IsVisible = True
                            gvDetail.Columns("SNFPer").Width = 100
                            gvDetail.Columns("SNFPer").HeaderText = "SNF %"
                            gvDetail.Columns("SNFPer").FormatString = "{0:n2}"

                            gvDetail.Columns("SNFKG").IsVisible = True
                            gvDetail.Columns("SNFKG").Width = 100
                            gvDetail.Columns("SNFKG").HeaderText = "SNF KG"
                            gvDetail.Columns("SNFKG").FormatString = "{0:n2}"

                            Dim summaryRowItem As New GridViewSummaryRowItem()
                            Dim Smitem As New GridViewSummaryItem("FAT_KG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Smitem)
                           
                            Smitem = New GridViewSummaryItem("SNFKG", "{0:F2}", GridAggregateFunction.Sum)
                            summaryRowItem.Add(Smitem)
                            gvDetail.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)

                            RadPageView1.SelectedPage = RadPageView1.Pages(2)
                            gvDetail.BestFitColumns()
                            Exit For
                        End If
                    Next
                End If
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        Finally
            ArrInnerOpClo = Nothing
        End Try
    End Sub

    Function GetBaseQuery(ByRef OuterOpClo As String, ByRef ArrInnerOpClo As Dictionary(Of String, String)) As String
        Dim strCodeColumn As String = ""
        Dim strCodeColumnMax As String = ""
        Dim strCodeDescColumn As String = ""
        Dim strCodeDescColumnMax As String = ""

        Dim strCodeColumnSelect As String = ""
        Dim strCodeDescColumnSelect As String = ""

        Dim strCodeColumnNull As String = ""
        Dim strCodeDescColumnNull As String = ""

        Dim strCategoryTable As String = ""
        If dtCategory IsNot Nothing AndAlso dtCategory.Rows.Count > 0 Then
            For ii As Integer = 0 To dtCategory.Rows.Count - 1
                If ii <> 0 Then
                    strCodeColumn += ","
                    strCodeColumnMax += ","
                    strCodeDescColumn += ","
                    strCodeDescColumnMax += ","

                    strCodeColumnSelect += ","
                    strCodeDescColumnSelect += ","

                    strCodeColumnNull += ","
                    strCodeDescColumnNull += ","
                End If
                strCodeColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumn += "[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
                strCodeDescColumnMax += "max([" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]) as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")).Trim() + "]"

                strCodeColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumnSelect += "VirtualCategoryTabel.[" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"

                strCodeColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeColumn")).Trim() + "]"
                strCodeDescColumnNull += "null as [" + clsCommon.myCstr(dtCategory.Rows(ii)("CodeDescColumn")) + "]"
            Next
            strCategoryTable = "select Item_Code," + strCodeColumnMax + "," + strCodeDescColumnMax + "  from (" + Environment.NewLine & _
            " select * from ( " + Environment.NewLine & _
            " select TSPL_ITEM_MASTER.Item_Code,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code+'DESC' as Item_Category_CodeDesc " + Environment.NewLine & _
            " ,TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values " + Environment.NewLine & _
            " ,TSPL_ITEM_CATEGORY_LEVEL_VALUES.DESCRIPTION as Category_Value_Desc " + Environment.NewLine & _
            " from  TSPL_ITEM_MASTER  " + Environment.NewLine & _
            " left outer join TSPL_ITEM_MASTER_CATEGORY on  TSPL_ITEM_MASTER_CATEGORY.Item_code = TSPL_ITEM_MASTER.Item_code " + Environment.NewLine & _
            " left outer join TSPL_ITEM_CATEGORY_LEVEL_VALUES on TSPL_ITEM_CATEGORY_LEVEL_VALUES.ITEM_CATEGORY_CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Category_Code and TSPL_ITEM_CATEGORY_LEVEL_VALUES.CODE=TSPL_ITEM_MASTER_CATEGORY.Item_Cagetory_Values" + Environment.NewLine & _
            " where 2=2 " + Environment.NewLine & _
            " )xx" + Environment.NewLine & _
            " Pivot " + Environment.NewLine & _
            " ( max(Item_Cagetory_Values) for Item_Category_Code   in ( " + strCodeColumn + ")" + Environment.NewLine & _
            " ) Pivt" + Environment.NewLine & _
            " Pivot " + Environment.NewLine & _
            " (" + Environment.NewLine & _
            " max(Category_Value_Desc) for Item_Category_CodeDesc in (" + strCodeDescColumn + ")" + Environment.NewLine & _
            " ) Pivt1 " + Environment.NewLine & _
            " ) xxx group by Item_Code "
        End If
        Dim strItemGroup As String = ""
        strItemGroup = " select Struct.Structure_Code,Structure_Descq,Struct_Val.Value as Item_Group,StructDtl.Description as Group_Description from TSPL_STRUCTURE_MASTER Struct left join (" & _
                       " select Custom_field_Code,Transaction_code,Value from TSPL_CUSTOM_FIELD_VALUES where Program_Code='" & clsUserMgtCode.itemStructure & "'  " & _
                       " and Custom_Field_Code='" & MIS_Item_Group & "') as Struct_Val  on Struct.Structure_Code=Struct_Val.Transaction_Code" & _
                       " left join (select Custom_Field_Code,SNo,Value,Description from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "') as StructDtl on Struct_Val.Value=StructDtl.Value "


        Dim qry As String = "select * from ( select TSPL_ITEM_MASTER.Product_Type, gl1.Account_code as Inventory_Control_Acc,gl1.Description as Inventory_Control_Acc_desc ,InventroyMovement.Fat_Rate,InventroyMovement.SNF_Rate ,InventroyMovement.Trans_Id,InventroyMovement.Trans_Type, (CASE WHEN (InventroyMovement.Trans_Type='IC-AD' AND TSPL_ADJUSTMENT_HEADER.Reference_Document='JWO-SRN-JLO') THEN 'Jobwork Consumption' ELSE " & _
            " TSPL_INVENTORY_SOURCE_CODE.Name END )as Trans_Type_Name,InventroyMovement.Source_Doc_No,InventroyMovement.Punching_Date, InventroyMovement.InOut,case when InventroyMovement.InOut='I' then 'In' else case when InventroyMovement.InOut='O' then 'Out' else '' end end as 'InOutView',"
        qry += " case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end as Main_Location_Code,MainLocationTable.Location_Desc as MainLocationDesc, InventroyMovement.Location_Code,TSPL_LOCATION_MASTER.Location_Desc AS [Loc Desp],TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress],SourceCode,SourceName,SourceType  ,Item_Group.Item_Group,Item_Group.Group_Description, InventroyMovement.Item_Code, InventroyMovement.MRP ,TSPL_ITEM_MASTER.Item_Desc,tspl_item_master.itf_code,TSPL_ITEM_MASTER.Structure_Code,TSPL_STRUCTURE_MASTER.Structure_Descq,"
        qry += " IsFromMilk,MilkFATKG,MilkSNFKG,case when IsFromMilk=1 then MilkFatPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) end as MilkFatPer,case when IsFromMilk=1 then MilkSNFPer else isnull((select TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range  from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) end as MilkSNFPer,TSPL_LOCATION_MASTER.Is_Section,TSPL_LOCATION_MASTER.Is_Sub_Location,"
        qry += " isnull((InventroyMovement.Stock_Qty * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end))  ,0) as QtyKG,"
        If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
            qry += " '" + clsCommon.myCstr(cmbUnit.SelectedValue) + "' as Stock_UOM,(InventroyMovement.Stock_Qty / TSPL_ITEM_UOM_DETAIL.Conversion_Factor) as Stock_Qty,"
            qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer,"
            qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end) * (TSPL_ITEM_UOM_DETAIL.Conversion_Factor)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"
        Else
            qry += " InventroyMovement.Stock_UOM,InventroyMovement.Stock_Qty,"
            qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0) as FatPer,"
            qry += " isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0) as SNFPer,"
        End If
        qry += " (case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=3 then InventroyMovement.FIFO_Cost else case when TSPL_PURCHASE_ACCOUNTS.Costing_Method=2 then InventroyMovement.LIFO_Cost else InventroyMovement.Avg_Cost end end ) as Cost,TSPL_ITEM_MASTER.Item_Category_Struct_Code " + Environment.NewLine

        If clsCommon.myLen(strCategoryTable) > 0 Then
            qry += "," + strCodeColumnSelect + "," + strCodeDescColumnSelect
        End If
        qry += " ,TSPL_ITEM_MASTER.Item_Type,VirtualTableItemType.Name as Item_Type_Name,TSPL_INVENTORY_SOURCE_CODE.In_Category,TSPL_INVENTORY_SOURCE_CODE.Out_Category,TSPL_INVENTORY_SOURCE_CODE.Code,(case when ISNULL(InventroyMovement.Location_Code,'')='' then InventroyMovement.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end) as PrimaryLocation "
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Transaction Wise") = CompairStringResult.Equal Then
            qry += " ,case TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG when TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG then TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_KG * case when InOut='I' then 1 else -1 end else case when InOut='I' then ( (case when IsFromMilk=1 then MilkFATKG else (Stock_Qty*isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER  left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='FAT'),0)) end) * case when InOut='I' then 1 else -1 end) else -1 end end as Prod_Fat_KG "
            qry += " ,case TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per  when TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per then TSPL_PP_PRODUCTION_ENTRY_DETAIL.FAT_Per else 0 end as Prod_Fat_Per  "
            qry += " ,case TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG when TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG then TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_KG * case when InOut='I' then 1 else -1 end else case when InOut='I' then (( (case when IsFromMilk=1 then MilkSNFKG else (Stock_Qty*isnull((select ((TSPL_ITEM_QC_PARAMETER_MASTER.Actual_Range/100) * (case when FATSNFConvertedUnit.Conversion_Factor=0 then 0 else 1/FATSNFConvertedUnit.Conversion_Factor end)) from TSPL_ITEM_QC_PARAMETER_MASTER left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code  where item_code=InventroyMovement.Item_Code and Type='SNF'),0)) end ) * case when InOut='I' then 1 else -1 end)) else -1 end end as Prod_SNF_KG "
            qry += " ,case TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per  when TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per then TSPL_PP_PRODUCTION_ENTRY_DETAIL.SNF_Per else 0 end as Prod_SNF_Per  "
        End If
        qry += ",is_consumption from ( "
        qry += " select 0 AS Fat_Rate,0 AS SNF_Rate ,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,0 as IsFromMilk,0 as MilkFatPer,0 as MilkSNFPer,0 as MilkFATKG,0 as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,is_consumption  from TSPL_INVENTORY_MOVEMENT " + Environment.NewLine
        qry += " union all " + Environment.NewLine
        qry += " select ISNULL(Fat_Rate,0) AS Fat_Rate,ISNULL(SNF_Rate,0) AS SNF_Rate,Trans_Id,Trans_Type,Source_Doc_No,Punching_Date,InOut,Location_Code,Item_Code,UOM, MRP,Stock_UOM,Stock_Qty,FIFO_Cost,LIFO_Cost,Avg_Cost,1 as IsFromMilk,Fat_Per as MilkFatPer ,SNF_Per as MilkSNFPer,Fat_KG as MilkFATKG,SNF_KG as MilkSNFKG,case when cust_code is not null and len(cust_code)>0 then cust_code else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Code else Other_Location_Code end end as SourceCode,case when cust_code is not null and len(cust_code)>0 then Cust_Name else case when Vendor_Code is not null and len(Vendor_Code)>0 then Vendor_Name else Other_Location_Desc end end as SourceName, case when cust_code is not null and len(cust_code)>0 then 'C' else case when Vendor_Code is not null and len(Vendor_Code)>0 then 'V' else case when Other_Location_Code is not null and len(Other_Location_Code)>0 then 'L' else '' end end end as SourceType,is_consumption from TSPL_INVENTORY_MOVEMENT_NEW" + Environment.NewLine
        qry += ") InventroyMovement " + Environment.NewLine
        qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=InventroyMovement.Item_Code" + Environment.NewLine
        qry += " left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code" + Environment.NewLine
        qry += " left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code" + Environment.NewLine
        qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code = InventroyMovement.Location_Code " + Environment.NewLine
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Transaction Wise") = CompairStringResult.Equal Then
            qry += " left outer join TSPL_PP_PRODUCTION_ENTRY_DETAIL on TSPL_PP_PRODUCTION_ENTRY_DETAIL.PROD_ENTRY_CODE=InventroyMovement.Source_Doc_No AND InventroyMovement.Item_Code=TSPL_PP_PRODUCTION_ENTRY_DETAIL.ITEM_CODE AND InventroyMovement.UOM=TSPL_PP_PRODUCTION_ENTRY_DETAIL.UNIT_CODE" + Environment.NewLine
        End If
        qry += " left outer join TSPL_LOCATION_MASTER as MainLocationTable on MainLocationTable.Location_Code =(case when TSPL_LOCATION_MASTER.Is_Section='N' and TSPL_LOCATION_MASTER.Is_Sub_Location='N' then TSPL_LOCATION_MASTER.Location_Code else TSPL_LOCATION_MASTER.Main_Location_Code end)"
        qry += " left outer join TSPL_ITEM_UOM_DETAIL as FATSNFConvertedUnit on FATSNFConvertedUnit.Item_Code=InventroyMovement.Item_Code and FATSNFConvertedUnit.UOM_Code='KG'"
        qry += " left outer join TSPL_INVENTORY_SOURCE_CODE on TSPL_INVENTORY_SOURCE_CODE.code=InventroyMovement.Trans_Type " & _
               " left outer join TSPL_ADJUSTMENT_HEADER ON TSPL_ADJUSTMENT_HEADER.Adjustment_No=InventroyMovement.Source_Doc_No  "
        If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
            qry += " inner join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=InventroyMovement.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='" + clsCommon.myCstr(cmbUnit.SelectedValue) + "'"
        End If
        If clsCommon.myLen(strCategoryTable) > 0 Then
            qry += " left outer join (" + strCategoryTable + ") as VirtualCategoryTabel on  VirtualCategoryTabel.Item_Code=InventroyMovement.Item_Code"
        End If
        qry += " left outer join (" & strItemGroup & ") as Item_Group on Item_Group.Structure_Code =TSPL_ITEM_MASTER.Structure_Code "
        qry += " left outer join (" & FrmItemMasterRMOther.LoadItemTypeQuery() & ") as VirtualTableItemType on VirtualTableItemType.Code = TSPL_ITEM_MASTER.Item_Type " & _
        " left outer join TSPL_GL_ACCOUNTS on TSPL_GL_ACCOUNTS.Account_Code =TSPL_PURCHASE_ACCOUNTS .Inv_Control_Account  " & _
        " left outer join TSPL_GL_ACCOUNTS gl1 on gl1.Account_Seg_Code1 =TSPL_GL_ACCOUNTS.Account_Seg_Code1  and gl1.Account_Seg_Code7 =  tspl_location_master.Loc_Segment_Code "
        qry += " Where 2=2 and TSPL_LOCATION_MASTER.GIT_Type<>'Y' and MainLocationTable.GIT_Type<>'Y'"

        If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
            qry += " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") " + Environment.NewLine
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "MI") = CompairStringResult.Equal Then
            qry += " and TSPL_ITEM_MASTER.Product_Type in ('MI')"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboFATSNF.SelectedValue), "MP") = CompairStringResult.Equal Then
            qry += " and TSPL_ITEM_MASTER.Product_Type in ('MP')"
        Else
            qry += " and TSPL_ITEM_MASTER.Product_Type in ('MI','MP')"
        End If
        qry += "  ) xxxxx "

        qry += " where 2=2 "

        If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            qry += " and Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ") " + Environment.NewLine
        End If
        If txtTransaction.arrValueMember IsNot Nothing AndAlso txtTransaction.arrValueMember.Count > 0 Then
            qry += " and Trans_Type in (" + clsCommon.GetMulcallString(txtTransaction.arrValueMember) + ") " + Environment.NewLine
        End If

        If txtItemGroup.arrValueMember IsNot Nothing AndAlso txtItemGroup.arrValueMember.Count > 0 Then
            qry += " and coalesce(xxxxx.Item_Group,'') in (" + clsCommon.GetMulcallString(txtItemGroup.arrValueMember) + ") "
        End If



        Dim strWhrCatg As String = ""
        Dim LocationFirstTime As Integer = 0
        Dim LocationAddress As String = String.Empty

        If rbtnLocationSelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvLocation.RowCount - 1
                If clsCommon.myCBool(gvLocation.Rows(ii).Cells("SEL").Value) Then
                    LocationFirstTime += 1
                    If LocationFirstTime = 1 Then
                        LocationAddress = clsDBFuncationality.getSingleValue("select TSPL_LOCATION_MASTER.Add1+Case When ISNULL(TSPL_LOCATION_MASTER.Add2,'')='' Then ''  else ', '+TSPL_LOCATION_MASTER.Add2+ Case When ISNULL(TSPL_LOCATION_MASTER.Add3,'')='' Then '' Else ', '+TSPL_LOCATION_MASTER.Add3+ Case When ISNULL(TSPL_LOCATION_MASTER.Pin_Code ,'')='' Then '' else '-'+CONVERT(varchar, TSPL_LOCATION_MASTER.Pin_Code) End End End  as [LocAddress] from TSPL_LOCATION_MASTER where Location_Code= '" & clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) & "'")
                    End If
                    If IsApplicable Then
                        strWhrCatg += " Or "
                    End If
                    strWhrCatg += " ((case when Is_Section='N' and Is_Sub_Location='N' then Location_Code else Main_Location_Code end) = '" + clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value) + "') "
                    IsApplicable = True
                    Dim arr As Dictionary(Of String, Object) = gvLocation.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " and Location_Code in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    End If
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one location")
            End If
            qry += " and (" + strWhrCatg + ")"
        End If



        strWhrCatg = ""
        If rbtnCategorySelect.IsChecked Then
            Dim IsApplicable As Boolean = False
            For ii As Integer = 0 To gvCategory.RowCount - 1
                If clsCommon.myCBool(gvCategory.Rows(ii).Cells("SEL").Value) Then
                    If IsApplicable Then
                        strWhrCatg += " and "
                    End If
                    IsApplicable = True
                    strWhrCatg += "("
                    Dim arr As Dictionary(Of String, Object) = gvCategory.Rows(ii).Tag
                    If arr IsNot Nothing AndAlso arr.Count > 0 Then
                        strWhrCatg += " [" + clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value) + "] in ("
                        Dim isFirstTime As Boolean = True
                        For Each strInn As String In arr.Keys
                            If Not isFirstTime Then
                                strWhrCatg += ","
                            End If
                            strWhrCatg += "'" + strInn + "'"
                            isFirstTime = False
                        Next
                        strWhrCatg += ")"
                    Else
                        strWhrCatg += " 2=2  "
                    End If
                    strWhrCatg += ")"
                End If
            Next
            If Not IsApplicable Then
                Throw New Exception("Please select at least one category")
            End If
            qry += " and (" + strWhrCatg + ")"
        End If
        ''End of Base Quert start Now

        Dim strPurchase As String = "(case when ( Trans_Type in ('BulkSRN','BulkSRNTrade','MCC-MSRN','SRN') or (Trans_Type in ('JWSRNESTM') and InOut='I' and Item_Type='R' and Product_Type='MI' )) then 1 else (case when Trans_Type in ('BulkSRNRet','SRN-RET','Purchase Return','M-PURRETURN') then -1 else 0 end) end)"
        Dim strTransferIn As String = "(case when Trans_Type in ('MilkTransferIn','Transfer') and InOut='I' then 1 else (case when Trans_Type in ('TRN-RET','MilkTransferInReturn') and InOut='O' then -1 else 0 end) end)"
        Dim strOtherIn As String = "(case when Trans_Type in ('IC-AD','Disassembly','PRD_STG_PROC','SI-MT') and InOut='I' then 1  else 0 end)"
        Dim strConsume As String = "(case when Trans_Type in ('PP_STD-FQC','PRD_STG_PROC','Disassembly','JWSRNESTM') and InOut='O' then 1  else 0 end)"
        Dim strProduced As String = "(case when (Trans_Type in ('Disassembly','PP_STD-FQC') or (Trans_Type in ('JWSRNESTM')  and Item_Type='F' and Product_Type='MP')) and InOut='I' then 1  else 0 end)"
        Dim strProducedByProductionEntry As String = "(case when Trans_Type in ('PROD_ENTRY') and InOut='I' then 1  else (case when Trans_Type in ('PP-PR') and InOut='O' then -1 else 0 end) end)"
        Dim strLoss As String = "(case when Trans_Type in ('PP_STD-FQC','PRD_STG_PROC','Disassembly','JWSRNESTM') and InOut='O' then 1  else (case when Trans_Type in ('PROD_ENTRY','PROD_WR') and is_consumption=2 and InOut='O'  then -1 else -1*(" + strProduced + ") end) end)"
        Dim strSale As String = "(case when Trans_Type in ('FS-SH','MCC-MSALE','MT_SALE_IN','PS-SH','ScrapIn','SD-SH','DisCanSale','DispatchBS') then 1 else (case when Trans_Type in ('MS-SR','FS-SR','MCC-MSR','PS-SR','SALE RETURN','SALERETURNBS') then -1  else 0 end) end)"
        Dim strTransferOut As String = "(case when Trans_Type in ('DispChallan') then 1 else (case when Trans_Type in ('DispChallanRet','DispChallan-RET') then -1 else (case when Trans_Type in ('Transfer') and InOut='O' then 1 else 0 end) end)end)"
        Dim strOtherOut As String = "(case when Trans_Type in ('IC-AD','SI-MT','Disassembly') and InOut='O' then 1 else 0 end)"

        OuterOpClo = "convert(decimal(18,2),OP_FAT) as OP_FAT,convert(decimal(18,2), OP_SNF) as OP_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Purchase_FAT) as Purchase_FAT,convert(decimal(18,2), Purchase_SNF) as Purchase_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Transfer_In_FAT) as Transfer_In_FAT,convert(decimal(18,2) ,Transfer_In_SNF) as Transfer_In_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Other_In_FAT) as Other_In_FAT,convert(decimal(18,2) ,Other_In_SNF) as Other_In_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), (OP_FAT+Purchase_FAT+Transfer_In_FAT+Other_In_FAT)) as Total_In_FAT,convert(decimal(18,2), (OP_SNF+Purchase_SNF+Transfer_In_SNF+Other_In_SNF)) as Total_In_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Consumed_FAT) as Consumed_FAT,convert(decimal(18,2) ,Consumed_SNF) as Consumed_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Produced_FAT) as Produced_FAT,convert(decimal(18,2) ,Produced_SNF) as Produced_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Loss_FAT) as Loss_FAT,convert(decimal(18,2) ,Loss_SNF) as Loss_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Production_Entry_FAT) as Production_Entry_FAT,convert(decimal(18,2) ,Production_Entry_SNF) as Production_Entry_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Sale_FAT) as Sale_FAT,convert(decimal(18,2) ,Sale_SNF) as Sale_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Transfer_Out_FAT) as Transfer_Out_FAT,convert(decimal(18,2) ,Transfer_Out_SNF) as Transfer_Out_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), Other_Out_FAT) as Other_Out_FAT,convert(decimal(18,2) ,Other_Out_SNF) as Other_Out_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2), (Sale_FAT+Transfer_Out_FAT+Other_Out_FAT)) as Total_Out_FAT,convert(decimal(18,2), (Sale_SNF+Transfer_Out_SNF+Other_Out_SNF)) as Total_Out_SNF" + Environment.NewLine + _
        ",convert(decimal(18,2),(OP_FAT+Purchase_FAT+Transfer_In_FAT+Other_In_FAT)-Consumed_FAT+Produced_FAT-(Sale_FAT+Transfer_Out_FAT+Other_Out_FAT)) as Balance_FAT,convert(decimal(18,2),(OP_SNF+Purchase_SNF+Transfer_In_SNF+Other_In_SNF)-Consumed_SNF+Produced_SNF-(Sale_SNF+Transfer_Out_SNF+Other_Out_SNF)) as Balance_SNF"


        ArrInnerOpClo = New Dictionary(Of String, String)
        ArrInnerOpClo.Add("OP_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end)  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * ((1*" + strPurchase + ") +(1*" + strTransferIn + ")+(1*" + strOtherIn + ")+(-1*" + strConsume + ")+(1*" + strProduced + ")+(-1*" + strSale + ")+(-1*" + strTransferOut + ")+(-1*" + strOtherOut + ")))  ")
        ArrInnerOpClo.Add("OP_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end)  * (CASE WHEN PUNCHING_DAte < '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * ((1*" + strPurchase + ")+(1*" + strTransferIn + ")+(1*" + strOtherIn + ")+(-1*" + strConsume + ")+(1*" + strProduced + ")+(-1*" + strSale + ")+(-1*" + strTransferOut + ")+(-1*" + strOtherOut + ")))")
        ArrInnerOpClo.Add("Purchase_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strPurchase + ") ")
        ArrInnerOpClo.Add("Purchase_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strPurchase + ")")
        ArrInnerOpClo.Add("Transfer_In_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strTransferIn + ")")
        ArrInnerOpClo.Add("Transfer_In_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strTransferIn + ")")
        ArrInnerOpClo.Add("Other_In_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strOtherIn + ")")
        ArrInnerOpClo.Add("Other_In_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strOtherIn + ")")
        ArrInnerOpClo.Add("Consumed_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strConsume + ")")
        ArrInnerOpClo.Add("Consumed_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strConsume + ")")
        ArrInnerOpClo.Add("Produced_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strProduced + ")")
        ArrInnerOpClo.Add("Produced_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strProduced + ")")
        ArrInnerOpClo.Add("Production_Entry_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strProducedByProductionEntry + ")")
        ArrInnerOpClo.Add("Production_Entry_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strProducedByProductionEntry + ")")
        ArrInnerOpClo.Add("Loss_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strLoss + ")")
        ArrInnerOpClo.Add("Loss_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strLoss + ")")
        ArrInnerOpClo.Add("Sale_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strSale + ")")
        ArrInnerOpClo.Add("Sale_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strSale + ")")
        ArrInnerOpClo.Add("Transfer_Out_FAT", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strTransferOut + ")")
        ArrInnerOpClo.Add("Transfer_Out_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strTransferOut + ") ")
        ArrInnerOpClo.Add("Other_Out_FAT ", "((case when IsFromMilk=1 then MilkFATKG else (STOCK_QTY*FatPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strOtherOut + ")")
        ArrInnerOpClo.Add("Other_Out_SNF", "((case when IsFromMilk=1 then MilkSNFKG else (STOCK_QTY*SNFPer) end) * (CASE WHEN PUNCHING_DAte >= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithStartTime(txtFromDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' AND PUNCHING_DAte <= '" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(txtToDate.Value), "dd/MMM/yyyy hh:mm:ss tt") + "' THEN 1 ELSE 0 end) * " + strOtherOut + ")")
        Return qry
    End Function

    Sub SetGridFormationOFGV1()
        gv1.GroupDescriptors.Clear()
        gv1.TableElement.TableHeaderHeight = 40
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv1.Columns.Count - 1
            gv1.Columns(ii).ReadOnly = True
            gv1.Columns(ii).IsVisible = False
        Next
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Main Location Wise") = CompairStringResult.Equal Then
            gv1.Columns("Main_Location_Code").HeaderText = "Main Location Code"

            gv1.Columns("MainLocationDesc").IsVisible = True
            gv1.Columns("MainLocationDesc").Width = 150
            gv1.Columns("MainLocationDesc").HeaderText = "Main Location"


            gv1.Columns("OP_FAT").IsVisible = True
            gv1.Columns("OP_FAT").Width = 100
            gv1.Columns("OP_FAT").HeaderText = "Opening FAT"
            gv1.Columns("OP_FAT").FormatString = "{0:n2}"

            gv1.Columns("OP_SNF").IsVisible = True
            gv1.Columns("OP_SNF").Width = 100
            gv1.Columns("OP_SNF").HeaderText = "Opening SNF"
            gv1.Columns("OP_SNF").FormatString = "{0:n2}"

            gv1.Columns("Purchase_FAT").IsVisible = True
            gv1.Columns("Purchase_FAT").Width = 100
            gv1.Columns("Purchase_FAT").HeaderText = "Purchase FAT"
            gv1.Columns("Purchase_FAT").FormatString = "{0:n2}"

            gv1.Columns("Purchase_SNF").IsVisible = True
            gv1.Columns("Purchase_SNF").Width = 100
            gv1.Columns("Purchase_SNF").HeaderText = "Purchase SNF"
            gv1.Columns("Purchase_SNF").FormatString = "{0:n2}"

            gv1.Columns("Transfer_In_FAT").IsVisible = True
            gv1.Columns("Transfer_In_FAT").Width = 100
            gv1.Columns("Transfer_In_FAT").HeaderText = "Transfer In FAT"
            gv1.Columns("Transfer_In_FAT").FormatString = "{0:n2}"

            gv1.Columns("Transfer_In_SNF").IsVisible = True
            gv1.Columns("Transfer_In_SNF").Width = 100
            gv1.Columns("Transfer_In_SNF").HeaderText = "Transfer In SNF"
            gv1.Columns("Transfer_In_SNF").FormatString = "{0:n2}"

            gv1.Columns("Other_In_FAT").IsVisible = True
            gv1.Columns("Other_In_FAT").Width = 100
            gv1.Columns("Other_In_FAT").HeaderText = "Other In FAT"
            gv1.Columns("Other_In_FAT").FormatString = "{0:n2}"

            gv1.Columns("Other_In_SNF").IsVisible = True
            gv1.Columns("Other_In_SNF").Width = 100
            gv1.Columns("Other_In_SNF").HeaderText = "Other In SNF"
            gv1.Columns("Other_In_SNF").FormatString = "{0:n2}"

            gv1.Columns("Total_In_FAT").IsVisible = True
            gv1.Columns("Total_In_FAT").Width = 100
            gv1.Columns("Total_In_FAT").HeaderText = "Total In FAT"
            gv1.Columns("Total_In_FAT").FormatString = "{0:n2}"

            gv1.Columns("Total_In_SNF").IsVisible = True
            gv1.Columns("Total_In_SNF").Width = 100
            gv1.Columns("Total_In_SNF").HeaderText = "Total In SNF"
            gv1.Columns("Total_In_SNF").FormatString = "{0:n2}"

            gv1.Columns("Consumed_FAT").IsVisible = True
            gv1.Columns("Consumed_FAT").Width = 100
            gv1.Columns("Consumed_FAT").HeaderText = "Consumed FAT"
            gv1.Columns("Consumed_FAT").FormatString = "{0:n2}"

            gv1.Columns("Consumed_SNF").IsVisible = True
            gv1.Columns("Consumed_SNF").Width = 100
            gv1.Columns("Consumed_SNF").HeaderText = "Consumed SNF"
            gv1.Columns("Consumed_SNF").FormatString = "{0:n2}"

            gv1.Columns("Produced_FAT").IsVisible = True
            gv1.Columns("Produced_FAT").Width = 100
            gv1.Columns("Produced_FAT").HeaderText = "Produced FAT"
            gv1.Columns("Produced_FAT").FormatString = "{0:n2}"

            gv1.Columns("Produced_SNF").IsVisible = True
            gv1.Columns("Produced_SNF").Width = 100
            gv1.Columns("Produced_SNF").HeaderText = "Produced SNF"
            gv1.Columns("Produced_SNF").FormatString = "{0:n2}"

            gv1.Columns("Loss_FAT").IsVisible = True
            gv1.Columns("Loss_FAT").Width = 100
            gv1.Columns("Loss_FAT").HeaderText = "Loss FAT"
            gv1.Columns("Loss_FAT").FormatString = "{0:n2}"

            gv1.Columns("Loss_SNF").IsVisible = True
            gv1.Columns("Loss_SNF").Width = 100
            gv1.Columns("Loss_SNF").HeaderText = "Loss SNF"
            gv1.Columns("Loss_SNF").FormatString = "{0:n2}"

            gv1.Columns("Production_Entry_FAT").IsVisible = True
            gv1.Columns("Production_Entry_FAT").Width = 100
            gv1.Columns("Production_Entry_FAT").HeaderText = "Production Entry FAT"
            gv1.Columns("Production_Entry_FAT").FormatString = "{0:n2}"

            gv1.Columns("Production_Entry_SNF").IsVisible = True
            gv1.Columns("Production_Entry_SNF").Width = 100
            gv1.Columns("Production_Entry_SNF").HeaderText = "Production Entry SNF"
            gv1.Columns("Production_Entry_SNF").FormatString = "{0:n2}"

            gv1.Columns("Sale_FAT").IsVisible = True
            gv1.Columns("Sale_FAT").Width = 100
            gv1.Columns("Sale_FAT").HeaderText = "Sale FAT"
            gv1.Columns("Sale_FAT").FormatString = "{0:n2}"

            gv1.Columns("Sale_SNF").IsVisible = True
            gv1.Columns("Sale_SNF").Width = 100
            gv1.Columns("Sale_SNF").HeaderText = "Sale SNF"
            gv1.Columns("Sale_SNF").FormatString = "{0:n2}"

            gv1.Columns("Transfer_Out_FAT").IsVisible = True
            gv1.Columns("Transfer_Out_FAT").Width = 100
            gv1.Columns("Transfer_Out_FAT").HeaderText = "Transfer Out FAT"
            gv1.Columns("Transfer_Out_FAT").FormatString = "{0:n2}"

            gv1.Columns("Transfer_Out_SNF").IsVisible = True
            gv1.Columns("Transfer_Out_SNF").Width = 100
            gv1.Columns("Transfer_Out_SNF").HeaderText = "Transfer Out SNF"
            gv1.Columns("Transfer_Out_SNF").FormatString = "{0:n2}"

            gv1.Columns("Other_Out_FAT").IsVisible = True
            gv1.Columns("Other_Out_FAT").Width = 100
            gv1.Columns("Other_Out_FAT").HeaderText = "Other Out FAT"
            gv1.Columns("Other_Out_FAT").FormatString = "{0:n2}"

            gv1.Columns("Other_Out_SNF").IsVisible = True
            gv1.Columns("Other_Out_SNF").Width = 100
            gv1.Columns("Other_Out_SNF").HeaderText = "Other Out SNF"
            gv1.Columns("Other_Out_SNF").FormatString = "{0:n2}"


            gv1.Columns("Total_Out_FAT").IsVisible = True
            gv1.Columns("Total_Out_FAT").Width = 100
            gv1.Columns("Total_Out_FAT").HeaderText = "Total Out FAT"
            gv1.Columns("Total_Out_FAT").FormatString = "{0:n2}"

            gv1.Columns("Total_Out_SNF").IsVisible = True
            gv1.Columns("Total_Out_SNF").Width = 100
            gv1.Columns("Total_Out_SNF").HeaderText = "Total Out SNF"
            gv1.Columns("Total_Out_SNF").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = True
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Balance FAT"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = True
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Balance SNF"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OP_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OP_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Purchase_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Purchase_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Transfer_In_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Transfer_In_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Other_In_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Other_In_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Total_In_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Total_In_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Consumed_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Consumed_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Produced_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Produced_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Loss_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Loss_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Production_Entry_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Production_Entry_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Sale_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Sale_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Transfer_Out_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Transfer_Out_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Other_Out_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Other_Out_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Total_Out_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Total_Out_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Main Location and Location Wise") = CompairStringResult.Equal Then
            gv1.Columns("Main_Location_Code").HeaderText = "Main Location Code"

            gv1.Columns("MainLocationDesc").IsVisible = True
            gv1.Columns("MainLocationDesc").Width = 150
            gv1.Columns("MainLocationDesc").HeaderText = "Main Location"

            gv1.Columns("Location_Code").HeaderText = "Location Code"

            gv1.Columns("Loc Desp").IsVisible = True
            gv1.Columns("Loc Desp").Width = 150
            gv1.Columns("Loc Desp").HeaderText = "Location"

            gv1.Columns("OP_FAT").IsVisible = True
            gv1.Columns("OP_FAT").Width = 100
            gv1.Columns("OP_FAT").HeaderText = "Opening FAT"
            gv1.Columns("OP_FAT").FormatString = "{0:n2}"

            gv1.Columns("OP_SNF").IsVisible = True
            gv1.Columns("OP_SNF").Width = 100
            gv1.Columns("OP_SNF").HeaderText = "Opening SNF"
            gv1.Columns("OP_SNF").FormatString = "{0:n2}"

            gv1.Columns("Purchase_FAT").IsVisible = True
            gv1.Columns("Purchase_FAT").Width = 100
            gv1.Columns("Purchase_FAT").HeaderText = "Purchase FAT"
            gv1.Columns("Purchase_FAT").FormatString = "{0:n2}"

            gv1.Columns("Purchase_SNF").IsVisible = True
            gv1.Columns("Purchase_SNF").Width = 100
            gv1.Columns("Purchase_SNF").HeaderText = "Purchase SNF"
            gv1.Columns("Purchase_SNF").FormatString = "{0:n2}"

            gv1.Columns("Transfer_In_FAT").IsVisible = True
            gv1.Columns("Transfer_In_FAT").Width = 100
            gv1.Columns("Transfer_In_FAT").HeaderText = "Transfer In FAT"
            gv1.Columns("Transfer_In_FAT").FormatString = "{0:n2}"

            gv1.Columns("Transfer_In_SNF").IsVisible = True
            gv1.Columns("Transfer_In_SNF").Width = 100
            gv1.Columns("Transfer_In_SNF").HeaderText = "Transfer In SNF"
            gv1.Columns("Transfer_In_SNF").FormatString = "{0:n2}"

            gv1.Columns("Other_In_FAT").IsVisible = True
            gv1.Columns("Other_In_FAT").Width = 100
            gv1.Columns("Other_In_FAT").HeaderText = "Other In FAT"
            gv1.Columns("Other_In_FAT").FormatString = "{0:n2}"

            gv1.Columns("Other_In_SNF").IsVisible = True
            gv1.Columns("Other_In_SNF").Width = 100
            gv1.Columns("Other_In_SNF").HeaderText = "Other In SNF"
            gv1.Columns("Other_In_SNF").FormatString = "{0:n2}"

            gv1.Columns("Total_In_FAT").IsVisible = True
            gv1.Columns("Total_In_FAT").Width = 100
            gv1.Columns("Total_In_FAT").HeaderText = "Total In FAT"
            gv1.Columns("Total_In_FAT").FormatString = "{0:n2}"

            gv1.Columns("Total_In_SNF").IsVisible = True
            gv1.Columns("Total_In_SNF").Width = 100
            gv1.Columns("Total_In_SNF").HeaderText = "Total In SNF"
            gv1.Columns("Total_In_SNF").FormatString = "{0:n2}"

            gv1.Columns("Consumed_FAT").IsVisible = True
            gv1.Columns("Consumed_FAT").Width = 100
            gv1.Columns("Consumed_FAT").HeaderText = "Consumed FAT"
            gv1.Columns("Consumed_FAT").FormatString = "{0:n2}"

            gv1.Columns("Consumed_SNF").IsVisible = True
            gv1.Columns("Consumed_SNF").Width = 100
            gv1.Columns("Consumed_SNF").HeaderText = "Consumed SNF"
            gv1.Columns("Consumed_SNF").FormatString = "{0:n2}"

            gv1.Columns("Produced_FAT").IsVisible = True
            gv1.Columns("Produced_FAT").Width = 100
            gv1.Columns("Produced_FAT").HeaderText = "Produced FAT"
            gv1.Columns("Produced_FAT").FormatString = "{0:n2}"

            gv1.Columns("Produced_SNF").IsVisible = True
            gv1.Columns("Produced_SNF").Width = 100
            gv1.Columns("Produced_SNF").HeaderText = "Produced SNF"
            gv1.Columns("Produced_SNF").FormatString = "{0:n2}"

            gv1.Columns("Loss_FAT").IsVisible = True
            gv1.Columns("Loss_FAT").Width = 100
            gv1.Columns("Loss_FAT").HeaderText = "Loss FAT"
            gv1.Columns("Loss_FAT").FormatString = "{0:n2}"

            gv1.Columns("Loss_SNF").IsVisible = True
            gv1.Columns("Loss_SNF").Width = 100
            gv1.Columns("Loss_SNF").HeaderText = "Loss SNF"
            gv1.Columns("Loss_SNF").FormatString = "{0:n2}"


            gv1.Columns("Production_Entry_FAT").IsVisible = True
            gv1.Columns("Production_Entry_FAT").Width = 100
            gv1.Columns("Production_Entry_FAT").HeaderText = "Production Entry FAT"
            gv1.Columns("Production_Entry_FAT").FormatString = "{0:n2}"

            gv1.Columns("Production_Entry_SNF").IsVisible = True
            gv1.Columns("Production_Entry_SNF").Width = 100
            gv1.Columns("Production_Entry_SNF").HeaderText = "Production Entry SNF"
            gv1.Columns("Production_Entry_SNF").FormatString = "{0:n2}"

            gv1.Columns("Sale_FAT").IsVisible = True
            gv1.Columns("Sale_FAT").Width = 100
            gv1.Columns("Sale_FAT").HeaderText = "Sale FAT"
            gv1.Columns("Sale_FAT").FormatString = "{0:n2}"

            gv1.Columns("Sale_SNF").IsVisible = True
            gv1.Columns("Sale_SNF").Width = 100
            gv1.Columns("Sale_SNF").HeaderText = "Sale SNF"
            gv1.Columns("Sale_SNF").FormatString = "{0:n2}"

            gv1.Columns("Transfer_Out_FAT").IsVisible = True
            gv1.Columns("Transfer_Out_FAT").Width = 100
            gv1.Columns("Transfer_Out_FAT").HeaderText = "Transfer Out FAT"
            gv1.Columns("Transfer_Out_FAT").FormatString = "{0:n2}"

            gv1.Columns("Transfer_Out_SNF").IsVisible = True
            gv1.Columns("Transfer_Out_SNF").Width = 100
            gv1.Columns("Transfer_Out_SNF").HeaderText = "Transfer Out SNF"
            gv1.Columns("Transfer_Out_SNF").FormatString = "{0:n2}"

            gv1.Columns("Other_Out_FAT").IsVisible = True
            gv1.Columns("Other_Out_FAT").Width = 100
            gv1.Columns("Other_Out_FAT").HeaderText = "Other Out FAT"
            gv1.Columns("Other_Out_FAT").FormatString = "{0:n2}"

            gv1.Columns("Other_Out_SNF").IsVisible = True
            gv1.Columns("Other_Out_SNF").Width = 100
            gv1.Columns("Other_Out_SNF").HeaderText = "Other Out SNF"
            gv1.Columns("Other_Out_SNF").FormatString = "{0:n2}"


            gv1.Columns("Total_Out_FAT").IsVisible = True
            gv1.Columns("Total_Out_FAT").Width = 100
            gv1.Columns("Total_Out_FAT").HeaderText = "Total Out FAT"
            gv1.Columns("Total_Out_FAT").FormatString = "{0:n2}"

            gv1.Columns("Total_Out_SNF").IsVisible = True
            gv1.Columns("Total_Out_SNF").Width = 100
            gv1.Columns("Total_Out_SNF").HeaderText = "Total Out SNF"
            gv1.Columns("Total_Out_SNF").FormatString = "{0:n2}"

            gv1.Columns("Balance_FAT").IsVisible = True
            gv1.Columns("Balance_FAT").Width = 100
            gv1.Columns("Balance_FAT").HeaderText = "Balance FAT"
            gv1.Columns("Balance_FAT").FormatString = "{0:n2}"

            gv1.Columns("Balance_SNF").IsVisible = True
            gv1.Columns("Balance_SNF").Width = 100
            gv1.Columns("Balance_SNF").HeaderText = "Balance SNF"
            gv1.Columns("Balance_SNF").FormatString = "{0:n2}"

            Dim summaryRowItem As New GridViewSummaryRowItem()
            Dim Smitem As New GridViewSummaryItem("OP_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("OP_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Purchase_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Purchase_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Transfer_In_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Transfer_In_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Other_In_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Other_In_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Total_In_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Total_In_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Consumed_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Consumed_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Produced_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Produced_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Loss_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Loss_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Production_Entry_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Production_Entry_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Sale_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Sale_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Transfer_Out_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Transfer_Out_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Other_Out_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Other_Out_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Total_Out_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Total_Out_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_FAT", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            Smitem = New GridViewSummaryItem("Balance_SNF", "{0:F2}", GridAggregateFunction.Sum)
            summaryRowItem.Add(Smitem)
            gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        End If
        gv1.BestFitColumns()
        ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            Dim ReportID As String = GetReportID()
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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
            common.clsCommon.MyMessageBoxShow(err.Message)
        End Try
    End Sub

    Private Sub gv1_ViewCellFormatting(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.ViewCellFormatting
        If TypeOf e.CellElement Is Telerik.WinControls.UI.GridSummaryCellElement Then
            e.CellElement.TextAlignment = ContentAlignment.MiddleRight
        End If
    End Sub

    Sub print(ByVal exporter As EnumExportTo)
        Try
            LoadData()

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)


            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
                arrHeader.Add("Unit : " + cmbUnit.SelectedValue)
            End If


            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location : " + strLoca)
            End If
            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("Stock Reco (" + cboType.Text + ")", gv1, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("Stock Reco (" + cboType.Text + ")", gv1, arrHeader, Me.Text, True)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Private Sub RadButton3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton3.Click
        EnableDisableCtrl(True)
        gv1.DataSource = Nothing
        gv1.Columns.Clear()
        gv1.Rows.Clear()
        gv1.GroupDescriptors.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        gv1.MasterTemplate.SummaryRowsBottom.Clear()
        arrBack = New List(Of String)
        RadPageViewPage2.Text = "Report"
        vsb.Visible = False
    End Sub

    Sub EnableDisableCtrl(ByVal val As Boolean)
        txtFromDate.Enabled = val
        txtToDate.Enabled = val
        RadGroupBox3.Enabled = val
        txtItem.Enabled = val
        RadGroupBox2.Enabled = val
        cmbUnit.Enabled = val
        cboType.Enabled = val
        cboFATSNF.Enabled = val
        txtTransaction.Enabled = val
        txtItemGroup.Enabled = val
        txtItemType.Enabled = val
    End Sub

    Private Sub rbtnLocationAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnLocationAll.ToggleStateChanged, rbtnLocationSelect.ToggleStateChanged
        gvLocation.Enabled = rbtnLocationSelect.IsChecked
        RadButton4.Enabled = rbtnLocationSelect.IsChecked
        RadButton5.Enabled = rbtnLocationSelect.IsChecked
    End Sub

    Private Sub rbtnCustomerAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rbtnCategoryAll.ToggleStateChanged, rbtnCategorySelect.ToggleStateChanged
        gvCategory.Enabled = rbtnCategorySelect.IsChecked
        RadButton6.Enabled = rbtnCategorySelect.IsChecked
        RadButton7.Enabled = rbtnCategorySelect.IsChecked
    End Sub

    Private Function GetReportID() As String
        Dim ReportID As String = ""
        If cboType.Enabled = False Then
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Main Location Wise") = CompairStringResult.Equal Then
                ReportID = "FGReNM" + clsCommon.myCstr(cboFATSNF.SelectedValue)
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Main Location and Location Wise") = CompairStringResult.Equal Then
                ReportID = "FGReNML" + clsCommon.myCstr(cboFATSNF.SelectedValue)
            End If
        End If
        Return ReportID
    End Function

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItemSett1.Click
        Dim ReportID As String = GetReportID()
        If clsCommon.myLen(ReportID) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
            End If
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub TreeView_NodeCheckedChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.TreeNodeCheckedEventArgs)
        TreeCheckBoxes(e.Node, e.Node.Checked)
    End Sub

    Public Sub TreeCheckBoxes(ByVal CurrentNode As RadTreeNode, ByVal val As Boolean)
        For Each Ctr As RadTreeNode In CurrentNode.Nodes
            Ctr.Checked = val
            TreeCheckBoxes(Ctr, val)
        Next
    End Sub

    Private Sub gvCategory_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvCategory.CellDoubleClick
        If clsCommon.myCBool(gvCategory.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 2
            frm.strCode = clsCommon.myCstr(gvCategory.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvCategory.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvCategory.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub gvLocation_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvLocation.CellDoubleClick
        If clsCommon.myCBool(gvLocation.CurrentRow.Cells("SEL").Value) Then
            Dim frm As New FrmCategorySelect()
            frm.lvl = 3
            frm.strCode = clsCommon.myCstr(gvLocation.CurrentRow.Cells("CODE").Value)
            frm.arrIn = gvLocation.CurrentRow.Tag
            frm.ShowDialog()
            If Not frm.isCancel Then
                gvLocation.CurrentRow.Tag = frm.arrOut
            End If
        End If
    End Sub

    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        If txtItemType.arrValueMember Is Nothing OrElse clsCommon.GetMulcallString(txtItemType.arrValueMember) = "All" Then
            qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        Else
            qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER where Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") order by Item_Code "

        End If

        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub

    Private Sub txtTransaction__My_Click(sender As Object, e As EventArgs) Handles txtTransaction._My_Click
        Dim qry As String = " select Code,Name,InOutType as [In/Out Type],Type from TSPL_INVENTORY_SOURCE_CODE where 2=2 "
        txtTransaction.arrValueMember = clsCommon.ShowMultipleSelectForm("TransTypeMulSe", qry, "Code", "Name", txtTransaction.arrValueMember, txtTransaction.arrDispalyMember)
    End Sub

    Function GetMIS_ITem_GroupColumn() As String
        Dim qry As String = ""
        qry = " select MAP.Custom_Field_Code from TSPL_CUSTOM_FIELD_MAPPING MAP " & _
            " left join TSPL_CUSTOM_FIELD_HEAD CF on MAP.Custom_Field_Code=CF.Code " & _
            " where CF.Name='MIS Item Group' and MAP.PROGRAM_CODE='" & clsUserMgtCode.itemStructure & "'"
        MIS_Item_Group = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        Return MIS_Item_Group
    End Function

    Private Sub txtItemGroup__My_Click(sender As Object, e As EventArgs) Handles txtItemGroup._My_Click
        Dim qry As String = " select SNo,Value,Description as Name,Custom_Field_Code as [Code] from TSPL_CUSTOM_FIELD_DETAIL where Custom_Field_Code='" & MIS_Item_Group & "' "
        txtItemGroup.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemGroupMulSel", qry, "Value", "Name", txtItemGroup.arrValueMember, txtItemGroup.arrDispalyMember)
    End Sub

    Private Sub RadButton4_Click(sender As Object, e As EventArgs) Handles RadButton4.Click
        CheckedAll(gvLocation)
    End Sub

    Private Sub CheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
        For ii As Integer = 0 To gv.ChildRows.Count - 1
            gv.ChildRows(ii).Cells("SEL").Value = True
        Next
    End Sub

    Private Sub UnCheckedAll(ByVal gv As RadGridView)
        For ii As Integer = 0 To gv.RowCount - 1
            gv.Rows(ii).Cells("SEL").Value = False
        Next
    End Sub

    Private Sub RadButton5_Click(sender As Object, e As EventArgs) Handles RadButton5.Click
        UnCheckedAll(gvLocation)
    End Sub

    Private Sub RadButton7_Click(sender As Object, e As EventArgs) Handles RadButton7.Click
        CheckedAll(gvCategory)
    End Sub

    Private Sub RadButton6_Click(sender As Object, e As EventArgs) Handles RadButton6.Click
        UnCheckedAll(gvCategory)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Type Wise Summary") = CompairStringResult.Equal Then

            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Group Wise Summary") = CompairStringResult.Equal Then
                arrBack.Remove("Item Type Wise Summary")
                cboType.SelectedValue = "Item Type Wise Summary"
                txtItemType.arrValueMember = arrItemType
                LoadData()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Category Wise Summary") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Group Wise Summary") Then
                arrBack.Remove("Item Group Wise Summary")
                cboType.SelectedValue = "Item Group Wise Summary"
                txtItemGroup.arrValueMember = arrItemGroup
                LoadData()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item Wise Summary") = CompairStringResult.Equal AndAlso arrBack.Contains("Category Wise Summary") Then
                arrBack.Remove("Category Wise Summary")
                cboType.SelectedValue = "Category Wise Summary"
                UnCheckedAll(gvCategory)
                If arrCat IsNot Nothing AndAlso arrCat.Count > 0 Then
                    rbtnCategorySelect.IsChecked = True
                    For Each str As String In arrCat.Keys
                        For ii As Integer = 0 To gvCategory.RowCount - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gvCategory.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                                gvCategory.Rows(ii).Cells("SEL").Value = True
                                gvCategory.Rows(ii).Tag = arrCat(str)
                            End If
                        Next
                    Next
                Else
                    rbtnCategoryAll.IsChecked = True
                End If
                LoadData()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Item And Location Wise Summary") = CompairStringResult.Equal AndAlso arrBack.Contains("Item Wise Summary") Then
                arrBack.Remove("Item Wise Summary")
                cboType.SelectedValue = "Item Wise Summary"
                txtItem.arrValueMember = arrItem
                LoadData()
            ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Document Wise Detail") = CompairStringResult.Equal AndAlso arrBack.Contains("Item And Location Wise Summary") Then
                arrBack.Remove("Item And Location Wise Summary")
                cboType.SelectedValue = "Item And Location Wise Summary"
                UnCheckedAll(gvLocation)
                If arrLoc IsNot Nothing AndAlso arrLoc.Count > 0 Then
                    rbtnLocationSelect.IsChecked = True
                    For Each str As String In arrLoc.Keys
                        For ii As Integer = 0 To gvLocation.RowCount - 1
                            If clsCommon.CompairString(clsCommon.myCstr(gvLocation.Rows(ii).Cells("CODE").Value), str) = CompairStringResult.Equal Then
                                gvLocation.Rows(ii).Cells("SEL").Value = True
                                gvLocation.Rows(ii).Tag = arrLoc(str)
                            End If
                        Next
                    Next
                Else
                    rbtnLocationAll.IsChecked = True
                End If
                LoadData()
            End If
            PageSetupReport_ID = GetReportID()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub chkIncludeGIT_ToggleStateChanged(sender As Object, args As StateChangedEventArgs)
        LoadLocation()
    End Sub

    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub

    Private Sub btnQuickExport_Click(sender As Object, e As EventArgs)
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.MISStockReco & "'"))
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
                arrHeader.Add("Unit : " + cmbUnit.SelectedValue)
            End If
            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location : " + strLoca)
            End If
            transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Shared Sub ExportBulkData(ByVal qry As String, ByVal arrVisibleColumAndCaption As Dictionary(Of String, String), ByVal strReportNameInSaveDialog As String)
        clsCommon.ProgressBarUpdate("Fatching data...")
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            If arrVisibleColumAndCaption Is Nothing OrElse arrVisibleColumAndCaption.Count <= 0 Then
                Throw New Exception("Please provice column and caption for export")
            End If

            Dim rowsPerSheet As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.QuickExport, clsFixedParameterCode.MaxRowsForQuickExport, Nothing))

            Dim FilePath As String = "C:\ERPTempFolder"
            Dim IsExists As Boolean = System.IO.Directory.Exists(FilePath)
            If Not IsExists Then
                System.IO.Directory.CreateDirectory(FilePath)
            End If
            strReportNameInSaveDialog += clsCommon.GetPrintDate(DateTime.Now, "yyyyMMddhhmmss")
            FilePath = "C:\ERPTempFolder\" + strReportNameInSaveDialog.Replace("/", "_").Replace("\\", "_") + ".xlsx"

            Dim intTotalRows As Integer = 0
            Dim intSheetCounter As Integer = 1
            Dim intReaderCounter As Integer = 0
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            Dim ResultsData As DataTable = Nothing
            Dim c As Integer = 0
            Dim firstTime As Boolean = True

            'Get the Columns names, types, this will help when we need to format the cells in the excel sheet.
            Dim dtSchema As DataTable = reader.GetSchemaTable()
            If dtSchema IsNot Nothing Then
                ResultsData = New DataTable()
                For Each drow As DataRow In dtSchema.Rows
                    Dim columnName As String = clsCommon.myCstr(drow("ColumnName"))
                    If arrVisibleColumAndCaption.ContainsKey(columnName) Then
                        Dim column = New DataColumn(columnName, DirectCast(drow("DataType"), Type))
                        column.Unique = CBool(drow("IsUnique"))
                        column.AllowDBNull = CBool(drow("AllowDBNull"))
                        column.AutoIncrement = CBool(drow("IsAutoIncrement"))
                        column.Caption = arrVisibleColumAndCaption(columnName)
                        ResultsData.Columns.Add(column)
                    End If
                Next
            End If
            Dim rowData(rowsPerSheet, ResultsData.Columns.Count) As Object
            Dim workBook As Microsoft.Office.Interop.Excel.Workbook = Nothing

            While reader.Read()
                intReaderCounter += 1
                clsCommon.ProgressBarUpdate("Fatching Data for Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                For i As Integer = 0 To ResultsData.Columns.Count - 1
                    rowData(c, i) = reader(ResultsData.Columns(i).ColumnName)
                Next
                c += 1
                If c = rowsPerSheet Then
                    clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                    workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
                    c = 0
                    ResultsData.Clear()
                    firstTime = False
                    intSheetCounter += 1
                End If
            End While

            If c <> 0 Then

                clsCommon.ProgressBarUpdate("Writing data in excel sheet.Sheet No " + clsCommon.myCstr(intSheetCounter) + " Row(s) " + clsCommon.myCstr(intReaderCounter))
                workBook = ExportToOxml(intSheetCounter, firstTime, c, ResultsData, rowData, FilePath, workBook)
            End If

            workBook = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()

            If intReaderCounter > 0 Then
                clsCommon.ProgressBarUpdate("Data exported.Opening File " + FilePath + ".Please wait...")
                Process.Start(FilePath)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Shared Function ExportToOxml(ByVal intSheetNo As Integer, ByVal firstTime As Boolean, ByVal RowsToWrite As Integer, ByVal Schema As DataTable, ByVal rawData(,) As Object, ByVal FilePath As String, ByRef wbook As Microsoft.Office.Interop.Excel.Workbook) As Microsoft.Office.Interop.Excel.Workbook
        Try
            Dim dt As New System.Data.DataTable()
            For i As Integer = 0 To Schema.Columns.Count - 1
                dt.Columns.Add("Column" & (i + 1))
                If clsCommon.myLen(Schema.Columns(i).Caption) > 0 Then
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).Caption
                Else
                    dt.Columns("Column" & (i + 1)).Caption = Schema.Columns(i).ColumnName
                End If
            Next

            Dim excel As New Microsoft.Office.Interop.Excel.Application
            If wbook Is Nothing Then
                Dim wBook1 As Microsoft.Office.Interop.Excel.Workbook = Nothing
                wbook = wBook1
                wbook = excel.Workbooks.Add()
            Else
                wbook = excel.Workbooks.Open(FilePath)
            End If
            Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet = Nothing
            Dim GridCurrentRowIndex As Int64 = -1
            Dim GridLastSavedRowIndex As Int64 = -1
            wSheet = wbook.Sheets.Add(, , 1)
            wbook.ActiveSheet.Move(After:=wbook.Sheets(wbook.Sheets.Count))
            If firstTime Then
                Try
                    CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet2"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                    CType(wbook.Sheets("Sheet3"), Microsoft.Office.Interop.Excel.Worksheet).Delete()
                Catch ex As Exception
                End Try
            End If
            wSheet.Name = "Sheet" & intSheetNo

            Dim jk As Integer = 0
            For i As Integer = 0 To Schema.Columns.Count - 1
                jk += 1
                Dim MyType As TypeCode = Type.GetTypeCode(Schema.Columns(i).DataType)
                If MyType = TypeCode.String Then
                    wSheet.Range(ColumnIndexToColumnLetter(jk) & ":" & ColumnIndexToColumnLetter(jk)).Cells.NumberFormat = "@"
                End If
            Next

            Dim colnum As Integer = -1
            Dim PrevCol As Integer = -1
            Dim ColNums(0 To Schema.Columns.Count - 1) As Integer

            Dim colIndex As Integer = 1
            Dim rowIndex As Integer = 1

            Dim dc As System.Data.DataColumn
            colIndex = 0
            For Each dc In Schema.Columns
                colIndex = colIndex + 1
                excel.Cells(rowIndex, colIndex) = dc.Caption
            Next

            Dim LastColumn As String = ColumnIndexToColumnLetter(Schema.Columns.Count)
            Dim Lastrow As Integer = RowsToWrite

            Dim row As Integer = 0
            Dim col As Integer = 0

            wSheet.Range("A2", LastColumn & (Lastrow + 1)).Value2 = rawData
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
            wSheet.Columns.AutoFit()
            CType(wbook.Sheets("Sheet1"), Microsoft.Office.Interop.Excel.Worksheet).Select()
            excel.DisplayAlerts = False
            wbook.SaveAs(FilePath, , , , , , Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive)
            wbook.Close(True)

            excel.Quit()
            excel = Nothing
            rawData = Nothing
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception

            Throw New Exception(ex.Message)
        End Try
        Return wbook
    End Function

    Private Shared Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = [String].Empty
        Dim [mod] As Integer = 0
        While div > 0
            [mod] = (div - 1) Mod 26
            colLetter = (Convert.ToChar(65 + [mod])).ToString & colLetter
            div = CInt((div - [mod]) / 26)
        End While
        Return colLetter
    End Function

    Private Sub LoadDataInGridViaDataReader(ByVal qry As String)
        Dim reader As System.Data.SqlClient.SqlDataReader = Nothing
        Try
            reader = clsDBFuncationality.GetDataReader(qry, Nothing)
            If reader Is Nothing OrElse Not reader.HasRows Then
                Throw New Exception("No Data found")
            End If
            RadPageView1.SelectedPage = RadPageViewPage2
            Dim dtTest As New DataTable
            dtTest.Load(reader)
            gv1.DataSource = dtTest
            GC.Collect()
            GC.WaitForPendingFinalizers()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            reader.Close()
        End Try
    End Sub

    Private Sub QExpExcel_Click(sender As Object, e As EventArgs) Handles QExpExcel.Click
        Try

            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
            arrHeader.Add("Name : " & clsDBFuncationality.getSingleValue("select program_name from tspl_program_Master where program_cODE='" & clsUserMgtCode.MISStockReco & "'"))
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
                arrHeader.Add("Unit : " + cmbUnit.SelectedValue)
            End If
            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location : " + strLoca)
            End If
            PageSetupReport_ID = GetReportID()
            If RadPageView1.SelectedPage Is RadPageView1.Pages(2) Then
                PageSetupReport_ID += "D"
                transportSql.applyExportTemplate(gvDetail, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gvDetail, "", Me.Text, , arrHeader)
            Else
                transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
                transportSql.QuickExportToExcel(gv1, "", Me.Text, , arrHeader)
            End If

            
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub QExpCSV_Click(sender As Object, e As EventArgs)
        Try
            If gv1 Is Nothing OrElse gv1.RowCount <= 0 Then
                MsgBox("Grid is empty!!!")
                Exit Sub
            End If
            ExportCSV(gv1, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try

    End Sub

    Public Sub ExportCSV(ByVal sender As RadGridView, Optional ByVal AddHeader As Boolean = False)
        Try
            Dim sfd As SaveFileDialog = New SaveFileDialog()
            Dim filePath As String
            sfd.FileName = Me.Text
            sfd.Filter = "CSV Files (*.csv) |*.csv"
            If sfd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                filePath = sfd.FileName
            Else
                Exit Sub
            End If
            Dim OpenInExcel As Boolean = True
            If gv1.Rows.Count * gv1.Columns.Count > 22000000 Then
                OpenInExcel = False
            Else
                OpenInExcel = True
            End If
            clsCommon.ProgressBarShow()
            IO.File.WriteAllLines(filePath, transportSql.ExportCSV(sender, AddHeader))
            clsCommon.ProgressBarHide()
            If OpenInExcel Then
                clsCommon.MyMessageBoxShow("Data Exported successfully")
                Process.Start(filePath)
            Else
                clsCommon.MyMessageBoxShow("Data Exported successfully but can not open through excel, use other utility to open the file.")
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub PDF_Click(sender As Object, e As EventArgs) Handles PDF.Click
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Date Range: " + clsCommon.GetPrintDate(txtFromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(txtToDate.Value, "dd/MM/yyyy"))
            arrHeader.Add("Company : " + objCommonVar.CurrentCompanyName)


            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If clsCommon.myLen(cmbUnit.SelectedValue) > 0 Then
                arrHeader.Add("Unit : " + cmbUnit.SelectedValue)
            End If


            If rbtnLocationSelect.IsChecked Then
                Dim strLoca As String = ""
                For Each grow As GridViewRowInfo In gvLocation.Rows
                    If clsCommon.myCBool(grow.Cells("SEL").Value) = True Then
                        strLoca += "," + clsCommon.myCstr(grow.Cells("NAME").Value)
                    End If
                Next
                arrHeader.Add("Location : " + strLoca)
            End If

            PageSetupReport_ID = GetReportID()
            transportSql.applyExportTemplate(gv1, PageSetupReport_ID)
            clsCommon.MyExportToPDF("Stock Reco (" + cboType.Text + ")", gv1, arrHeader, Me.Text, PageSetupReport_ID, objCommonVar.CurrentUserCode)

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub gvDetail_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles gvDetail.CellDoubleClick
        Try

            'If e.Column Is gv1.Columns("Item Code") OrElse e.Column Is gv1.Columns("Item Desc") Then
            '    Dim itemcode As String = ""
            '    itemcode = clsCommon.myCstr(gv1.CurrentRow.Cells("Item Code").Value)
            '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmItemMasterRMOther, itemcode)
            If e.Column Is gvDetail.Columns("Source_Doc_No") Then
                DrillDown()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
    ' Ticket No : TEC/16/08/19-000993 By Prabhakar
    Sub DrillDown()
        Try
            If gvDetail.CurrentRow.Index >= 0 Then
                If clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "BulkSRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmBulkMilkSRN, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "CRATE-REC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCrateReceviedDairySale, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "Disassembly") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmAssembDis, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "DispChallan") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCDispatch, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "FS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "PS-SH") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MCC-MSALE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmShipmentProductSale, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "FS-SR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "PS-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturndairy, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "IC-AD") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnStoreAdjustment, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "ISSTRAN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnIssueReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MCC-MSRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkSRN, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MilkTransferIn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferIn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "NRGP") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "RGP") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnGatePass, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "PP_ISSUE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionIssueEntry, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "PP_STD-FQC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ProcessProductionStandardizationFinalQC, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "PROD_ENTRY") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProductionEntry, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "SRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnSRN, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "SRN-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.SRNReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "Transfer") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.Transfer, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "Purchase Return") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.mbtnPurchaseReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "PRD_STG_PROC") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStageProcess, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MCC-MSR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCMaterialSaleReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "BulkSRNRet") = CompairStringResult.Equal Then
                    'No separate screen for display record
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "DisCanSale") = CompairStringResult.Equal Then
                    Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select CanSale_Doc_No from TSPL_CANSALE_DISPATCH_HEAD where Document_No='" + clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value) + "'"))
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmCanSale, clsCommon.myCstr(strDocNo))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "DispatchBS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSale, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "ScrapIn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSale, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "BulkSRNTrade") = CompairStringResult.Equal Then
                    'No separate screen for display record
                    'Dim strDocNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select ISNULL(tspl_bulk_milk_srn.Challan_No,'') AS Challan_No From tspl_bulk_milk_srn where tspl_bulk_milk_srn.srn_no='" + clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value) + "'"))
                    'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmDispatchBulkSaleTrade, strDocNo)
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "CSA-SALE") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASaleInvoice, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "CSA-SALEPATTI-RETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSASalePattiReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "DispatchBSTrade") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm("", clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                    ''''''''''''''''''''''''''''''''''''
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "DispChallanRet") = CompairStringResult.Equal Then
                    'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMCCTankerDispatchReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                    'ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "DispChallan-RET") = CompairStringResult.Equal Then
                    '    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.MCCDispatchReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "EX_SALE_IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmEXSalesInvoice, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "JWO-SRN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.JWO_SRN, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MCC-AISSUE") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MCC-ARETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPAssetIssue, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MCC-IISSUE") = CompairStringResult.Equal Then
                    'clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmVSPItemIssue, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MilkTransferInReturn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkTransferInReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MilkTransferJobWork") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransfer, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MilkTransJWOReturn") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmMilkJobWorkTransferReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MJ-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmJobMilkSRN, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "M-PURRETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmMilkPurchaseReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MS-SR") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.ScrapSaleRetrun, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "MT_SALE_IN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSalesInvoiceMT, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "PP_STDN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmProcessProductionStandardization, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "PROD_WR") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "Prod-Scrap") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmWreckageBooking, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))

                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "Sale Return") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSaleReturnProductSale, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "SaleReturnBS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmBulkSaleReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "SD-CSATRANS") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransfer, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "SD-CSATRANS-RETURN") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmCSATransferReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "TRN-RET") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.TransferReturn, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                ElseIf clsCommon.CompairString(clsCommon.myCstr(gvDetail.CurrentRow.Cells("Trans_Type").Value), "SI-MT") = CompairStringResult.Equal Then
                    clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.frmSiloMilkTransfer, clsCommon.myCstr(gvDetail.CurrentRow.Cells("Source_Doc_No").Value))
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub
End Class
