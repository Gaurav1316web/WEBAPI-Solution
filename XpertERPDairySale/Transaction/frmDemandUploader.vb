Imports System.Data.SqlClient
Imports System.IO
Imports common
Imports System.Globalization
Imports System.Text.RegularExpressions
'Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports Newtonsoft.Json.Linq
Public Class frmDemandUploader
    Inherits FrmMainTranScreen
#Region "Variable"
    Dim isNewEntry As Boolean = False
    Dim isInsideLoadData As Boolean = False
    Const colLineNo As String = "COLLNO"
    Const colTripNo As String = "colTripNo"
    Const colbtncol As String = "colbtncol"
    Const colCustCode As String = "colCustCode"
    Const colItemCode As String = "colItemCode"
    'Dim lstObj As List(Of clsDemandBookingSale)
    'Dim lstD As List(Of clsDemandUploaderDetails)
    Dim ApplyItemCapacityLimit As Boolean = False
#End Region
    Public Sub SetUserMgmtNew()
        'MyBase.SetUserMgmt(clsUserMgtCode.frmBookingProductSale)
        If Not (MyBase.isReadFlag) Then
            Throw New Exception("Permission Denied")
        End If
        btnSave.Visible = MyBase.isModifyFlag
        btnSavePost.Visible = MyBase.isPostFlag
    End Sub
    Private Sub frmDemandUploader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUserMgmtNew()

        AddNew()
    End Sub
    Public Sub AddNew()
        txtDate.Value = clsCommon.GETSERVERDATE()
        rbtnMorning.IsChecked = True
        rbtnCrate.IsChecked = True
        'gv1.Rows.Clear()
        'gv1.Columns.Clear()
        gv1.DataSource = Nothing
        txtLocation.Value = ""
        lblLocationDesc.Text = ""
        EnableDisable(True)
        btnSavePost.Visible = False
        txtLocation.Value = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Default_Location from TSPL_USER_MASTER where User_Code='" + objCommonVar.CurrentUserCode + "' "))
        If clsCommon.myLen(txtLocation.Value) > 0 Then
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        End If

    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Public Sub EnableDisable(ByVal flag As Boolean)
        btnGo.Enabled = flag
        btnUpload.Enabled = Not flag
        btnValidate.Enabled = Not flag

        btnSave.Enabled = Not flag
        txtDate.Enabled = flag
        txtLocation.Enabled = flag
        rgbShift.Enabled = flag
        rgbEntryUOM.Enabled = flag
        'btnExport.Enabled = flag
    End Sub
    Private Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Try
            Import()
            btnValidate.Enabled = True
            btnExport.Enabled = False
            btnUpload.Enabled = False
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub Import()
        Dim gvImport As New UserControls.MyRadGridView
        Dim arrVisbleColumns As New List(Of Integer)
        Try
            Me.Controls.Add(gvImport)
            If clsDemandBookingImport.importExcel(gvImport) Then
                LoadBlankGrid()
                If gvImport.Rows.Count > 0 Then
                    Try
                        Dim arrCustCodeExist As New List(Of String)
                        isInsideLoadData = True
                        clsCommon.ProgressBarPercentShow()
                        For ii As Integer = 1 To gvImport.Rows.Count - 1
                            clsCommon.ProgressBarPercentUpdate(((ii + 1) * 100) / gvImport.RowCount, clsCommon.myCstr((ii + 1)) + "/" + clsCommon.myCstr(gvImport.RowCount))
                            For kk As Integer = 0 To gv1.Columns.Count - 1
                                gv1.Rows(ii - 1).Cells(kk).Value = gvImport.Rows(ii).Cells(kk).Value
                            Next
                            gv1.Rows.AddNew()
                        Next
                    Catch ex As Exception
                        Throw New Exception(ex.Message)
                    Finally
                        clsCommon.ProgressBarPercentHide()
                    End Try
                    isInsideLoadData = False
                    clsCommon.MyMessageBoxShow(Me, "Data Transfer Completed!", Me.Text, MessageBoxButtons.OK)
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            Me.Controls.Remove(gvImport)
        End Try
    End Sub
    Private Sub LoadBlankGrid()
        Dim qry As String = String.Empty
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        gv1.DataSource = Nothing
        gv1.Rows.AddNew()
        Dim repoLineNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoLineNo = New GridViewDecimalColumn()
        repoLineNo.FormatString = ""
        repoLineNo.HeaderText = "Line No"
        repoLineNo.Name = colLineNo
        repoLineNo.Width = 50
        repoLineNo.ReadOnly = True
        repoLineNo.IsPinned = True
        repoLineNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoLineNo)
        Dim repoTripNo As GridViewDecimalColumn = New GridViewDecimalColumn()
        repoTripNo = New GridViewDecimalColumn()
        repoTripNo.FormatString = ""
        repoTripNo.HeaderText = "Trip No"
        repoTripNo.Name = colTripNo
        repoTripNo.Width = 50
        repoTripNo.ReadOnly = True
        repoTripNo.IsPinned = True
        repoTripNo.TextAlignment = System.Drawing.ContentAlignment.MiddleRight
        gv1.MasterTemplate.Columns.Add(repoTripNo)
        Dim repoCustCode As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        repoCustCode.FormatString = ""
        repoCustCode.HeaderText = "Code"
        repoCustCode.Name = colCustCode
        repoCustCode.HeaderImage = My.Resources.search4
        repoCustCode.TextImageRelation = TextImageRelation.TextBeforeImage
        repoCustCode.Width = 50
        repoCustCode.IsVisible = True
        repoCustCode.IsPinned = True
        repoCustCode.ReadOnly = True
        gv1.MasterTemplate.Columns.Add(repoCustCode)
        Dim repoIName As GridViewTextBoxColumn = New GridViewTextBoxColumn()
        qry = "select * from (select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code,tspl_item_master.Short_Description ,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit   from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1 "
        If rbtnCrate.IsChecked Then
            qry += " and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate') "
        ElseIf rbtnCratePouch.IsChecked Then
            qry += " and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate','Pouch') "
        ElseIf rbtnLTR.IsChecked Then
            qry += " and TSPL_ITEM_UOM_DETAIL.Uom_code in ('LTR') "
        End If
        qry += " union
    select 'Fresh' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,1 as RowNo,tspl_item_master.Sku_Seq,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit  from tspl_item_master 
    left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_FreshItem =1 AND TSPL_ITEM_MASTER.Is_Milk_Pouch =1 and isnull(TSPL_ITEM_MASTER.CAN,0)=0  and isnull(TSPL_ITEM_MASTER.CRATE,0)=0  and Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1 "
        If rbtnCrate.IsChecked Then
            qry += " and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate') "
        ElseIf rbtnCratePouch.IsChecked Then
            qry += " and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate','Pouch') "
        ElseIf rbtnLTR.IsChecked Then
            qry += " and TSPL_ITEM_UOM_DETAIL.Uom_code in ('LTR') "
        End If
        'and TSPL_ITEM_UOM_DETAIL.Uom_code in ('Crate','Pouch','LTR')"
        If Not clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
            qry += " union all
    Select 'Ambient' as FreshAmbient,tspl_item_master.Item_Code ,tspl_item_master.Short_Description,tspl_item_master.Item_Desc , TSPL_ITEM_UOM_DETAIL.UOM_Code,tspl_item_master.Short_Description +' - '+ TSPL_ITEM_UOM_DETAIL.UOM_Code as ItemDescNew,2 as RowNo,tspl_item_master.Sku_Seq,TSPL_ITEM_UOM_DETAIL.Conversion_Factor,TSPL_ITEM_UOM_DETAIL.Stocking_Unit   from tspl_item_master 
    Left outer join TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_UOM_DETAIL .item_code=tspl_item_master.Item_Code 
    where  tspl_item_master.Is_Ambient = 1 And isnull(TSPL_ITEM_MASTER.CAN, 0) = 0 And isnull(TSPL_ITEM_MASTER.CRATE, 0) = 0 And Item_Type ='F' and tspl_item_master.Active=1 and tspl_item_master.Is_DisplayDemand=1
