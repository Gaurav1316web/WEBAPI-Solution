'===================================================================
'------------Created By--Pankaj Kumar Chaudhary
'------------Created Date--13/02/2013-3:00PM-Wednesday
'------------Table Used--TSPL_ITEM_BARCODE, TSPL_ITEM_MASTER
'===================================================================
Imports common
Imports System.Data.SqlClient

Public Class FrmBarCodeGenerator
    Inherits FrmMainTranScreen

#Region "Variables"
    Dim Qry As String = ""
    Dim IsLoaddata As Boolean = False
    Const colLineNo As String = "LineNo"
    Const colItemCode As String = "ItemCode"
    Const colBarCode As String = "BarCode"
    Const colItemName As String = "ItemName"
    Const colItemCost As String = "ItemCost"
    Const colSellingPrice As String = "Selling Price"
    Const colItemMRP As String = "ItemMRP"
    Const colColor As String = "Color"
    Const colSize As String = "Size"
#End Region

    Private Sub FrmAssetSegment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadItemType()
        SetUserMgmtNew()
        chkBarCode.Checked = True
        chkMRP.Checked = True
        chkName.Checked = True
        LoadBlankGridPrint()
    End Sub

    Private Sub SetUserMgmtNew()
        ''MyBase.SetUserMgmt(clsUserMgtCode.frmBarCodeGenerator)
        If Not (MyBase.isReadFlag) Then
            '--------richa Ticket no. BM00000003121 15/07/2014 
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        '--------richa Ticket no. BM00000003014 02/07/2014 to enable/disable import/export option acc. to user mgmt setting -----------
        If btnSave.Visible = True Then
            RadMenuItem1.Enabled = True
            RadMenuItem2.Enabled = True
        Else
            RadMenuItem1.Enabled = False
            RadMenuItem2.Enabled = False
        End If
        '--------------------------------------------------
    End Sub

    Sub LoadItemType()
        Dim dt As New DataTable
        dt.Columns.Add("Code", GetType(String))
        dt.Columns.Add("Name", GetType(String))

        Dim dr As DataRow = dt.NewRow()
        dr("Code") = "F"
        dr("Name") = "Finished Goods"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "P"
        'dr("Name") = "Promotional Items"
        'dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "R"
        dr("Name") = "Raw Material"
        dt.Rows.Add(dr)

        dr = dt.NewRow()
        dr("Code") = "O"
        dr("Name") = "Others"
        dt.Rows.Add(dr)

        'dr = dt.NewRow()
        'dr("Code") = "T"
        'dr("Name") = "Trading Items"
        'dt.Rows.Add(dr)

        ddlItem.DataSource = dt
        ddlItem.ValueMember = "Code"
        ddlItem.DisplayMember = "Name"
    End Sub

    Sub LoadBlankGrid()
        gv1.Rows.Clear()
        gv1.Columns.Clear()

        Dim LineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        LineNo.FormatString = ""
        LineNo.HeaderText = "Line No"
        LineNo.Name = colLineNo
        LineNo.Width = 60
        LineNo.ReadOnly = True
        LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(LineNo)

        Dim BarCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        BarCode.FormatString = ""
        BarCode.HeaderText = "Bar Code"
        BarCode.Name = colBarCode
        BarCode.Width = 130
        BarCode.ReadOnly = False
        BarCode.IsVisible = True
        BarCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(BarCode)

        Dim ItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 130
        ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(ItemCode)

        Dim ItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemName.FormatString = ""
        ItemName.HeaderText = "Item Name"
        ItemName.Name = colItemName
        ItemName.Width = 250
        ItemName.ReadOnly = True
        ItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(ItemName)

        Dim ItemCost As GridViewDecimalColumn = New GridViewDecimalColumn()
        ItemCost.FormatString = ""
        ItemCost.HeaderText = "Item Cost"
        ItemCost.Name = colItemCost
        ItemCost.Width = 120
        ItemCost.ReadOnly = True
        ItemCost.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(ItemCost)

        Dim SellingPrice As GridViewDecimalColumn = New GridViewDecimalColumn()
        SellingPrice.FormatString = ""
        SellingPrice.HeaderText = "Selling Price"
        SellingPrice.Name = colSellingPrice
        SellingPrice.Width = 120
        SellingPrice.ReadOnly = False
        SellingPrice.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(SellingPrice)

        Dim MRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        MRP.FormatString = ""
        MRP.HeaderText = "MRP"
        MRP.Name = colItemMRP
        MRP.Width = 120
        MRP.ReadOnly = False
        MRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(MRP)

        Dim Color As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Color.FormatString = ""
        Color.HeaderText = "Color"
        Color.Name = colColor
        Color.Width = 60
        Color.ReadOnly = False
        Color.IsVisible = False
        Color.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(Color)

        Dim Size As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        Size.FormatString = ""
        Size.HeaderText = "Size"
        Size.Name = colSize
        Size.Width = 60
        Size.ReadOnly = False
        Size.IsVisible = False
        Size.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gv1.MasterTemplate.Columns.Add(Size)

        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Sub LoadBlankGridPrint()
        gvPrint.Rows.Clear()
        gvPrint.Columns.Clear()

        'Dim LineNo As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        'LineNo.FormatString = ""
        'LineNo.HeaderText = "Line No"
        'LineNo.Name = colLineNo
        'LineNo.Width = 60
        'LineNo.ReadOnly = True
        'LineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        'gvPrint.MasterTemplate.Columns.Add(LineNo)

        Dim BarCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        BarCode.FormatString = ""
        BarCode.HeaderText = "Bar Code"
        BarCode.Name = colBarCode
        BarCode.Width = 70
        BarCode.ReadOnly = False
        BarCode.IsVisible = True
        BarCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPrint.MasterTemplate.Columns.Add(BarCode)

        Dim ItemCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemCode.FormatString = ""
        ItemCode.HeaderText = "Item Code"
        ItemCode.Name = colItemCode
        ItemCode.Width = 70
        ItemCode.ReadOnly = True
        ItemCode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPrint.MasterTemplate.Columns.Add(ItemCode)

        Dim ItemName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        ItemName.FormatString = ""
        ItemName.HeaderText = "Item Name"
        ItemName.Name = colItemName
        ItemName.Width = 100
        ItemName.ReadOnly = True
        ItemName.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft
        gvPrint.MasterTemplate.Columns.Add(ItemName)

 

        Dim MRP As GridViewDecimalColumn = New GridViewDecimalColumn()
        MRP.FormatString = ""
        MRP.HeaderText = "MRP"
        MRP.Name = colItemMRP
        MRP.Width = 70
        MRP.ReadOnly = False
        MRP.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gvPrint.MasterTemplate.Columns.Add(MRP)

         

        gvPrint.AllowDeleteRow = True
        gvPrint.AllowAddNewRow = False
        gvPrint.ShowGroupPanel = False
        gvPrint.AllowColumnReorder = False
        gvPrint.AllowRowReorder = False
        gvPrint.EnableSorting = False
        gvPrint.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvPrint.MasterTemplate.ShowRowHeaderColumn = False
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If MyBase.isModifyonPasswordFlag Then
            If clsPasswordCheckForMasters.CheckMasterPwd(clsUserMgtCode.frmBarCodeGenerator, clsCommon.myCstr(objCommonVar.CurrentCompanyCode)) Then
            Else
                Return
            End If
        End If
        If chkIntegrate.Checked = True Then
            CreateBarCodeForSAGE()
        Else
            CreateBarCodeForTSPL()
        End If
    End Sub

    Private Sub CreateBarCodeForTSPL()
        Dim ArrBarCode As New List(Of clsBarCodeGenerator)
        Try
            If AllowToSave() Then
                Dim Arr As New List(Of clsBarCodeGenerator)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colBarCode).Value) > 0 Then
                        Dim objTr As New clsBarCodeGenerator()
                        objTr.Bar_Code = clsCommon.myCstr(grow.Cells(colBarCode).Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colItemCost).Value)
                        objTr.Item_Selling_Price = clsCommon.myCdbl(grow.Cells(colSellingPrice).Value)
                        objTr.Item_MRP = clsCommon.myCdbl(grow.Cells(colItemMRP).Value)
                        objTr.Item_color = clsCommon.myCstr(grow.Cells(colColor).Value)
                        objTr.Item_Size = clsCommon.myCstr(grow.Cells(colSize).Value)
                        objTr.Item_Type = clsCommon.myCstr(ddlItem.SelectedValue)
                        Arr.Add(objTr)
                    End If
                Next
                If (clsBarCodeGenerator.SaveData(Arr)) Then
                    common.clsCommon.MyMessageBoxShow(Me, "Data saved Successfully", Me.Text)
                    '    ShowBarCode()

                    '    PrintBarcode()
                    'Else
                    '    ShowBarCode()
                End If
            End If


        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub CreateBarCodeForSAGE()
        Dim ArrBarCode As New List(Of clsBCGonSAGE)
        Try
            If AllowToSave() Then
                Dim Arr As New List(Of clsBCGonSAGE)
                For Each grow As GridViewRowInfo In gv1.Rows
                    If clsCommon.myLen(grow.Cells(colBarCode).Value) > 0 Then
                        Dim objTr As New clsBCGonSAGE()
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells(colItemCode).Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells(colItemCost).Value)
                        objTr.Item_Selling_Price = clsCommon.myCdbl(grow.Cells(colSellingPrice).Value)
                        objTr.Item_MRP = clsCommon.myCdbl(grow.Cells(colItemMRP).Value)
                        objTr.Item_color = clsCommon.myCstr(grow.Cells(colColor).Value)
                        objTr.Item_Size = clsCommon.myCstr(grow.Cells(colSize).Value)
                        objTr.Item_Type = clsCommon.myCstr(ddlItem.SelectedValue)
                        Arr.Add(objTr)
                    End If
                Next
                If (clsBCGonSAGE.SaveData(ddlItem.SelectedValue, Arr)) Then
                    If (common.clsCommon.MyMessageBoxShow("Bar Code Generated Successfully, Do you want to print", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then
                        ShowBarCode()

                        PrintBarcode()
                    Else
                        ShowBarCode()
                    End If
                End If
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        For i As Integer = 0 To gv1.Rows.Count - 1
            If clsCommon.myLen(gv1.Rows(i).Cells(colBarCode).Value) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(i).Cells(colSellingPrice).Value) <= 0 Then
                RadMessageBox.Show("Please Insert Selling Price Against Item Code '" + clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value) + "' At Line No " + clsCommon.myCstr(gv1.Rows(i).Cells(colLineNo).Value) + "")
                Return False
            ElseIf clsCommon.myLen(gv1.Rows(i).Cells(colBarCode).Value) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(i).Cells(colItemMRP).Value) <= 0 Then
                RadMessageBox.Show("Please Insert MRP Against Item Code '" + clsCommon.myCstr(gv1.Rows(i).Cells(colItemCode).Value) + "' At Line No " + clsCommon.myCstr(gv1.Rows(i).Cells(colLineNo).Value) + "")
                Return False
            End If
        Next
        Return True
    End Function

    Private Sub ddlItem_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.Data.PositionChangedEventArgs) Handles ddlItem.SelectedIndexChanged
        If chkIntegrate.Checked = True Then
            LoadDataFromSAGE()
        Else
            LoadDataFromTSPL()
        End If
    End Sub

    Private Sub LoadDataFromTSPL()
        Try
            LoadBlankGrid()
            Dim ArrItem As New List(Of clsItemMaster)
            ddlItem.ValueMember = "Code"
            ddlItem.DisplayMember = "Name"
            ArrItem = clsItemMaster.GetItems(clsCommon.myCstr(ddlItem.SelectedValue))
            If ArrItem.Count > 0 Then
                Dim ii As Integer = 0
                For Each objTr As clsItemMaster In ArrItem
                    ii += 1
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(ii)

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objTr.Item_Desc
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).Value = objTr.Cost
                    gv1.Refresh()
                Next
                gv1.CurrentRow = gv1.Rows(0)
                gv1.Columns(colBarCode).IsVisible = True
                'btnPrintBarCode.Enabled = False
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub LoadDataFromSAGE()
        Try
            LoadBlankGrid()
            Dim dt As DataTable = clsDBFuncationality.GetDataTable("Select ICITEM.ITEMNO as ItemCode, ICITEM.[DESC] as ItemDesc, (Select FNPCOM.dbo.ICITEMO.VALUE  from FNPCOM.dbo.ICITEMO WHERE FNPCOM.dbo.ICITEMO.ITEMNO=FNPCOM.dbo.ICITEM .ITEMNO AND FNPCOM.dbo.ICITEMO.OPTFIELD='COST' ) as Item_Cost, (Select FNPCOM.dbo.ICITEMO.VALUE  from FNPCOM.dbo.ICITEMO WHERE FNPCOM.dbo.ICITEMO.ITEMNO=FNPCOM.dbo.ICITEM .ITEMNO AND FNPCOM.dbo.ICITEMO.OPTFIELD='PRICE' ) as [SellingPrice]   from FNPCOM.dbo.ICITEM ")
            If dt.Rows.Count > 0 Then
                Dim ii As Integer = 0
                For Each dr As DataRow In dt.Rows
                    ii += 1
                    gv1.Rows.AddNew()
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(ii)

                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = dr("ItemCode")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = dr("ItemDesc")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).Value = dr("Item_Cost")
                    gv1.Rows(gv1.Rows.Count - 1).Cells(colItemMRP).Value = dr("SellingPrice")
                Next
                gv1.CurrentRow = gv1.Rows(0)
                gv1.Columns(colBarCode).IsVisible = True
                'btnPrintBarCode.Enabled = False
            End If

        Catch ex As Exception
            RadMessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrintBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintBarCode.Click
        PrintBarcode()
    End Sub

    Private Sub PrintBarcode()
        Dim BarCode As String
        Dim ItemDesc As String
        Dim ItemMRP As Double
        Dim dtBarCode As New DataTable
        dtBarCode.Columns.Add("BarCodeImage", GetType(Byte()))
        dtBarCode.Columns.Add("BarCode", GetType(String))
        dtBarCode.Columns.Add("ItemDesc", GetType(String))
        dtBarCode.Columns.Add("MRP", GetType(Double))
        Dim ii As Integer
        Dim jj As Integer = 0
        For ii = 0 To gvPrint.Rows.Count - 1

            BarCode = clsCommon.myCstr(gvPrint.Rows(ii).Cells(colBarCode).Value)
            ItemDesc = clsCommon.myCstr(gvPrint.Rows(ii).Cells(colItemName).Value)
            ItemMRP = clsCommon.myCdbl(gvPrint.Rows(ii).Cells(colItemMRP).Value)

            Dim bytes() As Byte
            Dim BitmapConverter As System.ComponentModel.TypeConverter = System.ComponentModel.TypeDescriptor.GetConverter(clsCommon.MyBarcodeImage(BarCode, 1, False).[GetType]())
            bytes = DirectCast(BitmapConverter.ConvertTo(clsCommon.MyBarcodeImage(BarCode, 1, False), GetType(Byte())), Byte())
            dtBarCode.Rows.Add()
            dtBarCode.Rows(jj)("BarCodeImage") = bytes
            If chkBarCode.Checked = True Then
                dtBarCode.Rows(jj)("BarCode") = BarCode
            End If
            If chkName.Checked = True Then
                dtBarCode.Rows(jj)("ItemDesc") = ItemDesc
            End If
            If chkMRP.Checked = True Then
                dtBarCode.Rows(jj)("MRP") = ItemMRP
            End If
            jj += 1

        Next

        If dtBarCode.Rows.Count > 0 Then
            Dim frmCRV As New frmCrystalReportViewer()
            frmCRV.funreport(CrystalReportFolder.InventoryReport, dtBarCode, "crptBarCodeImages", "BarCode")
            frmCRV = Nothing
        End If
    End Sub

    Private Sub btnShowBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowBarCode.Click
        ShowBarCode()
    End Sub

    Private Sub ShowBarCode()
        If chkIntegrate.Checked = True Then
            ShowBarCodeFromSAGE()
        Else
            ShowBarCodeFromTSPL()
        End If
    End Sub

    Private Sub ShowBarCodeFromTSPL()
        Dim Arr As List(Of clsBarCodeGenerator)
        Dim ArrBarCode As New List(Of String)
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colBarCode).Value) > 0 Then
                    ArrBarCode.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value))
                End If
            Next
            If ArrBarCode.Count > 0 Then
                Dim ii As Integer = 0
                Arr = clsBarCodeGenerator.GetBarCodes(ArrBarCode)
                LoadBlankGrid()
                If Arr.Count > 0 Then
                    For Each objTr As clsBarCodeGenerator In Arr
                        ii += 1
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(ii)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = objTr.Bar_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSellingPrice).Value = objTr.Item_Selling_Price
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemMRP).Value = objTr.Item_MRP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colColor).Value = objTr.Item_color
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSize).Value = objTr.Item_Size
                    Next
                    gv1.CurrentRow = gv1.Rows(0)
                    gv1.Columns(colBarCode).IsVisible = True
                    'btnPrintBarCode.Enabled = True
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Bar Code Found", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Select Atleast Single Row", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub ShowBarCodeFromSAGE()
        Dim Arr As List(Of clsBCGonSAGE)
        Dim ArrBarCode As New List(Of String)
        Try
            For ii As Integer = 0 To gv1.Rows.Count - 1
                If clsCommon.myLen(gv1.Rows(ii).Cells(colBarCode).Value) > 0 Then
                    ArrBarCode.Add(clsCommon.myCstr(gv1.Rows(ii).Cells(colItemCode).Value))
                End If
            Next
            If ArrBarCode.Count > 0 Then
                Dim ii As Integer = 0
                Arr = clsBCGonSAGE.GetBarCodes(ArrBarCode)
                LoadBlankGrid()
                If Arr.Count > 0 Then
                    For Each objTr As clsBCGonSAGE In Arr
                        ii += 1
                        gv1.Rows.AddNew()
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colLineNo).Value = clsCommon.myCstr(ii)
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colBarCode).Value = objTr.Bar_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCode).Value = objTr.Item_Code
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemName).Value = objTr.Item_Desc
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemCost).Value = objTr.Item_Cost
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSellingPrice).Value = objTr.Item_Selling_Price
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colItemMRP).Value = objTr.Item_MRP
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colColor).Value = objTr.Item_color
                        gv1.Rows(gv1.Rows.Count - 1).Cells(colSize).Value = objTr.Item_Size
                    Next
                    gv1.CurrentRow = gv1.Rows(0)
                    gv1.Columns(colBarCode).IsVisible = True
                    'btnPrintBarCode.Enabled = True
                Else
                    clsCommon.MyMessageBoxShow(Me, "No Bar Code Found", Me.Text)
                End If
            Else
                clsCommon.MyMessageBoxShow(Me, "Please Select Atleast Single Row", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnRefersh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefersh.Click
        If chkIntegrate.Checked = True Then
            LoadDataFromSage()
        Else
            LoadDataFromTSPL()
        End If
    End Sub

    Private Sub chkIntegrate_ToggleStateChanged(ByVal sender As System.Object, ByVal args As Telerik.WinControls.UI.StateChangedEventArgs) Handles chkIntegrate.ToggleStateChanged
        If chkIntegrate.Checked = True Then
            ddlItem.Enabled = False
            LoadDataFromSage()
        Else
            ddlItem.Enabled = True
            LoadDataFromTSPL()
        End If
    End Sub

    Private Sub dgvItem_CurrentRowChanged(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.CurrentRowChangedEventArgs) Handles gv1.CurrentRowChanged
        Try
            If e.CurrentRow.Index >= 0 Then
                Dim qry As String = "select TSPL_ITEM_BARCODE.Bar_Code,TSPL_ITEM_BARCODE.Item_Code,TSPL_ITEM_MASTER.Item_Desc,TSPL_ITEM_BARCODE.Item_MRP "
                qry += " from TSPL_ITEM_BARCODE "
                qry += " LEFT outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_BARCODE.Item_Code"
                qry += " where TSPL_ITEM_BARCODE.Item_Code='" + clsCommon.myCstr(gv1.CurrentRow.Cells(colItemCode).Value) + "'"

                gvDetail.DataSource = Nothing
                gvDetail.Columns.Clear()
                gvDetail.Rows.Clear()
                gvDetail.DataSource = clsDBFuncationality.GetDataTable(qry)
                gvDetail.AllowAddNewRow = False
                gvDetail.AllowDragToGroup = False
                For ii As Integer = 0 To gvDetail.Columns.Count - 1
                    gvDetail.Columns(ii).ReadOnly = True
                    gvDetail.Columns(ii).IsVisible = False
                Next


                gvDetail.Columns("Bar_Code").IsVisible = True
                gvDetail.Columns("Bar_Code").Width = 150
                gvDetail.Columns("Bar_Code").HeaderText = "Bar Code"

                gvDetail.Columns("Item_Code").IsVisible = True
                gvDetail.Columns("Item_Code").Width = 150
                gvDetail.Columns("Item_Code").HeaderText = "Item Code"

                gvDetail.Columns("Item_Desc").IsVisible = True
                gvDetail.Columns("Item_Desc").Width = 250
                gvDetail.Columns("Item_Desc").HeaderText = "Item Description"

                gvDetail.Columns("Item_MRP").IsVisible = True
                gvDetail.Columns("Item_MRP").Width = 100
                gvDetail.Columns("Item_MRP").HeaderText = "MRP"
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub RadButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadButton1.Click
        LoadBlankGridPrint()
    End Sub

    Private Sub gvDetail_CellDoubleClick(ByVal sender As System.Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvDetail.CellDoubleClick
        If clsCommon.myLen(gvDetail.CurrentRow.Cells("Bar_Code").Value) > 0 Then
            gvPrint.Rows.AddNew()
            gvPrint.Rows(gvPrint.RowCount - 1).Cells(colBarCode).Value = clsCommon.myCstr(gvDetail.CurrentRow.Cells("Bar_Code").Value)
            gvPrint.Rows(gvPrint.RowCount - 1).Cells(colItemCode).Value = clsCommon.myCstr(gvDetail.CurrentRow.Cells("Item_Code").Value)
            gvPrint.Rows(gvPrint.RowCount - 1).Cells(colItemName).Value = clsCommon.myCstr(gvDetail.CurrentRow.Cells("Item_Desc").Value)
            gvPrint.Rows(gvPrint.RowCount - 1).Cells(colItemMRP).Value = clsCommon.myCstr(gvDetail.CurrentRow.Cells("Item_MRP").Value)
        End If
    End Sub

    Private Sub RadMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem1.Click
        Dim str As String
        str = "select Bar_Code,Item_Code,Item_Cost,Item_MRP,Item_Selling_Price from TSPL_ITEM_BARCODE"
        transportSql.ExporttoExcel(str, Me)
    End Sub

    Private Sub RadMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadMenuItem2.Click
        Dim gv As New RadGridView()
        Me.Controls.Add(gv)
        Dim currentdate As Date = Date.Today
        If transportSql.importExcel(gv, "Bar_Code", "Item_Code", "Item_Cost", "Item_MRP", "Item_Selling_Price") Then
            Try
                clsCommon.ProgressBarShow()
                Dim Arr As New List(Of clsBarCodeGenerator)
                For Each grow As GridViewRowInfo In gv.Rows
                    If clsCommon.myLen(grow.Cells("Bar_Code").Value) > 0 Then
                        Dim objTr As New clsBarCodeGenerator()
                        objTr.Bar_Code = clsCommon.myCstr(grow.Cells("Bar_Code").Value)
                        objTr.Item_Code = clsCommon.myCstr(grow.Cells("Item_Code").Value)
                        objTr.Item_Cost = clsCommon.myCdbl(grow.Cells("Item_Cost").Value)
                        objTr.Item_Selling_Price = clsCommon.myCdbl(grow.Cells("Item_Selling_Price").Value)
                        objTr.Item_MRP = clsCommon.myCdbl(grow.Cells("Item_MRP").Value)
                        'objTr.Item_color = clsCommon.myCstr(grow.Cells(colColor).Value)
                        'objTr.Item_Size = clsCommon.myCstr(grow.Cells(colSize).Value)
                        If clsCommon.myLen(objTr.Item_Code) <= 0 Then
                            Throw New Exception("Item code can't be left blank")
                        End If
                        objTr.Item_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Code from TSPL_ITEM_MASTER where Item_Code='" + objTr.Item_Code + "'"))
                        If clsCommon.myLen(objTr.Item_Code) <= 0 Then
                            Throw New Exception("This  '" + clsCommon.myCstr(grow.Cells("Item_Code").Value) + "'  item does not exist")
                        End If
                        objTr.Item_Type = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Item_Type from TSPL_ITEM_MASTER where Item_Code='" + objTr.Item_Code + "'"))
                        Arr.Add(objTr)
                    End If
                Next

                If (clsBarCodeGenerator.SaveData(Arr)) Then
                    clsCommon.ProgressBarHide()
                    common.clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed", Me.Text)
                End If

            Catch ex As Exception
                clsCommon.ProgressBarHide()
                myMessages.myExceptions(ex)
            Finally
                clsCommon.ProgressBarHide()
            End Try

        End If
        Me.Controls.Remove(gv)
    End Sub

    

    Private Sub gvPrint_UserDeletingRow(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewRowCancelEventArgs) Handles gvPrint.UserDeletingRow
        If common.clsCommon.MyMessageBoxShow("Delete The Current Row." + Environment.NewLine + "Are you sure?", Me.Text, MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.No Then
            e.Cancel = True
        End If
    End Sub
End Class

