Imports common
Imports System.Data.SqlClient


Public Class FrmMRPBasedPO
    Inherits FrmMainTranScreen

#Region "variables"
    Public _MRPNO As String = Nothing
    Dim ButtonToolTip As New ToolTip()
    Dim isinsideLoaddate As Boolean = False
    Dim isCellvaluechanged As Boolean = False

    Const colSelect As String = "Select"
    Const colLineNo As String = "lineno"
    Const colItem_Code As String = "colItem_Code"
    Const colItem_Desc As String = "colItem_Desc"
    Const colUNIT_CODE As String = "UNIT_CODE"
    Const colStockQty As String = "StockQty"
    Const colnetreqqty As String = "netreqqty"
    Const colremarks As String = "remarks"
    Const colVendorcode As String = "vendorcode"
    Const colOpenPO As String = "colOpenPO"
    Const colAutoPO As String = "colAutoPO"
    Const colAutoSchedule As String = "colAutoSchedule"
    Const colOpenPO_No As String = "colOpenPO_No"

    Const colVLineNo As String = "VLineNo"
    Const colVVendorCode As String = "VendrCode"
    Const colVVendorName As String = "Vendorname"
    Const colVItemCode As String = "VItemCode"
    Const colVItemName As String = "VItemName"
    Const colVUnit As String = "VUnit"
    Const ColVNetQty As String = "NetQty"
    Const colVPORate As String = "VPORate"
    Const colVPOLastRate As String = "VLastRate"
    Const colVPOAvgRate As String = "VAvgRate"
    Const colVOrderRate As String = "VUnitRate"
    Const ColVOrderQty As String = "VQuantity"
    Const colVRemarks As String = "VRemarks"
    Const colPONO As String = "colPONO"
    Const colPODate As String = "colPODate"
    Const ColMOQ As String = "ColMOQ"
    Const ColSPQ As String = "ColSPQ"
    Const ColSOB As String = "ColSOB"
    Const ColSOBQty As String = "ColSOBQty"
    Const ColActualQty As String = "ColActualQty"
    Const ColScheduleType As String = "ColScheduleType"
