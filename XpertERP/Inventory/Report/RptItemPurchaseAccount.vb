Imports common
Imports System.Data.SqlClient
Imports System.IO
'' Work done agaist ticket no. BHA/04/10/18-000594
Public Class RptItemPurchaseAccount
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim ButtonToolTip As New ToolTip()
    Const colSno As String = "colSno"
    Const colStructureCode As String = "colStructureCode"
    Const colItemCode As String = "colItemCode"
    Const colItemName As String = "colItemName"
    Const colStructureDesc As String = "colStructureDesc"
    Const colItemType As String = "colItemType"
    Const colItemTypeDesc As String = "colItemTypeDesc"
    Const colAccountCode As String = "colAccountCode"
    Const colAccountDesc As String = "colAccountDesc"
    Const colInventory As String = "colInventory"
    Const colInventoryDesc As String = "colInventoryDesc"
    Const colPayableClearing As String = "colPayableClearing"
    Const colPayableClearingDesc As String = "colPayableClearingDesc"
    Const ColShipment As String = "ColShipment"
    Const ColShipmentDesc As String = "ColShipmentDesc"
    Const colAdj As String = "colAdj"
    Const colAdjDesc As String = "colAdjDesc"
    Const colFGShortage As String = "colFGShortage"
    Const colFGShortageDesc As String = "colFGShortageDesc"
    Const colBreakage As String = "colBreakage"
    Const colBreakageDesc As String = "colBreakageDesc"
    Const colChillingCharges As String = "colChillingCharges"
    Const colChillingChargesDesc As String = "colChillingChargesDesc"
    Const colCreditDebitNote As String = "colCreditDebitNote"
    Const colCreditDebitNoteDesc As String = "colCreditDebitNoteDesc"
    Const colDifferenceAccount As String = "colDifferenceAccount"
    Const colDifferenceAccountDesc As String = "colDifferenceAccountDesc"
    Const colDisassembly As String = "colDisassembly"
    Const colDisassemblyDesc As String = "colDisassemblyDesc"
    Const colFAAccount As String = "colFAAccount"
    Const colFAAccountDesc As String = "colFAAccountDesc"
    Const colFrieghtCharges As String = "colFrieghtCharges"
    Const colFrieghtChargesDesc As String = "colFrieghtChargesDesc"
    Const colHandlingCharges As String = "colHandlingCharges"
    Const colHandlingChargesDesc As String = "colHandlingChargesDesc"
    Const colJobWorkAC As String = "colJobWorkAC"
    Const colJobWorkACDesc As String = "colJobWorkACDesc"
    Const colLossAccount As String = "colLossAccount"
    Const colLossAccountDesc As String = "colLossAccountDesc"
    Const colInvControlEmpties As String = "colInvControlEmpties"
    Const colInvControlEmptiesDesc As String = "colInvControlEmptiesDesc"
    Const colRejected As String = "colRejected"
    Const colRejectedDesc As String = "colRejectedDesc"
    Const colShortage As String = "colShortage"
    Const colShortageDesc As String = "colShortageDesc"
    Const colPhyisalInvAdj As String = "colPhyisalInvAdj"
    Const colPhyisalInvAdjDesc As String = "colPhyisalInvAdjDesc"
    Const colProvision As String = "colProvision"
    Const colProvisionDesc As String = "colProvisionDesc"
    Const colPurchaseAccount As String = "colPurchaseAccount"
    Const colPurchaseAccountDesc As String = "colPurchaseAccountDesc"
    Const colPurchaseControl As String = "colPurchaseControl"
    Const colPurchaseControlDesc As String = "colPurchaseControlDesc"
    Const colPurchaseJobWork As String = "colPurchaseJobWork"
    Const colPurchaseJobWorkDesc As String = "colPurchaseJobWorkDesc"
    Const colPurchaseLoss As String = "colPurchaseLoss"
    Const colPurchaseLossDesc As String = "colPurchaseLossDesc"
    Const colPurchaseSetOff As String = "colPurchaseSetOff"
    Const colPurchaseSetOffDesc As String = "colPurchaseSetOffDesc"
    Const colRGPClearing As String = "colRGPClearing"
    Const colRGPClearingDesc As String = "colRGPClearingDesc"
    Const colRM As String = "colRM"
    Const colRMDesc As String = "colRMDesc"
    Const colStockTrasnfer As String = "colStockTrasnfer"
    Const colStockTrasnferDesc As String = "colStockTrasnferDesc"
    Const colStockTrasnferIn As String = "colStockTrasnferIn"
    Const colStockTrasnferInDesc As String = "colStockTrasnferInDesc"
    Const colJobWork As String = "colJobWork"
    Const colJobWorkDesc As String = "colJobWorkDesc"
    Const colStoreConsumption As String = "colStoreConsumption"
    Const colStoreConsumptionDesc As String = "colStoreConsumptionDesc"
    Const colTransferClearing As String = "colTransferClearing"
    Const colTransferClearingDesc As String = "colTransferClearingDesc"
    Const colGainLossAccount As String = "colGainLossAccount"
    Const colGainLossAccountDesc As String = "colGainLossAccountDesc"
    Const colWIPAccount As String = "colWIPAccount"
    Const colWIPAccountDesc As String = "colWIPAccountDesc"
    Const colWreckage As String = "colWreckage"
    Const colWreckageDesc As String = "colWreckageDesc"
    Const colConsignmentAc As String = "colConsignmentAc"
    Const colConsignmentDesc As String = "colConsignmentDesc"
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
    Function AllowToSave() As Boolean
        Try
            Dim LineNo As String
           
            For Each grow As GridViewRowInfo In gv.Rows
                LineNo = clsCommon.myCstr(grow.Index + 1)
                Dim ItemCode As String = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                Dim AccountCode As String = clsCommon.myCstr(grow.Cells(colAccountCode).Value)
                Dim Inventory As String = clsCommon.myCstr(grow.Cells(colInventory).Value)
                Dim PayableClearing As String = clsCommon.myCstr(grow.Cells(colPayableClearing).Value)
                Dim Adj As String = clsCommon.myCstr(grow.Cells(colAdj).Value)
                Dim InvControlEmpties As String = clsCommon.myCstr(grow.Cells(colInvControlEmpties).Value)
                Dim TransferClearing As String = clsCommon.myCstr(grow.Cells(colTransferClearing).Value)
                Dim Shipment As String = clsCommon.myCstr(grow.Cells(ColShipment).Value)
                Dim Disassembly As String = clsCommon.myCstr(grow.Cells(colDisassembly).Value)
                Dim PhyisalInvAdj As String = clsCommon.myCstr(grow.Cells(colPhyisalInvAdj).Value)
                Dim CreditDebitNote As String = clsCommon.myCstr(grow.Cells(colCreditDebitNote).Value)
                Dim RGPClearing As String = clsCommon.myCstr(grow.Cells(colRGPClearing).Value)
                Dim Breakage As String = clsCommon.myCstr(grow.Cells(colBreakage).Value)
                Dim WIPAccount As String = clsCommon.myCstr(grow.Cells(colWIPAccount).Value)
                Dim RM As String = clsCommon.myCstr(grow.Cells(colRM).Value)
                Dim Rejected As String = clsCommon.myCstr(grow.Cells(colRejected).Value)
                Dim Shortage As String = clsCommon.myCstr(grow.Cells(colShortage).Value)
                Dim LossAccount As String = clsCommon.myCstr(grow.Cells(colLossAccount).Value)
                Dim PurchaseControl As String = clsCommon.myCstr(grow.Cells(colPurchaseControl).Value)
                Dim GainLossAccount As String = clsCommon.myCstr(grow.Cells(colGainLossAccount).Value)
                Dim JobWorkAC As String = clsCommon.myCstr(grow.Cells(colJobWorkAC).Value)
                Dim StockTrasnferIn As String = clsCommon.myCstr(grow.Cells(colStockTrasnferIn).Value)
                Dim StockTrasnfer As String = clsCommon.myCstr(grow.Cells(colStockTrasnfer).Value)
                Dim Provision As String = clsCommon.myCstr(grow.Cells(colProvision).Value)
                Dim ChillingCharges As String = clsCommon.myCstr(grow.Cells(colChillingCharges).Value)
                Dim FrieghtCharges As String = clsCommon.myCstr(grow.Cells(colFrieghtCharges).Value)
                Dim PurchaseAccount As String = clsCommon.myCstr(grow.Cells(colPurchaseAccount).Value)
                Dim PurchaseSetOff As String = clsCommon.myCstr(grow.Cells(colPurchaseSetOff).Value)
                Dim PurchaseJobWork As String = clsCommon.myCstr(grow.Cells(colPurchaseJobWork).Value)
                Dim ifferenceAccount As String = clsCommon.myCstr(grow.Cells(colDifferenceAccount).Value)
                Dim JobWork As String = clsCommon.myCstr(grow.Cells(colJobWork).Value)
                Dim HandlingCharges As String = clsCommon.myCstr(grow.Cells(colHandlingCharges).Value)
                Dim StoreConsumption As String = clsCommon.myCstr(grow.Cells(colStoreConsumption).Value)
                Dim FAAccount As String = clsCommon.myCstr(grow.Cells(colFAAccount).Value)
                Dim FGShortage As String = clsCommon.myCstr(grow.Cells(colFGShortage).Value)
                Dim PurchaseLoss As String = clsCommon.myCstr(grow.Cells(colPurchaseLoss).Value)
                Dim Wreckage As String = clsCommon.myCstr(grow.Cells(colWreckage).Value)
                If clsCommon.myLen(AccountCode) > 0 Then
                    Dim qry1 As String = "Select count(*) from tspl_purchase_accounts where purchase_class_code ='" + AccountCode + "'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Incorect Purchase Account Set (" & AccountCode & ") ", Me.Text)
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
                If clsCommon.myLen(Breakage) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Breakage + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & Breakage & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(ChillingCharges) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + ChillingCharges + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & ChillingCharges & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(CreditDebitNote) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + CreditDebitNote + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & CreditDebitNote & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(HandlingCharges) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + HandlingCharges + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & HandlingCharges & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Disassembly) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Disassembly + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & Disassembly & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(FAAccount) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + FAAccount + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & FAAccount & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(FGShortage) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + FGShortage + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & FGShortage & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(GainLossAccount) > 0 Then
                    Dim qry1 As String = "Select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + GainLossAccount + "'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & GainLossAccount & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
               
                If clsCommon.myLen(InvControlEmpties) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + InvControlEmpties + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & InvControlEmpties & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Inventory) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Inventory + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & Inventory & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(JobWork) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + JobWork + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow("Filled (" & JobWork & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(JobWorkAC) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + JobWorkAC + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & JobWorkAC & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(LossAccount) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + LossAccount + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & LossAccount & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Wreckage) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Wreckage + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & Wreckage & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(WIPAccount) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + WIPAccount + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & WIPAccount & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Rejected) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Rejected + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & Rejected & ") must be control account.", Me.Text)
                        Return False
                    End If
                End If
                If clsCommon.myLen(Shortage) > 0 Then
                    Dim qry1 As String = "select count(*) from TSPL_GL_ACCOUNTS where Account_Code='" + Shortage + "' AND ControlAccount ='Y'"
                    Dim check1 As Integer = clsDBFuncationality.getSingleValue(qry1)
                    If check1 <= 0 Then
                        clsCommon.MyMessageBoxShow(Me, "Filled (" & Shortage & ") must be control account.", Me.Text)
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

            Next
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
            Return False
        End Try
        Return True
    End Function
    Private Sub Update_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            Dim obj As New clsItemSalePurchaseSetMaster()
            obj.PurchaseMasterArr = New List(Of clsPurchaseAccountSets)

            If (AllowToSave()) Then
                For Each grow As GridViewRowInfo In gv.Rows
                    If (clsCommon.myLen(grow.Cells(colItemCode).Value) > 0) Then

                        Dim objTr As New clsPurchaseAccountSets()
                        objTr.colItemCode = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.colAccountCode = clsCommon.myCstr(grow.Cells(colAccountCode).Value)
                        objTr.colInventory = clsCommon.myCstr(grow.Cells(colInventory).Value)
                        objTr.colPayableClearing = clsCommon.myCstr(grow.Cells(colPayableClearing).Value)
                        objTr.colAdj = clsCommon.myCstr(grow.Cells(colInvControlEmpties).Value)
                        objTr.colInvControlEmpties = clsCommon.myCstr(grow.Cells(colInvControlEmpties).Value)
                        objTr.colTransferClearing = clsCommon.myCstr(grow.Cells(colTransferClearing).Value)
                        objTr.ColShipment = clsCommon.myCstr(grow.Cells(ColShipment).Value)
                        objTr.colDisassembly = clsCommon.myCstr(grow.Cells(colDisassembly).Value)
                        objTr.colPhyisalInvAdj = clsCommon.myCstr(grow.Cells(colPhyisalInvAdj).Value)
                        objTr.colCreditDebitNote = clsCommon.myCstr(grow.Cells(colCreditDebitNote).Value)
                        objTr.colRGPClearing = clsCommon.myCstr(grow.Cells(colRGPClearing).Value)
                        objTr.colBreakage = clsCommon.myCstr(grow.Cells(colBreakage).Value)
                        objTr.colWIPAccount = clsCommon.myCstr(grow.Cells(colWIPAccount).Value)
                        objTr.colRM = clsCommon.myCstr(grow.Cells(colRM).Value)
                        objTr.colRejected = clsCommon.myCstr(grow.Cells(colRejected).Value)
                        objTr.colShortage = clsCommon.myCstr(grow.Cells(colShortage).Value)
                        objTr.colLossAccount = clsCommon.myCstr(grow.Cells(colLossAccount).Value)
                        objTr.colPurchaseControl = clsCommon.myCstr(grow.Cells(colPurchaseControl).Value)
                        objTr.colGainLossAccount = clsCommon.myCstr(grow.Cells(colGainLossAccount).Value)
                        objTr.colJobWorkAC = clsCommon.myCstr(grow.Cells(colJobWorkAC).Value)
                        objTr.colStockTrasnferIn = clsCommon.myCstr(grow.Cells(colStockTrasnferIn).Value)
                        objTr.colStockTrasnfer = clsCommon.myCstr(grow.Cells(colStockTrasnfer).Value)
                        objTr.colProvision = clsCommon.myCstr(grow.Cells(colProvision).Value)
                        objTr.colChillingCharges = clsCommon.myCstr(grow.Cells(colChillingCharges).Value)
                        objTr.colFrieghtCharges = clsCommon.myCstr(grow.Cells(colFrieghtCharges).Value)
                        objTr.colPurchaseAccount = clsCommon.myCstr(grow.Cells(colPurchaseAccount).Value)
                        objTr.colPurchaseSetOff = clsCommon.myCstr(grow.Cells(colPurchaseSetOff).Value)
                        objTr.colPurchaseJobWork = clsCommon.myCstr(grow.Cells(colPurchaseJobWork).Value)
                        objTr.colDifferenceAccount = clsCommon.myCstr(grow.Cells(colDifferenceAccount).Value)
                        objTr.colJobWork = clsCommon.myCstr(grow.Cells(colJobWork).Value)
                        objTr.colHandlingCharges = clsCommon.myCstr(grow.Cells(colHandlingCharges).Value)
                        objTr.colStoreConsumption = clsCommon.myCstr(grow.Cells(colStoreConsumption).Value)
                        objTr.colFAAccount = clsCommon.myCstr(grow.Cells(colFAAccount).Value)
                        objTr.colPurchaseLoss = clsCommon.myCstr(grow.Cells(colPurchaseLoss).Value)
                        objTr.colWreckage = clsCommon.myCstr(grow.Cells(colWreckage).Value)
                        objTr.ConsignmentAc = clsCommon.myCstr(grow.Cells(colConsignmentAc).Value)

                        If (clsCommon.myLen(objTr.colItemCode) > 0) Then
                            obj.PurchaseMasterArr.Add(objTr)
                        End If

                    End If
                Next
                If (obj.PurchaseMasterArr Is Nothing OrElse obj.PurchaseMasterArr.Count <= 0) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Please Fill at list one Item", Me.Text)
                    Return
                End If
                If (obj.PurchaseUpdate(obj)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data Update Successfully", Me.Text)
                    FunReset()
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub FunReset()
        gv.Columns.Clear()
        gv.DataSource = Nothing
        txtItem.arrValueMember = Nothing
        txtItemType.arrValueMember = Nothing
        txtPurchaseSet.arrValueMember = Nothing
        txtItemStructure.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        isInsideLoadData = False
        gv.EnableFiltering = False
        chkOnlyview.Checked = False
    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        PageSetupReport_ID = MyBase.Form_ID
        TemplateGridview = gv
        Print(Exporter.Refresh)
    End Sub
    Private Sub Printbtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Printbtn.Click
        Print(Exporter.Print)
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
                clsCommon.ProgressBarShow()

                Dim qry As String = "select TSPL_ITEM_MASTER.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc	as [Item Desc],TSPL_ITEM_MASTER.Structure_Code as [Structure Code],TSPL_ITEM_MASTER.structure_desc as [Structure Description],TSPL_ITEM_MASTER.Item_Type as [Item Type],TSPL_ITEM_TYPE_MASTER.Item_Type_Name as [Item Type Name],TSPL_ITEM_MASTER.gl_account as [Consumption Account],GL_Consignment.Description as [Consumption Desc],TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code as[Account Code],TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Desc as [Account Desc],TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account as [Inventory Code]"
                qry += " ,GL_Inv_Control_Account.Description"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing as [Payable Clearing]"
                qry += " ,GL_Inv_payable_Clearning.Description as [Payable Clearing Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Shipment_Clearing as [Shipment Clearing]"
                qry += " ,GL_Shipment_Clearning.Description as [Shipment Clearing Desc] "
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Adjustment_Account as [Adjustment]"
                qry += " ,GL_Adjustment_Account.Description as [Adjustment Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit as [FG Shortage Account]"
                qry += " ,GL_Assembly_Cost_Cradit.Description as [FG Shortage Account Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account as [Breakage GL Account]"
                qry += " ,GL_Breakge_GL_Account.Description as [Breakage GL Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Chilling_Charges as [Chilling Charges]"
                qry += " ,GL_Chilling_Charges.Description as [Chilling Charges Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing as [Credit Debit Note]"
                qry += " ,GL_Credit_Debit_Note_Clearning.Description as [Credit Debit Note Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Difference_Account as [Difference Account]"
                qry += " ,GL_Difference_Account.Description as [Difference Account Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Disassembly_Expense as [Disassembly Expense]"
                qry += " ,GL_Disassembly_Expense.Description as [Disassembly Expense desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.FA_CLEARING_AC as [FA Account]"
                qry += " ,GL_FA_Clearning_Ac.Description as [FA Account Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Freight_Charges as [Freight Charges]"
                qry += " ,GL_Freight_Charges.Description as [Freight Charges Desc] "
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Handling_Charge as [Handling Charges]"
                qry += " ,GL_Handling_Charges.Description as [Handling Charges Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Job_Work_Ac as [Job Work Account]"
                qry += " ,GL_Job_Work_Ac.Description as [Job Work Account Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Loss_Ac as [Loss Account]"
                qry += " ,GL_Loss_Ac.Description as [Loss Account Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing as [Inv Control Empties]"
                qry += " ,GL_Non_Stock_Clearning.Description  as [Inv Control Empties Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Other_1 as [Rejected]"
                qry += " ,GL_Other1.Description as [Rejected Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Other_2 as [Shortage]"
                qry += " ,GL_Other2.Description as [Shortage Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Physical_Inv_Adjustment as [Physical Inv Adjustment]"
                qry += " ,GL_Physical_Inv_Adjustment.Description as [Physical Inv Adjustment Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Provision_Clearing as [Provision Clearing]"
                qry += " ,GL_Provision_Account.Description as [Provision Clearing Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Purchase_Account as [Purchase Account]"
                qry += " ,GL_Purchase_Account.Description as [Purchase Account Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Purchase_Control_Account [Purchase Control Account]"
                qry += " ,GL_Purchase_Control_Account.Description as [Purchase Control Account Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Purchase_JobWork as [Purchase Job Work]"
                qry += " ,GL_Purchase_JobWork.Description as [Purchase Job Work Desc] "
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Purchase_Loss as [Purchase Loss]"
                qry += " ,GL_PurchaseLoss.Description as [Purchase Loss Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Purchase_Set_Off as [Purchase Set Off]"
                qry += " ,GL_Purchase_SetOff.Description as [Purchase Set off Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Reserve_Stock [RGP Clearing]"
                qry += " ,GL_Reverse_Stock.Description as [RGP Clearing Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.RM_Consumption as [RM Consumption]"
                qry += " ,GL_RM_Consumption.Description as [RM Consumption Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_Acc as [Stock Transfer]"
                qry += " ,GL_Stock_Transfer_Acc.Description as [Stock Transfer Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_In as [Stock Transfer In]"
                qry += " ,GL_Stock_Transfer_In.Description as [Stock Transfer In Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_JobWork as [Job Work]"
                qry += " ,GL_Stock_Transfer_JobWork.Description as [Job Work Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Store_Consumption_Acc as [Store Consumption]"
                qry += " ,GL_Store_Consumption_Acc.Description as [Store Consumption Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Transfer_Clearing as [Transfer Clearing]"
                qry += " ,GL_Transfer_Clearing.Description as [Transfer clearing Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Transfer_Gain_Loss_Ac as [Gain Loss]"
                qry += " ,GL_Transfer_Gain_Loss_Ac.Description as [Gain Loss Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.WIP_Account as [WIP Account]"
                qry += " ,GL_WIP_Account.Description as [WIP Account Desc]"
                qry += " ,TSPL_PURCHASE_ACCOUNTS.Wrekage_Account as [Wreckage]"
                qry += " ,GL_Wrekage_Account.Description as [Wreckage Desc]"
                qry += " from TSPL_ITEM_MASTER"
                qry += " inner join TSPL_ITEM_TYPE_MASTER on TSPL_ITEM_TYPE_MASTER.Item_Type_Code=TSPL_ITEM_MASTER.Item_Type "
                qry += " left outer join TSPL_PURCHASE_ACCOUNTS on TSPL_PURCHASE_ACCOUNTS.Purchase_Class_Code=TSPL_ITEM_MASTER.Purchase_Class_Code"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Inv_Control_Account on GL_Inv_Control_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Control_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Inv_payable_Clearning on GL_Inv_payable_Clearning.Account_Code=TSPL_PURCHASE_ACCOUNTS.Inv_Payable_Clearing"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Adjustment_Account on GL_Adjustment_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Adjustment_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Shipment_Clearning on GL_Shipment_Clearning.Account_Code=TSPL_PURCHASE_ACCOUNTS.Shipment_Clearing"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Assembly_Cost_Cradit on GL_Assembly_Cost_Cradit.Account_Code=TSPL_PURCHASE_ACCOUNTS.Assembly_Cost_Credit"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Breakge_GL_Account on GL_Breakge_GL_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Breakage_Gl_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Chilling_Charges on GL_Chilling_Charges.Account_Code=TSPL_PURCHASE_ACCOUNTS.Chilling_Charges"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Credit_Debit_Note_Clearning on GL_Credit_Debit_Note_Clearning.Account_Code=TSPL_PURCHASE_ACCOUNTS.Credit_Debit_Note_Clearing"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Difference_Account on GL_Difference_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Difference_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Disassembly_Expense on GL_Disassembly_Expense.Account_Code=TSPL_PURCHASE_ACCOUNTS.Disassembly_Expense"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_FA_Clearning_Ac on GL_FA_Clearning_Ac.Account_Code=TSPL_PURCHASE_ACCOUNTS.FA_CLEARING_AC"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Freight_Charges on GL_Freight_Charges.Account_Code=TSPL_PURCHASE_ACCOUNTS.Freight_Charges"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Handling_Charges on GL_Handling_Charges.Account_Code=TSPL_PURCHASE_ACCOUNTS.Handling_Charge"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Job_Work_Ac on GL_Job_Work_Ac.Account_Code=TSPL_PURCHASE_ACCOUNTS.Job_Work_Ac"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Loss_Ac on GL_Loss_Ac.Account_Code=TSPL_PURCHASE_ACCOUNTS.Loss_Ac"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Non_Stock_Clearning on GL_Non_Stock_Clearning.Account_Code=TSPL_PURCHASE_ACCOUNTS.Non_Stock_Clearing"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Other1 on GL_Other1.Account_Code=TSPL_PURCHASE_ACCOUNTS.Other_1"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Other2 on GL_Other2.Account_Code=TSPL_PURCHASE_ACCOUNTS.Other_2"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Physical_Inv_Adjustment on GL_Physical_Inv_Adjustment.Account_Code=TSPL_PURCHASE_ACCOUNTS.Physical_Inv_Adjustment"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Provision_Account on GL_Provision_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Provision_Clearing"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Purchase_Account on GL_Purchase_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Purchase_Control_Account on GL_Purchase_Control_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Control_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Purchase_JobWork on GL_Purchase_JobWork.Account_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_JobWork"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_PurchaseLoss on GL_PurchaseLoss.Account_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Loss"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Purchase_SetOff on GL_Purchase_SetOff.Account_Code=TSPL_PURCHASE_ACCOUNTS.Purchase_Set_Off"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Reverse_Stock on GL_Reverse_Stock.Account_Code=TSPL_PURCHASE_ACCOUNTS.Reserve_Stock"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_RM_Consumption on GL_RM_Consumption.Account_Code=TSPL_PURCHASE_ACCOUNTS.RM_Consumption"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Stock_Transfer_Acc on GL_Stock_Transfer_Acc.Account_Code=TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_Acc"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Stock_Transfer_In on GL_Stock_Transfer_In.Account_Code=TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_In"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Stock_Transfer_JobWork on GL_Stock_Transfer_JobWork.Account_Code=TSPL_PURCHASE_ACCOUNTS.Stock_Transfer_JobWork"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Store_Consumption_Acc on GL_Store_Consumption_Acc.Account_Code=TSPL_PURCHASE_ACCOUNTS.Store_Consumption_Acc"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Transfer_Clearing on GL_Transfer_Clearing.Account_Code=TSPL_PURCHASE_ACCOUNTS.Transfer_Clearing"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Transfer_Gain_Loss_Ac on GL_Transfer_Gain_Loss_Ac.Account_Code=TSPL_PURCHASE_ACCOUNTS.Transfer_Gain_Loss_Ac"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_WIP_Account on GL_WIP_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.WIP_Account"
                qry += " left outer join TSPL_GL_ACCOUNTS as GL_Wrekage_Account on GL_Wrekage_Account.Account_Code=TSPL_PURCHASE_ACCOUNTS.Wrekage_Account"
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
                If txtItemStructure.arrValueMember IsNot Nothing AndAlso txtItemStructure.arrValueMember.Count > 0 Then
                    qry += " and TSPL_ITEM_MASTER.Structure_Code in (" + clsCommon.GetMulcallString(txtItemStructure.arrValueMember) + ")"
                End If

                Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()

                If dt IsNot Nothing AndAlso dt.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                ElseIf IsPrint = Exporter.Print Then
                    Dim frmCRV As New frmCrystalReportViewer()
                    frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, "crptItemPurchase", "Item Purchase Account Set Report", clsCommon.GETSERVERDATE())
                    frmCRV = Nothing
                Else
                    'gv.DataSource = dt

                    'For ii As Integer = 0 To gv.Columns.Count - 1
                    '    gv.Columns(ii).ReadOnly = True
                    'Next
                    'FormatGridUOM()
                    gv.DataSource = dt
                    FormatGridLoad()

                    'For Each row As DataRow In dt.Rows
                    '    gv.Rows.AddNew()
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(row("Item Code").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = clsCommon.myCstr(row("Item Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colStructureCode).Value = clsCommon.myCstr(row("Structure Code").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colStructureDesc).Value = clsCommon.myCstr(row("Structure Description").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colItemType).Value = clsCommon.myCstr(row("Item type").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colItemTypeDesc).Value = clsCommon.myCstr(row("Item Type Name").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colAccountCode).Value = clsCommon.myCstr(row("Account code").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colAccountDesc).Value = clsCommon.myCstr(row("Account Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colInventory).Value = clsCommon.myCstr(row("Inventory Code").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colInventoryDesc).Value = clsCommon.myCstr(row("Description").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPayableClearing).Value = clsCommon.myCstr(row("Payable Clearing").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPayableClearingDesc).Value = clsCommon.myCstr(row("Payable Clearing Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(ColShipment).Value = clsCommon.myCstr(row("Shipment Clearing").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(ColShipmentDesc).Value = clsCommon.myCstr(row("Shipment Clearing Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colAdj).Value = clsCommon.myCstr(row("Adjustment").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colAdjDesc).Value = clsCommon.myCstr(row("Adjustment Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colFGShortage).Value = clsCommon.myCstr(row("FG Shortage Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colFGShortageDesc).Value = clsCommon.myCstr(row("FG Shortage Account Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colBreakage).Value = clsCommon.myCstr(row("Breakage GL Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colBreakageDesc).Value = clsCommon.myCstr(row("Breakage GL Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colChillingCharges).Value = clsCommon.myCstr(row("Chilling Charges").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colChillingChargesDesc).Value = clsCommon.myCstr(row("Chilling Charges Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colCreditDebitNote).Value = clsCommon.myCstr(row("Credit Debit Note").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colCreditDebitNoteDesc).Value = clsCommon.myCstr(row("Credit Debit Note Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colCreditDebitNoteDesc).Value = clsCommon.myCstr(row("Credit Debit Note Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colDifferenceAccount).Value = clsCommon.myCstr(row("Difference Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colDifferenceAccountDesc).Value = clsCommon.myCstr(row("Difference Account Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colDisassembly).Value = clsCommon.myCstr(row("Disassembly Expense").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colDisassemblyDesc).Value = clsCommon.myCstr(row("Disassembly Expense Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colFAAccount).Value = clsCommon.myCstr(row("FA Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colFAAccountDesc).Value = clsCommon.myCstr(row("FA Account Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colFrieghtCharges).Value = clsCommon.myCstr(row("Freight Charges").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colFrieghtChargesDesc).Value = clsCommon.myCstr(row("Freight Charges Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colHandlingCharges).Value = clsCommon.myCstr(row("Handling Charges").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colHandlingChargesDesc).Value = clsCommon.myCstr(row("Handling Charges Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colJobWorkAC).Value = clsCommon.myCstr(row("Job Work Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colJobWorkACDesc).Value = clsCommon.myCstr(row("Job Work Account Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colLossAccount).Value = clsCommon.myCstr(row("Loss Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colLossAccountDesc).Value = clsCommon.myCstr(row("Loss Account Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colInvControlEmpties).Value = clsCommon.myCstr(row("Inv Control Empties").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colInvControlEmptiesDesc).Value = clsCommon.myCstr(row("Inv Control Empties Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colRejected).Value = clsCommon.myCstr(row("Rejected").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colRejectedDesc).Value = clsCommon.myCstr(row("Rejected Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colShortage).Value = clsCommon.myCstr(row("Shortage").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colShortageDesc).Value = clsCommon.myCstr(row("Shortage Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPhyisalInvAdj).Value = clsCommon.myCstr(row("Physical Inv Adjustment").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPhyisalInvAdjDesc).Value = clsCommon.myCstr(row("Physical Inv Adjustment Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colProvision).Value = clsCommon.myCstr(row("Provision Clearing").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colProvisionDesc).Value = clsCommon.myCstr(row("Provision Clearing Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseAccount).Value = clsCommon.myCstr(row("Purchase Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseAccountDesc).Value = clsCommon.myCstr(row("Purchase Account Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseControl).Value = clsCommon.myCstr(row("Purchase Control Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseControlDesc).Value = clsCommon.myCstr(row("Purchase Control Account Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseJobWork).Value = clsCommon.myCstr(row("Purchase Job Work").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseJobWorkDesc).Value = clsCommon.myCstr(row("Purchase Job Work Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseLoss).Value = clsCommon.myCstr(row("Purchase Loss").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseLossDesc).Value = clsCommon.myCstr(row("Purchase Loss Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseSetOff).Value = clsCommon.myCstr(row("Purchase Set Off").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseSetOffDesc).Value = clsCommon.myCstr(row("Purchase Set Off Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colRGPClearing).Value = clsCommon.myCstr(row("RGP Clearing").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colRGPClearingDesc).Value = clsCommon.myCstr(row("RGP Clearing Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colRM).Value = clsCommon.myCstr(row("RM Consumption").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colRMDesc).Value = clsCommon.myCstr(row("RM Consumption Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colStockTrasnfer).Value = clsCommon.myCstr(row("Stock Transfer").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colStockTrasnferDesc).Value = clsCommon.myCstr(row("Stock Transfer Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colStockTrasnferIn).Value = clsCommon.myCstr(row("Stock Transfer In").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colStockTrasnferInDesc).Value = clsCommon.myCstr(row("Stock Transfer In Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colJobWork).Value = clsCommon.myCstr(row("Job Work").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colJobWorkDesc).Value = clsCommon.myCstr(row("Job Work Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colStoreConsumption).Value = clsCommon.myCstr(row("Store Consumption").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colStoreConsumptionDesc).Value = clsCommon.myCstr(row("Store Consumption Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colTransferClearing).Value = clsCommon.myCstr(row("Transfer Clearing").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colTransferClearingDesc).Value = clsCommon.myCstr(row("Transfer Clearing Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colGainLossAccount).Value = clsCommon.myCstr(row("Gain Loss").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colGainLossAccountDesc).Value = clsCommon.myCstr(row("Gain Loss Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colWIPAccount).Value = clsCommon.myCstr(row("WIP Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colWIPAccountDesc).Value = clsCommon.myCstr(row("WIP Account Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colWreckage).Value = clsCommon.myCstr(row("Wreckage").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colWreckageDesc).Value = clsCommon.myCstr(row("Wreckage Desc").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colConsignmentAc).Value = clsCommon.myCstr(row("Consumption Account").ToString().Trim())
                    '    gv.Rows(gv.Rows.Count - 1).Cells(colConsignmentDesc).Value = clsCommon.myCstr(row("Consumption Desc").ToString().Trim())
                    'Next

                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv.BestFitColumns()
                    isInsideLoadData = True
                End If
            End If
            clsCommon.ProgressBarHide()
            If chkOnlyview.Checked = True Then
                btnUpdate.Enabled = False
            Else
                btnUpdate.Enabled = True
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles RadButton1.Click
        Try
            For ii As Integer = 0 To gv.Columns.Count - 1
                gv.Columns(ii).ReadOnly = False
                gv.Columns(ii).IsVisible = True
                gv.Columns(ii).Width = 100
            Next
            gv.DataSource = Nothing
            gv.Rows.Clear()
            gv.Columns.Clear()


            If transportSql.importExcel(gv, "Item Code", "Item Desc", "Structure Code", "Structure Desc", "Item type", "Item Type Name", "Consumption Account", "Consumption Desc", "Account code", "Account Desc", "Inventory", "Inventory Desc", "Payable Clearing", "Payable Clearing Desc", "Shipment Clearing", "Shipment Clearing Desc", "Adjustment", "Adjustment Desc", "FG Shortage Account", "FG Shortage Account Desc", "Breakage GL Account", "Breakage GL Account Desc", "Chilling Charges", "Chilling Charges Desc", "Credit Debit Note", "Credit Debit Note Desc", "Difference Account", "Difference Account Desc", "Disassembly Expense", "Disassembly Expense Desc", "FA Account", "FA Account Desc", "Freight Charges", "Freight Charges Desc", "Handling Charges", "Handling Charges Desc", "Job Work Account", "Job Work Account Desc", "Loss Account", "Loss Account Desc", "Inv Control Empties", "Inv Control Empties Desc", "Rejected", "Rejected Desc", "Shortage", "Shortage Desc", "Physical Inv Adjustment", "Physical Inv Adjustment Desc", "Provision Clearing", "Provision Clearing Desc", "Purchase Account", "Purchase Account Desc", "Purchase Control", "Purchase Control Desc", "Purchase Job Work", "Purchase Job Work Desc", "Purchase Loss", "Purchase Loss Desc", "Purchase Set Off", "Purchase Set Off Desc", "RGP Clearing", "RGP Clearing Desc", "RM Consumption", "RM Consumption Desc", "Stock Transfer", "Stock Transfer Desc", "Stock Transfer In", "Stock Transfer In Desc", "Job Work", "Job Work Desc", "Store Consumption", "Store Consumption Desc", "Transfer Clearing", "Transfer Clearing Desc", "Gain Loss", "Gain Loss Account", "WIP Account", "WIP Account Desc", "Wreakage", "Wreakage Desc") Then
                clsCommon.ProgressBarPercentShow()

                Dim dt As New DataTable()
                dt = gv.DataSource()
                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()
                FormatGridUOM()
                For Each row As DataRow In dt.Rows
                    gv.Rows.AddNew()
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemCode).Value = clsCommon.myCstr(row("Item Code").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemName).Value = clsCommon.myCstr(row("Item Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colStructureCode).Value = clsCommon.myCstr(row("Structure Code").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colStructureDesc).Value = clsCommon.myCstr(row("Structure Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemType).Value = clsCommon.myCstr(row("Item type").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colItemTypeDesc).Value = clsCommon.myCstr(row("Item Type Name").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccountCode).Value = clsCommon.myCstr(row("Account code").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAccountDesc).Value = clsCommon.myCstr(row("Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colInventory).Value = clsCommon.myCstr(row("Inventory").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colInventoryDesc).Value = clsCommon.myCstr(row("Inventory Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPayableClearing).Value = clsCommon.myCstr(row("Payable Clearing").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPayableClearingDesc).Value = clsCommon.myCstr(row("Payable Clearing Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(ColShipment).Value = clsCommon.myCstr(row("Shipment Clearing").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(ColShipmentDesc).Value = clsCommon.myCstr(row("Shipment Clearing Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAdj).Value = clsCommon.myCstr(row("Adjustment").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colAdjDesc).Value = clsCommon.myCstr(row("Adjustment Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colFGShortage).Value = clsCommon.myCstr(row("FG Shortage Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colFGShortageDesc).Value = clsCommon.myCstr(row("FG Shortage Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colBreakage).Value = clsCommon.myCstr(row("Breakage GL Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colBreakageDesc).Value = clsCommon.myCstr(row("Breakage GL Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colChillingCharges).Value = clsCommon.myCstr(row("Chilling Charges").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colChillingChargesDesc).Value = clsCommon.myCstr(row("Chilling Charges Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colCreditDebitNote).Value = clsCommon.myCstr(row("Credit Debit Note").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colCreditDebitNoteDesc).Value = clsCommon.myCstr(row("Credit Debit Note Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colDifferenceAccount).Value = clsCommon.myCstr(row("Difference Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colDifferenceAccountDesc).Value = clsCommon.myCstr(row("Difference Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colDisassembly).Value = clsCommon.myCstr(row("Disassembly Expense").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colDisassemblyDesc).Value = clsCommon.myCstr(row("Disassembly Expense Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colFAAccount).Value = clsCommon.myCstr(row("FA Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colFAAccountDesc).Value = clsCommon.myCstr(row("FA Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colFrieghtCharges).Value = clsCommon.myCstr(row("Freight Charges").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colFrieghtChargesDesc).Value = clsCommon.myCstr(row("Freight Charges Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colHandlingCharges).Value = clsCommon.myCstr(row("Handling Charges").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colHandlingChargesDesc).Value = clsCommon.myCstr(row("Handling Charges Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colJobWorkAC).Value = clsCommon.myCstr(row("Job Work Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colJobWorkACDesc).Value = clsCommon.myCstr(row("Job Work Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colLossAccount).Value = clsCommon.myCstr(row("Loss Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colLossAccountDesc).Value = clsCommon.myCstr(row("Loss Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colInvControlEmpties).Value = clsCommon.myCstr(row("Inv Control Empties").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colInvControlEmptiesDesc).Value = clsCommon.myCstr(row("Inv Control Empties Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colRejected).Value = clsCommon.myCstr(row("Rejected").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colRejectedDesc).Value = clsCommon.myCstr(row("Rejected Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colShortage).Value = clsCommon.myCstr(row("Shortage").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colShortageDesc).Value = clsCommon.myCstr(row("Shortage Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPhyisalInvAdj).Value = clsCommon.myCstr(row("Physical Inv Adjustment").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPhyisalInvAdjDesc).Value = clsCommon.myCstr(row("Physical Inv Adjustment Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colProvision).Value = clsCommon.myCstr(row("Provision Clearing").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colProvisionDesc).Value = clsCommon.myCstr(row("Provision Clearing Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseAccount).Value = clsCommon.myCstr(row("Purchase Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseAccountDesc).Value = clsCommon.myCstr(row("Purchase Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseControl).Value = clsCommon.myCstr(row("Purchase Control").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseControlDesc).Value = clsCommon.myCstr(row("Purchase Control Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseJobWork).Value = clsCommon.myCstr(row("Purchase Job Work").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseJobWorkDesc).Value = clsCommon.myCstr(row("Purchase Job Work Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseLoss).Value = clsCommon.myCstr(row("Purchase Loss").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseLossDesc).Value = clsCommon.myCstr(row("Purchase Loss Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseSetOff).Value = clsCommon.myCstr(row("Purchase Set Off").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colPurchaseSetOffDesc).Value = clsCommon.myCstr(row("Purchase Set Off Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colRGPClearing).Value = clsCommon.myCstr(row("RGP Clearing").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colRGPClearingDesc).Value = clsCommon.myCstr(row("RGP Clearing Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colRM).Value = clsCommon.myCstr(row("RM Consumption").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colRMDesc).Value = clsCommon.myCstr(row("RM Consumption Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colStockTrasnfer).Value = clsCommon.myCstr(row("Stock Transfer").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colStockTrasnferDesc).Value = clsCommon.myCstr(row("Stock Transfer Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colStockTrasnferIn).Value = clsCommon.myCstr(row("Stock Transfer In").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colStockTrasnferInDesc).Value = clsCommon.myCstr(row("Stock Transfer In Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colJobWork).Value = clsCommon.myCstr(row("Job Work").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colJobWorkDesc).Value = clsCommon.myCstr(row("Job Work Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colStoreConsumption).Value = clsCommon.myCstr(row("Store Consumption").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colStoreConsumptionDesc).Value = clsCommon.myCstr(row("Store Consumption Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colTransferClearing).Value = clsCommon.myCstr(row("Transfer Clearing").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colTransferClearingDesc).Value = clsCommon.myCstr(row("Transfer Clearing Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colGainLossAccount).Value = clsCommon.myCstr(row("Gain Loss").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colGainLossAccountDesc).Value = clsCommon.myCstr(row("Gain Loss Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colWIPAccount).Value = clsCommon.myCstr(row("WIP Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colWIPAccountDesc).Value = clsCommon.myCstr(row("WIP Account Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colWreckage).Value = clsCommon.myCstr(row("Wreakage").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colWreckageDesc).Value = clsCommon.myCstr(row("Wreakage Desc").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colConsignmentAc).Value = clsCommon.myCstr(row("Consumption Account").ToString().Trim())
                    gv.Rows(gv.Rows.Count - 1).Cells(colConsignmentDesc).Value = clsCommon.myCstr(row("Consumption Desc").ToString().Trim())
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
    'Ticket No-TEC/08/08/19-000983
    Private Sub FormatGridLoad()

        gv.Columns("Item Code").Name = colItemCode
        gv.Columns(colItemCode).FormatString = ""
        gv.Columns(colItemCode).HeaderText = "Item Code"
        gv.Columns(colItemCode).Width = 100
        gv.Columns(colItemCode).ReadOnly = True

        gv.Columns("Item Desc").Name = colItemName
        gv.Columns(colItemName).FormatString = ""
        gv.Columns(colItemName).HeaderText = "Item Desc"
        gv.Columns(colItemName).Width = 250
        gv.Columns(colItemName).ReadOnly = True

        gv.Columns("Structure Code").Name = colStructureCode
        gv.Columns(colStructureCode).FormatString = ""
        gv.Columns(colStructureCode).HeaderText = "Structure Code"
        gv.Columns(colStructureCode).Width = 100
        gv.Columns(colStructureCode).ReadOnly = True

        gv.Columns("Structure Description").Name = colStructureDesc
        gv.Columns(colStructureDesc).FormatString = ""
        gv.Columns(colStructureDesc).HeaderText = "Structure Desc"
        gv.Columns(colStructureDesc).Width = 100
        gv.Columns(colStructureDesc).ReadOnly = True

        gv.Columns("Item type").Name = colItemType
        gv.Columns(colItemType).FormatString = ""
        gv.Columns(colItemType).HeaderText = "Item Type"
        gv.Columns(colItemType).Width = 100
        gv.Columns(colItemType).ReadOnly = True

        gv.Columns("Item Type Name").Name = colItemTypeDesc
        gv.Columns(colItemTypeDesc).FormatString = ""
        gv.Columns(colItemTypeDesc).HeaderText = "Item Type Name"
        gv.Columns(colItemTypeDesc).Width = 100
        gv.Columns(colItemTypeDesc).ReadOnly = True

        gv.Columns("Consumption Account").Name = colConsignmentAc
        gv.Columns(colConsignmentAc).FormatString = ""
        gv.Columns(colConsignmentAc).HeaderText = "Consumption Account"
        gv.Columns(colConsignmentAc).Width = 250
        gv.Columns(colConsignmentAc).ReadOnly = True

        gv.Columns("Consumption Desc").Name = colConsignmentDesc
        gv.Columns(colConsignmentDesc).FormatString = ""
        gv.Columns(colConsignmentDesc).HeaderText = "Consumption Desc"
        gv.Columns(colConsignmentDesc).Width = 250
        gv.Columns(colConsignmentDesc).ReadOnly = True

        gv.Columns("Account code").Name = colAccountCode
        gv.Columns(colAccountCode).FormatString = ""
        gv.Columns(colAccountCode).HeaderText = "Account Code"
        gv.Columns(colAccountCode).Width = 100
        gv.Columns(colAccountCode).ReadOnly = True

        gv.Columns("Account Desc").Name = colAccountDesc
        gv.Columns(colAccountDesc).FormatString = ""
        gv.Columns(colAccountDesc).HeaderText = "Account Desc"
        gv.Columns(colAccountDesc).Width = 100
        gv.Columns(colAccountDesc).ReadOnly = True

        gv.Columns("Inventory Code").Name = colInventory
        gv.Columns(colInventory).FormatString = ""
        gv.Columns(colInventory).HeaderText = "Inventory"
        gv.Columns(colInventory).Width = 100
        gv.Columns(colInventory).ReadOnly = False

        gv.Columns("Description").Name = colInventoryDesc
        gv.Columns(colInventoryDesc).FormatString = ""
        gv.Columns(colInventoryDesc).HeaderText = "Inventory Desc"
        gv.Columns(colInventoryDesc).Width = 100
        gv.Columns(colInventoryDesc).ReadOnly = True

        gv.Columns("Payable Clearing").Name = colPayableClearing
        gv.Columns(colPayableClearing).FormatString = ""
        gv.Columns(colPayableClearing).HeaderText = "Payable Clearing"
        gv.Columns(colPayableClearing).Width = 100
        gv.Columns(colPayableClearing).ReadOnly = False

        gv.Columns("Payable Clearing Desc").Name = colPayableClearingDesc
        gv.Columns(colPayableClearingDesc).FormatString = ""
        gv.Columns(colPayableClearingDesc).HeaderText = "Payable Clearing Desc"
        gv.Columns(colPayableClearingDesc).Width = 100
        gv.Columns(colPayableClearingDesc).ReadOnly = True

        gv.Columns("Shipment Clearing").Name = ColShipment
        gv.Columns(ColShipment).FormatString = ""
        gv.Columns(ColShipment).HeaderText = "Shipment Clearing"
        gv.Columns(ColShipment).Width = 100
        gv.Columns(ColShipment).ReadOnly = False

        gv.Columns("Shipment Clearing Desc").Name = ColShipmentDesc
        gv.Columns(ColShipmentDesc).FormatString = ""
        gv.Columns(ColShipmentDesc).HeaderText = "Shipment Clearing Desc"
        gv.Columns(ColShipmentDesc).Width = 100
        gv.Columns(ColShipmentDesc).ReadOnly = True

        gv.Columns("Adjustment").Name = colAdj
        gv.Columns(colAdj).FormatString = ""
        gv.Columns(colAdj).HeaderText = "Adjustment"
        gv.Columns(colAdj).Width = 100
        gv.Columns(colAdj).ReadOnly = False

        gv.Columns("Adjustment Desc").Name = colAdjDesc
        gv.Columns(colAdjDesc).FormatString = ""
        gv.Columns(colAdjDesc).HeaderText = "Adjustment Desc"
        gv.Columns(colAdjDesc).Width = 100
        gv.Columns(colAdjDesc).ReadOnly = True

        gv.Columns("FG Shortage Account").Name = colFGShortage
        gv.Columns(colFGShortage).FormatString = ""
        gv.Columns(colFGShortage).HeaderText = "FG Shortage Account"
        gv.Columns(colFGShortage).Width = 100
        gv.Columns(colFGShortage).ReadOnly = False

        gv.Columns("FG Shortage Account Desc").Name = colFGShortageDesc
        gv.Columns(colFGShortageDesc).FormatString = ""
        gv.Columns(colFGShortageDesc).HeaderText = "FG Shortage Account Desc"
        gv.Columns(colFGShortageDesc).Width = 100
        gv.Columns(colFGShortageDesc).ReadOnly = True

        gv.Columns("Breakage GL Account").Name = colBreakage
        gv.Columns(colBreakage).FormatString = ""
        gv.Columns(colBreakage).HeaderText = "Breakage GL Account"
        gv.Columns(colBreakage).Width = 100
        gv.Columns(colBreakage).ReadOnly = False

        gv.Columns("Breakage GL Desc").Name = colBreakageDesc
        gv.Columns(colBreakageDesc).FormatString = ""
        gv.Columns(colBreakageDesc).HeaderText = "Breakage GL Account Desc"
        gv.Columns(colBreakageDesc).Width = 100
        gv.Columns(colBreakageDesc).ReadOnly = True

        gv.Columns("Chilling Charges").Name = colChillingCharges
        gv.Columns(colChillingCharges).FormatString = ""
        gv.Columns(colChillingCharges).HeaderText = "Chilling Charges"
        gv.Columns(colChillingCharges).Width = 100
        gv.Columns(colChillingCharges).ReadOnly = False

        gv.Columns("Chilling Charges Desc").Name = colChillingChargesDesc
        gv.Columns(colChillingChargesDesc).FormatString = ""
        gv.Columns(colChillingChargesDesc).HeaderText = "Chilling Charges Desc"
        gv.Columns(colChillingChargesDesc).Width = 100
        gv.Columns(colChillingChargesDesc).ReadOnly = True

        gv.Columns("Credit Debit Note").Name = colCreditDebitNote
        gv.Columns(colCreditDebitNote).FormatString = ""
        gv.Columns(colCreditDebitNote).HeaderText = "Credit Debit Note"
        gv.Columns(colCreditDebitNote).Width = 100
        gv.Columns(colCreditDebitNote).ReadOnly = False

        gv.Columns("Credit Debit Note Desc").Name = colCreditDebitNoteDesc
        gv.Columns(colCreditDebitNoteDesc).FormatString = ""
        gv.Columns(colCreditDebitNoteDesc).HeaderText = "Credit Debit Note Desc"
        gv.Columns(colCreditDebitNoteDesc).Width = 100
        gv.Columns(colCreditDebitNoteDesc).ReadOnly = True

        gv.Columns("Difference Account").Name = colDifferenceAccount
        gv.Columns(colDifferenceAccount).FormatString = ""
        gv.Columns(colDifferenceAccount).HeaderText = "Difference Account"
        gv.Columns(colDifferenceAccount).Width = 100
        gv.Columns(colDifferenceAccount).ReadOnly = False

        gv.Columns("Difference Account Desc").Name = colDifferenceAccountDesc
        gv.Columns(colDifferenceAccountDesc).FormatString = ""
        gv.Columns(colDifferenceAccountDesc).HeaderText = "Difference Account Desc"
        gv.Columns(colDifferenceAccountDesc).Width = 100
        gv.Columns(colDifferenceAccountDesc).ReadOnly = True

        gv.Columns("Disassembly Expense").Name = colDisassembly
        gv.Columns(colDisassembly).FormatString = ""
        gv.Columns(colDisassembly).HeaderText = "Disassembly Expense"
        gv.Columns(colDisassembly).Width = 100
        gv.Columns(colDisassembly).ReadOnly = False

        gv.Columns("Disassembly Expense Desc").Name = colDisassemblyDesc
        gv.Columns(colDisassemblyDesc).FormatString = ""
        gv.Columns(colDisassemblyDesc).HeaderText = "Disassembly Expense Desc"
        gv.Columns(colDisassemblyDesc).Width = 100
        gv.Columns(colDisassemblyDesc).ReadOnly = True

        gv.Columns("FA Account").Name = colFAAccount
        gv.Columns(colFAAccount).FormatString = ""
        gv.Columns(colFAAccount).HeaderText = "FA Account"
        gv.Columns(colFAAccount).Width = 100
        gv.Columns(colFAAccount).ReadOnly = False

        gv.Columns("FA Account Desc").Name = colFAAccountDesc
        gv.Columns(colFAAccountDesc).FormatString = ""
        gv.Columns(colFAAccountDesc).HeaderText = "FA Account Desc"
        gv.Columns(colFAAccountDesc).Width = 100
        gv.Columns(colFAAccountDesc).ReadOnly = True

        gv.Columns("Freight Charges").Name = colFrieghtCharges
        gv.Columns(colFrieghtCharges).FormatString = ""
        gv.Columns(colFrieghtCharges).HeaderText = "Freight Charges"
        gv.Columns(colFrieghtCharges).Width = 100
        gv.Columns(colFrieghtCharges).ReadOnly = False

        gv.Columns("Freight Charges Desc").Name = colFrieghtChargesDesc
        gv.Columns(colFrieghtChargesDesc).FormatString = ""
        gv.Columns(colFrieghtChargesDesc).HeaderText = "Freight Charges Desc"
        gv.Columns(colFrieghtChargesDesc).Width = 100
        gv.Columns(colFrieghtChargesDesc).ReadOnly = True

        gv.Columns("Handling Charges").Name = colHandlingCharges
        gv.Columns(colHandlingCharges).FormatString = ""
        gv.Columns(colHandlingCharges).HeaderText = "Handling Charges"
        gv.Columns(colHandlingCharges).Width = 100
        gv.Columns(colHandlingCharges).ReadOnly = False

        gv.Columns("Handling Charges Desc").Name = colHandlingChargesDesc
        gv.Columns(colHandlingChargesDesc).FormatString = ""
        gv.Columns(colHandlingChargesDesc).HeaderText = "Handling Charges Desc"
        gv.Columns(colHandlingChargesDesc).Width = 100
        gv.Columns(colHandlingChargesDesc).ReadOnly = True

        gv.Columns("Job Work Account").Name = colJobWorkAC
        gv.Columns(colJobWorkAC).FormatString = ""
        gv.Columns(colJobWorkAC).HeaderText = "Job Work Account"
        gv.Columns(colJobWorkAC).Width = 100
        gv.Columns(colJobWorkAC).ReadOnly = False

        gv.Columns("Job Work Account Desc").Name = colJobWorkACDesc
        gv.Columns(colJobWorkACDesc).FormatString = ""
        gv.Columns(colJobWorkACDesc).HeaderText = "Job Work Account Desc"
        gv.Columns(colJobWorkACDesc).Width = 100
        gv.Columns(colJobWorkACDesc).ReadOnly = True

        gv.Columns("Loss Account").Name = colLossAccount
        gv.Columns(colLossAccount).FormatString = ""
        gv.Columns(colLossAccount).HeaderText = "Loss Account"
        gv.Columns(colLossAccount).Width = 100
        gv.Columns(colLossAccount).ReadOnly = False

        gv.Columns("Loss Account Desc").Name = colLossAccountDesc
        gv.Columns(colLossAccountDesc).FormatString = ""
        gv.Columns(colLossAccountDesc).HeaderText = "Loss Account Desc"
        gv.Columns(colLossAccountDesc).Width = 100
        gv.Columns(colLossAccountDesc).ReadOnly = True

        gv.Columns("Inv Control Empties").Name = colInvControlEmpties
        gv.Columns(colInvControlEmpties).FormatString = ""
        gv.Columns(colInvControlEmpties).HeaderText = "Inv Control Empties"
        gv.Columns(colInvControlEmpties).Width = 100
        gv.Columns(colInvControlEmpties).ReadOnly = False

        gv.Columns("Inv Control Empties Desc").Name = colInvControlEmptiesDesc
        gv.Columns(colInvControlEmptiesDesc).FormatString = ""
        gv.Columns(colInvControlEmptiesDesc).HeaderText = "Inv Control Empties Desc"
        gv.Columns(colInvControlEmptiesDesc).Width = 100
        gv.Columns(colInvControlEmptiesDesc).ReadOnly = True

        gv.Columns("Rejected").Name = colRejected
        gv.Columns(colRejected).FormatString = ""
        gv.Columns(colRejected).HeaderText = "Rejected"
        gv.Columns(colRejected).Width = 100
        gv.Columns(colRejected).ReadOnly = False

        gv.Columns("Rejected Desc").Name = colRejectedDesc
        gv.Columns(colRejectedDesc).FormatString = ""
        gv.Columns(colRejectedDesc).HeaderText = "Rejected Desc"
        gv.Columns(colRejectedDesc).Width = 100
        gv.Columns(colRejectedDesc).ReadOnly = True

        gv.Columns("Shortage").Name = colShortage
        gv.Columns(colShortage).FormatString = ""
        gv.Columns(colShortage).HeaderText = "Shortage"
        gv.Columns(colShortage).Width = 100
        gv.Columns(colShortage).ReadOnly = False

        gv.Columns("Shortage Desc").Name = colShortageDesc
        gv.Columns(colShortageDesc).FormatString = ""
        gv.Columns(colShortageDesc).HeaderText = "Shortage Desc"
        gv.Columns(colShortageDesc).Width = 100
        gv.Columns(colShortageDesc).ReadOnly = True

        gv.Columns("Physical Inv Adjustment").Name = colPhyisalInvAdj
        gv.Columns(colPhyisalInvAdj).FormatString = ""
        gv.Columns(colPhyisalInvAdj).HeaderText = "Physical Inv Adjustment"
        gv.Columns(colPhyisalInvAdj).Width = 100
        gv.Columns(colPhyisalInvAdj).ReadOnly = False

        gv.Columns("Physical Inv Adjustment Desc").Name = colPhyisalInvAdjDesc
        gv.Columns(colPhyisalInvAdjDesc).FormatString = ""
        gv.Columns(colPhyisalInvAdjDesc).HeaderText = "Physical Inv Adjustment Desc"
        gv.Columns(colPhyisalInvAdjDesc).Width = 100
        gv.Columns(colPhyisalInvAdjDesc).ReadOnly = True

        gv.Columns("Provision Clearing").Name = colProvision
        gv.Columns(colProvision).FormatString = ""
        gv.Columns(colProvision).HeaderText = "Provision Clearing"
        gv.Columns(colProvision).Width = 100
        gv.Columns(colProvision).ReadOnly = False

        gv.Columns("Provision Clearing Desc").Name = colProvisionDesc
        gv.Columns(colProvisionDesc).FormatString = ""
        gv.Columns(colProvisionDesc).HeaderText = "Provision Clearing Desc"
        gv.Columns(colProvisionDesc).Width = 100
        gv.Columns(colProvisionDesc).ReadOnly = True

        gv.Columns("Purchase Account").Name = colPurchaseAccount
        gv.Columns(colPurchaseAccount).FormatString = ""
        gv.Columns(colPurchaseAccount).HeaderText = "Purchase Account"
        gv.Columns(colPurchaseAccount).Width = 100
        gv.Columns(colPurchaseAccount).ReadOnly = False

        gv.Columns("Purchase Account Desc").Name = colPurchaseAccountDesc
        gv.Columns(colPurchaseAccountDesc).FormatString = ""
        gv.Columns(colPurchaseAccountDesc).HeaderText = "Purchase Account Desc"
        gv.Columns(colPurchaseAccountDesc).Width = 100
        gv.Columns(colPurchaseAccountDesc).ReadOnly = True

        gv.Columns("Purchase Control Account").Name = colPurchaseControl
        gv.Columns(colPurchaseControl).FormatString = ""
        gv.Columns(colPurchaseControl).HeaderText = "Purchase Control"
        gv.Columns(colPurchaseControl).Width = 100
        gv.Columns(colPurchaseControl).ReadOnly = False

        gv.Columns("Purchase Control Account Desc").Name = colPurchaseControlDesc
        gv.Columns(colPurchaseControlDesc).FormatString = ""
        gv.Columns(colPurchaseControlDesc).HeaderText = "Purchase Control Desc"
        gv.Columns(colPurchaseControlDesc).Width = 100
        gv.Columns(colPurchaseControlDesc).ReadOnly = True

        gv.Columns("Purchase Job Work").Name = colPurchaseJobWork
        gv.Columns(colPurchaseJobWork).FormatString = ""
        gv.Columns(colPurchaseJobWork).HeaderText = "Purchase Job Work"
        gv.Columns(colPurchaseJobWork).Width = 100
        gv.Columns(colPurchaseJobWork).ReadOnly = False

        gv.Columns("Purchase Job Work Desc").Name = colPurchaseJobWorkDesc
        gv.Columns(colPurchaseJobWorkDesc).FormatString = ""
        gv.Columns(colPurchaseJobWorkDesc).HeaderText = "Purchase Job Work Desc"
        gv.Columns(colPurchaseJobWorkDesc).Width = 100
        gv.Columns(colPurchaseJobWorkDesc).ReadOnly = True

        gv.Columns("Purchase Loss").Name = colPurchaseLoss
        gv.Columns(colPurchaseLoss).FormatString = ""
        gv.Columns(colPurchaseLoss).HeaderText = "Purchase loss"
        gv.Columns(colPurchaseLoss).Width = 100
        gv.Columns(colPurchaseLoss).ReadOnly = False

        gv.Columns("Purchase Loss Desc").Name = colPurchaseLossDesc
        gv.Columns(colPurchaseLossDesc).FormatString = ""
        gv.Columns(colPurchaseLossDesc).HeaderText = "Purchase loss Desc"
        gv.Columns(colPurchaseLossDesc).Width = 100
        gv.Columns(colPurchaseLossDesc).ReadOnly = True

        gv.Columns("Purchase Set Off").Name = colPurchaseSetOff
        gv.Columns(colPurchaseSetOff).FormatString = ""
        gv.Columns(colPurchaseSetOff).HeaderText = "Purchase Set Off"
        gv.Columns(colPurchaseSetOff).Width = 100
        gv.Columns(colPurchaseSetOff).ReadOnly = False

        gv.Columns("Purchase Set Off Desc").Name = colPurchaseSetOffDesc
        gv.Columns(colPurchaseSetOffDesc).FormatString = ""
        gv.Columns(colPurchaseSetOffDesc).HeaderText = "Purchase Set Off Desc"
        gv.Columns(colPurchaseSetOffDesc).Width = 100
        gv.Columns(colPurchaseSetOffDesc).ReadOnly = True

        gv.Columns("RGP Clearing").Name = colRGPClearing
        gv.Columns(colRGPClearing).FormatString = ""
        gv.Columns(colRGPClearing).HeaderText = "RGP Clearing"
        gv.Columns(colRGPClearing).Width = 100
        gv.Columns(colRGPClearing).ReadOnly = False

        gv.Columns("RGP Clearing Desc").Name = colRGPClearingDesc
        gv.Columns(colRGPClearingDesc).FormatString = ""
        gv.Columns(colRGPClearingDesc).HeaderText = "RGP Clearing Desc"
        gv.Columns(colRGPClearingDesc).Width = 100
        gv.Columns(colRGPClearingDesc).ReadOnly = True

        gv.Columns("RM Consumption").Name = colRM
        gv.Columns(colRM).FormatString = ""
        gv.Columns(colRM).HeaderText = "RM Consumption"
        gv.Columns(colRM).Width = 100
        gv.Columns(colRM).ReadOnly = False

        gv.Columns("RM Consumption Desc").Name = colRMDesc
        gv.Columns(colRMDesc).FormatString = ""
        gv.Columns(colRMDesc).HeaderText = "RM Consumption Desc"
        gv.Columns(colRMDesc).Width = 100
        gv.Columns(colRMDesc).ReadOnly = True

        gv.Columns("Stock Transfer").Name = colStockTrasnfer
        gv.Columns(colStockTrasnfer).FormatString = ""
        gv.Columns(colStockTrasnfer).HeaderText = "Stock Transfer"
        gv.Columns(colStockTrasnfer).Width = 100
        gv.Columns(colStockTrasnfer).ReadOnly = False

        gv.Columns("Stock Transfer Desc").Name = colStockTrasnferDesc
        gv.Columns(colStockTrasnferDesc).FormatString = ""
        gv.Columns(colStockTrasnferDesc).HeaderText = "Stock Transfer Desc"
        gv.Columns(colStockTrasnferDesc).Width = 100
        gv.Columns(colStockTrasnferDesc).ReadOnly = True

        gv.Columns("Stock Transfer In").Name = colStockTrasnferIn
        gv.Columns(colStockTrasnferIn).FormatString = ""
        gv.Columns(colStockTrasnferIn).HeaderText = "Stock Transfer In"
        gv.Columns(colStockTrasnferIn).Width = 100
        gv.Columns(colStockTrasnferIn).ReadOnly = False

        gv.Columns("Stock Transfer In Desc").Name = colStockTrasnferInDesc
        gv.Columns(colStockTrasnferInDesc).FormatString = ""
        gv.Columns(colStockTrasnferInDesc).HeaderText = "Stock Transfer In Desc"
        gv.Columns(colStockTrasnferInDesc).Width = 100
        gv.Columns(colStockTrasnferInDesc).ReadOnly = True


        gv.Columns("Job Work").Name = colJobWork
        gv.Columns(colJobWork).FormatString = ""
        gv.Columns(colJobWork).HeaderText = "Job Work"
        gv.Columns(colJobWork).Width = 100
        gv.Columns(colJobWork).ReadOnly = False

        gv.Columns("Job Work Desc").Name = colJobWorkDesc
        gv.Columns(colJobWorkDesc).FormatString = ""
        gv.Columns(colJobWorkDesc).HeaderText = "Job Work Desc"
        gv.Columns(colJobWorkDesc).Width = 100
        gv.Columns(colJobWorkDesc).ReadOnly = True

        gv.Columns("Store Consumption").Name = colStoreConsumption
        gv.Columns(colStoreConsumption).FormatString = ""
        gv.Columns(colStoreConsumption).HeaderText = "Store Consumption"
        gv.Columns(colStoreConsumption).Width = 100
        gv.Columns(colStoreConsumption).ReadOnly = False

        gv.Columns("Store Consumption Desc").Name = colStoreConsumptionDesc
        gv.Columns(colStoreConsumptionDesc).FormatString = ""
        gv.Columns(colStoreConsumptionDesc).HeaderText = "Store Consumption Desc"
        gv.Columns(colStoreConsumptionDesc).Width = 100
        gv.Columns(colStoreConsumptionDesc).ReadOnly = True

        gv.Columns("Transfer Clearing").Name = colTransferClearing
        gv.Columns(colTransferClearing).FormatString = ""
        gv.Columns(colTransferClearing).HeaderText = "Transfer Clearing"
        gv.Columns(colTransferClearing).Width = 100
        gv.Columns(colTransferClearing).ReadOnly = False

        gv.Columns("Transfer Clearing Desc").Name = colTransferClearingDesc
        gv.Columns(colTransferClearingDesc).FormatString = ""
        gv.Columns(colTransferClearingDesc).HeaderText = "Transfer Clearing Desc"
        gv.Columns(colTransferClearingDesc).Width = 100
        gv.Columns(colTransferClearingDesc).ReadOnly = True

        gv.Columns("Gain Loss").Name = colGainLossAccount
        gv.Columns(colGainLossAccount).FormatString = ""
        gv.Columns(colGainLossAccount).HeaderText = "Gain Loss"
        gv.Columns(colGainLossAccount).Width = 100
        gv.Columns(colGainLossAccount).ReadOnly = False

        gv.Columns("Gain Loss Desc").Name = colGainLossAccountDesc
        gv.Columns(colGainLossAccountDesc).FormatString = ""
        gv.Columns(colGainLossAccountDesc).HeaderText = "Gain Loss Account"
        gv.Columns(colGainLossAccountDesc).Width = 100
        gv.Columns(colGainLossAccountDesc).ReadOnly = True

        gv.Columns("WIP Account").Name = colWIPAccount
        gv.Columns(colWIPAccount).FormatString = ""
        gv.Columns(colWIPAccount).HeaderText = "WIP Account"
        gv.Columns(colWIPAccount).Width = 100
        gv.Columns(colWIPAccount).ReadOnly = False

        gv.Columns("WIP Account Desc").Name = colWIPAccountDesc
        gv.Columns(colWIPAccountDesc).FormatString = ""
        gv.Columns(colWIPAccountDesc).HeaderText = "WIP Account Desc"
        gv.Columns(colWIPAccountDesc).Width = 100
        gv.Columns(colWIPAccountDesc).ReadOnly = True

        gv.Columns("Wreckage").Name = colWreckage
        gv.Columns(colWreckage).FormatString = ""
        gv.Columns(colWreckage).HeaderText = "Wreakage"
        gv.Columns(colWreckage).Width = 100
        gv.Columns(colWreckage).ReadOnly = False

        gv.Columns("Wreckage Desc").Name = colWreckageDesc
        gv.Columns(colWreckageDesc).FormatString = ""
        gv.Columns(colWreckageDesc).HeaderText = "Wreakage Desc"
        gv.Columns(colWreckageDesc).Width = 100
        gv.Columns(colWreckageDesc).ReadOnly = True

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
        repoString.HeaderText = "Item Desc"
        repoString.Name = colItemName
        repoString.Width = 250
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Structure Code"
        repoString.Name = colStructureCode
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Structure Desc"
        repoString.Name = colStructureDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Type"
        repoString.Name = colItemType
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)



        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Item Type Name"
        repoString.Name = colItemTypeDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Consumption Account"
        repoString.Name = colConsignmentAc
        repoString.Width = 250
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Consumption Desc"
        repoString.Name = colConsignmentDesc
        repoString.Width = 250
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)


        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Account Code"
        repoString.Name = colAccountCode
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
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
        repoString.Name = colPayableClearing
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Payable Clearing Desc"
        repoString.Name = colPayableClearingDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Shipment Clearing"
        repoString.Name = ColShipment
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Shipment Clearing Desc"
        repoString.Name = ColShipmentDesc
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
        repoString.HeaderText = "FG Shortage Account"
        repoString.Name = colFGShortage
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "FG Shortage Account Desc"
        repoString.Name = colFGShortageDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Breakage GL Account"
        repoString.Name = colBreakage
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Breakage GL Account Desc"
        repoString.Name = colBreakageDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Chilling Charges"
        repoString.Name = colChillingCharges
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Chilling Charges Desc"
        repoString.Name = colChillingChargesDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Credit Debit Note"
        repoString.Name = colCreditDebitNote
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Credit Debit Note Desc"
        repoString.Name = colCreditDebitNoteDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Difference Account"
        repoString.Name = colDifferenceAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Difference Account Desc"
        repoString.Name = colDifferenceAccountDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Disassembly Expense"
        repoString.Name = colDisassembly
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Disassembly Expense Desc"
        repoString.Name = colDisassemblyDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "FA Account"
        repoString.Name = colFAAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "FA Account Desc"
        repoString.Name = colFAAccountDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Freight Charges"
        repoString.Name = colFrieghtCharges
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Freight Charges Desc"
        repoString.Name = colFrieghtChargesDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Handling Charges"
        repoString.Name = colHandlingCharges
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Handling Charges Desc"
        repoString.Name = colHandlingChargesDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Job Work Account"
        repoString.Name = colJobWorkAC
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Job Work Account Desc"
        repoString.Name = colJobWorkACDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Loss Account"
        repoString.Name = colLossAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Loss Account Desc"
        repoString.Name = colLossAccountDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Inv Control Empties"
        repoString.Name = colInvControlEmpties
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Inv Control Empties Desc"
        repoString.Name = colInvControlEmptiesDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Rejected"
        repoString.Name = colRejected
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Rejected Desc"
        repoString.Name = colRejectedDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Shortage"
        repoString.Name = colShortage
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Shortage Desc"
        repoString.Name = colShortageDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Physical Inv Adjustment"
        repoString.Name = colPhyisalInvAdj
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Physical Inv Adjustment Desc"
        repoString.Name = colPhyisalInvAdjDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Provision Clearing"
        repoString.Name = colProvision
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Provision Clearing Desc"
        repoString.Name = colProvisionDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Account"
        repoString.Name = colPurchaseAccount
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Account Desc"
        repoString.Name = colPurchaseAccountDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Control"
        repoString.Name = colPurchaseControl
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Control Desc"
        repoString.Name = colPurchaseControlDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Job Work"
        repoString.Name = colPurchaseJobWork
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Job Work Desc"
        repoString.Name = colPurchaseJobWorkDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase loss"
        repoString.Name = colPurchaseLoss
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase loss Desc"
        repoString.Name = colPurchaseLossDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Set Off"
        repoString.Name = colPurchaseSetOff
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Purchase Set Off Desc"
        repoString.Name = colPurchaseSetOffDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "RGP Clearing"
        repoString.Name = colRGPClearing
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "RGP Clearing Desc"
        repoString.Name = colRGPClearingDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "RM Consumption"
        repoString.Name = colRM
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "RM Consumption Desc"
        repoString.Name = colRMDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Stock Transfer"
        repoString.Name = colStockTrasnfer
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Stock Transfer Desc"
        repoString.Name = colStockTrasnferDesc
        repoString.Width = 100
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Stock Transfer In"
        repoString.Name = colStockTrasnferIn
        repoString.Width = 100
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Stock Transfer In Desc"
        repoString.Name = colStockTrasnferInDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Job Work"
        repoString.Name = colJobWork
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Job Work Desc"
        repoString.Name = colJobWorkDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Store Consumption"
        repoString.Name = colStoreConsumption
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Store Consumption Desc"
        repoString.Name = colStoreConsumptionDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Transfer Clearing"
        repoString.Name = colTransferClearing
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Transfer Clearing Desc"
        repoString.Name = colTransferClearingDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Gain Loss"
        repoString.Name = colGainLossAccount
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Gain Loss Account"
        repoString.Name = colGainLossAccountDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "WIP Account"
        repoString.Name = colWIPAccount
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "WIP Account Desc"
        repoString.Name = colWIPAccountDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Wreakage"
        repoString.Name = colWreckage
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = False
        gv.MasterTemplate.Columns.Add(repoString)

        repoString = New GridViewTextBoxColumn()
        repoString.FormatString = ""
        repoString.HeaderText = "Wreakage Desc"
        repoString.Name = colWreckageDesc
        repoString.Width = 100
        'repoString.WrapText = True
        repoString.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoString)




    End Sub
    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub
    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtPurchaseSet._My_Click
        Dim qry As String
        qry = " select Purchase_Class_Code as Code,Purchase_Class_Desc as [Description] from TSPL_PURCHASE_ACCOUNTS "

        txtPurchaseSet.arrValueMember = clsCommon.ShowMultipleSelectForm("PurMulSel", qry, "Code", "Description", txtPurchaseSet.arrValueMember, txtPurchaseSet.arrDispalyMember)

    End Sub
    Private Sub txtItemStructure__My_Click(sender As Object, e As EventArgs) Handles txtItemStructure._My_Click
        Dim qry As String
        qry = " select Structure_Code as Code,structure_descq as [Description] from TSPL_STRUCTURE_MASTER "

        txtItemStructure.arrValueMember = clsCommon.ShowMultipleSelectForm("PurMulSel", qry, "Code", "Description", txtItemStructure.arrValueMember, txtItemStructure.arrDispalyMember)

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
            Dim TempFormId As String = PageSetupReport_ID
         
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
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
            gv.Columns(ii).Width = 100
        Next
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
        '    arrHeader.Add("Item Purchase Account Report")
        '    clsCommon.MyExportToExcelGrid("Item List", gv, arrHeader, "Item Purchase Account Report")
        'End If
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        Print(Exporter.PDF)
        Export(EnumExportTo.PDF)
        'If gv.Rows.Count > 0 Then
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add("Item Purchase Account Report")
        '    clsCommon.MyExportToPDF("Item List", gv, arrHeader, "Item Purchase Account Report", True)
        'End If
    End Sub

    Private Sub Export(ByVal IsPrint As EnumExportTo)
        If gv.Rows.Count > 0 Then
            Dim arrHeader As List(Of String) = New List(Of String)()
            arrHeader.Add("Name : Item Purchase Account Report")
            If txtPurchaseSet.arrDispalyMember IsNot Nothing AndAlso txtPurchaseSet.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Purchase Account Set : " + clsCommon.GetMulcallStringWithComma(txtPurchaseSet.arrDispalyMember))
            End If
            If txtItemType.arrDispalyMember IsNot Nothing AndAlso txtItemType.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Type : " + clsCommon.GetMulcallStringWithComma(txtItemType.arrDispalyMember))
            End If
            If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
            End If
            If txtItemStructure.arrDispalyMember IsNot Nothing AndAlso txtItemStructure.arrDispalyMember.Count > 0 Then
                arrHeader.Add("Item Structure : " + clsCommon.GetMulcallStringWithComma(txtItemStructure.arrDispalyMember))
            End If
            If (IsPrint = EnumExportTo.Excel) Then
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToExcelGrid("Item List", gv, arrHeader, "Item Purchase Account Report")
            Else
                transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                clsCommon.MyExportToPDF("Item List", gv, arrHeader, "Item Purchase Account Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
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
        If clsCommon.myLen(PageSetupReport_ID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = PageSetupReport_ID
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
        Dim TempFormId As String = PageSetupReport_ID
        TempFormId = Form_ID
        clsGridLayout.DeleteData(TempFormId, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
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
