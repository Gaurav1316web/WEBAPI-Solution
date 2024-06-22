' Created Sanjay
Imports common
Public Class frmProductionUtilityCost
    Inherits FrmMainTranScreen
    Dim ButtonToolTip As ToolTip = New ToolTip()

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnprint.Visible = MyBase.isPrintFlag
    End Sub

    Sub LoadBom()
        Dim strquery As String = "select BOM_CODE as [Code],DESCRIPTION as [Description], BOM_DATE as [Date],REVISION_NO as [Revision No] from TSPL_PP_BOM_HEAD where 2=2 "
        cbgProduction.DataSource = clsDBFuncationality.GetDataTable(strquery)
        cbgProduction.ValueMember = "Code"
        cbgProduction.DisplayMember = "Description"
    End Sub

    Sub Print()

        Dim strOVERHEAD As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', ['+OVERHEAD+']' from (select distinct TSPL_OVERHEAD_COST.Description as OVERHEAD FROM TSPL_OVERHEAD_COST) XXX For XML Path('')),1,1,'') "))

        Dim ItemOVERHEADName As String = ""
        If clsCommon.myLen(strOVERHEAD) > 0 Then
            ItemOVERHEADName = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" Select STUFF((Select ', isnull (TBL_OVERHEAD.['+OVERHEAD+'],0) as ' +'['+ OVERHEAD+']' from (select distinct TSPL_OVERHEAD_COST.Description as OVERHEAD FROM TSPL_OVERHEAD_COST ) XXX For XML Path('')),1,1,'')   "))
            ItemOVERHEADName = "," + ItemOVERHEADName
        End If

        Dim qry As String = ""
        qry += " select aa.* " + ItemOVERHEADName + " from (select TSPL_PP_BOM_HEAD.BOM_CODE as [Code]
             ,DESCRIPTION as [Description], convert(varchar(12),BOM_DATE,103) as [Date],REVISION_NO as [Revision No]
             , convert(varchar(12),Valid_FROM_DATE,103) as [Start Date]
             ,convert(varchar(12),Valid_UPTO_DATE,103) as [End Date]
             ,STATUS as [Status], PROD_ITEM_CODE as [Prod. Item Code],convert (decimal(10,2),PROD_QUANTITY) as [Prod. Qty.], PROD_ITEM_UNIT_CODE as [UNIT] "
        qry += " from TSPL_PP_BOM_HEAD  "
        qry += " where 2=2"
        If chkProductionSelect.IsChecked AndAlso cbgProduction.CheckedValue.Count = 0 Then
            clsCommon.MyMessageBoxShow("Please select atleast one BOM")
            Return
        ElseIf chkProductionSelect.IsChecked AndAlso cbgProduction.CheckedValue.Count > 0 Then
            qry += " and BOM_CODE in (" + clsCommon.GetMulcallString(cbgProduction.CheckedValue) + ")"
        End If
        qry += " and convert(date,BOM_DATE,103) >= '" + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy") + "' and  "
        qry += "convert(date,BOM_DATE,103) <= '" + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy") + "'   "
        If clsCommon.myLen(txtProducedItem.Value) > 0 Then
            qry += "and PROD_ITEM_CODE = '" + clsCommon.myCstr(txtProducedItem.Value) + "' "
        End If
        qry += ")aa"

        If clsCommon.myLen(strOVERHEAD) > 0 Then
            qry += " left outer Join (select bom_code, " + strOVERHEAD + "   from (