#End Region

    Private Sub FrmMRPBasedPO_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt AndAlso e.KeyCode = Keys.S Then
            btnsave.PerformClick()
        ElseIf e.Alt AndAlso e.KeyCode = Keys.C Then
            clsERPFuncationality.closeForm(Me)
        End If
    End Sub

    Private Sub FrmMRPBasedPO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtMRPNo.Enabled = False
        txtLocationcode.Enabled = False
        dtpfromdate.Enabled = False
        dtptodate.Enabled = False

        btnAddNew.Visible = False
        LoadBlankGrid()
        LoadPOBlankGrid()
        LoadMRPDetail()

        ButtonToolTip.SetToolTip(btnsave, "Press Alt+S for save/update")
        ButtonToolTip.SetToolTip(btnclose, "Press Alt+C for close window")
    End Sub

    Private Sub LoadBlankGrid()
        gvMRP.Columns.Clear()
        gvMRP.Rows.Clear()

        Dim item_code As New GridViewTextBoxColumn()
        Dim status As New GridViewCheckBoxColumn()

        status = New GridViewCheckBoxColumn()
        status.HeaderText = "Select"
        status.Name = colSelect
        status.Width = 60
        status.FormatString = ""
        gvMRP.MasterTemplate.Columns.Add(status)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 70
        item_code.Name = colLineNo
        item_code.HeaderText = "Line No"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 100
        item_code.Name = colItem_Code
        item_code.HeaderText = "Item Code"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 220
        item_code.Name = colItem_Desc
        item_code.HeaderText = "Description"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colUNIT_CODE
        item_code.HeaderText = "Description"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colStockQty
        item_code.HeaderText = "Stock Qty"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colnetreqqty
        item_code.HeaderText = "Required Qty"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 150
        item_code.Name = colremarks
        item_code.HeaderText = "Remarks"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colVendorcode
        item_code.HeaderText = "New Vendor(s)"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colOpenPO
        item_code.HeaderText = "Open PO"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colAutoPO
        item_code.HeaderText = "Auto PO"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colAutoSchedule
        item_code.HeaderText = "Auto Schedule"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colOpenPO_No
        item_code.HeaderText = "Open PO NO"
        item_code.ReadOnly = True
        gvMRP.MasterTemplate.Columns.Add(item_code)

        gvMRP.AllowDeleteRow = True
        gvMRP.AllowAddNewRow = False
        gvMRP.ShowGroupPanel = False
        gvMRP.AllowColumnReorder = True
        gvMRP.AllowRowReorder = False
        gvMRP.EnableSorting = False
        gvMRP.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gvMRP.MasterTemplate.ShowRowHeaderColumn = False
        gvMRP.Rows.AddNew()

        item_code = Nothing
        status = Nothing
    End Sub

    Private Sub LoadPOBlankGrid()
        gv_PO.Columns.Clear()
        gv_PO.Rows.Clear()

        Dim item_code As New GridViewTextBoxColumn()
        Dim reporate As New GridViewDecimalColumn()

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 70
        item_code.Name = colVLineNo
        item_code.HeaderText = "Line No"
        item_code.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 100
        item_code.Name = colVVendorCode
        item_code.HeaderText = "Vendor Code"
        item_code.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 150
        item_code.Name = colVVendorName
        item_code.HeaderText = "Vendor Name"
        item_code.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 100
        item_code.Name = colVItemCode
        item_code.HeaderText = "Item Code"
        item_code.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 220
        item_code.Name = colVItemName
        item_code.HeaderText = "Description"
        item_code.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colVUnit
        item_code.HeaderText = "Unit"
        item_code.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = ColVNetQty
        item_code.HeaderText = "Required Qty"
        item_code.ReadOnly = True
        item_code.IsVisible = False
        gv_PO.MasterTemplate.Columns.Add(item_code)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = colVPORate
        reporate.HeaderText = "Open PO Rate"
        reporate.DecimalPlaces = 2
        reporate.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(reporate)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = colVPOLastRate
        reporate.HeaderText = "Last PO Rate"
        reporate.DecimalPlaces = 2
        reporate.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(reporate)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = colVPOAvgRate
        reporate.HeaderText = "Last Avg Rate"
        reporate.DecimalPlaces = 2
        reporate.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(reporate)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = colVOrderRate
        reporate.HeaderText = "Unit Rate"
        reporate.DecimalPlaces = 2
        gv_PO.MasterTemplate.Columns.Add(reporate)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = ColVOrderQty
        reporate.HeaderText = "Order Quantity"
        reporate.DecimalPlaces = 2
        gv_PO.MasterTemplate.Columns.Add(reporate)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 150
        item_code.Name = colVRemarks
        item_code.HeaderText = "Remarks"
        item_code.ReadOnly = True
        item_code.MaxLength = 100
        gv_PO.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colPONO
        item_code.HeaderText = "Open PO No"
        item_code.ReadOnly = True
        item_code.MaxLength = 100
        gv_PO.MasterTemplate.Columns.Add(item_code)

        item_code = New GridViewTextBoxColumn()
        item_code.FormatString = ""
        item_code.Width = 80
        item_code.Name = colPODate
        item_code.HeaderText = "Open PO Date"
        item_code.ReadOnly = True
        item_code.MaxLength = 100
        gv_PO.MasterTemplate.Columns.Add(item_code)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = ColMOQ
        reporate.HeaderText = "MOQ"
        reporate.DecimalPlaces = 2
        reporate.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(reporate)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = ColSPQ
        reporate.HeaderText = "SPQ"
        reporate.DecimalPlaces = 2
        reporate.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(reporate)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = ColSOB
        reporate.HeaderText = "SOB"
        reporate.DecimalPlaces = 2
        reporate.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(reporate)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = ColSOBQty
        reporate.HeaderText = "SOB Qty"
        reporate.DecimalPlaces = 2
        reporate.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(reporate)

        reporate = New GridViewDecimalColumn()
        reporate.FormatString = ""
        reporate.Width = 80
        reporate.Name = ColActualQty
        reporate.HeaderText = "Actual Qty"
        reporate.DecimalPlaces = 2
        reporate.ReadOnly = True
        gv_PO.MasterTemplate.Columns.Add(reporate)

        Dim ddl As GridViewComboBoxColumn
        ddl = New GridViewComboBoxColumn
        
        ddl.DataSource = GetDT()
        ddl.ValueMember = "Code"
        ddl.DisplayMember = "Code"
        ddl.FormatString = ""
        ddl.Width = 100
        ddl.Name = ColScheduleType
        ddl.HeaderText = "Schedule Type"
        ddl.ReadOnly = False

        gv_PO.MasterTemplate.Columns.Add(ddl)

        gv_PO.AllowDeleteRow = True
        gv_PO.AllowAddNewRow = False
        gv_PO.ShowGroupPanel = False
        gv_PO.AllowColumnReorder = True
        gv_PO.AllowRowReorder = False
        gv_PO.EnableSorting = False
        gv_PO.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv_PO.MasterTemplate.ShowRowHeaderColumn = False
        gv_PO.Rows.AddNew()

        item_code = Nothing
        reporate = Nothing
    End Sub
    Function GetDT() As DataTable
        Dim qry As String = "select 'Monthly' as Code union select 'Weekly' as Code union  select 'Daily' as Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        Return dt
    End Function

    Private Sub LoadMRPDetail()
        Try
            gvMRP.Rows.Clear()
            If clsCommon.myLen(_MRPNO) > 0 Then
                Dim obj As clsMRPAutoMobile = clsMRPAutoMobile.GetData(_MRPNO, NavigatorType.Current)

                If obj IsNot Nothing AndAlso clsCommon.myLen(obj.MRP_CODE) > 0 Then
                    isinsideLoaddate = True
                    txtMRPNo.Value = obj.MRP_CODE
                    txtMrpDesc.Text = obj.MRP_DESCRIPTION
                    txtLocationcode.Value = obj.MRP_Location
                    txtLocationdesc.Text = clsLocation.GetName(obj.MRP_Location, Nothing)
                    dtpfromdate.Text = obj.MRP_FROM
                    dtptodate.Text = obj.MRP_TO

                    If obj.ObjListMRPDetail IsNot Nothing AndAlso obj.ObjListMRPDetail.Count > 0 Then
                        For Each objtr As clsMRPAutoMobileDetail In obj.ObjListMRPDetail
                            gvMRP.Rows.AddNew()

                            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colSelect).Value = False
                            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colLineNo).Value = objtr.Line_No
                            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colItem_Code).Value = objtr.Item_Code
                            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colItem_Desc).Value = objtr.Item_Desc
                            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colStockQty).Value = clsProductionPlanning.GetItemBalance(obj.MRP_Location, objtr.Item_Code, objtr.RM_UNIT_CODE)
                            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colnetreqqty).Value = objtr.Net_Requird_Qty
                            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colUNIT_CODE).Value = objtr.RM_UNIT_CODE
                            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colremarks).Value = objtr.Remarks

                            If chkConsiderOpenPO.Checked Then
                                Dim PO_No As String = clsMRPAutoMobile.GetOpenPO(objtr.Item_Code)
                                If clsCommon.myLen(PO_No) > 0 Then
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colOpenPO).Value = "Y"
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colOpenPO_No).Value = PO_No
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colOpenPO).ReadOnly = True

                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).Value = "N"
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).ReadOnly = True

                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoSchedule).Value = "Y"
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoSchedule).ReadOnly = True
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colVendorcode).ReadOnly = True
                                Else
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colOpenPO).Value = "N"
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colOpenPO).ReadOnly = True

                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoSchedule).Value = "N"
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoSchedule).ReadOnly = True

                                    If chkAutoIndent.IsChecked OrElse chkAutoPO.IsChecked Then
                                        gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).Value = "Y"
                                        gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).ReadOnly = True
                                    Else
                                        gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).Value = "N"
                                        gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).ReadOnly = True
                                    End If
                                End If
                            Else
                                gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colOpenPO).Value = "N"
                                gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colOpenPO).ReadOnly = True

                                gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoSchedule).Value = "N"
                                gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoSchedule).ReadOnly = True

                                If chkAutoIndent.IsChecked OrElse chkAutoPO.IsChecked Then
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).Value = "Y"
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).ReadOnly = True
                                Else
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).Value = "N"
                                    gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colAutoPO).ReadOnly = True
                                End If
                            End If
                           
                            ''=====get child of item is exist in bom===
                            'Dim bomcode As String = clsMRPAutoMobilePODetail.GetBOMOtherItems(objtr.Item_Code)
                            'While (clsCommon.myLen(bomcode) > 0)
                            '    Dim item_code As String = ""

                            '    Dim qry As String = "select consm_item_code,sum(isnull(consm_quantity,0)) as consm_quantity,consm_item_unit_code from TSPL_MF_BOM_DETAIL where 1=1 and "
                            '    qry += " bom_code in (" + bomcode + ") group by consm_item_code,consm_item_unit_code"
                            '    Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
                            '    If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            '        For Each dr As DataRow In dt.Rows
                            '            gvMRP.Rows.AddNew()

                            '            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colSelect).Value = False
                            '            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colLineNo).Value = objtr.Line_No
                            '            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colItem_Code).Value = clsCommon.myCstr(dr("consm_item_code"))
                            '            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colItem_Desc).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr("consm_item_code")), Nothing)
                            '            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colStockQty).Value = clsProductionPlanning.GetItemBalance(obj.MRP_Location, clsCommon.myCstr(dr("consm_item_code")), clsCommon.myCstr(dr("consm_item_unit_code")))
                            '            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colnetreqqty).Value = clsCommon.myCdbl(dr("consm_quantity"))
                            '            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colUNIT_CODE).Value = clsCommon.myCstr(dr("consm_item_unit_code"))
                            '            gvMRP.Rows(gvMRP.Rows.Count - 1).Cells(colremarks).Value = Nothing

                            '            item_code = item_code + "','" + clsCommon.myCstr(dr("consm_item_code"))
                            '        Next
                            '    End If

                            '    bomcode = clsMRPAutoMobilePODetail.GetBOMOtherItems(item_code)
                            'End While
                            ''=======================================================
                        Next
                    End If
                End If

                ResetLineno()
                isinsideLoaddate = False
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub ResetLineno()
        For Each grow As GridViewRowInfo In gvMRP.Rows
            grow.Cells(colLineNo).Value = grow.Index + 1
            grow.Cells(colVendorcode).Value = "Double Click"
        Next
    End Sub

    Private Sub btnAddNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddNew.Click

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Try
            If AllowToSave() Then
                Dim obj As New clsMRPAutoMobilePODetail()
                obj.Arr = New List(Of clsMRPAutoMobilePODetail)

                For Each grow As GridViewRowInfo In gv_PO.Rows
                    Dim objtr As New clsMRPAutoMobilePODetail()

                    objtr.MRP_CODE = clsCommon.myCstr(txtMRPNo.Value)
                    objtr.Bill_To_Location = clsCommon.myCstr(txtLocationcode.Value)
                    objtr.Vendor_Code = clsCommon.myCstr(grow.Cells(colVVendorCode).Value)
                    objtr.Vendor_Name = clsCommon.myCstr(grow.Cells(colVVendorName).Value)
                    objtr.Item_Code = clsCommon.myCstr(grow.Cells(colVItemCode).Value)
                    objtr.Unit_Code = clsCommon.myCstr(grow.Cells(colVUnit).Value)
                    objtr.Net_Req_Qty = clsCommon.myCdbl(grow.Cells(ColVNetQty).Value)
                    objtr.MRP_PO_Rate = clsCommon.myCdbl(grow.Cells(colVPORate).Value)
                    objtr.PO_Last_Rate = clsCommon.myCdbl(grow.Cells(colVPOLastRate).Value)
                    objtr.PO_Avg_Rate = clsCommon.myCdbl(grow.Cells(colVPOAvgRate).Value)
                    objtr.Item_Cost = clsCommon.myCdbl(grow.Cells(colVOrderRate).Value)
                    objtr.Qty = clsCommon.myCdbl(grow.Cells(ColVOrderQty).Value)
                    objtr.Remarks = clsCommon.myCstr(grow.Cells(colVRemarks).Value).Replace("'", "`")
                    If clsCommon.myLen(objtr.Remarks) > 100 Then
                        objtr.Remarks = objtr.Remarks.ToString().Substring(0, 100)
                    End If
                    '' open po columns
                    objtr.OpenPONO = clsCommon.myCstr(grow.Cells(colPONO).Value)
                    objtr.OpenPODate = clsCommon.myCstr(grow.Cells(colPODate).Value)
                    objtr.MOQ = clsCommon.myCdbl(grow.Cells(ColMOQ).Value)
                    objtr.SPQ = clsCommon.myCdbl(grow.Cells(ColSPQ).Value)
                    objtr.SOB = clsCommon.myCdbl(grow.Cells(ColSOB).Value)
                    objtr.SOBQty = clsCommon.myCdbl(grow.Cells(ColSOBQty).Value)
                    objtr.ActualQty = clsCommon.myCdbl(grow.Cells(ColActualQty).Value)
                    objtr.ScheduleType = clsCommon.myCstr(grow.Cells(ColScheduleType).Value)
                    If clsCommon.myLen(objtr.Vendor_Code) > 0 AndAlso objtr.Qty > 0 Then
                        obj.Arr.Add(objtr)
                    End If
                Next

                If clsMRPAutoMobilePODetail.SaveData(obj, clsCommon.myCstr(txtMRPNo.Value)) Then
                    myMessages.post()

                    clsERPFuncationality.closeForm(Me)
                End If

                obj = Nothing
            End If
            
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        Finally
            'clsERPFuncationality.closeForm(Me)
        End Try
    End Sub

    Private Function AllowToSave() As Boolean
        Try
            Dim oldqty As Decimal = 0
            For Each grow As GridViewRowInfo In gv_PO.Rows
                oldqty = 0
                Dim icode As String = clsCommon.myCstr(grow.Cells(colVItemCode).Value)
                Dim rate As Decimal = clsCommon.myCdbl(grow.Cells(colVOrderRate).Value)
                Dim qty As Decimal = clsCommon.myCdbl(grow.Cells(ColVOrderQty).Value)
                Dim netqty As Decimal = clsCommon.myCdbl(grow.Cells(ColVNetQty).Value)

                If grow.Index = 0 AndAlso clsCommon.myLen(icode) <= 0 Then
                    Throw New Exception("Fill atleast one row in grid for auto PO.")
                End If

                If clsCommon.myLen(icode) > 0 Then
                    If rate <= 0 Then
                        Throw New Exception("fill order rate at row no. " + clsCommon.myCstr(grow.Index + 1) + "")
                        gv_PO.CurrentRow = gv_PO.Rows(grow.Index)
                    End If

                    Dim upto As Integer = 0
                    oldqty += qty
                    For ii As Integer = grow.Index + 1 To gv_PO.Rows.Count - 1
                        Dim oldicode As String = clsCommon.myCstr(gv_PO.Rows(ii).Cells(colVItemCode).Value)

                        If clsCommon.CompairString(icode, oldicode) = CompairStringResult.Equal Then
                            oldqty += clsCommon.myCdbl(gv_PO.Rows(ii).Cells(ColVOrderQty).Value)
                            upto = ii
                        End If
                    Next
                    If clsCommon.myLen(grow.Cells(colPONO).Value) <= 0 Then
                        If oldqty > netqty Then
                            Throw New Exception("sum of order quantity of item: " + clsCommon.myCstr(grow.Cells(colVItemName).Value) + " should not exceed " + clsCommon.myCstr(netqty) + " see from row no. " + clsCommon.myCstr(grow.Index + 1) + " to " + clsCommon.myCstr(upto + 1) + "")
                        End If
                    End If
                    
                End If
            Next

            Return True
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Function

    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        clsERPFuncationality.closeForm(Me)
    End Sub

    Private Sub gvMRP_CellDoubleClick(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.GridViewCellEventArgs) Handles gvMRP.CellDoubleClick
        If e.ColumnIndex >= 0 Then
            If clsCommon.CompairString(gvMRP.Rows(e.RowIndex).Cells(colOpenPO).Value, "Y") = CompairStringResult.Equal Then
                Exit Sub
            End If
            If clsCommon.CompairString(gvMRP.Rows(e.RowIndex).Cells(colAutoPO).Value, "N") = CompairStringResult.Equal Then
                Exit Sub
            End If
            If e.Column Is gvMRP.Columns(colVendorcode) Then
                If clsCommon.myCBool(gvMRP.CurrentRow.Cells(colSelect).Value) = False Then
                    clsCommon.MyMessageBoxShow("First select the item by checking.")
                    Exit Sub
                End If

                Dim frm As New FrmCheckBoxGrid()
                frm.qry = "select vendor_code+' '+vendor_name as Value from tspl_vendor_master where Status='N'"
                frm.ShowDialog()

                If frm.arrValue IsNot Nothing AndAlso frm.arrValue.Count > 0 Then
                    For ii As Integer = gv_PO.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gvMRP.CurrentRow.Cells(colItem_Code).Value), clsCommon.myCstr(gv_PO.Rows(ii).Cells(colVItemCode).Value)) = CompairStringResult.Equal Then
                            gv_PO.Rows.RemoveAt(ii)
                        End If
                    Next

                    LoadVendorGrid(True, clsCommon.myCstr(gvMRP.CurrentRow.Cells(colItem_Code).Value), gvMRP.CurrentRow.Index)

                    For Each Str As String In frm.arrValue
                        If gv_PO.Rows.Count = 0 Then
                            gv_PO.Rows.AddNew()
                        End If
                        Dim vendrcode As String = Str.Substring(0, Str.IndexOf(" "))

                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVLineNo).Value = gv_PO.Rows.Count
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVVendorCode).Value = vendrcode
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVVendorName).Value = clsVendorMaster.GetName(vendrcode, Nothing)
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVItemCode).Value = clsCommon.myCstr(gvMRP.CurrentRow.Cells(colItem_Code).Value)
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVItemName).Value = clsItemMaster.GetItemName(clsCommon.myCstr(gvMRP.CurrentRow.Cells(colItem_Code).Value), Nothing)
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVUnit).Value = clsCommon.myCstr(gvMRP.CurrentRow.Cells(colUNIT_CODE).Value)
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColVNetQty).Value = clsCommon.myCdbl(gvMRP.CurrentRow.Cells(colnetreqqty).Value)
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVPORate).Value = 0
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVPOLastRate).Value = 0
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVPOAvgRate).Value = 0
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVOrderRate).Value = 0
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColVOrderQty).Value = clsCommon.myCdbl(gvMRP.CurrentRow.Cells(colnetreqqty).Value)
                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVRemarks).Value = Nothing
                        gv_PO.Rows.AddNew()
                    Next

                Else
                    For ii As Integer = gv_PO.Rows.Count - 1 To 0 Step -1
                        If clsCommon.CompairString(clsCommon.myCstr(gvMRP.CurrentRow.Cells(colItem_Code).Value), clsCommon.myCstr(gv_PO.Rows(ii).Cells(colVItemCode).Value)) = CompairStringResult.Equal Then
                            gv_PO.Rows.RemoveAt(ii)
                        End If
                    Next
                    gvMRP.CurrentRow.Cells(colSelect).Value = False
                End If
            End If
        End If
    End Sub

    Private Sub gvMRP_ValueChanging(ByVal sender As Object, ByVal e As Telerik.WinControls.UI.ValueChangingEventArgs) Handles gvMRP.ValueChanging
        Try
            If Not isinsideLoaddate Then
                If Not isCellvaluechanged Then
                    LoadVendorGrid(e.NewValue, clsCommon.myCstr(gvMRP.CurrentRow.Cells(colItem_Code).Value), gvMRP.CurrentRow.Index)
                End If
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(ex.Message)
        End Try
    End Sub

    Private Sub LoadVendorGrid(ByVal isLoad As Boolean, ByVal ItemCode As String, ByVal Introw As Integer)
        Try
            If isLoad Then
                Dim obj As clsMRPAutoMobile = clsMRPAutoMobile.GetAutoPODetail(ItemCode)
                Dim orderqty As Decimal = clsCommon.myCdbl(gvMRP.Rows(Introw).Cells(colnetreqqty).Value)
                Dim modevalue As Decimal = Nothing
                Dim qty As Decimal = Nothing

                If gv_PO.Rows.Count = 0 Then
                    gv_PO.Rows.AddNew()
                End If

                If obj IsNot Nothing AndAlso obj.Arr_Auto_Po.Count > 0 Then
                    modevalue = orderqty Mod obj.Arr_Auto_Po.Count
                    qty = Math.Round((orderqty - modevalue) / obj.Arr_Auto_Po.Count, 2)
                    If clsCommon.CompairString(gvMRP.Rows(Introw).Cells(colOpenPO).Value, "N") = CompairStringResult.Equal Then
                        For Each objtr As clsMRPAutoMobile In obj.Arr_Auto_Po
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVLineNo).Value = gv_PO.Rows.Count
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVVendorCode).Value = objtr.AAuto_Vendor_Code
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVVendorName).Value = objtr.AAuto_Vendor_Name
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVItemCode).Value = ItemCode
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVItemName).Value = clsItemMaster.GetItemName(ItemCode, Nothing)
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVUnit).Value = clsCommon.myCstr(gvMRP.Rows(Introw).Cells(colUNIT_CODE).Value)
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColVNetQty).Value = clsCommon.myCdbl(gvMRP.Rows(Introw).Cells(colnetreqqty).Value)
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVPORate).Value = objtr.AAuto_PO_Rate
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVPOLastRate).Value = objtr.AAuto_Last_Rate
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVPOAvgRate).Value = objtr.AAuto_Avg_Rate
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVOrderRate).Value = objtr.AAuto_PO_Rate
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColVOrderQty).Value = qty
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColActualQty).Value = qty
                            gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVRemarks).Value = Nothing
                            gv_PO.Rows.AddNew()
                        Next
                        gv_PO.Rows(gv_PO.Rows.Count - 2).Cells(ColVOrderQty).Value = qty + modevalue
                    Else
                        '' get open PO detail
                        Dim arrPO() As String = gvMRP.Rows(Introw).Cells(colOpenPO_No).Value.ToString.Split(";")
                        For Each strPO As String In arrPO
                            Dim objPOItem As clsMRPOpenPOItemDetail = clsMRPOpenPOItemDetail.GetOpenPODetail(strPO, gvMRP.Rows(Introw).Cells(colItem_Code).Value)
                            If Not objPOItem Is Nothing Then
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVLineNo).Value = gv_PO.Rows.Count
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVVendorCode).Value = objPOItem.Vendor_Code
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVVendorName).Value = objPOItem.Vendor_Name
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVItemCode).Value = objPOItem.Item_Code
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVItemName).Value = objPOItem.Item_Desc
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVUnit).Value = objPOItem.Unit_Code

                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVPORate).Value = objPOItem.Unit_Rate
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVPOLastRate).Value = 0
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVPOAvgRate).Value = 0
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVOrderRate).Value = objPOItem.Vendor_Unit_Rate
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColVOrderQty).Value = orderqty
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColVNetQty).Value = clsCommon.myCdbl(gvMRP.Rows(Introw).Cells(colnetreqqty).Value)
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colVRemarks).Value = Nothing
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colPONO).Value = strPO
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(colPODate).Value = objPOItem.OpenPODate
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColMOQ).Value = objPOItem.MOQ
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColSPQ).Value = objPOItem.SPQ
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColSOB).Value = objPOItem.SOB
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColSOBQty).Value = objPOItem.SOB * gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColVNetQty).Value / 100
                                If gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColVNetQty).Value <= objPOItem.MOQ Then
                                    gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColActualQty).Value = objPOItem.MOQ
                                Else
                                    If objPOItem.MOQ > 1 Then
                                        Dim TempQty As Decimal = objPOItem.MOQ
                                        Dim intloop As Integer = 1

                                        While objPOItem.MOQ * intloop < gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColSOBQty).Value
                                            intloop = intloop + 1
                                        End While
                                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColActualQty).Value = objPOItem.MOQ * intloop
                                    Else
                                        gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColActualQty).Value = clsCommon.myCdbl(gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColSOBQty).Value)
                                    End If

                                End If
                                gv_PO.Rows(gv_PO.Rows.Count - 1).Cells(ColScheduleType).Value = "Weekly"
                                gv_PO.Rows.AddNew()
                            End If
                        Next
                                                
                    End If

                End If
            Else
                For ii As Integer = gv_PO.Rows.Count - 1 To 0 Step -1
                    If clsCommon.CompairString(ItemCode, clsCommon.myCstr(gv_PO.Rows(ii).Cells(colVItemCode).Value)) = CompairStringResult.Equal Then
                        gv_PO.Rows.RemoveAt(ii)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
End Class
