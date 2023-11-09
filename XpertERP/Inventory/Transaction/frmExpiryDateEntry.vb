'-Updation by--[Pankaj Kumar Chaudhary] against Ticket no-[BM00000001633]
'---preeti Gupta---Ticket No.-BM00000003015--01/07/2014
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports common
Public Class FrmExpiryDateEntry
    Inherits FrmMainTranScreen

#Region "Variables"

    Public Const RowTypeAdjustmentQty As String = "Quantity"
    Public Const RowTypeAdjustmentCost As String = "Cost"
    Public Const RowTypeAdjustmentBoth As String = "Both"

    Private isCellValueChangedOpen As Boolean = False
    Private isNewEntry As Boolean = False
    Private isInsideLoadData As Boolean = False

    Const colLineNo As String = "COLLNO"
    Const colICode As String = "COLICODE"
    Const colIName As String = "COLINAME"
    Const colUnit As String = "COLUNIT"
    Const colQty As String = "COLQTY"
    Const colCost As String = "COLCOST"
    Const colMRP As String = "MRP"
    Const colAmount As String = "Amount"
    Const colRemarks As String = "REMARKS"
    Const colComment As String = "COMMENT"
    Const colLeak As String = "Leak"
    Const colBreak As String = "Break"
    Const colLiquidRate As String = "LiquidRate"
    Const colLiquidAmt As String = "LiquidAmt"
    Dim ButtonToolTip As ToolTip = New ToolTip()
    Public strDocumentNo As String