select bom_code, Description , Sum (overhead_cost) as amount  from (
SELECT TSPL_PP_COST_WITHOUT_BATCH.bom_code,TSPL_OVERHEAD_COST.Description,TSPL_PP_COST_WITHOUT_BATCH.overhead_cost 
FROM TSPL_PP_COST_WITHOUT_BATCH
left outer join TSPL_OVERHEAD_COST on TSPL_OVERHEAD_COST.COST_CODE=TSPL_PP_COST_WITHOUT_BATCH.COST_CODE
WHERE 1=1
union all
select TSPL_PRODUCTION_UPLOADER_DETAIL.BOM_Code,TSPL_OVERHEAD_COST.Description,TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL.Amount as overhead_cost
from TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL
left outer join TSPL_PRODUCTION_UPLOADER_DETAIL on TSPL_PRODUCTION_UPLOADER_DETAIL.PK_ID=TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL.Against_PKID
left outer join TSPL_OVERHEAD_COST on TSPL_OVERHEAD_COST.COST_CODE=TSPL_PRODUCTION_UPLOADER_OVERHEAD_COST_DETAIL.COST_CODE
) XXX_OVERHEAD group by bom_code, Description
) XXX_OVERHEAD_FINAL
pivot ( sum(amount) for Description in (" + strOVERHEAD + ") ) piv) as TBL_OVERHEAD on TBL_OVERHEAD.bom_code = aa.[Code]"

        End If

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
        gv.GroupDescriptors.Clear()
        gv.MasterTemplate.SummaryRowsBottom.Clear()
        gv.EnableFiltering = True

        If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
            common.clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
            Exit Sub
        Else
            gv.DataSource = dt
            SetGridFormation()
            'For ii As Integer = 0 To gv.Columns.Count - 1
            '    gv.Columns(ii).ReadOnly = True
            '    gv.Columns(ii).Width = 100
            '    'gv.Columns(gv.Columns.Count - 1).Width = 500
            'Next
            gv.EnableGrouping = False
        End If

        gv.MasterTemplate.AllowAddNewRow = False
        RadPageView1.SelectedPage = RadPageViewPage2
    End Sub

    Sub SetGridFormation()
        gv.ShowGroupPanel = False
        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False

        gv.MasterTemplate.BestFitColumns()

        For I As Int16 = 0 To gv.Columns.Count - 1
            gv.Columns(I).BestFit()
            gv.Columns(I).ReadOnly = True
        Next

        gv.ReadOnly = True

        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.EnableFiltering = True
        gv.ShowFilteringRow = True
        gv.AllowDeleteRow = False
        gv.EnableAlternatingRowColor = True
        gv.Columns(0).IsCurrent = True
        gv.Focus()
        gv.BestFitColumns()
    End Sub

    Sub Reset()
        LoadBom()
        fromDate.Value = clsCommon.GETSERVERDATE().AddDays(-30)
        ToDate.Value = clsCommon.GETSERVERDATE()
        txtProducedItem.Value = Nothing
        lblMasterItemName.Text = ""
        gv.DataSource = Nothing
        gv.Columns.Clear()
        gv.Rows.Clear()
    End Sub

    Private Sub txtMasterItem__MYValidating1(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtProducedItem._MYValidating
        Try
            Dim qry As String = "SELECT ITEM_CODE AS CODE,ITEM_DESC AS ITEM_NAME,ITEM_TYPE AS TYPE FROM TSPL_ITEM_MASTER "
            txtProducedItem.Value = clsCommon.ShowSelectForm("TSPL_PP_BOM_HEAD", qry, "Code", "ITEM_TYPE IN ('F','O') ", txtProducedItem.Value, "", isButtonClicked)

            Dim DT_ITEM As DataTable
            Dim STRQ As String
            STRQ = "SELECT ITEM_DESC,ITEM_TYPE,UNIT_CODE FROM TSPL_ITEM_MASTER WHERE ITEM_CODE='" & txtProducedItem.Value & "'"
            DT_ITEM = clsDBFuncationality.GetDataTable(STRQ)
            If DT_ITEM.Rows.Count > 0 Then
                Me.lblMasterItemName.Text = DT_ITEM.Rows(0).Item("ITEM_DESC")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Print()
    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub frmProductionUtilityCost_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Reset()
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+S for Print ")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N Reset ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        SetUserMgmtNew()
    End Sub

    Private Sub frmProductionUtilityCost_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.P Then
            Print()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        ElseIf e.Alt And e.KeyCode = Keys.N Then
            Reset()
        End If
    End Sub

    Private Sub chkProductionAll_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkProductionAll.ToggleStateChanged
        cbgProduction.Enabled = chkProductionSelect.IsChecked
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        printdata(EnumExportTo.Excel)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        printdata(EnumExportTo.PDF)
    End Sub

    Public Enum EnumExportTo
        Excel = 0
        PDF = 1
    End Enum

    Public Sub printdata(ByVal exporter As EnumExportTo)
        Try
            Dim arrHeader As List(Of String) = New List(Of String)()
            Dim strtemp As String = ""
            arrHeader.Add(strtemp)
            arrHeader.Add("From Date : " + clsCommon.GetPrintDate(fromDate.Value, "dd/MMM/yyyy"))
            arrHeader.Add("To Date : " + clsCommon.GetPrintDate(ToDate.Value, "dd/MMM/yyyy"))

            If exporter = EnumExportTo.Excel Then
                clsCommon.MyExportToExcelGrid("List Of BOM", gv, arrHeader, Me.Text)
            Else
                clsCommon.MyExportToPDF("List Of BOM", gv, arrHeader, "List Of BOM", False)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


End Class
