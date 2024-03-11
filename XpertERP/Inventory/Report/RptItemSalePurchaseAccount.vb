Imports common
Imports System.Data.SqlClient
Imports System.IO
'' work to be done agaist ticket no. BHA/05/10/18-000604

Public Class RptItemSalePurchaseAccount
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Const colSno As String = "colSno"
    Const colStructureCode As String = "colStructureCode"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colAccount As String = "colAccount"
    Const colAccountDesc As String = "colAccountDesc"
    Const colInventory As String = "colInventory"
    Const colInventoryDesc As String = "colInventoryDesc"
    Const colPayable As String = "colPayable"
    Const colPayableDesc As String = "colPayableDesc"
    Const colAdj As String = "colAdj"
    Const colAdjDesc As String = "colAdjDesc"
    Const ColWIP As String = "ColWIP"
    Const ColWIPDesc As String = "ColWIPDesc"
    Const colRM As String = "colRM"
    Const colRMDesc As String = "colRMDesc"
    Const colSaleAccountSetDesc As String = "colSaleAccountSetDesc"
    Const colSaleAccount As String = "colSaleAccount"
    Const colSaleAccountDesc As String = "colSaleAccountDesc"
    Const colSaleReurn As String = "colSaleReurn"
    Const colSaleAccountSet As String = "colSaleAccountSet"
    Const colSaleReurnDesc As String = "colSaleReurnDesc"
    Const colCOGS As String = "colCOGS"
    Const colCOGSDesc As String = "colCOGSDesc"
    Const colMRPWise As String = "colMRPWise"
    Const colFatRate As String = "colFatRate"
    Const colSnfRate As String = "colSnfRate"
    Const colItemCost As String = "colItemCost"
    Const colItemConsignmentAc As String = "colItemConsignmentAc"
    Const colItemConsignmentDesc As String = "colItemConsignmentDesc"
    Public isInsideLoadData As Boolean = False