And TSPL_ITEM_UOM_DETAIL.Default_UOM = 1"
        End If
        qry += " )z order by RowNo,Sku_Seq,Item_Code"
        Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            Dim i As Integer = 1
            Dim obj As ItemValueClass = New ItemValueClass()
            For Each dr As DataRow In dt.Rows
                repoIName = New GridViewTextBoxColumn()
                repoIName.FormatString = ""
                repoIName.HeaderText = clsCommon.myCstr(dr("UOM_Code"))
                obj = New ItemValueClass()
                obj.itemCode = clsCommon.myCstr(dr("Item_Code"))
                obj.itemDesc = clsCommon.myCstr(dr("Item_Desc"))
                obj.Unit_code = clsCommon.myCstr(dr("UOM_Code"))
                obj.IsFreshAmbient = clsCommon.myCstr(dr("FreshAmbient"))
                obj.ShortDesc = clsCommon.myCstr(dr("Short_Description"))
                obj.Conversion_Factor = clsCommon.myCdbl(dr("Conversion_Factor"))
                obj.Stocking_Unit = clsCommon.myCstr(dr("Stocking_Unit"))
                repoIName.Tag = obj
                repoIName.Name = colItemCode + clsCommon.myCstr(i)
                repoIName.Width = 50
                repoIName.ReadOnly = True
                repoIName.IsVisible = True
                i = i + 1
                gv1.MasterTemplate.Columns.Add(repoIName)
            Next
        End If
        gv1.AllowDeleteRow = False
        gv1.AllowAddNewRow = False
        gv1.ShowGroupPanel = False
        gv1.AllowColumnReorder = False
        gv1.AllowRowReorder = False
        gv1.EnableSorting = False
        gv1.EnableFiltering = False
        gv1.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom
        gv1.MasterTemplate.ShowRowHeaderColumn = False
        gv1.TableElement.TableHeaderHeight = 40
        View()
    End Sub
    Sub View()
        Try
            If gv1.Rows.Count > 0 Then
                Dim view As New ColumnGroupsViewDefinition()
                view.ColumnGroups.Add(New GridViewColumnGroup("Booth"))
                view.ColumnGroups(0).Rows.Add(New GridViewColumnGroupRow())
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colLineNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colTripNo).Name)
                view.ColumnGroups(0).Rows(0).ColumnNames.Add(gv1.Columns(colCustCode).Name)
                view.ColumnGroups(0).IsPinned = True
                Dim TempColGroupCount As Integer = 1
                Dim obj As ItemValueClass = New ItemValueClass()
                Dim i As Integer = 1
                For dblcolumns As Integer = 3 To gv1.Columns.Count - 1
                    Dim obj1 As ItemValueClass = TryCast(gv1.Columns(dblcolumns).Tag, ItemValueClass)
                    If obj1 IsNot Nothing Then
                        If clsCommon.CompairString(obj1.IsFreshAmbient, "Fresh") = CompairStringResult.Equal Then
                            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(obj1.ShortDesc)))
                            view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                            If rbtnCrate.IsChecked Then
                                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                                'dblcolumns = dblcolumns + 1
                            ElseIf rbtnCratePouch.IsChecked Then
                                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                                dblcolumns = dblcolumns + 1
                                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                                'dblcolumns = dblcolumns + 1
                            ElseIf rbtnLTR.IsChecked Then
                                view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                                'dblcolumns = dblcolumns + 1
                            End If
                            'view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            TempColGroupCount += 1
                        Else
                            view.ColumnGroups.Add(New GridViewColumnGroup(clsCommon.myCstr(obj1.ShortDesc)))
                            view.ColumnGroups(TempColGroupCount).Rows.Add(New GridViewColumnGroupRow())
                            view.ColumnGroups(TempColGroupCount).Rows(0).ColumnNames.Add(gv1.Columns(dblcolumns).Name)
                            TempColGroupCount += 1
                        End If
                    End If
                Next
                'MergeHorizontally(gv1, 0, gv1.Rows.Count - 1)
                gv1.ViewDefinition = view
            End If
        Catch ex As Exception
            common.clsCommon.MyMessageBoxShow(Me, ex.Message, "Error", MessageBoxButtons.OK, RadMessageIcon.Error)
        End Try
    End Sub
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        gv1.Rows.Clear()
        gv1.Columns.Clear()
        AddNew()
    End Sub
    Private Sub btnValidate_Click(sender As Object, e As EventArgs) Handles btnValidate.Click
        Try
            ValidateGrid()
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Sub ValidateGrid()
        Try
            btnSavePost.Enabled = False
            btnSave.Enabled = False
            Dim mess As String = ""
            Dim lineNo As Integer = 1
            Dim lstCode As New List(Of String)
            Dim location_Code As String = ""
            clsCommon.ProgressBarShow()

            For dblrows As Integer = 0 To gv1.Rows.Count - 1

                Dim k As Integer = 1
                Try
                    clsCommon.ProgressBarUpdate("Validating, Please wait..." & (dblrows + 1) & "/" & gv1.Rows.Count)
                    If (Not String.IsNullOrEmpty(clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value))) Then
                        If (ValidateBooth(gv1.Rows(dblrows).Cells(2).Value)) Then
                            If lstCode.Contains(clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value)) Then
                                Throw New Exception("Duplicate Booth Code [" + clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value) + "]")
                            Else
                                lstCode.Add(clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value))
                            End If
                            If clsCommon.myLen(txtLocation.Value) > 0 Then
                                location_Code = txtLocation.Value
                            Else
                                Throw New Exception("Location is Empty")
                            End If
                            Dim RouteNO As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from tspl_Customer_Master where Cust_Code='" + clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value) + "'"))
                                Dim DocumentNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Document_No from TSPL_DEMAND_BOOKING_MASTER where Route_No='" + clsCommon.myCstr(RouteNO) + "' and convert(date,Document_Date,103)='" + clsCommon.GetPrintDate(txtDate.Value) + "' and ShiftType='" + IIf(rbtnMorning.IsChecked, "Morning", "Evening") + "' and Posted=1 "))
                                If clsCommon.myLen(DocumentNo) > 0 Then
                                    Throw New Exception("Document already Posted for Route No -" & clsCommon.myCstr(RouteNO) & " Shift " + IIf(rbtnMorning.IsChecked, "Morning", "Evening") + "")
                                End If
                                For dblcolumns As Integer = 3 To gv1.Columns.Count - 1
                                    Dim obj1 As ItemValueClass = Nothing
                                    Try
                                        obj1 = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                                    Catch ex As Exception
                                    End Try
                                    k = k + 1
                                    If obj1 IsNot Nothing Then
                                        If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                                            If clsCommon.CompairString(clsCommon.myCstr(obj1.IsFreshAmbient), "Fresh") = CompairStringResult.Equal Then
                                            If clsCommon.CompairString(clsCommon.myCstr(obj1.Unit_code), "Crate") = CompairStringResult.Equal Then
                                                Dim cellValue As String = clsCommon.myCstr(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                                If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(AllowEntryInDecimal) from TSPL_ITEM_MASTER where Item_Code='" + obj1.itemCode + "' and  AllowEntryInDecimal=1")) = 0 Then
                                                    If cellValue.Contains(".") OrElse cellValue.Contains(",") Then
                                                        'gv1.Rows(dblrows).Cells(dblcolumns).Value = ""
                                                        Throw New Exception("Decimal values are not allowed.")
                                                    End If
                                                Else
                                                    If clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) Mod 0.5 <> 0 Then
                                                        Throw New Exception("Should be in multiple of 0.5")
                                                    End If
                                                End If
                                            Else
                                                Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(obj1.itemCode) & "'"))
                                                If ItemCrateType = 1 Then
                                                    Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(obj1.Unit_code) & "'"))
                                                    Dim CrateConvFactor As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and tspl_unit_master.Crate_Type ='Y' "))
                                                    Dim ItemConvFactor As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                                    Dim ItempouchCF As Decimal = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='Pouch' "))
                                                    Dim cellValue As String = clsCommon.myCstr(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                                    If Not clsCommon.CompairString(objCommonVar.CurrComp_Code1, "JPR") = CompairStringResult.Equal Then
                                                        If CrateConvFactor > 0 And ItemConvFactor > 0 Then
                                                            Dim DispatchQty As Decimal = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) * ItemConvFactor
                                                            If clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(AllowEntryInDecimal) from TSPL_ITEM_MASTER where Item_Code='" + obj1.itemCode + "' and  AllowEntryInDecimal=1")) = 0 Then
                                                                If cellValue.Contains(".") OrElse cellValue.Contains(",") Then
                                                                    'gv1.Rows(dblrows).Cells(dblcolumns).Value = ""
                                                                    Throw New Exception("Decimal values are not allowed.")
                                                                End If
                                                            Else
                                                                If DispatchQty Mod ItempouchCF <> 0 Then
                                                                    Throw New Exception("Should be in multiple of " + clsCommon.myCstr(ItempouchCF))
                                                                End If
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                            Dim PriceCode As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER where Cust_Code='" + clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value) + "'"))
                                            Dim itemRate As Double = GetItemRate(PriceCode, obj1.Unit_code, obj1.itemCode, lineNo, clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value), obj1.itemDesc)

                                            Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                                            Dim ItemConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                            Else
                                                Dim ItemCrateType As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select IS_CrateType  from TSPL_ITEM_MASTER Where Item_Code  ='" & clsCommon.myCstr(obj1.itemCode) & "'"))
                                                If ItemCrateType = 1 Then
                                                    Dim IsStockingUnit As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Stocking_Unit from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code  ='" & clsCommon.myCstr(obj1.Unit_code) & "'"))
                                                    Dim CrateConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and tspl_unit_master.Crate_Type ='Y' "))
                                                    Dim ItemConvFactor As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(obj1.itemCode) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(obj1.Unit_code) & "' "))
                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                            Else
                                Throw New Exception(" Booth: [ " + clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value) + " ] does not exist in ERP" & Environment.NewLine)
                        End If
                        lineNo += 1
                    End If
                Catch ex As Exception

                    mess += "Error at Line No:[" + clsCommon.myCstr(gv1.Rows(dblrows).Cells(0).Value) + "] " + ex.Message & Environment.NewLine
                End Try


            Next
            clsCommon.ProgressBarHide()
            If Not String.IsNullOrEmpty(mess) Then
                Throw New Exception(mess)
                btnSavePost.Enabled = False
                btnSave.Enabled = False
            Else
                btnSavePost.Enabled = True
                btnSave.Enabled = True
                btnValidate.Enabled = False
                clsCommon.MyMessageBoxShow(Me, "Validate Successfully.", Me.Text)
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Function ValidateBooth(ByVal BoothCode As String) As Boolean
        Try
            Dim count As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select count(Cust_Code) from TSPL_CUSTOMER_MASTER where Cust_Code='" + BoothCode + "' and Status='N'"))
            If count = 0 Then
                Return False
            End If
            Return True
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function GetItemRate(ByVal strPriceCode As String, ByVal Unit_Code As String, ByVal Item_Code As String, ByVal LineNo As Integer, ByVal Booth As String, ByVal ItemDesc As String) As Double
        Dim ItemRate As Double = 0
        Try
            Dim qry As String = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,XXXE.TAX1_Rate, " &
                   " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                   "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                   " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                   " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                   " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                   " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                   "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                   "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                   "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                   "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                   " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                   " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                   " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                   " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                   "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                   "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                   "TSPL_ITEM_PRICE_MASTER.Price_Code='" & strPriceCode & "' and UOM='" & Unit_Code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & Item_Code & "' AND Location_Code='" & clsCommon.myCstr(txtLocation.Value) & "'  " &
                   ") XXXE WHERE RowNo=1  "
            Dim dt As DataTable = clsDBFuncationality.GetDataTable(qry)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                ItemRate = clsCommon.myCdbl(dt.Rows(0).Item("Item_Basic_Price"))
                If ItemRate = 0 Then
                    Throw New Exception("Please Fill Selling Price for Location " & txtLocation.Value & "  for item " & clsCommon.myCstr(ItemDesc) & " at LineNo -" & clsCommon.myCstr(LineNo) & Environment.NewLine)
                End If
                Return ItemRate
            Else
                Throw New Exception("Please create Price chart for Customer " & Booth & " , item " & ItemDesc & " at LineNo - " & clsCommon.myCstr(LineNo))
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
        Return ItemRate
    End Function
    Private Sub txtLocation__MYValidating(sender As Object, e As EventArgs, isButtonClicked As Boolean) Handles txtLocation._MYValidating
        Try
            Dim qry As String = " select Location_Code as [Code],Location_Desc as [Description],Loc_Short_Name as [Short Name],Add1,Add2,Add3,Add4,City_Code as [City Code],State,Pin_Code as [Pin Code],Country,Telphone,Email,Location_Type as [Location Type],Loc_Status as [Location Status],Status_Date as [Status Date],Excisable,Loc_Segment_Code as [Location Segment Code],Type,Purchase_Tax_Group as [Purchase Tax Group],Sales_Tax_Group as [Sales Tax Group],Ecc_Number as [ECC Number],Registration_Number as [Registration Number],Commissionerate as [Commission Rate],Range_Code as [Range Code],Range_Name as [Range Name],Range_Address as [Range Address],Division_Code as [Division Code],Division_Name as [Division Name],Division_Address as [Division Address],Created_By as [Created By],Created_Date as [Created Date],Modify_By as [Modify By],Modify_Date as [Modify Date],Comp_code as [Company Code],TIN_No as [TIN No],TAN_No as [TAN No],TCAN_No as [TCAN No],Service_Tax_Reg_No as [Service Tax Registration No],DutyPaid as [Duty Paid],Purchase_Tax_GroupIS as [Purchase Tax Group Inter State],Sales_Tax_GroupIS as [Sales Tax Group Inter State],Stock_Transfer_Filled_Ac as [Stock Transfer Filled Account],Stock_Transfer_Empty_Ac as [Stock Transfer Empty Account],GIT_Location as [GIT Location],GIT_Type as [GIT Type],Rejected_Type as [Rejected Type],Rejected_Location as [Rejected Location],CSA_Type as [CSA Type],Cust_Code as [Cust Code],MCC_Type as [MCC Type],CST_No as [CST No],Phone1,Phone2  from TSPL_Location_MASTER"
            Dim WhrCls As String = " Loc_Status='N' and Location_Type='Physical' and Is_Section='N' and Is_Sub_Location='N' and CSA_Type <>'Y' and DutyPaid <>'Y' and Rejected_Type <>'Y' and GIT_Type<>'Y'"
            If clsCommon.myLen(objCommonVar.strCurrUserLocations) > 0 Then
                WhrCls += "  and  Location_Code in (" + objCommonVar.strCurrUserLocations + ")"
            End If
            txtLocation.Value = clsCommon.ShowSelectForm("MulDS-BOLocFndr", qry, "Code", WhrCls, txtLocation.Value, "Code", isButtonClicked)
            lblLocationDesc.Text = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Desc from TSPL_LOCATION_MASTER where Location_Code='" + txtLocation.Value + "'"))
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Try
            LoadBlankGrid()
            EnableDisable(False)
            btnSave.Enabled = False
            btnValidate.Enabled = False

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            transportSql.exportdata(gv1, "", "Demand Uploader", ,, False, False, True)
        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim obj As clsDemandBookingSale = Nothing
        Try
            If SaveData() Then
                clsCommon.MyMessageBoxShow(Me, "Save Successfully", Me.Text)
            End If

        Catch ex As Exception
            clsCommon.MyMessageBoxShow(Me, ex.Message, Me.Text)
        End Try
    End Sub
    Public Function SaveData() As Boolean
        Dim lstobj As New List(Of clsDemandBookingSale)
        Dim qry As String = String.Empty
        clsCommon.ProgressBarShow()
        Try
            If gv1.Rows.Count > 0 Then
                For dblrows As Integer = 0 To gv1.Rows.Count - 2
                    clsCommon.ProgressBarUpdate("Saving Booth Code [" & clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value) & "], Please wait..." & (dblrows + 1) & "/" & gv1.Rows.Count)
                    Dim strCustCode As String = clsCommon.myCstr(gv1.Rows(dblrows).Cells(2).Value)
                    Dim strShiftType As String = String.Empty
                    If rbtnMorning.IsChecked Then
                        strShiftType = "Morning"
                    ElseIf rbtnEvening.IsChecked Then
                        strShiftType = "Evening"
                    End If
                    Dim RouteNo As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Route_No from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCustCode + "'"))
                    If clsCommon.myLen(RouteNo) > 0 Then
                        qry = "Select Document_No from TSPL_DEMAND_BOOKING_MASTER where Route_No = '" + RouteNo + "' and  CONVERT(varchar, CAST(Document_Date AS datetime), 103) ='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MM/yyyy") & "' and ShiftType = '" & strShiftType & "' and IsIndividualCustomer=0"
                    End If
                    Dim DocumentNO As String = clsCommon.myCstr(clsDBFuncationality.getSingleValue(qry))
                    If clsCommon.myLen(DocumentNO) > 0 Then
                        Dim location_Code As String = txtLocation.Value
                        Dim k As Integer = 1
                        Dim lineNo As Integer = 1
                        Dim DObj As New List(Of clsDemandBookingSaleDetail)
                        For dblcolumns As Integer = 3 To gv1.Columns.Count - 1
                            Dim obj1 As ItemValueClass = Nothing
                            'Dim objDBD As New clsDemandBookingSaleDetail
                            Try
                                obj1 = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                            Catch ex As Exception
                            End Try
                            k = k + 1
                            If obj1 IsNot Nothing Then
                                If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                                    Dim objDBD As New clsDemandBookingSaleDetail
                                    objDBD.Line_No = lineNo
                                    objDBD.Document_No = DocumentNO
                                    objDBD.Cust_Code = strCustCode
                                    objDBD.Item_Code = clsCommon.myCstr(obj1.itemCode)
                                    objDBD.Item_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(obj1.itemCode) + "'"))
                                    objDBD.Unit_code = clsCommon.myCstr(obj1.Unit_code)
                                    objDBD.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                    'objDBD.REF_PK_ID = clsCommon.myCstr(clsCommon.myCdbl(dr("PK_ID")))
                                    objDBD.Price_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCustCode + "'"))
                                    objDBD.ShiftType = strShiftType
                                    objDBD.Vehicle_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vehicle_No from TSPL_VEHICLE_MASTER left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where TSPL_ROUTE_MASTER.Route_No='" + RouteNo + "'"))
                                    objDBD.Line_No = strCustCode
                                    objDBD.Trip_No = 1
                                    objDBD.TotalCrates_ItemWise = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                    Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objDBD.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                                    Dim ItemConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objDBD.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objDBD.Unit_code) & "' "))
                                    If CrateConvFactor_Ltr > 0 And ItemConvFactor_Ltr > 0 Then
                                        Dim DispatchQty As Double = clsCommon.myCdbl(objDBD.Qty) * ItemConvFactor_Ltr
                                        objDBD.TotalLtr_ItemWise = (DispatchQty / CrateConvFactor_Ltr)
                                    End If
                                    qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, XXXE.Tax_group,XXXE.TAX1_Rate, " &
                                    " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                                    "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                                    " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                                    " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                                    " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                                    " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt, XXXE.TAX5_Amt,XXXE.TAX6_Amt,XXXE.TAX7_Amt,XXXE.TAX8_Amt,XXXE.TAX9_Amt,XXXE.TAX10_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                                    "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                                    "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                                    "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, TSPL_ITEM_PRICE_MASTER.Tax_group,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                                    "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                                    " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                                    " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                                    " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                                    " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.TAX5_Amt,TSPL_ITEM_PRICE_MASTER.TAX6_Amt,TSPL_ITEM_PRICE_MASTER.TAX7_Amt,   TSPL_ITEM_PRICE_MASTER.TAX8_Amt,TSPL_ITEM_PRICE_MASTER.TAX9_Amt,TSPL_ITEM_PRICE_MASTER.TAX10_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                                    "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                                    "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                                    "TSPL_ITEM_PRICE_MASTER.Price_Code='" & objDBD.Price_Code & "' and UOM='" & objDBD.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objDBD.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(location_Code) & "'" &
                                    ") XXXE WHERE RowNo=1  "
                                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                                    If dt2.Rows.Count > 0 Then
                                        Dim dblRate As Decimal = clsCommon.myCDecimal(dt2.Rows(0).Item("Item_Basic_Price"))
                                        If dblRate = 0 Then
                                            Throw New Exception("Please Fill Selling Price for Location " & location_Code & "  for item " & clsCommon.myCstr(objDBD.Item_Desc) & Environment.NewLine)
                                        End If
                                        objDBD.Rate = dblRate
                                        objDBD.ItemNetAmount = Math.Round(clsCommon.myCdbl(objDBD.Qty) * clsCommon.myCdbl(dblRate), 2)
                                        objDBD.TAX_Group = clsCommon.myCstr(dt2.Rows(0).Item("TAX_Group"))
                                        objDBD.TAX1 = clsCommon.myCstr(dt2.Rows(0).Item("TAX1"))
                                        objDBD.TAX1_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX1_Rate"))
                                        objDBD.TAX1_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX1_Rate / 100), 2)
                                        objDBD.TAX1_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX2 = clsCommon.myCstr(dt2.Rows(0).Item("TAX2"))
                                        objDBD.TAX2_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX2_Rate"))
                                        objDBD.TAX2_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX2_Rate / 100), 2)
                                        objDBD.TAX2_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX3 = clsCommon.myCstr(dt2.Rows(0).Item("TAX3"))
                                        objDBD.TAX3_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX3_Rate"))
                                        objDBD.TAX3_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX3_Rate / 100), 2)
                                        objDBD.TAX3_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX4 = clsCommon.myCstr(dt2.Rows(0).Item("TAX4"))
                                        objDBD.TAX4_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX4_Rate"))
                                        objDBD.TAX4_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX4_Rate / 100), 2)
                                        objDBD.TAX4_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX5 = clsCommon.myCstr(dt2.Rows(0).Item("TAX5"))
                                        objDBD.TAX5_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX5_Rate"))
                                        objDBD.TAX5_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX5_Rate / 100), 2)
                                        objDBD.TAX5_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX6 = clsCommon.myCstr(dt2.Rows(0).Item("TAX6"))
                                        objDBD.TAX6_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX6_Rate"))
                                        objDBD.TAX6_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX6_Rate / 100), 2)
                                        objDBD.TAX6_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX7 = clsCommon.myCstr(dt2.Rows(0).Item("TAX7"))
                                        objDBD.TAX7_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX7_Rate"))
                                        objDBD.TAX7_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX7_Rate / 100), 2)
                                        objDBD.TAX7_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX8 = clsCommon.myCstr(dt2.Rows(0).Item("TAX8"))
                                        objDBD.TAX8_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX8_Rate"))
                                        objDBD.TAX8_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX8_Rate / 100), 2)
                                        objDBD.TAX8_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX9 = clsCommon.myCstr(dt2.Rows(0).Item("TAX9"))
                                        objDBD.TAX9_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX9_Rate"))
                                        objDBD.TAX9_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX9_Rate / 100), 2)
                                        objDBD.TAX9_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX10 = clsCommon.myCstr(dt2.Rows(0).Item("TAX10"))
                                        objDBD.TAX10_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX10_Rate"))
                                        objDBD.TAX10_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX10_Rate / 100), 2)
                                        objDBD.TAX10_Base_Amt = objDBD.ItemNetAmount
                                        DObj.Add(objDBD)
                                    End If
                                    lineNo += 1
                                End If
                            End If
                        Next
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try
                            Dim obj As clsDemandBookingSale = clsDemandBookingSale.GetData(DocumentNO, NavigatorType.Current, trans)
                            qry = "delete from TSPL_DEMAND_BOOKING_DETAIL where tr_code in (select tr_code from TSPL_DEMAND_BOOKING_DETAIL where Document_No='" + obj.Document_No + "' and Cust_Code='" + strCustCode + "') "
                            clsDBFuncationality.ExecuteNonQuery(qry, trans)
                            clsDemandBookingSaleDetail.SaveData(DocumentNO, txtDate.Value, DObj, trans, location_Code, strShiftType, True, False, RouteNo)
                            clsDemandBookingSale.SaveDemandHistoryData(obj, DObj, "Demand Uploader", "ERP", objCommonVar.CurrentUserCode, trans)
                            trans.Commit()
                            'clsCommon.MyMessageBoxShow(Me, "Saved Successfully", Me.Text)
                            'LoadBlankGrid()
                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try
                    Else

                        Dim dblTotalAmount As Double = 0
                        Dim dblTotalCrate As Double = 0
                        Dim dblTotalltr As Double = 0

                        Dim DBObj As New clsDemandBookingSale
                        DBObj.Route_No = RouteNo
                        DBObj.Document_Date = txtDate.Value
                        DBObj.Location_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Location_Code from TSPL_Route_Master where Route_No='" + RouteNo + "' "))
                        DBObj.IsIndividualCustomer = 0
                        DBObj.City_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select City_Code from TSPL_ROUTE_MASTER where Route_No='" + RouteNo + "'"))
                        DBObj.ItemType = "Fresh"
                        DBObj.ShiftType = strShiftType
                        DBObj.Price_code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select tspl_customer_master.price_CodeNon from TSPL_CUSTOMER_MASTER where Cust_Code='" + strCustCode + "'"))
                        DBObj.TripNo = 1
                        DBObj.Arr = New List(Of clsDemandBookingSaleDetail)
                        Dim k As Integer = 1
                        Dim lineNo As Integer = 1
                        Dim DObj As New List(Of clsDemandBookingSaleDetail)
                        For dblcolumns As Integer = 3 To gv1.Columns.Count - 1
                            Dim obj1 As ItemValueClass = Nothing
                            'Dim objDBD As New clsDemandBookingSaleDetail
                            Try
                                obj1 = TryCast(gv1.Columns(colItemCode + clsCommon.myCstr(k)).Tag, ItemValueClass)
                            Catch ex As Exception
                            End Try
                            k = k + 1

                            If obj1 IsNot Nothing Then
                                If clsCommon.myLen(clsCommon.myCstr(obj1.itemCode)) > 0 AndAlso clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value) > 0 Then
                                    Dim objDBD As New clsDemandBookingSaleDetail
                                    objDBD.Line_No = lineNo
                                    objDBD.Document_No = DocumentNO
                                    objDBD.Cust_Code = strCustCode
                                    objDBD.Item_Code = clsCommon.myCstr(obj1.itemCode)
                                    objDBD.Item_Desc = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Short_Description from TSPL_ITEM_MASTER where Item_Code='" + clsCommon.myCstr(obj1.itemCode) + "'"))
                                    objDBD.Unit_code = clsCommon.myCstr(obj1.Unit_code)
                                    objDBD.Qty = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                    objDBD.Price_Code = DBObj.Price_code
                                    objDBD.ShiftType = strShiftType
                                    objDBD.Vehicle_Code = clsCommon.myCstr(clsDBFuncationality.getSingleValue("select Vehicle_No from TSPL_VEHICLE_MASTER left join TSPL_ROUTE_MASTER on TSPL_ROUTE_MASTER.vehicle_code=TSPL_VEHICLE_MASTER.Vehicle_Id where TSPL_ROUTE_MASTER.Route_No='" + RouteNo + "'"))
                                    objDBD.Trip_No = 1
                                    objDBD.TotalCrates_ItemWise = clsCommon.myCdbl(gv1.Rows(dblrows).Cells(dblcolumns).Value)
                                    Dim CrateConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objDBD.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code='Ltr' "))
                                    Dim ItemConvFactor_Ltr As Double = clsCommon.myCdbl(clsDBFuncationality.getSingleValue("select Conversion_Factor  from TSPL_ITEM_UOM_DETAIL Left Outer Join tspl_unit_master on tspl_unit_master.Unit_Code = TSPL_ITEM_UOM_DETAIL.UOM_Code Where TSPL_ITEM_UOM_DETAIL.Item_Code ='" & clsCommon.myCstr(objDBD.Item_Code) & "' and TSPL_ITEM_UOM_DETAIL.UOM_Code ='" & clsCommon.myCstr(objDBD.Unit_code) & "' "))
                                    If CrateConvFactor_Ltr > 0 And ItemConvFactor_Ltr > 0 Then
                                        Dim DispatchQty As Double = clsCommon.myCdbl(objDBD.Qty) * ItemConvFactor_Ltr
                                        objDBD.TotalLtr_ItemWise = (DispatchQty / CrateConvFactor_Ltr)
                                    End If
                                    qry = " Select Is_With_Tax, RowNo, Item_Price_ID, XXXE.Item_Code, UOM, Start_Date, Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, XXXE.Tax_group,XXXE.TAX1_Rate, " &
                                " XXXE.TAX2_Rate,XXXE.TAX3_Rate,XXXE.TAX4_Rate,XXXE.TAX5_Rate, " &
                                "  XXXE.TAX6_Rate,XXXE.TAX7_Rate,XXXE.TAX8_Rate,XXXE.TAX9_Rate, " &
                                " XXXE.TAX10_Rate,XXXE.TAX1 ,XXXE.TAX2,XXXE.TAX3, " &
                                " XXXE.TAX4,XXXE.TAX5,XXXE.TAX6,XXXE.TAX7, " &
                                " XXXE.TAX8,XXXE.TAX9,XXXE.TAX10,XXXE.TAX1_Amt, " &
                                " XXXE.TAX2_Amt,XXXE.TAX3_Amt,XXXE.TAX4_Amt, XXXE.TAX5_Amt,XXXE.TAX6_Amt,XXXE.TAX7_Amt,XXXE.TAX8_Amt,XXXE.TAX9_Amt,XXXE.TAX10_Amt,XXXE.Against_Plan_TR_Code  from ( " &
                                "Select ROW_NUMBER() OVER (Partition By TSPL_ITEM_PRICE_MASTER.Item_Code ORDER BY TSPL_ITEM_PRICE_MASTER.Item_Code,  " &
                                "Start_Date Desc) as RowNo,Is_With_Tax, Item_Price_ID, TSPL_ITEM_PRICE_MASTER.Item_Code, UOM, Start_Date,  " &
                                "Item_Basic_Price,Item_Basic_Net,Price_Code,Item_Selling_Price, TSPL_ITEM_PRICE_MASTER.Tax_group,TSPL_ITEM_PRICE_MASTER.TAX1_Rate,  " &
                                "TSPL_ITEM_PRICE_MASTER.TAX2_Rate,TSPL_ITEM_PRICE_MASTER.TAX3_Rate,TSPL_ITEM_PRICE_MASTER.TAX4_Rate,TSPL_ITEM_PRICE_MASTER.TAX5_Rate,  " &
                                " TSPL_ITEM_PRICE_MASTER.TAX6_Rate, TSPL_ITEM_PRICE_MASTER.TAX7_Rate, TSPL_ITEM_PRICE_MASTER.TAX8_Rate, TSPL_ITEM_PRICE_MASTER.TAX9_Rate, " &
                                " TSPL_ITEM_PRICE_MASTER.TAX10_Rate, TSPL_ITEM_PRICE_MASTER.TAX1, TSPL_ITEM_PRICE_MASTER.TAX2, TSPL_ITEM_PRICE_MASTER.TAX3, " &
                                " TSPL_ITEM_PRICE_MASTER.TAX4, TSPL_ITEM_PRICE_MASTER.TAX5, TSPL_ITEM_PRICE_MASTER.TAX6, TSPL_ITEM_PRICE_MASTER.TAX7, " &
                                " TSPL_ITEM_PRICE_MASTER.TAX8,TSPL_ITEM_PRICE_MASTER.TAX9,TSPL_ITEM_PRICE_MASTER.TAX10,TSPL_ITEM_PRICE_MASTER.TAX1_Amt , TSPL_ITEM_PRICE_MASTER.TAX2_Amt ,TSPL_ITEM_PRICE_MASTER.TAX3_Amt ,TSPL_ITEM_PRICE_MASTER.TAX4_Amt,TSPL_ITEM_PRICE_MASTER.TAX5_Amt,TSPL_ITEM_PRICE_MASTER.TAX6_Amt,TSPL_ITEM_PRICE_MASTER.TAX7_Amt,   TSPL_ITEM_PRICE_MASTER.TAX8_Amt,TSPL_ITEM_PRICE_MASTER.TAX9_Amt,TSPL_ITEM_PRICE_MASTER.TAX10_Amt,TSPL_ITEM_PRICE_MASTER.Against_Plan_TR_Code from TSPL_ITEM_PRICE_MASTER  left  outer join  " &
                                "TSPL_ITEM_UOM_DETAIL on TSPL_ITEM_PRICE_MASTER.Item_Code=TSPL_ITEM_UOM_DETAIL.Item_Code and  " &
                                "TSPL_ITEM_PRICE_MASTER.UOM=TSPL_ITEM_UOM_DETAIL.UOM_Code   where  Start_Date<='" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  and (End_Date >= '" & clsCommon.GetPrintDate(txtDate.Value, "dd/MMM/yyyy") & "'  or End_date is null)  and  " &
                                "TSPL_ITEM_PRICE_MASTER.Price_Code='" & objDBD.Price_Code & "' and UOM='" & objDBD.Unit_code & "' and TSPL_ITEM_PRICE_MASTER.item_code='" & objDBD.Item_Code & "' AND Location_Code='" & clsCommon.myCstr(DBObj.Location_Code) & "'  " &
                                ") XXXE WHERE RowNo=1  "
                                    Dim dt2 As DataTable = clsDBFuncationality.GetDataTable(qry)
                                    If dt2.Rows.Count > 0 Then
                                        Dim dblRate As Decimal = clsCommon.myCDecimal(dt2.Rows(0).Item("Item_Basic_Price"))
                                        If dblRate = 0 Then
                                            Throw New Exception("Please Fill Selling Price for Location " & DBObj.Location_Code & "  for item " & clsCommon.myCstr(objDBD.Item_Desc) & Environment.NewLine)
                                        End If
                                        objDBD.Rate = dblRate
                                        objDBD.ItemNetAmount = Math.Round(clsCommon.myCdbl(objDBD.Qty) * clsCommon.myCdbl(dblRate), 2)
                                        objDBD.TAX_Group = clsCommon.myCstr(dt2.Rows(0).Item("TAX_Group"))
                                        objDBD.TAX1 = clsCommon.myCstr(dt2.Rows(0).Item("TAX1"))
                                        objDBD.TAX1_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX1_Rate"))
                                        objDBD.TAX1_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX1_Rate / 100), 2)
                                        objDBD.TAX1_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX2 = clsCommon.myCstr(dt2.Rows(0).Item("TAX2"))
                                        objDBD.TAX2_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX2_Rate"))
                                        objDBD.TAX2_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX2_Rate / 100), 2)
                                        objDBD.TAX2_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX3 = clsCommon.myCstr(dt2.Rows(0).Item("TAX3"))
                                        objDBD.TAX3_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX3_Rate"))
                                        objDBD.TAX3_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX3_Rate / 100), 2)
                                        objDBD.TAX3_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX4 = clsCommon.myCstr(dt2.Rows(0).Item("TAX4"))
                                        objDBD.TAX4_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX4_Rate"))
                                        objDBD.TAX4_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX4_Rate / 100), 2)
                                        objDBD.TAX4_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX5 = clsCommon.myCstr(dt2.Rows(0).Item("TAX5"))
                                        objDBD.TAX5_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX5_Rate"))
                                        objDBD.TAX5_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX5_Rate / 100), 2)
                                        objDBD.TAX5_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX6 = clsCommon.myCstr(dt2.Rows(0).Item("TAX6"))
                                        objDBD.TAX6_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX6_Rate"))
                                        objDBD.TAX6_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX6_Rate / 100), 2)
                                        objDBD.TAX6_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX7 = clsCommon.myCstr(dt2.Rows(0).Item("TAX7"))
                                        objDBD.TAX7_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX7_Rate"))
                                        objDBD.TAX7_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX7_Rate / 100), 2)
                                        objDBD.TAX7_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX8 = clsCommon.myCstr(dt2.Rows(0).Item("TAX8"))
                                        objDBD.TAX8_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX8_Rate"))
                                        objDBD.TAX8_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX8_Rate / 100), 2)
                                        objDBD.TAX8_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX9 = clsCommon.myCstr(dt2.Rows(0).Item("TAX9"))
                                        objDBD.TAX9_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX9_Rate"))
                                        objDBD.TAX9_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX9_Rate / 100), 2)
                                        objDBD.TAX9_Base_Amt = objDBD.ItemNetAmount
                                        objDBD.TAX10 = clsCommon.myCstr(dt2.Rows(0).Item("TAX10"))
                                        objDBD.TAX10_Rate = clsCommon.myCdbl(dt2.Rows(0).Item("TAX10_Rate"))
                                        objDBD.TAX10_Amt = Math.Round(objDBD.ItemNetAmount * (objDBD.TAX10_Rate / 100), 2)
                                        objDBD.TAX10_Base_Amt = objDBD.ItemNetAmount
                                        DBObj.Arr.Add(objDBD)
                                        dblTotalAmount += objDBD.ItemNetAmount
                                        dblTotalCrate += objDBD.Qty
                                        dblTotalltr += objDBD.TotalLtr_ItemWise
                                    End If
                                End If
                            End If
                            lineNo += 1
                        Next
                        DBObj.TotalQtyInCrates = dblTotalCrate
                        DBObj.DocumentAmount = dblTotalAmount
                        DBObj.TotalQtyInLtr = dblTotalltr
                        Dim trans As SqlTransaction = clsDBFuncationality.GetTransactin()
                        Try
                            If clsDemandBookingSale.SaveData(DBObj, True, True, False, trans) Then
                                trans.Commit()
                            End If
                        Catch ex As Exception
                            trans.Rollback()
                            Throw New Exception(ex.Message)
                        End Try

                    End If
                Next
                clsCommon.ProgressBarHide()
            End If
        Catch ex As Exception
            clsCommon.ProgressBarHide()
            Throw New Exception(ex.Message)
        End Try
        Return True
    End Function
End Class
