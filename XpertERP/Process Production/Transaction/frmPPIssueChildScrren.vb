Imports common
Imports System.Data.SqlClient
Imports System.IO

Public Class FrmPPIssueChildScrren
    Inherits FrmMainTranScreen

#Region "Variables"
    Const colSelect As String = "Select"
    Const colLoccode As String = "LocCode"
    Const colLocName As String = "LocName"
    Const colICode As String = "Icode"
    Const colIname As String = "Iname"
    Const colUnit As String = "Unit"
    Const colQty As String = "qty"
    Const colFatkg As String = "fatkg"
    Const colsnfkg As String = "snfkg"

    Dim ReportID As String = "CHILDPPISSUE"
    Dim ButtonToolTip As New ToolTip()

    Public Arr_Loc As List(Of String) = Nothing
    Public CheckStockServerDate As Boolean = True
#End Region

    Sub LoadBlankGrid()
        gv.Rows.Clear()
        gv.Columns.Clear()

        Dim repoSelect As GridViewCheckBoxColumn = New GridViewCheckBoxColumn()
        repoSelect.FormatString = ""
        repoSelect.Name = colSelect
        repoSelect.HeaderText = "Select"
        repoSelect.Width = 60
        gv.MasterTemplate.Columns.Add(repoSelect)

        Dim repoSelect1 As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoSelect1.FormatString = ""
        repoSelect1.Name = colLoccode
        repoSelect1.HeaderText = "Location Code"
        repoSelect1.Width = 100
        repoSelect1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSelect1)

        repoSelect1 = New GridViewTextBoxColumn()
        repoSelect1.FormatString = ""
        repoSelect1.Name = colLocName
        repoSelect1.HeaderText = "Location"
        repoSelect1.Width = 150
        repoSelect1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSelect1)

        repoSelect1 = New GridViewTextBoxColumn()
        repoSelect1.FormatString = ""
        repoSelect1.Name = colICode
        repoSelect1.HeaderText = "Item Code"
        repoSelect1.Width = 100
        repoSelect1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSelect1)

        repoSelect1 = New GridViewTextBoxColumn()
        repoSelect1.FormatString = ""
        repoSelect1.Name = colIname
        repoSelect1.HeaderText = "Description"
        repoSelect1.Width = 180
        repoSelect1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSelect1)

        repoSelect1 = New GridViewTextBoxColumn()
        repoSelect1.FormatString = ""
        repoSelect1.Name = colUnit
        repoSelect1.HeaderText = "UOM"
        repoSelect1.Width = 80
        repoSelect1.ReadOnly = True
        gv.MasterTemplate.Columns.Add(repoSelect1)

        Dim repoqty As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.Name = colQty
        repoqty.HeaderText = "Stock Quantity"
        repoqty.DecimalPlaces = 3
        repoqty.ReadOnly = True
        repoqty.Width = 80
        gv.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.Name = colFatkg
        repoqty.HeaderText = "FAT Kg"
        repoqty.DecimalPlaces = 3
        repoqty.ReadOnly = True
        repoqty.Width = 80
        gv.MasterTemplate.Columns.Add(repoqty)

        repoqty = New GridViewDecimalColumn()
        repoqty.FormatString = ""
        repoqty.Name = colsnfkg
        repoqty.HeaderText = "SNF Kg"
        repoqty.DecimalPlaces = 3
        repoqty.ReadOnly = True
        repoqty.Width = 80
        gv.MasterTemplate.Columns.Add(repoqty)

        gv.AllowDeleteRow = False
        gv.AllowAddNewRow = False
        gv.ShowGroupPanel = False
        gv.AllowColumnReorder = True
        gv.AllowRowReorder = False
        gv.EnableSorting = False
        gv.EnableFiltering = False
        gv.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv.MasterTemplate.ShowRowHeaderColumn = False

        'ReStoreGridLayout()
    End Sub

    Private Sub ReStoreGridLayout()
        Try
            If clsCommon.myLen(ReportID) > 0 Then
                Dim obj As clsGridLayout = New clsGridLayout()
                obj = CType(obj.GetData(ReportID, "", objCommonVar.CurrentUserCode), clsGridLayout)
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

    Private Sub FrmPPIssueChildScrren_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F5 Then
                btnsave.PerformClick()
            ElseIf e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub FrmPPIssueChildScrren_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ButtonToolTip.SetToolTip(btnsave, "Press F5 for save data.")
        ButtonToolTip.SetToolTip(btnclose, "Press Esc for close window.")
    End Sub

    Sub LoadData(ByVal dtpDate As Date, ByVal _Icode As String, ByVal _IUnit As String, ByVal _Main_Loc_Code As String, ByVal _IProductType As String, ByVal _IsSub_Location As Integer, ByVal arrLoc As String, ByVal DocDate As Date)
        Try
            If clsCommon.CompairString(clsCommon.myCstr(clsFixedParameter.GetData(clsFixedParameterType.CheckLiveStockInProductionDuringTrans, clsFixedParameterCode.CheckLiveStockInProductionDuringTrans, Nothing)), "1") = CompairStringResult.Equal Then
                CheckStockServerDate = True
            Else
                CheckStockServerDate = False
            End If
            If CheckStockServerDate Then
                DocDate = clsCommon.GETSERVERDATE()
            End If
            LoadBlankGrid()
            Dim qry As String = ""
     
            Dim str As String = ""
            Dim whrcls As String = ""
            Dim ShowLocationItemLocationwise As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.ShowSiloLocationItemLocationwise, clsFixedParameterCode.ShowSiloLocationItemLocationwise, Nothing))
            Dim strItemLoc As String = ""
            If ShowLocationItemLocationwise = 1 Then
                strItemLoc = " and location in ( select location_code from TSPL_LOCATION_ITEMMAPPING where Item_code ='" & _Icode & "')"
            End If
            If _IsSub_Location = 0 Then 'for section
                whrcls = " and location in (Select location_code from tspl_location_master where main_location_code='" + _Main_Loc_Code + "' and isnull(Is_Section,'N')='Y')" & strItemLoc

            ElseIf _IsSub_Location = 1 Then 'for sub-location
                whrcls = " and location in (Select location_code from tspl_location_master where main_location_code='" + _Main_Loc_Code + "' and isnull(Is_Sub_Location,'N')='Y')" & strItemLoc

            ElseIf _IsSub_Location = 2 Then 'for main plant
                whrcls = " and location ='" + _Main_Loc_Code + "' "  ' in (select location_code from tspl_location_master where location_code in (" + arrLoc + ") and isnull(csa_type,'N')<>'Y' and isnull(Is_Section,'N')<>'Y' and isnull(Is_Sub_Location,'N')<>'Y')
            End If


            '======================now merge milk and normal qty
            qry = "select Location,ICode,SUM(qty*RI) as Qty,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg from ("
            qry += " select xx.ICode,xx.Location, xx.Qty as OldQty,xx.fat_kg as old_fatkg,xx.snf_kg as old_snfkg,xx.RI ,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.Qty* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as Qty,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.fat_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as fat_kg,(case when isnull(FinalUOM.Conversion_Factor,0)>0 then ((xx.snf_kg* TSPL_ITEM_UOM_DETAIL.Conversion_Factor)/FinalUOM.Conversion_Factor) else 0 end) as snf_kg from ("
            qry += " select Item_Code as ICode,Location_Code as Location ,SUM(QtyNew*RI) as Qty,1 as RI,UOMNew as UOM,sum(fat_kg*RI) as fat_kg,sum(snf_kg*RI) as snf_kg  from("
            qry += " select Trans_Id,Item_Code ,Location_Code,case when InOut='I' then 1 else -1 end as RI,Qty as QtyNew,UOMNew,fat_kg,snf_kg from("
            qry += " select TSPL_INVENTORY_MOVEMENT.Trans_Id, TSPL_INVENTORY_MOVEMENT.Item_Code ,TSPL_INVENTORY_MOVEMENT.Location_Code , TSPL_INVENTORY_MOVEMENT.InOut,(TSPL_INVENTORY_MOVEMENT.Stock_Qty  ) as qty  ,TSPL_INVENTORY_MOVEMENT.Stock_Uom as UOMNew "
            qry += ",0 as fat_kg,0 as snf_kg"
            qry += " from TSPL_INVENTORY_MOVEMENT left outer join tspl_location_master on tspl_location_master.location_code=tspl_inventory_movement.location_code "
            qry += " where 2=2 "
            Dim settFromLocationStockNotCheckConsumptionLocation As Boolean = (clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.FromLocationStockNotCheckConsumptionLocation, clsFixedParameterCode.FromLocationStockNotCheckConsumptionLocation, Nothing)) > 0)
            If Not settFromLocationStockNotCheckConsumptionLocation Then
                qry += " and tspl_location_master.Is_Consumption_Location=0 "
            End If
            qry += " and TSPL_INVENTORY_MOVEMENT.Qty<>0 and TSPL_INVENTORY_MOVEMENT.Item_Code='" + _Icode + "' " 'and TSPL_INVENTORY_MOVEMENT.location_code='" + strLocation + "' 
            qry += " and (case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + _Main_Loc_Code + "' "

            Dim intSettingType As Integer = clsCommon.myCdbl(clsFixedParameter.GetData(clsFixedParameterType.IsConsiderOutTypeDocForBalance, clsFixedParameterCode.IsConsiderOutTypeDocForBalance, Nothing))
            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT.InOut='I' and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += " union all "

            qry += " select TSPL_INVENTORY_MOVEMENT_new.Trans_Id, TSPL_INVENTORY_MOVEMENT_new.Item_Code ,TSPL_INVENTORY_MOVEMENT_new.Location_Code , TSPL_INVENTORY_MOVEMENT_new.InOut,(TSPL_INVENTORY_MOVEMENT_new.Stock_Qty ) as qty  ,TSPL_INVENTORY_MOVEMENT_new.Stock_Uom as UOMNew "
            qry += ",(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.fat_kg,0) else 0 end) as fat_kg,(case when TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "' then isnull(TSPL_INVENTORY_MOVEMENT_new.snf_kg,0) else 0 end) as snf_kg"
            qry += " from TSPL_INVENTORY_MOVEMENT_new left outer join tspl_location_master on tspl_location_master.location_code=TSPL_INVENTORY_MOVEMENT_new.location_code "
            qry += " where 2=2 "
            If Not settFromLocationStockNotCheckConsumptionLocation Then
                qry += " and tspl_location_master.Is_Consumption_Location=0 "
            End If
            qry += "and TSPL_INVENTORY_MOVEMENT_new.Qty<>0 and TSPL_INVENTORY_MOVEMENT_new.Item_Code='" + _Icode + "' "
            qry += " and (case when tspl_location_master.is_section<>'Y' and tspl_location_master.is_sub_location<>'Y' then tspl_location_master.location_code else tspl_location_master.main_location_code end)='" + _Main_Loc_Code + "' "

            If intSettingType = 1 Then
                qry += " and 2=(case when TSPL_INVENTORY_MOVEMENT_new.InOut='O' then 2 else case when TSPL_INVENTORY_MOVEMENT_new.InOut='I' and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "' then 2 else 0 end end) "
            ElseIf intSettingType = 0 Then
                qry += " and TSPL_INVENTORY_MOVEMENT_new.Punching_Date<='" + clsCommon.GetPrintDate(clsCommon.GetDateWithEndTime(DocDate), "dd/MMM/yyyy hh:mm tt") + "'"
            End If
            qry += ")ax)axa group by Item_Code,Location_Code,UOMNew)xx left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL.Item_Code=xx.ICode and TSPL_ITEM_UOM_DETAIL.UOM_Code=xx.UOM left outer join TSPL_ITEM_UOM_DETAIL as FinalUOM on FinalUOM.Item_Code=xx.ICode and FinalUOM.UOM_Code='" + _IUnit + "')axx group by axx.Location,axx.ICode"
            '==============================================end here==========================================

            str = "select * from (" + qry + ")final where 1=1 "
            str += whrcls

            Dim dt As DataTable = clsDBFuncationality.GetDataTable(str)

            gv.Rows.Clear()
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each dr As DataRow In dt.Rows
                    If clsCommon.myCdbl(dr("qty")) > 0 Then
                        gv.Rows.AddNew()

                        gv.Rows(gv.Rows.Count - 1).Cells(colSelect).Value = False
                        gv.Rows(gv.Rows.Count - 1).Cells(colLoccode).Value = clsCommon.myCstr(dr("Location"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colLocName).Value = clsLocation.GetName(clsCommon.myCstr(dr("Location")), Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colICode).Value = clsCommon.myCstr(dr("icode"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colIname).Value = clsItemMaster.GetItemName(clsCommon.myCstr(dr("icode")), Nothing)
                        gv.Rows(gv.Rows.Count - 1).Cells(colUnit).Value = _IUnit
                        gv.Rows(gv.Rows.Count - 1).Cells(colQty).Value = clsCommon.myCdbl(dr("qty"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colFatkg).Value = clsCommon.myCdbl(dr("fat_kg"))
                        gv.Rows(gv.Rows.Count - 1).Cells(colsnfkg).Value = clsCommon.myCdbl(dr("snf_kg"))
                    End If
                Next
            End If
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub

    Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub btnsave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsave.Click
        Arr_Loc = New List(Of String)
        For Each grow As GridViewRowInfo In gv.Rows
            If clsCommon.myCBool(grow.Cells(colSelect).Value) = True Then
                Arr_Loc.Add(clsCommon.myCstr(grow.Cells(colLoccode).Value))
            End If
        Next

        Me.Close()
    End Sub

    Private Sub btndeleteLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndeleteLayout.Click
        clsGridLayout.DeleteData(ReportID, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Delete layout successfully", "Information")
    End Sub

    Private Sub btnSaveLayout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveLayout.Click
        If clsCommon.myLen(ReportID) > 0 Then
            gv.MasterTemplate.FilterDescriptors.Clear()
            Dim obj As New clsGridLayout()
            obj.ReportID = ReportID
            obj.UserID = objCommonVar.CurrentUserCode
            obj.GridLayout = New MemoryStream()
            gv.SaveLayout(obj.GridLayout)
            obj.GridColumns = gv.ColumnCount
            obj.GridLayout.Seek(0, System.IO.SeekOrigin.Begin)
            If obj.SaveData() Then
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information")
            End If
            ''stuti regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
        End If
    End Sub

    Private Sub gv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gv.Click

    End Sub
End Class
