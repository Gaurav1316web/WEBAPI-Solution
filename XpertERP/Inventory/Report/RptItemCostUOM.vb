Imports common
Imports System.Data.SqlClient
Imports System.IO
'' Work done agaist ticket no. BHA/12/10/18-000622 
Public Class RptItemCostUOM
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
        txtPurchaseSet.arrValueMember = Nothing
        isInsideLoadData = False
        txtItem.arrValueMember = Nothing
        txtPurchaseSet.arrValueMember = Nothing
        txtItemStructure.arrValueMember = Nothing
        txtItemType.arrValueMember = Nothing
        RadPageView1.SelectedPage = RadPageViewPage1
        ChkMilkType.Checked = False

    End Sub

    Private Sub btnprint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnprint.Click
        Try
            PageSetupReport_ID = MyBase.Form_ID + IIf(ChkMilkType.Checked = True, "M", "")
            TemplateGridview = gv
            Print(Exporter.Refresh)
        Catch ex As Exception

        End Try
    End Sub

    Enum Exporter
        Print = 2
        Refresh = 1
        PDF = 3
        Export = 4
    End Enum
    Private Sub txtItemType__My_Click(sender As Object, e As EventArgs) Handles txtItemType._My_Click
        txtItemType.arrValueMember = clsCommon.ShowMultipleSelectForm("ItemTypestoreco", FrmItemMasterRMOther.LoadItemTypeQuery(), "Code", "Name", txtItemType.arrValueMember, txtItemType.arrDispalyMember)
    End Sub
    Private Sub txtItemStructure__My_Click(sender As Object, e As EventArgs) Handles txtItemStructure._My_Click
        Dim qry As String
        qry = " select Structure_Code as Code,structure_descq as [Description] from TSPL_STRUCTURE_MASTER "

        txtItemStructure.arrValueMember = clsCommon.ShowMultipleSelectForm("PurMulSel", qry, "Code", "Description", txtItemStructure.arrValueMember, txtItemStructure.arrDispalyMember)

    End Sub
    Sub Print(ByVal IsPrint As Exporter)
        Try
            isInsideLoadData = False
            If IsPrint = Exporter.Print OrElse IsPrint = Exporter.Refresh Then
                Dim strCodeColumn As String = ""
                Dim qryfinal As String = ""

                If clsCommon.myCBool(ChkMilkType.Checked) = True Then
                    qryfinal = " select * from (select TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code as [Item Code],TSPL_ITEM_MASTER.Item_Desc as [Item Name],TSPL_STRUCTURE_MASTER.structure_descq as [Item Structure],TSPL_ITEM_MASTER.Item_Type as [Item Type],TSPL_ITEM_QC_PARAMETER_MASTER.StandardRate,TSPL_PARAMETER_MASTER.Description from TSPL_ITEM_QC_PARAMETER_MASTER"
                    qryfinal += " inner join TSPL_PARAMETER_MASTER on TSPL_PARAMETER_MASTER.Code=TSPL_ITEM_QC_PARAMETER_MASTER.Code "
                    qryfinal += " inner join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=TSPL_ITEM_QC_PARAMETER_MASTER.Item_Code"
                    qryfinal += " inner join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code "

                    qryfinal += "         where 1 = 1 and  TSPL_ITEM_MASTER.Product_type='MI'  " ' Is_FreshItem=1
                    If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                        qryfinal += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                    End If
                    If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                        qryfinal += " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")"
                    End If
                    If txtItemStructure.arrValueMember IsNot Nothing AndAlso txtItemStructure.arrValueMember.Count > 0 Then
                        qryfinal += " and TSPL_ITEM_MASTER.Structure_Code in (" + clsCommon.GetMulcallString(txtItemStructure.arrValueMember) + ")"
                    End If
                    qryfinal += " "
                    qryfinal += " )aaa"
                    qryfinal += " pivot (sum(StandardRate) for Description in ([Fat %],[SNF %])) as piot"

                Else

                    qryfinal = "(select WUOM.item_code as [Item Code] ,max(WUOM.[Item Name])as [Item Name],max(WUOM.[Item type]) as [Item Type],max(Structure_Descq)as [Structure Name],max(WUOM.[User Name]) as [User Name],max(WUOM.[Modify Date]) as [Modify Date], max(case when WUOM.ln=1 then WUOM.UOM_Code else '' end) as [UOM1]  ,max(case when WUOM.ln=1 then WUOM.item_cost else 0 end) as [UOM Cost1]  "
                    qryfinal += " ,max(case when WUOM.ln=2 then WUOM.UOM_Code else '' end ) as [UOM2] "
                    qryfinal += " ,max(case when WUOM.ln=2 then WUOM.Item_Cost else 0 end) as [UOM Cost2]  "
                    qryfinal += " ,max(case when WUOM.ln=3 then WUOM.UOM_Code else '' end ) as [UOM3] "
                    qryfinal += " ,max(case when WUOM.ln=3 then WUOM.Item_Cost else 0 end) as [UOM Cost3]  "
                    qryfinal += " ,max(case when WUOM.ln=4 then WUOM.UOM_Code else '' end ) as [UOM4] "
                    qryfinal += " ,max(case when WUOM.ln=4 then WUOM.Item_Cost else 0 end) as [UOM Cost4]  "
                    qryfinal += " ,max(case when WUOM.ln=5 then WUOM.UOM_Code else '' end ) as [UOM5] "
                    qryfinal += " ,max(case when WUOM.ln=5 then WUOM.Item_Cost else 0 end) as [UOM Cost5]  "
                    qryfinal += " ,max(case when WUOM.ln=6 then WUOM.UOM_Code else '' end ) as [UOM6] "
                    qryfinal += " ,max(case when WUOM.ln=6 then WUOM.Item_Cost else 0 end) as [UOM Cost6]  "
                    qryfinal += " ,max(case when WUOM.ln=7 then WUOM.UOM_Code else '' end ) as [UOM7] "
                    qryfinal += " ,max(case when WUOM.ln=7 then WUOM.Item_Cost else 0 end) as [UOM Cost7]  "
                    qryfinal += " ,max(case when WUOM.ln=8 then WUOM.UOM_Code else '' end ) as [UOM8] "
                    qryfinal += " ,max(case when WUOM.ln=8 then WUOM.Item_Cost else 0 end) as [UOM Cost8]   "
                    qryfinal += " from  ( select  ROW_NUMBER() OVER (PARTITION BY tspl_item_uom_detail.item_code ORDER BY tspl_item_uom_detail.item_code) AS ln,"
                    qryfinal += "   tspl_item_uom_detail.item_code, tspl_item_uom_detail.UOM_Code, tspl_item_uom_detail.Item_Cost,TSPL_ITEM_MASTER.Item_Desc as [Item Name],TSPL_ITEM_MASTER.Item_Type as [Item type],TSPL_STRUCTURE_MASTER.Structure_Descq,TSPL_USER_MASTER.User_Name as [User Name],convert(varchar,TSPL_ITEM_MASTER.Modify_Date,103) as [Modify Date] from tspl_item_uom_detail "
                    qryfinal += " left join TSPL_UNIT_MASTER on TSPL_UNIT_MASTER.unit_code = tspl_item_uom_detail.UOM_code"
                    qryfinal += " left outer join TSPL_ITEM_MASTER on TSPL_ITEM_MASTER.Item_Code=tspl_item_uom_detail.item_code"
                    qryfinal += " left outer join TSPL_STRUCTURE_MASTER on TSPL_STRUCTURE_MASTER.Structure_Code=TSPL_ITEM_MASTER.Structure_Code"
                    qryfinal += " left outer join TSPL_USER_MASTER on TSPL_USER_MASTER.User_Code=TSPL_ITEM_MASTER.Modify_By "
                    qryfinal += " where 2=2  "

                    If txtItem.arrValueMember IsNot Nothing AndAlso txtItem.arrValueMember.Count > 0 Then
                        qryfinal += " and TSPL_ITEM_MASTER.Item_Code in (" + clsCommon.GetMulcallString(txtItem.arrValueMember) + ")"
                    End If
                    If txtItemType.arrValueMember IsNot Nothing AndAlso txtItemType.arrValueMember.Count > 0 Then
                        qryfinal += " and TSPL_ITEM_MASTER.Item_Type in (" + clsCommon.GetMulcallString(txtItemType.arrValueMember) + ")"
                    End If
                    If txtItemStructure.arrValueMember IsNot Nothing AndAlso txtItemStructure.arrValueMember.Count > 0 Then
                        qryfinal += " and TSPL_ITEM_MASTER.Structure_Code in (" + clsCommon.GetMulcallString(txtItemStructure.arrValueMember) + ")"
                    End If

                    qryfinal += "  )WUOM group by item_code )"
                  

                End If

                Dim dtfinal As DataTable = clsDBFuncationality.GetDataTable(qryfinal)

                gv.DataSource = Nothing
                gv.Rows.Clear()
                gv.Columns.Clear()

                If dtfinal IsNot Nothing AndAlso dtfinal.Rows.Count <= 0 Then
                    common.clsCommon.MyMessageBoxShow(Me, "No Record Found", Me.Text)
                ElseIf IsPrint = Exporter.Print Then

                Else
                    gv.DataSource = dtfinal
                    RadPageView1.SelectedPage = RadPageViewPage2
                    gv.BestFitColumns()
                    isInsideLoadData = True
                    FormatGrid()
                End If
            End If
            ReStoreGridLayout()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)

        End Try
    End Sub


    Private Sub TxtMultiSelectFinder1__My_Click(sender As Object, e As EventArgs) Handles txtPurchaseSet._My_Click
        Dim qry As String
        qry = " select Unit_Code as Code,Unit_Desc as [Description] from TSPL_UNIT_MASTER "
        txtPurchaseSet.arrValueMember = clsCommon.ShowMultipleSelectForm("PurMulSel", qry, "Code", "Description", txtPurchaseSet.arrValueMember, txtPurchaseSet.arrDispalyMember)
    End Sub
    Private Sub TxtItem__My_Click(sender As Object, e As EventArgs) Handles txtItem._My_Click
        Dim qry As String
        qry = " select Item_Code as Code,Item_Desc as [Description] from TSPL_ITEM_MASTER "
        txtItem.arrValueMember = clsCommon.ShowMultipleSelectForm("PurMulSel", qry, "Code", "Description", txtItem.arrValueMember, txtItem.arrDispalyMember)
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

        gv.TableElement.TableHeaderHeight = 40
        gv.MasterTemplate.ShowRowHeaderColumn = False
        For ii As Integer = 0 To gv.Columns.Count - 1
            gv.Columns(ii).ReadOnly = True
            gv.Columns(ii).IsVisible = True
        Next

    End Sub

    Private Sub btnreset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnreset.Click
        FunReset()
    End Sub

    Private Sub btnexcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnexcel.Click
        Print(Exporter.Export)
        Export(EnumExportTo.Excel)
        'If gv.Rows.Count > 0 Then
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add("Item Cost Report")
        '    clsCommon.MyExportToExcelGrid("Item Cost List", gv, arrHeader, "Item Cost Report")
        'End If
    End Sub

    Private Sub btnpdf_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnpdf.Click
        Print(Exporter.PDF)
        Export(EnumExportTo.PDF)
        'If gv.Rows.Count > 0 Then
        '    Dim arrHeader As List(Of String) = New List(Of String)()
        '    arrHeader.Add("Item Cost Report")
        '    clsCommon.MyExportToPDF("Item Cost List", gv, arrHeader, "Item Cost Report")
        'End If
    End Sub
    Private Sub Export(ByVal exporter As EnumExportTo)
        Try
            If gv.Rows.Count > 0 Then
                Dim arrHeader As List(Of String) = New List(Of String)()
                arrHeader.Add("Company : " & objCommonVar.CurrentCompanyName)
                arrHeader.Add("Name : Item Cost Report")

                If txtItem.arrDispalyMember IsNot Nothing AndAlso txtItem.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item : " + clsCommon.GetMulcallStringWithComma(txtItem.arrDispalyMember))
                End If
                If txtPurchaseSet.arrDispalyMember IsNot Nothing AndAlso txtPurchaseSet.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("UOM : " + clsCommon.GetMulcallStringWithComma(txtPurchaseSet.arrDispalyMember))
                End If
                If txtItemType.arrDispalyMember IsNot Nothing AndAlso txtItemType.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item Type : " + clsCommon.GetMulcallStringWithComma(txtItemType.arrDispalyMember))
                End If
                If txtItemStructure.arrDispalyMember IsNot Nothing AndAlso txtItemStructure.arrDispalyMember.Count > 0 Then
                    arrHeader.Add("Item Structure : " + clsCommon.GetMulcallStringWithComma(txtItemStructure.arrDispalyMember))
                End If

                If exporter = EnumExportTo.Excel Then
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToExcelGrid("Item Cost List", gv, arrHeader, "Item Cost Report")
                Else
                    transportSql.applyExportTemplate(gv, PageSetupReport_ID)
                    clsCommon.MyExportToPDF("Item Cost Report", gv, arrHeader, "Item Cost Report", PageSetupReport_ID, objCommonVar.CurrentUserCode)
                End If
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub




    Private Sub btnclose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnclose.Click
        Me.Close()
    End Sub

    Private Sub rmSaveLayout_Click(sender As Object, e As EventArgs) Handles rmSaveLayout.Click
        Dim ReportID As String = PageSetupReport_ID

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
                common.clsCommon.MyMessageBoxShow(Me, "Layout saved successfully", "Information", Me.Text)
            End If

            ''richa agarwal regarding memory leakage
            obj.GridLayout.Close()
            obj.GridLayout.Dispose()
            ''---------------
        End If
    End Sub

    Private Sub rmDeleteLayout_Click(sender As Object, e As EventArgs) Handles rmDeleteLayout.Click
        Dim TempFormId As String = ""
        TempFormId = PageSetupReport_ID
        clsGridLayout.DeleteData(TempFormId, objCommonVar.CurrentUserCode)
        common.clsCommon.MyMessageBoxShow(Me, "Layout Delete successfully", "Information", Me.Text)
    End Sub

End Class
