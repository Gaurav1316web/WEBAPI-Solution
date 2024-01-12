' '' '' '' ''------ modified by priti adding functionality of internal requsition will not come for PO on 27/05/2012
Imports common
Public Class frmPendingTransfer
#Region "Variables"

    Public strCurrCode As String = Nothing
    Public strCurrCodeSRN As String = Nothing
    Public arrLoc As String = Nothing
    Public isFromSRN As Boolean = False
    Public ArrReturn As List(Of clsTransferDCCDetail) = Nothing
    Public ToLocCode As String = Nothing
    Public InternalTransfer As Integer = 0
    Public JobWorkType As Integer = 0

    Const colDSelect As String = "SELECT"
    Const colDCode As String = "CODE"
    Const colDICode As String = "ICODE"
    Const colDIName As String = "INAME"
    Const colDUnit As String = "UNIT"
    Const colDALTUnit As String = "ALT_UNIT"
    Const colDRate As String = "RATE"
    Const colDOutQty As String = "OUTQTY"
    Const colDFromLoc As String = "FromLoc"
    Const colDToLoc As String = "ToLoc"
    Const colDToMainLocName As String = "ToMainLocName"
    Const colDLoadInQty As String = "colDLoadInQty"
    Const colDUnApprovedQty As String = "UNAPPROVEDQTY"
    Const colDPendingQty As String = "PENDINGQTY"
    Const colDMRP As String = "colDMRP"
    Const colFOCItem As String = "colFOCItem"
    Const colHSelect As String = "SELECT"
    Const colHCode As String = "CODE"
    Const colHDate As String = "DATE"
    Const colHFromLoc As String = "FromLoc"
    Const colHToLoc As String = "ToLoc"
    Const colHToMainLocName As String = "ToMainLocName"

    Dim dtAllData As DataTable = Nothing
    Dim IsInsideLoadData As Boolean = False