#End Region

    Private Sub SetUserMgmtNew()
        '--preeti gupta--ticket no[BM00000003175]
        ''MyBase.SetUserMgmt(clsUserMgtCode.FrmExpiryDateEntry)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")

        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnPost.Visible = MyBase.isPostFlag
        btnDelete.Visible = MyBase.isDeleteFlag
        btnPrint.Visible = MyBase.isPrintFlag

        If MyBase.isReverse Then
            btnReverseAndRecreate.Enabled = True
        Else
            btnReverseAndRecreate.Enabled = False
        End If
        If btnSave.Visible = True Then
            RmiExport.Enabled = True
        Else
            RmiExport.Enabled = False
        End If
    End Sub
    Sub SetLength()
        txtDocNo.MyMaxLength = 30
        txtDesc.MaxLength = 200
        txtReference.MaxLength = 200
    End Sub
    Private Sub fndVehicleCode__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtVehicleCode._MYValidating
        Dim qry As String = "Select Vehicle_Id as Code,Number,Description,Model from TSPL_VEHICLE_MASTER"
        txtVehicleCode.Value = clsCommon.ShowSelectForm("ShipVehicaleFinder", qry, "Code", "", txtVehicleCode.Value, "", isButtonClicked)

        qry = "Select Number from TSPL_VEHICLE_MASTER where Vehicle_Id='" + txtVehicleCode.Value + "'"
        lblVhicleNo.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
    End Sub

    Private Sub FrmExpiryDateEntry_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.N AndAlso btnAddNew.Enabled Then
            AddNew()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.S AndAlso MyBase.isModifyFlag AndAlso btnSave.Enabled Then
            SaveData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.P AndAlso MyBase.isPostFlag AndAlso btnPost.Enabled Then
            PostData()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.D AndAlso MyBase.isDeleteFlag AndAlso btnDelete.Enabled Then
            DeleteData()
        ElseIf e.Alt And e.KeyCode = Keys.C Then
            CloseForm()
        ElseIf e.Control And e.Alt And e.Shift And e.KeyCode = Keys.F12 Then
            Dim frm As New FrmPWD(Nothing)
            frm.strType = "SIRC"
            frm.strCode = "SIReversAndCreate"
            frm.ShowDialog()
            If frm.isPasswordCorrect Then
                btnReverseAndRecreate.Visible = True

            End If
        
        End If

    End Sub
    Private Sub FrmExpiryDateEntry_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SetUserMgmtNew()
        ButtonToolTip.SetToolTip(btnSave, "Press Alt+S for Save/Update Trasnaction")
        ButtonToolTip.SetToolTip(btnPost, "Press Alt+P Post Trasnaction")
        ButtonToolTip.SetToolTip(btnDelete, "Press Alt+D Delete Trasnaction")
        ButtonToolTip.SetToolTip(btnClose, "Press Alt+C Close the Window")
        ButtonToolTip.SetToolTip(btnAddNew, "Press Alt+N Adding New Trasnaction")
        SetLength()


        LoadBlankGrid()
        AddNew()

        chkthirdparty.Visible = False
        If clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal Then
            chkthirdparty.Visible = True
        End If

        If clsCommon.myLen(strDocumentNo) > 0 Then
            LoadData(strDocumentNo, NavigatorType.Current)
        End If
        If clsCommon.myLen(Me.Tag) > 0 Then
            LoadData(clsCommon.myCstr(Me.Tag), NavigatorType.Current)
        End If
    End Sub
    Private Sub btnReverseAndRecreate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReverseAndRecreate.Click
        Dim Qry As String
        If clsCommon.myLen(txtDocNo.Value) > 0 Then
            If common.clsCommon.MyMessageBoxShow("Reverse and Recreate Current Document" + Environment.NewLine + "Are you sure", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                '' REASON FOR Reverse 
                Dim Reason As String = ""
                Dim frm As New FrmFreeTxtBox1
                frm.Text = "Remarks for Reverse"
                frm.ShowDialog()
                If clsCommon.myLen(frm.strRmks) <= 0 Then
                    Exit Sub
                Else
                    Reason = frm.strRmks
                End If
                Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                Try
                    Dim obj As clsExpiryDate = clsExpiryDate.GetData(txtDocNo.Value, NavigatorType.Current, trans)
                    If (obj Is Nothing OrElse clsCommon.myLen(obj.Document_No) <= 0) Then
                        Throw New Exception("No Data found to Create Journal entry")
                    End If
                    Dim VoucherNo As String = clsDBFuncationality.getSingleValue("select Voucher_No from TSPL_JOURNAL_MASTER where Source_Code='EX-AD' and Source_Doc_No='" + txtDocNo.Value + "'", trans)
                    If Not clsCommon.myLen(VoucherNo) <= 0 Then
                        'Throw New Exception("Journal voucher no not found to recreate journal Enry")


                        Qry = "delete from TSPL_JOURNAL_DETAILS where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                        Qry = "delete from TSPL_JOURNAL_MASTER where Voucher_No ='" + VoucherNo + "'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)

                        Qry = "select InOut,Trans_Type,Item_Code,Item_Desc,Location_Code,case when InOut='I' then -1 else 1 end *Qty as Qty ,UOM,MRP,ItemType,case when InOut='I' then -1 else 1 end* Basic_Cost as Basic_Cost from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + txtDocNo.Value + "' and Trans_Type='ExpiredItem'"
                        Dim dt As DataTable = clsDBFuncationality.GetDataTable(Qry, trans)
                        Dim ArrLocationDetails As List(Of clsItemLocationDetails) = New List(Of clsItemLocationDetails)
                        For Each objtr As DataRow In dt.Rows

                            Dim dblConvFac As Double = clsItemMaster.GetConvertionFactor(clsCommon.myCstr(objtr("Item_Code")), clsCommon.myCstr(objtr("UOM")), trans)
                            Dim objLocationDetails As New clsItemLocationDetails()
                            objLocationDetails.Item_Code = clsCommon.myCstr(objtr("Item_Code"))
                            objLocationDetails.Item_Desc = clsCommon.myCstr(objtr("Item_Desc"))
                            objLocationDetails.Location_Code = clsCommon.myCstr(objtr("Location_Code"))
                            objLocationDetails.Location_Desc = clsLocation.GetName(objLocationDetails.Location_Code, trans)
                            objLocationDetails.Item_Qty = clsCommon.myCdbl(objtr("Qty")) / dblConvFac
                            objLocationDetails.Amount = clsCommon.myCdbl(objtr("Basic_Cost"))
                            objLocationDetails.MRP = clsCommon.myCdbl(objtr("MRP")) * dblConvFac
                            objLocationDetails.ItemType = clsCommon.myCstr(objtr("ItemType"))

                            Dim strICode As String = objLocationDetails.Item_Code
                            Dim strUOM As String = clsCommon.myCstr(objtr("UOM"))
                            Dim dblQty As Double = objLocationDetails.Item_Qty
                            Dim strMRP As String = objLocationDetails.MRP

                            Dim dblBalQty As Double = Math.Round(clsItemLocationDetails.getBalanceWithUnapproveEmpty(strICode, txtLocation.Value, strMRP, strUOM, txtDocNo.Value, txtDate.Value, trans), 2, MidpointRounding.ToEven)
                            If dblQty > dblBalQty Then
                                Throw New Exception("Item - " + strICode + " , MRP - " + strMRP + Environment.NewLine + "Entered Quantity - " + clsCommon.myCstr(dblQty) + " and Actual Balance Quantity - " + clsCommon.myCstr(dblBalQty))
                            End If

                            ArrLocationDetails.Add(objLocationDetails)
                        Next
                        Dim strPostDate As String = clsCommon.GetPrintDate(clsCommon.GETSERVERDATE(trans), "dd/MM/yyyy")
                        clsItemLocationDetails.SaveData(strPostDate, ArrLocationDetails, trans)

                        Qry = "delete from TSPL_INVENTORY_MOVEMENT where Source_Doc_No='" + txtDocNo.Value + "' and Trans_Type='ExpiredItem'"
                        clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    End If
                    Qry = "Update TSPL_EXPIRY_HEADER set Posted = 'N' where Document_No='" + txtDocNo.Value + "'"
                    clsDBFuncationality.ExecuteNonQuery(Qry, trans)
                    saveCancelLog(Reason, "Reverse And Recreate", trans)
                    trans.Commit()

                    common.clsCommon.MyMessageBoxShow(Me, "Successfully Reversed and Recreated", Me.Text)
                    LoadData(txtDocNo.Value, NavigatorType.Current)
                Catch ex As Exception
                    trans.Rollback()
                    common.clsCommon.MyMessageBoxShow(Me, ex.Message)
                End Try
            End If
        End If
    End Sub
    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colICode
        repoICode.HeaderImage = Global.ERP.My.Resources.Resources.search4
        repoICode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoICode.Width = 100
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item Description"
        repoIName.Name = colIName
        repoIName.Width = 150
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)


        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "UOM"
        repoUnit.Name = colUnit
        repoUnit.Width = 100
        repoUnit.ReadOnly = False
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoQty.FormatString = ""
        repoQty.HeaderText = "Expired Qty"
        repoQty.Name = colQty
        repoQty.Width = 80
        repoQty.Minimum = 0
        repoQty.ShowUpDownButtons = False
        repoQty.Step = 0
        repoQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoQty)


        Dim repoAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmt.FormatString = ""
        repoAmt.HeaderText = "Cost"
        repoAmt.Name = colCost
        repoAmt.Width = 80
        repoAmt.Minimum = 0
        repoAmt.ReadOnly = False
        repoAmt.ShowUpDownButtons = False
        repoAmt.Step = 0
        repoAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoAmt)

        Dim repoAmount As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoAmount.WrapText = True
        repoAmount.ReadOnly = True
        repoAmount.FormatString = ""
        repoAmount.HeaderText = "Amount"
        repoAmount.Name = colAmount
        repoAmount.Width = 80
        repoAmount.Minimum = 0
        repoAmount.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoAmount.ShowUpDownButtons = False
        repoAmount.Step = 0
        gv1.MasterTemplate.Columns.Add(repoAmount)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.WrapText = True
        repoMRP.ReadOnly = False
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colMRP
        repoMRP.Width = 80
        repoMRP.Minimum = 0
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoMRP.ShowUpDownButtons = False
        repoMRP.Step = 0
        gv1.MasterTemplate.Columns.Add(repoMRP)

        Dim repoLeak As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLeak.WrapText = True
        repoLeak.ReadOnly = False
        repoLeak.FormatString = ""
        repoLeak.HeaderText = "Leak"
        repoLeak.Name = colLeak
        repoLeak.Width = 80
        repoLeak.Minimum = 0
        repoLeak.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLeak.ShowUpDownButtons = False
        repoLeak.Step = 0
        repoLeak.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLeak)

        Dim repoBreak As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoBreak.WrapText = True
        repoBreak.ReadOnly = False
        repoBreak.FormatString = ""
        repoBreak.HeaderText = "Breakage"
        repoBreak.Name = colBreak
        repoBreak.Width = 80
        repoBreak.Minimum = 0
        repoBreak.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoBreak.ShowUpDownButtons = False
        repoBreak.Step = 0
        repoBreak.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoBreak)

        Dim repoLiquidRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLiquidRate.WrapText = True
        repoLiquidRate.ReadOnly = True
        repoLiquidRate.FormatString = ""
        repoLiquidRate.HeaderText = "Liquid Rate"
        repoLiquidRate.Name = colLiquidRate
        repoLiquidRate.Width = 80
        repoLiquidRate.Minimum = 0
        repoLiquidRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLiquidRate.ShowUpDownButtons = False
        repoLiquidRate.Step = 0
        repoLiquidRate.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLiquidRate)

        Dim repoLiquidAmt As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLiquidAmt.WrapText = True
        repoLiquidAmt.ReadOnly = True
        repoLiquidAmt.FormatString = ""
        repoLiquidAmt.HeaderText = "Liquid Amount"
        repoLiquidAmt.Name = colLiquidAmt
        repoLiquidAmt.Width = 80
        repoLiquidAmt.Minimum = 0
        repoLiquidAmt.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        repoLiquidAmt.ShowUpDownButtons = False
        repoLiquidAmt.Step = 0
        repoLiquidAmt.IsVisible = False
        gv1.MasterTemplate.Columns.Add(repoLiquidAmt)

        Dim repoRemarks As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoRemarks = New GridViewTextBoxColumn()
        repoRemarks.FormatString = ""
        repoRemarks.HeaderText = "Remarks"
        repoRemarks.Name = colRemarks
        repoRemarks.Width = 150
        gv1.MasterTemplate.Columns.Add(repoRemarks)

        Dim repoSpecification As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSpecification = New GridViewTextBoxColumn()
        repoSpecification.FormatString = ""
        repoSpecification.HeaderText = "Comment"
        repoSpecification.Name = colComment
        repoSpecification.Width = 150
        gv1.MasterTemplate.Columns.Add(repoSpecification)



        gv1.AllowDeleteRow = True
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click

        'Help.ShowHelp(Me, Application.StartupPath & "\help file riva moda\Riva Help File.chm", HelpNavigator.Topic, "mk:@MSITStore:D:\KiwiERP\bin\Debug\HELPFI~1\RIVAHE~1.CHM::/Item%20Category.htm")

        'System.Diagnostics.Process.Start(Application.StartupPath & "\help file riva moda\Tax Group.htm")

        '' final code for help file
        ' '' ''Dim hlp As New HelpProvider
        ' '' ''hlp.HelpNamespace = Application.StartupPath & "\help file riva moda\Riva Help File.chm"
        ' '' ''hlp.SetHelpNavigator(Me, HelpNavigator.Topic)
        ' '' ''Help.ShowHelp(Me, hlp.HelpNamespace, HelpNavigator.Topic, "mk:@MSITStore:" & Application.StartupPath & "\HELPFI~1\RIVAHE~1.CHM::/Customer%20Item%20Approval%20screen.htm")

        ''Direct it can aslo use

        ''Help.ShowHelp(Me, Application.StartupPath & "\help file riva moda\Riva Help File.chm", HelpNavigator.Find, "mk:@MSITStore:D:\KiwiERP\bin\Debug\HELPFI~1\RIVAHE~1.CHM::/Item%20Category.htm")

        '' final code for help file
        'Help.ShowHelp(ParentForm, "Riva Help File.chm", "mk:@MSITStore:D:\KiwiERP\bin\Debug\help%20file%20riva%20moda\Riva%20Help%20File.chm::/Item%20Category.htm")
        'System.Diagnostics.Process.Start(Application.StartupPath & "\help file riva moda\Riva Help File.chm")

        AddNew()
    End Sub

    Sub AddNew()
        BlankAllControls()
        LoadBlankGrid()
        isNewEntry = True
        btnSave.Text = "Save"
        btnSave.Enabled = True
        btnPost.Enabled = True
        btnDelete.Enabled = True
        txtCustomer.Enabled = True
        txtshellqty.Text = ""
        txtShellAmt.Text = ""
        txtDate.Focus()
        gv1.Rows.AddNew()
        UsLock1.Status = ERPTransactionStatus.Pending
    End Sub

    Sub BlankAllControls()
        chkthirdparty.Checked = False
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtDesc.Text = ""
        txtReference.Text = ""
        txtDocNo.Value = ""
        txtDate.Value = clsCommon.GETSERVERDATE()
        txtCustomer.Value = ""
        lblCustomer.Text = ""
        txtDesc.Text = ""
        txtLocation.Value = ""
        lblLocation.Text = ""
        txtReference.Text = ""
        txtVehicleCode.Value = ""
        lblVhicleNo.Text = ""
        txtRefDocNo.Value = ""
    End Sub

    Private Sub gv1_CellValueChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gv1.CellValueChanged
        Try
            If (Not isInsideLoadData) Then
                If Not isCellValueChangedOpen Then
                    isCellValueChangedOpen = True
                    If e.Column Is gv1.Columns(colICode) OrElse e.Column Is gv1.Columns(colCost) OrElse e.Column Is gv1.Columns(colUnit) OrElse e.Column Is gv1.Columns(colQty) OrElse e.Column Is gv1.Columns(colMRP) OrElse e.Column Is gv1.Columns(colRemarks) OrElse e.Column Is gv1.Columns(colComment) Then
                        If e.Column Is gv1.Columns(colICode) Then
                            OpenICodeList(False)
                        ElseIf e.Column Is gv1.Columns(colUnit) Then
                            OpenUOMList(False)
                        ElseIf e.Column Is gv1.Columns(colQty) Then
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colCost) Then
                            OpenItemRateList(False)
                            UpdateCurrentRow(gv1.CurrentRow.Index)
                        ElseIf e.Column Is gv1.Columns(colMRP) Then
                            OpenItemMRPList(False)
                        End If
                    End If
                    isCellValueChangedOpen = False
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub
    Sub setBalance()
        UcItemBalance1.ItemCode = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        UcItemBalance1.ItemName = clsCommon.myCstr(gv1.CurrentRow.Cells(colIName).Value)
        UcItemBalance1.ItemMRP = clsCommon.myCdbl(gv1.CurrentRow.Cells(colMRP).Value)
        UcItemBalance1.LocationCode = txtLocation.Value
        UcItemBalance1.LocationName = lblLocation.Text
        UcItemBalance1.UOM = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        UcItemBalance1.TransNo = txtDocNo.Value
        UcItemBalance1.TransDate = txtDate.Value
        UcItemBalance1.ShowSOQty = True
        UcItemBalance1.RefreshData()
    End Sub
    Private Sub UpdateCurrentRow(ByVal IntRowNo As Integer)
        Dim strICode As String = clsCommon.myCstr(gv1.Rows(IntRowNo).Cells(colICode).Value)
        If clsCommon.myLen(strICode) > 0 Then
            Dim dblQty As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colQty).Value)
            Dim dblRate As Double = clsCommon.myCdbl(gv1.Rows(IntRowNo).Cells(colCost).Value)
            If clsCommon.myCdbl(dblQty) > 0 And clsCommon.myCdbl(dblRate) > 0 Then
                Dim dblAmt As Double = dblQty * dblRate
                gv1.Rows(IntRowNo).Cells(colAmount).Value = dblAmt
            Else
                gv1.Rows(IntRowNo).Cells(colAmount).Value = 0
            End If
        End If
    End Sub
    Sub OpenICodeList(ByVal isButtonClick As Boolean)
        Dim obj As clsItemMaster = clsItemMaster.FinderForFinishedGoods(clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value), isButtonClick)
        If obj IsNot Nothing AndAlso clsCommon.myLen(obj.Item_Code) > 0 Then
            gv1.CurrentRow.Cells(colICode).Value = obj.Item_Code
            gv1.CurrentRow.Cells(colIName).Value = obj.Item_Desc
            gv1.CurrentRow.Cells(colUnit).Value = ""
            gv1.CurrentRow.Cells(colCost).Value = 0
            gv1.CurrentRow.Cells(colMRP).Value = 0
            gv1.CurrentRow.Cells(colAmount).Value = 0
        Else
            SetBlankOfItemColumns()
        End If
    End Sub

    Sub OpenUOMList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        If clsCommon.myLen(strICode) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Item Code")
            Exit Sub
        End If
        Dim qry As String = "select  UOM_Code as Code,UOM_Description as [Description] from TSPL_ITEM_UOM_DETAIL "
        Dim WhrCls As String = "Item_Code ='" + strICode + "'"
        gv1.CurrentRow.Cells(colUnit).Value = clsCommon.ShowSelectForm("ExpiryDateUOM", qry, "Code", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value), "Code", isButtonClick)
    End Sub
    Sub OpenItemRateList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        Dim strUnitCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        If clsCommon.myLen(strICode) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Item Code")
            Exit Sub
        End If
        Dim qry As String = "select distinct Item_Basic_Price as Rate,Item_Code,UOM from TSPL_ITEM_PRICE_MASTER"
        Dim WhrCls As String = "Item_Code ='" + strICode + "' and  UOM='" & strUnitCode & "' "
        gv1.CurrentRow.Cells(colCost).Value = clsCommon.ShowSelectForm("ExpiryDateCost", qry, "Rate", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colCost).Value), "Rate", isButtonClick)
    End Sub
    Sub OpenItemMRPList(ByVal isButtonClick As Boolean)
        Dim strICode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colICode).Value)
        Dim strUnitCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colUnit).Value)
        If clsCommon.myLen(strICode) <= 0 Then
            common.clsCommon.MyMessageBoxShow(Me, "Please select Item Code")
            Exit Sub
        End If
        Dim qry As String = "select distinct Item_Basic_Net as MRP,Item_Code,UOM from TSPL_ITEM_PRICE_MASTER"
        Dim WhrCls As String = "Item_Code ='" + strICode + "' and  UOM='" & strUnitCode & "' "
        gv1.CurrentRow.Cells(colMRP).Value = clsCommon.ShowSelectForm("ExpiryDateMRP", qry, "MRP", WhrCls, clsCommon.myCstr(gv1.CurrentRow.Cells(colMRP).Value), "MRP", isButtonClick)
    End Sub
    Private Sub SetBlankOfItemColumns()
        gv1.CurrentRow.Cells(colICode).Value = ""
        gv1.CurrentRow.Cells(colIName).Value = ""
        gv1.CurrentRow.Cells(colUnit).Value = ""
        gv1.CurrentRow.Cells(colQty).Value = 0
        gv1.CurrentRow.Cells(colMRP).Value = 0
        gv1.CurrentRow.Cells(colCost).Value = 0
        gv1.CurrentRow.Cells(colAmount).Value = 0
        gv1.CurrentRow.Cells(colLeak).Value = 0
        gv1.CurrentRow.Cells(colBreak).Value = 0
    End Sub

    Private Sub gv1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gv1.Click
        If gv1.CurrentRow IsNot Nothing Then
            setBalance()
        End If
    End Sub



    Private Sub gv1_CurrentColumnChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentColumnChangedEventArgs) Handles gv1.CurrentColumnChanged
        If gv1.RowCount > 0 Then
            Dim intCurrRow As Integer = gv1.CurrentRow.Index
            gv1.CurrentRow.Cells(colLineNo).Value = clsCommon.myCdbl(intCurrRow + 1)
            If intCurrRow = gv1.Rows.Count - 1 Then
                gv1.Rows.AddNew()
                gv1.CurrentRow = gv1.Rows(intCurrRow)
            End If
        End If
    End Sub

    Private Sub gv1_CurrentRowChanged(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        If gv1.CurrentRow IsNot Nothing AndAlso Not e.CurrentRow.Index < 0 Then
            setBalance()
        End If
    End Sub

    Private Sub gv1_UserAddedRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowEventArgs) Handles gv1.UserAddedRow
        For i As Integer = 0 To gv1.Rows.Count - 1
            gv1.Rows(0).Cells(0).Value = 1
            If i <> 0 Then
                gv1.Rows(i).Cells(colLineNo).Value = i + 1
            End If
        Next
    End Sub
    Private Sub gv1_UserDeletingRow(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gv1.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub

    Private Sub RadButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        CloseForm()
    End Sub

    Sub CloseForm()
        Me.Close()
    End Sub

    Private Sub txtLocation__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtLocation._MYValidating
        Dim qry As String = ""
        Dim whrclas As String = ""
        qry = "select Location_Code ,Location_Desc from TSPL_LOCATION_MASTER "
        whrclas = " Location_Type='Physical' "
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrclas += " and Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If

        If chkthirdparty.Checked AndAlso clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal Then
            whrclas += " and isnull(vendor_code,'')<>''"
        ElseIf Not chkthirdparty.Checked AndAlso clsCommon.CompairString(MDI.IsLoc_Third_Party, "YES") = CompairStringResult.Equal Then
            whrclas += " and isnull(vendor_code,'')=''"
        End If
        txtLocation.Value = clsCommon.ShowSelectForm("ExpiryDateLocation", qry, "Location_Code", whrclas, txtLocation.Value, "", isButtonClicked)
        lblLocation.Text = clsDBFuncationality.getSingleValue("select Location_Desc   from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "' ")
    End Sub
    Private Sub txtCustomer__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtCustomer._MYValidating
        Dim qry As String = "select Cust_Code ,Customer_Name  from TSPL_CUSTOMER_MASTER"
        txtCustomer.Value = clsCommon.ShowSelectForm("Customer Code", qry, "Cust_Code", "Status='N'", txtCustomer.Value, "", isButtonClicked)
        lblCustomer.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'")
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        SaveData()
    End Sub

    Private Function AllowToSave() As Boolean
        If AllowFutureDateTransaction(txtDate.Value, Nothing) = False Then
            txtDate.Select()
            Return False
        End If
        If clsCommon.myLen(txtLocation.Value) <= 0 Then
            txtLocation.Focus()
            Throw New Exception("Please select Location")
        End If

        Dim strDocDate As String = clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy")

        For ii As Integer = 0 To gv1.Rows.Count - 1
            Dim strICode As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colICode).Value)
            Dim strIName As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colIName).Value)
            Dim strUOM As String = clsCommon.myCstr(gv1.Rows(ii).Cells(colUnit).Value)
            Dim dblExpiryQty As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colQty).Value)
            Dim dblLeak As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colLeak).Value)
            Dim dblBreak As Double = clsCommon.myCdbl(gv1.Rows(ii).Cells(colBreak).Value)
            Dim dblMRP As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colMRP).Value)
            Dim dblCost As String = clsCommon.myCdbl(gv1.Rows(ii).Cells(colCost).Value)
            Dim dblQty As Double
            Dim strPiceCode As String = Nothing
            Dim dblLiquidRate As Double = 0
            Dim dblLiquidAmt As Double = 0

            If clsCommon.myLen(strICode) > 0 Then
                If clsCommon.myLen(strUOM) <= 0 Then
                    Throw New Exception("Please enter UOM of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If

                If clsCommon.myLen(gv1.Rows(ii).Cells(colMRP).Value) <= 0 Then
                    Throw New Exception("Please enter MRP of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If
                dblQty = dblBreak + dblExpiryQty + dblLeak
                If clsCommon.myCdbl(dblQty) = 0 Then
                    Throw New Exception("Please enter Quantity of item " + strICode + " at Row No " + clsCommon.myCstr(ii + 1))
                End If

                If chkthirdparty.Checked Then

                End If
                If clsCommon.myLen(txtCustomer.Value) > 0 Then
                    strPiceCode = clsDBFuncationality.getSingleValue("select Price_Code from TSPL_CUSTOMER_MASTER where Cust_Code='" & txtCustomer.Value & "'")
                End If
                Dim dblBalQty As Double = clsItemLocationDetails.getBalance(strICode, txtLocation.Value, txtDocNo.Value, txtDate.Value, Nothing, strUOM, dblMRP)
                If dblQty > dblBalQty Then
                    common.clsCommon.MyMessageBoxShow("Item - " + strICode + Environment.NewLine + "Entered Quantity : " + clsCommon.myCstr(dblQty) + " and Balance Quantity : " + clsCommon.myCstr(dblBalQty), Me.Text)
                    Return False
                End If

            End If

            If clsCommon.myLen(strICode) > 0 Then
                For jj As Integer = 0 To gv1.Rows.Count - 1
                    If jj = ii Then
                        Continue For
                    End If
                    Dim strInnerICode As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colICode).Value)
                    Dim dblInnerMRP As Double = clsCommon.myCdbl(gv1.Rows(jj).Cells(colMRP).Value)


                    Dim strInnerUOM As String = clsCommon.myCstr(gv1.Rows(jj).Cells(colUnit).Value)
                    If dblMRP = dblInnerMRP AndAlso clsCommon.CompairString(strICode, strInnerICode) = CompairStringResult.Equal AndAlso clsCommon.CompairString(strUOM, strInnerUOM) = CompairStringResult.Equal Then
                        Dim Msg As String = "Same Item Exist at Row No " + clsCommon.myCstr(ii + 1) + " And " + clsCommon.myCstr(jj + 1)

                        Msg = Msg + Environment.NewLine + "Item: " + strICode + "(" + strIName + ")"
                        Msg = Msg + Environment.NewLine + "UOM: " + strUOM
                        If dblMRP > 0 Then
                            Msg = Msg + Environment.NewLine + "MRP: " + clsCommon.myCstr(dblMRP)
                        End If

                        common.clsCommon.MyMessageBoxShow(Me, Msg)
                        Return False
                    End If
                Next
            End If
            
        Next



        Return True
    End Function

    Private Function SaveData() As Boolean
        Try
            If (AllowToSave()) Then

                Dim obj As New clsExpiryDate()

                obj.chkthirdparty = "N"
                If chkthirdparty.Checked Then
                    obj.chkthirdparty = "Y"
                End If

                obj.Document_No = txtDocNo.Value
                obj.Document_Date = txtDate.Value
                'obj.Posting_Date
                obj.Reference = txtReference.Text
                obj.Description = txtDesc.Text
                'obj.Posted()
                obj.Loc_Code = txtLocation.Value
                obj.Loc_Desc = lblLocation.Text
                obj.Customer_CODE = txtCustomer.Value
                obj.Customer_NAME = lblCustomer.Text
                obj.Vehicle_Code = txtVehicleCode.Value
                obj.Vehicle_No = lblVhicleNo.Text
                obj.Reference_Document = txtRefDocNo.Value
                obj.Shell_Qty = clsCommon.myCdbl(txtshellqty.Text)
                obj.Shell_Amount = clsCommon.myCdbl(obj.Shell_Qty * 100)
                obj.Arr = New List(Of ClsExpiryDetails)()
                Dim isFirstTime As Boolean = True
                For Each grow As GridViewRowInfo In gv1.Rows
                    Dim objTr As New ClsExpiryDetails()
                    objTr.Document_Line_No = clsCommon.myCdbl(grow.Cells(colLineNo).Value)
                    objTr.Item_Code = clsCommon.myCstr(grow.Cells(colICode).Value)
                    objTr.Item_Description = clsCommon.myCstr(grow.Cells(colIName).Value)
                    objTr.Unit_Code = clsCommon.myCstr(grow.Cells(colUnit).Value)
                    objTr.mrp = clsCommon.myCdbl(grow.Cells(colMRP).Value)
                    objTr.Item_Quantity = clsCommon.myCdbl(grow.Cells(colQty).Value)
                    objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colCost).Value)
                    objTr.Amount = clsCommon.myCdbl(grow.Cells(colAmount).Value)
                    objTr.Remarks = clsCommon.myCstr(grow.Cells(colRemarks).Value)
                    objTr.Comments = clsCommon.myCstr(grow.Cells(colComment).Value)
                    objTr.Leakage_Qty = clsCommon.myCdbl(grow.Cells(colLeak).Value)
                    objTr.Breakage_Qty = clsCommon.myCdbl(grow.Cells(colBreak).Value)
                    objTr.Liquid_Rate = clsCommon.myCdbl(grow.Cells(colLiquidRate).Value)
                    objTr.Liquid_Amount = clsCommon.myCdbl(grow.Cells(colLiquidAmt).Value)
                    If isFirstTime Then
                        isFirstTime = False
                    End If


                    If (clsCommon.myLen(objTr.Item_Code) > 0) Then
                        obj.Arr.Add(objTr)
                    End If

                Next
                If (obj.Arr Is Nothing OrElse obj.Arr.Count <= 0) Then
                    Throw New Exception("Please Fill at list one Item")
                End If

                Dim isSaved As Boolean = obj.SaveData(obj, isNewEntry)
                clsCommon.MyMessageBoxShow("Data Saved Successfully", Me.Text)
                LoadData(obj.Document_No, NavigatorType.Current)
                Return isSaved
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Sub LoadData(ByVal strCode As String, ByVal NavTyep As NavigatorType)
        Try
            btnSave.Enabled = True
            btnPost.Enabled = True
            btnDelete.Enabled = True
            isInsideLoadData = True
            isNewEntry = False
            btnSave.Text = "Update"
            BlankAllControls()
            LoadBlankGrid()


            Dim obj As New clsExpiryDate()
            obj = clsExpiryDate.GetData(strCode, NavTyep, Nothing)

            If (obj IsNot Nothing AndAlso clsCommon.myLen(obj.Document_No) > 0) Then
                If clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                    btnSave.Enabled = False
                    btnPost.Enabled = False
                    btnDelete.Enabled = False
                End If

                chkthirdparty.Checked = False
                If clsCommon.CompairString(obj.chkthirdparty, "Y") = CompairStringResult.Equal Then
                    chkthirdparty.Checked = True
                End If

                txtDocNo.Value = obj.Document_No
                txtDate.Value = obj.Document_Date
                txtReference.Text = obj.Reference
                txtDesc.Text = obj.Description
                txtCustomer.Value = obj.Customer_CODE
                lblCustomer.Text = obj.Customer_NAME
                txtLocation.Value = obj.Loc_Code
                lblLocation.Text = obj.Loc_Desc
                txtVehicleCode.Value = obj.Vehicle_Code
                lblVhicleNo.Text = obj.Vehicle_No
                txtRefDocNo.Value = obj.Reference_Document
                txtshellqty.Text = obj.Shell_Qty
                txtShellAmt.Text = obj.Shell_Amount
                If obj.Arr IsNot Nothing AndAlso obj.Arr.Count > 0 Then
                    For Each objTr As ClsExpiryDetails In obj.Arr
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = objTr.Document_Line_No
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colICode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colIName).Value = objTr.Item_Description
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colUnit).Value = objTr.Unit_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colQty).Value = objTr.Item_Quantity
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colCost).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colMRP).Value = objTr.mrp
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colAmount).Value = objTr.Amount
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colRemarks).Value = objTr.Remarks
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colComment).Value = objTr.Comments
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLeak).Value = objTr.Leakage_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBreak).Value = objTr.Breakage_Qty
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLiquidRate).Value = objTr.Liquid_Rate
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLiquidAmt).Value = objTr.Liquid_Amount

                    Next

                    If Not clsCommon.CompairString(obj.Posted, "Y") = CompairStringResult.Equal Then
                        gv1.Rows.AddNew()
                        UsLock1.Status = ERPTransactionStatus.Pending
                    Else
                        UsLock1.Status = ERPTransactionStatus.Approved
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        Finally
            isInsideLoadData = False
        End Try
    End Sub

    Private Sub btnPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPost.Click
        PostData()
    End Sub

    Sub PostData()
        Try
            If (AllowToSave()) Then
                If (myMessages.postConfirm()) Then
                    If (clsExpiryDate.PostData(txtDocNo.Value)) Then
                        common.clsCommon.MyMessageBoxShow(Me, "Successfully Posted")
                        LoadData(txtDocNo.Value, NavigatorType.Current)
                    End If
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        DeleteData()
    End Sub

    Sub DeleteData()
        Try
            Dim Reason As String = ""
            If (myMessages.deleteConfirm()) Then
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
                If (clsExpiryDate.DeleteData(txtDocNo.Value)) Then
                    saveCancelLog(Reason, "Delete", Nothing)
                    common.clsCommon.MyMessageBoxShow(Me, "Data Deleted Successfully ")
                    AddNew()
                End If
            End If
        Catch ex As Exception
            myMessages.myExceptions(ex)
        End Try
    End Sub
    Function saveCancelLog(ByVal Reason As String, ByVal Activity_Type As String, Optional ByVal trans As System.Data.SqlClient.SqlTransaction = Nothing) As Boolean
        Dim obj As New clsCancelLog
        obj.Program_Code = Form_ID
        obj.DOCUMENT_NO = clsCommon.myCstr(Me.txtDocNo.Value)
        obj.REASON = Reason
        obj.ACTIVITY_TYPE = Activity_Type
        Return clsCancelLog.SaveData(obj, True, trans)
    End Function

    Private Sub txtDocNo__MYNavigator(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal NavType As common.NavigatorType) Handles txtDocNo._MYNavigator
        Try
            Dim qst As String = "select count(*) from TSPL_EXPIRY_HEADER where Document_No='" + txtDocNo.Value + "'"
            Dim count As Integer = clsCommon.myCdbl(clsDBFuncationality.getSingleValue(qst))
            If count = 0 Then
                txtDocNo.MyReadOnly = False
            Else
                txtDocNo.MyReadOnly = True
            End If
            LoadData(txtDocNo.Value, NavType)
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message)
        End Try
    End Sub

    Private Sub txtDocNo__MYValidating(ByVal sender As System.Object, ByVal e As System.EventArgs, ByVal isButtonClicked As System.Boolean) Handles txtDocNo._MYValidating
        Dim qry As String = "SELECT Document_No AS [DocumentNo], Document_Date as [Date], case when Posted='Y' then 'Yes' else 'No' end as Posted,Customer_NAME as [Customer],Loc_Code as [Location] FROM  TSPL_EXPIRY_HEADER  "
        Dim whrClas As String = " 1=1"
        If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
            whrClas += " AND Loc_Code in (" + objCommonVar.strCurrUserLocations + ")"
        End If


        txtDocNo.Value = clsCommon.ShowSelectForm("ExpiryDateDoc", qry, "DocumentNo", whrClas, txtDocNo.Value, "DocumentNo", isButtonClicked)
        LoadData(txtDocNo.Value, NavigatorType.Current)
    End Sub

   
    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        'System.Diagnostics.Process.Start(Application.StartupPath & "\help file riva moda\Riva Help File.chm")
        PrintData()
    End Sub
    Sub PrintData()
        Try
            If clsCommon.myLen(txtDocNo.Value) <= 0 Then
                Throw New Exception("Transaction No not found to print")
            End If
            PrintData(txtDocNo.Value, False)
        Catch ex As Exception
            RadMessageBox.Show(ex.Message, Me.Text)
        End Try
    End Sub

    Public Shared Sub PrintData(ByVal strDocNo As String, ByVal IsPreprinted As Boolean)
        Try

            Dim qry As String
            Dim dt As DataTable
            qry = "select * from TSPL_EXPIRY_HEADER left outer  join TSPL_EXPIRY_DETAIL on TSPL_EXPIRY_HEADER.DOcument_no=TSPL_EXPIRY_DETAIL.DOcument_no left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=TSPL_EXPIRY_HEADER.loc_code left outer join TSPL_COMPANY_MASTER on TSPL_COMPANY_MASTER.Comp_Code=TSPL_EXPIRY_HEADER.comp_code " & _
                 " where TSPL_EXPIRY_HEADER.DOcument_no='" + strDocNo + "' ORDER by document_line_no"
            dt = clsDBFuncationality.GetDataTable(qry)
            Dim frmCRV As New frmCrystalReportViewer()
            If IsPreprinted Then
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.PaperSize10x6, "crptExpiryDetails", "Expired Item Entry")
            Else
                frmCRV.funreport(CrystalReportFolder.InventoryReport, dt, EnumTecxpertPaperSize.NA, "crptExpiryDetails", "Expired Item Entry")
            End If
            frmCRV = Nothing
        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub txtRefDocNo__MYValidating(ByVal sender As Object, ByVal e As System.EventArgs, ByVal isButtonClicked As Boolean) Handles txtRefDocNo._MYValidating
        Dim qry As String = "select Sale_Invoice_No,Sale_Invoice_Date,Cust_Code,Cust_Name from TSPL_SALE_INVOICE_HEAD"
        txtRefDocNo.Value = clsCommon.ShowSelectForm("Ref Doc No", qry, "Sale_Invoice_No", "Is_Post='Y'", txtRefDocNo.Value, "", isButtonClicked)
        txtCustomer.Value = clsDBFuncationality.getSingleValue("select Cust_Code from TSPL_SALE_INVOICE_HEAD where Sale_Invoice_No='" + txtRefDocNo.Value + "'")
        lblCustomer.Text = clsDBFuncationality.getSingleValue("select Customer_Name from TSPL_CUSTOMER_MASTER where Cust_Code='" + txtCustomer.Value + "'")
        txtCustomer.Enabled = False
    End Sub

    'Private Sub txtShellAmt_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtShellAmt.TextChanged
    '    txtShellAmt.Text = txtshellqty.Text * 100
    'End Sub

    Private Sub txtshellqty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtshellqty.TextChanged
        txtShellAmt.Text = clsCommon.myCdbl(txtshellqty.Text) * 100
    End Sub

    Private Sub chkthirdparty_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkthirdparty.ToggleStateChanged

        txtLocation.Value = ""
        lblLocation.Text = ""

    End Sub

End Class