#End Region

    Private Sub SetUserMgmtNew()
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnexport.Visible = MyBase.isExport
        btnprint.Visible = MyBase.isPrintFlag
    End Sub

    Private Sub FrmItemListRpt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.R Then
            FunReset()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P Then
            btnprint.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            Me.Close()
        End If
    End Sub

    Private Sub FrmItemListRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        FunReset()
        ButtonToolTip.SetToolTip(btnreset, "Press Alt+R for reset window")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window")
        ButtonToolTip.SetToolTip(btnprint, "Press Alt+P for view report")

    End Sub



    Private Sub FunReset()
        gv.Columns.Clear()
        gv.DataSource = Nothing
        'txtItem.arrValueMember = Nothing
        'txtItemType.arrValueMember = Nothing
        txtPurchaseSet.arrValueMember = Nothing
        txtSaleAccount.arrValueMember = Nothing
        isInsideLoadData = False
        RadPageView1.SelectedPage = RadPageViewPage1
        gv.EnableFiltering = False
        chkOnlyview.Checked = False
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID
            TemplateGridview = gv
            Print(Exporter.Refresh)

        Catch ex As Exception

        End Try

    End Sub
    Function AllowToSave() As Boolean
        Try
            Dim LineNo As String
            For Each grow As GridViewRowInfo In gv.Rows
                LineNo = clsCommon.myCstr(grow.Index + 1)
                Dim Purchase_Account As String = ""
                Dim PurchaseAccount As String = clsCommon.myCstr(grow.Cells(colAccount).Value)
                Dim Inventory As String = clsCommon.myCstr(grow.Cells(colInventory).Value)
                Dim Payable As String = clsCommon.myCstr(grow.Cells(colPayable).Value)
                Dim Adj As String = clsCommon.myCstr(grow.Cells(colAdj).Value)
                Dim WIP As String = clsCommon.myCstr(grow.Cells(ColWIP).Value)
                Dim RM As String = clsCommon.myCstr(grow.Cells(colRM).Value)
                Dim SaleAccount As String = clsCommon.myCstr(grow.Cells(colSaleAccount).Value)
                Dim SaleReurn As String = clsCommon.myCstr(grow.Cells(colSaleReurn).Value)
                Dim COGS As String = clsCommon.myCstr(grow.Cells(colCOGS).Value)
                Dim SaleAccountSet As String = clsCommon.myCstr(grow.Cells(colSaleAccountSet).Value)
                If clsCommon.myLen(PurchaseAccount) > 0 Then
                    Dim qry1 As String = "Select count(*) from tspl_purchase_accounts where purchase_class_code ='" + PurchaseAccount + "'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Incorect Purchase Account Set (" & PurchaseAccount & ") ", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Inventory) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Inventory + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & Inventory & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Payable) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Payable + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & Payable & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Adj) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Adj + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & Adj & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(WIP) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + WIP + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & WIP & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(RM) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + RM + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & RM & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(SaleAccount) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + SaleAccount + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & SaleAccount & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(SaleReurn) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + SaleReurn + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & SaleReurn & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(COGS) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + COGS + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & COGS & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(SaleAccountSet) > 0 Then
                    Dim qry1 As String = "Select count(*) from tspl_sales_accounts where sales_class_code='" + SaleAccountSet + "'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Incorect Sales Account Set (" & SaleAccountSet & ") ", Me.Text)
                        Return False
                    End If
                End If
            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Sub Printbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Printbtn.Click
        Try

            Dim obj As New clsItemSalePurchaseSetMaster()
            obj.ItemArrTr = New List(Of clsItemSetDetail)
            obj.SaleArrTr = New List(Of clsItemSalePurchaseSetDetail)
            obj.PurchaseArrTr = New List(Of clsItemPurchaseSetDetail)

            If (AllowToSave()) Then
                For Each grow As GridViewRowInfo In gv.Rows
                    If (clsCommon.myLen(grow.Cells(colItemCode).Value) > 0) Then

                        Dim objTr As New clsItemSetDetail()
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.AccountCode = clsCommon.myCstr(grow.Cells(colAccount).Value)
                        objTr.SalesSetCode = clsCommon.myCstr(grow.Cells(colSaleAccountSet).Value)
                        objTr.ConsigmentAc = clsCommon.myCstr(grow.Cells(colItemConsignmentAc).Value)

                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.ItemArrTr.Add(objTr)
                        End If
                        Dim objtr1 As New clsItemPurchaseSetDetail()
                        objtr1.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objtr1.PurchaseSetCode = clsCommon.myCstr(grow.Cells(colAccount).Value)
                        objtr1.InventoryCode = clsCommon.myCstr(grow.Cells(colInventory).Value)
                        objtr1.PayableCode = clsCommon.myCstr(grow.Cells(colPayable).Value)
                        objtr1.AdjCode = clsCommon.myCstr(grow.Cells(colAdj).Value)
                        objtr1.WIPCode = clsCommon.myCstr(grow.Cells(ColWIP).Value)
                        objtr1.RMCode = clsCommon.myCstr(grow.Cells(colRM).Value)

                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.PurchaseArrTr.Add(objtr1)
                        End If

                        Dim objtr2 As New clsItemSalePurchaseSetDetail()
                        objtr2.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objtr2.SalesSetCode = clsCommon.myCstr(grow.Cells(colSaleAccountSet).Value)
                        objtr2.SaleAccount = clsCommon.myCstr(grow.Cells(colSaleAccount).Value)
                        objtr2.SaleReturn = clsCommon.myCstr(grow.Cells(colAdj).Value)
                        objtr2.COGS = clsCommon.myCstr(grow.Cells(colAdj).Value)
                        If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                            obj.SaleArrTr.Add(objtr2)
                        End If
                    End If
                Next
                If (obj.ItemArrTr Is Nothing OrElse obj.ItemArrTr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If
                If (obj.Update(obj)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Update Successfully", Me.Text)
                    FunReset()
                End If
            End If

            

        Catch ex As Exception

        End Try

    End Sub
    Enum Exporter
        Print = 2
        Refresh = 1
        PDF = 3
        Export = 4
    End Enum
    Sub Print(ByVal IsPrint As Exporter)
        Try
            isInsideLoadData = False
            If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then


                Dim qry As String = "select TSPL_ITEM_MASTER.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc	as [Item Desc],TSPL_ITEM_MASTER.gl_account as [Consumption Account],GL_Consignment.Description as [Consumption Desc],TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code as[Purchase Account Code],TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Desc as [Account Desc],TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as [Inventory] ,GL_Inv_Control_Account.Description as [Inventory Desc],TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing as [Payable Clearing] ,GL_Inv_payable_Clearning.Description as [Payable Clearing Desc] ,TSPL_PURCHASE_ACCOUNTS.Adjustment_Account as [Adjustment] ,GL_Adjustment_Account.Description as [Adjustment Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.WIP_Account as [WIP Account] ,GL_WIP_Account.Description as [WIP Account Desc] "
                qry += " ,TSPL_PURCHASE_ACCOUNTS.RM_Consumption as [RM Consumption] ,GL_RM_Consumption.Description as [RM Consumption Desc],TSPL_PURCHASE_ACCOUNTS.Shipment_Clearing as [Shipment Clearing],GL_ShipmentClearing.Description as [Shipment Clearing Desc] "
                qry += " ,TSPL_SALES_ACCOUNTS.Sales_Class_Code as [Sale Account Set]  ,TSPL_SALES_ACCOUNTS.Sales_Class_Desc as [Sale Account Set Desc]"
                qry += " ,TSPL_SALES_ACCOUNTS.Sales_Account as [Sale Account]  ,GL_Sale_Account.Description as [Sale Account Desc]"
                qry += " ,TSPL_SALES_ACCOUNTS.Sales_Return_Account as [Sale Return]  ,GL_Sale_Return.Description as [Sale Return Desc]"
                qry += " ,TSPL_SALES_ACCOUNTS.COGT_AC as [COGS] ,GL_Sale_COGS.Description as [COGS Desc] "
                qry += " from TSPL_ITEM_MASTER left outer join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.Item_Type_Code=TSPL_ITEM_MASTER.Item_Type  left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code left outer join TSPL_GL_ACCOUNTS as GL_Inv_Control_Account on GL_Inv_Control_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account left outer join TSPL_GL_ACCOUNTS as GL_Inv_payable_Clearning on GL_Inv_payable_Clearning.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing left outer join TSPL_GL_ACCOUNTS as GL_Adjustment_Account on GL_Adjustment_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Adjustment_Account left outer join TSPL_GL_ACCOUNTS as GL_RM_Consumption on GL_RM_Consumption.Account_Code=TSPL_PURCHASE_ACCOUNTS.RM_Consumption  left outer join TSPL_GL_ACCOUNTS as GL_WIP_Account on GL_WIP_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.WIP_Account"
                qry += " left outer join TSPL_SALES_ACCOUNTS on TSPL_SALES_ACCOUNTS.Sales_Class_Code=TSPL_ITEM_MASTER.Sale_Class_Code"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Sale_Account on GL_Sale_Account.Account_Code=TSPL_SALES_ACCOUNTS.Sales_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Sale_Return on GL_Sale_Return.Account_Code=TSPL_SALES_ACCOUNTS.Sales_Return_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Sale_COGS on GL_Sale_COGS.Account_Code=TSPL_SALES_ACCOUNTS.COGT_AC "
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_ShipmentClearing on GL_ShipmentClearing.Account_Code=TSPL_PURCHASE_ACCOUNTS.Shipment_Clearing "
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Consignment on GL_Consignment.Account_Code=TSPL_ITEM_MASTER.gl_account "
                qry += " where 2=2 "
                If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                End If
                If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")"
                End If
                If txtPurchaseSet.arrValueMember IsNot Nothing AndAlso txtPurchaseSet.arrValueMember.Count > 0 Then
                    qry += " and TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code in (" + clsCommon.GetMulcallString(txtPurchaseSet.arrValueMember) + ")"
                End If
                If txtSaleAccount.arrValueMember IsNot Nothing AndAlso txtSaleAccount.arrValueMember.Count > 0 Then
                    qry += " and TSPL_SALES_ACCOUNTS.Sales_Class_Code in (" + clsCommon.GetMulcallString(txtSaleAccount.arrValueMember) + ")"
                End If



                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()

                If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                ElseIf IsPrint = Exporter.Print Then

                Else
                    'gv.DataSource = dt
                    FormatGridUOM()
                    For Each row As DataRow In dt.Rows
                        gv.Rows.AddNew()
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(row("Item Code").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = clsCommon.myCstr(row("Item Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAccount).Value = clsCommon.myCstr(row("Purchase Account Code").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAccountDesc).Value = clsCommon.myCstr(row("Account Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colInventory).Value = clsCommon.myCstr(row("Inventory").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colInventoryDesc).Value = clsCommon.myCstr(row("Inventory Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colPayable).Value = clsCommon.myCstr(row("Payable Clearing").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colPayableDesc).Value = clsCommon.myCstr(row("Payable Clearing Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAdj).Value = clsCommon.myCstr(row("Adjustment").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colAdjDesc).Value = clsCommon.myCstr(row("Adjustment Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(ColWIP).Value = clsCommon.myCstr(row("WIP Account").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(ColWIPDesc).Value = clsCommon.myCstr(row("WIP Account Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colRM).Value = clsCommon.myCstr(row("RM Consumption").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colRMDesc).Value = clsCommon.myCstr(row("RM Consumption Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSaleAccountSet).Value = clsCommon.myCstr(row("Sale Account Set").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSaleAccountSetDesc).Value = clsCommon.myCstr(row("Sale Account Set Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSaleAccount).Value = clsCommon.myCstr(row("Sale Account").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSaleAccountDesc).Value = clsCommon.myCstr(row("Sale Account Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSaleReurn).Value = clsCommon.myCstr(row("Sale return").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colSaleReurnDesc).Value = clsCommon.myCstr(row("Sale return Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colCOGS).Value = clsCommon.myCstr(row("COGS").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colCOGSDesc).Value = clsCommon.myCstr(row("COGS Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colMRPWise).Value = clsCommon.myCstr(row("Shipment Clearing").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemCost).Value = clsCommon.myCstr(row("Shipment Clearing Desc").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemConsignmentAc).Value = clsCommon.myCstr(row("Consumption Account").ToString().Trim())
                        gv.Rows(gv.Rows.Count - 1).Cells(colItemConsignmentDesc).Value = clsCommon.myCstr(row("Consumption Desc").ToString().Trim())

                    Next

                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv.BestFitColumns()
                    isInsideLoadData = True
                End If
            End If
            If chkOnlyview.Checked = True Then
                Printbtn.Enabled = False
            Else
                Printbtn.Enabled = True
            End If

            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub
    Private Sub FormatGridUOM()

        Dim repoString As New GridViewTextBoxColumn()



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Code"
        repoString.Name = colItemCode
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Name"
        repoString.Name = colItemName
        repoString.Width = 250
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Consumption Account"
        repoString.Name = colItemConsignmentAc
        repoString.Width = 250
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Consumption Desc"
        repoString.Name = colItemConsignmentDesc
        repoString.Width = 250
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Account Code"
        repoString.Name = colAccount
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account Desc"
        repoString.Name = colAccountDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Inventory"
        repoString.Name = colInventory
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Inventory Desc"
        repoString.Name = colInventoryDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)




        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Payable Clearing"
        repoString.Name = colPayable
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Payable Clearing Desc"
        repoString.Name = colPayableDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Adjustment"
        repoString.Name = colAdj
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Adjustment Desc"
        repoString.Name = colAdjDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "WIP Account"
        repoString.Name = ColWIP
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "WIP Account Desc"
        repoString.Name = COlWIPDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "RM Consumption"
        repoString.Name = colRM
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "RM Consumption Desc"
        repoString.Name = colRMDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Shipment Clearing"
        repoString.Name = colMRPWise
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Shipment Clearing Desc"
        repoString.Name = colItemCost
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Sale Account Set"
        repoString.Name = colSaleAccountSet
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Sale Account Set Desc"
        repoString.Name = colSaleAccountSetDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Sale Account"
        repoString.Name = colSaleAccount
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Sale Account Desc"
        repoString.Name = colSaleAccountDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Sale Return"
        repoString.Name = colSaleReurn
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Sale Return Desc"
        repoString.Name = colSaleReurnDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "COGS"
        repoString.Name = colCOGS
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "COGS Desc"
        repoString.Name = colCOGSDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

      

    End Sub
    Private Sub gv_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv.CellValueChanged
        Try
            If (isInsideLoadData) Then

                If e.Column Is gv.Columns(colInventory) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colInventory).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colInventory).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colInventoryDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colInventory).Value + "' ")
                ElseIf e.Column Is gv.Columns(colPayable) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colPayable).Value = clsCommon.ShowSelectForm("fndPayable", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colPayable).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colPayableDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colPayable).Value + "' ")
                ElseIf e.Column Is gv.Columns(colAdj) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colAdj).Value = clsCommon.ShowSelectForm("fndInventoryControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colAdj).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colAdjDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colAdj).Value + "' ")
                ElseIf e.Column Is gv.Columns(ColWIP) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(ColWIP).Value = clsCommon.ShowSelectForm("fndWIP", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(ColWIP).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(ColWIPDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(ColWIP).Value + "' ")
                ElseIf e.Column Is gv.Columns(colRM) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colRM).Value = clsCommon.ShowSelectForm("fndRM", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colRM).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colRMDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colRM).Value + "' ")
                ElseIf e.Column Is gv.Columns(colSaleAccountSet) Then
                    Dim Qry As String = "select  Sales_Class_Code as Code , Sales_Class_Desc as Description  from TSPL_SALES_ACCOUNTS "
                    gv.CurrentRow.Cells(colSaleAccountSet).Value = clsCommon.ShowSelectForm("fndSaleAcSet", Qry, "Code", "", gv.CurrentRow.Cells(colSaleAccountSet).Value, "Code", False)
                    gv.CurrentRow.Cells(colSaleAccountSetDesc).Value = clsDBFuncationality.getSingleValue("Select Sales_Class_Desc from TSPL_SALES_ACCOUNTS Where Sales_Class_Code='" + gv.CurrentRow.Cells(colSaleAccountSet).Value + "' ")
                ElseIf e.Column Is gv.Columns(colSaleAccount) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colSaleAccount).Value = clsCommon.ShowSelectForm("fndISaleAccControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colSaleAccount).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colSaleAccountDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colSaleAccount).Value + "' ")
                ElseIf e.Column Is gv.Columns(colSaleReurn) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colSaleReurn).Value = clsCommon.ShowSelectForm("fndSaleRControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colSaleReurn).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colSaleReurnDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colSaleReurn).Value + "' ")
                ElseIf e.Column Is gv.Columns(colCOGS) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colCOGS).Value = clsCommon.ShowSelectForm("fndCOGSControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colCOGS).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colCOGSDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colCOGS).Value + "' ")
                ElseIf e.Column Is gv.Columns(colAccount) Then
                    Dim Qry As String = "select  Purchase_Class_Code as Code , Purchase_Class_Desc as [Description]  from TSPL_PURCHASE_ACCOUNTS "
                    gv.CurrentRow.Cells(colAccount).Value = clsCommon.ShowSelectForm("fndControl", Qry, "Code", "", gv.CurrentRow.Cells(colAccount).Value, "Code", False)
                    gv.CurrentRow.Cells(colAccountDesc).Value = clsDBFuncationality.getSingleValue("Select Purchase_Class_Desc from TSPL_PURCHASE_ACCOUNTS Where Purchase_Class_Code='" + gv.CurrentRow.Cells(colAccount).Value + "' ")
                ElseIf e.Column Is gv.Columns(colMRPWise) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colMRPWise).Value = clsCommon.ShowSelectForm("fndshipControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colMRPWise).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colItemCost).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colMRPWise).Value + "' ")
                ElseIf e.Column Is gv.Columns(colItemConsignmentAc) Then
                    Dim Qry As String = "select  Account_Code , Description  from TSPL_GL_ACCOUNTS "
                    gv.CurrentRow.Cells(colItemConsignmentAc).Value = clsCommon.ShowSelectForm("fndshipControl", Qry, "Account_Code", " ControlAccount ='Y' ", gv.CurrentRow.Cells(colItemConsignmentAc).Value, "Account_Code", False)
                    gv.CurrentRow.Cells(colItemConsignmentDesc).Value = clsDBFuncationality.getSingleValue("Select Description from TSPL_GL_ACCOUNTS Where Account_Code='" + gv.CurrentRow.Cells(colItemConsignmentAc).Value + "' ")

                End If
            End If
            'OpenICodeList(False)
        Catch ex As Exception

        End Try
    End Sub


    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub
    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtPurchaseSet._My_Click
        Dim qry As String
        qry = " select Purchase_Class_Code as Code,Purchase_Class_Desc as [Description] from TSPL_PURCHASE_ACCOUNTS "

        txtPurchaseSet.arrValueMember = clsCommon.ShowMultipleSelectForm("PurMulSel", qry, "Code", "Description", txtPurchaseSet.arrValueMember, txtPurchaseSet.arrDispalyMember)

    End Sub
    Private Sub txtSaleAccount__My_Click(sender As Object, e As EventArgs) Handles txtSaleAccount._My_Click
        Dim qry As String
        qry = " select Sales_Class_Code as Code,Sales_Class_Desc as [Description] from TSPL_Sales_ACCOUNTS "

        txtSaleAccount.arrValueMember = clsCommon.ShowMultipleSelectForm("SaleMulSel", qry, "Code", "Description", txtSaleAccount.arrValueMember, txtSaleAccount.arrDispalyMember)

    End Sub

    Private Sub txtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        If txtItemType.arrValueMember Is Nothing OrElse clsCommon.GetMulcallString(txtItemType.arrValueMember) = "All" Then
            qry = " select Item_Code as Code,Item_Desc as [Description] from TSPL_ITEM_MASTER  order by Item_Code "
        Else
            qry = " select Item_Code as Code,Item_Desc as [Description] from TSPL_ITEM_MASTER where Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ") order by Item_Code "

        End If
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemMulSel", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)

    End Sub
    Private Sub ReStoreGridLayout()
        Try
            Dim TempFormId As String = ""

            If clsCommon.myLen(TempFormId) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(TempFormId, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub FormatGrid()
        ' Dim strItemCode, head2 As String

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = False
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).Width = 100
        Next
        gv.Columns("Item Code").ReadOnly = True
        gv.Columns("Item Desc").ReadOnly = True

        'If chkUOMWise.Checked = True Then
        '    gv.Columns(3).Width = 250
        '    FormatGridUOM()
        'Else
        gv.Columns(1).Width = 250
        'End If

    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        Print(Exporter.Export)
        Export(EnumExportTo.Excel)
        'If gv.Rows.Count > 0 Then
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add("Item Sale Purchase Account Report")
        '    clsCommon.MyExportToExcelGrid("Item List", gv, arrHeader, "Item Sale Purchase Account Report")
        'End If
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        Print(Exporter.PDF)
        Export(EnumExportTo.PDF)
        'If gv.Rows.Count > 0 Then
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add("Item Sale Purchase Account Report")
        '    clsCommon.MyExportToPDF("Item List", gv, arrHeader, "Item Sale Purchase Account Report", True)
        'End If
    End Sub

    Private Sub Export(ByVal IsPrint As EnumExportTo)
        If gv.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Item Sale Purchase Account Report")
            If txtPurchaseSet.arrDispalyMember IsNot Nothing AndAlso txtPurchaseSet.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Purchase Account Set : " + clsCommon.GetMulcallStringWithComma(txtPurchaseSet.arrDispalyMember))
            End If
            If txtSaleAccount.arrDispalyMember IsNot Nothing AndAlso txtSaleAccount.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Sale Account Set: " + clsCommon.GetMulcallStringWithComma(txtSaleAccount.arrDispalyMember))
            End If
            If txtItemType.arrDispalyMember IsNot Nothing AndAlso txtItemType.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Type : " + clsCommon.GetMulcallStringWithComma(txtItemType.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
           
            If (IsPrint = EnumExportTo.Excel) Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Item List", gv, arrHeader, "Item Sale Purchase Account Report")
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Item List", gv, arrHeader, "Item Sale Purchase Account Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
            End If
        Else
            clsCommon.MyMessageBoxShow(Me, "No Data Found to Export", Me.Text)
            Exit Sub
        End If
    End Sub

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = MyBase.Form_ID

        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Dim TempFormId As String = ""
        TempFormId = Form_ID
        clsGridLayout.DeleteData(TempFormId, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Try
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = False
                gv.Columns(ii).IsVisible = True
                gv.Columns(ii).Width = 100
            Next
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()


            If transportSql.importExcel(gv, "Item Code", "Item Name", "Consumption Account", "Consumption Desc", "Purchase Account Code", "Account Desc", "Inventory", "Inventory Desc", "Payable Clearing", "Payable Clearing Desc", "Adjustment", "Adjustment Desc", "WIP Account", "WIP Account Desc", "RM Consumption", "RM Consumption Desc", "Sale Account Set", "Sale Account Set Desc", "Sale Account", "Sale Account Desc", "Sale Return", "Sale Return Desc", "COGS", "COGS Desc", "Shipment Clearing", "Shipment Clearing Desc") Then
                clsCommon.ProgressBarPercentShow()

                ''do sorting of records for easy saving purpose.
                Dim dt As New DataTable()
                dt = gv.DataSource()
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                FormatGridUOM()
                For Each row As DataRow In dt.Rows
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(row("Item Code").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = clsCommon.myCstr(row("Item Name").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccount).Value = clsCommon.myCstr(row("Purchase Account Code").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccountDesc).Value = clsCommon.myCstr(row("Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colInventory).Value = clsCommon.myCstr(row("Inventory").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colInventoryDesc).Value = clsCommon.myCstr(row("Inventory Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPayable).Value = clsCommon.myCstr(row("Payable Clearing").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPayableDesc).Value = clsCommon.myCstr(row("Payable Clearing Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAdj).Value = clsCommon.myCstr(row("Adjustment").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAdjDesc).Value = clsCommon.myCstr(row("Adjustment Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(ColWIP).Value = clsCommon.myCstr(row("WIP Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(ColWIPDesc).Value = clsCommon.myCstr(row("WIP Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colRM).Value = clsCommon.myCstr(row("RM Consumption").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colRMDesc).Value = clsCommon.myCstr(row("RM Consumption Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSaleAccountSet).Value = clsCommon.myCstr(row("Sale Account Set").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSaleAccountSetDesc).Value = clsCommon.myCstr(row("Sale Account Set Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSaleAccount).Value = clsCommon.myCstr(row("Sale Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSaleAccountDesc).Value = clsCommon.myCstr(row("Sale Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSaleReurn).Value = clsCommon.myCstr(row("Sale return").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colSaleReurnDesc).Value = clsCommon.myCstr(row("Sale return Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colCOGS).Value = clsCommon.myCstr(row("COGS").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colCOGSDesc).Value = clsCommon.myCstr(row("COGS Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colMRPWise).Value = clsCommon.myCstr(row("Shipment Clearing").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemCost).Value = clsCommon.myCstr(row("Shipment Clearing Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemConsignmentAc).Value = clsCommon.myCstr(row("Consumption Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemConsignmentDesc).Value = clsCommon.myCstr(row("Consumption Desc").ToString().Trim())

                Next
                'gv.DataSource = dt.DefaultView.ToTable()
                ''======================end here========================

                RadPageView1.SelectedPage = RadPageViewPage2
                clsCommon.ProgressBarPercentHide()
                clsCommon.MyMessageBoxShow(Me, "Data Transfered Successfully.", Me.Text)

            End If
        Catch ex As Exception
            clsCommon.ProgressBarPercentHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        Finally
            clsCommon.ProgressBarPercentHide()
        End Try
    End Sub

    ' Ticket No : TEC/02/05/19-000470 by prabhakar
    Private Sub chkOnlyview_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnlyview.CheckedChanged
        If chkOnlyview.Checked = True Then
            gv.EnableFiltering = True
        Else
            gv.EnableFiltering = False
        End If
    End Sub
End Class