#End Region

    Private Sub FrmPendingRequistion_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tp_ToDate.Value = clsCommon.GETSERVERDATE()
        tp_FromDate.Value = tp_ToDate.Value.AddMonths(-1)
        LoadBlankHeadGrid()
        LoadBlankGridDetail()
        LoadData()
    End Sub

    Sub LoadHeadData()
        IsInsideLoadData = True
        LoadBlankHeadGrid()
        Dim arr As New List(Of String)
        For Each dr As DataRow In dtAllData.Rows
            Dim strCode As String = clsCommon.myCstr(dr("code"))
            If Not arr.Contains(strCode) Then
                arr.Add(strCode)
                gvHead.Rows.AddNew()
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHSelect).Value = False
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHCode).Value = strCode
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHDate).Value = clsCommon.myCstr(dr("TransDate"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHFromLoc).Value = clsCommon.myCstr(dr("FromLocation"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHToLoc).Value = clsCommon.myCstr(dr("ToLocation"))
                gvHead.Rows(gvHead.RowCount - 1).Cells(colHToMainLocName).Value = clsCommon.myCstr(dr("ToGITLocation"))
            End If
        Next
        IsInsideLoadData = False
    End Sub

    Sub LoadBlankHeadGrid()
        gvHead.Rows.Clear()
        gvHead.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colHSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gvHead.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Transfer Out No"
        repoCode.Name = colHCode
        repoCode.Width = 170
        repoCode.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoCode)

        Dim repoDate As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoDate.FormatString = ""
        repoDate.HeaderText = "Date"
        repoDate.Name = colHDate
        repoDate.Width = 80
        repoDate.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoDate)

        Dim repoFromLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromLoc.FormatString = ""
        repoFromLoc.HeaderText = "From Location"
        repoFromLoc.Name = colHFromLoc
        repoFromLoc.Width = 170
        repoFromLoc.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoFromLoc)

        Dim repoToloc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoToloc.FormatString = ""
        repoToloc.HeaderText = "To Location"
        repoToloc.Name = colHToLoc
        repoToloc.Width = 170
        repoToloc.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoToloc)

        repoToloc = New GridViewTextBoxColumn()
        repoToloc.FormatString = ""
        repoToloc.HeaderText = "To Main Location"
        repoToloc.Name = colHToMainLocName
        repoToloc.Width = 170
        repoToloc.ReadOnly = True
        gvHead.MasterTemplate.Columns.Add(repoToloc)


        gvHead.ShowFilteringRow = True
        gvHead.EnableFiltering = True
        gvHead.AllowDeleteRow = False
        gvHead.AllowAddNewRow = False
        gvHead.ShowGroupPanel = False
        gvHead.AllowColumnReorder = False
        gvHead.AllowRowReorder = False
        gvHead.EnableSorting = False
        gvHead.EnableAlternatingRowColor = True
        gvHead.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvHead.MasterTemplate.ShowRowHeaderColumn = False
        gvHead.TableElement.TableHeaderHeight = 40
    End Sub

    Sub LoadBlankGridDetail()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.HeaderText = " "
        repoSelect.Name = colDSelect
        repoSelect.ReadOnly = False
        repoSelect.Width = 25
        repoSelect.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter
        gv1.MasterTemplate.Columns.Add(repoSelect)


        Dim repoCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCode.FormatString = ""
        repoCode.HeaderText = "Transfer Out No"
        repoCode.Name = colDCode
        repoCode.Width = 180
        repoCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCode)

        Dim repoICode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoICode.FormatString = ""
        repoICode.HeaderText = "Item Code"
        repoICode.Name = colDICode
        repoICode.Width = 100
        repoICode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoICode)

        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoIName.FormatString = ""
        repoIName.HeaderText = "Item"
        repoIName.Name = colDIName
        repoIName.Width = 180
        repoIName.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoIName)

        Dim repoUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoUnit.FormatString = ""
        repoUnit.HeaderText = "Unit"
        repoUnit.Name = colDUnit
        repoUnit.Width = 60
        repoUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoUnit)

        Dim repoALTUnit As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoALTUnit.FormatString = ""
        repoALTUnit.HeaderText = "Unit"
        repoALTUnit.Name = colDALTUnit
        repoALTUnit.Width = 60
        repoALTUnit.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoALTUnit)


        Dim repoRate As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoRate.FormatString = ""
        repoRate.HeaderText = "Rate"
        repoRate.Name = colDRate
        repoRate.ReadOnly = True
        repoUnit.IsVisible = False
        repoRate.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoRate)

        Dim repoOrderQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoOrderQty.FormatString = ""
        repoOrderQty.HeaderText = "Out Qty"
        repoOrderQty.Name = colDOutQty
        repoOrderQty.ReadOnly = True
        repoOrderQty.Width = 80
        repoOrderQty.WrapText = True
        repoOrderQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoOrderQty)


        Dim repoInQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoInQty.FormatString = ""
        repoInQty.HeaderText = "In Qty"
        repoInQty.Name = colDLoadInQty
        repoInQty.ReadOnly = True
        repoInQty.Width = 80
        repoInQty.WrapText = True
        repoInQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoInQty)

        Dim repoUnapproveQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoUnapproveQty.FormatString = ""
        repoUnapproveQty.HeaderText = "Unapproved Qty"
        repoUnapproveQty.Name = colDUnApprovedQty
        repoUnapproveQty.ReadOnly = True
        repoUnapproveQty.Width = 80
        repoUnapproveQty.WrapText = True
        repoUnapproveQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoUnapproveQty)

        Dim repoPendingQty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoPendingQty.FormatString = ""
        repoPendingQty.HeaderText = "Pending Qty"
        repoPendingQty.Name = colDPendingQty
        repoPendingQty.ReadOnly = True
        repoPendingQty.Width = 80
        repoPendingQty.WrapText = True
        repoPendingQty.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoPendingQty)

        Dim repoMRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoMRP.FormatString = ""
        repoMRP.HeaderText = "MRP"
        repoMRP.Name = colDMRP
        repoMRP.ReadOnly = True
        repoMRP.Width = 80
        repoMRP.WrapText = True
        repoMRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoMRP)


        Dim repoFromLoc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFromLoc.FormatString = ""
        repoFromLoc.HeaderText = "From Location"
        repoFromLoc.Name = colDFromLoc
        repoFromLoc.Width = 170
        repoFromLoc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoFromLoc)

        Dim repoToloc As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoToloc.FormatString = ""
        repoToloc.HeaderText = "To Location"
        repoToloc.Name = colDToLoc
        repoToloc.Width = 90
        repoToloc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoToloc)

        repoToloc = New GridViewTextBoxColumn()
        repoToloc.FormatString = ""
        repoToloc.HeaderText = "To Main Location"
        repoToloc.Name = colDToMainLocName
        repoToloc.Width = 170
        repoToloc.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoToloc)

        Dim repoFOCItem As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoFOCItem.FormatString = ""
        repoFOCItem.HeaderText = "FOC Item"
        repoFOCItem.Name = colFOCItem
        repoFOCItem.ReadOnly = True
        repoFOCItem.IsVisible = False
        repoFOCItem.Width = 100
        gv1.MasterTemplate.Columns.Add(repoFOCItem)


        gv1.AllowAddNewRow = False
        gv1.AllowDeleteRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = True
        gv1.AllowRowReorder = True
        gv1.EnableSorting = False
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.MasterTemplate.ShowColumnHeaders = True
        gv1.EnableAlternatingRowColor = True
        gv1.EnableFiltering = True
        gv1.ShowFilteringRow = True
        gv1.TableElement.TableHeaderHeight = 40
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        btnCancelPressed()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        btnOKPressed()

    End Sub

    Sub btnCancelPressed()
        Me.Close()
    End Sub

    Sub btnOKPressed()
        Dim dblPendingQty As Double
        ''richa agarwal to select only one transfer at a time
        If Not isAllowed() Then
            Exit Sub
        End If
        '------------
        ArrReturn = New List(Of clsTransferDCCDetail)
        Dim obj As clsTransferDCCDetail = Nothing
        For ii As Integer = 0 To gv1.RowCount - 1
            If (clsCommon.myCBool(gv1.Rows(ii).Cells(colDSelect).Value)) Then
                obj = New clsTransferDCCDetail()
                obj.TransferOutNo = clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)
                obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells(colDToLoc).Value)
                obj.Item_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDICode).Value)
                obj.Item_Desc = clsCommon.myCstr(gv1.Rows(ii).Cells(colDIName).Value)
                obj.Item_Cost = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDRate).Value)
                obj.Unit_code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDUnit).Value)
                obj.Alt_Unit_Code = clsCommon.myCstr(gv1.Rows(ii).Cells(colDALTUnit).Value)
                'obj.Location = clsCommon.myCstr(gv1.Rows(ii).Cells(coldlo "Location").Value)
                'obj.LocationName = clsCommon.myCstr(gv1.Rows(ii).Cells("LocationName").Value)
                obj.Out_Qty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDOutQty).Value)
                dblPendingQty = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDPendingQty).Value)
                obj.MRP = clsCommon.myCdbl(gv1.Rows(ii).Cells(colDMRP).Value)
                obj.FOCItem = clsCommon.myCdbl(gv1.Rows(ii).Cells(colFOCItem).Value)
                If dblPendingQty > 0 Then
                    ArrReturn.Add(obj)
                End If
            End If
        Next



        If ArrReturn.Count <= 0 Then
            ' common.clsCommon.MyMessageBoxShow("Please select at least one non zero Pending Transfer item.")
            common.clsCommon.MyMessageBoxShow(Me, "Please select at least one non zero Pending Transfer item", Me.Text)
        Else
            Me.Close()
        End If
    End Sub
    Private Function isAllowed() As Boolean
        Dim strheaddatacount As Integer = 0
        For ii As Integer = 0 To gvHead.RowCount - 1
            If clsCommon.myCBool(gvHead.Rows(ii).Cells(colHSelect).Value) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.Rows(ii).Cells(colHCode).Value)
                strheaddatacount = strheaddatacount + 1
            End If
        Next
        If strheaddatacount > 1 Then
            clsCommon.MyMessageBoxShow(Me, "Select only one Transfer at a time.", Me.Text)
            Return False
        End If
        Return True
    End Function

    Private Sub FrmPendingRequistion_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnOKPressed()
        ElseIf e.KeyCode = Keys.Escape Then
            btnCancelPressed()
        End If
    End Sub

    Private Sub gv1_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv1.DoubleClick
        If gv1.CurrentColumn Is gv1.Columns(colDCode) Then
            Dim strCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDCode).Value)
            Dim SelectStatus As Boolean = clsCommon.myCBool(gv1.CurrentRow.Cells(colDSelect).Value)
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows(ii).Cells(colDSelect).Value = Not SelectStatus
                End If
            Next
        End If
    End Sub

    Private Sub gvHead_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvHead.ValueChanging
        If Not IsInsideLoadData Then
            If gvHead.CurrentColumn Is gvHead.Columns(colHSelect) Then
                Dim strCode As String = clsCommon.myCstr(gvHead.CurrentRow.Cells(colHCode).Value)
                If clsCommon.myLen(strCode) > 0 Then
                    LoadDetailData(e.NewValue, strCode)
                End If
            End If
        End If
    End Sub

    Sub LoadDetailData(ByVal NewVal As Boolean, ByVal strCode As String)
        IsInsideLoadDataOfItem = True
        If NewVal Then
            For Each dr As DataRow In dtAllData.Rows
                If clsCommon.CompairString(strCode, clsCommon.myCstr(dr("Code"))) = CompairStringResult.Equal Then
                    Dim strToLocationCode As String = clsCommon.myCstr(dr("ToLocation"))
                    If clsCommon.myLen(ToLocCode) <= 0 AndAlso clsCommon.myLen(strToLocationCode) > 0 Then
                        ToLocCode = strToLocationCode
                    End If

                    gv1.Rows.AddNew()
                    'If (clsCommon.myLen(strVendorCode) > 0 Xor clsCommon.CompairString(VendorCode, strVendorCode) = CompairStringResult.Equal) Then
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = False
                    'Else
                    '    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    'End If
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDSelect).Value = True
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDCode).Value = clsCommon.myCstr(dr("code"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDICode).Value = clsCommon.myCstr(dr("ICode"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDIName).Value = clsCommon.myCstr(dr("IName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnit).Value = clsCommon.myCstr(dr("Unit"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDALTUnit).Value = clsCommon.myCstr(dr("ALT_Unit"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDVendor).Value = clsCommon.myCstr(dr("Vendor"))
                    'gv1.Rows(gv1.Rows.Count - 1).Cells(colDVendorName).Value = clsCommon.myCstr(dr("VendorName"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDRate).Value = clsCommon.myCdbl(dr("Rate"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDOutQty).Value = clsCommon.myCdbl(dr("OutQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDLoadInQty).Value = clsCommon.myCdbl(dr("InQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDUnApprovedQty).Value = clsCommon.myCdbl(dr("UnapprovedQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDPendingQty).Value = clsCommon.myCdbl(dr("PedningQty"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDFromLoc).Value = clsCommon.myCstr(dr("FromLocation"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDToLoc).Value = clsCommon.myCstr(dr("ToLocation"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDToMainLocName).Value = clsCommon.myCstr(dr("ToGITLocation"))
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colDMRP).Value = clsCommon.myCstr(dr("MRP"))
                End If
            Next
        Else
            For ii As Integer = gv1.Rows.Count - 1 To 0 Step -1
                If clsCommon.CompairString(strCode, clsCommon.myCstr(gv1.Rows(ii).Cells(colDCode).Value)) = CompairStringResult.Equal Then
                    gv1.Rows.RemoveAt(ii)
                End If
            Next
        End If
        IsInsideLoadDataOfItem = False
    End Sub
    Dim IsInsideLoadDataOfItem As Boolean = False
    Private Sub gv1_ValueChanging(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gv1.ValueChanging
        If Not IsInsideLoadDataOfItem Then
            If gv1.CurrentColumn Is gv1.Columns(colDSelect) Then
                If e.NewValue Then
                    Dim strFromLocCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDFromLoc).Value)
                    Dim strToLocCode As String = clsCommon.myCstr(gv1.CurrentRow.Cells(colDToLoc).Value)

                    If Not clsCommon.CompairString(clsCommon.myCstr(gv1.CurrentRow.Cells(colDToLoc).Value), ToLocCode) = CompairStringResult.Equal Then
                        common.clsCommon.MyMessageBoxShow(Me, "Can't select Current Transfer.It is For Location:" + strToLocCode)
                        e.Cancel = True
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        LoadData()
    End Sub

    Sub LoadData()
        Try
            Dim qry As String = Nothing
            Dim strDefaultLoc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select top 1 tspl_user_master.default_location from tspl_user_master left outer join tspl_location_master on tspl_location_master.location_code=tspl_user_master.default_location where tspl_user_master.user_code='" + objCommonVar.CurrentUserCode + "'"))
            Dim StrCrateTransferFromBooking As String = clsFixedParameter.GetData(clsFixedParameterType.CreateTransferFromBooking, clsFixedParameterCode.CreateTransferFromBooking, Nothing)
            Dim InDocMandatoryOnInternalTransfer As Boolean = clsCommon.myCBool(IIf(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.InDocMandatoryOnInternalTransfer, clsFixedParameterCode.InDocMandatoryOnInternalTransfer, Nothing)) = 1, True, False))
            If StrCrateTransferFromBooking = "1" Then
                qry = " select CAST(0 as bit) as Sel,code,ICode,max(IName) as IName,Unit,ALT_Unit,MAX(TSPL_LOCATION_MASTER.Location_Desc)  as FromLocation, " + Environment.NewLine &
                "MAX(TSPL_LOCATION_MASTER_1.Location_Desc) as ToLocationName,(TSPL_LOCATION_MASTER_GIT.location_desc) as ToGITLocation, SUM(Qty* case when RI=1 then 1 else 0 end) as OutQty, " + Environment.NewLine &
                "SUM(Qty* case when RI=-1 then 1 else 0 end) as InQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty , " + Environment.NewLine &
                "MAX(Rate) as Rate,Final.MRP as MRP ,(TransDate) as TransDate  ,max(final.FromLocation) as FromLocation,MAX(final.ToLocation) as ToLocation  " + Environment.NewLine &
                "from ( " + Environment.NewLine &
                "select TSPL_Transfer_ORDER_DETAIL.Line_No,TSPL_Transfer_ORDER_DETAIL.Document_No as Code,'' as Vendor,TSPL_Transfer_ORDER_DETAIL.Item_Code as ICode, " + Environment.NewLine &
                "TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_Transfer_ORDER_DETAIL.Row_Type as IType, TSPL_Transfer_ORDER_DETAIL.Out_Qty  as Qty, " + Environment.NewLine &
                "0 as Unapproved,TSPL_Transfer_ORDER_DETAIL.Unit_Code as Unit,TSPL_Transfer_ORDER_DETAIL.ALT_Unit_Code as ALT_Unit,From_Location as FromLocation,TSPL_TRANSFER_ORDER_HEAD.To_Location  as ToLocation , " + Environment.NewLine &
                "1 as RI,TSPL_Transfer_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_TRANSFER_ORDER_HEAD.Tax_Group,TSPL_Transfer_ORDER_DETAIL.TAX1_Rate, " + Environment.NewLine &
                "TSPL_Transfer_ORDER_DETAIL.TAX2_Rate,TSPL_Transfer_ORDER_DETAIL.TAX3_Rate,TSPL_Transfer_ORDER_DETAIL.TAX4_Rate,TSPL_Transfer_ORDER_DETAIL.TAX5_Rate, " + Environment.NewLine &
                "TSPL_Transfer_ORDER_DETAIL.TAX6_Rate,TSPL_Transfer_ORDER_DETAIL.TAX7_Rate,TSPL_Transfer_ORDER_DETAIL.TAX8_Rate,TSPL_Transfer_ORDER_DETAIL.TAX9_Rate, " + Environment.NewLine &
                "TSPL_Transfer_ORDER_DETAIL.TAX10_Rate,TSPL_Transfer_ORDER_DETAIL.MRP ,TSPL_Transfer_ORDER_DETAIL.Disc_Per,TSPL_TRANSFER_ORDER_HEAD.Document_Date as TransDate " + Environment.NewLine &
                "from TSPL_Transfer_ORDER_DETAIL  " + Environment.NewLine &
                "left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_Transfer_ORDER_DETAIL.Document_No " + Environment.NewLine &
                "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_Transfer_ORDER_DETAIL.Item_Code left Outer Join TSPL_GATEPASS_TRANSFER_HEAD on TSPL_GATEPASS_TRANSFER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.GatePassNo  " + Environment.NewLine &
                "where TSPL_TRANSFER_ORDER_HEAD.Status=1   and Transfer_Type='O' and Is_Status_In ='N'  and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) > =convert(date,'" + tp_FromDate.Value + "',103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <= convert(date,'" + tp_ToDate.Value + "',103) " + Environment.NewLine &
                "union all " + Environment.NewLine &
                "select TSPL_Transfer_ORDER_DETAIL.Line_No,TSPL_Transfer_ORDER_DETAIL.TransferOutNo as Code,'' as Vendor,TSPL_Transfer_ORDER_DETAIL.Item_Code as ICode, " + Environment.NewLine &
                "'' as IName,'' as IType,isnull(TSPL_Transfer_ORDER_DETAIL.In_Qty,0) as Qty,0 as Unapproved,TSPL_Transfer_ORDER_DETAIL.Unit_code as Unit,TSPL_Transfer_ORDER_DETAIL.ALT_Unit_code as ALT_Unit, " + Environment.NewLine &
                "'' as Location,TSPL_TRANSFER_ORDER_HEAD.From_Location as ToLocation,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate, " + Environment.NewLine &
                "0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate " + Environment.NewLine &
                "from TSPL_Transfer_ORDER_DETAIL " + Environment.NewLine &
                "left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_Transfer_ORDER_DETAIL.Document_No left Outer Join TSPL_GATEPASS_TRANSFER_HEAD on TSPL_GATEPASS_TRANSFER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.GatePassNo " + Environment.NewLine &
                "where TSPL_TRANSFER_ORDER_HEAD.Status=1   and len(isnull(TSPL_Transfer_ORDER_DETAIL.Document_No,''))>0  and Transfer_Type='I'  and Is_Status_In ='N'  " + Environment.NewLine &
                "union all   " + Environment.NewLine &
                "select TSPL_Transfer_ORDER_DETAIL.Line_No,TSPL_Transfer_ORDER_DETAIL.TransferOutNo as Code,'' as Vendor,TSPL_Transfer_ORDER_DETAIL.Item_Code as ICode, " + Environment.NewLine &
                "'' as IName,'' as IType,0 as Qty,isnull(TSPL_Transfer_ORDER_DETAIL.In_Qty,0) as Unapproved,TSPL_Transfer_ORDER_DETAIL.Unit_code as Unit,TSPL_Transfer_ORDER_DETAIL.ALT_Unit_code as ALT_Unit, " + Environment.NewLine &
                "'' as Location,TSPL_TRANSFER_ORDER_HEAD.From_Location as ToLocation,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate, " + Environment.NewLine &
                "0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate " + Environment.NewLine &
                "from TSPL_Transfer_ORDER_DETAIL " + Environment.NewLine &
                "left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_Transfer_ORDER_DETAIL.Document_No left Outer Join TSPL_GATEPASS_TRANSFER_HEAD on TSPL_GATEPASS_TRANSFER_HEAD.Document_No=TSPL_TRANSFER_ORDER_DETAIL.GatePassNo  " + Environment.NewLine &
                "where TSPL_TRANSFER_ORDER_HEAD.Status=0  and Transfer_Type='I'  and Is_Status_In ='N'    and len(isnull(TSPL_Transfer_ORDER_DETAIL.Document_No,''))>0 and " + Environment.NewLine &
                "TSPL_Transfer_ORDER_DETAIL.Document_No not in ('')  " + Environment.NewLine &
                ")Final " + Environment.NewLine &
                " left join TSPL_TRANSFER_RETURN on Final.Code=TSPL_TRANSFER_RETURN.Transfer_No " &
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=final.FromLocation " + Environment.NewLine &
                "left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1.Location_Code=Final.ToLocation " + Environment.NewLine &
                "left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_GIT on TSPL_LOCATION_MASTER_GIT.git_location=Final.ToLocation "
                If clsCommon.myLen(arrLoc) > 0 Then
                    qry += "inner Join (select TSPL_LOCATION_MASTER.Location_Code from TSPL_LOCATION_MASTER left outer join TSPL_GL_SEGMENT_PERMISSION on " + Environment.NewLine &
                        "TSPL_GL_SEGMENT_PERMISSION.Segment_Code=TSPL_LOCATION_MASTER.Loc_Segment_Code where TSPL_GL_SEGMENT_PERMISSION.User_Code='" & objCommonVar.CurrentUserCode & "' and " + Environment.NewLine &
                        "TSPL_GL_SEGMENT_PERMISSION.GL_Segment in (select Seg_No from TSPL_GL_SEGMENT_CODE where Seg_no='7')  ) a on " + Environment.NewLine &
                        "tspl_location_master_git.Location_Code= a.Location_Code"
                End If
                'qry += " where 1=1 and TSPL_TRANSFER_RETURN.Document_No is null"
                'qry += " group by Code,ICode,Unit,ALT_UNIT,MRP  having  SUM((Qty *RI)- Unapproved)>0 "

                qry += " where 1=1 "
                'If clsCommon.myLen(arrLoc) > 0 Then
                '    qry += " AND tspl_location_master_git.location_code in (" + arrLoc + ") "
                'End If
                qry += "AND ( Code NOT IN (Select TransferOutNo from TSPL_TRANSFER_ORDER_HEAD WHERE Transfer_Type='I')" &
                    " AND Code NOT IN (Select Document_No from TSPL_TRANSFER_ORDER_HEAD where Document_No  in (Select Transfer_No  from TSPL_TRANSFER_RETURN  WHERE DOcument_Type ='O') and Is_Status_In='N') ) " &
                    " or Code IN (select xx.TransferOutNo   from TSPL_TRANSFER_RETURN inner join (Select document_no,TransferOutNo  from TSPL_TRANSFER_ORDER_HEAD where TransferOutNo in (select Document_No  from TSPL_TRANSFER_Order_head where Transfer_Type ='O' and Is_Status_In ='N'  ) and Transfer_Type ='I')XX on TSPL_TRANSFER_RETURN.Transfer_No=XX.document_no and TSPL_TRANSFER_RETURN.DOcument_Type ='I') " &
                    " group by TransDate,Code,ICode,Unit,ALT_UNIT,MRP,TSPL_LOCATION_MASTER_GIT.location_desc having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by convert(Datetime, TransDate,103),Code,max(Line_No) "



            Else
                qry = " select CAST(0 as bit) as Sel,code,ICode,max(IName) as IName,Unit,ALT_Unit,MAX(TSPL_LOCATION_MASTER.Location_Desc)  as FromLocation, " + Environment.NewLine &
                "MAX(TSPL_LOCATION_MASTER_1.Location_Desc) as ToLocationName,(TSPL_LOCATION_MASTER_GIT.location_desc) as ToGITLocation, SUM(Qty* case when RI=1 then 1 else 0 end) as OutQty, " + Environment.NewLine &
                "SUM(Qty* case when RI=-1 then 1 else 0 end) as InQty, SUM(Unapproved) as UnapprovedQty, SUM((Qty *RI)- Unapproved) as PedningQty , " + Environment.NewLine &
                "MAX(Rate) as Rate,Final.MRP as MRP ,(TransDate) as TransDate  ,max(final.FromLocation) as FromLocation,MAX(final.ToLocation) as ToLocation,MAX(fINAL.IsJobWorkType ) AS IsJobWorkType " + Environment.NewLine &
                "from ( " + Environment.NewLine &
                "select TSPL_Transfer_ORDER_DETAIL.Line_No,TSPL_Transfer_ORDER_DETAIL.Document_No as Code,'' as Vendor,TSPL_Transfer_ORDER_DETAIL.Item_Code as ICode, " + Environment.NewLine &
                "TSPL_ITEM_MASTER.Item_Desc as IName,TSPL_Transfer_ORDER_DETAIL.Row_Type as IType, TSPL_Transfer_ORDER_DETAIL.Out_Qty  as Qty, " + Environment.NewLine &
                "0 as Unapproved,TSPL_Transfer_ORDER_DETAIL.Unit_Code as Unit,TSPL_Transfer_ORDER_DETAIL.ALT_Unit_Code as ALT_Unit,From_Location as FromLocation,TSPL_TRANSFER_ORDER_HEAD.To_Location  as ToLocation , " + Environment.NewLine &
                "1 as RI,TSPL_Transfer_ORDER_DETAIL.Item_Cost as Rate,1 as Chk,TSPL_TRANSFER_ORDER_HEAD.Tax_Group,TSPL_Transfer_ORDER_DETAIL.TAX1_Rate, " + Environment.NewLine &
                "TSPL_Transfer_ORDER_DETAIL.TAX2_Rate,TSPL_Transfer_ORDER_DETAIL.TAX3_Rate,TSPL_Transfer_ORDER_DETAIL.TAX4_Rate,TSPL_Transfer_ORDER_DETAIL.TAX5_Rate, " + Environment.NewLine &
                "TSPL_Transfer_ORDER_DETAIL.TAX6_Rate,TSPL_Transfer_ORDER_DETAIL.TAX7_Rate,TSPL_Transfer_ORDER_DETAIL.TAX8_Rate,TSPL_Transfer_ORDER_DETAIL.TAX9_Rate, " + Environment.NewLine &
                "TSPL_Transfer_ORDER_DETAIL.TAX10_Rate,TSPL_Transfer_ORDER_DETAIL.MRP ,TSPL_Transfer_ORDER_DETAIL.Disc_Per,Document_Date as TransDate,TSPL_TRANSFER_ORDER_HEAD.IsJobWorkType " + Environment.NewLine &
                "from TSPL_Transfer_ORDER_DETAIL  " + Environment.NewLine &
                "left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_Transfer_ORDER_DETAIL.Document_No " + Environment.NewLine &
                "left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_Transfer_ORDER_DETAIL.Item_Code " + Environment.NewLine &
                "where TSPL_TRANSFER_ORDER_HEAD.Status=1   and Transfer_Type='O' and Is_Status_In ='N' and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) > =convert(date,'" + tp_FromDate.Value + "',103) and convert(date,TSPL_TRANSFER_ORDER_HEAD.Document_Date,103) <= convert(date,'" + tp_ToDate.Value + "',103)  "
                If InDocMandatoryOnInternalTransfer = False Then
                    qry += " and TSPL_TRANSFER_ORDER_HEAD.InternalTransfer=0 "
                End If
                qry += " union all " + Environment.NewLine &
                "select TSPL_Transfer_ORDER_DETAIL.Line_No,TSPL_Transfer_ORDER_DETAIL.TransferOutNo as Code,'' as Vendor,TSPL_Transfer_ORDER_DETAIL.Item_Code as ICode, " + Environment.NewLine &
                "'' as IName,'' as IType,isnull(TSPL_Transfer_ORDER_DETAIL.In_Qty,0) as Qty,0 as Unapproved,TSPL_Transfer_ORDER_DETAIL.Unit_code as Unit,TSPL_Transfer_ORDER_DETAIL.ALT_Unit_code as ALT_Unit, " + Environment.NewLine &
                "'' as Location,TSPL_TRANSFER_ORDER_HEAD.From_Location as ToLocation,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate, " + Environment.NewLine &
                "0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,TSPL_TRANSFER_ORDER_HEAD.IsJobWorkType " + Environment.NewLine &
                "from TSPL_Transfer_ORDER_DETAIL " + Environment.NewLine &
                "left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_Transfer_ORDER_DETAIL.Document_No " + Environment.NewLine &
                "where TSPL_TRANSFER_ORDER_HEAD.Status=1   and len(isnull(TSPL_Transfer_ORDER_DETAIL.Document_No,''))>0  and Transfer_Type='I'  and Is_Status_In ='N'  " + Environment.NewLine &
                "union all   " + Environment.NewLine &
                "select TSPL_Transfer_ORDER_DETAIL.Line_No,TSPL_Transfer_ORDER_DETAIL.TransferOutNo as Code,'' as Vendor,TSPL_Transfer_ORDER_DETAIL.Item_Code as ICode, " + Environment.NewLine &
                "'' as IName,'' as IType,0 as Qty,isnull(TSPL_Transfer_ORDER_DETAIL.In_Qty,0) as Unapproved,TSPL_Transfer_ORDER_DETAIL.Unit_code as Unit,TSPL_Transfer_ORDER_DETAIL.ALT_Unit_code as ALT_Unit, " + Environment.NewLine &
                "'' as Location,TSPL_TRANSFER_ORDER_HEAD.From_Location as ToLocation,-1 as RI,0 as Rate,0 as Chk,'' as Tax_Group,0 as TAX1_Rate,0 as TAX2_Rate,0 as TAX3_Rate,0 as TAX4_Rate, " + Environment.NewLine &
                "0 as TAX5_Rate,0 as TAX6_Rate,0 as TAX7_Rate,0 as TAX8_Rate,0 as TAX9_Rate,0 as TAX10_Rate,MRP as MRP,0 as Disc_Per,null as TransDate,TSPL_TRANSFER_ORDER_HEAD.IsJobWorkType " + Environment.NewLine &
                "from TSPL_Transfer_ORDER_DETAIL " + Environment.NewLine &
                "left outer join TSPL_TRANSFER_ORDER_HEAD on TSPL_TRANSFER_ORDER_HEAD.Document_No=TSPL_Transfer_ORDER_DETAIL.Document_No " + Environment.NewLine &
                "where TSPL_TRANSFER_ORDER_HEAD.Status=0  and Transfer_Type='I'  and Is_Status_In ='N'   and len(isnull(TSPL_Transfer_ORDER_DETAIL.Document_No,''))>0 and " + Environment.NewLine &
                "TSPL_Transfer_ORDER_DETAIL.Document_No not in ('')  " + Environment.NewLine &
                ")Final " + Environment.NewLine &
                " left join TSPL_TRANSFER_RETURN on Final.Code=TSPL_TRANSFER_RETURN.Transfer_No " &
                "left outer join TSPL_LOCATION_MASTER on TSPL_LOCATION_MASTER.Location_Code=final.FromLocation " + Environment.NewLine &
                "left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_1 on TSPL_LOCATION_MASTER_1.Location_Code=Final.ToLocation "
                If clsCommon.myCBool(clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.AllowTransferInAfterGatePassOnly, clsFixedParameterCode.AllowTransferInAfterGatePassOnly, Nothing))) = True Then
                    qry += "  inner join  TSPL_DAIRYSALE_GATEPASS_MASTER on TSPL_DAIRYSALE_GATEPASS_MASTER.AgainstTransferNo = Final.code and TSPL_DAIRYSALE_GATEPASS_MASTER.Post = 'Y'"
                End If
                If clsCommon.CompairString(InternalTransfer, "1") = CompairStringResult.Equal Then
                    qry += " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_GIT on TSPL_LOCATION_MASTER_GIT.Location_code=Final.ToLocation "
                Else
                    qry += " left outer join TSPL_LOCATION_MASTER as TSPL_LOCATION_MASTER_GIT on TSPL_LOCATION_MASTER_GIT.git_location=Final.ToLocation "
                End If

                If clsCommon.myLen(arrLoc) > 0 Then
                    qry += "inner Join (select TSPL_LOCATION_MASTER.Location_Code from TSPL_LOCATION_MASTER left outer join TSPL_GL_SEGMENT_PERMISSION on " + Environment.NewLine & _
                        "TSPL_GL_SEGMENT_PERMISSION.Segment_Code=TSPL_LOCATION_MASTER.Loc_Segment_Code where TSPL_GL_SEGMENT_PERMISSION.User_Code='" & objCommonVar.CurrentUserCode & "' and " + Environment.NewLine & _
                        "TSPL_GL_SEGMENT_PERMISSION.GL_Segment in (select Seg_No from TSPL_GL_SEGMENT_CODE where Seg_no='7')  ) a on " + Environment.NewLine & _
                        "tspl_location_master_git.Location_Code= a.Location_Code"
                End If
                'qry += " where 1=1 and TSPL_TRANSFER_RETURN.Document_No is null"


                'qry += " group by Code,ICode,Unit,ALT_UNIT,MRP  having  SUM((Qty *RI)- Unapproved)>0 "

                qry += " where 1=1 "
                If clsCommon.CompairString(JobWorkType, "1") = CompairStringResult.Equal Then
                    qry += " AND FINAL.IsJobWorkType =1 "
                Else
                    qry += " AND FINAL.IsJobWorkType =0 "
                End If
                'If clsCommon.myLen(arrLoc) > 0 Then
                '    qry += " AND tspl_location_master_git.location_code in (" + arrLoc + ") "
                'End If
                qry += "AND ( Code NOT IN (Select TransferOutNo from TSPL_TRANSFER_ORDER_HEAD WHERE Transfer_Type='I')" & _
                    " AND Code NOT IN (Select Document_No from TSPL_TRANSFER_ORDER_HEAD where Document_No  in (Select Transfer_No  from TSPL_TRANSFER_RETURN  WHERE DOcument_Type ='O') and Is_Status_In='N') ) " & _
                    " or Code IN (select xx.TransferOutNo   from TSPL_TRANSFER_RETURN inner join (Select document_no,TransferOutNo  from TSPL_TRANSFER_ORDER_HEAD where TransferOutNo in (select Document_No  from TSPL_TRANSFER_Order_head where Transfer_Type ='O' and Is_Status_In ='N'  ) and Transfer_Type ='I')XX on TSPL_TRANSFER_RETURN.Transfer_No=XX.document_no and TSPL_TRANSFER_RETURN.DOcument_Type ='I') " & _
                    " group by TransDate,Code,ICode,Unit,ALT_UNIT,MRP,TSPL_LOCATION_MASTER_GIT.location_desc having SUM(Chk)>0 and SUM(Qty *RI) <>0  order by convert(Datetime, TransDate,103), Code,max(Line_No) "


            End If
            dtAllData = clsDBFuncationality.GetDataTable(qry)


            If dtAllData Is Nothing OrElse dtAllData.Rows.Count <= 0 Then
                common.clsCommon.MyMessageBoxShow(Me, "No Pending item found ", Me.Text)
                'Me.Close()
            End If
            LoadHeadData()
            LoadBlankGridDetail()
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
End Class

