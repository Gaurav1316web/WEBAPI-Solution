Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Windows.Forms
Imports common
Imports System.IO
Imports System.Data.SqlClient

' Ticket No : BHA/04/10/18-000595 by Prabhakar - New screen create
Public Class rptSaleAccountSetList
    Inherits FrmMainTranScreen
    Dim isLoadData As Boolean = True
    Dim isSelAccoutSetOpen = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim btnReferesh As Boolean = False
    Dim strQry As String = ""
    Const colItemCode As String = "Item Code"
    Const colItemDesc As String = "Item Desc"
    Const colStructureCode As String = "Structure Code"
    Const colStructureDesc As String = "Structure Desc"
    Const colItemType As String = "Item Type"
    Const colSaleAccountSetCode As String = "Sale Account Set Code"
    Const colSalesAccountSetDesc As String = "Sales Account Set Desc"
    Const colSalesAccount As String = "Sales Account"
    Const colSalesAccountDesc As String = "Sales Account Desc"
    Const colSalesReturnAccount As String = "Sales Return Account"
    Const colSalesReturnAccountDesc As String = "Sales Return Account Desc"
    ' Item Code  , Item Desc , Structure Code , Structure Desc , Item Type , Sale Account Set Code , Sales Account Set Desc ,
    ' Sales Account  , Sales Account Desc , Sales Return Account , Sales Return Account Desc 

    ' Cost Of Goods Sold Account , Cost Of Goods Sold Account Desc ,
    Const colCostOfGoodsSoldAccount As String = "Cost Of Goods Sold Account"
    Const colCostOfGoodsSoldAccountDesc As String = "Cost Of Goods Sold Account Desc"
    ' Cost Variance Account ,Cost Variance Account Desc
    Const colCostVarianceAccount As String = "Cost Variance Account"
    Const colCostVarianceAccountDesc As String = "Cost Variance Account Desc"
    ' Damaged Goods Account , Damaged Goods Account Desc
    Const colDamagedGoodsAccount As String = "Damaged Goods Account"
    Const colDamagedGoodsAccountDesc As String = "Damaged Goods Account Desc"
    ' Internal Usage Account , Internal Usage Account Desc
    Const colInternalUsageAccount As String = "Internal Usage Account"
    Const colInternalUsageAccountDesc As String = "Internal Usage Account Desc"
    ' Returnable Container Account , Returnable Container Account Desc
    Const colReturnableContainerAccount As String = "Returnable Container Account"
    Const colReturnableContainerAccountDesc As String = "Returnable Container Account Desc"
    ' Schemes Account , Schemes Account Desc
    Const colSchemesAccount As String = "Schemes Account"
    Const colSchemesAccountDesc As String = "Schemes Account Desc"
    ' Promotional Account , Promotional Account Desc
    Const colPromotionalAccount As String = "Promotional Account"
    Const colPromotionalAccountDesc As String = "Promotional Account Desc"
    ' Cogs InterBranch Account , Cogs InterBranch Account Desc
    Const colCogsInterBranchAccount As String = "Cogs InterBranch Account"
    Const colCogsInterBranchAccountDesc As String = "Cogs InterBranch Account Desc"
    ' Suspence Account , Suspence Account Desc
    Const colSuspenceAccount As String = "Suspence Account"
    Const colSuspenceAccountDesc As String = "Suspence Account Desc"
    ' Gain Loss Account,  Gain Loss Account Desc
    Const colGainLossAccount As String = "Gain Loss Account"
    Const colGainLossAccountDesc As String = "Stock Transfer Account Desc"
    ' Stock Transfer Account , Stock Transfer Account Desc
    Const colStockTransferAccount As String = "Stock Transfer Account"
    Const colStockTransferAccountDesc As String = "Stock Transfer Account Desc"
    Const colStockTransferAccountDesc5 As String = "Stock Transfer Account Desc5"
    ' Cost of Goods Transfer Account , Cost of Goods Transfer Account Desc
    Const colCostofGoodsTransferAccount As String = "Cost of Goods Transfer Account"
    Const colCostofGoodsTransferAccountDesc As String = "Cost of Goods Transfer Account Desc"
    ' Display Purpose Account , Display Purpose Account Desc
    Const colDisplayPurposeAccount As String = "Display Purpose Account"
    Const colDisplayPurposeAccountDesc As String = "Display Purpose Account Desc"

    ' Cost of Good Scheme ,Cost of Good Scheme Desc
    Const colCostOfGoodsSchemeAccount As String = "Cost of Goods Scheme"
    Const colCostOfGoodsSchemeAccountDesc As String = "Cost of Goods Scheme Desc"

    ' SaleReturnHeader , CostOfGoodSoldAcountHeader , ReturnableContainerAccountHeader , StockTransferAccountHeader  , CostofGoodsTransferAccountHeader

    Const colSaleReturnHeader1 As String = "SaleReturnHeader1"
    Const colCostOfGoodSoldAcountHeader1 As String = "CostOfGoodSoldAcountHeader1"
    Const colReturnableContainerAccountHeader1 As String = "ReturnableContainerAccountHeader1"
    Const colStockTransferAccountHeader1 As String = "StockTransferAccountHeader1"
    Const colCostofGoodsTransferAccountHeader1 As String = "CostofGoodsTransferAccountHeader1"
    



    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If

    End Sub


    Private Sub RptInventoryMovement_Load(sender As Object, e As EventArgs) Handles Me.Load
        Reset()
    End Sub
    Sub Reset()
        LoadBlankGrid()
        LoadItemType()
        txtItem.arrValueMember = Nothing
        txtItemStructureCode.arrValueMember = Nothing
        txtSaleAccountSet.arrValueMember = Nothing

        Gv1.DataSource = Nothing
        Gv1.Rows.Clear()
        Gv1.Columns.Clear()
        RadPageView1.SelectedPage = RadPageViewPage1
        btnPrint.Enabled = False
        btnUpdate.Enabled = False
        Gv1.EnableFiltering = False
        chkOnlyview.Checked = False
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        isLoadData = True
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = Gv1
        LoadData(False)
        isLoadData = False
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Reset()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(MyBase.Form_ID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(Form_ID, "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= Gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To Gv1.Columns.Count - 1 Step ii + 1
                        Gv1.Columns(ii).IsVisible = False
                        Gv1.Columns(ii).VisibleInColumnChooser = True
                    Next
                    Gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub
    
    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        qry = " select Item_Code,Item_Desc from TSPL_ITEM_MASTER  order by Item_Code "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel@Batch", qry, "Item_Code", "Item_Desc", txtItem.arrValueMember, txtItem.arrDispalyMember)
    End Sub
   
    Private Sub rmsaveLayout_Click(sender As Object, e As EventArgs)
        Dim ReportID As String = MyBase.Form_ID
        If clsCommon.myLen(ReportID) > 0 Then
            Gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            Gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = Gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If


            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs)
        clsGridLayout.DeleteData(MyBase.Form_ID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information")
    End Sub


    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles RadMenuItem1.Click
        Export(EnumExportTo.Excel)
        'Try
        '    If Gv1.Rows.Count <= 0 Then
        '        clsCommon.MyMessageBoxShow("No Data Found to Export", Me.Text)
        '        Exit Sub
        '    End If
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    'arrHeader.Add(("Date Range: " + clsCommon.GetPrintDate(fromDate.Value, "dd/MM/yyyy") + " To " + clsCommon.GetPrintDate(ToDate.Value, "dd/MM/yyyy")) + " ")
        '    'arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)

        '    clsCommon.MyExportToExcelGrid("", Gv1, arrHeader, Me.Text)

        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        'End Try
    End Sub
    Private Sub RadMenuItem2_Click(sender As Object, e As EventArgs) Handles RadMenuItem2.Click
        Export(EnumExportTo.PDF)
        'Try
        '    If Gv1.Rows.Count > 0 Then
        '        Dim arrHeader As List(Of String) = New List(Of String)()
        '        arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
        '        arrHeader.Add("Sale Account Set Report")
        '        clsCommon.MyExportToPDF("Sale Account Set Report", Gv1, arrHeader, "Sale Account Set Report", True)
        '    End If
        'Catch ex As Exception
        '    common.clsCommon.MyMessageBoxShow(ex.Message)
        'End Try
    End Sub

    Private Sub Export(ByVal IsPrint As EnumExportTo)
        Try
            If Gv1.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : Sale Account Set Report")

                If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
                End If
                If txtItemStructureCode.arrDispalyMember IsNot Nothing AndAlso txtItemStructureCode.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Structure Code : " + clsCommon.GetMulcallStringWithComma(txtItemStructureCode.arrDispalyMember))
                End If
                If txtSaleAccountSet.arrDispalyMember IsNot Nothing AndAlso txtSaleAccountSet.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Sale Account Set : " + clsCommon.GetMulcallStringWithComma(txtSaleAccountSet.arrDispalyMember))
                End If
                If clsCommon.CompairString(cboItemType.Text, "Select") <> CompairStringResult.Equal Then
                    arrHeader.Add("Item Type : " + cboItemType.Text)
                End If

                If IsPrint = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("Sale Account Set Report", Gv1, arrHeader, Me.Text)
                Else
                    transportSql.applyExportTemplate(Gv1, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Sale Account Set Report", Gv1, arrHeader, "Sale Account Set Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtItemStructureCode__My_Click(sender As Object, e As EventArgs) Handles txtItemStructureCode._My_Click
        Dim qry As String
        qry = " select Structure_Code as [Code],Structure_Descq as [Description]  from TSPL_STRUCTURE_MASTER "
        txtItemStructureCode.arrValueMember = clsCommon.ShowMultipleSelectForm("StructureCod@Multi", qry, "Item_Code", "Item_Desc", txtItemStructureCode.arrValueMember, txtItemStructureCode.arrDispalyMember)
    End Sub

    Private Sub txtSaleAccountSet__My_Click(sender As Object, e As EventArgs) Handles txtSaleAccountSet._My_Click
        ' select TSPL_SALES_ACCOUNTS.Sales_Class_Code , TSPL_SALES_ACCOUNTS.Sales_Class_Desc from TSPL_SALES_ACCOUNTS
        Dim qry As String
        qry = " select TSPL_SALES_ACCOUNTS.Sales_Class_Code  as [Code],TSPL_SALES_ACCOUNTS.Sales_Class_Desc as [Description]  from TSPL_SALES_ACCOUNTS "
        txtSaleAccountSet.arrValueMember = clsCommon.ShowMultipleSelectForm("Sale@AccountSet", qry, "Item_Code", "Item_Desc", txtSaleAccountSet.arrValueMember, txtSaleAccountSet.arrDispalyMember)
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        LoadData(True)
    End Sub

    Sub SetGridFormatOFGV()


        Gv1.TableElement.TableHeaderHeight = 40
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To Gv1.Columns.Count - 1
            Gv1.Columns(ii).ReadOnly = True
            Gv1.Columns(ii).IsVisible = False
        Next

        ' Item Code  , Item Desc , Structure Code , Structure Desc ,
        Gv1.Columns("Item Code").IsVisible = True
        Gv1.Columns("Item Code").Width = 120
        Gv1.Columns("Item Code").HeaderText = "Item Code"

        Gv1.Columns("Item Desc").IsVisible = True
        Gv1.Columns("Item Desc").Width = 120
        Gv1.Columns("Item Desc").HeaderText = "Item Desc"

        Gv1.Columns("Structure Code").IsVisible = True
        Gv1.Columns("Structure Code").Width = 120
        Gv1.Columns("Structure Code").HeaderText = "Structure Code"

        Gv1.Columns("Structure Desc").IsVisible = True
        Gv1.Columns("Structure Desc").Width = 120
        Gv1.Columns("Structure Desc").HeaderText = "Structure Desc"

        ' Item Type , Sale Account Set Code , Sales Account Set Desc ,
        Gv1.Columns("Item Type").IsVisible = True
        Gv1.Columns("Item Type").Width = 120
        Gv1.Columns("Item Type").HeaderText = "Item Type"

        Gv1.Columns("Sale Account Set Code").IsVisible = True
        Gv1.Columns("Sale Account Set Code").Width = 120
        Gv1.Columns("Sale Account Set Code").HeaderText = "Sale Account Set Code"

        Gv1.Columns("Sales Account Set Desc").IsVisible = True
        Gv1.Columns("Sales Account Set Desc").Width = 120
        Gv1.Columns("Sales Account Set Desc").HeaderText = "Sales Account Set Desc"

        ' Sales Account  , Sales Account Desc ,
        Gv1.Columns("Sales Account").IsVisible = True
        Gv1.Columns("Sales Account").Width = 120
        Gv1.Columns("Sales Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Sales Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblsale' "))            '"Sales Account"

        Gv1.Columns("Sales Account Desc").IsVisible = True
        Gv1.Columns("Sales Account Desc").Width = 120
        Gv1.Columns("Sales Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Sales Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblsale' ")) ' "Sales Account Desc"

        ' Sales Return Account , Sales Return Account Desc
        Gv1.Columns("Sales Return Account").IsVisible = True
        Gv1.Columns("Sales Return Account").Width = 120
        Gv1.Columns("Sales Return Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Sale Return Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblreturns' ")) '"Sales Return Account"

        Gv1.Columns("Sales Return Account Desc").IsVisible = True
        Gv1.Columns("Sales Return Account Desc").Width = 120
        Gv1.Columns("Sales Return Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Sale Return Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblreturns' ")) '"Sales Return Account Desc"

        ' Cost Of Goods Sold Account , Cost Of Goods Sold Account Desc ,
        Gv1.Columns("Cost Of Goods Sold Account").IsVisible = True
        Gv1.Columns("Cost Of Goods Sold Account").Width = 120
        Gv1.Columns("Cost Of Goods Sold Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Of Goods Sold Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME ='ITEM-SAL-ACC' and LABEL_ID ='rdlblcostofgoodssold' ")) '"Cost Of Goods Sold Account"

        Gv1.Columns("Cost Of Goods Sold Account Desc").IsVisible = True
        Gv1.Columns("Cost Of Goods Sold Account Desc").Width = 120
        Gv1.Columns("Cost Of Goods Sold Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Of Goods Sold Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME ='ITEM-SAL-ACC' and LABEL_ID ='rdlblcostofgoodssold' "))  '"Cost Of Goods Sold Account Desc"


        ' --============================================
        ' Cost Variance Account ,Cost Variance Account Desc

        Gv1.Columns("Cost Variance Account").IsVisible = True
        Gv1.Columns("Cost Variance Account").Width = 120
        Gv1.Columns("Cost Variance Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Variance Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblcostvariance' ")) ' "Cost Variance Account"

        Gv1.Columns("Cost Variance Account Desc").IsVisible = True
        Gv1.Columns("Cost Variance Account Desc").Width = 120
        Gv1.Columns("Cost Variance Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Variance Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblcostvariance' ")) '"Cost Variance Account Desc"

        ' Damaged Goods Account , Damaged Goods Account Desc

        Gv1.Columns("Damaged Goods Account").IsVisible = True
        Gv1.Columns("Damaged Goods Account").Width = 120
        Gv1.Columns("Damaged Goods Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Damaged Goods Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblDamagedgoods' "))  '"Damaged Goods Account"

        Gv1.Columns("Damaged Goods Account Desc").IsVisible = True
        Gv1.Columns("Damaged Goods Account Desc").Width = 120
        Gv1.Columns("Damaged Goods Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Damaged Goods Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblDamagedgoods' ")) '"Damaged Goods Account Desc"
        ' Internal Usage Account , Internal Usage Account Desc
        Gv1.Columns("Internal Usage Account").IsVisible = True
        Gv1.Columns("Internal Usage Account").Width = 120
        Gv1.Columns("Internal Usage Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Internal Usage Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblInternalusage' ")) ' "Internal Usage Account"





        Gv1.Columns("Internal Usage Account Desc").IsVisible = True
        Gv1.Columns("Internal Usage Account Desc").Width = 120
        Gv1.Columns("Internal Usage Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Internal Usage Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblInternalusage' "))  ' "Internal Usage Account Desc"
        ' Returnable Container Account , Returnable Container Account Desc
        Gv1.Columns("Returnable Container Account").IsVisible = True
        Gv1.Columns("Returnable Container Account").Width = 120
        Gv1.Columns("Returnable Container Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Returnable Container Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='lblreturnable' "))  '"Returnable Container Account"

        Gv1.Columns("Returnable Container Account Desc").IsVisible = True
        Gv1.Columns("Returnable Container Account Desc").Width = 120
        Gv1.Columns("Returnable Container Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Returnable Container Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='lblreturnable' "))  '"Returnable Container Account Desc"
        ' Schemes Account , Schemes Account Desc
        Gv1.Columns("Schemes Account").IsVisible = True
        Gv1.Columns("Schemes Account").Width = 120
        Gv1.Columns("Schemes Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Schemes Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel1' ")) '"Schemes Account"

        Gv1.Columns("Schemes Account Desc").IsVisible = True
        Gv1.Columns("Schemes Account Desc").Width = 120
        Gv1.Columns("Schemes Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Schemes Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel1' "))  '"Schemes Account Desc"
        ' Promotional Account , Promotional Account Desc
        Gv1.Columns("Promotional Account").IsVisible = True
        Gv1.Columns("Promotional Account").Width = 120
        Gv1.Columns("Promotional Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Promotional Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel2' "))

        Gv1.Columns("Promotional Account Desc").IsVisible = True
        Gv1.Columns("Promotional Account Desc").Width = 120
        Gv1.Columns("Promotional Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Promotional Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel2' "))



        ' Cogs InterBranch Account , Cogs InterBranch Account Desc
        Gv1.Columns("Cogs InterBranch Account").IsVisible = True
        Gv1.Columns("Cogs InterBranch Account").Width = 120
        Gv1.Columns("Cogs InterBranch Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cogs InterBranch Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel1' "))

        Gv1.Columns("Cogs InterBranch Account Desc").IsVisible = True
        Gv1.Columns("Cogs InterBranch Account Desc").Width = 120
        Gv1.Columns("Cogs InterBranch Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cogs InterBranch Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel1' "))
        ' Suspence Account , Suspence Account Desc
        Gv1.Columns("Suspence Account").IsVisible = True
        Gv1.Columns("Suspence Account").Width = 120
        Gv1.Columns("Suspence Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Suspence Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel2' "))

        Gv1.Columns("Suspence Account Desc").IsVisible = True
        Gv1.Columns("Suspence Account Desc").Width = 120
        Gv1.Columns("Suspence Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Suspence Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel2' "))
        ' Gain Loss Account,  Gain Loss Account Desc
        Gv1.Columns("Gain Loss Account").IsVisible = True
        Gv1.Columns("Gain Loss Account").Width = 120
        Gv1.Columns("Gain Loss Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select COALESCE( MAX(NEW_LABEL_NAME), 'Gain Loss Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel3' "))

        Gv1.Columns("Gain Loss Account Desc").IsVisible = True
        Gv1.Columns("Gain Loss Account Desc").Width = 120
        Gv1.Columns("Gain Loss Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select COALESCE( MAX(NEW_LABEL_NAME), 'Gain Loss Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel3' "))
        ' Stock Transfer Account , Stock Transfer Account Desc
        Gv1.Columns("Stock Transfer Account").IsVisible = True
        Gv1.Columns("Stock Transfer Account").Width = 120
        Gv1.Columns("Stock Transfer Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Stock Transfer Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel4' "))

        Gv1.Columns("Stock Transfer Account Desc").IsVisible = True
        Gv1.Columns("Stock Transfer Account Desc").Width = 120
        Gv1.Columns("Stock Transfer Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Stock Transfer Account') + ' Desc' from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel4' "))



        Gv1.Columns("Cost of Goods Transfer Account").IsVisible = True
        Gv1.Columns("Cost of Goods Transfer Account").Width = 120
        Gv1.Columns("Cost of Goods Transfer Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost of Goods Transfer Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel5' "))

        Gv1.Columns("Cost of Goods Transfer Account Desc").IsVisible = True
        Gv1.Columns("Cost of Goods Transfer Account Desc").Width = 120
        Gv1.Columns("Cost of Goods Transfer Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost of Goods Transfer Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel5' "))
        ' Display Purpose Account , Display Purpose Account Desc
        Gv1.Columns("Display Purpose Account").IsVisible = True
        Gv1.Columns("Display Purpose Account").Width = 120
        Gv1.Columns("Display Purpose Account").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Display Purpose Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel6' "))

        Gv1.Columns("Display Purpose Account Desc").IsVisible = True
        Gv1.Columns("Display Purpose Account Desc").Width = 120
        Gv1.Columns("Display Purpose Account Desc").HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Display Purpose Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel6' "))

        Gv1.Columns("SaleReturnHeader").IsVisible = False
        Gv1.Columns("SaleReturnHeader").Width = 120
        Gv1.Columns("SaleReturnHeader").HeaderText = "SaleReturnHeader"

        Gv1.Columns("CostOfGoodSoldAcount").IsVisible = False
        Gv1.Columns("CostOfGoodSoldAcount").Width = 120
        Gv1.Columns("CostOfGoodSoldAcount").HeaderText = "CostOfGoodSoldAcount"

        Gv1.Columns("ReturnableContainerAccount").IsVisible = False
        Gv1.Columns("ReturnableContainerAccount").Width = 120
        Gv1.Columns("ReturnableContainerAccount").HeaderText = "ReturnableContainerAccount"

        Gv1.Columns("StockTransferAccount").IsVisible = False
        Gv1.Columns("StockTransferAccount").Width = 120
        Gv1.Columns("StockTransferAccount").HeaderText = "StockTransferAccount"

        Gv1.Columns("CostofGoodsTransferAccount").IsVisible = False
        Gv1.Columns("CostofGoodsTransferAccount").Width = 120
        Gv1.Columns("CostofGoodsTransferAccount").HeaderText = "CostofGoodsTransferAccount"

        ' SaleReturnHeader , CostOfGoodSoldAcount , ReturnableContainerAccount , StockTransferAccount  , CostofGoodsTransferAccount
        ' '' as SaleReturnHeader, '' as  CostOfGoodSoldAcount , '' as  ReturnableContainerAccount  , '' as  StockTransferAccount , '' as CostofGoodsTransferAccount




        'gv.ShowGroupPanel = True



    End Sub


    Sub LoadBlankGrid()

        Gv1.Rows.Clear()
        Gv1.Columns.Clear()

        ' Item Code  , Item Desc , Structure Code , Structure Desc , Item Type , Sale Account Set Code , Sales Account Set Desc , Sales Account  , Sales Account Desc , Sales Return Account , Sales Return Account Desc
        ' Cost Of Goods Sold Account , Cost Of Goods Sold Account Desc , Cost Variance Account ,Cost Variance Account Desc
        ' Damaged Goods Account , Damaged Goods Account Desc
        ' Internal Usage Account , Internal Usage Account Desc
        ' Returnable Container Account , Returnable Container Account Desc
        ' Schemes Account , Schemes Account Desc
        ' Promotional Account , Promotional Account Desc
        ' Cogs InterBranch Account , Cogs InterBranch Account Desc
        ' Suspence Account , Suspence Account Desc
        ' Gain Loss Account,  Gain Loss Account Desc
        ' Stock Transfer Account , Stock Transfer Account Desc
        ' Cost of Goods Transfer Account , Cost of Goods Transfer Account Desc
        ' Display Purpose Account , Display Purpose Account Desc

        Dim ItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 130
        ItemCode.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemCode)

        Dim ItemDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemDesc.FormatString = ""
        ItemDesc.HeaderText = "Item Desc"
        ItemDesc.Name = colItemDesc
        ItemDesc.Width = 130
        ItemDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemDesc)

        '  Structure Code , Structure Desc , Item Type , Sale Account Set Code , 
        Dim StructureCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        StructureCode.FormatString = ""
        StructureCode.HeaderText = "Structure Code"
        StructureCode.Name = colStructureCode
        StructureCode.Width = 130
        StructureCode.ReadOnly = False
        StructureCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        StructureCode.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(StructureCode)

        Dim StructureDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        StructureDesc.FormatString = ""
        StructureDesc.HeaderText = "Structure Desc"
        StructureDesc.Name = colStructureDesc
        StructureDesc.Width = 130
        StructureDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(StructureDesc)

        Dim ItemType As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemType.FormatString = ""
        ItemType.HeaderText = "Item Type"
        ItemType.Name = colItemType
        ItemType.Width = 130
        ItemType.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ItemType)

        Dim SaleAccountSetCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SaleAccountSetCode.FormatString = ""
        SaleAccountSetCode.HeaderText = "Sale Account Set Code"
        SaleAccountSetCode.Name = colSaleAccountSetCode
        SaleAccountSetCode.Width = 130
        SaleAccountSetCode.ReadOnly = False
        SaleAccountSetCode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        SaleAccountSetCode.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(SaleAccountSetCode)

        ' Sales Account Set Desc , Sales Account  , Sales Account Desc ,
        Dim SalesAccountSetDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SalesAccountSetDesc.FormatString = ""
        SalesAccountSetDesc.HeaderText = "Sales Account Set Desc"
        SalesAccountSetDesc.Name = colSalesAccountSetDesc
        SalesAccountSetDesc.Width = 130
        SalesAccountSetDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(SalesAccountSetDesc)

        Dim SalesAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SalesAccount.FormatString = ""
        SalesAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Sales Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblsale' "))
        SalesAccount.Name = colSalesAccount
        SalesAccount.Width = 130
        SalesAccount.ReadOnly = False
        SalesAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        SalesAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(SalesAccount)

        Dim SalesAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SalesAccountDesc.FormatString = ""
        SalesAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Sales Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblsale' "))
        SalesAccountDesc.Name = colSalesAccountDesc
        SalesAccountDesc.Width = 130
        SalesAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(SalesAccountDesc)

        '  Sales Return Account , Sales Return Account Desc

        Dim SalesReturnAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SalesReturnAccount.FormatString = ""
        SalesReturnAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Sale Return Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblreturns' "))
        SalesReturnAccount.Name = colSalesReturnAccount
        SalesReturnAccount.Width = 130
        SalesReturnAccount.ReadOnly = False
        SalesReturnAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        SalesReturnAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(SalesReturnAccount)

        Dim SalesReturnAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SalesReturnAccountDesc.FormatString = ""
        SalesReturnAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Sale Return Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblreturns' "))
        SalesReturnAccountDesc.Name = colSalesReturnAccountDesc
        SalesReturnAccountDesc.Width = 130
        SalesReturnAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(SalesReturnAccountDesc)

        ' 
        ' 
        ' 
        ' 
        ' 
        ' 

        ' Cost Of Goods Sold Account , Cost Of Goods Sold Account Desc , 
        Dim CostOfGoodsSoldAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostOfGoodsSoldAccount.FormatString = ""
        CostOfGoodsSoldAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Of Goods Sold Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME ='ITEM-SAL-ACC' and LABEL_ID ='rdlblcostofgoodssold' "))
        CostOfGoodsSoldAccount.Name = colCostOfGoodsSoldAccount
        CostOfGoodsSoldAccount.Width = 130
        CostOfGoodsSoldAccount.ReadOnly = False
        CostOfGoodsSoldAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        CostOfGoodsSoldAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(CostOfGoodsSoldAccount)

        Dim CostOfGoodsSoldAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostOfGoodsSoldAccountDesc.FormatString = ""
        CostOfGoodsSoldAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Of Goods Sold Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME ='ITEM-SAL-ACC' and LABEL_ID ='rdlblcostofgoodssold' "))
        CostOfGoodsSoldAccountDesc.Name = colCostOfGoodsSoldAccountDesc
        CostOfGoodsSoldAccountDesc.Width = 130
        CostOfGoodsSoldAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(CostOfGoodsSoldAccountDesc)

        ' Cost Variance Account ,Cost Variance Account Desc

        Dim CostVarianceAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostVarianceAccount.FormatString = ""
        CostVarianceAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Variance Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblcostvariance' "))
        CostVarianceAccount.Name = colCostVarianceAccount
        CostVarianceAccount.Width = 130
        CostVarianceAccount.ReadOnly = False
        CostVarianceAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        CostVarianceAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(CostVarianceAccount)

        Dim CostVarianceAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostVarianceAccountDesc.FormatString = ""
        CostVarianceAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Variance Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblcostvariance' "))
        CostVarianceAccountDesc.Name = colCostVarianceAccountDesc
        CostVarianceAccountDesc.Width = 130
        CostVarianceAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(CostVarianceAccountDesc)

        ' Damaged Goods Account , Damaged Goods Account Desc

        Dim DamagedGoodsAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DamagedGoodsAccount.FormatString = ""
        DamagedGoodsAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Damaged Goods Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblDamagedgoods' "))
        DamagedGoodsAccount.Name = colDamagedGoodsAccount
        DamagedGoodsAccount.Width = 130
        DamagedGoodsAccount.ReadOnly = False
        DamagedGoodsAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        DamagedGoodsAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(DamagedGoodsAccount)

        Dim DamagedGoodsAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DamagedGoodsAccountDesc.FormatString = ""
        DamagedGoodsAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Damaged Goods Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblDamagedgoods' "))
        DamagedGoodsAccountDesc.Name = colDamagedGoodsAccountDesc
        DamagedGoodsAccountDesc.Width = 130
        DamagedGoodsAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(DamagedGoodsAccountDesc)

        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        ' 
        '

        ' Internal Usage Account , Internal Usage Account Desc

        Dim InternalUsageAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        InternalUsageAccount.FormatString = ""
        InternalUsageAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Internal Usage Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblInternalusage' "))
        InternalUsageAccount.Name = colInternalUsageAccount
        InternalUsageAccount.Width = 130
        InternalUsageAccount.ReadOnly = False
        InternalUsageAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        InternalUsageAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(InternalUsageAccount)

        Dim InternalUsageAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        InternalUsageAccountDesc.FormatString = ""
        InternalUsageAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Internal Usage Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblInternalusage' "))
        InternalUsageAccountDesc.Name = colInternalUsageAccountDesc
        InternalUsageAccountDesc.Width = 130
        InternalUsageAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(InternalUsageAccountDesc)



        ' Returnable Container Account , Returnable Container Account Desc

        Dim ReturnableContainerAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ReturnableContainerAccount.FormatString = ""
        ReturnableContainerAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Returnable Container Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='lblreturnable' "))
        ReturnableContainerAccount.Name = colReturnableContainerAccount
        ReturnableContainerAccount.Width = 130
        ReturnableContainerAccount.ReadOnly = False
        ReturnableContainerAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        ReturnableContainerAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(ReturnableContainerAccount)

        Dim ReturnableContainerAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ReturnableContainerAccountDesc.FormatString = ""
        ReturnableContainerAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Returnable Container Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='lblreturnable' "))
        ReturnableContainerAccountDesc.Name = colReturnableContainerAccountDesc
        ReturnableContainerAccountDesc.Width = 130
        ReturnableContainerAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(ReturnableContainerAccountDesc)

        ' Schemes Account , Schemes Account Desc
        Dim SchemesAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SchemesAccount.FormatString = ""
        SchemesAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Schemes Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel1' "))
        SchemesAccount.Name = colSchemesAccount
        SchemesAccount.Width = 130
        SchemesAccount.ReadOnly = False
        SchemesAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        SchemesAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(SchemesAccount)

        Dim SchemesAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SchemesAccountDesc.FormatString = ""
        SchemesAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Schemes Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel1' "))
        SchemesAccountDesc.Name = colSchemesAccountDesc
        SchemesAccountDesc.Width = 130
        SchemesAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(SchemesAccountDesc)

        ' Promotional Account , Promotional Account Desc
        Dim PromotionalAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        PromotionalAccount.FormatString = ""
        PromotionalAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Promotional Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel2' "))
        PromotionalAccount.Name = colPromotionalAccount
        PromotionalAccount.Width = 130
        PromotionalAccount.ReadOnly = False
        PromotionalAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        PromotionalAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(PromotionalAccount)

        Dim PromotionalAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        PromotionalAccountDesc.FormatString = ""
        PromotionalAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Promotional Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel2' "))
        PromotionalAccountDesc.Name = colPromotionalAccountDesc
        PromotionalAccountDesc.Width = 130
        PromotionalAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(PromotionalAccountDesc)

        '
        ' 
        '
        ' 
        ' 
        ' 
        ' 
        ' 

        ' Cogs InterBranch Account , Cogs InterBranch Account Desc
        Dim CogsInterBranchAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CogsInterBranchAccount.FormatString = ""
        CogsInterBranchAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cogs InterBranch Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel1' "))
        CogsInterBranchAccount.Name = colCogsInterBranchAccount
        CogsInterBranchAccount.Width = 130
        CogsInterBranchAccount.ReadOnly = False
        CogsInterBranchAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        CogsInterBranchAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(CogsInterBranchAccount)

        Dim CogsInterBranchAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CogsInterBranchAccountDesc.FormatString = ""
        CogsInterBranchAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cogs InterBranch Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel1' "))
        CogsInterBranchAccountDesc.Name = colCogsInterBranchAccountDesc
        CogsInterBranchAccountDesc.Width = 130
        CogsInterBranchAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(CogsInterBranchAccountDesc)
        ' Suspence Account , Suspence Account Desc
        Dim SuspenceAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SuspenceAccount.FormatString = ""
        SuspenceAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Suspence Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel2' "))
        SuspenceAccount.Name = colSuspenceAccount
        SuspenceAccount.Width = 130
        SuspenceAccount.ReadOnly = False
        SuspenceAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        SuspenceAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(SuspenceAccount)

        Dim SuspenceAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SuspenceAccountDesc.FormatString = ""
        SuspenceAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Suspence Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel2' "))
        SuspenceAccountDesc.Name = colSuspenceAccountDesc
        SuspenceAccountDesc.Width = 130
        SuspenceAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(SuspenceAccountDesc)

        ' Gain Loss Account,  Gain Loss Account Desc
        Dim GainLossAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        GainLossAccount.FormatString = ""
        GainLossAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select COALESCE( MAX(NEW_LABEL_NAME), 'Gain Loss Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel3' "))
        GainLossAccount.Name = colGainLossAccount
        GainLossAccount.Width = 130
        GainLossAccount.ReadOnly = False
        GainLossAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        GainLossAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(GainLossAccount)

        Dim GainLossAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        GainLossAccountDesc.FormatString = ""
        GainLossAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select COALESCE( MAX(NEW_LABEL_NAME), 'Gain Loss Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel3' "))
        GainLossAccountDesc.Name = colGainLossAccountDesc
        GainLossAccountDesc.Width = 130
        GainLossAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(GainLossAccountDesc)

        ' Stock Transfer Account , Stock Transfer Account Desc
        Dim StockTransferAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        StockTransferAccount.FormatString = ""
        StockTransferAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Stock Transfer Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel4' "))
        StockTransferAccount.Name = colStockTransferAccount
        StockTransferAccount.Width = 130
        StockTransferAccount.ReadOnly = False
        StockTransferAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        StockTransferAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(StockTransferAccount)

        Dim StockTransferAccountDesc5 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        StockTransferAccountDesc5.FormatString = ""
        StockTransferAccountDesc5.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Stock Transfer Account') + ' Desc' from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel4' "))
        StockTransferAccountDesc5.Name = colStockTransferAccountDesc5
        StockTransferAccountDesc5.Width = 130
        StockTransferAccountDesc5.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(StockTransferAccountDesc5)

        ' Cost of Goods Transfer Account , Cost of Goods Transfer Account Desc
        ' 
        ' 
        ' 
        ' 
        ' Cost of Goods Transfer Account , Cost of Goods Transfer Account Desc
        Dim CostofGoodsTransferAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostofGoodsTransferAccount.FormatString = ""
        CostofGoodsTransferAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost of Goods Transfer Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel5' "))
        CostofGoodsTransferAccount.Name = colCostofGoodsTransferAccount
        CostofGoodsTransferAccount.Width = 130
        CostofGoodsTransferAccount.ReadOnly = False
        CostofGoodsTransferAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        CostofGoodsTransferAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(CostofGoodsTransferAccount)

        Dim CostofGoodsTransferAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostofGoodsTransferAccountDesc.FormatString = ""
        CostofGoodsTransferAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost of Goods Transfer Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel5' "))
        CostofGoodsTransferAccountDesc.Name = colCostofGoodsTransferAccountDesc
        CostofGoodsTransferAccountDesc.Width = 130
        CostofGoodsTransferAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(CostofGoodsTransferAccountDesc)
        ' Display Purpose Account , Display Purpose Account Desc
        Dim DisplayPurposeAccount As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DisplayPurposeAccount.FormatString = ""
        DisplayPurposeAccount.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Display Purpose Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel6' "))
        DisplayPurposeAccount.Name = colDisplayPurposeAccount
        DisplayPurposeAccount.Width = 130
        DisplayPurposeAccount.ReadOnly = False
        DisplayPurposeAccount.HeaderImage = Global.ERP.My.Resources.Resources.search4
        DisplayPurposeAccount.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(DisplayPurposeAccount)

        Dim DisplayPurposeAccountDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        DisplayPurposeAccountDesc.FormatString = ""
        DisplayPurposeAccountDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Display Purpose Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel6' "))
        DisplayPurposeAccountDesc.Name = colDisplayPurposeAccountDesc
        DisplayPurposeAccountDesc.Width = 130
        DisplayPurposeAccountDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(DisplayPurposeAccountDesc)

        Dim CostOfGoodsScheme As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostOfGoodsScheme.FormatString = ""
        CostOfGoodsScheme.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Of Goods Scheme Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel7' "))
        CostOfGoodsScheme.Name = colCostOfGoodsSchemeAccount
        CostOfGoodsScheme.Width = 130
        CostOfGoodsScheme.ReadOnly = False
        CostOfGoodsScheme.HeaderImage = Global.ERP.My.Resources.Resources.search4
        CostOfGoodsScheme.TextImageRelation = TextImageRelation.TextBeforeImage
        Gv1.MasterTemplate.Columns.Add(CostOfGoodsScheme)

        Dim CostOfGoodsSchemeDesc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostOfGoodsSchemeDesc.FormatString = ""
        CostOfGoodsSchemeDesc.HeaderText = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Of Goods Scheme Account')+ ' Desc'  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel7' "))
        CostOfGoodsSchemeDesc.Name = colCostOfGoodsSchemeAccountDesc
        CostOfGoodsSchemeDesc.Width = 130
        CostOfGoodsSchemeDesc.ReadOnly = True
        Gv1.MasterTemplate.Columns.Add(CostOfGoodsSchemeDesc)

        ' SaleReturnHeader , CostOfGoodSoldAcountHeader , ReturnableContainerAccountHeader , StockTransferAccountHeader  , CostofGoodsTransferAccountHeader



        Dim SaleReturnHeader As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        SaleReturnHeader.FormatString = ""
        SaleReturnHeader.HeaderText = "SaleReturnHeader"
        SaleReturnHeader.Name = colSaleReturnHeader1
        SaleReturnHeader.Width = 130
        SaleReturnHeader.ReadOnly = True
        SaleReturnHeader.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(SaleReturnHeader)

        Dim CostOfGoodSoldAcountHeader As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostOfGoodSoldAcountHeader.FormatString = ""
        CostOfGoodSoldAcountHeader.HeaderText = "CostOfGoodSoldAcountHeader"
        CostOfGoodSoldAcountHeader.Name = colCostOfGoodSoldAcountHeader1
        CostOfGoodSoldAcountHeader.Width = 130
        CostOfGoodSoldAcountHeader.ReadOnly = True
        CostOfGoodSoldAcountHeader.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(CostOfGoodSoldAcountHeader)

        Dim ReturnableContainerAccountHeader As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ReturnableContainerAccountHeader.FormatString = ""
        ReturnableContainerAccountHeader.HeaderText = "ReturnableContainerAccountHeader"
        ReturnableContainerAccountHeader.Name = colReturnableContainerAccountHeader1
        ReturnableContainerAccountHeader.Width = 130
        ReturnableContainerAccountHeader.ReadOnly = True
        ReturnableContainerAccountHeader.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(ReturnableContainerAccountHeader)

        Dim StockTransferAccountHeader As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        StockTransferAccountHeader.FormatString = ""
        StockTransferAccountHeader.HeaderText = "StockTransferAccountHeader"
        StockTransferAccountHeader.Name = colStockTransferAccountHeader1
        StockTransferAccountHeader.Width = 130
        StockTransferAccountHeader.ReadOnly = True
        StockTransferAccountHeader.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(StockTransferAccountHeader)

        Dim CostofGoodsTransferAccountHeader As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        CostofGoodsTransferAccountHeader.FormatString = ""
        CostofGoodsTransferAccountHeader.HeaderText = "CostofGoodsTransferAccountHeader"
        CostofGoodsTransferAccountHeader.Name = colCostofGoodsTransferAccountHeader1
        CostofGoodsTransferAccountHeader.Width = 130
        CostofGoodsTransferAccountHeader.ReadOnly = True
        CostofGoodsTransferAccountHeader.IsVisible = False
        Gv1.MasterTemplate.Columns.Add(CostofGoodsTransferAccountHeader)

        Gv1.AllowAddNewRow = False
        Gv1.ShowGroupPanel = False
        Gv1.AllowColumnReorder = False
        Gv1.AllowRowReorder = False
        Gv1.EnableSorting = False

        Gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        Gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Public Sub LoadData(ByVal isPrint As Boolean)
        Try
            Dim qry As String = ""
            Dim dt As New DataTable
            '===========Update by preeti gupta Against ticket no[ERO/02/07/19-000668]
            ' Item Code  , Item Desc , Structure Code , Structure Desc , Item Type , Sale Account Set Code , Sales Account Set Desc , Sales Account  , Sales Account Desc , Sales Return Account , Sales Return Account Desc
            ' Cost Of Goods Sold Account , Cost Of Goods Sold Account Desc , Cost Variance Account ,Cost Variance Account Desc
            ' Damaged Goods Account , Damaged Goods Account Desc
            ' Internal Usage Account , Internal Usage Account Desc
            ' Returnable Container Account , Returnable Container Account Desc
            ' Schemes Account , Schemes Account Desc
            ' Promotional Account , Promotional Account Desc
            ' Cogs InterBranch Account , Cogs InterBranch Account Desc
            ' Suspence Account , Suspence Account Desc
            ' Gain Loss Account,  Gain Loss Account Desc
            ' Stock Transfer Account , Stock Transfer Account Desc
            ' Cost of Goods Transfer Account , Cost of Goods Transfer Account Desc
            ' Display Purpose Account , Display Purpose Account Desc
            Dim strSaleReturnHeader As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Sale Return Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblreturns' "))

            Dim strCostOfGoodSoldAcount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost Of Goods Sold Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME ='ITEM-SAL-ACC' and LABEL_ID ='rdlblcostofgoodssold' "))
            Dim strReturnableContainerAccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Returnable Container Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='lblreturnable' "))
            Dim strStockTransferAccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("  select  COALESCE( MAX(NEW_LABEL_NAME), 'Stock Transfer Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel4' "))
            Dim strCostofGoodsTransferAccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost of Goods Transfer Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel5' "))
            'Dim strCostofGoodsSchemeAccount As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(" select  COALESCE( MAX(NEW_LABEL_NAME), 'Cost of Goods Scheme Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel6' "))

            qry = " Select TSPL_ITEM_MASTER.Item_Code as [Item Code] ,TSPL_ITEM_MASTER.Item_Desc as [Item Desc] , TSPL_ITEM_MASTER.Structure_Code as [Structure Code], TSPL_STRUCTURE_MASTER.Structure_Descq as [Structure Desc] ,TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME as [Item Type],TSPL_ITEM_MASTER.Sale_Class_Code as [Sale Account Set Code] , TSPL_SALES_ACCOUNTS.Sales_Class_Desc as [Sales Account Set Desc], " & _
                  " TSPL_SALES_ACCOUNTS.Sales_Account  as [Sales Account], tspl_gl_accounts_Sales.Description as [Sales Account Desc] ,TSPL_SALES_ACCOUNTS.Sales_Return_Account as  [Sales Return Account] ,tspl_gl_accounts_Sales_Return.Description as [Sales Return Account Desc], " & _
                  " TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold as [Cost Of Goods Sold Account], tspl_gl_accounts_Sales_Cost_Of_Goods_Sold.Description as [Cost Of Goods Sold Account Desc], " & _
                  " TSPL_SALES_ACCOUNTS.Cost_Variance as [Cost Variance Account], tspl_gl_accounts_Sales_Cost_Variance.Description as [Cost Variance Account Desc],  " & _
                  " TSPL_SALES_ACCOUNTS.Damaged_Goods as [Damaged Goods Account]  ,tspl_gl_accounts_Sales_Damaged_Goods.Description as [Damaged Goods Account Desc], " & _
                  " TSPL_SALES_ACCOUNTS.Internal_Usage as [Internal Usage Account] ,tspl_gl_accounts_Sales_Internal_Usage.Description as [Internal Usage Account Desc], " & _
                  " TSPL_SALES_ACCOUNTS.Returnable_Container as [Returnable Container Account] , tspl_gl_accounts_Sales_Returnable_Container.Description as [Returnable Container Account Desc],  " & _
                  " TSPL_SALES_ACCOUNTS.Schemes as [Schemes Account],tspl_gl_accounts_Sales_Schemes.Description as [Schemes Account Desc], " & _
                  " TSPL_SALES_ACCOUNTS.Promotional as [Promotional Account] , tspl_gl_accounts_Sales_Promotional.Description as [Promotional Account Desc],  " & _
                  " TSPL_SALES_ACCOUNTS.Cogs_InterBranch as [Cogs InterBranch Account],tspl_gl_accounts_Sales_Cogs_InterBranch.Description as [Cogs InterBranch Account Desc],  " & _
                  " TSPL_SALES_ACCOUNTS.Suspence_Account as [Suspence Account],tspl_gl_accounts_Sales_Suspence_Account.Description as [Suspence Account Desc],  " & _
                  " TSPL_SALES_ACCOUNTS.Gain_Loss_Account as [Gain Loss Account], tspl_gl_accounts_Sales_Gain_Loss_Account.Description as [Gain Loss Account Desc],  " & _
                  " TSPL_SALES_ACCOUNTS.Stock_Transfer_AC as [Stock Transfer Account] ,tspl_gl_accounts_Sales_Stock_Transfer_AC.Description as [Stock Transfer Account Desc],   " & _
                  " TSPL_SALES_ACCOUNTS.COGT_AC as [Cost of Goods Transfer Account],tspl_gl_accounts_Sales_COGT_AC.Description as [Cost of Goods Transfer Account Desc],  " & _
                  " TSPL_SALES_ACCOUNTS.DisplayPurpose_Account as [Display Purpose Account], tspl_gl_accounts_Sales_DisplayPurpose_Account.Description as [Display Purpose Account Desc],TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Scheme as [Cost of Goods Scheme Account],tspl_gl_accounts_Sale_Cost_OF_Good_Scheme_Account.Description as [Cost of Goods Scheme Account Desc]  " & _
                  " ,'" + strSaleReturnHeader + "'  as SaleReturnHeader, '" + strCostOfGoodSoldAcount + "' as CostOfGoodSoldAcount, '" + strReturnableContainerAccount + "' as ReturnableContainerAccount, '" + strStockTransferAccount + "' as StockTransferAccount ,'" + strCostofGoodsTransferAccount + "' as CostofGoodsTransferAccount " & _
                  " from TSPL_ITEM_MASTER  " & _
                  " left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code = TSPL_ITEM_MASTER.Sale_Class_Code " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales on tspl_gl_accounts_Sales.Account_Code =TSPL_SALES_ACCOUNTS.Sales_Account  " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Return on tspl_gl_accounts_Sales_Return.Account_Code =TSPL_SALES_ACCOUNTS.Sales_Return_Account " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Cost_Of_Goods_Sold on tspl_gl_accounts_Sales_Cost_Of_Goods_Sold.Account_Code =TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Cost_Variance on tspl_gl_accounts_Sales_Cost_Variance.Account_Code =TSPL_SALES_ACCOUNTS.Cost_Variance " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Damaged_Goods on tspl_gl_accounts_Sales_Damaged_Goods.Account_Code =TSPL_SALES_ACCOUNTS.Damaged_Goods " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Internal_Usage on tspl_gl_accounts_Sales_Internal_Usage.Account_Code =TSPL_SALES_ACCOUNTS.Internal_Usage  " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Returnable_Container on tspl_gl_accounts_Sales_Returnable_Container.Account_Code =TSPL_SALES_ACCOUNTS.Returnable_Container " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Schemes on tspl_gl_accounts_Sales_Schemes.Account_Code =TSPL_SALES_ACCOUNTS.Schemes " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Promotional on tspl_gl_accounts_Sales_Promotional.Account_Code =TSPL_SALES_ACCOUNTS.Promotional " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Cogs_InterBranch on tspl_gl_accounts_Sales_Cogs_InterBranch.Account_Code =TSPL_SALES_ACCOUNTS.Cogs_InterBranch " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Suspence_Account on tspl_gl_accounts_Sales_Suspence_Account.Account_Code =TSPL_SALES_ACCOUNTS.Suspence_Account  " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Gain_Loss_Account on tspl_gl_accounts_Sales_Gain_Loss_Account.Account_Code =TSPL_SALES_ACCOUNTS.Gain_Loss_Account " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Stock_Transfer_AC on tspl_gl_accounts_Sales_Stock_Transfer_AC.Account_Code =TSPL_SALES_ACCOUNTS.Stock_Transfer_AC " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_COGT_AC on tspl_gl_accounts_Sales_COGT_AC.Account_Code =TSPL_SALES_ACCOUNTS.COGT_AC  " & _
                  " left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_DisplayPurpose_Account on tspl_gl_accounts_Sales_DisplayPurpose_Account.Account_Code =TSPL_SALES_ACCOUNTS.DisplayPurpose_Account " & _
                   " left outer join tspl_gl_accounts as tspl_gl_accounts_Sale_Cost_OF_Good_Scheme_Account on tspl_gl_accounts_Sale_Cost_OF_Good_Scheme_Account.Account_Code =TSPL_SALES_ACCOUNTS.cost_of_goods_scheme " & _
                  " left outer join TSPL_ITEM_TYPE_MASTER  On TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type " & _
                  " left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code = TSPL_ITEM_MASTER.Structure_Code " & _
                  " where 2= 2 "
            If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            End If
            If txtItemStructureCode.arrValueMember IsNot Nothing AndAlso txtItemStructureCode.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER.Structure_Code in (" + clsCommon.GetMulcallString(txtItemStructureCode.arrValueMember) + ")"
            End If
            If txtSaleAccountSet.arrValueMember IsNot Nothing AndAlso txtSaleAccountSet.arrValueMember.Count > 0 Then
                qry += " and TSPL_ITEM_MASTER.Sale_Class_Code in (" + clsCommon.GetMulcallString(txtSaleAccountSet.arrValueMember) + ")"
            End If
            If clsCommon.CompairString(cboItemType.Text, "Select") <> CompairStringResult.Equal Then
                qry += " and TSPL_ITEM_MASTER.Item_Type in ('" + cboItemType.SelectedValue + "')"
            End If
            qry = qry + " Order by TSPL_ITEM_MASTER.Item_Code "
            '======================= Start =================================
            'qry = " DECLARE @SaleAccount VARCHAR(100) = '' select @SaleAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Sale Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblsale'  DECLARE @SaleReturnAccount VARCHAR(100) = ''  select @SaleReturnAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Sale Return Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblreturns'  DECLARE @CostOfgoodSold VARCHAR(100) = ''  select @CostOfgoodSold = COALESCE( MAX(NEW_LABEL_NAME), 'Cost Of Goods Sold Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME ='ITEM-SAL-ACC' and LABEL_ID ='rdlblcostofgoodssold'   DECLARE @CostVariance VARCHAR(100) = ''  select @CostVariance = COALESCE( MAX(NEW_LABEL_NAME), 'Cost Variance Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblcostvariance'  DECLARE @Damagedgoods VARCHAR(100) = ''  select @Damagedgoods = COALESCE( MAX(NEW_LABEL_NAME), 'Damaged Goods Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblDamagedgoods'  DECLARE @InternalUsageAccount VARCHAR(100) = ''  select @InternalUsageAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Internal Usage Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='rdlblInternalusage'  DECLARE @ReturnableContainerAccount VARCHAR(100) = ''  select @ReturnableContainerAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Returnable Container Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='lblreturnable'  DECLARE @SchemesAccount VARCHAR(100) = ''   select @SchemesAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Schemes Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel1'   DECLARE @PromotionalAccount VARCHAR(100) = ''   select @PromotionalAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Promotional Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='RadLabel2'   DECLARE @CogsInterBranchAccount VARCHAR(100) = ''  select @CogsInterBranchAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Cogs InterBranch Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel1' " & _
            '      " DECLARE @SuspenceAccount VARCHAR(100) = ''  select @SuspenceAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Suspence Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel2'  DECLARE @GainLossAccount VARCHAR(100) = ''  select @GainLossAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Gain Loss Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel3'  DECLARE @StockTransferAccount VARCHAR(100) = '' " & _
            '      " select @StockTransferAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Stock Transfer Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel4'  DECLARE @CostofGoodsTransferAccount VARCHAR(100) = ''  select @CostofGoodsTransferAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Cost of Goods Transfer Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel5'   DECLARE @DisplayPurposeAccount VARCHAR(100) = ''  select @DisplayPurposeAccount = COALESCE( MAX(NEW_LABEL_NAME), 'Display Purpose Account')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = 'ITEM-SAL-ACC' and LABEL_ID ='MyLabel6' "
            'qry = qry + "   DECLARE @SQLText NVARCHAR(MAX) =   'select XXX.[Item Code] ,XXX.[Item Desc] , XXX.[Structure Code] ,XXX.[Structure Desc] ,   XXX.ITEM_TYPE_NAME as [Item Type] ,XXX.[Sale Class Code] as [Sale Account Set Code], XXX.[Sales Class Desc] as [Sale Account Set Desc]   ,XXX.[Sales Account]   AS [' + @SaleAccount + '], XXX.[Sales Account Desc] AS  [' + @SaleAccount + ' Desc' + ']  , XXX.Sales_Return_Account AS [' + @SaleReturnAccount + '] ,XXX.[Sales Return Account Desc]   AS [' + @SaleReturnAccount +' Desc' + ']   , XXX.[Cost Of Goods Sold Account] AS  [' + @CostOfgoodSold + ']  , XXX.[Cost Of Goods Sold Account Desc] AS [' + @CostOfgoodSold + ' Desc' + ']  , XXX.[Cost Variance Account] AS [' +  @CostVariance + '] , XXX.[Cost Variance Account Desc] as [' +  @CostVariance + ' Desc' +']  , XXX.[Damaged_Goods Account] AS [' +  @Damagedgoods + '] , XXX.[Damaged_Goods Account Desc] AS [' +  @Damagedgoods + ' Desc ' + ']  , XXX.[Internal Usage Account]  AS [' +  @InternalUsageAccount + '] , XXX.[Internal Usage Account Desc] AS [' +  @InternalUsageAccount + ' Desc' + '] " & _
            '            " , XXX.[Returnable Container Account]   AS [' +  @ReturnableContainerAccount + '] , XXX.[Returnable Container Account Desc]  AS [' +  @ReturnableContainerAccount + ' Desc'+'] " & _
            '            " , XXX.[Schemes Account]  AS [' +  @SchemesAccount + '] , XXX.[Schemes Account Desc] AS [' +  @SchemesAccount + ' Desc' + ']  , XXX.[Promotional Account] AS  [' +  @PromotionalAccount + '] , XXX.[Promotional Account Desc] AS  [' +  @PromotionalAccount + ' Desc' +  ']  , XXX.[Cogs InterBranch Account]  AS  [' +  @CogsInterBranchAccount + '] , XXX.[Cogs InterBranch Account Desc] AS  [' +  @CogsInterBranchAccount + ' Desc'+ ']  , XXX.[Suspence Account]  AS  [' +  @SuspenceAccount + '] , XXX.[Suspence Account Desc] AS  [' +  @SuspenceAccount +' Desc' +']  , XXX.[Gain Loss Account]  AS  [' +  @GainLossAccount + '] , XXX.[Gain Loss Account Desc]  AS  [' +  @GainLossAccount + ' Desc'+ '] " & _
            '            " , XXX.[Stock Transfer Account]  AS  [' +  @StockTransferAccount + '] , XXX.[Stock Transfer Account Desc] AS  [' +  @StockTransferAccount +' Desc' +']  , XXX.[Cost of Goods Transfer Account]   AS  [' +  @CostofGoodsTransferAccount + '] , XXX.[Cost of Goods Transfer Account Desc] AS  [' +  @CostofGoodsTransferAccount +' Desc' +']  , XXX.[Display Purpose Account]   AS  [' +  @DisplayPurposeAccount + '] , XXX.[Display Purpose Account Desc] AS  [' +  @DisplayPurposeAccount +' Desc' +']   from (Select   TSPL_ITEM_MASTER.Item_Code as [Item Code] ,TSPL_ITEM_MASTER.Item_Desc as [Item Desc] , TSPL_ITEM_MASTER.Structure_Code as [Structure Code], TSPL_ITEM_MASTER.Structure_Desc as [Structure Desc] ,TSPL_ITEM_MASTER.Item_Type as [Item Type] , TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_NAME,TSPL_ITEM_MASTER.Sale_Class_Code as [Sale Class Code] , TSPL_SALES_ACCOUNTS.Sales_Class_Desc as [Sales Class Desc],  TSPL_SALES_ACCOUNTS.Sales_Account  as [Sales Account],   tspl_gl_accounts_Sales.Description as [Sales Account Desc] ,TSPL_SALES_ACCOUNTS.Sales_Return_Account ,tspl_gl_accounts_Sales_Return.Description as [Sales Return Account Desc],  TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold as [Cost Of Goods Sold Account],   tspl_gl_accounts_Sales_Cost_Of_Goods_Sold.Description as [Cost Of Goods Sold Account Desc],  TSPL_SALES_ACCOUNTS.Cost_Variance as [Cost Variance Account], tspl_gl_accounts_Sales_Cost_Variance.Description as [Cost Variance Account Desc],   TSPL_SALES_ACCOUNTS.Damaged_Goods as [Damaged_Goods Account]  ,tspl_gl_accounts_Sales_Damaged_Goods.Description as [Damaged_Goods Account Desc],  TSPL_SALES_ACCOUNTS.Internal_Usage as [Internal Usage Account] ,tspl_gl_accounts_Sales_Internal_Usage.Description as [Internal Usage Account Desc],  TSPL_SALES_ACCOUNTS.Returnable_Container as [Returnable Container Account] , tspl_gl_accounts_Sales_Returnable_Container.Description as [Returnable Container Account Desc],   TSPL_SALES_ACCOUNTS.Schemes as [Schemes Account], " & _
            '            " tspl_gl_accounts_Sales_Schemes.Description as [Schemes Account Desc],  TSPL_SALES_ACCOUNTS.Promotional as [Promotional Account] , tspl_gl_accounts_Sales_Promotional.Description as [Promotional Account Desc],     TSPL_SALES_ACCOUNTS.Cogs_InterBranch as [Cogs InterBranch Account],tspl_gl_accounts_Sales_Cogs_InterBranch.Description as [Cogs InterBranch Account Desc],   TSPL_SALES_ACCOUNTS.Suspence_Account as [Suspence Account],tspl_gl_accounts_Sales_Suspence_Account.Description as [Suspence Account Desc],	 " & _
            '            " TSPL_SALES_ACCOUNTS.Gain_Loss_Account as [Gain Loss Account], tspl_gl_accounts_Sales_Gain_Loss_Account.Description as [Gain Loss Account Desc],   TSPL_SALES_ACCOUNTS.Stock_Transfer_AC as [Stock Transfer Account] ,tspl_gl_accounts_Sales_Stock_Transfer_AC.Description as [Stock Transfer Account Desc], 	" & _
            '            " TSPL_SALES_ACCOUNTS.COGT_AC as [Cost of Goods Transfer Account],tspl_gl_accounts_Sales_COGT_AC.Description as [Cost of Goods Transfer Account Desc], 	" & _
            '            " TSPL_SALES_ACCOUNTS.DisplayPurpose_Account as [Display Purpose Account], tspl_gl_accounts_Sales_DisplayPurpose_Account.Description as [Display Purpose Account Desc]   from TSPL_ITEM_MASTER   left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code = TSPL_ITEM_MASTER.Sale_Class_Code  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales on tspl_gl_accounts_Sales.Account_Code =TSPL_SALES_ACCOUNTS.Sales_Account   left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Return on tspl_gl_accounts_Sales_Return.Account_Code =TSPL_SALES_ACCOUNTS.Sales_Return_Account  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Cost_Of_Goods_Sold on tspl_gl_accounts_Sales_Cost_Of_Goods_Sold.Account_Code =TSPL_SALES_ACCOUNTS.Cost_Of_Goods_Sold  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Cost_Variance on tspl_gl_accounts_Sales_Cost_Variance.Account_Code =TSPL_SALES_ACCOUNTS.Cost_Variance  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Damaged_Goods on tspl_gl_accounts_Sales_Damaged_Goods.Account_Code =TSPL_SALES_ACCOUNTS.Damaged_Goods  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Internal_Usage on tspl_gl_accounts_Sales_Internal_Usage.Account_Code =TSPL_SALES_ACCOUNTS.Internal_Usage   left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Returnable_Container on tspl_gl_accounts_Sales_Returnable_Container.Account_Code =TSPL_SALES_ACCOUNTS.Returnable_Container  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Schemes on tspl_gl_accounts_Sales_Schemes.Account_Code =TSPL_SALES_ACCOUNTS.Schemes  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Promotional on tspl_gl_accounts_Sales_Promotional.Account_Code =TSPL_SALES_ACCOUNTS.Promotional  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Cogs_InterBranch on tspl_gl_accounts_Sales_Cogs_InterBranch.Account_Code =TSPL_SALES_ACCOUNTS.Cogs_InterBranch  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Suspence_Account on tspl_gl_accounts_Sales_Suspence_Account.Account_Code =TSPL_SALES_ACCOUNTS.Suspence_Account   left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Gain_Loss_Account on tspl_gl_accounts_Sales_Gain_Loss_Account.Account_Code =TSPL_SALES_ACCOUNTS.Gain_Loss_Account  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_Stock_Transfer_AC on tspl_gl_accounts_Sales_Stock_Transfer_AC.Account_Code =TSPL_SALES_ACCOUNTS.Stock_Transfer_AC  left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_COGT_AC on tspl_gl_accounts_Sales_COGT_AC.Account_Code =TSPL_SALES_ACCOUNTS.COGT_AC   left outer join tspl_gl_accounts as tspl_gl_accounts_Sales_DisplayPurpose_Account on tspl_gl_accounts_Sales_DisplayPurpose_Account.Account_Code =TSPL_SALES_ACCOUNTS.DisplayPurpose_Account " & _
            '            " left outer join TSPL_ITEM_TYPE_MASTER  On TSPL_ITEM_TYPE_MASTER.ITEM_TYPE_CODE = TSPL_ITEM_MASTER.Item_Type " & _
            '            " ) XXX where 2=2  "

            'If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
            '    qry += " and XXX.[Item Code] in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
            '    End If
            'If txtItemStructureCode.arrValueMember IsNot Nothing AndAlso txtItemStructureCode.arrValueMember.Count > 0 Then
            '    qry += " and XXX.[Structure Code] in (" + clsCommon.GetMulcallString(txtItemStructureCode.arrValueMember) + ")"
            '    End If
            'If txtSaleAccountSet.arrValueMember IsNot Nothing AndAlso txtSaleAccountSet.arrValueMember.Count > 0 Then
            '    qry += " and XXX.[Sale Class Code] in (" + clsCommon.GetMulcallString(txtSaleAccountSet.arrValueMember) + ")"
            '    End If
            'If clsCommon.CompairString(cboItemType.Text, "Select") <> CompairStringResult.Equal Then
            '    qry += " and XXX.[Item Type] in (''" + cboItemType.SelectedValue + "'')"
            'End If
            'qry = qry + " ' Select  @SQLText"
            'qry = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            '======================= End=================================

            dt = clsDBFuncationality.GetDataTable(qry)

            If dt Is Nothing OrElse dt.Rows.Count > 0 Then
                'Gv1.DataSource = dt
                'For ii As Integer = 0 To Gv1.Columns.Count - 1
                '    Gv1.Columns(ii).ReadOnly = True
                'Next
                '====================================

                ' Item Code  , Item Desc , Structure Code , Structure Desc , Item Type , Sale Account Set Code , Sales Account Set Desc , Sales Account  , Sales Account Desc ,
                ' Sales Return Account , Sales Return Account Desc
                ' Cost Of Goods Sold Account , Cost Of Goods Sold Account Desc ,
                '  Cost Variance Account ,Cost Variance Account Desc
                ' Damaged Goods Account , Damaged Goods Account Desc
                ' Internal Usage Account , Internal Usage Account Desc
                ' Returnable Container Account , Returnable Container Account Desc
                ' Schemes Account , Schemes Account Desc
                ' Promotional Account , Promotional Account Desc
                ' Cogs InterBranch Account , Cogs InterBranch Account Desc
                ' Suspence Account , Suspence Account Desc
                ' Gain Loss Account,  Gain Loss Account Desc
                ' Stock Transfer Account , Stock Transfer Account Desc
                ' Cost of Goods Transfer Account , Cost of Goods Transfer Account Desc
                ' Display Purpose Account , Display Purpose Account Desc
                If isPrint = True Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funsubreportWithdt(CrystalReportFolder.KwalitySalesReport, dt, clsERPFuncationality.CompanyAddresShowinFooter(), "rptSaleAccountSetList", "Sale Account Set List", "rptCompanyAddress.rpt")
                    frmCRV = Nothing
                Else
                    Gv1.DataSource = Nothing
                    Gv1.Rows.Clear()
                    Gv1.Columns.Clear()
                    Gv1.GroupDescriptors.Clear()
                    Gv1.MasterTemplate.SummaryRowsBottom.Clear()
                    Gv1.MasterView.Refresh()
                    LoadBlankGrid()
                    For Each dr As DataRow In dt.Rows
                        Gv1.Rows.AddNew()
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(dr("Item Code"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemDesc).Value = clsCommon.myCstr(dr("Item Desc"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colStructureCode).Value = clsCommon.myCstr(dr("Structure Code"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colStructureDesc).Value = clsCommon.myCstr(dr("Structure Desc"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colItemType).Value = clsCommon.myCstr(dr("Item Type"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSaleAccountSetCode).Value = clsCommon.myCstr(dr("Sale Account Set Code"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSalesAccountSetDesc).Value = clsCommon.myCstr(dr("Sales Account Set Desc"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSalesAccount).Value = clsCommon.myCstr(dr("Sales Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSalesAccountDesc).Value = clsCommon.myCstr(dr("Sales Account Desc"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSalesReturnAccount).Value = clsCommon.myCstr(dr("Sales Return Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSalesReturnAccountDesc).Value = clsCommon.myCstr(dr("Sales Return Account Desc"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCostOfGoodsSoldAccount).Value = clsCommon.myCstr(dr("Cost Of Goods Sold Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCostOfGoodsSoldAccountDesc).Value = clsCommon.myCstr(dr("Cost Of Goods Sold Account Desc"))
                        '  Cost Variance Account ,Cost Variance Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCostVarianceAccount).Value = clsCommon.myCstr(dr("Cost Variance Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCostVarianceAccountDesc).Value = clsCommon.myCstr(dr("Cost Variance Account Desc"))
                        ' Damaged Goods Account , Damaged Goods Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamagedGoodsAccount).Value = clsCommon.myCstr(dr("Damaged Goods Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDamagedGoodsAccountDesc).Value = clsCommon.myCstr(dr("Damaged Goods Account Desc"))
                        ' Internal Usage Account , Internal Usage Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colInternalUsageAccount).Value = clsCommon.myCstr(dr("Internal Usage Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colInternalUsageAccountDesc).Value = clsCommon.myCstr(dr("Internal Usage Account Desc"))
                        ' Returnable Container Account , Returnable Container Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colReturnableContainerAccount).Value = clsCommon.myCstr(dr("Returnable Container Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colReturnableContainerAccountDesc).Value = clsCommon.myCstr(dr("Returnable Container Account Desc"))
                        ' Schemes Account , Schemes Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSchemesAccount).Value = clsCommon.myCstr(dr("Schemes Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSchemesAccountDesc).Value = clsCommon.myCstr(dr("Schemes Account Desc"))
                        ' Promotional Account , Promotional Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPromotionalAccount).Value = clsCommon.myCstr(dr("Promotional Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colPromotionalAccountDesc).Value = clsCommon.myCstr(dr("Promotional Account Desc"))
                        ' Cogs InterBranch Account , Cogs InterBranch Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCogsInterBranchAccount).Value = clsCommon.myCstr(dr("Cogs InterBranch Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCogsInterBranchAccountDesc).Value = clsCommon.myCstr(dr("Cogs InterBranch Account Desc"))
                        ' Suspence Account , Suspence Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSuspenceAccount).Value = clsCommon.myCstr(dr("Suspence Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colSuspenceAccountDesc).Value = clsCommon.myCstr(dr("Suspence Account Desc"))
                        ' Gain Loss Account,  Gain Loss Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colGainLossAccount).Value = clsCommon.myCstr(dr("Gain Loss Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colGainLossAccountDesc).Value = clsCommon.myCstr(dr("Gain Loss Account Desc"))
                        ' Stock Transfer Account , Stock Transfer Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colStockTransferAccount).Value = clsCommon.myCstr(dr("Stock Transfer Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colStockTransferAccountDesc5).Value = clsCommon.myCstr(dr("Stock Transfer Account Desc"))
                        ' Cost of Goods Transfer Account , Cost of Goods Transfer Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCostofGoodsTransferAccount).Value = clsCommon.myCstr(dr("Cost of Goods Transfer Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCostofGoodsTransferAccountDesc).Value = clsCommon.myCstr(dr("Cost of Goods Transfer Account Desc"))
                        ' Display Purpose Account , Display Purpose Account Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDisplayPurposeAccount).Value = clsCommon.myCstr(dr("Display Purpose Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colDisplayPurposeAccountDesc).Value = clsCommon.myCstr(dr("Display Purpose Account Desc"))

                        ' Cost of good scheme Account ,  Cost of Goods scheme Desc
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCostOfGoodsSchemeAccount).Value = clsCommon.myCstr(dr("Cost of Goods Scheme Account"))
                        Gv1.Rows(Gv1.Rows.Count - 1).Cells(colCostOfGoodsSchemeAccountDesc).Value = clsCommon.myCstr(dr("Cost of Goods Scheme Account Desc"))
                    Next
                    '===================================

                    RadPageView1.SelectedPage = RadPageViewPage2
                    ' SetGridFormatOFGV()
                    Gv1.BestFitColumns()
                    '  Gv1.EnableFiltering = True
                    btnPrint.Enabled = True
                    If chkOnlyview.Checked = True Then
                        btnUpdate.Enabled = False
                    Else
                        btnUpdate.Enabled = True
                    End If

                End If
            Else
                clsCommon.MyMessageBoxShow("No Data Found to Display", Me.Text)
                btnPrint.Enabled = False
                btnUpdate.Enabled = False
                Exit Sub
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub
    Sub LoadItemType()
        Dim dt As New DataTable()
        Dim Whr = " AND IS_NON_INVENTORY=0 "
        dt = clsItemMaster.getItemTypeQuery(Whr)
        cboItemType.DataSource = dt
        cboItemType.ValueMember = "Code"
        cboItemType.DisplayMember = "Name"
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        ' select Sales_Class_Code, Sales_Class_Desc , Sales_Account , Sales_Return_Account , Cost_Of_Goods_Sold , Cost_Variance, Damaged_Goods, Internal_Usage , 
        'Returnable_Container , Schemes,Promotional , Cogs_InterBranch , Suspence_Account , Gain_Loss_Account , Stock_Transfer_AC, COGT_AC, DisplayPurpose_Account from TSPL_SALES_ACCOUNTS
        For Each grow As GridViewRowInfo In Gv1.Rows
            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colSalesAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "rdlblsale", "Sales Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSalesAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colSalesAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "rdlblsale", "Sales Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "", Me.Text)
                    'Throw New Exception("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "rdlblsale", "Sales Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If


            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colSalesReturnAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "rdlblreturns", "Sale Return Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSalesReturnAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colSalesReturnAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "rdlblreturns", "Sale Return Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colCostOfGoodsSoldAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "rdlblcostofgoodssold", "Cost Of Goods Sold Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCostOfGoodsSoldAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colCostOfGoodsSoldAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "rdlblcostofgoodssold", "Cost Of Goods Sold Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If


            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colCostVarianceAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "rdlblcostvariance", "Cost Variance Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCostVarianceAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colCostVarianceAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "rdlblcostvariance", "Cost Variance Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colDamagedGoodsAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "rdlblDamagedgoods", "Damaged Goods Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colDamagedGoodsAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colDamagedGoodsAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "rdlblDamagedgoods", "Damaged Goods Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If




            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colInternalUsageAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "rdlblInternalusage", "Internal Usage Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colInternalUsageAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colInternalUsageAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "rdlblInternalusage", "Internal Usage Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colReturnableContainerAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "lblreturnable", "Returnable Container Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colReturnableContainerAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colReturnableContainerAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "lblreturnable", "Returnable Container Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colCogsInterBranchAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "MyLabel1", "Cogs InterBranch Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCogsInterBranchAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colCogsInterBranchAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "MyLabel1", "Cogs InterBranch Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If


            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colDisplayPurposeAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "MyLabel6", "Display Purpose Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colDisplayPurposeAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colDisplayPurposeAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "MyLabel6", "Display Purpose Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If


            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colCostOfGoodsSoldAccount).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill  (  " + GetcolumnName("ITEM-SAL-ACC", "MyLabel7", "Cost Of Goods Scheme Account") + "  ) of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCostOfGoodsSoldAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colCostOfGoodsSoldAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "MyLabel7", "Cost Of Goods Scheme Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            'clsCommon.myCstr(grow.Cells(colSchemesAccount).Value)
            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSchemesAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colSchemesAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "RadLabel1", "Schemes Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colPromotionalAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colPromotionalAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "RadLabel2", "Promotional Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colSuspenceAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colSuspenceAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "MyLabel2", "Suspence Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colStockTransferAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colStockTransferAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "MyLabel4", "Stock Transfer Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            If clsCommon.myLen(clsCommon.myCstr(grow.Cells(colCostofGoodsTransferAccount).Value)) > 0 Then
                Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + clsCommon.myCstr(grow.Cells(colCostofGoodsTransferAccount).Value) + "' AND ControlAccount ='Y'"
                Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                If check1 <= 0 Then
                    clsCommon.MyMessageBoxShow("Filled  ( " + GetcolumnName("ITEM-SAL-ACC", "MyLabel5", "Cost of Goods Transfer Account") + ") must be control account." + Environment.NewLine + "For Item code " + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "")
                    Return
                End If
            End If

            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colStructureCode).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill Structure Code of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If

            If String.IsNullOrEmpty(clsCommon.myCstr(grow.Cells(colSaleAccountSetCode).Value)) Then
                clsCommon.MyMessageBoxShow("Please fill Sale Account Set code  of Item Code ( " & clsCommon.myCstr(grow.Cells(colItemCode).Value) & ") ")
                Return
            End If




        Next
        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try
            ' Dim Arr As New List(Of clsUpdateSaleAccountSet)
            For Each grow As GridViewRowInfo In Gv1.Rows
                Dim obj As New clsUpdateSaleAccountSet()
                obj.Sales_Class_Code = clsCommon.myCstr(grow.Cells(colSaleAccountSetCode).Value)
                obj.Sales_Account = clsCommon.myCstr(grow.Cells(colSalesAccount).Value)
                obj.Sales_Return_Account = clsCommon.myCstr(grow.Cells(colSalesReturnAccount).Value)

                obj.Cost_Of_Goods_Sold = clsCommon.myCstr(grow.Cells(colCostOfGoodsSoldAccount).Value)
                obj.Cost_Variance = clsCommon.myCstr(grow.Cells(colCostVarianceAccount).Value)
                obj.Damaged_Goods = clsCommon.myCstr(grow.Cells(colDamagedGoodsAccount).Value)
                obj.Internal_Usage = clsCommon.myCstr(grow.Cells(colInternalUsageAccount).Value)

                obj.Returnable_Container = clsCommon.myCstr(grow.Cells(colReturnableContainerAccount).Value)
                obj.Schemes = clsCommon.myCstr(grow.Cells(colSchemesAccount).Value)
                obj.Promotional = clsCommon.myCstr(grow.Cells(colPromotionalAccount).Value)
                obj.Cogs_InterBranch = clsCommon.myCstr(grow.Cells(colCogsInterBranchAccount).Value)

                obj.Suspence_Account = clsCommon.myCstr(grow.Cells(colSuspenceAccount).Value)
                obj.Gain_Loss_Account = clsCommon.myCstr(grow.Cells(colGainLossAccount).Value)
                obj.Stock_Transfer_AC = clsCommon.myCstr(grow.Cells(colStockTransferAccount).Value)
                obj.COGT_AC = clsCommon.myCstr(grow.Cells(colCostofGoodsTransferAccount).Value)
                obj.DisplayPurpose_Account = clsCommon.myCstr(grow.Cells(colDisplayPurposeAccount).Value)
                obj.CostOfGoodsSchemeAccount = clsCommon.myCstr(grow.Cells(colCostOfGoodsSchemeAccount).Value)
                obj.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                obj.Structure_Code = clsCommon.myCstr(grow.Cells(colStructureCode).Value)

                ' Arr.Add(obj)

                If (clsUpdateSaleAccountSet.SaveData(obj, trans)) Then

                End If

            Next
            clsCommon.MyMessageBoxShow("Data saved Successfully", Me.Text)
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
            clsCommon.MyMessageBoxShow(ex.ToString(), Me.Text)
        End Try

    End Sub



    Private Sub Gv1_CellValueChanged(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellValueChanged
        If isLoadData = False Then
            If e.Column Is Gv1.Columns(colSalesAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenSaleAccount(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colSalesReturnAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenReturnSaleAccount(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colStructureCode) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenItemStrctureCode(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colCostOfGoodsSoldAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenCostOfGoodSold(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colCostVarianceAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenCostVariance(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colDamagedGoodsAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenDamageGood(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colInternalUsageAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenInternalUsage(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colReturnableContainerAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenReturnableContainer(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colSchemesAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenSchemes(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colPromotionalAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenPromotional(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colCogsInterBranchAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenCogsInterBranch(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colSuspenceAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenSuspenceAccount(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colGainLossAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenGainLossAccount(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colStockTransferAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenStockTransferAC(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colCostofGoodsTransferAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenCOGTAC(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colDisplayPurposeAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenDisplayPurposeAccount(False)
                isSelAccoutSetOpen = False

            ElseIf e.Column Is Gv1.Columns(colCostOfGoodsSchemeAccount) AndAlso isSelAccoutSetOpen = False Then
                isSelAccoutSetOpen = True
                OpenCostOfGoodsSchemAccount(False)
                isSelAccoutSetOpen = False
            ElseIf e.Column Is Gv1.Columns(colSaleAccountSetCode) Then
                isSelAccoutSetOpen = True
                OpenSaleAccountSetCode(False)
                isSelAccoutSetOpen = False
            End If
        End If
    End Sub
    Sub OpenSaleAccount(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts   "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colSalesAccount).Value = clsCommon.ShowSelectForm("SaleAccount@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colSalesAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colSalesAccount).Value + "'"
        Gv1.CurrentRow.Cells(colSalesAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colSalesAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colSalesAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colSalesAccount).Value = strChangeAccountcode
                dgrv.Cells(colSalesAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================


    End Sub

    Sub OpenReturnSaleAccount(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colSalesReturnAccount).Value = clsCommon.ShowSelectForm("SaleReturnAccount@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colSalesReturnAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colSalesReturnAccount).Value + "'"
        Gv1.CurrentRow.Cells(colSalesReturnAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colSalesReturnAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colSalesReturnAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colSalesReturnAccount).Value = strChangeAccountcode
                dgrv.Cells(colSalesReturnAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================

    End Sub

    Sub OpenItemStrctureCode(ByVal isButtonClick As Boolean)
        Dim qry As String = " select Structure_Code as [Code],Structure_Descq as [Description]  from TSPL_STRUCTURE_MASTER "
        Dim whrCls As String = " "
        Gv1.CurrentRow.Cells(colStructureCode).Value = clsCommon.ShowSelectForm("Structurecode@Finder", qry, "Code", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colStructureCode).Value), "Code", isButtonClick)

        qry = "select Structure_Descq from TSPL_STRUCTURE_MASTER where Structure_Code ='" + Gv1.CurrentRow.Cells(colStructureCode).Value + "'"
        Gv1.CurrentRow.Cells(colStructureDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

    End Sub

    Sub OpenCostOfGoodSold(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colCostOfGoodsSoldAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colCostOfGoodsSoldAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colCostOfGoodsSoldAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCostOfGoodsSoldAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colCostOfGoodsSoldAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colCostOfGoodsSoldAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colCostOfGoodsSoldAccount).Value = strChangeAccountcode
                dgrv.Cells(colCostOfGoodsSoldAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================
    End Sub

    Sub OpenCostVariance(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colCostVarianceAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colCostVarianceAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colCostVarianceAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCostVarianceAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colCostVarianceAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colCostVarianceAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colCostVarianceAccount).Value = strChangeAccountcode
                dgrv.Cells(colCostVarianceAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================

    End Sub

    Sub OpenDamageGood(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colDamagedGoodsAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colDamagedGoodsAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colDamagedGoodsAccount).Value + "'"
        Gv1.CurrentRow.Cells(colDamagedGoodsAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colDamagedGoodsAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colDamagedGoodsAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colDamagedGoodsAccount).Value = strChangeAccountcode
                dgrv.Cells(colDamagedGoodsAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================
    End Sub

    Sub OpenInternalUsage(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colInternalUsageAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colInternalUsageAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colInternalUsageAccount).Value + "'"
        Gv1.CurrentRow.Cells(colInternalUsageAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colInternalUsageAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colInternalUsageAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colInternalUsageAccount).Value = strChangeAccountcode
                dgrv.Cells(colInternalUsageAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================
    End Sub

    Sub OpenReturnableContainer(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colReturnableContainerAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colReturnableContainerAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colReturnableContainerAccount).Value + "'"
        Gv1.CurrentRow.Cells(colReturnableContainerAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colReturnableContainerAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colReturnableContainerAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colReturnableContainerAccount).Value = strChangeAccountcode
                dgrv.Cells(colReturnableContainerAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================

    End Sub

    Sub OpenSchemes(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colSchemesAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colSchemesAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colSchemesAccount).Value + "'"
        Gv1.CurrentRow.Cells(colSchemesAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colSchemesAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colSchemesAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colSchemesAccount).Value = strChangeAccountcode
                dgrv.Cells(colSchemesAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================

    End Sub

    Sub OpenPromotional(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colPromotionalAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colPromotionalAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colPromotionalAccount).Value + "'"
        Gv1.CurrentRow.Cells(colPromotionalAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colPromotionalAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colPromotionalAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colPromotionalAccount).Value = strChangeAccountcode
                dgrv.Cells(colPromotionalAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================

    End Sub

    Sub OpenCogsInterBranch(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colCogsInterBranchAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colCogsInterBranchAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colCogsInterBranchAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCogsInterBranchAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colCogsInterBranchAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colCogsInterBranchAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colCogsInterBranchAccount).Value = strChangeAccountcode
                dgrv.Cells(colCogsInterBranchAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================

    End Sub
    Sub OpenSuspenceAccount(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colSuspenceAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colSuspenceAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colSuspenceAccount).Value + "'"
        Gv1.CurrentRow.Cells(colSuspenceAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colSuspenceAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colSuspenceAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colSuspenceAccount).Value = strChangeAccountcode
                dgrv.Cells(colSuspenceAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================
    End Sub

    Sub OpenGainLossAccount(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colGainLossAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colGainLossAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colGainLossAccount).Value + "'"
        Gv1.CurrentRow.Cells(colGainLossAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colGainLossAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colGainLossAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colGainLossAccount).Value = strChangeAccountcode
                dgrv.Cells(colGainLossAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================

    End Sub

    Sub OpenStockTransferAC(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colStockTransferAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colStockTransferAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colStockTransferAccount).Value + "'"
        Gv1.CurrentRow.Cells(colStockTransferAccountDesc5).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colStockTransferAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colStockTransferAccountDesc5).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colStockTransferAccount).Value = strChangeAccountcode
                dgrv.Cells(colStockTransferAccountDesc5).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================

    End Sub

    Sub OpenCOGTAC(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colCostofGoodsTransferAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colCostofGoodsTransferAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colStockTransferAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCostofGoodsTransferAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colCostofGoodsTransferAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colCostofGoodsTransferAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colCostofGoodsTransferAccount).Value = strChangeAccountcode
                dgrv.Cells(colCostofGoodsTransferAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================

    End Sub

    Sub OpenDisplayPurposeAccount(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colDisplayPurposeAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSold@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colDisplayPurposeAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colDisplayPurposeAccount).Value + "'"
        Gv1.CurrentRow.Cells(colDisplayPurposeAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colDisplayPurposeAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colDisplayPurposeAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colDisplayPurposeAccount).Value = strChangeAccountcode
                dgrv.Cells(colDisplayPurposeAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================
    End Sub
    Sub OpenCostOfGoodsSchemAccount(ByVal isButtonClick As Boolean)
        Dim qry As String = "select Account_Code as [Account],Description from tspl_gl_accounts  "
        Dim whrCls As String = " ControlAccount ='Y' "
        Gv1.CurrentRow.Cells(colCostOfGoodsSchemeAccount).Value = clsCommon.ShowSelectForm("CostOfGoodSchem@Finder", qry, "Account", whrCls, clsCommon.myCstr(Gv1.CurrentRow.Cells(colCostOfGoodsSchemeAccount).Value), "Account", isButtonClick)

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colCostOfGoodsSchemeAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCostOfGoodsSchemeAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
        '=============================================================================================================
        Dim strChangeAccountcode As String = Gv1.CurrentRow.Cells(colCostOfGoodsSchemeAccount).Value
        Dim strChangeAccountDesc As String = Gv1.CurrentRow.Cells(colCostOfGoodsSchemeAccountDesc).Value
        Dim strSaleAccountSetCode As String = Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value
        For Each dgrv As GridViewRowInfo In Gv1.Rows
            If clsCommon.CompairString(dgrv.Cells(colSaleAccountSetCode).Value, strSaleAccountSetCode) = CompairStringResult.Equal Then
                dgrv.Cells(colCostOfGoodsSchemeAccount).Value = strChangeAccountcode
                dgrv.Cells(colCostOfGoodsSchemeAccountDesc).Value = strChangeAccountDesc
            End If
        Next
        'Dim strcurrentRow As Integer = Gv1.CurrentCell.RowIndex
        '================================================================================================================
    End Sub

    Sub OpenSaleAccountSetCode(ByVal isButtonClick As Boolean)
        Dim qry As String = " select Sales_Class_Code , Sales_Class_Desc  , Sales_Account , Sales_Return_Account , Cost_Of_Goods_Sold , Cost_Variance, Damaged_Goods, Internal_Usage , Returnable_Container , Schemes,Promotional , Cogs_InterBranch , Suspence_Account , Gain_Loss_Account , Stock_Transfer_AC, COGT_AC, DisplayPurpose_Account from TSPL_SALES_ACCOUNTS "
        Dim whrCls As String = "  "
        Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value = clsCommon.ShowSelectForm("SaleAccountSet@Finder", qry, "Sales_Class_Code", "", clsCommon.myCstr(Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value), "Sales_Class_Code", isButtonClick)

        qry = "select Sales_Class_Desc from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colSalesAccountSetDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Sales_Account from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colSalesAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colSalesAccount).Value + "'"
        Gv1.CurrentRow.Cells(colSalesAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Sales_Return_Account from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colSalesReturnAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colSalesReturnAccount).Value + "'"
        Gv1.CurrentRow.Cells(colSalesReturnAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Cost_Of_Goods_Sold from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colCostOfGoodsSoldAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colCostOfGoodsSoldAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCostOfGoodsSoldAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Cost_Variance from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colCostVarianceAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colCostVarianceAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCostVarianceAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Damaged_Goods from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colDamagedGoodsAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colDamagedGoodsAccount).Value + "'"
        Gv1.CurrentRow.Cells(colDamagedGoodsAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Internal_Usage from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colInternalUsageAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colInternalUsageAccount).Value + "'"
        Gv1.CurrentRow.Cells(colInternalUsageAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Returnable_Container from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colReturnableContainerAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colReturnableContainerAccount).Value + "'"
        Gv1.CurrentRow.Cells(colReturnableContainerAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Schemes from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colSchemesAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colSchemesAccount).Value + "'"
        Gv1.CurrentRow.Cells(colSchemesAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Promotional from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colPromotionalAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colPromotionalAccount).Value + "'"
        Gv1.CurrentRow.Cells(colPromotionalAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Cogs_InterBranch from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colCogsInterBranchAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colCogsInterBranchAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCogsInterBranchAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Suspence_Account from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colSuspenceAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colSuspenceAccount).Value + "'"
        Gv1.CurrentRow.Cells(colSuspenceAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Gain_Loss_Account from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colGainLossAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colGainLossAccount).Value + "'"
        Gv1.CurrentRow.Cells(colGainLossAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select Stock_Transfer_AC from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colStockTransferAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colStockTransferAccount).Value + "'"
        Gv1.CurrentRow.Cells(colStockTransferAccountDesc5).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select COGT_AC from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colCostofGoodsTransferAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colCostofGoodsTransferAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCostofGoodsTransferAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))


        '------------------
        qry = "select DisplayPurpose_Account from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colDisplayPurposeAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colDisplayPurposeAccount).Value + "'"
        Gv1.CurrentRow.Cells(colDisplayPurposeAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        '------------------
        qry = "select cost_of_goods_scheme from TSPL_SALES_ACCOUNTS where Sales_Class_Code ='" + Gv1.CurrentRow.Cells(colSaleAccountSetCode).Value + "'"
        Gv1.CurrentRow.Cells(colCostOfGoodsSchemeAccount).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))

        qry = "select Description from tspl_gl_accounts where Account_Code ='" + Gv1.CurrentRow.Cells(colCostOfGoodsSchemeAccount).Value + "'"
        Gv1.CurrentRow.Cells(colCostOfGoodsSchemeAccountDesc).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))





    End Sub


    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim dgv As New RadGridView
        Me.Controls.Add(dgv)
        'If transportSql.importExcel(dgv, "Item Code", "Item Desc", "Structure Code", "Structure Desc", "Item Type", "Sale Account Set Code", "Sales Account Set Desc", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblsale", "Sales Account") + " ", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblsale", "Sales Account") + " Desc" + " ", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblreturns", "Sale Return Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblreturns", "Sale Return Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblcostofgoodssold", "Cost Of Goods Sold Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblcostofgoodssold", "Cost Of Goods Sold Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblcostvariance", "Cost Variance Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblcostvariance", "Cost Variance Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblDamagedgoods", "Damaged Goods Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblDamagedgoods", "Damaged Goods Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblInternalusage", "Internal Usage Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblInternalusage", "Internal Usage Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "lblreturnable", "Returnable Container Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "lblreturnable", "Returnable Container Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "RadLabel1", "Schemes Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "RadLabel1", "Schemes Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "RadLabel2", "Promotional Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "RadLabel2", "Promotional Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel1", "Cogs InterBranch Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel1", "Cogs InterBranch Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel2", "Suspence Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel2", "Suspence Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel3", "Gain Loss Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel3", "Gain Loss Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel4", "Stock Transfer Account") + "", ) Then
        If transportSql.importExcel(dgv, "Item Code", "Item Desc", "Structure Code", "Structure Desc", "Item Type", "Sale Account Set Code", "Sales Account Set Desc", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblsale", "Sales Account") + " ", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblsale", "Sales Account") + " Desc" + " ", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblreturns", "Sale Return Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblreturns", "Sale Return Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblcostofgoodssold", "Cost Of Goods Sold Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblcostofgoodssold", "Cost Of Goods Sold Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblcostvariance", "Cost Variance Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblcostvariance", "Cost Variance Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblDamagedgoods", "Damaged Goods Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblDamagedgoods", "Damaged Goods Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblInternalusage", "Internal Usage Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "rdlblInternalusage", "Internal Usage Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "lblreturnable", "Returnable Container Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "lblreturnable", "Returnable Container Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "RadLabel1", "Schemes Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "RadLabel1", "Schemes Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "RadLabel2", "Promotional Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "RadLabel2", "Promotional Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel1", "Cogs InterBranch Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel1", "Cogs InterBranch Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel2", "Suspence Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel2", "Suspence Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel3", "Gain Loss Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel3", "Gain Loss Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel4", "Stock Transfer Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel4", "Stock Transfer Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel5", "Cost of Goods Transfer Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel5", "Cost of Goods Transfer Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel6", "Display Purpose Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel6", "Display Purpose Account") + " Desc" + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel7", "Cost of Goods Scheme Account") + "", "" + GetcolumnName("ITEM-SAL-ACC", "MyLabel7", "Cost of Goods Scheme Account") + " Desc" + "") Then
            Dim linno As Integer = 0
            Dim trans As SqlTransaction = Nothing
            Try
                connectSql.OpenConnection()
                trans = clsDBFuncationality.GetTransactin()
                clsCommon.ProgressBarShow()


                For Each dgrv As GridViewRowInfo In dgv.Rows
                    linno += 1

                    Dim strItemCode As String = clsCommon.myCstr(dgrv.Cells(0).Value)
                    Dim strStructureCode As String = clsCommon.myCstr(dgrv.Cells(2).Value)
                    Dim strItemType As String = clsCommon.myCstr(dgrv.Cells(4).Value)
                    Dim strSaleAccountSetCode As String = clsCommon.myCstr(dgrv.Cells(5).Value)
                    Dim strSaleAccount As String = clsCommon.myCstr(dgrv.Cells(7).Value) ' use
                    Dim strSaleReturnAccount As String = clsCommon.myCstr(dgrv.Cells(9).Value) ' use
                    Dim strCostOfGoodsSoldAccount As String = clsCommon.myCstr(dgrv.Cells(11).Value) ' use
                    Dim strCostVarianceAccount As String = clsCommon.myCstr(dgrv.Cells(13).Value) ' use
                    Dim strDamagedGoodsAccount As String = clsCommon.myCstr(dgrv.Cells(15).Value) ' use
                    Dim strInternalUsageAccount As String = clsCommon.myCstr(dgrv.Cells(17).Value) 'use
                    Dim strReturnableContainerAccount As String = clsCommon.myCstr(dgrv.Cells(19).Value) ' use
                    Dim strSchemesAccount As String = clsCommon.myCstr(dgrv.Cells(21).Value) ' use
                    Dim strPromotionalAccount As String = clsCommon.myCstr(dgrv.Cells(23).Value) ' use
                    Dim strCogsInterBranchAccount As String = clsCommon.myCstr(dgrv.Cells(25).Value) ' use
                    Dim strSuspenceAccount As String = clsCommon.myCstr(dgrv.Cells(27).Value) ' use
                    Dim strGainLossAccount As String = clsCommon.myCstr(dgrv.Cells(29).Value)
                    Dim strStockTransferAccount As String = clsCommon.myCstr(dgrv.Cells(31).Value) '  use
                    Dim strCostofGoodsTransferAccount As String = clsCommon.myCstr(dgrv.Cells(33).Value) ' use
                    Dim strDisplayPurposeAccount As String = clsCommon.myCstr(dgrv.Cells(35).Value)
                    Dim strCostOfGoodsSchemeAccount As String = clsCommon.myCstr(dgrv.Cells(37).Value)



                    Dim obj As New clsUpdateSaleAccountSet()

                    If String.IsNullOrEmpty(strItemCode) = True Then
                        Throw New Exception("Please fill Item Code." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strItemCode) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_ITEM_MASTER where ITEM_Code='" + strItemCode + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Item Code ( " & strStockTransferAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Item_Code = strItemCode

                    If String.IsNullOrEmpty(strStructureCode) = True Then
                        Throw New Exception("Please fill Structure Code." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strStructureCode) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_STRUCTURE_MASTER where Structure_Code='" + strStructureCode + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Structure Code ( " & strStockTransferAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Structure_Code = strStructureCode

                    If String.IsNullOrEmpty(strSaleAccountSetCode) = True Then
                        Throw New Exception("Please fill Structure Code." + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno))
                    End If
                    Dim SaleAccountExistOrNot As Boolean = clsCommon.myCBool(clsDBFuncationality.getSingleValue("select count(*)from tspl_sales_accounts where Sales_class_code='" + strSaleAccountSetCode + "'", trans))
                    If SaleAccountExistOrNot = False Then
                        Throw New Exception("Invalid Sales Acccount values" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno))
                    End If
                    obj.Sales_Class_Code = strSaleAccountSetCode


                    '  SELECT ITEM_TYPE_CODE AS Code, ITEM_TYPE_NAME  as Name FROM TSPL_ITEM_TYPE_MASTER WHERE 1=1   AND IS_NON_INVENTORY=0 

                    If String.IsNullOrEmpty(strDisplayPurposeAccount) = True Then
                        Throw New Exception("Please fill " + strDisplayPurposeAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strDisplayPurposeAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strDisplayPurposeAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Stock Transfer account ( " & strDisplayPurposeAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strDisplayPurposeAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Stock Transfer account ( " & strStockTransferAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.DisplayPurpose_Account = strDisplayPurposeAccount

                    '================================
                    If String.IsNullOrEmpty(strCostOfGoodsSchemeAccount) = True Then
                        Throw New Exception("Please fill " + strCostOfGoodsSchemeAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strCostOfGoodsSchemeAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostOfGoodsSchemeAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Cost Of Goods account ( " & strCostOfGoodsSchemeAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostOfGoodsSchemeAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Cost of goods Scheme account ( " & strCostOfGoodsSchemeAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.CostOfGoodsSchemeAccount = strDisplayPurposeAccount
                    '================================
                    If clsCommon.myLen(strStockTransferAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strStockTransferAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Stock Transfer account ( " & strStockTransferAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strStockTransferAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Stock Transfer account ( " & strStockTransferAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Stock_Transfer_AC = strStockTransferAccount

                    If clsCommon.myLen(strCostofGoodsTransferAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostofGoodsTransferAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled Cost Of Goods Transfer AC ( " & strCostofGoodsTransferAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostofGoodsTransferAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled Cost Of Goods Transfer AC ( " & strCostofGoodsTransferAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If

                    End If

                    obj.COGT_AC = strCostofGoodsTransferAccount

                    If String.IsNullOrEmpty(strSaleAccount) = True Then
                        Throw New Exception("Please fill " + strSaleAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strSaleAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSaleAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled sales account ( " & strSaleAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSaleAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled sales account ( " & strSaleAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Sales_Account = strSaleAccount

                    If String.IsNullOrEmpty(strSaleReturnAccount) = True Then
                        Throw New Exception("Please fill " + strSaleReturnAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strSaleReturnAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSaleReturnAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled sales return account ( " & strSaleReturnAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSaleReturnAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled sales return account ( " & strSaleReturnAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Sales_Return_Account = strSaleReturnAccount

                    If String.IsNullOrEmpty(strCostOfGoodsSoldAccount) = True Then
                        Throw New Exception("Please fill " + strCostOfGoodsSoldAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strCostOfGoodsSoldAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostOfGoodsSoldAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled cost of goods sold ( " & strCostOfGoodsSoldAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostOfGoodsSoldAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled cost of goods sold ( " & strCostOfGoodsSoldAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Cost_Of_Goods_Sold = strCostOfGoodsSoldAccount

                    If String.IsNullOrEmpty(strCostVarianceAccount) = True Then
                        Throw New Exception("Please fill " + strCostVarianceAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strCostVarianceAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostVarianceAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled cost variance ( " & strCostVarianceAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCostVarianceAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled cost variance ( " & strCostVarianceAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    obj.Cost_Variance = strCostVarianceAccount

                    If String.IsNullOrEmpty(strDamagedGoodsAccount) = True Then
                        Throw New Exception("Please fill " + strDamagedGoodsAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strDamagedGoodsAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strDamagedGoodsAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled damaged goods ( " & strDamagedGoodsAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strDamagedGoodsAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled damaged goods ( " & strDamagedGoodsAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    obj.Damaged_Goods = strDamagedGoodsAccount

                    If String.IsNullOrEmpty(strInternalUsageAccount) = True Then
                        Throw New Exception("Please fill " + strInternalUsageAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If
                    If clsCommon.myLen(strInternalUsageAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strInternalUsageAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled internal usages ( " & strInternalUsageAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strInternalUsageAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled internal usages ( " & strInternalUsageAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Internal_Usage = strInternalUsageAccount

                    If String.IsNullOrEmpty(strReturnableContainerAccount) = True Then
                        Throw New Exception("Please fill " + strReturnableContainerAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strReturnableContainerAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strReturnableContainerAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled returnable container ( " & strReturnableContainerAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strReturnableContainerAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled returnable container ( " & strReturnableContainerAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Returnable_Container = strReturnableContainerAccount


                    If clsCommon.myLen(strSchemesAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSchemesAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled schemes ( " & strSchemesAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSchemesAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled schemes ( " & strSchemesAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    obj.Schemes = strSchemesAccount


                    If clsCommon.myLen(strPromotionalAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strPromotionalAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled promotional ( " & strPromotionalAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strPromotionalAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled promotional ( " & strPromotionalAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If

                    obj.Promotional = strPromotionalAccount

                    If String.IsNullOrEmpty(strCogsInterBranchAccount) = True Then
                        Throw New Exception("Please fill " + strCogsInterBranchAccount + "." + Environment.NewLine + "at line no. " + clsCommon.myCstr(linno) + ".")
                    End If

                    If clsCommon.myLen(strCogsInterBranchAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCogsInterBranchAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled cogs interBranch ( " & strCogsInterBranchAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strCogsInterBranchAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled cogs interBranch ( " & strCogsInterBranchAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Cogs_InterBranch = strCogsInterBranchAccount


                    If clsCommon.myLen(strSuspenceAccount) > 0 Then
                        Dim qry As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSuspenceAccount + "'"
                        Dim check As Integer = clsDBFuncationality.getSingleValue(qry, trans)
                        If check <= 0 Then
                            Throw New Exception("Filled suspence account ( " & strSuspenceAccount & ") does not exist" + Environment.NewLine + ".First make its entry first at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                        Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + strSuspenceAccount + "' AND ControlAccount ='Y'"
                        Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1, trans)
                        If check1 <= 0 Then
                            Throw New Exception("Filled suspence account ( " & strSuspenceAccount & ") must be control account" + Environment.NewLine + " at line no. " + clsCommon.myCstr(linno) + ".")
                        End If
                    End If
                    obj.Suspence_Account = strSuspenceAccount



                    clsUpdateSaleAccountSet.SaveData(obj, trans)
                Next
                trans.Commit()
                clsCommon.ProgressBarHide()
                common.clsCommon.MyMessageBoxShow(Me, "Data Transferred Completed", Me.Text, MessageBoxButtons.OK)
            Catch ex As Exception
                trans.Rollback()
                clsCommon.ProgressBarHide()

                myMessages.myExceptions(ex)

            End Try
        End If
        Me.Controls.Remove(dgv)
    End Sub

    Public Function GetcolumnName(ByVal strFormId As String, ByVal strLabelId As String, ByVal strDefaltName As String) As String
        Dim strcolumnName As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select  COALESCE( MAX(NEW_LABEL_NAME), '" + strDefaltName + "')  from TSPL_CLIENT_FORM_LABEL_SETTING where Form_NAME = '" + strFormId + "' and LABEL_ID ='" + strLabelId + "'"))
        Return strcolumnName
    End Function

    Private Sub Gv1_CellDoubleClick(sender As Object, e As GridViewCellEventArgs) Handles Gv1.CellDoubleClick
        If e.Column Is Gv1.Columns(colItemCode) Then
            Dim strCode As String = Gv1.CurrentRow.Cells(colItemCode).Value
            If clsCommon.myLen(strCode) <= 0 Then
                clsCommon.MyMessageBoxShow("No Item code Found.")
            Else
                clsOpenTransactionForm.OpenTransacionForm(clsUserMgtCode.FrmItemMasterRMOther, strCode)
            End If
        End If
    End Sub


    Private Sub chkOnlyview_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyview.CheckedChanged
        If chkOnlyview.Checked = True Then
            Gv1.EnableFiltering = True
        Else
            Gv1.EnableFiltering = False
        End If
    End Sub
End Class

Public Class clsUpdateSaleAccountSet
#Region "Variables"
    ' select Sales_Class_Code, Sales_Class_Desc , Sales_Account , Sales_Return_Account , Cost_Of_Goods_Sold , Cost_Variance, Damaged_Goods, Internal_Usage , 
    'Returnable_Container , Schemes,Promotional , Cogs_InterBranch , Suspence_Account , Gain_Loss_Account , Stock_Transfer_AC, COGT_AC, DisplayPurpose_Account from TSPL_SALES_ACCOUNTS
    Public Sales_Class_Code As String = Nothing
    Public Sales_Class_Desc As String = Nothing
    Public Sales_Account As String = Nothing
    Public Sales_Return_Account As String = Nothing
    Public Cost_Of_Goods_Sold As String = Nothing
    Public Cost_Variance As String = Nothing
    Public Damaged_Goods As String = Nothing
    Public Internal_Usage As String = Nothing
    Public Returnable_Container As String = Nothing
    Public Schemes As String = Nothing
    Public Promotional As String = Nothing
    Public Cogs_InterBranch As String = Nothing
    Public Suspence_Account As String = Nothing
    Public Gain_Loss_Account As String = Nothing
    Public Stock_Transfer_AC As String = Nothing
    Public COGT_AC As String = Nothing
    Public DisplayPurpose_Account As String = Nothing
    Public CostOfGoodsSchemeAccount As String = Nothing
    
    ' For Item Master
    Public Item_Code As String = Nothing
    Public Structure_Code As String = Nothing
    'Public Sale_Class_Code As String = Nothing
    Public Arr As List(Of clsUpdateSaleAccountSet) = Nothing




#End Region
    Public Shared Function SaveData(ByVal obj As clsUpdateSaleAccountSet, ByVal trans As SqlTransaction) As Boolean

        'If (Arr IsNot Nothing AndAlso Arr.Count > 0) Then
        '    For Each obj As clsUpdateSaleAccountSet In Arr
        Dim coll As New Hashtable()
        ' select Sales_Class_Code, Sales_Class_Desc , Sales_Account , Sales_Return_Account , Cost_Of_Goods_Sold , Cost_Variance, Damaged_Goods, Internal_Usage , 
        'Returnable_Container , Schemes,Promotional , Cogs_InterBranch , Suspence_Account , Gain_Loss_Account , Stock_Transfer_AC, COGT_AC, DisplayPurpose_Account from TSPL_SALES_ACCOUNTS
        clsCommon.AddColumnsForChange(coll, "Sales_Class_Code", obj.Sales_Class_Code)
        ' clsCommon.AddColumnsForChange(coll, "Sales_Class_Desc", obj.Sales_Class_Desc)
        clsCommon.AddColumnsForChange(coll, "Sales_Account", obj.Sales_Account)
        clsCommon.AddColumnsForChange(coll, "Sales_Return_Account", obj.Sales_Return_Account)
        clsCommon.AddColumnsForChange(coll, "Cost_Of_Goods_Sold", obj.Cost_Of_Goods_Sold)

        clsCommon.AddColumnsForChange(coll, "Cost_Variance", obj.Cost_Variance)
        clsCommon.AddColumnsForChange(coll, "Damaged_Goods", obj.Damaged_Goods)
        clsCommon.AddColumnsForChange(coll, "Internal_Usage", obj.Internal_Usage)
        clsCommon.AddColumnsForChange(coll, "Returnable_Container", obj.Returnable_Container)
        clsCommon.AddColumnsForChange(coll, "Schemes", obj.Schemes)
        clsCommon.AddColumnsForChange(coll, "Promotional", obj.Promotional)
        clsCommon.AddColumnsForChange(coll, "Cogs_InterBranch", obj.Cogs_InterBranch)
        clsCommon.AddColumnsForChange(coll, "Suspence_Account", obj.Suspence_Account)
        clsCommon.AddColumnsForChange(coll, "Gain_Loss_Account", obj.Gain_Loss_Account)
        clsCommon.AddColumnsForChange(coll, "Stock_Transfer_AC", obj.Stock_Transfer_AC)
        clsCommon.AddColumnsForChange(coll, "COGT_AC", obj.COGT_AC)
        clsCommon.AddColumnsForChange(coll, "DisplayPurpose_Account", obj.DisplayPurpose_Account)
        clsCommon.AddColumnsForChange(coll, "cost_of_goods_scheme", obj.CostOfGoodsSchemeAccount)
        clsCommonFunctionality.UpdateDataTable(coll, "TSPL_SALES_ACCOUNTS", OMInsertOrUpdate.Update, "TSPL_SALES_ACCOUNTS.Sales_Class_Code='" + obj.Sales_Class_Code + "'", trans)
        Dim coll2 As New Hashtable()
        clsCommon.AddColumnsForChange(coll2, "Item_Code", obj.Item_Code)
        clsCommon.AddColumnsForChange(coll2, "Structure_Code", obj.Structure_Code)
        clsCommon.AddColumnsForChange(coll2, "Sale_Class_Code", obj.Sales_Class_Code)
        clsCommonFunctionality.UpdateDataTable(coll2, "tspl_item_master", OMInsertOrUpdate.Update, "tspl_item_master.Item_Code='" + obj.Item_Code + "'", trans)
        '    Next
        'End If
        Return True
    End Function
End Class
