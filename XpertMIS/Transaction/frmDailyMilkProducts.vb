Imports common
Imports System.Data
Imports System.Data.SqlClient
Imports XpertERPEngine
Imports Telerik.WinControls.UI
Imports XpertERPEngineFine
Public Class frmDailyMilkProducts
    Inherits FrmMainTranScreen


#Region "Variables"
    Dim ReportID As String = clsUserMgtCode.frmDailyMilkProducts
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False
    Private isLoadGrid As Boolean = False

#End Region


    Private Sub frmDailyMilkProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        isNewEntry = True
        isLoadGrid = True
        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for Save/Update ")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D  for Delete ")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnNew, "Press Alt+N Adding New ")
        BlankAllControls()
        LoadGrid()
    End Sub
    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnsave.Visible = MyBase.isModifyFlag
        btnsave.Visible = True
        btnDelete.Visible = MyBase.isDeleteFlag
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        isLoadGrid = True
        AddNew()
    End Sub
    Sub AddNew()
        LoadGrid()
        BlankAllControls()
        isNewEntry = True
        btnsave.Text = "Save"
        btnsave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtReportDate.Focus()
        txtReportingDate.Focus()
        '' gv1.Rows.AddNew()
    End Sub
    Sub BlankAllControls()
        '' LoadBlankGrid()
        txtDocNo.MyReadOnly = False
        txtDocNo.Value = Nothing
        txtMilkProcOwnDC.Value = "0"
        txtMilkReceiptRMG.Value = "0"
        txtLocalMilkMarketing.Value = "0"
        txtSuppliestNMG.Value = "0"
        txtSuppliestRMG.Value = "0"
        txtMilkIssued.Value = "0"
        txtFAT.Value = "0"
        txtSNF.Value = "0"
        txtMilkReceiveConversion.Value = "0"
        txtOwnMilkConv.Value = "0"
        txtMilkDispConv.Value = "0"
        txtRCDFunitName1.Text = ""
        txtRCDFunitName2.Text = ""
        txtRCDFunitName3.Text = ""
        txtRCDFunitName4.Text = ""
        txtRCDFunitName5.Text = ""
        txtRCDFunit1.Value = "0"
        txtRCDFunit2.Value = "0"
        txtRCDFunit3.Value = "0"
        txtRCDFunit4.Value = "0"
        txtRCDFunit5.Value = "0"
        txtReportingDate.Value = clsCommon.GETSERVERDATE()
        txtReportDate.Value = DateTime.Today.AddDays(-1)
        txtRmrks.Text = ""
        txtSMPPurchase.Value = "0"
        txtGheePurchase.Value = "0"
        txtGheeReceipt.Value = "0"
        txtSMPReceipt.Value = "0"
        txtTableButter.Value = "0"
    End Sub
    Sub LoadGrid()
        Try
            If isLoadGrid Then
                Dim query As String = "select Item_Name as Products,Item_UOM as Unit,'' as Production, '' as Total, '' as [Inter Unit],'' as [Own Stock],'' as [Stock At Other Units],'' AS [Stock of Other Units], '' AS Reconstitution,'' as [Out-State Ghee Sale]  from TSPL_MIS_ITEM_MASTER"

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    gv1.Visible = True
                    gv1.DataSource = dt
                    SetGridFormat(gv1)
                    ReStoreGridLayout()
                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found")
                    gv1.DataSource = Nothing
                End If
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Sub

    Private Sub ReStoreGridLayout()
        Try
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
            MessageBox.Show(err.Message)
        End Try
    End Sub
    Sub SetGridFormat(ByRef Gv1 As RadGridView)
        Gv1.ShowGroupPanel = False
        Gv1.ShowRowHeaderColumn = False
        Gv1.AllowAddNewRow = False
        Gv1.AllowDeleteRow = False
        Gv1.EnableFiltering = False
        Gv1.ShowFilteringRow = False
        Gv1.MasterTemplate.SummaryRowsBottom.Clear()
        Dim summaryRowItem As New GridViewSummaryRowItem()


        For ii As Integer = 0 To Gv1.Columns.Count - 1
            'Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).BestFit()
        Next
        Gv1.MasterTemplate.SummaryRowsBottom.Add(summaryRowItem)
        Gv1.AutoSizeRows = False
        Gv1.BestFitColumns()
    End Sub

    Private Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        SaveData()
    End Sub
    Private Function SaveData() As Boolean
        Try
            Dim obj As New clsDailyMilkProducts()
            obj.Document_No = txtDocNo.Value
            obj.Report_Date = txtReportDate.Value
            obj.Reporting_Date = txtReportingDate.Value
            obj.MILK_RECEIPT = txtMilkProcOwnDC.Value
            obj.MILK_PROCUREMENT = txtMilkProcOwnDC.Value
            obj.MILK_RECEIPT = txtMilkReceiptRMG.Value
            obj.LOCAL_MILK = txtLocalMilkMarketing.Value
            obj.SUPPLIEST_NMG = txtSuppliestNMG.Value
            obj.SUPPLIEST_RMG = txtSuppliestRMG.Value
            obj.SNF = txtSNF.Value
            obj.FAT = txtFAT.Value
            obj.MILK_RECEIVED = txtMilkReceiveConversion.Value
            obj.MILK_DISPATCH = txtMilkDispConv.Value
            obj.OWN_MILK = txtOwnMilkConv.Value
            obj.RCDF_UNIT_FIRST = txtRCDFunit1.Value
            obj.RCDF_UNIT_SECOND = txtRCDFunit2.Value
            obj.RCDF_UNIT_THIRD = txtRCDFunit3.Value
            obj.RCDF_UNIT_FOURTH = txtRCDFunit4.Value
            obj.RCDF_UNIT_FIFTH = txtRCDFunit5.Value
            obj.RCDF_NAME_FIRST = txtRCDFunitName1.Text
            obj.RCDF_NAME_SECOND = txtRCDFunitName2.Text
            obj.RCDF_NAME_THIRD = txtRCDFunitName3.Text
            obj.RCDF_NAME_FOURTH = txtRCDFunitName4.Text
            obj.RCDF_NAME_FIFTH = txtRCDFunitName5.Text
            obj.GHEE_PURCHASE = txtGheePurchase.Value
            obj.GHEE_RECEIPT = txtGheeReceipt.Value
            obj.SMP_PURCHASE = txtSMPPurchase.Value
            obj.SMP_RECEIPT = txtSMPReceipt.Value
            obj.TABLE_BUTTER = txtTableButter.Value
            obj.Remarks = txtRmrks.Text

            obj.Arr = New List(Of clsDailyMilkProductsDetails)()
            Dim isFirstTime As Boolean = True
            For Each grow As GridViewRowInfo In gv1.Rows
                Dim objTr As New clsDailyMilkProductsDetails()
                objTr.Products = clsCommon.myCstr(grow.Cells("Products").Value)
                objTr.Item_UOM = clsCommon.myCstr(grow.Cells("Unit").Value)
                objTr.Production = clsCommon.myCdbl(grow.Cells("Production").Value)
                objTr.Total = clsCommon.myCdbl(grow.Cells("Total").Value)
                objTr.Inter_Unit = clsCommon.myCdbl(grow.Cells("Inter Unit").Value)
                objTr.Own_Stock = clsCommon.myCdbl(grow.Cells("Own Stock").Value)
                objTr.Stock_At_Other_Units = clsCommon.myCdbl(grow.Cells("Stock At Other Units").Value)
                objTr.Stock_Of_Other_Units = clsCommon.myCdbl(grow.Cells("Stock Of Other Units").Value)
                objTr.Reconstitution = clsCommon.myCdbl(grow.Cells("Reconstitution").Value)
                objTr.Out_State_Ghee_Sale = clsCommon.myCdbl(grow.Cells("Out-State Ghee Sale").Value)
                If (clsCommon.myLen(objTr.Products) > 0) Then
                    obj.Arr.Add(objTr)
                End If
            Next
            Dim sqlqry As String = "select count(1) from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD where Document_No ='" + txtDocNo.Value + "' "
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sqlqry))
            If count = 0 Then
                isNewEntry = True
            Else
                isNewEntry = False
            End If
            If (obj.SaveData(obj, isNewEntry)) Then
                common.clsCommon.MyMessageBoxShow(Me, "Data Saved Successfully", Me.Text)

            End If
            Return True
        Catch ex As Exception

        End Try
    End Function

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating

        Dim str As String = "select count(*) from TSPL_MIS_DAILY_MILK_PRODUCT_HEAD where Document_No ='" + txtDocNo.Value + "' "
        Dim no As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(str))
        If no = 0 AndAlso isButtonClicked = False Then
            txtDocNo.MyReadOnly = False

        Else
            txtDocNo.MyReadOnly = True
        End If
        If txtDocNo.MyReadOnly OrElse isButtonClicked Then

            txtDocNo.Value = clsDailyMilkProducts.getFinder("", txtDocNo.Value, isButtonClicked)
            If txtDocNo.Value <> "" Then
                LoadData(txtDocNo.Value, NavigatorType.Current)
            Else
                AddNew()
            End If
        End If


    End Sub

    Private Sub txtDocNo__MYNavigator(sender As Object, e As EventArgs, NavType As NavigatorType) Handles txtDocNo._MYNavigator
        txtDocNo.Value = clsDailyMilkProducts.DocNO_Navigation(NavType, txtDocNo.Value)
        LoadData(txtDocNo.Value, NavigatorType.Current)
    End Sub
    Sub LoadData(ByVal docno As String, ByVal navtype As NavigatorType)
        Try
            isInsideLoadData = True
            txtDocNo.MyReadOnly = True
            isLoadGrid = False
            Dim obj As New clsDailyMilkProducts()
            obj = clsDailyMilkProducts.GetData(docno, navtype)
            If (obj IsNot Nothing) Then
                AddNew()
                isNewEntry = False
                btnsave.Text = "Update"
                txtDocNo.Value = obj.Document_No
                txtReportDate.Value = obj.Report_Date
                txtReportingDate.Value = obj.Reporting_Date
                txtMilkProcOwnDC.Value = obj.MILK_RECEIPT
                txtMilkProcOwnDC.Value = obj.MILK_PROCUREMENT
                txtMilkReceiptRMG.Value = obj.MILK_RECEIPT
                txtLocalMilkMarketing.Value = obj.LOCAL_MILK
                txtSuppliestNMG.Value = obj.SUPPLIEST_NMG
                txtSuppliestRMG.Value = obj.SUPPLIEST_RMG
                txtSNF.Value = obj.SNF
                txtFAT.Value = obj.FAT
                txtMilkReceiveConversion.Value = obj.MILK_RECEIVED
                txtMilkDispConv.Value = obj.MILK_DISPATCH
                txtOwnMilkConv.Value = obj.OWN_MILK
                txtRCDFunit1.Value = obj.RCDF_UNIT_FIRST
                txtRCDFunit2.Value = obj.RCDF_UNIT_SECOND
                txtRCDFunit3.Value = obj.RCDF_UNIT_THIRD
                txtRCDFunit4.Value = obj.RCDF_UNIT_FOURTH
                txtRCDFunit5.Value = obj.RCDF_UNIT_FIFTH
                txtRCDFunitName1.Text = obj.RCDF_NAME_FIRST
                txtRCDFunitName2.Text = obj.RCDF_NAME_SECOND
                txtRCDFunitName3.Text = obj.RCDF_NAME_THIRD
                txtRCDFunitName4.Text = obj.RCDF_NAME_FOURTH
                txtRCDFunitName5.Text = obj.RCDF_NAME_FIFTH
                txtGheePurchase.Value = obj.GHEE_PURCHASE
                txtGheeReceipt.Value = obj.GHEE_RECEIPT
                txtSMPPurchase.Value = obj.SMP_PURCHASE
                txtSMPReceipt.Value = obj.SMP_RECEIPT
                txtTableButter.Value = obj.TABLE_BUTTER
                txtRmrks.Text = obj.Remarks
                'For Each objow As clsDailyMilkProductsDetails In obj.Arr
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Products").Value = objow.Products
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Unit").Value = objow.Item_UOM
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Production").Value = objow.Production
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Total").Value = objow.Total
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Inter Unit").Value = objow.Inter_Unit
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Own Stock").Value = objow.Own_Stock
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Stock At Other Units").Value = objow.Stock_At_Other_Units
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Stock of Other Units").Value = objow.Stock_Of_Other_Units
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Reconstitution").Value = objow.Reconstitution
                '    gv1.Rows(gv1.Rows.Count - 1).Cells("Out-State Ghee Sale").Value = objow.Out_State_Ghee_Sale

                '    gv1.Rows.AddNew()
                'Next
                Dim query As String = "select Item_Name as Products, Item_UOM as Unit, Production,Total, Inter_Unit as [Inter Unit],Own_Stock as  [Own Stock], Stock_At_Other_Units as [Stock At Other Units],Stock_Of_Other_Units as [Stock Of Other Units], Reconstitution,Out_State_Ghee_Sale as [Out-State Ghee Sale]   from TSPL_MIS_DAILY_MILK_PRODUCT_DETAIL where  Document_No=  '" + docno + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(query)
                If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
                    gv1.Visible = True
                    gv1.DataSource = dt
                    SetGridFormat(gv1)
                    ReStoreGridLayout()
                Else
                    common.clsCommon.MyMessageBoxShow("No Data Found")
                    gv1.DataSource = Nothing
                End If
            End If
                isInsideLoadData = False
        Catch ex As Exception
            isInsideLoadData = False
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Private Sub DeleteData()
        Try
            If (clsDailyMilkProducts.DeleteData(txtDocNo.Value)) Then
                clsCommon.MyMessageBoxShow(Me, "Delete Data successfully", Me.Text)
                AddNew()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class