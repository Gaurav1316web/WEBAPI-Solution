''for Ticket No - BM00000000640 done by Dipti Waila
''for ticket No - BM00000000684
Imports Telerik.WinControls.UI
Imports Telerik.WinControls
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Imports System.Configuration
Imports Telerik.Collections.Generic
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Globalization
Imports System.Text
Imports common
Imports System.IO

Public Class frmIndent
    Inherits FrmMainTranScreen

#Region "Custom Variable"
    Const colItemCode As String = "dgvitemcode"
    Const colItemName As String = "dgvitemname"
    Const colUOM As String = "dgvUom"
    Const colPriceDate As String = "dgvcombopricedate"
    Const colMRP As String = "dgvmrpprice"
    Const colMRPInBottel As String = "dgvMRPInBottle"
    Const colBasicPrice As String = "dgvBasicPrice"
    Const colAssessableAmt As String = "dgvAssessable_amt"
    Const colItemCost As String = "dgvItemCost"
    Const colTransferQty As String = "dgvTransferQty"
    Const colPendingQty As String = "dgvPendingQty"
    Const colLoadInQty As String = "dgvLoadInQty"
    Const colBreakage As String = "dgvbreakage"
    Const colLeak As String = "Leak"
    Const colShortage As String = "shortage"
    Const colAmount As String = "dgvAmount"
    Const colTax As String = "dgvTax"
    Const colTotal As String = "dgvTotal"
    Const colActualBalanceQty As String = "ColActualBalQty"
    Const colBatchNo As String = "batchnumber"
    Const colTPT As String = "tpt"
    Const colEmptyValue As String = "emptyvalue"
    Const colBasicPriceWithTax As String = "BasicPrice_WithTax"
    Const colConversion As String = "conversion"
    Const colPendingBalance As String = "pendingbalance"
    Const colApplyTotal As String = "applytotal"
    Const colTransferQUANTITY As String = "TransferQty"
    Const colPensingBalanceInBottle As String = "pendingbalanceinbottle"
    Const colApplyTotalInBottle As String = "applytotalinbottle"
    Const colBasicAmt As String = "COLBASICAMT"

    Const colTaxRate1 As String = "COLTAXRATE1"
    Const colTaxRate2 As String = "COLTAXRATE2"
    Const colTaxRate3 As String = "COLTAXRATE3"
    Const colTaxRate4 As String = "COLTAXRATE4"
    Const colTaxRate5 As String = "COLTAXRATE5"
    Const colTaxRate6 As String = "COLTAXRATE6"
    Const colTaxRate7 As String = "COLTAXRATE7"
    Const colTaxRate8 As String = "COLTAXRATE8"
    Const colTaxRate9 As String = "COLTAXRATE9"
    Const colTaxRate10 As String = "COLTAXRATE10"

    Dim isInsideLoadData As Boolean = False


    Public TransferNo As String = Nothing
    Private IsRouteFillAgain As Boolean = False
    Public strTrnasfer As String = Nothing
    Public strItemCodeFromQuickSettlement As String = Nothing

    Dim ds As New DataSet
    Dim trans As SqlTransaction
    Dim dr As DataTable
    Dim strexcisable As String
    Dim strFromLType As String
    Dim strToLType As String
    Dim insidecellvaluechanged As Boolean = False
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Dim l1User, l2User, l3User, l4User, l5User As String
    Dim conversionamt As Decimal = 1
    Dim deductconversionamt As Decimal = 0
    Dim pendingqty As New ArrayList
    Dim RouteNo As String = String.Empty
    Dim StrQuery As String = String.Empty
    Dim FromLocationLoadInTime As String = String.Empty
    Dim HOS As String = ""
    Dim TDM As String = ""
    Dim ADC As String = ""
    Dim CE As String = ""

    Dim isNewEntry As Boolean = False
    Dim isCellValueChanged As Boolean = False
    Dim repoTransferQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoLoadinQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoBreakageQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoLeakageQty As GridViewDecimalColumn = New GridViewDecimalColumn()
    Dim repoShortageQty As GridViewDecimalColumn = New GridViewDecimalColumn()
#End Region

    Public Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.Indent)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        rdbtnsave.Visible = MyBase.isModifyFlag
        rdbtnPost.Visible = MyBase.isPostFlag
        rdbtndelete.Visible = MyBase.isDeleteFlag
        rdbtnprint.Visible = MyBase.isPrintFlag

    End Sub

    Public Sub New(ByVal LoadInNo As String, ByVal ItemCode As String, ByVal user As String, ByVal company As String)
        InitializeComponent()
        strTrnasfer = LoadInNo
        strItemCodeFromQuickSettlement = ItemCode
        Dim Sql As String = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code  FROM TSPL_USER_MASTER WHERE User_Code='" + objCommonVar.CurrentUserCode + "'"
        dr = clsDBFuncationality.GetDataTable(Sql)
        For Each row As DataRow In dr.Rows
            l1User = row(0).ToString()
            l2User = row(1).ToString()
            l3User = row(2).ToString()
            l4User = row(3).ToString()
            l5User = row(4).ToString()
        Next


    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colItemCode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        repoICode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Name"
        repoIName.Name = colItemName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        repoIName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUOM
        repoUnit.Width = 70
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoPriceDate As GridViewComboBoxColumn = New GridViewComboBoxColumn()
        repoPriceDate.HeaderText = "Price Date"
        repoPriceDate.FormatString = "{0:d}"
        repoPriceDate.Name = colPriceDate
        repoPriceDate.WrapText = True
        repoPriceDate.ReadOnly = True
        repoPriceDate.Width = 80
        gv1.MasterTemplate.Columns.Add(repoPriceDate)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Minimum = 0
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoMRPInbottle As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRPInbottle.FormatString = ""
        repoMRPInbottle.HeaderText = "MRP In Bottle"
        repoMRPInbottle.Minimum = 0
        repoMRPInbottle.Name = colMRPInBottel
        repoMRPInbottle.Width = 80
        repoMRPInbottle.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRPInbottle.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoMRPInbottle)

        Dim repoBasicPrice As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBasicPrice.FormatString = ""
        repoBasicPrice.HeaderText = "Basic Price"
        repoBasicPrice.Minimum = 0
        repoBasicPrice.Name = colBasicPrice
        repoBasicPrice.Width = 80
        repoBasicPrice.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBasicPrice.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBasicPrice)


        Dim repoAssessableAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAssessableAmt.FormatString = ""
        repoAssessableAmt.HeaderText = "Assessable Amt"
        repoAssessableAmt.Minimum = 0
        repoAssessableAmt.Name = colAssessableAmt
        repoAssessableAmt.Width = 80
        repoAssessableAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAssessableAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAssessableAmt)

        Dim repoItemCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoItemCost.FormatString = ""
        repoItemCost.HeaderText = "Item Cost"
        repoItemCost.Minimum = 0
        repoItemCost.Name = colItemCost
        repoItemCost.Width = 80
        repoItemCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoItemCost.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoItemCost)

        repoTransferQty = New GridViewDecimalColumn()
        repoTransferQty.FormatString = ""
        repoTransferQty.HeaderText = "Quantity"
        repoTransferQty.Minimum = 0
        repoTransferQty.Name = colTransferQty
        repoTransferQty.Width = 80
        repoTransferQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTransferQty.ReadOnly = False
        repoTransferQty.ShowUpDownButtons = False
        repoTransferQty.Step = 0
        gv1.MasterTemplate.Columns.Add(repoTransferQty)

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Quantity"
        repoPendingQty.Minimum = 0
        repoPendingQty.Name = colPendingQty
        repoPendingQty.Width = 80
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoPendingQty.ReadOnly = True
        repoPendingQty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

        repoLoadinQty = New GridViewDecimalColumn()
        repoLoadinQty.FormatString = ""
        repoLoadinQty.HeaderText = "Loadin Qty"
        repoLoadinQty.Minimum = 0
        repoLoadinQty.Name = colLoadInQty
        repoLoadinQty.Width = 80
        repoLoadinQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLoadinQty.ReadOnly = False
        repoLoadinQty.ShowUpDownButtons = False
        repoLoadinQty.Step = 0
        gv1.MasterTemplate.Columns.Add(repoLoadinQty)

        repoBreakageQty = New GridViewDecimalColumn()
        repoBreakageQty.FormatString = ""
        repoBreakageQty.HeaderText = "Breakage"
        repoBreakageQty.Minimum = 0
        repoBreakageQty.Name = colBreakage
        repoBreakageQty.Width = 80
        repoBreakageQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBreakageQty.ReadOnly = False
        repoBreakageQty.ShowUpDownButtons = False
        repoBreakageQty.Step = 0
        gv1.MasterTemplate.Columns.Add(repoBreakageQty)

        repoLeakageQty = New GridViewDecimalColumn()
        repoLeakageQty.FormatString = ""
        repoLeakageQty.HeaderText = "Leakage"
        repoLeakageQty.Minimum = 0
        repoLeakageQty.Name = colLeak
        repoLeakageQty.Width = 80
        repoLeakageQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLeakageQty.ReadOnly = False
        repoLeakageQty.ShowUpDownButtons = False
        repoLeakageQty.Step = 0
        gv1.MasterTemplate.Columns.Add(repoLeakageQty)

        repoShortageQty = New GridViewDecimalColumn()
        repoShortageQty.FormatString = ""
        repoShortageQty.HeaderText = "Shortage"
        repoShortageQty.Minimum = 0
        repoShortageQty.Name = colShortage
        repoShortageQty.Width = 80
        repoShortageQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoShortageQty.ReadOnly = False
        repoShortageQty.ShowUpDownButtons = False
        repoShortageQty.Step = 0
        gv1.MasterTemplate.Columns.Add(repoShortageQty)

        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Amount"
        repoAmt.Minimum = 0
        repoAmt.Name = colAmount
        repoAmt.Width = 80
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoAmt)


        Dim repoBasicAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBasicAmt.FormatString = ""
        repoBasicAmt.HeaderText = "Basic Amount"
        repoBasicAmt.Minimum = 0
        repoBasicAmt.Name = colBasicAmt
        repoBasicAmt.Width = 80
        repoBasicAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBasicAmt.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoBasicAmt)


        Dim repoTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTax.FormatString = ""
        repoTax.HeaderText = "Tax"
        repoTax.Minimum = 0
        repoTax.Name = colTax
        repoTax.Width = 80
        repoTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTax.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTax)

        Dim repoTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotal.FormatString = ""
        repoTotal.HeaderText = "Total"
        repoTotal.Minimum = 0
        repoTotal.Name = colTotal
        repoTotal.Width = 80
        repoTotal.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTotal.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoTotal)


        Dim repoAcutalBalanceQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAcutalBalanceQty.FormatString = ""
        repoAcutalBalanceQty.HeaderText = "Actual Balance Qty"
        repoAcutalBalanceQty.Minimum = 0
        repoAcutalBalanceQty.Name = colActualBalanceQty
        repoAcutalBalanceQty.Width = 80
        repoAcutalBalanceQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAcutalBalanceQty.ReadOnly = True
        repoAcutalBalanceQty.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoAcutalBalanceQty)

        Dim repoBatchNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoBatchNo.FormatString = ""
        repoBatchNo.HeaderText = "Batch No"
        repoBatchNo.Name = colBatchNo
        repoBatchNo.Width = 80
        repoBatchNo.ReadOnly = True
        repoBatchNo.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoBatchNo)


        Dim repoTPT As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTPT.FormatString = ""
        repoTPT.HeaderText = "TPT Value"
        repoTPT.Minimum = 0
        repoTPT.Name = colTPT
        repoTPT.Width = 100
        repoTPT.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoTPT.ReadOnly = True
        repoTPT.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoTPT)

        Dim repoBasicPriceWithTax As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBasicPriceWithTax.FormatString = ""
        repoBasicPriceWithTax.HeaderText = "Basic Price With Tax"
        repoBasicPriceWithTax.Minimum = 0
        repoBasicPriceWithTax.Name = colBasicPriceWithTax
        repoBasicPriceWithTax.Width = 80
        repoBasicPriceWithTax.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBasicPriceWithTax.ReadOnly = True
        repoBasicPriceWithTax.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoBasicPriceWithTax)

        Dim repoEmptyValue As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoEmptyValue.FormatString = ""
        repoEmptyValue.HeaderText = "Empty"
        repoEmptyValue.Minimum = 0
        repoEmptyValue.Name = colEmptyValue
        repoEmptyValue.Width = 80
        repoEmptyValue.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoEmptyValue.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoEmptyValue)

        Dim repoCovnersion As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoCovnersion.FormatString = ""
        repoCovnersion.HeaderText = "Conversion"
        repoCovnersion.Minimum = 0
        repoCovnersion.Name = colConversion
        repoCovnersion.Width = 80
        repoCovnersion.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoCovnersion.ReadOnly = True
        repoCovnersion.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoCovnersion)

        Dim repoPendingBalance As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingBalance.FormatString = ""
        repoPendingBalance.HeaderText = "Pending Balance"
        repoPendingBalance.Minimum = 0
        repoPendingBalance.Name = colPendingBalance
        repoPendingBalance.Width = 80
        repoPendingBalance.ReadOnly = True
        repoPendingBalance.IsVisible = False
        gv1.Columns.Add(repoPendingBalance)

        Dim repoApplyTotal As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoApplyTotal.FormatString = ""
        repoApplyTotal.HeaderText = "Apply Total"
        repoApplyTotal.Minimum = 0
        repoApplyTotal.Name = colApplyTotal
        repoApplyTotal.Width = 80
        repoApplyTotal.ReadOnly = True
        repoApplyTotal.IsVisible = False
        gv1.Columns.Add(repoApplyTotal)

        Dim repoTransferQuantity As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTransferQuantity.FormatString = ""
        repoTransferQuantity.HeaderText = "Transfer QUANTITY"
        repoTransferQuantity.Minimum = 0
        repoTransferQuantity.Name = colTransferQUANTITY
        repoTransferQuantity.Width = 80
        repoTransferQuantity.ReadOnly = True
        repoTransferQuantity.IsVisible = False
        gv1.Columns.Add(repoTransferQuantity)

        Dim repoBalInBottle As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBalInBottle.FormatString = ""
        repoBalInBottle.HeaderText = "Pending Balance In Bottle"
        repoBalInBottle.Minimum = 0
        repoBalInBottle.Name = colPensingBalanceInBottle
        repoBalInBottle.Width = 80
        repoBalInBottle.ReadOnly = True
        repoBalInBottle.IsVisible = False
        gv1.Columns.Add(repoBalInBottle)

        Dim repoTotAppInBootle As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTotAppInBootle.FormatString = ""
        repoTotAppInBootle.HeaderText = "Apply Total In Bottle"
        repoTotAppInBootle.Minimum = 0
        repoTotAppInBootle.Name = colApplyTotalInBottle
        repoTotAppInBootle.Width = 80
        repoTotAppInBootle.ReadOnly = True
        repoTotAppInBootle.IsVisible = False
        gv1.Columns.Add(repoTotAppInBootle)


        Dim repoTaxRate1 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate1 = New GridViewDecimalColumn()
        repoTaxRate1.FormatString = ""
        repoTaxRate1.HeaderText = "Tax Rate 1"
        repoTaxRate1.Name = colTaxRate1
        repoTaxRate1.IsVisible = False
        repoTaxRate1.ReadOnly = True
        repoTaxRate1.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate1)

        Dim repoTaxRate2 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate2 = New GridViewDecimalColumn()
        repoTaxRate2.FormatString = ""
        repoTaxRate2.HeaderText = "Tax Rate 2"
        repoTaxRate2.Name = colTaxRate2
        repoTaxRate2.IsVisible = False
        repoTaxRate2.ReadOnly = True
        repoTaxRate2.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate2)

        Dim repoTaxRate3 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate3 = New GridViewDecimalColumn()
        repoTaxRate3.FormatString = ""
        repoTaxRate3.HeaderText = "Tax Rate 3"
        repoTaxRate3.Name = colTaxRate3
        repoTaxRate3.IsVisible = False
        repoTaxRate3.ReadOnly = True
        repoTaxRate3.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate3)

        Dim repoTaxRate4 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate4 = New GridViewDecimalColumn()
        repoTaxRate4.FormatString = ""
        repoTaxRate4.HeaderText = "Tax Rate 4"
        repoTaxRate4.Name = colTaxRate4
        repoTaxRate4.IsVisible = False
        repoTaxRate4.ReadOnly = True
        repoTaxRate4.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate4)

        Dim repoTaxRate5 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate5 = New GridViewDecimalColumn()
        repoTaxRate5.FormatString = ""
        repoTaxRate5.HeaderText = "Tax Rate 5"
        repoTaxRate5.Name = colTaxRate5
        repoTaxRate5.IsVisible = False
        repoTaxRate5.ReadOnly = True
        repoTaxRate5.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate5)

        Dim repoTaxRate6 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate6 = New GridViewDecimalColumn()
        repoTaxRate6.FormatString = ""
        repoTaxRate6.HeaderText = "Tax Rate 6"
        repoTaxRate6.Name = colTaxRate6
        repoTaxRate6.IsVisible = False
        repoTaxRate6.ReadOnly = True
        repoTaxRate6.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate6)

        Dim repoTaxRate7 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate7 = New GridViewDecimalColumn()
        repoTaxRate7.FormatString = ""
        repoTaxRate7.HeaderText = "Tax Rate 7"
        repoTaxRate7.Name = colTaxRate7
        repoTaxRate7.IsVisible = False
        repoTaxRate7.ReadOnly = True
        repoTaxRate7.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate7)


        Dim repoTaxRate8 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate8 = New GridViewDecimalColumn()
        repoTaxRate8.FormatString = ""
        repoTaxRate8.HeaderText = "Tax Rate 8"
        repoTaxRate8.Name = colTaxRate8
        repoTaxRate8.IsVisible = False
        repoTaxRate8.ReadOnly = True
        repoTaxRate8.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate8)


        Dim repoTaxRate9 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate9 = New GridViewDecimalColumn()
        repoTaxRate9.FormatString = ""
        repoTaxRate9.HeaderText = "Tax Rate 9"
        repoTaxRate9.Name = colTaxRate9
        repoTaxRate9.IsVisible = False
        repoTaxRate9.ReadOnly = True
        repoTaxRate9.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate9)

        Dim repoTaxRate10 As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTaxRate10 = New GridViewDecimalColumn()
        repoTaxRate10.FormatString = ""
        repoTaxRate10.HeaderText = "Tax Rate 10"
        repoTaxRate10.Name = colTaxRate10
        repoTaxRate10.IsVisible = False
        repoTaxRate10.ReadOnly = True
        repoTaxRate10.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTaxRate10)


        gv1.AllowDeleteRow = True
        'gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40

        ReStoreGridLayout()
    End Sub

    Private Sub AddNew()
        isNewEntry = True
        'txtTransferDate.Enabled = False
        txtFromLoaction.Value = String.Empty
        txtToLocation.Value = String.Empty
        UsLock1.Status = ERPTransactionStatus.Pending
        txtRouteNo.Value = String.Empty
        txtRouteNo.Enabled = False
        lblRouteNo.Text = ""
        txtPriceCode.Value = String.Empty
        lblPriceCode.Text = String.Empty
        fndTaxGroup.Value = String.Empty
        lblReferenceDocumentNo.Text = ""
        txtKMReading.Text = String.Empty
        txtKMReading.ReadOnly = False
        txtReference.Text = String.Empty
        txtDescription.Text = String.Empty
        txtLoadoutNo.Value = String.Empty
        txtVehicleCode.Value = String.Empty
        txtVehicle.Text = ""
        lblVehicleDesc.Text = String.Empty
        lblFromLocation.Text = String.Empty
        lblToLocation.Text = String.Empty
        lblPriceCode.Text = String.Empty
        lblfb.Text = 0.0
        lblfc.Text = 0.0
        chkApplyShell.Checked = False
        chkAgainstFForm.Enabled = False
        chkAgainstFForm.Checked = False
        rdbeall.IsChecked = False
        rdbeb.IsChecked = False
        rdbec.IsChecked = False
        rdbfc.IsChecked = False
        rdbfb.IsChecked = False
        rdball.IsChecked = False
        gv2.DataSource = Nothing
        gv2.Rows.Clear()
        txtitemcost.Text = 0.0
        txtAmount.Text = 0.0
        txtTotalAmount.Text = 0.0
        txtTotalTaxAmt.Text = 0.0
        txtTotalTax.Text = "0.00"

        txtTripNo.Text = String.Empty
        txtTripNo.ReadOnly = False
        rdbtnsave.Enabled = True
        txtLoadoutNo.Visible = False
        rdlblloadoutno.Visible = True
        cboModeofTransport.Text = "By Road"

        txtTransferDate.Value = clsCommon.GETSERVERDATE()
        lblSalesman.Text = ""
        txtSalesman.Value = ""
        cboTransferType.Enabled = False
        cboTransferType.Text = "Load Out"

        cmbitemtype.SelectedIndex = 1
        cmbitemtype.Enabled = False
        txtTransferNo.MyReadOnly = False
        txtTransferNo.Value = String.Empty
        txtPriceCode.MyReadOnly = False
        txtPriceCode.Enabled = True
        txtLoadInOutAmt.Text = 0.0
        txtVehicleCode.MyReadOnly = False
        txtVehicleCode.Enabled = True
        txtReference.ReadOnly = False
        txtDescription.ReadOnly = False
        'txtTransferDate.Enabled = True
        rdbtnPost.Enabled = True
        rdbtnsave.Enabled = True
        rdbtndelete.Enabled = True

        txtTripNo.Text = "1"
        txtToLocation.Enabled = True
        chkApplyShell.Enabled = False
        LoadBlankGrid()
        cboType.SelectedValue = "Excise"
        cboType_Validating(Nothing, Nothing)
        cboType.Enabled = True
        txtTransferDate.Focus()
        pnlItemType.Visible = False
        txtFromLoaction.Enabled = True
        lblTotalBasicAmt.Text = ""
        cboType.Visible = True
        lblType.Visible = True
        chkIsAutoCreate.Checked = False
    End Sub

    Sub LoadType()
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = ""
        dr("Name") = "Select"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Route"
        dr("Name") = "Route"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Excise"
        dr("Name") = "Excise"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "Depot"
        dr("Name") = "Depot"
        dt.Rows.Add(dr)


        cboType.DataSource = dt
        cboType.ValueMember = "Code"
        cboType.DisplayMember = "Name"
    End Sub

    Private Sub frmTransfer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetUserMgmtNew()
        LoadType()
        Threading.Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        txtTransferNo.MyMaxLength = 30
        txtTransferNo.MyReadOnly = True
        AddNew()
        ButtonToolTip.SetToolTip(rdbtnsave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(rdbtnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(rdbtndelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(rdbtnclose, "Press Esc Close the Window")
        ButtonToolTip.SetToolTip(rdbtnreset, "Press Alt+N Adding New Transction")
        ButtonToolTip.SetToolTip(rdbtnprint, "Press Alt+R for Print Preview")



        cboTransferType.Text = "Load Out"
        cmbitemtype.SelectedIndex = 1
        txtFromLoaction.Focus()
        If clsCommon.myLen(strTrnasfer) > 0 Then
            txtTransferNo.Value = strTrnasfer
            'FunFill()
            LoadData(strTrnasfer)
            If strItemCodeFromQuickSettlement IsNot Nothing Then
                Dim I As Integer = 0
                For Each dr As GridViewRowInfo In gv1.Rows
                    Dim gridItmCode As String = dr.Cells(colItemCode).Value
                    If strItemCodeFromQuickSettlement = gridItmCode.Trim Then
                        gv1.CurrentRow = gv1.Rows(I)
                        Return
                    End If
                    I += 1
                Next
            End If
        End If
        ReStoreGridLayout()
        txtTripNo.Text = "1"
        LoadBlankGrid()
        rdpageviewTrnasfer.SelectedPage = rdmageview1transfer
        If clsCommon.myLen(strTrnasfer) > 0 Then
            LoadData(strTrnasfer)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag))
        End If
    End Sub

    Private Sub frmTransfer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso rdbtnreset.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso rdbtnsave.Enabled Then
            SaveTransfer()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso rdbtnPost.Enabled = True Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.R AndAlso rdbtnprint.Enabled Then
            printdata(True)
         
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso rdbtndelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            closeform()
        ElseIf e.KeyCode = Keys.Enter Then
            If gv1.Rows.Count > 0 Then
                Dim i As Integer = gv1.CurrentRow.Index + 1
                If gv1.Rows.Count > i Then
                    gv1.CurrentRow = gv1.Rows(i)
                ElseIf gv1.Rows.Count = i Then
                    gv1.CurrentRow = gv1.Rows(0)
                End If
            End If
        End If
    End Sub

    Private Sub rdbfc_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbfc.ToggleStateChanged, rdbfb.ToggleStateChanged, rdball.ToggleStateChanged
        If rdbfc.IsChecked Then
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                priceDateSelection(False, "FC")
            Else
                FunFillItemDetails_LoadOut_No("FC")
            End If
        ElseIf rdbfb.IsChecked Then
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                priceDateSelection(False, "FB")
            Else
                FunFillItemDetails_LoadOut_No("FB")
            End If
        ElseIf rdball.IsChecked Then
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                priceDateSelection(False)
            Else
                FunFillItemDetails_LoadOut_No()
            End If
        Else
            chkApplyShell.Enabled = False
            chkApplyShell.Checked = False
        End If
    End Sub

    Private Sub rdbec_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles rdbec.ToggleStateChanged, rdbeb.ToggleStateChanged, rdbeall.ToggleStateChanged
        If rdbec.IsChecked Then
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                priceDateSelection(False, "EC")
            Else
                funloadinagainstloadout("EC")
            End If
        ElseIf rdbeb.IsChecked Then
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                priceDateSelection(False, "EB")
            Else
                funloadinagainstloadout("EB")
            End If
        ElseIf rdbeall.IsChecked Then
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                priceDateSelectionEMPTY()
            Else
                funloadinagainstloadout()
            End If
        End If
    End Sub

    Private Sub cmbitemtype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cmbitemtype.SelectedIndexChanged
        grpFull.Visible = False
        grpEmpty.Visible = False
        cboType.Visible = False
        lblType.Visible = False
        If cmbitemtype.SelectedIndex = 0 Then
            FunDisable()
            cboType.SelectedIndex = 0
        ElseIf cmbitemtype.SelectedIndex = 1 Then
            FunEnable()
            grpFull.Visible = False 'True But Changed
            cboType.Visible = True
            lblType.Visible = True

        ElseIf cmbitemtype.SelectedIndex = 2 Then
            FunEnable()
            grpEmpty.Visible = False 'True But Changed
            cboType.SelectedIndex = 0
            'fndTaxGroup.Enabled = False
        End If
    End Sub

    Private Sub rddrplisttransfertype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles cboTransferType.SelectedIndexChanged, cboTransferType.SelectedValueChanged
        pnlItemType.Visible = False

        If cboTransferType.SelectedIndex = 0 Then
            FunDisable()
            grpFull.Visible = False

            grpEmpty.Visible = False

        ElseIf clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
            'cmbitemtype.Enabled = True
            FunLoadToLocationPhysical()
            '  FunLoadFromLocation(False)
            FunColumnEditable()
            txtLoadoutNo.Visible = False
            rdlblloadoutno.Visible = False
            'fndTaxGroup.Enabled = False
            chkApplyShell.Enabled = False
            txtFromLoaction.Enabled = True
            grpFull.Visible = False

            grpEmpty.Visible = False 'True But Changed

        ElseIf clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
            'cmbitemtype.Enabled = True
            rdlblloadoutno.Enabled = True
            rdlblloadoutno.Visible = True
            txtLoadoutNo.MyReadOnly = False
            txtLoadoutNo.Visible = True
            rdlblloadoutno.Visible = True
            FunColumnEditable()
            'fndTaxGroup.Enabled = False
            chkApplyShell.Enabled = False
            txtKMReading.ReadOnly = False
            txtLoadoutNo.Enabled = True
            If txtTransferNo.Value = "" Then
                txtLoadoutNo.Focus()
            End If
            grpFull.Visible = False 'True But Changed
            pnlItemType.Visible = True
        End If
        ReStoreGridLayout()
    End Sub

    Private Sub FunDeleteShellItem()
        Dim count As Decimal = 0
        For Each gro As GridViewRowInfo In gv1.Rows
            If gro.Cells(colItemCode).Value = "PC300BSH" AndAlso clsCommon.myCdbl(gro.Cells(colTransferQty).Value) <= 0 Then
                count = gro.Index
                Exit For
            End If
        Next
        If count > 0 Then
            gv1.Rows.RemoveAt(count)
        Else
            common.clsCommon.MyMessageBoxShow(Me, "No Shell Item Apply", Me.Text)
        End If
    End Sub

    Private Sub FunAddShellItem()
        Dim count As Decimal = 0
        For Each gro As GridViewRowInfo In gv1.Rows
            count = count + 1
            If gro.Cells(colItemCode).Value = "PC300BSH" Then
                common.clsCommon.MyMessageBoxShow(Me, "Shell Item Applied", Me.Text)
                Exit Sub
            End If
        Next
        Dim myreader As DataTable = clsDBFuncationality.GetDataTable("select TOP 1  UOM , Start_Date , Item_Basic_Net , Item_Basic_Price  from TSPL_ITEM_PRICE_MASTER where Item_Code = 'PC300BSH'  order by Start_Date desc ")
        If myreader.Rows.Count > 0 Then
            For Each row As DataRow In myreader.Rows
                Dim viewInfo As New GridViewInfo(gv1.MasterTemplate)
                Dim grow As New GridViewDataRowInfo(viewInfo)
                grow.Cells(colItemCode).Value = "PC300BSH"
                grow.Cells(colItemName).Value = "PEPSI COLA 300 ML BOTTLE SHELL"
                grow.Cells(colPriceDate).Value = CDate(row("Start_Date")).ToString("dd-MM-yyyy")
                grow.Cells(colUOM).Value = Convert.ToString(row("UOM"))
                grow.Cells(colAssessableAmt).Value = "0.00"
                grow.Cells(colAmount).Value = "0.00"
                grow.Cells(colTax).Value = "0.00"
                grow.Cells(colTotal).Value = "0.00"
                grow.Cells(colTPT).Value = "0.00"
                grow.Cells(colMRP).Value = Convert.ToString(row("Item_Basic_Net"))
                grow.Cells(colBatchNo).Value = ""
                grow.Cells(colBasicPrice).Value = Convert.ToString(row("Item_Basic_Price"))
                grow.Cells(colLeak).Value = "0.00"
                grow.Cells(colLeak).ReadOnly = True
                grow.Cells(colShortage).Value = "0.00"
                grow.Cells(colShortage).ReadOnly = True
                grow.Cells(colItemCost).Value = "0.00"
                grow.Cells(colConversion).Value = "1"
                grow.Cells(colPendingBalance).Value = "0.00"
                grow.Cells(colTransferQty).Value = "0.00"
                grow.Cells(colPendingQty).Value = "0.00"
                grow.Cells(colLoadInQty).Value = "0.00"
                grow.Cells(colBreakage).Value = "0.00"
                grow.Cells(colItemCost).Value = Convert.ToString(row("Item_Basic_Net"))
                grow.Cells(colBasicPriceWithTax).Value = Convert.ToString(row("Item_Basic_Price"))
                grow.Cells(colEmptyValue).Value = "0.00"
                gv1.Rows.Insert(count, grow)
            Next
        Else
            common.clsCommon.MyMessageBoxShow(Me, "Shell Item Does Not Exist", Me.Text)
        End If
    End Sub

    Private Sub rdbtnclose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnclose.Click
        closeform()
    End Sub

    Private Sub RouteDesc()
        lblRouteNo.Text = connectSql.RunScalar("Select  route_desc from tspl_route_master where route_no ='" + txtRouteNo.Value + "'")
    End Sub

    Public Sub RouteNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If txtRouteNo.Value <> "" Then
            If txtFromLoaction.Value = "" Then
                common.clsCommon.MyMessageBoxShow(Me, "Please choose location first.", Me.Text)
                txtRouteNo.Value = String.Empty
                'txtFromLoaction.Focus()
                txtRouteNo.Value = ""
            End If
        End If
        lblRouteNo.Text = connectSql.RunScalar("Select  route_desc from tspl_route_master where route_no ='" + txtRouteNo.Value + "'")
        If lblRouteNo.Text <> "" Then
            Dim CheckExcisable As String = String.Empty
            Dim myreader As DataTable = clsDBFuncationality.GetDataTable("Select employee_code,price_code,NonPrice_Code,vehicle_code from tspl_route_master where route_no = '" + txtRouteNo.Value + "' ")
            If myreader.Rows.Count > 0 Then
                For Each row As DataRow In myreader.Rows
                    txtSalesman.Text = Convert.ToString(row("employee_code"))
                    txtToLocation.Value = Convert.ToString(row("employee_code"))
                    CheckExcisable = connectSql.RunScalar("select Excisable  from TSPL_LOCATION_MASTER where Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "'")
                    If CheckExcisable = "F" Then
                        txtPriceCode.Value = Convert.ToString(row("NonPrice_Code"))
                    Else
                        txtPriceCode.Value = Convert.ToString(row("price_code"))
                    End If
                    txtVehicleCode.Value = Convert.ToString(row("vehicle_code"))
                    txtVehicle.Text = txtVehicleCode.Value
                Next
            End If
            txtPriceCode.Enabled = False
            cboPriceDate.Enabled = True
        Else
            'FunLoadToLocationPhysical()
            txtPriceCode.Enabled = True
            txtPriceCode.Value = ""
        End If
    End Sub

    Private Sub rdbtnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnsave.Click
        SaveTransfer()
    End Sub

    Private Sub SaveTransfer()
        Try
            If AllowToSave() Then
                SaveData()
                clsCommon.MyMessageBoxShow(Me, "Data saved successfully", Me.Text)
                PrintDataNew(False)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub

    Private Sub FunFillEmptyvalue(ByVal grow As GridViewRowInfo)
        Dim emptybottle As Decimal = 0
        Dim emptyshell As Decimal = 0
        If grow.Cells(colConversion).Value = 1 Then
            Dim emptyreader As DataTable = clsDBFuncationality.GetDataTable("select isnull(Empty_Value_Shell,0) as [Empty_Value_Shell] , isnull(Empty_Value_Bottle,0) as [Empty_Value_Bottle]   from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and UOM = '" + Convert.ToString(grow.Cells(colUOM).Value) + "' and Item_Basic_Net = '" + Convert.ToString(grow.Cells(colMRP).Value) + "' and Start_Date = '" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' AND Price_Code ='" + txtPriceCode.Value + "' ")
            If emptyreader.Rows.Count > 0 Then
                For Each row As DataRow In emptyreader.Rows
                    emptybottle = clsCommon.myCdbl(row("Empty_Value_Bottle"))
                    emptyshell = clsCommon.myCdbl(row("Empty_Value_Shell"))
                Next
            End If
            'emptyreader.Close()
            If grow.Cells(colLoadInQty).Value = 0 Then
                grow.Cells(colEmptyValue).Value = emptybottle + emptyshell
            End If
        Else
            Dim emptyreader As DataTable = clsDBFuncationality.GetDataTable("select isnull(Empty_Value_Shell,0) as [Empty_Value_Shell] , isnull(Empty_Value_Bottle,0) as [Empty_Value_Bottle]   from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and UOM = '" + Convert.ToString(grow.Cells(colUOM).Value) + "' and Item_Basic_Net = '" + Convert.ToString(grow.Cells(colMRP).Value) + "' and Start_Date = '" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' AND Price_Code ='" + txtPriceCode.Value + "' ")
            If emptyreader.Rows.Count > 0 Then
                For Each row As DataRow In emptyreader.Rows
                    emptybottle = row("Empty_Value_Bottle")
                    emptyshell = row("Empty_Value_Shell")
                Next
            End If
            'emptyreader.Close()
            '*************aDDED bY  mANOJ cHAUHAN : TO CHANGE  IN EMPTY VALUE WITHOUT QTY *
            If grow.Cells(colUOM).Value = "FB" Then
                grow.Cells(colEmptyValue).Value = clsCommon.myCdbl(emptyshell) + clsCommon.myCdbl(emptybottle)
            Else
                grow.Cells(colEmptyValue).Value = clsCommon.myCdbl(emptyshell) + clsCommon.myCdbl(emptybottle * clsCommon.myCdbl(grow.Cells(colLoadInQty).Value))
            End If
            If clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) = 0 Then
                grow.Cells(colEmptyValue).Value = emptybottle + emptyshell
            End If
        End If
        Dim exicisable As String = connectSql.RunScalar("Select Excisable from tspl_location_Master where Location_code='" + Convert.ToString(FromLocationLoadInTime) + "'")
        If exicisable = "T" Then
            grow.Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) * clsCommon.myCdbl(grow.Cells(colBasicPrice).Value), 2) + Math.Round(clsCommon.myCdbl(grow.Cells(colLeak).Value) * clsCommon.myCdbl(grow.Cells(colEmptyValue).Value), 2)
            grow.Cells(colTotal).Value = grow.Cells(colAmount).Value
        Else
            grow.Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) * clsCommon.myCdbl(grow.Cells(colMRP).Value), 2) + Math.Round(clsCommon.myCdbl(grow.Cells(colLeak).Value) * clsCommon.myCdbl(grow.Cells(colEmptyValue).Value), 2)
            grow.Cells(colTotal).Value = grow.Cells(colAmount).Value

        End If
    End Sub

    Private Sub dgvitemdetails1_CellFormatting(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CellFormattingEventArgs) Handles gv1.CellFormatting
        Try
            If e.Column.Index >= 0 Then


                If e.Column Is gv1.Columns(colTransferQty) Then
                    gv1.CurrentRow.Cells(colTransferQty).ReadOnly = IIf(clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal, False, True)
                ElseIf e.Column Is gv1.Columns(colLoadInQty) Then
                    gv1.CurrentRow.Cells(colLoadInQty).ReadOnly = IIf(clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal, False, True)
                ElseIf e.Column Is gv1.Columns(colShortage) Then
                    gv1.CurrentRow.Cells(colShortage).ReadOnly = IIf(clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal, False, True)
                ElseIf e.Column Is gv1.Columns(colBreakage) Then
                    gv1.CurrentRow.Cells(colBreakage).ReadOnly = IIf(clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal, False, True)
                ElseIf e.Column Is gv1.Columns(colLeak) Then
                    gv1.CurrentRow.Cells(colLeak).ReadOnly = IIf(clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.CurrentRow.Cells(colEmptyValue).Value) > 0, False, True)
                End If
            End If
        Catch ex As Exception
            '        clsCommon.MyMessageBoxShow(ex.Message, Me.Text)
        End Try
    End Sub


    Private Sub dgvitemdetails1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If Not isCellValueChanged Then
                isCellValueChanged = True

                If Not isInsideLoadData Then
                    Dim total1, PendingQty, AppliedQty As Decimal

                    Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
                    Dim column As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)

                    If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                        If e.Column.Name = colLoadInQty Or e.Column.Name = colBreakage Or e.Column.Name = colLeak Or e.Column.Name = colShortage Then
                            If grow.Cells(colUOM).Value <> "SH" Then
                                total1 = TotalApplyQtyInBottle()
                            Else
                                total1 = TotalApplyQtyInBottle()
                            End If
                        End If
                    End If

                    If column.Name = colLoadInQty Then
                        If grow.Cells(colUOM).Value = "SH" Then
                            AppliedQty = FunTotalApply()
                            PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select isnull(SUM(Item_Qty),0) from TSPL_INDENT_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colItemCode).Value + "' and  Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                            If total1 > PendingQty And Not (clsCommon.CompairString(cmbitemtype.Text, "Empty") = CompairStringResult.Equal) Then
                                common.clsCommon.MyMessageBoxShow(Me, "Shell Qty Cann't more than Pending Qty")
                                grow.Cells(colLoadInQty).Value = "0.00"
                                FunFillEmptyvalue(grow)
                            Else
                                FunFillEmptyvalue(grow)
                            End If
                        Else
                            If total1 > (clsCommon.myCdbl(grow.Cells(colPensingBalanceInBottle).Value) + clsCommon.myCdbl(grow.Cells(colApplyTotalInBottle).Value)) Then
                                common.clsCommon.MyMessageBoxShow(Me, "Load in Qty Cann't more than Pending Qty")
                                grow.Cells(colLoadInQty).Value = "0.00"
                                FunFillEmptyvalue(grow)
                            Else
                                FunFillEmptyvalue(grow)
                            End If
                        End If
                    ElseIf column.Name = colBreakage Then
                        If grow.Cells(colUOM).Value = "SH" Then
                            AppliedQty = FunTotalApply()
                            PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select sum(isnull(Item_Qty ,0)) from TSPL_INDENT_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colItemCode).Value + "' and  Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                            If total1 > PendingQty Then 'And Not (clsCommon.CompairString(cmbitemtype.Text, "Empty") = CompairStringResult.Equal) Then
                                common.clsCommon.MyMessageBoxShow(Me, "Shell Qty Cann't more than Pending Qty", Me.Text)
                                grow.Cells(colBreakage).Value = "0.00"
                                FunFillEmptyvalue(grow)
                            Else
                                FunFillEmptyvalue(grow)
                            End If
                        Else
                            If total1 > (clsCommon.myCdbl(grow.Cells(colPensingBalanceInBottle).Value) + clsCommon.myCdbl(grow.Cells(colApplyTotalInBottle).Value)) Then
                                common.clsCommon.MyMessageBoxShow(Me, "Breakage Qty Cann't more than Pending Qty", Me.Text)
                                grow.Cells(colBreakage).Value = "0.00"
                                FunFillEmptyvalue(grow)
                            End If
                        End If

                    ElseIf column.Name = colLeak Then
                        If grow.Cells(colUOM).Value = "SH" Then
                            PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select sum(isnull(Item_Qty ,0)) from TSPL_INDENT_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colItemCode).Value + "' and  Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                        Else
                            PendingQty = clsCommon.myCdbl(grow.Cells(colPensingBalanceInBottle).Value) + clsCommon.myCdbl(grow.Cells(colApplyTotalInBottle).Value)
                        End If
                        If total1 > PendingQty Then 'And Not (clsCommon.CompairString(cmbitemtype.Text, "Empty") = CompairStringResult.Equal) Then ' clsCommon.myCdbl(grow.Cells(colPensingBalanceInBottle).Value) + clsCommon.myCdbl(grow.Cells(colApplyTotalInBottle).Value) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Leak Qty Cann't more than Pending Qty", Me.Text)
                            grow.Cells(colLeak).Value = "0.00"
                            FunFillEmptyvalue(grow)
                        Else
                            FunFillEmptyvalue(grow)
                        End If
                    ElseIf column.Name = colShortage Then
                        If grow.Cells(colUOM).Value = "SH" Then
                            PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select sum(isnull(Item_Qty ,0)) from TSPL_INDENT_DETAIL where Item_Code='" + gv1.CurrentRow.Cells(colItemCode).Value + "' and  Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                        Else
                            PendingQty = clsCommon.myCdbl(grow.Cells(colPensingBalanceInBottle).Value) + clsCommon.myCdbl(grow.Cells(colApplyTotalInBottle).Value)
                        End If
                        If total1 > PendingQty Then 'And Not (clsCommon.CompairString(cmbitemtype.Text, "Empty") = CompairStringResult.Equal) Then ' clsCommon.myCdbl(grow.Cells(colPensingBalanceInBottle).Value) + clsCommon.myCdbl(grow.Cells(colApplyTotalInBottle).Value) Then
                            common.clsCommon.MyMessageBoxShow(Me, "Shortage Qty Cann't more than Pending Qty", Me.Text)
                            grow.Cells(colShortage).Value = "0.00"
                            FunFillEmptyvalue(grow)

                        End If
                    ElseIf column.Name = colTransferQty Then
                        Dim query As String = "select allow_negative_inv from TSPL_INV_PARAMETERS "
                        Dim check As String = connectSql.RunScalar(query)
                        Dim Sql As String = "SELECT ISNULL(SUM(ISNULL(Item_Qty,0)),0) AS [Item_Qty] FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + clsCommon.myCstr(grow.Cells(colItemCode).Value) + "'AND location_code='" + txtFromLoaction.Value + "' AND MRP='" + (clsCommon.myCdbl(grow.Cells(colMRP).Value)).ToString() + "' "
                        Dim qty As Decimal = clsCommon.myCdbl(connectSql.RunScalar(Sql))
                        'If qty < clsCommon.myCdbl(grow.Cells(colTransferQty).Value) And check = "N" Then
                        '    common.clsCommon.MyMessageBoxShow("You Can't Transfer Item,Your Balance Item Qty'" + clsCommon.myCstr(qty) + "'")
                        '    grow.Cells(colTransferQty).Value = 0.0
                        'Else
                        Dim excisable As String = connectSql.RunScalar("select Excisable  from TSPL_LOCATION_MASTER where Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "'")
                        If excisable = "T" Then
                            grow.Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colTransferQty).Value) * clsCommon.myCdbl(grow.Cells(colBasicPrice).Value), 2)
                        Else
                            grow.Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colTransferQty).Value) * clsCommon.myCdbl(grow.Cells(colMRP).Value), 2)
                        End If
                        If fndTaxGroup.Value = "" Then
                            grow.Cells(colTotal).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAmount).Value), 2)
                        Else
                            grow.Cells(colTotal).Value = Math.Round(clsCommon.myCdbl(grow.Cells(colAmount).Value), 2)
                        End If
                        totalAmounts()
                        'End If
                    End If
                    FunFillTotalAmount()
                    FunFbFcTotal()
                    FunLoadinValue()
                End If

                isCellValueChanged = False
            End If


        Catch ex As Exception
            RadMessageBox.Show(Me, ex.Message, Me.Text)
            isCellValueChanged = False
        End Try


    End Sub

    Private Sub FunLoadinValue()
        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
            txtitemcost.Text = 0.0
            For Each grow As GridViewRowInfo In gv1.Rows
                If Not clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) = 0 Or Not clsCommon.myCdbl(grow.Cells(colBreakage).Value) = 0 Or Not clsCommon.myCdbl(grow.Cells(colLeak).Value) = 0 Or Not clsCommon.myCdbl(grow.Cells(colShortage).Value) = 0 Then
                    If clsCommon.myCdbl(grow.Cells(colConversion).Value) = 1 Then
                        txtitemcost.Text = txtitemcost.Text + Math.Round(clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) * clsCommon.myCdbl(grow.Cells(colItemCost).Value), 2)
                    Else
                        Dim loadinqty As Decimal = Math.Round(clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) / clsCommon.myCdbl(grow.Cells(colConversion).Value), 2)
                        txtitemcost.Text = txtitemcost.Text + Math.Round(loadinqty * clsCommon.myCdbl(grow.Cells(colItemCost).Value), 2)
                    End If
                End If
            Next
        End If
    End Sub

    Private Function FunTotalApply() As Decimal
        Dim TotalApplyAmt As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            If grow.Cells(colUOM).Value = "FC" Then
                If Not clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) = 0 Or Not clsCommon.myCdbl(grow.Cells(colBreakage).Value) = 0 Or Not clsCommon.myCdbl(grow.Cells(colLeak).Value) = 0 Or Not clsCommon.myCdbl(grow.Cells(colShortage).Value) = 0 Then
                    TotalApplyAmt = TotalApplyAmt + Math.Round(grow.Cells(colLoadInQty).Value / grow.Cells(colConversion).Value + grow.Cells(colBreakage).Value / grow.Cells(colConversion).Value + grow.Cells(colShortage).Value / grow.Cells(colConversion).Value + grow.Cells(colLeak).Value / grow.Cells(colConversion).Value, 2)
                End If
            End If
        Next
        Return TotalApplyAmt
    End Function

    Private Sub FunFillTotalAmount()
        Dim ItemAmt, Tax, TotalAmt, NetAmt As Decimal
        NetAmt = 0
        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(grow.Cells(colTransferQty).Value) > 0 Then
                    ItemAmt = ItemAmt + clsCommon.myCdbl(grow.Cells(colAmount).Value)
                    Tax = Tax + clsCommon.myCdbl(grow.Cells(colTax).Value)
                    TotalAmt = TotalAmt + clsCommon.myCdbl(grow.Cells(colTotal).Value)
                End If
            Next
        Else
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) > 0 Or clsCommon.myCdbl(grow.Cells(colLeak).Value) > 0 Then
                    ItemAmt = ItemAmt + clsCommon.myCdbl(grow.Cells(colAmount).Value)
                    Tax = Tax + clsCommon.myCdbl(grow.Cells(colTax).Value)
                    TotalAmt = TotalAmt + clsCommon.myCdbl(grow.Cells(colTotal).Value)
                End If
            Next
        End If
        Dim dblTotalBasicAmt As Double = 0
        For Each row As GridViewRowInfo In gv1.Rows
            Dim dblqty As Double = 0
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                NetAmt = NetAmt + (clsCommon.myCdbl(row.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(row.Cells(colTPT).Value) + clsCommon.myCdbl(row.Cells(colEmptyValue).Value)) * clsCommon.myCdbl(row.Cells(colTransferQty).Value)
                dblqty = clsCommon.myCdbl(row.Cells(colTransferQty).Value)
            ElseIf clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                'If CStr(row.Cells(colUOM).Value) = "FB" Then
                '    NetAmt = NetAmt + ((clsCommon.myCdbl(row.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(row.Cells(colTPT).Value)) * clsCommon.myCdbl(row.Cells(colLoadInQty).Value)) + clsCommon.myCdbl(row.Cells(colEmptyValue).Value)
                'Else
                NetAmt = NetAmt + (Math.Round(clsCommon.myCdbl(row.Cells(colBasicPriceWithTax).Value), 2) + Math.Round(clsCommon.myCdbl(row.Cells(colTPT).Value), 2) + Math.Round(clsCommon.myCdbl(row.Cells(colEmptyValue).Value), 2)) * (Math.Round(clsCommon.myCdbl(row.Cells(colLoadInQty).Value), 2) + Math.Round(clsCommon.myCdbl(row.Cells(colLeak).Value), 2) + Math.Round(clsCommon.myCdbl(row.Cells(colBreakage).Value), 2) + Math.Round(clsCommon.myCdbl(row.Cells(colShortage).Value), 2))
                dblqty = clsCommon.myCdbl(row.Cells(colLoadInQty).Value)
            End If
            row.Cells(colBasicAmt).Value = clsCommon.myCdbl(row.Cells(colBasicPriceWithTax).Value) * dblqty
            dblTotalBasicAmt += clsCommon.myCdbl(row.Cells(colBasicAmt).Value)
        Next
        txtLoadInOutAmt.Text = clsCommon.myFormat(Math.Round(NetAmt, 2))
        txtAmount.Text = ItemAmt
        txtTotalTaxAmt.Text = Tax
        txtTotalTax.Text = clsCommon.myFormat(Tax)
        txtTotalAmount.Text = TotalAmt
        lblTotalBasicAmt.Text = clsCommon.myFormat(dblTotalBasicAmt)
    End Sub

    Private Sub rdbtnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnPost.Click
        PostData()
    End Sub

    Private Sub rdbtndelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtndelete.Click
        DeleteData()
    End Sub

    Public Sub DeleteData()
        Try

            Dim qry As String = "select Post from TSPL_INDENT_HEAD where Indent_No='" + txtTransferNo.Value + "'"
            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qry)) = 1 Then
                clsCommon.MyMessageBoxShow(Me, "Already Posted Transaction", Me.Text)
                Exit Sub
            End If
            If chkIsAutoCreate.Checked Then
                Throw New Exception("Auto Generated Transaction can't be direclty Delete")
            End If
            Dim Reason As String = ""
            If myMessages.deleteConfirm() Then
                If clsCancelLog.CheckForReasonOnDelete() Then
                    '' REASON FOR DELETE 
                    Dim frm As New FrmFreeTxtBox1
                    frm.Text = "Remarks for Delete"
                    frm.ShowDialog()
                    If clsCommon.myLen(frm.strRmks) <= 0 Then
                        Exit Sub
                    Else
                        Reason = frm.strRmks
                    End If
                End If
                FunDelete(Reason)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try

    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtTransferNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function
    Private Sub rdbtnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnreset.Click
        AddNew()
    End Sub

    Private Sub dgvTaxDetails_EditorRequired(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.EditorRequiredEventArgs) Handles gv2.EditorRequired
        If TypeOf gv2.CurrentColumn Is GridViewComboBoxColumn Then
            Dim coltaxrate As GridViewComboBoxColumn = TryCast(gv2.Columns("taxRate"), GridViewComboBoxColumn)
            Dim Sql As String = "select Tax_Rate from TSPL_TAX_RATES WHERE Tax_Code='" + gv2.CurrentRow.Cells("taxAuthority").Value + "' and  Tax_Type = 's'"
            ds = connectSql.RunSQLReturnDS(Sql)
            coltaxrate.ValueMember = "Tax_Rate"
            coltaxrate.DataSource = ds.Tables(0)
        End If
    End Sub

    Private Sub dgvTaxDetails_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv2.CellValueChanged
        Dim grow As GridViewRowInfo = TryCast(e.Row, GridViewRowInfo)
        Dim column As GridViewDataColumn = TryCast(e.Column, GridViewDataColumn)
        If column.Name = "taxRate" Then
            Dim mrp As Decimal = 0.0
            Dim basic As Decimal = 0.0
            Dim netAmount As Decimal = 0.0
            For Each gro As GridViewRowInfo In gv1.Rows
                'mrp = mrp + clsCommon.myCdbl(gro.Cells(colMRP).Value) * clsCommon.myCdbl(gro.Cells(colTransferQty).Value)
                'basic = basic + clsCommon.myCdbl(gro.Cells(colAmount).Value)
                netAmount = netAmount + clsCommon.myCdbl(gro.Cells(colTotal).Value)
                'gro.Cells("taxAmount").Value = Math.Round(calculateItemTax(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colItemCost).Value)), 2)
                'gro.Cells("totalTaxAmount").Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colTax).Value), 2)
                'gro.Cells("totalItemAmount").Value = Math.Round(clsCommon.myCdbl(gro.Cells(colAmount).Value) + clsCommon.myCdbl(gro.Cells("totalTaxAmount").Value), 2)
            Next
            txtTotalAmount.Text = netAmount
            Dim mrp1 As Decimal
            For Each gro As GridViewRowInfo In gv1.Rows
                mrp1 = mrp1 + Math.Round(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colItemCost).Value), 2)
            Next
            calculateTax(mrp1)
            Dim totalTax As Decimal = 0.0
            For Each gr As GridViewRowInfo In gv2.Rows
                totalTax = Math.Round(totalTax + clsCommon.myCdbl(gr.Cells(5).Value), 2)
            Next
            txtTotalTaxAmt.Text = totalTax
            txtTotalTax.Text = clsCommon.myFormat(totalTax)
            totalAmounts()
        End If
        If e.Column.Name = "taxAmount" Then
            Dim totalTax As Decimal = 0.0
            For Each gr As GridViewRowInfo In gv2.Rows
                totalTax = Math.Round(totalTax + clsCommon.myCdbl(gr.Cells(5).Value), 2)
            Next
            txtTotalTaxAmt.Text = totalTax
            txtTotalTax.Text = clsCommon.myFormat(totalTax)
        End If
    End Sub

    Private Sub fndloadno_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '' fndloadno.Query = "select Indent_No as 'Load Out Number',description as 'Description' from TSPL_INDENT_HEAD where (From_Location in (Select location_code from TSPL_LOCATION_MASTER where (Excisable ='F'))) and Post ='Y' order by 'Load Out Number'desc "
        'fndloadno.Query = "select distinct TSPL_INDENT_HEAD .Indent_No as 'Load Out Number',description as 'Description',CONVERT(varchar(15), Indent_Date , 103) as [Transfer Date], From_Location as [From Location], To_Location as [To Location] from TSPL_INDENT_HEAD inner join TSPL_INDENT_DETAIL on TSPL_INDENT_HEAD.Indent_No=TSPL_INDENT_DETAIL.Indent_No  where From_Location in (Select location_code from TSPL_LOCATION_MASTER) and Post ='Y' and Transfer_Type ='LO' and Pending_Qty > 0 AND  TSPL_INDENT_HEAD.Post ='Y' and TSPL_INDENT_HEAD .Indent_No not in(select Load_Out_No from TSPL_INDENT_HEAD  ) "
        'If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
        '    fndloadno.Query += " and To_Location in(" + objCommonVar.strCurrUserLocations + " ) "
        'End If
        'fndloadno.Query += " order by 'Load Out Number' desc "

        'fndloadno.ConnectionString = connectSql.SqlCon()
        'fndloadno.Caption = "Load Out No"
        'fndloadno.ValueToSelect = "Load Out Number"
        'fndloadno.ValueToSelect1 = "Description"
    End Sub

    Public Sub loadOutNo_Text_changed(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim LoadOut As String = connectSql.RunScalar("Select distinct TSPL_INDENT_HEAD.Indent_No from TSPL_INDENT_HEAD inner join TSPL_INDENT_DETAIL on TSPL_INDENT_HEAD.Indent_No=TSPL_INDENT_DETAIL.Indent_No  where TSPL_INDENT_HEAD.Indent_No='" + fndloadno.Value + "' and  Pending_Qty > 0 AND TSPL_INDENT_HEAD.Post ='Y' and TSPL_INDENT_HEAD .Indent_No not in(select Load_Out_No from TSPL_INDENT_HEAD  ) ")
        'If LoadOut IsNot Nothing Then
        '    If rddrplisttransfertype.SelectedIndex = 1 Then
        '        itemfunfill_loadoutno()
        '    ElseIf rddrplisttransfertype.SelectedIndex = 2 Then
        '        itemfunfill_loadoutno()
        '    End If
        'End If
    End Sub

    Private Sub ToLocDesc()
        lblToLocation.Text = connectSql.RunScalar("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtToLocation.Value) + "'")
    End Sub



    Private Sub fndtolocation_Leave_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sql2 As String = "Select Excisable from TSPL_location_master where location_code ='" + txtToLocation.Value.Trim() + "'"
        Dim tolocExcise As String = connectSql.RunScalar(sql2)
        If tolocExcise = "T" And cmbitemtype.SelectedIndex <> 2 Then
            common.clsCommon.MyMessageBoxShow(Me, "To Location Cann't be Excisable", Me.Text)
            txtToLocation.Value = ""
            Exit Sub
        End If
    End Sub

    Private Sub rdbtnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbtnprint.Click
        PrintDataNew(True)
    End Sub





    Private Sub FunCheckEnableDisable()
        If cmbitemtype.SelectedIndex = 1 Then

        ElseIf cmbitemtype.SelectedIndex = 2 Then

        End If
    End Sub

    Private Function UpdateBalanceQtyOfTransferLO(ByVal strTransferNo As String, ByVal trans As SqlTransaction) As Boolean
        If clsCommon.myLen(strTransferNo) > 0 Then
            Dim qry As String = "Update TSPL_INDENT_DETAIL set TSPL_INDENT_DETAIL.Pending_Qty=xxx.BalanceQty ,TSPL_INDENT_DETAIL.Pending_Balance_In_Bottle=ROUND(xxx.BalanceINBottelQty ,0)  from TSPL_INDENT_DETAIL inner Join ("
            qry += " select xxxx.Indent_No,MAX(xxxx.Salesmancode) as Salesmancode,MAX(xxxx.SalemanName) as SalemanName,MAX(xxxx.Route_No) as Route_No,MAX(xxxx.Route_Desc) as Route_Desc,xxxx.Item_Code,Price_Date,sum(xxxx.Item_Qty * case when RI =1 then 1 else case when RI in (2,3,4) then -1 else 0 end end) as BalanceQty,"
            qry += " ROUND((sum(xxxx.Item_Qty * case when RI =1 then 1 else case when RI in (2,3,4) then -1 else 0 end end)*(select Top 1 Conversion_Factor from TSPL_ITEM_UOM_DETAIL where  TSPL_ITEM_UOM_DETAIL.Item_Code=xxxx.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code='FB')),0) as BalanceINBottelQty,"
            qry += " sum(Pending_Qty) as BalanceQtyInDataBase,xxxx.MRP"
            qry += "  from ("
            qry += " select TSPL_INDENT_DETAIL.Indent_No,TSPL_INDENT_DETAIL.Indent_No as DocNo,Item_Code,TSPL_INDENT_DETAIL.Price_Date,Item_Qty,1 as RI,1 as Chk,TSPL_INDENT_DETAIL.Pending_Qty,TSPL_INDENT_HEAD.Salesmancode,TSPL_EMPLOYEE_MASTER.Emp_Name as SalemanName,TSPL_INDENT_HEAD.Route_No,TSPL_INDENT_HEAD.Route_Desc ,MRP"
            qry += " from TSPL_INDENT_DETAIL "
            qry += " left outer join TSPL_INDENT_HEAD on TSPL_INDENT_HEAD.Indent_No=TSPL_INDENT_DETAIL.Indent_No"
            qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_INDENT_HEAD.Salesmancode"
            qry += " where  TSPL_INDENT_HEAD.Transfer_Type='LO' and TSPL_INDENT_HEAD.Post='Y' and  TSPL_INDENT_HEAD.Indent_No='" + strTransferNo + "'"
            qry += " union all "
            qry += " select TSPL_INDENT_HEAD.Load_Out_No as Indent_No ,TSPL_INDENT_HEAD.Indent_No as DocNo,TSPL_INDENT_DETAIL.Item_Code,TSPL_INDENT_DETAIL.Price_Date,(ISNULL( TSPL_INDENT_DETAIL.Burst,0)+isnull(TSPL_INDENT_DETAIL.Leak,0)+isnull(TSPL_INDENT_DETAIL.Shortage,0)+TSPL_INDENT_DETAIL.LoadIn_Qty) /Conversion_Factor  as Item_Qty  ,4 as RI,0 as Chk,0 as Pending_Qty,'' as Salesmancode,'' as SalemanName,'' as Route_No,'' as Route_Desc ,((MRP * Conversion_Factor )+CASE when TSPL_INDENT_DETAIL.Uom='EB' then 100 else 0 end)  AS MRP"
            qry += " from TSPL_INDENT_DETAIL "
            qry += " left outer join TSPL_INDENT_HEAD on TSPL_INDENT_DETAIL.Indent_No=TSPL_INDENT_HEAD.Indent_No"
            qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INDENT_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INDENT_DETAIL.Uom"
            qry += " where Transfer_Type='LI' and TSPL_INDENT_HEAD.Load_Out_No='" + strTransferNo + "'"
            qry += " ) xxxx group by Indent_No,Item_Code,Price_Date,MRP having SUM(chk)>0 "
            qry += " )xxx on xxx.Indent_No=TSPL_INDENT_DETAIL.Indent_No and  xxx.Item_Code=TSPL_INDENT_DETAIL.Item_Code and xxx.Price_Date=TSPL_INDENT_DETAIL.Price_Date  and xxx.MRP=TSPL_INDENT_DETAIL.MRP"
            clsDBFuncationality.ExecuteNonQuery(qry, trans)
        End If
        Return True
    End Function

    Private Sub LoadPendingBalanceAgainstTransfer()
        If clsCommon.myLen(txtLoadoutNo.Value) > 0 Then
            Dim qry As String = "select Indent_No,Item_Code,Price_Date,sum(Item_Qty * case when RI =1 then 1 else case when RI in (2,3,4) then -1 else 0 end end) as BalanceQty from (" + Environment.NewLine
            qry += " select Indent_No,Item_Code,Price_Date,Item_Qty,1 as RI from TSPL_INDENT_DETAIL where Indent_No='" + txtLoadoutNo.Value + "'" + Environment.NewLine
            qry += " union all " + Environment.NewLine
            qry += " select TSPL_SHIPMENT_MASTER.Indent_No as Indent_No,TSPL_SHIPMENT_DETAILS.Item_Code,TSPL_SHIPMENT_DETAILS.Price_Date,TSPL_SHIPMENT_DETAILS.Shipped_Qty /Conversion_Factor as Item_Qty ,2 as RI" + Environment.NewLine
            qry += " from TSPL_SHIPMENT_DETAILS " + Environment.NewLine
            qry += "left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No" + Environment.NewLine
            qry += "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SHIPMENT_DETAILS.Unit_code" + Environment.NewLine
            qry += "where TSPL_SHIPMENT_MASTER.Is_Post='Y' and  TSPL_SHIPMENT_MASTER.Indent_No='" + txtLoadoutNo.Value + "' " + Environment.NewLine
            qry += "union all " + Environment.NewLine
            qry += "select TSPL_SHIPMENT_MASTER.Indent_No as Indent_No,TSPL_SHIPMENT_DETAILS.Item_Code,TSPL_SHIPMENT_DETAILS.Price_Date,TSPL_SHIPMENT_DETAILS.Shipped_Qty /Conversion_Factor   as Item_Qty ,3 as RI" + Environment.NewLine
            qry += " from TSPL_SHIPMENT_DETAILS" + Environment.NewLine
            qry += "left outer join TSPL_SHIPMENT_MASTER on TSPL_SHIPMENT_MASTER.Shipment_No=TSPL_SHIPMENT_DETAILS.Shipment_No" + Environment.NewLine
            qry += "left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_SHIPMENT_DETAILS.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_SHIPMENT_DETAILS.Unit_code" + Environment.NewLine
            qry += "where TSPL_SHIPMENT_MASTER.Is_Post='N' and  TSPL_SHIPMENT_MASTER.Indent_No='" + txtLoadoutNo.Value + "'  and TSPL_SHIPMENT_MASTER.Shipment_No not in('" + txtTransferNo.Value + "') " + Environment.NewLine
            qry += " union all " + Environment.NewLine
            qry += " select TSPL_INDENT_HEAD.Load_Out_No as Indent_No,TSPL_INDENT_DETAIL.Item_Code,TSPL_INDENT_DETAIL.Price_Date,(ISNULL( TSPL_INDENT_DETAIL.Burst,0)+isnull(TSPL_INDENT_DETAIL.Leak,0)+isnull(TSPL_INDENT_DETAIL.Shortage,0)+TSPL_INDENT_DETAIL.LoadIn_Qty) /Conversion_Factor  as Item_Qty  ,4 as RI" + Environment.NewLine
            qry += " from TSPL_INDENT_DETAIL " + Environment.NewLine
            qry += " left outer join TSPL_INDENT_HEAD on TSPL_INDENT_DETAIL.Indent_No=TSPL_INDENT_HEAD.Indent_No" + Environment.NewLine
            qry += " left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INDENT_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code=TSPL_INDENT_DETAIL.Uom" + Environment.NewLine
            qry += "where TSPL_INDENT_HEAD.Load_Out_No='" + txtLoadoutNo.Value + "' and Transfer_Type='LI'  "
            qry += ") xxx group by Indent_No,Item_Code,Price_Date"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    Dim strdtDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(dr("Price_Date")), "dd/MM/yyyy")
                    For ii As Integer = 0 To gv1.RowCount - 1
                        Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)
                        Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUOM).Value)
                        Dim strPriceDate As String = clsCommon.GetPrintDate(clsCommon.myCDate(gv1.Rows(ii).Cells(colPriceDate).Value), "dd/MM/yyyy")
                        If clsCommon.CompairString(strICode, clsCommon.myCstr(dr("Item_Code"))) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strPriceDate, strdtDate) = CompairStringResult.Equal Then
                            gv1.Rows(ii).Cells("ColActualBalQty").Value = Math.Round(clsCommon.myCdbl(dr("BalanceQty")), 2, MidpointRounding.ToEven)
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    Private Sub FunDelete(ByVal Reason As String)
        Dim tr As SqlTransaction = clsDBFuncationality.GetTransactin()
        Try

            Dim qry As String = "delete from TSPL_INDENT_DETAIL where Indent_No='" + txtTransferNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tr)
            qry += "delete from TSPL_INDENT_HEAD where Indent_No='" + txtTransferNo.Value + "'"
            clsDBFuncationality.ExecuteNonQuery(qry, tr)
            saveCancelLog(Reason, "Delete", trans)
            tr.Commit()
            myMessages.delete()

            rdbtndelete.Enabled = False
            rdbtnPost.Enabled = False

            rdbtnprint.Enabled = False
            AddNew()
        Catch ex As Exception
            tr.Rollback()
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub PostData()
        Try
            clsIndentHead.PostData(txtTransferNo.Value)
            common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted", Me.Text)
            LoadData(txtTransferNo.Value)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Function FunReturnLocation(ByVal loc As String, Optional ByVal trans As SqlTransaction = Nothing) As String
        If trans Is Nothing Then
            Return connectSql.RunScalar("select Location_Type  from TSPL_LOCATION_MASTER where Location_Code = '" + loc + "'")
        Else
            Return connectSql.RunScalar(trans, "select Location_Type  from TSPL_LOCATION_MASTER where Location_Code = '" + loc + "'")

        End If
    End Function

    Private Function postTransfer(ByVal trans As SqlTransaction, ByVal strTranferDate As String) As Boolean
        Dim Sql As String
        Dim lineNo As Integer = 1
        Dim fromItemQty As Decimal = 0
        Dim fromCogs As Decimal = 0
        Dim fromUnitCogs As Decimal = 0
        Dim toItemQty As Decimal = 0
        Dim toCogs As Decimal = 0
        Dim toUnitCogs As Decimal = 0
        Dim fromShipmentCogs As Decimal = 0.0
        Dim toShipmentCogs As Decimal = 0.0
        Dim basicAmt As Decimal = 0.0
        Dim frmj As New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        Dim StrVoucher As String = frmj.fnAutoGenerateNo(trans, txtTransferDate.Value, False, txtFromLoaction.Value, False)
        Sql = "SELECT SourceDescription  FROM TSPL_GL_SOURCECODE WHERE SourceCode = 'MM-TF'"
        Dim strSourceDesc As String = connectSql.RunScalar(Sql)
        Dim strInvoiceNo As String = txtTransferNo.Value
        Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER"
        Dim Jrnl As String = CInt(connectSql.RunScalar(strJrnl)) + 1
        Dim dt As String = connectSql.serverDate()
        Dim frmloc As String = FunReturnLocation(txtFromLoaction.Value, trans)
        Dim toloc As String = FunReturnLocation(txtToLocation.Value, trans)
        strTranferDate = txtTransferDate.Value.ToString("dd/MM/yyyy")
        'If frmloc = "Physical" AndAlso toloc = "Physical" Then
        '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "SD-TF"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", Me.rdtxtdescription.Text), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", Me.rdtxtdescription.Text), New SqlParameter("@Comments", Me.rdtxtdescription.Text), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", Me.fndtolocation.Value), New SqlParameter("@CustVend_Name", Me.fndtolocation.Value), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", Me.rdtxttotalamountt.Text), New SqlParameter("@Total_Credit_Amt", Me.rdtxttotalamountt.Text), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
        'ElseIf frmloc = "Physical" AndAlso toloc = "Logical" Then
        '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "SD-TS"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", Me.rdtxtdescription.Text), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", Me.rdtxtdescription.Text), New SqlParameter("@Comments", Me.rdtxtdescription.Text), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", Me.fndtolocation.Value), New SqlParameter("@CustVend_Name", Me.fndtolocation.Value), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", Me.rdtxttotalamountt.Text), New SqlParameter("@Total_Credit_Amt", Me.rdtxttotalamountt.Text), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
        'End If
        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "MM-TF"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", clsCommon.myCstr(Me.txtDescription.Text)), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", clsCommon.myCstr(Me.txtDescription.Text)), New SqlParameter("@Comments", clsCommon.myCstr(Me.txtDescription.Text)), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", Me.txtToLocation.Value), New SqlParameter("@CustVend_Name", Me.txtToLocation.Value), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", Me.txtTotalAmount.Text), New SqlParameter("@Total_Credit_Amt", Me.txtTotalAmount.Text), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

        Dim voucherdesc As String = "Transfer for load out no " + clsCommon.myCstr(txtTransferNo.Value) + " from " + txtFromLoaction.Value + " and to " + txtToLocation.Value + " "
        connectSql.RunSqlTransaction(trans, "update TSPL_JOURNAL_MASTER set Voucher_Desc ='" + voucherdesc + "' where Voucher_No = '" + StrVoucher + "'")
        If FunItemLocationUpdate(trans) Then
            fromShipmentCogs = clsCommon.myCdbl(txtitemcost.Text)
            toShipmentCogs = fromShipmentCogs + clsCommon.myCdbl(txtTotalTaxAmt.Text)
            Dim strFromInvAcc As String = ""
            Dim strFromInvAccDesc As String = ""
            Dim strToInvAcc As String = ""
            Dim strToInvAccDesc As String = ""
            Dim strShpClrAcc As String = ""
            Dim strShpClrAccDesc As String = ""
            Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(txtFromLoaction.Value) + "'"
            Dim fromLocSegCode As String = connectSql.RunScalar(Sql)
            Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(txtToLocation.Value) + "'"
            Dim toLocSegCode As String = connectSql.RunScalar(Sql)
            Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
           " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
            " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + gv1.Rows(0).Cells(colItemCode).Value + "'"
            strFromInvAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), fromLocSegCode)
            strToInvAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), toLocSegCode)
            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
            strFromInvAccDesc = connectSql.RunScalar(Sql)
            If strFromInvAccDesc Is Nothing Then
                Throw New Exception("Inventory Control Account not found.")

            End If
            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToInvAcc + "'"
            strToInvAccDesc = connectSql.RunScalar(trans, Sql)
            If strToInvAccDesc Is Nothing Then
                Throw New Exception("Stock Reserve Account not found." + Environment.NewLine + " Account Code " + strToInvAcc)

            End If
            strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, txtFromLoaction.Value, trans)
            Dim obj As Accountsegment = Accountsegment.Getaccountcodedesc(strFromInvAcc, trans)
            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFromInvAcc), New SqlParameter("@Account_Desc", strFromInvAccDesc), New SqlParameter("@Amount", fromShipmentCogs * (-1)), New SqlParameter("@Description", clsCommon.myCstr(Me.txtDescription.Text)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
            lineNo = lineNo + 1
            Dim taxAmt As Decimal
            Dim ttlTotalTaxAmt As Decimal = 0
            For Each gro As GridViewRowInfo In gv2.Rows
                Dim strTaxCode As String = gro.Cells("taxAuthority").Value
                If gro.Cells("taxAmount").Value.ToString().Substring(0, 1) = "-" Then
                    taxAmt = Math.Round(clsCommon.myCdbl(gro.Cells("taxAmount").Value), 2)
                Else
                    If gro.Cells("taxAmount").Value = 0 Then
                        taxAmt = 0
                    Else
                        taxAmt = Math.Round(clsCommon.myCdbl(gro.Cells("taxAmount").Value), 2)
                    End If
                End If
                Dim strNetPayAcc As String = ""
                Dim strNetPayAccDesc As String = ""
                ' Sql = "SELECT Tax_Net_Payable FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
                Sql = "SELECT Tax_Liability_Account FROM TSPL_TAX_MASTER WHERE Tax_Code = '" + strTaxCode + "'"
                If Not connectSql.RunScalar(Sql).ToString() = "" Then
                    strNetPayAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), fromLocSegCode)
                End If

                If Not strNetPayAcc = "" Then
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strNetPayAcc + "'"
                    strNetPayAccDesc = connectSql.RunScalar(Sql)
                End If
                If Not taxAmt = 0 AndAlso strNetPayAcc <> "" Then
                    strNetPayAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strNetPayAcc, txtFromLoaction.Value, trans)
                    Dim obj1 As Accountsegment = Accountsegment.Getaccountcodedesc(strNetPayAcc, trans)
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strNetPayAcc), New SqlParameter("@Account_Desc", strNetPayAccDesc), New SqlParameter("@Amount", taxAmt * (-1)), New SqlParameter("@Description", clsCommon.myCstr(Me.txtDescription.Text)), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj1.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj1.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj1.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj1.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj1.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj1.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj1.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj1.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj1.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj1.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj1.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj1.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj1.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj1.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj1.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj1.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj1.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj1.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj1.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj1.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                End If
                ttlTotalTaxAmt = ttlTotalTaxAmt + taxAmt
            Next
            fromShipmentCogs = fromShipmentCogs + ttlTotalTaxAmt

            strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, txtToLocation.Value, trans)
            obj = Accountsegment.Getaccountcodedesc(strToInvAcc, trans)
            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToInvAcc), New SqlParameter("@Account_Desc", strToInvAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
            lineNo = lineNo + 1


            '***************By Manoj: To add GL-Entry Transfer Filled and Empty Account
            If cmbitemtype.Text = "Full" Then
                Dim strFrmFilledAcc As String = Nothing
                Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + txtFromLoaction.Value + "'"
                Dim strFrmFilledAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                If strFrmFilledAccFirst Is Nothing Then
                    Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)

                Else
                    strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, txtFromLoaction.Value, False, trans)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                    Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

                    strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, txtFromLoaction.Value, trans)
                    obj = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                End If
                Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + txtToLocation.Value + "'"
                Dim strToFilledAcc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                If strToFilledAcc Is Nothing Then
                    Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)

                Else
                    strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, txtToLocation.Value, False, trans)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                    Dim strTOFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtToLocation.Value + "'")
                    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                    strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, txtToLocation.Value, trans)
                    obj = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs * -1), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                End If
            ElseIf cmbitemtype.Text = "Empty" Then
                Dim strFrmFilledAcc As String = Nothing
                Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + txtFromLoaction.Value + "'"
                Dim strFrmFilledAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                If strFrmFilledAccFirst Is Nothing Then
                    Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)
                Else
                    strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, txtFromLoaction.Value, False, trans)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                    Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                    strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, txtFromLoaction.Value, trans)
                    obj = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                End If
                Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + txtToLocation.Value + "'"
                Dim strToFilledAcc As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                If strToFilledAcc Is Nothing Then
                    Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)
                Else
                    strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, txtToLocation.Value, False, trans)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                    Dim strTOFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtToLocation.Value + "'")
                    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                    strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, txtToLocation.Value, trans)
                    obj = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", fromShipmentCogs * -1), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                End If
            End If
            '*************** End : Manoj

            '************************************* Add Empty Entry
            If cmbitemtype.SelectedIndex <> 2 Then

                Dim EmptyAmt As Decimal = 0
                For Each gro As GridViewRowInfo In gv1.Rows
                    EmptyAmt = EmptyAmt + Math.Round(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colEmptyValue).Value), 2)
                Next

                Dim strFrmPurchaseAcc As String = Nothing
                Dim AccSet As String = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + gv1.Rows(0).Cells(colItemCode).Value + "'"))
                Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                Dim strFrmPurchaseAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                If strFrmPurchaseAccFirst Is Nothing Then
                    Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)

                Else
                    If strFrmPurchaseAccFirst.Length > 4 Then
                        strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                    End If
                    strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmPurchaseAccFirst, txtFromLoaction.Value, False, trans)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                    Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

                    strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, txtFromLoaction.Value, trans)
                    obj = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                End If



                Dim strFrmEmptyAcc As String = Nothing
                Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + txtFromLoaction.Value + "'"
                Dim strFrmEmptyAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                If strFrmEmptyAccFirst Is Nothing Then
                    Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)

                Else
                    strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmEmptyAccFirst, txtFromLoaction.Value, False, trans)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                    Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                    strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, txtFromLoaction.Value, trans)

                    obj = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                End If


                strFrmEmptyAcc = Nothing
                Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + txtFromLoaction.Value + "'"
                strFrmEmptyAccFirst = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                If strFrmEmptyAccFirst Is Nothing Then
                    Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)

                Else
                    strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmEmptyAccFirst, txtToLocation.Value, False, trans)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                    Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                    strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, txtToLocation.Value, trans)
                    obj = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                End If


                strFrmPurchaseAcc = Nothing
                AccSet = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + gv1.Rows(0).Cells(colItemCode).Value + "'"))
                Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                strFrmPurchaseAccFirst = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                If strFrmPurchaseAccFirst Is Nothing Then
                    Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)

                Else
                    If strFrmPurchaseAccFirst.Length > 4 Then
                        strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                    End If
                    strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmPurchaseAccFirst, txtToLocation.Value, False, trans)
                    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                    Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                    strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, txtToLocation.Value, trans)

                    obj = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                    lineNo = lineNo + 1
                End If
                '************* End
            End If

            Sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + StrVoucher + "' "
            connectSql.RunSqlTransaction(trans, Sql)
            Return True
        Else
            trans.Rollback()
            Return False
        End If
    End Function

    Public Sub FillItemDetails(ByVal TransferNo As String)
        LoadBlankGrid()
        Dim sql As String = "SELECT convert(varchar(10),Price_Date,103) as Date , Item_Qty,Assessable_Amt, Amount, Net_Amount, Pending_Qty,LoadIn_Qty,Total_Tax,Total_Item_Amt,uom,breakage,MRP, TSPL_INDENT_DETAIL.Item_Code, TSPL_INDENT_DETAIL.Item_Desc, Basic_Price,Item_Price,TSPL_INDENT_DETAIL.batch_no,BasicPrice_WithTax,Empty_Value,TPT_Value, Burst, Leak , Shortage, (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_INDENT_DETAIL.Item_Code and UOM_Code = TSPL_INDENT_DETAIL.Uom ) as [ConvFactor],mrp_in_bottle ,MRP*(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_INDENT_DETAIL.Item_Code and UOM_Code = TSPL_INDENT_DETAIL.Uom ) AS [ORDERBY],case when UOM ='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end SKuSeq FROM TSPL_INDENT_DETAIL left outer join TSPL_ITEM_MASTER on TSPL_INDENT_DETAIL.Item_Code=TSPL_ITEM_MASTER.Item_Code  where Indent_No='" + TransferNo + "' ORDER BY  SKuSeq ,ORDERBY,Uom desc  "
        Dim dr1 As DataTable
        dr1 = clsDBFuncationality.GetDataTable(sql)
        'Dim row As Integer = 0
        If dr1.Rows.Count > 0 Then
            For Each row As DataRow In dr1.Rows

                Dim grow As GridViewRowInfo = gv1.Rows.AddNew()
                grow.Cells(colPriceDate).Value = row("Date").ToString()
                grow.Cells(colTransferQty).Value = clsCommon.myCdbl(row("Item_Qty"))
                grow.Cells(colAssessableAmt).Value = clsCommon.myCdbl(row("Assessable_Amt"))
                grow.Cells(colAmount).Value = row("Amount").ToString()

                grow.Cells(colPendingQty).Value = row("Pending_Qty").ToString()
                grow.Cells(colLoadInQty).Value = row("LoadIn_Qty").ToString()
                grow.Cells(colBreakage).Value = row("Burst")
                grow.Cells(colLeak).Value = row(colLeak).ToString()
                grow.Cells(colShortage).Value = row(colShortage).ToString()
                grow.Cells(colTax).Value = row("Total_Tax").ToString()
                grow.Cells(colTotal).Value = row("Total_Item_Amt").ToString()
                grow.Cells(colUOM).Value = row("uom").ToString()
                grow.Cells(colMRP).Value = row("MRP").ToString()
                grow.Cells(colMRPInBottel).Value = row("MRP_in_bottle").ToString()
                grow.Cells(colItemCode).Value = row("Item_Code").ToString()
                grow.Cells(colItemName).Value = row("Item_Desc").ToString()
                grow.Cells(colBasicPrice).Value = row("Basic_Price").ToString()
                grow.Cells(colItemCost).Value = row("Item_Price").ToString()
                grow.Cells(colBatchNo).Value = row("batch_no").ToString()
                grow.Cells(colTPT).Value = Convert.ToString(row("TPT_Value"))
                grow.Cells(colEmptyValue).Value = Convert.ToString(row("Empty_Value"))
                grow.Cells(colBasicPriceWithTax).Value = Convert.ToString(row(colBasicPriceWithTax))
                grow.Cells(colConversion).Value = Convert.ToString(row("ConvFactor"))
                grow.Cells(colApplyTotal).Value = Convert.ToString(clsCommon.myCdbl(row("loadin_qty")) / clsCommon.myCdbl(row("convfactor")) + clsCommon.myCdbl(row("Burst")) / clsCommon.myCdbl(row("convfactor")) + clsCommon.myCdbl(row(colShortage)) / clsCommon.myCdbl(row("convfactor")) + clsCommon.myCdbl(row(colLeak)) / clsCommon.myCdbl(row("convfactor")))
            Next
            gv1.CurrentRow = gv1.Rows(0)
        End If
    End Sub

    Public Sub funSetFirstRow()
        If gv1.Rows.Count > 0 Then
            gv1.CurrentRow = gv1.Rows(0)
        End If
    End Sub

    Private Sub FunFillStock()
        Dim StockQty As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            StockQty = clsCommon.myCdbl(connectSql.RunScalar("select SUM(ISNULL(item_qty,0)) from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and Location_Code ='" + Convert.ToString(txtFromLoaction.Value) + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value) + "'"))
            If StockQty > 0 And grow.Cells(colPendingQty).Value < StockQty Then
                grow.Cells(colPendingQty).Value = StockQty
            End If
        Next
    End Sub

    Private Sub FunFillPendingBalanceQty()
        Dim PendingQty, convfactor, PendingQtyInBottle As Decimal
        Dim MrpPrice As Decimal = 0
        For Each grow As GridViewRowInfo In gv1.Rows
            PendingQty = PendingQty + clsCommon.myCdbl(grow.Cells(colPendingQty).Value)
        Next
        For Each grow As GridViewRowInfo In gv1.Rows
            If cmbitemtype.SelectedIndex = 2 Then
                If clsCommon.myCdbl(grow.Cells(colConversion).Value) = 1 Then
                    MrpPrice = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                    PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select ISNULL(Pending_Qty,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(MrpPrice) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                    grow.Cells(colPendingQty).Value = PendingQty
                    PendingQtyInBottle = clsCommon.myCdbl(connectSql.RunScalar("select ISNULL(Pending_Balance_In_Bottle,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(MrpPrice) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                    grow.Cells(colPensingBalanceInBottle).Value = PendingQtyInBottle
                    grow.Cells(colTransferQUANTITY).Value = clsCommon.myCdbl(connectSql.RunScalar("select ISNULL(Item_Qty,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(MrpPrice) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                    PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select ISNULL(Pending_Qty,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                Else
                    MrpPrice = clsCommon.myCdbl(grow.Cells(colMRP).Value) * clsCommon.myCdbl(grow.Cells(colConversion).Value) + 100
                    PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select ISNULL(Pending_Qty,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                    grow.Cells(colPendingQty).Value = PendingQty
                    PendingQtyInBottle = clsCommon.myCdbl(connectSql.RunScalar("select ISNULL(Pending_Balance_In_Bottle,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(MrpPrice) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                    grow.Cells(colPensingBalanceInBottle).Value = PendingQtyInBottle
                    grow.Cells(colTransferQUANTITY).Value = clsCommon.myCdbl(connectSql.RunScalar("select ISNULL(Item_Qty,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(MrpPrice) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                    PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select ISNULL(Pending_Qty,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(MrpPrice) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))

                End If
                If clsCommon.myCdbl(grow.Cells(colConversion).Value) = 1 Then
                    convfactor = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and UOM_Code <> '" + Convert.ToString(grow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
                Else
                    convfactor = clsCommon.myCdbl(grow.Cells(colConversion).Value)
                End If
                grow.Cells(colApplyTotalInBottle).Value = Math.Round(grow.Cells(colTransferQUANTITY).Value * convfactor - PendingQtyInBottle, 2)
            ElseIf cmbitemtype.SelectedIndex = 1 Then
                PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select ISNULL(Pending_Qty,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                grow.Cells(colPendingQty).Value = PendingQty

                PendingQtyInBottle = clsCommon.myCdbl(connectSql.RunScalar("select isnull(Pending_Balance_In_Bottle,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                grow.Cells(colPensingBalanceInBottle).Value = PendingQtyInBottle
                grow.Cells(colTransferQUANTITY).Value = clsCommon.myCdbl(connectSql.RunScalar("select isnull(Item_Qty,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))
                PendingQty = clsCommon.myCdbl(connectSql.RunScalar("select isnull(Pending_Qty,0) from TSPL_INDENT_DETAIL where Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date ='" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and Indent_No = '" + Convert.ToString(txtLoadoutNo.Value) + "'"))

                grow.Cells(colApplyTotal).Value = Math.Round(grow.Cells(colTransferQUANTITY).Value - PendingQty, 2)
                If clsCommon.myCdbl(grow.Cells(colConversion).Value) = 1 Then
                    convfactor = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and UOM_Code <> '" + Convert.ToString(grow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
                Else
                    convfactor = clsCommon.myCdbl(grow.Cells(colConversion).Value)
                End If
                grow.Cells(colApplyTotalInBottle).Value = Math.Round(grow.Cells(colTransferQUANTITY).Value * convfactor - PendingQtyInBottle, 2)
            End If
        Next
    End Sub

    Private Sub funfillofloadinagainstloadout()
        Dim itemcode, itemdate, mrp As String
        Dim pricedate As Date
        Dim sql As String = "SELECT convert(varchar(10),Price_Date,103) as Date , Item_Qty,Assessable_Amt, Amount, Net_Amount, Pending_Qty,LoadIn_Qty,Total_Tax,Total_Item_Amt,uom,breakage,MRP, Item_Code, Item_Desc, Basic_Price,Item_Price,batch_no,BasicPrice_WithTax,Empty_Value,TPT_Value, Burst, Leak , Shortage FROM TSPL_INDENT_DETAIL where Indent_No='" + txtTransferNo.Value + "'"
        Dim drfill As DataTable = clsDBFuncationality.GetDataTable(sql)
        If drfill.Rows.Count > 0 Then
            For Each row As DataRow In drfill.Rows
                itemcode = Convert.ToString(row("Item_Code"))
                mrp = Convert.ToString(row("MRP"))
                pricedate = CDate(row("Date"))
                itemdate = pricedate.ToString("dd-MM-yyyy")
                For Each gr As GridViewRowInfo In gv1.Rows
                    If gr.Cells(colItemCode).Value = itemcode AndAlso gr.Cells(colPriceDate).Value = itemdate AndAlso gr.Cells(colMRP).Value = mrp Then
                        gr.Cells(colLoadInQty).Value = Convert.ToString(row("LoadIn_Qty"))
                        gr.Cells(colBreakage).Value = Convert.ToString(row("Burst"))
                        gr.Cells(colLeak).Value = Convert.ToString(row(colLeak))
                        gr.Cells(colShortage).Value = Convert.ToString(row(colShortage))
                        gr.Cells(colTax).Value = Convert.ToString(row("Total_Tax"))
                        gr.Cells(colAmount).Value = Convert.ToString(row("amount"))
                        gr.Cells(colTotal).Value = Convert.ToString(row("Total_Item_Amt"))
                        gr.Cells(colEmptyValue).Value = Convert.ToString(Math.Round(row("Empty_Value"), 2))
                    End If
                Next
            Next
            'dr.Close()
        End If
    End Sub

    Private Sub FunDisable()
        'If cboTransferType.SelectedIndex > 0 Then
        '    cmbitemtype.Enabled = True
        'Else : cboTransferType.SelectedIndex = 0
        '    cmbitemtype.Enabled = False
        'End If

        txtFromLoaction.Enabled = False
        txtToLocation.Enabled = False
        txtPriceCode.Enabled = False
        txtRouteNo.Enabled = False
        'fndTaxGroup.Enabled = False
        txtVehicleCode.Enabled = False
        txtKMReading.ReadOnly = True
        txtReference.ReadOnly = False
        txtReference.ReadOnly = False
        txtDescription.ReadOnly = False
        gv1.ReadOnly = True
        gv2.ReadOnly = True
        txtTripNo.ReadOnly = False
    End Sub

    Private Sub FunEnable()
        'cmbitemtype.Enabled = True
        txtFromLoaction.Enabled = True
        txtToLocation.Enabled = True
        txtPriceCode.Enabled = True
        txtRouteNo.Enabled = True
        'fndTaxGroup.Enabled = True
        txtVehicleCode.Enabled = True
        txtReference.ReadOnly = False
        txtDescription.ReadOnly = False
        gv1.ReadOnly = False
        gv2.ReadOnly = False
        txtKMReading.ReadOnly = False
        txtReference.ReadOnly = False
        txtTripNo.ReadOnly = False
    End Sub

    Private Sub FunToolTip()

    End Sub

    Private Sub FunLoadToLocationPhysical()
        'fndtolocation.Query = clsERPFuncationality.UserAvailableLocationQuery + "AND LM.Excisable ='F' AND LM.Location_Type ='Physical' "  '"select Location_Desc as [Location Name], location_code as [Location Code],Location_Type as [Location Type]  from TSPL_LOCATION_MASTER  where Excisable ='F'"
        'fndtolocation.ConnectionString = connectSql.SqlCon()
        'fndtolocation.Caption = "To Location"
        'fndtolocation.ValueToSelect = "Code"
        'fndtolocation.ValueToSelect1 = "Location Type"
    End Sub

    Private Sub FunEmpName()
        lblSalesman.Text = connectSql.RunScalar("select Emp_Name  from TSPL_EMPLOYEE_MASTER  where EMP_CODE = '" + Convert.ToString(txtSalesman.Text) + "'")
    End Sub

    Private Sub FunColumnEditable()
        Try
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                gv1.Columns(colTransferQty).ReadOnly = False
                gv1.Columns(colLoadInQty).ReadOnly = True
                gv1.Columns(colBreakage).ReadOnly = True
                gv1.Columns(colLeak).ReadOnly = True
                gv1.Columns(colShortage).ReadOnly = True
            ElseIf clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                gv1.Columns(colTransferQty).ReadOnly = True
                gv1.Columns(colLoadInQty).ReadOnly = False
                gv1.Columns(colBreakage).ReadOnly = False
                gv1.Columns(colLeak).ReadOnly = False
                gv1.Columns(colShortage).ReadOnly = False
            End If
        Catch ex As Exception

        End Try

    End Sub

    Public Sub closeform()
        Me.Close()

    End Sub

    Private Function TotalApplyQtyInBottle() As Decimal
        Dim totalMRP As Decimal = 0
        ' Dim currentindex As Integer
        'Dim pendingqty As Decimal
        'Dim balance As Decimal
        'Dim totalamt As Decimal
        Dim pendingvalue As Decimal = 0
        Dim otherconversion As Decimal = 0
        Dim total As Decimal = 0
        Dim totalloadinqty12 As Decimal = 0
        Dim total1 As Decimal = 0
        Dim ConvFact As Decimal = 0
        If cmbitemtype.Text = "Full" Then
            For Each gr As GridViewRowInfo In gv1.Rows
                If Not clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colBreakage).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colLeak).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colShortage).Value) = 0 Then
                    Dim strconversion As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gr.Cells(colItemCode).Value + "' and UOM_Code = '" + gr.Cells(colUOM).Value + "'"))
                    If strconversion = 1 Then
                        If clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = 1 Then
                            Dim convfactor As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gv1.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code <> '" + Convert.ToString(gv1.CurrentRow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = gr.Cells(colMRP).Value AndAlso gv1.CurrentRow.Cells(colBatchNo).Value = gr.Cells(colBatchNo).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colBreakage).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colLeak).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colShortage).Value) * convfactor
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        Else
                            Dim convfactor As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gv1.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code = '" + Convert.ToString(gv1.CurrentRow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))

                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = gr.Cells(colMRP).Value AndAlso gv1.CurrentRow.Cells(colBatchNo).Value = gr.Cells(colBatchNo).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colBreakage).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colLeak).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colShortage).Value) * convfactor
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        End If
                    Else
                        If clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = 1 Then
                            Dim convfactor As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gv1.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code <> '" + Convert.ToString(gv1.CurrentRow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value / strconversion = gr.Cells(colMRP).Value AndAlso gv1.CurrentRow.Cells(colBatchNo).Value = gr.Cells(colBatchNo).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        Else
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value = gr.Cells(colMRP).Value AndAlso gv1.CurrentRow.Cells(colBatchNo).Value = gr.Cells(colBatchNo).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        End If
                    End If
                End If
            Next
        Else
            For Each gr As GridViewRowInfo In gv1.Rows
                If Not clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colBreakage).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colLeak).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colShortage).Value) = 0 Then
                    Dim strconversion As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gr.Cells(colItemCode).Value + "' and UOM_Code = '" + gr.Cells(colUOM).Value + "'"))
                    If strconversion = 1 Then
                        If clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = 1 Then
                            Dim convfactor As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gv1.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code <> '" + Convert.ToString(gv1.CurrentRow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso (gv1.CurrentRow.Cells(colMRP).Value * clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value)) + 100 = gr.Cells(colMRP).Value Then
                                If clsCommon.CompairString(gr.Cells(colUOM).Value, "SH") = CompairStringResult.Equal Then
                                    total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
                                Else
                                    total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colBreakage).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colLeak).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colShortage).Value) * convfactor
                                End If
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        Else
                            Dim convfactor As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gv1.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code = '" + Convert.ToString(gv1.CurrentRow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))

                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso (gv1.CurrentRow.Cells(colMRP).Value * clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value)) + 100 = gr.Cells(colMRP).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colBreakage).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colLeak).Value) * convfactor + clsCommon.myCdbl(gr.Cells(colShortage).Value) * convfactor
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        End If
                    Else
                        If clsCommon.myCdbl(gv1.CurrentRow.Cells(colConversion).Value) = 1 Then
                            Dim convfactor As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(gv1.CurrentRow.Cells(colItemCode).Value) + "' and UOM_Code <> '" + Convert.ToString(gv1.CurrentRow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value = gr.Cells(colMRP).Value * strconversion + 100 Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        Else
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value = gr.Cells(colMRP).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        End If
                    End If
                End If
            Next
        End If
        Return total1
    End Function


    Private Function TotalApplyQty() As Decimal
        Dim totalMRP As Decimal = 0
        'Dim currentindex As Integer
        'Dim pendingqty As Decimal
        'Dim balance As Decimal
        'Dim totalamt As Decimal
        Dim pendingvalue As Decimal = 0
        Dim otherconversion As Decimal = 0
        Dim total As Decimal = 0
        Dim totalloadinqty12 As Decimal = 0
        Dim total1 As Decimal = 0
        If cmbitemtype.Text = "Full" Then
            For Each gr As GridViewRowInfo In gv1.Rows
                If Not clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colBreakage).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colLeak).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colShortage).Value) = 0 Then
                    Dim strconversion As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gr.Cells(colItemCode).Value + "' and UOM_Code = '" + gr.Cells(colUOM).Value + "'"))
                    If strconversion = 1 Then
                        Dim currentconversion1 As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gv1.CurrentRow.Cells(colItemCode).Value + "' and UOM_Code = '" + gv1.CurrentRow.Cells(colUOM).Value + "'"))
                        If currentconversion1 = 1 Then
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * currentconversion1 = gr.Cells(colMRP).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colBreakage).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colLeak).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colShortage).Value) / strconversion
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        Else
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * currentconversion1 = gr.Cells(colMRP).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        End If
                    Else
                        Dim currentconversion As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gv1.CurrentRow.Cells(colItemCode).Value + "' and UOM_Code = '" + gv1.CurrentRow.Cells(colUOM).Value + "'"))
                        If currentconversion = 1 Then
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value / strconversion = gr.Cells(colMRP).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colBreakage).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colLeak).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colShortage).Value) / strconversion
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        Else
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value = gr.Cells(colMRP).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colBreakage).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colLeak).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colShortage).Value) / strconversion
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        End If
                    End If
                End If
            Next
        Else
            For Each gr As GridViewRowInfo In gv1.Rows
                If Not clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colBreakage).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colLeak).Value) = 0 Or Not clsCommon.myCdbl(gr.Cells(colShortage).Value) = 0 Then
                    Dim strconversion As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gr.Cells(colItemCode).Value + "' and UOM_Code = '" + gr.Cells(colUOM).Value + "'"))
                    If strconversion = 1 Then
                        Dim currentconversion1 As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gv1.CurrentRow.Cells(colItemCode).Value + "' and UOM_Code = '" + gv1.CurrentRow.Cells(colUOM).Value + "'"))
                        If currentconversion1 = 1 Then
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * currentconversion1 = gr.Cells(colMRP).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colBreakage).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colLeak).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colShortage).Value) / strconversion
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        Else
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value * currentconversion1 + 100 = gr.Cells(colMRP).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) + clsCommon.myCdbl(gr.Cells(colBreakage).Value) + clsCommon.myCdbl(gr.Cells(colLeak).Value) + clsCommon.myCdbl(gr.Cells(colShortage).Value)
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        End If
                    Else
                        Dim currentconversion As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + gv1.CurrentRow.Cells(colItemCode).Value + "' and UOM_Code = '" + gv1.CurrentRow.Cells(colUOM).Value + "'"))
                        If currentconversion = 1 Then
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value = gr.Cells(colMRP).Value * strconversion + 100 Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colBreakage).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colLeak).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colShortage).Value) / strconversion
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        Else
                            If gv1.CurrentRow.Cells(colItemCode).Value = gr.Cells(colItemCode).Value AndAlso gv1.CurrentRow.Cells(colPriceDate).Value = gr.Cells(colPriceDate).Value AndAlso gv1.CurrentRow.Cells(colMRP).Value = gr.Cells(colMRP).Value Then
                                total1 = total1 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colBreakage).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colLeak).Value) / strconversion + clsCommon.myCdbl(gr.Cells(colShortage).Value) / strconversion
                                totalloadinqty12 = totalloadinqty12 + clsCommon.myCdbl(gr.Cells(colLoadInQty).Value) / strconversion
                            End If
                        End If
                    End If
                End If
            Next
        End If
        Return total1
    End Function

    Public Sub FunFbFcTotal()
        Dim totalfc As Decimal = 0
        Dim totalfb As Decimal = 0
        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
            For Each g As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(g.Cells(colTransferQty).Value) <> 0 Then
                    If g.Cells(colUOM).Value = "FC" Then
                        totalfc = totalfc + clsCommon.myCdbl(g.Cells(colTransferQty).Value)
                    End If
                    If g.Cells(colUOM).Value = "FB" Then
                        totalfb = totalfb + clsCommon.myCdbl(g.Cells(colTransferQty).Value)
                    End If
                End If
            Next
        Else
            For Each g As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(g.Cells(colLoadInQty).Value) <> 0 Or clsCommon.myCdbl(g.Cells(colBreakage).Value) <> 0 Or clsCommon.myCdbl(g.Cells(colLeak).Value) <> 0 Or clsCommon.myCdbl(g.Cells(colShortage).Value) <> 0 Then
                    If g.Cells(colUOM).Value = "FC" Then
                        totalfc = totalfc + clsCommon.myCdbl(g.Cells(colLoadInQty).Value) + clsCommon.myCdbl(g.Cells(colBreakage).Value) + clsCommon.myCdbl(g.Cells(colLeak).Value) + clsCommon.myCdbl(g.Cells(colShortage).Value)
                    End If
                    If g.Cells(colUOM).Value = "FB" Then
                        totalfb = totalfb + clsCommon.myCdbl(g.Cells(colLoadInQty).Value) + clsCommon.myCdbl(g.Cells(colBreakage).Value) + clsCommon.myCdbl(g.Cells(colLeak).Value) + clsCommon.myCdbl(g.Cells(colShortage).Value)
                    End If
                End If
            Next
        End If
        If totalfc = 0 Then
            lblfb.Text = CStr(totalfb)
            lblfc.Text = 0
        ElseIf totalfb = 0 Then
            lblfc.Text = CStr(totalfc)
            lblfb.Text = 0
        ElseIf totalfb <> 0 And totalfc <> 0 Then
            lblfb.Text = CStr(totalfb)
            lblfc.Text = CStr(totalfc)
        ElseIf totalfb = 0 And totalfc = 0 Then
            lblfc.Text = 0
            lblfb.Text = 0
        End If
    End Sub

    Private Function abatement(ByVal trans As SqlTransaction) As Decimal
        Dim abat As Decimal = 0
        Dim sql As String = "select abatement_percent from tspl_abatement_master where start_date <='" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "' and end_date >= '" + Format(connectSql.myDate(trans), "MM/dd/yyyy") + "'"
        abat = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(sql, trans))
        If abat > 0 Then
            '    abatdr.Read()
            abat = abat
            'abatdr.Close()
        Else
            abat = 60
        End If
        Return abat
    End Function

    Private Function ConversionFactor(ByVal ItemCode As String, ByVal Uom As String, Optional ByVal trans As SqlTransaction = Nothing) As Decimal
        If trans IsNot Nothing Then
            Return clsCommon.myCdbl(connectSql.RunScalar(trans, "select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + ItemCode + "' and UOM_Code = '" + Uom + "' "))
        Else
            Return clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = '" + ItemCode + "' and UOM_Code = '" + Uom + "' "))
        End If
    End Function

    Private Sub priceDateSelectionEMPTY()
        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
            LoadBlankGrid()
            Dim location As String = ""
            Dim lastqty As Decimal = 0
            Dim lastbatchnumber As String = ""
            Dim loc As String
            Dim sql As String
            ' Dim pricedate As Date
            Dim emptyvalue As Decimal = 0
            Dim basicprice As Decimal = 0
            Dim tptcheck As String = "N"
            sql = "SELECT Item_Code,Item_Desc,UOM,Start_Date,Item_Basic_Net,Item_Basic_Price ,Empty_Value_Bottle , Empty_Value_Shell,ISNULL(TAX1_Amt,0) AS TAX1_Amt ,ISNULL(TAX2_Amt,0) AS TAX2_Amt, ISNULL(TAX3_Amt,0) AS TAX3_Amt, ISNULL(TAX4_Amt,0) AS TAX4_Amt, ISNULL(TAX5_Amt,0) AS TAX5_Amt, ISNULL(TAX6_Amt,0) AS TAX6_Amt, ISNULL(TAX7_Amt,0) AS TAX7_Amt,ISNULL(TAX8_Amt,0) AS TAX8_Amt,ISNULL(TAX9_Amt,0) AS TAX9_Amt,ISNULL(TAX10_Amt,0) AS TAX10_Amt  FROM View_TSPL_SHIPMENT_ITEMS  Where Price_Code='" + txtPriceCode.Value + "' and UOM = 'EC' OR UOM = 'EB'   Order By Sku_Seq"
            Dim pdDR As DataTable = clsDBFuncationality.GetDataTable(sql)
            Dim i As Integer = 0
            If pdDR.Rows.Count > 0 Then
                For Each row As DataRow In pdDR.Rows
                    Dim datarowinfo As GridViewRowInfo = gv1.Rows.AddNew()
                    datarowinfo.Cells(colItemCode).Value = row(0).ToString()
                    datarowinfo.Cells(colItemName).Value = row(1).ToString()
                    datarowinfo.Cells(colUOM).Value = row(2).ToString()
                    ' Dim tptdr As SqlDataReader
                    ' Dim tpt As String
                    'tptdr = connectSql.RunSqlReturnDR("SELECT  Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10   FROM TSPL_ITEM_PRICE_MASTER Where Price_Code='" + fndPriceCode.Value + "' and Item_Code = '" + datarowinfo.Cells(colItemCode).Value + "' AND Item_Basic_Net ='" + pdDR("Item_Basic_Net").ToString() + "' AND Item_Basic_Price ='" + pdDR("Item_Basic_Price").ToString() + "'")
                    'If tptdr.HasRows Then
                    '    tptdr.Read()
                    '    For j As Integer = 1 To 10
                    '        Dim Price_Amount As String = "Price_Amount" + j.ToString()
                    '        Dim Price_Comp As String = "Price_Comp" + j.ToString()
                    '        tptcheck = connectSql.RunScalar("select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code = '" + Convert.ToString(tptdr(Price_Comp)) + "'")
                    '        If tptcheck = "Y" Then
                    '            tpt = Convert.ToString(tptdr(Price_Amount))
                    '            Exit For
                    '        End If
                    '    Next
                    'End If
                    Dim qry As String = "SELECT case when PC1.TPT_Type='Y' then ISNULL(Price_Amount1,0) else 0 end"
                    qry += " + case when PC2.TPT_Type='Y' then ISNULL(Price_Amount2,0) else 0 end "
                    qry += " + case when PC3.TPT_Type='Y' then ISNULL(Price_Amount3,0) else 0 end "
                    qry += " + case when PC4.TPT_Type='Y' then ISNULL(Price_Amount4,0) else 0 end "
                    qry += " + case when PC5.TPT_Type='Y' then ISNULL(Price_Amount5,0) else 0 end "
                    qry += " + case when PC6.TPT_Type='Y' then ISNULL(Price_Amount6,0) else 0 end "
                    qry += " + case when PC7.TPT_Type='Y' then ISNULL(Price_Amount7,0) else 0 end "
                    qry += " + case when PC8.TPT_Type='Y' then ISNULL(Price_Amount8,0) else 0 end "
                    qry += " + case when PC9.TPT_Type='Y' then ISNULL(Price_Amount9,0) else 0 end "
                    qry += " + case when PC10.TPT_Type='Y' then ISNULL(Price_Amount10,0) else 0 end "
                    qry += " FROM TSPL_ITEM_PRICE_MASTER "
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC1 on PC1.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp1"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC2 on PC2.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp2"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC3 on PC3.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp3"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC4 on PC4.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp4"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC5 on PC5.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp5"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC6 on PC6.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp6"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC7 on PC7.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp7"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC8 on PC8.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp8"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC9 on PC9.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp9"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC10 on PC10.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp10"
                    qry += " Where Price_Code='" + txtPriceCode.Value + "' and Item_Code = '" + datarowinfo.Cells(colItemCode).Value + "' AND Item_Basic_Net ='" + row("Item_Basic_Net").ToString() + "' AND Item_Basic_Price ='" + row("Item_Basic_Price").ToString() + "'"


                    datarowinfo.Cells(colTPT).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                    emptyvalue = clsCommon.myCdbl(connectSql.RunScalar("SELECT (Empty_Value_Bottle+Empty_Value_Shell) as total  FROM View_TSPL_SHIPMENT_ITEMS  Where Price_Code='" + txtPriceCode.Value + "' and Item_Code = '" + datarowinfo.Cells(colItemCode).Value + "' AND Item_Basic_Net ='" + row("Item_Basic_Net").ToString() + "' AND Item_Basic_Price ='" + row("Item_Basic_Price").ToString() + "' Order By Sku_Seq"))
                    basicprice = clsCommon.myCdbl(connectSql.RunScalar("SELECT (Item_Basic_Price +ISNULL(TAX1_Amt,0) +ISNULL(TAX2_Amt,0)+ ISNULL(TAX3_Amt,0)+ ISNULL(TAX4_Amt,0)+ ISNULL(TAX5_Amt,0)+ ISNULL(TAX6_Amt,0)+ ISNULL(TAX7_Amt,0)+ISNULL(TAX8_Amt,0)+ISNULL(TAX9_Amt,0)+ISNULL(TAX10_Amt,0)) AS TOTAL  FROM View_TSPL_SHIPMENT_ITEMS  Where Price_Code='" + txtPriceCode.Value + "' AND Item_Basic_Net = '" + row("Item_Basic_Net").ToString() + "' AND Item_Basic_Price = '" + row("Item_Basic_Price").ToString() + "' AND Item_Code = '" + datarowinfo.Cells(colItemCode).Value + "' Order By Sku_Seq"))
                    datarowinfo.Cells(colEmptyValue).Value = emptyvalue
                    datarowinfo.Cells(colBasicPriceWithTax).Value = basicprice
                    Dim colPDate As GridViewComboBoxColumn = TryCast(gv1.Columns(colPriceDate), GridViewComboBoxColumn)
                    sql = "Select distinct CONVERT(varchar(10), Start_Date, 103) as Start_Date from TSPL_ITEM_PRICE_MASTER Where item_code='" + datarowinfo.Cells(colItemCode).Value.ToString() + "' and Price_Code ='" + txtPriceCode.Value + "'"
                    ds = connectSql.RunSQLReturnDS(sql)
                    colPDate.ValueMember = "Start_Date"
                    colPDate.DataSource = ds.Tables(0)
                    datarowinfo.Cells(colPriceDate).Value = row(3).ToString()
                    datarowinfo.Cells(colMRP).Value = row(4).ToString()
                    datarowinfo.Cells(colAssessableAmt).Value = Math.Round((datarowinfo.Cells(colMRP).Value * abatement(Nothing) / 100), 2)
                    Dim itemqty As Decimal = 0.0
                    Dim amt As Decimal = 0.0
                    Dim stockqty As Decimal = 0.0
                    Dim itemcost As Decimal = 0.0
                    Dim query As String = "select Indent_No from TSPL_INDENT_DETAIL where Indent_No='" + txtTransferNo.Value + "'and Item_Code='" + datarowinfo.Cells(colItemCode).Value.ToString() + "'"
                    Dim trasnferno As String = connectSql.RunScalar(query)
                    If trasnferno = Nothing Then
                        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                            gv1.Columns(colPendingQty).HeaderText = "Stock Qty"
                            Dim sql2 As String = "Select location_code from TSPL_LOCATION_master where LTRIM(location_desc)='" + txtFromLoaction.Value.Trim() + "'"
                            loc = connectSql.RunScalar(sql2)
                            location = loc
                            sql = "SELECT ISNULL(SUM(ISNULL(ITEM_QTY,0)),0) FROM TSPL_ITEM_LOCATION_DETAILS WHERE Item_Code='" + row(0).ToString() + "' AND  Location_Code='" + loc + "' and MRP='" + datarowinfo.Cells(colMRP).Value.ToString() + "'"
                            stockqty = connectSql.RunScalar(sql)
                            If Not IsDBNull(connectSql.RunScalar(sql)) And connectSql.RunScalar(sql) <> Nothing Then
                                datarowinfo.Cells(colPendingQty).Value = connectSql.RunScalar(sql)
                            Else
                                datarowinfo.Cells(colPendingQty).Value = 0.0
                            End If
                            Dim sql1 As String = "SELECT ISNULL(SUM(ISNULL(Item_Qty,0)),0) AS [Item_Qty],  ISNULL(SUM(ISNULL(Amount,0)),0) AS [Amount] FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + datarowinfo.Cells(colItemCode).Value.ToString() + "' " & _
               " AND location_code='" + loc + "' AND MRP='" + datarowinfo.Cells(colMRP).Value.ToString() + "' "

                            dr = clsDBFuncationality.GetDataTable(sql1)
                            If dr.Rows.Count > 0 Then
                                For Each rowNew As DataRow In dr.Rows
                                    itemqty = clsCommon.myCdbl(rowNew(0).ToString())
                                    amt = clsCommon.myCdbl(rowNew(1).ToString())
                                    If itemqty > 0 Then
                                        itemcost = clsCommon.myCdbl(Math.Round(amt / itemqty, 2))
                                    Else
                                        itemcost = 0
                                    End If
                                Next
                            End If
                        End If
                        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                            gv1.Columns(colPendingQty).HeaderText = "Pending Qty"
                            datarowinfo.Cells(colPendingQty).Value = "0"
                            Dim sql3 As String = "Select to_location from TSPL_INDENT_HEAD where Indent_No='" + txtLoadoutNo.Value + "' "
                            Dim frmlocation1 As String = connectSql.RunScalar(sql3)
                            Dim sql4 As String = "SELECT ISNULL(SUM(ISNULL(Item_Qty,0)),0) AS [Item_Qty],  ISNULL(SUM(ISNULL(Amount,0)),0) AS [Amount] FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + datarowinfo.Cells(colItemCode).Value.ToString() + "' " & _
                                       " AND location_code='" + frmlocation1 + "' AND MRP='" + datarowinfo.Cells(colMRP).Value.ToString() + "'"
                            Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(sql4)
                            If dr1.Rows.Count > 0 Then
                                For Each rowdr1 As DataRow In dr1.Rows
                                    itemqty = clsCommon.myCdbl(rowdr1(0).ToString())
                                    amt = clsCommon.myCdbl(rowdr1(1).ToString())
                                    If itemqty > 0 Then
                                        itemcost = clsCommon.myCdbl(Math.Round(amt / itemqty, 2))
                                    Else
                                        itemcost = 0
                                    End If
                                Next
                            End If
                            'dr1.Close()
                        End If
                    Else
                    End If
                    Dim query1 As String = "select item_price from TSPL_INDENT_DETAIL where Indent_No ='" + txtTransferNo.Value + "'and item_code='" + datarowinfo.Cells(colItemCode).Value.ToString() + "'"
                    Dim cost As Decimal = clsCommon.myCdbl(connectSql.RunScalar(query1))
                    If cost <> 0 Then
                        datarowinfo.Cells(colItemCost).Value = cost
                    Else
                        datarowinfo.Cells(colItemCost).Value = itemcost
                    End If
                    datarowinfo.Cells(colTransferQty).Value = "0"
                    datarowinfo.Cells(colLoadInQty).Value = "0"
                    datarowinfo.Cells(colBreakage).Value = "0"
                    datarowinfo.Cells(colAmount).Value = "0"
                    datarowinfo.Cells(colTax).Value = Math.Round(calculateItemTax(clsCommon.myCdbl(row(4).ToString())), 2)
                    datarowinfo.Cells(colTotal).Value = "0"
                    datarowinfo.Cells(colBasicPrice).Value = Math.Round(clsCommon.myCdbl(row(5).ToString()), 2)
                Next
                'pdDR.Close()
                'gv1.AllowAddNewRow = False
            End If
        End If
    End Sub

    Private Function calculateItemTax(ByVal mrp As Decimal, Optional ByVal ItemType As String = Nothing) As Decimal
        Dim RowCount As Integer = 1
        Dim taxAmt As Decimal = 0
        If gv2.RowCount <> 0 Then
            Dim totalTaxAmt As Decimal = 0.0
            Dim netItemAmount As Decimal = 0.0
            If ItemType = "SM" Then
                taxAmt = Math.Round(mrp * 65 / 100, 2)
            Else
                taxAmt = Math.Round(mrp * abatement(Nothing) / 100, 2)
            End If
            ' Dim taxAmt As Decimal = Math.Round(mrp * abatement() / 100, 2)
            For Each grow As GridViewRowInfo In gv2.Rows

                If grow.Cells("surtax").Value = "N" Then
                    If ItemType = "SM" Then
                        taxAmt = Math.Round(mrp * 65 / 100, 2)
                    Else
                        taxAmt = Math.Round(mrp * abatement(Nothing) / 100, 2)
                    End If
                    grow.Cells("itemAssess").Value = taxAmt
                ElseIf grow.Cells("surtax").Value = "Y" Then
                    Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                    For Each gro As GridViewRowInfo In gv2.Rows
                        If gro.Cells(0).Value = strSurtaxCode Then
                            taxAmt = gro.Cells("itemTaxAmt").Value
                            grow.Cells("itemAssess").Value = taxAmt
                            Exit For
                        End If
                    Next
                End If
                'If grow.Cells("taxable").Value = "N" Then
                '    taxAmt = 0
                'End If
                If ItemType = "SM" Then
                    If RowCount = 1 Then
                        taxAmt = Math.Round(clsCommon.myCdbl(taxAmt) * clsCommon.myCdbl(6 / 100), 2)
                    Else
                        taxAmt = Math.Round(clsCommon.myCdbl(taxAmt) * clsCommon.myCdbl(grow.Cells(2).Value) / 100, 2)
                    End If
                Else
                    taxAmt = Math.Round(clsCommon.myCdbl(taxAmt) * clsCommon.myCdbl(grow.Cells(2).Value) / 100, 2)
                End If

                grow.Cells("itemTaxAmt").Value = taxAmt
                totalTaxAmt = totalTaxAmt + Math.Round(taxAmt, 2)
                RowCount = RowCount + 1
            Next
            Return totalTaxAmt
        Else
            ' common.clsCommon.MyMessageBoxShow("No tax details available.")
            Return 0
        End If
    End Function

    Private Sub calculateTax(ByVal MRP As Decimal, Optional ByVal trans As SqlTransaction = Nothing)
        If gv2.RowCount <> 0 Then
            Dim taxAmt As Decimal
            Dim totalTaxAmt As Decimal = 0.0
            For Each grow As GridViewRowInfo In gv2.Rows
                If grow.Cells("surtax").Value = "N" Then
                    grow.Cells(4).Value = Math.Round(MRP * abatement(trans) / 100, 2)
                ElseIf grow.Cells("surtax").Value = "Y" Then
                    Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                    Dim assess As Decimal = 0
                    For Each gro As GridViewRowInfo In gv2.Rows
                        If gro.Cells(0).Value = strSurtaxCode Then
                            assess = gro.Cells(5).Value
                            Exit For
                        End If
                    Next
                    grow.Cells(4).Value = Math.Round(assess, 2)
                End If
                taxAmt = Math.Round(clsCommon.myCdbl(grow.Cells(4).Value) * clsCommon.myCdbl(grow.Cells(2).Value) / 100, 2)
                grow.Cells(5).Value = taxAmt.ToString()
            Next
        Else
            'common.clsCommon.MyMessageBoxShow("No tax details available.")
        End If
    End Sub

    Private Sub calculateTaxAssessableAmt(ByVal AssAmt As Decimal, ByVal ArrItemRate As List(Of Double))
        If gv2.RowCount <> 0 Then
            Dim taxAmt As Decimal
            Dim totalTaxAmt As Decimal = 0.0
            Dim counter As Integer = 0
            For Each grow As GridViewRowInfo In gv2.Rows
                If grow.Cells("surtax").Value = "N" Then
                    grow.Cells(4).Value = Math.Round(AssAmt, 2)
                ElseIf grow.Cells("surtax").Value = "Y" Then
                    Dim strSurtaxCode As String = grow.Cells("surtaxCode").Value
                    Dim assess As Decimal = 0
                    For Each gro As GridViewRowInfo In gv2.Rows
                        If gro.Cells(0).Value = strSurtaxCode Then
                            assess = gro.Cells(5).Value
                            Exit For
                        End If
                    Next
                    grow.Cells(4).Value = Math.Round(assess, 2)
                End If
                taxAmt = Math.Round(clsCommon.myCdbl(grow.Cells(4).Value) * ArrItemRate(counter) / 100, 2)
                grow.Cells(5).Value = taxAmt.ToString()
                counter += 1
            Next
        End If
    End Sub

    Private Sub totalAmounts()
        Dim mrp As Decimal = 0.0
        Dim netAmount As Decimal = 0.0
        Dim basicAmt As Decimal = 0.0
        Dim totalDiscount As Decimal = 0.0
        Dim totalTax As Decimal = 0.0
        Dim basicprice As Decimal = 0.0
        Dim i As Integer
        Dim totaltax1 As Decimal = 0.0
        txtitemcost.Text = 0.0
        Dim NetAmt As Decimal = 0
        For Each gro As GridViewRowInfo In gv1.Rows
            If clsCommon.myCdbl(gro.Cells(colTransferQty).Value) > 0 Then
                Dim strItemType As String = clsCommon.myCstr(gro.Cells(colItemCode).Value).Substring(0, 2)
                If Not gro.Cells(colTransferQty).Value = 0 AndAlso clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                    gro.Cells(colTax).Value = Math.Round(calculateItemTax(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colMRP).Value), strItemType), 2)
                    totalTax = totalTax + Math.Round(clsCommon.myCdbl(gro.Cells(colTax).Value), 2)
                    txtitemcost.Text = txtitemcost.Text + clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colItemCost).Value)

                ElseIf Not gro.Cells(colPendingQty).Value = 0 AndAlso clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                    gro.Cells(colTax).Value = Math.Round(calculateItemTax(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) - (clsCommon.myCdbl(gro.Cells(colPendingQty).Value)) * clsCommon.myCdbl(gro.Cells(colMRP).Value), strItemType), 2)
                    totalTax = totalTax + Math.Round(clsCommon.myCdbl(gro.Cells(colTax).Value), 2)
                End If
                If gro.Cells(colTax).Value <> 0 Then
                    gro.Cells(colTotal).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTax).Value), 2) + Math.Round(clsCommon.myCdbl(gro.Cells(colAmount).Value), 2)
                End If
                If Not gro.Cells(colTransferQty).Value = 0 AndAlso clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                    mrp = mrp + Math.Round(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colMRP).Value), 2)
                    Dim excisable As String = connectSql.RunScalar("select Excisable  from TSPL_LOCATION_MASTER where Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "'")
                    If excisable = "T" Then
                        gro.Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colBasicPrice).Value), 2)
                    Else
                        gro.Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colMRP).Value), 2)
                    End If
                    basicAmt = basicAmt + gro.Cells(colAmount).Value
                    netAmount = netAmount + Math.Round(clsCommon.myCdbl(gro.Cells(colAmount).Value), 2)
                    basicprice = basicprice + clsCommon.myCdbl(gro.Cells(colBasicPrice).Value) * clsCommon.myCdbl(gro.Cells(colTransferQty).Value)
                ElseIf Not gro.Cells(colPendingQty).Value = 0 AndAlso clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                    mrp = mrp + Math.Round(clsCommon.myCdbl(gro.Cells(colPendingQty).Value) * clsCommon.myCdbl(gro.Cells(colMRP).Value), 2)
                    basicAmt = basicAmt + Math.Round(clsCommon.myCdbl(gro.Cells(colPendingQty).Value) * clsCommon.myCdbl(gro.Cells(colItemCost).Value), 2)
                    netAmount = netAmount + Math.Round(clsCommon.myCdbl(gro.Cells(colAmount).Value), 2)
                End If
            End If
            'NetAmt = NetAmt + (clsCommon.myCdbl(row.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(row.Cells(colTPT).Value) + clsCommon.myCdbl(row.Cells(colEmptyValue).Value)) * clsCommon.myCdbl(row.Cells(colTransferQty).Value)
        Next
        For i = 0 To gv2.Rows.Count - 1
            If Not gv2.Rows(i).Cells("taxAmount").Value Then
                totaltax1 = totaltax1 + Math.Round(clsCommon.myCdbl(gv2.Rows(i).Cells("taxAmount").Value), 2)
            End If
            gv2.Rows(i).Cells("basicAmount").Value = basicprice
        Next

        For Each row As GridViewRowInfo In gv1.Rows
            '**************Edited By Manoj : Change Calculate Transfer Amount Same for both og case like "FB" & "FC"
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                NetAmt = NetAmt + (clsCommon.myCdbl(row.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(row.Cells(colTPT).Value) + clsCommon.myCdbl(row.Cells(colEmptyValue).Value)) * clsCommon.myCdbl(row.Cells(colTransferQty).Value)
            ElseIf clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                'If CStr(row.Cells(colUOM).Value) = "FB" Then
                'NetAmt = NetAmt + (((clsCommon.myCdbl(row.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(row.Cells(colTPT).Value)) * clsCommon.myCdbl(row.Cells(colLoadInQty).Value)) + clsCommon.myCdbl(row.Cells(colEmptyValue).Value))
                'Else
                NetAmt = NetAmt + ((clsCommon.myCdbl(row.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(row.Cells(colTPT).Value) + clsCommon.myCdbl(row.Cells(colEmptyValue).Value)) * (clsCommon.myCdbl(row.Cells(colLoadInQty).Value + Math.Round(clsCommon.myCdbl(row.Cells(colLeak).Value), 2) + Math.Round(clsCommon.myCdbl(row.Cells(colBreakage).Value), 2) + Math.Round(clsCommon.myCdbl(row.Cells(colShortage).Value), 2))))
            End If
            ' End If
            '*********************End By MAnoj
        Next
        txtLoadInOutAmt.Text = clsCommon.myFormat(Math.Round(NetAmt, 2))


        txtAmount.Text = basicAmt

        txtTotalTaxAmt.Text = Math.Round(totaltax1, 2)
        txtTotalTax.Text = clsCommon.myFormat(totaltax1)
        txtTotalAmount.Text = clsCommon.myCdbl(txtAmount.Text) + clsCommon.myCdbl(txtTotalTaxAmt.Text)
        calculateTax(mrp)
    End Sub

    Private Function FunItemLocationUpdate(ByVal trans As SqlTransaction) As Boolean
        Try
            Dim SqlQuery, BatchNumber, flavourcodetem, packcodetem, empname, ItemType As String
            Dim ItemDs, ToItemDs As New DataSet()
            Dim ItemQty, Cogs, UnitCogs, ApplyQty, ShippedQty, ItemLocationQty, Amount As Decimal
            Dim MfgDate, ExpiryDate As String
            Dim transferdate As Date = CDate(txtTransferDate.Value).Date
            BatchNumber = ""
            For Each grow As GridViewRowInfo In gv1.Rows
                If clsCommon.myCdbl(grow.Cells(colTransferQty).Value) > 0 Then
                    flavourcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Flavour Type')")
                    packcodetem = connectSql.RunScalar(trans, "select Class_Code  from TSPL_ITEM_DETAILS where Item_Code= '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and Class_Name = (select inv_class_name from TSPL_INV_CLASS where Class_Type = 'Size Type')")
                    empname = connectSql.RunScalar(trans, "select Emp_Name  from TSPL_EMPLOYEE_MASTER where EMP_CODE = '" + txtSalesman.Text + "'")
                    connectSql.RunSpTransaction(trans, "sp_TEMP_PROVISIONAL_SALES_insert_update_delete", New SqlParameter("@operation", "insert"), New SqlParameter("@transferno", txtTransferNo.Value), New SqlParameter("@transferdate", transferdate), New SqlParameter("@vehiclecode", txtVehicleCode.Value), New SqlParameter("@loadoutlocation", txtFromLoaction.Value), New SqlParameter("@loadinlocation", "null"), New SqlParameter("Salesmancode", txtSalesman.Text), New SqlParameter("@empname", empname), New SqlParameter("@itemcode", grow.Cells(colItemCode).Value), New SqlParameter("@itemdesc", grow.Cells(colItemName).Value), New SqlParameter("@loadoutqty", grow.Cells(colTransferQty).Value), New SqlParameter("@loadinqty", "0"), New SqlParameter("@conversionfactor", grow.Cells(colConversion).Value), New SqlParameter("@loadinno", "null"), New SqlParameter("@breakage", "0"), New SqlParameter("@leak", "0"), New SqlParameter("@mrp", Convert.ToString(grow.Cells(colMRP).Value)), New SqlParameter("@unitcode", Convert.ToString(grow.Cells(colUOM).Value)), New SqlParameter("@vehicleno", txtVehicleCode.Value), New SqlParameter("@packcode", packcodetem), New SqlParameter("@flavourcode", flavourcodetem))
                    ShippedQty = Math.Round(clsCommon.myCdbl(grow.Cells(colTransferQty).Value / grow.Cells(colConversion).Value), 2)
                    SqlQuery = "SELECT Item_Qty, Amount,Batch_No FROM TSPL_ITEM_LOCATION_DETAILS where Item_Qty>0 and  Item_Code='" + grow.Cells(colItemCode).Value + "' " & _
        " AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "'   and MRP='" + (clsCommon.myCdbl(grow.Cells(colMRP).Value) * grow.Cells(colConversion).Value).ToString() + "' order by Expiry_Date asc"
                    ItemDs = connectSql.RunSQLReturnDS(trans, SqlQuery)
                    If ItemDs.Tables(0).Rows.Count > 0 Then
                        For count As Integer = 0 To ItemDs.Tables(0).Rows.Count - 1
                            If Not clsCommon.myCdbl(ItemDs.Tables(0).Rows(count)("Item_Qty")) = 0 And Not clsCommon.myCdbl(ItemDs.Tables(0).Rows(count)("amount")) = 0 Then
                                ItemQty = clsCommon.myCdbl(ItemDs.Tables(0).Rows(count)("Item_Qty"))
                                Cogs = clsCommon.myCdbl(ItemDs.Tables(0).Rows(count)("Amount"))
                                UnitCogs = Math.Round(Cogs / ItemQty, 2)
                                BatchNumber = Convert.ToString(ItemDs.Tables(0).Rows(count)("Batch_No"))
                                If ShippedQty > ItemQty Then
                                    ApplyQty = ItemQty
                                    ShippedQty = ShippedQty - ItemQty
                                Else
                                    ShippedQty = ShippedQty - ItemQty
                                    ApplyQty = (ShippedQty + ItemQty)
                                End If
                                If ShippedQty >= 0 Then
                                    ItemLocationQty = ItemQty - ShippedQty
                                    Amount = Cogs - (UnitCogs * ItemLocationQty)
                                    ItemLocationQty = 0
                                    Amount = 0
                                    Dim tim As String = grow.Cells(colItemCode).Value
                                    SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(ItemLocationQty) + "', " & _
                                        "Amount='" + Convert.ToString(Amount) + "' where Item_Code='" + grow.Cells(colItemCode).Value + "' " & _
                                        " AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "' and MRP='" + (clsCommon.myCdbl(grow.Cells(colMRP).Value) * grow.Cells(colConversion).Value).ToString() + "' and batch_no = '" + BatchNumber + "'"
                                    connectSql.RunSqlTransaction(trans, SqlQuery)
                                Else
                                    ItemLocationQty = ShippedQty * -1
                                    Amount = UnitCogs * ItemLocationQty
                                    ItemLocationQty = Math.Round(ItemLocationQty, 2)
                                    Amount = Math.Round(Amount, 2)
                                    Dim tim As String = grow.Cells(colItemCode).Value
                                    SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(ItemLocationQty) + "', " & _
                                        "Amount='" + Convert.ToString(Amount) + "' where Item_Code='" + grow.Cells(colItemCode).Value + "' " & _
                                        " AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "' and MRP='" + (clsCommon.myCdbl(grow.Cells(colMRP).Value) * grow.Cells(colConversion).Value).ToString() + "' and batch_no = '" + BatchNumber + "'"
                                    connectSql.RunSqlTransaction(trans, SqlQuery)
                                End If
                            End If
                            SqlQuery = "SELECT Item_Qty, Amount FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "' " & _
" AND location_code='" + Convert.ToString(txtToLocation.Value) + "' AND MRP='" + grow.Cells(colMRP).Value.ToString() + "' "
                            ToItemDs.Clear()
                            ToItemDs = connectSql.RunSQLReturnDS(trans, SqlQuery)
                            If ToItemDs.Tables(0).Rows.Count > 0 Then
                                ItemQty = clsCommon.myCdbl(ToItemDs.Tables(0).Rows(0)(0).ToString())
                                Cogs = clsCommon.myCdbl(ToItemDs.Tables(0).Rows(0)(1).ToString())
                                If ItemQty = 0 Then
                                    UnitCogs = 0
                                Else
                                    UnitCogs = Math.Round(Cogs / ItemQty, 2)
                                End If
                                Dim TransferCogs As Decimal = ApplyQty * grow.Cells(colItemCost).Value
                                Dim taxamt As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select  (isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0)+isnull(TAX3_Amt,0)+isnull(TAX4_Amt,0)+isnull(TAX5_Amt,0)+isnull(TAX6_Amt,0)+isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) as [taxamt]  from TSPL_INDENT_DETAIL where Indent_No =   '" + Convert.ToString(txtTransferNo.Value) + "' and Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value) + "' and Price_Date = '" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and Batch_No = '" + Convert.ToString(grow.Cells(colBatchNo).Value) + "'"))
                                taxamt = taxamt / Math.Round(clsCommon.myCdbl(grow.Cells(colTransferQty).Value / grow.Cells(colConversion).Value), 2) * ApplyQty
                                Dim we As String = grow.Cells(colItemCode).Value
                                SqlQuery = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + (ItemQty + clsCommon.myCdbl(ApplyQty)).ToString() + "', " & _
             "Amount='" & (Cogs + TransferCogs + taxamt).ToString() & "' where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "' " & _
             " AND location_code='" + Convert.ToString(txtToLocation.Value) + "' AND MRP='" + grow.Cells(colMRP).Value.ToString() + "' "
                                connectSql.RunSqlTransaction(trans, SqlQuery)
                            Else
                                Dim taxamt As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select  (isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0)+isnull(TAX3_Amt,0)+isnull(TAX4_Amt,0)+isnull(TAX5_Amt,0)+isnull(TAX6_Amt,0)+isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) as [taxamt]  from TSPL_INDENT_DETAIL where Indent_No =   '" + Convert.ToString(txtTransferNo.Value) + "' and Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value) + "' and Price_Date = '" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and Batch_No = '" + Convert.ToString(grow.Cells(colBatchNo).Value) + "'"))
                                taxamt = taxamt / Math.Round(clsCommon.myCdbl(grow.Cells(colTransferQty).Value / grow.Cells(colConversion).Value), 2) * ApplyQty

                                MfgDate = Date.Now.ToString("yyyy-MM-dd")
                                ExpiryDate = Date.Now.ToString("yyyy-MM-dd")
                                ItemType = connectSql.RunScalar(trans, "select ItemType  from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + grow.Cells(colItemCode).Value.ToString() + "' and Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "' and Batch_No = '" + BatchNumber + "'")
                                SqlQuery = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + grow.Cells(colItemCode).Value.ToString() + "','" + grow.Cells(colItemName).Value.ToString() + "','" + Convert.ToString(txtToLocation.Value) + "'," & _
                                " '','" + Convert.ToString(ApplyQty) + "','" + Convert.ToString(ApplyQty * UnitCogs + taxamt) + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','" + grow.Cells(colMRP).Value.ToString() + "','" + MfgDate.ToString() + "','" + BatchNumber + "','" + ExpiryDate.ToString() + "', '" + ItemType + "')"
                                connectSql.RunSqlTransaction(trans, SqlQuery)
                            End If



                            If ShippedQty = 0 Then
                                Exit For
                            End If
                        Next count
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(ex.ToString())
            Return False
        End Try

    End Function

    Private Function postTransferLogical(ByVal trans As SqlTransaction, ByVal strTranferDate As String) As Boolean
        Dim count As Integer = 0
        Dim Sql As String
        Dim lineNo As Integer = 1
        Dim fromItemQty As Decimal = 0
        Dim fromCogs As Decimal = 0
        Dim fromUnitCogs As Decimal = 0
        Dim toItemQty As Decimal = 0
        Dim toCogs As Decimal = 0
        Dim toUnitCogs As Decimal = 0
        Dim TotalAmt As Decimal = 0
        Dim fromShipmentCogs As Decimal = 0
        Dim toShipmentCogs As Decimal = 0
        Dim frmj As New frmJournalEntry(objCommonVar.CurrentUserCode, objCommonVar.CurrentCompanyCode)
        Dim StrVoucher As String = frmj.fnAutoGenerateNo(trans, txtTransferDate.Value, False, txtFromLoaction.Value, False)
        Sql = "SELECT SourceDescription  FROM TSPL_GL_SOURCECODE WHERE SourceCode = 'MM-TF'"
        Dim strSourceDesc As String = connectSql.RunScalar(Sql)
        Dim strInvoiceNo As String = txtTransferNo.Value
        ' Dim tolocation As String
        Dim strJrnl As String = "select (case when max(journal_no) is not null then max(journal_no) else 0 end) from TSPL_JOURNAL_MASTER"
        Dim Jrnl As String = CInt(connectSql.RunScalar(strJrnl)) + 1
        ''Dim dt As String = connectSql.serverDate()
        Dim frmloc As String = FunReturnLocation(txtFromLoaction.Value, trans)
        Dim toloc As String = FunReturnLocation(txtToLocation.Value, trans)
        strTranferDate = txtTransferDate.Value.ToString("dd/MM/yyyy")
        'If frmloc = "Physical" AndAlso toloc = "Physical" AndAlso rddrplisttransfertype.SelectedIndex = 1 Then
        '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "SD-TF"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", Me.rdtxtdescription.Text), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", Me.rdtxtdescription.Text), New SqlParameter("@Comments", Me.rdtxtdescription.Text), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", Me.fndtolocation.Value), New SqlParameter("@CustVend_Name", Me.fndtolocation.Value), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", Me.rdtxttotalamountt.Text), New SqlParameter("@Total_Credit_Amt", rdtxttotalamountt.Text), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

        'ElseIf frmloc = "Physical" AndAlso toloc = "Logical" AndAlso rddrplisttransfertype.SelectedIndex = 1 Then
        '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "SD-TS"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", Me.rdtxtdescription.Text), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", Me.rdtxtdescription.Text), New SqlParameter("@Comments", Me.rdtxtdescription.Text), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", Me.fndtolocation.Value), New SqlParameter("@CustVend_Name", Me.fndtolocation.Value), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", Me.rdtxttotalamountt.Text), New SqlParameter("@Total_Credit_Amt", rdtxttotalamountt.Text), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
        'ElseIf frmloc = "Logical" AndAlso toloc = "Physical" AndAlso rddrplisttransfertype.SelectedIndex = 2 Then
        '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "SD-LS"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", Me.rdtxtdescription.Text), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", Me.rdtxtdescription.Text), New SqlParameter("@Comments", Me.rdtxtdescription.Text), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", Me.fndtolocation.Value), New SqlParameter("@CustVend_Name", Me.fndtolocation.Value), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", Me.rdtxttotalamountt.Text), New SqlParameter("@Total_Credit_Amt", rdtxttotalamountt.Text), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
        'ElseIf frmloc = "Physical" AndAlso toloc = "Physical" AndAlso rddrplisttransfertype.SelectedIndex = 2 Then

        '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "SD-LI"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", Me.rdtxtdescription.Text), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", Me.rdtxtdescription.Text), New SqlParameter("@Comments", Me.rdtxtdescription.Text), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", Me.fndtolocation.Value), New SqlParameter("@CustVend_Name", Me.fndtolocation.Value), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", Me.rdtxttotalamountt.Text), New SqlParameter("@Total_Credit_Amt", rdtxttotalamountt.Text), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

        'End If
        connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_MASTER_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Source_Code", "MM-TF"), New SqlParameter("@Source_Desc", strSourceDesc), New SqlParameter("@Source_Doc_No", strInvoiceNo), New SqlParameter("@Source_Doc_Date", strTranferDate), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Voucher_Desc", Me.txtDescription.Text), New SqlParameter("@Source_Narration", strSourceDesc), New SqlParameter("@Remarks", Me.txtDescription.Text), New SqlParameter("@Comments", Me.txtDescription.Text), New SqlParameter("@Auto_Reverse", "N"), New SqlParameter("@Reverse_Date", strTranferDate), New SqlParameter("@Source_Type", "C"), New SqlParameter("@CustVend_Code", Me.txtToLocation.Value), New SqlParameter("@CustVend_Name", Me.txtToLocation.Value), New SqlParameter("@Transaction_Type", "N"), New SqlParameter("@Total_Debit_Amt", Me.txtTotalAmount.Text), New SqlParameter("@Total_Credit_Amt", txtTotalAmount.Text), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate()), New SqlParameter("@Modify_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modify_Date", connectSql.serverDate()), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))

        Dim voucherdesc As String = "Transfer for load out no " + txtTransferNo.Value + " from " + txtFromLoaction.Value + " and to " + txtToLocation.Value + " "
        connectSql.RunSqlTransaction(trans, "update TSPL_JOURNAL_MASTER set Voucher_Desc ='" + voucherdesc + "' where Voucher_No = '" + StrVoucher + "'")
        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
            If FunItemLocationUpdateLoadIn(trans) Then
                Dim strFromInvAcc As String = ""
                Dim strFromInvAccDesc As String = ""
                Dim strToInvAcc As String = ""
                Dim strToInvAccDesc As String = ""
                Dim strShpClrAcc As String = ""
                Dim strShpClrAccDesc As String = ""
                Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(txtFromLoaction.Value) + "'"
                Dim fromLocSegCode As String = connectSql.RunScalar(trans, Sql)
                Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(txtToLocation.Value) + "'"
                Dim toLocSegCode As String = connectSql.RunScalar(trans, Sql)
                If strexcisable = "F" AndAlso strFromLType = "Logical" Then
                    Sql = "SELECT PA.Reserve_Stock FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
              " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
               " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + gv1.Rows(0).Cells(colItemCode).Value + "'"
                Else
                    Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                                  " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                                   " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + gv1.Rows(0).Cells(colItemCode).Value + "'"
                End If
                strFromInvAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), fromLocSegCode)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
                strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
                If strFromInvAccDesc Is Nothing Then
                    Throw New Exception("Inventory Control Account not found.")

                End If
                If strexcisable = "F" AndAlso strToLType = "Logical" Then
                    Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                     " TSPL_GL_ACCOUNTS AS GLA ON PA.Reserve_Stock = GLA.Account_Code WHERE IM.Item_Code='" + gv1.Rows(0).Cells(colItemCode).Value + "'"
                    strToInvAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), toLocSegCode)
                Else
                    Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                   " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + gv1.Rows(0).Cells(colItemCode).Value + "'"
                    strToInvAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), toLocSegCode)
                End If
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToInvAcc + "'"
                strToInvAccDesc = connectSql.RunScalar(Sql)
                If strToInvAccDesc Is Nothing Then
                    Throw New Exception("Reserve Stock Account not found.")

                End If
                'Dim Qry As String = "select DISTINCT total_item_cost from TSPL_INDENT_DETAIL L inner join TSPL_INDENT_HEAD H on L.Indent_No=H.Indent_No" & _
                '                  " where H.Indent_No  ='" + fndloadno.Value + "'"
                'Dim ItmCost As String = clsCommon.myCstr(connectSql.RunScalar(Qry))
                Dim totItmQty As Decimal = 0
                Dim Uom As String = Nothing
                Dim itmQty As Decimal
                Dim ItmCost As String
                Dim LNo As Integer = 1
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim Qry As String = "select DISTINCT total_item_cost from TSPL_INDENT_DETAIL L inner join TSPL_INDENT_HEAD H on L.Indent_No=H.Indent_No" & _
                                " where H.Indent_No  ='" + txtLoadoutNo.Value + "'  and L.Item_Code ='" + grow.Cells(colItemCode).Value + "' "
                    ItmCost = clsCommon.myCstr(connectSql.RunScalar(Qry))
                    If ItmCost = "" Or ItmCost = Nothing Then
                        ItmCost = clsCommon.myCdbl(grow.Cells(colItemCost).Value)
                    End If
                    itmQty = grow.Cells(colLoadInQty).Value
                    Dim dh As String = grow.Cells(colItemCode).Value
                    Dim CnvrsnFctr As String = connectSql.RunScalar(trans, "select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and UOM_Code = '" + Convert.ToString(grow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'")
                    Uom = CStr(grow.Cells(colUOM).Value)
                    If Uom = "FB" Then
                        itmQty = itmQty / clsCommon.myCdbl(CnvrsnFctr)
                    End If
                    totItmQty = totItmQty + itmQty
                    LNo += 1
                    TotalAmt = TotalAmt + clsCommon.myCdbl(ItmCost) * itmQty
                Next
                ' TotalAmt = clsCommon.myCdbl(ItmCost) * totItmQty

                strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, txtFromLoaction.Value, trans)
                Dim obj3 As Accountsegment = Accountsegment.Getaccountcodedesc(strFromInvAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFromInvAcc), New SqlParameter("@Account_Desc", strFromInvAccDesc), New SqlParameter("@Amount", TotalAmt * (-1)), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj3.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj3.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj3.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj3.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj3.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj3.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj3.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj3.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj3.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj3.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj3.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj3.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj3.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj3.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj3.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj3.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj3.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj3.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj3.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj3.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj3.Account_Seg_Desc10))
                lineNo = lineNo + 1

                strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, txtToLocation.Value, trans)
                Dim obj4 As Accountsegment = Accountsegment.Getaccountcodedesc(strToInvAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToInvAcc), New SqlParameter("@Account_Desc", strToInvAccDesc), New SqlParameter("@Amount", TotalAmt), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj4.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj4.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj4.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj4.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj4.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj4.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj4.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj4.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj4.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj4.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj4.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj4.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj4.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj4.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj4.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj4.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj4.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj4.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj4.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj4.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj4.Account_Seg_Desc10))
                lineNo = lineNo + 1

                '***************By Manoj: To add GL-Entry Transfer Filled and Empty Account
                If Not (strToLType = "Logical" OrElse strFromLType = "Logical") Then
                    If cmbitemtype.Text = "Full" Then
                        Dim strFrmFilledAcc As String = Nothing
                        Dim Loc1 As String = "select From_Location  from TSPL_INDENT_HEAD where Indent_No ='" + txtLoadoutNo.Value + "'"
                        Loc1 = connectSql.RunScalar(trans, Loc1)
                        Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + Loc1 + "'"
                        Dim strFrmFilledAccFirst As String = connectSql.RunScalar(trans, Sql)
                        If strFrmFilledAccFirst Is Nothing Then
                            Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)

                        Else
                            strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, txtFromLoaction.Value, False, trans)
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                            Dim strFilledAccDesc As String = connectSql.RunScalar(Sql)
                            Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                            Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                            strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, txtFromLoaction.Value, trans)
                            Dim obj8 As Accountsegment = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", TotalAmt), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj8.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj8.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj8.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj8.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj8.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj8.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj8.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj8.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj8.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj8.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj8.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj8.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj8.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj8.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj8.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj8.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj8.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj8.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj8.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj8.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj8.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                        Sql = "select Stock_Transfer_Filled_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + txtToLocation.Value + "'"
                        Dim strToFilledAcc As String = connectSql.RunScalar(trans, Sql)
                        If strToFilledAcc Is Nothing Then
                            Throw New Exception("Stock Transfer filled Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)

                        Else
                            strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, txtToLocation.Value, False, trans)
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                            Dim strTOFilledAccDesc As String = connectSql.RunScalar(Sql)
                            Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtToLocation.Value + "'")
                            Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")

                            strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, txtToLocation.Value, trans)
                            Dim obj7 As Accountsegment = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", TotalAmt * -1), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj7.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj7.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj7.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj7.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj7.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj7.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj7.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj7.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj7.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj7.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj7.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj7.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj7.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj7.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj7.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj7.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj7.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj7.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj7.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj7.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj7.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    ElseIf cmbitemtype.Text = "Empty" Then
                        Dim strFrmFilledAcc As String = Nothing
                        Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + txtFromLoaction.Value + "'"
                        Dim strFrmFilledAccFirst As String = connectSql.RunScalar(trans, Sql)
                        If strFrmFilledAccFirst Is Nothing Then
                            Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmFilledAcc)

                        Else
                            strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, txtFromLoaction.Value, False, trans)
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmFilledAcc + "'"
                            Dim strFilledAccDesc As String = connectSql.RunScalar(Sql)
                            Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                            Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                            strFrmFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmFilledAcc, txtFromLoaction.Value, trans)
                            Dim obj8 As Accountsegment = Accountsegment.Getaccountcodedesc(strFrmFilledAcc, trans)
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmFilledAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", TotalAmt), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj8.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj8.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj8.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj8.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj8.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj8.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj8.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj8.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj8.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj8.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj8.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj8.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj8.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj8.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj8.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj8.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj8.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj8.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj8.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj8.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj8.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                        Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + txtToLocation.Value + "'"
                        Dim strToFilledAcc As String = connectSql.RunScalar(trans, Sql)
                        If strToFilledAcc Is Nothing Then
                            Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strToFilledAcc)

                        Else
                            strToFilledAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmFilledAccFirst, txtToLocation.Value, False, trans)
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToFilledAcc + "'"
                            Dim strTOFilledAccDesc As String = connectSql.RunScalar(Sql)
                            Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtToLocation.Value + "'")
                            Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                            strToFilledAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToFilledAcc, txtToLocation.Value, trans)
                            Dim obj7 As Accountsegment = Accountsegment.Getaccountcodedesc(strToFilledAcc, trans)
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToFilledAcc), New SqlParameter("@Account_Desc", strTOFilledAccDesc), New SqlParameter("@Amount", TotalAmt * -1), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj7.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj7.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj7.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj7.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj7.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj7.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj7.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj7.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj7.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj7.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj7.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj7.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj7.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj7.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj7.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj7.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj7.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj7.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj7.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj7.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj7.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                    '*************** End : Manoj
                End If


                '************************************* Add Empty Entry
                If cmbitemtype.SelectedIndex <> 2 Then

                    If Not (strToLType = "Logical" OrElse strFromLType = "Logical") Then
                        Dim obj As Accountsegment
                        Dim EmptyAmt As Decimal = 0
                        For Each gro As GridViewRowInfo In gv1.Rows
                            EmptyAmt = EmptyAmt + Math.Round(clsCommon.myCdbl(gro.Cells(colLoadInQty).Value) * clsCommon.myCdbl(gro.Cells(colEmptyValue).Value), 2)
                        Next
                        Dim Loc2 As String = "select From_Location  from TSPL_INDENT_HEAD where Indent_No ='" + txtLoadoutNo.Value + "'"
                        Loc2 = connectSql.RunScalar(trans, Loc2)
                        Dim strFrmPurchaseAcc As String = Nothing
                        Dim AccSet As String = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + gv1.Rows(0).Cells(colItemCode).Value + "'"))
                        Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                        Dim strFrmPurchaseAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        If strFrmPurchaseAccFirst Is Nothing Then
                            Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)

                        Else
                            If strFrmPurchaseAccFirst.Length > 4 Then
                                strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                            End If
                            strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmPurchaseAccFirst, txtFromLoaction.Value, False, trans)
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                            Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                            Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                            Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                            strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, txtFromLoaction.Value, trans)
                            obj = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If



                        Dim strFrmEmptyAcc As String = Nothing
                        Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + Loc2 + "'"
                        Dim strFrmEmptyAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        If strFrmEmptyAccFirst Is Nothing Then
                            Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)

                        Else
                            strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmEmptyAccFirst, txtFromLoaction.Value, False, trans)
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                            Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                            Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                            Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                            strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, txtFromLoaction.Value, trans)
                            obj = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If


                        strFrmEmptyAcc = Nothing
                        Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + Loc2 + "'"
                        Dim strToEmptyAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        If strFrmEmptyAccFirst Is Nothing Then
                            Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)

                        Else
                            strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strToEmptyAccFirst, txtToLocation.Value, False, trans)
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                            Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                            Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                            Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                            strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmEmptyAcc, txtToLocation.Value, trans)
                            obj = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If


                        strFrmPurchaseAcc = Nothing
                        AccSet = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + gv1.Rows(0).Cells(colItemCode).Value + "'"))
                        Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                        strFrmPurchaseAccFirst = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                        If strFrmPurchaseAccFirst Is Nothing Then
                            Throw New Exception("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)

                        Else
                            If strFrmPurchaseAccFirst.Length > 4 Then
                                strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                            End If
                            strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmPurchaseAccFirst, txtToLocation.Value, False, trans)
                            Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                            Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                            Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'")
                            Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                            strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFrmPurchaseAcc, txtToLocation.Value, trans)
                            obj = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                            connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                            lineNo = lineNo + 1
                        End If
                    End If
                End If
                '************* End
                Sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + StrVoucher + "' "
                connectSql.RunSqlTransaction(trans, Sql)
                Return True
            Else
                Return False
            End If
        Else
            If FunItemLocationUpdate(trans) Then
                Dim strFromInvAcc As String = ""
                Dim strFromInvAccDesc As String = ""
                Dim strToInvAcc As String = ""
                Dim strToInvAccDesc As String = ""
                Dim strShpClrAcc As String = ""
                Dim strShpClrAccDesc As String = ""
                Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(txtFromLoaction.Value) + "'"
                Dim fromLocSegCode As String = connectSql.RunScalar(trans, Sql)
                Sql = "SELECT Loc_Segment_Code from TSPL_LOCATION_MASTER WHERE Location_Code='" + Convert.ToString(txtToLocation.Value) + "'"
                Dim toLocSegCode As String = connectSql.RunScalar(trans, Sql)
                Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
               " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + gv1.Rows(0).Cells(colItemCode).Value + "'"
                strFromInvAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), fromLocSegCode)
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFromInvAcc + "'"
                strFromInvAccDesc = connectSql.RunScalar(trans, Sql)
                If strFromInvAccDesc Is Nothing Then
                    Throw New Exception("Inventory Control Account not found.")

                End If
                If strexcisable = "F" AndAlso strToLType = "Logical" Then
                    Sql = "SELECT PA.Reserve_Stock FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                    " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                     " TSPL_GL_ACCOUNTS AS GLA ON PA.Reserve_Stock = GLA.Account_Code WHERE IM.Item_Code='" + gv1.Rows(0).Cells(colItemCode).Value + "'"
                    strToInvAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), toLocSegCode)
                Else
                    Sql = "SELECT PA.Inv_Control_Account FROM TSPL_ITEM_MASTER AS IM INNER JOIN " & _
                   " TSPL_PURCHASE_ACCOUNTS AS PA ON IM.Purchase_Class_Code = PA.Purchase_Class_Code INNER JOIN " & _
                    " TSPL_GL_ACCOUNTS AS GLA ON PA.Inv_Control_Account = GLA.Account_Code WHERE IM.Item_Code='" + gv1.Rows(0).Cells(colItemCode).Value + "'"
                    strToInvAcc = connectSql.RunScalar(Sql).Replace(connectSql.RunScalar(Sql).ToString().Substring(connectSql.RunScalar(Sql).ToString().Length - 3, 3), toLocSegCode)
                End If
                Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strToInvAcc + "'"
                strToInvAccDesc = connectSql.RunScalar(Sql)
                If strToInvAccDesc Is Nothing Then
                    Throw New Exception("Reserve Stock Account not found.")

                End If

                strFromInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strFromInvAcc, txtFromLoaction.Value, trans)
                Dim obj5 As Accountsegment = Accountsegment.Getaccountcodedesc(strFromInvAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFromInvAcc), New SqlParameter("@Account_Desc", strFromInvAccDesc), New SqlParameter("@Amount", clsCommon.myCdbl(txtitemcost.Text) * (-1)), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj5.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj5.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj5.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj5.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj5.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj5.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj5.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj5.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj5.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj5.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj5.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj5.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj5.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj5.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj5.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj5.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj5.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj5.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj5.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj5.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj5.Account_Seg_Desc10))
                lineNo = lineNo + 1

                strToInvAcc = clsERPFuncationality.ChangeGLAccountLocationSegment(strToInvAcc, txtToLocation.Value, trans)
                Dim obj6 As Accountsegment = Accountsegment.Getaccountcodedesc(strToInvAcc, trans)
                connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strToInvAcc), New SqlParameter("@Account_Desc", strToInvAccDesc), New SqlParameter("@Amount", clsCommon.myCdbl(txtitemcost.Text)), New SqlParameter("@Description", Me.txtDescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", obj6.Account_Group_Code), New SqlParameter("@Account_Seg_Code1", obj6.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj6.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj6.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj6.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj6.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj6.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj6.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj6.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj6.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj6.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj6.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj6.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", obj6.Account_Seg_Code7), New SqlParameter("@Account_Seg_Desc7", obj6.Account_Seg_Desc7), New SqlParameter("@Account_Seg_Code8", obj6.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj6.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj6.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj6.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj6.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj6.Account_Seg_Desc10))
                lineNo = lineNo + 1



                '************************************* Add Empty Entry

                'Dim obj As Accountsegment
                'Dim EmptyAmt As Decimal = 0
                'For Each gro As GridViewRowInfo In dgvitemdetails1.Rows
                '    EmptyAmt = EmptyAmt + Math.Round(clsCommon.myCdbl(gro.Cells(colTransferQty).Value) * clsCommon.myCdbl(gro.Cells(colEmptyValue).Value), 2)
                'Next

                'Dim strFrmPurchaseAcc As String = Nothing
                'Dim AccSet As String = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + dgvitemdetails1.Rows(0).Cells(colItemCode).Value + "'"))
                'Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                'Dim strFrmPurchaseAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                'If strFrmPurchaseAccFirst Is Nothing Then
                '    common.clsCommon.MyMessageBoxShow("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)
                '    trans.Rollback()
                '    connectSql.CloseConnection(connectSql.Connection())
                '    Return False
                'Else
                '    If strFrmPurchaseAccFirst.Length > 4 Then
                '        strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                '    End If
                '    strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmPurchaseAccFirst, fndfromlocation.Value, False)
                '    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                '    Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                '    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + fndfromlocation.Value + "'")
                '    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                '    obj = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", Me.rdtxtdescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", loc), New SqlParameter("@Account_Seg_Desc7", Locdesc), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                '    lineNo = lineNo + 1
                'End If



                'Dim strFrmEmptyAcc As String = Nothing
                'Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + fndfromlocation.Value + "'"
                'Dim strFrmEmptyAccFirst As String = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                'If strFrmEmptyAccFirst Is Nothing Then
                '    common.clsCommon.MyMessageBoxShow("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)
                '    trans.Rollback()
                '    connectSql.CloseConnection(connectSql.Connection())
                '    Return False
                'Else
                '    strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmEmptyAccFirst, fndfromlocation.Value, False)
                '    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                '    Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                '    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + fndfromlocation.Value + "'")
                '    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                '    obj = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", Me.rdtxtdescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", loc), New SqlParameter("@Account_Seg_Desc7", Locdesc), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                '    lineNo = lineNo + 1
                'End If


                'strFrmEmptyAcc = Nothing
                'Sql = "select Stock_Transfer_Empty_Ac  from TSPL_LOCATION_MASTER where Location_Code ='" + fndtolocation.Value + "'"
                'strFrmEmptyAccFirst = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                'If strFrmEmptyAccFirst Is Nothing Then
                '    common.clsCommon.MyMessageBoxShow("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmEmptyAcc)
                '    trans.Rollback()
                '    connectSql.CloseConnection(connectSql.Connection())
                '    Return False
                'Else
                '    strFrmEmptyAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmEmptyAccFirst, fndtolocation.Value, False)
                '    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmEmptyAcc + "'"
                '    Dim strFilledAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                '    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + fndfromlocation.Value + "'")
                '    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                '    obj = Accountsegment.Getaccountcodedesc(strFrmEmptyAcc, trans)
                '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmEmptyAcc), New SqlParameter("@Account_Desc", strFilledAccDesc), New SqlParameter("@Amount", EmptyAmt * -1), New SqlParameter("@Description", Me.rdtxtdescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", loc), New SqlParameter("@Account_Seg_Desc7", Locdesc), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                '    lineNo = lineNo + 1
                'End If


                'strFrmPurchaseAcc = Nothing
                'AccSet = clsCommon.myCstr(connectSql.RunScalar(trans, "select Purchase_Class_Code  from TSPL_ITEM_MASTER where Item_Code ='" + dgvitemdetails1.Rows(0).Cells(colItemCode).Value + "'"))
                'Sql = "select Non_Stock_Clearing  from TSPL_PURCHASE_ACCOUNTS where Purchase_Class_Code ='" + AccSet + "'"
                'strFrmPurchaseAccFirst = clsCommon.myCstr(connectSql.RunScalar(trans, Sql))
                'If strFrmPurchaseAccFirst Is Nothing Then
                '    common.clsCommon.MyMessageBoxShow("Stock Transfer Empty Account not found." + Environment.NewLine + " Account Code " + strFrmPurchaseAccFirst)
                '    trans.Rollback()
                '    connectSql.CloseConnection(connectSql.Connection())
                '    Return False
                'Else
                '    If strFrmPurchaseAccFirst.Length > 4 Then
                '        strFrmPurchaseAccFirst = strFrmPurchaseAccFirst.Substring(0, 4)
                '    End If
                '    strFrmPurchaseAcc = clsERPFuncationality.ChangeGLAccountWithOutLOcSegment(strFrmPurchaseAccFirst, fndtolocation.Value, False)
                '    Sql = "SELECT  Description FROM TSPL_GL_ACCOUNTS WHERE  Account_Code = '" + strFrmPurchaseAcc + "'"
                '    Dim strPurchaseAccDesc As String = clsCommon.myCstr(connectSql.RunScalar(Sql))
                '    Dim loc As String = connectSql.RunScalar("select Loc_Segment_Code  from TSPL_LOCATION_MASTER where Location_Code='" + fndfromlocation.Value + "'")
                '    Dim Locdesc As String = connectSql.RunScalar("select Description  from TSPL_GL_SEGMENT_CODE where Seg_No=7 and Segment_code ='" + loc + "'")
                '    obj = Accountsegment.Getaccountcodedesc(strFrmPurchaseAcc, trans)
                '    connectSql.RunSpTransaction(trans, "sp_TSPL_JOURNAL_DETAILS_INSERT", New SqlParameter("@Journal_No", Jrnl), New SqlParameter("@Voucher_No", StrVoucher), New SqlParameter("@Voucher_Date", strTranferDate), New SqlParameter("@Detail_Line_No", lineNo), New SqlParameter("@Account_code", strFrmPurchaseAcc), New SqlParameter("@Account_Desc", strPurchaseAccDesc), New SqlParameter("@Amount", EmptyAmt), New SqlParameter("@Description", Me.rdtxtdescription.Text), New SqlParameter("@Reference", ""), New SqlParameter("@Posting_Date", strTranferDate), New SqlParameter("@Account_Type", "I"), New SqlParameter("@Account_Group_Code", "Group"), New SqlParameter("@Account_Seg_Code1", obj.Account_Seg_Code1), New SqlParameter("@Account_Seg_Desc1", obj.Account_Seg_Desc1), New SqlParameter("@Account_Seg_Code2", obj.Account_Seg_Code2), New SqlParameter("@Account_Seg_Desc2", obj.Account_Seg_Desc2), New SqlParameter("@Account_Seg_Code3", obj.Account_Seg_Code3), New SqlParameter("@Account_Seg_Desc3", obj.Account_Seg_Desc3), New SqlParameter("@Account_Seg_Code4", obj.Account_Seg_Code4), New SqlParameter("@Account_Seg_Desc4", obj.Account_Seg_Desc4), New SqlParameter("@Account_Seg_Code5", obj.Account_Seg_Code5), New SqlParameter("@Account_Seg_Desc5", obj.Account_Seg_Desc5), New SqlParameter("@Account_Seg_Code6", obj.Account_Seg_Code6), New SqlParameter("@Account_Seg_Desc6", obj.Account_Seg_Desc6), New SqlParameter("@Account_Seg_Code7", loc), New SqlParameter("@Account_Seg_Desc7", Locdesc), New SqlParameter("@Account_Seg_Code8", obj.Account_Seg_Code8), New SqlParameter("@Account_Seg_Desc8", obj.Account_Seg_Desc8), New SqlParameter("@Account_Seg_Code9", obj.Account_Seg_Code9), New SqlParameter("@Account_Seg_Desc9", obj.Account_Seg_Desc9), New SqlParameter("@Account_Seg_Code10", obj.Account_Seg_Code10), New SqlParameter("@Account_Seg_Desc10", obj.Account_Seg_Desc10))
                '    lineNo = lineNo + 1
                'End If

                '************* End





                Sql = "update TSPL_JOURNAL_MASTER SET Authorized= 'A' WHERE Voucher_No='" + StrVoucher + "' "
                connectSql.RunSqlTransaction(trans, Sql)
                Return True
            Else
                Return False
            End If
        End If

    End Function

    Private Function FunItemLocationUpdateLoadIn(ByVal trans As SqlTransaction) As Boolean
        Try
            Dim fromCogs As Decimal = 0
            Dim fromUnitCogs As Decimal = 0
            Dim toItemQty As Decimal = 0
            Dim toCogs As Decimal = 0
            Dim toUnitCogs As Decimal = 0
            Dim fromShipmentCogs As Decimal = 0
            Dim toShipmentCogs As Decimal = 0
            Dim Count As Decimal = 0
            Dim ItemType As String
            Dim Sql As String = String.Empty
            Dim ItemDs As New DataSet()
            Dim itmcost As Decimal
            Dim LoadOutItmCost As Decimal
            Dim mrpprice1, pndqty, totalloadin1 As Decimal
            For Each grow As GridViewRowInfo In gv1.Rows
                If grow.Cells(colUOM).Value <> "SH" Then
                    If clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) > 0 Or clsCommon.myCdbl(grow.Cells(colBreakage).Value) > 0 Or clsCommon.myCdbl(grow.Cells(colShortage).Value) > 0 Or clsCommon.myCdbl(grow.Cells(colLeak).Value) > 0 Then
                        Count = Count + 1
                        If cmbitemtype.Text = "Full" Then
                            mrpprice1 = clsCommon.myCdbl(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value)
                            Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "' " & _
        " AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "' AND MRP='" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and batch_no = '" + grow.Cells(colBatchNo).Value + "' "
                            LoadOutItmCost = connectSql.RunScalar(trans, " select  Total_Item_Cost from TSPL_INDENT_DETAIL where Indent_No ='" + txtLoadoutNo.Value + "' and Item_Code ='" + grow.Cells(colItemCode).Value.ToString() + "'" & _
                                 " AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + grow.Cells(colBatchNo).Value + "' ")
                            pndqty = connectSql.RunScalar(trans, "select Pending_qty from TSPL_INDENT_DETAIL where Indent_No ='" + txtLoadoutNo.Value + "'and Item_Code ='" + grow.Cells(colItemCode).Value + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "'")
                        Else
                            If clsCommon.myCdbl(grow.Cells(colConversion).Value) <> 1 Then
                                mrpprice1 = clsCommon.myCdbl(clsCommon.myCdbl(grow.Cells(colMRP).Value) * grow.Cells(colConversion).Value) + 100
                                pndqty = connectSql.RunScalar(trans, "select Pending_qty from TSPL_INDENT_DETAIL where Indent_No ='" + txtLoadoutNo.Value + "'and Item_Code ='" + grow.Cells(colItemCode).Value + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "'")
                            Else
                                mrpprice1 = clsCommon.myCdbl(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value)
                                pndqty = connectSql.RunScalar(trans, "select Pending_qty from TSPL_INDENT_DETAIL where Indent_No ='" + txtLoadoutNo.Value + "'and Item_Code ='" + grow.Cells(colItemCode).Value + "'and MRP='" + Convert.ToString(mrpprice1) + "' and price_date = '" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "'")

                            End If
                            LoadOutItmCost = connectSql.RunScalar(trans, " select  Total_Item_Cost from TSPL_INDENT_DETAIL where Indent_No ='" + txtLoadoutNo.Value + "' and Item_Code ='" + grow.Cells(colItemCode).Value.ToString() + "'" & _
                                  " AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + grow.Cells(colBatchNo).Value + "' ")
                            Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "' " & _
                             " AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "' AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + grow.Cells(colBatchNo).Value + "' "

                        End If
                        ItemDs.Clear()
                        ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                        ' totalloadin1 = Math.Round(grow.Cells(colLoadInQty).Value / grow.Cells(colConversion).Value + grow.Cells(colBreakage).Value / grow.Cells(colConversion).Value + grow.Cells(colShortage).Value / grow.Cells(colConversion).Value, 2)
                        totalloadin1 = Math.Round(grow.Cells(colLoadInQty).Value / grow.Cells(colConversion).Value, 2)
                        'Dim BalanceQty As Decimal = pndqty - totalloadin1 - Math.Round(grow.Cells(colLeak).Value / grow.Cells(colConversion).Value, 2)
                        'connectSql.RunSqlTransaction(trans, "update TSPL_INDENT_DETAIL set pending_qty='" + Convert.ToString(BalanceQty) + "' where Indent_No='" + fndloadno.Value + "' and item_code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and price_date = '" + CDate(grow.Cells(colPriceDate).Value).ToString("yyyy-MM-dd") + "' and MRP='" + Convert.ToString(mrpprice1) + "'")
                        Dim loadintem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select loadinqty  from TEMP_PROVISIONAL_SALES where Indent_No='" + txtLoadoutNo.Value + "'  and item_code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and mrp = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "'"))
                        Dim breakagetem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select  breakage  from TEMP_PROVISIONAL_SALES where Indent_No='" + txtLoadoutNo.Value + "'  and item_code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and mrp = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "'"))
                        Dim leaktem As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select leak from TEMP_PROVISIONAL_SALES where Indent_No='" + txtLoadoutNo.Value + "'  and item_code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and mrp = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "'"))
                        Dim Amount As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select isnull(Amount,0) from TEMP_PROVISIONAL_SALES where Indent_No='" + txtLoadoutNo.Value + "'  and item_code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and mrp = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "'"))
                        Dim Shortage As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select Shortage from TEMP_PROVISIONAL_SALES where Indent_No='" + txtLoadoutNo.Value + "'  and item_code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and mrp = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "'"))
                        loadintem = Math.Round(loadintem + grow.Cells(colLoadInQty).Value / grow.Cells(colConversion).Value, 2)
                        breakagetem = Math.Round(breakagetem + grow.Cells(colBreakage).Value / grow.Cells(colConversion).Value, 2)
                        leaktem = Math.Round(leaktem + grow.Cells(colLeak).Value / grow.Cells(colConversion).Value, 2)
                        Shortage = Math.Round(Shortage + grow.Cells(colShortage).Value / grow.Cells(colConversion).Value, 2)
                        Amount = Math.Round(Amount + (clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) + clsCommon.myCdbl(grow.Cells(colBreakage).Value) + clsCommon.myCdbl(grow.Cells(colLeak).Value) + clsCommon.myCdbl(grow.Cells(colShortage).Value)) * (clsCommon.myCdbl(grow.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(grow.Cells(colTPT).Value) + clsCommon.myCdbl(grow.Cells(colEmptyValue).Value)), 2)
                        connectSql.RunSqlTransaction(trans, "update TEMP_PROVISIONAL_SALES set Shortage=" + Shortage.ToString() + ", Amount=" + Amount.ToString() + ", LoadInQty = '" + Convert.ToString(loadintem) + "', Breakage ='" + Convert.ToString(breakagetem) + "', Leak = '" + Convert.ToString(leaktem) + "' where Indent_No = '" + txtLoadoutNo.Value + "' and Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and MRP = '" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "'")
                        Dim absvalue As Decimal = 0
                        If ItemDs.Tables(0).Rows.Count > 0 Then
                            '************Edited By: Manoj for Update Item Cost On Location Detail
                            Dim amt As Decimal = clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Amount"))
                            Dim qty As Decimal = totalloadin1
                            'itmcost = clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Amount")) / clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Item_Qty"))
                            itmcost = LoadOutItmCost
                            'Dim newAmt As Decimal = amt - (qty * itmcost)
                            Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                       "Amount='" + (clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Amount")) - (totalloadin1 * itmcost).ToString()).ToString() + "' where Item_Code='" + grow.Cells(colItemCode).Value + "' " & _
                           " AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "' AND MRP='" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "

                            '*******************************

                            '  before to manoj    Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                            '"Amount='" + (clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Amount")) - (totalloadin1 * grow.Cells(colItemCost).Value).ToString()).ToString() + "' where Item_Code='" + grow.Cells(colItemCode).Value + "' " & _
                            '    " AND location_code='" + Convert.ToString(fndfromlocation.Value) + "' AND MRP='" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "
                            connectSql.RunSqlTransaction(trans, Sql)
                        Else
                            ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + grow.Cells(colItemCode).Value.ToString() + "' and Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "' and Batch_No = '" + Convert.ToString(grow.Cells(colBatchNo).Value) + "'")
                            Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + grow.Cells(colItemCode).Value + "','" + grow.Cells(colItemName).Value + "','" + Convert.ToString(txtFromLoaction.Value) + "'," & _
                           " '','" + totalloadin1.ToString() + "','" + (totalloadin1 * grow.Cells(colItemCost).Value).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(grow.Cells(colBatchNo).Value) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', 'FM')"
                            connectSql.RunSqlTransaction(trans, Sql)
                        End If
                        If cmbitemtype.Text = "Full" Then
                            Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "' " & _
        " AND location_code='" + Convert.ToString(txtToLocation.Value) + "' AND MRP='" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and batch_no = '" + grow.Cells(colBatchNo).Value + "' "
                        Else
                            If clsCommon.myCdbl(grow.Cells(colConversion).Value) <> 1 Then
                                mrpprice1 = clsCommon.myCdbl(clsCommon.myCdbl(grow.Cells(colMRP).Value) * clsCommon.myCdbl(grow.Cells(colConversion).Value)) + 100
                            Else
                                mrpprice1 = clsCommon.myCdbl(grow.Cells(colMRP).Value * clsCommon.myCdbl(grow.Cells(colConversion).Value))
                            End If
                            Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "' " & _
        " AND location_code='" + Convert.ToString(txtToLocation.Value) + "' AND MRP='" + Convert.ToString(mrpprice1) + "' and batch_no = '" + grow.Cells(colBatchNo).Value + "' "
                        End If
                        ItemDs.Clear()
                        ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                        'Dim applyqty As Decimal = grow.Cells(colLoadInQty).Value / grow.Cells(colConversion).Value + grow.Cells(colBreakage).Value / grow.Cells(colConversion).Value + grow.Cells(colShortage).Value / grow.Cells(colConversion).Value
                        Dim applyqty As Decimal = grow.Cells(colLoadInQty).Value / grow.Cells(colConversion).Value
                        ' applyqty = applyqty * grow.Cells(colItemCost).Value
                        If ItemDs.Tables(0).Rows.Count > 0 Then
                            '********** Edited By Manoj For Updation Into Item Location Deatil
                            applyqty = applyqty * itmcost
                            Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                                    "Amount='" + Convert.ToString(clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Amount")) + applyqty) + "' where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "' " & _
                                        " AND location_code='" + Convert.ToString(txtToLocation.Value) + "' AND MRP='" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "
                            connectSql.RunSqlTransaction(trans, Sql)

                            '************* End Manoj

                            'Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                            '         "Amount='" + Convert.ToString(clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Amount")) + applyqty) + "' where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "' " & _
                            '             " AND location_code='" + Convert.ToString(fndtolocation.Value) + "' AND MRP='" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "

                        Else
                            ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + grow.Cells(colItemCode).Value.ToString() + "' and Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "' and Batch_No = '" + Convert.ToString(grow.Cells(colBatchNo).Value) + "'")

                            '****** Edited By Manoj For Updation Into Item Location Deatil
                            Dim itmc As Decimal = grow.Cells(colItemCost).Value
                            ' Dim newc As Decimal
                            ' Dim itmcost1 As Decimal = clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Amount")) / clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Item_Qty"))
                            Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + grow.Cells(colItemCode).Value + "','" + grow.Cells(colItemName).Value + "','" + Convert.ToString(txtToLocation.Value) + "'," & _
                           " '','" + totalloadin1.ToString() + "','" + (totalloadin1 * Math.Round(itmcost, 2)).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(grow.Cells(colBatchNo).Value) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', '" + ItemType + "')"

                            '************************************************
                            'Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + grow.Cells(colItemCode).Value + "','" + grow.Cells(colItemName).Value + "','" + Convert.ToString(fndtolocation.Value) + "'," & _
                            '" '','" + totalloadin1.ToString() + "','" + (totalloadin1 * grow.Cells(colItemCost).Value).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(grow.Cells(colBatchNo).Value) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', '" + ItemType + "')"
                            connectSql.RunSqlTransaction(trans, Sql)

                        End If
                        Dim S As String = clsCommon.myCdbl(grow.Cells(colLeak).Value)
                        If clsCommon.myCdbl(grow.Cells(colLeak).Value) > 0 Then
                            Dim fathercode As String = connectSql.RunScalar(trans, "SELECT Father_Code  FROM TSPL_ITEM_MASTER WHERE Item_Code = '" + Convert.ToString(grow.Cells(colItemCode).Value) + "'")
                            Dim fathedesc As String = connectSql.RunScalar(trans, "select Item_Desc from tspl_item_master where item_code= '" + fathercode + "'")
                            If fathercode = "NIL" Then
                                Dim strmessage As String = "Father Code not defined " + Environment.NewLine
                                strmessage += "for this Item " + Convert.ToString(grow.Cells(colItemCode).Value)
                                common.clsCommon.MyMessageBoxShow(strmessage)
                                trans.Rollback()
                                Return False
                            End If
                            Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(grow.Cells(colItemCode).Value) + "' " & _
                          " AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "' AND MRP='" + Convert.ToString(grow.Cells(colMRP).Value * grow.Cells(colConversion).Value) + "' and batch_no = '" + grow.Cells(colBatchNo).Value + "' "
                            ItemDs.Clear()
                            ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                            totalloadin1 = Math.Round(grow.Cells(colLeak).Value / grow.Cells(colConversion).Value, 2)
                            If ItemDs.Tables(0).Rows.Count > 0 Then
                                Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Item_qty")) - totalloadin1) + "', " & _
                            "Amount='" + (clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Amount")) - (totalloadin1 * grow.Cells(colItemCost).Value).ToString()).ToString() + "' where Item_Code='" + Convert.ToString(grow.Cells(colItemCode).Value) + "' " & _
                                " AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "' AND MRP='" + grow.Cells(colMRP).Value.ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "'  "
                                connectSql.RunSqlTransaction(trans, Sql)
                            Else
                                ItemType = connectSql.RunScalar(trans, "select ItemType   from TSPL_ITEM_LOCATION_DETAILS where Item_Code = '" + grow.Cells(colItemCode).Value.ToString() + "' and Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "' and Batch_No = '" + Convert.ToString(grow.Cells(colBatchNo).Value) + "'")
                                Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + Convert.ToString(grow.Cells(colItemCode).Value) + "','" + grow.Cells(colItemName).Value + "','" + Convert.ToString(txtFromLoaction.Value) + "'," & _
                               " '','" + grow.Cells(colTransferQty).Value.ToString() + "','" + (grow.Cells(colTransferQty).Value * grow.Cells(colItemCost).Value).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','" + grow.Cells(colMRP).Value.ToString() + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(grow.Cells(colBatchNo).Value) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', '" + ItemType + "')"
                                connectSql.RunSqlTransaction(trans, Sql)
                            End If
                            Sql = "SELECT Item_Qty, Amount, batch_no FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                     " AND location_code='" + Convert.ToString(txtToLocation.Value) + "' AND MRP='" + clsCommon.myCdbl(grow.Cells(colMRP).Value * clsCommon.myCdbl(grow.Cells(colConversion).Value)).ToString() + "' and batch_no = '" + Convert.ToString(grow.Cells(colBatchNo).Value) + "'"
                            ItemDs.Clear()
                            ItemDs = connectSql.RunSQLReturnDS(trans, Sql)
                            If ItemDs.Tables(0).Rows.Count > 0 Then
                                Sql = "UPDATE TSPL_ITEM_LOCATION_DETAILS SET Item_Qty='" + Convert.ToString(clsCommon.myCdbl(ItemDs.Tables(0).Rows(0)("Item_qty")) + totalloadin1) + "', " & _
                                         "Amount='" + (toCogs + clsCommon.myCdbl(grow.Cells(colTotal).Value.ToString())).ToString() + "' where Item_Code='" + Convert.ToString(fathercode) + "' " & _
                                             " AND location_code='" + Convert.ToString(txtToLocation.Value) + "' AND MRP='" + grow.Cells(colMRP).Value.ToString() + "' and batch_no = '" + ItemDs.Tables(0).Rows(0)("batch_no").ToString() + "' " ' "
                                connectSql.RunSqlTransaction(trans, Sql)
                            Else
                                'Sql = "SELECT Item_Basic_Net ,UOM,Empty_Value_Bottle FROM TSPL_ITEM_PRICE_MASTER WHERE Item_Code ='" + Convert.ToString(fathercode) + "'"
                                'Dim DS As DataSet = connectSql.RunSQLReturnDS(trans, Sql)

                                Sql = "Insert Into TSPL_ITEM_LOCATION_DETAILS Values ('" + Convert.ToString(fathercode) + "','" + fathedesc + "','" + Convert.ToString(txtToLocation.Value) + "'," & _
                                " '','" + grow.Cells(colLeak).Value.ToString() + "','" + (grow.Cells(colLeak).Value * grow.Cells(colItemCost).Value).ToString() + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentUserCode + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + objCommonVar.CurrentCompanyCode + "','" + ((clsCommon.myCdbl(grow.Cells(colMRP).Value)) * clsCommon.myCdbl(grow.Cells(colConversion).Value)).ToString() + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "','" + Convert.ToString(grow.Cells(colBatchNo).Value) + "','" + Format(connectSql.myDate(), "MM/dd/yyyy") + "', 'E')"
                                connectSql.RunSqlTransaction(trans, Sql)
                            End If
                        End If
                    End If
                End If
            Next
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
            trans.Rollback()
            Return False
        End Try
    End Function

    Public Sub FunFillItemDetails_LoadOut_No(Optional ByVal LoadOutNo As String = Nothing)
        LoadBlankGrid()
        Dim SqlQuery As String = String.Empty
        Dim MRPChk As Decimal = 0
        If LoadOutNo Is Nothing Then
            SqlQuery = "SELECT  td.Batch_No,   ISNULL(IPM.TAX1_Amt,0)+ ISNULL(IPM.TAX2_Amt,0)+ ISNULL(IPM.TAX3_Amt,0) + ISNULL(IPM.TAX4_Amt,0) + ISNULL(IPM.TAX5_Amt,0) + ISNULL(IPM.TAX6_Amt,0) + ISNULL(IPM.TAX7_Amt,0) + ISNULL(IPM.TAX8_Amt,0) + ISNULL(IPM.TAX9_Amt,0) + ISNULL(IPM.TAX10_Amt,0) + ISNULL(IPM.Item_Basic_Price ,0) AS [BASICPRICEWITHTAX] ,convert(varchar(10),TD.Price_Date,103) as Date , ISNULL(IPM.Empty_Value_Bottle+ IPM.Empty_Value_Shell ,0) AS EMPTY,   TD.Item_Qty,TD.Assessable_Amt, TD.Amount, TD.Net_Amount, TD.Pending_Qty,TD.LoadIn_Qty,TD.Total_Tax,TD.Total_Item_Amt,IPM.uom,TD.breakage, IPM.Item_Basic_Net , IPM.Item_Basic_Price , TD.Price_Date, TD.Item_Code, TD.Item_Desc, TD.Basic_Price, (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM ) as [ConversionFactor] , TD.Pending_Balance_In_Bottle ,TD.MRP_In_Bottle ,TD.MRP_In_Bottle,IPM.Item_Basic_Net*(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) AS [ORDERBY],case when IPM.UOM ='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end SKuSeq FROM TSPL_INDENT_DETAIL TD  JOIN TSPL_ITEM_PRICE_MASTER IPM ON TD.Item_Code = IPM.Item_Code AND TD.Price_Date = IPM.Start_Date and (CASE WHEN IPM.UOM='FB' THEN  TD.MRP/(SELECT Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TD.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) ELSE TD.MRP END  )    =IPM.Item_Basic_Net    JOIN TSPL_ITEM_MASTER IM ON IPM.Item_Code = IM.Item_Code and (select Top 1 Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = IPM.Item_Code and UOM_Code  = IPM.UOM  )*IPM.Item_Basic_Net in (select mrp from TSPL_INDENT_DETAIL where Indent_No = '" + txtLoadoutNo.Value + "' and Item_Code = IPM.Item_Code  and Start_Date = IPM.Start_Date )   where TD.Indent_No='" + txtLoadoutNo.Value + "'  and IPM.Price_Code = '" + txtPriceCode.Value + "' ORDER BY  SKuSeq ,ORDERBY,Uom desc  "
        ElseIf LoadOutNo = "FC" Then
            SqlQuery = "SELECT  td.Batch_No,   ISNULL(IPM.TAX1_Amt,0)+ ISNULL(IPM.TAX2_Amt,0)+ ISNULL(IPM.TAX3_Amt,0) + ISNULL(IPM.TAX4_Amt,0) + ISNULL(IPM.TAX5_Amt,0) + ISNULL(IPM.TAX6_Amt,0) + ISNULL(IPM.TAX7_Amt,0) + ISNULL(IPM.TAX8_Amt,0) + ISNULL(IPM.TAX9_Amt,0) + ISNULL(IPM.TAX10_Amt,0) + ISNULL(IPM.Item_Basic_Price ,0) AS [BASICPRICEWITHTAX] ,convert(varchar(10),TD.Price_Date,103) as Date , ISNULL(IPM.Empty_Value_Bottle+ IPM.Empty_Value_Shell ,0) AS EMPTY,   TD.Item_Qty,TD.Assessable_Amt, TD.Amount, TD.Net_Amount, TD.Pending_Qty,TD.LoadIn_Qty,TD.Total_Tax,TD.Total_Item_Amt,IPM.uom,TD.breakage, IPM.Item_Basic_Net , IPM.Item_Basic_Price , TD.Price_Date, TD.Item_Code, TD.Item_Desc, TD.Basic_Price, (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM ) as [ConversionFactor] , TD.Pending_Balance_In_Bottle ,TD.MRP_In_Bottle ,TD.MRP_In_Bottle,IPM.Item_Basic_Net*(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) AS [ORDERBY],case when IPM.UOM ='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end SKuSeq FROM TSPL_INDENT_DETAIL TD  JOIN TSPL_ITEM_PRICE_MASTER IPM ON TD.Item_Code = IPM.Item_Code AND TD.Price_Date = IPM.Start_Date and (CASE WHEN IPM.UOM='FB' THEN  TD.MRP/(SELECT Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TD.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) ELSE TD.MRP END  )    =IPM.Item_Basic_Net   JOIN TSPL_ITEM_MASTER IM ON IPM.Item_Code = IM.Item_Code and (select Top 1 Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = IPM.Item_Code and UOM_Code  = IPM.UOM  )*IPM.Item_Basic_Net in (select mrp from TSPL_INDENT_DETAIL where Indent_No = '" + txtLoadoutNo.Value + "' and Item_Code = IPM.Item_Code  and Start_Date = IPM.Start_Date )   where TD.Indent_No='" + txtLoadoutNo.Value + "'  and IPM.Price_Code = '" + txtPriceCode.Value + "'  and (IPM.UOM = '" + LoadOutNo + "' or IPM.UOM = 'SH') ORDER BY  SKuSeq ,ORDERBY,Uom desc  "
        ElseIf LoadOutNo = "FB" Then
            SqlQuery = "SELECT  td.Batch_No,   ISNULL(IPM.TAX1_Amt,0)+ ISNULL(IPM.TAX2_Amt,0)+ ISNULL(IPM.TAX3_Amt,0) + ISNULL(IPM.TAX4_Amt,0) + ISNULL(IPM.TAX5_Amt,0) + ISNULL(IPM.TAX6_Amt,0) + ISNULL(IPM.TAX7_Amt,0) + ISNULL(IPM.TAX8_Amt,0) + ISNULL(IPM.TAX9_Amt,0) + ISNULL(IPM.TAX10_Amt,0) + ISNULL(IPM.Item_Basic_Price ,0) AS [BASICPRICEWITHTAX] ,convert(varchar(10),TD.Price_Date,103) as Date , ISNULL(IPM.Empty_Value_Bottle+ IPM.Empty_Value_Shell ,0) AS EMPTY,   TD.Item_Qty,TD.Assessable_Amt, TD.Amount, TD.Net_Amount, TD.Pending_Qty,TD.LoadIn_Qty,TD.Total_Tax,TD.Total_Item_Amt,IPM.uom,TD.breakage, IPM.Item_Basic_Net , IPM.Item_Basic_Price , TD.Price_Date, TD.Item_Code, TD.Item_Desc, TD.Basic_Price, (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM ) as [ConversionFactor] , TD.Pending_Balance_In_Bottle ,TD.MRP_In_Bottle ,TD.MRP_In_Bottle,IPM.Item_Basic_Net*(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) AS [ORDERBY],case when IPM.UOM ='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end SKuSeq FROM TSPL_INDENT_DETAIL TD  JOIN TSPL_ITEM_PRICE_MASTER IPM ON TD.Item_Code = IPM.Item_Code AND TD.Price_Date = IPM.Start_Date and (CASE WHEN IPM.UOM='FB' THEN  TD.MRP/(SELECT Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL WHERE TSPL_ITEM_UOM_DETAIL.Item_Code = TD.Item_Code AND TSPL_ITEM_UOM_DETAIL.UOM_Code='FB' ) ELSE TD.MRP END  )    =IPM.Item_Basic_Net   JOIN TSPL_ITEM_MASTER IM ON IPM.Item_Code = IM.Item_Code and (select Top 1 Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = IPM.Item_Code and UOM_Code  = IPM.UOM  )*IPM.Item_Basic_Net in (select mrp from TSPL_INDENT_DETAIL where Indent_No = '" + txtLoadoutNo.Value + "' and Item_Code = IPM.Item_Code  and Start_Date = IPM.Start_Date )   where TD.Indent_No='" + txtLoadoutNo.Value + "'  and IPM.Price_Code = '" + txtPriceCode.Value + "' and (IPM.UOM = '" + LoadOutNo + "' or IPM.UOM = 'SH') ORDER BY SKuSeq ,ORDERBY,Uom desc  "
        End If
        Dim dr1 As DataTable = clsDBFuncationality.GetDataTable(SqlQuery)

        Dim itemqty As Decimal = 0
        Dim amt As Decimal = 0
        Dim itemcost As Decimal = 0
        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
            gv1.Columns(colPendingQty).HeaderText = "Pending Qty"
        End If
        isInsideLoadData = True
        If dr1.Rows.Count > 0 Then
            For Each row As DataRow In dr1.Rows
                Dim grow As GridViewRowInfo = gv1.Rows.AddNew()
                MRPChk = connectSql.RunScalar(trans, "select NoMRP from TSPL_ITEM_MASTER where Item_Code ='" + row("Item_Code").ToString() + "'")
                grow.Cells(colItemCode).Value = row("Item_Code").ToString()
                grow.Cells(colItemName).Value = row("Item_Desc").ToString()
                grow.Cells(colPriceDate).Value = Format(row("Price_Date"), "dd-MM-yyyy")
                grow.Cells(colAssessableAmt).Value = row("Assessable_Amt").ToString()
                'grow.Cells(colAmount).Value = 0
                'grow.Cells(colTax).Value = 0
                'grow.Cells(colTotal).Value = 0
                Dim pricedate As String = Format(row("Price_Date"), "yyyy-MM-dd")
                'grow.Cells(colLoadInQty).Value = 0
                grow.Cells(colBreakage).Value = clsCommon.myCdbl(row("breakage"))
                grow.Cells(colUOM).Value = clsCommon.myCstr(row("UOM"))
                Dim tptvalue As Decimal = 0
                Dim qry As String = "SELECT case when PC1.TPT_Type='Y' then ISNULL(Price_Amount1,0) else 0 end"
                qry += " + case when PC2.TPT_Type='Y' then ISNULL(Price_Amount2,0) else 0 end "
                qry += " + case when PC3.TPT_Type='Y' then ISNULL(Price_Amount3,0) else 0 end "
                qry += " + case when PC4.TPT_Type='Y' then ISNULL(Price_Amount4,0) else 0 end "
                qry += " + case when PC5.TPT_Type='Y' then ISNULL(Price_Amount5,0) else 0 end "
                qry += " + case when PC6.TPT_Type='Y' then ISNULL(Price_Amount6,0) else 0 end "
                qry += " + case when PC7.TPT_Type='Y' then ISNULL(Price_Amount7,0) else 0 end "
                qry += " + case when PC8.TPT_Type='Y' then ISNULL(Price_Amount8,0) else 0 end "
                qry += " + case when PC9.TPT_Type='Y' then ISNULL(Price_Amount9,0) else 0 end "
                qry += " + case when PC10.TPT_Type='Y' then ISNULL(Price_Amount10,0) else 0 end "
                qry += " FROM TSPL_ITEM_PRICE_MASTER "
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC1 on PC1.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp1"
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC2 on PC2.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp2"
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC3 on PC3.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp3"
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC4 on PC4.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp4"
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC5 on PC5.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp5"
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC6 on PC6.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp6"
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC7 on PC7.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp7"
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC8 on PC8.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp8"
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC9 on PC9.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp9"
                qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC10 on PC10.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp10"
                qry += " Where Price_Code='" + txtPriceCode.Value + "' and Item_Code = '" + grow.Cells(colItemCode).Value + "' AND Item_Basic_Net ='" + row("Item_Basic_Net").ToString() + "' AND Item_Basic_Price ='" + row("Item_Basic_Price").ToString() + "'"

                Dim basicprice As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select case when TSPL_ITEM_MASTER.NoMRP=1 then  NetLTPT+(isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0) +isnull(TAX3_Amt,0) +isnull(TAX4_Amt,0) +isnull(TAX5_Amt,0) +isnull(TAX6_Amt,0) +isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) else NetLTPT end   from TSPL_ITEM_PRICE_MASTER inner join TSPL_ITEM_MASTER on TSPL_ITEM_PRICE_MASTER.Item_Code =TSPL_ITEM_MASTER.Item_Code  Where Price_Code='" + txtPriceCode.Value + "' AND Item_Basic_Net = '" + row("Item_Basic_Net").ToString() + "' AND Item_Basic_Price = '" + row("Item_Basic_Price").ToString() + "' AND TSPL_ITEM_PRICE_MASTER.Item_Code = '" + grow.Cells(colItemCode).Value + "'"))

                grow.Cells(colBasicPriceWithTax).Value = basicprice

                grow.Cells(colTPT).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                If MRPChk = 1 And clsCommon.myLen(txtRouteNo.Value) = 0 Then
                    grow.Cells(colMRP).Value = basicprice
                    grow.Cells(colBasicPrice).Value = basicprice
                Else
                    grow.Cells(colMRP).Value = clsCommon.myCdbl(row("Item_Basic_Net"))
                    grow.Cells(colBasicPrice).Value = clsCommon.myCdbl(row("Item_Basic_Price"))
                End If
                grow.Cells(colMRPInBottel).Value = clsCommon.myCdbl(row("MRP_In_Bottle"))
                grow.Cells(colPriceDate).Value = Format(row("Price_Date"), "dd-MM-yyyy")
                grow.Cells(colBatchNo).Value = row("batch_no").ToString()
                'grow.Cells(colLeak).Value = 0
                'grow.Cells(colShortage).Value = 0
                If clsCommon.myCdbl(row("ConversionFactor")) = 1 Then
                    grow.Cells(colTransferQty).Value = clsCommon.myCdbl(row("Item_Qty"))
                    grow.Cells(colPendingQty).Value = clsCommon.myCdbl(row("Pending_Qty"))
                Else
                    grow.Cells(colTransferQty).Value = 0
                    grow.Cells(colPendingQty).Value = 0
                End If

                itemcost = clsCommon.myCdbl(connectSql.RunScalar("SELECT case when ISNULL(SUM(ISNULL(Item_Qty,0)),0)>0 then ISNULL(SUM(ISNULL(Amount,0)),0)/ISNULL(SUM(ISNULL(Item_Qty,0)),0) else 0 end   AS [Cost] FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "'  AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "' AND MRP='" + grow.Cells(colMRP).Value.ToString() + "' "))
                Dim cost As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select item_price from TSPL_INDENT_DETAIL where Indent_No ='" + txtTransferNo.Value + "'and item_code='" + grow.Cells(colItemCode).Value.ToString() + "'"))
                If cost <> 0 Then
                    grow.Cells(colItemCost).Value = cost
                Else
                    grow.Cells(colItemCost).Value = Math.Round(itemcost, 2)
                End If

                grow.Cells(colConversion).Value = clsCommon.myCdbl(row("ConversionFactor"))
                grow.Cells(colPendingBalance).Value = clsCommon.myCdbl(row("Pending_Qty"))
                grow.Cells(colPensingBalanceInBottle).Value = clsCommon.myCdbl(row("Pending_Balance_In_Bottle"))
                grow.Cells(colApplyTotalInBottle).Value = 0
                grow.Cells(colEmptyValue).Value = clsCommon.myCdbl(row("EMPTY"))
            Next
        End If
        isInsideLoadData = False
        'dr1.Close() 
        'gv1.AllowAddNewRow = False
    End Sub

    Private Sub funloadinagainstloadout(Optional ByVal itemtype As String = "ALL")
        LoadBlankGrid()
        'If itemtype = "EC" Then
        '    sql = "SELECT  td.Batch_No,   ISNULL(IPM.TAX1_Amt,0)+ ISNULL(IPM.TAX2_Amt,0)+ ISNULL(IPM.TAX3_Amt,0) + ISNULL(IPM.TAX4_Amt,0) + ISNULL(IPM.TAX5_Amt,0) + ISNULL(IPM.TAX6_Amt,0) + ISNULL(IPM.TAX7_Amt,0) + ISNULL(IPM.TAX8_Amt,0) + ISNULL(IPM.TAX9_Amt,0) + ISNULL(IPM.TAX10_Amt,0) + ISNULL(IPM.Item_Basic_Price ,0) AS [BASICPRICEWITHTAX] ,convert(varchar(10),TD.Price_Date,103) as Date , ISNULL(IPM.Empty_Value_Bottle+ IPM.Empty_Value_Shell ,0) AS EMPTY,   TD.Item_Qty,TD.Assessable_Amt, TD.Amount, TD.Net_Amount, TD.Pending_Qty,TD.LoadIn_Qty,TD.Total_Tax,TD.Total_Item_Amt,IPM.uom,TD.breakage, IPM.Item_Basic_Net , IPM.Item_Basic_Price , TD.Price_Date, td.Item_Code, TD.Item_Desc, TD.Basic_Price , TD.Leak , TD.Shortage , TD.Burst , (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) as ConvFact, TD.Pending_Balance_In_Bottle,TD.mrp_in_bottle ,td.Basic_Price*(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) AS [ORDERBY],case when IPM.UOM ='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end SKuSeq FROM TSPL_INDENT_DETAIL TD  JOIN TSPL_ITEM_PRICE_MASTER IPM ON TD.Item_Code = IPM.Item_Code AND TD.Price_Date = IPM.Start_Date  JOIN TSPL_ITEM_MASTER IM ON IPM.Item_Code = IM.Item_Code and (select Top 1 Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = IPM.Item_Code and UOM_Code  = IPM.UOM  )*IPM.Item_Basic_Net in (select mrp from TSPL_INDENT_DETAIL where Indent_No = '" + txtLoadoutNo.Value + "' and Item_Code = IPM.Item_Code  and Start_Date = IPM.Start_Date )   where TD.Indent_No='" + txtLoadoutNo.Value + "'  and IPM.Price_Code = '" + txtPriceCode.Value + "' and (ipm.uom = '" + itemtype + "' or IPM.UOM ='SH') ORDER BY  SKuSeq,Item_Code ,Uom desc ,ORDERBY desc  "
        'ElseIf itemtype = "EB" Then
        '    sql = "SELECT  td.Batch_No,   ISNULL(IPM.TAX1_Amt,0)+ ISNULL(IPM.TAX2_Amt,0)+ ISNULL(IPM.TAX3_Amt,0) + ISNULL(IPM.TAX4_Amt,0) + ISNULL(IPM.TAX5_Amt,0) + ISNULL(IPM.TAX6_Amt,0) + ISNULL(IPM.TAX7_Amt,0) + ISNULL(IPM.TAX8_Amt,0) + ISNULL(IPM.TAX9_Amt,0) + ISNULL(IPM.TAX10_Amt,0) + ISNULL(IPM.Item_Basic_Price ,0) AS [BASICPRICEWITHTAX] ,convert(varchar(10),TD.Price_Date,103) as Date , ISNULL(IPM.Empty_Value_Bottle+ IPM.Empty_Value_Shell ,0) AS EMPTY,   TD.Item_Qty,TD.Assessable_Amt, TD.Amount, TD.Net_Amount, TD.Pending_Qty,TD.LoadIn_Qty,TD.Total_Tax,TD.Total_Item_Amt,IPM.uom,TD.breakage, IPM.Item_Basic_Net , IPM.Item_Basic_Price , TD.Price_Date, td.Item_Code, TD.Item_Desc, TD.Basic_Price , TD.Leak , TD.Shortage , TD.Burst , (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) as ConvFact, TD.Pending_Balance_In_Bottle,TD.mrp_in_bottle ,td.Basic_Price*(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) AS [ORDERBY],case when IPM.UOM ='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end SKuSeq FROM TSPL_INDENT_DETAIL TD  JOIN TSPL_ITEM_PRICE_MASTER IPM ON TD.Item_Code = IPM.Item_Code AND TD.Price_Date = IPM.Start_Date  JOIN TSPL_ITEM_MASTER IM ON IPM.Item_Code = IM.Item_Code and (select Top 1 Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = IPM.Item_Code and UOM_Code  = IPM.UOM  )*IPM.Item_Basic_Net in (select mrp from TSPL_INDENT_DETAIL where Indent_No = '" + txtLoadoutNo.Value + "' and Item_Code = IPM.Item_Code  and Start_Date = IPM.Start_Date )   where TD.Indent_No='" + txtLoadoutNo.Value + "'  and IPM.Price_Code = '" + txtPriceCode.Value + "' and (ipm.uom = '" + itemtype + "' or IPM.UOM ='SH') ORDER BY  SKuSeq,Item_Code ,Uom desc ,ORDERBY desc  "
        'Else
        '    sql = "SELECT td.Batch_No,   ISNULL(IPM.TAX1_Amt,0)+ ISNULL(IPM.TAX2_Amt,0)+ ISNULL(IPM.TAX3_Amt,0) + ISNULL(IPM.TAX4_Amt,0) + ISNULL(IPM.TAX5_Amt,0) + ISNULL(IPM.TAX6_Amt,0) + ISNULL(IPM.TAX7_Amt,0) + ISNULL(IPM.TAX8_Amt,0) + ISNULL(IPM.TAX9_Amt,0) + ISNULL(IPM.TAX10_Amt,0) + ISNULL(IPM.Item_Basic_Price ,0) AS [BASICPRICEWITHTAX] ,convert(varchar(10),TD.Price_Date,103) as Date , ISNULL(IPM.Empty_Value_Bottle+ IPM.Empty_Value_Shell ,0) AS EMPTY,   case when IPM.UOM ='EB' then 0 else  TD.Item_Qty end Item_Qty ,TD.Assessable_Amt, TD.Amount, TD.Net_Amount, case when IPM.UOM ='EB' then 0 else  TD.Pending_Qty end as  Pending_Qty,TD.LoadIn_Qty,TD.Total_Tax,TD.Total_Item_Amt,IPM.uom,TD.breakage, IPM.Item_Basic_Net , IPM.Item_Basic_Price , TD.Price_Date, IPM.Item_Code, TD.Item_Desc, TD.Basic_Price, (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) as ConvFact, TD.Pending_Balance_In_Bottle,TD.mrp_in_bottle ,td.Basic_Price*(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) AS [ORDERBY],case when IPM.UOM ='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end SKuSeq FROM TSPL_INDENT_DETAIL TD JOIN TSPL_ITEM_PRICE_MASTER IPM ON TD.Item_Code = IPM.Item_Code AND TD.Price_Date = IPM.Start_Date JOIN TSPL_ITEM_MASTER IM ON IPM.Item_Code = IM.Item_Code     where TD.Indent_No='" + txtLoadoutNo.Value + "' and TD.Pending_Qty <> 0  ORDER BY  SKuSeq,Item_Code ,Uom desc ,ORDERBY desc  "
        'End If

        Dim sql As String = "SELECT td.Batch_No,   ISNULL(IPM.TAX1_Amt,0)+ ISNULL(IPM.TAX2_Amt,0)+ ISNULL(IPM.TAX3_Amt,0) + ISNULL(IPM.TAX4_Amt,0) + ISNULL(IPM.TAX5_Amt,0) + ISNULL(IPM.TAX6_Amt,0) + ISNULL(IPM.TAX7_Amt,0) + ISNULL(IPM.TAX8_Amt,0) + ISNULL(IPM.TAX9_Amt,0) + ISNULL(IPM.TAX10_Amt,0) + ISNULL(IPM.Item_Basic_Price ,0) AS [BASICPRICEWITHTAX] ,convert(varchar(10),TD.Price_Date,103) as Date , ISNULL(IPM.Empty_Value_Bottle+ IPM.Empty_Value_Shell ,0) AS EMPTY,   case when IPM.UOM ='EB' then 0 else  TD.Item_Qty end Item_Qty ,TD.Assessable_Amt, TD.Amount, TD.Net_Amount, case when IPM.UOM ='EB' then 0 else  TD.Pending_Qty end as  Pending_Qty,TD.LoadIn_Qty,TD.Total_Tax,TD.Total_Item_Amt,IPM.uom,TD.breakage, IPM.Item_Basic_Net , IPM.Item_Basic_Price , TD.Price_Date, IPM.Item_Code, TD.Item_Desc, TD.Basic_Price, (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) as ConvFact, TD.Pending_Balance_In_Bottle,TD.mrp_in_bottle ,td.Basic_Price*(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TD.Item_Code and UOM_Code = IPM.UOM) AS [ORDERBY],case when IPM.UOM ='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end SKuSeq FROM TSPL_INDENT_DETAIL TD JOIN TSPL_ITEM_PRICE_MASTER IPM ON TD.Item_Code = IPM.Item_Code AND TD.Price_Date = IPM.Start_Date JOIN TSPL_ITEM_MASTER IM ON IPM.Item_Code = IM.Item_Code     where TD.Indent_No='" + txtLoadoutNo.Value + "'   "
        If clsCommon.CompairString(itemtype, "EC") = CompairStringResult.Equal Then
            sql += " and  ipm.uom in ( '" + itemtype + "',      'SH')"
        ElseIf clsCommon.CompairString(itemtype, "EB") = CompairStringResult.Equal Then
            sql += " and  ipm.uom in( '" + itemtype + "','SH')  "
        End If
        sql += " ORDER BY  SKuSeq,Item_Code ,Uom desc ,ORDERBY desc "
        Dim dr1 As DataTable
        dr1 = clsDBFuncationality.GetDataTable(sql)
        'Dim row As Integer = 0
        Dim itemqty As Decimal = 0
        Dim amt As Decimal = 0
        Dim tptcheck As String = "N"
        Dim itemcost As Decimal = 0
        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
            gv1.Columns(colPendingQty).HeaderText = "Pending Qty"
        End If
        '  Dim mrp, basicprice As Decimal
        If dr1.Rows.Count > 0 Then
            isInsideLoadData = True
            For Each row As DataRow In dr1.Rows
                Dim grow As GridViewRowInfo = gv1.Rows.AddNew()
                grow.Cells(colItemCode).Value = row("Item_Code").ToString()
                grow.Cells(colItemName).Value = row("Item_Desc").ToString()
                grow.Cells(colPriceDate).Value = Format(row("Price_Date"), "dd-MM-yyyy")
                grow.Cells(colAssessableAmt).Value = row("Assessable_Amt").ToString()
                grow.Cells(colAmount).Value = "0.00"
                grow.Cells(colTax).Value = "0.00"
                grow.Cells(colTotal).Value = "0.00"
                Dim pricedate As String = Format(row("Price_Date"), "yyyy-MM-dd")
                grow.Cells(colLoadInQty).Value = "0.00"
                grow.Cells(colBreakage).Value = row("breakage").ToString()
                grow.Cells(colUOM).Value = row("UOM").ToString()
                Dim tptvalue As Decimal = 0
                grow.Cells(colBasicPriceWithTax).Value = Convert.ToString(row("BASICPRICEWITHTAX"))
                Dim tptdr As DataTable = clsDBFuncationality.GetDataTable("select  Price_Comp1 , Price_Amount1,Price_Comp2 ,Price_Amount2,Price_Comp3 ,Price_Amount3,Price_Comp4 ,Price_Amount4,Price_Comp5 ,Price_Amount5,Price_Comp6 ,Price_Amount6,Price_Comp7 ,Price_Amount7,Price_Comp8 ,Price_Amount8,Price_Comp9 ,Price_Amount9,Price_Comp10 ,Price_Amount10  from TSPL_ITEM_PRICE_MASTER where Item_Code = '" + grow.Cells(colItemCode).Value + "' and Start_Date = '" + pricedate + "' and UOM = '" + grow.Cells(colUOM).Value + "' and price_code = '" + txtPriceCode.Value + "'")
                If tptdr.Rows.Count > 0 Then
                    For Each rownew As DataRow In tptdr.Rows
                        For icount As Integer = 1 To 10
                            Dim Price_Amount As String = "Price_Amount" + icount.ToString()
                            Dim Price_Comp As String = "Price_Comp" + icount.ToString()
                            tptcheck = connectSql.RunScalar("select TPT_TYPE  from TSPL_PRICE_COMPONENT_MASTER WHERE Price_Comp_code = '" + Convert.ToString(rownew(Price_Comp)) + "'")
                            If tptcheck = "Y" Then
                                tptvalue = Convert.ToString(rownew(Price_Amount))
                                Exit For
                            End If
                        Next
                    Next
                End If
                grow.Cells(colTPT).Value = Convert.ToString(tptvalue)
                grow.Cells(colEmptyValue).Value = Convert.ToString(row("EMPTY"))
                grow.Cells(colMRP).Value = Convert.ToString(row("Item_Basic_Net"))
                grow.Cells(colMRPInBottel).Value = Convert.ToString(row("mrp_in_bottle"))
                grow.Cells(colPriceDate).Value = Format(row("Price_Date"), "dd-MM-yyyy")
                grow.Cells(colBatchNo).Value = row("batch_no").ToString()
                grow.Cells(colLeak).Value = "0.00"
                grow.Cells(colShortage).Value = "0.00"
                grow.Cells(colBasicPrice).Value = Convert.ToString(row("Item_Basic_Price"))
                Dim sql3 As String = "SELECT Location_Code   FROM TSPL_LOCATION_MASTER WHERE Location_Desc ='" + txtFromLoaction.Value + "' "
                grow.Cells(colTransferQty).Value = Convert.ToString(row("Item_Qty"))
                grow.Cells(colPendingQty).Value = Convert.ToString(row("Pending_Qty"))
                Dim frmlocation1 As String = connectSql.RunScalar(sql3)
                Dim sql4 As String = "SELECT ISNULL(SUM(ISNULL(Item_Qty,0)),0) AS [Item_Qty],  ISNULL(SUM(ISNULL(Amount,0)),0) AS [Amount] FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + grow.Cells(colItemCode).Value.ToString() + "' " & _
                           " AND location_code='" + txtFromLoaction.Value + "' AND MRP='" + grow.Cells(colMRP).Value.ToString() + "'"
                Dim dr12 As DataTable = clsDBFuncationality.GetDataTable(sql4)
                If dr12.Rows.Count > 0 Then
                    For Each row11 As DataRow In dr12.Rows
                        itemqty = clsCommon.myCdbl(row11(0).ToString())
                        amt = clsCommon.myCdbl(row11(1).ToString())
                        If itemqty > 0 Then
                            itemcost = clsCommon.myCdbl(Math.Round(amt / itemqty, 2))
                        Else
                            itemcost = "0.00"
                        End If
                    Next
                End If
                'dr12.Close()
                Dim query1 As String = "select item_price from TSPL_INDENT_DETAIL where Indent_No ='" + txtTransferNo.Value + "'and item_code='" + grow.Cells(colItemCode).Value.ToString() + "'"
                Dim cost As Decimal = clsCommon.myCdbl(connectSql.RunScalar(query1))
                If cost <> 0 Then
                    grow.Cells(colItemCost).Value = cost
                Else
                    grow.Cells(colItemCost).Value = itemcost
                End If
                grow.Cells(colPendingBalance).Value = Convert.ToString(row("Pending_Qty"))
                grow.Cells(colConversion).Value = Convert.ToString(row("ConvFact"))
                grow.Cells(colPensingBalanceInBottle).Value = clsCommon.myCdbl(row("Pending_Balance_In_Bottle"))
                grow.Cells(colApplyTotalInBottle).Value = "0.00"
            Next

            txtPriceCode.Enabled = False
            txtFromLoaction.Enabled = False
            txtRouteNo.Enabled = False

            If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                FunFillPendingBalanceQty()
            End If

            isInsideLoadData = False
        End If
        'dr1.Close()
        'gv1.AllowAddNewRow = False
    End Sub

    Public Sub itemfunfill_loadoutno()
        Try
            Dim myreader As DataTable = clsDBFuncationality.GetDataTable("SELECT CONVERT(varchar(10),Indent_Date,103)as Indent_Date ,CONVERT(varchar(10),Posting_Date,103) as posting_date, Convert(varchar(10),Load_out_date,103) as Load_Out_Date,To_Location,From_Location,Price_Code,convert(varchar(10),Price_Date,103)as price_date, Reference, description, Route_No,Salesmancode, Vehicle_Code, Vehicle_No, Mode_Of_Transport, Km_Reading, Item_Amount,Total_Item_Amount,post,trip_no,item_type,Trans_Type FROM TSPL_INDENT_HEAD where Indent_No='" + txtLoadoutNo.Value + "'")
            If myreader.Rows.Count > 0 Then
                For Each row As DataRow In myreader.Rows
                    txtTransferDate.Value = CDate(row("Indent_Date")).ToString("dd/MM/yyyy")
                    txtTransferDate.Text = CDate(row("posting_date").ToString()).ToString("dd/MM/yyyy")
                    txtLoadoutDate.Value = CDate(row("Indent_Date")).ToString("dd/MM/yyyy")
                    txtFromLoaction.Value = Convert.ToString(row("To_Location"))
                    txtToLocation.Value = Convert.ToString(row("From_Location"))
                    If 2 = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select sum(1) from TSPL_LOCATION_MASTER where Location_Type ='Physical'  and   Location_Code in ('" + clsCommon.myCstr(txtFromLoaction.Value) + "','" + clsCommon.myCstr(txtToLocation.Value) + "')")) Then
                        txtToLocation.Value = ""
                    End If
                    FromLocationLoadInTime = txtToLocation.Value
                    txtPriceCode.Value = Convert.ToString(row("Price_Code"))
                    txtPriceCode.Enabled = False
                    txtFromLoaction.Enabled = False
                    txtReference.Text = Convert.ToString(row("Reference"))
                    txtReference.ReadOnly = False
                    txtDescription.ReadOnly = False
                    txtDescription.Text = Convert.ToString(row("description"))
                    txtRouteNo.Value = Convert.ToString(row("Route_No"))
                    txtRouteNo.Enabled = False
                    txtSalesman.Text = Convert.ToString(row("Salesmancode"))
                    txtVehicleCode.Value = Convert.ToString(row("Vehicle_Code"))
                    txtVehicle.Text = txtVehicleCode.Value
                    lblVehicleDesc.Text = Convert.ToString(row("Vehicle_No"))
                    cboModeofTransport.Text = Convert.ToString(row("Mode_Of_Transport"))
                    txtKMReading.Text = Convert.ToString(row("Km_Reading"))
                    txtVehicleCode.Enabled = False
                    txtKMReading.ReadOnly = False
                    txtTripNo.Text = Convert.ToString(row("trip_no"))
                    txtTripNo.ReadOnly = False
                    If Convert.ToString(row("item_type")) = "Full" Then
                        cmbitemtype.SelectedIndex = 1
                    Else
                        cmbitemtype.SelectedIndex = 2

                    End If
                    cboType.SelectedValue = clsCommon.myCstr(row("Trans_Type"))
                    cboType.Enabled = False
                    'cmbitemtype.Enabled = False
                    lblRouteNo.Text = connectSql.RunScalar("select Route_Desc  from TSPL_ROUTE_MASTER where Route_No  = '" + txtRouteNo.Value + "'")
                Next
                If cmbitemtype.Text = "Full" Then

                    rdball.IsChecked = True
                    chkApplyShell.Enabled = True
                    FunFillItemDetails_LoadOut_No()

                Else

                    rdbeall.IsChecked = True
                    chkApplyShell.Enabled = True
                    funloadinagainstloadout()

                End If

                fndtolocation_Leave_1()
                'fndTaxGroup.Enabled = False
                gv1.Enabled = True
            End If
            'myreader.Close()
            'End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub

    Public Sub loadIn_totalamount()
        Dim mrp As Decimal = 0.0
        Dim netAmount As Decimal = 0.0
        Dim basicAmt As Decimal = 0.0
        Dim totalDiscount As Decimal = 0.0
        Dim totalTax As Decimal = 0.0
        For Each gro As GridViewRowInfo In gv1.Rows
            If Not gro.Cells(colTransferQty).Value = 0 Then

            End If

            If Not gro.Cells(colLoadInQty).Value = 0 Then
                gro.Cells(colAmount).Value = Math.Round(clsCommon.myCdbl(gro.Cells(colLoadInQty).Value) * clsCommon.myCdbl(gro.Cells(colItemCost).Value), 2)
                basicAmt = basicAmt + gro.Cells(colAmount).Value
                netAmount = netAmount + Math.Round(clsCommon.myCdbl(gro.Cells(colAmount).Value), 2)
            End If

        Next
        txtAmount.Text = basicAmt
        txtTotalAmount.Text = clsCommon.myCdbl(txtAmount.Text) + clsCommon.myCdbl(txtTotalTaxAmt.Text)
    End Sub

    Public Sub readable()
        'txtTransferDate.Enabled = False
        'txtTransferDate.Enabled = False
        cboTransferType.Enabled = False
        txtRouteNo.MyReadOnly = True
        cboModeofTransport.Enabled = False
        txtFromLoaction.MyReadOnly = True
        txtToLocation.MyReadOnly = True
        txtKMReading.ReadOnly = False
        cboPriceDate.Enabled = False

        txtVehicleCode.MyReadOnly = True
        txtReference.ReadOnly = False
        txtLoadoutDate.Enabled = False
        'rdtxtvehicledesc.ReadOnly = True
        txtDescription.Width = 163
        txtDescription.Height = 20
        txtDescription.ReadOnly = False
        rdlblloadoutno.Visible = True
        txtLoadoutNo.Visible = True
        txtLoadoutNo.MyReadOnly = True
        'fndTaxGroup.Enabled = False
        gv2.Enabled = False
        txtLoadoutNo.MendatroryField = True
        'If objCommonVar.CurrentUserCode <> "ADMIN" Then
        '    If funSetUserAccess() = False Then Exit Sub
        'End If

    End Sub

    Public Sub PrintDataNew(ByVal isPrePrint As Boolean)
        Dim qry As String
        Try
            If clsCommon.myLen(txtTransferNo.Value) > 0 Then
                qry = "select (TSPL_LOCATION_MASTER.Add1+(case when LEN(TSPL_LOCATION_MASTER.Add2)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Add2+(case when LEN(TSPL_LOCATION_MASTER.Add3)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Add3+(case when LEN(TSPL_LOCATION_MASTER.Add4)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Add4+(case when LEN(TSPL_LOCATION_MASTER.City_Code)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.City_Code+(case when LEN(TSPL_LOCATION_MASTER.State)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.State+(case when LEN(TSPL_LOCATION_MASTER.Country)>0 then ' ,' else '' end)+TSPL_LOCATION_MASTER.Country) as Address, "
                qry += " TSPL_INDENT_HEAD.Indent_No, CONVERT(varchar, TSPL_INDENT_HEAD.Indent_Date ,103) as Indent_Date, TSPL_INDENT_HEAD.Vehicle_No, "
                qry += " '' as Cust_Name,TSPL_INDENT_DETAIL.Item_Qty, TSPL_INDENT_DETAIL.Item_Code, TSPL_INDENT_DETAIL.Item_Desc, TSPL_INDENT_DETAIL.MRP, "
                qry += " TSPL_INDENT_DETAIL.Basic_Price, TSPL_INDENT_DETAIL.BasicPrice_WithTax, TSPL_INDENT_DETAIL.Basic_Amt, "
                qry += " CONVERT(varchar(100),TSPL_INDENT_HEAD.Date_Time_Removal,108) as RemovelTime,(select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where Item_Code = TSPL_INDENT_DETAIL.Item_Code and  (TSPL_ITEM_UOM_DETAIL.UOM_Code = case when TSPL_INDENT_DETAIL.Uom='FB' then 'FC' else case when TSPL_INDENT_DETAIL.Uom='FC' then 'FB' end end) )  AS Conversion, "
                qry += " TSPL_COMPANY_MASTER.Comp_Name, Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else TSPL_COMPANY_MASTER.Add1 + Case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add1 + case When ISNULL(TSPL_COMPANY_MASTER.Add1,'')='' Then '' Else ', '+TSPL_COMPANY_MASTER.Add1 End End End as CompAddress, TSPL_COMPANY_MASTER.Logo_Img, TSPL_COMPANY_MASTER.Logo_Img2, TSPL_COMPANY_MASTER.Tin_No as CompTinNo, TSPL_INDENT_DETAIL.Assessable_Amt as Item_Assessable_Rate, TSPL_EMPLOYEE_MASTER.Emp_Name,  "
                qry += " (TSPL_EMPLOYEE_MASTER.Add1+Case when ISNULL(TSPL_EMPLOYEE_MASTER.Add2,'')='' Then '' Else ', '+TSPL_EMPLOYEE_MASTER.Add2  End) as [EmpAddress]"
                qry += " from TSPL_INDENT_DETAIL "
                qry += " left outer join TSPL_INDENT_HEAD on  TSPL_INDENT_HEAD.Indent_No=TSPL_INDENT_DETAIL.Indent_No "
                qry += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_INDENT_DETAIL.Item_Code "
                qry += " left outer join TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE=TSPL_INDENT_HEAD.Salesmancode "
                qry += " left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_INDENT_HEAD.To_Location "
                qry += " LEFT OUTER JOIN TSPL_COMPANY_MASTER ON TSPL_INDENT_HEAD.Comp_Code = TSPL_COMPANY_MASTER.Comp_Code  "
                qry += " where TSPL_INDENT_HEAD.Indent_No='" + txtTransferNo.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                Dim frmCRV As New frmCrystalReportViewer()
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptIndentDetail", "Indent Details")
                frmCRV = Nothing
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please select Transfer no", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Public Sub printdata(ByVal isPrePrint As Boolean)
        Try
            If clsCommon.myLen(txtTransferNo.Value) > 0 Then
                Dim qry As String = "select Transfer_Type,Tax_Group from TSPL_INDENT_HEAD where Indent_No='" + txtTransferNo.Value + "'"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    Dim strType As String = ""
                    If clsCommon.CompairString(clsCommon.myCstr(dt.Rows(0)("Transfer_Type")), "LI") = CompairStringResult.Equal Then
                        strType = "LI"
                    ElseIf clsCommon.myLen(dt.Rows(0)("Tax_Group")) > 0 Then
                        strType = "LOT"
                    Else
                        strType = "LON"
                    End If
                    Dim arr As New ArrayList
                    arr.Add(txtTransferNo.Value)
                    frmRptTransfer.funTransfer("", "", strType, arr)
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "No Data found to Print", Me.Text)
                End If
            Else
                common.clsCommon.MyMessageBoxShow(Me, "Please select Transfer no", Me.Text)
            End If

        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    'Private Function funSetUserAccess() As Boolean
    '    Try
    '        Dim strRights As String
    '        Dim strTemp() As String
    '        Dim strProgCode = "STO-TRANSFER"
    '        strRights = enuUserRights.enuRead & "," & enuUserRights.enuModify & "," & enuUserRights.enuDelete
    '        strRights = modUserMgt.funGetPermissions(strRights, strProgCode)
    '        strTemp = Split(strRights, ",")
    '        If strTemp(0) = "0" Then
    '            MsgBox("Permission Denied", MsgBoxStyle.Critical, Me.Text)
    '            funSetUserAccess = False
    '            blnRead = False
    '            Me.Close()
    '            Exit Function
    '        Else
    '            blnRead = True
    '        End If
    '        If strTemp(1) = "0" Then 'Grant modify access
    '            rdbtnsave.Enabled = False
    '        End If
    '        If strTemp(2) = "0" Then 'Grant modify access
    '            rdbtndelete.Enabled = False
    '        End If

    '        funSetUserAccess = True
    '    Catch er As Exception

    '    End Try
    'End Function
 

   

    Private Sub priceDateSelection(ByVal IsChkInGrid As Boolean, Optional ByVal itemtype As String = Nothing)
        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
            If Not IsChkInGrid Then
                LoadBlankGrid()
            End If


            Dim location As String = ""
            Dim lastqty As Decimal = 0
            Dim lastbatchnumber As String = ""
            ' Dim loc As String
            Dim tptcheck As String = "N"
            Dim sql As String = ""
            'Dim pricedate As Date
            'strViewQuery = " SELECT     TOP (100) PERCENT I.Item_Code, I.Item_Desc, CONVERT(varchar(10), P.Start_Date, 103) AS Start_Date, P.UOM, P.Price_Code, P.Item_Basic_Net,i.Batch_No , P.Tax_group  , P.Item_Basic_Price, P.Empty_Value_Shell, P.Empty_Value_Bottle, I.Item_Type, I.show, I.Sku_Seq, P.TAX1_Rate,P.TAX2_Rate,P.TAX3_Rate,P.TAX4_Rate,P.TAX5_Rate,P.TAX6_Rate,P.TAX7_Rate,P.TAX8_Rate,P.TAX9_Rate,P.TAX10_Rate , P.TAX1_Amt ,P.TAX2_Amt,P.TAX3_Amt,P.TAX4_Amt,P.TAX5_Amt,P.TAX6_Amt,P.TAX7_Amt,P.TAX8_Amt,P.TAX9_Amt,P.TAX10_Amt,P.Abatement " & _
            '               " FROM dbo.TSPL_ITEM_PRICE_MASTER AS P  INNER JOIN (SELECT     Item_Code, UOM, MAX(Start_Date) AS MaxDateTime, Item_Basic_Net,  Price_Code, Tax_group  FROM  dbo.TSPL_ITEM_PRICE_MASTER WHERE Start_Date<=  '" + clsCommon.GetPrintDate(txtTransferDate.Value, "dd/MMM/yyyy") + "'  GROUP BY Item_Code, UOM, Item_Basic_Net,  Price_Code, Tax_group ) AS groupedP ON P.Item_Code = groupedP.Item_Code AND  P.Start_Date = groupedP.MaxDateTime AND P.UOM = groupedP.UOM AND P.Item_Basic_Net = groupedP.Item_Basic_Net  AND P.Price_Code = groupedP.Price_Code and P.Tax_group = groupedP.Tax_group   INNER JOIN dbo.TSPL_ITEM_MASTER AS I ON P.Item_Code = I.Item_Code ORDER BY I.Item_Code "


            Dim strViewQuery As String = "SELECT   TSPL_ITEM_MASTER.Item_Code, TSPL_ITEM_MASTER.Item_Desc, CONVERT(varchar(10), TSPL_ITEM_PRICE_MASTER.Start_Date, 103) AS Start_Date, TSPL_ITEM_PRICE_MASTER.UOM, TSPL_ITEM_PRICE_MASTER.Price_Code, TSPL_ITEM_PRICE_MASTER.Item_Basic_Net,TSPL_ITEM_MASTER.Batch_No , TSPL_ITEM_PRICE_MASTER.Tax_group  , TSPL_ITEM_PRICE_MASTER.Item_Basic_Price, TSPL_ITEM_PRICE_MASTER.Empty_Value_Shell, TSPL_ITEM_PRICE_MASTER.Empty_Value_Bottle, TSPL_ITEM_MASTER.Item_Type, TSPL_ITEM_MASTER.show, TSPL_ITEM_MASTER.Sku_Seq, TSPL_ITEM_PRICE_MASTER.TAX1_Rate,TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,TSPL_ITEM_PRICE_MASTER.TAX6_Rate,TSPL_ITEM_PRICE_MASTER.TAX7_Rate,TSPL_ITEM_PRICE_MASTER.TAX8_Rate,TSPL_ITEM_PRICE_MASTER.TAX9_Rate,TSPL_ITEM_PRICE_MASTER.TAX10_Rate , TSPL_ITEM_PRICE_MASTER.TAX1_Amt ,TSPL_ITEM_PRICE_MASTER.TAX2_Amt,TSPL_ITEM_PRICE_MASTER.TAX3_Amt,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.TAX5_Amt,TSPL_ITEM_PRICE_MASTER.TAX6_Amt,TSPL_ITEM_PRICE_MASTER.TAX7_Amt,TSPL_ITEM_PRICE_MASTER.TAX8_Amt,TSPL_ITEM_PRICE_MASTER.TAX9_Amt,TSPL_ITEM_PRICE_MASTER.TAX10_Amt,TSPL_ITEM_PRICE_MASTER.Abatement  "
            strViewQuery += " FROM TSPL_ITEM_PRICE_MASTER "
            strViewQuery += " INNER join TSPL_ITEM_MASTER ON TSPL_ITEM_MASTER.Item_Code = TSPL_ITEM_PRICE_MASTER.Item_Code  "
            strViewQuery += " INNER JOIN ("
            strViewQuery += " SELECT Item_Code, UOM, MAX(Start_Date) AS MaxDateTime, Item_Basic_Net,  Price_Code, Tax_group  FROM  dbo.TSPL_ITEM_PRICE_MASTER WHERE Start_Date<=  '" + clsCommon.GetPrintDate(txtTransferDate.Value, "dd/MMM/yyyy") + "'    GROUP BY Item_Code, UOM, Item_Basic_Net,  Price_Code, Tax_group  having 2=2 "
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Excise") = CompairStringResult.Equal Then
                strViewQuery += " and Tax_group='" + clsCommon.myCstr(fndTaxGroup.Value) + "'"
            End If
            strViewQuery += " ) AS groupedP ON TSPL_ITEM_PRICE_MASTER.Item_Code = groupedP.Item_Code AND  TSPL_ITEM_PRICE_MASTER.Start_Date = groupedP.MaxDateTime AND TSPL_ITEM_PRICE_MASTER.UOM = groupedP.UOM AND TSPL_ITEM_PRICE_MASTER.Item_Basic_Net = groupedP.Item_Basic_Net  AND TSPL_ITEM_PRICE_MASTER.Price_Code = groupedP.Price_Code and TSPL_ITEM_PRICE_MASTER.Tax_group = groupedP.Tax_group"


            If itemtype Is Nothing Then
                sql = "SELECT Item_Code,Item_Desc,UOM,Start_Date,Item_Basic_Net,Item_Basic_Price ,Empty_Value_Bottle , Empty_Value_Shell,ISNULL(TAX1_Amt,0) AS TAX1_Amt ,ISNULL(TAX2_Amt,0) AS TAX2_Amt, ISNULL(TAX3_Amt,0) AS TAX3_Amt, ISNULL(TAX4_Amt,0) AS TAX4_Amt, ISNULL(TAX5_Amt,0) AS TAX5_Amt, ISNULL(TAX6_Amt,0) AS TAX6_Amt, ISNULL(TAX7_Amt,0) AS TAX7_Amt,ISNULL(TAX8_Amt,0) AS TAX8_Amt,ISNULL(TAX9_Amt,0) AS TAX9_Amt,ISNULL(TAX10_Amt,0) AS TAX10_Amt,ISNULL(TAX1_Rate,0) AS TAX1_Rate,ISNULL(TAX2_Rate,0) AS TAX2_Rate, ISNULL(TAX3_Rate,0) AS TAX3_Rate, ISNULL(TAX4_Rate,0) AS TAX4_Rate, ISNULL(TAX5_Rate,0) AS TAX5_Rate, ISNULL(TAX6_Rate,0) AS TAX6_Rate, ISNULL(TAX7_Rate,0) AS TAX7_Rate,ISNULL(TAX8_Rate,0) AS TAX8_Rate,ISNULL(TAX9_Rate,0) AS TAX9_Rate,ISNULL(TAX10_Rate,0) AS TAX10_Rate, (SELECT Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code = View_TSPL_SHIPMENT_ITEMS.Item_Code AND UOM_Code = View_TSPL_SHIPMENT_ITEMS.UOM ) AS [ConversionFactor],isnull(cast(((case when UOM='FC' then Item_Basic_Net when UOM='EC' then Item_Basic_Net-100 end) /(SELECT Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code = View_TSPL_SHIPMENT_ITEMS.Item_Code AND UOM_Code = case when UOM='FC' then 'FB' when UOM='EC' then 'EB' end )) as decimal (18,2)),0) as [MRPInBottle],Abatement ,case when UOM='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end OrderBy  FROM (" + strViewQuery + ") as View_TSPL_SHIPMENT_ITEMS  Where Price_Code='" + txtPriceCode.Value + "' and ((Item_Type  = 'F' and (UOM ='FC' or UOM ='SH')) or Item_Type  = 'P')   and Item_Code in (select Item_Code  from TSPL_ITEM_LOCATION_DETAILS where Item_Code = View_TSPL_SHIPMENT_ITEMS.Item_Code and MRP = View_TSPL_SHIPMENT_ITEMS.Item_Basic_Net and Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "' and Item_Qty <> 0  ) Order By OrderBy"
            ElseIf itemtype = "FC" Then
                sql = "SELECT Item_Code,Item_Desc,UOM,Start_Date,Item_Basic_Net,Item_Basic_Price ,Empty_Value_Bottle , Empty_Value_Shell,ISNULL(TAX1_Amt,0) AS TAX1_Amt ,ISNULL(TAX2_Amt,0) AS TAX2_Amt, ISNULL(TAX3_Amt,0) AS TAX3_Amt, ISNULL(TAX4_Amt,0) AS TAX4_Amt, ISNULL(TAX5_Amt,0) AS TAX5_Amt, ISNULL(TAX6_Amt,0) AS TAX6_Amt, ISNULL(TAX7_Amt,0) AS TAX7_Amt,ISNULL(TAX8_Amt,0) AS TAX8_Amt,ISNULL(TAX9_Amt,0) AS TAX9_Amt,ISNULL(TAX10_Amt,0) AS TAX10_Amt,ISNULL(TAX1_Rate,0) AS TAX1_Rate,ISNULL(TAX2_Rate,0) AS TAX2_Rate, ISNULL(TAX3_Rate,0) AS TAX3_Rate, ISNULL(TAX4_Rate,0) AS TAX4_Rate, ISNULL(TAX5_Rate,0) AS TAX5_Rate, ISNULL(TAX6_Rate,0) AS TAX6_Rate, ISNULL(TAX7_Rate,0) AS TAX7_Rate,ISNULL(TAX8_Rate,0) AS TAX8_Rate,ISNULL(TAX9_Rate,0) AS TAX9_Rate,ISNULL(TAX10_Rate,0) AS TAX10_Rate, (SELECT Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code = View_TSPL_SHIPMENT_ITEMS.Item_Code AND UOM_Code = View_TSPL_SHIPMENT_ITEMS.UOM ) AS [ConversionFactor],isnull(cast((Item_Basic_Net /(SELECT Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code = View_TSPL_SHIPMENT_ITEMS.Item_Code AND UOM_Code = 'FB' ))as decimal(18,2)),0) as [MRPInBottle],Abatement,case when UOM='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end OrderBy  FROM (" + strViewQuery + ") as View_TSPL_SHIPMENT_ITEMS  Where Price_Code='" + txtPriceCode.Value + "' and ((Item_Type  = 'F' and (UOM ='FC' or UOM ='SH')) or Item_Type  = 'P')   and Item_Code in (select Item_Code  from TSPL_ITEM_LOCATION_DETAILS where Item_Code = View_TSPL_SHIPMENT_ITEMS.Item_Code and MRP = View_TSPL_SHIPMENT_ITEMS.Item_Basic_Net and Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "' and Item_Qty <> 0  ) Order By OrderBy"
            ElseIf itemtype = "EC" Then
                sql = "SELECT Item_Code,Item_Desc,UOM,Start_Date,Item_Basic_Net,Item_Basic_Price ,Empty_Value_Bottle , Empty_Value_Shell,ISNULL(TAX1_Amt,0) AS TAX1_Amt ,ISNULL(TAX2_Amt,0) AS TAX2_Amt, ISNULL(TAX3_Amt,0) AS TAX3_Amt, ISNULL(TAX4_Amt,0) AS TAX4_Amt, ISNULL(TAX5_Amt,0) AS TAX5_Amt, ISNULL(TAX6_Amt,0) AS TAX6_Amt, ISNULL(TAX7_Amt,0) AS TAX7_Amt,ISNULL(TAX8_Amt,0) AS TAX8_Amt,ISNULL(TAX9_Amt,0) AS TAX9_Amt,ISNULL(TAX10_Amt,0) AS TAX10_Amt,ISNULL(TAX1_Rate,0) AS TAX1_Rate,ISNULL(TAX2_Rate,0) AS TAX2_Rate, ISNULL(TAX3_Rate,0) AS TAX3_Rate, ISNULL(TAX4_Rate,0) AS TAX4_Rate, ISNULL(TAX5_Rate,0) AS TAX5_Rate, ISNULL(TAX6_Rate,0) AS TAX6_Rate, ISNULL(TAX7_Rate,0) AS TAX7_Rate,ISNULL(TAX8_Rate,0) AS TAX8_Rate,ISNULL(TAX9_Rate,0) AS TAX9_Rate,ISNULL(TAX10_Rate,0) AS TAX10_Rate, (SELECT Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code = View_TSPL_SHIPMENT_ITEMS.Item_Code AND UOM_Code = View_TSPL_SHIPMENT_ITEMS.UOM ) AS [ConversionFactor] ,isnull(cast(((Item_Basic_Net-100) /(SELECT Conversion_Factor  FROM TSPL_ITEM_UOM_DETAIL WHERE Item_Code = View_TSPL_SHIPMENT_ITEMS.Item_Code AND UOM_Code = 'EB' ))as decimal(18,2)),0) as [MRPInBottle],Abatement,case when UOM='SH' then ( select max(Sku_Seq)+1  from TSPL_ITEM_MASTER) else Sku_Seq end OrderBy FROM (" + strViewQuery + ") as View_TSPL_SHIPMENT_ITEMS  Where Price_Code='" + txtPriceCode.Value + "' and ((Item_Type  = 'F' and (UOM ='EC' or UOM ='SH')) or Item_Type  = 'P')  and Item_Code in (select Item_Code  from TSPL_ITEM_LOCATION_DETAILS where Item_Code = View_TSPL_SHIPMENT_ITEMS.Item_Code and MRP = View_TSPL_SHIPMENT_ITEMS.Item_Basic_Net and Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "' and Item_Qty <> 0  ) Order By OrderBy"
            End If

            Dim pdDR As DataTable = clsDBFuncationality.GetDataTable(sql)
            Dim i As Integer = 0
            If pdDR.Rows.Count > 0 Then
                gv1.Enabled = True
                'gv1.AllowAddNewRow = True
                For Each row As DataRow In pdDR.Rows
                    If IsChkInGrid = True Then
                        Dim ItemCode As String = row(0).ToString()

                        Dim Mrp As Double = clsCommon.myCdbl(row(4))

                        Dim isFound As Boolean = False
                        For ii As Integer = 0 To gv1.Rows.Count - 1
                            Dim grdItm As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value)

                            Dim grdMrp As Double = clsCommon.myCstr(gv1.Rows(ii).Cells(colMRP).Value)

                            If clsCommon.myLen(grdItm) > 0 Then
                                If clsCommon.CompairString(grdItm, ItemCode) = CompairStringResult.Equal AndAlso grdMrp = Mrp Then
                                    isFound = True
                                    Exit For
                                End If
                            End If
                        Next
                        If isFound Then
                            Continue For
                        End If

                    End If

                    Dim datarowinfo As GridViewRowInfo = gv1.Rows.AddNew()
                    datarowinfo.Cells(colItemCode).Value = row(0).ToString()
                    datarowinfo.Cells(colItemName).Value = row(1).ToString()
                    datarowinfo.Cells(colUOM).Value = row(2).ToString()



                    Dim basicprice, emptyvalue As String
                    'Dim tptdr As SqlDataReader
                    'Dim tpt As String


                    Dim qry As String = "SELECT case when PC1.TPT_Type='Y' then ISNULL(Price_Amount1,0) else 0 end"
                    qry += " + case when PC2.TPT_Type='Y' then ISNULL(Price_Amount2,0) else 0 end "
                    qry += " + case when PC3.TPT_Type='Y' then ISNULL(Price_Amount3,0) else 0 end "
                    qry += " + case when PC4.TPT_Type='Y' then ISNULL(Price_Amount4,0) else 0 end "
                    qry += " + case when PC5.TPT_Type='Y' then ISNULL(Price_Amount5,0) else 0 end "
                    qry += " + case when PC6.TPT_Type='Y' then ISNULL(Price_Amount6,0) else 0 end "
                    qry += " + case when PC7.TPT_Type='Y' then ISNULL(Price_Amount7,0) else 0 end "
                    qry += " + case when PC8.TPT_Type='Y' then ISNULL(Price_Amount8,0) else 0 end "
                    qry += " + case when PC9.TPT_Type='Y' then ISNULL(Price_Amount9,0) else 0 end "
                    qry += " + case when PC10.TPT_Type='Y' then ISNULL(Price_Amount10,0) else 0 end "
                    qry += " FROM TSPL_ITEM_PRICE_MASTER "
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC1 on PC1.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp1"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC2 on PC2.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp2"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC3 on PC3.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp3"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC4 on PC4.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp4"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC5 on PC5.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp5"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC6 on PC6.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp6"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC7 on PC7.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp7"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC8 on PC8.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp8"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC9 on PC9.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp9"
                    qry += " left outer join TSPL_PRICE_COMPONENT_MASTER as PC10 on PC10.Price_Comp_code=TSPL_ITEM_PRICE_MASTER.Price_Comp10"
                    qry += " Where Price_Code='" + txtPriceCode.Value + "' and Item_Code = '" + datarowinfo.Cells(colItemCode).Value + "' AND Item_Basic_Net ='" + row("Item_Basic_Net").ToString() + "' AND Item_Basic_Price ='" + row("Item_Basic_Price").ToString() + "'"


                    datarowinfo.Cells(colTPT).Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                    emptyvalue = connectSql.RunScalar("SELECT isnull((Empty_Value_Bottle+Empty_Value_Shell),0) as total  FROM View_TSPL_SHIPMENT_ITEMS  Where Price_Code='" + txtPriceCode.Value + "' and Item_Code = '" + datarowinfo.Cells(colItemCode).Value + "' AND Item_Basic_Net ='" + row("Item_Basic_Net").ToString() + "' AND Item_Basic_Price ='" + row("Item_Basic_Price").ToString() + "' Order By Sku_Seq")

                    basicprice = connectSql.RunScalar("select case when TSPL_ITEM_MASTER.NoMRP=1 then  NetLTPT+(isnull(TAX1_Amt,0)+isnull(TAX2_Amt,0) +isnull(TAX3_Amt,0) +isnull(TAX4_Amt,0) +isnull(TAX5_Amt,0) +isnull(TAX6_Amt,0) +isnull(TAX7_Amt,0)+isnull(TAX8_Amt,0)+isnull(TAX9_Amt,0)+isnull(TAX10_Amt,0)) else NetLTPT end   from TSPL_ITEM_PRICE_MASTER inner join TSPL_ITEM_MASTER on TSPL_ITEM_PRICE_MASTER.Item_Code =TSPL_ITEM_MASTER.Item_Code Where Price_Code='" + txtPriceCode.Value + "' AND Item_Basic_Net = '" + row("Item_Basic_Net").ToString() + "' AND Item_Basic_Price = '" + row("Item_Basic_Price").ToString() + "' AND TSPL_ITEM_PRICE_MASTER.Item_Code = '" + datarowinfo.Cells(colItemCode).Value + "'")

                    datarowinfo.Cells(colEmptyValue).Value = clsCommon.myCdbl(emptyvalue)
                    datarowinfo.Cells(colBasicPriceWithTax).Value = basicprice
                    Dim colPDate As GridViewComboBoxColumn = TryCast(gv1.Columns(colPriceDate), GridViewComboBoxColumn)
                    sql = "Select distinct CONVERT(varchar(10), Start_Date, 103) as Start_Date from TSPL_ITEM_PRICE_MASTER Where item_code='" + datarowinfo.Cells(colItemCode).Value.ToString() + "' and Price_Code ='" + txtPriceCode.Value + "'"
                    ds = connectSql.RunSQLReturnDS(sql)
                    colPDate.ValueMember = "Start_Date"
                    colPDate.DataSource = ds.Tables(0)
                    datarowinfo.Cells(colPriceDate).Value = row(3).ToString()
                    datarowinfo.Cells(colMRP).Value = row(4).ToString()
                    datarowinfo.Cells(colMRPInBottel).Value = row("MRPInBottle").ToString()

                    datarowinfo.Cells(colBatchNo).Value = connectSql.RunScalar("SELECT Batch_No  FROM TSPL_ITEM_LOCATION_DETAILS WHERE Item_Code = '" + Convert.ToString(datarowinfo.Cells(colItemCode).Value) + "' AND Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "' AND MRP = '" + Convert.ToString(datarowinfo.Cells(colMRP).Value) + "' AND Batch_No <> ''")

                    datarowinfo.Cells(colAssessableAmt).Value = Math.Round(clsCommon.myCdbl(row("Abatement")), 2)
                    Dim itemqty As Decimal = 0.0
                    Dim amt As Decimal = 0.0
                    Dim stockqty As Decimal = 0.0
                    Dim itemcost As Decimal = 0.0
                    gv1.Columns(colPendingQty).HeaderText = "Stock Qty"
                    sql = "SELECT ISNULL(SUM(ISNULL(ITEM_QTY,0)),0) FROM TSPL_ITEM_LOCATION_DETAILS WHERE Item_Code='" + row(0).ToString() + "' AND  Location_Code='" + Convert.ToString(txtFromLoaction.Value) + "' and MRP='" + datarowinfo.Cells(colMRP).Value.ToString() + "'"
                    stockqty = clsCommon.myCdbl(connectSql.RunScalar(sql))
                    If Not IsDBNull(connectSql.RunScalar(sql)) And connectSql.RunScalar(sql) <> Nothing Then
                        datarowinfo.Cells(colPendingQty).Value = connectSql.RunScalar(sql)
                    Else
                        datarowinfo.Cells(colPendingQty).Value = 0.0
                    End If

                    itemcost = clsCommon.myCdbl(connectSql.RunScalar("SELECT case when ISNULL(SUM(ISNULL(Item_Qty,0)),0)>0 then ISNULL(SUM(ISNULL(Amount,0)),0)/ISNULL(SUM(ISNULL(Item_Qty,0)),0) else 0 end   AS [Cost] FROM TSPL_ITEM_LOCATION_DETAILS where Item_Code='" + datarowinfo.Cells(colItemCode).Value.ToString() + "'  AND location_code='" + Convert.ToString(txtFromLoaction.Value) + "' AND MRP='" + datarowinfo.Cells(colMRP).Value.ToString() + "' "))
                    Dim cost As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select item_price from TSPL_INDENT_DETAIL where Indent_No ='" + txtTransferNo.Value + "'and item_code='" + datarowinfo.Cells(colItemCode).Value.ToString() + "'"))
                    If cost <> 0 Then
                        datarowinfo.Cells(colItemCost).Value = cost
                    Else
                        datarowinfo.Cells(colItemCost).Value = Math.Round(itemcost, 2)
                    End If
                    datarowinfo.Cells(colTransferQty).Value = "0"
                    datarowinfo.Cells(colLoadInQty).Value = "0"
                    datarowinfo.Cells(colBreakage).Value = "0"
                    datarowinfo.Cells(colAmount).Value = "0"
                    datarowinfo.Cells(colTax).Value = Math.Round(calculateItemTax(clsCommon.myCdbl(row(4).ToString())), 2)
                    datarowinfo.Cells(colTotal).Value = "0"
                    datarowinfo.Cells(colBasicPrice).Value = Math.Round(clsCommon.myCdbl(row(5).ToString()), 2)
                    datarowinfo.Cells(colLeak).Value = "0"
                    datarowinfo.Cells(colShortage).Value = "0"
                    datarowinfo.Cells(colConversion).Value = Convert.ToString(row("ConversionFactor"))

                    datarowinfo.Cells(colTaxRate1).Value = Convert.ToString(row("TAX1_Rate"))
                    datarowinfo.Cells(colTaxRate2).Value = Convert.ToString(row("TAX2_Rate"))
                    datarowinfo.Cells(colTaxRate3).Value = Convert.ToString(row("TAX3_Rate"))
                    datarowinfo.Cells(colTaxRate4).Value = Convert.ToString(row("TAX4_Rate"))
                    datarowinfo.Cells(colTaxRate5).Value = Convert.ToString(row("TAX5_Rate"))
                    datarowinfo.Cells(colTaxRate6).Value = Convert.ToString(row("TAX6_Rate"))
                    datarowinfo.Cells(colTaxRate7).Value = Convert.ToString(row("TAX7_Rate"))
                    datarowinfo.Cells(colTaxRate8).Value = Convert.ToString(row("TAX8_Rate"))
                    datarowinfo.Cells(colTaxRate9).Value = Convert.ToString(row("TAX9_Rate"))
                    datarowinfo.Cells(colTaxRate10).Value = Convert.ToString(row("TAX10_Rate"))
                Next
                cboTransferType.Enabled = False
                'cmbitemtype.Enabled = False
                txtFromLoaction.Enabled = False
                txtPriceCode.Enabled = False
                txtRouteNo.Enabled = False
            End If
            'gv1.AllowAddNewRow = True
        End If
        funSetFirstRow()
    End Sub

    Public Sub New(ByVal user As String, ByVal company As String)
        InitializeComponent()
        objCommonVar.CurrentUserCode = user
        objCommonVar.CurrentCompanyCode = company
        Dim Sql As String = "SELECT  User_Type,Level1_Code, Level2_Code, Level3_Code, Level4_Code  FROM TSPL_USER_MASTER WHERE User_Code='" + objCommonVar.CurrentUserCode + "'"
        dr = clsDBFuncationality.GetDataTable(Sql)
        For Each row As DataRow In dr.Rows
            l1User = row(0).ToString()
            l2User = row(1).ToString()
            l3User = row(2).ToString()
            l4User = row(3).ToString()
            l5User = row(4).ToString()
        Next
    End Sub

    Private Sub CHKSHELL_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkApplyShell.ToggleStateChanged
        isInsideLoadData = True
        If chkApplyShell.Checked = True Then
            FunAddShellItem()
        Else
            FunDeleteShellItem()
        End If
        isInsideLoadData = False
    End Sub

    Private Sub fndtolocation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FunLoadToLocationPhysical()
    End Sub

    Private Sub NumericKeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If gv1.CurrentColumn.Name = colTransferQty Or gv1.CurrentColumn.Name = colLoadInQty Or gv1.CurrentColumn.Name = colBreakage Or gv1.CurrentColumn.Name = colLeak Or gv1.CurrentColumn.Name = colShortage Then
            e.Handled = globalFunc.TrapKey(Asc(e.KeyChar))
        End If
    End Sub

    Private Sub rdtxtsalesmancode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        FunEmpName()
    End Sub

    Private Sub fndfromlocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtFromLoaction._MYValidating
        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cmbitemtype.Text), "Full") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(cboType.SelectedValue)) <= 0 Then
            txtFromLoaction.Value = ""
            clsCommon.MyMessageBoxShow(Me, "Please select Type", Me.Text)
            cboType.Focus()
            Exit Sub
        End If

        Dim qry As String = "Select  LM.Location_Code as Code,LM.Location_Desc as Description,Location_type as 'Location Type',(case LM.Excisable when 'T'then 'Excisable'else 'Non-Excisable'end) as 'Excisable'  from TSPL_LOCATION_MASTER as LM "
        Dim whrclas As String = " LM.Location_Type ='Physical'"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                whrclas += " and LM.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Excise") = CompairStringResult.Equal Then
            whrclas += " and LM.Excisable ='T'"
        ElseIf clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Depot") = CompairStringResult.Equal OrElse clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Route") = CompairStringResult.Equal Then
            whrclas += " and LM.Excisable <>'T'"
        End If
        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
            whrclas += " and LM.GIT_Type='N'"
        End If


        txtFromLoaction.Value = clsCommon.ShowSelectForm("IndentFromLoc", qry, "Code", whrclas, txtFromLoaction.Value, "Code", isButtonClicked)
        lblFromLocation.Text = connectSql.RunScalar("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "'")
        routeEnableFromLoc()
        fromLocPostEnable()

        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtFromLoaction.Value) > 0 Then
            cboType.Enabled = False
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Route") = CompairStringResult.Equal Then
            txtRouteNo.Focus()
        End If
    End Sub

    Private Sub FromLocDesc()
        lblFromLocation.Text = connectSql.RunScalar("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtFromLoaction.Value) + "'")
    End Sub

    Private Sub routeEnableFromLoc()
        Dim CheckExcisable As String = connectSql.RunScalar("select excisable from TSPL_LOCATION_MASTER  where Location_Type  ='Physical' and location_code='" + Convert.ToString(txtFromLoaction.Value) + "'")
        If CheckExcisable = "F" Then
            'fndTaxGroup.Enabled = False
            gv2.ReadOnly = True
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Depot") = CompairStringResult.Equal Then
                txtRouteNo.Enabled = False
            Else
                txtRouteNo.Enabled = True
            End If


            chkAgainstFForm.Enabled = False
        Else
            txtRouteNo.Enabled = False
            chkAgainstFForm.Enabled = True
            If cmbitemtype.SelectedIndex = 1 Then
                'fndTaxGroup.Enabled = True
                gv2.ReadOnly = False
                fndTaxGroup.MendatroryField = True
            ElseIf cmbitemtype.SelectedIndex = 2 Then
                'fndTaxGroup.Enabled = False
                gv2.ReadOnly = True
                fndTaxGroup.MendatroryField = False
            End If
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Excise") = CompairStringResult.Equal Then
                txtRouteNo.Enabled = True
            Else
                txtRouteNo.Enabled = False
            End If

        End If
    End Sub

    Public Sub fndtolocation_Leave_1()
        Dim sql2 As String = "Select Location_Desc from TSPL_location_master where location_code ='" + txtToLocation.Value.Trim() + "'"
        Dim tolocExcise As String = connectSql.RunScalar(sql2)
        lblToLocation.Text = tolocExcise
        'If tolocExcise = "T" And cmbitemtype.SelectedIndex <> 2 Then
        '    common.clsCommon.MyMessageBoxShow("To Location Cann't be Excisable")
        '    fndtolocation.Value = ""
        '    Exit Sub
        'End If
    End Sub

    Private Sub fndtolocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtToLocation._MYValidating
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Route") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtRouteNo.Value) <= 0 Then
            clsCommon.MyMessageBoxShow(Me, "Please first select Route", Me.Text)
            txtToLocation.Value = ""
            txtRouteNo.Focus()
            Exit Sub
        End If

        Dim qry As String = "Select  LM.Location_Code as Code,LM.Location_Desc as Description,Location_type as 'Location Type',(case LM.Excisable when 'T'then 'Excisable'else 'Non-Excisable'end) as 'Excisable',GIT_Location as GITLocation from TSPL_LOCATION_MASTER as LM "
        Dim whrclas As String = "2=2"

        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                whrclas += " and LM.Excisable ='F' AND LM.Location_Type ='Logical'"
            Else
                whrclas += " and LM.Location_Type ='Physical' and GIT_Type='N'"
            End If
            txtToLocation.Value = clsCommon.ShowSelectForm("IndentToLoc", qry, "Code", whrclas, txtToLocation.Value, "Code", isButtonClicked)
        ElseIf clsLocation.isLocatinExcisable(txtFromLoaction.Value) AndAlso clsCommon.myLen(txtRouteNo.Value) > 0 Then
            whrclas += " and LM.Location_Type ='Physical' and len(GIT_location)>0"
            txtToLocation.Value = clsCommon.ShowSelectForm("IndentToLoc", qry, "GITLocation", whrclas, txtToLocation.Value, "GITLocation", isButtonClicked)
        Else

            whrclas += " and LM.Excisable<>'T'"
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                whrclas += " and LM.Excisable ='F' AND LM.Location_Type ='Logical'"
                txtToLocation.Value = clsCommon.ShowSelectForm("IndentToLoc", qry, "Code", whrclas, txtToLocation.Value, "Code", isButtonClicked)
            Else
                whrclas += " and LM.Location_Type ='Physical' and len(GIT_location)>0"
                txtToLocation.Value = clsCommon.ShowSelectForm("IndentToLoc", qry, "GITLocation", whrclas, txtToLocation.Value, "GITLocation", isButtonClicked)
            End If

        End If

        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrclas += " and LM.Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If



        txtSalesman.Text = txtToLocation.Value
        lblToLocation.Text = connectSql.RunScalar("SELECT Location_Desc  FROM TSPL_LOCATION_MASTER WHERE Location_Code = '" + Convert.ToString(txtToLocation.Value) + "'")
        routeEnableToLoc()
    End Sub

    Private Sub routeEnableToLoc()
        Dim sql2 As String = "Select Location_Type from TSPL_location_master where location_code ='" + txtToLocation.Value.Trim() + "'"
        Dim tolocExcise As String = connectSql.RunScalar(sql2)
        Dim sql22 As String = "Select Location_Type from TSPL_location_master where location_code ='" + txtFromLoaction.Value.Trim() + "'"
        Dim fromlocExcise As String = connectSql.RunScalar(sql22)
        If tolocExcise = "Physical" And fromlocExcise = "Physical" And cmbitemtype.SelectedIndex <> 2 Then
            txtRouteNo.Enabled = False
            'common.clsCommon.MyMessageBoxShow("To Location Cann't be Excisable")
            'fndtolocation.Value = ""
        Else
            txtRouteNo.Enabled = True
        End If
    End Sub

    Private Sub fndrouteno__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtRouteNo._MYValidating
        If IsRouteFillAgain = False Then
            Dim qry As String = "select route_no as RouteNo,route_desc as [Description],employee_code as [Sales man Code],RoutePrice_Code as [Route Price Code] from TSPL_ROUTE_MASTER"
            Dim whrClas As String = "status='A'"
            If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Excise") = CompairStringResult.Equal Then
                whrClas += " and len(ISNULL(RoutePrice_Code,''))>0"
            End If
            txtRouteNo.Value = clsCommon.ShowSelectForm("IndRouteNo", qry, "RouteNo", whrClas, txtRouteNo.Value, "", isButtonClicked)

            If txtRouteNo.Value <> "" Then
                If txtFromLoaction.Value = "" Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please choose location first.", Me.Text)
                    txtRouteNo.Value = String.Empty
                    txtFromLoaction.Focus()
                    txtRouteNo.Value = ""
                End If
            End If
            lblRouteNo.Text = connectSql.RunScalar("Select  route_desc from tspl_route_master where route_no ='" + txtRouteNo.Value + "'")
            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                Dim myreader As DataTable = clsDBFuncationality.GetDataTable("Select employee_code,price_code,NonPrice_Code,vehicle_code,RoutePrice_Code from tspl_route_master where route_no = '" + txtRouteNo.Value + "' ")
                If myreader IsNot Nothing AndAlso myreader.Rows.Count > 0 Then
                    txtSalesman.Text = Convert.ToString(myreader.Rows(0)("employee_code"))
                    txtToLocation.Value = Convert.ToString(myreader.Rows(0)("employee_code"))

                    If clsLocation.isLocatinExcisable(txtFromLoaction.Value) Then
                        txtSalesman.Value = clsCommon.myCstr(myreader.Rows(0)("employee_code"))
                        lblSalesman.Text = clsEmployeeMaster.GetName(txtSalesman.Value, Nothing)
                        txtToLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select GIT_Location from TSPL_LOCATION_MASTER where Location_Code='" + txtFromLoaction.Value + "'"))

                        txtPriceCode.Value = Convert.ToString(myreader.Rows(0)("RoutePrice_Code"))

                        If clsCommon.myLen(txtPriceCode.Value) <= 0 Then
                            clsCommon.MyMessageBoxShow(Me, "Please set Route Price code of route " + txtRouteNo.Value, Me.Text)
                            txtRouteNo.Value = ""
                            lblRouteNo.Text = ""
                            Exit Sub
                        End If
                        PriceChange()
                    Else
                        txtPriceCode.Value = Convert.ToString(myreader.Rows(0)("NonPrice_Code"))
                        PriceChange()

                    End If
                    txtVehicleCode.Value = Convert.ToString(myreader.Rows(0)("vehicle_code"))
                    txtVehicle.Text = txtVehicleCode.Value
                    IsRouteFillAgain = True
                End If

                txtPriceCode.Enabled = False
                txtRouteNo.Enabled = False
                cboPriceDate.Enabled = True
                IsRouteFillAgain = False
            Else
                txtRouteNo.Enabled = True
                txtPriceCode.Enabled = True
                txtPriceCode.Value = ""
            End If
            ToLocDesc()
            VechileDesc()
            PriceCodeDesc()
            fromLocPostEnable()

        End If
        'common.clsCommon.MyMessageBoxShow("" + TT + " AND " + clsCommon.myCstr(System.DateTime.Now.Minute) + ":" + clsCommon.myCstr(System.DateTime.Now.Second) + "", "", MessageBoxButtons.OK)
    End Sub

    Private Sub fndvehiclecode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVehicleCode._MYValidating
        Dim qry As String = "Select distinct  vehicle_id ,Description from TSPL_VEHICLE_MASTER"
        txtVehicleCode.Value = clsCommon.ShowSelectForm("IndVehicleNo", qry, "vehicle_id", "", txtVehicleCode.Value, "vehicle_id", isButtonClicked)
        txtVehicle.Text = txtVehicleCode.Value
        lblVehicleDesc.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'")
    End Sub

    Private Sub VechileDesc()
        lblVehicleDesc.Text = connectSql.RunScalar("Select Description  from TSPL_VEHICLE_MASTER where Vehicle_Id = '" + Convert.ToString(txtVehicleCode.Value) + "'")
    End Sub

    Private Sub PriceCodeDesc()
        lblPriceCode.Text = connectSql.RunScalar("select Price_Code_Desc  from TSPL_PRICE_COMPONENT_MAPPING where Price_Code =  '" + Convert.ToString(txtPriceCode.Value) + "'")
    End Sub

    Private Sub PriceChange()
        isInsideLoadData = True
        If cmbitemtype.Text = "Full" Then
            priceDateSelection(False, "FC")
            rdbfc.IsChecked = True
        Else
            priceDateSelection(False, "EC")
            rdbec.IsChecked = True
        End If
        isInsideLoadData = False
        'End If
        txtRouteNo.Enabled = False
    End Sub

    Private Sub fndPriceCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtPriceCode._MYValidating
        Dim qry As String = " SELECT DISTINCT Price_Code  ,Price_Code_Desc  FROM [TSPL_PRICE_COMPONENT_MAPPING] "
        txtPriceCode.Value = clsCommon.ShowSelectForm("IndPriceCode", qry, "Price_Code", "", txtPriceCode.Value, "Price_Code", isButtonClicked)
        lblPriceCode.Text = connectSql.RunScalar("select Price_Code_Desc  from TSPL_PRICE_COMPONENT_MAPPING where Price_Code =  '" + Convert.ToString(txtPriceCode.Value) + "'")
        If lblPriceCode.Text <> "" Then
            PriceChange()

        End If
    End Sub

    Private Sub fndloadno__MYValidating_1(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLoadoutNo._MYValidating
        Dim WhrCls As String = ""
        Dim qry As String = ""
        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
            ' qry = "select distinct TSPL_INDENT_HEAD .Indent_No ,description as 'Description',CONVERT(varchar(15), Indent_Date , 103) as [Transfer Date], From_Location as [From Location], To_Location as [To Location], Reference   from TSPL_INDENT_HEAD inner join TSPL_INDENT_DETAIL on TSPL_INDENT_HEAD.Indent_No=TSPL_INDENT_DETAIL.Indent_No "
            qry = "select distinct Indent_No ,description ,[Transfer Date] ,[From Location] ,[To Location] from (select distinct TSPL_INDENT_HEAD .Indent_No ,description as 'Description',CONVERT(varchar(15), Indent_Date , 103) as [Transfer Date], From_Location as [From Location], To_Location as [To Location], Reference,case when len(Route_No)>0 then From_Location else To_Location end as [Location]   ,Post,Transfer_Type ,Pending_Qty  from TSPL_INDENT_HEAD inner join TSPL_INDENT_DETAIL on TSPL_INDENT_HEAD.Indent_No=TSPL_INDENT_DETAIL.Indent_No ) as Query"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += " and  Query.Location  in(" + objCommonVar.strCurrUserLocations + " ) "
            End If
            txtLoadoutNo.Value = clsCommon.ShowSelectForm("IndLoadOutNO", qry, "Indent_No", "Query.[From Location]  in (Select location_code from TSPL_LOCATION_MASTER) and Post ='Y' and Transfer_Type ='LO' and Pending_Qty > 0 AND Query.Post ='Y' and Query .Indent_No not in(select Load_Out_No from TSPL_INDENT_HEAD  )" + WhrCls + "", txtLoadoutNo.Value, "Indent_No", isButtonClicked)
            If clsCommon.myLen(txtLoadoutNo.Value) <= 0 Then
                Exit Sub
            End If
        Else
            qry = "select distinct TSPL_INDENT_HEAD .Indent_No ,description as 'Description',CONVERT(varchar(15), Indent_Date , 103) as [Transfer Date], From_Location as [From Location], To_Location as [To Location], Reference   from TSPL_INDENT_HEAD inner join TSPL_INDENT_DETAIL on TSPL_INDENT_HEAD.Indent_No=TSPL_INDENT_DETAIL.Indent_No"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += " and To_Location in(" + objCommonVar.strCurrUserLocations + " ) "
            End If
            txtLoadoutNo.Value = clsCommon.ShowSelectForm("IndLoadOutNO", qry, "Indent_No", "From_Location in (Select location_code from TSPL_LOCATION_MASTER) and Post ='Y' and Transfer_Type ='LO' and Pending_Qty > 0 AND  TSPL_INDENT_HEAD.Post ='Y' and TSPL_INDENT_HEAD .Indent_No not in(select Load_Out_No from TSPL_INDENT_HEAD  ) " + WhrCls + "", txtLoadoutNo.Value, "Indent_No", isButtonClicked)
        End If
        Dim LoadOut As String = connectSql.RunScalar("Select distinct TSPL_INDENT_HEAD.Indent_No from TSPL_INDENT_HEAD inner join TSPL_INDENT_DETAIL on TSPL_INDENT_HEAD.Indent_No=TSPL_INDENT_DETAIL.Indent_No  where TSPL_INDENT_HEAD.Indent_No='" + txtLoadoutNo.Value + "' and  Pending_Qty > 0 AND TSPL_INDENT_HEAD.Post ='Y' and TSPL_INDENT_HEAD .Indent_No not in(select Load_Out_No from TSPL_INDENT_HEAD  ) ")
        If LoadOut IsNot Nothing Then
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                itemfunfill_loadoutno()
            ElseIf clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                itemfunfill_loadoutno()
                If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                    'txtTransferDate.Enabled = False
                    txtToLocation.Enabled = False
                Else
                    'txtTransferDate.Enabled = True
                    txtToLocation.Enabled = True
                End If
            End If
            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                FunFillItemDetails_LoadOut_No()
                rdball.IsChecked = True
                chkApplyShell.Enabled = True
            End If

        End If
        ToLocDesc()
        VechileDesc()
        PriceCodeDesc()
        FromLocDesc()
        LoadPendingBalanceAgainstTransfer()
        fromLocPostEnable()
        txtLoadoutNo.Enabled = False
        cboTransferType.Enabled = False
    End Sub

    Private Sub DeleteLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteLayout.Click
        clsGridLayout.DeleteData(GetReportID(), objCommonVar.CurrentUserCode)
    End Sub

    Private Sub SaveLayout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLayoutbtn.Click
        If clsCommon.myLen(GetReportID()) > 0 Then
            gv1.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = GetReportID()
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv1.SaveLayout(obj.GridLayout)
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            obj.GridColumns = gv1.ColumnCount
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully",  Me.Text)
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(GetReportID()) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(GetReportID(), "", objCommonVar.CurrentUserCode), clsGridLayout)
                If Not obj Is Nothing AndAlso obj.GridColumns >= gv1.ColumnCount Then
                    Dim ii As Integer
                    For ii = 0 To gv1.Columns.Count - 1 Step ii + 1
                        gv1.Columns(ii).IsVisible = False
                        gv1.Columns(ii).VisibleInColumnChooser = True
                    Next

                    gv1.LoadLayout(obj.GridLayout)
                    obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
                End If

                repoTransferQty.ReadOnly = False
                repoLoadinQty.ReadOnly = False
                repoBreakageQty.ReadOnly = False
                repoLeakageQty.ReadOnly = False
                repoShortageQty.ReadOnly = False
            End If
        Catch err As Exception
            MessageBox.Show(err.Message)
        End Try
    End Sub

    Private Function GetReportID() As String
        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
            Return "IndentMainGrid"
        Else
            Return "IndentInMainGrid"
        End If
    End Function

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        PrintDataNew(False)
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        'PrintDataNew(True)
    End Sub

    Private Sub txtVehicle_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVehicle.Leave

        txtVehicleCode.Value = txtVehicle.Text

        VechileDesc()
    End Sub

    Private Sub txtVehicle_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVehicle.TextChanged

    End Sub

    Private Sub fromLocPostEnable()
        Dim CheckLogical As String = connectSql.RunScalar(" select Location_Type  from TSPL_LOCATION_MASTER where Location_Code ='" + Convert.ToString(txtFromLoaction.Value) + "'")
        If CheckLogical = "Logical" Then
            rdbtnPost.Enabled = False
        Else
            rdbtnPost.Enabled = True
        End If
    End Sub


    Private Sub txttoloc_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblToLocation.Click

    End Sub

    Private Sub lblloadoutdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblloadoutdate.Click

    End Sub

    Private Function AllowToSave() As Boolean
        Dim location_type1 = connectSql.RunScalar("select location_type from TSPL_LOCATION_master where Location_Code ='" + Convert.ToString(txtToLocation.Value) + "'")
        Dim exicisable As String = connectSql.RunScalar("Select Excisable from tspl_location_Master where Location_code='" + Convert.ToString(txtFromLoaction.Value) + "'")
        Dim IsPOst As Decimal = clsCommon.myCdbl(connectSql.RunScalar("select 1 from TSPL_INDENT_HEAD where Indent_No ='" + txtTransferNo.Value + "' and Post ='1'"))
        If IsPOst = 1 Then
            Throw New Exception("Document Already Posted.")
        End If
        'If clsCommon.myLen(txtRouteNo.Value) > 0 And clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal And isNewEntry Then
        '    Dim sqlRouteCheck As String = "select 1  from TSPL_INDENT_HEAD where Route_No ='" + txtRouteNo.Value + "' and convert(date,Indent_Date,103) = convert(date,'" + clsCommon.myCDate(txtTransferDate.Value, "dd/MM/yyyy") + "',103)and Transfer_Type ='LO' and LEN(Route_No )>0"
        '    Dim DuplicateFlag As Integer = clsDBFuncationality.getSingleValue(sqlRouteCheck)
        '    If DuplicateFlag = 1 Then
        '        Dim frm As New FrmPWD(Nothing)
        '        If clsCommon.MyMessageBoxShow("Do you want to create another Loadout for RouteNo:'" + txtRouteNo.Value + "' on Date '" + clsCommon.myCDate(txtTransferDate.Value, "dd/MM/yyyy") + "'? ", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
        '            frm.strCode = "Duplicate Route"
        '            frm.strType = "Route"
        '            frm.ShowDialog()
        '        End If
        '        If Not frm.isPasswordCorrect Then
        '            Return False
        '        End If
        '    End If
        'End If
        Dim qry As String = ""

        'If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Route") = CompairStringResult.Equal Then
        '    qry = "select Indent_No from TSPL_INDENT_HEAD where To_Location='" + txtToLocation.Value + "' and Transfer_Type='LO' and  Indent_No not in (select Load_Out_No from TSPL_INDENT_HEAD) and Indent_No not in ('" + txtTransferNo.Value + "')"
        '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)

        '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
        '        Dim strMsg As String = "Please First Make Loadin of Loadout no" + Environment.NewLine
        '        For Each dr As DataRow In dt.Rows
        '            strMsg += clsCommon.myCstr(dr("Indent_No")) + Environment.NewLine
        '        Next
        '        Throw New Exception(strMsg)
        '    End If
        'End If
        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
            qry = "select EntryDateTime  from TSPL_INDENT_HEAD where Indent_No='" + txtLoadoutNo.Value + "'"
            If clsCommon.myCDate(txtTransferDate.Value, "dd/MMM/yyyy") < clsCommon.myCDate(clsDBFuncationality.getSingleValue(qry), "dd/MMM/yyyy") Then
                Throw New Exception("Loadin Date can't be less than Loadout Date")
            End If
        End If

        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal AndAlso clsCommon.CompairString(clsCommon.myCstr(cmbitemtype.Text), "Full") = CompairStringResult.Equal AndAlso clsCommon.myLen(clsCommon.myCstr(cboType.SelectedValue)) <= 0 Then
            cboType.Focus()
            Throw New Exception("Please select Type")
        ElseIf cmbitemtype.SelectedIndex = 0 Then
            cmbitemtype.Focus()
            Throw New Exception("Select The Item Type")
        ElseIf cboTransferType.SelectedIndex = 0 Then
            cboTransferType.Focus()
            Throw New Exception("Select The Transfer Type")
        ElseIf txtRouteNo.Value = "" And location_type1 = "Logical" Then
            txtRouteNo.Focus()
            Throw New Exception("Route No can't be blank")
        ElseIf cboModeofTransport.SelectedIndex = 0 Then
            cboModeofTransport.Focus()
            Throw New Exception("Mode Of Transport Can't be Blank")
        ElseIf txtFromLoaction.Value = String.Empty Then
            txtFromLoaction.Focus()
            Throw New Exception("From Location can't be Blank")
        ElseIf txtToLocation.Value = String.Empty Then
            txtToLocation.Focus()
            Throw New Exception("To Location can't be Blank")
        ElseIf txtTripNo.Text = String.Empty Then
            txtTripNo.Focus()
            Throw New Exception("Trip No can't be Blank")
            'ElseIf txtKMReading.Text = String.Empty Then
            '    txtKMReading.Focus()
            '    Throw New Exception("KM Reading can't be Blank")
        ElseIf clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal And txtLoadoutNo.Value = String.Empty Then
            txtLoadoutNo.Focus()
            Throw New Exception("LoadOut No. can't be Blank")
        End If
        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal AndAlso clsCommon.myLen(txtRouteNo.Value) > 0 AndAlso location_type1 = "Logical" Then
            qry = "select 1 from TSPL_SALESMAN_MAPPING  where Salesman_Code='" + txtRouteNo.Value + "'"
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                Throw New Exception("Please map Route Hierarchy")
            End If
        End If
        If (clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal And clsCommon.myLen(txtRouteNo.Value) = 0) Then
            Dim LoadOutDate As Date = clsCommon.myCDate(connectSql.RunScalar("Select distinct TSPL_INDENT_HEAD.Indent_Date from TSPL_INDENT_HEAD   where TSPL_INDENT_HEAD.Indent_No='" + txtLoadoutNo.Value + "'  AND TSPL_INDENT_HEAD.Post ='Y'  "))
            If Not clsCommon.myCDate(LoadOutDate) <= clsCommon.myCDate(txtTransferDate.Value) Then
                txtTransferDate.Value = clsCommon.GetPrintDate(LoadOutDate, "dd/MM/yyyy")
                Throw New Exception("LoadIn Date Should Be Equal Or Greater than LoadOut Date!")
            End If
        Else
            If exicisable = "T" And fndTaxGroup.Value = String.Empty Then
                If cmbitemtype.SelectedIndex = 1 Then
                    fndTaxGroup.Focus()
                    Throw New Exception("TaxGroup can't be blank")
                End If
            End If
        End If


        If chkIsAutoCreate.Checked Then
            Throw New Exception("Auto Generated Transaction can't be directly save/Update")
        End If




        Dim transferqty As Decimal = 0
        Dim loadinqty As Decimal = 0
        Dim LoadInBottletot As Decimal = 0
        Dim ConversionVal As Decimal = 0
        Dim TotalBottle As Decimal = 0
        Dim TotalShell As Decimal = 0
        Dim strFirstItem As String = ""
        Dim strFirstItemUOM As String = ""
        Dim dblTransferShell As Decimal = 0
        For count As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myCdbl(gv1.Rows(count).Cells(colTransferQty).Value) > 0 Then
                transferqty = transferqty + clsCommon.myCdbl(gv1.Rows(count).Cells(colTransferQty).Value)
            End If

            If clsCommon.myCdbl(gv1.Rows(count).Cells(colEmptyValue).Value) > 0 And clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(count).Cells(colUOM).Value), "FB") = CompairStringResult.Equal And (clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value) > 0) Then
                TotalBottle = TotalBottle + clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value)
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(count).Cells(colUOM).Value), "EB") = CompairStringResult.Equal And (clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value) > 0) Then
                TotalBottle = TotalBottle + clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value)
                If clsCommon.myLen(strFirstItem) <= 0 AndAlso clsItemMaster.IsItemTypeEmpty(clsCommon.myCstr(gv1.Rows(count).Cells(colItemCode).Value), clsCommon.myCstr(gv1.Rows(count).Cells(colUOM).Value), Nothing) Then
                    strFirstItem = clsCommon.myCstr(gv1.Rows(count).Cells(colItemCode).Value)
                    strFirstItemUOM = clsCommon.myCstr(gv1.Rows(count).Cells(colUOM).Value)
                End If
            End If

            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(count).Cells(colUOM).Value), "SH") = CompairStringResult.Equal And (clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value) > 0) Then
                TotalShell = TotalShell + clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value)

            End If
            If clsCommon.CompairString(clsCommon.myCstr(gv1.Rows(count).Cells(colUOM).Value), "SH") = CompairStringResult.Equal AndAlso clsCommon.myCdbl(gv1.Rows(count).Cells(colTransferQty).Value) > 0 Then
                dblTransferShell = clsCommon.myCdbl(gv1.Rows(count).Cells(colTransferQty).Value)
            End If

            If clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) > 0 Or clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value) > 0 Then
                loadinqty = loadinqty + clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value)
            End If

            If gv1.Rows(count).Cells(colUOM).Value = "FC" Then
                ConversionVal = clsCommon.myCdbl(connectSql.RunScalar("select isnull(Conversion_Factor,0)  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code='" + gv1.Rows(count).Cells(colItemCode).Value + "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='FB'"))
                LoadInBottletot = LoadInBottletot + (ConversionVal * (clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value)))
            ElseIf gv1.Rows(count).Cells(colUOM).Value = "FB" Then
                LoadInBottletot = LoadInBottletot + (clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value))
                If clsCommon.myLen(strFirstItem) <= 0 AndAlso clsItemMaster.IsItemTypeEmpty(clsCommon.myCstr(gv1.Rows(count).Cells(colItemCode).Value), clsCommon.myCstr(gv1.Rows(count).Cells(colUOM).Value), Nothing) Then
                    strFirstItem = clsCommon.myCstr(gv1.Rows(count).Cells(colItemCode).Value)
                    strFirstItemUOM = clsCommon.myCstr(gv1.Rows(count).Cells(colUOM).Value)
                End If
            End If
            If gv1.Rows(count).Cells(colUOM).Value = "EC" Then
                ConversionVal = clsCommon.myCdbl(connectSql.RunScalar("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code='" + gv1.Rows(count).Cells(colItemCode).Value + "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='EB'"))
                LoadInBottletot = LoadInBottletot + (ConversionVal * (clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value)))
            ElseIf gv1.Rows(count).Cells(colUOM).Value = "EB" Then
                LoadInBottletot = LoadInBottletot + (clsCommon.myCdbl(gv1.Rows(count).Cells(colLeak).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colLoadInQty).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colBreakage).Value) + clsCommon.myCdbl(gv1.Rows(count).Cells(colShortage).Value))
            End If


            If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                Dim strICode As String = clsCommon.myCstr(gv1.Rows(count).Cells(colItemCode).Value)
                Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(count).Cells(colTransferQty).Value)
                Dim strUOM As String = clsCommon.myCstr(gv1.Rows(count).Cells(colUOM).Value)
                Dim strMRP As String = clsCommon.myCstr(gv1.Rows(count).Cells(colMRP).Value)

                'If dblQty > 0 AndAlso (clsCommon.CompairString(strUOM, "FB") = CompairStringResult.Equal OrElse clsCommon.CompairString(strUOM, "FC") = CompairStringResult.Equal) Then
                '    Dim dblBalQty As Double = Math.Round(clsItemLocationDetails.getBalanceWithUnapprove(strICode, txtFromLoaction.Value, strMRP, strUOM, txtTransferNo.Value), 2, MidpointRounding.ToEven)
                '    If dblQty > dblBalQty Then
                '        Throw New Exception("Item - " + strICode + " , MRP - " + strMRP + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblQty) + " and Actual Balance Quantity - " + clsCommon.myCstr(dblBalQty) + " at Row No " + clsCommon.myCstr(count + 1))
                '    End If
                'End If
            End If
        Next


        Dim LOcTypeChk As String = clsCommon.myCstr(connectSql.RunScalar("select Location_Type  from tspl_location_master where Location_Code ='" + txtFromLoaction.Value + "'"))
        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal And LOcTypeChk <> "Logical" Then
            Dim CUom As String = ""
            If cmbitemtype.SelectedIndex = 2 Then
                CUom = "EB"
            ElseIf cmbitemtype.SelectedIndex = 1 Then
                CUom = "FB"
            End If
            Dim LOudOutBottleTot As Decimal = clsCommon.myCdbl(connectSql.RunScalar(" select isnull(SUM(tot),0) from ( select Item_Qty * (select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL where TSPL_ITEM_UOM_DETAIL.Item_Code=TSPL_INDENT_DETAIL.Item_Code and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" + CUom + "' ) as tot from TSPL_INDENT_DETAIL  where Indent_No ='" + txtLoadoutNo.Value + "')as xxx"))
            If LoadInBottletot <> LOudOutBottleTot Then
                Throw New Exception("Load in Qty Should be equal to LoadOut Qty.")
            End If
            'If TotalBottle > 0 AndAlso clsCommon.myLen(strFirstItem) > 0 AndAlso clsCommon.myLen(strFirstItemUOM) > 0 Then
            If clsCommon.myLen(strFirstItem) > 0 AndAlso clsCommon.myLen(strFirstItemUOM) > 0 Then
                Dim dblConvFactor As Double = clsItemMaster.GetConvertionFactor(strFirstItem, strFirstItemUOM, Nothing)
                If ((Math.Ceiling(TotalBottle / dblConvFactor) + dblTransferShell) <> TotalShell) Then
                    If TotalShell = 0 Then
                        Throw New Exception("Please Apply Shell with " + clsCommon.myCstr(Math.Ceiling(TotalBottle / dblConvFactor) + dblTransferShell) + " Qty")
                    Else
                        Throw New Exception("Shell Qty must be " + clsCommon.myCstr(Math.Ceiling(TotalBottle / dblConvFactor) + dblTransferShell) + "")
                    End If
                    Return False
                End If
            End If
        End If

        If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal AndAlso transferqty = 0 Then
            Throw New Exception("Transfer Qty Can't be Blank")
        End If

        If clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal And Not (txtLoadoutNo.Value = String.Empty) Then
            Dim LoadInNO As String = clsDBFuncationality.getSingleValue("select top 1 Indent_No from TSPL_INDENT_HEAD where Load_Out_No ='" + txtLoadoutNo.Value + "' and Indent_No not in ('" + txtTransferNo.Value + "')")
            If clsCommon.myLen(LoadInNO) > 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "LoadOut No. already has been used by LoadIn No:'" + LoadInNO + "'  ", "Transfer", MessageBoxButtons.OK, RadMessageIcon.Info)
                txtLoadoutNo.Value = ""
                Return False
            End If
        End If

        'If Not funvalidatevehicle() Then
        '    Return False
        'End If


        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            Dim obj As clsIndentHead = New clsIndentHead()
            obj.Indent_No = txtTransferNo.Value
            obj.Reference_Doc_No = lblReferenceDocumentNo.Text
            obj.EntryDateTime = txtTransferDate.Value
            obj.Indent_Date = txtTransferDate.Value
            obj.Transfer_Type = IIf(clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal, "LO", "LI")
            obj.Load_Out_No = txtLoadoutNo.Value
            obj.From_Location = txtFromLoaction.Value
            obj.To_Location = txtToLocation.Value
            obj.Price_Date = txtTransferDate.Value
            obj.Tax_Group = fndTaxGroup.Value
            obj.Reference = txtReference.Text
            obj.description = txtDescription.Text
            obj.Route_No = txtRouteNo.Value
            obj.Salesmancode = txtSalesman.Value
            obj.Price_Code = txtPriceCode.Value
            obj.Vehicle_Code = txtVehicleCode.Value
            obj.Vehicle_No = lblVehicleDesc.Text
            obj.Mode_Of_Transport = cboModeofTransport.Text
            obj.Km_Reading = txtKMReading.Text

            obj.Level1_User_code = l1User
            obj.Level2_User_code = l2User
            obj.Level3_User_code = l3User
            obj.Level4_User_code = l4User
            obj.Level5_User_code = l5User
            obj.Load_Out_Date = txtLoadoutDate.Value

            obj.Posting_Date = txtTransferDate.Value
            obj.Trip_No = txtTripNo.Text
            obj.Item_Type = cmbitemtype.Text
            obj.Is_Complete = "N"

            obj.Trans_Type = clsCommon.myCstr(cboType.SelectedValue)
            obj.Total_Basic_Amt = clsCommon.myCdbl(lblTotalBasicAmt.Text)

            If clsCommon.myLen(txtRouteNo.Value) > 0 Then
                Dim strQ As String = "select Level1_Code,Level2_Code,Level3_Code,Level4_Code  from TSPL_SALESMAN_MAPPING where Salesman_Code ='" + txtRouteNo.Value + "'"
                Dim DtSales As DataTable = clsDBFuncationality.GetDataTable(strQ, trans)
                If DtSales IsNot Nothing AndAlso DtSales.Rows.Count > 0 Then
                    obj.HOS = clsCommon.myCstr(DtSales.Rows(0)("Level1_Code"))
                    obj.TDM = clsCommon.myCstr(DtSales.Rows(0)("Level2_Code"))
                    obj.ADC = clsCommon.myCstr(DtSales.Rows(0)("Level3_Code"))
                    obj.CE = clsCommon.myCstr(DtSales.Rows(0)("Level4_Code"))
                    obj.Route_Type_Id = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_Type_Id  from  TSPL_ROUTE_MASTER inner join  TSPL_ROUTE_TYPE on TSPL_ROUTE_MASTER.Type=TSPL_ROUTE_TYPE.Route_Type_Id where TSPL_ROUTE_MASTER.Route_No='" + txtRouteNo.Value + "'"))
                End If
            End If
            obj.FromLoc_Desc = lblFromLocation.Text
            obj.ToLoc_Desc = lblToLocation.Text
            obj.Route_Desc = lblRouteNo.Text
            obj.Price_Desc = lblPriceCode.Text
            obj.Vehicle_Desc = lblVehicleDesc.Text

            obj.Is_AgainstFormF = chkAgainstFForm.Checked
            obj.Location_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Type  from TSPL_LOCATION_MASTER   where  Location_Code='" + IIf(clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal, txtToLocation.Value, txtFromLoaction.Value) + "'"))
            obj.Tax_Group_Type = "S"
            obj.Quick_Settlement = "N"
            'obj.Sale_Invoice_Completed()
            'obj.Printed
            'obj.Is_Shipped()

            obj.Arr = New List(Of clsIndentDetail)
            Dim counter As Integer = 1
            For Each grow As GridViewRowInfo In gv1.Rows

                Dim objTr As New clsIndentDetail()
                objTr.Line_No = counter

                objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                objTr.Item_Desc = clsCommon.myCstr(grow.Cells(colItemName).Value)
                objTr.Price_Date = clsCommon.myCDate(grow.Cells(colPriceDate).Value)
                objTr.Item_Qty = clsCommon.myCdbl(grow.Cells(colTransferQty).Value)
                objTr.MRP = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                objTr.Item_Price = clsCommon.myCdbl(grow.Cells(colItemCost).Value)
                objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                'objTr.Disc_Perc = 
                'objTr.Disc_Amount = 
                objTr.Net_Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                objTr.Pending_Qty = clsCommon.myCdbl(grow.Cells(colTransferQty).Value)



                'objTr.Complete = 
                objTr.Assessable_Amt = clsCommon.myCdbl(grow.Cells(colAssessableAmt).Value)
                objTr.LoadIn_Qty = clsCommon.myCdbl(grow.Cells(colLoadInQty).Value)
                objTr.Uom = clsCommon.myCstr(grow.Cells(colUOM).Value)
                'objTr.Breakage = clsCommon.myCdbl(grow.Cells(colBreakage).Value)
                objTr.Burst = clsCommon.myCdbl(grow.Cells(colBreakage).Value)
                objTr.Leak = clsCommon.myCdbl(grow.Cells(colLeak).Value)
                objTr.Shortage = clsCommon.myCdbl(grow.Cells(colShortage).Value)
                objTr.Basic_Price = clsCommon.myCdbl(grow.Cells(colBasicPrice).Value)
                objTr.Batch_No = clsCommon.myCstr(grow.Cells(colBatchNo).Value)
                objTr.BasicPrice_WithTax = clsCommon.myCdbl(grow.Cells(colBasicPriceWithTax).Value)
                objTr.Empty_Value = clsCommon.myCdbl(grow.Cells(colEmptyValue).Value)
                objTr.TPT_Value = clsCommon.myCdbl(grow.Cells(colTPT).Value)
                'objTr.Pending_Balance_In_Bottle = 
                objTr.MRP_In_Bottle = clsCommon.myCdbl(grow.Cells(colMRPInBottel).Value)

                objTr.Basic_Amt = clsCommon.myCdbl(grow.Cells(colBasicAmt).Value)

                Dim ArrItemRate As New List(Of Double)
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate1).Value))
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate2).Value))
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate3).Value))
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate4).Value))
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate5).Value))
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate6).Value))
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate7).Value))
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate8).Value))
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate9).Value))
                ArrItemRate.Add(clsCommon.myCdbl(grow.Cells(colTaxRate10).Value))



                calculateTaxAssessableAmt(Math.Round(objTr.Item_Qty * objTr.Assessable_Amt, 2), ArrItemRate)
                'calculateTax(Math.Round(objTr.Item_Qty * objTr.MRP, 2))

                If (gv2.Rows.Count > 0) Then
                    objTr.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells("taxAuthority").Value)
                    objTr.TAX1_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate1).Value)
                    objTr.Tax1_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells("assessibleAmount").Value)
                    objTr.TAX1_Amt = clsCommon.myCdbl(gv2.Rows(0).Cells("taxAmount").Value)

                    If counter = 1 Then
                        obj.TAX1 = clsCommon.myCstr(gv2.Rows(0).Cells("taxAuthority").Value)
                        obj.TAX1_Rate = clsCommon.myCdbl(gv2.Rows(0).Cells("taxRate").Value)
                    End If
                    obj.Tax1_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(0).Cells("assessibleAmount").Value)
                    obj.TAX1_Amt += clsCommon.myCdbl(gv2.Rows(0).Cells("taxAmount").Value)
                End If

                If (gv2.Rows.Count > 1) Then
                    objTr.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells("taxAuthority").Value)
                    objTr.TAX2_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate2).Value)
                    objTr.Tax2_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells("assessibleAmount").Value)
                    objTr.TAX2_Amt = clsCommon.myCdbl(gv2.Rows(1).Cells("taxAmount").Value)
                    If counter = 1 Then
                        obj.TAX2 = clsCommon.myCstr(gv2.Rows(1).Cells("taxAuthority").Value)
                        obj.TAX2_Rate = clsCommon.myCdbl(gv2.Rows(1).Cells("taxRate").Value)
                    End If
                    obj.Tax2_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(1).Cells("assessibleAmount").Value)
                    obj.TAX2_Amt += clsCommon.myCdbl(gv2.Rows(1).Cells("taxAmount").Value)
                End If
                If (gv2.Rows.Count > 2) Then
                    objTr.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells("taxAuthority").Value)
                    objTr.TAX3_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate3).Value)
                    objTr.Tax3_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells("assessibleAmount").Value)
                    objTr.TAX3_Amt = clsCommon.myCdbl(gv2.Rows(2).Cells("taxAmount").Value)
                    If counter = 1 Then
                        obj.TAX3 = clsCommon.myCstr(gv2.Rows(2).Cells("taxAuthority").Value)
                        obj.TAX3_Rate = clsCommon.myCdbl(gv2.Rows(2).Cells("taxRate").Value)
                    End If
                    obj.Tax3_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(2).Cells("assessibleAmount").Value)
                    obj.TAX3_Amt += clsCommon.myCdbl(gv2.Rows(2).Cells("taxAmount").Value)
                End If
                If (gv2.Rows.Count > 3) Then
                    objTr.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells("taxAuthority").Value)
                    objTr.TAX4_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate4).Value)
                    objTr.Tax4_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells("assessibleAmount").Value)
                    objTr.TAX4_Amt = clsCommon.myCdbl(gv2.Rows(3).Cells("taxAmount").Value)
                    If counter = 1 Then
                        obj.TAX4 = clsCommon.myCstr(gv2.Rows(3).Cells("taxAuthority").Value)
                        obj.TAX4_Rate = clsCommon.myCdbl(gv2.Rows(3).Cells("taxRate").Value)
                    End If
                    obj.Tax4_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(3).Cells("assessibleAmount").Value)
                    obj.TAX4_Amt += clsCommon.myCdbl(gv2.Rows(3).Cells("taxAmount").Value)
                End If
                If (gv2.Rows.Count > 4) Then
                    objTr.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells("taxAuthority").Value)
                    objTr.TAX5_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate5).Value)
                    objTr.Tax5_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells("assessibleAmount").Value)
                    objTr.TAX5_Amt = clsCommon.myCdbl(gv2.Rows(4).Cells("taxAmount").Value)
                    If counter = 1 Then
                        obj.TAX5 = clsCommon.myCstr(gv2.Rows(4).Cells("taxAuthority").Value)
                        obj.TAX5_Rate = clsCommon.myCdbl(gv2.Rows(4).Cells("taxRate").Value)
                    End If
                    obj.Tax5_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(4).Cells("assessibleAmount").Value)
                    obj.TAX5_Amt += clsCommon.myCdbl(gv2.Rows(4).Cells("taxAmount").Value)
                End If
                If (gv2.Rows.Count > 5) Then
                    objTr.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells("taxAuthority").Value)
                    objTr.TAX6_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate6).Value)
                    objTr.Tax6_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells("assessibleAmount").Value)
                    objTr.TAX6_Amt = clsCommon.myCdbl(gv2.Rows(5).Cells("taxAmount").Value)
                    If counter = 1 Then
                        obj.TAX6 = clsCommon.myCstr(gv2.Rows(5).Cells("taxAuthority").Value)
                        obj.TAX6_Rate = clsCommon.myCdbl(gv2.Rows(5).Cells("taxRate").Value)
                    End If
                    obj.Tax6_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(5).Cells("assessibleAmount").Value)
                    obj.TAX6_Amt += clsCommon.myCdbl(gv2.Rows(5).Cells("taxAmount").Value)
                End If
                If (gv2.Rows.Count > 6) Then
                    objTr.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells("taxAuthority").Value)
                    objTr.TAX7_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate7).Value)
                    objTr.Tax7_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells("assessibleAmount").Value)
                    objTr.TAX7_Amt = clsCommon.myCdbl(gv2.Rows(6).Cells("taxAmount").Value)
                    If counter = 1 Then
                        obj.TAX7 = clsCommon.myCstr(gv2.Rows(6).Cells("taxAuthority").Value)
                        obj.TAX7_Rate = clsCommon.myCdbl(gv2.Rows(6).Cells("taxRate").Value)
                    End If
                    obj.Tax7_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(6).Cells("assessibleAmount").Value)
                    obj.TAX7_Amt += clsCommon.myCdbl(gv2.Rows(6).Cells("taxAmount").Value)
                End If
                If (gv2.Rows.Count > 7) Then
                    objTr.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells("taxAuthority").Value)
                    objTr.TAX8_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate8).Value)
                    objTr.Tax8_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells("assessibleAmount").Value)
                    objTr.TAX8_Amt = clsCommon.myCdbl(gv2.Rows(7).Cells("taxAmount").Value)

                    If counter = 1 Then
                        obj.TAX8 = clsCommon.myCstr(gv2.Rows(7).Cells("taxAuthority").Value)
                        obj.TAX8_Rate = clsCommon.myCdbl(gv2.Rows(7).Cells("taxRate").Value)
                    End If
                    obj.Tax8_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(7).Cells("assessibleAmount").Value)
                    obj.TAX8_Amt += clsCommon.myCdbl(gv2.Rows(7).Cells("taxAmount").Value)
                End If
                If (gv2.Rows.Count > 8) Then
                    objTr.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells("taxAuthority").Value)
                    objTr.TAX9_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate9).Value)
                    objTr.Tax9_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells("assessibleAmount").Value)
                    objTr.TAX9_Amt = clsCommon.myCdbl(gv2.Rows(8).Cells("taxAmount").Value)
                    If counter = 1 Then
                        obj.TAX9 = clsCommon.myCstr(gv2.Rows(8).Cells("taxAuthority").Value)
                        obj.TAX9_Rate = clsCommon.myCdbl(gv2.Rows(8).Cells("taxRate").Value)
                    End If
                    obj.Tax9_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(8).Cells("assessibleAmount").Value)
                    obj.TAX9_Amt += clsCommon.myCdbl(gv2.Rows(8).Cells("taxAmount").Value)
                End If
                If (gv2.Rows.Count > 9) Then
                    objTr.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells("taxAuthority").Value)
                    objTr.TAX10_Rate = clsCommon.myCdbl(grow.Cells(colTaxRate10).Value)
                    objTr.Tax10_Assessable_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells("assessibleAmount").Value)
                    objTr.TAX10_Amt = clsCommon.myCdbl(gv2.Rows(9).Cells("taxAmount").Value)

                    If counter = 1 Then
                        obj.TAX10 = clsCommon.myCstr(gv2.Rows(9).Cells("taxAuthority").Value)
                        obj.TAX10_Rate = clsCommon.myCdbl(gv2.Rows(9).Cells("taxRate").Value)
                    End If
                    obj.Tax10_Assessable_Amt += clsCommon.myCdbl(gv2.Rows(9).Cells("assessibleAmount").Value)
                    obj.TAX10_Amt += clsCommon.myCdbl(gv2.Rows(9).Cells("taxAmount").Value)
                End If

                objTr.Total_Tax = objTr.TAX1_Amt + objTr.TAX2_Amt + objTr.TAX3_Amt + objTr.TAX4_Amt + objTr.TAX5_Amt + objTr.TAX6_Amt + objTr.TAX7_Amt + objTr.TAX8_Amt + objTr.TAX9_Amt + objTr.TAX10_Amt

                objTr.Total_Item_Amt = objTr.Total_Tax + objTr.Net_Amount

                If Not clsCommon.CompairString(objTr.Uom, "SH") = CompairStringResult.Equal Then
                    Dim ConvrsnVal As Decimal = clsCommon.myCdbl(connectSql.RunScalar(trans, "select UD.Conversion_Factor from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + objTr.Item_Code + "' and UOM_Code = '" + objTr.Uom + "' AND UM.Create_Price = 'Y'"))
                    If clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
                        obj.Total_Transfer_QtyInCase = obj.Total_Transfer_QtyInCase + (clsCommon.myCdbl(grow.Cells(colTransferQty).Value)) / ConvrsnVal
                        objTr.Total_QtyInCase = clsCommon.myCdbl(grow.Cells(colTransferQty).Value) / ConvrsnVal
                    ElseIf clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                        obj.Total_Transfer_QtyInCase = obj.Total_Transfer_QtyInCase + (clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) + clsCommon.myCdbl(grow.Cells(colBreakage).Value) + clsCommon.myCdbl(grow.Cells(colShortage).Value) + clsCommon.myCdbl(grow.Cells(colLeak).Value)) / ConvrsnVal
                        objTr.Total_QtyInCase = (clsCommon.myCdbl(grow.Cells(colLoadInQty).Value) + clsCommon.myCdbl(grow.Cells(colBreakage).Value) + clsCommon.myCdbl(grow.Cells(colShortage).Value) + clsCommon.myCdbl(grow.Cells(colLeak).Value)) / ConvrsnVal
                    End If
                End If

                If clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
                    Dim ItmCost As Decimal = Math.Round(clsCommon.myCdbl(grow.Cells(colItemCost).Value) * clsCommon.myCdbl(grow.Cells(colTransferQty).Value) + Math.Round(objTr.Total_Tax, 6), 6)
                    objTr.Total_Item_Cost = Math.Round(ItmCost / clsCommon.myCdbl(grow.Cells(colTransferQty).Value), 6)
                ElseIf clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                    Dim itmQty As Decimal = clsCommon.myCdbl(grow.Cells(colLoadInQty).Value)
                    Dim CnvrsnFctr As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select UD.Conversion_Factor   from TSPL_ITEM_UOM_DETAIL UD JOIN TSPL_UNIT_MASTER UM ON UD.UOM_Code = UM.Unit_Code  where Item_Code= '" + Convert.ToString(grow.Cells(colItemCode).Value) + "' and UOM_Code = '" + Convert.ToString(grow.Cells(colUOM).Value) + "' AND UM.Create_Price = 'Y'"))
                    If clsCommon.CompairString(objTr.Uom, "FB") = CompairStringResult.Equal Then
                        itmQty = itmQty / clsCommon.myCdbl(CnvrsnFctr)
                    End If
                    Dim ItmCost As Decimal = clsCommon.myCdbl(grow.Cells(colItemCost).Value) * itmQty + Math.Round(objTr.Total_Tax, 2)
                    If itmQty > 0 Then
                        objTr.Total_Item_Cost = ItmCost / itmQty
                    End If
                End If

                If clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                    counter += 1
                    obj.Arr.Add(objTr)
                ElseIf clsCommon.myCdbl(grow.Cells(colTransferQty).Value) > 0 Then
                    counter += 1
                    obj.Arr.Add(objTr)
                End If
            Next
            txtTotalTaxAmt.Text = obj.TAX1_Amt + obj.TAX2_Amt + obj.TAX3_Amt + obj.TAX4_Amt + obj.TAX5_Amt + obj.TAX6_Amt + obj.TAX7_Amt + obj.TAX8_Amt + obj.TAX9_Amt + obj.TAX10_Amt
            obj.Item_Amount = clsCommon.myCdbl(txtAmount.Text)
            obj.Total_Tax_Amount = clsCommon.myCdbl(txtTotalTaxAmt.Text)
            obj.Total_Item_Amount = clsCommon.myCdbl(txtTotalAmount.Text)
            obj.Total_Transfer_Amount = clsCommon.myCdbl(txtLoadInOutAmt.Text)

            If (obj.SaveData(obj, isNewEntry)) Then
                LoadData(obj.Indent_No)
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Function funvalidatevehicle() As Boolean
        Dim count As Decimal = 0
        Dim segno As String = String.Empty
        Dim strvehiclenum As String = txtVehicleCode.Value
        Dim sql As String = "select segment_code from TSPL_GL_SEGMENT_CODE where segment_code  = '" + Convert.ToString(txtVehicleCode.Value) + "' "
        If Not String.IsNullOrEmpty(connectSql.RunScalar(sql)) Then
            Return True
        Else
            Dim strmessage As String = "This vehicle code doesn't exist" + Environment.NewLine
            strmessage += "Do you want to continue "
            If common.clsCommon.MyMessageBoxShow(Me, strmessage, Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                count = clsCommon.myCdbl(connectSql.RunScalar("select COUNT(*) from TSPL_GL_SEGMENT_CODE where Segment_name = 'Vehicles'"))
                txtVehicleCode.Value = Convert.ToString(count + 1) '+ "-Man"
                strvehiclenum = txtVehicle.Text + "-Hired"
                sql = "select seg_no from tspl_gl_segment where seg_name='Vehicles'"
                segno = CStr(connectSql.RunScalar(sql))
                connectSql.RunSpTransaction("sp_tspl_gl_segmentcode_insert", New SqlParameter("@segno", segno), New SqlParameter("@segmentname", "Vehicles"), New SqlParameter("@segmentcode", txtVehicleCode.Value), New SqlParameter("@desc", strvehiclenum), New SqlParameter("@acccode", "NULL"), New SqlParameter("@createdby", objCommonVar.CurrentUserCode), New SqlParameter("@createddate", connectSql.serverDate(trans)), New SqlParameter("@modifiedby", objCommonVar.CurrentUserCode), New SqlParameter("@modifieddate", connectSql.serverDate(trans)), New SqlParameter("@compcode", objCommonVar.CurrentCompanyCode))
                connectSql.RunSpTransaction("SP_TSPL_VEHICLE_MASTER_INSERT", New SqlParameter("@Vehicle_Id", txtVehicleCode.Value), New SqlParameter("@Model", ""), New SqlParameter("@Number", strvehiclenum), New SqlParameter("@Description", strvehiclenum), New SqlParameter("@Type", "H"), New SqlParameter("@Start_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@End_Date", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Vehicle_Reg_No", ""), New SqlParameter("@Vehicle_Chesis_No", ""), New SqlParameter("@Capacity", ""), New SqlParameter("@Insurance", ""), New SqlParameter("@Pollution_Check", ""), New SqlParameter("@Fitness", Date.Now.ToString("dd/MM/yyyy")), New SqlParameter("@Trans_Type", ""), New SqlParameter("@Road_Tax", ""), New SqlParameter("@Transport_Id", ""), New SqlParameter("@Created_By", objCommonVar.CurrentUserCode), New SqlParameter("@Created_Date", connectSql.serverDate(trans)), New SqlParameter("@Modified_By", objCommonVar.CurrentUserCode), New SqlParameter("@Modified_Date", connectSql.serverDate(trans)), New SqlParameter("@Comp_Code", objCommonVar.CurrentCompanyCode))
                lblVehicleDesc.Text = txtVehicle.Text + "-Hired"
                txtVehicle.Text = txtVehicleCode.Value
                Return True
            Else
                txtVehicleCode.Value = String.Empty
                txtVehicle.Text = txtVehicleCode.Value
                Return False
            End If
        End If
    End Function

    Private Sub fndTaxGroup__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles fndTaxGroup._MYValidating
        Dim qry As String = "SELECT TAX_Group_Code as  Code,Tax_Group_Desc as Description From TSPL_TAX_GROUP_MASTER"
        Dim whrclas As String = " excisable='Y'and Vat='N'and Stax='N' and Tax_Group_Type = 's' and Is_Transfer=1"
        fndTaxGroup.Value = clsCommon.ShowSelectForm("IndTaxgroup", qry, "Code", whrclas, fndTaxGroup.Value, "Code", isButtonClicked)
        funtaxgroupfill()
    End Sub

    Public Sub funtaxgroupfill()
        Dim Sql As String = "SELECT DISTINCT G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, MAX(R.Tax_Rate) AS Tax_Rate, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
      " FROM TSPL_TAX_GROUP_DETAILS AS G INNER JOIN TSPL_TAX_RATES AS R ON G.Tax_Code = R.Tax_Code " & _
          " WHERE G.Tax_Group_Code = '" + fndTaxGroup.Value + "' AND G.Tax_Group_Type = 'S' and R.Tax_Type='S' GROUP BY G.Tax_Code, G.Tax_Code_Desc, G.Trans_Code, G.Taxable, G.Surtax, G.Surtax_Tax_Code " & _
          " ORDER BY G.Trans_Code"
        ds = RunSQLReturnDS(Sql)
        If ds.Tables(0).Rows.Count > 0 Then
            gv2.DataSource = Nothing
            gv2.Rows.Clear()
            gv2.DataSource = ds.Tables(0)
            totalAmounts()
        End If
    End Sub

    Private Sub txtTotalTaxAmt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTotalTaxAmt.TextChanged
        Try
            txtTotalAmount.Text = clsCommon.myFormat(clsCommon.myCdbl(txtAmount.Text) + clsCommon.myCdbl(txtTotalTaxAmt.Text))
            txtTotalTax.Text = clsCommon.myFormat(txtTotalTaxAmt.Text)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub fndtrnasferno__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtTransferNo._MYValidating
        Dim WhrCls As String = "2=2"
        Dim qry As String = "Select TSPL_INDENT_HEAD.* from TSPL_INDENT_HEAD"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += " and case when Transfer_Type = 'LO' THEN From_Location else To_Location end in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        txtTransferNo.Value = clsCommon.ShowSelectForm("IndTransferNo", qry, "Indent_No", WhrCls, txtTransferNo.Value, "Indent_No", isButtonClicked)
        'FunFill()
        LoadData(txtTransferNo.Value)
        cboTransferType.Enabled = False
    End Sub

    Private Sub fndtrnasferno__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtTransferNo._MYNavigator
        Dim WhrCls As String = ""
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            WhrCls += " and case when Transfer_Type = 'LO' THEN From_Location else To_Location end in (" + objCommonVar.strCurrUserLocations + ")"
        End If
        Dim qry As String = "select Indent_No  from TSPL_INDENT_HEAD  Where 2=2 " + WhrCls + " "
        Select Case NavType
            Case NavigatorType.First
                qry += " and TSPL_INDENT_HEAD.Indent_No=(select MIN(Indent_No) from TSPL_INDENT_HEAD)"
            Case NavigatorType.Last
                qry += " and TSPL_INDENT_HEAD.Indent_No=(select MAX(Indent_No) from TSPL_INDENT_HEAD)"
            Case NavigatorType.Next
                qry += " and TSPL_INDENT_HEAD.Indent_No=(select Min(Indent_No) from TSPL_INDENT_HEAD where Indent_No > '" + txtTransferNo.Value + "')"
            Case NavigatorType.Previous
                qry += " and TSPL_INDENT_HEAD.Indent_No=(select Max(Indent_No) from TSPL_INDENT_HEAD where Indent_No < '" + txtTransferNo.Value + "')"
            Case NavigatorType.Current
                qry += " and TSPL_INDENT_HEAD.Indent_No='" + txtTransferNo.Value + "'"
        End Select

        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If (dt IsNot Nothing AndAlso dt.Rows.Count > 0) Then
            txtTransferNo.Value = clsCommon.myCstr(dt.Rows(0)("Indent_No"))
        End If
        LoadData(txtTransferNo.Value)
    End Sub

    Sub LoadData(ByVal strLONo As String)
        isInsideLoadData = True
        LoadBlankGrid()
        Dim obj As clsIndentHead = clsIndentHead.GetData(strLONo, Nothing)
        isNewEntry = False
        cboType.Enabled = False

        cboType.Visible = False
        lblType.Visible = False
        If obj IsNot Nothing Then
            txtTransferNo.Value = obj.Indent_No
            txtTransferDate.Value = obj.EntryDateTime
            chkIsAutoCreate.Checked = obj.is_Auto_Created_Trans
            If clsCommon.CompairString(obj.Transfer_Type, "LO") = CompairStringResult.Equal Then
                cboTransferType.Text = "Load Out"
                pnlItemType.Visible = False
            Else
                cboTransferType.Text = "Load In"
                txtLoadoutNo.Visible = True
                txtLoadoutNo.Enabled = False
                txtLoadoutNo.Value = obj.Load_Out_No
                gv2.ReadOnly = True
                txtDescription.ReadOnly = False
                rdlblloadoutno.Visible = True
                rdlblloadoutno.Enabled = True
                pnlItemType.Visible = True
                cboType.Enabled = False
            End If
            lblReferenceDocumentNo.Text = obj.Reference_Doc_No
            chkAgainstFForm.Checked = IIf(chkAgainstFForm.Checked, True, False)
            txtTripNo.Text = obj.Trip_No
            txtFromLoaction.Value = obj.From_Location
            routeEnableFromLoc()
            lblFromLocation.Text = obj.FromLoc_Desc
            txtToLocation.Value = obj.To_Location
            lblToLocation.Text = obj.ToLoc_Desc
            fndTaxGroup.Value = obj.Tax_Group
            txtPriceCode.Value = obj.Price_Code
            lblPriceCode.Text = obj.Price_Desc
            txtLoadoutNo.Value = obj.Load_Out_No
            txtReference.Text = obj.Reference
            lblTotalBasicAmt.Text = clsCommon.myFormat(obj.Total_Basic_Amt)
            If clsCommon.myLen(obj.Item_Type) <= 0 Then
                cmbitemtype.SelectedIndex = 0
            ElseIf clsCommon.CompairString(obj.Item_Type, "Full") = CompairStringResult.Equal Then
                cmbitemtype.SelectedIndex = 1
                cboType.Visible = True
                lblType.Visible = True
            ElseIf clsCommon.CompairString(obj.Item_Type, "Empty") = CompairStringResult.Equal Then
                cmbitemtype.SelectedIndex = 2
            End If
            'cmbitemtype.Enabled = False
            txtDescription.Text = obj.description
            txtRouteNo.Value = obj.Route_No

            lblRouteNo.Text = obj.Route_Desc
            txtSalesman.Value = obj.Salesmancode

            If clsCommon.myLen(txtSalesman.Text) > 0 Then
                lblSalesman.Text = connectSql.RunScalar("select  Emp_Name as Name from TSPL_EMPLOYEE_MASTER where EMP_CODE= '" + txtSalesman.Value + "'")
            End If


            txtVehicleCode.Value = obj.Vehicle_Code
            txtVehicle.Text = txtVehicleCode.Value
            lblVehicleDesc.Text = obj.Vehicle_Desc
            cboModeofTransport.Text = obj.Mode_Of_Transport
            txtKMReading.Text = obj.Km_Reading
            txtAmount.Text = clsCommon.myFormat(obj.Item_Amount)
            txtTotalTaxAmt.Text = clsCommon.myFormat(obj.Total_Tax_Amount)
            txtTotalTax.Text = clsCommon.myFormat(obj.Total_Tax_Amount)

            If clsCommon.myLen(txtRouteNo.Value) > 0 And clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                txtToLocation.Enabled = False
            End If
            UsLock1.Status = IIf(obj.Post = 1, ERPTransactionStatus.Approved, ERPTransactionStatus.Pending)
            cboType.SelectedValue = obj.Trans_Type
            txtTotalAmount.Text = clsCommon.myFormat(obj.Total_Item_Amount)
            txtLoadInOutAmt.Text = clsCommon.myFormat(Math.Round(obj.Total_Transfer_Amount, 2))
            For Each objtr As clsIndentDetail In obj.Arr
                Dim grow As GridViewRowInfo = gv1.Rows.AddNew()
                grow.Cells(colPriceDate).Value = clsCommon.GetPrintDate(objtr.Price_Date, "dd/MM/yyyy")
                grow.Cells(colTransferQty).Value = objtr.Item_Qty
                grow.Cells(colAssessableAmt).Value = objtr.Assessable_Amt
                grow.Cells(colAmount).Value = objtr.Amount
                grow.Cells(colPendingQty).Value = objtr.Pending_Qty
                grow.Cells(colLoadInQty).Value = objtr.LoadIn_Qty
                grow.Cells(colBreakage).Value = objtr.Burst
                grow.Cells(colLeak).Value = objtr.Leak
                grow.Cells(colShortage).Value = objtr.Shortage
                grow.Cells(colTax).Value = objtr.Total_Tax
                grow.Cells(colTotal).Value = objtr.Total_Item_Amt
                grow.Cells(colUOM).Value = objtr.Uom
                grow.Cells(colMRP).Value = objtr.MRP
                grow.Cells(colMRPInBottel).Value = objtr.MRP_In_Bottle
                grow.Cells(colItemCode).Value = objtr.Item_Code
                grow.Cells(colItemName).Value = objtr.Item_Desc
                grow.Cells(colBasicPrice).Value = objtr.Basic_Price
                grow.Cells(colItemCost).Value = objtr.Item_Price
                grow.Cells(colBatchNo).Value = objtr.Batch_No
                grow.Cells(colTPT).Value = objtr.TPT_Value
                grow.Cells(colEmptyValue).Value = objtr.Empty_Value
                grow.Cells(colBasicPriceWithTax).Value = objtr.BasicPrice_WithTax


                grow.Cells(colTaxRate1).Value = objtr.TAX1_Rate
                grow.Cells(colTaxRate2).Value = objtr.TAX2_Rate
                grow.Cells(colTaxRate3).Value = objtr.TAX3_Rate
                grow.Cells(colTaxRate4).Value = objtr.TAX4_Rate
                grow.Cells(colTaxRate5).Value = objtr.TAX5_Rate
                grow.Cells(colTaxRate6).Value = objtr.TAX6_Rate
                grow.Cells(colTaxRate7).Value = objtr.TAX7_Rate
                grow.Cells(colTaxRate8).Value = objtr.TAX8_Rate
                grow.Cells(colTaxRate9).Value = objtr.TAX9_Rate
                grow.Cells(colTaxRate10).Value = objtr.TAX10_Rate

                grow.Cells(colBasicAmt).Value = objtr.Basic_Amt

                Dim converFactor As Double = clsItemMaster.GetConvertionFactor(objtr.Item_Code, objtr.Uom, Nothing)
                grow.Cells(colConversion).Value = converFactor
                grow.Cells(colApplyTotal).Value = (objtr.LoadIn_Qty / converFactor) + (objtr.Burst / converFactor) + (objtr.Shortage / converFactor) + (objtr.Leak / converFactor)
            Next
            If obj.Post = 1 Then
                rdbtnsave.Enabled = False
                rdbtndelete.Enabled = False
                rdbtnPost.Enabled = False
            Else
                If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                    FunFillStock()
                Else
                    chkApplyShell.Enabled = True
                    FunFillPendingBalanceQty()
                End If
                rdbtnsave.Enabled = True
                rdbtndelete.Enabled = True
                rdbtnPost.Enabled = True

            End If

            Dim BasicAmt As Decimal = 0
            Dim NetLoadInOutAmt As Decimal = 0
            For Each grow As GridViewRowInfo In gv1.Rows
                BasicAmt = BasicAmt + clsCommon.myCdbl(grow.Cells(colAmount).Value)
                If clsCommon.CompairString(cboTransferType.Text, "Load Out") = CompairStringResult.Equal Then
                    NetLoadInOutAmt = NetLoadInOutAmt + (clsCommon.myCdbl(grow.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(grow.Cells(colTPT).Value) + clsCommon.myCdbl(grow.Cells(colEmptyValue).Value)) * clsCommon.myCdbl(grow.Cells(colTransferQty).Value)
                ElseIf clsCommon.CompairString(cboTransferType.Text, "Load In") = CompairStringResult.Equal Then
                    If CStr(grow.Cells(colUOM).Value) = "FB" Then
                        NetLoadInOutAmt = NetLoadInOutAmt + ((clsCommon.myCdbl(grow.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(grow.Cells(colTPT).Value)) * clsCommon.myCdbl(grow.Cells(colLoadInQty).Value)) + clsCommon.myCdbl(grow.Cells(colEmptyValue).Value)
                    Else
                        NetLoadInOutAmt = NetLoadInOutAmt + (clsCommon.myCdbl(grow.Cells(colBasicPriceWithTax).Value) + clsCommon.myCdbl(grow.Cells(colTPT).Value) + clsCommon.myCdbl(grow.Cells(colEmptyValue).Value)) * clsCommon.myCdbl(grow.Cells(colLoadInQty).Value)
                    End If
                End If
            Next

            Dim Sql, taxCodeDesc As String
            gv2.DataSource = Nothing
            gv2.Rows.Clear()
            For count As Integer = 1 To 10
                Sql = "Select (case When Tax" + count.ToString() + " is NULL THEN '' else Tax" + count.ToString() + " end),Tax" + count.ToString() + "_Rate,Tax" + count.ToString() + "_Assessable_Amt,Tax" + count.ToString() + "_Amt from TSPL_INDENT_HEAD WHERE Indent_No='" + txtTransferNo.Value + "'"
                ds = connectSql.RunSQLReturnDS(Sql)
                If ds.Tables(0) IsNot Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
                    Dim taxCode As String = Convert.ToString(ds.Tables(0).Rows(0)(0))
                    Dim taxRate As Decimal = clsCommon.myCdbl(ds.Tables(0).Rows(0)(1))
                    Dim assAmt As Decimal = clsCommon.myCdbl(ds.Tables(0).Rows(0)(2))
                    Dim taxAmt As Decimal = clsCommon.myCdbl(ds.Tables(0).Rows(0)(3))
                    Sql = "Select Taxable,Surtax,Surtax_Tax_Code From TSPL_TAX_GROUP_DETAILS WHERE Tax_Code='" + taxCode + "' and Tax_Group_Code='" + fndTaxGroup.Value + "'"
                    Dim dss As DataSet = connectSql.RunSQLReturnDS(Sql)
                    Dim taxable As String = ""
                    Dim surtax As String = ""
                    Dim surtaxcode As String = ""
                    If dss.Tables(0).Rows.Count > 0 Then
                        taxable = dss.Tables(0).Rows(0)("Taxable").ToString()
                        surtax = dss.Tables(0).Rows(0)("Surtax").ToString()
                        surtaxcode = dss.Tables(0).Rows(0)("Surtax_Tax_Code").ToString()
                    End If
                    If Not ds.Tables(0).Rows(0)(0).ToString() = "" Then
                        Sql = "Select Tax_Code_Desc from TSPL_TAX_MASTER where Tax_Code='" + ds.Tables(0).Rows(0)(0).ToString() + "'"
                        taxCodeDesc = connectSql.RunScalar(Sql)
                        gv2.Rows.Add(taxCode, taxCodeDesc, taxRate, BasicAmt, assAmt, taxAmt, taxable, surtax, surtaxcode, "", "")
                    End If
                End If
            Next count
            txtRouteNo.Enabled = False
            If Not obj.Post = 1 Then
                priceDateSelection(True, IIf(clsCommon.CompairString(obj.Item_Type, "Full") = CompairStringResult.Equal, "FC", "EC"))
                If clsCommon.CompairString(obj.Trans_Type, "Route") = CompairStringResult.Equal AndAlso clsCommon.CompairString(obj.Transfer_Type, "LI") = CompairStringResult.Equal Then
                    rdbtnPost.Enabled = False
                Else
                    rdbtnPost.Enabled = True
                End If
            End If
            txtFromLoaction.Enabled = False
            txtPriceCode.Enabled = False
            txtRouteNo.Enabled = False

            gv1.CurrentRow = gv1.Rows(0)
        Else
            rdbtndelete.Enabled = False
            rdbtnPost.Enabled = False
            rdbtnprint.Enabled = False
        End If
        FunFbFcTotal()
        isInsideLoadData = False
    End Sub

    Sub BlankAllControls()
        txtTransferNo.Value = ""
        txtTransferDate.Value = clsCommon.GETSERVERDATE()
        cboTransferType.Text = "Load Out"
        txtLoadoutNo.Value = ""
        txtFromLoaction.Value = ""
        txtToLocation.Value = ""
        txtTransferDate.Value = ""
        fndTaxGroup.Value = ""
        txtReference.Text = ""
        txtDescription.Text = ""
        txtRouteNo.Value = ""
        txtSalesman.Text = ""
        txtPriceCode.Value = ""
        txtVehicleCode.Value = ""
        lblVehicleDesc.Text = ""
        cboModeofTransport.Text = ""
        txtKMReading.Text = ""
        txtAmount.Text = ""
        txtTotalTaxAmt.Text = ""
        txtTotalTax.Text = ""
        txtTotalAmount.Text = ""

        txtLoadoutDate.Value = ""
        txtTransferDate.Value = ""
        txtTripNo.Text = ""
        cmbitemtype.Text = ""
        lblFromLocation.Text = ""
        lblToLocation.Text = ""
        lblRouteNo.Text = ""
        lblPriceCode.Text = ""
        lblVehicleDesc.Text = ""
        txtLoadInOutAmt.Text = clsCommon.myFormat(0)
        chkAgainstFForm.Checked = False
    End Sub


    Private Sub cboType_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles cboType.Validating
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Excise") = CompairStringResult.Equal AndAlso clsCommon.CompairString(cmbitemtype.Text, "Full") = CompairStringResult.Equal Then
            Dim qry As String = "select Tax_Group_Code from TSPL_TAX_GROUP_MASTER where Is_Transfer=1 and Tax_Group_Type='S'"
            Dim strTaxGroupofTransferType As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
            If clsCommon.myLen(strTaxGroupofTransferType) > 0 Then
                fndTaxGroup.Value = strTaxGroupofTransferType
                funtaxgroupfill()
            End If
        Else
            gv2.DataSource = Nothing
            gv2.Rows.Clear()
        End If
        If clsCommon.CompairString(clsCommon.myCstr(cboType.SelectedValue), "Excise") = CompairStringResult.Equal Then
            txtSalesman.Enabled = True
        Else
            txtSalesman.Value = ""
            lblSalesman.Text = ""
            txtSalesman.Enabled = False
        End If
    End Sub

    Private Sub rddrplisttransfertype_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTransferType.SelectedValueChanged, cboTransferType.SelectedIndexChanged

    End Sub

    Private Sub txtSalesman__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtSalesman._MYValidating
        Dim qry As String = "select EMP_CODE as Code,Emp_Name as Name from TSPL_EMPLOYEE_MASTER   "
        txtSalesman.Value = clsCommon.ShowSelectForm("IndtransferSale", qry, "Code", "Designation='SALES'", txtSalesman.Value, "Code", isButtonClicked)
        lblSalesman.Text = connectSql.RunScalar("select  Emp_Name as Name from TSPL_EMPLOYEE_MASTER where EMP_CODE= '" + txtSalesman.Value + "'")
    End Sub

    Private Sub btnUnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnPost.Click
        Try
            If clsCommon.myLen(txtTransferNo.Value) > 0 Then
                Dim qry As String = "select 1 from TSPL_INDENT_HEAD where Indent_No='" + txtTransferNo.Value + "' and Post=1"
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                If dt Is Nothing OrElse dt.Rows.Count <= 0 Then
                    Throw New Exception("Transaction status should be posted.")
                End If
                qry = "select Transfer_No from TSPL_TRANSFER_HEAD where Against_Indent_No='" + txtTransferNo.Value + "'"
                dt = New DataTable()
                dt = clsDBFuncationality.GetDataTable(qry)
                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                    qry = "Indent Used in following Transfer"
                    For Each dr As DataRow In dt.Rows
                        qry += Environment.NewLine + clsCommon.myCstr(dr("Transfer_No"))
                    Next
                    qry += Environment.NewLine + "Can't unpost it"
                    Throw New Exception(qry)
                End If
                If clsCommon.MyMessageBoxShow(Me, "Unpost the current transaction" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo, RadMessageIcon.Question) = System.Windows.Forms.DialogResult.Yes Then
                    qry = "update TSPL_INDENT_HEAD set Post=0,Posting_Date=null where Indent_No='" + txtTransferNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(qry)
                    clsCommon.MyMessageBoxShow("Tansaction unposted succesffuly", Me.Text)
                    LoadData(txtTransferNo.Value)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnCheckSlip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckSlip.Click
        If txtTransferNo.Value = "" Then
            common.clsCommon.MyMessageBoxShow(Me, "Please Select Transfer Indent No.", Me.Text)
        Else
            Dim whereclause As String = " where TSPL_INDENT_HEAD.Indent_No =  '" + txtTransferNo.Value + "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='FB'"
            Dim strQuery As String = ""
            strQuery = " select Flavour,Size,max(Case1) as [Case],max(TSPL_INV_CLASS_DETAILS.Inv_Class_Desc) as [Flavour Description],max(Vehicle_No) as Vehicle_No,max(Indent_No) as [Indent No] "
            strQuery += " ,max(Indent_Date) as [Date],max(Route_No) as [Route],max(abc.Item_Code) as ItemCode ,sum(ordered ) as [ordered],'' as[Loaded] "
            strQuery += " ,max(MRPBottle) as   MRPBottle ,max(Comp_Name) as Comp_Name,max(Address) as Address,MAX(TPTName ) as TPTName, max(Route_Desc) as Route_Desc, max([SalesmanName]) as [SalesmanName]  ,max(Location) as [Location]  from( "
            strQuery += " SELECT  LEFT(TSPL_INDENT_DETAIL.Item_Code ,2) AS [Flavour], cast(SUBSTRING (TSPL_INDENT_DETAIL.Item_Code,3,LEN(TSPL_INDENT_DETAIL.Item_Code)-5)as DECIMAL(9,2) ) as [Size] , "

            strQuery += " TSPL_INDENT_DETAIL.Item_Code,SUBSTRING(TSPL_INDENT_DETAIL.Item_Code,len(TSPL_INDENT_DETAIL.Item_Code )-2,1)as [Case1],RIGHT(TSPL_INDENT_DETAIL.Item_Code,2) as bottlecase "
            strQuery += " ,TSPL_INDENT_HEAD.Indent_No,TSPL_INDENT_HEAD.Indent_Date ,TSPL_INDENT_HEAD.Vehicle_No ,TSPL_INDENT_HEAD.Route_No "
            strQuery += " ,Comp_Name,case when LEN(TSPL_COMPANY_MASTER.Add2 )>0 then TSPL_COMPANY_MASTER.Add1+', '+ TSPL_COMPANY_MASTER.Add2 else case when LEN(TSPL_COMPANY_MASTER.Add3 )>0 then TSPL_COMPANY_MASTER.Add1+', '+ TSPL_COMPANY_MASTER.Add2+', '+TSPL_COMPANY_MASTER.Add3 else TSPL_COMPANY_MASTER.Add1 end end as [Address], TSPL_INDENT_HEAD.Route_Desc,TSPL_INDENT_DETAIL.MRP,TSPL_INDENT_DETAIL.uom  "
            strQuery += " ,(case when TSPL_INDENT_DETAIL.uom='FC' then MRP/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor else MRP end )as MRPBottle,(case when TSPL_INDENT_DETAIL.Uom ='FB' then  Item_Qty/ TSPL_ITEM_UOM_DETAIL.Conversion_Factor else Item_Qty end) as [ordered], "
            strQuery += " (select max(vendor_name) from TSPL_VENDOR_MASTER LEFT outer join TSPL_VEHICLE_MASTER on TSPL_VEHICLE_MASTER.transport_id =TSPL_VENDOR_MASTER.vendor_code left outer join TSPL_INDENT_HEAD on TSPL_INDENT_HEAD.Vehicle_Code = TSPL_VEHICLE_MASTER.Vehicle_Id where TSPL_INDENT_HEAD.Indent_No=TSPL_INDENT_DETAIL.Indent_No  ) as [TPTName],"
            strQuery += " TSPL_INDENT_DETAIL.Item_Qty,TSPL_INDENT_HEAD.Salesmancode ,Emp_Name as [SalesmanName]  ,TSPL_INDENT_HEAD.From_Location ,TSPL_INDENT_HEAD.FromLoc_Desc as [Location]   FROM TSPL_INDENT_DETAIL  left outer join  TSPL_INDENT_HEAD ON TSPL_INDENT_DETAIL.Indent_No=TSPL_INDENT_HEAD.Indent_No "
            strQuery += " left outer join TSPL_COMPANY_MASTER on TSPL_INDENT_HEAD.Comp_Code=TSPL_COMPANY_MASTER.Comp_Code "
            strQuery += " left outer join  TSPL_ITEM_UOM_DETAIL on  TSPL_ITEM_UOM_DETAIL.Item_Code= TSPL_INDENT_DETAIL.Item_Code"
            strQuery += " left outer join  TSPL_EMPLOYEE_MASTER on TSPL_EMPLOYEE_MASTER.EMP_CODE =TSPL_INDENT_HEAD.Salesmancode"
            strQuery += " " + whereclause + " "
            strQuery += " )abc left outer join TSPL_INV_CLASS_DETAILS on   Flavour =TSPL_INV_CLASS_DETAILS.Inv_Class_Code "
            ' strQuery += " left outer join  TSPL_ITEM_UOM_DETAIL on  TSPL_ITEM_UOM_DETAIL.Item_Code= abc.Item_Code "
            'strQuery += " where TSPL_ITEM_UOM_DETAIL.UOM_Code=abc .Uom  "
            strQuery += "  group by Size ,Flavour order by CASE  WHEN Size  = '200' THEN 1  WHEN Size = '250' THEN 2 WHEN Size = '300' THEN 3 WHEN Size = '500' THEN 4 WHEN Size = '600' THEN 5 WHEN Size = '1.0' THEN 6 WHEN Size = '1.2' THEN 7 WHEN Size = '1.5' THEN 8 ELSE 9 END "
            strQuery = strQuery

            Try
                Dim dt As DataTable = clsDBFuncationality.GetDataTable(StrQuery)
                If dt.Rows.Count > 0 Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.InventoryReport, clsDBFuncationality.GetDataTable(strQuery), "crptCheckSlipIndentReport", "Check Slip")
                    frmCRV = Nothing
                Else
                    common.clsCommon.MyMessageBoxShow(Me, "No Data Found", "Transfer Indent Report", MessageBoxButtons.OK)
                End If

            Catch ex As Exception
                common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Transfer Indent Report", MessageBoxButtons.OK)
            End Try
        End If
    End Sub

End Class

