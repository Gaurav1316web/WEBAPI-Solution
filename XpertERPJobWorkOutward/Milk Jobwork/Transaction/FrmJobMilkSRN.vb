'===========BM00000007790,Rohit========================
'============BM00000007632,BM00000007791,BM00000007790,BM00000008075=====================
Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmJobMilkSRN
    Inherits FrmMainTranScreen
    Dim isDocPosted As Boolean = False
    Dim validatefatsnf As Boolean = False
    Public Const colSlNo As String = "SLNO"
    Public Const colItemCode As String = "ItemCode"
    Public Const colItemDesc As String = "ItemDesc"
    Public Const colHSN_Code As String = "colHSN_Code"
    Public Const colQty As String = "Qty"
    Public Const colUOM As String = "UOM"
    Public Const colFat As String = "colFAT"

    Dim isCellValueChanged As Boolean = False

    Public Const colSNF As String = "colSNF"
    Public Const colFatKG As String = "colFATKG"
    Public Const colSNFKG As String = "colSNFKG"
    Public Const colGrossWeight As String = "colGrossWeight"
    Public Const colTareWeight As String = "colTareWeight"
    Public Const colNetWeight As String = "colNetWeight"
    Public Const colFatRate As String = "colFATRate"
    Public Const colSNFRate As String = "colSNFRate"
    Public Const colFatAmt As String = "colFatAmt"
    Public Const colSNFAmt As String = "colSNFAmt"
    Public Const colAmt As String = "colAmt"
    Public Const colMilkRate As String = "colMilkRate"
    Public Const colStandardRate As String = "colStandardRate"
    Public Const colNetRate As String = "colNetRate"
    Public Const colDeducRate As String = "colDeducRate"
    Public Const colIncenRate As String = "colIncenRate"
    Public Const colFinalMilkRate As String = "colFinalMilkRate"
    Public Const colDeduc As String = "colDeduc"
    Public Const colIncentive As String = "colIncentive"
    Public Const colSpecialDeduction As String = "colSpecialDeduction"
    Public Const colActAmt As String = "colActAmt"
    Public Const colBalance As String = "colBalance"
    Public Const colBalFatKG As String = "colBalFATKG"
    Public Const colBalSNFKG As String = "colBalSNFKG"
    Dim isCellValueChangedOpen As Boolean = False
    Dim isCellChangingUOM As Boolean
    Dim isInsideLoadData As Boolean = False
    Public fatColName As String = String.Empty
    Public snfColName As String = String.Empty
    Public errorControl As clsErrorControl = New clsErrorControl()
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim obj As clsJobMilkSRN = Nothing
    Public DocumentNo As String = ""
    Dim Conv_factor As Double = 0.0

    Sub Reset()
        isCellValueChangedOpen = True
        txtLocation.Enabled = True
        fndTankerNo.Enabled = True
        'isCellValueChangedOpen = False
        isCellChangingUOM = False
        fndSRNNo.Value = ""
        'fndWeighmentNo.Enabled = True
        Dim dt As Date = clsCommon.GETSERVERDATE(Nothing, "dd/MMM/yyyy hh:mm:ss tt")
        txtGateEntryNo.Text = ""
        dtpSRNDATE.Value = dt
        fndWeighmentNo.Value = ""
        dtpWeighmentDate.Value = dt
        TxtTankerNo.Text = Nothing
        FndLocation.Value = Nothing
        FndVendor.Value = Nothing
        txtVendor.Text = ""
        lblVendorName.Text = ""
        txtLocation.Text = clsGateEntry.getUsersDefaultLocation()
        lblLocationDesc.Text = clsLocation.GetName(txtLocation.Text, Nothing)
        txtChallanNo.Text = ""
        dtpChallanDate.Value = dt
        fndTankerNo.Value = ""
        lblTankerTransporterName.Text = ""
        fndPriceChart.Value = ""
        TxtFatWeightage.Text = "0"
        TxtSNFWeightage.Text = "0"
        txtfatPercentage.Text = "0"
        txtSNFPercentage.Text = "0"
        txtStanadardrate.Text = "0"
        txtTolerance.Text = "0"
        loadBlankItemGrid()
        txtQCNo.Text = ""
        dtpQCInTime.Value = dt
        dtpQCOutTime.Text = dt
        txtDipValue.Text = ""
        txtRemarks.Text = ""
        loadBlankParameterGrid()
        ''richa Against Ticket no .BM00000003725 on 05/08/2014
        loadBlankParameterGridwithRange()
        ''=============================================
        lblFATKg.Text = ""
        lblSNFKG.Text = ""
        lblFATRate.Text = ""
        lblSNFRate.Text = ""
        lblFatAmount.Text = ""
        lblSnfAmount.Text = ""
        lblTotalAmount.Text = ""
        lblTotalQtyValue.Text = ""
        lblStandardRate.Text = ""
        lblIncentive.Text = ""
        lblSpecialDeduction.Text = ""
        lblDeduction.Text = ""
        lblActualAmount.Text = ""
        btnSave.Enabled = True
        btnPrint.Enabled = False
        btnDelete.Enabled = False
        btnPost.Enabled = False
        btnSave.Text = "Save"
        fndSRNNo.MyReadOnly = False
        lblPending.Status = ERPTransactionStatus.Pending
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Hidden
        RadPageView1.Pages("RadPageViewPage2").Item.Visibility = ElementVisibility.Collapsed
        RadPageView1.SelectedPage = RadPageViewPage1
        fatColName = String.Empty
        snfColName = String.Empty
        FindAndRestoreGridLayout(Me)
        FindAndSetTabStopFalse(Me)
        isDocPosted = False
        lblReverse.Visible = False
        isCellValueChangedOpen = False
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        Me.Close()
        GC.Collect()
    End Sub

    Private Sub mnuSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSaveLayout.Click
        If clsCommon.myLen(MyBase.Form_ID) > 0 Then
            gvItem.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = MyBase.Form_ID & "gvItem"
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gvItem.SaveLayout(obj.GridLayout)
            obj.GridColumns = gvItem.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                gvItem.MasterTemplate.FilterDescriptors.Clear()
                Dim obj1 As New clsGridLayout()
                obj1.ReportID = MyBase.Form_ID & "gvParam"
                obj1.UserID = objCommonVar.CurrentUserCode
                obj1.GridLayout = New MemoryStream()
                gvParam.SaveLayout(obj1.GridLayout)
                obj1.GridColumns = gvParam.ColumnCount
                obj1.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                If obj1.SaveData() Then
                    ''richa Against Ticket no .BM00000003725 on 05/08/2014
                    Dim obj2 As New clsGridLayout()
                    obj2.ReportID = MyBase.Form_ID & "gvRange"
                    obj2.UserID = objCommonVar.CurrentUserCode
                    obj2.GridLayout = New MemoryStream()
                    gvRange.SaveLayout(obj2.GridLayout)
                    obj2.GridColumns = gvRange.ColumnCount
                    obj2.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                    If obj2.SaveData() Then
                        common.clsCommon.MyMessageBoxShow("Layout saved successfully", "Information")
                    End If
                    ''stuti regarding memory leakage
                    obj2.GridLayout.Close()
                    obj2.GridLayout.Dispose()
                    ''==================================================
                End If
                ''stuti regarding memory leakage
                obj1.GridLayout.Close()
                obj1.GridLayout.Dispose()
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub mnuDeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuDeleteLayout.Click
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvItem", objCommonVar.CurrentUserCode)
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvParam", objCommonVar.CurrentUserCode)
        ''richa Against Ticket no .BM00000003725 on 05/08/2014
        clsGridLayout.DeleteData(MyBase.Form_ID & "gvRange", objCommonVar.CurrentUserCode)
        ''=====================================================
        ReStoreGridLayout()
        common.clsCommon.MyMessageBoxShow("Layout Delete successfully", "Information")
    End Sub

    Private Sub mnuExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
        GC.Collect()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gvItem", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvItem.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvItem.Columns.Count - 1 Step ii + 1
                        gvItem.Columns(ii).IsVisible = False
                        gvItem.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvItem.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gvParam", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvParam.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvParam.Columns.Count - 1 Step ii + 1
                        gvParam.Columns(ii).IsVisible = False
                        gvParam.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvParam.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            ''richa Against Ticket no .BM00000003725 on 05/08/2014
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID & "gvRange", "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gvRange.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gvRange.Columns.Count - 1 Step ii + 1
                        gvRange.Columns(ii).IsVisible = False
                        gvRange.Columns(ii).VisibleInColumnChooser = True
                    Next
                    gvRange.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
            ''======================================================

        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub loadBlankItemGrid()

        gvItem.Rows.Clear()
        gvItem.Columns.Clear()
        gvItem.DataSource = Nothing

        gvItem.Columns.Add(colSlNo, "SL. NO.")
        gvItem.Columns(colSlNo).Width = 60
        gvItem.Columns(colSlNo).ReadOnly = True

        gvItem.Columns.Add(colItemCode, "Item Code")
        gvItem.Columns(colItemCode).Width = 100
        gvItem.Columns(colItemCode).ReadOnly = IIf(RdbManual.IsChecked, False, True)

        gvItem.Columns.Add(colItemDesc, "Item Desc")
        gvItem.Columns(colItemDesc).Width = 320
        gvItem.Columns(colItemDesc).ReadOnly = True

        gvItem.Columns.Add(colHSN_Code, "HSN Code")
        gvItem.Columns(colHSN_Code).Width = 100
        gvItem.Columns(colHSN_Code).ReadOnly = True

        gvItem.Columns.Add(colUOM, "UOM")
        gvItem.Columns(colUOM).Width = 120
        gvItem.Columns(colUOM).ReadOnly = False

        gvItem.Columns.Add(colBalFatKG, "Balance FAT (KG)")
        gvItem.Columns(colBalFatKG).Width = 75
        gvItem.Columns(colBalFatKG).ReadOnly = True
        gvItem.Columns(colBalFatKG).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colBalSNFKG, "Balance SNF (KG)")
        gvItem.Columns(colBalSNFKG).Width = 75
        gvItem.Columns(colBalSNFKG).ReadOnly = True
        gvItem.Columns(colBalSNFKG).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colBalance, "Balance Qty")
        gvItem.Columns(colBalance).Width = 75
        gvItem.Columns(colBalance).ReadOnly = True
        gvItem.Columns(colBalance).IsVisible = False
        gvItem.Columns(colBalance).VisibleInColumnChooser = False
        gvItem.Columns(colBalance).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colQty, "Qty")
        gvItem.Columns(colQty).Width = 75
        gvItem.Columns(colQty).ReadOnly = False
        gvItem.Columns(colQty).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colGrossWeight, "Gross Weight")
        gvItem.Columns(colGrossWeight).Width = 120
        gvItem.Columns(colGrossWeight).ReadOnly = IIf(RdbTankerReceipt.IsChecked, True, False)
        gvItem.Columns(colGrossWeight).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colTareWeight, "Tare Weight")
        gvItem.Columns(colTareWeight).Width = 120
        gvItem.Columns(colTareWeight).ReadOnly = IIf(RdbTankerReceipt.IsChecked, True, False)
        gvItem.Columns(colTareWeight).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colNetWeight, "Net Weight")
        gvItem.Columns(colNetWeight).Width = 120
        gvItem.Columns(colNetWeight).ReadOnly = True
        gvItem.Columns(colNetWeight).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFat, "FAT (%)")
        gvItem.Columns(colFat).Width = 75
        gvItem.Columns(colFat).ReadOnly = IIf(RdbTankerReceipt.IsChecked, True, False)
        gvItem.Columns(colFat).IsVisible = True
        gvItem.Columns(colFat).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNF, "SNF (%)")
        gvItem.Columns(colSNF).Width = 75
        gvItem.Columns(colSNF).ReadOnly = IIf(RdbTankerReceipt.IsChecked, True, False)
        gvItem.Columns(colSNF).IsVisible = True
        gvItem.Columns(colSNF).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFatKG, "FAT (KG)")
        gvItem.Columns(colFatKG).Width = 75
        gvItem.Columns(colFatKG).ReadOnly = True
        'gvItem.Columns(colFatKG).IsVisible = False
        gvItem.Columns(colFatKG).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNFKG, "SNF (KG)")
        gvItem.Columns(colSNFKG).Width = 75
        gvItem.Columns(colSNFKG).ReadOnly = True
        'gvItem.Columns(colSNFKG).IsVisible = False
        gvItem.Columns(colSNFKG).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFatRate, "FAT Rate")
        gvItem.Columns(colFatRate).Width = 100
        gvItem.Columns(colFatRate).ReadOnly = True
        gvItem.Columns(colFatRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNFRate, "SNF Rate")
        gvItem.Columns(colSNFRate).Width = 100
        gvItem.Columns(colSNFRate).ReadOnly = True
        gvItem.Columns(colSNFRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFatAmt, "FAT Amt")
        gvItem.Columns(colFatAmt).Width = 100
        gvItem.Columns(colFatAmt).ReadOnly = True
        gvItem.Columns(colFatAmt).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colSNFAmt, "SNF Amt")
        gvItem.Columns(colSNFAmt).Width = 100
        gvItem.Columns(colSNFAmt).ReadOnly = True
        gvItem.Columns(colSNFAmt).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colMilkRate, "Basic Rate")
        gvItem.Columns(colMilkRate).Width = 150
        gvItem.Columns(colMilkRate).ReadOnly = False
        gvItem.Columns(colMilkRate).TextAlignment = ContentAlignment.MiddleRight


        gvItem.Columns.Add(colStandardRate, "Standard Rate")
        gvItem.Columns(colStandardRate).Width = 150
        gvItem.Columns(colStandardRate).ReadOnly = True
        gvItem.Columns(colStandardRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colDeducRate, "Deduction Per Unit")
        gvItem.Columns(colDeducRate).Width = 150
        gvItem.Columns(colDeducRate).ReadOnly = True
        gvItem.Columns(colDeducRate).TextAlignment = ContentAlignment.MiddleRight


        gvItem.Columns.Add(colIncenRate, "Incentive Per Unit")
        gvItem.Columns(colIncenRate).Width = 150
        gvItem.Columns(colIncenRate).ReadOnly = True
        gvItem.Columns(colIncenRate).TextAlignment = ContentAlignment.MiddleRight



        gvItem.Columns.Add(colAmt, "Amount")
        gvItem.Columns(colAmt).Width = 150
        gvItem.Columns(colAmt).ReadOnly = True
        gvItem.Columns(colAmt).IsVisible = False
        gvItem.Columns(colAmt).TextAlignment = ContentAlignment.MiddleRight




        gvItem.Columns.Add(colDeduc, "Deduction")
        gvItem.Columns(colDeduc).Width = 100
        gvItem.Columns(colDeduc).ReadOnly = True
        gvItem.Columns(colDeduc).IsVisible = False
        gvItem.Columns(colDeduc).TextAlignment = ContentAlignment.MiddleRight



        gvItem.Columns.Add(colIncentive, "Incentive")
        gvItem.Columns(colIncentive).Width = 100
        gvItem.Columns(colIncentive).ReadOnly = True
        gvItem.Columns(colIncentive).IsVisible = False
        gvItem.Columns(colIncentive).TextAlignment = ContentAlignment.MiddleRight



        ''richa Against Ticket No.BM00000003719 on 04/09/2014
        gvItem.Columns.Add(colSpecialDeduction, "Special Deduction")
        gvItem.Columns(colSpecialDeduction).Width = 100
        gvItem.Columns(colSpecialDeduction).ReadOnly = True
        gvItem.Columns(colSpecialDeduction).TextAlignment = ContentAlignment.MiddleRight
        '-------------------------------------------------

        gvItem.Columns.Add(colNetRate, "Net Rate")
        gvItem.Columns(colNetRate).Width = 150
        gvItem.Columns(colNetRate).ReadOnly = True
        gvItem.Columns(colNetRate).TextAlignment = ContentAlignment.MiddleRight

        gvItem.Columns.Add(colFinalMilkRate, "Final Milk Rate")
        gvItem.Columns(colFinalMilkRate).Width = 150
        gvItem.Columns(colFinalMilkRate).ReadOnly = True
        gvItem.Columns(colFinalMilkRate).TextAlignment = ContentAlignment.MiddleRight


        gvItem.Columns.Add(colActAmt, "Amount")
        gvItem.Columns(colActAmt).Width = 75
        gvItem.Columns(colActAmt).ReadOnly = True
        gvItem.Columns(colActAmt).TextAlignment = ContentAlignment.MiddleRight



        gvItem.Rows.AddNew()
        gvItem.Rows(0).Cells(colSlNo).Value = "1"
        gvItem.AllowAddNewRow = False
        gvItem.AllowDeleteRow = False
        gvItem.AllowRowReorder = False
        gvItem.ShowGroupPanel = False
        gvItem.EnableFiltering = False
        gvItem.EnableSorting = False
        gvItem.EnableGrouping = False
        gvItem.AllowColumnChooser = True
        gvItem.AllowColumnReorder = True
        ReStoreGridLayout()

    End Sub

    Sub loadBlankParameterGrid()
        isInsideLoadData = True
        If clsCommon.myLen(txtLocation.Text) <= 0 Then
            gvParam.Rows.Clear()
            gvParam.Columns.Clear()
            gvParam.DataSource = Nothing
            Exit Sub
        End If
        Dim whrCls As String = String.Empty
        If clsERPFuncationality.isLocationMcc(txtLocation.Text) Then
            whrCls = " where Param_for='MCC' or Param_for='BOTH'"
        Else
            whrCls = " where Param_for='PLANT' or Param_for='BOTH'"
        End If
        Dim pFields As Boolean = True
        Dim gridWidth As Integer = 60
        Dim qry As String = " select *,case when Type='NA' then 1 when  Type='FAT' then 2 when Type='SNF' then 3 when Type='CLR' then 4 when Type='OTHERS' then 5 else 6 end as Ordering from tspl_parameter_Master " & whrCls & "  order by Ordering "
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
            pFields = True
        Else
            pFields = False
        End If
        gvParam.Rows.Clear()
        gvParam.Columns.Clear()
        gvParam.DataSource = Nothing
        gvParam.Columns.Add("colSLNO", "SL. No.")
        gvParam.Columns("colSLNO").Width = 60
        gvParam.Columns("colSLNO").ReadOnly = True
        gvParam.Columns("colSLNO").Tag = "SLNO"
        If pFields Then

            For i As Integer = 0 To dt.Rows.Count() - 1
                gvParam.Columns.Add(dt.Rows(i)("Code"), dt.Rows(i)("Description"))
                gvParam.Columns(dt.Rows(i)("Code")).Width = 120
                gvParam.Columns(dt.Rows(i)("Code")).ReadOnly = True
                gvParam.Columns(dt.Rows(i)("Code")).Tag = dt.Rows(i)("Type")
                If clsCommon.CompairString(dt.Rows(i)("Type"), "FAT") = CompairStringResult.Equal Then
                    fatColName = dt.Rows(i)("Code")
                End If
                If clsCommon.CompairString(dt.Rows(i)("Type"), "SNF") = CompairStringResult.Equal Then
                    snfColName = dt.Rows(i)("Code")
                End If
            Next
        Else
            Throw New Exception("There is No parameter defined in Parameter Master. Please Define atleast FAT and SNF")
        End If
        gvParam.Rows.AddNew()
        gvParam.Rows(0).Cells("colSLNO").Value = "1"
        gvParam.AllowAddNewRow = False
        gvParam.AllowDeleteRow = False
        gvParam.AllowRowReorder = False
        gvParam.ShowGroupPanel = False
        gvParam.EnableFiltering = False
        gvParam.EnableSorting = False
        gvParam.EnableGrouping = False
        gvParam.AllowColumnChooser = True
        ReStoreGridLayout()
        isInsideLoadData = False
    End Sub
    ''richa Against Ticket no .BM00000003725 on 05/08/2014
    Sub loadBlankParameterGridwithRange()
        'Dim whrCls As String = String.Empty
        'If clsERPFuncationality.isCurrentUserMCC Then
        '    whrCls = " where Param_for='MCC' or Param_for='BOTH'"
        'Else
        '    whrCls = " where Param_for='PLANT' or Param_for='BOTH'"
        'End If
        'Dim pFields As Boolean = True
        'Dim gridWidth As Integer = 60
        ''Dim qry As String = "Select TSPL_PARAMETER_MASTER.Code, TSPL_PARAMETER_MASTER.Description as [Type]  from TSPL_PARAMETER_RANGE_MASTER Left Outer Join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_RANGE_MASTER.Code=TSPL_PARAMETER_MASTER.Code where TSPL_PARAMETER_RANGE_MASTER.Code in (Select Code from TSPL_PARAMETER_MASTER ) order by TSPL_PARAMETER_MASTER.Code  "
        '' Dim qry As String = "Select max(TSPL_PARAMETER_MASTER.Code) as Code, Max(TSPL_PARAMETER_MASTER.Description) as [Type],Max(TSPL_PARAMETER_RANGE_MASTER.Effective_Date),case when TSPL_PARAMETER_MASTER.Type='FAT' then 1 when TSPL_PARAMETER_MASTER.Type='SNF' then 2  when TSPL_PARAMETER_MASTER.type='CLR' then 3 else 4 end as ordering   from TSPL_PARAMETER_RANGE_MASTER Left Outer Join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_RANGE_MASTER.Code=TSPL_PARAMETER_MASTER.Code where TSPL_PARAMETER_RANGE_MASTER.Code in (Select Code from TSPL_PARAMETER_MASTER where Nature='R' ) group by TSPL_PARAMETER_RANGE_MASTER.Code order by TSPL_PARAMETER_RANGE_MASTER.Code "
        'Dim qry As String = "select xx.*,case when TSPL_PARAMETER_MASTER.Type='FAT' then 1 when TSPL_PARAMETER_MASTER.Type='SNF' then 2  when TSPL_PARAMETER_MASTER.type='CLR' then 3 else 4 end as ordering  from (Select max(TSPL_PARAMETER_MASTER.Code) as Code, Max(TSPL_PARAMETER_MASTER.Description) as [Type],Max(TSPL_PARAMETER_RANGE_MASTER.Effective_Date) as effective_date from TSPL_PARAMETER_RANGE_MASTER Left Outer Join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_RANGE_MASTER.Code=TSPL_PARAMETER_MASTER.Code where TSPL_PARAMETER_RANGE_MASTER.Code in (Select Code from TSPL_PARAMETER_MASTER where Nature='R' ) group by TSPL_PARAMETER_RANGE_MASTER.Code  ) xx left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=xx.Code " & whrCls & " order by ordering "
        'Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        'If dt.Rows.Count > 0 AndAlso dt IsNot Nothing Then
        '    pFields = True
        'Else
        '    pFields = False
        'End If
        gvRange.Rows.Clear()
        gvRange.Columns.Clear()
        gvRange.DataSource = Nothing
        'gvRange.Columns.Add("colSLNO", "SL. No.")
        'gvRange.Columns("colSLNO").Width = 60
        'gvRange.Columns("colSLNO").ReadOnly = True
        'gvRange.Columns("colSLNO").Tag = "SLNO"
        'If pFields Then

        '    For i As Integer = 0 To dt.Rows.Count() - 1
        '        gvRange.Columns.Add(dt.Rows(i)("Code") + "LR", dt.Rows(i)("Type") + " Lower Range")
        '        gvRange.Columns(dt.Rows(i)("Code") + "LR").Width = 90
        '        gvRange.Columns(dt.Rows(i)("Code") + "LR").ReadOnly = True
        '        gvRange.Columns(dt.Rows(i)("Code") + "LR").Tag = dt.Rows(i)("Type") + "Lower Range"
        '        gvRange.Columns(dt.Rows(i)("Code") + "LR").WrapText = True

        '        gvRange.Columns.Add(dt.Rows(i)("Code") + "UR", dt.Rows(i)("Type") + " Upper Range")
        '        gvRange.Columns(dt.Rows(i)("Code") + "UR").Width = 90
        '        gvRange.Columns(dt.Rows(i)("Code") + "UR").ReadOnly = True
        '        gvRange.Columns(dt.Rows(i)("Code") + "UR").Tag = dt.Rows(i)("Type") + "Upper Range"
        '        gvRange.Columns(dt.Rows(i)("Code") + "UR").WrapText = True

        '        gvRange.Columns.Add(dt.Rows(i)("Code") + "Value", dt.Rows(i)("Type") + " Value")
        '        gvRange.Columns(dt.Rows(i)("Code") + "Value").Width = 90
        '        gvRange.Columns(dt.Rows(i)("Code") + "Value").ReadOnly = True
        '        gvRange.Columns(dt.Rows(i)("Code") + "Value").Tag = dt.Rows(i)("Type") + "Value"
        '        gvRange.Columns(dt.Rows(i)("Code") + "Value").WrapText = True
        '    Next
        'End If

        gvRange.Columns.Add("Code", "Parameter")
        gvRange.Columns("Code").Width = 250
        gvRange.Columns("Code").ReadOnly = True

        gvRange.Columns.Add("LowerRange", "Lower Range")
        gvRange.Columns("LowerRange").Width = 90
        gvRange.Columns("LowerRange").ReadOnly = True

        gvRange.Columns.Add("UpperRange", "Upper Range")
        gvRange.Columns("UpperRange").Width = 90
        gvRange.Columns("UpperRange").ReadOnly = True

        gvRange.Columns.Add("Value", "Value")
        gvRange.Columns("Value").Width = 90
        gvRange.Columns("Value").ReadOnly = True

        gvRange.Columns.Add("QcValue", "QC Value")
        gvRange.Columns("QcValue").Width = 90
        gvRange.Columns("QcValue").ReadOnly = True


        gvRange.Columns.Add("IncentiveDeduction", "Incentive/Deduction")
        gvRange.Columns("IncentiveDeduction").Width = 200
        gvRange.Columns("IncentiveDeduction").ReadOnly = True


        'gvRange.Rows.AddNew()
        'gvRange.Rows(0).Cells("colSLNO").Value = "1"
        gvRange.AllowAddNewRow = False
        gvRange.AllowDeleteRow = False
        gvRange.AllowRowReorder = False
        gvRange.ShowGroupPanel = False
        gvRange.EnableFiltering = False
        gvRange.EnableSorting = False
        gvRange.EnableGrouping = False
        ReStoreGridLayout()
    End Sub
    ''=====================================================================
    Private Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReset.Click
        Reset()
    End Sub
    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmJobMilkSRN)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub FrmJobMilkSRN_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnReset.Enabled Then
            btnReset_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso btnSave.Enabled AndAlso MyBase.isModifyFlag Then
            btnSave_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso btnPost.Enabled AndAlso MyBase.isPostFlag Then
            btnPost_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso btnDelete.Enabled AndAlso MyBase.isDeleteFlag Then
            btnDelete_Click(sender, e)
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C AndAlso btnClose.Enabled Then
            btnClose_Click(sender, e)
        End If
    End Sub

    Private Sub FrmJobMilkSRN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        validatefatsnf = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.ValidateFatSNFOnJobMilkSRN, clsFixedParameterCode.ValidateFatSNFOnJobMilkSRN, Nothing)) = "1", True, False))
        Reset()
        MyBase.ReStoreGridLayoutMain(Me)
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D To Delete ")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C To Close the Window")
        ButtonToolTip.SetToolTip(btnReset, "Press Alt+N For New")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P to Post the Transaction")
        If clsCommon.myLen(DocumentNo) > 0 Then
            LoadData(DocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub

    Sub SaveData(ByVal isPost As Boolean)
        Dim trans As SqlTransaction = Nothing
        Try
            obj = New clsJobMilkSRN()
            If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                obj.isNewEntry = True
            Else
                obj.isNewEntry = False
            End If
            trans = clsDBFuncationality.GetTransactin()
            Dim dt As Date = clsCommon.GETSERVERDATE(trans, "dd/MMM/yyyy hh:mm:ss tt")
            If obj.isNewEntry Then
                If RdbManual.IsChecked = True Then
                    obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dtpSRNDATE.Value, clsDocType.JobMilkSRN, "Manual", txtLocation.Text)
                ElseIf RdbSkuReceipt.IsChecked = True Then
                    obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dtpSRNDATE.Value, clsDocType.JobMilkSRN, "SKU Receipt", txtLocation.Text)
                ElseIf RdbTankerReceipt.IsChecked = True Then
                    obj.SRN_NO = clsERPFuncationality.GetNextCode(trans, dtpSRNDATE.Value, clsDocType.JobMilkSRN, "Tanker", txtLocation.Text)
                End If

                If clsCommon.myLen(obj.SRN_NO) <= 0 Then
                    Throw New Exception("Error In SRN  No Genertion")
                    Exit Sub
                End If
                'If clsCommon.MyMessageBoxShow("Want to Create Auto PO ?", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                obj.PO_NO = clsERPFuncationality.GetNextCode(trans, dt, clsDocType.BulkMilkPO, "", txtLocation.Text)
                If clsCommon.myLen(obj.PO_NO) <= 0 Then
                    Throw New Exception("Error In PO  No Genertion")
                    Exit Sub
                End If
                obj.PO_Date = dtpSRNDATE.Value
                'End If

            Else
                obj.SRN_NO = clsCommon.myCstr(fndSRNNo.Value)
            End If
            obj.isApproved = 0
            fndSRNNo.Value = obj.SRN_NO
            obj.SRN_Date = dtpSRNDATE.Value
            obj.Gate_Entry_No = txtGateEntryNo.Text
            obj.Weighment_No = fndWeighmentNo.Value
            obj.Weighment_Date = dtpWeighmentDate.Value
            obj.Vendor_Code = txtVendor.Text
            obj.Loc_Code = txtLocation.Text
            obj.TANKER_SKU_MANUAL = IIf(RdbTankerReceipt.IsChecked, 1, IIf(RdbSkuReceipt.IsChecked, 2, 3))
            obj.Challan_No = txtChallanNo.Text
            obj.Challan_Date = dtpChallanDate.Value
            obj.Tanker_No = fndTankerNo.Value
            obj.Price_Code = fndPriceChart.Value
            obj.QC_No = txtQCNo.Text
            obj.Qc_Date = dtpQCInTime.Value
            If RdbManual.IsChecked Then
                obj.Tanker_No = clsCommon.myCstr(TxtTankerNo.Text)
            End If
            If Not isPost Then
                obj.isPosted = 0
            End If
            obj.Item_Code = gvItem.Rows(0).Cells(colItemCode).Value
            obj.Item_Desc = gvItem.Rows(0).Cells(colItemDesc).Value
            obj.UOM = gvItem.Rows(0).Cells(colUOM).Value
            obj.Gross_Weight = gvItem.Rows(0).Cells(colGrossWeight).Value
            obj.Tare_Weight = gvItem.Rows(0).Cells(colTareWeight).Value
            obj.Net_Weight = gvItem.Rows(0).Cells(colNetWeight).Value
            obj.Qty = gvItem.Rows(0).Cells(colQty).Value
            obj.snf_Per = gvItem.Rows(0).Cells(colSNF).Value
            obj.fat_per = gvItem.Rows(0).Cells(colFat).Value
            obj.fat_KG = gvItem.Rows(0).Cells(colFatKG).Value
            obj.SNF_KG = gvItem.Rows(0).Cells(colSNFKG).Value
            obj.fat_Rate = gvItem.Rows(0).Cells(colFatRate).Value
            obj.SNF_Rate = gvItem.Rows(0).Cells(colSNFRate).Value
            obj.Amount = gvItem.Rows(0).Cells(colAmt).Value
            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            obj.SpecialDeduction = clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value)
            ''================================================
            obj.Deduction = gvItem.Rows(0).Cells(colDeducRate).Value
            obj.Incentive = gvItem.Rows(0).Cells(colIncenRate).Value
            obj.Actual_Amount = gvItem.Rows(0).Cells(colActAmt).Value
            obj.Balance = gvItem.Rows(0).Cells(colBalance).Value
            obj.BalanceFatKg = gvItem.Rows(0).Cells(colBalFatKG).Value
            obj.BalanceSNFKg = gvItem.Rows(0).Cells(colBalSNFKG).Value
            obj.BasicRate = gvItem.Rows(0).Cells(colMilkRate).Value
            obj.Standardrate = gvItem.Rows(0).Cells(colStandardRate).Value
            obj.NetRate = gvItem.Rows(0).Cells(colNetRate).Value

            obj.FatAmt = gvItem.Rows(0).Cells(colFatAmt).Value
            obj.SnfAmt = gvItem.Rows(0).Cells(colSNFAmt).Value
            obj.FinalMilkRate = gvItem.Rows(0).Cells(colFinalMilkRate).Value

            obj.Modify_By = objCommonVar.CurrentUserCode
            obj.Modify_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            obj.comp_code = objCommonVar.CurrentCompanyCode
            If obj.isNewEntry Then
                obj.Created_By = objCommonVar.CurrentUserCode
                obj.Created_Date = clsCommon.GetPrintDate(dt, "dd/MM/yyyy hh:mm:ss tt")
            End If
            If gvRange IsNot Nothing AndAlso gvRange.Rows.Count > 0 Then
                Dim i As Integer = 0
                Dim objParam As New clsJobSRNParam
                obj.arrObj = New List(Of clsJobSRNParam)
                For i = 0 To gvRange.Rows.Count - 1
                    objParam = New clsJobSRNParam
                    objParam.SRN_No = clsCommon.myCstr(obj.SRN_NO)
                    objParam.Parameter = clsCommon.myCstr(gvRange.Rows(i).Cells("Code").Value)
                    objParam.Lower_Range = clsCommon.myCstr(gvRange.Rows(i).Cells("LowerRange").Value)
                    objParam.Upper_Range = clsCommon.myCstr(gvRange.Rows(i).Cells("UpperRange").Value)
                    objParam.value = clsCommon.myCstr(gvRange.Rows(i).Cells("Value").Value)
                    objParam.QCValue = clsCommon.myCstr(gvRange.Rows(i).Cells("QcValue").Value)
                    objParam.Incen_Deduc = clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value)
                    obj.arrObj.Add(objParam)
                Next
            End If
            If clsJobMilkSRN.saveData(obj, trans) Then
                trans.Commit()
                If Not isPost Then
                    If clsCommon.CompairString(btnSave.Text, "Save") = CompairStringResult.Equal Then
                        clsCommon.MyMessageBoxShow("Data Saved Successfully")
                    Else
                        clsCommon.MyMessageBoxShow("Data Updated Successfully")
                    End If
                End If
                LoadData(obj.SRN_NO, NavigatorType.Current)
                Exit Sub
            End If
            'clsCommon.MyMessageBoxShow("Data Not Saved ")
            'btnSave.Text = "Save"
            'btnDelete.Enabled = False
            'btnPost.Enabled = False
            'btnPrint.Enabled = False
            'fndSRNNo.MyReadOnly = False
            'trans.Rollback()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function allowToSave() As Boolean
        Try
            '===================Added by preeti Gupta==============
            If AllowFutureDateTransaction(dtpSRNDATE.Value, Nothing) = False Then
                dtpSRNDATE.Select()
                Return False
            End If
            '===========================================================
            If clsCommon.myLen(fndTankerNo.Value) <= 0 And clsCommon.myLen(TxtTankerNo.Text) <= 0 Then
                Throw New Exception("Please Select Tanker No")
                errorControl.SetError(fndTankerNo, "Please Select Tanker No")
            Else
                errorControl.ResetError(fndTankerNo)
            End If
            If RdbManual.CheckState = CheckState.Unchecked And RdbSkuReceipt.CheckState = CheckState.Unchecked And RdbTankerReceipt.CheckState = CheckState.Unchecked Then
                Throw New Exception("Please Select Tanker No or SKU or Manual")
                errorControl.SetError(RdbTankerReceipt, "Please Select Tanker No or SKU or Manual")
            Else
                errorControl.ResetError(RdbTankerReceipt)
            End If
            'If clsCommon.myLen(fndPriceChart.Value) <= 0 Then
            '    Throw New Exception("Please Select a Price Chart")
            '    errorControl.SetError(fndPriceChart, "Please Select a Price Chart")
            'Else
            '    errorControl.ResetError(fndPriceChart)
            'End If

            If clsCommon.myLen(gvItem.Rows(0).Cells(colMilkRate)) <= 0 Then
                Throw New Exception("Please enter Basic Rate")
            End If

            If clsCommon.myCDate(dtpChallanDate.Value, "dd/MMM/yyyy") > clsCommon.myCDate(dtpSRNDATE.Value, "dd/MMM/yyyy") Then
                Throw New Exception("SRN Date should not be smaller than Challan date")
                errorControl.SetError(dtpSRNDATE, "SRN Date should not be smaller than Challan date")
            Else
                errorControl.ResetError(dtpSRNDATE)
            End If
            If clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.MilkProc, clsFixedParameterCode.AllowSRNDateAfterCurrentDate, Nothing)) = 0 Then
                Dim dt As Date = clsCommon.GETSERVERDATE()
                If clsCommon.myCDate(dtpSRNDATE.Value, "dd/MMM/yyyy hh:mm:ss tt") > clsCommon.myCDate(dt, "dd/MMM/yyyy hh:mm:ss tt") Then
                    dtpSRNDATE.Value = dt
                    Throw New Exception("SRN Date should not be Larger than current date")
                    errorControl.SetError(dtpSRNDATE, "SRN Date should not be Larger than current date")
                Else
                    errorControl.ResetError(dtpSRNDATE)
                End If
            End If


            'Dim chk As Integer = 0
            'chk = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("   select count(*) from TSPL_Job_MILK_SRN where gate_entry_no='" & txtGateEntryNo.Text & "' and srn_no <>'" & fndSRNNo.Value & "' and isnull(srn_return_no,'')='' "))
            'If chk > 0 Then
            '    Throw New Exception("The Same Tanker you have selected is Already used in other SRN.")
            'End If

            For Each row As GridViewRowInfo In gvItem.Rows
                If clsCommon.myLen(clsCommon.myCstr(row.Cells(colItemCode).Value)) > 0 Then
                    If clsCommon.myCdbl(row.Cells(colBalance).Value) < (clsCommon.myCdbl(row.Cells(colFatKG).Value) + clsCommon.myCdbl(row.Cells(colSNFKG).Value)) Then
                        If clsCommon.MyMessageBoxShow("Please Check FAT(KG) and SNF(KG).it is going greater then balance qty.Do you want to save it.", "Alert", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then 'clsCommon.MyMessageBoxShow("Please Check net weight.it should not be greater then balance qty.")
                            Return False
                        End If
                        'Return False
                    End If
                    If clsCommon.myCdbl(row.Cells(colQty).Value) <= 0 Then
                        clsCommon.MyMessageBoxShow("Please Fill Qty.it should not be greater then 0.")
                        Return False
                    End If
                End If
            Next
            Return True

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If allowToSave() Then
            SaveData(False)
        End If

    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub
    Sub DeleteData()
        If clsCommon.myLen(fndSRNNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow("Please Enter SRN No To delete ")
        Else
            'Dim isUsed As Integer = clsDBFuncationality.getSingleValue("select SUM(row_Count ) from (select COUNT(*) as row_Count from  TSPL_Milk_Weighment_Detail where gate_entry_no='" & fndGateEntryNO.Value & "' union all select COUNT(*) as row_Count from tspl_Milk_quality_check where gate_entry_no='" & fndGateEntryNO.Value & "') xx ")
            'If isUsed > 0 Then
            'clsCommon.MyMessageBoxShow("Gate Entry No is in use")
            'Exit Sub
            'End If
            If myMessages.deleteConfirm() Then
                If clsJobMilkSRN.deleteData(fndSRNNo.Value, Nothing) Then
                    Reset()
                    myMessages.delete()
                End If
            End If
        End If
    End Sub
    ''Updateed by Preeti Gupta ticket no[BM00000004915]
    Sub printData(ByVal SRNNo As String)
        'If clsCommon.myLen(SRNNo) > 0 Then
        '    Dim strQuery As String = "select  TSPL_Job_MILK_SRN.Created_By,TSPL_Job_MILK_SRN.Modify_By , TSPL_COMPANY_MASTER.Comp_Code,TSPL_COMPANY_MASTER.Comp_Name  as comp_Name,TSPL_LOCATION_MASTER.Add1 as Loc_Add1,TSPL_LOCATION_MASTER.Add2 as Loc_Add2,TSPL_LOCATION_MASTER.Add3 as Loc_add3,TSPL_LOCATION_MASTER.TIN_No as Loc_Tin_No,TSPL_LOCATION_MASTER.Pin_Code as Loc_Pin_Code ,case when ISNULL(TSPL_LOCATION_MASTER.Phone1,'')='(+__)__________' then '' else TSPL_LOCATION_MASTER.Phone1 end +  Case When   ISNULL(TSPL_LOCATION_MASTER.Phone2,'')<>'(+__)__________' Then ', '+ TSPL_LOCATION_MASTER.Phone2 Else'' End as  Loc_Phn ,TSPL_Job_MILK_SRN.SRN_NO ,convert(varchar,TSPL_Job_MILK_SRN.SRN_Date,103) as SRN_Date ,TSPL_VENDOR_MASTER.Vendor_Name,TSPL_Job_MILK_SRN.Tanker_No,TSPL_ITEM_MASTER.Item_Desc ,TSPL_Job_MILK_SRN.Net_Weight ,TSPL_Job_MILK_SRN.fat_per ,TSPL_Job_MILK_SRN.snf_Per,TSPL_Job_MILK_SRN.UOM ,t_Acidity.Param_Field_Value as Acidity  ,TSPL_LOCATION_MASTER.add1 +case when len(TSPL_LOCATION_MASTER.add2)>0 then ', '+TSPL_LOCATION_MASTER.add2 else '' end +case when LEN(isnull(TSPL_LOCATION_MASTER.Add3,''))>0 then ', '+isnull(TSPL_LOCATION_MASTER.Add3,'') else ' ' end + case when LEN(TSPL_LOCATION_MASTER.City_Code)>0 then ', '+TSPL_LOCATION_MASTER.City_Code else ' ' end + case when len(TSPL_LOCATION_MASTER.State )>0 then TSPL_LOCATION_MASTER.State else '' end  as Loc_address from TSPL_Job_MILK_SRN"

        '    strQuery += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER .Location_Code =TSPL_Job_MILK_SRN.Loc_Code "
        '    strQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER .Comp_Code =TSPL_COMPANY_MASTER .Comp_Code "
        '    strQuery += " left outer join TSPL_VENDOR_MASTER  on TSPL_VENDOR_MASTER .Vendor_Code =TSPL_Job_MILK_SRN.Vendor_Code "
        '    strQuery += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER .Item_Code =TSPL_Job_MILK_SRN.Item_Code  "
        '    strQuery += " Left Outer Join (Select TSPL_QC_Parameter_Detail.*    From TSPL_Job_MILK_SRN      Left Outer Join TSPL_QC_Parameter_Detail On TSPL_QC_Parameter_Detail.QC_No        = TSPL_Job_MILK_SRN.QC_No And TSPL_QC_Parameter_Detail.Param_Type = 'Acidity') t_Acidity On t_Acidity.QC_No = TSPL_Job_MILK_SRN.QC_No"
        '    strQuery += " where  TSPL_Job_MILK_SRN.SRN_NO='" & SRNNo & "'"


        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(strQuery)
        '    frmCrystalReportViewer.funreport(CrystalReportFolder.MilkProcurement, dt, "rptBulkMilkSRN", "Milk SRN")
        'Else
        '    clsCommon.MyMessageBoxShow("Please select an invoice to print")
        'End If
    End Sub
    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        printData(fndSRNNo.Value)
    End Sub

    Private Sub btnPost_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPost.Click
        postData()
    End Sub
    Sub postData()
        Try
            Dim qry As String = ""
            Dim msg As String = ""
            Dim dt As DataTable = Nothing
            If (myMessages.postConfirm()) Then
                If Not allowToSave() Then
                    Exit Sub
                End If
                SaveData(True)
                If (clsJobMilkSRN.postData(fndSRNNo.Value, Me.Form_ID)) Then
                    msg = "Successfully Posted"
                Else
                    qry = "select No_Of_Level, LEVEL from TSPL_APPROVAL_LEVEL_SCREEN where User_Code='" + objCommonVar.CurrentUserCode + "' and Trans_Code='" + MyBase.Form_ID + "' "
                    dt = clsDBFuncationality.GetDataTable(qry)
                    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                        Dim level As String = dt.Rows(0)("LEVEL").ToString()
                        Dim NoOflevel As Integer = clsCommon.myCdbl(dt.Rows(0)("No_Of_Level"))
                        If clsCommon.CompairString(level, "Level1") = CompairStringResult.Equal Then
                            msg = "Level 1 Approval done. "
                            If NoOflevel = 1 Then
                                msg += "Successfully Posted. "
                            Else
                                msg += "Level 2 Approval Required."
                            End If
                        ElseIf clsCommon.CompairString(level, "Level2") = CompairStringResult.Equal Then
                            msg = "Level 2 Approval done. "
                            If NoOflevel = 2 Then
                                msg += "Successfully Posted "
                            Else
                                msg += "Level 3 Approval Required."
                            End If
                        Else
                            msg = "Level 3 Approval done. Successfully Posted. "
                        End If
                    End If
                End If
                common.clsCommon.MyMessageBoxShow(msg)
                LoadData(fndSRNNo.Value, NavigatorType.Current)
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Sub LoadData(ByVal strSrnNo As String, ByVal nav As NavigatorType)
        isInsideLoadData = True
        obj = clsJobMilkSRN.getData(strSrnNo, nav)

        If obj IsNot Nothing Then
            isDocPosted = IIf(obj.isPosted = 0, False, True)
            txtLocation.Enabled = False
            fndSRNNo.Value = obj.SRN_NO
            dtpSRNDATE.Value = obj.SRN_Date
            fndWeighmentNo.Value = obj.Weighment_No
            dtpWeighmentDate.Value = obj.Weighment_Date
            txtQCNo.Text = obj.QC_No
            txtGateEntryNo.Text = obj.Gate_Entry_No
            txtVendor.Text = obj.Vendor_Code
            FndVendor.Value = obj.Vendor_Code
            lblVendorName.Text = clsVendorMaster.GetName(obj.Vendor_Code, Nothing)
            txtLocation.Text = obj.Loc_Code
            FndLocation.Value = obj.Loc_Code
            TxtTankerNo.Text = obj.Tanker_No
            lblLocationDesc.Text = clsLocation.GetName(obj.Loc_Code, Nothing)
            txtChallanNo.Text = obj.Challan_No
            dtpChallanDate.Value = obj.Challan_Date
            fndPriceChart.Value = obj.Price_Code
            If obj.TANKER_SKU_MANUAL = 1 Then
                RdbTankerReceipt.CheckState = CheckState.Checked
            ElseIf obj.TANKER_SKU_MANUAL = 2 Then
                RdbSkuReceipt.CheckState = CheckState.Checked
            ElseIf obj.TANKER_SKU_MANUAL = 3 Then
                RdbManual.CheckState = CheckState.Checked
            End If
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(" select * from tspl_Bulk_price_master where Price_Code ='" & fndPriceChart.Value & "'  ")
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                TxtFatWeightage.Text = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                TxtSNFWeightage.Text = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                txtfatPercentage.Text = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                txtSNFPercentage.Text = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                txtStanadardrate.Text = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                txtTolerance.Text = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                '    gvItem.Rows(0).Cells(colStandardRate).Value = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
            End If
            fndTankerNo.Value = obj.Tanker_No
            loadBlankItemGrid()
            'isCellValueChangedOpen = True
            gvItem.Rows(0).Cells(colItemCode).Value = obj.Item_Code

            gvItem.Rows(0).Cells(colItemDesc).Value = obj.Item_Desc
            gvItem.Rows(0).Cells(colHSN_Code).Value = obj.HSN_Code
            gvItem.Rows(0).Cells(colUOM).Value = obj.UOM
            'isCellValueChangedOpen = False
            gvItem.Rows(0).Cells(colGrossWeight).Value = clsCommon.myFormat(obj.Gross_Weight)
            gvItem.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(obj.Tare_Weight)
            gvItem.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(obj.Net_Weight)
            gvItem.Rows(0).Cells(colQty).Value = clsCommon.myFormat(obj.Qty)
            If obj.isPosted = 0 Then
                gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_per, 3))
                gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(obj.snf_Per, 3))
                gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_KG, 3), False, True, False, 3, True)
                gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(obj.SNF_KG, 3), False, True, False, 3, True)
                gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_Rate, 2))
                gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(obj.SNF_Rate, 2))
            Else
                gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(obj.fat_per)
                gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(obj.snf_Per)
                gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(obj.fat_KG, False, True, False, 3, True)
                gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(obj.SNF_KG, False, True, False, 3, True)
                gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(obj.fat_Rate)
                gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(obj.SNF_Rate)
            End If
            'gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_per, 2))
            'gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(obj.snf_Per, 2))
            'gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(obj.Net_Weight * obj.fat_per, 3) / 100, False, True, False, 3, True)
            'gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(obj.Net_Weight * obj.snf_Per, 3) / 100, False, True, False, 3, True)
            'gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(obj.fat_Rate, 2))
            'gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(obj.SNF_Rate, 2))
            gvItem.Rows(0).Cells(colAmt).Value = clsCommon.myFormat(obj.Amount)
            gvItem.Rows(0).Cells(colDeducRate).Value = clsCommon.myFormat(obj.Deduction)
            gvItem.Rows(0).Cells(colIncenRate).Value = clsCommon.myFormat(obj.Incentive)
            ''richa Against Ticket No.BM00000003719 on 04/09/2014
            gvItem.Rows(0).Cells(colSpecialDeduction).Value = clsCommon.myFormat(obj.SpecialDeduction)
            'gvItem.Rows(0).Cells(colIncenRate).Value = obj.Incentive
            'gvItem.Rows(0).Cells(colDeducRate).Value = obj.Deduction
            ''=======================================================
            gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat(obj.Actual_Amount)
            gvItem.Rows(0).Cells(colBalance).Value = clsCommon.myFormat(obj.Balance)
            gvItem.Rows(0).Cells(colBalFatKG).Value = clsCommon.myFormat(obj.BalanceFatKg)
            gvItem.Rows(0).Cells(colBalSNFKG).Value = clsCommon.myFormat(obj.BalanceSNFKg)
            gvItem.Rows(0).Cells(colQty).Value = clsCommon.myFormat(obj.Qty)
            gvItem.Rows(0).Cells(colStandardRate).Value = clsCommon.myFormat(obj.Standardrate)
            gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat(obj.NetRate)
            gvItem.Rows(0).Cells(colMilkRate).Value = clsCommon.myFormat(obj.BasicRate)

            gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat(obj.FatAmt)
            gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat(obj.SnfAmt)
            gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat(obj.FinalMilkRate)
            Dim objQ As clsMilkQualityCheck = clsMilkQualityCheck.getData(txtQCNo.Text, "Tanker", NavigatorType.Current)
            dtpQCInTime.Value = objQ.QC_In_Date_Time
            dtpQCOutTime.Value = objQ.QC_Out_Date_Time
            txtRemarks.Text = objQ.Remarks
            txtDipValue.Text = objQ.Dip_Value
            loadBlankParameterGrid()
            If objQ.arrQcParam IsNot Nothing Then
                For i As Integer = 0 To objQ.arrQcParam.Count - 1
                    Try
                        gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                        If clsCommon.CompairString(objQ.arrQcParam(i).Param_Field_Code, fatColName) = CompairStringResult.Equal OrElse clsCommon.CompairString(objQ.arrQcParam(i).Param_Field_Code, snfColName) = CompairStringResult.Equal Then
                            If obj.isPosted = 0 Then
                                gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(MyMath.RoundDown(gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value, 2))
                            Else
                                gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value)
                            End If

                        End If
                    Catch ex1 As Exception
                    End Try
                Next
            End If

            'Try
            '    gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myCdbl(obj.Net_Weight) * clsCommon.myCdbl(gvParam.Rows(0).Cells(fatColName).Value) / 100
            '    gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myCdbl(obj.Net_Weight) * clsCommon.myCdbl(gvParam.Rows(0).Cells(snfColName).Value) / 100
            'Catch ex3 As Exception
            'End Try
            ''richa Against Ticket no .BM00000003725 on 05/08/2014
            loadBlankParameterGridwithRange()
            If obj.arrObj IsNot Nothing AndAlso obj.arrObj.Count > 0 Then
                For j As Integer = 0 To obj.arrObj.Count - 1
                    gvRange.Rows.AddNew()
                    gvRange.Rows(j).Cells("Code").Value = obj.arrObj(j).Parameter
                    gvRange.Rows(j).Cells("LowerRange").Value = clsCommon.myFormat(obj.arrObj(j).Lower_Range)
                    gvRange.Rows(j).Cells("UpperRange").Value = clsCommon.myFormat(obj.arrObj(j).Upper_Range)
                    gvRange.Rows(j).Cells("Value").Value = obj.arrObj(j).value
                    gvRange.Rows(j).Cells("QcValue").Value = obj.arrObj(j).QCValue
                    gvRange.Rows(j).Cells("IncentiveDeduction").Value = clsCommon.myFormat(obj.arrObj(j).Incen_Deduc)
                    'Code
                    'LowerRange
                    'UpperRange
                    'Value
                    'QcValue
                    'IncentiveDeduction
                Next
            Else
                ' Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Lower_range,Upper_range,Value  from TSPL_PARAMETER_RANGE_MASTER order by Code ")
                ' Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Max(Lower_range) as Lower_range,Max(Upper_range) as Upper_range,Max(Value) as Value,Max(TSPL_PARAMETER_RANGE_MASTER.Effective_Date)  from TSPL_PARAMETER_RANGE_MASTER group by TSPL_PARAMETER_RANGE_MASTER.Code order by TSPL_PARAMETER_RANGE_MASTER.Code ")
                Dim whrCls As String = String.Empty
                If clsERPFuncationality.isLocationMcc(txtLocation.Text) Then
                    whrCls = " and Param_for='MCC' or Param_for='BOTH'"
                Else
                    whrCls = " and Param_for='PLANT' or Param_for='BOTH'"
                End If
                'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select xx.*,case when TSPL_PARAMETER_MASTER.Type='FAT' then 1 when TSPL_PARAMETER_MASTER.Type='SNF' then 2  when TSPL_PARAMETER_MASTER.type='CLR' then 3 else 4 end as ordering from (Select MAX(TSPL_PARAMETER_RANGE_MASTER.Code) as code, Max(Lower_range) as Lower_range,Max(Upper_range) as Upper_range,Max(Value) as Value,Max(TSPL_PARAMETER_RANGE_MASTER.Effective_Date) as effective_date from TSPL_PARAMETER_RANGE_MASTER group by TSPL_PARAMETER_RANGE_MASTER.Code ) xx left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=xx.Code " & whrCls & " order by ordering ")
                'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '    Dim colcount As Integer = 1
                '    For k As Integer = 0 To dt.Rows.Count - 1
                '        Try
                '            gvRange.Rows(0).Cells(colcount).Value = dt.Rows(k)("Lower_range")
                '            gvRange.Rows(0).Cells(colcount + 1).Value = dt.Rows(k)("Upper_range")
                '            gvRange.Rows(0).Cells(colcount + 2).Value = dt.Rows(k)("Value")
                '            colcount = colcount + 3
                '        Catch ex2 As Exception
                '        End Try
                '    Next
                'End If
                Dim paramName As String = String.Empty
                Dim qry1 As String = " select code from TSPL_PARAMETER_MASTER  where nature='R' " & whrCls
                Dim qry2 As String = String.Empty
                Dim qry3 As String = String.Empty
                Dim paramValue As Double = 0
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                        paramValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry3))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range, '" & paramValue & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Text & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'  order by Effective_Date desc  "
                        If i <> dt1.Rows.Count - 1 Then
                            qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                        End If
                    Next

                    qry2 = " select * from ( " & qry2 & " ) yyy"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                            gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", dt2.Rows(j)("Lower_Range"), dt2.Rows(j)("Upper_Range"), "", dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                        Next
                    End If
                End If

                qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='A' " & whrCls
                qry2 = ""
                Dim paramValue1 As String = ""
                dt1 = clsDBFuncationality.GetDataTable(qry1)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                        paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value , '" & paramValue1 & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Text & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
                        If i <> dt1.Rows.Count - 1 Then
                            qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                        End If
                    Next

                    qry2 = " select * from ( " & qry2 & " ) yyy"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                            gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("Condition_value"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                        Next
                    End If
                End If


                qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='B' " & whrCls
                qry2 = ""
                paramValue1 = ""
                dt1 = clsDBFuncationality.GetDataTable(qry1)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & obj.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                        paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status , '" & paramValue1 & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Text & "'   and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'  order by Effective_Date desc  "
                        If i <> dt1.Rows.Count - 1 Then
                            qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                        End If
                    Next

                    qry2 = " select * from ( " & qry2 & " ) yyy"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                            gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("status"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                        Next
                    End If
                End If
            End If
            ''=====================================================
            btnSave.Text = "Update"
            Dim qry As String = " select isnull(srn_return_no,'')  as srn_return from TSPL_Job_MILK_SRN where srn_no='" & fndSRNNo.Value & "'"
            If clsCommon.CompairString(clsDBFuncationality.getSingleValue(qry), "") = CompairStringResult.Equal Then
                lblReverse.Visible = False
            Else
                lblReverse.Visible = True

            End If
            If obj.isPosted = 1 Then
                lblPending.Status = ERPTransactionStatus.Approved
                btnSave.Enabled = False
                btnDelete.Enabled = False
                btnPost.Enabled = False
            Else
                lblPending.Status = ERPTransactionStatus.Pending
                btnSave.Enabled = True
                btnDelete.Enabled = True
                btnPost.Enabled = True
            End If
            btnPrint.Enabled = True
        Else
            Reset()
        End If
        isInsideLoadData = False
    End Sub

    Sub loadWeighmentData(ByVal strWeighmentNo As String)
        isInsideLoadData = True
        Try
            Dim srnNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select srn_no from TSPL_Job_MILK_SRN where weighment_no='" & strWeighmentNo & "' and isnull(srn_return_no,'')=''"))
            If clsCommon.myLen(srnNo) > 0 Then
                LoadData(srnNo, NavigatorType.Current)
                Exit Sub
            End If
            Dim objW As clsMilkWeighment = clsMilkWeighment.getData(strWeighmentNo, "Tanker", NavigatorType.Current)
            Dim strQcNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_Milk_quality_check.qc_no from TSPL_Milk_Weighment_Detail left outer join tspl_Milk_quality_check on tspl_Milk_quality_check.gate_entry_no=TSPL_Milk_Weighment_Detail.gate_entry_no where TSPL_Milk_Weighment_Detail.weighment_no='" & fndWeighmentNo.Value & "'"))
            Dim objQ As clsMilkQualityCheck = clsMilkQualityCheck.getData(strQcNo, "BulkProc", NavigatorType.Current)
            If objW IsNot Nothing AndAlso objQ IsNot Nothing Then
                'Reset()
                fndWeighmentNo.Value = objW.Weighment_No
                dtpWeighmentDate.Value = clsCommon.GetPrintDate(objW.Weighment_Date, "dd/MM/yyyy hh:mm:ss tt")
                txtVendor.Text = objW.Vendor_Code
                FndVendor.Value = objW.Vendor_Code
                FndLocation.Value = objW.location_Code
                lblVendorName.Text = objW.Vendor_Desc
                txtLocation.Text = objW.location_Code

                lblLocationDesc.Text = objW.Location_Desc
                txtChallanNo.Text = objW.Challan_No
                dtpChallanDate.Value = objW.Challan_Date
                fndTankerNo.Value = objW.Tanker_No
                txtDipValue.Text = objW.Dip_Value
                txtGateEntryNo.Text = objW.Gate_Entry_No
                loadBlankItemGrid()
                'isCellValueChangedOpen = True
                gvItem.Rows(0).Cells(colItemCode).Value = objW.Item_Code

                gvItem.Rows(0).Cells(colItemDesc).Value = objW.Item_Desc
                gvItem.Rows(0).Cells(colHSN_Code).Value = clsItemMaster.GetItemHSNCode(objW.Item_Code, Nothing)
                gvItem.Rows(0).Cells(colUOM).Value = objW.UOM
                'isCellValueChangedOpen = False
                gvItem.Rows(0).Cells(colGrossWeight).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Gross_Weight, 0))
                gvItem.Rows(0).Cells(colTareWeight).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Tare_Weight, 0))
                gvItem.Rows(0).Cells(colNetWeight).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Net_Weight, 0))
                gvItem.Rows(0).Cells(colQty).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Qty_In_Kg, 0))
                gvItem.Rows(0).Cells(colBalance).Value = GetBalanceQty()
                'gvItem.Rows(0).Cells(colFat).Value = objW.fat_per
                'gvItem.Rows(0).Cells(colSNF).Value = objW.snf_Per
                'gvItem.Rows(0).Cells(colFatKG).Value = objW.Net_Weight * objW.fat_per / 100
                'gvItem.Rows(0).Cells(colSNFKG).Value = objW.Net_Weight * objW.snf_Per / 100
                txtQCNo.Text = objQ.QC_No
                dtpQCInTime.Value = objQ.QC_In_Date_Time
                dtpQCOutTime.Value = objQ.QC_Out_Date_Time
                txtRemarks.Text = objQ.Remarks
                loadBlankParameterGrid()
                If objQ.arrQcParam IsNot Nothing Then
                    For i As Integer = 0 To objQ.arrQcParam.Count - 1
                        Try
                            gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                            If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal Then
                                gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(objQ.arrQcParam(i).Param_Field_Value, 3))
                                gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colSNF).Value)
                            End If
                            If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal Then
                                gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(objQ.arrQcParam(i).Param_Field_Value, 2))
                                gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colFat).Value)
                            End If
                        Catch exxx As Exception
                        End Try
                    Next
                End If

                Try
                    gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Net_Weight * MyMath.RoundDown(gvParam.Rows(0).Cells(fatColName).Value, 2) / 100, 3), False, True, False, 3, True)
                    gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(objW.Net_Weight * MyMath.RoundDown(gvParam.Rows(0).Cells(snfColName).Value, 2) / 100, 3), False, True, False, 3, True)
                Catch ex4 As Exception
                End Try
                ''richa Against Ticket no .BM00000003725 on 05/08/2014
                loadBlankParameterGridwithRange()
                Dim whrCls As String = String.Empty
                If clsERPFuncationality.isLocationMcc(txtLocation.Text) Then
                    whrCls = " and Param_for='MCC' or Param_for='BOTH'"
                Else
                    whrCls = " and (Param_for='PLANT' or Param_for='BOTH')"
                End If
                '' Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Max(Lower_range) as Lower_range,Max(Upper_range) as Upper_range,Max(Value) as Value,Max(TSPL_PARAMETER_RANGE_MASTER.Effective_Date)  from TSPL_PARAMETER_RANGE_MASTER group by TSPL_PARAMETER_RANGE_MASTER.Code order by TSPL_PARAMETER_RANGE_MASTER.Code ")
                'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select xx.*,case when TSPL_PARAMETER_MASTER.Type='FAT' then 1 when TSPL_PARAMETER_MASTER.Type='SNF' then 2  when TSPL_PARAMETER_MASTER.type='CLR' then 3 else 4 end as ordering from (Select MAX(TSPL_PARAMETER_RANGE_MASTER.Code) as code, Max(Lower_range) as Lower_range,Max(Upper_range) as Upper_range,Max(Value) as Value,Max(TSPL_PARAMETER_RANGE_MASTER.Effective_Date) as effective_date from TSPL_PARAMETER_RANGE_MASTER group by TSPL_PARAMETER_RANGE_MASTER.Code ) xx left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=xx.Code " & whrcls & " order by ordering ")
                'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '    Dim colcount As Integer = 1
                '    For k As Integer = 0 To dt.Rows.Count - 1
                '        Try
                '            gvRange.Rows(0).Cells(colcount).Value = dt.Rows(k)("Lower_range")
                '            gvRange.Rows(0).Cells(colcount + 1).Value = dt.Rows(k)("Upper_range")
                '            gvRange.Rows(0).Cells(colcount + 2).Value = dt.Rows(k)("Value")
                '            colcount = colcount + 3
                '        Catch exxx As Exception
                '        End Try
                '    Next
                'End If

                Dim paramName As String = String.Empty
                Dim qry1 As String = " select code,Description from TSPL_PARAMETER_MASTER  where nature='R' " & whrCls
                Dim qry2 As String = String.Empty
                Dim qry3 As String = String.Empty
                Dim paramValue As Double = 0
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & objQ.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                        paramValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry3))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range , '" & paramValue & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Text & "'  and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
                        If i <> dt1.Rows.Count - 1 Then
                            qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                        End If
                    Next

                    qry2 = " select * from ( " & qry2 & " ) yyy"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                            gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", dt2.Rows(j)("Lower_Range"), dt2.Rows(j)("Upper_Range"), "", dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                        Next
                    End If
                End If

                qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='A' " & whrCls
                qry2 = ""
                Dim paramValue1 As String = ""
                dt1 = clsDBFuncationality.GetDataTable(qry1)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & objQ.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                        paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Text & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
                        If i <> dt1.Rows.Count - 1 Then
                            qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                        End If
                    Next

                    qry2 = " select * from ( " & qry2 & " ) yyy"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                            gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("Condition_value"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                        Next
                    End If
                End If


                qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='B' " & whrCls
                qry2 = ""
                paramValue1 = ""
                dt1 = clsDBFuncationality.GetDataTable(qry1)
                If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                    For i As Integer = 0 To dt1.Rows.Count - 1
                        qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & objQ.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                        paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Text & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'    order by Effective_Date desc  "
                        If i <> dt1.Rows.Count - 1 Then
                            qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                        End If
                    Next

                    qry2 = " select * from ( " & qry2 & " ) yyy"
                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                        For j As Integer = 0 To dt2.Rows.Count - 1
                            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                            gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("status"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                        Next
                    End If
                End If
                ''=====================================================

                'gvItem.Rows(0).Cells(colDeducRate).Value = getDeduction()
                'gvItem.Rows(0).Cells(colIncenRate).Value = getIncentive()

                '''''richa Against Ticket No.BM00000003719 on 04/09/2014
                gvItem.Rows(0).Cells(colIncenRate).Value = clsCommon.myFormat(getIncentivePerUnit())
                gvItem.Rows(0).Cells(colDeducRate).Value = clsCommon.myFormat(getDeductionPerUnit())
                gvItem.Rows(0).Cells(colSpecialDeduction).Value = clsCommon.myFormat(objQ.DeductionAmount)
                gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat(clsCommon.myCdbl(txtStanadardrate.Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colIncenRate).Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value))
                gvItem.Rows(0).Cells(colNetRate).EndEdit()
                gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat("0")
                ''=======================================================
                Dim strQry As String = ""
                strQry = " select  * from TSPL_Bulk_Price_MASTER  where   TSPL_Bulk_Price_MASTER.Price_Code in (select PriceCode  from Tspl_Vendor_Price_Chart_mapping where VendorCode='" & txtVendor.Text & "' and isDefault=1 ) "
                Dim FatW As Double = 0
                Dim SNfW As Double = 0
                Dim FATRatio As Double = 0
                Dim SNFRatio As Double = 0
                Dim StdRate As Double = 0
                Dim fatKG As Double = 0
                Dim snfKG As Double = 0
                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable(strQry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    fndPriceChart.Value = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                    FatW = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                    SNfW = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                    FATRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                    SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                    StdRate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                    TxtFatWeightage.Text = FatW
                    TxtSNFWeightage.Text = SNfW
                    txtfatPercentage.Text = FATRatio
                    txtSNFPercentage.Text = SNFRatio
                    txtStanadardrate.Text = clsCommon.myFormat(StdRate)
                    txtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                Else
                    TxtFatWeightage.Text = "0"
                    TxtSNFWeightage.Text = "0"
                    txtfatPercentage.Text = "0"
                    txtSNFPercentage.Text = "0"
                    txtStanadardrate.Text = "0"
                    txtTolerance.Value = "0"
                    fndTankerNo.Enabled = True
                End If
                OpenPriceChart(True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Sub loadGateEntryData(ByVal strGateEntryNo As String)
        isInsideLoadData = True
        Try
            Dim srnNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select srn_no from TSPL_Job_MILK_SRN where Gate_Entry_no='" & strGateEntryNo & "' and isnull(srn_return_no,'')=''"))
            If clsCommon.myLen(srnNo) > 0 Then
                LoadData(srnNo, NavigatorType.Current)
                Exit Sub
            End If
            Dim objW As clsMilkGateEntry = clsMilkGateEntry.getData(strGateEntryNo, "SKU_Receipt", NavigatorType.Current)
            '  Dim strQcNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_Milk_quality_check.qc_no from TSPL_Milk_Weighment_Detail left outer join tspl_Milk_quality_check on tspl_Milk_quality_check.gate_entry_no=TSPL_Milk_Weighment_Detail.gate_entry_no where TSPL_Milk_Weighment_Detail.weighment_no='" & fndWeighmentNo.Value & "'"))
            ' Dim objQ As clsMilkQualityCheck = clsMilkQualityCheck.getData(strQcNo, "BulkProc", NavigatorType.Current)
            If objW IsNot Nothing Then
                'Reset()
                fndWeighmentNo.Value = objW.Gate_Entry_No
                dtpWeighmentDate.Value = clsCommon.GetPrintDate(objW.Date_And_Time, "dd/MM/yyyy hh:mm:ss tt")
                txtVendor.Text = objW.Vendor_Code
                lblVendorName.Text = objW.Vendor_Desc
                txtLocation.Text = objW.location_Code
                lblLocationDesc.Text = objW.Location_Desc
                FndVendor.Value = objW.Vendor_Code
                FndLocation.Value = objW.location_Code
                txtChallanNo.Text = objW.Challan_No
                dtpChallanDate.Value = objW.Challan_Date
                fndTankerNo.Value = objW.Tanker_No
                txtDipValue.Text = 0
                txtGateEntryNo.Text = objW.Gate_Entry_No
                loadBlankItemGrid()
                'isCellValueChangedOpen = True
                gvItem.Rows(0).Cells(colItemCode).Value = objW.Item_Code

                gvItem.Rows(0).Cells(colItemDesc).Value = objW.Item_Desc
                gvItem.Rows(0).Cells(colHSN_Code).Value = clsItemMaster.GetItemHSNCode(objW.Item_Code, Nothing)
                gvItem.Rows(0).Cells(colUOM).Value = objW.UOM
                'isCellValueChangedOpen = False
                gvItem.Rows(0).Cells(colGrossWeight).Value = 0
                gvItem.Rows(0).Cells(colTareWeight).Value = 0
                gvItem.Rows(0).Cells(colNetWeight).Value = 0
                gvItem.Rows(0).Cells(colQty).Value = objW.Qty_In_Kg
                gvItem.Rows(0).Cells(colFat).Value = objW.fat_per
                gvItem.Rows(0).Cells(colSNF).Value = objW.snf_Per
                gvItem.Rows(0).Cells(colBalance).Value = GetBalanceQty()
                'gvItem.Rows(0).Cells(colFat).Value = objW.fat_per
                'gvItem.Rows(0).Cells(colSNF).Value = objW.snf_Per
                'gvItem.Rows(0).Cells(colFatKG).Value = objW.Net_Weight * objW.fat_per / 100
                'gvItem.Rows(0).Cells(colSNFKG).Value = objW.Net_Weight * objW.snf_Per / 100
                txtQCNo.Text = ""
                dtpQCInTime.Value = Nothing
                dtpQCOutTime.Value = Nothing
                txtRemarks.Text = ""
                loadBlankParameterGrid()
                'If objQ.arrQcParam IsNot Nothing Then
                '    For i As Integer = 0 To objQ.arrQcParam.Count - 1
                '        Try
                '            gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = objQ.arrQcParam(i).Param_Field_Value
                '            If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "SNF") = CompairStringResult.Equal Then
                '                gvItem.Rows(0).Cells(colSNF).Value = clsCommon.myFormat(MyMath.RoundDown(objQ.arrQcParam(i).Param_Field_Value, 2))
                '                gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colSNF).Value)
                '            End If
                '            If clsCommon.CompairString(objQ.arrQcParam(i).Param_Type, "FAT") = CompairStringResult.Equal Then
                '                gvItem.Rows(0).Cells(colFat).Value = clsCommon.myFormat(MyMath.RoundDown(objQ.arrQcParam(i).Param_Field_Value, 2))
                '                gvParam.Rows(0).Cells(objQ.arrQcParam(i).Param_Field_Code).Value = clsCommon.myFormat(gvItem.Rows(0).Cells(colFat).Value)
                '            End If
                '        Catch exxx As Exception
                '        End Try
                '    Next
                'End If

                Try
                    gvItem.Rows(0).Cells(colFatKG).Value = clsCommon.myFormat(MyMath.RoundDown(0 * MyMath.RoundDown(gvParam.Rows(0).Cells(fatColName).Value, 2) / 100, 3), False, True, False, 3, True)
                    gvItem.Rows(0).Cells(colSNFKG).Value = clsCommon.myFormat(MyMath.RoundDown(0 * MyMath.RoundDown(gvParam.Rows(0).Cells(snfColName).Value, 2) / 100, 3), False, True, False, 3, True)
                Catch ex4 As Exception
                End Try
                ''richa Against Ticket no .BM00000003725 on 05/08/2014
                loadBlankParameterGridwithRange()
                Dim whrCls As String = String.Empty
                If clsERPFuncationality.isLocationMcc(txtLocation.Text) Then
                    whrCls = " and Param_for='MCC' or Param_for='BOTH'"
                Else
                    whrCls = " and (Param_for='PLANT' or Param_for='BOTH')"
                End If
                '' Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select Max(Lower_range) as Lower_range,Max(Upper_range) as Upper_range,Max(Value) as Value,Max(TSPL_PARAMETER_RANGE_MASTER.Effective_Date)  from TSPL_PARAMETER_RANGE_MASTER group by TSPL_PARAMETER_RANGE_MASTER.Code order by TSPL_PARAMETER_RANGE_MASTER.Code ")
                'Dim dt As DataTable = clsDBFuncationality.GetDataTable("select xx.*,case when TSPL_PARAMETER_MASTER.Type='FAT' then 1 when TSPL_PARAMETER_MASTER.Type='SNF' then 2  when TSPL_PARAMETER_MASTER.type='CLR' then 3 else 4 end as ordering from (Select MAX(TSPL_PARAMETER_RANGE_MASTER.Code) as code, Max(Lower_range) as Lower_range,Max(Upper_range) as Upper_range,Max(Value) as Value,Max(TSPL_PARAMETER_RANGE_MASTER.Effective_Date) as effective_date from TSPL_PARAMETER_RANGE_MASTER group by TSPL_PARAMETER_RANGE_MASTER.Code ) xx left outer join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=xx.Code " & whrcls & " order by ordering ")
                'If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                '    Dim colcount As Integer = 1
                '    For k As Integer = 0 To dt.Rows.Count - 1
                '        Try
                '            gvRange.Rows(0).Cells(colcount).Value = dt.Rows(k)("Lower_range")
                '            gvRange.Rows(0).Cells(colcount + 1).Value = dt.Rows(k)("Upper_range")
                '            gvRange.Rows(0).Cells(colcount + 2).Value = dt.Rows(k)("Value")
                '            colcount = colcount + 3
                '        Catch exxx As Exception
                '        End Try
                '    Next
                'End If

                Dim paramName As String = String.Empty
                Dim qry1 As String = " select code,Description from TSPL_PARAMETER_MASTER  where nature='R' " & whrCls
                Dim qry2 As String = String.Empty
                Dim qry3 As String = String.Empty
                Dim paramValue As Double = 0
                Dim dt1 As DataTable = clsDBFuncationality.GetDataTable(qry1)
                'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                '    'For i As Integer = 0 To dt1.Rows.Count - 1
                '    '    qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & objQ.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                '    '    paramValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry3))
                '        qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Upper_range ,Lower_range , '" & paramValue & "' as QCValue     from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and " & paramValue & ">=Lower_range  and  " & paramValue & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Text & "'  and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
                '    '    If i <> dt1.Rows.Count - 1 Then
                '    '        qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                '    '    End If
                '    'Next

                '    qry2 = " select * from ( " & qry2 & " ) yyy"
                '    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                '    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                '        For j As Integer = 0 To dt2.Rows.Count - 1
                '            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                '            gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", dt2.Rows(j)("Lower_Range"), dt2.Rows(j)("Upper_Range"), "", dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                '        Next
                '    End If
                'End If

                'qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='A' " & whrCls
                'qry2 = ""
                'Dim paramValue1 As String = ""
                'dt1 = clsDBFuncationality.GetDataTable(qry1)
                'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                '    'For i As Integer = 0 To dt1.Rows.Count - 1
                '    '    qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & objQ.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                '    '    paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                '    '    qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,Condition_value  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and condition_value='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Text & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
                '    '    If i <> dt1.Rows.Count - 1 Then
                '    '        qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                '    '    End If
                '    'Next

                '    qry2 = " select * from ( " & qry2 & " ) yyy"
                '    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                '    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                '        For j As Integer = 0 To dt2.Rows.Count - 1
                '            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                '            gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("Condition_value"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                '        Next
                '    End If
                'End If


                'qry1 = " select code from TSPL_PARAMETER_MASTER  where nature='B' " & whrCls
                'qry2 = ""
                'paramValue1 = ""
                'dt1 = clsDBFuncationality.GetDataTable(qry1)
                'If dt1 IsNot Nothing AndAlso dt1.Rows.Count > 0 Then
                '    'For i As Integer = 0 To dt1.Rows.Count - 1
                '    '    qry3 = " select Param_Field_Value  from TSPL_QC_Parameter_Detail where QC_No='" & objQ.QC_No & "' and Param_Field_Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "'"
                '    '    paramValue1 = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry3))
                '    '    qry2 = qry2 & "   select top 1  coalesce(Value,0.0) as value,Code,status  , '" & paramValue1 & "' as QCValue    from TSPL_PARAMETER_RANGE_MASTER where Code='" & clsCommon.myCstr(dt1.Rows(i)("Code")) & "' and status='" & paramValue1 & "'    and effective_date<='" & clsCommon.GetPrintDate(dtpSRNDATE.Value, "dd/MMM/yyyy") & "' and loc_code='" & txtLocation.Text & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'    order by Effective_Date desc  "
                '    '    If i <> dt1.Rows.Count - 1 Then
                '    '        qry2 = qry2 & Environment.NewLine & " Union all " & Environment.NewLine
                '    '    End If
                '    'Next

                '    qry2 = " select * from ( " & qry2 & " ) yyy"
                '    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry2)
                '    If dt2 IsNot Nothing AndAlso dt2.Rows.Count > 0 Then
                '        For j As Integer = 0 To dt2.Rows.Count - 1
                '            paramName = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Description  from TSPL_PARAMETER_MASTER where Code='" & dt2.Rows(j)("Code") & "' "))
                '            gvRange.Rows.Add(dt2.Rows(j)("Code") & " ( " & paramName & ")", "", "", dt2.Rows(j)("status"), dt2.Rows(j)("QCValue"), dt2.Rows(j)("Value"))
                '        Next
                '    End If
                'End If
                ' ''=====================================================

                'gvItem.Rows(0).Cells(colDeducRate).Value = getDeduction()
                'gvItem.Rows(0).Cells(colIncenRate).Value = getIncentive()

                '''''richa Against Ticket No.BM00000003719 on 04/09/2014
                gvItem.Rows(0).Cells(colIncenRate).Value = clsCommon.myFormat(getIncentivePerUnit())
                gvItem.Rows(0).Cells(colDeducRate).Value = clsCommon.myFormat(getDeductionPerUnit())
                ' gvItem.Rows(0).Cells(colSpecialDeduction).Value = clsCommon.myFormat(objQ.DeductionAmount)
                gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat(clsCommon.myCdbl(txtStanadardrate.Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colIncenRate).Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value))
                gvItem.Rows(0).Cells(colNetRate).EndEdit()
                gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat("0")
                ''=======================================================
                Dim strQry As String = ""
                strQry = " select  * from TSPL_Bulk_Price_MASTER  where   TSPL_Bulk_Price_MASTER.Price_Code in (select PriceCode  from Tspl_Vendor_Price_Chart_mapping where VendorCode='" & txtVendor.Text & "' and isDefault=1 ) "
                Dim FatW As Double = 0
                Dim SNfW As Double = 0
                Dim FATRatio As Double = 0
                Dim SNFRatio As Double = 0
                Dim StdRate As Double = 0
                Dim fatKG As Double = 0
                Dim snfKG As Double = 0
                Dim dt As DataTable
                dt = clsDBFuncationality.GetDataTable(strQry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    fndPriceChart.Value = clsCommon.myCstr(dt.Rows(0)("Price_Code"))
                    FatW = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                    SNfW = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                    FATRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                    SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                    StdRate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                    TxtFatWeightage.Text = FatW
                    TxtSNFWeightage.Text = SNfW
                    txtfatPercentage.Text = FATRatio
                    txtSNFPercentage.Text = SNFRatio
                    txtStanadardrate.Text = clsCommon.myFormat(StdRate)
                    txtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
                Else
                    TxtFatWeightage.Text = "0"
                    TxtSNFWeightage.Text = "0"
                    txtfatPercentage.Text = "0"
                    txtSNFPercentage.Text = "0"
                    txtStanadardrate.Text = "0"
                    txtTolerance.Value = "0"
                    fndTankerNo.Enabled = True
                End If
                OpenPriceChart(True)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Function getDeductionPerUnit() As Double
        Dim rValue As Double = 0
        For i As Integer = 0 To gvRange.Rows.Count - 1
            If clsCommon.myLen(gvRange.Rows(i).Cells("Code").Value) > 0 And clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value) < 0 Then
                rValue = rValue + clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value)
            End If
        Next
        Return rValue

    End Function
    Function getIncentivePerUnit() As Double
        Dim rValue As Double = 0
        For i As Integer = 0 To gvRange.Rows.Count - 1
            If clsCommon.myLen(gvRange.Rows(i).Cells("Code").Value) > 0 And clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value) > 0 Then
                rValue = rValue + clsCommon.myCdbl(gvRange.Rows(i).Cells("IncentiveDeduction").Value)
            End If
        Next
        Return rValue
    End Function
    Function getDeduction(ByVal paramCode As String, ByVal value As Double, ByVal docDate As Date) As Double
        Dim strQry As String = "select top 1  coalesce(Value,0.0) as value   from TSPL_PARAMETER_RANGE_MASTER where Code='" & paramCode & "' and " & value & ">=Lower_range  and " & value & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") & "' and coalesce(Value,0.0)<0.0   and loc_code='" & txtLocation.Text & "' and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
        Dim deducValue As Double = 0
        deducValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
        Return deducValue
    End Function

    Function getDeduction() As Double
        Dim totDeducValue As Double = 0
        For i As Integer = 1 To gvParam.Columns.Count - 1
            totDeducValue = totDeducValue + getDeduction(gvParam.Columns(i).Name, clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value), dtpSRNDATE.Value)
        Next
        Return totDeducValue
    End Function

    Function getIncentive(ByVal paramCode As String, ByVal value As Double, ByVal docDate As Date) As Double
        Dim strQry As String = "select top 1  coalesce(Value,0.0) as value   from TSPL_PARAMETER_RANGE_MASTER where Code='" & paramCode & "' and " & value & ">=Lower_range  and " & value & "<=Upper_range  and effective_date<='" & clsCommon.GetPrintDate(docDate, "dd/MMM/yyyy") & "'  and coalesce(Value,0.0)>0.0   and loc_code='" & txtLocation.Text & "'  and isReject=0  and vendor_class='" & clsDBFuncationality.getSingleValue(" select Vendor_Type from tspl_vendor_master where Vendor_code ='" & txtVendor.Text & "' ") & "'   order by Effective_Date desc  "
        Dim IncenValue As Double = 0
        IncenValue = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(strQry))
        Return IncenValue
    End Function

    Function getIncentive() As Double
        Dim totIncenValue As Double = 0
        For i As Integer = 1 To gvParam.Columns.Count - 1
            totIncenValue = totIncenValue + getIncentive(gvParam.Columns(i).Name, clsCommon.myCdbl(gvParam.Rows(0).Cells(i).Value), dtpSRNDATE.Value)
        Next
        Return totIncenValue
    End Function

    Private Sub fndWeighmentNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndWeighmentNo._MYValidating

    End Sub
    Private Sub fndPriceChart__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndPriceChart._MYValidating
        Dim whrcls As String = String.Empty
        Dim FatW As Double = 0
        Dim SNfW As Double = 0
        Dim FATRate As Double = 0
        Dim SNFRate As Double = 0
        Dim FATValue As Double = 0
        Dim SNfValue As Double = 0
        Dim FATRatio As Double = 0
        Dim SNFRatio As Double = 0
        Dim StdRate As Double = 0
        Dim fatKG As Double = 0
        Dim snfKG As Double = 0
        isDocPosted = False
        If clsCommon.myLen(txtVendor.Text) > 0 Then
            whrcls = "  TSPL_Bulk_Price_MASTER.Price_Code in (select PriceCode  from Tspl_Vendor_Price_Chart_mapping where VendorCode='" & txtVendor.Text & "') "
        Else
            clsCommon.MyMessageBoxShow("Please select a weighment no ")
            Exit Sub
        End If
        fndPriceChart.Value = clsPriceChartBulkProc.getFinder(whrcls, fndPriceChart.Value, isButtonClicked)
        Dim strQry As String = String.Empty
        Dim dt As DataTable
        If clsCommon.myLen(fndPriceChart.Value) > 0 Then
            strQry = " select  * from TSPL_Bulk_Price_MASTER  where Price_Code='" & fndPriceChart.Value & "' "
            dt = clsDBFuncationality.GetDataTable(strQry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                FatW = clsCommon.myCdbl(dt.Rows(0)("Fat_Weightage"))
                SNfW = clsCommon.myCdbl(dt.Rows(0)("Snf_Weightage"))
                FATRatio = clsCommon.myCdbl(dt.Rows(0)("Fat_Percentage"))
                SNFRatio = clsCommon.myCdbl(dt.Rows(0)("Snf_Percentage"))
                StdRate = clsCommon.myCdbl(dt.Rows(0)("Standard_Rate"))
                TxtFatWeightage.Text = FatW
                TxtSNFWeightage.Text = SNfW
                txtfatPercentage.Text = FATRatio
                txtSNFPercentage.Text = SNFRatio
                txtStanadardrate.Text = StdRate
                txtTolerance.Value = clsCommon.myCdbl(dt.Rows(0)("Tolerance"))
            Else
                TxtFatWeightage.Text = "0"
                TxtSNFWeightage.Text = "0"
                txtfatPercentage.Text = "0"
                txtSNFPercentage.Text = "0"
                txtStanadardrate.Text = "0"
                txtTolerance.Value = "0"
                fndTankerNo.Enabled = True
            End If
        End If
        OpenPriceChart(True)
    End Sub

    Sub OpenPriceChart(ByVal IsPriceChartSelected As Boolean)
        If Not isDocPosted Then
            Dim FatW As Double = 0
            Dim SNfW As Double = 0
            Dim FATRate As Double = 0
            Dim SNFRate As Double = 0
            Dim FATValue As Double = 0
            Dim SNfValue As Double = 0
            Dim FATRatio As Double = 0
            Dim SNFRatio As Double = 0
            Dim StdRate As Double = 0
            Dim fatKG As Double = 0
            Dim snfKG As Double = 0
            Dim strQry As String = String.Empty
            Dim whrcls As String = String.Empty
            'Dim dt As DataTable
            If clsCommon.myLen(fndPriceChart.Value) > 0 Then
                If IsPriceChartSelected Then
                    gvItem.Rows(0).Cells(colMilkRate).Value = clsCommon.myFormat(txtStanadardrate.Text)
                End If
                FatW = clsCommon.myCdbl(TxtFatWeightage.Text)
                SNfW = clsCommon.myCdbl(TxtSNFWeightage.Text)
                gvItem.Rows(0).Cells(colStandardRate).Value = txtStanadardrate.Text
                fatKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colFatKG).Value), 3), False, True, False, 3, True)
                snfKG = clsCommon.myFormat(MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colSNFKG).Value), 3), False, True, False, 3, True)
                FATRatio = clsCommon.myCdbl(txtfatPercentage.Text)
                SNFRatio = clsCommon.myCdbl(txtSNFPercentage.Text)
                gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat((clsCommon.myCdbl(gvItem.Rows(0).Cells(colMilkRate).Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colIncenRate).Value)) + (clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) - clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value)))
                gvItem.Rows(0).Cells(colNetRate).EndEdit()
                FATRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetRate).Value) * FatW / FATRatio, 2)
                SNFRate = MyMath.RoundDown(clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetRate).Value) * SNfW / SNFRatio, 2)
                FATValue = MyMath.RoundDown(fatKG * FATRate, 2)
                SNfValue = MyMath.RoundDown(snfKG * SNFRate, 2)
                gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat(MyMath.RoundDown(FATRate, 2))
                gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat(MyMath.RoundDown(SNFRate, 2))
                gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat(MyMath.RoundDown(FATValue, 2))
                gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat(MyMath.RoundDown(SNfValue, 2))
                fndTankerNo.Enabled = False
                gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat(Math.Round(FATValue + SNfValue, 0))
                If RdbTankerReceipt.IsChecked Then
                    gvItem.Rows(0).Cells(colQty).Value = gvItem.Rows(0).Cells(colNetWeight).Value
                End If
                If clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value) <= 0 Then
                    gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat(Math.Round(clsCommon.myCdbl(gvItem.Rows(0).Cells(colActAmt).Value) / clsCommon.myCdbl(gvItem.Rows(0).Cells(colQty).Value), 2))
                End If
            Else
                gvItem.Rows(0).Cells(colMilkRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colNetRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colStandardRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFatRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colSNFRate).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFatAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colSNFAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myFormat("0")
                gvItem.Rows(0).Cells(colFinalMilkRate).Value = clsCommon.myFormat("0")
            End If
        End If

    End Sub
    Private Sub gvItem_CellValidated(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatedEventArgs) Handles gvItem.CellValidated
        If Not isInsideLoadData Then
            If Not isCellValueChangedOpen Then
                isCellValueChangedOpen = True
                If e.Column Is gvItem.Columns(colMilkRate) Then
                    Dim stdRate As Double = clsCommon.myCdbl(gvItem.Rows(0).Cells(colStandardRate).Value)
                    Dim BasicRate As Double = clsCommon.myCdbl(gvItem.Rows(0).Cells(colMilkRate).Value)
                    Dim tolRance As Double = clsCommon.myCdbl(txtTolerance.Value)
                    If BasicRate <= (stdRate + (stdRate * tolRance / 100)) AndAlso BasicRate >= (stdRate - (stdRate * tolRance / 100)) Then
                        'gvItem.Rows(0).Cells(colNetRate).Value = (BasicRate + clsCommon.myCdbl(gvItem.Rows(0).Cells(colIncenRate).Value)) + (clsCommon.myCdbl(gvItem.Rows(0).Cells(colDeducRate).Value) + clsCommon.myCdbl(gvItem.Rows(0).Cells(colSpecialDeduction).Value))
                        'gvItem.Rows(0).Cells(colActAmt).Value = clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetRate).Value) * clsCommon.myCdbl(gvItem.Rows(0).Cells(colNetWeight).Value)
                        OpenPriceChart(False)
                    Else
                        gvItem.Rows(0).Cells(colMilkRate).Value = gvItem.Rows(0).Cells(colStandardRate).Value
                        clsCommon.MyMessageBoxShow("Invalid Basic Rate.It must be within tolerance of standard rate")
                        gvItem.Rows(0).Cells(colMilkRate).EndEdit()
                    End If
                End If
                isCellValueChangedOpen = False
            End If
        End If

    End Sub

    Private Sub gvItem_CellValidating(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellValidatingEventArgs) Handles gvItem.CellValidating

    End Sub

    Private Sub gvItem_CellValueChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvItem.CellValueChanged
        Try
            Dim qty As Double = 0
            Dim deduction As Double = 0
            Dim incen As Double = 0
            Dim spDeduc As Double = 0
            If Not isInsideLoadData Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gvItem.Columns(colItemCode) Then
                        OpenICodeList(True)
                        gvItem.Rows.AddNew()
                    End If
                    If gvItem.CurrentColumn Is gvItem.Columns(colUOM) And Not isCellChangingUOM Then
                        OpenUOMList(True)
                    End If
                    If e.Column Is gvItem.Columns(colGrossWeight) Or e.Column Is gvItem.Columns(colTareWeight) Then

                        gvItem.CurrentRow.Cells(colNetWeight).Value = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colGrossWeight).Value) - clsCommon.myCdbl(gvItem.CurrentRow.Cells(colTareWeight).Value)
                        If RdbTankerReceipt.IsChecked Then
                            gvItem.Rows(0).Cells(colQty).Value = gvItem.Rows(0).Cells(colNetWeight).Value
                        End If

                    End If

                    If e.Column Is gvItem.Columns(colItemCode) Then
                        isCellValueChanged = True
                        OpenICodeList(False)
                        Cal_FAT()
                        Cal_SNF()
                        isCellValueChanged = False
                    End If

                    If e.Column Is gvItem.Columns(colUOM) Then
                        isCellValueChanged = True
                        OpenUOMList(False)
                        Cal_FAT()
                        Cal_SNF()
                        isCellValueChanged = False
                    End If

                    If (e.Column Is gvItem.Columns(colQty)) Or (e.Column Is gvItem.Columns(colFat)) Or (e.Column Is gvItem.Columns(colFatKG)) Then
                        isCellValueChanged = True
                        If validatefatsnf Then
                            If clsCommon.myCdbl(gvItem.CurrentRow.Cells(colFatKG).Value) > clsCommon.myCdbl(gvItem.CurrentRow.Cells(colBalFatKG).Value) Then
                                RadPageView1.SelectedPage = RadPageViewPage1
                                gvItem.CurrentRow.Cells(colFatKG).Value = Nothing
                                Throw New Exception("Filled FAT KG cannot be more than available FAT KG.")
                            End If
                        End If
                        Cal_FAT()
                        isCellValueChanged = False
                    End If

                    If (e.Column Is gvItem.Columns(colQty)) Or (e.Column Is gvItem.Columns(colSNF)) Or (e.Column Is gvItem.Columns(colSNFKG)) Then
                        isCellValueChanged = True
                        If validatefatsnf Then
                            If clsCommon.myCdbl(gvItem.CurrentRow.Cells(colSNFKG).Value) > clsCommon.myCdbl(gvItem.CurrentRow.Cells(colBalSNFKG).Value) Then
                                RadPageView1.SelectedPage = RadPageViewPage1
                                gvItem.CurrentRow.Cells(colSNFKG).Value = Nothing
                                Throw New Exception("Filled SNF KG cannot be more than available SNF KG.")
                            End If
                        End If
                        Cal_SNF()
                        isCellValueChanged = False
                    End If
                    isCellValueChangedOpen = False
                End If

            End If
        Catch ex As Exception
            isCellValueChangedOpen = False
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally

        End Try
    End Sub
    Private Sub Cal_FAT()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing

            qty = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colqty).Value)
            fat = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colFat).Value)

            fat_kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value), qty, fat, Nothing) ' Math.Round((qty * fat) / 100, DecimalPoint)
            gvItem.CurrentRow.Cells(colfatkg).Value = fat_kg
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub Cal_SNF()
        Try
            Dim qty As Decimal = Nothing
            Dim fat As Decimal = Nothing
            Dim fat_kg As Decimal = Nothing

            qty = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colqty).Value)
            fat = clsCommon.myCdbl(gvItem.CurrentRow.Cells(colSNF).Value)

            fat_kg = clsBOM.GetFatSNFKG_AfterConversion(clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value), clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value), qty, fat, Nothing) ' Math.Round((qty * fat) / 100, DecimalPoint)
            gvItem.CurrentRow.Cells(colsnfkg).Value = fat_kg
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub


    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim whrcls As String = ""
        Dim obj As clsItemMaster

        obj = clsItemMaster.FinderForItem(clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value), "", isButtonClick, Nothing, whrcls)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gvItem.CurrentRow.Cells(colItemCode).Value = obj.Item_Code
            gvItem.CurrentRow.Cells(colItemDesc).Value = obj.Item_Desc
            gvItem.CurrentRow.Cells(colUOM).Value = obj.Unit_Code
            gvItem.CurrentRow.Cells(colFat).Value = clsCommon.myCstr(clsBOM.GetFAT_PERS(obj.Item_Code))
            gvItem.CurrentRow.Cells(colSNF).Value = clsCommon.myCstr(clsBOM.GetSNF_PERS(obj.Item_Code))
            gvItem.CurrentRow.Cells(colBalance).Value = GetBalanceQty()
            gvItem.CurrentRow.Cells(colBalFatKG).Value = GetBalanceFatkg()
            gvItem.CurrentRow.Cells(colBalSNFKG).Value = GetBalanceSNFkg()
            'gvItem.CurrentRow.Cells(colQty).Value = 0
        Else
            gvItem.CurrentRow.Cells(colItemCode).Value = ""
            gvItem.CurrentRow.Cells(colItemDesc).Value = ""
            gvItem.CurrentRow.Cells(colUOM).Value = ""
            gvItem.CurrentRow.Cells(colFat).Value = 0.0
            gvItem.CurrentRow.Cells(colSNF).Value = 0.0
            gvItem.CurrentRow.Cells(colBalance).Value = 0.0
            gvItem.CurrentRow.Cells(colBalFatKG).Value = 0.0
            gvItem.CurrentRow.Cells(colBalSNFKG).Value = 0.0
            ' gvItem.CurrentRow.Cells(colQty).Value = 0
        End If
    End Sub

    Private Sub btnReverse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReverse.Click

    End Sub

    Private Sub fndSRNNo__MYNavigator(ByVal sender As Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles fndSRNNo._MYNavigator
        LoadData(fndSRNNo.Value, NavType)
    End Sub

    Private Sub fndSRNNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndSRNNo._MYValidating
        Try
            Dim whrCls As String = String.Empty
            If Not clsMccMaster.isCurrentUserHO() Then
                If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                    whrCls = "  loc_code in  (" & objCommonVar.strCurrUserLocations & ") "
                End If
            End If
            fndSRNNo.Value = clsJobMilkSRN.getFinder(whrCls, fndSRNNo.Value, isButtonClicked)
            If clsCommon.myLen(fndSRNNo.Value) > 0 Then
                LoadData(fndSRNNo.Value, NavigatorType.Current)
            Else
                Reset()
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub fndWeighmentNo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles fndWeighmentNo.Load

    End Sub

    Private Sub fndTankerNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles fndTankerNo._MYValidating
        Try
            If RdbManual.CheckState = CheckState.Unchecked And RdbSkuReceipt.CheckState = CheckState.Unchecked And RdbTankerReceipt.CheckState = CheckState.Unchecked Then
                clsCommon.MyMessageBoxShow("Please Select a Type First")
                RdbTankerReceipt.Focus()
            End If
            Dim whrCls As String = String.Empty

            'If clsCommon.myLen(whrCls) > 0 Then
            '    whrCls = whrCls & " and "
            'End If
            'Dim PrevCode As String = fndTankerNo.Value
            Dim qry As String = String.Empty
            If RdbSkuReceipt.IsChecked = True Then
                qry = "   select TSPL_Milk_Gate_Entry_Details.Tanker_No as [TankerNo],'' as [Weighment No] ,Null as [Weighment Date] ,TSPL_Milk_Gate_Entry_Details.Gate_Entry_No as [Gate Entry No] ,TSPL_Milk_Gate_Entry_Details.Doc_Type as [Doc Type] ,TSPL_Milk_Gate_Entry_Details.Date_And_Time as [Gate Entry Date And Time] ,TSPL_Milk_Gate_Entry_Details.Challan_No as [Challan No] ,TSPL_Milk_Gate_Entry_Details.Challan_Date as [Challan Date]  ,case when isnull(TSPL_Milk_Gate_Entry_Details.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_Milk_Gate_Entry_Details.Posting_Date as [Posting Date]  ,TSPL_Milk_Gate_Entry_Details.location_Code as [Location Code] ,TSPL_Milk_Gate_Entry_Details.Location_Desc as [Location Desc] ,TSPL_Milk_Gate_Entry_Details.Vendor_Code as [Vendor Code] ,TSPL_Milk_Gate_Entry_Details.Vendor_Desc as [Vendor Desc] ,TSPL_Milk_Gate_Entry_Details.Item_Code as [Item Code] ,TSPL_Milk_Gate_Entry_Details.Item_Desc as [Item Desc] ,TSPL_Milk_Gate_Entry_Details.Qty_In_Kg as [Qty] ,TSPL_Milk_Gate_Entry_Details.snf_Per as [SNF(%)] ,TSPL_Milk_Gate_Entry_Details.fat_per as [FAT(%)] ,TSPL_Milk_Gate_Entry_Details.Created_By as [Created By] ,TSPL_Milk_Gate_Entry_Details.Created_Date as [Created Date] ,TSPL_Milk_Gate_Entry_Details.Modify_By as [Modify By] ,TSPL_Milk_Gate_Entry_Details.Modify_Date as [Modify Date] ,TSPL_Milk_Gate_Entry_Details.comp_code as [Company Code] ,0 as [Gross Weight] ,0 as [Dip Value] ,0 as [Tare Weight] ,0 as [Net Weight]   From TSPL_Milk_Gate_Entry_Details	 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Milk_Gate_Entry_Details.vendor_code where 1=1 "
                If Not clsMccMaster.isCurrentUserHO() Then
                    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        whrCls = " and  TSPL_Milk_Gate_Entry_Details.location_Code  in (" & objCommonVar.strCurrUserLocations & ")"
                    End If
                End If
                whrCls = whrCls & " and    TSPL_Milk_Gate_Entry_Details.isPosted=1 and TSPL_Milk_Gate_Entry_Details.doc_type='Sku_Receipt'  and TSPL_Milk_Gate_Entry_Details.Gate_Entry_No not in (select isnull(Gate_Entry_No,'') from TSPL_Job_MILK_SRN where isnull(srn_return_no,'')='' )"
            ElseIf RdbTankerReceipt.IsChecked = True Then
                qry = "  select TSPL_Milk_Weighment_Detail.Tanker_No as [TankerNo],TSPL_Milk_Weighment_Detail.Weighment_No as [Weighment No] ,TSPL_Milk_Weighment_Detail.Weighment_date as [Weighment Date] ,TSPL_Milk_Weighment_Detail.Gate_Entry_No as [Gate Entry No] ,TSPL_Milk_Weighment_Detail.Doc_Type as [Doc Type] ,TSPL_Milk_Weighment_Detail.Date_And_Time as [Gate Entry Date And Time] ,TSPL_Milk_Weighment_Detail.Challan_No as [Challan No] ,TSPL_Milk_Weighment_Detail.Challan_Date as [Challan Date]  ,case when isnull(TSPL_Milk_Weighment_Detail.isPosted,0)=0 then 'No' else 'Yes' end as [Is Posted] ,TSPL_Milk_Weighment_Detail.Posting_Date as [Posting Date]  ,TSPL_Milk_Weighment_Detail.location_Code as [Location Code] ,TSPL_Milk_Weighment_Detail.Location_Desc as [Location Desc] ,TSPL_Milk_Weighment_Detail.Vendor_Code as [Vendor Code] ,TSPL_Milk_Weighment_Detail.Vendor_Desc as [Vendor Desc] ,TSPL_Milk_Weighment_Detail.Item_Code as [Item Code] ,TSPL_Milk_Weighment_Detail.Item_Desc as [Item Desc] ,TSPL_Milk_Weighment_Detail.Qty_In_Kg as [Qty] ,TSPL_Milk_Weighment_Detail.snf_Per as [SNF(%)] ,TSPL_Milk_Weighment_Detail.fat_per as [FAT(%)] ,TSPL_Milk_Weighment_Detail.Created_By as [Created By] ,TSPL_Milk_Weighment_Detail.Created_Date as [Created Date] ,TSPL_Milk_Weighment_Detail.Modify_By as [Modify By] ,TSPL_Milk_Weighment_Detail.Modify_Date as [Modify Date] ,TSPL_Milk_Weighment_Detail.comp_code as [Company Code] ,TSPL_Milk_Weighment_Detail.Gross_Weight as [Gross Weight] ,TSPL_Milk_Weighment_Detail.Dip_Value as [Dip Value] ,TSPL_Milk_Weighment_Detail.Tare_Weight as [Tare Weight] ,TSPL_Milk_Weighment_Detail.Net_Weight as [Net Weight]   From TSPL_Milk_Weighment_Detail	 left outer join TSPL_VENDOR_MASTER on TSPL_VENDOR_MASTER.Vendor_Code=TSPL_Milk_Weighment_Detail.vendor_code where 1=1  "
                If Not clsMccMaster.isCurrentUserHO() Then
                    If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                        whrCls = " and  TSPL_Milk_Weighment_Detail.location_Code  in (" & objCommonVar.strCurrUserLocations & ")"
                    End If
                End If
                whrCls = whrCls & " and    TSPL_Milk_Weighment_Detail.isPosted=1 and TSPL_Milk_Weighment_Detail.doc_type='Tanker' and TSPL_Milk_Weighment_Detail.Weighment_No not in (select isnull(Weighment_No,'') from TSPL_Job_MILK_SRN where isnull(srn_return_no,'')='' )"
            End If
            qry = qry & whrCls

            Dim dt As DataRow = clsCommon.ShowSelectFormForRow("JobMilkSRN", qry, "TankerNo", fndTankerNo.Value)
            If dt Is Nothing Then
                Exit Sub
            End If
            'fndWeighmentNo.Value = clsJobMilkSRN.getWeighmentFinder(whrCls, fndWeighmentNo.Value, isButtonClicked)
            fndWeighmentNo.Value = clsCommon.myCstr(dt("Weighment No"))
            txtGateEntryNo.Text = clsCommon.myCstr(dt("Gate Entry No"))
            'Gate Entry No
            Dim chk As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(*) from tspl_transaction_approval where document_no=(select QC_No from tspl_Milk_quality_check where gate_entry_no='" & clsCommon.myCstr(dt("Gate Entry No")) & "') and approve=0"))
            'If clsCommon.myLen(fndWeighmentNo.Value) <= 0 Then
            '    fndWeighmentNo.Value = PrevCode
            'End If
            If chk > 0 Then
                Throw New Exception("You can not process SRN of this tanker. It is pending for special Deduction")
            End If
            If clsCommon.myLen(fndWeighmentNo.Value) > 0 Then
                isDocPosted = False
                loadWeighmentData(fndWeighmentNo.Value)
            ElseIf RdbSkuReceipt.IsChecked = True Then
                isDocPosted = False
                loadGateEntryData(txtGateEntryNo.Text)
            Else
                Reset()

            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub gvItem_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles gvItem.Validating

    End Sub

    Private Sub btnPrintPO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrintPO.Click
        printPoData()
    End Sub
    Sub printPoData()
        clsCommon.MyMessageBoxShow("No Print Format Found")
    End Sub

    Private Sub FndLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndLocation._MYValidating
        '' Anubhooti 07-Oct-2014 (Location Finder Changes On The Basis Of Rejected Location)
        Dim qry As String = "Select Location_Code As [Code],ISNULL(Location_Desc,'') As [Location Desc]  From TSPL_LOCATION_MASTER  "
        Dim WhrCls As String = " Rejected_Type='N' AND Location_Type='Physical'  "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        FndLocation.Value = clsCommon.ShowSelectForm("RejLocNRGP", qry, "Code", WhrCls, FndLocation.Value, "Code", isButtonClicked)
        If clsCommon.myLen(FndLocation.Value) > 0 Then
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select isnull(Location_Desc,'') AS Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + FndLocation.Value + "'"))
            txtLocation.Text = FndLocation.Value
        Else
            lblLocationDesc.Text = ""
            txtLocation.Text = ""
        End If
        'isCellValueChangedOpen = True
        loadBlankItemGrid()
        'isCellValueChangedOpen = False
        'gvItem.Rows.Clear()
        'gvItem.Rows.AddNew()
    End Sub

    Private Sub fndVendor__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles FndVendor._MYValidating
        Dim qry As String = "select dISTINCT TSPL_MILK_RGP_HEAD.Vendor_Code as [Code],Vendor_Name  as [Name] from  TSPL_MILK_RGP_HEAD "
        FndVendor.Value = clsCommon.ShowSelectForm("RGPCustFNDer", qry, "Code", "Location='" & clsCommon.myCstr(FndLocation.Value) & "'", FndVendor.Value, "Code", isButtonClicked)
        If clsCommon.myLen(FndVendor.Value) > 0 Then
            lblVendorName.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vendor_Name from TSPL_Vendor_MASTER where Vendor_Code ='" + FndVendor.Value + "'"))
            txtVendor.Text = FndVendor.Value
        Else
            lblVendorName.Text = ""
            txtVendor.Text = ""
        End If
    End Sub

    Private Sub RdbManual_CheckStateChanged(sender As Object, e As EventArgs) Handles RdbManual.CheckStateChanged
        Try
            If RdbManual.IsChecked = True Then
                TxtTankerNo.Visible = True
                TxtTankerNo.Enabled = True
                FndVendor.Visible = True
                FndLocation.Visible = True
                txtChallanNo.ReadOnly = False
                dtpChallanDate.Enabled = True
                dtpChallanDate.ReadOnly = False
                fndTankerNo.Visible = False
                txtVendor.Visible = False
                txtLocation.Visible = False
                RadPageView1.Pages("QcDetails").Item.Visibility = ElementVisibility.Hidden
                If gvItem.Columns.Count > 0 Then
                    gvItem.Columns(colQty).IsVisible = True
                End If
            Else
                TxtTankerNo.Visible = False
                TxtTankerNo.Text = Nothing
                FndVendor.Value = Nothing
                FndLocation.Value = Nothing
                FndVendor.Visible = False
                FndLocation.Visible = False
                txtChallanNo.ReadOnly = True
                dtpChallanDate.Enabled = False
                dtpChallanDate.ReadOnly = True
                fndTankerNo.Visible = True
                txtVendor.Visible = True
                txtLocation.Visible = True
                RadPageView1.Pages("QcDetails").Item.Visibility = ElementVisibility.Visible
                If gvItem.Columns.Count > 0 Then
                    gvItem.Columns(colQty).IsVisible = False
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim qry As String = "select UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL"
            Dim whrCls As String = "Item_Code='" + strICode + "'"
            gvItem.CurrentRow.Cells(colUOM).Value = clsCommon.ShowSelectForm("SRNItefndnder", qry, "Code", whrCls, clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value), "Code", isButtonClick)
            gvItem.CurrentRow.Cells(colBalance).Value = GetBalanceQty()
        End If
    End Sub

    Function GetBalanceQty()
        Try
            If gvItem.Rows.Count > 0 Then ' and TSPL_MILK_RGP_DETAIL.item_code='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value) & "' 
                If clsCommon.myLen(clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value)) > 0 Then
                    Dim sQuery As String = "select sum(FAT_KG)+sum(SNF_KG)-coalesce(tt.QTy,0) as Qty,Unit_Code from tspl_milk_RGP_Head inner join TSPL_MILK_RGP_DETAIL on TSPL_MILK_RGP_DETAIL.RGP_No=tspl_milk_RGP_Head.RGP_No " _
                                           & " and Vendor_Code='" & FndVendor.Value & "' and Location='" & clsCommon.myCstr(FndLocation.Value) & "'  and tspl_milk_RGP_Head.status=1" _
                                           & " left join (select (Sum(FAT_KG)+Sum(SNF_KG)) as Qty,Vendor_Code,Loc_Code from TSPL_JOB_MILK_Srn group by Vendor_Code,Loc_Code) tt on tt.vendor_COde=tspl_milk_RGP_Head.vendor_Code and tt.loc_COde=tspl_milk_RGP_Head.Location group by Unit_Code,tt.QTy " 'and TSPL_MILK_RGP_DETAIL.UNIT_code='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value) & "'
                    Dim DtBalance As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                    If DtBalance.Rows.Count > 0 Then
                        Conv_factor = 1
                        Return clsCommon.myCdbl(DtBalance.Rows(0).Item("Qty")) * Conv_factor
                    Else
                        Return 0
                    End If
                End If
            End If
            Return 0
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
            Return 0
        End Try
    End Function

    Function GetBalanceFatkg()
        Try
            If gvItem.Rows.Count > 0 Then
                If clsCommon.myLen(clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value)) > 0 Then
                    Dim sQuery As String = "select sum(FAT_KG)-coalesce(tt.QTy,0) as Qty,Unit_Code from tspl_milk_RGP_Head inner join TSPL_MILK_RGP_DETAIL on TSPL_MILK_RGP_DETAIL.RGP_No=tspl_milk_RGP_Head.RGP_No " _
                                           & " and Vendor_Code='" & FndVendor.Value & "' and Location='" & clsCommon.myCstr(FndLocation.Value) & "'  and tspl_milk_RGP_Head.status=1" _
                                           & " left join (select (Sum(FAT_KG)) as Qty,Vendor_Code,Loc_Code from TSPL_JOB_MILK_Srn group by Vendor_Code,Loc_Code) tt on tt.vendor_COde=tspl_milk_RGP_Head.vendor_Code and tt.loc_COde=tspl_milk_RGP_Head.Location group by Unit_Code,tt.QTy " 'and TSPL_MILK_RGP_DETAIL.UNIT_code='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value) & "'
                    Dim DtBalance As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                    If DtBalance.Rows.Count > 0 Then
                        Conv_factor = 1
                        Return clsCommon.myCdbl(DtBalance.Rows(0).Item("Qty")) * Conv_factor
                    Else
                        Return 0
                    End If
                End If
            End If
            Return 0
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
            Return 0
        End Try
    End Function

    Function GetBalanceSNFkg()
        Try
            If gvItem.Rows.Count > 0 Then
                If clsCommon.myLen(clsCommon.myCstr(gvItem.CurrentRow.Cells(colItemCode).Value)) > 0 Then
                    Dim sQuery As String = "select sum(SNF_KG)-coalesce(tt.QTy,0) as Qty,Unit_Code from tspl_milk_RGP_Head inner join TSPL_MILK_RGP_DETAIL on TSPL_MILK_RGP_DETAIL.RGP_No=tspl_milk_RGP_Head.RGP_No " _
                                           & " and Vendor_Code='" & FndVendor.Value & "' and Location='" & clsCommon.myCstr(FndLocation.Value) & "'  and tspl_milk_RGP_Head.status=1" _
                                           & " left join (select (Sum(SNF_KG)) as Qty,Vendor_Code,Loc_Code from TSPL_JOB_MILK_Srn group by Vendor_Code,Loc_Code) tt on tt.vendor_COde=tspl_milk_RGP_Head.vendor_Code and tt.loc_COde=tspl_milk_RGP_Head.Location group by Unit_Code,tt.QTy " 'and TSPL_MILK_RGP_DETAIL.UNIT_code='" & clsCommon.myCstr(gvItem.CurrentRow.Cells(colUOM).Value) & "'
                    Dim DtBalance As DataTable = clsDBFuncationality.GetDataTable(sQuery)
                    If DtBalance.Rows.Count > 0 Then
                        Conv_factor = 1
                        Return clsCommon.myCdbl(DtBalance.Rows(0).Item("Qty")) * Conv_factor
                    Else
                        Return 0
                    End If
                End If
            End If
            Return 0
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.ToString)
            Return 0
        End Try
    End Function
End Class
